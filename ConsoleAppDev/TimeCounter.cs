using System;
using System.Diagnostics;

namespace SortTests
{

  public class TimeCounter : System.Diagnostics.Stopwatch
  {
    private long _runningTicks = 0;
    private long _subTicks;
    private IntPtr _CurrentProcAff;

    public long RunningTicks
    {
      get { return _runningTicks; }
    }

    public long SubTicks
    {
      get { return _subTicks; }
    }

    /// <summary>
    /// Saves original Affinity mask value and
    /// sets Affinity to single processor to stablize results and
    /// </summary>
    /// <returns></returns>
    public void SetOneProcessorAffinity()
    {
      Process proc = System.Diagnostics.Process.GetCurrentProcess();
      _CurrentProcAff = proc.ProcessorAffinity;
      proc.ProcessorAffinity = (System.IntPtr)1;
    }

    public void RestoreProcessorAffinity()
    {
      Process proc = System.Diagnostics.Process.GetCurrentProcess();
      proc.ProcessorAffinity = _CurrentProcAff;
    }

    public new void StartNew()
    {
      _runningTicks = 0;
      base.Reset();
      base.Start();
    }

    public new void Stop()
    {
      base.Stop();
      _subTicks = base.ElapsedTicks;
      _runningTicks += base.ElapsedTicks;
      base.Reset();
    }

    /// <summary>
    /// Get Average Elaped Milliseconds for given number of iterations
    /// </summary>
    /// <param name="Iters"></param>
    /// <returns>Average of _runningTicks in rounded ms. 
    /// (rounded to nearest 10 if Iters <= 1)</returns>
    public long AvElapsedMilSec(int Iters)
    {
      double totalMS = (double)_runningTicks / Stopwatch.Frequency * 1000;
      if (Iters <= 1) { return RoundNear10s(totalMS); }
      return (long)Math.Round(totalMS / Iters);
    }

    /// <summary>
    /// Gets Elapsed milliseconds rounded to nearest 10s
    /// </summary>
    public long ElapsedMilSec
    {
      get { return RoundNear10s((double)_subTicks / Stopwatch.Frequency * 1000); }
    }

    /// <summary>
    /// Round to nearest 10s
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    private long RoundNear10s(double n)
    {
      n /= 10.0;
      return (long)(Math.Round(n) * 10);
    }
  }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SortTests
{
  public class ProcesserTime
  {
    private TimeSpan StartTime;
    private TimeSpan EndTime;
    private TimeSpan RunningTime;

    private Process _proc;

    public ProcesserTime(Process p)
    {
      _proc = p;
      StartTime = p.TotalProcessorTime;
    }

    public void StartNew()
    {
      RunningTime = TimeSpan.Zero;
      StartTime = _proc.TotalProcessorTime;
    }

    public void Stop()
    {
      EndTime = _proc.TotalProcessorTime;
      RunningTime += EndTime - StartTime;
    }

    public void Start()
    {
      StartTime = _proc.TotalProcessorTime;
    }

    public long ElapsedMilliseconds
    {
      get
      {
        return (long)(RunningTime.TotalMilliseconds);
      }
    }

    public long ElapsedTicks
    {
      get
      {
        return RunningTime.Ticks;
      }
    }

  }
}

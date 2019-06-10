using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using WpfControlDev.Extend;

namespace WpfControlDev.Threading.Working
{
    /// <summary>
    /// 基于任务的模式
    /// </summary>
    /// <typeparam name="TCommandArgs">命令参数</typeparam>
    /// <typeparam name="TResult">单元处理返回结果</typeparam>
    public class TAP<TWorkArgs, TResult> : IWork<TWorkArgs, TResult>
    {
        #region 私有字段
        private Func<TWorkArgs, TResult> work;           //工作单元处理委托
        private Action<IWorkResult<TResult>> uiAction;   //UI更新委托

        private IWorkResult<TResult> workResult;         //工作结果
        private WaitMode waitMode;                       //等待模式
        private IWorkResult[] preWorkUnitList;           //前置工作列表
        private SynchronizationContext synchronizationContext;
        private bool isSuccessPreWorkUnitList = true;
        #endregion

        #region 构造函数
        public TAP()
        {
            this.workResult = new WorkResult<TResult>()
            {
                IsComplete = false,
                IsError = false,
                Result = default(TResult)
            };
            this.synchronizationContext = SynchronizationContext.Current;
        }
        public TAP(Func<TWorkArgs, TResult> work)
            : this()
        {
            this.work = work;
        }
        #endregion

        #region IWork<TWorkArgs, TResult> 成员
        public IWorkResult<TResult> Handle(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            return this.Handle(() => { return args; }, uiAction, preWorkUnitList, mode);
        }

        public IWorkResult<TResult> Handle(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            this.waitMode = mode;
            this.uiAction = uiAction;
            this.preWorkUnitList = preWorkUnitList;
            this.workResult.IsError = false;
            this.workResult.IsComplete = false;

            Task<IWorkResult<TResult>> task = new Task<IWorkResult<TResult>>(
                o =>
                {
                    TResult result = default(TResult);
                    try
                    {
                        if (this.waitMode == WaitMode.Default || this.waitMode == WaitMode.Bll)
                        {
                            if (!WorkResult.Wait(this.preWorkUnitList))
                            {
                                this.isSuccessPreWorkUnitList = false;
                                return this.workResult;
                            }
                        }

                        result = this.work(args());
                        this.workResult.Result = result;
                    }
                    catch (DevException hsex)
                    {
                        this.workResult.IsError = true;
                        this.workResult.HsufErrorNo = hsex.ErrorNo;
                        this.workResult.ErrorMessage = hsex.Message;
                    }
                    catch (Exception exc)
                    {
                        this.workResult.IsError = true;
                        this.workResult.ErrorMessage = exc.Message;
                    }

                    return this.workResult;
                }, args);
            task.Start();
            task.ContinueWith(this.UpdateUI);

            return workResult;
        }

        public IWorkResult<TResult> HandleTick(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval)
        {
            return this.HandleTick(() => { return args; }, uiAction, preWorkUnitList, mode, interval);
        }

        public IWorkResult<TResult> HandleTick(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval)
        {
            this.workResult.IsComplete = true;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 0, 0, interval);
            timer.Tick += (sender, e) =>
            {
                if (this.workResult.IsComplete)
                {
                    this.Handle(args, uiAction, preWorkUnitList, mode);
                }
            };
            timer.Start();

            this.workResult.Timer = timer;
            return this.workResult;
        }
        #endregion

        #region 私有方法
        private void UpdateUI(Task<IWorkResult<TResult>> task)
        {
            if (this.uiAction == null)
            {
                this.workResult.IsComplete = true;
                return;
            }

            if (this.waitMode == WaitMode.UI)
            {
                if (!WorkResult.Wait(this.preWorkUnitList))
                {
                    this.isSuccessPreWorkUnitList = false;
                }
            }

            this.synchronizationContext.Post(UpdateUISafePost, task.Result);
        }
        private void UpdateUISafePost(object obj)
        {
            if (!this.isSuccessPreWorkUnitList)
            {
                this.workResult.IsError = true;
                this.workResult.ErrorMessage = "前置工作处理出错！";
            }

            try
            {
                this.uiAction((IWorkResult<TResult>)obj);
            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }

            this.workResult.IsComplete = true;
        }
        #endregion
    }
}
using System;
using WpfControlDev.Extend;
using WpfControlDev.Threading.Working;

namespace WpfControlDev.Threading
{
    /// <summary>
    /// 工作线程调用代理命令
    /// </summary>
    /// <typeparam name="TWorkArgs">命令参数</typeparam>
    /// <typeparam name="TResult">单元处理返回结果</typeparam>
    public class WorkCommand<TWorkArgs, TResult> : IWorkCommand<TWorkArgs, TResult>
    {
        #region 私有字段
        /// <summary>
        /// 工作执行类
        /// </summary>
        private IWork<TWorkArgs, TResult> work;

        /// <summary>
        /// 工作同步执行
        /// </summary>
        private Func<TWorkArgs, TResult> workSync;
        #endregion

        #region 构造函数
        public WorkCommand(Func<TWorkArgs, TResult> work)
            : this(work, WorkMode.TAP)
        { }
        public WorkCommand(Func<TWorkArgs, TResult> work, WorkMode workMode)
        {
            switch (workMode)
            {
                case WorkMode.APM:
                    this.work = new APM<TWorkArgs, TResult>(work);
                    break;
                case WorkMode.EAP:
                    this.work = new EAP<TWorkArgs, TResult>(work);
                    break;
                case WorkMode.TAP:
                    this.work = new TAP<TWorkArgs, TResult>(work);
                    break;
            }
            this.workSync = work;
        }
        #endregion

        #region IWorkCommand<TWorkArgs, TResult> 成员
        public IWorkResult<TResult> Execute(TWorkArgs args)
        {
            return this.Execute(args, (o) => { });
        }

        public IWorkResult<TResult> Execute(TWorkArgs args, Action<IWorkResult<TResult>> uiAction)
        {
            return this.Execute(args, uiAction, null);
        }

        public IWorkResult<TResult> Execute(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList)
        {
            return this.Execute(args, uiAction, preWorkUnitList, WaitMode.Default);
        }

        public IWorkResult<TResult> Execute(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            return this.work.Handle(args, uiAction, preWorkUnitList, mode);
        }

        public IWorkResult<TResult> Execute(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList)
        {
            return this.work.Handle(args, uiAction, preWorkUnitList, WaitMode.Default);
        }

        public IWorkResult<TResult> Execute(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            return this.work.Handle(args, uiAction, preWorkUnitList, mode);
        }

        public IWorkResult<TResult> ExecuteTick(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval)
        {
            return this.work.HandleTick(args, uiAction, preWorkUnitList, mode, interval);
        }

        public IWorkResult<TResult> ExecuteTick(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval)
        {
            return this.work.HandleTick(args, uiAction, preWorkUnitList, mode, interval);
        }

        public IWorkResult<TResult> ExecuteSync(TWorkArgs args)
        {
            return this.ExecuteSync(args, null);
        }

        public IWorkResult<TResult> ExecuteSync(TWorkArgs args, Action<IWorkResult<TResult>> uiAction)
        {
            return this.ExecuteSync(args, uiAction, null);
        }

        public IWorkResult<TResult> ExecuteSync(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList)
        {
            return this.ExecuteSync(args, uiAction, preWorkUnitList, WaitMode.Default);
        }

        public IWorkResult<TResult> ExecuteSync(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            return this.ExecuteSync(() => { return args; }, uiAction, preWorkUnitList, mode);
        }

        public IWorkResult<TResult> ExecuteSync(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            IWorkResult<TResult> workResult = new WorkResult<TResult>()
            {
                IsError = false,
                Result = default(TResult)
            };

            if (mode == WaitMode.Default || mode == WaitMode.Bll)
            {
                this.Wait(preWorkUnitList, workResult);
            }

            if (!workResult.IsError)
            {
                try
                {
                    TResult result = this.workSync(args());
                    workResult.Result = result;
                }
                catch (DevException hsex)
                {
                    workResult.IsError = true;
                    workResult.HsufErrorNo = hsex.ErrorNo;
                    workResult.ErrorMessage = hsex.Message;
                }
                catch (Exception exc)
                {
                    workResult.IsError = true;
                    workResult.ErrorMessage = exc.Message;
                }
            }

            if (!workResult.IsError)
            {
                if (mode == WaitMode.UI)
                {
                    this.Wait(preWorkUnitList, workResult);
                }
            }

            if (uiAction != null)
            {
                try
                {
                    uiAction(workResult);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }

            workResult.IsComplete = true;

            return workResult;
        }


        #endregion

        #region 私有方法
        private void Wait(IWorkResult[] preWorkUnitList, IWorkResult<TResult> workResult)
        {
            if (!WorkResult.Wait(preWorkUnitList))
            {
                workResult.IsError = true;
                workResult.ErrorMessage = "前置工作处理出错！";
            }
        }
        #endregion
    }
}
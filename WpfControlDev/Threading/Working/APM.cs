using System;

namespace WpfControlDev.Threading.Working
{
    /// <summary>
    /// 异步编程模型
    /// </summary>
    /// <typeparam name="TCommandArgs">命令参数</typeparam>
    /// <typeparam name="TResult">单元处理返回结果</typeparam>
    public class APM<TWorkArgs, TResult> : IWork<TWorkArgs, TResult>
    {
        #region 私有字段
        private Func<TWorkArgs, TResult> work;      //工作单元处理委托
        private Action<TResult> uiAction;           //UI更新委托

        private IWorkResult workResult;             //工作结果
        #endregion

        #region 构造函数
        public APM(Func<TWorkArgs, TResult> work)
        {
            this.work = work;
            this.workResult = new WorkResult();
        }
        #endregion

        #region IWork<TWorkArgs, TResult> 成员
        public IWorkResult<TResult> Handle(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            throw new NotImplementedException();
        }

        public IWorkResult<TResult> Handle(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode)
        {
            throw new NotImplementedException();
        }

        public IWorkResult<TResult> HandleTick(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval)
        {
            throw new NotImplementedException();
        }

        public IWorkResult<TResult> HandleTick(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
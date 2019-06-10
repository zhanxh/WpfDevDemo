using System;

namespace WpfControlDev.Threading.Working
{
    /// <summary>
    /// 工作服务
    /// </summary>
    public interface IWork<TWorkArgs, TResult>
    {
        /// <summary>
        /// 异步处理
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Handle(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        /// <summary>
        /// 异步处理
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Handle(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        IWorkResult<TResult> HandleTick(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval);
        IWorkResult<TResult> HandleTick(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval);
    }
}
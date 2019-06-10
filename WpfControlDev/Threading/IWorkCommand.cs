using System;

namespace WpfControlDev.Threading
{
    public interface IWorkCommand<TWorkArgs>
    {
        IWorkResult Execute(TWorkArgs args);

        IWorkResult Execute(TWorkArgs args, Action<IWorkResult> uiAction);

        IWorkResult Execute(TWorkArgs args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList);

        IWorkResult Execute(TWorkArgs args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        IWorkResult Execute(Func<TWorkArgs> args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList);

        IWorkResult Execute(Func<TWorkArgs> args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        IWorkResult ExecuteTick(TWorkArgs args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval);

        IWorkResult ExecuteTick(Func<TWorkArgs> args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval);

        IWorkResult ExecuteSync(TWorkArgs args, Action<IWorkResult> uiAction);

        IWorkResult ExecuteSync(TWorkArgs args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList);

        IWorkResult ExecuteSync(TWorkArgs args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        IWorkResult ExecuteSync(Func<TWorkArgs> args, Action<IWorkResult> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);
    }

    /// <summary>
    /// 线程调用代理命令
    /// </summary>
    /// <typeparam name="TWorkArgs">命令参数</typeparam>
    /// <typeparam name="TResult">单元处理返回结果</typeparam>
    public interface IWorkCommand<TWorkArgs, TResult>
    {
        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Execute(TWorkArgs args);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Execute(TWorkArgs args, Action<IWorkResult<TResult>> uiAction);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Execute(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Execute(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Execute(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> Execute(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        /// <summary>
        /// 间隔执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        /// <param name="interval">间隔时间（毫秒）</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> ExecuteTick(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval);
        
        /// <summary>
        /// 间隔执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        /// <param name="interval">间隔时间（毫秒）</param>
        /// <returns>工作结果</returns>
        IWorkResult<TResult> ExecuteTick(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode, int interval);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        IWorkResult<TResult> ExecuteSync(TWorkArgs args);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        IWorkResult<TResult> ExecuteSync(TWorkArgs args, Action<IWorkResult<TResult>> uiAction);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        IWorkResult<TResult> ExecuteSync(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        IWorkResult<TResult> ExecuteSync(TWorkArgs args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);

        /// <summary>
        /// 执行工作单元
        /// </summary>
        /// <param name="args">命令参数</param>
        /// <param name="uiAction">UI更新委托</param>
        /// <param name="preWorkUnitList">前置工作单元列表</param>
        /// <param name="mode">工作模式</param>
        IWorkResult<TResult> ExecuteSync(Func<TWorkArgs> args, Action<IWorkResult<TResult>> uiAction, IWorkResult[] preWorkUnitList, WaitMode mode);
    }
}

namespace WpfControlDev.Threading
{
    /// <summary>
    /// 工作模式
    /// </summary>
    public enum WorkMode
    {
        /// <summary>
        /// 异步编程模型
        /// </summary>
        APM,
        /// <summary>
        /// 基于事件的异步模式
        /// </summary>
        EAP,
        /// <summary>
        /// 基于任务的模式
        /// </summary>
        TAP
    }

    /// <summary>
    /// 等待模式
    /// </summary>
    public enum WaitMode
    {
        /// <summary>
        /// 默认等待模式，业务处理需要等待前置工作完成
        /// </summary>
        Default,
        /// <summary>
        /// 业务处理需要等待前置工作完成
        /// </summary>
        Bll,
        /// <summary>
        /// UI更新需要等待前置工作完成
        /// </summary>
        UI
    }
}
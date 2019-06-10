using System;
using System.Windows.Threading;

namespace WpfControlDev.Threading
{
    /// <summary>
    /// 工作结果
    /// </summary>
    public interface IWorkResult
    {
        /// <summary>
        /// 工作唯一编号
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// 是否完成工作
        /// </summary>
        bool IsComplete { get; set; }

        /// <summary>
        /// 是否有错误
        /// </summary>
        bool IsError { get; set; }

        /// <summary>
        /// HsufException异常包含的错误号
        /// </summary>
        string HsufErrorNo { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        string ErrorMessage { get; set; }

        /// <summary>
        /// 计时器
        /// </summary>
        DispatcherTimer Timer { get; set; }

        /// <summary>
        /// 停止间隔执行
        /// </summary>
        /// <param name="timer">计时器</param>
        void Stop(DispatcherTimer timer);

        /// <summary>
        /// 开始间隔执行
        /// </summary>
        /// <param name="timer">计时器</param>
        void Start(DispatcherTimer timer);
    }

    /// <summary>
    /// 工作结果
    /// </summary>
    /// <typeparam name="TResult">工作执行结果类型</typeparam>
    public interface IWorkResult<TResult> : IWorkResult
    {
        /// <summary>
        /// 工作执行结果
        /// </summary>
        TResult Result { get; set; }
    }
}
using System;
using System.Windows.Threading;

namespace WpfControlDev.Threading
{
    /// <summary>
    /// 工作结果
    /// </summary>
    public class WorkResult : IWorkResult
    {
        #region 私有字段
        private Guid id;  //唯一编号
        #endregion

        #region 构造函数
        public WorkResult()
        {
            this.id = Guid.NewGuid();
        }
        #endregion

        #region IWorkResult 成员
        public Guid Id
        {
            get { return this.id; }
        }

        public bool IsComplete { get; set; }

        public bool IsError { get; set; }

        public string HsufErrorNo { get; set; }
        public string ErrorMessage { get; set; }

        public DispatcherTimer Timer { get; set; }

        public void Stop(DispatcherTimer timer)
        {
            if (timer != null)
            {
                timer.Stop();
            }
        }

        public void Start(DispatcherTimer timer)
        {
            if (timer != null)
                timer.Start();
        }
        #endregion

        #region 静态方法
        /// <summary>
        /// 等待前置工作完成
        /// </summary>
        /// <param name="preWorkUnitList">前置工作列表</param>
        public static bool Wait(IWorkResult[] preWorkUnitList)
        {
            bool isSuccess = true;
            while (preWorkUnitList != null && true)
            {
                System.Threading.Thread.Sleep(1);
                bool isComplete = true;
                foreach (IWorkResult result in preWorkUnitList)
                {
                    if (result != null)
                    {
                        if (result.IsError)
                        {
                            isSuccess = false;
                            break;
                        }
                        if (!result.IsComplete)
                        {
                            isComplete = false;
                            break;
                        }
                    }
                }
                if (isComplete || !isSuccess)
                    break;
            }
            return isSuccess;
        }
        #endregion
    }

    /// <summary>
    /// 工作结果
    /// </summary>
    /// <typeparam name="TResult">工作执行结果类型</typeparam>
    public class WorkResult<TResult> : WorkResult, IWorkResult<TResult>
    {
        #region IWorkResult<TResult> 成员
        public TResult Result { get; set; }
        #endregion
    }
}
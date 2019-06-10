using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace WpfControlDev.Threading
{
    /// <summary>
    /// 工作执行管理
    /// </summary>
    public sealed class WorkManager
    {
        #region 私有字段
        private List<IWorkResult> workList = null;
        #endregion

        #region 构造函数
        public WorkManager()
        {
            this.workList = new List<IWorkResult>();
        }
        #endregion

        #region 公共方法
        /// <summary>
        /// 添加工作执行结果
        /// </summary>
        /// <param name="workResult">工作执行结果</param>
        /// <returns>工作执行管理类</returns>
        public WorkManager Add(IWorkResult workResult)
        {
            this.workList.Add(workResult);

            return this;
        }

        /// <summary>
        /// 清理所有工作执行结果
        /// </summary>
        /// <returns>工作执行管理类</returns>
        public WorkManager Clear()
        {
            foreach (IWorkResult wr in this.workList)
            {
                if (wr.Timer != null)
                {
                    wr.Stop(wr.Timer);
                }
            }

            this.workList.Clear();
            return this;
        }
        #endregion

        #region 公共静态方法
        /// <summary>
        /// 统一执行
        /// </summary>
        /// <param name="action">执行委托</param>
        public static DispatcherTimer TickDo(Func<IWorkResult[]> workGroupAction, int interval)
        {
            IWorkResult[] arrWorkResult = workGroupAction();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = new TimeSpan(interval);
            timer.Tick += (sender, e) =>
            {
                if (IsComplete(arrWorkResult) && workGroupAction != null)
                {
                    arrWorkResult = workGroupAction();
                }
            };
            timer.Start();

            return timer;
        }

        /// <summary>
        /// 开始间隔执行
        /// </summary>
        public static void Start(DispatcherTimer timer)
        {
            if (timer != null)
                timer.Start();
        }

        /// <summary>
        /// 停止间隔执行
        /// </summary>
        public static void Stop(DispatcherTimer timer)
        {
            if (timer != null)
                timer.Stop();
        }
        #endregion

        #region 私有方法
        /// <summary>
        /// 判断任务列表是否都已完成
        /// </summary>
        /// <param name="arrWorkResult">任务列表</param>
        /// <returns>是否完成</returns>
        private static bool IsComplete(IWorkResult[] arrWorkResult)
        {
            if (arrWorkResult != null)
            {
                foreach (IWorkResult wr in arrWorkResult)
                {
                    if (!wr.IsComplete)
                        return false;
                }
            }

            return true;
        }
        #endregion
    }
}
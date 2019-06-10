using System;

namespace WpfControlDev.Extend
{
    /// <summary>
    /// 表示错误发生在HSUF框架里
    /// </summary>
    [Serializable]
    public class DevException : Exception
    {
        #region 属性
        /// <summary>
        /// 错误号
        /// </summary>
        public string ErrorNo { get; set; }
        #endregion

        #region 构造函数
        /// <summary>
        /// 初始化一个新的<c>HsufException</c>实例
        /// </summary>
        public DevException() : base() {

        }

        /// <summary>
        /// 初始化一个新的<c>HsufException</c>实例
        /// </summary>
        /// <param name="message">错误信息</param>
        public DevException(string errorNo, string message)
            : base(message)
        {
            this.ErrorNo = errorNo;
        }

        /// <summary>
        /// 初始化一个新的<c>HsufException</c>实例
        /// </summary>
        /// <param name="message">错误信息</param>
        /// <param name="innerException">内部异常</param>
        public DevException(string errorNo, string message, Exception innerException)
            : base(message, innerException)
        {
            this.ErrorNo = errorNo;
        }

        /// <summary>
        /// 初始化一个新的<c>HsufException</c>实例
        /// </summary>
        /// <param name="format">格式化字符串</param>
        /// <param name="args">格式化参数</param>
        public DevException(string format, params object[] args) : base(string.Format(format, args)) { }
        #endregion
    }
}
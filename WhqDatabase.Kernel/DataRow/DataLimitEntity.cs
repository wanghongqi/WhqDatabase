using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhqDatabase.Kernel.DataRow
{
    /// <summary>
    /// 截取
    /// </summary>
    public class DataLimitEntity
    {
        /// <summary>
        /// 开始位置
        /// </summary>
        public int Start { get; set; }
        /// <summary>
        /// 长度
        /// </summary>
        public int Length { get; set; }
    }
}

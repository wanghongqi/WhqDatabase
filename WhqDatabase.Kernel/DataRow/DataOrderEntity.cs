using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhqDatabase.Kernel.DataRow
{
    /// <summary>
    /// 排序实体
    /// </summary>
    public class DataOrderEntity
    {
        /// <summary>
        /// 列
        /// </summary>
        public string[] Columns { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public Comparison<DataRowEntity> Comparison { get; set; }
    }
}

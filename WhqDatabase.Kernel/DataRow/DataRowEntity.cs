using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataContent;

namespace WhqDatabase.Kernel.DataRow
{
    /// <summary>
    /// 数据行实体
    /// </summary>
    public class DataRowEntity
    {
        /// <summary>
        /// 表
        /// </summary>
        public DataTableEntity Table { get; set; }
        /// <summary>
        /// 列
        /// </summary>
        public string[] Columns { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public object[] Contents { get; set; }
    }
}

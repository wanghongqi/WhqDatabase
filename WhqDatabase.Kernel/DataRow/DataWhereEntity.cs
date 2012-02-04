using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhqDatabase.Kernel.DataRow
{
    /// <summary>
    /// 条件
    /// </summary>
    public class DataWhereEntity
    {
        /// <summary>
        /// 列
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public Func<object, bool> Predicate { get; set; }
    }
}

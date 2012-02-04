using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WhqDatabase.Kernel.DataPage
{
    /// <summary>
    /// 页面类型枚举
    /// </summary>
    public enum EPageType
    {
        /// <summary>
        /// 空
        /// </summary>
        Empty=0,
        Database=1,
        DataFile=2,
        DataTable=6,
        DataColumn=7,
        DataContent=11
    }
}

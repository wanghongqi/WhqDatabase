/*
 * WhqDatabase
 * http://longtianyu1.blog.163.com/
 * 
 * Copyright 2012,Wang Hongqi(王洪岐,longtianyu1@163.com)
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 * Author: Wang Hongqi(王洪岐,longtianyu1@163.com)
 * Date: 2012-02-04
 */
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

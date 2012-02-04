/*!
 * WhqDatabase
 * http://longtianyu1.blog.163.com/
 * 
 * Copyright 2012,Wang Hongqi(王洪岐)
 * Dual licensed under the MIT or GPL Version 2 licenses.
 *
 * Author: Wang Hongqi(王洪岐)
 * Date: 2012-02-04
 */
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

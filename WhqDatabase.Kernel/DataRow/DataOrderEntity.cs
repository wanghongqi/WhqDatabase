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

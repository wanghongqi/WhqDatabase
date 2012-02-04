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
using WhqDatabase.Kernel.DataPage;

namespace WhqDatabase.Kernel.DataContent
{
    /// <summary>
    /// 内容位置
    /// </summary>
    public class ContentPoint
    {
        /// <summary>
        /// 分页编号
        /// </summary>
        public long ContentPageID { get; set; }
        /// <summary>
        /// 分页顺序编号
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 在分页中位置
        /// </summary>
        public int ContentIndex { get; set; }
        /// <summary>
        /// 获取行号
        /// </summary>
        /// <param name="length">长度</param>
        /// <returns></returns>
        public int GetRowIndex(int length = Config.ROWINDEX_LENGTH)
        {
            return ((Config.PAGE_SIZE - ContentPageEntity.CONTENT_START) / length) * PageIndex + ContentIndex;
        }
    }
}

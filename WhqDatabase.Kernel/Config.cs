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

namespace WhqDatabase.Kernel
{
    /// <summary>
    /// 配置
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// 页大小=32M
        /// </summary>
        public const int PAGE_SIZE = 32 * 1024;
        /// <summary>
        /// 数据库信息页起始编号
        /// </summary>
        public const int DATABASE_PAGE_ID = 0;
        /// <summary>
        /// 数据文件信息页起始编号
        /// </summary>
        public const int DATAFILE_PAGE_ID = 1;
        /// <summary>
        /// 数据表信息页起始编号
        /// </summary>
        public const int DATATABLE_PAGE_ID = 2;

        /// <summary>
        /// 行索引长度
        /// </summary>
        public const int ROWINDEX_LENGTH = 0x1;
    }
}

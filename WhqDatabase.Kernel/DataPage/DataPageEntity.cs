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
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.Database;

namespace WhqDatabase.Kernel.DataPage
{
    /// <summary>
    /// 数据页
    /// </summary>
    public class DataPageEntity
    {
        /// <summary>
        /// 数据文件
        /// </summary>
        public DataFileEntity DataFile { get; set; }
        /// <summary>
        /// 所在数据库
        /// </summary>
        public DatabaseEntity Database { get { return DataFile.Database; } }
        
        /// <summary>
        /// 编号
        /// </summary>
        public long ID { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public byte[] Content { get; set; }
    }
}

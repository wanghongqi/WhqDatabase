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
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;

namespace WhqDatabase.Test
{
    public class WhqHelper
    {
        public static DatabaseEntity modelD;
        /// <summary>
        /// 数据库初始化
        /// </summary>
        public static void DatabaseInit()
        {
            modelD = new DatabaseEntity
            {
                Name = Config.DatabaseName,
                MasterFile = new DataFileEntity { FilePath = Config.DataFilePath }
            };
            DatabaseManage.Delete(modelD);
            DatabaseManage.Create(modelD);
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        public static void DatabaseOpen()
        {
            modelD = new DatabaseEntity
            {
                MasterFile = new DataFileEntity { FilePath = Config.DataFilePath }
            };
            DatabaseManage.Read(modelD);
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void DatabaseClose()
        {
            DatabaseManage.Close(modelD);
        }
    }
}

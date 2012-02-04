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
using System.Web;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataContent;
using WhqDatabase.Kernel.DataRow;

namespace WhqDatabase.Service.Common
{
    /// <summary>
    /// 主数据库
    /// </summary>
    public class DBMaster
    {
        /// <summary>
        /// 主数据库编号
        /// </summary>
        public const int DBMASTER_ID = 1;
        /// <summary>
        /// 主数据库对象
        /// </summary>
        public static DatabaseEntity DB;
        /// <summary>
        /// 创建
        /// </summary>
        public static void Create()
        {
            DB = new DatabaseEntity
            {
                Name = Config.DBMasterName,
                MasterFile = new DataFileEntity { FilePath = Config.DBMasterPath }
            };
            DatabaseManage.Create(DB);
            //建表
            DataTableEntity modelDT = new DataTableEntity { Name = "Database" };
            DataTableManage.Create(DB, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", Type = EColumnType.User, DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Name", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnEntity modelDC3 = new DataColumnEntity { Name = "Path", Type = EColumnType.User, DataType = EContentType.String, DataLength = 1024 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);
            DataColumnManage.Create(modelDT, modelDC3);
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = new string[] { "ID", "Name", "Path" }, Contents = new object[] { DBMASTER_ID, DB.Name, DB.MasterFile.FilePath } });
            DatabaseManage.Close(DB);
        }
        /// <summary>
        /// 打开数据库
        /// </summary>
        public static void Open()
        {
            DB = new DatabaseEntity
            {
                MasterFile = new DataFileEntity { FilePath = Config.DBMasterPath }
            };
            DatabaseManage.Read(DB);
            DB.ID = DBMASTER_ID;
        }
        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void Close()
        {
            DatabaseManage.Close(DB);
        }
    }
}
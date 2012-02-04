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
using System.Web;
using System.IO;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataRow;
using WhqDatabase.Kernel.DataFile;

namespace WhqDatabase.Service.Common
{
    /// <summary>
    /// 控制
    /// </summary>
    public class Manage
    {
        /// <summary>
        /// 是否正在运行
        /// </summary>
        public static bool IsRun { get; set; }
        /// <summary>
        /// 数据库列表
        /// </summary>
        public static List<DatabaseEntity> ListDB { get; set; }
        /// <summary>
        /// 启动
        /// </summary>
        public static void Start()
        {
            if (!File.Exists(Config.DBMasterPath))
            {
                DBMaster.Create();
            }
            //打开主数据库
            DBMaster.Open();
            ListDB = new List<DatabaseEntity>();
            ListDB.Add(DBMaster.DB);
            //寻找其他数据库
            DataTableEntity modelDT = new DataTableEntity { Name = "Database" };
            modelDT = DataTableManage.Read(DBMaster.DB, modelDT);
            string[] listColumn = new string[] { "Name" };
            List<DataRowEntity> listDR = DataRowManage.Read(modelDT,
                new string[] { "ID", "Name", "Path" } ,
                new DataWhereEntity[] { new DataWhereEntity { ColumnName = "ID", Predicate = info => Convert.ToInt32(info) != 1 } });
            //逐个打开
            foreach (DataRowEntity modelDR in listDR)
            {
                DatabaseEntity modelDB = new DatabaseEntity
                {
                    MasterFile = new DataFileEntity { FilePath = modelDR.Contents[2].ToString() }
                };
                DatabaseManage.Read(modelDB);
                modelDB.ID = Convert.ToInt32(modelDR.Contents[0]);
                ListDB.Add(modelDB);
            }
            IsRun = true;
        }
        /// <summary>
        /// 停止
        /// </summary>
        public static void Stop()
        {
            DBMaster.Close();
            IsRun = false;
        }
        /// <summary>
        /// 移除数据库
        /// </summary>
        /// <param name="modelDB"></param>
        public static void DBRemove(DatabaseEntity modelDB)
        {
            DataTableEntity modelDT = new DataTableEntity { Name = "Database" };
            modelDT = DataTableManage.Read(DBMaster.DB, modelDT);
            DataRowManage.Delete(modelDT, new DataWhereEntity[] { new DataWhereEntity { ColumnName = "ID", Predicate = info => Convert.ToInt32(info) == modelDB.ID } });
            ListDB.Remove(modelDB);
            modelDB.MasterFile.FileStream.Close();
        }
        /// <summary>
        /// 附加数据库
        /// </summary>
        /// <param name="modelDB"></param>
        public static void DBAdd(DatabaseEntity modelDB)
        {
            DataTableEntity modelDT = new DataTableEntity { Name = "Database" };
            modelDT = DataTableManage.Read(DBMaster.DB, modelDT);
            Manage.ListDB.Add(modelDB);
            List<DataRowEntity> listDR = DataRowManage.Read(modelDT, new string[] { "ID" });
            modelDB.ID = (listDR.Count == 0) ? 1 : listDR.Max(info => Convert.ToInt32(info.Contents[0])) + 1;
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = new string[] { "ID", "Name", "Path" }, Contents = new object[] { modelDB.ID, modelDB.Name, modelDB.MasterFile.FilePath } });
        }
    }
}
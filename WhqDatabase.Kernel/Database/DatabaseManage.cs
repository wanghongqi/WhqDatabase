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
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.DataPage;

namespace WhqDatabase.Kernel.Database
{
    /// <summary>
    /// 数据库
    /// </summary>
    public class DatabaseManage
    {
        /// <summary>
        /// 创建
        /// </summary>
        public static void Create(DatabaseEntity model)
        {
            model.MasterFile.Database = model;
            DataFileManage.Create(model.MasterFile);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public static void Update(DatabaseEntity model)
        {
            DataPageManage.Write(new DataPageEntity { DataFile = model.MasterFile, ID = Config.DATABASE_PAGE_ID, Content = model.ToBytes() });
            DataFileManage.Update(model.MasterFile);
        }
        /// <summary>
        /// 读取
        /// </summary>
        public static void Read(DatabaseEntity model)
        {
            model.MasterFile.Database = model;
            //读主文件
            DataFileManage.Read(model.MasterFile);
            //读数据库信息
            DataPageEntity modelD = new DataPageEntity { DataFile = model.MasterFile, ID = Config.DATABASE_PAGE_ID };
            DataPageManage.Read(modelD);
            model.FromBytes(modelD.Content);
        }
        /// <summary>
        /// 删除
        /// </summary>
        public static void Delete(DatabaseEntity model)
        {
            DataFileManage.Delete(model.MasterFile);
        }
        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="model"></param>
        public static void Close(DatabaseEntity model)
        {
            model.MasterFile.FileStream.Close();
            model = null;
        }
    }
}

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
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;

namespace WhqDatabase.Test
{
    /// <summary>
    /// 数据库测试
    /// </summary>
    [TestClass]
    public class TDatabase
    {
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void DatabaseCreate()
        {
            DatabaseEntity modelD = new DatabaseEntity
            {
                Name = Config.DatabaseName,
                MasterFile = new DataFileEntity { FilePath = Config.DataFilePath }
            };
            DatabaseManage.Delete(modelD);
            DatabaseManage.Create(modelD);
            DatabaseManage.Close(modelD);
        }
        /// <summary>
        /// 修改
        /// </summary>
        [TestMethod]
        public void DatabaseUpdate()
        {
            DatabaseCreate();
            DatabaseEntity modelD = new DatabaseEntity { MasterFile = new DataFileEntity { FilePath = Config.DataFilePath } };
            DatabaseManage.Read(modelD);
            modelD.Name = "TestUpdate";
            DatabaseManage.Update(modelD);
            DatabaseManage.Close(modelD);

            modelD = new DatabaseEntity { MasterFile = new DataFileEntity { FilePath = Config.DataFilePath } };
            DatabaseManage.Read(modelD);
            Assert.AreEqual(modelD.Name, "TestUpdate");
            DatabaseManage.Close(modelD);
        }
        /// <summary>
        /// 读取
        /// </summary>
        [TestMethod]
        public void DatabaseRead()
        {
            DatabaseCreate();
            DatabaseEntity modelD = new DatabaseEntity { MasterFile = new DataFileEntity { FilePath = Config.DataFilePath } };
            DatabaseManage.Read(modelD);
            DatabaseManage.Close(modelD);
        }
        /// <summary>
        /// 删除
        /// </summary>
        [TestMethod]
        public void DatabaseDelete()
        {
            DatabaseEntity modelD = new DatabaseEntity { MasterFile = new DataFileEntity { FilePath = Config.DataFilePath } };
            DatabaseManage.Delete(modelD);
        }
    }
}

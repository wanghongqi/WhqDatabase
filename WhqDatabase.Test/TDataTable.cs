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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.DataTable;

namespace WhqDatabase.Test
{
    /// <summary>
    /// 表测试
    /// </summary>
    [TestClass]
    public class TDataTable
    {
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void DataTableCreate()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 更新
        /// </summary>
        [TestMethod]
        public void DataTableUpdate()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            modelDT.Name = "TestTable2";
            DataTableManage.Update(modelDT);
            DataTableManage.Read(WhqHelper.modelD, modelDT);
            Assert.AreEqual(modelDT.Name, "TestTable2");
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 读取
        /// </summary>
        [TestMethod]
        public void DataTableRead()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            WhqHelper.DatabaseClose();

            WhqHelper.DatabaseOpen();
            modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT=DataTableManage.Read(WhqHelper.modelD, modelDT);
            Assert.IsNotNull(modelDT);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 删除
        /// </summary>
        [TestMethod]
        public void DataTableDelete()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataTableManage.Delete(modelDT);
            WhqHelper.DatabaseClose();

            WhqHelper.DatabaseOpen();
            modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            Assert.IsNull(modelDT);
            Assert.AreEqual(DataFileManage.GetEmptyPageID(WhqHelper.modelD.MasterFile), 3);
            WhqHelper.DatabaseClose();
        }
    }
}

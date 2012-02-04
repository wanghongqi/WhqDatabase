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
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase;
using WhqDatabase.Kernel.DataContent;
using WhqDatabase.Kernel.DataRow;

namespace WhqDatabase.Test
{
    /// <summary>
    /// 表测试
    /// </summary>
    [TestClass]
    public class TDataColumn
    {
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void DataColumnCreate()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", DataType = EContentType.String, DataLength = 32 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 名称更新
        /// </summary>
        [TestMethod]
        public void DataColumnUpdateName()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", DataType = EContentType.String, DataLength = 32 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);

            DataColumnManage.Update(modelDC2, new DataColumnEntity { Name = "TestColumn2", DataLength = modelDC2.DataLength, DataType = modelDC2.DataType });
            DataColumnManage.Read(modelDT, modelDC2);
            Assert.AreEqual(modelDC2.Name, "TestColumn2");
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 类型更新
        /// </summary>
        [TestMethod]
        public void DataColumnUpdateType()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", DataType = EContentType.Int, DataLength = 4 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);

            DataColumnManage.Update(modelDC2, new DataColumnEntity { Name = "TestColumn2", DataType = EContentType.String, DataLength = 32 });
            DataColumnManage.Read(modelDT, modelDC2);
            Assert.AreEqual(modelDC2.Name, "TestColumn2");
            Assert.AreEqual(modelDC2.DataType, EContentType.String);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 带数据类型更新
        /// </summary>
        [TestMethod]
        public void DataColumnUpdateTypeWithData()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", DataType = EContentType.Int, DataLength = 4 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);

            string[] listColumn = new string[] { "ID", "Column"};
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 1, 1 } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 2, 2 } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 3, 3 } });

            DataColumnManage.Update(modelDC2, new DataColumnEntity { Name = "TestColumn2", DataType = EContentType.String, DataLength = 32 });
            DataColumnManage.Read(modelDT, modelDC2);
            Assert.AreEqual(modelDC2.Name, "TestColumn2");
            Assert.AreEqual(modelDC2.DataType, EContentType.String);
            Assert.AreEqual(DataRowManage.ReadCount(modelDT), 3);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 读取
        /// </summary>
        [TestMethod]
        public void DataColumnRead()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", DataType = EContentType.String, DataLength = 32 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);
            WhqHelper.DatabaseClose();

            WhqHelper.DatabaseOpen();
            modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT=DataTableManage.Read(WhqHelper.modelD, modelDT);
            modelDC1 = new DataColumnEntity { Name = "ID"};
            modelDC1 = DataColumnManage.Read(modelDT, modelDC1);
            Assert.IsNotNull(modelDC1);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 删除
        /// </summary>
        [TestMethod]
        public void DataColumnDelete()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", DataType = EContentType.String, DataLength = 32 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);
            DataColumnManage.Delete(modelDC1);
            WhqHelper.DatabaseClose();

            WhqHelper.DatabaseOpen();
            modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            modelDC1 = new DataColumnEntity { Name = "ID" };
            modelDC1 = DataColumnManage.Read(modelDT, modelDC1);
            Assert.IsNull(modelDC1);
            WhqHelper.DatabaseClose();
        }
    }
}

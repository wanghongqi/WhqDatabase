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
using WhqDatabase.Kernel.DataRow;
using WhqDatabase.Kernel.DataContent;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;

namespace WhqDatabase.Test
{
    /// <summary>
    /// 数据行
    /// </summary>
    [TestClass]
    public class TDataRow
    {
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void DataRowCreate()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", Type = EColumnType.User, DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Name", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnEntity modelDC3 = new DataColumnEntity { Name = "Content", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);
            DataColumnManage.Create(modelDT, modelDC3);
            string[] listColumn = new string[] { "ID", "Name","Content" };
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 1, "whq", "Conten123t" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 2, "zyp", "11111111b" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 3, "z423yp", "Cosdfntent" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 4, "sdf4", "Contsdfent" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 5, "z423yp", "Co3entent" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 6, "sdf4", "Con4213tent" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 7, "z423yp", "Cosdafntent" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 8, "zyp", "22222222a" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 9, "z423yp", "Contentsdf" } });
            DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { 10, "sdf4", "234Content" } });
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void DataRowCreateSome()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", Type = EColumnType.User, DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Name", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnEntity modelDC3 = new DataColumnEntity { Name = "Content", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);
            DataColumnManage.Create(modelDT, modelDC3);
            string[] listColumn = new string[] { "ID", "Name", "Content" };
            for (int i = 0; i < 50; i++)
            {
                DataRowManage.Create(modelDT, new DataRowEntity { Columns = listColumn, Contents = new object[] { i + 1, "name", (i + 1) + "Content" } });
            }
            Assert.AreEqual(DataRowManage.ReadCount(modelDT), 50);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 修改
        /// </summary>
        [TestMethod]
        public void DataRowUpdate()
        {
            DataRowCreate();
            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            DataRowManage.Update(modelDT, new DataRowEntity { Columns = new string[] { "Name", "Content" }, Contents = new object[] { "hahahha", "ConHaHaHa" } }, new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } });
            List<DataRowEntity> listDR = DataRowManage.Read(modelDT, new string[] { "ID", "Name", "Content" }, new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } });
            Assert.AreEqual(listDR.Count, 0);
            listDR = DataRowManage.Read(modelDT, new string[] { "ID", "Name", "Content"  }, new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "hahahha" } });
            Assert.AreEqual(listDR[0].Contents[0], 2);
            Assert.AreEqual(listDR[0].Contents[1], "hahahha");
            Assert.AreEqual(listDR[0].Contents[2], "ConHaHaHa");
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 读取
        /// </summary>
        [TestMethod]
        public void DataRowRead()
        {
            DataRowCreate();
            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            List<DataRowEntity> listDR = DataRowManage.Read(modelDT,new string[] { "ID", "Name", "Content" } , new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } });
            Assert.AreEqual(listDR.Count, 2);
            Assert.AreEqual(listDR[0].Contents[0], 2);
            Assert.AreEqual(listDR[0].Contents[1], "zyp");
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 读取排序
        /// </summary>
        [TestMethod]
        public void DataRowReadOrder()
        {
            DataRowCreate();
            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            //Content倒序
            List<DataRowEntity> listDR = DataRowManage.Read(modelDT, new string[] { "ID", "Name", "Content" },
                new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } },
                new DataOrderEntity { Columns = new string[] { "Content" }, Comparison = (x, y) => -string.Compare(x.Contents[0].ToString(), y.Contents[0].ToString()) });
            Assert.AreEqual(listDR[0].Contents[2], "22222222a");
            //Content正序
            listDR = DataRowManage.Read(modelDT, new string[] { "ID", "Name", "Content" },
                new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } },
                new DataOrderEntity { Columns = new string[] { "Content" }, Comparison = (x, y) => string.Compare(x.Contents[0].ToString(), y.Contents[0].ToString()) });
            Assert.AreEqual(listDR[0].Contents[2], "11111111b");
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 读取排序截取
        /// </summary>
        [TestMethod]
        public void DataRowReadOrderLimit()
        {
            DataRowCreate();
            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            List<DataRowEntity> listDR = DataRowManage.Read(modelDT, new string[] { "ID", "Name", "Content" },
                new DataWhereEntity[] { new DataWhereEntity { ColumnName = "ID", Predicate = info => Convert.ToInt32(info) > 2 } },
                new DataOrderEntity { Columns = new string[] { "ID" }, Comparison = (x, y) => Convert.ToInt32(x.Contents[0]) - Convert.ToInt32(y.Contents[0]) },
                new DataLimitEntity { Start = 1, Length = 4 });
            Assert.AreEqual(listDR.Count, 4);
            Assert.AreEqual(listDR[0].Contents[0], 4);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 删除
        /// </summary>
        [TestMethod]
        public void DataRowDelete()
        {
            DataRowCreate();
            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            DataRowManage.Delete(modelDT, new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } });

            List<DataRowEntity> listDR = DataRowManage.Read(modelDT, new string[] { "ID", "Name" } , new DataWhereEntity[] { new DataWhereEntity { ColumnName = "Name", Predicate = info => info.ToString() == "zyp" } });
            Assert.AreEqual(listDR.Count, 0);
            WhqHelper.DatabaseClose();
        }
    }
}

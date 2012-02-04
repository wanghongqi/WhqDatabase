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
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase;
using WhqDatabase.Kernel.DataContent;

namespace WhqDatabase.Test
{
    /// <summary>
    /// 内容测试
    /// </summary>
    [TestClass]
    public class TDataContent
    {
        /// <summary>
        /// 获取第一个空位
        /// </summary>
        [TestMethod]
        public void GetFirstEmpty()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID", Type = EColumnType.User, DataType = EContentType.Byte, DataLength = 1 };
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "Column", Type = EColumnType.User, DataType = EContentType.String, DataLength = 32 };
            DataColumnManage.Create(modelDT, modelDC1);
            DataColumnManage.Create(modelDT, modelDC2);

            ContentPoint cp= DataContentManage.GetFirstEmpty(modelDT);
            Assert.AreEqual(cp.ContentPageID, 4);
            Assert.AreEqual(cp.ContentIndex, 0);
            Assert.AreEqual(cp.PageIndex, 0);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void ContentCreate()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "ID", Type = EColumnType.User, DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC3 = new DataColumnEntity { Name = "Name", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnManage.Create(modelDT, modelDC2);
            DataColumnManage.Create(modelDT, modelDC3);
            //添加内容
            int RowIndex = DataContentManage.GetFirstEmpty(modelDT).GetRowIndex();
            DataContentManage.Create(modelDT.Columns[0], new DataContentEntity { Content = 1 }, RowIndex);
            DataContentManage.Create(modelDC2, new DataContentEntity { Content = 1 },RowIndex);
            DataContentManage.Create(modelDC3, new DataContentEntity { Content = "whq" }, RowIndex);

            RowIndex = DataContentManage.GetFirstEmpty(modelDT).GetRowIndex();
            DataContentManage.Create(modelDT.Columns[0], new DataContentEntity { Content = 1 }, RowIndex);
            DataContentManage.Create(modelDC2, new DataContentEntity { Content = 2 }, RowIndex);
            DataContentManage.Create(modelDC3, new DataContentEntity { Content = "zyp" }, RowIndex);

            RowIndex = DataContentManage.GetFirstEmpty(modelDT).GetRowIndex();
            DataContentManage.Create(modelDT.Columns[0], new DataContentEntity { Content = 1 }, RowIndex);
            DataContentManage.Create(modelDC2, new DataContentEntity { Content = 3 }, RowIndex);
            DataContentManage.Create(modelDC3, new DataContentEntity { Content = "xxx" }, RowIndex);

            Assert.AreEqual(DataContentManage.GetFirstEmpty(modelDT).GetRowIndex(), 3);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 创建
        /// </summary>
        [TestMethod]
        public void ContentCreateSome()
        {
            WhqHelper.DatabaseInit();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            DataTableManage.Create(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC2 = new DataColumnEntity { Name = "ID", Type = EColumnType.User, DataType = EContentType.Int, DataLength = 4 };
            DataColumnEntity modelDC3 = new DataColumnEntity { Name = "Name", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnEntity modelDC4 = new DataColumnEntity { Name = "Content", Type = EColumnType.User, DataType = EContentType.String, DataLength = 0x100 };
            DataColumnManage.Create(modelDT, modelDC2);
            DataColumnManage.Create(modelDT, modelDC3);
            DataColumnManage.Create(modelDT, modelDC4);
            //添加内容
            int RowIndex = DataContentManage.GetFirstEmpty(modelDT).GetRowIndex();
            for (int i = 0; i < 50; i++)
            {
                DataContentManage.Create(modelDT.Columns[0], new DataContentEntity { Content = 1 }, RowIndex);
                DataContentManage.Create(modelDC2, new DataContentEntity { Content = i }, RowIndex);
                DataContentManage.Create(modelDC3, new DataContentEntity { Content = "whq" }, RowIndex);
                DataContentManage.Create(modelDC4, new DataContentEntity { Content = "whq is Good!" }, RowIndex);
                RowIndex++;
            }

            Assert.AreEqual(DataContentManage.Read(modelDT.Columns[0], info => true).Count(), 50);

            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 改
        /// </summary>
        [TestMethod]
        public void ContentUpdate()
        {
            ContentCreate();

            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "Name" };
            modelDC1 = DataColumnManage.Read(modelDT, modelDC1);
            DataContentEntity modelDContent = DataContentManage.Read(modelDC1, info => info.Content.ToString() == "xxx").First();
            modelDContent.Content = "xxxxx";
            DataContentManage.Update(modelDContent);

            IEnumerable<DataContentEntity> result = DataContentManage.Read(modelDC1, info => info.Content.ToString() == "xxx");
            Assert.AreEqual(result.Count(), 0);
            result = DataContentManage.Read(modelDC1, info => info.Content.ToString() == "xxxxx");
            Assert.AreEqual(result.Count(), 1);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 读
        /// </summary>
        [TestMethod]
        public void ContentRead()
        {
            ContentCreate();

            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT=DataTableManage.Read(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "ID"};
            modelDC1 = DataColumnManage.Read(modelDT, modelDC1);
            IEnumerable<DataContentEntity> result = DataContentManage.Read(modelDC1, info => Convert.ToInt32(info.Content) == 2);
            Assert.AreEqual(result.First().ContentIndex, 1);
            result = DataContentManage.Read(modelDC1, info => Convert.ToInt32(info.Content) > 1);
            Assert.AreEqual(result.Count(), 2);
            Assert.AreEqual(DataContentManage.Read(modelDT.Columns[0], info => true).Count(),3);
            WhqHelper.DatabaseClose();
        }
        /// <summary>
        /// 删
        /// </summary>
        [TestMethod]
        public void ContentDelete()
        {
            ContentCreate();

            WhqHelper.DatabaseOpen();
            DataTableEntity modelDT = new DataTableEntity { Name = "TestTable" };
            modelDT = DataTableManage.Read(WhqHelper.modelD, modelDT);
            DataColumnEntity modelDC1 = new DataColumnEntity { Name = "Name" };
            modelDC1 = DataColumnManage.Read(modelDT, modelDC1);
            IEnumerable<int> listDC = DataContentManage.Read(modelDC1, info => info.Content.ToString() == "whq").Select(info => info.RowIndex);
            //删除行
            foreach (DataContentEntity modelDC in DataContentManage.Read(modelDT.Columns[0], info => listDC.Contains(info.RowIndex)))
            {
                DataContentManage.Delete(modelDC);
            }

            Assert.AreEqual(DataContentManage.GetFirstEmpty(modelDT).GetRowIndex(), 0);
            Assert.AreEqual(DataContentManage.Read(modelDC1, info => info.Content.ToString() == "whq").Count(), 0);
            WhqHelper.DatabaseClose();
        }
    }
}

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
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.DataContent;

namespace WhqDatabase.Kernel.DataColumn
{
    /// <summary>
    /// 列管理
    /// </summary>
    public class DataColumnManage
    {
        /// <summary>
        /// 创建
        /// </summary>
        public static void Create(DataTableEntity modelDT, DataColumnEntity modelDC)
        {
            //赋值
            modelDC.ColumnPage = modelDT.ColumnPage;
            modelDC.Table = modelDT;
            //生成编号
            if (modelDC.Type == EColumnType.RowIndex)
            {
                modelDC.ID = 0;
            }
            else
            {
                modelDC.ID = modelDT.Columns.Max(info => info.ID) + 1;
            }
            //列信息编号
            modelDC.ContentPageID = DataFileManage.GetEmptyPageID(modelDT.DataFile);
            //写入列
            modelDC.ColumnPage.Columns.Add(modelDC);
            modelDC.ColumnPage.DataPage.Content = modelDC.ColumnPage.ToBytes();
            DataPageManage.Write(modelDC.ColumnPage.DataPage);
            //表用户列
            if (modelDC.Type == EColumnType.User)
            {
                modelDT.UserColumns.Add(modelDC);
            }
            //创建数据分页
            byte[] array = new byte[Config.PAGE_SIZE];
            array[0] = Convert.ToByte(EPageType.DataContent);
            DataPageManage.Write(new DataPageEntity { DataFile = modelDT.DataFile, ID = modelDC.ContentPageID, Content = array });
            //更新文件
            DataFileManage.Update(modelDT.DataFile);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public static void Update(DataColumnEntity modelDC,DataColumnEntity newDC)
        {
            if (modelDC.DataLength!= newDC.DataLength || modelDC.DataType!=newDC.DataType)//改变列类型或长度
            {
                List<DataContentEntity> listDC = DataContentManage.Read(modelDC, info => true);
                //清空列的内容页
                long ContentPageID = modelDC.ContentPageID;
                while (ContentPageID > 0)
                {
                    DataPageEntity modelDP = new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = ContentPageID };
                    ContentPageID = DataContentManage.ReadNextPageID(modelDP);
                    DataPageManage.Clear(modelDP);
                }
                //创建数据分页
                byte[] array = new byte[Config.PAGE_SIZE];
                array[0] = Convert.ToByte(EPageType.DataContent);
                DataPageManage.Write(new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = modelDC.ContentPageID, Content = array });
                //改变列
                modelDC.Name = newDC.Name;
                modelDC.DataType = newDC.DataType;
                modelDC.DataLength = newDC.DataLength;
                modelDC.ColumnPage.DataPage.Content = modelDC.ColumnPage.ToBytes();
                DataPageManage.Write(modelDC.ColumnPage.DataPage);
                //写入数据
                for (int i = 0; i < listDC.Count; i++)
                {
                    DataContentManage.Create(modelDC, listDC[i], i);
                }
            }
            else if (modelDC.Name != newDC.Name)//只改变列名
            {
                modelDC.Name = newDC.Name;
                modelDC.ColumnPage.DataPage.Content = modelDC.ColumnPage.ToBytes();
                DataPageManage.Write(modelDC.ColumnPage.DataPage);
            }
        }
        /// <summary>
        /// 读取
        /// </summary>
        public static DataColumnEntity Read(DataTableEntity modelDT, DataColumnEntity modelDC)
        {
            foreach (DataColumnEntity modelTemp in modelDT.Columns.Where(info => info.Name == modelDC.Name))
            {
                return modelTemp;
            }
            return null;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public static void Delete(DataColumnEntity modelDC)
        {
            //清空列的内容页
            long ContentPageID = modelDC.ContentPageID;
            while (ContentPageID > 0)
            {
                DataPageEntity modelDP = new DataPageEntity { DataFile = modelDC.Table.DataFile, ID = ContentPageID };
                ContentPageID = DataContentManage.ReadNextPageID(modelDP);
                DataPageManage.Clear(modelDP);
            }
            modelDC.ColumnPage.Columns.Remove(modelDC);
            modelDC.Table.UserColumns.Remove(modelDC);
            modelDC.ColumnPage.DataPage.Content = modelDC.ColumnPage.ToBytes();
            DataPageManage.Write(modelDC.ColumnPage.DataPage);
            modelDC = null;
        }
    }
}

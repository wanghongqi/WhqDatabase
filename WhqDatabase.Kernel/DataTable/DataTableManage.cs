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
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataContent;
using WhqDatabase.Kernel.DataFile;

namespace WhqDatabase.Kernel.DataTable
{
    /// <summary>
    /// 数据表管理
    /// </summary>
    public class DataTableManage
    {
        /// <summary>
        /// 创建
        /// </summary>
        public static void Create(DatabaseEntity modelD,DataTableEntity modelDT)
        {
            modelDT.TablePage = modelD.MasterFile.TablePage;
            modelDT.ID = modelD.Tables.Count == 0 ? 1 : modelD.Tables.Max(info => info.ID) + 1;
            //列信息编号
            modelDT.ColumnPageID = DataFileManage.GetEmptyPageID(modelD.MasterFile);
            //写入表
            modelDT.TablePage.Tables.Add(modelDT);
            modelDT.TablePage.DataPage.Content = modelDT.TablePage.ToBytes();
            DataPageManage.Write(modelDT.TablePage.DataPage);
            //创建列分页
            ColumnPageEntity modelCP = new ColumnPageEntity { Columns = new List<DataColumnEntity>(),  Table = modelDT };
            DataPageEntity modelDP = new DataPageEntity { DataFile = modelD.MasterFile, ID = modelDT.ColumnPageID ,Content = modelCP.ToBytes()};
            DataPageManage.Write(modelDP);
            //赋值
            modelCP.DataPage = modelDP;
            modelDT.ColumnPage = modelCP;
            modelDT.UserColumns = new List<DataColumnEntity>();
            //创建系统列
            DataColumnEntity modelDC = new DataColumnEntity { Name = EColumnType.RowIndex.ToString(), Type = EColumnType.RowIndex, DataType = EContentType.Byte, DataLength = Config.ROWINDEX_LENGTH };
            DataColumnManage.Create(modelDT, modelDC);
        }
        /// <summary>
        /// 修改
        /// </summary>
        public static void Update(DataTableEntity modelDT)
        {
            modelDT.TablePage.DataPage.Content = modelDT.TablePage.ToBytes();
            DataPageManage.Write(modelDT.TablePage.DataPage);
        }
        /// <summary>
        /// 读取
        /// </summary>
        public static DataTableEntity Read(DatabaseEntity modelD, DataTableEntity modelDT)
        {
            foreach(DataTableEntity modelTemp in modelD.Tables.Where(info=>info.Name==modelDT.Name))
            {
                return modelTemp;
            }
            return null;
        }
        /// <summary>
        /// 删除
        /// </summary>
        public static void Delete(DataTableEntity modelDT)
        {
            //逐个删除列
            for (int i = 0; i < modelDT.Columns.Count;i++ )
            {
                DataColumnManage.Delete(modelDT.Columns[i]);
            }
            //删除列空间
            DataPageManage.Clear(new DataPageEntity { DataFile = modelDT.DataFile, ID = modelDT.ColumnPageID });
            //移除表
            TablePageEntity modelTP = modelDT.TablePage;
            modelTP.Tables.Remove(modelDT);
            modelDT = null;
            modelTP.DataPage.Content = modelTP.ToBytes();
            DataPageManage.Write(modelTP.DataPage);
        }
        /// <summary>
        /// 获取表中行索引列表
        /// </summary>
        /// <param name="modelDT"></param>
        public static List<int> GetRowIndexList(DataTableEntity modelDT)
        {
            List<int> listRowIndex = new List<int>();
            long ContentPageID = modelDT.Columns[0].ContentPageID;
            int PageIndex = 0;
            while (ContentPageID > 0)
            {
                ContentPageEntity modelCP = DataContentManage.ReadContentPage(new DataPageEntity { DataFile = modelDT.DataFile, ID = ContentPageID }, new ContentPageEntity { Column = modelDT.Columns[0], PageIndex = PageIndex });
                PageIndex++;
                ContentPageID = modelCP.NextPageID;
                listRowIndex.AddRange(modelCP.Contents.Select(info => info.RowIndex));
            }
            return listRowIndex;
        }
    }
}

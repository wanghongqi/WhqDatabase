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
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.DataContent;
using WhqDatabase.Kernel.Common;

namespace WhqDatabase.Kernel.DataPage
{
    /// <summary>
    /// 内容页实体
    /// </summary>
    public class ContentPageEntity
    {
        /// <summary>
        /// 下一页编号起始位置
        /// </summary>
        public const int NEXTPAGEID_START = 8;
        /// <summary>
        /// 内容起始位置
        /// </summary>
        public const int CONTENT_START = 0x20;
        /// <summary>
        /// 所在分页
        /// </summary>
        public DataPageEntity DataPage { get; set; }
        /// <summary>
        /// 所在表
        /// </summary>
        public DataTableEntity Table { get { return Column.Table; } }
        /// <summary>
        /// 所在列
        /// </summary>
        public DataColumnEntity Column { get; set; }
        /// <summary>
        /// 内容
        /// </summary>
        public List<DataContentEntity> Contents { get; set; }
        /// <summary>
        /// 页号
        /// </summary>
        public int PageIndex { get; set; }
        /// <summary>
        /// 当前页编号
        /// </summary>
        public long PageID { get { return DataPage.ID; } }
        /// <summary>
        /// 下一页编号
        /// </summary>
        public long NextPageID { get; set; }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="arr"></param>
        public void FromBytes(byte[] arr)
        {
            NextPageID = BitConverter.ToInt64(arr, NEXTPAGEID_START);
            Contents = new List<DataContentEntity>();
            if (Column.Type == EColumnType.RowIndex)
            {
                for (int i = 0; i < (Config.PAGE_SIZE - CONTENT_START) / Column.DataLength; i++)
                {
                    byte[] temp = new byte[Column.DataLength];
                    temp = ByteUtil.ReadBytes(arr, i * Column.DataLength + CONTENT_START, Column.DataLength);
                    if (ByteUtil.IsNotZero(temp))
                    {
                        DataContentEntity modelDT = new DataContentEntity();
                        modelDT.ContentPage = this;
                        modelDT.ContentIndex = i;
                        modelDT.Column = Column;
                        modelDT.FromBytes(temp);
                        Contents.Add(modelDT);
                    }
                }
            }
            else
            {
                //获取行索引
                List<int> listRowIndex = DataTableManage.GetRowIndexList(Table);
                for (int i = 0; i < Column.PageMaxCount; i++)
                {
                    if (listRowIndex.Contains(PageIndex * Column.PageMaxCount + i))
                    {
                        byte[] temp = new byte[Column.DataLength];
                        temp = ByteUtil.ReadBytes(arr, i * Column.DataLength + CONTENT_START, Column.DataLength);
                        DataContentEntity modelDT = new DataContentEntity();
                        modelDT.ContentPage = this;
                        modelDT.ContentIndex = i;
                        modelDT.Column = Column;
                        modelDT.FromBytes(temp);
                        Contents.Add(modelDT);
                    }
                }
            }
        }
    }
}

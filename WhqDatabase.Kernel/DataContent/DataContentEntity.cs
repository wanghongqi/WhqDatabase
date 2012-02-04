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
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataPage;
using WhqDatabase.Kernel.DataColumn;

namespace WhqDatabase.Kernel.DataContent
{
    /// <summary>
    /// 数据内容实体
    /// </summary>
    public class DataContentEntity
    {
        /// <summary>
        /// 所在表
        /// </summary>
        public DataTableEntity Table { get { return Column.Table; } }
        /// <summary>
        /// 所在内容分页
        /// </summary>
        public ContentPageEntity ContentPage { get; set; }
        /// <summary>
        /// 所在列
        /// </summary>
        public DataColumnEntity Column { get; set; }
        /// <summary>
        /// 在分页中位置
        /// </summary>
        public int ContentIndex { get; set; }
        /// <summary>
        /// 行号
        /// </summary>
        public int RowIndex
        {
            get
            {
                return new ContentPoint { PageIndex = ContentPage.PageIndex, ContentIndex = ContentIndex }
                    .GetRowIndex(WhqDatabase.Kernel.Config.ROWINDEX_LENGTH);
            }
        }
        /// <summary>
        /// 内容
        /// </summary>
        public object Content { get; set; }
        /// <summary>
        /// 转成比特数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] array = new byte[Column.DataLength];
            ContentType.GetBytes(Column.DataType, Content).CopyTo(array, 0);
            return array;
        }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="arr"></param>
        public void FromBytes(byte[] arr)
        {
            Content = ContentType.GetObject(Column.DataType, arr);
        }
    }
}

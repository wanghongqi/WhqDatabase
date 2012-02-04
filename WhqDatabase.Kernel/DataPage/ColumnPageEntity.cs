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
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Kernel.Common;
using WhqDatabase.Kernel.DataTable;

namespace WhqDatabase.Kernel.DataPage
{
    /// <summary>
    /// 列页实体
    /// </summary>
    public class ColumnPageEntity
    {
        /// <summary>
        /// 下一页编号起始位置
        /// </summary>
        public const int NEXTPAGEID_START = 8;
        /// <summary>
        /// 所在分页
        /// </summary>
        public DataPageEntity DataPage { get; set; }
        /// <summary>
        /// 所在表
        /// </summary>
        public DataTableEntity Table { get; set; }
        /// <summary>
        /// 包含列
        /// </summary>
        public List<DataColumnEntity> Columns { get; set; }
        /// <summary>
        /// 下一页编号
        /// </summary>
        public long NextPageID { get; set; }

        /// <summary>
        /// 转成byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] array = new byte[Config.PAGE_SIZE];
            array[0] = Convert.ToByte(EPageType.DataColumn);
            BitConverter.GetBytes(NextPageID).CopyTo(array, NEXTPAGEID_START);
            for (int i = 0; i < Columns.Count; i++)
            {
                Columns[i].ToBytes().CopyTo(array, (i + 1) * DataColumnEntity.DATA_SIZE);
            }
            return array;
        }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="arr"></param>
        public void FromBytes(byte[] arr)
        {
            NextPageID = BitConverter.ToInt64(arr, NEXTPAGEID_START);
            Columns = new List<DataColumnEntity>();
            for (int i = 1; i < Config.PAGE_SIZE / DataColumnEntity.DATA_SIZE; i++)
            {
                byte[] temp = new byte[DataColumnEntity.DATA_SIZE];
                temp = ByteUtil.ReadBytes(arr, i * DataColumnEntity.DATA_SIZE, DataColumnEntity.DATA_SIZE);
                if (ByteUtil.IsNotZero(temp))
                {
                    DataColumnEntity modelDT = new DataColumnEntity();
                    modelDT.ColumnPage = this;
                    modelDT.Table = Table;
                    modelDT.FromBytes(temp);
                    Columns.Add(modelDT);
                }
            }
        }
    }
}

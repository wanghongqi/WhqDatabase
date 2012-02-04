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
using System.IO;
using WhqDatabase.Kernel.Database;
using WhqDatabase.Kernel.DataPage;

namespace WhqDatabase.Kernel.DataFile
{
    /// <summary>
    /// 数据文件实体
    /// </summary>
    public class DataFileEntity
    {
        /// <summary>
        /// 最大分页编号起始位置
        /// </summary>
        public const int MAXPAGEID_START = 8;
        /// <summary>
        /// 所在数据库
        /// </summary>
        public DatabaseEntity Database { get; set; }
        /// <summary>
        /// 表页
        /// </summary>
        public TablePageEntity TablePage { get; set; }
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 最大分页编号
        /// </summary>
        public long MaxPageID { get; set; }
        /// <summary>
        /// 文件流
        /// </summary>
        public FileStream FileStream { get; set; }
        /// <summary>
        /// 转成byte[]
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] array = new byte[Config.PAGE_SIZE];
            array[0] = Convert.ToByte(EPageType.DataFile);
            BitConverter.GetBytes(MaxPageID).CopyTo(array, MAXPAGEID_START);
            return array;
        }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="arr"></param>
        public void FromBytes(byte[] arr)
        {
            MaxPageID = BitConverter.ToInt64(arr, MAXPAGEID_START);
        }
    }
}

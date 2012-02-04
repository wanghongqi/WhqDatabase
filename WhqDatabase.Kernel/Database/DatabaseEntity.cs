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
using WhqDatabase.Kernel.DataFile;
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Kernel.DataPage;

namespace WhqDatabase.Kernel.Database
{
    /// <summary>
    /// 数据库实体
    /// </summary>
    public class DatabaseEntity
    {
        /// <summary>
        /// 名称起始位置
        /// </summary>
        public const int NAME_START = 0x8;
        /// <summary>
        /// 名称长度
        /// </summary>
        public const int NAME_LENGTH = 0xff;
        /// <summary>
        /// 在当前数据库系统中编号(不储存在文件中)
        /// </summary>
        public int ID { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 主数据文件
        /// </summary>
        public DataFileEntity MasterFile { get; set; }
        /// <summary>
        /// 数据表列表
        /// </summary>
        public List<DataTableEntity> Tables { get { return MasterFile.TablePage.Tables; } }
        /// <summary>
        /// 转成比特数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBytes()
        {
            byte[] array = new byte[Config.PAGE_SIZE];
            array[0] = Convert.ToByte(EPageType.Database);
            Encoding.UTF8.GetBytes(Name).CopyTo(array, NAME_START);
            return array;
        }
        /// <summary>
        /// 从byte[]中读取
        /// </summary>
        /// <param name="arr"></param>
        public void FromBytes(byte[] arr)
        {
            Name = Encoding.UTF8.GetString(arr, NAME_START, NAME_LENGTH).TrimEnd('\0');
        }
    }
}

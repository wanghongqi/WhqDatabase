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
using WhqDatabase.Kernel.DataFile;
using System.IO;

namespace WhqDatabase.Kernel.DataPage
{
    /// <summary>
    /// 页文件管理
    /// </summary>
    public class DataPageManage
    {
        /// <summary>
        /// 写入
        /// </summary>
        /// <param name="model">页实体</param>
        /// <param name="start">页中起始位置</param>
        /// <param name="length">长度</param>
        public static void Write(DataPageEntity model,int start=0, int length=Config.PAGE_SIZE)
        {
            model.DataFile.FileStream.Seek(model.ID * Config.PAGE_SIZE + start, SeekOrigin.Begin);
            model.DataFile.FileStream.Write(model.Content, 0, length);
        }
        /// <summary>
        /// 读取到model.Content中
        /// </summary>
        /// <param name="model">页实体</param>
        /// <param name="start">页中起始位置</param>
        /// <param name="length">长度</param>
        public static void Read(DataPageEntity model, int start = 0, int length = Config.PAGE_SIZE)
        {
            model.DataFile.FileStream.Seek(model.ID * Config.PAGE_SIZE + start, SeekOrigin.Begin);
            model.Content = new byte[length];
            model.DataFile.FileStream.Read(model.Content, 0, length);
        }
        /// <summary>
        /// 清空页
        /// </summary>
        /// <param name="model"></param>
        public static void Clear(DataPageEntity model)
        {
            model.Content = new byte[] { Convert.ToByte(EPageType.Empty) };
            Write(model, 0, 1);
        }
    }
}

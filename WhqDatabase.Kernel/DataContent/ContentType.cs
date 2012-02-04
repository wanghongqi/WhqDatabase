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

namespace WhqDatabase.Kernel.DataContent
{
    /// <summary>
    /// 数据类型枚举
    /// </summary>
    public enum EContentType
    {
        /// <summary>
        /// 单字符
        /// </summary>
        Byte=1,
        /// <summary>
        /// 整型
        /// </summary>
        Int=2,
        /// <summary>
        /// 长整型
        /// </summary>
        Long=3,
        /// <summary>
        /// 字符型
        /// </summary>
        String=4
    }
    public class ContentType
    {
        /// <summary>
        /// 获取类型
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public static EContentType GetType(int index)
        {
            return (EContentType)Enum.Parse(typeof(EContentType), Enum.GetName(typeof(EContentType), index));
        }
        /// <summary>
        /// 根据数据类型获取byte[]
        /// </summary>
        /// <param name="t"></param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static byte[] GetBytes(EContentType t, object obj)
        {
            switch (t)
            {
                case EContentType.Byte:
                    return new byte[] { Convert.ToByte(obj) };
                case EContentType.Int:
                    return BitConverter.GetBytes(Convert.ToInt32(obj));
                case EContentType.Long:
                    return BitConverter.GetBytes(Convert.ToInt64(obj));
                case EContentType.String:
                    return Encoding.UTF8.GetBytes(Convert.ToString(obj));
                default:
                    return null;
            }
        }

        public static object GetObject(EContentType t, byte[] arr)
        {
            switch (t)
            {
                case EContentType.Byte:
                    return arr[0];
                case EContentType.Int:
                    return BitConverter.ToInt32(arr,0);
                case EContentType.Long:
                    return BitConverter.ToInt64(arr,0);
                case EContentType.String:
                    return Encoding.UTF8.GetString(arr).TrimEnd('\0');
                default:
                    return null;
            }
        }
    }
}

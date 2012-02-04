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

namespace WhqDatabase.Kernel.Common
{
    /// <summary>
    /// byte工具类
    /// </summary>
    public class ByteUtil
    {
        /// <summary>
        /// 读取数组中Byte[]
        /// </summary>
        /// <param name="array">数组</param>
        /// <param name="offset">开始偏移量</param>
        /// <param name="count">总长度，超出部分将截取掉</param>
        /// <returns></returns>
        public static byte[] ReadBytes(byte[] array, int offset, int count)
        {
            byte[] arrOut = new byte[count];
            for (int i = 0; i < count; i++)
            {
                arrOut[i] = array[i + offset];
            }
            return arrOut;
        }
        /// <summary>
        /// byte[]中有值
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static bool IsNotZero(byte[] array)
        {
            foreach (byte b in array)
            {
                if (b > 0) return true;
            }
            return false;
        }
    }
}

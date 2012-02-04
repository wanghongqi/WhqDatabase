using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhqDatabase.Service.Common
{
    /// <summary>
    /// 数组工具类
    /// </summary>
    public class ArrayUtil
    {
        /// <summary>
        /// 对象数组转字符串数组
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        public static string[] ArrObjToStr(object[] arr)
        {
            string[] arrReturn = new string[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                arrReturn[i] = arr[i].ToString();
            }
            return arrReturn;
        }
    }
}
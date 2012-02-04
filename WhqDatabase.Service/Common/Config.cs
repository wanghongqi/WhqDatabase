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
using System.Web;
using System.Configuration;

namespace WhqDatabase.Service.Common
{
    /// <summary>
    /// 配置
    /// </summary>
    public class Config
    {
        /// <summary>
        /// 主数据库名
        /// </summary>
        public const string DBMasterName = "Master";
        /// <summary>
        /// 主数据库路径
        /// </summary>
        public static string DBMasterPath
        {
            get { return ConfigurationManager.AppSettings["DBMasterPath"].ToString(); }
        }
    }
}
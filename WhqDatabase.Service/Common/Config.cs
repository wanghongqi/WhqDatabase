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
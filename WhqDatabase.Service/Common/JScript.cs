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
using System.Web.UI;

namespace WhqDatabase.Service.Common
{
    /// <summary>
    /// 脚本
    /// </summary>
    public class JScript
    {
        /// <summary>
        /// 显示消息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public static void ShowMessage(Page page, string script)
        {
            ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(),"alert('"+ script+"！')", true);
        }
        /// <summary>
        /// 输出脚本
        /// </summary>
        /// <param name="page"></param>
        /// <param name="script"></param>
        public static void ResponseScript(Page page, string script)
        {
            ScriptManager.RegisterStartupScript(page, typeof(Page), Guid.NewGuid().ToString(), script, true);
        }
    }
}
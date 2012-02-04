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
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WhqDatabase.Kernel.DataColumn;
using WhqDatabase.Service.Common;

namespace WhqDatabase.Service
{
    public partial class DataColumnEdit : System.Web.UI.Page
    {
        /// <summary>
        /// 数据库对象
        /// </summary>
        protected DataColumnEntity Column;
        /// <summary>
        /// 页面初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Init(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["DatabaseID"]) 
                && !string.IsNullOrEmpty(Request.QueryString["DataTableID"])
                && !string.IsNullOrEmpty(Request.QueryString["DataColumnID"]))
            {
                Column = Manage.ListDB.First(info => info.ID == Convert.ToInt32(Request.QueryString["DatabaseID"])).Tables.First(info => info.ID == Convert.ToInt32(Request.QueryString["DataTableID"])).Columns.First(info => info.ID == Convert.ToInt32(Request.QueryString["DataColumnID"]));
            }
        }
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
    }
}
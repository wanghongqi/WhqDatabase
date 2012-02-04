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
using WhqDatabase.Kernel.DataTable;
using WhqDatabase.Service.Common;
using WhqDatabase.Kernel.DataRow;

namespace WhqDatabase.Service
{
    public partial class Menu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            rptDatabase.DataSource = Manage.ListDB;
            rptDatabase.DataBind();
        }
    }
}
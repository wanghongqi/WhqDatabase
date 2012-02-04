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
using System.Web.Security;
using System.Web.SessionState;

namespace WhqDatabase.Service
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            Common.Manage.Start();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {
            Common.Manage.Stop();
        }
    }
}
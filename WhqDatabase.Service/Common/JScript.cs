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
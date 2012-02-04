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
using System.Web.UI.WebControls;

namespace WhqDatabase.Service.UserControl
{
    public delegate void PageBarChangeNum(object sender, PageBarChangeNumArgs e);
    public partial class PageBar : System.Web.UI.UserControl
    {
        /// <summary>
        /// 页号
        /// </summary>
        public int PageNum
        {
            get { return Convert.ToInt32(ViewState[this.ID+"_PageNum"]); }
            set { ViewState[this.ID + "_PageNum"] = value; }
        }
        /// <summary>
        /// 总行数
        /// </summary>
        public int RowCount
        {
            get { return Convert.ToInt32(ViewState[this.ID + "_RowCount"]); }
            set
            {
                ViewState[this.ID + "_RowCount"] = value;
                ContentBind();
            }
        }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int PageSize
        {
            get { return Convert.ToInt32(ViewState[this.ID + "_PageSize"]); }
            set { ViewState[this.ID + "_PageSize"] = value; }
        }
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get { return (int)Math.Ceiling(RowCount * 1.0 / PageSize); }
        }
        /// <summary>
        /// 事件
        /// </summary>
        public event PageBarChangeNum ChangeNum;
        /// <summary>
        /// 页面加载
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //页号
            if (ViewState[this.ID + "_PageNum"] == null)
            {
                ViewState[this.ID + "_PageNum"] = 1;
            }
            if (!IsPostBack)
            {
                ContentBind();
            }
        }
        /// <summary>
        /// 内容显示
        /// </summary>
        protected void ContentBind()
        {
            txtPageNum.Text = litPageNum.Text = PageNum.ToString();
            litRowCount.Text = RowCount.ToString();
            litPageCount.Text = PageCount.ToString();
            //首页
            if (PageNum == 1)
            {
                lbFirst.Enabled = false;
                lbPrev.Enabled = false;
            }
            else
            {
                lbFirst.Enabled = true;
                lbPrev.Enabled = true;
            }
            //尾页
            if (PageNum == PageCount)
            {
                lbLast.Enabled = false;
                lbNext.Enabled = false;
            }
            else
            {
                lbLast.Enabled = true;
                lbNext.Enabled = true;
            }
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbFirst_Click(object sender, EventArgs e)
        {
            PageNum = 1;
            ChangeNum(this, new PageBarChangeNumArgs { PageNum = PageNum });
            ContentBind();
        }
        /// <summary>
        /// 上一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbPrev_Click(object sender, EventArgs e)
        {
            PageNum -= 1;
            ChangeNum(this, new PageBarChangeNumArgs { PageNum = PageNum });
            ContentBind();
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbNext_Click(object sender, EventArgs e)
        {
            PageNum += 1;
            ChangeNum(this, new PageBarChangeNumArgs { PageNum = PageNum });
            ContentBind();
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lbLast_Click(object sender, EventArgs e)
        {
            PageNum = PageCount;
            ChangeNum(this, new PageBarChangeNumArgs { PageNum = PageNum });
            ContentBind();
        }
        /// <summary>
        /// 跳转
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnPageGo_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(txtPageNum.Text);
            PageNum = num;
            ChangeNum(this, new PageBarChangeNumArgs { PageNum = PageNum });
            ContentBind();
        }
    }
    /// <summary>
    /// 翻页参数
    /// </summary>
    public class PageBarChangeNumArgs : EventArgs
    {
        /// <summary>
        /// 页号
        /// </summary>
        public int PageNum { get; set; }
    }
}
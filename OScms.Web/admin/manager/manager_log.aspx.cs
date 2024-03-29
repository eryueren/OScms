﻿using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.manager
{
    public partial class manager_log : Web.UI.ManagePage
    {
        protected int totalCount;
        protected int page;
        protected int pageSize;

        protected string keywords = string.Empty;
        Model.managers.manager model = new Model.managers.manager();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.keywords = OSRequest.GetQueryString("keywords");
            this.pageSize = GetPageSize(10); //每页数量
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager_log", OSEnums.ActionEnum.View.ToString()); //检查权限
                model = GetAdminInfo(); //取得当前管理员信息
                RptBind("id>0" + CombSqlTxt(keywords), "add_time desc,id desc");
            }
        }

        #region 组合SQL查询语句==========================
        protected string CombSqlTxt(string _keywords)
        {
            StringBuilder strTemp = new StringBuilder();
            _keywords = _keywords.Replace("'", "");
            if (!string.IsNullOrEmpty(_keywords))
            {
                strTemp.Append(" and (user_name like  '%" + _keywords + "%' or action_type like '%" + _keywords + "%')");
            }

            return strTemp.ToString();
        }
        #endregion

        #region 数据绑定=================================
        private void RptBind(string _strWhere, string _orderby)
        {
            this.page = OSRequest.GetQueryInt("page", 1);
            txtKeywords.Text = this.keywords;
            BLL.managers.manager_log bll = new BLL.managers.manager_log();
            this.rptList.DataSource = bll.GetList(this.pageSize, this.page, _strWhere, _orderby, out this.totalCount);
            this.rptList.DataBind();

            //绑定页码
            txtPageNum.Text = this.pageSize.ToString();
            string pageUrl = Utils.CombUrlTxt("manager_log.aspx", "keywords={0}&page={1}", this.keywords, "__id__");
            PageContent.InnerHtml = Utils.OutPageList(this.pageSize, this.page, this.totalCount, pageUrl, 8);
        }
        #endregion

        #region 返回每页数量=============================
        private int GetPageSize(int _default_size)
        {
            int _pagesize;
            if (int.TryParse(Utils.GetCookie("manager_page_size"), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    return _pagesize;
                }
            }
            return _default_size;
        }
        #endregion

        //关健字查询
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", txtKeywords.Text));
        }
              
        //设置分页数量
        protected void txtPageNum_TextChanged(object sender, EventArgs e)
        {
            int _pagesize;
            if (int.TryParse(txtPageNum.Text.Trim(), out _pagesize))
            {
                if (_pagesize > 0)
                {
                    Utils.WriteCookie("manager_page_size", _pagesize.ToString(), 14400);
                }
            }
            Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", this.keywords));
        }

        //批量删除
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("manager_log", OSEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.managers.manager_log bll = new BLL.managers.manager_log();
            int sucCount = bll.Delete(7);
            AddAdminLog(OSEnums.ActionEnum.Delete.ToString(), "删除管理日志" + sucCount + "条"); //记录日志
            Response.Redirect(Utils.CombUrlTxt("manager_log.aspx", "keywords={0}", this.keywords));
          
        }
    }
}
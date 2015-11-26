using System;
using System.IO;
using System.Data;
using System.Xml;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.settings
{
    public partial class url_rewrite_list : Web.UI.ManagePage
    {
        protected string channel = string.Empty;
        protected string type = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.channel = OSRequest.GetQueryString("channel");
            this.type = OSRequest.GetQueryString("type");

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("site_url_rewrite", OSEnums.ActionEnum.View.ToString()); //检查权限
                RptBind();
            }
        }



        #region 绑定数据=================================
        private void RptBind()
        {
            rptList.DataSource = new BLL.configs.url_rewrite().GetList("");
            rptList.DataBind();
        }
        #endregion



        //删除操作
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("site_url_rewrite", OSEnums.ActionEnum.Delete.ToString()); //检查权限
            BLL.configs.url_rewrite bll = new BLL.configs.url_rewrite();
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string urlName = ((HiddenField)rptList.Items[i].FindControl("hideName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    bll.Remove("name", urlName);
                }
            }
            AddAdminLog(OSEnums.ActionEnum.Delete.ToString(), "删除URL配置信息"); //记录日志
            PageSuccessMsg("添加链接成功！", "", "url_rewrite_list.aspx");
         
        }

    }
}
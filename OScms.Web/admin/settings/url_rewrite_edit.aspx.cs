using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.settings
{
    public partial class url_rewrite_edit : Web.UI.ManagePage
    {
        private string action = OSEnums.ActionEnum.Add.ToString(); //操作类型
        private string urlName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = OSRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == OSEnums.ActionEnum.Edit.ToString())
            {
                this.action = OSEnums.ActionEnum.Edit.ToString();//修改类型
                this.urlName = OSRequest.GetQueryString("name");
                if (string.IsNullOrEmpty(this.urlName))
                {
                    PageErrorMsg("传输参数不正确");
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("site_url_rewrite", OSEnums.ActionEnum.View.ToString()); //检查权限
             
                if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(urlName);
                }
                else
                {
                    txtName.Attributes.Add("ajaxurl", "../../ashx/admin_ajax.ashx?action=urlrewrite_name_validate");
                }
            }
        }



        #region 赋值操作=================================
        private void ShowInfo(string _urlName)
        {
            BLL.configs.url_rewrite bll = new BLL.configs.url_rewrite();
            Model.configs.url_rewrite model = bll.GetInfo(_urlName);

            txtName.Text = model.name;
            txtName.ReadOnly = true;
            txtTemplet.Text = model.templet;
            txtPage.Text = model.page;
            //绑定URL配置列表
            rptList.DataSource = model.url_rewrite_items;
            rptList.DataBind();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            BLL.configs.url_rewrite bll = new BLL.configs.url_rewrite();
            Model.configs.url_rewrite model = new Model.configs.url_rewrite();

            model.name = txtName.Text.Trim();
            model.page = txtPage.Text.Trim();
            model.templet = txtTemplet.Text.Trim();
            //添加URL重写节点
            List<Model.configs.url_rewrite_item> items = new List<Model.configs.url_rewrite_item>();
            string[] itemPathArr = Request.Form.GetValues("itemPath");
            string[] itemPatternArr = Request.Form.GetValues("itemPattern");
            string[] itemQuerystringArr = Request.Form.GetValues("itemQuerystring");
            if (itemPathArr != null && itemPatternArr != null && itemQuerystringArr != null)
            {
                for (int i = 0; i < itemPathArr.Length; i++)
                {
                    items.Add(new Model.configs.url_rewrite_item { path = itemPathArr[i], pattern = itemPatternArr[i], querystring = itemQuerystringArr[i] });
                }
            }
            model.url_rewrite_items = items;

            if (bll.Add(model))
            {
                AddAdminLog(OSEnums.ActionEnum.Add.ToString(), "添加URL配置信息:" + model.name); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(string _urlName)
        {
            BLL.configs.url_rewrite bll = new BLL.configs.url_rewrite();
            Model.configs.url_rewrite model = bll.GetInfo(_urlName);
            model.page = txtPage.Text.Trim();
            model.templet = txtTemplet.Text.Trim();
            //添加URL重写节点
            List<Model.configs.url_rewrite_item> items = new List<Model.configs.url_rewrite_item>();
            string[] itemPathArr = Request.Form.GetValues("itemPath");
            string[] itemPatternArr = Request.Form.GetValues("itemPattern");
            string[] itemQuerystringArr = Request.Form.GetValues("itemQuerystring");
            if (itemPathArr != null && itemPatternArr != null && itemQuerystringArr != null)
            {
                for (int i = 0; i < itemPathArr.Length; i++)
                {
                    items.Add(new Model.configs.url_rewrite_item { path = itemPathArr[i], pattern = itemPatternArr[i], querystring = itemQuerystringArr[i] });
                }
            }
            model.url_rewrite_items = items;

            if (bll.Edit(model))
            {
                AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "修改URL配置信息:" + model.name); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("site_url_rewrite", OSEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.urlName))
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("修改配置成功！", "", "url_rewrite_list.aspx");
               
            }
            else //添加
            {
                ChkAdminLevel("site_url_rewrite", OSEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加配置成功！", "url_rewrite_edit.aspx", "url_rewrite_list.aspx");
              
            }
        }

    }
}
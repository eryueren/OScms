using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.manage.link
{
    public partial class link_edit : OS.Web.UI.ManagePage
    {
        private string action = OSEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = OSRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == OSEnums.ActionEnum.Edit.ToString())
            {
                this.action = OSEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = OSRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    PageErrorMsg("传输参数不正确");
                }
                if (!new BLL.plugins.link().Exists(this.id))
                {
                    PageErrorMsg("记录不存在或已被删除");
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("link", OSEnums.ActionEnum.View.ToString()); //检查权限
                if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.plugins.link bll = new BLL.plugins.link();
            Model.plugins.link model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            if (model.is_red == 1)
            {
                cbIsRed.Checked = true;
            }
            else
            {
                cbIsRed.Checked = false;
            }
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtSortId.Text = model.sort_id.ToString();
            txtUserName.Text = model.user_name;
            txtUserTel.Text = model.user_tel;
            txtEmail.Text = model.email;
            txtSiteUrl.Text = model.site_url;
            txtImgUrl.Text = model.img_url;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.plugins.link model = new Model.plugins.link();
            BLL.plugins.link bll = new BLL.plugins.link();

            model.title = txtTitle.Text.Trim();
            model.is_lock = Utils.StrToInt(rblIsLock.SelectedValue, 0);
            if (cbIsRed.Checked == true)
            {
                model.is_red = 1;
            }
            else
            {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.user_name = txtUserName.Text.Trim();
            model.user_tel = txtUserTel.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.site_url = txtSiteUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url))
            {
                model.is_image = 0;
            }
            if (bll.Add(model) > 0)
            {
                AddAdminLog(OSEnums.ActionEnum.Add.ToString(), "添加友情链接：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.plugins.link bll = new BLL.plugins.link();
            Model.plugins.link model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.is_lock = Utils.StrToInt(rblIsLock.SelectedValue, 0);
            if (cbIsRed.Checked == true)
            {
                model.is_red = 1;
            }
            else
            {
                model.is_red = 0;
            }
            model.sort_id = Utils.StrToInt(txtSortId.Text.Trim(), 99);
            model.user_name = txtUserName.Text.Trim();
            model.user_tel = txtUserTel.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.site_url = txtSiteUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.is_image = 1;
            if (string.IsNullOrEmpty(model.img_url))
            {
                model.is_image = 0;
            }
            if (bll.Update(model))
            {
                AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "修改友情链接：" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("link", OSEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("修改链接成功！", "", "list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("plugin_link", OSEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加链接成功！", "", "list.aspx");
            }
        }

    }
}
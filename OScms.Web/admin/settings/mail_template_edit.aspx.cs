using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.settings
{
    public partial class mail_template_edit : Web.UI.ManagePage
    {
        private string action = YLEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = YLRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == YLEnums.ActionEnum.Edit.ToString())
            {
                this.action = YLEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = YLRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    PageErrorMsg("传输参数不正确");
                }
                if (!new BLL.configs.mail_template().Exists(this.id))
                {
                    PageErrorMsg("记录不存在或已被删除");
                  
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("user_mail_template", YLEnums.ActionEnum.View.ToString()); //检查权限
                if (action == YLEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.configs.mail_template bll = new BLL.configs.mail_template();
            Model.configs.mail_template model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            txtCallIndex.Text = model.call_index;
            txtMailTitle.Text = model.maill_title;
            txtContent.Value = model.content;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.configs.mail_template model = new Model.configs.mail_template();
            BLL.configs.mail_template bll = new BLL.configs.mail_template();

            model.title = txtTitle.Text.Trim();
            model.call_index = txtCallIndex.Text.Trim();
            model.maill_title = txtMailTitle.Text.Trim();
            model.content = txtContent.Value;

            if (bll.Add(model) > 0)
            {
                AddAdminLog(YLEnums.ActionEnum.Add.ToString(), "添加邮件模板:" + model.title); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.configs.mail_template bll = new BLL.configs.mail_template();
            Model.configs.mail_template model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.call_index = txtCallIndex.Text.Trim();
            model.maill_title = txtMailTitle.Text.Trim();
            model.content = txtContent.Value;

            if (bll.Update(model))
            {
                AddAdminLog(YLEnums.ActionEnum.Edit.ToString(), "修改邮件模板:" + model.title); //记录日志
                result = true;
            }

            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == YLEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("user_mail_template", YLEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加邮件模板成功！", "", "mail_template_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("user_mail_template", YLEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加邮件模板成功！", "mail_template_edit.aspx", "mail_template_list.aspx");
            }
        }

    }
}
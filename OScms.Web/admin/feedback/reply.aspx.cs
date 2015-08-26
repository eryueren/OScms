using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.manage.feedback
{
    public partial class reply : OS.Web.UI.ManagePage
    {
        private int id = 0;
        protected Model.plugins.feedback model = new Model.plugins.feedback();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.id = YLRequest.GetQueryInt("id");
            if (this.id == 0)
            {
                PageErrorMsg("传输参数不正确");
            }
            if (!new BLL.plugins.feedback().Exists(this.id))
            {
                PageErrorMsg("记录不存在或已被删除");
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("feedback", YLEnums.ActionEnum.View.ToString()); //检查权限
                ShowInfo(this.id);
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.plugins.feedback bll = new BLL.plugins.feedback();
            model = bll.GetModel(_id);
            txtReContent.Text = Utils.ToTxt(model.reply_content);
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("feedback", YLEnums.ActionEnum.Reply.ToString()); //检查权限
            BLL.plugins.feedback bll = new BLL.plugins.feedback();
            model = bll.GetModel(this.id);
            model.reply_content = Utils.ToHtml(txtReContent.Text);
            model.reply_time = DateTime.Now;
            bll.Update(model);
            AddAdminLog(YLEnums.ActionEnum.Reply.ToString(), "回复留言插件内容：" + model.title); //记录日志
            PageSuccessMsg("留言回复成功！", "", "list.aspx");
        }

    }
}
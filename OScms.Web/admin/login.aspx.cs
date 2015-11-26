using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;
using System.Security.Cryptography;
using System.Text;

namespace OS.Web.admin {
	public partial class login : System.Web.UI.Page {
		protected void Page_Load(object sender, EventArgs e) {
			if (!Page.IsPostBack) {
				txtUserName.Text = Utils.GetCookie("LoginName");
			}
		}
		protected void btnSubmit_Click(object sender, EventArgs e) {
			string userName = txtUserName.Text.Trim();
			string userPwd = txtPassword.Text.Trim();
			string ManageCode = TxtManageCode.Text.Trim();
			if (ManageCode != OS.Web.UI.BasePage.config.emailnickname) {
				msgtip.InnerHtml = "管理认证码输入不正确";
				return;
			}
			if (userName.Equals("") || userPwd.Equals("")) {
				msgtip.InnerHtml = "请输入用户名或密码";
				return;
			}
			if (Session["AdminLoginSun"] == null) {
				Session["AdminLoginSun"] = 1;
			}
			else {
				Session["AdminLoginSun"] = Convert.ToInt32(Session["AdminLoginSun"]) + 1;
			}
			//判断登录错误次数
			if (Session["AdminLoginSun"] != null && Convert.ToInt32(Session["AdminLoginSun"]) > 5) {
				msgtip.InnerHtml = "错误超过5次，关闭浏览器重新登录！";
				return;
			}
			BLL.managers.manager bll = new BLL.managers.manager();
			Model.managers.manager model = bll.GetModel(userName, userPwd, true);
			if (model == null) {
				msgtip.InnerHtml = "用户名或密码有误，请重试！";
				return;
			}
			Session[OSKeys.SESSION_ADMIN_INFO] = model;
			Session.Timeout = 45;
			//写入登录日志
			Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig();
			if (siteConfig.logstatus > 0) {
				new BLL.managers.manager_log().Add(model.id, model.user_name, OSEnums.ActionEnum.Login.ToString(), "用户登录");
			}
			//写入Cookies
			Utils.WriteCookie("LoginName", model.user_name, 14400);
			Utils.WriteCookie("AdminName", "OS", model.user_name);
			Utils.WriteCookie("AdminPwd", "OS", model.password);
			Response.Redirect("Main.aspx");
			return;
		}
	}
}
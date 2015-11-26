using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.users {
	public partial class user_edit : Web.UI.ManagePage {
		string defaultpassword = "0|0|0|0"; //默认显示密码
		protected string action = OSEnums.ActionEnum.Add.ToString(); //操作类型
		private int id = 0, flag = 0;
		protected string groupId = string.Empty;

		protected void Page_Load(object sender, EventArgs e) {
			string _action = OSRequest.GetQueryString("action");
			if (!string.IsNullOrEmpty(_action) && _action == OSEnums.ActionEnum.Edit.ToString()) {
				this.action = OSEnums.ActionEnum.Edit.ToString();//修改类型
				this.id = OSRequest.GetQueryInt("id");
				if (this.id == 0) {
					JscriptMsg("传输参数不正确！", "back", "Error");
					return;
				}
				if (!new BLL.users.users().Exists(this.id)) {
					JscriptMsg("信息不存在或已被删除！", "back", "Error");
					return;
				}
			}
			if (!Page.IsPostBack) {
				ChkAdminLevel("user_list", OSEnums.ActionEnum.View.ToString()); //检查权限
				TreeBind("is_lock=0"); //绑定类别
				groupId = string.IsNullOrEmpty(ddlGroupId.SelectedValue.ToString()) ? "1" : ddlGroupId.SelectedValue.ToString();
				txtUserName.Text = Utils.GetUserNumber(groupId);
				txtUserName.Enabled = false;
				if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
                {
					ShowInfo(this.id);
					txtUserName.Visible = true;
				}
				groupId = string.IsNullOrEmpty(ddlGroupId.SelectedValue.ToString()) ? "1" : ddlGroupId.SelectedValue.ToString();

			}
		}

		#region 绑定类别=================================
		private void TreeBind(string strWhere) {
			BLL.users.user_groups bll = new BLL.users.user_groups();
			DataTable dt = bll.GetList(0, strWhere, "grade asc,id asc").Tables[0];

			this.ddlGroupId.Items.Clear();
			this.ddlGroupId.Items.Add(new ListItem("请选择组别...", ""));
			foreach (DataRow dr in dt.Rows) {
				this.ddlGroupId.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
			}
		}
		#endregion

		#region 赋值操作=================================
		private void ShowInfo(int _id) {
			BLL.users.users bll = new BLL.users.users();
			Model.users.users model = bll.GetModel(_id);

			ddlGroupId.SelectedValue = model.group_id.ToString();
			rblStatus.SelectedValue = model.status.ToString();
			txtUserName.Text = model.user_name;
			txtUserName.ReadOnly = true;
			txtUserName.Attributes.Remove("ajaxurl");
			if (!string.IsNullOrEmpty(model.password)) {
				txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
			}
			txtEmail.Text = model.email;
			txtNickName.Text = model.nick_name;
			txtAvatar.Text = model.avatar;
			rblSex.SelectedValue = model.sex;
			if (model.birthday != null) {
				txtBirthday.Text = model.birthday.GetValueOrDefault().ToString("yyyy-MM-dd");
			}
			txtTelphone.Text = model.telphone;
			txtMobile.Text = model.mobile;
			txtQQ.Text = model.qq;
			txtAddress.Text = model.address;
			txtAmount.Text = model.amount.ToString();
			txtPoint.Text = model.point.ToString();
			txtExp.Text = model.exp.ToString();
			lblRegTime.Text = model.reg_time.ToString();
			lblRegIP.Text = model.reg_ip.ToString();


			txtUniversities.Text = model.universities;
			txtProfessional.Text = model.professional;
			txtGoodat.Text = model.goodat;
			txtWorkat.Text = model.workat;
			txtIndustry.Text = model.industry;
			//查找最近登录信息
			Model.users.user_login_log logModel = new BLL.users.user_login_log().GetLastModel(model.user_name);
			if (logModel != null) {
				lblLastTime.Text = logModel.login_time.ToString();
				lblLastIP.Text = logModel.login_ip;
			}
		}
		#endregion

		#region 增加操作=================================
		private bool DoAdd() {
			bool result = false;
			Model.users.users model = new Model.users.users();
			BLL.users.users bll = new BLL.users.users();

			model.group_id = int.Parse(ddlGroupId.SelectedValue);
			model.status = int.Parse(rblStatus.SelectedValue);
			//检测用户名是否重复
			if (bll.Exists(txtUserName.Text.Trim())) {
				return false;
			}
			//model.user_name = Utils.DropHTML(txtUserName.Text.Trim());
			model.user_name = Utils.GetUserNumber(model.group_id.ToString());
			//检测用户名是否重复
			if (bll.Exists(model.user_name.Trim())) {
				model.user_name = Utils.GetUserNumber(model.group_id.ToString());
			}
			//获得6位的salt加密字符串
			model.salt = Utils.GetCheckCode(6);
			//以随机生成的6位字符串做为密钥加密
			model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
			model.email = Utils.DropHTML(txtEmail.Text);
			model.nick_name = Utils.DropHTML(txtNickName.Text);
			model.avatar = Utils.DropHTML(txtAvatar.Text);
			model.sex = rblSex.SelectedValue;
			DateTime _birthday;
			if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday)) {
				model.birthday = _birthday;
			}
			model.telphone = Utils.DropHTML(txtTelphone.Text.Trim());
			model.mobile = Utils.DropHTML(txtMobile.Text.Trim());

			model.universities = txtUniversities.Text;
			model.professional = txtProfessional.Text;
			model.goodat = txtGoodat.Text;
			model.workat = txtWorkat.Text;
			model.industry = txtIndustry.Text;

			model.qq = Utils.DropHTML(txtQQ.Text);
			model.address = Utils.DropHTML(txtAddress.Text.Trim());
			model.amount = decimal.Parse(txtAmount.Text.Trim());
			model.point = int.Parse(txtPoint.Text.Trim());
			model.exp = int.Parse(txtExp.Text.Trim());
			model.reg_time = DateTime.Now;
			model.reg_ip = OSRequest.GetIP();

			if (bll.Add(model) > 0) {
				AddAdminLog(OSEnums.ActionEnum.Add.ToString(), "添加用户:" + model.user_name); //记录日志
				result = true;
			}
			return result;
		}
		#endregion

		#region 修改操作=================================
		private bool DoEdit(int _id) {
			bool result = false;
			BLL.users.users bll = new BLL.users.users();
			Model.users.users model = bll.GetModel(_id);
			if (model.status != int.Parse(rblStatus.SelectedValue) && int.Parse(rblStatus.SelectedValue) == 0) {
				flag = 1;
			}
			model.group_id = int.Parse(ddlGroupId.SelectedValue);
			model.status = int.Parse(rblStatus.SelectedValue);
			//判断密码是否更改
			if (txtPassword.Text.Trim() != defaultpassword) {
				//获取用户已生成的salt作为密钥加密
				model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
			}
			model.email = Utils.DropHTML(txtEmail.Text);
			model.nick_name = Utils.DropHTML(txtNickName.Text);
			model.avatar = Utils.DropHTML(txtAvatar.Text);
			model.sex = rblSex.SelectedValue;
			DateTime _birthday;
			if (DateTime.TryParse(txtBirthday.Text.Trim(), out _birthday)) {
				model.birthday = _birthday;
			}
			model.universities = txtUniversities.Text;
			model.professional = txtProfessional.Text;
			model.goodat = txtGoodat.Text;
			model.workat = txtWorkat.Text;
			model.industry = txtIndustry.Text;

			model.telphone = Utils.DropHTML(txtTelphone.Text.Trim());
			model.mobile = Utils.DropHTML(txtMobile.Text.Trim());
			model.qq = Utils.DropHTML(txtQQ.Text);
			model.address = Utils.DropHTML(txtAddress.Text.Trim());
			model.amount = Utils.StrToDecimal(txtAmount.Text.Trim(), 0);
			model.point = Utils.StrToInt(txtPoint.Text.Trim(), 0);
			model.exp = Utils.StrToInt(txtExp.Text.Trim(), 0);

			if (bll.Update(model)) {
				AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "修改用户信息:" + model.user_name); //记录日志
				result = true;
			}
			return result;
		}
		#endregion

		//保存
		protected void btnSubmit_Click(object sender, EventArgs e) {
			if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
            {
				ChkAdminLevel("user_list", OSEnums.ActionEnum.Edit.ToString()); //检查权限
				if (!DoEdit(this.id)) {
					JscriptMsg("保存过程中发生错误！", "", "Error");
					return;
				}
				if (flag != 0) {
					string strBody = txtNickName.Text + "(" + txtUserName.Text + ")" + "，您的注册信息已通过。登陆入口：http://chncra.org/login.aspx";
					OS.Common.OSMail.sendMail("smtp.exmail.qq.com", "contact@cncra.org", "123456a", OS.Web.UI.BasePage.config.webcompany, "contact@cncra.org", txtEmail.Text, "注册信息审核通过", strBody);
					JMsg("审核通过信息已发送到用户邮箱");
				}
				JscriptMsg("修改用户成功！", "user_list.aspx", "Success");
			} else //添加
            {
				ChkAdminLevel("user_list", OSEnums.ActionEnum.Add.ToString()); //检查权限
				if (!DoAdd()) {
					BLL.users.users bllUser = null;
					//检测用户名是否重复
					if (bllUser.ExistsMobile(txtMobile.Text.Trim())) {
						JMsg("手机号码已注册请使用未注册手机号"); return;
					}
					//检测用户名是否重复
					if (bllUser.ExistsEmail(txtEmail.Text.Trim())) {
						JMsg("邮箱已注册请使用未注册邮箱"); return;
					}
					JscriptMsg("保存过程中发生错误！", "", "Error");
					return;
				}
				JscriptMsg("添加用户成功！", "user_list.aspx", "Success");
			}
		}

		/// <summary>
		/// 添加编辑删除提示
		/// </summary>
		/// <param name="msgtitle">提示文字</param>
		/// <param name="url">返回地址</param>
		/// <param name="msgcss">CSS样式</param>
		protected void JMsg(string msgtitle) {
			//Page.Response.Write("<Script Language='JavaScript'>window.alert('" + msgtitle + "');</script>");
			Page.ClientScript.RegisterStartupScript(Page.GetType(), "message",
		" <script   language=javascript> alert( '" + msgtitle + " '); </script> ");
		}
	}
}
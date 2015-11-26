using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.manager
{
    public partial class manager_edit : Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //默认显示密码
        private string action = OSEnums.ActionEnum.Add.ToString(); //操作类型
        protected int id = OSRequest.GetQueryInt("id");
        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = OSRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == OSEnums.ActionEnum.Edit.ToString())
            {
                this.action = OSEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id==0)
                {
                    PageErrorMsg("传输参数不正确");

                }
                if (!new BLL.managers.manager().Exists(this.id))
                {
                    PageErrorMsg("记录不存在或已被删除");
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("manager", OSEnums.ActionEnum.View.ToString()); //检查权限
                Model.managers.manager model = GetAdminInfo(); //取得管理员信息
                RoleBind(ddlRoleId, model.role_type);
                if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 角色类型=================================
        private void RoleBind(DropDownList ddl, int role_type)
        {
            BLL.managers.manager_role bll = new BLL.managers.manager_role();
            DataTable dt = bll.GetList("").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("请选择角色...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["role_type"]) >= role_type)
                {
                    ddl.Items.Add(new ListItem(dr["role_name"].ToString(), dr["id"].ToString()));
                }
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.managers.manager bll = new BLL.managers.manager();
            Model.managers.manager model = bll.GetModel(_id);
            ddlRoleId.SelectedValue = model.role_id.ToString();
            if (model.is_lock == 0)
            {
                cbIsLock.Checked = true;
            }
            else
            {
                cbIsLock.Checked = false;
            }
            txtUserName.Text = model.user_name;
            txtUserName.ReadOnly = true;
            txtUserName.Attributes.Remove("ajaxurl");
            if (!string.IsNullOrEmpty(model.password))
            {
                txtPassword.Attributes["value"] = txtPassword1.Attributes["value"] = defaultpassword;
            }
            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            Model.managers.manager model = new Model.managers.manager();
            BLL.managers.manager bll = new BLL.managers.manager();
            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.managers.manager_role().GetModel(model.role_id).role_type;
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }
            //检测用户名是否重复
            if (bll.Exists(txtUserName.Text.Trim()))
            {
                return false;
            }
            model.user_name = txtUserName.Text.Trim();
            //获得6位的salt加密字符串
            model.salt = Utils.GetCheckCode(6);
            //以随机生成的6位字符串做为密钥加密
            model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.add_time = DateTime.Now;

            if (bll.Add(model) > 0)
            {
                AddAdminLog(OSEnums.ActionEnum.Add.ToString(), "添加管理员:" + model.user_name); //记录日志
                return true;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.managers.manager bll = new BLL.managers.manager();
            Model.managers.manager model = bll.GetModel(_id);

            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.managers.manager_role().GetModel(model.role_id).role_type;
            if (cbIsLock.Checked == true)
            {
                model.is_lock = 0;
            }
            else
            {
                model.is_lock = 1;
            }
            //判断密码是否更改
            if (txtPassword.Text.Trim() != defaultpassword)
            {
                //获取用户已生成的salt作为密钥加密
                model.password = DESEncrypt.Encrypt(txtPassword.Text.Trim(), model.salt);
            }
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();

            if (bll.Update(model))
            {
                AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "修改管理员:" + model.user_name); //记录日志
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
                ChkAdminLevel("manager", OSEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加管理员信息成功！", "", "manager_list.aspx");
            }
            else //添加
            {
                ChkAdminLevel("manager", OSEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加管理员信息成功！", "manager_edit.aspx", "manager_list.aspx");
  
            }
        }
    }
}
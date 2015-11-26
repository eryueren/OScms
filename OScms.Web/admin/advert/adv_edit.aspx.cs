using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.manage.advert
{
    public partial class adv_edit : OS.Web.UI.ManagePage
    {
        private string action = OSEnums.ActionEnum.Add.ToString(); //操作类型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = OSRequest.GetQueryString("action");

            if (!string.IsNullOrEmpty(_action) && _action == OSEnums.ActionEnum.Edit.ToString())
            {
                this.action = OSEnums.ActionEnum.Edit.ToString();//修改类型
                this.id = OSRequest.GetQueryInt("id", 0);
                if (this.id < 1)
                {
                    PageErrorMsg("传输参数不正确");
             
                }
                if (!new BLL.plugins.advert().Exists(this.id))
                {
                    PageErrorMsg("信息不存在或已被删除");
 
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("advert", OSEnums.ActionEnum.View.ToString()); //检查权限
                if (action == OSEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.plugins.advert bll = new BLL.plugins.advert();
            Model.plugins.advert model = bll.GetModel(_id);

            txtTitle.Text = model.title;
            rblType.SelectedValue = model.type.ToString();
            txtRemark.Text = model.remark;
            txtViewNum.Text = model.view_num.ToString();
            txtPrice.Text = model.price.ToString();
            txtViewWidth.Text = model.view_width.ToString();
            txtViewHeight.Text = model.view_height.ToString();
            rblTarget.SelectedValue = model.target;
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            Model.plugins.advert model = new Model.plugins.advert();
            BLL.plugins.advert bll = new BLL.plugins.advert();

            model.title = txtTitle.Text.Trim();
            model.type = int.Parse(rblType.SelectedValue);
            model.price = decimal.Parse(txtPrice.Text.Trim());
            model.remark = txtRemark.Text.Trim();
            model.view_num = int.Parse(txtViewNum.Text.Trim());
            model.view_width = int.Parse(txtViewWidth.Text.Trim());
            model.view_height = int.Parse(txtViewHeight.Text.Trim());
            model.target = rblTarget.SelectedValue;

            if (bll.Add(model) >0)
            {
                AddAdminLog(OSEnums.ActionEnum.Add.ToString(), "添加广告位：" + model.title); //记录日志
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = false;
            BLL.plugins.advert bll = new BLL.plugins.advert();
            Model.plugins.advert model = bll.GetModel(_id);

            model.title = txtTitle.Text.Trim();
            model.type = int.Parse(rblType.SelectedValue);
            model.price = decimal.Parse(txtPrice.Text.Trim());
            model.remark = txtRemark.Text.Trim();
            model.view_num = int.Parse(txtViewNum.Text.Trim());
            model.view_width = int.Parse(txtViewWidth.Text.Trim());
            model.view_height = int.Parse(txtViewHeight.Text.Trim());
            model.target = rblTarget.SelectedValue;

            if (bll.Update(model))
            {
                AddAdminLog(OSEnums.ActionEnum.Edit.ToString(), "修改广告位：" + model.title); //记录日志
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
                ChkAdminLevel("advert", OSEnums.ActionEnum.Edit.ToString()); //检查权限

                if (!DoEdit(this.id))
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("修改信息成功", "", "index.aspx");
            }
            else //添加
            {
                ChkAdminLevel("advert", OSEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("添加信息成功", "", "index.aspx");
            }
        }

    }
}
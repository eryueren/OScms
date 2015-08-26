using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.article
{
    public partial class category_edit : Web.UI.ManagePage
    {
        private string action = YLEnums.ActionEnum.Add.ToString(); //操作类型
        protected string channel_name = string.Empty; //频道名称
        private int channel_id;
        private int id = YLRequest.GetQueryInt("id");

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = YLRequest.GetQueryString("action");
            this.channel_id = YLRequest.GetQueryInt("channel_id");

            if (this.channel_id == 0)
            {
                PageErrorMsg("频道参数不正确");
            }

            if (!string.IsNullOrEmpty(_action) && _action == YLEnums.ActionEnum.Edit.ToString())
            {
                this.action = YLEnums.ActionEnum.Edit.ToString();//修改类型
                if (this.id == 0)
                {
                    PageErrorMsg("传输参数不正确");
                }
                if (!new BLL.contents.article_category().Exists(this.id))
                {
                    PageErrorMsg("类别不存在或已被删除");
                }
            }
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("category", YLEnums.ActionEnum.View.ToString()); //检查权限
                TreeBind(this.channel_id); //绑定类别
                FieldBind(); //绑定扩展字段
                if (action == YLEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    if (this.id > 0)
                    {
                        this.ddlParentId.SelectedValue = this.id.ToString();
                    }
                    txtCallIndex.Attributes.Add("ajaxurl", "../../ashx/admin_ajax.ashx?action=navigation_validate");
                }
            }
        }

        #region 绑定类别=================================
        private void TreeBind(int _channel_id)
        {
            BLL.contents.article_category bll = new BLL.contents.article_category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlParentId.Items.Clear();
            this.ddlParentId.Items.Add(new ListItem("无父级分类", "1"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlParentId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        #region 绑定扩展字段=============================
        private void FieldBind()
        {
            BLL.contents.article_attribute_field bll = new BLL.contents.article_attribute_field();
            DataTable dt = bll.GetList(0, "", "sort_id asc,id desc").Tables[0];

            this.cblAttributeField.Items.Clear();
            foreach (DataRow dr in dt.Rows)
            {
                this.cblAttributeField.Items.Add(new ListItem(dr["title"].ToString(), dr["id"].ToString()));
            }
        }
        #endregion

        #region 赋值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.contents.article_category bll = new BLL.contents.article_category();
            Model.contents.article_category model = bll.GetModel(_id);

            ddlParentId.SelectedValue = model.parent_id.ToString();
            txtCallIndex.Text = model.call_index;
            txtCallIndex.Attributes.Add("ajaxurl", "../../ashx/admin_ajax.ashx?action=navigation_validate&old_name=" + Utils.UrlEncode(model.call_index));
            txtCallIndex.Focus(); //设置焦点，防止JS无法提交
            txtTitle.Text = model.title;
            txtSortId.Text = model.sort_id.ToString();
            txtSeoTitle.Text = model.seo_title;
            txtSeoKeywords.Text = model.seo_keywords;
            txtSeoDescription.Text = model.seo_description;
            txtLinkUrl.Text = model.link_url;
            txtImgUrl.Text = model.img_url;
            txtContent.Value = model.content;
            rblStatus.SelectedValue = model.model_id.ToString();
            if (model.is_add_category == 1)
            {
                cblItem.Items[0].Selected = true;
            }
            if (model.is_add_content == 1)
            {
                cblItem.Items[1].Selected = true;
            }
            if (model.is_show_top == 1)
            {
                cblItem.Items[2].Selected = true;
            }
            if (model.is_show_foot == 1)
            {
                cblItem.Items[3].Selected = true;
            }
            if (model.is_albums == 1)
            {
                cbIsAlbums.Checked = true;
            }
            if (model.is_attach == 1)
            {
                cbIsAttach.Checked = true;
            }
            txtPageSize.Text = model.page_size.ToString();
            //赋值扩展字段
            if (model.category_fields != null)
            {
                for (int i = 0; i < cblAttributeField.Items.Count; i++)
                {
                    Model.contents.category_field modelt = model.category_fields.Find(p => p.field_id == int.Parse(cblAttributeField.Items[i].Value)); //查找对应的字段ID
                    if (modelt != null)
                    {
                        cblAttributeField.Items[i].Selected = true;
                    }
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            try
            {
                Model.contents.article_category model = new Model.contents.article_category();
                BLL.contents.article_category bll = new BLL.contents.article_category();
                model.channel_id = this.channel_id;
                model.nav_type = YLEnums.NavigationEnum.WebSite.ToString().Trim();
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();

                //  model.parent_id = int.Parse(ddlParentId.SelectedValue);
                model.parent_id = this.id;
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.seo_title = txtSeoTitle.Text;
                model.seo_keywords = txtSeoKeywords.Text;
                model.seo_description = txtSeoDescription.Text;
                model.link_url = txtLinkUrl.Text.Trim();
                model.img_url = txtImgUrl.Text.Trim();
                model.content = txtContent.Value;
                model.model_id = int.Parse(rblStatus.SelectedValue);
                model.is_add_category = 0;
                model.is_add_content = 0;
                model.is_show_top = 0;
                model.is_show_foot = 0;
                if (cblItem.Items[0].Selected == true)
                {
                    model.is_add_category = 1;
                }
                if (cblItem.Items[1].Selected == true)
                {
                    model.is_add_content = 1;
                }
                if (cblItem.Items[2].Selected == true)
                {
                    model.is_show_top = 1;
                }
                if (cblItem.Items[3].Selected == true)
                {
                    model.is_show_foot = 1;
                }

                if (cbIsAlbums.Checked == true)
                {
                    model.is_albums = 1;
                }
                if (cbIsAttach.Checked == true)
                {
                    model.is_attach = 1;
                }
                model.page_size = Utils.StrToInt(txtPageSize.Text.Trim(), 10);
                //添加频道扩展字段
                List<Model.contents.category_field> ls = new List<Model.contents.category_field>();
                for (int i = 0; i < cblAttributeField.Items.Count; i++)
                {
                    if (cblAttributeField.Items[i].Selected)
                    {
                        ls.Add(new Model.contents.category_field { field_id = Utils.StrToInt(cblAttributeField.Items[i].Value, 0) });
                    }
                }
                model.category_fields = ls;

                if (bll.Add(model) > 0)
                {
                    AddAdminLog(YLEnums.ActionEnum.Add.ToString(), "添加" + this.channel_name + "频道栏目分类:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            try
            {
                BLL.contents.article_category bll = new BLL.contents.article_category();
                Model.contents.article_category model = bll.GetModel(_id);

                int parentId = int.Parse(ddlParentId.SelectedValue);
                model.channel_id = this.channel_id;
                model.call_index = txtCallIndex.Text.Trim();
                model.title = txtTitle.Text.Trim();
                //如果选择的父ID不是自己,则更改
                //if (parentId != model.id)
                //{
                //    model.parent_id = parentId;
                //}
                model.sort_id = int.Parse(txtSortId.Text.Trim());
                model.seo_title = txtSeoTitle.Text;
                model.seo_keywords = txtSeoKeywords.Text;
                model.seo_description = txtSeoDescription.Text;
                model.link_url = txtLinkUrl.Text.Trim();
                model.img_url = txtImgUrl.Text.Trim();
                model.content = txtContent.Value;
                model.model_id = int.Parse(rblStatus.SelectedValue);
                model.is_add_category = 0;
                model.is_add_content = 0;
                model.is_show_top = 0;
                model.is_show_foot = 0;
                if (cblItem.Items[0].Selected == true)
                {
                    model.is_add_category = 1;
                }
                if (cblItem.Items[1].Selected == true)
                {
                    model.is_add_content = 1;
                }
                if (cblItem.Items[2].Selected == true)
                {
                    model.is_show_top = 1;
                }
                if (cblItem.Items[3].Selected == true)
                {
                    model.is_show_foot = 1;
                }
                if (cbIsAlbums.Checked == true)
                {
                    model.is_albums = 1;
                }
                if (cbIsAttach.Checked == true)
                {
                    model.is_attach = 1;
                }
                model.page_size = Utils.StrToInt(txtPageSize.Text.Trim(), 10);
                //添加频道扩展字段
                List<Model.contents.category_field> ls = new List<Model.contents.category_field>();
                for (int i = 0; i < cblAttributeField.Items.Count; i++)
                {
                    if (cblAttributeField.Items[i].Selected)
                    {
                        ls.Add(new Model.contents.category_field { category_id = model.id, field_id = Utils.StrToInt(cblAttributeField.Items[i].Value, 0) });
                    }
                }
                model.category_fields = ls;
                if (bll.Update(model))
                {
                    AddAdminLog(YLEnums.ActionEnum.Edit.ToString(), "修改" + this.channel_name + "频道栏目分类:" + model.title); //记录日志
                    return true;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        #endregion

        //保存类别
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == YLEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("category", YLEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit(this.id))
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                PageSuccessMsg("修改类别成功", "", "category_list.aspx");

            }
            else //添加
            {
                ChkAdminLevel("category", YLEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                string id_url1 = this.id != -2 ? "&Id=" + this.id : "";
                string id_url2 = ddlParentId.SelectedValue != "" ? "&Id=" + ddlParentId.SelectedValue : id_url1;
                PageSuccessMsg("添加类别成功", "category_edit.aspx?action=" + YLEnums.ActionEnum.Add + "&channel_id=" + this.channel_id + id_url2, "category_list.aspx");
            }
        }

        #region 返回页面的类型===========================
        protected string GetPageTypeTxt(string type_name)
        {
            string result = "";
            switch (type_name)
            {
                case "index":
                    result = "首页";
                    break;
                case "category":
                    result = "栏目页";
                    break;
                case "list":
                    result = "列表页";
                    break;
                case "detail":
                    result = "详细页";
                    break;
            }
            return result;
        }
        #endregion

    }
}
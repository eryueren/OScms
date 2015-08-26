using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.settings
{
    public partial class template_edit : Web.UI.ManagePage
    {
        protected string skin = YLRequest.GetQueryString("skin");
        protected string _action = YLRequest.GetQueryString("action");
        protected string filePath; //文件路径
        protected string fileName; //文件名称
        protected string sl = "";
        protected string url = "";
        protected string menu = "";
        protected string style = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            sl = !string.IsNullOrEmpty(skin) ? skin + "/" : "";
            url = !string.IsNullOrEmpty(sl) ? "&skin=" + skin : "";

       
         
            if (!string.IsNullOrEmpty(_action) && _action == YLEnums.ActionEnum.Edit.ToString())
            {
                fileName = YLRequest.GetQueryString("filename");
                if (string.IsNullOrEmpty(fileName))
                {
                    PageErrorMsg("传输参数不正确");
                }
          
                filePath = Utils.GetMapPath(@"../../Template/" + sl + fileName.Replace("/", ""));
                if (!File.Exists(filePath))
                {
                    PageErrorMsg("该文件不存在");
                }
            }

            if (!Page.IsPostBack)
            {
                ChkAdminLevel("app_templet_list", YLEnums.ActionEnum.View.ToString()); //检查权限
                style = _action == YLEnums.ActionEnum.Edit.ToString() ? " style=\"display:block\"" : " style=\"display:none\"";
                if (_action == YLEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(filePath);
                }
                else
                {
                    if (!string.IsNullOrEmpty(skin))
                    {
                        txtFileName.Attributes.Add("ajaxurl", "../../ashx/admin_ajax.ashx?action=file_validate&type=" + fileType.SelectedValue + "&skin=" + skin);
                    }
                    else
                    {
                        txtFileName.Attributes.Add("ajaxurl", "../../ashx/admin_ajax.ashx?action=file_validate&type=" + fileType.SelectedValue);
                    }
                }
            }
        }

        #region 赋值操作=================================
        private void ShowInfo(string _path)
        {
        
            fileType.Visible = false;
            txtFileName.Visible = false;
            txtFileName.Attributes.Remove("ajaxurl");

            menu = sl + fileName;
            using (StreamReader objReader = new StreamReader(_path, Encoding.UTF8))
            {
                txtContent.Text = objReader.ReadToEnd();
                objReader.Close();
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = false;
            string htmlPath = "/Template/" + sl + txtFileName.Text.Replace(".", "") + fileType.SelectedValue;  //文件保存相对路径
            using (StreamWriter sw = new StreamWriter(Utils.GetMapPath(htmlPath), false, Encoding.UTF8))
            {
                sw.WriteLine(txtContent.Text);
                sw.Flush();
                sw.Close();
                result = true;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit()
        {
            bool result = false;
            using (FileStream fs = new FileStream(this.filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            {
                Byte[] info = Encoding.UTF8.GetBytes(txtContent.Text);
                fs.Write(info, 0, info.Length);
                fs.Close();
                result = true;
            }
            return result;
        }
        #endregion

        //保存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (_action == YLEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel("app_templet_list", YLEnums.ActionEnum.Edit.ToString()); //检查权限
                if (!DoEdit())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                AddAdminLog(YLEnums.ActionEnum.Add.ToString(), "添加模板文件:" + this.fileName);//记录日志
                PageSuccessMsg("模板保存成功", "", "template_list.aspx" + url.Replace("&", "?"));
            }
            else //添加
            {
                ChkAdminLevel("app_templet_list", YLEnums.ActionEnum.Add.ToString()); //检查权限
                if (!DoAdd())
                {
                    PageErrorMsg("保存过程中发生错误啦");
                }
                AddAdminLog(YLEnums.ActionEnum.Edit.ToString(), "修改模板文件:" + this.fileName);//记录日志
                PageSuccessMsg("模板添加成功", "template_edit.aspx?action=" + YLEnums.ActionEnum.Add + url, "template_list.aspx" + url.Replace("&","?"));
            }

            //using (FileStream fs = new FileStream(this.filePath, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite))
            //{
            //    Byte[] info = Encoding.UTF8.GetBytes(txtContent.Text);
            //    fs.Write(info, 0, info.Length);
            //    fs.Close();
            //}
            //AddAdminLog(YLEnums.ActionEnum.Edit.ToString(), "修改模板文件:" + this.fileName);//记录日志
            //PageSuccessMsg("模板保存成功！", "", "template_list.aspx");
        }
    }
}
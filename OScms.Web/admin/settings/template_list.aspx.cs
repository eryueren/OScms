using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using OS.Common;

namespace OS.Web.admin.settings
{
    public partial class template_list : Web.UI.ManagePage
    {
       protected string skinName = YLRequest.GetQueryString("skin");
       protected string _action = YLRequest.GetString("action");
       protected string _fileName = YLRequest.GetString("file");
       protected string _folderName = YLRequest.GetString("folder");
       protected  string sl = "";
       protected string url = "";
       protected string file = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                ChkAdminLevel("app_templet_list", YLEnums.ActionEnum.View.ToString()); //检查权限
                sl = !string.IsNullOrEmpty(skinName) ? "/" + skinName : "";
                url = !string.IsNullOrEmpty(sl) ? "&skin=" + skinName : "";
                file = !string.IsNullOrEmpty(skinName) ? "" : "";


                if (!string.IsNullOrEmpty(_folderName) && _action == "del")
                {
                    DelDir(Utils.GetMapPath(@"../../Template/" + _folderName));
                }
                if (!string.IsNullOrEmpty(_fileName) && _action == "del")
                {
                    string p = !string.IsNullOrEmpty(skinName) ? skinName + "/" : "";
                    DelFile(Utils.GetMapPath(@"../../Template/" + p), _fileName);
                }
            }
        }
        protected string list()
        {
            StringBuilder sb = new StringBuilder();
            DirectoryInfo[] ChildDirectory;                         //子目录集
            FileInfo[] NewFileInfo;                                 //当前所有文件
          
            DirectoryInfo FatherDirectory = new DirectoryInfo(Utils.GetMapPath(@"../../Template" + sl)); //当前目录
            ChildDirectory = FatherDirectory.GetDirectories("*.*"); //得到子目录集
            NewFileInfo = FatherDirectory.GetFiles();
            foreach (DirectoryInfo dirInfo in ChildDirectory)       //获取此级目录下的一级目录
            {
                sb.Append(" <tr >\n");
                sb.Append(" <td ></td>");
                sb.Append(" <td><img src=\"../images/FileIcon/folder.gif\" alt=\"点击进入下级目录\"><a href=\"template_list.aspx?skin=" + dirInfo.Name.ToString() + "\"  title=\"点击进入下级目录\">" + dirInfo.Name.ToString() + "</a></td>");
                sb.Append(" <td>文件夹</td>");
                sb.Append(" <td>-</td>");
                sb.Append(" <td>" + dirInfo.LastWriteTime.ToString() + "</td>");
                sb.Append(" <td > ");
                sb.Append(" <a  href=\"javascript:editFolderName('" + dirInfo.Name + "')\" >改名</a> | ");
                sb.Append(" <a  href=\"javascript:del_folder('" + dirInfo.Name + "')\" >删除</a>");
                sb.Append(" </td>");
                sb.Append(" </tr >\n");
            }
            foreach (FileInfo DirFile in NewFileInfo)                    //获取此级目录下的所有文件
            {
                sb.Append("<tr >\n");
                if (SelectFile(DirFile.Extension))                       //传入文件后缀名,判断是否是被显示的文件类型,默认显示html,htm,css
                {
                    sb.Append(" <td ></td>");
                    sb.Append(" <td><img src=\"../images/FileIcon/" + GetFileIco(DirFile.Extension.ToString()) + "\">  " + DirFile.Name.ToString() + "</td>");
                    sb.Append(" <td>" + DirFile.Extension.ToString() + "文件</td>");
                    sb.Append(" <td>" + DirFile.Length.ToString() + "</td>");
                    sb.Append(" <td>" + DirFile.LastWriteTime.ToString() + "</td>");
                    sb.Append(" <td > ");
                    sb.Append(" <a href=\"template_edit.aspx?action=" + YLEnums.ActionEnum.Edit + "&filename=" + DirFile.Name.ToString() + url + "\">编辑</a> | ");
                    sb.Append(" <a  href=\"javascript:dels('" + DirFile.Name + "')\" >删除</a>");
                    sb.Append(" </td>");
                }
                sb.Append("</tr >\n");
            }
            return sb.ToString();
        }

        protected string GetFileIco(string type)
        {
            string Str_ImgPath;
            switch (type.ToLower())
            {
                case ".html":
                    Str_ImgPath = "html.gif";
                    break;
                case ".css":
                    Str_ImgPath = "css.gif";
                    break;
                case ".js":
                    Str_ImgPath = "js.gif";
                    break;
                default:
                    Str_ImgPath = "unknown.gif";
                    break;
            }
            return Str_ImgPath;
        }
        protected bool SelectFile(string Extension)
        {
            bool value = false;
            switch (Extension.ToLower())
            {
                case ".html":
                    value = true;
                    break;
                case ".css":
                    value = true;
                    break;
                case ".js":
                    value = true;
                    break;
                default:
                    value = false;
                    break;
            }
            return value;
        }
        #region 数据绑定=================================
        private void RptBind()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("name", Type.GetType("System.String"));
            dt.Columns.Add("creationtime", Type.GetType("System.String"));
            dt.Columns.Add("updatetime", Type.GetType("System.String"));
            DirectoryInfo dirInfo = new DirectoryInfo(Utils.GetMapPath(@"../../Template"));
            foreach (FileInfo file in dirInfo.GetFiles())
            {
                DataRow dr = dt.NewRow();
                dr["name"] = file.Name;
                dr["creationtime"] = file.CreationTime;
                dr["updatetime"] = file.LastWriteTime;
                dt.Rows.Add(dr);
            }

            this.rptList.DataSource = dt;
            this.rptList.DataBind();
        }
        #endregion

        //删除文件
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            ChkAdminLevel("app_templet_list", YLEnums.ActionEnum.Delete.ToString()); //检查权限
            for (int i = 0; i < rptList.Items.Count; i++)
            {
                string fileName = ((HiddenField)rptList.Items[i].FindControl("hideName")).Value;
                CheckBox cb = (CheckBox)rptList.Items[i].FindControl("chkId");
                if (cb.Checked)
                {
                    Utils.DeleteFile("../../Template/" + fileName);
                }
            }
            AddAdminLog(YLEnums.ActionEnum.Delete.ToString(), "删除模板文件，模板:" + this.skinName);//记录日志
            Response.Redirect("template_list.aspx");

        }


        /// <summary>
        /// 修改文件夹名称
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>修改文件夹名称</returns>
        /// Code By DengXi


        protected void EidtDirName(string path)
        {
            string str_OldName = Request.QueryString["OldFileName"];
            string str_NewName = Request.QueryString["NewFileName"];
            if (str_OldName == "" || str_OldName == null || str_OldName == string.Empty || str_NewName == "" || str_NewName == null || str_NewName == string.Empty)
                PageErrorMsg("参数传递错误");

             Templet tpClass = new Templet();
            int result = tpClass.EidtName(path, str_OldName, str_NewName, 0);
            //if (result == 1)
            //    PageRight("更改文件夹名成功！", s_url);
            //else
            //    PageError("参数传递错误！", s_url);
        }

        /// <summary>
        /// 修改文件名称
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>修改文件名称</returns>
        /// Code By DengXi

        protected void EidtFileName(string path)
        {
        //    string str_OldName = Request.QueryString["OldFileName"];
        //    string str_NewName = Request.QueryString["NewFileName"];

        //    if (str_OldName == "" || str_OldName == null || str_OldName == string.Empty || str_NewName == "" || str_NewName == null || str_NewName == string.Empty)
        //        PageError("参数传递错误!", s_url);

        //    NetCMS.Content.Templet.Templet tpClass = new NetCMS.Content.Templet.Templet();
        //    int result = tpClass.EidtName(path, str_OldName, str_NewName, 1);
        //    if (result == 1)
        //        PageRight("更改文件名成功！", s_url);
        //    else
        //        PageError("参数传递错误！", s_url);
        }

        /// <summary>
        /// 删除文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>删除文件夹</returns>
        /// Code By DengXi

        protected void DelDir(string path)
        {
            int result = 0;
            Templet tpClass = new Templet();
            result = tpClass.Del(path, "", 0);
            if (result == 1)
                PageSuccessMsg("删除文件夹成功！", "", "template_list.aspx");
            else
                PageErrorMsg("参数错误");
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>删除文件</returns>
        /// Code By DengXi

        protected void DelFile(string path,string filename)
        {
            int result = 0;
            Templet tpClass = new Templet();
            result = tpClass.Del(path, filename, 1);
            if (result == 1)
                  PageSuccessMsg("删除文件成功！", "", "template_list.aspx");
            else
                PageErrorMsg("参数错误");
        }

        /// <summary>
        /// 添加文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>添加文件夹</returns>
        /// Code By DengXi


        protected void AddDir(string path)
        {
        //    string str_DirName = Request.QueryString["filename"];

        //    int result = 0;
        //    NetCMS.Content.Templet.Templet tpClass = new NetCMS.Content.Templet.Templet();
        //    result = tpClass.AddDir(path, str_DirName);
        //    if (result == 1)
        //        PageRight("添加文件夹成功!", s_url);
        //    else
        //        PageError("未知错误!", s_url);
        }

    }
}
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.SessionState;
using OS.Web.UI;
using OS.Common;

namespace OS.Web.ashx
{
    /// <summary>
    /// 管理后台AJAX处理页
    /// </summary>
    public class admin_ajax : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //取得处事类型
            string action = OSRequest.GetQueryString("action");

            switch (action)
            {
                case "attribute_field_validate": //验证扩展字段是否重复
                    attribute_field_validate(context);
                    break;
                case "urlrewrite_name_validate": //验证URL调用名称是否重复
                    urlrewrite_name_validate(context);
                    break;
                case "navigation_validate": //验证导航菜单ID是否重复
                    navigation_validate(context);
                    break;
                case "manager_validate": //验证管理员用户名是否重复
                    manager_validate(context);
                    break;
                case "get_remote_fileinfo": //获取远程文件的信息
                    get_remote_fileinfo(context);
                    break;

                case "validate_username": //验证会员用户名是否重复
                    validate_username(context);
                    break;
                case "edit_category": //验证会员用户名是否重复
                    edit_category(context);
                    break;

                case "delete_category": //验证会员用户名是否重复
                    delete_category(context);
                    break;

                case "file_validate": //验证文件是否重复
                    file_validate(context);
                    break;

                case "create_folder": //创建目录
                    create_folder(context);
                    break;
            }
        }

        #region 验证扩展字段是否重复============================
        private void attribute_field_validate(HttpContext context)
        {
            string column_name = OSRequest.GetString("param");
            if (string.IsNullOrEmpty(column_name))
            {
                context.Response.Write("{ \"info\":\"名称不可为空\", \"status\":\"n\" }");
                return;
            }
            BLL.contents.article_attribute_field bll = new BLL.contents.article_attribute_field();
            if (bll.Exists(column_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证URL调用名称是否重复=========================
        private void urlrewrite_name_validate(HttpContext context)
        {
            string new_name = OSRequest.GetString("param");
            string old_name = OSRequest.GetString("old_name");
            if (string.IsNullOrEmpty(new_name))
            {
                context.Response.Write("{ \"info\":\"名称不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (new_name.ToLower() == old_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
                return;
            }
            BLL.configs.url_rewrite bll = new BLL.configs.url_rewrite();
            if (bll.Exists(new_name))
            {
                context.Response.Write("{ \"info\":\"该名称已被使用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该名称可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证导航菜单ID是否重复==========================
        private void navigation_validate(HttpContext context)
        {
            string navname = OSRequest.GetString("param");
            string old_name = OSRequest.GetString("old_name");
            if (string.IsNullOrEmpty(navname))
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID不可为空！\", \"status\":\"n\" }");
                return;
            }
            if (navname.ToLower() == old_name.ToLower())
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID可使用\", \"status\":\"y\" }");
                return;
            }
            //检查保留的名称开头
            if (navname.ToLower().StartsWith("channel_"))
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID系统保留，请更换！\", \"status\":\"n\" }");
                return;
            }
            BLL.contents.article_category bll = new BLL.contents.article_category();
            if (bll.Exists(navname))
            {
                context.Response.Write("{ \"info\":\"该导航菜单ID已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"该导航菜单ID可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 验证管理员用户名是否重复========================
        private void manager_validate(HttpContext context)
        {
            string user_name = OSRequest.GetString("param");
            if (string.IsNullOrEmpty(user_name))
            {
                context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
                return;
            }
            BLL.managers.manager bll = new BLL.managers.manager();
            if (bll.Exists(user_name))
            {
                context.Response.Write("{ \"info\":\"用户名已被占用，请更换！\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"用户名可使用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 获取远程文件的信息==============================
        private void get_remote_fileinfo(HttpContext context)
        {
            string filePath = OSRequest.GetFormString("remotepath");
            if (string.IsNullOrEmpty(filePath))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"没有找到远程附件地址！\"}");
                return;
            }
            if (!filePath.ToLower().StartsWith("http://"))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"不是远程附件地址！\"}");
                return;
            }
            try
            {
                HttpWebRequest _request = (HttpWebRequest)WebRequest.Create(filePath);
                HttpWebResponse _response = (HttpWebResponse)_request.GetResponse();
                int fileSize = (int)_response.ContentLength;
                string fileName = filePath.Substring(filePath.LastIndexOf("/") + 1);
                string fileExt = filePath.Substring(filePath.LastIndexOf(".") + 1).ToUpper();
                context.Response.Write("{\"status\": 1, \"msg\": \"获取远程文件成功！\", \"name\": \"" + fileName + "\", \"path\": \"" + filePath + "\", \"size\": " + fileSize + ", \"ext\": \"" + fileExt + "\"}");
            }
            catch
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"远程文件不存在！\"}");
                return;
            }
        }
        #endregion

        #region 验证用户名是否可用==============================
        private void validate_username(HttpContext context)
        {
            string user_name = OSRequest.GetString("param");
            //如果为Null，退出
            if (string.IsNullOrEmpty(user_name))
            {
                context.Response.Write("{ \"info\":\"请输入用户名\", \"status\":\"n\" }");
                return;
            }
            Model.configs.userconfig userConfig = new BLL.configs.userconfig().loadConfig();
            //过滤注册用户名字符
            string[] strArray = userConfig.regkeywords.Split(',');
            foreach (string s in strArray)
            {
                if (s.ToLower() == user_name.ToLower())
                {
                    context.Response.Write("{ \"info\":\"用户名不可用\", \"status\":\"n\" }");
                    return;
                }
            }
            BLL.users.users bll = new BLL.users.users();
            //查询数据库
            if (bll.Exists(user_name.Trim()))
            {
                context.Response.Write("{ \"info\":\"用户名已重复\", \"status\":\"n\" }");
                return;
            }
            context.Response.Write("{ \"info\":\"用户名可用\", \"status\":\"y\" }");
            return;
        }
        #endregion

        #region 判断是否登陆以及是否开启静态====================
        private int GetIsLoginAndIsStaticstatus()
        {
            Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig();
            //取得管理员登录信息
            Model.managers.manager adminInfo = new Web.UI.ManagePage().GetAdminInfo();
            if (adminInfo == null)
                return  -1;
            else if (!new BLL.managers.manager_role().Exists(adminInfo.role_id, "app_builder_html", OSEnums.ActionEnum.Build.ToString()))
                return -2;
            else if (siteConfig.staticstatus != 2)
                return -3;
            else
                return 1;
        }
        #endregion

        #region 栏目转移==============================
        private void edit_category(HttpContext context)
        {
            //取得管理员登录信息
            Model.managers.manager adminInfo = new Web.UI.ManagePage().GetAdminInfo();
            if (adminInfo == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"未登录或已超时，请重新登录！\"}");
                return;
            }

            int category1 = OSRequest.GetFormInt("Category1");
            int category2 = OSRequest.GetFormInt("Category2");
            if (category1 == category2)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"移动目录跟目的目录不能相同！\"}");
                return;
            }

            BLL.contents.article_category bll = new BLL.contents.article_category();
            Model.contents.article_category model = bll.GetModel(category2);

            if (model.class_list.IndexOf("," + category1 + ",") > 0)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"父级本不能向其子级转移！\"}");
                return;
            }

            if (bll.GetModel(category1).parent_id == category2)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"移动目录已经在目的目录里！\"}");
                return;
            }

            DataTable dt = bll.GetList(0, "class_list like '%," + category1 + ",%'", "class_layer asc").Tables[0];
            if (dt.Rows != null && dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    int id = Convert.ToInt32(dt.Rows[i]["id"]);
                    string class_list = dt.Rows[i]["class_list"].ToString();
                    int new_channel_id = model.channel_id;
                    string new_class_list = model.class_list + class_list.Substring(class_list.IndexOf("," + category1 + ",")).TrimStart(',');
                    string new_class_layer = new_class_list.Trim(',').Split(',').Length.ToString();
                    if (i == 0)  
                    {
                        bll.UpdateField(id, "channel_id=" + new_channel_id + ", parent_id=" + category2 + ", class_list='" + new_class_list + "',class_layer=" + new_class_layer);
                    }
                    else
                    {
                        bll.UpdateField(id, "channel_id=" + new_channel_id + ", class_list='" + new_class_list + "',class_layer=" + new_class_layer);
                    }
                }
            }
           
            new BLL.managers.manager_log().Add(adminInfo.id, adminInfo.user_name, OSEnums.ActionEnum.Edit.ToString(), "移动栏目：" + bll.GetModel(category1).title); //记录日志
            context.Response.Write("{\"status\": 1, \"msg\": \"栏目转移成功！\"}");
        }

        #endregion


        #region 删除栏目==============================
        private void delete_category(HttpContext context)
        {
            //取得管理员登录信息
            Model.managers.manager adminInfo = new Web.UI.ManagePage().GetAdminInfo();
            if (adminInfo == null)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"未登录或已超时，请重新登录！\"}");
                return;
            }
            int category_id = OSRequest.GetQueryInt("category_id");
            if (category_id == 0)
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"参数错误！\"}");
                return;
            }
            BLL.contents.article_category bll = new BLL.contents.article_category();
            new BLL.managers.manager_log().Add(adminInfo.id, adminInfo.user_name, OSEnums.ActionEnum.Edit.ToString(), "删除栏目：" + bll.GetTitle(category_id)); //记录日志
            bll.Delete(category_id);
            context.Response.Write("{\"status\": 1, \"msg\": \"栏目删除成功！\"}");
        }
        #endregion


        private void file_validate(HttpContext context)
        {
            string file_name = OSRequest.GetString("param");
            string skin = OSRequest.GetString("skin");
            string type = OSRequest.GetString("type");
            if (string.IsNullOrEmpty(file_name))
            {
                context.Response.Write("{ \"info\":\"文件名不可为空\", \"status\":\"n\" }");
                return;
            }
            string sl = !string.IsNullOrEmpty(skin) ? "/" + skin : "";
            DirectoryInfo FatherDirectory = new DirectoryInfo(Utils.GetMapPath(@"../Template" + sl)); //当前目录
            FileInfo[] NewFileInfo = FatherDirectory.GetFiles();
            foreach (FileInfo DirFile in NewFileInfo)                    //获取此级目录下的所有文件
            {
                if (file_name + type == DirFile.Name.ToString())
                {
                    context.Response.Write("{ \"info\":\"该文件名已被占用，请更换！\", \"status\":\"n\" }");
                    return;
                }
            }
            context.Response.Write("{ \"info\":\"该文件名可使用\", \"status\":\"y\" }");
            return;
        }

        private void create_folder(HttpContext context)
        {
            string _name = OSRequest.GetFormString("_name");
            string folderName = OSRequest.GetFormString("folderName");
            if (string.IsNullOrEmpty(folderName))
            {
                context.Response.Write("{\"status\": 0, \"msg\": \"文件夹名称不可为空！\"}");
                return;
            }
            if (string.IsNullOrEmpty(_name))
            {
                try
                {
                    DirectoryInfo di = new DirectoryInfo(Utils.GetMapPath(@"../Template/" + folderName));
                    if (di.Exists)
                    {
                        context.Response.Write("{\"status\": 0, \"msg\": \"该文件夹已经存在，请更换！\"}");
                        return;
                    }
                    else
                    {
                        di.Create();
                        context.Response.Write("{\"status\": 1, \"msg\": \"创建文件夹成功！\"}");
                        return;
                    }
                }
                catch (Exception error)
                {
                    context.Response.Write("{ \"info\":\"创建文件夹失败！失败原因：" + error.ToString() + "\", \"status\":\"n\" }");
                    return;
                }
            }
            else {
                if (_name == folderName)
                {
                    context.Response.Write("{\"status\": 1, \"msg\": \"更改文件名成功！\"}");
                    return;
                }
                try
                {
                    DirectoryInfo di = new DirectoryInfo(Utils.GetMapPath(@"../Template/" + _name));
                    if (di.Exists)
                    {
                        di.MoveTo(Utils.GetMapPath(@"../Template/" + folderName));
                        context.Response.Write("{\"status\": 1, \"msg\": \"更改文件名成功！\"}");
                        return;
                    }
                }
                catch (Exception error)
                {
                    context.Response.Write("{ \"info\":\"更改文件名成功失败！原因：" + error.ToString() + "\", \"status\":\"n\" }");
                    return;
                }
            }

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
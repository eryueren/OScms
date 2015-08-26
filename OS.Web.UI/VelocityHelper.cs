using System;
using System.Collections.Generic;
using System.Web;
using System.Text;
using System.IO;
using NVelocity;
using NVelocity.App;
using NVelocity.Context;
using NVelocity.Runtime;
using Commons.Collections;


namespace  OS.Web.UI
{
    public class VelocityHelper
    {
        public string JavaScript = "";
        private VelocityEngine velocity = null;
        private IContext context = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="templatDir">模板文件夹路径</param>
        /// <param name="templatDir">模板所用编码</param>
        public VelocityHelper(string templatDir, string charset)
        {
            Init(templatDir, charset);
        }
        /// <summary>
        /// 无参数构造函数
        /// </summary>
        public VelocityHelper()
        {
            Init(".", "utf-8");
        }

        public VelocityHelper(object obj)
        {
            Init(".", "utf-8");
            Put("OS", obj);
          
        }


        /// <summary>
        /// 初始话NVelocity模块
        /// </summary>
        /// <param name="templatDir">模板文件夹路径</param>
        public void Init(string templatDir, string charset)
        {
            //创建VelocityEngine实例对象
            velocity = new VelocityEngine();

            //使用设置初始化VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, HttpContext.Current.Server.MapPath(templatDir));
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, charset);
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, charset);
            velocity.Init(props);

            //为模板变量赋值
            context = new VelocityContext();
        }
        /// <summary>
        /// 给模板变量赋值
        /// </summary>
        /// <param name="key">模板变量</param>
        /// <param name="value">模板变量值</param>
        public void Put(string key, object value)
        {
            if (context == null)
                context = new VelocityContext();
            context.Put(key, value);
        }
        /// <summary>
        /// 显示模板
        /// </summary>
        /// <param name="templatFileName">模板文件名</param>
        public void Display(string templatFileName)
        {
            //从文件中读取模板
            Template template = new Template();
            try
            {
                template = velocity.GetTemplate(templatFileName);
            }
            catch (Exception ex)
            {
 
            }
            //合并模板
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            //输出
            //            HttpContext.Current.Response.Clear();
            //            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.Write(writer.ToString());
            if (JavaScript.Length > 0)
            {
                HttpContext.Current.Response.Write("<script" + ">" + JavaScript + "</" + "script>");
            }
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 显示模板
        /// </summary>
        /// <param name="templatFileName">模板文件名</param>
        public string Displays(string templatFileName)
        {
            //从文件中读取模板
            //Template template = velocity.GetTemplate(templatFileName);
            Template template = velocity.GetTemplate(templatFileName, "UTF-8");
            //合并模板
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            return writer.ToString();

            //输出
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Write(writer.ToString());
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }

        /// <summary>
        /// 公共文件保存使用方法
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filePath">文件的服务器物理存储路径</param>
        public void CreatePublicFile(string content, string filePath, System.Text.Encoding endcode)
        {
            string Dir = Path.GetDirectoryName(filePath);
            if (!Directory.Exists(Dir))
            {
                Directory.CreateDirectory(Dir);
            }
            StreamWriter writer = new StreamWriter(filePath, false, endcode);
            writer.Write(content);
            writer.Flush();
            writer.Close();
            writer.Dispose();
        }

        /// <summary>
        /// 公共文件保存使用方法(文件编码为gb2312)
        /// </summary>
        /// <param name="content">文件内容</param>
        /// <param name="filePath">文件的服务器物理存储路径</param>
        public void CreatePublicFile(string content, string filePath)
        {
            CreatePublicFile(content, filePath, System.Text.Encoding.GetEncoding("utf-8"));
        }

    }
}

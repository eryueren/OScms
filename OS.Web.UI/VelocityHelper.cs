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
        /// ���캯��
        /// </summary>
        /// <param name="templatDir">ģ���ļ���·��</param>
        /// <param name="templatDir">ģ�����ñ���</param>
        public VelocityHelper(string templatDir, string charset)
        {
            Init(templatDir, charset);
        }
        /// <summary>
        /// �޲������캯��
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
        /// ��ʼ��NVelocityģ��
        /// </summary>
        /// <param name="templatDir">ģ���ļ���·��</param>
        public void Init(string templatDir, string charset)
        {
            //����VelocityEngineʵ������
            velocity = new VelocityEngine();

            //ʹ�����ó�ʼ��VelocityEngine
            ExtendedProperties props = new ExtendedProperties();
            props.AddProperty(RuntimeConstants.RESOURCE_LOADER, "file");
            props.AddProperty(RuntimeConstants.FILE_RESOURCE_LOADER_PATH, HttpContext.Current.Server.MapPath(templatDir));
            props.AddProperty(RuntimeConstants.INPUT_ENCODING, charset);
            props.AddProperty(RuntimeConstants.OUTPUT_ENCODING, charset);
            velocity.Init(props);

            //Ϊģ�������ֵ
            context = new VelocityContext();
        }
        /// <summary>
        /// ��ģ�������ֵ
        /// </summary>
        /// <param name="key">ģ�����</param>
        /// <param name="value">ģ�����ֵ</param>
        public void Put(string key, object value)
        {
            if (context == null)
                context = new VelocityContext();
            context.Put(key, value);
        }
        /// <summary>
        /// ��ʾģ��
        /// </summary>
        /// <param name="templatFileName">ģ���ļ���</param>
        public void Display(string templatFileName)
        {
            //���ļ��ж�ȡģ��
            Template template = new Template();
            try
            {
                template = velocity.GetTemplate(templatFileName);
            }
            catch (Exception ex)
            {
 
            }
            //�ϲ�ģ��
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            //���
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
        /// ��ʾģ��
        /// </summary>
        /// <param name="templatFileName">ģ���ļ���</param>
        public string Displays(string templatFileName)
        {
            //���ļ��ж�ȡģ��
            //Template template = velocity.GetTemplate(templatFileName);
            Template template = velocity.GetTemplate(templatFileName, "UTF-8");
            //�ϲ�ģ��
            StringWriter writer = new StringWriter();
            template.Merge(context, writer);
            return writer.ToString();

            //���
            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.Write(writer.ToString());
            //HttpContext.Current.Response.Flush();
            //HttpContext.Current.Response.End();
        }

        /// <summary>
        /// �����ļ�����ʹ�÷���
        /// </summary>
        /// <param name="content"></param>
        /// <param name="filePath">�ļ��ķ���������洢·��</param>
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
        /// �����ļ�����ʹ�÷���(�ļ�����Ϊgb2312)
        /// </summary>
        /// <param name="content">�ļ�����</param>
        /// <param name="filePath">�ļ��ķ���������洢·��</param>
        public void CreatePublicFile(string content, string filePath)
        {
            CreatePublicFile(content, filePath, System.Text.Encoding.GetEncoding("utf-8"));
        }

    }
}

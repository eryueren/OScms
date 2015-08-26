//======================================================
//==     (c)2008 aspxcms inc by NeTCMS v1.0              ==
//==          Forum:bbs.aspxcms.com                   ==
//==         Website:www.aspxcms.com                  ==
//======================================================
using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using  OS.Common;
using System.Text.RegularExpressions;

namespace  OS.Common
{
    /// <summary>
    /// ��ҳ������
    /// </summary>
    public class ProgressBar
    {
        /// <summary>
        /// �������ĳ�ʼ��
        /// </summary>
        public static void Start(int i)
        {
            Start("���ڼ���...",i);
        }
        /// <summary>
        /// �������ĳ�ʼ��
        /// </summary>
        /// <param name="msg">�ʼ��ʾ����Ϣ</param>
        public static void Start(string msg,int i)
        {
            string s = "<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n<head>\r\n<title></title>\r\n\r\n";

            //s += "<link href=\"../css/css.css\" rel=\"stylesheet\" type=\"text/css\" />\r\n";
            s += "<style>body {text-align:center;margin-top: 30px;}#ProgressBarSide" + i + " {height:25px;border:1px #2F2F2F;width:80%;background:#EEFAFF;margin:0 auto; }</style>\r\n";
            s += "<script language=\"javascript\">\r\n";
            s += "function SetPorgressBar(msg, pos)\r\n";
            s += "{\r\n";
            s += "document.getElementById('ProgressBar" + i + "').style.width = pos + \"%\";\r\n";
            s += "WriteText('Msg"+i+"',msg + \" �����\" + pos + \"%\");\r\n";
            s += "}\r\n";
            s += "function SetCompleted(msg)\r\n{\r\nif(msg==\"\")\r\nWriteText(\"Msg" + i + "\",\"��ɡ�\");\r\n";
            s += "else\r\nWriteText(\"Msg"+i+"\",msg);\r\n}\r\n";
            s += "function WriteText(id, str)\r\n";
            s += "{\r\n";
            s += "var strTag = '<span style=\"font-family:Verdana, Arial, Helvetica;font-size=11.5px;color:#DD5800\">' + str + '</span>';\r\n";
            s += "document.getElementById(id).innerHTML = strTag;\r\n";
            s += "}\r\n";
            s += "</script>\r\n</head>\r\n<body>\r\n";
            s += "<div id=\"Msg"+i+"\"><span style=\"font-family:Verdana, Arial, Helvetica;font-size=11.5px;color:#DD5800;\">" + msg + "</span></div>\r\n";
            s += "<div id=\"ProgressBarSide" + i + "\"  style=\"color:Silver;border-width:1px;border-style:Solid;\">\r\n";
            s += "<div id=\"ProgressBar" + i + "\" style=\"background-color:#008BCE; height:25px; width:0%;color:#fff; \"></div>\r\n";
            s += "</div>\r\n</body>\r\n</html>\r\n";
            s += "<br>\r\n";
            System.Web.HttpContext.Current.Response.Write(s);
            System.Web.HttpContext.Current.Response.Flush();
        }
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="Msg">�ڽ������Ϸ���ʾ����Ϣ</param>
        /// <param name="Pos">��ʾ���ȵİٷֱ�����</param>
        public static void Roll(string Msg, int Pos)
        {
            string jsBlock = "<script language=\"javascript\">SetPorgressBar('" + Msg + "'," + Pos + ");</script>";
            System.Web.HttpContext.Current.Response.Write(jsBlock);
            System.Web.HttpContext.Current.Response.Flush();
        }
    }
}

using System;
using System.Collections;
using System.Text;
using System.Xml;
using  OS.Common;
using System.Collections.Generic;

namespace  OS.DAL.configs
{
    public class urls_config
    {
        public Hashtable loadConfig(string urlFilePath)
        {
            Hashtable ht = new Hashtable();

            XmlDocument xml = new XmlDocument();
            xml.Load(urlFilePath);

            XmlNode root = xml.SelectSingleNode("urls");
            foreach (XmlNode n in root.ChildNodes)
            {
                if (n.NodeType != XmlNodeType.Comment && n.Name.ToLower() == "rewrite")
                {
                    XmlAttribute name = n.Attributes["name"];
                    XmlAttribute path = n.Attributes["path"];
                    XmlAttribute page = n.Attributes["page"];
                    XmlAttribute querystring = n.Attributes["querystrings"];
                    XmlAttribute pattern = n.Attributes["pattern"];

                    if (name != null && path != null && page != null && querystring != null && pattern != null)
                    {
                        ht.Add(name.Value, new Model.configs.urls_config(name.Value, path.Value, pattern.Value, page.Value.Replace("^", "&"),querystring.Value.Replace("^", "&")));
                    }
                }
            }
            return ht;
        }


        #region 增、删、改操作=================================================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Model.configs.urls_config model)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                XmlElement xe = doc.CreateElement("rewrite");
                if (!string.IsNullOrEmpty(model.Name))
                    xe.SetAttribute("name", model.Name);
                if (!string.IsNullOrEmpty(model.Path))
                    xe.SetAttribute("path", model.Path);
                if (!string.IsNullOrEmpty(model.Pattern))
                    xe.SetAttribute("pattern", model.Pattern);
                if (!string.IsNullOrEmpty(model.Page))
                    xe.SetAttribute("page", model.Page);
                if (!string.IsNullOrEmpty(model.QueryString))
                    xe.SetAttribute("querystrings", model.QueryString);
                XmlNode newXn = xn.AppendChild(xe);

                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Model.configs.urls_config model)
        {
            string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == model.Name.ToLower())
                    {
                        if (!string.IsNullOrEmpty(model.Path))
                            xe.SetAttribute("path", model.Path);
                        else if (xe.Attributes["path"] != null)
                            xe.Attributes["path"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.Pattern))
                            xe.SetAttribute("pattern", model.Pattern);
                        else if (xe.Attributes["Pattern"] != null)
                            xe.Attributes["pattern"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.Page))
                            xe.SetAttribute("page", model.Page);
                        else if (xe.Attributes["Page"] != null)
                            xe.Attributes["page"].RemoveAll();

                        if (!string.IsNullOrEmpty(model.QueryString))
                            xe.SetAttribute("querystrings", model.QueryString);
                        else if (xe.Attributes["querystrings"] != null)
                            xe.Attributes["querystrings"].RemoveAll();

                        doc.Save(filePath);
                        return true;
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                for (int i = xnList.Count - 1; i >= 0; i--)
                {
                    XmlElement xe = (XmlElement)xnList.Item(i);
                    if (xe.Attributes[attrName] != null && xe.Attributes[attrName].Value.ToLower() == attrValue.ToLower())
                    {
                        xn.RemoveChild(xe);
                    }
                }
                doc.Save(filePath);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    for (int i = xn.ChildNodes.Count - 1; i >= 0; i--)
                    {
                        XmlElement xe2 = (XmlElement)xn.ChildNodes.Item(i);
                        if (xe2.Attributes["name"].Value.ToLower() == xe.Attributes["name"].Value.ToLower())
                        {
                            xn.RemoveChild(xe2);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 导入节点
        /// </summary>
        public bool Import(XmlNodeList xnList)
        {
            try
            {
                string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
                XmlDocument doc = new XmlDocument();
                doc.Load(filePath);
                XmlNode xn = doc.SelectSingleNode("urls");
                foreach (XmlElement xe in xnList)
                {
                    if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                    {
                        if (xe.Attributes["name"] != null)
                        {
                            XmlNode n = doc.ImportNode(xe, true);
                            xn.AppendChild(n);
                        }
                    }
                }
                doc.Save(filePath);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region 扩展方法=====================================================
        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.configs.urls_config GetInfo(string attrValue)
        {
            Model.configs.urls_config model = new Model.configs.urls_config();
            string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            XmlNodeList xnList = xn.ChildNodes;
            if (xnList.Count > 0)
            {
                foreach (XmlElement xe in xnList)
                {
                    if (xe.Attributes["name"].Value.ToLower() == attrValue.ToLower())
                    {
                        if (xe.Attributes["name"] != null)
                            model.Name = xe.Attributes["name"].Value;

                        if (xe.Attributes["path"] != null)
                            model.Path = xe.Attributes["path"].Value;

                        if (xe.Attributes["pattern"] != null)
                            model.Pattern = xe.Attributes["pattern"].Value;

                        if (xe.Attributes["page"] != null)
                            model.Page = xe.Attributes["page"].Value;

                        if (xe.Attributes["querystrings"] != null)
                            model.QueryString = xe.Attributes["querystrings"].Value;
  
      
                        return model;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// 取得URL配制列表
        /// </summary>
        public List<Model.configs.urls_config> GetList()
        {
            List<Model.configs.urls_config> ls = new List<Model.configs.urls_config>();
            string filePath = Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode xn = doc.SelectSingleNode("urls");
            foreach (XmlElement xe in xn.ChildNodes)
            {
                if (xe.NodeType != XmlNodeType.Comment && xe.Name.ToLower() == "rewrite")
                {
                    if (xe.Attributes["name"] != null)
                    {
                        Model.configs.urls_config model = new Model.configs.urls_config();
                        if (xe.Attributes["name"] != null)
                            model.Name = xe.Attributes["name"].Value;

                        if (xe.Attributes["path"] != null)
                            model.Path = xe.Attributes["path"].Value;

                        if (xe.Attributes["pattern"] != null)
                            model.Pattern = xe.Attributes["pattern"].Value;

                        if (xe.Attributes["page"] != null)
                            model.Page = xe.Attributes["page"].Value;

                        if (xe.Attributes["querystrings"] != null)
                            model.QueryString = xe.Attributes["querystrings"].Value;

                        ls.Add(model);
                    }
                }
            }
            return ls;
        }

        #endregion
    }
}

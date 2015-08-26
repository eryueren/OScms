using System;
using System.Collections;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Collections.Generic;
using  OS.Common;

namespace OS.BLL.configs
{
    public class urls_config
    {
        private readonly DAL.configs.urls_config dal = new DAL.configs.urls_config();

        public Hashtable loadConfig(string urlFilePath)
        {
            //从缓存中根据键读取，并使用as转换
            Hashtable Cache_Siteurl = HttpContext.Current.Cache["Cache_Siteurl"] as Hashtable;
            if (Cache_Siteurl == null)
            {
                //创建缓存依赖项
                CacheDependency dependency = new CacheDependency(urlFilePath);
                //创建缓存
                HttpContext.Current.Cache.Add("Cache_Siteurl", dal.loadConfig(urlFilePath), dependency, Cache.NoAbsoluteExpiration, new TimeSpan(0, 30, 0), CacheItemPriority.Default, null);
                Cache_Siteurl = HttpContext.Current.Cache["Cache_Siteurl"] as Hashtable;
            }
            return Cache_Siteurl;
        }


        #region 增、删、改操作=================================================
        /// <summary>
        /// 增加节点
        /// </summary>
        public bool Add(Model.configs.urls_config model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 修改节点
        /// </summary>
        public bool Edit(Model.configs.urls_config model)
        {
            return dal.Edit(model);
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        public bool Remove(string attrName, string attrValue)
        {
            return dal.Remove(attrName, attrValue);
        }

        /// <summary>
        /// 批量删除节点
        /// </summary>
        public bool Remove(XmlNodeList xnList)
        {
            return dal.Remove(xnList);
        }
        #endregion

        #region 扩展方法=====================================================

        /// <summary>
        /// 返回URL映射List列表
        /// </summary>
        public List<Model.configs.urls_config> GetListAll()
        {
            List<Model.configs.urls_config> ls = CacheHelper.Get<List<Model.configs.urls_config>>(YLKeys.CACHE_SITE_URLS_LIST);
            if (ls == null)
            {
                CacheHelper.Insert(YLKeys.CACHE_SITE_URLS_LIST, dal.GetList(), Utils.GetXmlMapPath(YLKeys.FILE_URL_XML_CONFING));
                ls = CacheHelper.Get<List<Model.configs.urls_config>>(YLKeys.CACHE_SITE_URLS_LIST);
            }
            return ls;
        }

        /// <summary>
        /// 检查名称是否存在
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool Exists(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return false;
            }
            foreach (Model.configs.urls_config modelt in GetListAll())
            {
                if (modelt.Name == name)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 取得节点配制信息
        /// </summary>
        public Model.configs.urls_config GetInfo(string attrValue)
        {
            return dal.GetInfo(attrValue);
        }

        /// <summary>
        /// 根据频道名称返回URL映射列表
        /// </summary>
        public List<Model.configs.urls_config> GetList()
        {
            List<Model.configs.urls_config> ls = GetListAll();

            return ls;
        }


        #endregion

    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using  OS.Common;

namespace OS.BLL.configs
{
    public partial class siteconfig
    {
        private readonly DAL.configs.siteconfig dal = new DAL.configs.siteconfig();

        /// <summary>
        ///  读取配置文件
        /// </summary>
        public Model.configs.siteconfig loadConfig()
        {
            Model.configs.siteconfig model = CacheHelper.Get<Model.configs.siteconfig>(YLKeys.CACHE_SITE_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(YLKeys.CACHE_SITE_CONFIG, dal.loadConfig(Utils.GetXmlMapPath(YLKeys.FILE_SITE_XML_CONFING)),
                    Utils.GetXmlMapPath(YLKeys.FILE_SITE_XML_CONFING));
                model = CacheHelper.Get<Model.configs.siteconfig>(YLKeys.CACHE_SITE_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存配置文件
        /// </summary>
        public Model.configs.siteconfig saveConifg(Model.configs.siteconfig model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(YLKeys.FILE_SITE_XML_CONFING));
        }

    }
}

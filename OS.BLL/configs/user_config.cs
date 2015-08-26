using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Web;
using System.Web.Caching;
using  OS.Common;

namespace OS.BLL.configs
{
    public partial class userconfig
    {
        private readonly DAL.configs.userconfig dal = new DAL.configs.userconfig();

        /// <summary>
        ///  读取用户配置文件
        /// </summary>
        public Model.configs.userconfig loadConfig()
        {
            Model.configs.userconfig model = CacheHelper.Get<Model.configs.userconfig>(YLKeys.CACHE_USER_CONFIG);
            if (model == null)
            {
                CacheHelper.Insert(YLKeys.CACHE_USER_CONFIG, dal.loadConfig(Utils.GetXmlMapPath(YLKeys.FILE_USER_XML_CONFING)),
                    Utils.GetXmlMapPath(YLKeys.FILE_USER_XML_CONFING));
                model = CacheHelper.Get<Model.configs.userconfig>(YLKeys.CACHE_USER_CONFIG);
            }
            return model;
        }

        /// <summary>
        ///  保存用户配置文件
        /// </summary>
        public Model.configs.userconfig saveConifg(Model.configs.userconfig model)
        {
            return dal.saveConifg(model, Utils.GetXmlMapPath(YLKeys.FILE_USER_XML_CONFING));
        }

    }
}

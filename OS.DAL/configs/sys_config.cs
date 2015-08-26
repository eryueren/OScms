using System;
using System.Collections.Generic;
using System.Text;
using  OS.Common;

namespace  OS.DAL.configs
{
    /// <summary>
    /// 数据访问类:站点配置
    /// </summary>
    public partial class siteconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.configs.siteconfig loadConfig(string configFilePath)
        {
            return (Model.configs.siteconfig)SerializationHelper.Load(typeof(Model.configs.siteconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.configs.siteconfig saveConifg(Model.configs.siteconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}

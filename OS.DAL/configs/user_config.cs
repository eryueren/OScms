using System;
using System.Collections.Generic;
using System.Text;
using  OS.Common;

namespace  OS.DAL.configs
{
    /// <summary>
    /// 数据访问类:会员配置
    /// </summary>
    public partial class userconfig
    {
        private static object lockHelper = new object();

        /// <summary>
        ///  读取站点配置文件
        /// </summary>
        public Model.configs.userconfig loadConfig(string configFilePath)
        {
            return (Model.configs.userconfig)SerializationHelper.Load(typeof(Model.configs.userconfig), configFilePath);
        }

        /// <summary>
        /// 写入站点配置文件
        /// </summary>
        public Model.configs.userconfig saveConifg(Model.configs.userconfig model, string configFilePath)
        {
            lock (lockHelper)
            {
                SerializationHelper.Save(model, configFilePath);
            }
            return model;
        }

    }
}

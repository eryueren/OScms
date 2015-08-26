using System;
using System.Collections.Generic;
using System.Text;

namespace OS.BLL.contents
{
    public class article_images_size
    {
        private readonly Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //获得站点配置信息
        private readonly DAL.contents.article_images_size dal;

        public article_images_size()
		{
            dal = new DAL.contents.article_images_size(siteConfig.sysdatabaseprefix);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public List<Model.contents.article_images_size> GetCategoryImageSizeById(int category_id)
        {
            return dal.GetCategoryImageSizeById(category_id);
        }
    }
}

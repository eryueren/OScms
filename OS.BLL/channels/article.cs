using System;
using System.Data;
using System.Collections.Generic;
using  OS.Common;

namespace OS.BLL.channels
{
    /// <summary>
    /// ��������
    /// </summary>
    public partial class article
    {
        private readonly Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.channels.article dal;

        public article()
        {
            dal = new DAL.channels.article(siteConfig.sysdatabaseprefix);
        }

        public DataSet GetList( int Top, string strWhere)
        {
            return dal.GetList(Top, strWhere);
        }

        public DataSet GetList(string channel_name, int Top, string strWhere)
        {
            return dal.GetList(channel_name, Top, strWhere);
        }
        /// <summary>
        /// ��ҳ����
        /// </summary>
        public DataSet GetList(int pageSize,int pageIndex, string strWhere, string filedOrder, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, strWhere, filedOrder, out recordCount);
        }



    }
}
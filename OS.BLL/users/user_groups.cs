using System;
using System.Data;
using System.Collections.Generic;

namespace OS.BLL.users
{
    /// <summary>
    /// �û���(�ȼ�)
    /// </summary>
    public partial class user_groups
    {


        private readonly Model.configs.siteconfig siteConfig = new BLL.configs.siteconfig().loadConfig(); //���վ��������Ϣ
        private readonly DAL.users.user_groups dal;

        public user_groups()
        {
            dal = new DAL.users.user_groups(siteConfig.sysdatabaseprefix);
        }
       
        #region  ��������
        /// <summary>
        /// �Ƿ���ڸü�¼
        /// </summary>
        public bool Exists(int id)
        {
            return dal.Exists(id);
        }

        /// <summary>
        /// �����û�������
        /// </summary>
        public string GetTitle(int id)
        {
            return dal.GetTitle(id);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Add(Model.users.user_groups model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// ����һ������
        /// </summary>
        public bool Update(Model.users.user_groups model)
        {
            return dal.Update(model);
        }


        /// <summary>
        /// �õ�һ������ʵ��
        /// </summary>
        public Model.users.user_groups GetModel(int id)
        {
            return dal.GetModel(id);
        }

        /// <summary>
        /// ȡ��Ĭ�����ʵ��
        /// </summary>
        public Model.users.user_groups GetDefault()
        {
            return dal.GetDefault();
        }

        /// <summary>
        /// ���ݾ���ֵ�������������ʵ��
        /// </summary>
        public Model.users.user_groups GetUpgrade(int group_id, int exp)
        {
            return dal.GetUpgrade(group_id, exp);
        }

        /// <summary>
        /// ���ǰ��������
        /// </summary>
        public DataSet GetList(int Top, string strWhere, string filedOrder)
        {
            return dal.GetList(Top, strWhere, filedOrder);
        }

        #endregion  Method
    }
}
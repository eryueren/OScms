using System;
namespace OS.Model.contents
{
    /// <summary>
    /// Ƶ�����Ա�
    /// </summary>
    [Serializable]
    public partial class category_field
    {
        public category_field()
        { }
        #region Model
        private int _id;
        private int _categoryl_id;
        private int _field_id;
        /// <summary>
        /// ����ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// Ƶ��ID
        /// </summary>
        public int category_id
        {
            set { _categoryl_id = value; }
            get { return _categoryl_id; }
        }
        /// <summary>
        /// �ֶ�ID
        /// </summary>
        public int field_id
        {
            set { _field_id = value; }
            get { return _field_id; }
        }
        #endregion Model

    }
}
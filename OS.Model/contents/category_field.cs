using System;
namespace OS.Model.contents
{
    /// <summary>
    /// 频道属性表
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
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 频道ID
        /// </summary>
        public int category_id
        {
            set { _categoryl_id = value; }
            get { return _categoryl_id; }
        }
        /// <summary>
        /// 字段ID
        /// </summary>
        public int field_id
        {
            set { _field_id = value; }
            get { return _field_id; }
        }
        #endregion Model

    }
}
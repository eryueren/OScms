using System;
using System.Collections.Generic;

namespace OS.Model.contents
{
    /// <summary>
    /// 内容类别
    /// </summary>
    [Serializable]
    public partial class article_category
    {
        public article_category()
        { }
        #region Model
        private int _id;
        private int _channel_id = 0;
        private string _title;
        private string _call_index = "";
        private int _parent_id = 0;
        private string _class_list = "";
        private int _class_layer = 1;
        private int _sort_id = 99;
        private int _model_id = 0;
        private int _is_add_category = 0;
        private int _is_add_content = 0;
        private int _is_show_top = 0;
        private int _is_show_foot = 0;
        private int _is_albums = 0;
        private int _is_attach = 0;
        private int _page_size = 0;
        private string _link_url;
        private string _img_url;
        private string _content;
        private string _seo_title;
        private string _seo_keywords;
        private string _seo_description;
        private string _nav_type;
        private string _sub_title;
        private string _action_type = "";
        private int _is_sys = 0;
        private int _is_lock = 0;
        /// <summary>
        /// 自增ID
        /// </summary>
        public int id
        {
            set { _id = value; }
            get { return _id; }
        }
        /// <summary>
        /// 所属栏目ID
        /// </summary>
        public int channel_id
        {
            set { _channel_id = value; }
            get { return _channel_id; }
        }
        /// <summary>
        /// 类别标题
        /// </summary>
        public string title
        {
            set { _title = value; }
            get { return _title; }
        }
        /// <summary>
        /// 调用别名
        /// </summary>
        public string call_index
        {
            set { _call_index = value; }
            get { return _call_index; }
        }
        /// <summary>
        /// 父类别ID
        /// </summary>
        public int parent_id
        {
            set { _parent_id = value; }
            get { return _parent_id; }
        }
        /// <summary>
        /// 字类别ID列表(逗号分隔开)
        /// </summary>
        public string class_list
        {
            set { _class_list = value; }
            get { return _class_list; }
        }
        /// <summary>
        /// 类别深度
        /// </summary>
        public int class_layer
        {
            set { _class_layer = value; }
            get { return _class_layer; }
        }
        /// <summary>
        /// 排序数字
        /// </summary>
        public int sort_id
        {
            set { _sort_id = value; }
            get { return _sort_id; }
        }

        /// <summary>
        ///模板编号
        /// </summary>
        public int model_id
        {
            set { _model_id = value; }
            get { return _model_id; }
        }
        /// <summary>
        ///  是否添加栏目
        /// </summary>
        public int is_add_category
        {
            set { _is_add_category = value; }
            get { return _is_add_category; }
        }
        /// <summary>
        ///     是否添加内容
        /// </summary>
        public int is_add_content
        {
            set { _is_add_content = value; }
            get { return _is_add_content; }
        }
        /// <summary>
        ///      是否在首页头部显示
        /// </summary>
        public int is_show_top
        {
            set { _is_show_top = value; }
            get { return _is_show_top; }
        }
        /// <summary>
        ///        是否在首页底部显示
        /// </summary>
        public int is_show_foot
        {
            set { _is_show_foot = value; }
            get { return _is_show_foot; }
        }
        /// <summary>
        /// 是否开启相册功能
        /// </summary>
        public int is_albums
        {
            set { _is_albums = value; }
            get { return _is_albums; }
        }
        /// <summary>
        /// 是否开启附件功能
        /// </summary>
        public int is_attach
        {
            set { _is_attach = value; }
            get { return _is_attach; }
        }
        /// <summary>
        /// 每页显示数量
        /// </summary>
        public int page_size
        {
            set { _page_size = value; }
            get { return _page_size; }
        }
        /// <summary>
        /// URL跳转地址
        /// </summary>
        public string link_url
        {
            set { _link_url = value; }
            get { return _link_url; }
        }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string img_url
        {
            set { _img_url = value; }
            get { return _img_url; }
        }
        /// <summary>
        /// 备注说明
        /// </summary>
        public string content
        {
            set { _content = value; }
            get { return _content; }
        }
        /// <summary>
        /// SEO标题
        /// </summary>
        public string seo_title
        {
            set { _seo_title = value; }
            get { return _seo_title; }
        }
        /// <summary>
        /// SEO关健字
        /// </summary>
        public string seo_keywords
        {
            set { _seo_keywords = value; }
            get { return _seo_keywords; }
        }
        /// <summary>
        /// SEO描述
        /// </summary>
        public string seo_description
        {
            set { _seo_description = value; }
            get { return _seo_description; }
        }
        private List<article_images_size> _imagesize_values;
        /// <summary>
        /// 图片尺寸
        /// </summary>
        public List<article_images_size> imagesize_values
        {
            set { _imagesize_values = value; }
            get { return _imagesize_values; }
        }

        private List<category_field> _category_fields;
        /// <summary>
        /// 扩展字段 
        /// </summary>
        public List<category_field> category_fields
        {
            set { _category_fields = value; }
            get { return _category_fields; }
        }


        /// <summary>
        /// 
        /// </summary>
        public string nav_type
        {
            set { _nav_type = value; }
            get { return _nav_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string sub_title
        {
            set { _sub_title = value; }
            get { return _sub_title; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string action_type
        {
            set { _action_type = value; }
            get { return _action_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int is_sys
        {
            set { _is_sys = value; }
            get { return _is_sys; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int is_lock
        {
            set { _is_lock = value; }
            get { return _is_lock; }
        }

        #endregion Model
    }
}
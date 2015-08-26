using System;
namespace OS.Model.configs
{
	/// <summary>
	/// yl_temp:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class yl_temp
	{
		public yl_temp()
		{}
		#region Model
		private int _id;
		private string _name;
		private string _title;
		private int? _status=0;
		/// <summary>
		/// 
		/// </summary>
		public int id
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string name
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string title
		{
			set{ _title=value;}
			get{return _title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int? status
		{
			set{ _status=value;}
			get{return _status;}
		}
		#endregion Model

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
	}
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace OScms.Web {
	public partial class Site : System.Web.UI.MasterPage {
		public int id = OS.Common.OSRequest.GetInt("top", 0);
		protected void Page_Load(object sender, EventArgs e) {

		}
	}
}
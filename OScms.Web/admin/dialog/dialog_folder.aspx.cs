using System;
using System.Data;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OS.Common;

namespace OS.Web.admin.dialog
{
    public partial class dialog_folder : Web.UI.ManagePage
    {
        private string filename = OSRequest.GetString("name");
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(filename))
                {
                    this.txtFolderName.Value = filename.Trim();
                    this._name.Value = filename;
                }
            }
        }

    }
}
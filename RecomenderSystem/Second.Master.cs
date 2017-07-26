using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace RecomenderSystem
{
    public partial class Second : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Roles.IsUserInRole("Admin"))
            {
                lbladmin.Visible = true;
            }
            else
            {
                lbladmin.Visible = false;
            }
        }
    }
}

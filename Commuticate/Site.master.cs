using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoginLink_PreRender(object sender, EventArgs e)
    {
        if (Program.UserLoggedIn)
        {
            this.LoginLink.Text = "Log Out";
            this.LoginLink.NavigateUrl = "~/Logout/";
        }
    }
}
 
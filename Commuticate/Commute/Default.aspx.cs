using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Routes_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        using(var elm = new Commuticate.Repository.Entities())
        {
            var rrr = (from rg in elm.RouteGroups where rg.CommuterId == Program.UserId select rg);
            RouteGroupRepeater.DataSource = rrr;
            RouteGroupRepeater.DataBind();
        }    
    }
}
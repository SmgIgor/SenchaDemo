using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Commuticate.Repository;

public partial class Commute_View_Default : System.Web.UI.Page
{
    public int RouteGroupId
    {
        get
        {
            int i = 0;
            Int32.TryParse(Request["ID"], out i);
            return i;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.PreRender += new EventHandler(Commute_View_Default_PreRender);

        using (var elm = new Entities())
        {
            var grp = elm.RouteGroups.FirstOrDefault(g => g.Id == this.RouteGroupId);
            if (grp != null)
            {
                var rIds = (from rgr in grp.RouteGroupRoutes select rgr.RouteId);

                var rout = elm.Routes.Where(r => rIds.Contains(r.Id));

                this.RouteRepeater.DataSource = rout;
                this.RouteRepeater.DataBind();
            }            
        }
    }

    void Commute_View_Default_PreRender(object sender, EventArgs e)
    {
        using (var elm = new Entities())
        {
            var grp = elm.RouteGroups.FirstOrDefault(g => g.Id == this.RouteGroupId);
            if (grp != null)
                this.RouteGroupNameTextbox.Text = grp.Description;
            else
                Response.Redirect("/Commute/");
        }
    }

    protected  void UpdateGroupName_Click(object sender, EventArgs e)
    {
        using (var elm = new Entities() )
        {
            var grp = elm.RouteGroups.FirstOrDefault(g => g.Id == this.RouteGroupId);
            if (grp != null)
            {
                grp.Description = this.RouteGroupNameTextbox.Text;
                elm.SaveChanges();
            }
            
        }
    }
}
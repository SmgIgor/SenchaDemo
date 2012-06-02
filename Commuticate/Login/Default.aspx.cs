using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login_Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void Login_Click(object sender, EventArgs e)
    {
        Program.UserId = 0;

        using (var ent = new Commuticate.Repository.Entities())
        {
            var comm = ent.Commuters.FirstOrDefault(
                c => c.Email.Equals(this.LoginUserName.Text, StringComparison.InvariantCultureIgnoreCase)
                     && c.Password.Equals(this.LoginPassword.Text, StringComparison.Ordinal));

            
            if (comm != null)
            {
                Program.UserId = comm.Id;
                Response.Redirect("~/");
            }
        }

        this.StatusMessageLiteral.Text = "Incorrect Login";
    }

    protected void StatusMessage_OnPreRender(object sender, EventArgs e)
    {
        var lit = sender as Literal;
        if (lit != null)
            lit.Parent.Visible = !string.IsNullOrEmpty(lit.Text);
    }
}
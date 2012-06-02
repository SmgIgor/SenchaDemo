using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CommuterAccountWebControl : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.PreRender  += OnPreRender;
        this.AccountPanel.PreRender += new EventHandler(AccountPanel_PreRender);

        if (Program.UserLoggedIn)
        {
            using (var ent = new Commuticate.Repository.Entities())
            {
                var comm = ent.Commuters.FirstOrDefault(i => i.Id == Program.UserId);

                if (comm != null)
                {
                    this.FullNameTextBox.Text = this.UserFullnameHeader.Text = comm.FullName;
                    this.EmailTextBox.Text = this.UserEmailLiteral.Text = comm.Email;
                    this.PasswordTextBox.Text = comm.Password;

                    var pData = comm.CommuterDatas.FirstOrDefault(r => r.DataType == "Phone");
                    if (pData != null)
                        this.UserPhoneLiteral.Text = pData.DataValue;
                }
            }
        }
    }

    protected void OnPreRender(object sender, EventArgs eventArgs)
    {
        this.NewAccountPanel.Visible = !Program.UserLoggedIn;
        this.AccountPanel.Visible = !this.NewAccountPanel.Visible;
    }

    protected void Edit_Click(object sender, EventArgs e)
    {
        this.CreateAccount.Visible = true;
        this.AccountPanel.Visible = false;
    }

    protected void AccountPanel_PreRender(object sender, EventArgs e)
    {
        
    }



    protected void CreateAccount_Click(object sender, EventArgs e)
    {
        using (var ent = new Commuticate.Repository.Entities())
        {
            var comm = new Commuticate.Repository.Commuter()
                           {Email = EmailTextBox.Text, FullName = FullNameTextBox.Text, Password = PasswordTextBox.Text};

            ent.AddToCommuters(comm);
            ent.SaveChanges();

            Program.UserId = comm.Id;
        }
    }
}
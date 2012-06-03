<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CommuterAccountControl.ascx.cs" Inherits="CommuterAccountWebControl" %>

<div class="commuterInfo">
<asp:Panel id="NewAccountPanel" runat="server" visible="True">
    <div class="form">
        <header><h2>Signup</h2></header>

        <div class="elem">
            <label>Name: <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="FullNameTextBox" ValidationGroup="CreateAccount" /></label>
            <asp:TextBox runat="server" ID="FullNameTextBox" ValidationGroup="CreateAccount" />
        </div>
        
        <div class="elem">
            <label>Email: <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="EmailTextBox" ValidationGroup="CreateAccount" /></label>
            <asp:TextBox runat="server" ID="EmailTextBox" ValidationGroup="CreateAccount" />
        </div>
        
        <div class="elem">
            <label>Password: <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="PasswordTextBox" ValidationGroup="CreateAccount" /></label>
            <asp:TextBox runat="server" ID="PasswordTextBox" ValidationGroup="CreateAccount" />
        </div>

        <div class="elem">
            <label>&nbsp;</label> 
            <asp:Button runat="server" ID="CreateAccount" Text="Create" ValidationGroup="CreateAccount" OnClick="CreateAccount_Click" />
        </div>
    </div>
</asp:Panel>

<asp:Panel id="AccountPanel" runat="server" visible="False">
    <header>
        <h3>
            <asp:Literal runat="server" ID="UserFullnameHeader"/>
            <asp:LinkButton runat="server" OnClick="Edit_Click" Text="[ edit ]" CausesValidation="False" />
        </h3>
    </header>
    
    <div><label>Email: </label><asp:Literal runat="server" ID="UserEmailLiteral" /></div>
    <div><label>Phone: </label><asp:Literal runat="server" ID="UserPhoneLiteral" /></div>

</asp:Panel>
</div>
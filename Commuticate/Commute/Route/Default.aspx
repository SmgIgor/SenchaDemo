<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Commute_Route_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="form">
        <header>Route</header>
        
        <div class="elem">
            <label>Name: <asp:RequiredFieldValidator runat="server" ControlToValidate="NameTextBox" ErrorMessage="*" Text="*"/> </label>
            <asp:TextBox runat="server" ID="NameTextBox"/>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>


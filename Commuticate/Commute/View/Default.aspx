<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Commute_View_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
   <div class="form">
       <div class="elem">
           <label>Name: <asp:RequiredFieldValidator runat="server" ValidationGroup="GroupName" ErrorMessage="*" Text="*" ControlToValidate="RouteGroupNameTextbox" /></label>
           <asp:TextBox runat="server" ID="RouteGroupNameTextbox" ValidationGroup="GroupName"/>
           <asp:LinkButton runat="server" ValidationGroup="GroupName" OnClick="UpdateGroupName_Click" ></asp:LinkButton>
       </div>
   </div>    
   
   <div>
       
       <asp:Repeater runat="server" ID="RouteRepeater">
           <ItemTemplate>
               <div> <a href='<%# String.Format("/Commute/Route/?id={0}&gid={1}", Eval("ID"), this.RouteGroupId) %>'><%# Eval("Description") %></a></div>
           </ItemTemplate>

       </asp:Repeater>

   </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>


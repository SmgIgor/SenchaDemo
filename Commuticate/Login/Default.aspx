﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Login_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <div class="form">
        <asp:Panel runat="server" EnableViewState="False" Visible="False" CssClass="StatusMsg">
            <asp:Literal ID="StatusMessageLiteral" runat="server" OnPreRender="StatusMessage_OnPreRender" />
        </asp:Panel>

        <div class="elem">
            <label>Username:</label>
            <asp:TextBox ID="LoginUserName" runat="server" />
        </div>
        
        <div class="elem">
            <label>Password:</label>
            <asp:TextBox ID="LoginPassword" runat="server" TextMode="Password" />
        </div>
        
        <div class="elem">
            <label></label>
            <asp:Button runat="server" ID="LoginButton" Text="Login" OnClick="Login_Click"/>
        </div>
    </div>
</asp:Content>


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Routes_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="HeaderContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" Runat="Server">
    
    <h3>Route Groups</h3>
    <asp:Repeater runat="server" EnableViewState="False" ID="RouteGroupRepeater">
       
        <HeaderTemplate>
        </HeaderTemplate>
        
        <ItemTemplate>
            <div class="commute">
                <a href='<%# Eval("ID", "/Commute/View/?ID={0}")%>'>
                    <%# Eval("Description") %>
                </a>
            </div>
        </ItemTemplate>

        <FooterTemplate>
            
        </FooterTemplate>
    </asp:Repeater>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="FooterContent" Runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="ScriptContent" Runat="Server">
</asp:Content>


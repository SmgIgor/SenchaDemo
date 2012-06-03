<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="Default.aspx.cs" Inherits="_Default" %>
    
<%@ Register Src="~/CommuterAccountControl.ascx" TagName="CommuterAccount" TagPrefix="uc1" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="rightColumn">
        <img src="images/phone.png" alt="phone"/>

    </div>
    <div class="leftColumn">
        <div class="intro">
            <header><h2>Commute</h2></header>
            <p>
                Everyday we commute to work, home, school and even to grandma's house. <br />
                We setout blindly, hoping the traffic and weather on the 9's tells you of the accident before there is no turning back. To only have the traffic report come in a minute to late and a dollar short.
            </p>
            <p>
                Imagine the day when you no longer have to guess which route is best. Imangine if you were alerted on your phone to traffic and weather conditions before you begain your commute without waiting on your local news.<br/>
            </p>
            <p>Imagine no more <b>Commuticate</b> is here.</p>
        </div>
        <uc1:CommuterAccount ID="CommuterControl" runat="server" />
    </div>
</asp:Content>
<asp:Content runat="server" ContentPlaceHolderID="ScriptContent"></asp:Content>
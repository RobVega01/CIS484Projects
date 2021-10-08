<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="2-HomePage.aspx.cs" Inherits="CIS484Projects.WebForm4" %>


<asp:Content ID="homePageHeader" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
    <section id="welcomeUser">Current User: "<%:Session["UserName"]%>"</section>
     <h1>StoryAnalyzer</h1>
</asp:Content>

<asp:Content ID="homePageBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
    h2 
        {
            position: absolute;
            top: 170px;
            left: 30px;
        }
    #LogoutModBox 
        {
            position: absolute;
            border: inset;
            bottom: 150px;
            right: 125px;
            height: 260px;
            width: 400px;
            padding: 30px;
            background-color:#2D8F78;
        }
    #orgBioBox 
        {
            position: absolute;
            border: inset;
            bottom: 180px;
            left: 30px;
            height: 220px;
            width: 700px;
            padding: 30px;
            font-size: 24px;
            text-align:center;
            margin-bottom: 10px;
            background-color: #2D8F78;
        }
    </style>
    <br />
    <br />
    
    
    <h2 style="font-family:Garamond">Welcome to the StoryAnalyzer Home Page!</h2>
    <br />
    <br />

<!-- Box of text describing the purpose of StoryAnalyzer as a tool to writers. -->
    <section id="orgBioBox">
        Would you like to add a bio to your organization? (optional) <br /><br />
        <asp:TextBox ID="UpdateOrgBioTextBox" runat="server" TextMode="MultiLine" style="height:145px; width:650px; resize:none;"></asp:TextBox>
        <br />
        <asp:Button ID="UpdateOrgBioBtn" runat="server" Text="Update" OnClick="UpdateOrgBioBtn_Click" />
    </section>

<!-- Home Page Logout Button/Modification Fields -->
    <section id="LogoutModBox">
        <h3 align="right">

            <!--Fields and Buttons Still Need to be Connected-->
            Change User Information: <br />
            <asp:Label ID="ChangeFirstLbl" runat="server" Text="Update First Name: "></asp:Label>
            <asp:TextBox ID="ChangeFirstTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="ChangeLastLbl" runat="server" Text="Update Last Name: "></asp:Label>
            <asp:TextBox ID="ChangeLastTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="ChangePasswordLbl" runat="server" Text="Update Password: "></asp:Label>
            <asp:TextBox ID="ChangePasswordTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="ChangeEmailLbl" runat="server" Text="Update Email: "></asp:Label>
            <asp:TextBox ID="ChangeEmailTextBox" runat="server"></asp:TextBox>
            <br />
            <asp:Label ID="ChangeOrgLbl" runat="server" Text="Update Organization: "></asp:Label>
            <asp:TextBox ID="ChangeOrgTextBox" runat="server"></asp:TextBox>
            <br /><br />
            <asp:Label ID="ErrorEmptyLbl" runat="server" style="font-size:small" Text=""></asp:Label><br />
            <asp:Label ID="ErrorBadEmailLbl" runat="server" style="font-size:small" Text=""></asp:Label><br /><br />
            <asp:Button ID="UpdateAllInfoBtn" runat="server" Text="Update All User Info" OnClick="UpdateAllInfoBtn_Click"/>
            <br /><br />
            
        </h3>
    </section>
    <asp:Button ID="LogoutButton" runat="server" Text="Log Out From This User Account" 
                OnClick="LogoutButton_Click" style="position:absolute; right: 10px; top: 50px;"/>

<!--NavButton Section-->
    <section id="btnSegment">
        <asp:Button ID="navToTextsBtn" runat="server" Text="View Uploaded Texts" 
            OnClick="navToTextsBtn_Click" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
        <asp:Button ID="navToAnalysisBtn" runat="server" Text="View Analyses" 
            OnClick="navToAnalysisBtn_Click" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
    </section>
    
    
</asp:Content>
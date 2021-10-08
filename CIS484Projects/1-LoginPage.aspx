<%@ Page Title="StoryAnalyzer Home" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="1-LoginPage.aspx.cs" Inherits="CIS484Projects.WebForm1" %>


<asp:Content ID="homePageHeader" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
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
    #LoginBox 
        {
            position: absolute;
            border: inset;
            bottom: 190px;
            right: 125px;
            height: 220px;
            width: 400px;
            padding: 30px;
            background-color:#2D8F78;
        }
    #descriptionBox 
        {
            position: absolute;
            border: inset;
            bottom: 180px;
            left: 30px;
            height: 220px;
            width: 700px;
            padding: 30px;
            font-size: 32px;
            font-family:Garamond;
            text-align:center;
            margin-bottom: 10px;
            background-color: #2D8F78;
        }
    #registrationBox 
        {
            position: absolute;
            border: inset;
            top: 100px;
            right: 125px;
            height: 60px;
            width: 200px;
            background-color: #2D8F78;
            text-align:center;
        }
    </style>
    <br />
    <br />
    
    
    <h2 style="font-family:Garamond">Welcome to StoryAnalyzer! Please Login to Access the Service.</h2>
    <br />
    <br />

<!-- Box of text describing the purpose of StoryAnalyzer as a tool to writers. -->
    <section id="descriptionBox">
        StoryAnalyzer is an analysis tool in which you can post your writings and send them to our special analysis tool.
        StoryAnalyzer then stores the analyses provided by the tool so you can consistently improve your writing!
        StoryAnalyzer is a free service, so sign up and improve your writing!
    </section>
<!-- Box for User Registration -->
    <section id="registrationBox">
        <br />
        <asp:Button ID="registrationNavButton" runat="server" Text="Register as New User" OnClick="registrationNavButton_Click"/>
    </section>

<!-- Home Page Login Link -->
    <section id="LoginBox">
        <h3 align="right">
            <asp:Label ID="LoginLabel" runat="server" Text="Please enter your email and password to login."></asp:Label>
            <br />
            <br />
            Email: <asp:TextBox ID="EmailForLoginTextBox" runat="server" AutoComplete="on"></asp:TextBox><br />
            Password: <asp:TextBox ID="PasswordForLoginTextBox" runat="server" AutoComplete="on"></asp:TextBox><br />
            <asp:Button ID="LoginBtn" runat="server" Text="Login" OnClick="LoginBtn_Click"/> 
            <asp:Button ID="AdminLoginBtn" runat="server" Text="Auto-Login Button" OnClick="AdminLoginBtn_Click"/>
            <br /><br />
            <asp:Label ID="ResultLabel" runat="server" Text=""></asp:Label>
        </h3>
    </section>
            
<!-- No Nav Buttons on Login Page, Auto-Redirects to Home Page on Successful Login -->
</asp:Content>

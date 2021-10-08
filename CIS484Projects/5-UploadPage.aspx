<%@ Page Title="Upload a New Text" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="5-UploadPage.aspx.cs" Inherits="CIS484Projects.WebForm2" %>

<asp:Content ID="txtUploadHeader" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
    <section id="welcomeUser">Current User: "<%:Session["UserName"]%>"</section>
    <h1>StoryAnalyzer</h1>
    <style>
        #errorSegment {
            position: absolute;
            top: 190px;
            right: 300px;
        }
    </style>
</asp:Content>

<asp:Content ID="txtUploadBody" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<!--Text Boxes and Their Titles-->
    <h3 style="position:absolute; top: 150px; left: 100px; font-family:Garamond;">Title of Document:</h3>
    <asp:TextBox ID="nameUpload" runat="server" style="width: 400px; position: absolute; top: 190px; left: 100px;"></asp:TextBox>
    <asp:FileUpload ID="TextFileUpload" Text="Choose File to Read" runat="server" style="position:absolute; top: 160px; left: 550px;"/>
    <asp:Button ID="fileReaderBtn" runat="server" Text="Read Selected File into Textbox" style="position:absolute; top: 190px; left: 550px;" OnClick="fileReaderBtn_Click" />
    <h3 style="position:absolute; top: 200px; left: 100px; font-family:Garamond;">Document Text (copy and paste into box):</h3>
    <asp:TextBox ID="textContentUpload" runat="server" style=" resize:none; width: 735px; height: 250px; 
        position: absolute; top: 240px; left: 100px;" TextMode="MultiLine"></asp:TextBox>
    <br />
    
    <section id="errorSegment">
        <asp:Label ID="ContentErrorLabel" runat="server" Text=""></asp:Label> <br />
        <asp:Label ID="NameErrorLabel" runat="server" Text=""></asp:Label> <br />
    </section>

    <section id="uploadBtnSegment" style="position: absolute; bottom: 200px; right: 695px;">
        <asp:Button ID="uploadNewTextBtn" runat="server" OnClick="uploadNewTextBtn_Click" Text="Upload/Update Text"/>
    </section>
    

<!--NavButton Section-->
    <section id="btnSegment">
            <asp:Button ID="navToHomepageBtn" runat="server" OnClick="navToHomepageBtn_Click"
            Text="Return To Home Page" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
        Height="50px" Width="200px" Font-Size="Larger" />
            <asp:Button ID="navToTextsBtn" runat="server" OnClick="navToTextsBtn_Click" 
            Text="View Uploaded Texts" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
        Height="50px" Width="200px" Font-Size="Larger" />
    </section>
    
    

</asp:Content>

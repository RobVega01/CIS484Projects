<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="3-ViewTextsPage.aspx.cs" Inherits="CIS484Projects.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
    <section id="welcomeUser">Current User: "<%:Session["UserName"]%>"</section>
    <h1>StoryAnalyzer</h1>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
<!--NavButton Section-->
    <br /><br /><br /><br /><br /><br />

    <section id="TextReaderSegment">
        <asp:Label ID="TextReaderLbl" runat="server" Text="Select the text you would like to read from the dropdown menu."></asp:Label>
        <br />
        
        <asp:DropDownList 
            ID="DropDownList1" 
            DataSourceID = "Lab2TextTitles"
            DataTextField = "textName" 
            runat="server"
            AutoPostBack="true"
            OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>

        <br /><br />
        <h3>Contents of File:</h3>
        <asp:TextBox ID="TextReaderContent" runat="server" TextMode="MultiLine" ReadOnly="true" style="height:200px;
        width:700px; resize:none;" AutoPostBack="true"></asp:TextBox>
    </section>

    <section id="btnSegment">
        <asp:Button ID="navToHomepageBtn" runat="server" OnClick="navToHomepageBtn_Click"
            Text="Return To Home Page" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
        <asp:Button ID="navToUploadBtn" runat="server" OnClick="navToUploadBtn_Click" 
            Text="Upload A New Text" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger"/>
        <asp:Button ID="navToEditingBtn" runat="server" OnClick="navToEditingBtn_Click" 
            Text="Edit an Existing Text" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
    </section>

        <asp:SqlDataSource ID="Lab2TextTitles" 
            ConnectionString="<%$ ConnectionStrings:Lab3 %>" 
            SelectCommand="SELECT textName 
                FROM Texts t, Users u 
                WHERE t.userID=u.userID 
                AND u.email = @email"
            runat="server">
            <SelectParameters>
                <asp:SessionParameter Name="email" SessionField="UserName" Type="String"/>
            </SelectParameters>
        </asp:SqlDataSource>

</asp:Content>
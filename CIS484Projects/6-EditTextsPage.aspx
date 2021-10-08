<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="6-EditTextsPage.aspx.cs" Inherits="CIS484Projects.WebForm6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
    <section id="welcomeUser">Current User: "<%:Session["UserName"]%>"</section>
    <h1>StoryAnalyzer</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

    <section id="TextEditSection" style="position:absolute; left:100px; top:140px;">
        Choose which story to edit:<br />
        <asp:DropDownList ID="EditDropDownList" runat="server"
            DataSourceID="TextEditSqlDataSource"
            DataTextField="textName" 
            AutoPostBack="true" 
            OnSelectedIndexChanged="EditDropDownList_SelectedIndexChanged">
        </asp:DropDownList>

        <br /><br />
        <asp:Label ID="TitleLbl" runat="server" Text="Edit Story Title: "></asp:Label><br />
        <asp:TextBox ID="TitleTextBox" runat="server" Width="500px"></asp:TextBox><br />
        <asp:Label ID="ContentLbl" runat="server" Text="Edit Story Content: "></asp:Label><br />
        <asp:TextBox ID="ContentTextBox" runat="server" style="height:200px; width: 700px; resize: none;" TextMode="MultiLine"></asp:TextBox>
        <br /><br />
        <asp:Button ID="UpdateTextBtn" runat="server" Text="Save Edits" OnClick="UpdateTextBtn_Click" />
    </section>
    
    <section id="btnSegment">
        <asp:Button ID="ReturnToHomeBtn" runat="server" OnClick="ReturnToHomeBtn_Click"
            Text="Return to Home" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
        <asp:Button ID="navToTextsBtn" runat="server" OnClick="navToTextsBtn_Click" 
            Text="Return to Texts" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
    </section>
    <asp:SqlDataSource ID="TextEditSqlDataSource" 
        runat="server" 
        ConnectionString="<%$ ConnectionStrings:Lab2 %>" 
        SelectCommand="SELECT textName 
                FROM Texts t, Users u 
                WHERE t.userID=u.userID 
                AND u.email = @email">
        <SelectParameters>
                <asp:SessionParameter Name="email" SessionField="UserName" Type="String"/>
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

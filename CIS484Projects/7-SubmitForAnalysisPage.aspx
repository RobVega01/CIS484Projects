<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="7-SubmitForAnalysisPage.aspx.cs" Inherits="CIS484Projects.WebForm7" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
    <section id="welcomeUser">Current User: "<%:Session["UserName"]%>"</section>
    <h1>StoryAnalyzer</h1>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    
    <section id="AnalysisSubmSection" style="position:absolute; top:150px;">
        <asp:Label ID="AnalysisLbl" runat="server" Text="Select Story for Analysis:"></asp:Label>
        <asp:DropDownList ID="AnalysisDDL" runat="server"
            DataSourceID="AnalysisSubmSqlDataSource"
            DataTextField="textName" 
            AutoPostBack="true">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="AnSubBtn" 
            runat="server" 
            Text="Submit for Analysis" 
            OnClick="AnSubBtn_Click" />
    </section>

    <section id="btnSegment">
        <asp:Button ID="ReturnToHomeBtn" runat="server" OnClick="ReturnToHomeBtn_Click"
            Text="Return to Home" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
        <asp:Button ID="NavToAnalysesBtn" runat="server" Text="Return to Analyses" 
            OnClick="NavToAnalysesBtn_Click" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
        
    </section>
    <asp:SqlDataSource ID="AnalysisSubmSqlDataSource" 
        ConnectionString="<%$ ConnectionStrings:Lab2 %>" 
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

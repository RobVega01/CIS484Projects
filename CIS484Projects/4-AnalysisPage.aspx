<%@ Page Title="View Analyses" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="4-AnalysisPage.aspx.cs" Inherits="CIS484Projects.WebForm3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    Created By Conor Hay and Robert Vega
    <section id="welcomeUser">Current User: "<%:Session["UserName"]%>"</section>
    <h1>StoryAnalyzer</h1>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <br /><br /><br /><br /><br /><br /><br /><br />
    

    All Stories with Analyses
    <br />
    <asp:DropDownList 
        ID="TextDropDownList" 
        DataSourceID = "Lab2TextTitles"
        DataTextField = "textName" 
        runat="server"
        OnSelectedIndexChanged="TextDropDownList_SelectedIndexChanged"
        AutoPostBack="true">
        <asp:ListItem Index="0">--Select Story--</asp:ListItem>
    </asp:DropDownList>

    <br />Analyses For Chosen Story<br />
    <asp:DropDownList ID="AnalysisDropDownList" 
        runat="server" 
        AutoPostBack="true"
        AppendDataBoundItems="true"
        DataTextField="analysisName"
        DataValueField="analysisID" 
        OnSelectedIndexChanged="AnalysisDropDownList_SelectedIndexChanged">
    </asp:DropDownList>

    
    <br />
    Story Analysis
    <br />
    <asp:TextBox ID="AnalysisDisplayTextBox" runat="server" TextMode="MultiLine" 
        style="position:absolute; top:290px;  
        width:500px; height:200px; resize:none;" 
        ReadOnly="true">
    </asp:TextBox>
    
<!--NavButton Section-->
    <section id="btnSegment">
        <asp:Button ID="NavToHomePageBtn" runat="server" Text="Return To Home Page" 
            OnClick="NavToHomePageBtn_Click" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
        <asp:Button ID="NavToAnalysisSubmitBtn" runat="server" Text="Submit New Analysis" 
            OnClick="NavToAnalysisSubmitBtn_Click" BackColor="#2D8F78" Font-Names="Garamond" Font-Bold="True" 
            Height="50px" Width="200px" Font-Size="Larger" />
    </section>

    <asp:SqlDataSource ID="Lab2TextTitles" 
            ConnectionString="<%$ ConnectionStrings:Lab2 %>" 
            SelectCommand="SELECT * 
                FROM Texts t, Users u 
                WHERE t.userID=u.userID 
                AND u.email = @email"
            runat="server">
        <SelectParameters>
                <asp:SessionParameter Name="email" SessionField="UserName" Type="String"/>
            </SelectParameters>
    </asp:SqlDataSource>
</asp:Content>

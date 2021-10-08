<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="1a-UserRegPage.aspx.cs" Inherits="CIS484Projects.WebForm8" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="UserLoginNavBtn" runat="server" Text="Return to Login Page" OnClick="UserLoginNavBtn_Click"/>

    <div id="UserRegistrationSection">
        <asp:Label ID="firstLbl" runat="server" Text="First Name: "></asp:Label> <br />
        <asp:TextBox ID="firstTextbox" runat="server"></asp:TextBox> <br /><br />
        <asp:Label ID="lastLbl" runat="server" Text="Last Name: "></asp:Label> <br />
        <asp:TextBox ID="lastTextbox" runat="server"></asp:TextBox> <br /><br />
        <asp:Label ID="emailLbl" runat="server" Text="Email:"></asp:Label> <br />
        <asp:TextBox ID="emailTextbox" runat="server"></asp:TextBox> <br /><br />
        <asp:Label ID="passLbl" runat="server" Text="Password:"></asp:Label> <br />
        <asp:TextBox ID="passTextbox" runat="server"></asp:TextBox> <br /><br />
        <asp:Label ID="orgLbl" runat="server" Text="Organization: "></asp:Label> <br />
        <asp:TextBox ID="orgTextbox" runat="server"></asp:TextBox> <br /><br />
        <asp:Button ID="userRegBtn" runat="server" Text="Register" OnClick="userRegBtn_Click"/><br /><br />
        <asp:Label ID="emptyErrorLbl" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="nonAtomicEmailErrorLbl" runat="server" Text=""></asp:Label><br />
        <asp:Label ID="regSuccessLbl" runat="server" Text=""></asp:Label>
    </div>
</asp:Content>

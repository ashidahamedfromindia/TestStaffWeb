<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="TestStaffWeb.RegisterPage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <div>
            <h2>Registeration Page</h2>
            <asp:TextBox ID="txtName" runat="server" Placeholder="Name"></asp:TextBox><br /><br />
            <asp:TextBox ID="txtEmail" runat="server" Placeholder="Email"></asp:TextBox><br /><br />
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Placeholder="Password"></asp:TextBox><br /><br />
            <asp:Button ID="btnSubmit" runat="server" Text="Register" OnClick="btnSubmit_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back to Login" OnClick="btnBack_Click" />
        </div>

</asp:Content>
   


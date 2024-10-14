<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MyProfilePage.aspx.cs" Inherits="TestStaffWeb.MyProfilePage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div style="text-align: center; margin-bottom: 20px;">
      <asp:Button ID="Button1" runat="server" Text="Home" OnClick="btnBacktoHome_Click" CssClass="button-spacing" />
     </div>
        <h2>Your Profile Information</h2>
        <p>Your details along with Remarks by Admin</p>

        
    <div style="text-align: center; margin-top: 20px;">
    <asp:GridView ID="GridProfile" runat="server" AutoGenerateColumns="False" Style="margin-top: 20px; width: 100%;">
    <Columns>
        <asp:BoundField DataField="FullName" HeaderText="Full Name">
            <ItemStyle Width="200px" />
        </asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email">
            <ItemStyle Width="200px" />
        </asp:BoundField>
        <asp:BoundField DataField="Remarks" HeaderText="Remarks">
            <ItemStyle Width="300px" />
        </asp:BoundField>
    </Columns>
</asp:GridView>
</div>

    <div style="position:center; bottom: 20px; right: 20px; text-align: center;">
        <asp:Button ID="Button2" runat="server" Text="DELETE ACCOUNT" OnClick="btnDeleteProfile_Click" CssClass="button-spacing" Style="color: red;" OnClientClick="return confirmDeletion();" />  
    <div style="margin-top: 10px;">

    <asp:Label ID="lblMessage" runat="server" ForeColor="Red" 
        Style="text-align:center;" />
        </div></div>

    <script type="text/javascript">
        function confirmDeletion() {
            return confirm("Are you sure you want to delete your account? This action cannot be undone.");
        }
    </script>
</asp:Content>

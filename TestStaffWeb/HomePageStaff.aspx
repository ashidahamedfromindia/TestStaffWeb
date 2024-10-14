<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePageStaff.aspx.cs" Inherits="TestStaffWeb.HomePageStaff"MasterPageFile="~/Site.master" %> 

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    
    <div style="text-align: right; margin-bottom: 10px;">
        <asp:Button ID="Button1" runat="server" Text="My Profile" OnClick="btnMyProfile_Click" CssClass="button-spacing" />
    </div>

    <h1>Welcome to the Staff Website</h1>
    <p>About the Company: We are a leading provider of innovative solutions in our industry.</p>
    
    <div style="position: relative; margin-top: 20px;">
     <asp:GridView ID="gvStaff" runat="server" AutoGenerateColumns="False" OnRowCommand="gvStaff_RowCommand" CssClass="grid-view"> 
        <Columns>
            <asp:BoundField DataField="FullName" HeaderText="Full Name" />
            <asp:BoundField DataField="Email" HeaderText="Email" />
            <asp:TemplateField HeaderText="Is Administrator">
                <ItemTemplate>
                    <asp:Label ID="lblIsAdmin" runat="server" Text='<%# Convert.ToBoolean(Eval("IsAdministrator")) ? "Yes" : "No" %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
        </Columns>
    </asp:GridView>
   </div>
</asp:Content>

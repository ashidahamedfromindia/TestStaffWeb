<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HomePageAdmin.aspx.cs" Inherits="TestStaffWeb.HomePage" MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
  
   <div style="text-align: right; margin-bottom: 10px;">
    <asp:Button ID="Button1" runat="server" Text="My Profile" OnClick="btnMyProfile_Click" CssClass="button-spacing" />
   </div>
   
   <h1>Welcome to the Admin Portal of Staff Website</h1>
   <p>Here,You can edit the role,active status and remarks for your staffs</p>
        
   <div style="position: relative; margin-top: 20px;">    
    <asp:GridView ID="gvStaff" runat="server" AutoGenerateColumns="False"
        OnRowCommand="gvStaff_RowCommand" 
        OnRowEditing="gvStaff_RowEditing" 
        OnRowUpdating="gvStaff_RowUpdating" 
        OnRowDeleting="gvStaff_RowDeleting" 
        DataKeyNames="StaffId">
      <Columns>
       <asp:BoundField DataField="FullName" HeaderText="Full Name" />
        <asp:BoundField DataField="Email" HeaderText="Email" />

        <asp:TemplateField HeaderText="IsActive">
          <ItemTemplate>
           <asp:CheckBox ID="chkIsActive" runat="server" Checked='<%# Eval("IsActive") %>' />
          </ItemTemplate>
        </asp:TemplateField>
   
        <asp:TemplateField HeaderText="Is Admin">
          <ItemTemplate>
           <asp:CheckBox ID="chkIsAdmin" runat="server" Checked='<%# Eval("IsAdministrator") %>' />
          </ItemTemplate>
        </asp:TemplateField>
  
        <asp:TemplateField HeaderText="Remarks">
         <ItemTemplate>
          <asp:TextBox ID="txtRemarks" runat="server" Text='<%# Eval("Remarks") %>' />
         </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField>
         <ItemTemplate>
            <asp:Button ID="btnDelete" runat="server" Text="Delete" CommandName="Delete" CommandArgument='<%# Eval("StaffId") %>' />
         </ItemTemplate>
        </asp:TemplateField>


        <asp:CommandField ShowEditButton="True" ButtonType="Button" />
     </Columns>
   </asp:GridView>
  </div>
</asp:Content>


using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace TestStaffWeb
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var master = this.Master as SiteMaster;
                if (master != null)
                {
                    master.SetLogoutButtonVisibility(true);
                }
                LoadStaffData();
            }
        }

        private void LoadStaffData()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("GetAllStaff", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                gvStaff.DataSource = reader;
                gvStaff.DataBind();
            }
        }

        protected void btnMyProfile_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyProfilePage.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("LoginPage.aspx");
        }

        protected void gvStaff_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Edit")
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                if (rowIndex >= 0 && rowIndex < gvStaff.Rows.Count)
                {
                    GridViewRow row = gvStaff.Rows[rowIndex];

                    int staffId = Convert.ToInt32(gvStaff.DataKeys[rowIndex].Value);
                    CheckBox chkIsActive = (CheckBox)row.FindControl("chkIsActive");
                    CheckBox chkIsAdmin = (CheckBox)row.FindControl("chkIsAdmin");
                    TextBox txtRemarks = (TextBox)row.FindControl("txtRemarks");

                    UpdateStaffStatus(staffId, chkIsActive.Checked, chkIsAdmin.Checked, txtRemarks.Text);
                    LoadStaffData();
                }
            }
        }
        protected void gvStaff_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int staffId = Convert.ToInt32(gvStaff.DataKeys[e.RowIndex].Value);
            DeleteStaff(staffId);
            LoadStaffData();  // Refresh the grid data
        }
        protected void gvStaff_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the new edit index
            gvStaff.EditIndex = e.NewEditIndex;


            // Rebind the data to the GridView
            LoadStaffData();
        }
        protected void gvStaff_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            int rowIndex = e.RowIndex;

            // Get the StaffId from the DataKeys
            int staffId = Convert.ToInt32(gvStaff.DataKeys[rowIndex].Value);
            CheckBox chkIsActive = (CheckBox)gvStaff.Rows[rowIndex].FindControl("chkIsActive");
            CheckBox chkIsAdmin = (CheckBox)gvStaff.Rows[rowIndex].FindControl("chkIsAdmin");
            TextBox txtRemarks = (TextBox)gvStaff.Rows[rowIndex].FindControl("txtRemarks");

            // Update the staff status
            UpdateStaffStatus(staffId, chkIsActive.Checked, chkIsAdmin.Checked, txtRemarks.Text);

            // Reset the edit index
            gvStaff.EditIndex = -1;

            // Reload the staff data
            LoadStaffData();
        }

        private void UpdateStaffStatus(int staffId, bool isActive, bool isAdmin, string remarks)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("UpdateStaffStatus", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                cmd.Parameters.AddWithValue("@IsActive", isActive);
                cmd.Parameters.AddWithValue("@IsAdministrator", isAdmin);
                cmd.Parameters.AddWithValue("@Remarks", remarks);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
        private void DeleteStaff(int staffId)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteStaff", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", staffId);

                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }

    }
}
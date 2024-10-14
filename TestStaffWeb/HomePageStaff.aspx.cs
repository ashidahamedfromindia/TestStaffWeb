using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace TestStaffWeb
{
    public partial class HomePageStaff : System.Web.UI.Page
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
                BindGridView();
            }
        }
        private void BindGridView()
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("GetAllStaff", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(reader);
                    gvStaff.DataSource = dt;
                    gvStaff.DataBind();
                }
            }
        }

        protected void btnMyProfile_Click(object sender, EventArgs e)
        {
            // Navigate to the My Profile page
            Response.Redirect("MyProfilePage.aspx");
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            // Log out logic (e.g., clear session, etc.)
            Session.Clear();
            Response.Redirect("LoginPage.aspx"); // Navigate back to the login page
        }
        protected void gvStaff_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            // Handle row commands (if needed, for edit or other operations)
            if (e.CommandName == "Edit") // Example for handling edit command
            {
                int rowIndex = Convert.ToInt32(e.CommandArgument);
                // Logic to handle the edit operation
                // You can retrieve the data from the row using gvStaff.DataKeys[rowIndex].Value
            }
            // Add other command handling as necessary
        }
    }
}
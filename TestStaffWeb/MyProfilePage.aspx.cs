using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TestStaffWeb
{
    public partial class MyProfilePage : System.Web.UI.Page
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
                LoadProfileData();
            }
        }

        private void LoadProfileData()
        {
            int staffId = Convert.ToInt32(Session["StaffId"]);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Staff_GetMyProfile", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                GridProfile.DataSource = dt;
                GridProfile.DataBind();
            }
        }
        protected void btnBacktoHome_Click(object sender, EventArgs e)
        {
            int staffId = Convert.ToInt32(Session["StaffId"]);
            bool isAdmin = false;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("Staff_IsAdminCheck", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StaffId", staffId);
                conn.Open();
                object result = cmd.ExecuteScalar();
                if (result != null && result != DBNull.Value)
                {
                    isAdmin = Convert.ToBoolean(result);
                }


                if (isAdmin)
                {
                    Response.Redirect("HomePageAdmin.aspx");
                }
                else
                {
                    Response.Redirect("HomePageStaff.aspx");
                }
            }
        }
        protected void btnDeleteProfile_Click(object sender, EventArgs e)
        {
            // Logic to delete the user's profile
            object value = DeleteUserProfile();

            // Show a message and redirect to the login page after a brief delay
            lblMessage.Text = "Account deleted successfully.";
            lblMessage.Visible = true;

            // Use JavaScript to redirect after showing the message
            ScriptManager.RegisterStartupScript(this, GetType(), "redirect",
                "setTimeout(function() { window.location = 'LoginPage.aspx'; }, 2000);", true);
        }

        private object DeleteUserProfile()
        {
            
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {
                SqlCommand cmd = new SqlCommand("DeleteStaff", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                int staffId = Convert.ToInt32(Session["StaffId"]);
                cmd.Parameters.AddWithValue("@StaffId", staffId);

                conn.Open();
                return cmd.ExecuteNonQuery(); // Get the number of rows affected
               
            }
        }
    }
}
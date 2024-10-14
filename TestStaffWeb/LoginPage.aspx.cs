using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TestStaffWeb
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var master = this.Master as SiteMaster; 
                if (master != null)
                {
                    master.SetLogoutButtonVisibility(false); 
                }
            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StaffConnectionString"].ConnectionString))
            {

                SqlCommand cmd = new SqlCommand("StaffLogin", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@HashPassword", txtPassword.Text);
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    Session["StaffId"] = reader["StaffId"];
                    Session["IsAdministrator"] = reader["IsAdministrator"];
                    bool isAdmin = Convert.ToBoolean(reader["IsAdministrator"]);
                    if (isAdmin)
                    {
                        Response.Redirect("HomePageAdmin.aspx");
                    }
                    else
                    {
                        Response.Redirect("HomePageStaff.aspx");
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid email or password.";
                }
            }
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegisterPage.aspx");

        }

    }
}
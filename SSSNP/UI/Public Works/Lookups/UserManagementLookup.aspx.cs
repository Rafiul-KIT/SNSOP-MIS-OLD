using System;
using System.Data.SqlClient;
using System.Web.Configuration;
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works.Lookups
{
    public partial class UserManagementLookup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Any initial setup if needed
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            RadGridDetails.Rebind();
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            // Clear the filter fields
            txtFilterUserName.Text = string.Empty;
            txtFilterUserType.Text = string.Empty;
            txtFilterEmail.Text = string.Empty;

            // Optionally clear the grid's data source and rebind to show no results
            RadGridDetails.DataSource = null;
            RadGridDetails.Rebind();
        }


        protected void RadGridDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string userName = txtFilterUserName.Text.Trim();
            string userType = txtFilterUserType.Text.Trim();
            string email = txtFilterEmail.Text.Trim();

            string query = "SELECT User_id AS UserId, User_name AS UserName, User_type AS UserType, User_code AS UserCode, " +
                           "Status, Email, GBVUser, IP_Approver AS IPApprover, email_receiver AS EmailReceiver " +
                           "FROM tbl_Users WHERE 1=1";

            if (!string.IsNullOrEmpty(userName))
            {
                query += " AND User_name LIKE @UserName";
            }
            if (!string.IsNullOrEmpty(userType))
            {
                query += " AND User_type = @UserType"; 
            }
            if (!string.IsNullOrEmpty(email))
            {
                query += " AND Email LIKE @Email";
            }

            string connStr = WebConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!string.IsNullOrEmpty(userName))
                    {
                        cmd.Parameters.AddWithValue("@UserName", "%" + userName + "%");
                    }
                    if (!string.IsNullOrEmpty(userType))
                    {
                        cmd.Parameters.AddWithValue("@UserType", userType);
                    }
                    if (!string.IsNullOrEmpty(email))
                    {
                        cmd.Parameters.AddWithValue("@Email", "%" + email + "%");
                    }

                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    RadGridDetails.DataSource = reader;
                }
            }
        }

        protected void RadGridDetails_ItemCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string[] args = e.CommandArgument.ToString().Split('|');
                string userId = args[0];
                string userName = args[1];
                string userType = args[2];
                string userCode = args[3];
                string status = args[4];
                string email = args[5];
                string gbvUser = args[6];
                string ipApprover = args[7];
                string emailReceiver = args[8];

                string script = $"window.opener.ApplyChanges('{userId}', '{userName}', '{userType}', '{userCode}', '{status}', '{email}', '{gbvUser}', '{ipApprover}', '{emailReceiver}'); window.close();";
                ClientScript.RegisterStartupScript(this.GetType(), "SelectRow", script, true);
            }
        }
    }
}

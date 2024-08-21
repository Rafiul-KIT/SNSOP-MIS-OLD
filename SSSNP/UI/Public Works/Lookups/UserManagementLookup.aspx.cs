using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
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
            RadGridDetails.Rebind(); // Rebind the grid to refresh data based on new search criteria
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            txtFilterUserName.Text = string.Empty;
            txtFilterUserType.Text = string.Empty;
            txtFilterEmail.Text = string.Empty;
            RadGridDetails.DataSource = null;
            RadGridDetails.Rebind();
        }

        protected void RadGridDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                // Get search criteria from the textboxes
                string userId = txtFilterUserName.Text.Trim();
                string userType = txtFilterUserType.Text.Trim();
                string email = txtFilterEmail.Text.Trim();

                string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand("GetUserInfoKIT", conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters and handle nulls
                        cmd.Parameters.AddWithValue("@User_id", string.IsNullOrEmpty(userId) ? (object)DBNull.Value : "%" + userId + "%");
                        cmd.Parameters.AddWithValue("@User_type", string.IsNullOrEmpty(userType) ? (object)DBNull.Value : userType);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(email) ? (object)DBNull.Value : email);

                        conn.Open();
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        RadGridDetails.DataSource = dataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception, log it or display a message
                Response.Write("Error: " + ex.Message);
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
                string createdBy = args[9];
                string createdDt = args[10];
                string modifiedBy = args[11];
                string modifiedDt = args[12];

                // Pass all the values to the ApplyChanges JavaScript function
                string script = $"window.opener.ApplyChanges('{userId}', '{userName}', '{userType}', '{userCode}', '{status}', '{email}', '{gbvUser}', '{ipApprover}', '{emailReceiver}', '{createdBy}', '{createdDt}', '{modifiedBy}', '{modifiedDt}'); window.close();";
                ClientScript.RegisterStartupScript(this.GetType(), "SelectRow", script, true);
            }
        }

    }
}

using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
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
            //RadGridDetails.Rebind();
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            txtFilterUserName.Text = string.Empty;
            txtFilterUserType.Text = string.Empty;
            txtFilterEmail.Text = string.Empty;
            //RadGridDetails.DataSource = null;
            //RadGridDetails.Rebind();
        }

        /*protected void RadGridDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    LogMessage("Database connection opened successfully.");

                    string query = "SELECT 1";  // Simple test query

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        RadGridDetails.DataSource = dataTable;
                        LogMessage("Query executed successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                LogMessage($"An error occurred: {ex.Message}");
            }
        }


        private void LogMessage(string message)
        {
            // Implement your logging logic here
            // For example, logging to a label on the page
            Label lblLog = (Label)FindControl("lblLog");
            if (lblLog != null)
            {
                lblLog.Text += $"{DateTime.Now}: {message}<br />";
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
        }*/
    }
}

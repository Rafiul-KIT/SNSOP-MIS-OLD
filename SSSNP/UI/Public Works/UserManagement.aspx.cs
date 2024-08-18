using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class UserManagement : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialization code here if needed
            }
        }

        /*public void ApplyChanges(string userId, string userName, string userType, string userCode, string status, string email, string gbvUser, string ipApprover, string emailReceiver)
        {
            txtUserId.Text = userId;
            txtUserName.Text = userName;
            txtUserType.Text = userType;
            txtUserCode.Text = userCode;
            txtStatus.Text = status;
            txtEmail.Text = email;
            txtGBVUser.Text = gbvUser;
            txtIPApprover.Text = ipApprover;
            txtEmailReceiver.Text = emailReceiver;
        }*/

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            InsertOrUpdateUser(isInsert: true);
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            InsertOrUpdateUser(isInsert: false);
        }

        private void InsertOrUpdateUser(bool isInsert)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            string query = "InsUpdUserKIT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters
                        cmd.Parameters.AddWithValue("@User_id", txtUserId.Text);
                        cmd.Parameters.AddWithValue("@User_name", txtUserName.Text);
                        cmd.Parameters.AddWithValue("@User_type", txtUserType.Text);
                        cmd.Parameters.AddWithValue("@User_code", txtUserCode.Text);
                        cmd.Parameters.AddWithValue("@Status", ddlStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                        cmd.Parameters.AddWithValue("@GBVUser", rblGBVUser.SelectedValue == "True");
                        cmd.Parameters.AddWithValue("@IP_Approver", rblIPApprover.SelectedValue == "True");
                        cmd.Parameters.AddWithValue("@email_receiver", int.Parse(txtEmailReceiver.Text));

                        if (isInsert)
                        {
                            // Insert operation: use Created_by and Created_dt
                            cmd.Parameters.AddWithValue("@Created_by", hfCreatedBy.Value);
                            cmd.Parameters.AddWithValue("@Created_dt", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Modified_by", DBNull.Value);
                            cmd.Parameters.AddWithValue("@Modified_dt", DBNull.Value);
                        }
                        else
                        {
                            // Update operation: use Modified_by and Modified_dt
                            cmd.Parameters.AddWithValue("@Modified_by", hfModifiedBy.Value);
                            cmd.Parameters.AddWithValue("@Modified_dt", DateTime.Now);
                            cmd.Parameters.AddWithValue("@Created_by", hfCreatedBy.Value);
                            cmd.Parameters.AddWithValue("@Created_dt", hfCreatedDt.Value);
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exception, log it or display a message
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear TextBox fields
            txtUserId.Text = "";
            txtUserName.Text = "";
            txtUserType.Text = "";
            txtUserCode.Text = "";
            txtEmail.Text = "";
            txtEmailReceiver.Text = "";

            // Reset DropDownList to the "Select" option
            ddlStatus.SelectedIndex = 0;

            // Reset RadioButtonLists
            rblGBVUser.ClearSelection();
            rblIPApprover.ClearSelection();
        }

    }
}

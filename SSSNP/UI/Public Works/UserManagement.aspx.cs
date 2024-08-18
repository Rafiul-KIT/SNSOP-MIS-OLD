using System;
using System.Configuration;
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

        public void ApplyChanges(string userId, string userName, string userType, string userCode, string status, string email, string gbvUser, string ipApprover, string emailReceiver)
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
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            string query = "EXEC InsUpdUserKIT @User_id, @User_name, @User_type, @User_code, @Status, @Created_by, @Modified_by, @Email, @GBVUser, @IP_Approver, @email_receiver";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@User_id", txtUserId.Text);
                    cmd.Parameters.AddWithValue("@User_name", txtUserName.Text);
                    cmd.Parameters.AddWithValue("@User_type", txtUserType.Text);
                    cmd.Parameters.AddWithValue("@User_code", txtUserCode.Text);
                    cmd.Parameters.AddWithValue("@Status", txtStatus.Text);
                    cmd.Parameters.AddWithValue("@Created_by", Session["UserId"]); // Assuming you have the user ID stored in the session
                    cmd.Parameters.AddWithValue("@Modified_by", Session["UserId"]);
                    cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                    cmd.Parameters.AddWithValue("@GBVUser", txtGBVUser.Text);
                    cmd.Parameters.AddWithValue("@IP_Approver", txtIPApprover.Text);
                    cmd.Parameters.AddWithValue("@email_receiver", txtEmailReceiver.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}

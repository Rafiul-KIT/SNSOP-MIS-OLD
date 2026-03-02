using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class UserManagement : Page
    {
        private static readonly object _lock = new object();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialization code here if needed
            }
        }

        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Server-side validation for mandatory fields
                if (string.IsNullOrEmpty(txtUserId.Text))
                {
                    ShowWarning("User ID is required.");
                    return;
                }

                if (string.IsNullOrEmpty(txtUserName.Text))
                {
                    ShowWarning("User Name is required.");
                    return;
                }

                if (string.IsNullOrEmpty(ddlUserType.SelectedValue))
                {
                    ShowWarning("User Type is required.");
                    return;
                }

                if (string.IsNullOrEmpty(txtUserCode.Text))
                {
                    ShowWarning("User Code is required.");
                    return;
                }

                if (string.IsNullOrEmpty(ddlStatus.SelectedValue))
                {
                    ShowWarning("Status is required.");
                    return;
                }

                if (string.IsNullOrEmpty(txtEmail.Text))
                {
                    ShowWarning("Email is required.");
                    return;
                }

                // Additional length checks if necessary
                if (txtUserId.Text.Length > 50 || txtUserCode.Text.Length > 10 || txtEmail.Text.Length > 150)
                {
                    ShowWarning("Input exceeds the maximum allowed length.");
                    return;
                }

                string userEmail = txtEmail.Text;
                string userName = txtUserName.Text;

                // Acquire a lock to ensure thread safety during operations
                lock (_lock)
                {
                    bool emailExists = EmailExistsInDatabase(userEmail);

                    if (!emailExists)
                    {
                        InsertOperation(userEmail, userName);
                    }
                    else
                    {
                        UpdateOperation(userEmail, userName);
                    }
                }

                // Clear hidden fields
                hfCreatedBy.Value = "";
                hfCreatedDt.Value = "";
                hfModifiedBy.Value = "";
                hfModifiedDt.Value = "";
                hfOriginalEmail.Value = "";
            }
            catch (Exception ex)
            {
                ShowWarning($"Error during save operation: {ex.Message}");
            }
        }

        protected void InsertOperation(string userEmail, string userName)
        {
            try
            {
                string accessToken = GetAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    bool isUserCreated = CreateOrUpdateExternalUser(accessToken, userEmail, userName);

                    if (isUserCreated)
                    {
                        InsertOrUpdateUser(isInsert: true);
                        ShowMessage("User created successfully.");
                    }
                    else
                    {
                        ShowWarning("Failed to create user in Microsoft Active Directory! Make sure the email address you have entered is registered into Microsoft Account.");
                    }
                }
                else
                {
                    ShowWarning("Failed to acquire Azure AD access token. Could not create user.");
                }
            }
            catch (Exception ex)
            {
                
                ShowWarning($"Error during insert operation: {ex.Message}");
            }
        }

        protected void UpdateOperation(string userEmail, string userName)
        {
            try
            {
                string originalEmail = hfOriginalEmail.Value;

                if (userEmail != originalEmail)
                {
                    ShowWarning("Please check first from look up if the user has been registered.");
                    return;
                }

                string accessToken = GetAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    bool isUserUpdated = CreateOrUpdateExternalUser(accessToken, userEmail, userName);

                    if (isUserUpdated)
                    {
                        InsertOrUpdateUser(isInsert: false);
                        ShowMessage("User updated successfully.");
                    }
                    else
                    {
                        ShowWarning("Failed to update user in Microsoft Active Directory! Please contact your system administrator.");
                    }
                }
                else
                {
                    ShowWarning("Failed to acquire Azure AD access token.");
                }
            }
            catch (Exception ex)
            {
                
                ShowWarning($"Error during update operation: {ex.Message}");
            }
        }

        private string GetAccessToken()
        {
            try
            {
                string azureADEnv = ConfigurationManager.AppSettings["AZUREAD_ENV"];
                string clientId = "";
                string tenantId = ConfigurationManager.AppSettings["AzureADTenantId"];
                string clientSecret = "";
                if (azureADEnv.Equals("local"))
                {
                    clientId = ConfigurationManager.AppSettings["AzureADClientIdLocal"];
                    clientSecret = ConfigurationManager.AppSettings["AzureADClientSecretLocal"];
                } 
                else if (azureADEnv.Equals("dev"))
                {
                    clientId = ConfigurationManager.AppSettings["AzureADClientIdDev"];
                    clientSecret = ConfigurationManager.AppSettings["AzureADClientSecretDev"];
                } 
                else if (azureADEnv.Equals("prod"))
                {
                    clientId = ConfigurationManager.AppSettings["AzureADClientIdProd"];
                    clientSecret = ConfigurationManager.AppSettings["AzureADClientSecretProd"];
                }

                IConfidentialClientApplication confidentialClient = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}/v2.0"))
                    .Build();

                string[] scopes = new[] { "https://graph.microsoft.com/.default" };

                AuthenticationResult authResult = confidentialClient
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync().GetAwaiter().GetResult();

                return authResult.AccessToken;
            }
            catch (Exception ex)
            {
                
                ShowWarning($"Error generating access token: {ex.Message}");
                return null;
            }
        }

        private bool CreateOrUpdateExternalUser(string accessToken, string userEmail, string userName)
        {
            string uri = "https://graph.microsoft.com/v1.0/invitations";
            Uri urlCheck = new Uri(uri);

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + accessToken);
            httpWReq.ContentType = "application/json";
            httpWReq.Method = "POST";

            try
            {
                var request = new UserCreationModel()
                {
                    invitedUserDisplayName = userName,
                    invitedUserEmailAddress = userEmail,
                    inviteRedirectUrl = ConfigurationManager.AppSettings["SSSNPWEB"].ToString(),
                    sendInvitationMessage = true
                };

                string reqStr = JsonConvert.SerializeObject(request);
                var data = Encoding.ASCII.GetBytes(reqStr);
                httpWReq.ContentLength = data.Length;

                using (var newStream = httpWReq.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string jsonResponse = reader.ReadToEnd();
                        return true;
                    }
                }
            }
            catch (WebException ex)
            {
                ShowWarning($"Error : {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                ShowWarning($"Error : {ex.Message}");
                return false;
            }
        }

        private bool EmailExistsInDatabase(string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            string query = "SELECT COUNT(1) FROM tbl_Users WHERE Email = @Email";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Email", email);
                        conn.Open();
                        int count = Convert.ToInt32(cmd.ExecuteScalar());
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowWarning($"Error checking email in database: {ex.Message}");
                return false;
            }
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
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@User_id", string.IsNullOrEmpty(txtUserId.Text) ? (object)DBNull.Value : txtUserId.Text);
                        cmd.Parameters.AddWithValue("@User_name", string.IsNullOrEmpty(txtUserName.Text) ? (object)DBNull.Value : txtUserName.Text);
                        cmd.Parameters.AddWithValue("@User_type", string.IsNullOrEmpty(ddlUserType.SelectedValue) ? (object)DBNull.Value : ddlUserType.SelectedValue);
                        cmd.Parameters.AddWithValue("@User_code", string.IsNullOrEmpty(txtUserCode.Text) ? (object)DBNull.Value : txtUserCode.Text);
                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(ddlStatus.SelectedValue) ? (object)DBNull.Value : ddlStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                        cmd.Parameters.AddWithValue("@GBVUser", string.IsNullOrEmpty(rblGBVUser.SelectedValue) ? (object)DBNull.Value : (object)(rblGBVUser.SelectedValue == "True"));
                        cmd.Parameters.AddWithValue("@IP_Approver", string.IsNullOrEmpty(rblIPApprover.SelectedValue) ? (object)DBNull.Value : (object)(rblIPApprover.SelectedValue == "True"));
                        cmd.Parameters.AddWithValue("@email_receiver", string.IsNullOrEmpty(rblEmailReceiver.SelectedValue) ? (object)DBNull.Value : (object)int.Parse(rblEmailReceiver.SelectedValue));

                        if (isInsert)
                        {
                            cmd.Parameters.AddWithValue("@Created_by", Session["uid"] != null ? (object)Session["uid"] : DBNull.Value);
                            cmd.Parameters.AddWithValue("@Modified_by", DBNull.Value);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@Modified_by", Session["uid"] != null ? (object)Session["uid"] : DBNull.Value);
                            cmd.Parameters.AddWithValue("@Created_by", hfCreatedBy.Value);
                        }

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                ShowWarning($"Error inserting/updating user: {ex.Message}");
            }
        }

       
        private void ShowWarning(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "text-danger";
            lblMessage.Visible = true;
        }

        private void ShowMessage(string message)
        {
            lblMessage.Text = message;
            lblMessage.CssClass = "text-success";
            lblMessage.Visible = true;
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            txtUserId.Text = "";
            txtUserName.Text = "";
            ddlUserType.SelectedIndex = 0;
            txtUserCode.Text = "";
            txtEmail.Text = "";
            ddlStatus.SelectedIndex = 0;
            rblGBVUser.ClearSelection();
            rblIPApprover.ClearSelection();
            rblEmailReceiver.ClearSelection();
            lblMessage.Text = "";

            // Clear hidden fields
            hfCreatedBy.Value = "";
            hfCreatedDt.Value = "";
            hfModifiedBy.Value = "";
            hfModifiedDt.Value = "";
            hfOriginalEmail.Value = "";

            // Clear server-side validation warnings if needed
            Page.Validate();
            foreach (BaseValidator validator in Page.Validators)
            {
                validator.IsValid = true;
            }
        }
    }

    public class UserCreationModel
    {
        public string invitedUserDisplayName { get; set; }
        public string invitedUserEmailAddress { get; set; }
        public string inviteRedirectUrl { get; set; }
        public bool sendInvitationMessage { get; set; }
    }
}

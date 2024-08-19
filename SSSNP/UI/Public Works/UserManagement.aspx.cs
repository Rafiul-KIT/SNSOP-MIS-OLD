using Microsoft.Graph;
using Microsoft.Identity.Client;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class UserManagement : Page
    {
        protected bool IsNewUser = true;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialization code here if needed
            }
        }

        protected async void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                // Check if the email already exists in the database
                if (EmailExists(txtEmail.Text))
                {
                    ShowWarning("This email is already registered.");
                    return;
                }

                // Call the Azure AD API before the database operation
                string accessToken = await GetAccessTokenAsync();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    bool isAzureADUserCreated = await CreateExternalUserAsync(accessToken);

                    if (isAzureADUserCreated)
                    {
                        InsertOrUpdateUser(isInsert: true);
                        ShowMessage("User inserted successfully.");
                    }
                    else
                    {
                        ShowWarning("Failed to create user in Azure AD.");
                    }
                }
                else
                {
                    ShowWarning("Failed to acquire Azure AD access token.");
                }
            }
            catch (Exception ex)
            {
                ShowWarning($"Error during insert operation: {ex.Message}");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                IsNewUser = false;

                // Ensure the email is not modified during an update
                if (IsNewUser)
                {
                    ShowWarning("Cannot modify the email during an update.");
                    return;
                }

                InsertOrUpdateUser(isInsert: false);
                ShowMessage("User updated successfully.");
            }
            catch (Exception ex)
            {
                ShowWarning($"Error during update operation: {ex.Message}");
            }
        }

        private async Task<string> GetAccessTokenAsync()
        {
            string clientId = "2d80a939-a955-4c38-a911-a1f8cb986a2c"; // Replace with your Azure AD application client ID
            string tenantId = "4a9fe01e-5c9c-4cb8-b299-fb5211533420"; // Replace with your Azure AD tenant ID
            string clientSecret = "3BP8Q~TikdfxoOyfWjyWRj2RXlujNlS2LZQFnb-S"; // Replace with your Azure AD application client secret

            try
            {
                IConfidentialClientApplication confidentialClient = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}/v2.0"))
                    .Build();

                string[] scopes = new[] { "https://graph.microsoft.com/.default" };

                AuthenticationResult authResult = await confidentialClient
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync();

                if (authResult == null || string.IsNullOrEmpty(authResult.AccessToken))
                {
                    ShowWarning("Failed to acquire access token or token is empty.");
                    return null;
                }

                return authResult.AccessToken;
            }
            catch (Exception ex)
            {
                ShowWarning($"Error generating access token: {ex.Message}");
                return null;
            }
        }

        private async Task<bool> CreateExternalUserAsync(string accessToken)
        {
            var graphClient = new GraphServiceClient(
                new DelegateAuthenticationProvider((requestMessage) =>
                {
                    requestMessage.Headers.Authorization =
                        new AuthenticationHeaderValue("Bearer", accessToken);

                    return Task.CompletedTask;
                }));

            var invitation = new Invitation
            {
                InvitedUserEmailAddress = txtEmail.Text,
                InviteRedirectUrl = "https://dev.karoothitbd.com:5051", // Replace with your application's URL
                InvitedUserDisplayName = txtUserName.Text,
                SendInvitationMessage = true,
                InvitedUserMessageInfo = new InvitedUserMessageInfo
                {
                    CustomizedMessageBody = "You are invited to collaborate with us."
                }
            };

            try
            {
                var invitationResponse = await graphClient.Invitations.Request().AddAsync(invitation);
                ShowMessage($"Invitation sent successfully to: {invitationResponse.InvitedUserEmailAddress}");
                return true;
            }
            catch (ServiceException ex)
            {
                ShowWarning($"Error creating external user: {ex.Message}");
                return false;
            }
        }

        private bool EmailExists(string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "SELECT COUNT(*) FROM tbl_Users WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    conn.Open();
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0;
                }
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
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Add parameters with NULL handling
                        cmd.Parameters.AddWithValue("@User_id", string.IsNullOrEmpty(txtUserId.Text) ? (object)DBNull.Value : txtUserId.Text);
                        cmd.Parameters.AddWithValue("@User_name", string.IsNullOrEmpty(txtUserName.Text) ? (object)DBNull.Value : txtUserName.Text);
                        cmd.Parameters.AddWithValue("@User_type", string.IsNullOrEmpty(ddlUserType.SelectedValue) ? (object)DBNull.Value : ddlUserType.SelectedValue);
                        cmd.Parameters.AddWithValue("@User_code", string.IsNullOrEmpty(txtUserCode.Text) ? (object)DBNull.Value : txtUserCode.Text);
                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(ddlStatus.SelectedValue) ? (object)DBNull.Value : ddlStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                        cmd.Parameters.AddWithValue("@GBVUser", string.IsNullOrEmpty(rblGBVUser.SelectedValue) ? (object)DBNull.Value : (object)(rblGBVUser.SelectedValue == "True"));
                        cmd.Parameters.AddWithValue("@IP_Approver", string.IsNullOrEmpty(rblIPApprover.SelectedValue) ? (object)DBNull.Value : (object)(rblIPApprover.SelectedValue == "True"));

                        // Safely parse Email Receiver value, or set to NULL if not provided
                        if (int.TryParse(txtEmailReceiver.Text, out int emailReceiver))
                        {
                            cmd.Parameters.AddWithValue("@email_receiver", emailReceiver);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@email_receiver", DBNull.Value);
                        }

                        // Handle Created_by and Modified_by correctly
                        int? createdBy = null;
                        int? modifiedBy = null;

                        if (isInsert)
                        {
                            if (int.TryParse(hfCreatedBy.Value, out int createdByValue))
                            {
                                createdBy = createdByValue;
                            }
                            modifiedBy = null; // Not used during insert
                        }
                        else
                        {
                            if (Session["userlevel"] != null && int.TryParse(Session["userlevel"].ToString(), out int modifiedByValue))
                            {
                                modifiedBy = modifiedByValue;
                            }
                            if (int.TryParse(hfCreatedBy.Value, out int createdByValue))
                            {
                                createdBy = createdByValue;
                            }
                        }

                        cmd.Parameters.AddWithValue("@Created_by", createdBy.HasValue ? (object)createdBy.Value : DBNull.Value);
                        cmd.Parameters.AddWithValue("@Modified_by", modifiedBy.HasValue ? (object)modifiedBy.Value : DBNull.Value);

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

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear all fields
            txtUserId.Text = "";
            txtUserName.Text = "";
            ddlUserType.SelectedIndex = 0;
            txtUserCode.Text = "";
            txtEmail.Text = "";
            ddlStatus.SelectedIndex = 0;
            rblGBVUser.ClearSelection();
            rblIPApprover.ClearSelection();
            txtEmailReceiver.Text = "";
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
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.UI;
using Newtonsoft.Json.Linq;

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

        protected async void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string userEmail = txtEmail.Text;
                string userName = txtUserName.Text;

                // Get the access token from Azure AD
                string accessToken = await GetAccessTokenAsync();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    bool invitationSent = false;

                    // Check if the user is external or internal
                    if (IsExternalUser(userEmail))
                    {
                        // Send invitation to external user
                        invitationSent = await InviteExternalUserAsync(accessToken, userEmail, userName);
                    }
                    else
                    {
                        // Create internal user in Azure AD
                        invitationSent = await CreateInternalUserAsync(accessToken, userEmail, userName);
                    }

                    if (invitationSent)
                    {
                        // If the invitation or user creation was successful, insert or update the user in your database
                        InsertOrUpdateUser(isInsert: true);
                        ShowMessage("User inserted successfully.");
                    }
                    else
                    {
                        ShowWarning("Failed to send invitation or create user. User was not inserted.");
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

        protected async void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string userEmail = txtEmail.Text;
                string userName = txtUserName.Text;

                // Check if the email exists in the database
                if (!EmailExistsInDatabase(userEmail))
                {
                    ShowWarning("The email is not registered.");
                    return;
                }

                // Get the access token from Azure AD
                string accessToken = await GetAccessTokenAsync();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    bool updateSuccess = false;

                    // Check if the user is external or internal
                    if (IsExternalUser(userEmail))
                    {
                        // Update the invitation for an external user (if needed)
                        updateSuccess = await UpdateExternalUserAsync(accessToken, userEmail, userName);
                    }
                    else
                    {
                        // Update internal user in Azure AD (if needed)
                        updateSuccess = await UpdateInternalUserAsync(accessToken, userEmail, userName);
                    }

                    if (updateSuccess)
                    {
                        // If the update in Azure AD was successful, update the user in your database
                        InsertOrUpdateUser(isInsert: false);
                        ShowMessage("User updated successfully.");
                    }
                    else
                    {
                        ShowWarning("Failed to update the user in Azure AD. User was not updated.");
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

        private async Task<string> GetAccessTokenAsync()
        {
            using (var client = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://login.microsoftonline.com/4a9fe01e-5c9c-4cb8-b299-fb5211533420/oauth2/v2.0/token");

                var parameters = new[]
                {
                    new KeyValuePair<string, string>("client_id", "2d80a939-a955-4c38-a911-a1f8cb986a2c"),
                    new KeyValuePair<string, string>("scope", "https://graph.microsoft.com/.default"),
                    new KeyValuePair<string, string>("client_secret", "3BP8Q~TikdfxoOyfWjyWRj2RXlujNlS2LZQFnb-S"),
                    new KeyValuePair<string, string>("grant_type", "client_credentials")
                };

                request.Content = new FormUrlEncodedContent(parameters);

                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var tokenResponse = JObject.Parse(content);
                    return tokenResponse["access_token"].ToString();
                }

                return null;
            }
        }

        private async Task<bool> InviteExternalUserAsync(string accessToken, string userEmail, string userName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var invitationPayload = new
                {
                    invitedUserEmailAddress = userEmail,
                    inviteRedirectUrl = "https://dev.karoothitbd.com:5051",
                    invitedUserDisplayName = userName,
                    sendInvitationMessage = true
                };

                var response = await client.PostAsJsonAsync("https://graph.microsoft.com/v1.0/invitations", invitationPayload);

                return response.IsSuccessStatusCode;
            }
        }

        private async Task<bool> CreateInternalUserAsync(string accessToken, string userEmail, string userName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var internalUserPayload = new
                {
                    accountEnabled = true,
                    displayName = userName,
                    mailNickname = userName.Split('@')[0],
                    userPrincipalName = userEmail,
                    passwordProfile = new
                    {
                        forceChangePasswordNextSignIn = true,
                        password = "StrongPassword!23"
                    }
                };

                var response = await client.PostAsJsonAsync("https://graph.microsoft.com/v1.0/users", internalUserPayload);

                return response.IsSuccessStatusCode;
            }
        }

        private async Task<bool> UpdateExternalUserAsync(string accessToken, string userEmail, string userName)
        {
            // Example: Resend invitation or update external user details
            // You can adjust this method to handle the specific update operations needed.
            return await InviteExternalUserAsync(accessToken, userEmail, userName);
        }

        private async Task<bool> UpdateInternalUserAsync(string accessToken, string userEmail, string userName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                var internalUserPayload = new
                {
                    accountEnabled = true,
                    displayName = userName,
                    mailNickname = userName.Split('@')[0]
                    // Add other properties to update as needed
                };

                var response = await client.PatchAsJsonAsync($"https://graph.microsoft.com/v1.0/users/{userEmail}", internalUserPayload);

                return response.IsSuccessStatusCode;
            }
        }

        private bool IsExternalUser(string userEmail)
        {
            // Replace "yourdomain.com" with your actual domain
            return !userEmail.EndsWith("@yourdomain.com", StringComparison.OrdinalIgnoreCase);
        }

        private bool EmailExistsInDatabase(string email)
        {
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            string query = "SELECT COUNT(1) FROM tbl_Users WHERE Email = @Email";

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

        private void InsertOrUpdateUser(bool isInsert)
        {
            // Your existing code to insert or update the user in your database
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            string query = "InsUpdUserKIT";

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;

                        // Add parameters with NULL handling
                        cmd.Parameters.AddWithValue("@User_id", string.IsNullOrEmpty(txtUserId.Text) ? (object)DBNull.Value : txtUserId.Text);
                        cmd.Parameters.AddWithValue("@User_name", string.IsNullOrEmpty(txtUserName.Text) ? (object)DBNull.Value : txtUserName.Text);
                        cmd.Parameters.AddWithValue("@User_type", string.IsNullOrEmpty(ddlUserType.SelectedValue) ? (object)DBNull.Value : ddlUserType.SelectedValue);
                        cmd.Parameters.AddWithValue("@User_code", string.IsNullOrEmpty(txtUserCode.Text) ? (object)DBNull.Value : txtUserCode.Text);
                        cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(ddlStatus.SelectedValue) ? (object)DBNull.Value : ddlStatus.SelectedValue);
                        cmd.Parameters.AddWithValue("@Email", string.IsNullOrEmpty(txtEmail.Text) ? (object)DBNull.Value : txtEmail.Text);
                        cmd.Parameters.AddWithValue("@GBVUser", string.IsNullOrEmpty(rblGBVUser.SelectedValue) ? (object)DBNull.Value : (object)(rblGBVUser.SelectedValue == "True"));
                        cmd.Parameters.AddWithValue("@IP_Approver", string.IsNullOrEmpty(rblIPApprover.SelectedValue) ? (object)DBNull.Value : (object)(rblIPApprover.SelectedValue == "True"));

                        if (int.TryParse(txtEmailReceiver.Text, out int emailReceiver))
                        {
                            cmd.Parameters.AddWithValue("@email_receiver", emailReceiver);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@email_receiver", DBNull.Value);
                        }

                        int? createdBy = null;
                        int? modifiedBy = null;

                        if (isInsert)
                        {
                            if (Session["uid"] != null && int.TryParse(Session["uid"].ToString(), out int createdByValue))
                            {
                                createdBy = createdByValue;
                            }
                            modifiedBy = null;
                        }
                        else
                        {
                            if (Session["uid"] != null && int.TryParse(Session["uid"].ToString(), out int modifiedByValue))
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
    }
}

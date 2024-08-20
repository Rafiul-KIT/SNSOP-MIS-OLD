using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Web.UI;
using Microsoft.Identity.Client;
using Newtonsoft.Json;

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

        protected void BtnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                string userEmail = txtEmail.Text;
                string userName = txtUserName.Text;

                // Check if the email already exists in the database
                if (EmailExistsInDatabase(userEmail))
                {
                    ShowWarning("The email address is already registered. Please try with a different email address.");
                    return;
                }

                // Get the access token from Azure AD
                string accessToken = GetAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    // Create the user in Azure AD
                    bool isUserCreated = CreateExternalUser(accessToken, userEmail, userName);

                    if (isUserCreated)
                    {
                        // Only insert the user into the database if the Azure AD user creation was successful
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


        protected void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                string userEmail = txtEmail.Text;
                string userName = txtUserName.Text;
                string originalEmail = hfOriginalEmail.Value;

                // Check if the email exists in the database and if it has changed from the original lookup
                if (EmailExistsInDatabase(userEmail))
                {
                    if (userEmail != originalEmail)
                    {
                        ShowWarning("The email address cannot be changed. Please revert to the original email or cancel the update.");
                        return;
                    }
                }
                else
                {
                    ShowWarning("The email is not registered.");
                    return;
                }
                // Get the access token from Azure AD
                string accessToken = GetAccessToken();

                if (!string.IsNullOrEmpty(accessToken))
                {
                    // Update the user in Azure AD (this could involve different logic based on your needs)
                    bool isUserUpdated = CreateExternalUser(accessToken, userEmail, userName);

                    if (isUserUpdated)
                    {
                        // Update the user in the database
                        InsertOrUpdateUser(isInsert: false);
                        ShowMessage("User updated successfully.");
                    }
                    else
                    {
                        ShowWarning("Failed to update user into database! Please contact your system administrator.");
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
                string clientId = "2d80a939-a955-4c38-a911-a1f8cb986a2c";
                string tenantId = "4a9fe01e-5c9c-4cb8-b299-fb5211533420";
                string clientSecret = "3BP8Q~TikdfxoOyfWjyWRj2RXlujNlS2LZQFnb-S";

                IConfidentialClientApplication confidentialClient = ConfidentialClientApplicationBuilder
                    .Create(clientId)
                    .WithClientSecret(clientSecret)
                    .WithAuthority(new Uri($"https://login.microsoftonline.com/{tenantId}/v2.0"))
                    .Build();

                string[] scopes = new[] { "https://graph.microsoft.com/.default" };

                AuthenticationResult authResult = confidentialClient
                    .AcquireTokenForClient(scopes)
                    .ExecuteAsync().GetAwaiter().GetResult();
                //Debug.WriteLine(authResult.AccessToken);
                return authResult.AccessToken;
            }
            catch (Exception ex)
            {
                ShowWarning($"Error generating access token: {ex.Message}");
                return null;
            }
        }


        private static bool CreateExternalUser(string accessToken, string userEmail, string userName)
        {
            string uri = "https://graph.microsoft.com/v1.0/invitations"; // Azure AD API endpoint for user invitation
            Uri urlCheck = new Uri(uri);

            //Debug.WriteLine("after urlccheck");
            // Create the HTTP request
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + accessToken);
            httpWReq.ContentType = "application/json";
            httpWReq.Method = "POST";

            try
            {
                // Create the request body
                var request = new UserCreationModel()
                {
                    invitedUserDisplayName = userName,
                    invitedUserEmailAddress = userEmail,
                    inviteRedirectUrl = "https://dev.karoothitbd.com:5051",
                    sendInvitationMessage = true
                };

                string reqStr = JsonConvert.SerializeObject(request);
                var data = Encoding.ASCII.GetBytes(reqStr);
                httpWReq.ContentLength = data.Length;

                // Write the request body to the request stream
                using (var newStream = httpWReq.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                    newStream.Flush();
                    newStream.Close();
                }

                // Get the response from the API
                using (HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                    {
                        string jsonResponse = reader.ReadToEnd();
                        //Debug.WriteLine("User created successfully: " + jsonResponse);
                        return true; // Indicate that the user was created successfully
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle WebException, which can occur due to network issues or HTTP errors
                //Debug.WriteLine($"An error occurred while calling the API: {ex.Message}");
                if (ex.Response != null)
                {
                    using (var responseStream = ex.Response.GetResponseStream())
                    using (var reader = new StreamReader(responseStream))
                    {
                        string errorResponse = reader.ReadToEnd();
                        //Debug.WriteLine("Error details: " + errorResponse);
                    }
                }
                return false; // Indicate that the user creation failed
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                //Debug.WriteLine($"An unexpected error occurred: {ex.Message}");
                return false; // Indicate that the user creation failed
            }
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
            rblEmailReceiver.Text = "";
            lblMessage.Text = "";
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

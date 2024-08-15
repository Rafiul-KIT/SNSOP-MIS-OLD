using System;
using System.Web.UI;
using System.Web;

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

        protected void btnLookup_Click(object sender, EventArgs e)
        {
            // Use ScriptManager to open the popup window
            //string script = "window.open('Lookups/UserManagementLookup.aspx', 'Lookup', 'width=800,height=600');";
            //ScriptManager.RegisterStartupScript(this, GetType(), "OpenWindow", script, true);
        }

        // Method to populate fields after selection from the lookup page
        public void ApplyChanges(string userId, string userName, string userType, string userCode, string status, string email, string gbvUser, string ipApprover, string emailReceiver)
        {
            // Populate the textboxes with the data received from the lookup page
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
    }
}

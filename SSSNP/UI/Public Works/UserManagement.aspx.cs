using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.SessionState;
using System.Text.RegularExpressions;


namespace SSSNP.UI.Public_Works
{
    public partial class UserManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialization code here if needed
            }
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
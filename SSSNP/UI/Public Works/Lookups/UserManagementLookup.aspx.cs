using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

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

        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            // Clear the filter fields
            txtFilterUserName.Text = string.Empty;
            txtFilterUserType.Text = string.Empty;
            txtFilterEmail.Text = string.Empty;

        }
    }
}

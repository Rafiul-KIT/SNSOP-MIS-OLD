using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works.Lookups
{
    public partial class UserManagementLookup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Initialization code here
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            // Logic to populate GridView based on the search criteria
            // Example:
            // var data = GetData(txtFilterUserName.Text, txtFilterUserType.Text, txtFilterEmail.Text);
            // grdDetails.DataSource = data;
            // grdDetails.DataBind();
        }

        protected void grdDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                string selectedValue = e.CommandArgument.ToString();
                string script = $"SelectRow('{selectedValue}');";
                ClientScript.RegisterStartupScript(this.GetType(), "SelectRow", script, true);
            }
        }
    }
}
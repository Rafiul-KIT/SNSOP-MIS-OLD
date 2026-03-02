using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class UpdateSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            success.Visible = true;

        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            Response.Redirect("IPBeneficiaryRegistration.aspx");
        }
    }
}
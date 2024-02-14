using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;

public partial class WageRequestResultDIS : System.Web.UI.Page
{
   

    
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString.Count > 0)
        {
            Label1.Text = "Wage Request is Submitted Successfully.Your Request Number is :" + Request.QueryString[0].ToString().Trim();
        }
    }
   
    
   
}

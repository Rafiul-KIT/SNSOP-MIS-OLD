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
using System.Text;

public partial class MasterwithoutMenu : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        //////TSTDashboard.Visible = false;
        //////IPDashboard.Visible = false;
        //////LiPA.Visible = false;
        //////LiDashboardIP.Visible = false;
    
   
  
     
        if (Session.Count > 0)
        {
            lblwecome.Text = Session["username"].ToString();

           
        }
        else
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }

    }



}

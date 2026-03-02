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

public partial class CCMaster1 : System.Web.UI.MasterPage
{

    protected void Page_Load(object sender, EventArgs e)
    {
        TSTDashboard.Visible = false;
        IPDashboard.Visible = false;
        
        if (Session.Count > 0)
        {
            lblwecome.Text = Session["username"].ToString();
            pnlreport.Visible = false;
            if (Session["user_type"].ToString() == "Adm")
            {
                TST.Visible = false;
                Masters.Visible = true;
                IP.Visible = false;
                
                //lblwecome.Text = "Admin";
            }
            else if (Session["user_type"].ToString() == "TST" || Session["user_type"].ToString() == "Adm")
            {
                Masters.Visible = false;
                TST.Visible = true;
                IP.Visible = false;
                TSTDashboard.Visible = true;
                //lblwecome.Text = "Technical Support Team";
            }
            else if (Session["user_type"].ToString() == "IP")
            {
                Masters.Visible = false;
                TST.Visible = false;
                IP.Visible = true;
                IPDashboard.Visible = true;
                //lblwecome.Text = "Implementing Partner";


            }
            else if (Session["user_type"].ToString() == "BDC")
            {
                BDC.Visible = true;
                TST.Visible = false;
                //Master.Visible = false;
                IP.Visible = false;
                PDC.Visible = false;
                //lblwecome.Text = "Implementing Partner";
            }
            else if (Session["user_type"].ToString() == "PDC")
            {
                PDC.Visible = true;
                TST.Visible = false;
                //Master.Visible = false;
                IP.Visible = false;
                BDC.Visible = false;
               //lblwecome.Text = "Implementing Partner";
            }

            if (Session["user_type"].ToString() == "Help" || Session["user_type"].ToString() == "Admin")
            {
                pnlreport.Visible = true;

            }
            else
            {
                pnlreport.Visible = false;
            }
        }
        else
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }

    }



}

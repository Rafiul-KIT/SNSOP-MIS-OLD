using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class UI_APIDashboard : System.Web.UI.Page
{
    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("~/frmuserlogin.aspx");
        }

        if (!IsPostBack)
        {
            FillDetails();

            if (Session["user_type"].ToString().Trim() == "Cordinator")
            {
                Label14.Text = "Pending Wage requests for Review";
                Label15.Text = "Reviewed Wage requests";

            }
            if (Session["user_type"].ToString().Trim() == "Specialist")
            {
                Label14.Text = "Pending Wage requests for Verification";
                Label15.Text = "Verified Wage requests";

            }
        }
    }

    void FillDetails()
    {
        DataSet ds = new DataSet();
        if (Session["User_Code"].ToString().Trim() != null && Session["User_Code"].ToString().Trim() != "")
        {
            ds = Gen.getAPIDashboard(Session["User_Code"].ToString().Trim());
        }
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                Label1.Text = ds.Tables[0].Rows[0]["Total"].ToString().Trim();
                Label26.Text = ds.Tables[0].Rows[0]["Pending"].ToString().Trim();
                Label13.Text = ds.Tables[0].Rows[0]["confirmed"].ToString().Trim();
                Label41.Text = ds.Tables[0].Rows[0]["Dropped"].ToString().Trim();
            }

        }
    }
}

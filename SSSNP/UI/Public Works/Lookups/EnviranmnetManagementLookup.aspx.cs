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

public partial class UI_Public_Works_Lookups_EnviranmnetManagementLookup : System.Web.UI.Page
{
    General Gen = new General();
    General clsGeneral = new General();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Projects();
            clsGeneral.getIpDet(ddlIP);
            if (Session["user_type"].ToString() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString()).Value;
                ddlIP.Enabled = false;

                Gen.getProjects(ddlNameOfPrject, ddlIP.SelectedValue);
            }
            else if (Session["user_type"].ToString() == "TST")
            {
                clsGeneral.GetIpsByTST(ddlIP, Session["uid"].ToString());

                ddlIP.Enabled = true;
            }
          //  fillgrid();

            Gen.getStates(ddlState);
        }
    }
    private void Projects()
    {
        DataSet ds = new DataSet();
        ds = Gen.Getprojects();

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlNameOfPrject.DataSource = ds.Tables[0];
            ddlNameOfPrject.DataTextField = "PrjName";
            ddlNameOfPrject.DataValueField = "intPrjid";
            ddlNameOfPrject.DataBind();
            ddlNameOfPrject.Items.Insert(0, "--Select--");
        }


    }
    void fillgrid()
    {

        string User = "";
        if (Session["user_type"].ToString() == "TST")
        {
            //User = Session["User_Code"].ToString();
            User = "%";
        }
        else
        {
            User = "%";
        }

        DataSet ds = new DataSet();
        ds = Gen.getEnvData(ddlIP.SelectedValue, ddlNameOfPrject.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue);

        if (ds.Tables[0].Rows.Count > 0)
        {

            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
          //  btnexp.Visible = false;
           // BtnPrint.Visible = false;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");

        }
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomas();
    }
    void getBomas()
    {
        ddlBoma.Items.Clear();
        if (ddlPayam.SelectedIndex != 0)
        {
            Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
        }
        else
        {
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }

    void getPayams()
    {
        ddlPayam.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }

    protected void BtnSave1_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = Gen.getEnvData(ddlIP.SelectedValue, ddlNameOfPrject.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue);

        if (ds.Tables[0].Rows.Count > 0)
        {
           
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    private void clear()
    {
        ddlIP.SelectedIndex = ddlNameOfPrject.SelectedIndex = ddlState.SelectedIndex = ddlCounties.SelectedIndex = ddlPayam.SelectedIndex = ddlBoma.SelectedIndex = 0;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intWorkid") + "')");
        }
    }
}

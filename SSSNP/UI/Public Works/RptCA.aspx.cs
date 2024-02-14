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

//created by suresh as on 1-17-2016 for county adm lookup 
//tables td_CountyAdmDet,getCASearch

public partial class RptCA : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();
    
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            fillgrid();
        }
    }
    #endregion


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {

        string User = "";
        if (Session["user_type"].ToString() == "TST")
        {
            User = Session["uid"].ToString();
        }
        else
        {
            User = "%";
        }

        DataSet ds = Gen.getCASearch(ddlState.SelectedValue, ddlCounties.SelectedValue,txtca.Text,User);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HyperLink h1 = (HyperLink)e.Row.Cells[0].Controls[0];
            h1.Text = "Edit";
            h1.NavigateUrl = "TSTCountyAdmistratorReg.aspx?prj=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intCountyAdm")).Trim();
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCounties.Items.Clear();
        
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            

        }
    }

    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}

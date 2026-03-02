
#region Using/Inherits Classess
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
using System.Drawing;
using System.IO;
using Telerik.Web.UI;
using System.Text;
#endregion

#region Amendment History Begin
//Form Name : Beneficiary Search Details
//Purpose : To Search Beneficiary Details
//Tables used : tm_States,tm_Counties,tm_Payams,tm_Bomas,tbl_BeneficiaryDet
//Stored procedures Used : sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,sp_GetBenfMainDetails
#endregion Amendment History End


public partial class frmMainBenDet : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        #region Is not Postback Statement
        if (!IsPostBack)
        {
            clsGeneral.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
            Gen.getStates(ddlState);
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString[0].ToString() != "")
                {
                    hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
        #endregion
    }
    #endregion

    #region Search Report and Filling Details Functions
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetMainBenfDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        DataTable dt = ds.Tables[0];
        telGridDetails.DataSource = dt;
        telGridDetails.Visible = true;
        telGridDetails.DataBind();
    }
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }
    #endregion

    #region Gridview Events
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HyperLink h1 = (HyperLink)e.Row.Cells[10].Controls[0];
            h1.Target = "_blank";
            h1.NavigateUrl = "frmBenFullDetails.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();
        }
    }

    protected void telGridDetails_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetMainBenfDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        DataTable dt = ds.Tables[0];
        telGridDetails.DataSource = dt;
        telGridDetails.Visible = true;

    }

    protected void telGridDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = telGridDetails.MasterTableView.PageSize * telGridDetails.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            HyperLink hyperLinkView = e.Item.FindControl("colLink") as HyperLink;
            hyperLinkView.Target = "_blank";
            hyperLinkView.NavigateUrl = "frmBenViewDetails1.aspx?ID=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenId"];
        }

        //if (Request.QueryString[0].ToString() == "1")
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;
        //        //item["checkColumn"].Visible = false;
        //        item["btnUpdate"].Text = "Update";
        //    }
        //}
    }
    #endregion

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
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

    protected void ddlCounties_SelectedIndexChanged1(object sender, EventArgs e)
    {
        getPayams();
    }
    protected void ddlPayam_SelectedIndexChanged1(object sender, EventArgs e)
    {
        getBomas();
    }

    protected void ddlAreasofWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        getWorkActivities();
    }
    void getWorkActivities()
    {


    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

        getworksbyPojects();
    }

    void getworksbyPojects()
    {


    }
    #endregion

    #region Button Event Functionalities
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (ddlIP.SelectedIndex == 0 && ddlState.SelectedIndex == 0 && ddlCounties.SelectedIndex == 0 && ddlPayam.SelectedIndex == 0 && ddlBoma.SelectedIndex == 0 && txtbenf.Text == "" && txtenrollment.Text == "" && txthousehold.Text == "")
        {
            lblresult.Text = "Please select Atleast one to Search";
        }
        else
        {
            // btnexp.Visible = true;
            fillgrid();
            lblresult.Text = "";
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmMainBenDet.aspx");
    }
    #endregion

    #region Export Excel and Print Functions

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    void fillgridExport()
    {
        DataSet ds1 = new DataSet();
        ds1 = clsGeneral.GetMainBenfDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if (ds1.Tables.Count > 0)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds1.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds1.Tables[0];
                grdDetails.DataBind();
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds1.Tables[0];
                grdDetails.DataBind();

            }
        }
    }


    protected void btnexp_Click(object sender, EventArgs e)
    {
        grdDetails.Visible = true;

        this.fillgridExport();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BeneficiaryDetails.csv");
        Response.Charset = "";
        Response.ContentType = "application/text";


        StringBuilder columnbind = new StringBuilder();
        for (int k = 0; k < grdDetails.Columns.Count; k++)
        {

            columnbind.Append(grdDetails.Columns[k].HeaderText + ',');
        }

        columnbind.Append("\r\n");
        for (int i = 0; i < grdDetails.Rows.Count; i++)
        {
            for (int k = 0; k < grdDetails.Columns.Count; k++)
            {

                columnbind.Append(grdDetails.Rows[i].Cells[k].Text + ',');
            }

            columnbind.Append("\r\n");
        }
        Response.Output.Write(columnbind.ToString());
        Response.Flush();
        Response.End();
        grdDetails.Visible = false;
    }
    #endregion

}

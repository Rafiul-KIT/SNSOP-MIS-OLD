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

public partial class WagesPaymentDasboardDrildownAdmin : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
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
            clsGeneral.getIpDet(ddlIP);

            Gen.getStates(ddlState);
            if (Request.QueryString.Count>0)
            {
                if (Request.QueryString[0].ToString() != "")
                { hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
    }
    #endregion


   
    void fillgrid()
    {
        DataSet ds = new DataSet();
        if (Request.QueryString[0].ToString().Trim() == "1")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Total Wage Requests";
        }
        else  if (hdnStat.Value.ToString() == "2")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Pending Wage Requests";
        }
        else  if (hdnStat.Value.ToString() == "3")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Approved Wage Requests";
        }
        else if (hdnStat.Value.ToString() == "4")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Rejected Wage Requests";
        }
        else if (hdnStat.Value.ToString() == "5")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Total Payment Confirmation";
        }
        else if (hdnStat.Value.ToString() == "6")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Pending Payment Confirmation";
        }
        else if (hdnStat.Value.ToString() == "7")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Approved Payment Confirmation";
        }
        else if (hdnStat.Value.ToString() == "8")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Rejected Payment Confirmation";
        }
        else if (hdnStat.Value.ToString() == "60")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDetAdmin(Session["uid"].ToString().Trim(), Session["AdminWageOfficerType"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Rejected Invoice by CO";
        }

        if(ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();

                grdDetails.Columns[9].Visible = false;
                if (Session["AdminWageOfficerType"].ToString().Trim() == "PA")
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "6")
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = true;
                    }

                    else
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = false;
                    }
                }
                else
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        grdDetails.Columns[8].Visible = true;
                        grdDetails.Columns[9].Visible = false;
                    }
                    else if (Request.QueryString[0].ToString() == "6")
                    {
                        grdDetails.Columns[8].Visible = true;
                        grdDetails.Columns[9].Visible = false;
                    }

                    else
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = false;
                    }
                }

                if (Request.QueryString[0].ToString() == "60")
                {
                    grdDetails.Columns[12].Visible = true;
                }
                else
                {
                    grdDetails.Columns[12].Visible = false;
                }
               
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
        }
      


    }
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
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
            HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hdfid");
            hdfDeptID.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intWagePaymentReqid"));
            HiddenField hdfreqids = (HiddenField)e.Row.FindControl("hdfreqids");
            hdfreqids.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intWagePaymentReqPAid"));
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");




            if (Session["AdminWageOfficerType"].ToString().Trim() == "Cordinator")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "2"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "100"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "7"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1000"));
                }
            }
            else if (Session["AdminWageOfficerType"].ToString().Trim() == "Specialist")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "3"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "101"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "8"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1001"));
                }
            }
            else if (Session["AdminWageOfficerType"].ToString().Trim() == "Manager")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "4"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "102"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "9"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1002"));
                }
            }
            else if (Session["AdminWageOfficerType"].ToString().Trim() == "PA")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "5"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "103"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "10"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1003"));
                }
            }

            else if (Session["AdminWageOfficerType"].ToString().Trim() == "WB")
            {
               
                    ddlStatus.Items.Insert(0, new ListItem("Cleared", "10"));
                    ddlStatus.Items.Insert(1, new ListItem("Not Cleared", "1003"));
                
            }   









           // ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intWorkId") + "','" + DataBinder.Eval(e.Row.DataItem, "invoiceid") + "')");
        }
    }


    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gen.getProjects(ddlProject, ddlIP.SelectedValue);
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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click1(object sender, EventArgs e)
    {

        Button BtnSave = (Button)sender;

        GridViewRow row = (GridViewRow)BtnSave.NamingContainer;
        HiddenField hdfApplID = (HiddenField)row.FindControl("hdfid");
        HiddenField hdfreqids = (HiddenField)row.FindControl("hdfreqids");
         DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
        TextBox txtPromotor = (TextBox)row.FindControl("txtPromotor");

    //    HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hdfid");




        int a = Gen.InsApprovalStatusPaymentConfirmation(hdfreqids.Value, ddlStatus.SelectedValue, Session["uid"].ToString().Trim(), txtPromotor.Text);
        if (a >= 0)
        {
            lblresult.Text = "Submitted Successfully";

            fillgrid();
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("WagesPaymentDasboardDrildownAdmin.aspx?Status=" + Request.QueryString[0].ToString().Trim());
    }
}

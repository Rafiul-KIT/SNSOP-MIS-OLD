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

public partial class TransferBenfAfterConfirmation : System.Web.UI.Page
{
    //#region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    //#endregion
    decimal NoofPayams1, NoofBomas1, TotalWorkProposed1, TotaltobeApproved1, TotalInprogress1, TotalClosed1, TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1, Type11, Type21, Type31, Type41, Type51, Type61, TotalWorkProposed12;
    //#region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            //  clsGeneral.getIpDet(ddlIP);

            Gen.getStates(ddlState);
            Gen.getStates(ddlstate1);
            TrasDisplay.Visible = false;
            TrasferBoma1.Visible = false;
            Button2.Visible = false;
        //    fillgrid();
            //if (Request.QueryString.Count > 0)
            //{
            //    if (Request.QueryString[0].ToString() != "")
            //    {
            //        hdnStat.Value = Request.QueryString[0].ToString();
            //        fillgrid();
            //    }
            //}
        }
    }
    void fillgrid()
    {

        //Button1.Visible = false;
        //Button2.Visible = false;
        DataSet ds = Gen.FillBenDetailsforDeleteandTransfer("%", txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            //Button2.Visible = true;
            lblMsg.Visible = false;
            lblMsg.Text = "";
            TrasDisplay.Visible = true;

            TrasferBoma1.Visible = true;
            Button2.Visible = true;
            //if (Request.QueryString[0].ToString() == "1")
            //{
            //    //Button1.Visible = true;
            //    Button2.Visible = true;
            //    grdDetails.Columns[1].Visible = true;
            //}
            //else
            //{
            //    grdDetails.Columns[1].Visible = false;
            //    //Button1.Visible = false;
            //    Button2.Visible = false;
            //}

        }
        else
        {
            lblMsg.Visible = true;
            lblMsg.Text = "No Records found or if enrollment number given, given enrollment number beneficiary is in paid or already applied wage.";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            TrasDisplay.Visible = false;

            TrasferBoma1.Visible = false;
            Button2.Visible = false;
        }

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TransferBenfAfterConfirmation.aspx");
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (Request.QueryString[0].ToString() == "1")
        //{
        //    //  HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hlRedirectAAB");

        //    if ((e.Row.RowType == DataControlRowType.DataRow))
        //    {
        //        HiddenField hdfGroundedNo1 = (HiddenField)e.Row.Cells[0].FindControl("hdfid");
        //        hdfGroundedNo1.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenTempid")).Trim();
        //        //e.Row.Cells[9].Visible = true;
        //        //e.Row.Cells[10].Visible = true;

        //        //HyperLink h1 = (HyperLink)e.Row.Cells[9].Controls[0];
        //        //h1.Text = "Update";
        //        //h1.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenTempid")).Trim();

        //        // HyperLink h2 = (HyperLink)e.Row.Cells[9].Controls[0];
        //        //  h2.Text = "Delete";
        //        // h1.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenTempid")).Trim();
        //    }
        //}
        //else
        //{

        //    if ((e.Row.RowType == DataControlRowType.DataRow))
        //    {
        //        //e.Row.Cells[9].Visible = false;
        //        //e.Row.Cells[10].Visible = false;
        //    }
        //}
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HiddenField hdfid = (HiddenField)e.Row.Cells[0].FindControl("hdfid");
            hdfid.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();
        }
    

    }


    protected void grdDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    Gen.getProjects(ddlProject, ddlIP.SelectedValue);
    //}
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
    void getcountiesUpdate()
    {
        ddlcounty1.Items.Clear();
        ddlpayam1.Items.Clear();

        ddlboma1.Items.Clear();

        if (ddlstate1.SelectedIndex != 0)
        {
            Gen.getCounties(ddlcounty1, ddlstate1.SelectedValue);
            ddlpayam1.Items.Insert(0, "--Select--");
            ddlboma1.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlcounty1.Items.Insert(0, "--Select--");
            ddlpayam1.Items.Insert(0, "--Select--");
            ddlboma1.Items.Insert(0, "--Select--");

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
    void getPayamsUpdate()
    {
        ddlpayam1.Items.Clear();
        ddlboma1.Items.Clear();
      
        if (ddlcounty1.SelectedIndex != 0)
        {
            Gen.getPayams(ddlpayam1, ddlcounty1.SelectedValue);
            ddlboma1.Items.Insert(0, "--Select--");
        }
        else
        {
            ddlpayam1.Items.Insert(0, "--Select--");
            ddlboma1.Items.Insert(0, "--Select--");
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
    void getBomasUpdate()
    {

        ddlboma1.Items.Clear();
        if (ddlpayam1.SelectedIndex != 0)
        {
            Gen.getBomas(ddlboma1, ddlpayam1.SelectedValue);
        }
        else
        {
            ddlboma1.Items.Insert(0, "--Select--");
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
    protected void Button1_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {

    }
    protected void Button2_Click(object sender, EventArgs e)
    {



        if (ddlstate1.SelectedIndex == 0 || ddlpayam1.SelectedIndex == 0 || ddlcounty1.SelectedIndex == 0 || ddlboma1.SelectedIndex == 0)
        {
            lblmsg0.Text = "Please Select Trasfer to Boma";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        else
        {

            LblMessage2.Text = "";
            int count = 0;
            for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
            {
                CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("CheckBox1");


                if (c.Checked == true)
                {
                    count++;
                }
                else
                {
                }
            }
            if (count == 0)
            {
                lblmsg0.Text = "At least one Household should be selected";
                Failure.Visible = true;
                ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
                return;

            }
            else
            {
                for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
                {
                    GridViewRow row = (GridViewRow)grdDetails.Rows[iii];
                    HiddenField hdfid = (HiddenField)grdDetails.Rows[iii].Cells[0].FindControl("hdfid");
                    CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("CheckBox1");


                    if (c.Checked == true)
                    {
                        int result = Gen.TransferAPIDashAfterConfirmation(hdfid.Value, ddlstate1.SelectedValue, ddlcounty1.SelectedValue, ddlpayam1.SelectedValue, ddlboma1.SelectedValue, Session["uid"].ToString());

                    }
                }
                LblMessage2.Text = "Successfully Transfered";
                ddlstate1.SelectedIndex = 0;
                ddlcounty1.Items.Clear();
                ddlcounty1.Items.Insert(0, "--Select--");
                ddlpayam1.Items.Clear();
                ddlpayam1.Items.Insert(0, "--Select--");
                ddlboma1.Items.Clear();
                ddlboma1.Items.Insert(0, "--Select--");
                lblmsg0.Text = "";
                Failure.Visible = false;
                fillgrid();
            }
        }
    }
    protected void ddlstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcountiesUpdate();

    }
    protected void ddlcounty1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayamsUpdate();
    }
    protected void ddlpayam1_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomasUpdate();
    }
}

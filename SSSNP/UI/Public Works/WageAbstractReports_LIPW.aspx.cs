using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;
// Developer : Srikanth
// Date : 07-08-2017
// Purpose: Attendance Report Time out expired


public partial class WageAbstractReports_LIPW : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int cnt = -1;
    decimal paidamount, AttendanceCount, paidamounta;
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (!IsPostBack)
        {

            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {

    }



    void clear()
    {


        grdClosebatchdet.DataSource = null;
        grdClosebatchdet.DataBind();

        tblreport.Visible = false;
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {




        }
    }
    protected void BtnSave3_Click(object sender, EventArgs e)
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";

        //if (cmf.convertDateIndia(txtStartDate.Text) < cmf.convertDateIndia(lblWorkStartDate.Text))
        //{
        //    lblmsg0.Text = "Work Start Date should be greater than the paid date. ";
        //    success.Visible = false;
        //    Failure.Visible = true;

        //    return;

        //}

        //if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //{
        //    lblmsg0.Text = "Paid date should be less than the current date.";
        //    success.Visible = false;
        //    Failure.Visible = true;
        //    return;

        //}


        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();


            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ds = Gen.getWageReportAbstract(hdfID.Value.ToString(),  ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, "LIPW");
            }
            else
            {
                ds = Gen.getWageReportAbstract(hdfID.Value.ToString(),  ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, "LIPW");
            }


            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;



                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                    if (Session["user_type"].ToString().Trim() == "IP")
                    {
                        LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString();
                    }
                    else
                    {
                        LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString() + "  | Total Paid Amount in SSP : " + ds.Tables[1].Rows[0][3].ToString();
                    }
                    //  LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                    //    grdClosebatchdet.Rows[0].Visible = false;
                    lblmessage.Text = "";

                }
                else
                {
                    //tblreport.Visible = false;
                    if (Session["user_type"].ToString().Trim() == "IP")
                    {
                        LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                    }
                    else
                    {
                        LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                    }
                    //LblResultA.Text = "Total Paid Amount in USD: 0" + "  | Total Attendance : 0"  + "  | Total Paid Amount in SSP : 0" ;
                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                    //lblmessage.Text = "There is no attendance/wages in between days";
                }
            }
            else
            {
                if (Session["user_type"].ToString().Trim() == "IP")
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                }
                else
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                }
                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();


                //lblmessage.Text = "There is no attendance/wages in between days";
            }

            //DataSet dsnew = new DataSet();

            //dsnew = Gen.getAttendanceReportNewReportMultipleWagesTotal(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue);

        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
            //LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
            lblmessage.Text = "There is no attendance/wages ";
        }
        finally
        {
        }
    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("WageAbstractReports_LIPW.aspx");
    }

    public string DisableTheButton(Control pge, Control btn, string vldGroup)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
        if (vldGroup.Trim() == "")
        {
            sb.Append("if (Page_ClientValidate() == false) { return false; }} ");

        }
        else
        {
            sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");

        }
        sb.Append("this.value = 'Please wait...';");
        sb.Append("this.disabled = true;");
        sb.Append(pge.Page.GetPostBackEventReference(btn));
        //sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }

    protected void grdClosebatchdet_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    cnt = cnt + 1;
        //    e.Row.Cells[0].Text = cnt.ToString().Trim();

        //DataSet ds = (DataSet)ViewState["dt"];

        //int iPresentCount = 0, iAbsentCount = 0;

        //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
        //{


        //    if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
        //    {
        //        iPresentCount = iPresentCount + 1;
        //    }
        //    if (e.Row.Cells[i].Text.Trim().ToLower() == "absent")
        //    {
        //        iAbsentCount = iAbsentCount + 1;
        //    }

        //}

        //e.Row.Cells[ds.Tables[0].Columns.Count - 1].Text = iPresentCount.ToString().Trim();
        //e.Row.Cells[ds.Tables[0].Columns.Count - 2].Text = iAbsentCount.ToString().Trim();

        ////localhost.Himayatmis objUserDetails = new localhost.Himayatmis();
        //DataSet dsresult = new DataSet();
        ////dsresult = objUserDetails.getEmployeeDetbyIDDate(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intTraineeID")).Trim(), new comFunctions().convertDateIndia(txtAttendancedate.Text));

        //dsresult = cmn.atdt(grdClosebatchdet.DataKeys[e.Row.RowIndex].Values[0].ToString(),grdClosebatchdet.DataKeys[e.Row.RowIndex].Values[1].ToString(), txtAttendancedate.Text);
        //if (dsresult.Tables[0].Rows.Count > 0)
        //{
        //    e.Row.Cells[6].Text = dsresult.Tables[0].Rows[0]["Attendence"].ToString().Trim();



        //}
        //else
        //{ e.Row.Cells[6].Text = "Absent"; }

        //grdClosebatchdet.Rows[0].Visible = false;
        //  }


    }
    public void fillgrid()
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";

        //if (cmf.convertDateIndia(txtStartDate.Text) < cmf.convertDateIndia(lblWorkStartDate.Text))
        //{
        //    lblmsg0.Text = "Work Start Date should be greater than the paid date. ";
        //    success.Visible = false;
        //    Failure.Visible = true;

        //    return;

        //}

        //if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //{
        //    lblmsg0.Text = "Paid date should be less than the current date.";
        //    success.Visible = false;
        //    Failure.Visible = true;
        //    return;

        //}


        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();


            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ds = Gen.getWageReportAbstract(hdfID.Value.ToString(),  ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, "LIPW");
            }
            else
            {
                ds = Gen.getWageReportAbstract(hdfID.Value.ToString(),  ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, "LIPW");
            }


            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;



                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                    if (Session["user_type"].ToString().Trim() == "IP")
                    {
                        LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString();
                    }
                    else
                    {
                        LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString() + "  | Total Paid Amount in SSP : " + ds.Tables[1].Rows[0][3].ToString();
                    }
                    //  LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                    //    grdClosebatchdet.Rows[0].Visible = false;
                    lblmessage.Text = "";

                }
                else
                {
                    //tblreport.Visible = false;
                    if (Session["user_type"].ToString().Trim() == "IP")
                    {
                        LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                    }
                    else
                    {
                        LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                    }
                    //LblResultA.Text = "Total Paid Amount in USD: 0" + "  | Total Attendance : 0"  + "  | Total Paid Amount in SSP : 0" ;
                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                    //lblmessage.Text = "There is no attendance/wages in between days";
                }
            }
            else
            {
                if (Session["user_type"].ToString().Trim() == "IP")
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                }
                else
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                }
                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();
                //lblmessage.Text = "There is no attendance/wages in between days";
            }

            //DataSet dsnew = new DataSet();
            //dsnew = Gen.getAttendanceReportNewReportMultipleWagesTotal(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue);
        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
            //LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
            lblmessage.Text = "There is no attendance/wages ";
        }
        finally
        {
        }
    }
    protected void grdClosebatchdet_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdClosebatchdet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdClosebatchdet.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    //public override void VerifyRenderingInServerForm(Control control)
    //{
    //    //base.VerifyRenderingInServerForm(control);
    //}
    protected void ExportToExcel()
    {
        //GridView Export = new GridView();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;FileName=ViewFiles.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {
            // grdBReg.DataBind();

            //  lblresult0.Visible = true;

            //            Export.DataSource = FillBRegister();
            //          Export.DataBind();
            tblreport.Visible = true;
            //ds = cmn.getpostplacedtraineedet(Request.QueryString["type"].ToString(), Request.QueryString["intPIAid"].ToString());
            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();

            //GridView1.DataSource = ds.Tables[0];
            //GridView1.DataBind();

            //   grdtraineedetails.AllowPaging = false;
            //grdtraineedetails.DataBind();
            // grdtraineedetails.PageSize = 100000;

            //     grdtraineedetails0.Columns[1].Visible = false;

            //grdDetails.DataSource = //((DataTable)Session["dtt_load"]).DefaultView; ;

            // grdDetails.DataBind();
            //cmn.fillhimayatsummary(grdDetails);



            tblreport.RenderControl(htw);

            // grdBReg.RenderControl(htw);
            // ((DataTable)Session["dtt_load"]).Clear();
        }
        catch (Exception eee)
        {
        }






        //DataTable dt = new DataTable();


        //print.RenderControl(htw);
        Page.Response.Write(sw.ToString());

        //   grdtraineedetails.PageSize = 20;
        //   LinkButton1.PostBackUrl = Request.Url.ToString();
        //   lblresult0.Visible = false;
        Response.End();

        //grdDetails.Columns[1].Visible = true;
        // grdDetails.Visible = false;
        // grdtraineedetails.AllowPaging = true;



        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + " Wage payment report.xls");
        //Response.Charset = "";
        //this.EnableViewState = false;
        //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //System.IO.StringWriter sw = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        //try
        //{



        //    grdDetails.Visible = true;
        //    DataSet ds1 = new DataSet();
        //    ds1 = Gen.getWagesReport(hdfID.Value.ToString());
        //    if (ds1.Tables[0].Rows.Count > 0)
        //    {
        //        grdDetails.DataSource = ds1.Tables[0];
        //        grdDetails.DataBind();
        //        BtnSave1.Visible = true;
        //    }
        //    else
        //    {
        //        BtnSave1.Visible = false;
        //        grdDetails.DataSource = null;
        //        grdDetails.DataBind();
        //    }

        //    grdDetails.RenderControl(htw);


        //}
        //catch (Exception eee)
        //{
        //}







        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.AddHeader("content-disposition", "attachment;FileName=ViewDetails.xls");
        //Response.Charset = "";
        //// this.EnableViewState = false;
        //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //System.IO.StringWriter sw = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        //try
        //{



        //    grdClosebatchdet.Visible = true;
        //    //fillgrid1();
        //    fillgrid();

        //    grdClosebatchdet.RenderControl(htw);


        //}
        //catch (Exception eee)
        //{
        //}







        //Page.Response.Write(sw.ToString());


        //Response.End();
        //grdClosebatchdet.Visible = false;


    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }


    //public override void
    // VerifyRenderingInServerForm(Control control)
    //{
    //    return;
    //}



    protected void btnexp_Click(object sender, EventArgs e)
    {
        ExportToCSV();
        //ExportToExcel();
    }


    protected void ExportToCSV()
    {
        //GridView Export = new GridView();
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/text";
        Response.AddHeader("content-disposition", "attachment;FileName=WagesRequestAbstractReport.csv");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringBuilder sbGrid = new StringBuilder();
        //grdClosebatchdet.DataBind();
        try
        {
            //Header
            for (int k = 1; k < grdClosebatchdet.HeaderRow.Cells.Count; k++)
            {
                sbGrid.Append(grdClosebatchdet.HeaderRow.Cells[k].Text + ',');
            }
            sbGrid.Append("\r\n");

            for (int i = 0; i < grdClosebatchdet.Rows.Count; i++)
            {
                for (int k = 1; k < grdClosebatchdet.HeaderRow.Cells.Count; k++)
                {
                    //add separator
                    sbGrid.Append(grdClosebatchdet.Rows[i].Cells[k].Text + ',');
                }
                //append new line
                sbGrid.Append("\r\n");
            }
        }
        catch (Exception eee)
        {
        }
        Response.Write(sbGrid.ToString());
        Response.Flush();
        Response.End();
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

    protected void grdClosebatchdet_PreRender(object sender, EventArgs e)
    {
        //Hide SSP and 
        if (Session["user_type"].ToString().Trim() == "IP")
        {
            int iTotColumns = 0;
            iTotColumns = grdClosebatchdet.Columns.Count;
            for (int iCount = 0; iCount < iTotColumns; iCount++)
            {
                if (grdClosebatchdet.Columns[iCount].HeaderText.ToLower().Trim() == "AmountPaidSSP".ToLower())
                {
                    grdClosebatchdet.Columns[iCount].Visible = false;
                }
                else
                {
                    grdClosebatchdet.Columns[iCount].Visible = true;
                }
            }
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
}

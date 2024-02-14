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
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text.html.simpleparser;

#region Amendament History

/// <summary>

/// Project : SNSDP

/// Purpose of Form: SNSDP Wages Report

/// Working Task : SNSDP Wages Report

/// Developer : Srikanth

/// Date : 04-08-2017

/// Store Procedures Modified : getWagesReport

/// Tables Used Modified: tr_BenWagesPayment

/// </summary>
#endregion
public partial class PDCHouseholdWagesSSPRpt : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages
    decimal paidamount, AttendanceCount, paidamounta;

    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        
        if (!IsPostBack)
        {

            
            
        }


        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            

            
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            
            
        }

    }

    void FillDetails()
    {
        btnexp.Visible = false;
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();
        
        //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());
        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");


            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();

            DataSet ds1 = new DataSet();
            ds1= Gen.getWagesReport(hdfID.Value.ToString());

            if (ds1.Tables[0].Rows.Count > 0)
            {
                Failure.Visible = false;
                success.Visible = false;
                lblmsg0.Text = "";
                grdDetails.DataSource = ds1.Tables[0];
                grdDetails.DataBind();
                grdDetails.Columns[15].Visible = false;
                grdDetails.Columns[16].Visible = false;
               
                btnexp.Visible = true;
                tr_group_search.Visible = true;
            }
            else
            {

                Failure.Visible = true;
                success.Visible = false;
                lblmsg0.Text = "There is no Wage payments for this Work code";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                btnexp.Visible = false;
            }
        }
    }    



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        



     
       
    }

    void clear()
    {
        
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal paidamount1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt"));
            paidamount = paidamount1 + paidamount;

            decimal AttendanceCount1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "cnt"));
            AttendanceCount = AttendanceCount1 + AttendanceCount;


            decimal paidamount1a = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "PaidAmountUSD"));
            paidamounta = paidamount1a + paidamounta;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[9].Text = "Total";
            e.Row.Cells[10].Text = AttendanceCount.ToString();
            e.Row.Cells[13].Text = paidamount.ToString();
            e.Row.Cells[16].Text = paidamounta.ToString();
        }
    }
    protected void BtnSave3_Click(object sender, EventArgs e)
    {


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
        

       
    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdWagesRpt.aspx");
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

    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {

        ExportToExcel();
    }
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
            grdDetails.Visible = true;
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



            div_print.RenderControl(htw);

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

    }

    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_group_type.SelectedValue == "")
        {
            FillDetails();
        }
        if (ddl_group_type.SelectedValue == "Group")
        {
            //tr_group_code.Style.Add("display", "block");
            tr_group_code.Visible = true;
           // hdfFlagID.Value = "1";

            DataSet ds1 = new DataSet();
            ds1 = Gen.getWagesReportByGroup(hdfID.Value.ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                Failure.Visible = false;
                success.Visible = false;
                lblmsg0.Text = "";
                grdDetails.DataSource = ds1.Tables[0];
                grdDetails.DataBind();
                grdDetails.Columns[15].Visible = false;
                grdDetails.Columns[16].Visible = false;
                btnexp.Visible = true;
            }
            else
            {

                Failure.Visible = true;
                success.Visible = false;
                lblmsg0.Text = "There is no Wage payments for this Work code";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                btnexp.Visible = false;
            }
        }
        if (ddl_group_type.SelectedValue == "CST")
        {
            //tr_group_code.Style.Add("display", "none");
            tr_group_code.Visible = false;
            ddl_group_code.SelectedValue = "";
           // hdfFlagID.Value = "1";

            DataSet ds1 = new DataSet();
            ds1 = Gen.getWagesReportByGroup(hdfID.Value.ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);

            if (ds1.Tables[0].Rows.Count > 0)
            {
                Failure.Visible = false;
                success.Visible = false;
                lblmsg0.Text = "";
                grdDetails.DataSource = ds1.Tables[0];
                grdDetails.DataBind();
                grdDetails.Columns[15].Visible = false;
                grdDetails.Columns[16].Visible = false;
                btnexp.Visible = true;
            }
            else
            {

                Failure.Visible = true;
                success.Visible = false;
                lblmsg0.Text = "There is no Wage payments for this Work code";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                btnexp.Visible = false;
            }
        }

    }
    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        //hdfFlagID.Value = "1";

        DataSet ds = new DataSet();



        DataSet ds1 = new DataSet();
        ds1 = Gen.getWagesReportByGroup(hdfID.Value.ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            Failure.Visible = false;
            success.Visible = false;
            lblmsg0.Text = "";
            grdDetails.DataSource = ds1.Tables[0];
            grdDetails.DataBind();
            grdDetails.Columns[15].Visible = false;
            grdDetails.Columns[16].Visible = false;
            btnexp.Visible = true;
        }
        else
        {

            Failure.Visible = true;
            success.Visible = false;
            lblmsg0.Text = "There is no Wage payments for this Work code";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            btnexp.Visible = false;
        }
    }

}

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


public partial class PDCHouseholdAttendanceRpt : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int cnt = -1;
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("AttendaceUnderMaintainance.aspx");
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
     //   this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (!IsPostBack)
        {

            
           
        }


        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            

          
            FillDetails();
            
           
        }

    }

    void FillDetails()
    {
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
            

        }
    }    



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        



       
       
    }

    public void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = Gen.getAttendanceReport(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddlBeneficiary.SelectedValue, ddlGroupList.SelectedValue);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dt"] = ds;
            grdClosebatchdet.DataSource = ds.Tables[0];
            grdClosebatchdet.DataBind();
           grdClosebatchdet.Rows[0].Visible = false;

        }
        else
        {

            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
        }
        
    }

    void clear()
    {
        

        grdClosebatchdet.DataSource = null;
        grdClosebatchdet.DataBind();
        TxtFromDate.Text = "";
    //    ddlyear.SelectedIndex = 0;
        TxtToDate.Text = "";
        lblProject.Text = "";
        lblWorkCode.Text = "";
        lblBoma.Text = "";
        lblWorkStartDate.Text = "";
        lblState.Text="";
        lblCounty.Text = "";
        lblPayam.Text = "";
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            cnt = cnt + 1;
            e.Row.Cells[0].Text = cnt.ToString().Trim();
           
            DataSet ds = (DataSet)ViewState["dt"];

            int iPresentCount = 0, iAbsentCount = 0;

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {

                if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
                {
                    iPresentCount = iPresentCount + 1;
                }
                if (e.Row.Cells[i].Text.Trim().ToLower() == "absent")
                {
                    iAbsentCount = iAbsentCount + 1;
                }

            }

            e.Row.Cells[4].Text = iPresentCount.ToString().Trim();
            e.Row.Cells[5].Text = iAbsentCount.ToString().Trim();
         // e.Row.Cells[ds.Tables[0].Columns[0]].Text = cnt.ToString().Trim();

            
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

        try
        {
            DataSet ds = new DataSet();
            ds = Gen.getAttendanceReport(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddlBeneficiary.SelectedValue, hdGroupNo.Value);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                   grdClosebatchdet.Rows[0].Visible = false;
                    lblmessage.Text = "";

                }
                else
                {

                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                    lblmessage.Text = "There is no attendance in between days";
                }
            }
            else
            {

                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();
                lblmessage.Text = "There is no attendance in between days";
            }
        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            lblmessage.Text = "There is no attendance in between days";
        }
        finally
        {
        }
    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdAttendanceRpt.aspx");
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

        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            cnt = cnt + 1;
            e.Row.Cells[0].Text = cnt.ToString().Trim();

            DataSet ds = (DataSet)ViewState["dt"];

            int iPresentCount = 0, iAbsentCount = 0;

            for (int i = 0; i < ds.Tables[0].Columns.Count+1; i++)
            {

                if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
                {
                    iPresentCount = iPresentCount + 1;
                }
                if (e.Row.Cells[i].Text.Trim().ToLower() == "absent")
                {
                    iAbsentCount = iAbsentCount + 1;
                }

            }

            e.Row.Cells[4].Text = iPresentCount.ToString().Trim();
            e.Row.Cells[5].Text = iAbsentCount.ToString().Trim();
            e.Row.Cells[6].Text = Convert.ToString(Convert.ToInt32(((cmf.convertDateIndia(TxtToDate.Text).AddDays(1) - cmf.convertDateIndia(TxtFromDate.Text)).TotalDays)) - (iPresentCount + iAbsentCount));
        //  e.Row.Cells[6].Text  = Convert.ToString(Convert.ToInt32(e.Row.Cells[3].Text) - (iPresentCount + iAbsentCount));
            ////// e.Row.Cells[ds.Tables[0].Columns[0]].Text = cnt.ToString().Trim();


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
            grdClosebatchdet.Visible = true;
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
        ExportToExcel();
        //ExportToExcel();
    }
    //protected void ExportToExcel()
    //{



    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + " Wage payment report.xls");
    //    Response.Charset = "";
    //    // this.EnableViewState = false;
    //    Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

    //    System.IO.StringWriter sw = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

    //    try
    //    {



    //        grdClosebatchdet.Visible = true;
    //        //DataSet ds1 = new DataSet();
    //        //ds1 = Gen.getWagesReport(hdfID.Value.ToString());
    //        //if (ds1.Tables[0].Rows.Count > 0)
    //        //{
    //        //    grdClosebatchdet.DataSource = ds1.Tables[0];
    //        //    grdClosebatchdet.DataBind();
    //        //    //BtnSave1.Visible = true;
    //        //}
    //        //else
    //        //{
    //        //    // BtnSave1.Visible = false;
    //        //    grdClosebatchdet.DataSource = null;
    //        //    grdClosebatchdet.DataBind();
    //        //}
    //        fillgrid();
    //        grdClosebatchdet.RenderControl(htw);


    //    }
    //    catch (Exception eee)
    //    {
    //    }

    //}


    protected void BeneficiarySelect(object sender, EventArgs e)
    {
        if (ddlBeneficiary.Text == "Group")
        {
            trGroup.Visible = true;
            //hdBeneficiaryType.Value = "";
        }
        else
        {
            trGroup.Visible = false;
            // hdBeneficiaryType.Value = ddlBeneficiary.SelectedValue.ToString();
            hdGroupNo.Value = "";
            ddlGroupList.SelectedValue = "";
        }
    }

    protected void ddlGroupList_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdGroupNo.Value = ddlGroupList.SelectedValue.ToString();
        // hdBeneficiaryType.Value = ddlGroupList.SelectedValue.ToString();
    }
}

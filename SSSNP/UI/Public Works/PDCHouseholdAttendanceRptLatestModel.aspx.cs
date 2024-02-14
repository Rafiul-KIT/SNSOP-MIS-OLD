using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;

public partial class PDCHouseholdAttendanceRptLatestModel : System.Web.UI.Page
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
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
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

            fillgrid();

        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
    }

    public void fillgrid()
    {
        try
        {
            DateTime d1 = cmf.convertDateIndia(TxtFromDate.Text);
            DateTime d2 = cmf.convertDateIndia(TxtToDate.Text);

            if ((d2.Date - d1.Date).TotalDays >= 75)
            {
                lblmessage.Text = "Please Select 75 days Duration only";
            }
            else
            {
                hdfcounts.Value = ((d2.Date - d1.Date).TotalDays + 7).ToString();

                DataSet ds = new DataSet();
                ds = Gen.getAttendanceReportLatestModel(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, "%", "%");
                if (ds != null)
                {
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        rgAttendanceReport.DataSource = ds.Tables[0];
                        rgAttendanceReport.DataBind();
                        //  ViewState["dt"] = ds;
                        //grdClosebatchdet.DataSource = ds.Tables[0];
                        //grdClosebatchdet.DataBind();
                        //grdClosebatchdet.Rows[0].Visible = false;
                        lblmessage.Text = "";
                    }
                    else
                    {
                        rgAttendanceReport.DataSource = null;
                        rgAttendanceReport.DataBind();
                        //grdClosebatchdet.DataSource = null;
                        //grdClosebatchdet.DataBind();
                        lblmessage.Text = "There is no attendance in between days";
                    }
                }
                else
                {
                    rgAttendanceReport.DataSource = null;
                    rgAttendanceReport.DataBind();
                    //grdClosebatchdet.DataSource = null;
                    //grdClosebatchdet.DataBind();
                    lblmessage.Text = "There is no attendance in between days";
                }
            }
        }
        catch (Exception ex)
        {
            rgAttendanceReport.DataSource = null;
            rgAttendanceReport.DataBind();
            //grdClosebatchdet.DataSource = null;
            //grdClosebatchdet.DataBind();
            //lblmessage.Text = "There is no attendance in between days - Ex";
        }
        finally
        {
        }

    }

    void clear()
    {
        //grdClosebatchdet.DataSource = null;
        //grdClosebatchdet.DataBind();
        TxtFromDate.Text = "";
        //    ddlyear.SelectedIndex = 0;
        TxtToDate.Text = "";
        lblProject.Text = "";
        lblWorkCode.Text = "";
        lblBoma.Text = "";
        lblWorkStartDate.Text = "";
        lblState.Text = "";
        lblCounty.Text = "";
        lblPayam.Text = "";
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    protected void rgAttendanceReport_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = new DataSet();
        if ((hdfID.Value != null && hdfID.Value != "") && (TxtFromDate != null && TxtFromDate.Text != null && TxtFromDate.Text != "") && (TxtToDate != null && TxtToDate.Text != null && TxtToDate.Text != ""))
        {
            ds = Gen.getAttendanceReportLatestModel(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, "%", "%");

            DataTable dt = ds.Tables[0];
            //Remove first line of dataTable because empty (due to SP data building)
            //dt.Rows.Remove(dt.Rows[0]);
            dt.AcceptChanges();
            rgAttendanceReport.DataSource = dt;
        }
    }

    protected void rgAttendanceReport_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = rgAttendanceReport.MasterTableView.PageSize * rgAttendanceReport.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
        }
    }

    protected void rgAttendanceReport_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }


    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            //   cnt = cnt + 1;
            //   e.Row.Cells[0].Text = cnt.ToString().Trim();
            //   DataSet ds = (DataSet)ViewState["dt"];
            //   int iPresentCount = 0, iAbsentCount = 0;
            //   for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //   {
            //       if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
            //       {
            //           iPresentCount = iPresentCount + 1;
            //       }
            //       if (e.Row.Cells[i].Text.Trim().ToLower() == "absent")
            //       {
            //           iAbsentCount = iAbsentCount + 1;
            //       }
            //   }
            //   e.Row.Cells[4].Text = iPresentCount.ToString().Trim();
            //   e.Row.Cells[5].Text = iAbsentCount.ToString().Trim();
            //// e.Row.Cells[ds.Tables[0].Columns[0]].Text = cnt.ToString().Trim();
        }
    }

    protected void BtnSave3_Click(object sender, EventArgs e)
    {
       
        fillgrid();

    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdAttendanceRptLatestModel.aspx");
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

    ////protected void grdClosebatchdet_RowDataBound(object sender, GridViewRowEventArgs e)
    ////{
    ////    if (e.Row.RowType == DataControlRowType.DataRow)
    ////    {
    ////        //DataSet dsn = new DataSet();
    ////        //dsn = Gen.getAttendanceReportLatestPresent(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim());
    ////        //if (dsn.Tables[0].Rows.Count > 0)
    ////        //{
    ////        //    e.Row.Cells[3].Text = dsn.Tables[1].Rows[0][0].ToString();
    ////        //    e.Row.Cells[4].Text = dsn.Tables[0].Rows[0][0].ToString();
    ////        //    e.Row.Cells[5].Text = Convert.ToString(Convert.ToInt32(e.Row.Cells[3].Text) + Convert.ToInt32(dsn.Tables[0].Rows[0][0].ToString()));
    ////        //}

    ////        cnt = cnt + 1;
    ////        e.Row.Cells[0].Text = cnt.ToString().Trim();
    ////        //DataSet ds = (DataSet)ViewState["dt"];
    ////        int iPresentCount = 0, iAbsentCount = 0;

    ////        for (int i = 0; i < Convert.ToInt32(hdfcounts.Value); i++)
    ////        {
    ////            if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
    ////            {
    ////                iPresentCount = iPresentCount + 1;
    ////            }
    ////            if (i > 5)
    ////            {
    ////                if (e.Row.Cells[i].Text.Trim().ToLower() != "present")
    ////                {
    ////                    iAbsentCount = iAbsentCount + 1;
    ////                }
    ////            }
    ////        }
    ////        e.Row.Cells[4].Text = iPresentCount.ToString().Trim();
    ////        e.Row.Cells[5].Text = iAbsentCount.ToString().Trim();
    ////    }
    ////}

    //protected void grdClosebatchdet_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}

    //protected void grdClosebatchdet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdClosebatchdet.PageIndex = e.NewPageIndex;
    //    fillgrid();
    //}

    //protected void ExportToExcel()
    //{
    //    //GridView Export = new GridView();
    //    Response.Clear();
    //    Response.Buffer = true;
    //    Response.ContentType = "application/vnd.ms-excel";
    //    Response.AddHeader("content-disposition", "attachment;FileName=ViewFiles.xls");
    //    Response.Charset = "";
    //    // this.EnableViewState = false;
    //    Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    System.IO.StringWriter sw = new System.IO.StringWriter();
    //    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

    //    try
    //    {
    //        // grdBReg.DataBind();
    //        //  lblresult0.Visible = true;
    //        //            Export.DataSource = FillBRegister();
    //        //          Export.DataBind();
    //        grdClosebatchdet.Visible = true;
    //        //ds = cmn.getpostplacedtraineedet(Request.QueryString["type"].ToString(), Request.QueryString["intPIAid"].ToString());
    //        //GridView1.DataSource = ds.Tables[0];
    //        //GridView1.DataBind();
    //        //GridView1.DataSource = ds.Tables[0];
    //        //GridView1.DataBind();
    //        //   grdtraineedetails.AllowPaging = false;
    //        //grdtraineedetails.DataBind();
    //        // grdtraineedetails.PageSize = 100000;
    //        //     grdtraineedetails0.Columns[1].Visible = false;
    //        //grdDetails.DataSource = //((DataTable)Session["dtt_load"]).DefaultView; ;
    //        // grdDetails.DataBind();
    //        //cmn.fillhimayatsummary(grdDetails);
    //        div_print.RenderControl(htw);
    //        // grdBReg.RenderControl(htw);
    //        // ((DataTable)Session["dtt_load"]).Clear();
    //    }
    //    catch (Exception eee)
    //    {
    //    }

    //    //DataTable dt = new DataTable();
    //    //print.RenderControl(htw);
    //    Page.Response.Write(sw.ToString());
    //    //   grdtraineedetails.PageSize = 20;
    //    //   LinkButton1.PostBackUrl = Request.Url.ToString();
    //    //   lblresult0.Visible = false;
    //    Response.End();
    //    //grdDetails.Columns[1].Visible = true;
    //    // grdDetails.Visible = false;
    //    // grdtraineedetails.AllowPaging = true;
    //    //Response.Clear();
    //    //Response.Buffer = true;
    //    //Response.ContentType = "application/vnd.ms-excel";
    //    //Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + " Wage payment report.xls");
    //    //Response.Charset = "";
    //    //this.EnableViewState = false;
    //    //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    //System.IO.StringWriter sw = new System.IO.StringWriter();
    //    //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
    //    //try
    //    //{
    //    //    grdDetails.Visible = true;
    //    //    DataSet ds1 = new DataSet();
    //    //    ds1 = Gen.getWagesReport(hdfID.Value.ToString());
    //    //    if (ds1.Tables[0].Rows.Count > 0)
    //    //    {
    //    //        grdDetails.DataSource = ds1.Tables[0];
    //    //        grdDetails.DataBind();
    //    //        BtnSave1.Visible = true;
    //    //    }
    //    //    else
    //    //    {
    //    //        BtnSave1.Visible = false;
    //    //        grdDetails.DataSource = null;
    //    //        grdDetails.DataBind();
    //    //    }
    //    //    grdDetails.RenderControl(htw);
    //    //}
    //    //catch (Exception eee)
    //    //{
    //    //}
    //    //Response.Clear();
    //    //Response.Buffer = true;
    //    //Response.ContentType = "application/vnd.ms-excel";
    //    //Response.AddHeader("content-disposition", "attachment;FileName=ViewDetails.xls");
    //    //Response.Charset = "";
    //    //// this.EnableViewState = false;
    //    //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //    //System.IO.StringWriter sw = new System.IO.StringWriter();
    //    //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);
    //    //try
    //    //{
    //    //    grdClosebatchdet.Visible = true;
    //    //    //fillgrid1();
    //    //    fillgrid();
    //    //    grdClosebatchdet.RenderControl(htw);
    //    //}
    //    //catch (Exception eee)
    //    //{
    //    //}
    //    //Page.Response.Write(sw.ToString());
    //    //Response.End();
    //    //grdClosebatchdet.Visible = false;
    //}

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
        //ExportToExcel();
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
}
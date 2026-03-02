using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using OfficeOpenXml;
using System.Text.RegularExpressions;

public partial class PDCHouseholdAttendanceRptNewFormat : System.Web.UI.Page
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


    protected void rgAttendanceReport_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = Gen.getAttendanceReportNewReport(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddlBeneficiary.SelectedValue, hdGroupNo.Value);

        DataTable dt = ds.Tables[0];
        rgAttendanceReport.DataSource = dt;
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

    public void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = Gen.getAttendanceReportNewReport(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddlBeneficiary.SelectedValue, ddlGroupList.SelectedValue);

        if (ds.Tables[0].Rows.Count > 0)
        {
            ViewState["dt"] = ds;
            tblreport.Visible = true;
            //grdClosebatchdet.DataSource = ds.Tables[0];
            //grdClosebatchdet.DataBind();
            //   grdClosebatchdet.Rows[0].Visible = false;
        }
        else
        {
            tblreport.Visible = false;
            //grdClosebatchdet.DataSource = null;
            //grdClosebatchdet.DataBind();
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

        lblProject0.Text = lblProject.Text;
        lblState0.Text = lblState.Text;
        lblCounty0.Text = lblCounty.Text;
        lblPayam0.Text = lblPayam.Text;
        lblBoma0.Text = lblBoma.Text;
        lblfromdate.Text = TxtFromDate.Text;
        lbltodate.Text = TxtToDate.Text;
        lblWorkCode0.Text = lblWorkCode.Text;
        lblWorkStartDate0.Text = lblWorkStartDate.Text;

        try
        {
            DataSet ds = new DataSet();
            ds = Gen.getAttendanceReportNewReport(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddlBeneficiary.SelectedValue, hdGroupNo.Value);
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    
                    rgAttendanceReport.DataSource = ds.Tables[0];
                    rgAttendanceReport.DataBind();
                    
                    lblmessage.Text = "";
                }
                else
                {
                    tblreport.Visible = false;

                    
                    lblmessage.Text = "There is no attendance in between days";
                }
            }
            else
            {
                
                lblmessage.Text = "There is no attendance in between days";
            }
        }
        catch (Exception ex)
        {
            
            lblmessage.Text = "There is no attendance in between days";
        }
        finally
        {
        }
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdAttendanceRptNewFormat.aspx");
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



    /*protected void ExportToExcel()
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
            
            tblreport.Visible = true;
            
            tblreport.RenderControl(htw);
            
        }
        catch (Exception eee)
        {
        }

        
        Page.Response.Write(sw.ToString());
        
        Response.End();
        

    }*/

    protected void ExportToExcel()
    {
        // Try to reuse the DataSet already prepared in ViewState
        var ds = ViewState["dt"] as DataSet;

        // Fallback: if ViewState is empty, requery (keeps behavior consistent with your page)
        if (ds == null)
        {
            ds = Gen.getAttendanceReportNewReport(
                hdfID.Value.ToString(),
                TxtFromDate.Text,
                TxtToDate.Text,
                ddlBeneficiary.SelectedValue,
                hdGroupNo.Value
            );
        }

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            lblmessage.Text = "There is no attendance in between days";
            return;
        }

        // Build the Excel package
        using (var package = new ExcelPackage())
        {
            // Create a worksheet per DataTable
            for (int t = 0; t < ds.Tables.Count; t++)
            {
                DataTable table = ds.Tables[t];
                if (table == null) continue;

                string sheetName = string.IsNullOrWhiteSpace(table.TableName)
                    ? (t == 0 ? "Attendance" : $"Sheet{t + 1}")
                    : table.TableName;

                // EPPlus sheet name rules: max 31 chars, no []:*?/\
                sheetName = Regex.Replace(sheetName, @"[\[\]\:\*\?\/\\]", "_");
                if (sheetName.Length > 31) sheetName = sheetName.Substring(0, 31);

                var ws = package.Workbook.Worksheets.Add(sheetName);

                // Load data: true = print column names as header
                ws.Cells["A1"].LoadFromDataTable(table, true);

                // Optional: autosize columns (can be heavy for very large data)
                // ws.Cells[ws.Dimension.Address].AutoFitColumns();
            }

            // Prepare HTTP response
            string fileName = $"Attendance_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";
            var bytes = package.GetAsByteArray();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", $"attachment; filename={fileName}");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(bytes);
            Response.Flush();

            // Avoid ThreadAbortException from Response.End()
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
        ExportToExcel();
        //ExportToExcel();
    }


    
    protected void BeneficiarySelect(object sender, EventArgs e)
    {
        if (ddlBeneficiary.Text == "Group")
        {
            trGroup.Visible = true;
            
        }
        else
        {
            trGroup.Visible = false;
            
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
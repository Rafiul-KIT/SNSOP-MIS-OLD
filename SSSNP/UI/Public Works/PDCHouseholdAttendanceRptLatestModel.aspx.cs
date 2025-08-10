using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using OfficeOpenXml;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using System.Drawing;

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
                        lblmessage.Text = "";
                    }
                    else
                    {
                        rgAttendanceReport.DataSource = null;
                        rgAttendanceReport.DataBind();
                        lblmessage.Text = "There is no attendance in between days";
                    }
                }
                else
                {
                    rgAttendanceReport.DataSource = null;
                    rgAttendanceReport.DataBind();
                    lblmessage.Text = "There is no attendance in between days";
                }
            }
        }
        catch (Exception ex)
        {
            rgAttendanceReport.DataSource = null;
            rgAttendanceReport.DataBind();
        }
        finally
        {
        }

    }

    void clear()
    {
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

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
        //ExportToExcel();
        ExportToExcel();
    }

    /*private void ExportToExcel()
    {
        // Make sure the grid has data (NeedDataSource will run on Rebind())
        rgAttendanceReport.ExportSettings.FileName = "Attendance_" + DateTime.Now.ToString("yyyyMMdd_HHmm");
        rgAttendanceReport.ExportSettings.ExportOnlyData = true;   // no buttons, etc.
        rgAttendanceReport.ExportSettings.IgnorePaging = true;     // all rows
        rgAttendanceReport.ExportSettings.OpenInNewWindow = true;  // nice UX
        rgAttendanceReport.ExportSettings.Excel.Format = Telerik.Web.UI.GridExcelExportFormat.Xlsx;

        rgAttendanceReport.Rebind();               // ensures DataSource is set
        rgAttendanceReport.MasterTableView.ExportToExcel();
    }*/

    /*private void ExportToExcel()
    {
        var ds = Gen.getAttendanceReportLatestModel(
            hdfID.Value, TxtFromDate.Text, TxtToDate.Text, "%", "%");

        if (ds == null || ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
        {
            lblmessage.Text = "No data to export.";
            return;
        }

        using (var package = new ExcelPackage())
        {
            // If you have multiple tables and want multiple sheets:
            for (int t = 0; t < ds.Tables.Count; t++)
            {
                var dt = ds.Tables[t];
                var ws = package.Workbook.Worksheets.Add(
                    string.IsNullOrWhiteSpace(dt.TableName) ? $"Sheet{t + 1}" : dt.TableName);

                // Load data (true => include column headers)
                ws.Cells["A1"].LoadFromDataTable(dt, true, TableStyles.Medium9);

                // Auto-fit columns
                ws.Cells[ws.Dimension.Address].AutoFitColumns();

                // Optional: format header
                using (var rng = ws.Cells[1, 1, 1, dt.Columns.Count])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(1, 49, 97)); // dark blue
                    rng.Style.Font.Color.SetColor(Color.White);
                }

                // Optional: set nice formats for typed columns
                for (int c = 0; c < dt.Columns.Count; c++)
                {
                    var col = dt.Columns[c];
                    var dataRange = ws.Cells[2, c + 1, dt.Rows.Count + 1, c + 1];
                    if (col.DataType == typeof(DateTime))
                        dataRange.Style.Numberformat.Format = "dd-mm-yyyy";
                    else if (col.DataType == typeof(decimal) || col.DataType == typeof(double) || col.DataType == typeof(float))
                        dataRange.Style.Numberformat.Format = "#,##0.00";
                    else if (col.DataType == typeof(int) || col.DataType == typeof(long))
                        dataRange.Style.Numberformat.Format = "0";
                    // If your “date”/“number” columns are strings, either convert before binding
                    // or force text: dataRange.Style.Numberformat.Format = "@";
                }
            }

            var bytes = package.GetAsByteArray();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", $"attachment;filename=Attendance_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
            Response.BinaryWrite(bytes);
            // Avoid ThreadAbortException:
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }*/

    private void ExportToExcel()
    {
        // Get your data (same as grid)
        var ds = Gen.getAttendanceReportLatestModel(
            hdfID.Value, TxtFromDate.Text, TxtToDate.Text, "%", "%");

        var dt = (ds != null && ds.Tables.Count > 0) ? ds.Tables[0] : null;
        if (dt == null || dt.Rows.Count == 0)
        {
            lblmessage.Text = "No data to export.";
            return;
        }

        // For EPPlus 5/6+: uncomment next line. For EPPlus 4.x: REMOVE it.
        // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

        using (var package = new ExcelPackage())
        {
            var ws = package.Workbook.Worksheets.Add("Attendance");

            // Plain dump with column headers, no styling, no formats
            ws.Cells["A1"].LoadFromDataTable(dt, true);

            var bytes = package.GetAsByteArray();

            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition",
                $"attachment;filename=Attendance_{DateTime.Now:yyyyMMdd_HHmm}.xlsx");
            Response.BinaryWrite(bytes);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }
}
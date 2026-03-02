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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using OfficeOpenXml;
using System.Text;
public partial class rptAttendanceFailure : System.Web.UI.Page
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
            fillgrid();
        }
    }
    #endregion
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetrptAttendanceFailure(txtWorkID.Text, txthouseholdNo.Text, txtADate.Text, txtDate.Text);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                //decimal CourseFee = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalPaidAmount"));
                //decimal Discount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotaUnpaidlAmount"));
                //decimal Paid = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
                //decimal TotalPaidBeneficiries = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalPaidBeneficiries"));
                //decimal TotalUnpaidBeneficiries = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalUnpaidBeneficiries"));
                //decimal TotalBeneficiries = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalBeneficiries"));
                //decimal TotalPaidAttendance = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalPaidAttendance"));
                //decimal TotalUnpaidAttendance = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalUnpaidAttendance"));
                //decimal TotalAttendance = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalAttendance"));


                //grdDetails.FooterRow.Cells[1].Text = "Total";
                //grdDetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                //grdDetails.FooterRow.Cells[2].Text = CourseFee.ToString();
                //grdDetails.FooterRow.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[3].Text = Discount.ToString();
                //grdDetails.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[4].Text = Paid.ToString();
                //grdDetails.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[5].Text = TotalPaidBeneficiries.ToString();
                //grdDetails.FooterRow.Cells[5].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[6].Text = TotalUnpaidBeneficiries.ToString();
                //grdDetails.FooterRow.Cells[6].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[7].Text = TotalBeneficiries.ToString();
                //grdDetails.FooterRow.Cells[7].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[8].Text = TotalPaidAttendance.ToString();
                //grdDetails.FooterRow.Cells[8].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[9].Text = TotalUnpaidAttendance.ToString();
                //grdDetails.FooterRow.Cells[9].HorizontalAlign = HorizontalAlign.Right;
                //grdDetails.FooterRow.Cells[10].Text = TotalAttendance.ToString();
                //grdDetails.FooterRow.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
        }
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
        }
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }

    private void ExportToExcel()
    {
        string filename = "Attendance Failure Report.xlsx";
        DataTable dt = new DataTable();
        dt = clsGeneral.GetrptAttendanceFailure(txtWorkID.Text, txthouseholdNo.Text, txtADate.Text, txtDate.Text).Tables[0];
        //Set Name of DataTables.
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.ClearContent();
        HttpContext.Current.Response.ClearHeaders();
        HttpContext.Current.Response.Buffer = true;
        HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=Attendance Failure Report.xlsx");


        using (ExcelPackage pack = new ExcelPackage())
        {
            ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
            ws.Cells["A1"].LoadFromDataTable(dt, true);
            var ms = new System.IO.MemoryStream();
            pack.SaveAs(ms);
            ms.WriteTo(HttpContext.Current.Response.OutputStream);
        }

        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();
    }

    private void ExportToCsv()
    {

        //string filename = "NewExcelData.xlsx";
        DataTable dt = new DataTable();
        dt = clsGeneral.GetrptAttendanceFailure(txtWorkID.Text, txthouseholdNo.Text, txtADate.Text, txtDate.Text).Tables[0];
        //Build the CSV file data as a Comma separated string.
        Response.Clear();

        Response.Buffer = true;

        Response.AddHeader("content-disposition", "attachment;filename=DataTable.csv");

        Response.Charset = "";

        Response.ContentType = "application/text";

        StringBuilder sb = new StringBuilder();

        for (int k = 0; k < dt.Columns.Count; k++)
        {
            sb.Append(dt.Columns[k].ColumnName + ',');
        }

        //append new line

        sb.Append("\r\n");

        for (int i = 0; i < dt.Rows.Count; i++)
        {

            for (int k = 0; k < dt.Columns.Count; k++)
            {
                //add separator

                sb.Append(dt.Rows[i][k].ToString().Replace(",", ";") + ',');

            }

            //append new line

            sb.Append("\r\n");

        }

        Response.Output.Write(sb.ToString());

        Response.Flush();

        Response.End();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}

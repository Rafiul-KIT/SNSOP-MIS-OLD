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
using OfficeOpenXml;
using Telerik.Web.UI;
using System.Net;
using HtmlAgilityPack;
using SSSNP_SNSOP.App_Code;

public partial class WageAbstractReportsPaidAPI : System.Web.UI.Page
{
    //designing and developed by siva as on 27-02-2016
    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int cnt = -1;
    decimal paidamount, AttendanceCount, paidamounta;
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    ErrorLogger eLog = new ErrorLogger();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (Session.Count == 0)
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }

        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
            //btnPrintSelected.Visible = false;

            btnPrintSelected.Enabled = false;
            BtnPrint.Enabled = false;
            btnexp.Enabled = false;
            btnexp0.Enabled = false;

        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        clear();
    }

    void clear()
    {
        //grdClosebatchdet.DataSource = null;
        //grdClosebatchdet.DataBind();
        rgDetails.DataSource = null;
        rgDetails.Rebind();
        tblreport.Visible = false;
        ddlState.SelectedIndex = 0;
        ddlCounties.SelectedIndex = 0;
        ddlPayam.SelectedIndex = 0;
        ddlBoma.SelectedIndex = 0;
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
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();

            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ds = Gen.getWagePaidReportAbstractAPI(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
            }
            else
            {
                ds = Gen.getWagePaidReportAbstractAPI(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    //grdClosebatchdet.DataSource = ds.Tables[0];
                    //grdClosebatchdet.DataBind();
                    btnPrintSelected.Enabled = true;
                    BtnPrint.Enabled = true;
                    btnexp.Enabled = true;
                    btnexp0.Enabled = true;
                    rgDetails.Rebind();
                    lblmessage.Text = "";
                }
                else
                {
                    rgDetails.DataSource = null;
                    rgDetails.Rebind();
                    btnPrintSelected.Enabled = false;
                    BtnPrint.Enabled = false;
                    btnexp.Enabled = false;
                    btnexp0.Enabled = false;
                    //grdClosebatchdet.DataSource = null;
                    //grdClosebatchdet.DataBind();
                }
            }
            else
            {
                rgDetails.DataSource = null;
                rgDetails.Rebind();
                btnPrintSelected.Enabled = false;
                BtnPrint.Enabled = false;
                btnexp.Enabled = false;
                btnexp0.Enabled = false;
                //grdClosebatchdet.DataSource = null;
                //grdClosebatchdet.DataBind();
            }
        }
        catch (Exception ex)
        {
            //grdClosebatchdet.DataSource = null;
            //grdClosebatchdet.DataBind();
            btnPrintSelected.Enabled = false;
            BtnPrint.Enabled = false;
            btnexp.Enabled = false;
            btnexp0.Enabled = false;
            rgDetails.DataSource = null;
            rgDetails.Rebind();
            lblmessage.Text = "There is no attendance/wages";
        }
        finally
        {
        }
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("WageAbstractReportsPaidAPI.aspx");
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
    }

    public void fillgrid()
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();

            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ds = Gen.getWagePaidReportAbstractAPI(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
            }
            else
            {
                ds = Gen.getWagePaidReportAbstractAPI(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    //grdClosebatchdet.DataSource = ds.Tables[0];
                    //grdClosebatchdet.DataBind();
                    lblmessage.Text = "";
                }
                else
                {
                    //grdClosebatchdet.DataSource = null;
                    //grdClosebatchdet.DataBind();
                }
            }
            else
            {
                //grdClosebatchdet.DataSource = null;
            }
        }
        catch (Exception ex)
        {
            //grdClosebatchdet.DataSource = null;
            //grdClosebatchdet.DataBind();
            lblmessage.Text = "There is no attendance/wages Ex ";
        }
        finally
        {
        }
    }

    protected void rgDetails_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = Gen.getWagePaidReportAbstractAPI(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
        DataTable dt = ds.Tables[0];
        if (ds.Tables[0].Rows.Count > 0)
        {
            rgDetails.DataSource = dt;
            rgDetails.Visible = true;
        }
        else
        {
            rgDetails.DataSource = null;
            rgDetails.Visible = true;
        }
    }

    protected void rgDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = rgDetails.MasterTableView.PageSize * rgDetails.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();


            HyperLink hyperLinkViewPaidPerson = e.Item.FindControl("hlInvoice") as HyperLink;
            //hyperLinkViewPaidPerson.Text = ((System.Data.DataRowView)e.Item.DataItem).Row["Paidto"].ToString();
            hyperLinkViewPaidPerson.Target = "_blank";
            hyperLinkViewPaidPerson.NavigateUrl = "ViewWagesReqDetailsAutoRec.aspx?WorkReqId=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intWagePaymentReqid"];
            //hyperLinkViewPaidPerson.NavigateUrl = "ViewWagesReqDetails.aspx?WorkReqId=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intWagePaymentReqid"];
        }
    }

    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;FileName=ViewFiles.xls");
        Response.Charset = "";
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
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnPrintSelected_Click(object sender, EventArgs e)
    {
        List<string> lsIntWageReqNo = new List<string>();

        string strUrlInvoicePrint = string .Empty;
        string strFinalURL = string.Empty;
        string strCurrentURL = Context.Request.Url.ToString();


        strUrlInvoicePrint = strCurrentURL.Replace("WageAbstractReportsPaidAPI", "BulkInvoicePrintAutoRec") + "?intWageReqId={0}";
        //strUrlInvoicePrint = strCurrentURL+ "BulkInvoicePrint.aspx?intWageReqId={0}";

        string snsopweb = "";
        if (ConfigurationManager.AppSettings["build.profile.active"].ToString() == "prod")
        {
            snsopweb = ConfigurationManager.AppSettings["SSSNPWEB"].ToString() + "UI/Public Works/BulkInvoicePrintAutoRec.aspx?intWageReqId={0}";
        }
        else
        {
            snsopweb = strUrlInvoicePrint;
        }

        foreach (GridDataItem dataItem in rgDetails.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                string strIntWageReqNo = "";
                strIntWageReqNo = dataItem.GetDataKeyValue("intWagePaymentReqid").ToString();
                lsIntWageReqNo.Add(strIntWageReqNo);
            }
        }

        if (lsIntWageReqNo.Count == 0)
        {
            LblResultA.Text = "At least one Invoice should be selected";
            LblResultA.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmessage.Text, true);
            //return;
        }
        else
        {
            string strNewPage = "<div style=\"break-after:page\"></div>";
            string strAllInvoices = "";
            //for each selected invoice\
            foreach (string strIntWageReqNoTemp in lsIntWageReqNo)
            {

                //call page for invoice rendering and  get HTML of panel
                //string strCompleteUrl = strUrlInvoicePrint + "?intWageReqId=" + strIntWageReqNoTemp;
                WebClient WebCl = new WebClient();
                //string strCompleteUrl = string.Format(strUrlInvoicePrint, strIntWageReqNoTemp);
                //string strHtmlInvTemp = WebCl.DownloadString(strCompleteUrl);

                string strCompleteUrl1 = string.Format(snsopweb, strIntWageReqNoTemp);
                string strHtmlInvTemp1 = null;
                try
                {
                    strHtmlInvTemp1 = WebCl.DownloadString(strCompleteUrl1);
                }
                catch (Exception ex)
                {
                    eLog.LogError(ex);
                    LblResultA.Text = ex.ToString();
                    LblResultA.Visible = true;
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmessage.Text, true);
                    return;
                } 

                HtmlDocument htmInvTemp = new HtmlDocument();
                htmInvTemp.CreateElement("head");
                //htmInvTemp.LoadHtml(strHtmlInvTemp);
                htmInvTemp.LoadHtml(strHtmlInvTemp1);
                HtmlNode nodePnl1 = htmInvTemp.GetElementbyId("pnl1");
                strAllInvoices = strAllInvoices + nodePnl1.InnerHtml + strNewPage;
            }
            //Create document with all invoices

            //Save output to .htm file
            string strFileName = Session["username"].ToString() + "_Invoices_" + DateTime.Today.ToString("yyyyMMdd") + ".htm";
            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "text/html";
            Response.AddHeader("content-disposition", "attachment;FileName=" + strFileName);
            Response.Charset = "";
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Write(strAllInvoices);
            Response.Flush();
            Response.End();
        }
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
        rgDetails.ExportSettings.FileName = "WagePaidRequestAbstractInvoice" + DateTime.Now.ToString("yyyyMMddHHss");
        rgDetails.ExportSettings.IgnorePaging = true;
        rgDetails.ExportToCsv();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }

    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }

    void getPayams()
    {
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
            ddlBoma.Items.Insert(0, "--Select--");
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

    protected void btnexp0_Click(object sender, EventArgs e)
    {
        lblmessage.Text = "";
        string filename = "WagepaidinvoiceDetails.xlsx";
        DataSet dsAll = new DataSet();
        if (Session["user_type"].ToString().Trim() == "IP")
        {
            dsAll = Gen.getAttendanceReportNewReportMultipleWagesfromPayConnectTeamTotalExportall(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
        }
        else
        {
            dsAll = Gen.getAttendanceReportNewReportMultipleWagesfromPayConnectTeamTotalExportall(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text);
        }
        if (dsAll.Tables[0].Rows.Count > 0)
        {
            DataTable dt = new DataTable();
            dt = dsAll.Tables[0];
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.ClearContent();
            HttpContext.Current.Response.ClearHeaders();
            HttpContext.Current.Response.Buffer = true;
            HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
            HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=WagepaidinvoiceDetails.xlsx");

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
        else
        {
            lblmessage.Text = "No Data to Export";
        }
    }
}
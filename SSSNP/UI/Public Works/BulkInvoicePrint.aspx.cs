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
using System.IO;
using System.Drawing;
using Telerik.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Net;


public partial class BulkInvoicePrint : System.Web.UI.Page
{

    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    decimal TotalAmountSSP = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            hdfFlagID.Value = "0";
        }
        if (hdfFlagID.Value == "0")
        {
            FillDetails();
        }

        txtExchangerate.Enabled = false;
    }

    void FillDetails()
    {
        DataSet dsn = new DataSet();
        dsn = Gen.GetWageRequestbyIDInvoiceDetFullDetails(Request.QueryString[0].ToString().Trim());
        if (dsn.Tables[0].Rows.Count > 0)
        {
            hdfFlagID.Value = "1";
            DataSet ds = new DataSet();


            //check if it  is DIS
            //id DIS?
            if (dsn.Tables[0].Rows[0]["intWorkid"].ToString() == "999")
            {
                ds = Gen.GetworkproposalforProgressDIS(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblInvoiceNumber.Text= dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                    lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                    hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                    lblBoma.Text = "";
                    trBoma.Visible = false;
                    lblWorkStartDate.Text = "";
                    trWorkStartDate.Visible = false;
                    lblState.Text = "";
                    trState.Visible = false;
                    lblCounty.Text = "";
                    trCounty.Visible = false;
                    lblPayam.Text = "";
                    trPayam.Visible = false;
                    lblWageRequestNo.Text = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
                    txtExchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                    lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                    lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                    lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                    lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
                    lblfromdate.Text = "";
                    trFromDate.Visible = false;
                    lbltodate.Text = "";
                    trToDate.Visible = false;
                    Label4.Text = dsn.Tables[0].Rows[0]["PaidCount"].ToString();
                    Label7.Text = dsn.Tables[0].Rows[0]["UnpaidCount"].ToString();
                    Label9.Text = dsn.Tables[0].Rows[0]["TotalCount"].ToString();
                    Label11.Text = dsn.Tables[0].Rows[0]["PaidAlternateHouseholds"].ToString();
                    Label13.Text = dsn.Tables[0].Rows[0]["PaidActualHouseholds"].ToString();
                    Label27.Text = dsn.Tables[0].Rows[0]["ToalAmount"].ToString();
                    Label29.Text = dsn.Tables[0].Rows[0]["PaidAmount"].ToString();
                    lblPaidAmountDIS.Text = dsn.Tables[0].Rows[0]["PaidAmountDIS"].ToString();
                    lblPaidAmountPublicWorks.Text = dsn.Tables[0].Rows[0]["PaidAmountPublicWorks"].ToString();
                    lblPaidAmountDISSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountDISSSp"].ToString();
                    lblPaidAmountPublicWorksSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountPublicWorksSSP"].ToString();
                    Label31.Text = dsn.Tables[0].Rows[0]["UnPaidAmount"].ToString();
                    LblCoRevon.Text = dsn.Tables[0].Rows[0]["CoordinatorSenton"].ToString();
                    LblSpRevon.Text = dsn.Tables[0].Rows[0]["SpecilaistSenton"].ToString();
                    LblMaRevon.Text = dsn.Tables[0].Rows[0]["ManagerSenton"].ToString();

                    lblPaymentDate.Text = dsn.Tables[0].Rows[0]["paidDate"].ToString();
                    lblAmountpaidSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountSSP"].ToString();
                    lblPaymentCycle.Text = dsn.Tables[0].Rows[0]["WagePaymentCycle"].ToString();
                    intWagePaymentReqid.Value = dsn.Tables[0].Rows[0]["intWagePaymentReqid"].ToString().Trim();
                }
            }
            else
            {
                ds = Gen.GetworkproposalforProgress(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblInvoiceNumber.Text = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                    lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                    hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                    lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                    lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
                    lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                    lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
                    lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
                    lblWageRequestNo.Text = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
                    txtExchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                    lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                    lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                    lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                    lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
                    lblfromdate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy");
                    lbltodate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy");

                    Label4.Text = dsn.Tables[0].Rows[0]["PaidCount"].ToString();
                    Label7.Text = dsn.Tables[0].Rows[0]["UnpaidCount"].ToString();
                    Label9.Text = dsn.Tables[0].Rows[0]["TotalCount"].ToString();
                    Label11.Text = dsn.Tables[0].Rows[0]["PaidAlternateHouseholds"].ToString();
                    Label13.Text = dsn.Tables[0].Rows[0]["PaidActualHouseholds"].ToString();
                    Label27.Text = dsn.Tables[0].Rows[0]["ToalAmount"].ToString();
                    Label29.Text = dsn.Tables[0].Rows[0]["PaidAmount"].ToString();
                    lblPaidAmountDIS.Text = dsn.Tables[0].Rows[0]["PaidAmountDIS"].ToString();
                    lblPaidAmountPublicWorks.Text = dsn.Tables[0].Rows[0]["PaidAmountPublicWorks"].ToString();
                    lblPaidAmountDISSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountDISSSp"].ToString();
                    lblPaidAmountPublicWorksSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountPublicWorksSSP"].ToString();
                    Label31.Text = dsn.Tables[0].Rows[0]["UnPaidAmount"].ToString();
                    LblCoRevon.Text = dsn.Tables[0].Rows[0]["CoordinatorSenton"].ToString();
                    LblSpRevon.Text = dsn.Tables[0].Rows[0]["SpecilaistSenton"].ToString();
                    LblMaRevon.Text = dsn.Tables[0].Rows[0]["ManagerSenton"].ToString();

                    lblPaymentDate.Text = dsn.Tables[0].Rows[0]["paidDate"].ToString();
                    lblAmountpaidSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountSSP"].ToString();
                    lblPaymentCycle.Text = dsn.Tables[0].Rows[0]["WagePaymentCycle"].ToString();
                    intWagePaymentReqid.Value = dsn.Tables[0].Rows[0]["intWagePaymentReqid"].ToString().Trim();
                }
            }

            DataSet ds2 = Gen.GetWageRequestbyIDWages1(Request.QueryString[0].ToString().Trim());
            if (ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds2.Tables[0];
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HyperLink hbm = (HyperLink)e.Row.Cells[4].Controls[0];
            hbm.NavigateUrl = "frmBenInvoice.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();// +"&status=13";
            hbm.Target = "_blank";
            HyperLink hbm1 = (HyperLink)e.Row.Cells[12].Controls[0];

            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PayStatus")).Trim().ToLower() == "paid" && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PaidToType")).Trim().ToLower() == "")
            {
                hbm1.NavigateUrl = "InvoiceNotAvaialbleDet.aspx";// +"&status=13";
                hbm1.Target = "_blank";
            }
            else
            {
                hbm1.NavigateUrl = "frmBenInvoice.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();// +"&status=13";
                hbm1.Target = "_blank";
            }
        }
    }


    protected void rgDetails_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds1 = new DataSet();


        ds1 = Gen.getAttendanceCountFromTobyIDPaymentInvoiceNewOne(hdfID.Value, lblfromdate.Text, lbltodate.Text, "", "", intWagePaymentReqid.Value, Request.QueryString[0].ToString().Trim());

        if (hdfID.Value.ToString() == "999")
        {
            rgDetails.MasterTableView.GetColumn("cnt").HeaderText = "No of Days To be Paid";
        }
        DataTable dt = ds1.Tables[0];
        if (ds1.Tables[0].Rows.Count > 0)
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
            if (e.Item is GridDataItem)
            {
                int rowCounter;
                System.Web.UI.WebControls.Label lbl = e.Item.FindControl("rowNumberLabel") as System.Web.UI.WebControls.Label;
                rowCounter = rgDetails.MasterTableView.PageSize * rgDetails.MasterTableView.CurrentPageIndex;
                lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

                HyperLink hyperLinkView = e.Item.FindControl("hlHhCode") as HyperLink;
                hyperLinkView.Text = ((System.Data.DataRowView)e.Item.DataItem).Row["household_number"].ToString();
                hyperLinkView.Target = "_blank";
                hyperLinkView.NavigateUrl = "frmBenInvoice.aspx?ID=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenId"];

                HyperLink hyperLinkViewPaidPerson = e.Item.FindControl("hlPersonHHCode") as HyperLink;
                hyperLinkViewPaidPerson.Text = ((System.Data.DataRowView)e.Item.DataItem).Row["Paidto"].ToString();
                hyperLinkViewPaidPerson.Target = "_blank";
                hyperLinkViewPaidPerson.NavigateUrl = "frmBenInvoice.aspx?ID=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenId"];
            }
    }

    protected void grdDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string sen, sen1, sen2;

            sen2 = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFilePath")) + "/" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")));
            sen1 = sen2.Replace(@"\", @"/");
            sen = sen1.Replace(@"[path]", "~/");

            string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            sen = sen.Replace(sssnpdir, "~");

            HyperLink1.NavigateUrl = sen.ToString();
            HyperLink1.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName"));
            HyperLink1.Target = "_blank";
        }
    }

}

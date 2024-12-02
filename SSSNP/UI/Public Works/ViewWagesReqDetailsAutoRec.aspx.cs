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


public partial class ViewWagesReqDetailsAutoRec : System.Web.UI.Page
{


    int Totalnoofdays1 = 0;
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    decimal TotalAmountSSP = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }
        if (!IsPostBack)
        {
            hdfFlagID.Value = "0";
        }
        if (hdfFlagID.Value == "0")
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }

        txtExchangerate.Enabled = false;
    }

    void FillDetails()
    {
        #region MopUP 
        //Check if MopUp exists populating list of beneficiaries paid during this step
        //DataSet dsMopUpBeneficiaries = Gen.GetMopUpPayments(Request.QueryString[0].ToString().Trim());
        #endregion

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
                    lblTotalPaymRequested.Text = dsn.Tables[0].Rows[0]["TotalCount"].ToString();
                    Label11.Text = dsn.Tables[0].Rows[0]["PaidAlternateHouseholds"].ToString();
                    Label13.Text = dsn.Tables[0].Rows[0]["PaidActualHouseholds"].ToString();
                    Label27.Text = dsn.Tables[0].Rows[0]["ToalAmount"].ToString();
                    Label29.Text = dsn.Tables[0].Rows[0]["PaidAmount"].ToString();
                    lblPaidAmountDIS.Text = dsn.Tables[0].Rows[0]["PaidAmountDIS"].ToString();
                    lblPaidAmountPublicWorks.Text = dsn.Tables[0].Rows[0]["PaidAmountPublicWorks"].ToString();
                    lblPaidAmountDISSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountDISSSp"] != DBNull.Value ? Convert.ToDecimal(dsn.Tables[0].Rows[0]["PaidAmountDISSSp"]).ToString("f2"): "";
                    lblPaidAmountPublicWorksSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountPublicWorksSSP"] != DBNull.Value ? Convert.ToDecimal(dsn.Tables[0].Rows[0]["PaidAmountPublicWorksSSP"]).ToString("f2") : "";
                    Label31.Text = dsn.Tables[0].Rows[0]["UnPaidAmount"].ToString();
                    LblCoRevon.Text = dsn.Tables[0].Rows[0]["CoordinatorSenton"].ToString();
                    LblSpRevon.Text = dsn.Tables[0].Rows[0]["SpecilaistSenton"].ToString();
                    LblMaRevon.Text = dsn.Tables[0].Rows[0]["ManagerSenton"].ToString();

                    lblPaymentDate.Text = dsn.Tables[0].Rows[0]["paidDate"].ToString();
                    lblAmountpaidSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountSSP"] != DBNull.Value ? Convert.ToDecimal(dsn.Tables[0].Rows[0]["PaidAmountSSP"]).ToString("f2"): "";
                    lblPaymentCycle.Text = dsn.Tables[0].Rows[0]["WagePaymentCycle"].ToString();
                    intWagePaymentReqid.Value = dsn.Tables[0].Rows[0]["intWagePaymentReqid"].ToString().Trim();

                    if (Session["user_type"].ToString().Trim() == "Cordinator" || Session["user_type"].ToString().Trim() == "IP")
                    {
                        trwagerate.Visible = false;
                    }
                    else
                    {
                        trwagerate.Visible = true;
                    }
                }
            }
            else
            {
                ds = Gen.GetworkproposalforProgress(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
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
                    lblTotalPaymRequested.Text = dsn.Tables[0].Rows[0]["TotalCount"].ToString();
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

                    if (Session["user_type"].ToString().Trim() == "Cordinator" || Session["user_type"].ToString().Trim() == "IP")
                    {
                        trwagerate.Visible = false;
                    }
                    else
                    {
                        trwagerate.Visible = true;
                    }
                }
            }

            DataSet ds2 = Gen.GetWageRequestbyIDWages1(Request.QueryString[0].ToString().Trim());
            if (ds2.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = ds2.Tables[0];
                GridView1.DataBind();
                trRefDocsTitle.Visible = true;
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                trRefDocsTitle.Visible = false;
            }
        }
    }


    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

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


    protected void rg1StepPaidBeneficiaries_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds1 = new DataSet();


        ds1 = Gen.getPaymentsFirstStep(intWagePaymentReqid.Value); 

        if (hdfID.Value.ToString() == "999")
        {
            rg1StepPaidBeneficiaries.MasterTableView.GetColumn("PresentDays").HeaderText = "No of Days To be Paid";
            rg1StepPaidBeneficiaries.MasterTableView.GetColumn("AbsentDays").Visible = false;
        }
        DataTable dt = ds1.Tables[0];
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lbl1StepPaidBen.Text = dt.Rows.Count.ToString();
            lbl1StepActualPaidUSD.Text = dt.Compute("Sum(Amt)", string.Empty).ToString();
            lbl1StepPaidToBeneficiaries.Text = dt.Select("household_number=paidTo").Length.ToString();
            lbl1StepPaydToAlt.Text = dt.Select("household_number<>paidTo").Length.ToString();
            lbl1StepActualPaidSSP.Text = dt.Compute("Sum(Amt1)", string.Empty).ToString();
            lbl1StepActualPaidUSDDIS.Text = dt.Compute("Sum(Amt)", "Component=\'Component 1 (Direct Income Support)\'").ToString();
            lbl1StepActualPaidSSPDIS.Text = dt.Compute("Sum(Amt1)", "Component=\'Component 1 (Direct Income Support)\'").ToString();
            lbl1StepActualPaidSSPLIPW.Text = dt.Compute("Sum(Amt1)", "Component=\'Component 1 (Public Works)'").ToString();
            lbl1StepActualPaidUSDLIPW.Text = dt.Compute("Sum(Amt)", "Component=\'Component 1 (Public Works)'").ToString();
            rg1StepPaidBeneficiaries.DataSource = dt;
            rg1StepPaidBeneficiaries.Visible = true;
        }
        else
        {
            rg1StepPaidBeneficiaries.DataSource = null;
            rg1StepPaidBeneficiaries.Visible = true;
        }

        

    }

    protected void rg1StepPaidBeneficiaries_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
            if (e.Item is GridDataItem)
            {
                int rowCounter;
                Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
                rowCounter = rg1StepPaidBeneficiaries.MasterTableView.PageSize * rg1StepPaidBeneficiaries.MasterTableView.CurrentPageIndex;
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


    protected void rg1MopUpBeneficiaries_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds1 = new DataSet();


        ds1 = Gen.GetMopUpPayments(intWagePaymentReqid.Value);

        if (hdfID.Value.ToString() == "999")
        {
            rg1MopUpBeneficiaries.MasterTableView.GetColumn("PresentDays").HeaderText = "No of Days To be Paid";
            rg1MopUpBeneficiaries.MasterTableView.GetColumn("AbsentDays").Visible = false;
        }
        DataTable dt = ds1.Tables[0];
        if (ds1.Tables[0].Rows.Count > 0)
        {
            lblMopUpPaidBen.Text = dt.Rows.Count.ToString();
            lblMopUpActualPaidUSD.Text = dt.Compute("Sum(Amt)", string.Empty).ToString();
            lblMopUpPaidToBeneficiaries.Text = dt.Select("household_number=paidTo").Length.ToString();
            lblMopUpPaydToAlt.Text = dt.Select("household_number<>paidTo").Length.ToString();
            lblMopUpActualPaidUSDDIS.Text = dt.Compute("Sum(Amt)", "Component=\'Component 1 (Direct Income Support)\'").ToString();
            lblMopUpActualPaidUSDLIPW.Text = dt.Compute("Sum(Amt)", "Component=\'Component 1 (Public Works)'").ToString();
            lblMopUpActualPaidSSP.Text = dt.Compute("Sum(Amt1)", string.Empty).ToString();
            lblMopUpActualPaidSSPDIS.Text = dt.Compute("Sum(Amt1)", "Component=\'Component 1 (Direct Income Support)\'").ToString();
            lblMopUpActualPaidSSPLIPW.Text = dt.Compute("Sum(Amt1)", "Component=\'Component 1 (Public Works)'").ToString();
            rg1MopUpBeneficiaries.DataSource = dt;
            rg1MopUpBeneficiaries.Visible = true;
        }
        else
        {
            rg1MopUpBeneficiaries.DataSource = null;
            rg1MopUpBeneficiaries.Visible = true;
            trMopUpCountersLabel.Visible = false;
            trMopUpCounters.Visible = false;
        }
    }

    protected void rg1MopUpBeneficiaries_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
            if (e.Item is GridDataItem)
            {
                int rowCounter;
                Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
                rowCounter = rg1MopUpBeneficiaries.MasterTableView.PageSize * rg1MopUpBeneficiaries.MasterTableView.CurrentPageIndex;
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

    protected void BtnSave3_Click(object sender, EventArgs e)
    {
    }

    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
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
        sb.Append(";");
        return sb.ToString();
    }

    protected void txtAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        BtnSave2.Visible = false;
    }

    protected void grdDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string sen, sen1, sen2;

            sen2 = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFilePath")) + "/" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")));
            sen1 = sen2.Replace(@"\", @"/");
            sen = sen1.Replace(@"", "~/"); 

            string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            sen = sen.Replace(sssnpdir, "~");

            HyperLink1.NavigateUrl = sen.ToString();
            HyperLink1.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName"));
            HyperLink1.Target = "_blank";
        }


    }

}

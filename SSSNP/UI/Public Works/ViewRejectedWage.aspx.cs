using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
using System.Drawing;


public partial class ViewRejectedWage : System.Web.UI.Page
{
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    string strWageRequestStatus;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }
        if (!IsPostBack)
        {
            
        }
        Failure.Visible = false;
        success.Visible = false;
        FillDetails();
    }

    void FillDetails()
    {

        DataSet dsn = new DataSet();
        dsn = Gen.GetWageRequestbyID(Request.QueryString[0].ToString().Trim());

        if (dsn.Tables[0].Rows.Count > 0)
        {
            strWageRequestStatus = dsn.Tables[0].Rows[0]["Status"].ToString();
           

            //id DIS?
            if (dsn.Tables[0].Rows[0]["intWorkid"].ToString() == "999")
            {
                //DIS
                DataSet ds = new DataSet();
                ds = Gen.GetworkproposalforProgressDIS(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    DataSet dsDISLocation = Gen.getWageReqLocationDIS(Request.QueryString[0].ToString().Trim());

                    decimal usdRate = Convert.ToDecimal(dsn.Tables[0].Rows[0]["WageRateUSD"].ToString());
                    decimal exRate = Convert.ToDecimal(dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString());
                    decimal result = usdRate * exRate;

                    lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                    lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                    lblWorkStartDate.Text = "";
                    trWorkStartDate.Visible = false;
                    lblState.Text = dsDISLocation.Tables[0].Rows[0]["StateName"].ToString(); ;
                    trState.Visible = true;
                    lblCounty.Text = dsDISLocation.Tables[0].Rows[0]["CountieName"].ToString();
                    trCounty.Visible = true;
                    lblPayam.Text = dsDISLocation.Tables[0].Rows[0]["PayamName"].ToString(); ;
                    trPayam.Visible = true;
                    lblBoma.Text = dsDISLocation.Tables[0].Rows[0]["BomaName"].ToString(); ;
                    trBoma.Visible = true;
                    lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
                    lblexchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                    lblAmountInSSP.Text = result.ToString("N2");  //dsn.Tables[0].Rows[0]["AmountInSSP"].ToString();
                    lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                    lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                    lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                    lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
                    lblfromdate.Text = "";
                    trFromDate.Visible = false;
                    lbltodate.Text = "";
                    trToDate.Visible = false;
                    lblRequestNumber.Text = dsn.Tables[0].Rows[0]["ReqNo"].ToString();

                    if (Session["user_type"].ToString().Trim() == "Cordinator" || Session["user_type"].ToString().Trim() == "IP")
                    {
                        trwagerate.Visible = false;
                        trAmountSSP.Visible = false;
                        rgDetails.Columns.FindByUniqueName("ExchangeRateNew").Visible = false;
                        rgDetails.Columns.FindByUniqueName("AmountInSSP").Visible = false;
                    }
                    else if (Session["user_type"].ToString().Trim() == "Specialist" && (dsn.Tables[0].Rows[0]["Status"].ToString() == "1" || dsn.Tables[0].Rows[0]["Status"].ToString() == "2"))
                    {
                        trwagerate.Visible = false;
                        trAmountSSP.Visible = false;
                        rgDetails.Columns.FindByUniqueName("ExchangeRateNew").Visible = false;
                        rgDetails.Columns.FindByUniqueName("AmountInSSP").Visible = false;
                    }
                    else
                    {
                        trwagerate.Visible = true;
                        trAmountSSP.Visible = true;
                        rgDetails.Columns.FindByUniqueName("ExchangeRateNew").Visible = true;
                        rgDetails.Columns.FindByUniqueName("AmountInSSP").Visible = true;
                    }
                }
            }
            else
            {
                DataSet ds = new DataSet();
                ds = Gen.GetworkproposalforProgress(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    decimal usdRate = Convert.ToDecimal(dsn.Tables[0].Rows[0]["WageRateUSD"].ToString());
                    decimal exRate = Convert.ToDecimal(dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString());
                    decimal result = usdRate * exRate;

                    lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                    lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                    lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                    lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
                    lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                    lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
                    lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
                    lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
                    lblexchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                    lblAmountInSSP.Text = result.ToString("N2");  //dsn.Tables[0].Rows[0]["AmountInSSP"].ToString();
                    lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                    lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                    lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                    lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
                    lblfromdate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy");
                    lbltodate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy");

                    if (Session["user_type"].ToString().Trim() == "Cordinator" || Session["user_type"].ToString().Trim() == "IP")
                    {
                        trwagerate.Visible = false;
                        trAmountSSP.Visible = false;
                        rgDetails.Columns.FindByUniqueName("ExchangeRateNew").Visible = false;
                        rgDetails.Columns.FindByUniqueName("AmountInSSP").Visible = false;
                    }
                    else if (Session["user_type"].ToString().Trim() == "Specialist" && (dsn.Tables[0].Rows[0]["Status"].ToString() == "1" || dsn.Tables[0].Rows[0]["Status"].ToString() == "2"))
                    {
                        trwagerate.Visible = false;
                        trAmountSSP.Visible = false;
                        rgDetails.Columns.FindByUniqueName("ExchangeRateNew").Visible = false;
                        rgDetails.Columns.FindByUniqueName("AmountInSSP").Visible = false;
                    }
                    else
                    {
                        trwagerate.Visible = true;
                        trAmountSSP.Visible = true;
                        rgDetails.Columns.FindByUniqueName("ExchangeRateNew").Visible = true;
                        rgDetails.Columns.FindByUniqueName("AmountInSSP").Visible = true;
                    }
                }
            }

            DataSet ds1 = new DataSet();
            ds1 = Gen.getAttendanceCountFromTobyID(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim(), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy"), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy"), "", "", Request.QueryString[0].ToString().Trim());

            int iBenCount = 0;
            int iPaidCount = 0;
            int iUnpaidCount = 0;

            iBenCount = ds1.Tables[0].Rows.Count;

            iPaidCount = ds1.Tables[0].Select("PaymentStatus='Paid'").Length;
            iUnpaidCount = ds1.Tables[0].Select("PaymentStatus='Unpaid'").Length;
           
            lblBenfCountValue.Text = iBenCount.ToString();
            lblPaidCountValue.Text = iPaidCount.ToString();
            lblUnpaidCountValue.Text = iUnpaidCount.ToString();

            if (strWageRequestStatus == "0")
            {
                lblpayingagent.Visible = false;
                lblcoordinator.Visible = false;
                lblspecialist.Visible = false;
                lblmanager.Visible = false;
                ddlPayingAgent.Visible = true;
                ddlCoordinator.Visible = true;
                ddlSpecialist.Visible = true;
                ddlManager.Visible = true;

                if (!IsPostBack)
                {
                    getOfficers();
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

    protected void rbBenfFilterCheckChanged(Object sender, EventArgs e)
    {
        rgDetails.Rebind();
    }

    protected void rgDetails_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet dsn = new DataSet();
        dsn = Gen.GetWageRequestbyID(Request.QueryString[0].ToString().Trim());

        DataSet ds = Gen.getAttendanceCountFromTobyID(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim(), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy"), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy"), "", "", Request.QueryString[0].ToString().Trim());
        DataTable dt = ds.Tables[0];
        DataTable dtFiltered = new DataTable();

        rgDetails.DataSource = dtFiltered;
        if (dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim() == "999")
        {
            rgDetails.MasterTableView.GetColumn("cnt").HeaderText = "No of Days To be Paid";
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
        }

        if (e.Item is GridPagerItem)
        {
            int iTotalRows = ((System.Data.DataTable)rgDetails.DataSource).Rows.Count;
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
            PageSizeCombo.Items.Clear();
            PageSizeCombo.Items.Add(new RadComboBoxItem("250", "250"));
            PageSizeCombo.FindItemByText("250").Attributes.Add("ownerTableViewId", rgDetails.MasterTableView.ClientID);
            //}
            if (iTotalRows > 500)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("500", "500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgDetails.MasterTableView.ClientID);
            }

            if (iTotalRows > 1000)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000", "1000"));
                PageSizeCombo.FindItemByText("100").Attributes.Add("ownerTableViewId", rgDetails.MasterTableView.ClientID);
            }

            PageSizeCombo.Items.Add(new RadComboBoxItem("All", iTotalRows.ToString()));
            PageSizeCombo.FindItemByText("All").Attributes.Add("ownerTableViewId", rgDetails.MasterTableView.ClientID);
            if (e.Item.OwnerTableView.PageSize.ToString() == iTotalRows.ToString())
            {
                PageSizeCombo.FindItemByText("All").Selected = true;
            }
            else
            {
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }

    protected void rgDetails_ItemCommand(object sender, GridCommandEventArgs e)
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

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            //Query to delete
            int iImageId = (int)GridView1.DataKeys[e.RowIndex].Value;
            Gen.DeleteWageRequestAttachment(iImageId);
            FillDetails();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string sen, sen1, sen2;

            sen2 = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFilePath")) + "/" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")));
            sen1 = sen2.Replace(@"\", @"/");
            string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            sen = sen1.Replace(sssnpdir, "~/");
            sen = sen.Replace(sssnpdir, "~");

            HyperLink1.NavigateUrl = sen.ToString();
            HyperLink1.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName"));
            HyperLink1.Target = "_blank";

            int iWageRequestStatus = System.Convert.ToInt32(strWageRequestStatus);

            //Disable deletion if user <> uploadedBy
            string strUploadedBy = DataBinder.Eval(e.Row.DataItem, "UploadedBy").ToString();
            if (
                strUploadedBy != Session["username"].ToString().Trim()
                || (iWageRequestStatus > 4 && !(Session["user_type"].ToString().Trim() == "Specialist" && iWageRequestStatus == 102))
                || (Session["user_type"].ToString().Trim() == "IP" && iWageRequestStatus > 1 && iWageRequestStatus < 100)
                || (Session["user_type"].ToString().Trim() == "Cordinator" && iWageRequestStatus > 2 && iWageRequestStatus < 100)
                || (Session["user_type"].ToString().Trim() == "Specialist" && iWageRequestStatus > 3 && iWageRequestStatus < 100)
                )
            {
                LinkButton btnDelete = (LinkButton)e.Row.Cells[0].Controls[0];
                btnDelete.Enabled = false;
                btnDelete.Text = "";
            }
        }
    }

    #region Draft Status management (selection of approvers and approval button)

    //Get officers list
    public void getOfficers()
    {
        DataSet dsoff = new DataSet();
        dsoff = Gen.getOfficers("%");
        if (dsoff.Tables[0].Rows.Count > 0)
        {
            ddlPayingAgent.DataSource = dsoff.Tables[0];
            ddlPayingAgent.DataTextField = "User_name";
            ddlPayingAgent.DataValueField = "intUserid";
            ddlPayingAgent.DataBind();
        }
        if (dsoff.Tables[1].Rows.Count > 0)
        {
            ddlCoordinator.DataSource = dsoff.Tables[1];
            ddlCoordinator.DataTextField = "User_name";
            ddlCoordinator.DataValueField = "intUserid";
            ddlCoordinator.DataBind();
        }
        if (dsoff.Tables[2].Rows.Count > 0)
        {
            ddlSpecialist.DataSource = dsoff.Tables[2];
            ddlSpecialist.DataTextField = "User_name";
            ddlSpecialist.DataValueField = "intUserid";
            ddlSpecialist.DataBind();
        }
        if (dsoff.Tables[3].Rows.Count > 0)
        {
            ddlManager.DataSource = dsoff.Tables[3];
            ddlManager.DataTextField = "User_name";
            ddlManager.DataValueField = "intUserid";
            ddlManager.DataBind();
        }
    }

    protected void BtnDelete_Click(object sender, EventArgs e)
    {
                int a = Gen.DeleteWageRequestDraft(Request.QueryString[0].ToString().Trim(), Session["uid"].ToString().Trim());

        if (a > 0)
        {
            //lblresult.Text = "";
            lblmsg.Text = "";
            lblmsg.ForeColor = Color.Orange;
            success.Visible = true;
            lblmsg.Text = "Wage Request Successfully Deleted";
            lblmsg.Visible = true;
            btnDelete.Visible = false;
            ddlPayingAgent.Visible = false;
            ddlSpecialist.Visible = false;
            ddlCoordinator.Visible = false;
            ddlManager.Visible = false;
            lblpayingagent.Text = "";
            lblpayingagent.Visible = false;
            lblcoordinator.Text = "";
            lblcoordinator.Visible = false;
            lblspecialist.Text = "";
            lblspecialist.Visible = false;
            lblmanager.Text = "";
            lblmanager.Visible = false;

            string status = "";
            status = "The above Wage Request has been deleted from the system.";
            //lblmsg.Text = status;
        }
        else
        {
            lblmsg0.Text = "You cannot delete the Wage Request";
        }
    }



    
    #endregion
}

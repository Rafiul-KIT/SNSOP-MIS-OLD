#region Using/Inherits Classess
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Net.Mail;
using Telerik.Web.UI;
#endregion
#region Amendment History Begin
//Form Name : Rejected wage reuest update
//Purpose : To Add Rejected wage reuest update
//Tables used : tr_BenAttendanceDet,td_WorkWageRequest,td_WageReqBenDet,tbl_WorkMaster,tbl_BeneficiaryDet,tr_BenfWorkDet
//Stored procedures Used : getOfficers,GetworkproposalforProgress,getAttendanceCountFromToWagesNewWithAmount,getCheckingWagesApplyrestrictionbyIDNewModel,insertWageRequest,InsUpdBeneficiaryWagesRequestAlternateNewModel,insertWageRequestUpdatesStatusRemoveData
#endregion Amendment History End

public partial class UpdationIPBenfWages : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    int Totalnoofdays1 = 0;
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    decimal TotalAmountSSP = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    DataRow dtrdr;
    DataTable myDtNewRecdr = new DataTable();
    DataTable myDtNewRecdr1 = new DataTable();
    public DataTable dtMyTable1;
    public DataTable dtMyTable2;
    public DataTable dtMyTableCertificate;
    public DataTable dtMyTablepr;
    public DataTable dtMyTableCertificate2;
    decimal dAmtTotal;
    int iNumDaysPresentTotal;
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        #region Is not Postback Statement
        if (!IsPostBack)
        {
            Session["PhotoFilePath"] = "";
            Session["PhotoFileName"] = "";
            hdfFlagID.Value = "0";
            dtMyTableCertificate = createtablecrtificate2();
            Session["CertificateTb1"] = dtMyTableCertificate;
        }
        #endregion
        // Upload Documents
        UploadFile1();
        #region fill details Calling
        if (hdfFlagID.Value == "0")
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }
        txtExchangerate.Enabled = false;
        #endregion
    }
    #endregion

    protected void rgAttendances_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = new DataSet();
        if (hdfID.Value.ToString() == "999")
        {
            ds = Gen.getAttendanceCountFromToWagesDIS(Request.QueryString[0].ToString().Trim());
            rgAttendances.MasterTableView.GetColumn("cnt").HeaderText = "No of Days To be Paid";
        }
        else
        {
            ds = Gen.getAttendanceCountFromToWagesRejected(hdfID.Value.ToString(), lblfromdate.Text, lbltodate.Text, "", "");
        }

        DataTable dt = ds.Tables[0];
        rgAttendances.DataSource = dt;

        if (rgAttendances.DataSource == null)
        {
            rgAttendances.Visible = false;
        }
        else
        {
            rgAttendances.Visible = true;
        }

        for (int iIndex = 0; iIndex < dt.Rows.Count; iIndex++)
        {
            DataRow drTemp = dt.Rows[iIndex];
            decimal dAmtTemp;
            int iNumDaysPresentTemp;
            //dAmtTemp = Convert.ToDecimal(drTemp["Amt"]);
            iNumDaysPresentTemp = Convert.ToInt32(drTemp["cnt"]);
            iNumDaysPresentTotal = iNumDaysPresentTotal + iNumDaysPresentTemp;
            dAmtTemp = iNumDaysPresentTemp * Convert.ToDecimal(lblwagerate.Text);
            dAmtTotal = dAmtTotal + dAmtTemp;
        }

    }

    protected void rgAttendances_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = rgAttendances.MasterTableView.PageSize * rgAttendances.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            decimal dAmountUSD;
            int iNumDaysPresent;

            Label cellAmountUSD = e.Item.FindControl("AmtLabel") as Label;
            GridDataItem diDaysPresent = e.Item as GridDataItem;
            iNumDaysPresent = Convert.ToInt32(diDaysPresent["cnt"].Text);

            dAmountUSD = iNumDaysPresent * Convert.ToDecimal(lblwagerate.Text);
            cellAmountUSD.Text = dAmountUSD.ToString();
        }
        if (e.Item is GridFooterItem)
        {
            GridFooterItem AmtFooter = (GridFooterItem)e.Item;
            //(AmtFooter["Amt"].FindControl("txtAmtTotal") as TextBox).Text = dAmtTotal.ToString();
            var cntFooterAmt = (e.Item as GridFooterItem)["Amt"];
            var cntFooterCnt = (e.Item as GridFooterItem)["cnt"];
            cntFooterAmt.Text = dAmtTotal.ToString();
            cntFooterCnt.Text = iNumDaysPresentTotal.ToString();
        }
        if (e.Item is GridPagerItem)
        {
            int iTotalRows = ((System.Data.DataTable)rgAttendances.DataSource).Rows.Count;
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
            PageSizeCombo.Items.Clear();
            //if (iTotalRows > 250)
            //{
            PageSizeCombo.Items.Add(new RadComboBoxItem("250", "250"));
            PageSizeCombo.FindItemByText("250").Attributes.Add("ownerTableViewId", rgAttendances.MasterTableView.ClientID);
            //}
            if (iTotalRows > 500)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("500", "500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgAttendances.MasterTableView.ClientID);
            }

            if (iTotalRows > 1000)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000", "1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgAttendances.MasterTableView.ClientID);
            }

            PageSizeCombo.Items.Add(new RadComboBoxItem("All", iTotalRows.ToString()));
            PageSizeCombo.FindItemByText("All").Attributes.Add("ownerTableViewId", rgAttendances.MasterTableView.ClientID);
            //if (!IsPostBack)
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

    protected void rgAttendances_ItemCommand(object sender, GridCommandEventArgs e)
    {
    }

    #region Fill Details function in fields 
    void FillDetails()
    {
        DataSet dsn = new DataSet();
        dsn = Gen.GetWageRequestbyID(Request.QueryString[0].ToString().Trim());

        if (dsn.Tables[0].Rows.Count > 0)
        {
            //id DIS?
            if (dsn.Tables[0].Rows[0]["intWorkid"].ToString() == "999")
            {
                //DIS
                hdfFlagID.Value = "1";
                DataSet ds = new DataSet();

                ds = Gen.GetworkproposalforProgressDIS(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfWorkid.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                    lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                    lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                    hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                    //lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                    lblBoma.Visible = false;
                    //lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
                    lblWorkStartDate.Visible = false;
                    trApprovedDate.Visible = false;
                    //lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                    lblState.Visible = false;
                    //lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
                    lblCounty.Visible = false;
                    //lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
                    lblPayam.Visible = false;
                    lblWageRequestNo.Text = "Wage Request No :" + dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
                    txtExchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                    lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                    lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                    lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString(); 
                    lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
                    lblfromdate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy");
                    lbltodate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy");
                    trFromDate.Visible = false;
                    trToDate.Visible = false;
                    lblfromdate.Visible = false;
                    lbltodate.Visible = false;
                    hdfIP.Value = ds.Tables[0].Rows[0]["IPid"].ToString();
                    HdfReqNo.Value = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblPaymentCycle.Text = dsn.Tables[0].Rows[0]["PaymentCycle"].ToString();
                    trState.Visible = false;
                    trCounty.Visible = false;
                    trPayam.Visible = false;
                    trBoma.Visible = false;
                    trPayingAgent.Visible = false;
                }
            }
            else
            {
                hdfFlagID.Value = "1";
                DataSet ds = new DataSet();

                ds = Gen.GetworkproposalforProgress(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

                if (ds.Tables[0].Rows.Count > 0)
                {
                    hdfWorkid.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                    lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                    lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                    hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                    lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                    lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
                    lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                    lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
                    lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
                    lblWageRequestNo.Text = "Wage Request No :" + dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
                    txtExchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                    lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                    lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                    lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                    lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
                    lblfromdate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy");
                    lbltodate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy");
                    hdfIP.Value = ds.Tables[0].Rows[0]["IPid"].ToString();
                    HdfReqNo.Value = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                    lblPaymentCycle.Text = dsn.Tables[0].Rows[0]["PaymentCycle"].ToString();
                }
            }

            DataSet ds1a = new DataSet();

            //ds1a = Gen.getAttendanceCountFromToWagesRejected(hdfID.Value.ToString(), lblfromdate.Text, lbltodate.Text, "", "");
            ds1a = Gen.getAttendanceCountFromToWagesRejected(hdfID.Value.ToString(), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy"), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy"), "", "");
            rgAttendances.Rebind();


            if (ds1a.Tables[0].Rows.Count > 0)
            {
                //grdDetails.DataSource = ds1a.Tables[0];
                //grdDetails.DataBind();
                rgAttendances.Rebind();
            }
            else
            {
                //grdDetails.DataSource = null;
                //grdDetails.DataBind();
                rgAttendances.DataSource = null;
                rgAttendances.Rebind();
            }


        }

    }
    #endregion

    #region Button Click functionalities
    protected void BtnSave_ClickRadGrid(object sender, EventArgs e)
    {
        int count = 0;

        foreach (GridDataItem dataItem in rgAttendances.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                count = count + 1;
                int iRowIndex = dataItem.ClientRowIndex;
                //rgAttendances.MasterTableView[iRowIndex]     ["TotAttend"]
                if (dataItem["cnt"].Text == "0")
                {
                    lblmsg0.Text = dataItem["NameofHousehold"].Text + " Beneficiary should have attendance";
                    Failure.Visible = true;
                    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
                    return;
                }
            }
        }
        if (((DataTable)Session["CertificateTb1"]).Rows.Count == 0)
        {
            lblmsg0.Text = "Please Upload at least one document";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        if (count == 0)
        {
            lblmsg0.Text = "At lease one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }


        foreach (GridDataItem dataItem in rgAttendances.MasterTableView.Items)
        {
            string strIntBenId = dataItem.GetDataKeyValue("intBenId").ToString();

            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                DataSet dscheck = Gen.getRestrictionofWageApplybyBenficiaryIDatUpdate(hdfWorkid.Value, lblfromdate.Text, lbltodate.Text, strIntBenId, Request.QueryString[0].ToString().Trim());
                if (dscheck.Tables[0].Rows.Count > 0)
                {
                    BtnSave1.Visible = false;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:none");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");
                    //grdDetails.DataSource = null;
                    //grdDetails.DataBind();
                    rgAttendances.DataSource = null;
                    rgAttendances.Rebind();

                    GridView1.DataSource = null;
                    GridView1.DataBind();
                    BtnSave1.Visible = true;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:block");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:block");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:block");
                    Failure.Visible = true;
                    lblmsg0.Text = "You have already applied for this beneficiary between these days - " + dataItem["EnrollmentNo"].Text;
                    return;
                }

                if (dscheck.Tables[1].Rows.Count > 0)
                {
                    BtnSave1.Visible = false;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:none");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");
                    BtnSave1.Visible = true;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:block");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:block");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:block");
                    Failure.Visible = true;
                    lblmsg0.Text = "You have already applied for this benficiary between/within these days - " + dataItem["EnrollmentNo"].Text;
                    return;
                }
            }
        }

        if (txtExchangerate.Text == "" || txtExchangerate.Text == "0" || txtExchangerate.Text == "0.00")
        {
            //txtExchangerate.Text = "51.40";
            txtExchangerate.Text = "---";
        }
        if (((DataTable)Session["CertificateTb1"]).Rows.Count > 0)
        {
            for (int n = 0; n < ((DataTable)Session["CertificateTb1"]).Rows.Count; n++)
            {
                if (((DataTable)Session["CertificateTb1"]).Rows[n]["intEmpEarningid1"].ToString().Trim() == "0")
                {
                    ((DataTable)Session["CertificateTb1"]).Rows[n]["intWagePaymentReqid"] = Request.QueryString[0].ToString().Trim();
                }
            }
            GetNewRectoInsertdr2();
            int statuspr = Gen.bulkInsertdrInterDetails(myDtNewRecdr1);
        }

        int resultwage = Gen.DeleteExistingWageRequest(Request.QueryString[0].ToString().Trim(), Session["uid"].ToString().Trim());

        foreach (GridDataItem dataItem in rgAttendances.MasterTableView.Items)
        {
            string strHdfBenid = dataItem.GetDataKeyValue("intBenId").ToString();
            RadDropDownList ddlAltChoice = (RadDropDownList)dataItem.FindControl("rgDdlAltChoice");
            string strAltChoiceValue = ddlAltChoice.SelectedValue;

            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                if (txtExchangerate.Text != "" && dataItem["Amt"].Text != "") //Amount paid in USD
                {
                    string chkalternte = "";
                    if (ddlAltChoice.SelectedValue != "0")
                    {
                        chkalternte = ddlAltChoice.SelectedValue;
                    }
                    int s = Gen.InsUpdBeneficiaryWagesRequestAlternate(strHdfBenid, dataItem["Amt"].Text, Session["uid"].ToString(), lblwagerate.Text, hdfID.Value.ToString(), Request.QueryString[0].ToString().Trim(), dataItem["cnt"].Text, chkalternte);

                    if (s == 999)
                    {
                        lblmsg0.Text = "Already Exist. ";
                        success.Visible = false;
                        Failure.Visible = true;
                    }
                }
            }
        }

        int aa = Gen.InsUpdBeneficiaryWagesRequestUpdateAttendancebyID(Request.QueryString[0].ToString().Trim(), hdfID.Value.ToString(), lblfromdate.Text, lbltodate.Text);
        lblmsg.Text = "Wage Request for Payment Submited Successfully..!";
        success.Visible = true;
        Failure.Visible = false;

        string message = "Wage Request for Payment Submited Successfully..!";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("alert('");
        sb.Append(message);
        sb.Append("');");
        ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
        Response.Redirect("IPWagesDasboardDrildownUpdate.aspx?Status=30");
    }


    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        // Response.Redirect("WageIPProcessDashboard.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        try
        {
            if (Session["PhotoFilePath"].ToString() != null && Session["PhotoFilePath"].ToString() != "")
            {
                if (GridView1.Rows.Count == 0)
                {
                    Session["dtMyTable2"] = createtablecrtificate2();
                    Session["CertificateTb1"] = ((DataTable)Session["dtMyTable2"]);
                }
                DataTable dupTradeDataTable2;
                DataView dv = new DataView((DataTable)Session["CertificateTb1"]);
                dupTradeDataTable2 = dv.ToTable();

                if (dupTradeDataTable2.Rows.Count > 0)
                {
                    dupTradeDataTable2 = dv.ToTable();
                }

                AddDataToTableCeertificate1("", txtDescription.Text, ((DataTable)Session["CertificateTb1"]), Session["PhotoFileName"].ToString(), Session["PhotoFilePath"].ToString(), Session["uid"].ToString());

                this.GridView1.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
                this.GridView1.DataBind();

                lblFileName.Text = "";
                clear1();
                lblresult.Text = "";
            }
            else
            {
                lblresult.Text = "Please Upload Documnet";
            }
        }
        catch (Exception ee)
        {
        }
        GridView1.Visible = true;
    }

    protected void BtnSave3_Click(object sender, EventArgs e)
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
        //sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }

    #endregion

    #region Mail Send Functionalities
    public void SendMailforWages(string numbers, string anothermail, string Ipname, string WorkCode, string WorkRequest)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);
        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        // mail.To.Add(to);
        string anotherm = to + "";
        string anothermail1 = ",";

        if (hdfIP.Value == "1001")
        {
            anothermail1 = anotherm + ",";
        }
        else if (hdfIP.Value == "1005")
        {
            anothermail1 = anotherm + ",";
        }
        else
        {
            anothermail1 = anotherm;
        }
        string[] multi = anothermail1.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }
        mail.From = new MailAddress(from, ":: SSSNP Public works MIS :: Wages", System.Text.Encoding.UTF8);
        mail.Subject = "SSSNP Public Works MIS – Notification on Wage Payroll for Review";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<H2>Notification on Wage Payroll for Review</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + WorkCode + "<br> <br> Work Request No: " + WorkRequest + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
        }
    }
    #endregion

    #region GridView Events Function
    //protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {
    //        HiddenField HdfReqID = (HiddenField)e.Row.Cells[1].FindControl("HdfReqID");
    //        HiddenField hdfRegBenid = (HiddenField)e.Row.Cells[1].FindControl("hdfRegBenid");
    //        HiddenField HdfBenid = (HiddenField)e.Row.Cells[1].FindControl("HdfBenid");
    //        CheckBox CheckBox1 = (CheckBox)e.Row.Cells[1].FindControl("CheckBox1");
    //        HdfBenid.Value = DataBinder.Eval(e.Row.DataItem, "intBenid").ToString();
    //        DropDownList ddlalternate = (DropDownList)e.Row.Cells[0].FindControl("ddlalternate");
    //        ddlalternate.Items.Clear();
    //        ddlalternate.Items.Insert(0, "If any");
    //        e.Row.Cells[6].Text = "0";
    //        HdfReqID.Value = "0";
    //        e.Row.Cells[6].Text = Convert.ToString(Convert.ToDecimal(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"))) * Convert.ToDecimal(lblwagerate.Text));
    //        //e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text));
    //        //   txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text.Trim()));
    //        decimal TotalAmountGrd = (Convert.ToDecimal(e.Row.Cells[6].Text));
    //        TotalAmountGrd1 = TotalAmountGrd + TotalAmountGrd1;
    //        int Totalnoofdays = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"));
    //        Totalnoofdays1 = Totalnoofdays + Totalnoofdays1;
    //        decimal TotalAmount1 = Convert.ToDecimal(e.Row.Cells[6].Text);
    //        TotalAmount = TotalAmount + TotalAmount1;
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[3].Text = "Total";
    //        e.Row.Cells[5].Text = Totalnoofdays1.ToString();
    //        e.Row.Cells[6].Text = TotalAmountGrd1.ToString();
    //    }
    //}

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ((DataTable)Session["CertificateTb1"]).Rows.RemoveAt(e.RowIndex);
            this.GridView1.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
            this.GridView1.DataBind();
        }
        catch (Exception ex)
        {
            //  lblresult.Text = "Please enter correct data";// ex.ToString();
        }
        finally
        {
        }
    }
    #endregion

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void txtAmount_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
    }

    protected void txtExchangerate_TextChanged(object sender, EventArgs e)
    {
    }
    #endregion

    #region Bulk Insert Functionalities
    protected void GetNewRectoInsertdr2()
    {
        myDtNewRecdr1 = (DataTable)Session["CertificateTb1"];
        DataView dvdr1 = new DataView(myDtNewRecdr1);
        //dvdr.RowFilter = "new = 0";
        myDtNewRecdr1 = dvdr1.ToTable();
    }

    protected DataTable createtablecrtificate2()
    {
        dtMyTable2 = new DataTable("CertificateTb1");
        dtMyTable2.Columns.Add("new", typeof(string));
        dtMyTable2.Columns.Add("intWagePaymentReqid", typeof(string));
        dtMyTable2.Columns.Add("Description", typeof(string));
        dtMyTable2.Columns.Add("PhotoFileName", typeof(string));
        dtMyTable2.Columns.Add("PhotoFilePath", typeof(string));
        dtMyTable2.Columns.Add("Created_by", typeof(string));
        dtMyTable2.Columns.Add("intEmpEarningid1", typeof(string));

        return dtMyTable2;
    }

    void clear1()
    {
        txtDescription.Text = "";
        Session["PhotoFilePath"] = "";
        Session["PhotoFileName"] = "";
    }

    private void AddDataToTableCeertificate1(string intWagePaymentReqid, string Description, DataTable myTable2,
      string PhotoFileName, string PhotoFilePath, string Created_by)
    {
        try
        {
            DataRow Row;
            Row = myTable2.NewRow();
            dtMyTable2 = new DataTable("CertificateTb1");
            Row["intWagePaymentReqid"] = intWagePaymentReqid;
            Row["Description"] = Description;
            Row["PhotoFileName"] = PhotoFileName;
            Row["PhotoFilePath"] = PhotoFilePath;
            Row["Created_by"] = Session["uid"].ToString().Trim();
            Row["intEmpEarningid1"] = "0";
            myTable2.Rows.Add(Row);
        }
        catch (Exception ex)
        {
            //  ((DataTable)Session["myDatatable"]).Rows.Clear();
            //  Response.Redirect("~/EmpFacility.aspx");
            throw ex;
        }
    }
    #endregion

    #region Doc or Image Binding
    public void UploadFile1()
    {
        string LeaseFilePath = "";
        string LeaseFileName = "";

        string sFileDir6 = Server.MapPath("~\\WageFile");
        if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload1.PostedFile))
        {
            string sFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            try
            {
                string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
                int i = fileType.Length;
                if (fileType[i - 1].ToUpper().Trim() == "PDF" || fileType[i - 1].ToUpper().Trim() == "DOC" || fileType[i - 1].ToUpper().Trim() == "DOCX" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG" || fileType[i - 1].ToUpper().Trim() == "PNG")
                {
                    string newPath = "";
                    if (Session.Count > 0)
                    {
                        newPath = System.IO.Path.Combine(sFileDir6, Session["uid"].ToString());
                    }
                    else
                    {
                        newPath = System.IO.Path.Combine(sFileDir6, "1000");
                    }

                    newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                    LeaseFilePath = newPath.ToString();
                    LeaseFileName = sFileName;
                    // Create the subfolder
                    if (!Directory.Exists(newPath))
                        System.IO.Directory.CreateDirectory(newPath);
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                    int count = dir.GetFiles().Length;
                    if (count == 0)
                        FileUpload1.PostedFile.SaveAs(newPath + "\\" + sFileName);

                    else
                    {
                        if (count == 1)
                        {
                            string[] Files = Directory.GetFiles(newPath);
                            FileUpload1.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        }
                    }
                    Session["PhotoFilePath"] = LeaseFilePath;
                    Session["PhotoFileName"] = LeaseFileName;
                    lblFileName.Text = Session["PhotoFileName"].ToString();
                }
                else
                {
                    //  lblresult.Text = "<font color='red'>Upload PDF,Doc files only..!</font>";
                    return;
                    //  Response.Write("<script>alert('Upload PDF,Doc files only  ')</script> "); //+ fileType[1].Trim(); 
                }
            }
            catch (Exception)//in case of an error
            {
                DeleteFile(LeaseFilePath + "\\" + LeaseFileName);
                // DeleteFile(sFileDir + sFileName);
            }
        }
    }


    public void DeleteFile(string strFileName)
    {//Delete file from the server
        if (strFileName.Trim().Length > 0)
        {
            FileInfo fi = new FileInfo(strFileName);
            if (fi.Exists)//if file exists delete it
            {
                fi.Delete();
            }
        }
    }
    #endregion
}
#region Using/Inherits Classess
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.IO;
using Telerik.Web.UI;
using SSSNP_SNSOP.Entity;
using System.Collections.Generic;
using SSSNP_SNSOP.Core;
using System.Web;
#endregion

#region Amendment History Begin
//Form Name : Apply for Wage Reuest
//Purpose : To Add Wage Reuest
//Tables used : tr_BenAttendanceDet,td_WorkWageRequest,td_WageReqBenDet,tbl_WorkMaster,tbl_BeneficiaryDet,tr_BenfWorkDet
//Stored procedures Used : getOfficers,GetworkproposalforProgress,getAttendanceCountFromToWagesNewWithAmount,getCheckingWagesApplyrestrictionbyIDNewModel,insertWageRequest,InsUpdBeneficiaryWagesRequestAlternateNewModel,insertWageRequestUpdatesStatusRemoveData
#endregion Amendment History End


public partial class PDCHouseholdWagesApplyDIS : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    int Totalnoofdays1 = 0;
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
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
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 500;

        // If Session cleared, Redirect to Login page
        if (Session.Count == 0)
        {
            Response.Redirect("~/frmuserlogin.aspx");
        }
        // Look up Click Function Intialization

        if (CUser.Current.IsIPRole || CUser.Current.IsCoordinatorRole)
        {
            lblExchangeRate.Visible = false;
            txtExchangerate.Visible = false;

        }
        else
        {
            lblExchangeRate.Visible = true;
            txtExchangerate.Visible = true;
        }

        // Button Click Disable wait and Validation group
        this.btnSearchBeneficiaries.Attributes.Add("onclick", DisableTheButton(this.Page, this.btnSearchBeneficiaries, "group"));
        //this.BtnSave1.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave1, "group"));

        #region Is not Postback Statement
        if (!IsPostBack)
        {
            DateTime dtNow = DateTime.Today;
            int iYear = dtNow.Year;
            int iMonth = dtNow.Month;
            ddlYear.SelectedValue = iYear.ToString();
            ddlMonth.SelectedValue = iMonth.ToString().PadLeft(2, '0');
            Gen.getStates(ddlState);

            getPaymentCycle();
        }
        #endregion

        UploadFile1();
    }
    #endregion


    private void getPaymentCycle()
    {
        DataSet dsPaymentCycle = new DataSet();
        dsPaymentCycle = Gen.getPaymentCycle();
        if (dsPaymentCycle.Tables[0].Rows.Count > 0)
        {
            ddlPaymentCycle.DataSource = dsPaymentCycle.Tables[0];
            ddlPaymentCycle.DataTextField = "WagePaymentCycle";
            ddlPaymentCycle.DataValueField = "intPaymentCycleID";
            ddlPaymentCycle.DataBind();
        }
    }


    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        //hdfFlagID.Value = "1";

        gvAttachments.DataSource = null;
        gvAttachments.DataBind();

        string strStateID;
        string strCountyID;
        if (ddlState.SelectedValue.Trim() != "--Select--")
        {
            strStateID = ddlState.SelectedValue;
        }
        else
        {
            strStateID = "%";
        }
        if (ddlCounty.SelectedValue.Trim() != "--Select--")
        {
            strCountyID = ddlCounty.SelectedValue;
        }
        else
        {
            strCountyID = "%";
        }

        DataSet dsExcWageRates = Gen.getExchRateWageRatebyStateCounty(strStateID, strCountyID);
        if (dsExcWageRates.Tables[0].Rows.Count == 0)
        {
            txtWageRate.Text = "0";
            txtExchangerate.Text = "0";
            txtWageRate.Enabled = false;
            return;
        }
        txtWageRate.Text = dsExcWageRates.Tables[0].Rows[0][1].ToString().Trim();
        txtExchangerate.Text = dsExcWageRates.Tables[0].Rows[0][0].ToString().Trim();
        txtWageRate.Enabled = false;

    }
    #endregion



    protected void BtnCanc_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void btnApplyforWage_Click(object sender, EventArgs e)
    {
        // 999 >>> Dummy Work ID for DIS
        string strWorkIdDIS = "999";
        int count = 0;
        string strAppendStringRadgrid = "";

        foreach (GridDataItem dataItem in rgBeneficiariesDIS.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                count = count + 1;
                int iRowIndex = dataItem.ClientRowIndex;
            }
        }

        if (count == 0)
        {
            lblmsg0.Text = "At lease one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        else
        {
            strAppendStringRadgrid = "";
            foreach (GridDataItem dataItem in rgBeneficiariesDIS.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
                {
                    string strIntBenId = "";
                    strIntBenId = dataItem.GetDataKeyValue("intBenId").ToString();
                    strAppendStringRadgrid = strAppendStringRadgrid + "," + strIntBenId;
                }
            }
            strAppendStringRadgrid = strAppendStringRadgrid.Trim(',');
        }



        string strFromDateDummy = string.Format("{0}-{1}-{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, "01");
        string strToDateDummy;
        if (ddlMonth.SelectedValue == "01" || ddlMonth.SelectedValue == "03" || ddlMonth.SelectedValue == "05" || ddlMonth.SelectedValue == "07" || ddlMonth.SelectedValue == "08" || ddlMonth.SelectedValue == "10" || ddlMonth.SelectedValue == "12")
        {
            strToDateDummy = string.Format("{0}-{1}-{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, "31");
        }
        else if (ddlMonth.SelectedValue == "04" || ddlMonth.SelectedValue == "06" || ddlMonth.SelectedValue == "09" || ddlMonth.SelectedValue == "11")
        {
            strToDateDummy = string.Format("{0}-{1}-{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, "30");
        }
        else
        {
            if (DateTime.IsLeapYear(Convert.ToInt32(ddlYear.SelectedValue)))
            {
                strToDateDummy = string.Format("{0}-{1}-{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, "29");
            }
            else
            {
                strToDateDummy = string.Format("{0}-{1}-{2}", ddlYear.SelectedValue, ddlMonth.SelectedValue, "28");
            }
        }

        //DUPLICATE CHECK: check if at least one of selected beneficiaries already belogns to an existing WR with same year-month-Pyament cycle:
        // if it exists: STOP with warning
        // else go on
        #region Check duplicate WR for beneficiaries
        bool bDuplicateWR = false;
        bDuplicateWR = Gen.CheckWageRequestDuplicate(ddlYear.SelectedValue, ddlMonth.SelectedValue, ddlPaymentCycle.SelectedValue, ddlBoma.SelectedValue, strAppendStringRadgrid);
        if (bDuplicateWR)
        {
            lblmsg0.Text = "Wage request cannot be created: some of selected beneficiaries has already payment request for selected month or Payment Cycle.<br /> Refresh the list before trying again: press Search button."; Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        //else GO on with creation
        #endregion

        int resultwage = Gen.insertWageRequestDraft(strWorkIdDIS, txtWageRate.Text, strFromDateDummy, strToDateDummy, ddlYear.SelectedValue, ddlMonth.SelectedValue, ddlPaymentCycle.SelectedValue.ToString(), txtExchangerate.Text, Session["uid"].ToString().Trim(), "Component 1 (Public Works)");

        if (resultwage > 0)
        {
            if (Session["CertificateTb1"] != null)
            {
                if (((DataTable)Session["CertificateTb1"]).Rows.Count > 0)
                {
                    for (int n = 0; n < ((DataTable)Session["CertificateTb1"]).Rows.Count; n++)
                    {
                        if (((DataTable)Session["CertificateTb1"]).Rows[n]["intEmpEarningid1"].ToString().Trim() == "0")
                        {
                            ((DataTable)Session["CertificateTb1"]).Rows[n]["intWagePaymentReqid"] = resultwage.ToString();  //Convert.ToString(i);
                        }
                    }
                    GetNewRectoInsertdr2();
                    int statuspr = Gen.bulkInsertdrInterDetails(myDtNewRecdr1);
                }
            }
        }

        string AppendString1 = string.Empty;
        AppendString1 = strAppendStringRadgrid;

        int s = Gen.InsUpdBeneficiaryWagesRequestAlternateNewModelDIS(AppendString1, Session["uid"].ToString(), txtWageRate.Text.Trim(), strWorkIdDIS, resultwage.ToString(), strFromDateDummy, strToDateDummy, txtDaysToBePaid.Text);

        int aa = Gen.InsUpdBeneficiaryWagesRequestUpdateAttendancebyID(resultwage.ToString(), "999", strFromDateDummy, strToDateDummy);


        string[] arrStrIntBenIDs = strAppendStringRadgrid.Split(',');
        foreach (string strIntbenIdTemp in arrStrIntBenIDs)
        {
            int iResInsertBenWorkWage = Gen.WorkAllocation(strWorkIdDIS, strIntbenIdTemp, Session["uid"].ToString().Trim());
        }

        #region Cash+
        foreach (GridDataItem dataItem in rgBeneficiariesDIS.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                string strIntBenId = "";
                strIntBenId = dataItem.GetDataKeyValue("intBenId").ToString();
                bool bFLI = (dataItem.FindControl("FLIchkSelected") as CheckBox).Checked;
                bool bECD = (dataItem.FindControl("ECDchkSelected") as CheckBox).Checked;
                bool bWASH = (dataItem.FindControl("WASHchkSelected") as CheckBox).Checked;
                //resultwage.ToString()
                //UPDATE RECORD WITH CAH+ activities
                Gen.UpdateCashPlusActivitiesDIS(resultwage.ToString(), strIntBenId, bFLI, bECD, bWASH);
            }
        }
        #endregion


        string status = "";
        Session["CertificateTb1"] = null;
        Session["PhotoFilePath"] = "";
        Session["PhotoFileName"] = "";
        lblmsg.Text = "Wage Request Submited Successfully..!";
        status = Session["username"].ToString() + " raised above payment: please review it.";
        try
        {

            int resultwageresult = Gen.insertWageRequestUpdatesStatusRemoveDataDraft(strWorkIdDIS, txtWageRate.Text, strFromDateDummy, strToDateDummy, System.DateTime.Now.Year.ToString(), System.DateTime.Now.Month.ToString(), txtExchangerate.Text, Session["uid"].ToString().Trim());


        }
        catch (Exception)
        {

        }
        SendMailforWagesDISDraft(status, "", Session["username"].ToString());
        success.Visible = true;
        Failure.Visible = false;
        clear();
        Response.Redirect("WageRequestResultDIS.aspx?WageReqNo=" + System.DateTime.Now.Year.ToString() + "-SNSOPWR-" + resultwage.ToString());
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    protected void btnSearchBeneficiaries_Click(object sender, EventArgs e)
    {
        Failure.Visible = false;
        lblmsg0.Text = "";

        DataSet dsExcWageRates = new DataSet();
        //check if Boma has been selected

        if (ddlBoma.SelectedValue.Trim() == "--Select--" || ddlBoma.SelectedValue.Trim() == "0" || ddlBoma.SelectedValue.Trim() == "")
        {
            lblmsg0.Text = "A Boma must be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }


        FillGridBeneficiaries();

        FillDetails();
    }

    private void FillGridBeneficiaries()
    {
        int iMonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int iStateId;
        int iCountyId;
        int iPayamId;
        int iBomaId;

        if (ddlState.SelectedValue.Trim() != "--Select--")
        {
            iStateId = Convert.ToInt32(ddlState.SelectedValue);
        }
        else
        {
            iStateId = 0;
        }
        if (ddlCounty.SelectedValue.Trim() != "--Select--")
        {
            iCountyId = Convert.ToInt32(ddlCounty.SelectedValue);
        }
        else
        {
            iCountyId = 0;
        }
        if (ddlPayam.SelectedValue.Trim() != "--Select--")
        {
            iPayamId = Convert.ToInt32(ddlPayam.SelectedValue);
        }
        else
        {
            iPayamId = 0;
        }
        if (ddlBoma.SelectedValue.Trim() != "--Select--")
        {
            iBomaId = Convert.ToInt32(ddlBoma.SelectedValue);
        }
        else
        {
            iBomaId = 0;
        }

        short paymentCycle;
        short.TryParse(ddlPaymentCycle.SelectedValue.ToLower(), out paymentCycle);


        List<BeneficiaryDIS> lsBeneficiaries = SSSNPContext.SSSNPManager.GetBeneficiariesDIS(new SSSNP_SNSOP.Entity.FilterCriteria.BeneficiaryDISCriteria()
        {
            Month = iMonth,
            Year = ddlYear.SelectedValue,
            intStateID = iStateId,
            intCountieID = iCountyId,
            intPayamID = iPayamId,
            intBomasID = iBomaId,
            PaymentCycle = paymentCycle
        });

        rgBeneficiariesDIS.DataSource = lsBeneficiaries;
        rgBeneficiariesDIS.Rebind();
        if (rgBeneficiariesDIS.DataSource == null)
        {
            rgBeneficiariesDIS.Visible = false;
        }
        else
        {
            rgBeneficiariesDIS.Visible = true;
        }
    }


    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdWagesApplyDIS.aspx");
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


    protected void gvAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ((DataTable)Session["CertificateTb1"]).Rows.RemoveAt(e.RowIndex);
            this.gvAttachments.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
            this.gvAttachments.DataBind();
        }
        catch (Exception ex)
        {
            //  lblresult.Text = "Please enter correct data";// ex.ToString();
        }
        finally
        {
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        gvAttachments.Visible = true;
        try
        {
            if (txtDescription.Text.Trim() == "")
            {
                lblresult.Text = "Please Enter Upload Description";
            }
            else
            {
                if (Session["PhotoFilePath"].ToString() != null && Session["PhotoFilePath"].ToString() != "")
                {
                    if (gvAttachments.Rows.Count == 0)
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

                    this.gvAttachments.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
                    this.gvAttachments.DataBind();

                    lblFileName.Text = "";
                    clear1();
                    lblresult.Text = "";
                    // GridView1.Columns[0].Visible = false;
                }
                else
                {
                    lblresult.Text = "Please Upload Document";
                }
            }
        }
        catch (Exception ee)
        {

        }
        gvAttachments.Visible = true;
    }


    private void GetCounties()
    {
        ddlCounty.Items.Clear();
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounty, ddlState.SelectedValue);
            ddlCounty.Enabled = true;
            ddlPayam.Items.Insert(0, " --Select--");
            ddlBoma.Items.Insert(0, " --Select--");
        }
        else
        {
            ddlCounty.Enabled = false;
            ddlCounty.Items.Insert(0, " --Select--");
            ddlPayam.Items.Insert(0, " --Select--");
            ddlPayam.Enabled = false;
            ddlBoma.Items.Insert(0, " --Select--");
            ddlBoma.Enabled = false;

        }
    }

    void getPayams()
    {
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();
        if (ddlCounty.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounty.SelectedValue);

            ddlPayam.Enabled = true;
            ddlBoma.Items.Insert(0, "--Select--");
            ddlBoma.Enabled = false;
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
            ddlPayam.Enabled = false;
            ddlBoma.Items.Insert(0, "--Select--");
            ddlBoma.Enabled = false;

        }
    }

    void getBomas()
    {
        ddlBoma.Items.Clear();
        if (ddlPayam.SelectedIndex != 0)
        {
            Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
            ddlBoma.Enabled = true;
        }
        else
        {
            ddlBoma.Items.Insert(0, "--Select--");
            ddlBoma.Enabled = false;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetCounties();
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomas();
    }

    void clear()
    {
        //ddlState.SelectedValue = "0";
        ddlState.SelectedValue = "--Select--";
        ddlCounty.Items.Clear();
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();


        btnApplyforWage.Text = "Apply for Wage Request";
        btnApplyforWage.Visible = false;
        lblDesc.Attributes.Add("style", "visibility:visible;display:none");
        lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");

        lblmsg0.Text = "";
        Failure.Visible = false;
    }

    // Upload Wage Beneficiary Document Functions
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

    void clear1()
    {
        txtDescription.Text = "";
        Session["PhotoFilePath"] = "";
        Session["PhotoFileName"] = "";
    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        StringBuilder javaScript = new StringBuilder();
        javaScript.Append("\n<script language=JavaScript>\n");
        javaScript.Append("window.history.forward(1);\n");
        javaScript.Append("</script>\n");
        Page.RegisterClientScriptBlock("HistoryScript", javaScript.ToString());
    }

    protected void rgBeneficiariesDIS_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {

        int iMonth = Convert.ToInt32(ddlMonth.SelectedValue);
        int iStateId;
        int iCountyId;
        int iPayamId;
        int iBomaId;

        if (ddlState.SelectedValue.Trim() != "--Select--")
        {
            iStateId = Convert.ToInt32(ddlState.SelectedValue);
        }
        else
        {
            iStateId = 0;
        }
        if (ddlCounty.SelectedValue.Trim() != "--Select--")
        {
            iCountyId = Convert.ToInt32(ddlCounty.SelectedValue);
        }
        else
        {
            iCountyId = 0;
        }
        if (ddlPayam.SelectedValue.Trim() != "--Select--")
        {
            iPayamId = Convert.ToInt32(ddlPayam.SelectedValue);
        }
        else
        {
            iPayamId = 0;
        }
        if (ddlBoma.SelectedValue.Trim() != "--Select--")
        {
            iBomaId = Convert.ToInt32(ddlBoma.SelectedValue);
        }
        else
        {
            iBomaId = 0;
        }


        List<BeneficiaryDIS> lsBeneficiaries = SSSNPContext.SSSNPManager.GetBeneficiariesDIS(new SSSNP_SNSOP.Entity.FilterCriteria.BeneficiaryDISCriteria()
        {
            Month = iMonth,
            Year = ddlYear.SelectedValue,
            intStateID = iStateId,
            intCountieID = iCountyId,
            intPayamID = iPayamId,
            intBomasID = iBomaId
        });

        rgBeneficiariesDIS.DataSource = lsBeneficiaries;
        //TODO: Method to extract eligible DIS beneficiaries for wage requests
        //DataSet ds = Gen.getAttendanceCountFromToWagesNewwithAmount(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.Items[2].Value, "", txtAmount.Text);

        // TO BE DECOMMENTED
        //DataTable dt = ds.Tables[0];

        //rgBeneficiariesDIS.DataSource = dt;

        if (rgBeneficiariesDIS.DataSource == null)
        {
            rgBeneficiariesDIS.Visible = false;
        }
        else
        {
            rgBeneficiariesDIS.Visible = true;
        }
    }

    protected void rgBeneficiariesDIS_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = rgBeneficiariesDIS.MasterTableView.PageSize * rgBeneficiariesDIS.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
        }
        if (e.Item is GridPagerItem)
        {
            int iTotalRows = ((List<BeneficiaryDIS>)rgBeneficiariesDIS.DataSource).Count;
            //int iTotalRows = ((System.Data.DataTable)rgBeneficiariesDIS.DataSource).Rows.Count;
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
            PageSizeCombo.Items.Clear();
            //if (iTotalRows > 250)
            //{
            PageSizeCombo.Items.Add(new RadComboBoxItem("250", "250"));
            PageSizeCombo.FindItemByText("250").Attributes.Add("ownerTableViewId", rgBeneficiariesDIS.MasterTableView.ClientID);
            //}
            if (iTotalRows > 500)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("500", "500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", rgBeneficiariesDIS.MasterTableView.ClientID);
            }

            if (iTotalRows > 1000)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000", "1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", rgBeneficiariesDIS.MasterTableView.ClientID);
            }

            PageSizeCombo.Items.Add(new RadComboBoxItem("All", iTotalRows.ToString()));
            PageSizeCombo.FindItemByText("All").Attributes.Add("ownerTableViewId", rgBeneficiariesDIS.MasterTableView.ClientID);
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

    #region Mail Send Functionalities

    public void SendMailforWagesDISDraft(string numbers, string anothermail, string Ipname)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SNSOP System\" <{0}>", sssnpmail);
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
      
        DataSet dsIPApprovers = Gen.GetIPApprovers();
        int iTotIPapprovers = dsIPApprovers.Tables[0].Rows.Count;

        for (int iCountIPapproves = 0;  iCountIPapproves  < iTotIPapprovers;  iCountIPapproves ++)
        {
            mail.To.Add(new MailAddress((dsIPApprovers.Tables[0].Rows[iCountIPapproves]["Email"]).ToString()));
        }

        mail.From = new MailAddress(from, ":: SNSOP Public works MIS :: Wages", System.Text.Encoding.UTF8);
        mail.Subject = "SNSOP MIS – Notification on Wage Payroll for Review";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<H2>Notification on Wage Payroll for Review - DIS</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + "DIS" + "<br> <br> Work Request No: " + "DIS" + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);
        client.Port = 587;
        client.Host = smtpserver;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("no-reply@southsudansafetynet.info", "2sOQenMaTV-?");

        try
        {
            client.Send(mail);
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
    public void SendMailforWagesDIS(string numbers, string anothermail, string Ipname)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);
        
        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        // mail.To.Add(to);

        string anothermail1 = "";

        string[] multi = anothermail1.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        mail.From = new MailAddress(from, ":: SSSNP Public works MIS :: Wages", System.Text.Encoding.UTF8);
        mail.Subject = "SSSNP Public Works MIS – Notification on Wage Payroll for Review";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<H2>Notification on Wage Payroll for Review - DIS</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + "DIS" + "<br> <br> Work Request No: " + "DIS" + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

        try
        {
            client.Send(mail);
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

    #region Bulk Insert Functionalities - Wage Attachments
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
    #endregion

    protected void ddlPaymentCycle_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearchBeneficiaries_Click(sender, e);
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearchBeneficiaries_Click(sender, e);
    }

    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnSearchBeneficiaries_Click(sender, e);
    }
}
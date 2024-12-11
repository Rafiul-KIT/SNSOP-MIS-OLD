#region Using/Inherits Classess
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.IO;
using Telerik.Web.UI;
using System.Web;
using System.Configuration;
using SSSNP.Network;
#endregion

#region Amendment History Begin
//Form Name : Apply for Wage Reuest
//Purpose : To Add Wage Reuest
//Tables used : tr_BenAttendanceDet,td_WorkWageRequest,td_WageReqBenDet,tbl_WorkMaster,tbl_BeneficiaryDet,tr_BenfWorkDet
//Stored procedures Used : getOfficers,GetworkproposalforProgress,getAttendanceCountFromToWagesNewWithAmount,getCheckingWagesApplyrestrictionbyIDNewModel,insertWageRequest,InsUpdBeneficiaryWagesRequestAlternateNewModel,insertWageRequestUpdatesStatusRemoveData
#endregion Amendment History End


public partial class PDCHouseholdWagesApply : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    DataTable myDtNewRecdr = new DataTable();
    DataTable myDtNewRecdr1 = new DataTable();
    public DataTable dtMyTable1;
    public DataTable dtMyTable2;
    public DataTable dtMyTableCertificate;
    public DataTable dtMyTablepr;
    public DataTable dtMyTableCertificate2;
    private EmailService emailService = new EmailService();
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
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

        // Button Click Disable wait and Validation group
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        //this.BtnSave1.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave1, "group"));

        if (CUser.Current.IsIPRole || CUser.Current.IsCoordinatorRole)
        {
            LBLExchangeRate.Visible = false;
            txtExchangerate.Visible = false;

        }
        else
        {
            LBLExchangeRate.Visible = true;
            txtExchangerate.Visible = true;
        }

        #region Is not Postback Statement
        if (!IsPostBack)
        {
            Session["PhotoFilePath"] = "";
            Session["PhotoFileName"] = "";
            GrdExisted.DataSource = null;
            GrdExisted.DataBind();
            getPaymentCycle();
            BtnApplyForWR.Visible = false;
            lblDesc.Attributes.Add("style", "visibility:visible;display:none");
            lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
            lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");
            dtMyTableCertificate = createtablecrtificate2();
            Session["CertificateTb1"] = dtMyTableCertificate;
        }
        #endregion

        // Upload Documents
        UploadFile1();

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            tr_group_search.Visible = true;
            BtnApplyForWR.Text = "Apply for Wage Request";
        }
        #endregion
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
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

        //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());
        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //rgAttendances.DataSource = ds.Tables[0];
            //rgAttendances.DataBind();
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            hdfIP.Value = ds.Tables[0].Rows[0]["IPid"].ToString();

            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            DataSet dsnew = new DataSet();
            dsnew = Gen.getExchangeRatebyState(ds.Tables[0].Rows[0]["intCountieid"].ToString());

            txtExchangerate.Text = "";
            txtAmount.Text = "";

            if (dsnew.Tables[0].Rows.Count > 0)
            {
                txtExchangerate.Text = dsnew.Tables[0].Rows[0][0].ToString().Trim();
                txtAmount.Text = dsnew.Tables[0].Rows[0][1].ToString().Trim();
            }
            txtAmount.Enabled = false;
            //grdDetails.DataSource = null;
            //grdDetails.DataBind();
            ddl_group_code.Enabled = false;
            GridView1.DataSource = null;
            GridView1.DataBind();
            GrdExisted.DataSource = null;
            GrdExisted.DataBind();
            //rgAttendances.DataSource = null;
            //rgAttendances.DataBind();
            //TxtFromDate.Text = "";
            //TxtToDate.Text = "";
            rgAttendances.Rebind();
        }
    }
    #endregion

    #region Button Click functionalities


    protected void BtnCanc_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void BtnApplyForWR_Click(object sender, EventArgs e)
    {
        #region Temporary solution to allow DIS attendance submission> to be restored once new DIS process is in place
        //if (cmf.convertDateIndia(TxtFromDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //{
        //    lblmsg0.Text = "From date should be less than the current date.";
        //    success.Visible = false;
        //    Failure.Visible = true;
        //    return;
        //}
        //else if (cmf.convertDateIndia(TxtToDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //{
        //    lblmsg0.Text = "To date should be less than the current date.";
        //    success.Visible = false;
        //    Failure.Visible = true;
        //    return;
        //}
        #endregion
        //else
        {
            GrdExisted.DataSource = null;
            GrdExisted.DataBind();
            int count = 0;
            string strAppendStringRadgrid = "";

            foreach (GridDataItem dataItem in rgAttendances.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
                {
                    count = count + 1;
                    int iRowIndex = dataItem.ClientRowIndex;
                    //rgAttendances.MasterTableView[iRowIndex]     ["TotAttend"]
                    if (dataItem["TotAttend"].Text == "0")
                    {
                        lblmsg0.Text = dataItem["NameofHousehold"].Text + " Beneficiary should have attendance";
                        Failure.Visible = true;
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
                        return;
                    }
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
                if ((null != Session["CertificateTb1"]) && (((DataTable)Session["CertificateTb1"]).Rows.Count == 0))
                {
                    Failure.Visible = true;
                    lblmsg0.Text = "Please upload at least one document";
                    return;
                }

                strAppendStringRadgrid = "";
                foreach (GridDataItem dataItem in rgAttendances.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
                    {
                        //Retrieve intBenTempid
                        string strIntBenId = "";
                        strIntBenId = dataItem.GetDataKeyValue("intBenId").ToString();
                        strAppendStringRadgrid = strAppendStringRadgrid + "," + strIntBenId;
                    }
                }
                strAppendStringRadgrid = strAppendStringRadgrid.Trim(',');


                DataSet dschecks = Gen.getRestrictionofWageApplybyBenficiaryIDbyNewModel(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, strAppendStringRadgrid);
                if (dschecks.Tables[0].Rows.Count > 0)
                {
                    BtnApplyForWR.Visible = false;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:none");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");
                    BtnApplyForWR.Visible = true;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:block");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:block");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:block");
                    Failure.Visible = true;
                    lblmsg0.Text = "You have already applied for some benficiaries between/within these days as below list";
                    GrdExisted.DataSource = null;
                    GrdExisted.DataBind();
                    GrdExisted.DataSource = dschecks.Tables[0];
                    GrdExisted.DataBind();
                    return;
                }
            }

            int resultwage = Gen.insertWageRequestDraft(hdfID.Value.ToString(), txtAmount.Text, TxtFromDate.Text, TxtToDate.Text, System.DateTime.Now.Year.ToString(), System.DateTime.Now.Month.ToString(), ddlPaymentCycle.SelectedValue.ToString(), txtExchangerate.Text, Session["uid"].ToString().Trim(), "Component 1 (Public Works)");

            if (resultwage > 0)
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

            GridView1.DataSource = null;
            GridView1.DataBind();
            string AppendString1 = string.Empty;
            AppendString1 = strAppendStringRadgrid;

            int s = Gen.InsUpdBeneficiaryWagesRequestAlternateNewModel(AppendString1, Session["uid"].ToString(), txtAmount.Text.Trim(), hdfID.Value.ToString(), resultwage.ToString(), TxtFromDate.Text, TxtToDate.Text);

            int aa = Gen.InsUpdBeneficiaryWagesRequestUpdateAttendancebyID(resultwage.ToString(), hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text);
            string status = "";
            Session["CertificateTb1"] = null;
            Session["PhotoFilePath"] = "";
            Session["PhotoFileName"] = "";
            lblmsg.Text = "Wage Request Submited Successfully..!";
            status = Session["username"].ToString() + " has raise the above payment: Please review it.";
            try
            {
                int resultwageresult = Gen.insertWageRequestUpdatesStatusRemoveDataDraft(hdfID.Value.ToString(), txtAmount.Text, TxtFromDate.Text, TxtToDate.Text, System.DateTime.Now.Year.ToString(), System.DateTime.Now.Month.ToString(), txtExchangerate.Text, Session["uid"].ToString().Trim());
            }
            catch (Exception)
            {

            }


            GrdExisted.DataSource = null;
            GrdExisted.DataBind();
            //SendMailforWagesDraft(status, "", Session["username"].ToString(), lblWorkCode.Text, System.DateTime.Now.Year.ToString() + "-SNSOPWR-" + resultwage.ToString());

            string subject = "SNSOP MIS – Notification on Wage Payroll for Review";

            string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                                                                                                 //mail.Body = "<H2>Notification on Wage Payroll for Review - DIS</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + "DIS" + "<br> <br> Work Request No: " + "DIS" + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
            string body = "<H2>Notification on Wage Payroll for Review - LIPW</H2><br> <br> <b> IP NAME: " + Session["username"].ToString() + 
                "</b> <br><br> Work Code: " + lblWorkCode.Text + "<br> <br> Work Request No: " + "-SNSOPWR-" + resultwage.ToString() + 
                " <br> Please review the above Wage Request. <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards";

            emailService.sendEmailToIpApprover(subject, body);

            success.Visible = true;
            Failure.Visible = false;
            clear();
            Response.Redirect("WageRequestResult.aspx?WageReqNo=" + System.DateTime.Now.Year.ToString() + "-SNSOPWR-" + resultwage.ToString());
        }
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    protected void BtnSave3_Click(object sender, EventArgs e)
    {

        #endregion
        //else
        {
            Failure.Visible = false;
            lblmsg0.Text = "";
            DataSet ds = new DataSet();
            ds.Clear();
            DataSet dsnew = new DataSet();
            dsnew.Clear();
            int cnt = 0;
            cnt = ddl_group_code.Items.Count;
            int paymentCycle = 0;
            int.TryParse(ddlPaymentCycle.SelectedValue, out paymentCycle);

            ds = Gen.getAttendanceCountFromToWagesNewwithAmount(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.Items[2].Value, "", txtAmount.Text, paymentCycle);

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    rgAttendances.Rebind();
                    BtnApplyForWR.Visible = true;
                    tr_group_search.Visible = true;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:block");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:block");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:block");
                    Failure.Visible = false;
                    lblmsg0.Text = "";
                    GrdExisted.DataSource = null;
                    GrdExisted.DataBind();
                }
                else
                {
                    BtnApplyForWR.Visible = false;
                    lblDesc.Attributes.Add("style", "visibility:visible;display:none");
                    lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
                    lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");

                    //grdDetails.DataSource = null;
                    //grdDetails.DataBind();
                    Failure.Visible = true;
                    lblmsg0.Text = "There is no records/already applied";
                    GrdExisted.DataSource = null;
                    GrdExisted.DataBind();
                }
            }
            else
            {
                BtnApplyForWR.Visible = false;
                lblDesc.Attributes.Add("style", "visibility:visible;display:none");
                lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
                lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");
                Failure.Visible = true;
                lblmsg0.Text = "There is no records/already applied";
                GrdExisted.DataSource = null;
                GrdExisted.DataBind();
            }
        }
    }

    protected void rgAttendances_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if ((hdfID != null && hdfID.Value != null && hdfID.Value != "")
            && (TxtFromDate != null && TxtFromDate.Text != "")
            && (TxtToDate != null && TxtToDate.Text != "")
            && (ddl_group_type.Items[2] != null && ddl_group_type.Items[2].Value != null)
            && (txtAmount != null && txtAmount.Text != "")
            )
        {
            int paymentCycle = 0;
            int.TryParse(ddlPaymentCycle.SelectedValue, out paymentCycle);

            DataSet ds = Gen.getAttendanceCountFromToWagesNewwithAmount(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.Items[2].Value, "", txtAmount.Text, paymentCycle);
            DataTable dt = ds.Tables[0];

            rgAttendances.DataSource = dt;
        }
        if (rgAttendances.DataSource == null)
        {
            rgAttendances.Visible = false;
        }
        else
        {
            rgAttendances.Visible = true;
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

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdWagesApply.aspx");
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
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
                    // GridView1.Columns[0].Visible = false;
                }
                else
                {
                    lblresult.Text = "Please Upload Documnet";
                }
            }
        }
        catch (Exception ee)
        {
            ////lbldtvalid.Text = "Please enter correct Date.";
            ////lbldtvalid.ForeColor = System.Drawing.Color.DarkRed;
        }
        GridView1.Visible = true;
        //  clear1();
    }

    #region Clear Function
    void clear()
    {
        BtnApplyForWR.Text = "Apply for Wage Request";
        txtStartDate.Text = "";
        //grdDetails.DataSource = null;
        //grdDetails.DataBind();
        GridView1.DataSource = null;
        GridView1.DataBind();
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
        BtnApplyForWR.Visible = false;
        lblDesc.Attributes.Add("style", "visibility:visible;display:none");
        lblDesc2.Attributes.Add("style", "visibility:visible;display:none");
        lblDescLabel.Attributes.Add("style", "visibility:visible;display:none");
        GrdExisted.DataSource = null;
        GrdExisted.DataBind();
        rgAttendances.DataSource = null;
        rgAttendances.DataBind();
        rgAttendances.Rebind();
        lblmsg0.Text = "";
        Failure.Visible = false;
    }

    // Upload Wage Beneficiary Documnt Functions
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
    #endregion
    #region Gridview Events
    //protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {
    //        string Amt = "";
    //        CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("CheckBox1");

    //        if (e.Row.Cells[7].Text != "")
    //        {
    //            decimal TotalAmountGrd = (Convert.ToDecimal(e.Row.Cells[8].Text.ToString()));
    //            TotalAmountGrd1 = TotalAmountGrd + TotalAmountGrd1;
    //        }
    //        else
    //        {
    //            e.Row.Cells[8].Text = "0.00";
    //        }

    //        int Totalnoofdays = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"));
    //        Totalnoofdays1 = Totalnoofdays + Totalnoofdays1;
    //    }
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[6].Text = "Total";
    //        e.Row.Cells[7].Text = Totalnoofdays1.ToString();
    //        e.Row.Cells[8].Text = TotalAmountGrd1.ToString();
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
        //if (grdDetails.Rows.Count > 0)
        //{
        //}
    }

    protected void ddlManager_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #endregion
    #region Mail Send Functionalities
    public void SendMailforWagesDraft(string numbers, string anothermail, string Ipname, string WorkCode, string WorkRequest)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        DataSet dsIPApprovers = Gen.GetIPApprovers();
        int iTotIPapprovers = dsIPApprovers.Tables[0].Rows.Count;

        for (int iCountIPapproves = 0; iCountIPapproves < iTotIPapprovers; iCountIPapproves++)
        {
            mail.To.Add(new MailAddress((dsIPApprovers.Tables[0].Rows[iCountIPapproves]["Email"]).ToString()));
        }

        mail.From = new MailAddress(from, ":: SNSOP Public works MIS :: Wages", System.Text.Encoding.UTF8);
        mail.Subject = "SNSOP MIS – Notification on Wage Payroll for Review";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        //mail.Body = "<H2>Notification on Wage Payroll for Review - DIS</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + "DIS" + "<br> <br> Work Request No: " + "DIS" + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.Body = "<H2>Notification on Wage Payroll for Review</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + WorkCode + "<br> <br> Work Request No: " + WorkRequest + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.Host = smtpserver;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("no-reply@southsudansafetynet.info", "2sOQenMaTV-?");

        MailAddress fromMail = new MailAddress("no-reply@southsudansafetynet.info");
        MailAddress toMail = new MailAddress(ConfigurationManager.AppSettings["OneEmailRecipient"].ToString());

        mail = new MailMessage(fromMail, toMail);
        mail.Subject = "SNSOP MIS – Notification on Wage Payroll for Review";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Notification on Wage Payroll for Review</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + WorkCode + "<br> <br> Work Request No: " + WorkRequest + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

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

    public void SendMailforWages(string numbers, string anothermail, string Ipname, string WorkCode, string WorkRequest)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);
        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
      

        string anothermail1 = "";

        //}
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
            // HttpContext.Current.Response.Write(errorMessage);
        } // end try
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
}
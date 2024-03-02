#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using Telerik.Web.UI;
using System.Text;
#endregion

#region Amendment History Begin
//Form Name : PDC Household Attendance
//Purpose : To Add Household Attendance
//Tables used :  tr_BenAttendanceDet,tbl_BeneficiaryDet,tm_Bomas
//Stored procedures Used : BenefeciaryAttendance,GetWorkwiseBenDet,GetWorkwiseBenDetSearchbyGroup_Sp,GetWorkwiseBenDetWithDate,BenefeciaryAttendanceNewModel
#endregion Amendment History End

public partial class PDCHouseholdAttendance : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    bool admin = false; bool update = false;
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count == 0)
        {
            Response.Redirect("~/frmuserlogin.aspx");
        }
        if (Session["uid"].ToString() == "1066")
            admin = true;
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (!IsPostBack)
        {
            BtnSave3.Visible = false;
            Session["IPName"] = "";
            txtStartDate.Visible = false;
            
        }
        // Look up Click Function Intialization
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));

        #region Is not Postback Statement
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Failure.Visible = false;
            success.Visible = false;
            RadDetail.DataSource = null;
            RadDetail.Rebind();
            FillDetails();
            BtnSave3.Text = "Submit";
            tr_group_search.Visible = false;
        }
        #endregion

        #region BN RadWindow
        if (!Page.IsPostBack)
        {
            //rwConfirmOperation.VisibleOnPageLoad = false;
            //rwConfirmOperation.Visible = false;
        }
        #endregion
    }
    #endregion

    #region Dropdown Binding Functionalities and Index Changed 
    // Group Type Selecton Changed
    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_group_type.SelectedValue == "")
        {
            FillDetails();
        }
        if (ddl_group_type.SelectedValue == "Group")
        {
            tr_group_code.Visible = true;
            hdfFlagID.Value = "1";
            DataSet dsBen = new DataSet();
            dsBen = Gen.GetWorkwiseBenDetSearchbyGroup_Sp(hdfID.Value.ToString(), ddl_group_type.SelectedValue.ToString(), ddl_group_code.SelectedValue.ToString());
            if (dsBen.Tables[0].Rows.Count > 0)
            {
                // BtnSave3.Visible = true;
                //grdDetails.Visible = true;
                //grdDetails.DataSource = dsBen.Tables[0];
                //grdDetails.DataBind();
                RadDetail.Rebind();
                // BtnSave3.Visible = true;
            }
            else
            {
                //  BtnSave3.Visible = false;
                // grdDetails.Visible = false;
            }
        }
        if (ddl_group_type.SelectedValue == "CST")
        {
            //tr_group_code.Style.Add("display", "none");
            tr_group_code.Visible = false;
            ddl_group_code.SelectedValue = "";
            hdfFlagID.Value = "1";
            DataSet dsBen = new DataSet();
            dsBen = Gen.GetWorkwiseBenDetSearchbyGroup_Sp(hdfID.Value.ToString(), ddl_group_type.SelectedValue.ToString(), ddl_group_code.SelectedValue.ToString());
            if (dsBen.Tables[0].Rows.Count > 0)
            {
                //   BtnSave3.Visible = true;
                //grdDetails.Visible = true;
                //grdDetails.DataSource = dsBen.Tables[0];
                //grdDetails.DataBind();
                RadDetail.Rebind();
                // BtnSave3.Visible = true;
            }
            else
            {
                //  BtnSave3.Visible = false;
                //grdDetails.Visible = false;
            }
        }
    }

    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfFlagID.Value = "1";
        DataSet dsBen = new DataSet();
        dsBen = Gen.GetWorkwiseBenDetSearchbyGroup_Sp(hdfID.Value.ToString(), ddl_group_type.SelectedValue.ToString(), ddl_group_code.SelectedValue.ToString());
        if (dsBen.Tables[0].Rows.Count > 0)
        {
            // BtnSave3.Visible = true;
            //    RadDetail.Rebind();
            //grdDetails.Visible = true;
            //grdDetails.DataSource = dsBen.Tables[0];
            //grdDetails.DataBind();
            //    // BtnSave3.Visible = true;
        }
        else
        {
            //  BtnSave3.Visible = false;
        }
    }
    #endregion

    #region Button Click functionalities 
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        SearchDetails();
    }

    private bool IsCheckMandatoryBox()
    {
        foreach (GridDataItem item in RadDetail.Items)
        {
            CheckBox cbControl = (CheckBox)item.Cells[2].FindControl("chkSelected");
            if (cbControl.Checked)
            {
                CheckBox cbFLI = (CheckBox)item.Cells[4].FindControl("FLIchkSelected");
                CheckBox cbECD = (CheckBox)item.Cells[5].FindControl("ECDchkSelected");
                CheckBox cbWAS = (CheckBox)item.Cells[6].FindControl("WASHchkSelected");
                if (!cbFLI.Checked && !cbECD.Checked && !cbWAS.Checked) return false;

            }
        }
        return true;

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (cmf.convertDateIndia(txtStartDate.Text) < cmf.convertDateIndia(lblWorkStartDate.Text))
        {
            lblmsg0.Text = "Attendance Date should be greater than the Work Start date. ";
            success.Visible = false;
            Failure.Visible = true;
            return;
        }

        #region Temporary solution to allow DIS attendance submission> to be restored once new DIS process is in place
        //if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //{
        //    lblmsg0.Text = "Attendance date should be less than the current date.";
        //    success.Visible = false;
        //    Failure.Visible = true;
        //    return;
        //}
        #endregion

        //if (!IsCheckMandatoryBox())
        //{
        //    // messaggio
        //    //ScriptManager.RegisterStartupScript(this, GetType(), "unique_key",     "element.onclick = function(){ return confirm('Are you sure you want to delete?'); };",       true);
        //    var ttt = "ok";



        //    rwConfirmOperation.VisibleOnPageLoad = true;
        //    rwConfirmOperation.Visible = true;


        //} 

        //DA SOSTARE SUL CLICK DEI PULSANTI
        int count = 0;
        StringBuilder AppendString = new StringBuilder();
        GridHeaderItem hItem = (GridHeaderItem)RadDetail.MasterTableView.GetItems(GridItemType.Header)[0];
        CheckBox chkAll = (CheckBox)hItem.FindControl("checkAll");

        //Check all verification removed: each item is verified for selection in any case
        //if (!chkAll.Checked)
        //{
        foreach (GridDataItem item in RadDetail.Items)
        {
            CheckBox cbControl = (CheckBox)item.Cells[0].FindControl("chkSelected");
            if (cbControl.Checked)
            {
                DropDownList ddlAttendanc = ((DropDownList)item.Cells[10].FindControl("ddlStatus"));
                int benId = 0;
                int.TryParse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["intBenid"].ToString(), out benId);
                AppendString.Append(string.Format("{0}{1},", benId.ToString(), ddlAttendanc.SelectedValue));
                count += 1;
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
            string appendString = AppendString.ToString().Substring(0, AppendString.ToString().Length - 1);
            int s = Gen.BenefeciaryAttendanceNewLatestModel(hdfID.Value.ToString(), appendString, txtStartDate.Text, Session["uid"].ToString());

            if (s == 999)
            {
                Failure.Visible = true;
                lblmsg0.Text = "Already wage applied with this date..!";
                success.Visible = false;
                Failure.Visible = true;
            }
            else
            {
                #region Cash+
                foreach (GridDataItem dataItem in RadDetail.MasterTableView.Items)
                {
                    if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
                    {
                        string strIntBenId = "";
                        strIntBenId = dataItem.GetDataKeyValue("intBenid").ToString();
                        bool bFLI = (dataItem.FindControl("FLIchkSelected") as CheckBox).Checked;
                        bool bECD = (dataItem.FindControl("ECDchkSelected") as CheckBox).Checked;
                        bool bWASH = (dataItem.FindControl("WASHchkSelected") as CheckBox).Checked;
                        //resultwage.ToString()
                        //UPDATE RECORD WITH CAH+ activities
                        Gen.UpdateCashPlusActivitiesLIPW(hdfID.Value.ToString(), strIntBenId, txtStartDate.Text, bFLI, bECD, bWASH);
                    }
                }
                #endregion
                Failure.Visible = false;
                lblmsg.Text = "Submitted Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
        }
    }

    void clear()
    {
        BtnSave3.Text = "Submit";
        txtStartDate.Text = "";
        RadDetail.DataSource = null;
        RadDetail.Rebind();
        //grdDetails.DataSource = null;
        //grdDetails.DataBind();
        tr_group_search.Visible = false;
        BtnSave3.Visible = false;
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
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

    #region FillDetails by Work id for Beneficiary Detaiks adn work details
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        //  UpdateProgress.Focus();
        // System.Threading.Thread.Sleep(5000);
        DataSet ds = new DataSet();
        //Jyotshna On 24-05-2016
        //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());
        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            if (ds.Tables[0].Rows[0]["Status"].ToString() == "Completed")
            {
                BtnSave3.Enabled = false;
            }
            else
            {
                BtnSave3.Enabled = true;
            }
            Session["IPName"] = ds.Tables[0].Rows[0]["IPName"].ToString();
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            try
            {
                lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            }
            catch
            { }
            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            txtStartDate.Visible = true;
            RadDetail.Rebind();
            //grdDetails.DataSource = null;
            //grdDetails.DataBind();
            //  BtnSave3.Visible = false;
            Failure.Visible = false;
            lblmsg.Text = "";
            success.Visible = false;
            lblmsg0.Text = "";
        }
        else
        {
            //grdDetails.DataSource = null;
            //grdDetails.DataBind();
            //   BtnSave3.Visible = false;
        }
    }

    private void SearchDetails()
    {
    }
    #endregion

    #region gridview Events 
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    }

    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //grdDetails.PageIndex = e.NewPageIndex;
        //FillDetails();
    }
    #endregion

    #region TextBox Changed Functionalities
    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        // ** commented by ambik as per discussion with sunita and samiran
        // FillDetails(); 

        if (hdfID.Value != "")
        {
            if (txtStartDate.Text.Trim() != "")
            {
                lblmsg0.Text = "";
                Failure.Visible = false;
                DataSet dsBen = new DataSet();
                dsBen = Gen.GetWorkwiseBenDetBySelectionofDate(hdfID.Value.ToString(), txtStartDate.Text);
                if (dsBen.Tables[0].Rows.Count > 0)
                {
                    //   BtnSave3.Visible = true;
                    //grdDetails.DataSource = dsBen.Tables[0];
                    //grdDetails.DataBind();
                    //  RadDetail.DataSource = dsBen.Tables[0];
                    RadDetail.Rebind();
                    lblmsg0.Text = "";
                    Failure.Visible = false;
                    Failure.Visible = false;
                    lblmsg.Text = "";
                    success.Visible = false;
                    lblmsg0.Text = "";
                }
                else
                {
                    RadDetail.DataSource = null;
                    //grdDetails.DataSource = null;
                    //grdDetails.DataBind();
                    BtnSave3.Visible = false;
                    lblmsg0.Text = "You have already applied wages with this date";
                    Failure.Visible = true;
                    lblmsg.Text = "";
                    success.Visible = false;
                }
            }
            else
            {
                lblmsg0.Text = "Please Select Date";
                Failure.Visible = true;
                lblmsg.Text = "";
                success.Visible = false;
            }
        }
        else
        {
            //grdDetails.DataSource = null;
            //grdDetails.DataBind();
            RadDetail.DataSource = null;
            //     BtnSave3.Visible = false;
            lblmsg.Text = "";
            success.Visible = false;
            lblmsg0.Text = "";
            Failure.Visible = false;
        }
    }
    #endregion

    #region Mail Send Functionalities
    public void SendMailAnother(string Name)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        // string anothermail = "";
        string anothermail = System.Configuration.ConfigurationManager.AppSettings["TwoEmailRecipients"];
        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        string Messge = Name + " has updated Attendance for above work. Please verify it.";
        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "SSSNP Public Works MIS - Work Attendance Notification";
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "Dear Team <br><br>  <H2>SSSNP Public Works MIS - Work Attendance Notification</H2><br> <b> IP NAME: " + Name + "</b> <br> Work Code: 2017-SNSDP-Public works in Juba Municipality-1108 <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href=" + sssnpweb + " target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + Name + "";
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
        } // end try
    }
    #endregion

    protected void RadDetail_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (!string.IsNullOrEmpty(hdfID.Value.ToString()))
        {
            DataSet dsBen = new DataSet();
            dsBen = Gen.GetWorkwiseBenDetBySelectionofDate(hdfID.Value.ToString(), "01/01/2020");
            if (dsBen.Tables[0].Rows.Count > 0)
            {
                BtnSave3.Visible = true;
                RadDetail.DataSource = dsBen.Tables[0];
                RadDetail.Visible = true;
            }
            else
            {
                BtnSave3.Visible = true;
                RadDetail.Visible = false;
            }
        }
    }

    protected void RadDetail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadDetail.MasterTableView.PageSize * RadDetail.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
        }
    }

    protected void RadDetail_ItemCreated(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridPagerItem)
        {
            var dropDown = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
            var totalCount = ((GridPagerItem)e.Item).Paging.DataSourceCount;
            var sizes = new Dictionary<string, string>() {
                            {"50", "50"}
                };
            if (totalCount > 100)
            {
                sizes.Add("100", "100");
            }
            if (totalCount > 200)
            {
                sizes.Add("200", "200");
            }
            sizes.Add("All", totalCount.ToString());
            dropDown.Items.Clear();
            foreach (var size in sizes)
            {
                var cboItem = new RadComboBoxItem() { Text = size.Key, Value = size.Value };
                cboItem.Attributes.Add("ownerTableViewId", e.Item.OwnerTableView.ClientID);
                dropDown.Items.Add(cboItem);
            }
            if (dropDown.FindItemByValue(e.Item.OwnerTableView.PageSize.ToString()) != null)
            {
                dropDown.FindItemByValue(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
            else
            {
                dropDown.FindItemByText("All").Selected = true;
                dropDown.FindItemByValue("50").Selected = true;
            }
        }
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        clear();
    }

    #region Button
    protected void RadButtonYes_Click(object sender, EventArgs e)
    {

    }

    protected void RadButtonNo_Click(object sender, EventArgs e)
    {

        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "OnClientClickedClose", string.Format("ClosePopup();"), true);

    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

public partial class Verify_Invoice : System.Web.UI.Page
{
    //designing and developed by siva as on 27-02-2016

    //Purpose : To update financial Details
    //Tables used : tr_FinancialApproval,tbl_WorkMaster
    //Stored procedures Used : UpdFundApproved,sp_getCounties,sp_getPayams,getBomasbyID
    //
    int delete = 0;
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    DataRow dtrdr;
    decimal FundRaiseAmount1;
    DataTable myDtNewRecdr = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {


        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        if (!IsPostBack)
        {
            ResetFormControlDisableEnable(this, false);

            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);



            Gen.getCouncilMems(chkTst, "TST");
            Gen.getCouncilMems(ChkCA, "CA");
            Gen.getCouncilMems(ChkPDC, "PDC");
            Gen.getCouncilMems(ChkBDC, "BDC");

            //lblIP.Text = Session["username"].ToString();

            if (Request.QueryString["invoice"] != null && Request.QueryString["WorkId"] != null)
            {
                hdfFlagID.Value = "0";
                hdfInVoiceID.Value = Request.QueryString["invoice"].ToString();
                hdfID.Value = Request.QueryString["WorkId"].ToString();
            }


        }

        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            trTSTApproval.Visible = true;

            trworkcode.Visible = true;
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            //if (Session["userlevel"].ToString() == "1")
            //{
            //    BtnDelete.Visible = true;
            //}
            //else
            //{
            //    BtnDelete.Visible = false;
            //}
            BtnSave1.Text = "Update";
        }
    }



    public void ResetFormControlDisableEnable(Control parent, bool s)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlDisableEnable(c, s);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Enabled = s;
                        break;

                    case "System.Web.UI.WebControls.DropDownList":

                        ((DropDownList)c).Enabled = s;

                        break;

                    case "System.Web.UI.WebControls.CheckBoxList":

                        ((CheckBoxList)c).Enabled = s;

                        break;




                }
            }
        }



        ddl_invoice_status.Enabled = true;
        txtInvoiceRemarks.Enabled = true;

        txtRaiseDate0.Enabled = true;

    }
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetWorkProposalByid(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            fillgridforTr();


            //txtQualification.Text = ds.Tables[0].Rows[0]["QualificationName"].ToString();
            //txtDecription.Text=ds.Tables[0].Rows[0]["QualificationDescription"].ToString();
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["Ipid"].ToString()).Value;
            ddlIP.Enabled = false;
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["intprjid"].ToString();
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
                ddlstatus.SelectedValue = ddlstatus.Items.FindByValue(ds.Tables[0].Rows[0]["status"].ToString()).Value;

            //if (ddlstatus.SelectedValue == "InProgress" || ddlstatus.SelectedValue == "Closed")
            //{
            //    BtnSave1.Enabled = false;
            //}
            //else
            //{
            //    BtnSave1.Enabled = true;
            //}

            if (ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString() != "")
                txtPDCApprvDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[0].Rows[0]["StatusDate"].ToString() != "")
                txtApprRejDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StatusDate"].ToString()).ToString("dd-MM-yyyy");
            txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

            getFundsDet();

            //txtRaiseDate.Text, txtBillAmount.Text, txtBillRemarks.Text
            if (ddlstatus.SelectedValue == "Rejected")
            {
                Label426.Text = "Rejected Date";
            }
            else
            {
                Label426.Text = "Approved Date";
            }

            getprojectDet();
            txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["MeetingDate"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[0].Rows[0]["intStateid"].ToString() != "")
            {
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["intStateid"].ToString();
                getcounties();
            }
            if (ds.Tables[0].Rows[0]["intCountieid"].ToString() != "")
            {
                ddlCounties.SelectedValue = ds.Tables[0].Rows[0]["intCountieid"].ToString();
                getPayams();
            }
            if (ds.Tables[0].Rows[0]["intPayamid"].ToString() != "")
            {
                ddlPayam.SelectedValue = ds.Tables[0].Rows[0]["intPayamid"].ToString();
                getBomas();
            }
            if (ds.Tables[0].Rows[0]["intBomasid"].ToString() != "")
                ddlBoma.SelectedValue = ds.Tables[0].Rows[0]["intBomasid"].ToString();

            lblworkcode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                gvpractical0.DataSource = ds.Tables[1];
                gvpractical0.DataBind();
                gvpractical0.Columns[0].Visible = false;

            }
            else
            {
                gvpractical0.DataSource = null;
                gvpractical0.DataBind();
            }


            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (ListItem item in chkTst.Items)
                {
                    for (int n = 0; n < ds.Tables[2].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[2].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                            //  CheckBoxList1.Visible = true;
                        }
                    }
                }
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                foreach (ListItem item in ChkCA.Items)
                {
                    for (int n = 0; n < ds.Tables[3].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[3].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                            //  CheckBoxList1.Visible = true;
                        }
                    }
                }
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                foreach (ListItem item in ChkPDC.Items)
                {
                    for (int n = 0; n < ds.Tables[4].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[4].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                            //  CheckBoxList1.Visible = true;
                        }
                    }
                }
            }

            if (ds.Tables[5].Rows.Count > 0)
            {
                foreach (ListItem item in ChkBDC.Items)
                {
                    for (int n = 0; n < ds.Tables[5].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[5].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                            //  CheckBoxList1.Visible = true;
                        }
                    }
                }
            }

        }
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }

    void fillgridforTr()
    {
    
        DataSet ds = Gen.getFundsDetByInvoiceID(hdfInVoiceID.Value.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {

            grdDet.DataSource = ds.Tables[0];
            grdDet.DataBind();

        }
        else
        {

            grdDet.DataSource = null;
            grdDet.DataBind();

        }

        if (ds.Tables[1].Rows.Count > 0)
        {

            grdDet0.DataSource = ds.Tables[1];
            grdDet0.DataBind();

        }
        else
        {

            grdDet0.DataSource = null;
            grdDet0.DataBind();

        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {



        if (cmf.convertDateIndia(txtRaiseDate0.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        {
            lblmsg0.Text = "Date of Approved should be less than the current date.";
            success.Visible = false;
            Failure.Visible = true;
            return;

        }


        if (cmf.convertDateIndia(txtRaiseDate0.Text) < cmf.convertDateIndia(txtRaiseDate.Text))
        {
            lblmsg0.Text = "Approved Date should be greater than the Invoice date. ";
            success.Visible = false;
            Failure.Visible = true;

            return;

        }


        if (hdfID.Value == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Work from Lookup.')", true);
            return;
        }

        DataSet ds = Gen.GetUserPermissionByUser(hdfInVoiceID.Value);
        string Coordinator = "";
        string Manager = ""; 
        string Specialist = "";
        if (ds.Tables[0].Rows.Count  > 0)
        { Coordinator = ds.Tables[0].Rows[0]["CoOrdinator_Id"].ToString();
          Manager= ds.Tables[0].Rows[0]["Manager_Id"].ToString();
          Specialist = ds.Tables[0].Rows[0]["Specialist_Id"].ToString();
        }
        string Status = "";
        string  FromId="";
        string ToId="";
        if (ddl_invoice_status.SelectedValue == "Approved")
        {
            Status = "Sp-Approve";
            FromId = Specialist;
            ToId= Manager;
        }
        else if (ddl_invoice_status.SelectedValue == "Rejected")
        { Status = "Sp-Reject";
            FromId =Specialist;
            ToId = Coordinator;
        }



        string i = Gen.UpdateFinancialStatus(hdfInVoiceID.Value, Status, txtInvoiceRemarks.Text, txtRaiseDate0.Text, Session["uid"].ToString(),FromId,ToId );
        if (i == "Success")
        {
            DataSet dsIp = Gen.GetWorkProposalByid(hdfID.Value.ToString());

            if (dsIp.Tables[0].Rows.Count > 0)
            {

                hdfTSTID.Value = dsIp.Tables[0].Rows[0]["intTST"].ToString();
                hdfTSTEmail.Value = dsIp.Tables[0].Rows[0]["TSTemail"].ToString();
                hdfTSTName.Value = dsIp.Tables[0].Rows[0]["TstName"].ToString();
                hdfWorkCode.Value = dsIp.Tables[0].Rows[0]["WorkCode"].ToString();
                hdfIPEmail.Value = dsIp.Tables[0].Rows[0]["IPemaill"].ToString();
            }

            if (Status == "Sp-Reject")
            {
              SendMailAnotherReject("", hdfWorkCode.Value, Session["username"].ToString());
            }
            else if (Status == "Sp-Approve")
            {
             SendMailAnotherApprv("", hdfWorkCode.Value, Session["username"].ToString());
            }


            lblmsg.Text = "Status Updated Successfully..!";
            success.Visible = true;
            Failure.Visible = false;

            fillgridforTr();
            //clear();
        }

        txtRaiseDate0.Text = "";
        ddl_invoice_status.SelectedIndex = 0;
        //ddlPaymentMode.SelectedIndex = 0;
        //txtChequeNo.Text = "";
        //txtRaiseDate1.Text = "";
        //txtPaidAmt.Text = "";

    }

    public void SendMailAnotherReject(string anothermail, string workcode, string Name)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        string Messge = "";
        //if (Status.ToString() == "Invoice Appovals" || )
        //{

        Messge = "The above invoice has been is rejected. Please review.";
        //}


        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - Notification on Invoice for Review.";

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<br> Work Code: " + workcode.ToString() + "<br> <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + Name.ToString() + "";
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

    
    public void SendMailAnotherApprv(string anothermail, string workcode, string Name)
    {
        
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        string Messge = "";
        //if (Status.ToString() == "Invoice Appovals" || )
        //{

        Messge = "The above invoice has been verified for reimbursement. Please approve.";
        //}


        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - Notification on Invoice for Approval.";

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<br> Work Code: " + workcode.ToString() + "<br> <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + Name.ToString() + "";
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

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSTFinancialApproval.aspx");
        clear();
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        for (int i = 0; i < grdDet.Rows.Count; i++)
        {
            GridViewRow row = grdDet.Rows[i];
            bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
            if (isChecked)
            {
                //string ids = grdDet.DataKeys[i].Value.ToString();
                string ids = grdDet.DataKeys[i].Value.ToString();
                Gen.deleteFinancialTrans(ids);
                cnt = cnt + 1;
            }

            lblmsg.Text = "Deleted Successfully.";
            success.Visible = true;
            Failure.Visible = false;
            //fillgridforTr();
        }
        fillgridforTr();
        if (cnt == 0)
        {
            //lblresult0.Text = " Select Atleast one Record to delete ";

            lblmsg0.Text = "Select Atleast one Record to delete ";
            success.Visible = false;
            Failure.Visible = true;
        }
        else
            //fillNews();
            fillgridforTr();
    }

    void clear()
    {

        grdDet.DataSource = null;
        grdDet.DataBind();

        txtRaiseDate0.Text = "";
        //ddlPaymentMode.SelectedIndex = 0;
        //txtChequeNo.Text = "";
        //txtRaiseDate1.Text = "";
        //txtPaidAmt.Text = "";

        txtRaiseDate.Text = "";
        txtBillAmount.Text = "";
        txtBillRemarks.Text = "";


        trworkcode.Visible = false;
        trTSTApproval.Visible = false;

        txtRemarks.Text = "";
        txtPDCApprvDate.Text = "";
        txtApprRejDate.Text = "";
        ddlstatus.SelectedIndex = 0;

        Gen.getCouncilMems(chkTst, "TST");
        Gen.getCouncilMems(ChkCA, "CA");
        Gen.getCouncilMems(ChkPDC, "PDC");
        Gen.getCouncilMems(ChkBDC, "BDC");
        ddlProject.SelectedIndex = 0;

        lblworkcode.Text = "";
        lblTargetBenefeciaries.Text = "";
        lblCost.Text = "";


        BtnSave1.Text = "Save";
        txtStartDate.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCounties.Items.Clear();
        ddlCounties.Items.Insert(0, "--Select--");
        ddlCounties.SelectedIndex = 0;

        ddlPayam.Items.Clear();
        ddlPayam.Items.Insert(0, "--Select--");
        ddlPayam.SelectedIndex = 0;

        ddlBoma.Items.Clear();

        ddlBoma.Items.Insert(0, "--Select--");
        ddlBoma.SelectedIndex = 0;
        //BtnDelete.Visible = false;

        gvpractical0.DataSource = null;
        gvpractical0.DataBind();
    }



    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");

        }
    }

    void getPayams()
    {
        ddlPayam.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
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
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        getprojectDet();
    }
    void getprojectDet()
    {
        DataSet ds = new DataSet();
        if (ddlProject.SelectedValue != "--Select--")
        {
            ds = Gen.GetProjectDetbyId(ddlProject.SelectedValue);
            lblTargetBenefeciaries.Text = ds.Tables[0].Rows[0]["TargetBeneficiary"].ToString();
            lblCost.Text = ds.Tables[0].Rows[0]["ProjectCost"].ToString();

        }
        else
        {
            lblTargetBenefeciaries.Text = "";
            lblCost.Text = "";
        }
    }

    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIP.SelectedValue != "--Select--")
        {
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
        }
        else
        {
            ddlProject.Items.Clear();
            ddlProject.Items.Insert(0, "--Select--");
        }

    }


    void getFundRaiseDet()
    {
        DataSet ds = Gen.GetProjectDetbyId(hdfID.Value);
        lblTargetBenefeciaries.Text = ds.Tables[0].Rows[0]["TargetBeneficiary"].ToString();
        lblCost.Text = ds.Tables[0].Rows[0]["ProjectCost"].ToString();


    }

    void getFundsDet()
    {
        DataSet ds = Gen.getFundsDetByInvoiceID(hdfInVoiceID.Value.ToString());
        if (ds.Tables[1].Rows.Count > 0)
        {
            txtRaiseDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["FundRaiseDate"].ToString()).ToString("dd-MM-yyyy");
            txtBillAmount.Text = ds.Tables[1].Rows[0]["FundRaiseAmount"].ToString();
            txtBillRemarks.Text = ds.Tables[1].Rows[0]["Remarks"].ToString();

            //if (ds.Tables[0].Rows[0]["ApprovedDate"].ToString() != "")
            //    txtRaiseDate0.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ApprovedDate"].ToString()).ToString("dd-MM-yyyy");
            //if (ds.Tables[0].Rows[0]["PaymentMode"].ToString() != "")
            //    ddlPaymentMode.SelectedValue = ddlPaymentMode.Items.FindByValue(ds.Tables[0].Rows[0]["PaymentMode"].ToString()).Value;
            //txtChequeNo.Text = ds.Tables[0].Rows[0]["TransactionNo"].ToString();
            //if (ds.Tables[0].Rows[0]["PaymentDate"].ToString() != "")
            //    txtRaiseDate1.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PaymentDate"].ToString()).ToString("dd-MM-yyyy");
            //txtPaidAmt.Text = ds.Tables[0].Rows[0]["PaidAmt"].ToString();

        }
    }
    protected void ddl_invoice_status_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_invoice_status.SelectedValue == "Rejected")
        {
            Label433.Text = "Rejected Date";
            trRemarks.Visible = true;
        }
        else
        {
            Label433.Text = "Approved Date";
            trRemarks.Visible = false;
        }
    }

    protected void grdDet0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            decimal FundRaiseAmount = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "FundRaiseAmount"));
            FundRaiseAmount1 = FundRaiseAmount + FundRaiseAmount1;

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[2].Text = FundRaiseAmount1.ToString();
            hdftot.Value = FundRaiseAmount1.ToString();

        }
    }
}
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
#region Amendment History Begin
//Form Name : Meterail Reuest
//Purpose : To Insert Meterail Reuest
// Tabels Used: tr_FundsRaising,tbl_WorkMaster,tm_States,tm_Counties,tm_Payams,tm_Bomas,tr_FinancialApproval
//Stored procedures Used : UpdFundRaising,sp_getCounties,sp_getPayams,getBomasbyID,getOfficers,sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,getFundsDet,GetProjectsbyIP
#endregion Amendment History End
public partial class IPRaiseFunds : System.Web.UI.Page
{

    #region reference class calling and Variable declaration
    int delete = 0;
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    DataRow dtrdr;
    DataTable myDtNewRecdr = new DataTable();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {


        // Look up Click Function Intialization
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        #region Is not Postback Statement
        if (!IsPostBack)
        {
            ResetFormControlDisableEnable(this, false);

            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
                                 


            Gen.getCouncilMems(chkTst,"TST");
            Gen.getCouncilMems(ChkCA, "CA");
            Gen.getCouncilMems(ChkPDC, "PDC");
            Gen.getCouncilMems(ChkBDC, "BDC");
            PopulateCoordinator();
            PopulateSpecialist();
            PopulateManager();
            //lblIP.Text = Session["username"].ToString();

            if (Request.QueryString["invoice"] != null && Request.QueryString["WorkId"] != null)
            {
                hdfFlagID.Value = "0";
                hdfInVoiceID.Value = Request.QueryString["invoice"].ToString();
                hdfID.Value = Request.QueryString["WorkId"].ToString();
            }
           

        }
        #endregion

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            trTSTApproval.Visible = true;
            
            trworkcode.Visible = true;
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            if (Session["userlevel"].ToString() == "1")
            {
                BtnDelete.Visible = true;
            }
            else
            {
                BtnDelete.Visible = false;
            }
            BtnSave1.Text = "Update";
        }
        #endregion
    }
    #endregion

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
    private void PopulateCoordinator()
    {
        DataSet ds = Gen.GetCoordinators();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlCoordinator.DataSource = ds.Tables[0];
                ddlCoordinator.DataValueField = "intUserid";
                ddlCoordinator.DataTextField = "User_id";
                ddlCoordinator.DataBind();
                ddlCoordinator.SelectedIndex = 0;
            }
        }
    }

    private void PopulateSpecialist()
    {
        DataSet ds = Gen.GetSpecialist();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlSpecialist.DataSource = ds.Tables[0];
                ddlSpecialist.DataValueField = "intUserid";
                ddlSpecialist.DataTextField = "User_id";
                ddlSpecialist.DataBind();
                ddlSpecialist.SelectedIndex = 0;
            }
        }
    }

    private void PopulateManager()
    {
        DataSet ds = Gen.GetManager();
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlManger.DataSource = ds.Tables[0];
                ddlManger.DataValueField = "intUserid";
                ddlManger.DataTextField = "User_id";
                ddlManger.DataBind();
                ddlManger.SelectedIndex = 0;
            }
        }
    }
    #endregion

    #region Reset Form Controls
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

        txtRaiseDate.Enabled = true;
        txtBillAmount.Enabled = true;
        txtBillRemarks.Enabled = true;
        chkTst.Enabled = false;
        ChkCA.Enabled = false;
        ChkPDC.Enabled = false;
        ChkBDC.Enabled = false;
    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetWorkProposalByid(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
           
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["Ipid"].ToString()).Value;
            ddlIP.Enabled = false;
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["intprjid"].ToString();
            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
            ddlstatus.SelectedValue = ddlstatus.Items.FindByValue(ds.Tables[0].Rows[0]["status"].ToString()).Value;

          


            fillgridforTr();

            if (ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()!="")
            txtPDCApprvDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[0].Rows[0]["StatusDate"].ToString() != "")
            txtApprRejDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StatusDate"].ToString()).ToString("dd-MM-yyyy");
            txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();

            getFundsDet();

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
    #endregion

    #region Button Click functionalities Save, Update,Clear, Delete, Look up
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (hdfInVoiceID.Value == "")
        {

            if (cmf.convertDateIndia(txtRaiseDate.Text) < cmf.convertDateIndia(txtApprRejDate.Text))
            {
                lblmsg0.Text = "Bill Invoice Date should be greater than the Approved date. ";
                success.Visible = false;
                Failure.Visible = true;

                return;

            }

            if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
            {
                lblmsg0.Text = "Bill Invoice Date should be less than the current date.";
                success.Visible = false;
                Failure.Visible = true;
                return;

            }

            if (hdfID.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Work from Lookup.')", true);
                return;
            }
            if (ddlstatus.SelectedValue == "New Proposal" || ddlstatus.SelectedValue == "Rejected")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approved Denied.')", true);
                return;
            }

            int i = Gen.UpdFundRaising(hdfID.Value, txtRaiseDate.Text, txtBillAmount.Text, txtBillRemarks.Text, Session["uid"].ToString(), ddlCoordinator.SelectedValue, ddlSpecialist.SelectedValue, ddlManger.SelectedValue);
            if (i > 0)
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
                if (ddlstatus.SelectedValue == "Approved")
                {
                    SendMailAnotherApprv(hdfWorkCode.Value, ddlstatus.SelectedValue, hdfTSTName.Value);
                }

                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }

            fillgridforTr();
            txtRaiseDate.Text = "";
            txtBillAmount.Text = "";
            txtBillRemarks.Text = "";
            lblmsg.Text = "Updated Successfully..!";
            success.Visible = true;
            Failure.Visible = false;
        }
        else
        {
            if (cmf.convertDateIndia(txtRaiseDate.Text) < cmf.convertDateIndia(txtApprRejDate.Text))
            {
                lblmsg0.Text = "Bill Invoice Date should be greater than the Approved date. ";
                success.Visible = false;
                Failure.Visible = true;

                return;

            }

            if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
            {
                lblmsg0.Text = "Bill Invoice Date should be less than the current date.";
                success.Visible = false;
                Failure.Visible = true;
                return;

            }

            if (hdfID.Value == "")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please select Work from Lookup.')", true);
                return;
            }
            if (ddlstatus.SelectedValue == "New Proposal" || ddlstatus.SelectedValue == "Rejected")
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Approved Denied.')", true);
                return;
            }
            int i = Gen.UpdFundRaisingUsingInvoiceId(hdfID.Value, txtRaiseDate.Text, txtBillAmount.Text, txtBillRemarks.Text, Session["uid"].ToString(), ddlCoordinator.SelectedValue, ddlSpecialist.SelectedValue, ddlManger.SelectedValue,hdfInVoiceID.Value );
            if (i > 0)
            {

                

                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }

            fillgridforTr();
            txtRaiseDate.Text = "";
            txtBillAmount.Text = "";
            txtBillRemarks.Text = "";
            lblmsg.Text = "Updated Successfully..!";
            success.Visible = true;
            Failure.Visible = false;

        }
    }



    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("IPRaiseFunds.aspx");

        clear();
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

    }

    void clear()
    {
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
        BtnDelete.Visible = false;

        gvpractical0.DataSource = null;
        gvpractical0.DataBind();
    }



    #endregion

    #region Mail Sending Functions

    public void SendMailAnotherApprv(string workcode, string Status,string Name)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        string anothermail="";
        anothermail = "";
        anothermail = System.Configuration.ConfigurationManager.AppSettings["TwoEmailRecipients"];
        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

       
        string Messge = "";
        if (Status.ToString() == "Approved")
        {

            Messge = "The above invoice has been is submitted for reimbursement. Please review.";
        }


        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS – Notification on Invoice Submitted.";// Session["user_id"].ToString();

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body =  "<br> Work Code: " + workcode.ToString() + "<br> <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + ddlIP.SelectedItem.Text+"";
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

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
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
        DataSet ds = Gen.getFundsDet(hdfID.Value);
        if(ds.Tables[0].Rows.Count>0)
        {

            txtRaiseDate.Text = Convert.ToDateTime(ds.Tables[1].Rows[0]["FundRaiseDate"].ToString()).ToString("dd-MM-yyyy");
            txtBillAmount.Text = ds.Tables[1].Rows[0]["FundRaiseAmount"].ToString();
            txtBillRemarks.Text = ds.Tables[1].Rows[0]["Remarks"].ToString(); 
        
        }
    }

    void fillgridforTr()
    {

        DataSet ds = Gen.getFundsDet(hdfID.Value.ToString());
        if (ds.Tables[1].Rows.Count > 0)
        {

            grdDet.DataSource = ds.Tables[1];
            grdDet.DataBind();

        }
        else
        {

            grdDet.DataSource = null;
            grdDet.DataBind();

        }
    }
    #endregion
}

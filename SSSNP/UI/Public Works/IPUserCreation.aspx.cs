#region Using/Inherits Classess
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
using System.Text.RegularExpressions;
#endregion

#region Amendment History Begin
//Form Name : IP User Creation
//Purpose : To Add IP User Creation Details
//Tables used : td_IPDetails,tbl_Users
//Stored procedures Used : GetIPUserCreationLookup,getIpDet,sp_InsUpdIPUserCreation
#endregion Amendment History End

public partial class IPUserCreation : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    General Gen = new General();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // Look up Click Function Intialization
        this.btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        // Is not Postback Statement
        if (!IsPostBack)
        {
            Gen.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
          
            trPass1.Visible = true;
            trPass.Visible = true;

        }
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }
    }
    #endregion

    #region fill details Calling using Lookup
    void FillDetails()
    {
        string ids = "";
        if (Request.QueryString.Count > 0)
        {
            ids = Request.QueryString[0].ToString();
        }
        else
        {
            ids = hdfID.Value;
        }
        hdfFlagID.Value = "1";
        DataSet ds = Gen.GetIPUserCreationLookup("", ids);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            txtuser.Text = ds.Tables[0].Rows[0]["User_id"].ToString();
            txtOfficerName.Text = ds.Tables[0].Rows[0]["User_name"].ToString();
            Gen.getIpDet(ddlIP);
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["User_Code"].ToString()).Value;
            txtuser.Enabled = false;

            trPass1.Visible = false;
            trPass.Visible = false;
        }
    }
    #endregion

    #region Button Click functionalities Save, Update,Clear
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

    }

    private bool ValidatePassword(string password, out string ErrorMessage)
    {
        var input = password;
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new Exception("Password should not be empty");
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,15}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasUpperChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasMiniMaxChars.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasNumber.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }

        else if (!hasSymbols.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else
        {
            return true;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {

        Failure.Visible = false;


        string message = "";
        if (!ValidatePassword(txtpass.Text, out message))
        {
            Failure.Visible = true;
            lblmsg0.Text = message;
            return;
        }

        string passwd = "";
        if (!String.IsNullOrEmpty(txtpass.Text))
            passwd = Gen.CalculateMD5Hash(txtpass.Text);

        int i = Gen.InsUpdIPUserCreation(hdfID.Value, ddlIP.SelectedValue, txtOfficerName.Text, txtmail.Text, DdlActive.SelectedValue, txtuser.Text, passwd, DdlRole.SelectedValue, Session["uid"].ToString());
        if (i > 999)
        {
            lblmsg.Text = "Added Successfully..!";
            success.Visible = true;
            Failure.Visible = false;
            if (txtmail.Text.Trim().ToString() != "")
            {
                SendMailAnother(txtmail.Text.Trim().ToString());
            }
            clear();
        }
        else if (i == 100)
        {
            lblmsg.Text = "Updated Successfully..!";
            success.Visible = true;
            Failure.Visible = false;
            clear();
        }
        else if (i == 1)
        {
            lblmsg0.Text = "User id Already in Use Try Another..!";
            success.Visible = false;
            Failure.Visible = true;
            txtuser.Focus();
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("IPUserCreation.aspx");
    }
    #endregion

    #region Clear Function
    void clear()
    {
        BtnSave1.Text = "Save";

        txtOfficerName.Text = "";
        txtmail.Text = "";

        txtuser.Text = "";
        txtpass.Text = "";
        txtuser.Enabled = true;
        txtpass.Enabled = true;
        trPass1.Visible = true;
        trPass.Visible = true;


    }
    #endregion
    #region Mail Send Functionalities
    public void SendMailAnother(string anothermail)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - IP Login Credentials -" + Session["user_id"].ToString();

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "Dear " + txtOfficerName.Text + "<br><br>  <H2>SSSNP Public Works MIS - IP Login Credentials</H2><br> <b> NAME: " + txtOfficerName.Text + "</b> <br><br> USER ID: " + txtuser.Text + "<br> <br> Password : " + txtpass.Text + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>t ";
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


}

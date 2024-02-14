
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
//Form Name : IP Registration
//Purpose : To Add IP Details and Creation of IP user
//Tables used : td_IPDetails,tbl_Users
//Stored procedures Used : getIPSearch,getIPbyID,deleteIP,insrtIP
#endregion Amendment History End


public partial class TSTIPregistration : System.Web.UI.Page
{

    #region reference class calling
    General Gen = new General();
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

            // Passing IP ID through  Request QueryString fill IP Details to Controls
            if (Request.QueryString.Count > 0)
            {
                Failure.Visible = false;
                success.Visible = false;
                FillDetails();
                if (Session["userlevel"].ToString() == "3")
                {
                    BtnDelete.Visible = true;
                }
                else
                {
                    BtnDelete.Visible = false;
                }
                BtnSave1.Text = "Update";
            }
            
        }
        #endregion
        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0" ))
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            if (Session["userlevel"].ToString() == "3")
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

    #region Fill Details function in fields using Lookup
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
        DataSet ds = new DataSet();


        ds = Gen.getIPbyID(ids.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {


            txtip.Text = ds.Tables[0].Rows[0]["IPName"].ToString();
            ddltypeip.SelectedValue = ddltypeip.Items.FindByValue(ds.Tables[0].Rows[0]["TypeofIP"].ToString()).Value;
            txtauthorised.Text = ds.Tables[0].Rows[0]["AuthorisedPerson"].ToString();
            txtmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();         
            txtcontact1.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            txtauthorised0.Text = ds.Tables[0].Rows[0]["SecondaryContactNo"].ToString();
            txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtauthorised0.Text = ds.Tables[0].Rows[0]["SecondaryAuthorisedPerson"].ToString();

            txtScontact.Text = ds.Tables[0].Rows[0]["SPerson"].ToString();
            txtSEmail.Text = ds.Tables[0].Rows[0]["SEmail"].ToString();

            txtweb.Text = ds.Tables[0].Rows[0]["Website"].ToString();
            txtuser.Text = ds.Tables[1].Rows[0]["User_id"].ToString();
            txtuser.Enabled = false;
            txtpass.Enabled = false;
            txtpass.TextMode = TextBoxMode.SingleLine;
            txtpass.Text = "*****"; 
            
          



        }
    }
    #endregion

    #region Button Click functionalities Save, Update, Delete, Look up
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    // Delete Button Function
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteIP(hdfID.Value);

        if (i == 1)
        {
            lblmsg.Text = "Deleted Successfully.";
            success.Visible = true;
            Failure.Visible = false;
            clear();
        }
        else if (i == 3)
        {
            lblmsg0.Text = "Not Possible.";
            success.Visible = false;
            Failure.Visible = true;
            clear();
        }
        else
        {
            lblmsg0.Text = "Please contact to Administrator.";
            success.Visible = false;
            Failure.Visible = true;
            clear();
        }
    }
    // Clear Button Function
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSTIPregistration.aspx");
    }
    // Save Button Function

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

        string ids = "";
        if (Request.QueryString.Count > 0)
        {
            ids = Request.QueryString[0].ToString();
        }
        else
        {
            ids = hdfID.Value;
        }

        string message = "";
        if (!ValidatePassword(txtpass.Text, out message))
        {
            Failure.Visible = true;
            lblmsg0.Text = message;
            return;
        }

        Gen.IPName = txtip.Text;
        Gen.TypeofIP = ddltypeip.SelectedValue;
        Gen.AuthorisedPerson = txtauthorised.Text;
        Gen.ContactNo = txtcontact1.Text;
        Gen.SecondaryContactNo = txtauthorised0.Text;
        Gen.Email = txtmail.Text;
        Gen.Address = txtaddress.Text;
        Gen.Website = txtweb.Text;
        Gen.SecondaryAuthorisedPerson = txtauthorised0.Text;

        


        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {

            DataSet dsUser = new DataSet();
            dsUser = Gen.CheckUserid(txtuser.Text.Trim());
            //
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                Failure.Visible = true;
                lblmsg0.Text = " User ID already Exists, Please specify another User ID.";
                txtuser.Text = "";
                txtpass.Text = "";
                return;
               
            }

            string passwd = "";
            if (!String.IsNullOrEmpty(txtpass.Text))
                passwd = Gen.CalculateMD5Hash(txtpass.Text);

            int i = Gen.insrtIP("0", Session["uid"].ToString(), txtuser.Text, passwd, Session["User_Code"].ToString(), txtScontact.Text, txtSEmail.Text);
            if (i != 999)
            {
                SendMailAnother(txtmail.Text);


                lblmsg.Text = "Added Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
            }
        }
        else
        {
            string passwd = "";
            if (!String.IsNullOrEmpty(txtpass.Text))
                passwd = Gen.CalculateMD5Hash(txtpass.Text);

            int i = Gen.insrtIP(ids.ToString(), Session["uid"].ToString(), txtuser.Text, passwd, Session["User_Code"].ToString(), txtScontact.Text, txtSEmail.Text);

            if (i != 999)
            {
                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
               
            }
        }
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
        mail.Body = "Dear " + txtip.Text + "<br><br>  <H2>SSSNP Public Works MIS - IP Login Credentials</H2><br> <b> NAME: " + txtip.Text + "</b> <br><br> USER ID: " + txtuser.Text + "<br> <br> Password : " + txtpass.Text + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>t ";
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

    #region Clear Function
    void clear()
    {
        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;
        txtauthorised0.Text = "";
        txtScontact.Text = ""; txtSEmail.Text = "";
       
        txtip.Text = "";
        ddltypeip.SelectedIndex = 0;
     
        txtauthorised.Text = "";
        txtmail.Text = "";
        txtauthorised0.Text = "";
        txtcontact1.Text = "";
        txtaddress.Text = "";
        txtweb.Text = "";
        txtuser.Text = "";
        txtpass.Text = "";
        txtuser.Enabled = true;
        txtpass.Enabled = true;
       
        
    }
    #endregion
   
}

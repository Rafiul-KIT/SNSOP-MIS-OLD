
//************************** This interface is created by Ravi for helpdesk ******************************
// ***********************************  Date  :  14-05-2012  *********************************
/// Software Engineer: R.Ravichandra
/// Created By R.Ravi Chandra on 14-05-2012 at 12.20PM For Inserting Help Desk -, StoredProcedure name:sp_Instfeedback.
/// 
/// 
/// 



using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.IO;

public partial class Helpdesk : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    comFunctions fssFunctions = new comFunctions();
    General cmn = new General();
    /// <summary>
    ///Begin  Created By Ravi Chandra on 14-05-2012 at 12.20PM For Help Desk Declaration 
    /// </summary>
    string mobPIA = String.Empty;
    /// <summary>
    ///End  Created By Ravi Chandra on 14-05-2012 at 12.20PM For Help Desk Declaration
    /// </summary>
    string piaid = String.Empty;
    string trCode = String.Empty;
    string mppia = String.Empty;
    DataSet ds;
    byte[] scanUpload = new byte[36871];
    //string scanUploadFileName = "";
    byte[] letterUpload = new byte[36871];
    //string letterUploadFileName = "";



    /// <summary>
    /// Creaeted By Srikanth Regarding Generation Number:
    /// 
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// 
    string generateid;
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        try
        {
            //Begin Created By Srikanth on 18-5-2012 
            this.BtnSave.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave, "group"));
            //Begin Created By Srikanth on 18-5-2012 
            if (Session.Count > 0)
            {

                // Modified by Srikanth on 30-08-2012 for Help Desk Registration to MORD Team
                if (Session["user_type"].ToString().Trim() == "Head")
                {
                    Label57.Text = Session["User_Code"].ToString().Trim();
                    DataSet ds = cmn.GetPiaName(Session["User_Code"].ToString());
                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Label57.Text = ds.Tables[0].Rows[0]["TstName"].ToString();
                        //Label58.Text = ds.Tables[0].Rows[0]["piaIncharge"].ToString();
                        Label58.Text = "Cap Foundation";

                    }
                }
                else if (Session["user_type"].ToString().Trim() == "MORD")
                {
                    Label57.Text = Session["user_id"].ToString().Trim();
                    Label58.Text = Session["username"].ToString().Trim();
                }
                else
                {
                    Label57.Text = Session["user_id"].ToString().Trim();
                    Label58.Text = Session["username"].ToString().Trim();
                }
                // Modified by Srikanth on 30-08-2012 for Help Desk Registration to MORD Team
            }

            if (txtupload.PostedFile != null && txtupload.PostedFile.ContentLength > 0 && !clsGeneral.IsExecutable(txtupload.PostedFile))
                UploadFile();

            if (!IsPostBack)
            {

                Session["letterUpload"] = letterUpload;
                Session["letterUploadFileName"] = "";

                //Session["scanUpload"] = scanUpload;
                //Session["scanUploadFileName"] = "";
                cmn.getfeedback(ddlfeedback);


            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion


    #region Button Disable By Srikanth on 17-5-2012
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


    #endregion

    #region Upload File
    private void UploadFile()
    {

        Session["letterUploadFileName"] = System.IO.Path.GetFileName(txtupload.PostedFile.FileName);


        try
        {
            string[] fileType = txtupload.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;
            Session["letterUpload"] = new byte[txtupload.PostedFile.ContentLength];
            if (fileType[letterI - 1].ToUpper().Trim() == "PDF" || fileType[letterI - 1].ToUpper().Trim() == "DOC" || fileType[letterI - 1].ToUpper().Trim() == "JPG" || fileType[letterI - 1].ToUpper().Trim() == "GIF" || fileType[letterI - 1].ToUpper().Trim() == "JPEG" || fileType[letterI - 1].ToUpper().Trim() == "XLS" || fileType[letterI - 1].ToUpper().Trim() == "XLSX" || fileType[letterI - 1].ToUpper().Trim() == "DOCX")
            {
                //   letterUpload = new byte[fupLetter.PostedFile.ContentLength];

                // Session["letterUpload"] = new byte[fupLetter.PostedFile.ContentLength];
                HttpPostedFile Image = txtupload.PostedFile;
                Image.InputStream.Read((byte[])Session["letterUpload"], 0, (int)txtupload.PostedFile.ContentLength);
                lblScanLetter.Text = Session["letterUploadFileName"].ToString().Trim();

            }

        }
        catch (Exception ex)
        {

            lblmsg0.Text = "An Error Occured. Please Try Again!" + ex.Message;
            success.Visible = false;
            Failure.Visible = true;

        }

        finally
        {

        }
    }
    #endregion

    #region Resetfromcontrol
    public void Resetfromcontrol(Control parent)
    {
        try
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Controls.Count > 0)
                {
                    Resetfromcontrol(c);
                }
                else
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            ((TextBox)c).Text = "";
                            break;

                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region OnPreRender
    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);

            StringBuilder javaScript = new StringBuilder();
            javaScript.Append("\n<script language=JavaScript>\n");
            javaScript.Append("window.history.forward(1);\n");
            javaScript.Append("</script>\n");

            Page.RegisterClientScriptBlock("HistoryScript", javaScript.ToString());
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region Btnclear

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Helpdesk.aspx");
    }
    #endregion

    #region Clear
    public void clear()
    {
        ddlfeedback.SelectedValue = "--Select--";
        txtsubjet.Text = "";
        lblScanLetter.Text = "";
    }
    #endregion

    #region BtnSave
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            string intPIAid = "";
            if (Session["user_type"].ToString().Trim() == "MORD")
            {

                intPIAid = Session["uid"].ToString().Trim();
            }
            else
            {
                intPIAid = Session["user_code"].ToString().Trim();
            }
            int i = 1;
            int ij = 0;
            int ji = 0;
            string hdCode = clsGeneral.getHelpDeskID(intPIAid).ToString();
            string newhdCode;


            int number;

            if (hdCode == "")
            {
                newhdCode = Label57.Text.Trim() + "-001";
            }
            else
            {

                generateid = hdCode.ToString();
                string extract = "";
                extract = hdCode.ToString();

                if (extract.Contains("-") == true)
                {

                    string[] words = generateid.Split('-');
                    if (words[words.Length - 1].Length > 3)
                        generateid = words[words.Length - 1].Trim();
                    else
                        generateid = extract.Substring(extract.Length - 3);
                }
                else
                {
                    generateid = generateid + 1;
                }
                if (generateid != "")
                {
                    number = Convert.ToInt32(generateid.Trim()) + 1;


                    if (number.ToString().Length == 1)
                    {
                        generateid = "00" + number;
                    }
                    else if (number.ToString().Length == 2)
                    {
                        generateid = "0" + number;
                    }
                    else
                    {
                        generateid = number.ToString();
                    }

                }
                newhdCode = Label57.Text.Trim() + "-" + generateid;
            }







            string hd_Username = Label58.Text;
            string intfb_id = ddlfeedback.SelectedValue.ToString();
            string hd_desc = txtsubjet.Text.ToString();
            string Created_by = Session["User_id"].ToString();

            int returnValue = cmn.insertFeedBack(intPIAid, hd_Username, intfb_id, hd_desc, Session["letterUploadFileName"].ToString().Trim(), (byte[])(Session["letterUpload"]), Session["user_type"].ToString(), Session["uid"].ToString(), newhdCode, txtemail.Text.Trim());
            if (returnValue == -1)
            {
                string mesg1 = "Successfully Registered - Ref. No : " + newhdCode.Trim();
        
                if (txtemail.Text != "")
                    SendMailAnother(newhdCode, txtemail.Text.Trim());


                Session.Remove("letterUploadFileName");
                Session.Remove("letterUpload");

                Response.Redirect("FrmHDResult.aspx?Msg=" + mesg1);
                //  ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mesg1 + "');", true);
                Resetfromcontrol(this);

                //lblresult.Text = "You succefully Entered the HelpDesk ";
                clear();

            }
            else
            {
                string mesg1 = "Contact admistrator";
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mesg1 + "');", true);
            }
        }

        catch (Exception ex)
        {

        }
        finally
        {
        }


    }

    #region Mail Sendings Created By Srikanth

    public void SendMail(string numbers)
    {
        // It seems not in use. MVV

        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string supportmail = System.Configuration.ConfigurationManager.AppSettings["SUPPORTMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        string to = supportmail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
  
        mail.From = new MailAddress(from, ":: CAP Foundation  MIS ::", System.Text.Encoding.UTF8);
        if (Session["user_type"].ToString().Trim() == "MORD")
        {
            mail.Subject = "CAP Foundation  - Help Desk Registration from  -" + Label57.Text;
        }
        else
        {
            mail.Subject = "CAP Foundation  - Help Desk Registration From " + Label57.Text;
        }
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>CAP Foundation  MIS - Help Desk Registration</H2><br><br>To : FIRM,  <br> <br> <b> NAME: " + Label57.Text + "</b> <br><br> USER NAME: " + Label58.Text + "<br> Feed Back Type: " + ddlfeedback.SelectedItem.Text + " <br> <br> Our  Comments : " + txtsubjet.Text + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> Please Rectify this as early as possible<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        object a = (byte[])Session["File1"];
        mail.Attachments.Add(new Attachment(new MemoryStream((byte[])Session["letterUpload"]), Session["letterUploadFileName"].ToString()));
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

        try
        {
            client.Send(mail);
            //Session.Remove("letterUpload");
            //Session.Remove("letterUploadFileName");
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
            HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }    

    public void SendMailAnother(string numbers, string anothermail)
    {

        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SUPPORTMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);

        mail.From = new MailAddress(from, ":: SSSNP Public works MIS ::", System.Text.Encoding.UTF8);
        if (Session["user_type"].ToString().Trim() == "MORD")
        {
            mail.Subject = "SSSNP Public works  - MIS Help Desk Registration From " + Label57.Text;
        }
        else
        {
            mail.Subject = "SSSNP Public works  - MIS Help Desk Registration From " + Label57.Text;
        }
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>SSSNP Public works MIS Help Desk Registration</H2><br><br>To : FIRM,  <br> <br> <b> NAME: " + Label57.Text + "</b> <br><br> USER NAME: " + Label58.Text + "<br> Feed Back Type: " + ddlfeedback.SelectedItem.Text + " <br> <br> Our  Comments : " + txtsubjet.Text + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> Please Rectify this as early as possible<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        object a = (byte[])Session["File5"];
        mail.Attachments.Add(new Attachment(new MemoryStream((byte[])Session["letterUpload"]), Session["letterUploadFileName"].ToString()));
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
            HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }

    #endregion

    #endregion
}

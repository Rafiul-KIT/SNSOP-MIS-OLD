

using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.Configuration;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Data.SqlClient;

/// <summary>
/// Summary description for common
/// </summary>
public class comFunctions
{
       
    public comFunctions()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    #region ClearControls
    public void ResetFormControl(Control parent)
    {


        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControl(c);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Text = "";
                        break;

                    case "System.Web.UI.WebControls.DropDownList":

                        if (((DropDownList)c).Items.Count > 0)
                        {
                            ((DropDownList)c).SelectedIndex = 0;
                        }
                        break;
                }
            }
        }
    }
    #endregion

    #region converting dd/mm/yyyy to mm/dd/yyyy

    public DateTime convertDateIndia(string datetoconvert)
    {
        DateTime dtParam;
        System.Globalization.CultureInfo enGB = new System.Globalization.CultureInfo("en-GB");
        
        try
        {
            dtParam = Convert.ToDateTime(datetoconvert.ToString(), enGB);
            return dtParam;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

       
        }


    }

    #endregion

    #region Send Mail Message
    public bool sendMail(string mailID, string srvMailID, string srvMailPassword, string dispName, string mailSubject, string smtpHost, string mailDescription, string port)
    {
        bool sent = false;

        try
        {
            string frmAddress = srvMailID.ToString();
            string toAddress = mailID.ToString();
            
            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

            mail.To.Add(toAddress);

            mail.Bcc.Add("");
            mail.From = new System.Net.Mail.MailAddress(srvMailID, dispName.ToString(), System.Text.Encoding.UTF8);
            mail.Subject = mailSubject.ToString();
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = mailDescription.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();

            Client.Credentials = new System.Net.NetworkCredential(frmAddress.ToString(), srvMailPassword.ToString());
            Client.Port = Convert.ToInt32(port);
            Client.Host = smtpHost.ToString();
            Client.EnableSsl = true;

            Client.Send(mail);
            sent = true;

        }
        catch (Exception ex)
        {
            sent = false;
        }
        finally
        {
            
        }
        return sent;
    }

    #endregion

    #region sending SMS
    public string sendSMS(string strReceip, string strMsg)
    {
        try
        {

            Uri objUri = new Uri("" + HttpUtility.UrlEncode(strMsg) + "&PhoneNumber=" + strReceip);
            WebRequest objWebRequest = WebRequest.Create(objUri);
            WebResponse objWebResponse = objWebRequest.GetResponse();
            Stream objStream = objWebResponse.GetResponseStream();
            StreamReader objStreamReader = new StreamReader(objStream);
            string strHTML = objStreamReader.ReadToEnd();
            return strHTML;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
    }
    #endregion

    #region Disable the Button

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

    #region for Helpdesk
    public bool sendMailwithatt(string mailID, string srvMailID, string srvMailPassword, string dispName, string mailSubject, string smtpHost, string mailDescription, string port, string filename, byte[] attchedfile)
    {
        bool sent = false;

        try
        {
            string frmAddress = srvMailID.ToString();
            string toAddress = mailID.ToString();


            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            
            mail.To.Add(toAddress);
            
            mail.Bcc.Add("");
            mail.From = new System.Net.Mail.MailAddress(srvMailID, dispName.ToString(), System.Text.Encoding.UTF8);
            mail.Subject = mailSubject.ToString();
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = mailDescription.ToString();
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            if (filename != "")
                mail.Attachments.Add(new Attachment(new MemoryStream(attchedfile), filename));
            mail.IsBodyHtml = true;
            mail.Priority = System.Net.Mail.MailPriority.High;

            System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient();

            Client.Credentials = new System.Net.NetworkCredential(frmAddress.ToString(), srvMailPassword.ToString());
            Client.Port = Convert.ToInt32(port);
            Client.Host = smtpHost.ToString();
            Client.EnableSsl = true;

            Client.Send(mail);
            sent = true;

        }
        catch (Exception ex)
        {
            sent = false;
        }
        finally
        {

        }
        return sent;
    }
 #endregion
  
}
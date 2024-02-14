using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;

public partial class Errorpages_oops : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public void SendMailAnother(string anothermail)
    {
        // It seems not in use. MVV

        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.CC.Add("");
        mail.CC.Add("");

        
        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - Custom error page From -" + Session["user_id"].ToString();
        
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>SSSNP Public Works MIS - Custom error page</H2><br><br>To   <br> <br> <b> NAME: " + Session["username"].ToString() + "</b> <br><br> USER ID: " + Session["user_id"].ToString() + "<br> <br> Password : " + Session["password"].ToString() + "<br> <br> Error Page URL: " + Request.QueryString[0].ToString() + " <br> <br> Please Rectify this as early as possible<br><br>Thanks & Regards<br> ";
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
}

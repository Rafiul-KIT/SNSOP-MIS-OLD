using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net.Mail;

//designing and developed by suresh as on 12-1-2016
//Purpose : To Add Implementaion Patner Details
//Tables used : td_IPDetails
//Stored procedures Used :getIPbyID,deleteIP,insrtIP


public partial class AutoAlertMonthlyIPFundsDisbursementReport : System.Web.UI.Page
{
    General Gen = new General();

    protected void Page_Load(object sender, EventArgs e)
    {
 



        
        //if (Session.Count <= 0)
        //{
        //    Response.Redirect("../../frmUserLogin.aspx");
        //}
      
        if (!IsPostBack)
        {


          
            ReportViewer1.LocalReport.EnableHyperlinks = true;

            GetAlerts();

        }

      
    }


    public void GetAlerts()
    {
        int Month = 0;
        int Year = 0;
        //try { Month = Convert.ToInt32(System.DateTime.Now.Month); }
        try { Month = Convert.ToInt32("3"); }
        catch { }
        try { Year = Convert.ToInt32(System.DateTime.Now.Year); }
        catch { }
        DataTable dt = new DataTable();
        General gen = new General();
        DataSet ds = gen.MonthlyIPFundsDisbursementReportAutoAlerts(Month, Year);
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
            dt.Columns.Add("Link", typeof(string));

            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String strUrl = originalPath.Substring(0, originalPath.LastIndexOf("/"));

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                { dr["Link"] = strUrl + "/MonthlyFundsDisbursementReport.aspx?Project=" + dr["ID"].ToString(); }
                ReportViewer1.SizeToReportContent = true;

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report/MonthlyReport.rdlc");

                ReportViewer1.LocalReport.DataSources.Clear();

                List<ReportParameter> paraList = new List<ReportParameter>();

                paraList.Add(new ReportParameter("Name", "Implementing Partner"));
                paraList.Add(new ReportParameter("ReportName", "Monthly IP Funds Disbursement Report"));
                ReportDataSource source = new ReportDataSource("DataSet1", dt);



                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.LocalReport.SetParameters(paraList);
                ReportViewer1.LocalReport.Refresh();
                btnMailSend.Visible = true;
                string fileName = Server.MapPath("ReportDoc/MonthlyReport" + DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + ".pdf");
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                byte[] Bytes = ReportViewer1.LocalReport.Render("PDF", "");
                using (FileStream stream = new FileStream(fileName, FileMode.Create))
                {
                    stream.Write(Bytes, 0, Bytes.Length);
                }
                SendMailAnother(fileName);
                //if (File.Exists(fileName))
                //{
                //    File.Delete(fileName);
                //}
    
            }
            else
            {
                try
                {
                    btnMailSend.Visible = false;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    List<ReportParameter> paraList = new List<ReportParameter>();

                    paraList.Add(new ReportParameter("Name", "Implementing Partner"));
                    paraList.Add(new ReportParameter("ReportName", "No Records Found"));
                    ReportDataSource source = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.DataSources.Add(source);
                    ReportViewer1.LocalReport.SetParameters(paraList);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch { }

            }
        }

      
    }


    protected void btnMailSend_Clicked(object sender, EventArgs e)
    {
        string fileName = Server.MapPath("ReportDoc/MonthlyReport"+DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss")+".pdf");
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
        byte[] Bytes = ReportViewer1.LocalReport.Render("PDF","");
        using (FileStream stream = new FileStream(fileName, FileMode.Create))
        {
            stream.Write(Bytes, 0, Bytes.Length);
        }
        SendMailAnother(fileName);
        if (File.Exists(fileName))
        {
            File.Delete(fileName);
        }
    }

    public void SendMailAnother(string fileName)
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


        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS :: Auto Alerts Monthly Report on " + DateTime.Now.ToString("dd-MMM-yyyy"), System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Report Status - Auto Alerts Monthly Report on " + DateTime.Now.ToString("dd-MMM-yyyy");

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "Dear Team <br><br>  Please find the dashboard status on SSSNP as on dated " +DateTime.Now.ToString("dd-MM-yyyy");
        mail.BodyEncoding = System.Text.Encoding.UTF8;

        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;
        mail.Attachments.Add(new Attachment(fileName));

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
            // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int Month = 0;
        int Year = 0;
        try { Month = Convert.ToInt32(ddlMonth.SelectedValue); }
        catch { }
        try { Year = Convert.ToInt32(ddlyear.SelectedValue); }
        catch { }
        DataTable dt = new DataTable();
        General gen = new General();
        DataSet ds = gen.MonthlyIPFundsDisbursementReport(Month, Year);
        if (ds.Tables.Count > 0)
        {
            dt = ds.Tables[0];
            dt.Columns.Add("Link", typeof(string));
         
            String originalPath = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            String strUrl = originalPath.Substring(0, originalPath.LastIndexOf("/"));
      
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                { dr["Link"] = strUrl + "/MonthlyFundsDisbursementReport.aspx?Project=" + dr["ID"].ToString(); }
                ReportViewer1.SizeToReportContent = true;

                ReportViewer1.LocalReport.ReportPath = Server.MapPath("Report/MonthlyReport.rdlc");

                ReportViewer1.LocalReport.DataSources.Clear();

                List<ReportParameter> paraList = new List<ReportParameter>();

                paraList.Add(new ReportParameter("Name", "Implementing Partner"));
                paraList.Add(new ReportParameter("ReportName", "Monthly IP Funds Disbursement Report"));
                ReportDataSource source= new ReportDataSource("DataSet1", dt);
              

                
                ReportViewer1.LocalReport.DataSources.Add(source);
                ReportViewer1.LocalReport.SetParameters(paraList);
                ReportViewer1.LocalReport.Refresh();
                btnMailSend.Visible = true ;

            }
            else
            {
                try
                {
                    btnMailSend.Visible = false;
                    ReportViewer1.LocalReport.DataSources.Clear();

                    List<ReportParameter> paraList = new List<ReportParameter>();

                    paraList.Add(new ReportParameter("Name", "Implementing Partner"));
                    paraList.Add(new ReportParameter("ReportName", "No Records Found"));
                    ReportDataSource source = new ReportDataSource("DataSet1", dt);
                    ReportViewer1.LocalReport.DataSources.Add(source);
                    ReportViewer1.LocalReport.SetParameters(paraList);
                    ReportViewer1.LocalReport.Refresh();
                }
                catch { }
                
            }
        }
    }
  
}

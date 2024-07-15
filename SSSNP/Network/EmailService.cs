using SSSNP_SNSOP.Core.DbHandler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace SSSNP.Network
{
    public class EmailService
    {
        private string _connStr = "";
        private IDbOperation _dbOperation = null;

        public EmailService()
        {
            var buildProfile = ConfigurationManager.AppSettings["build.profile.active"].ToString();
            if (buildProfile.Equals("prod"))
            {
                _connStr = ConfigurationManager.AppSettings["snsop_db_connStr_prod"].ToString();
            }
            else
            {
                _connStr = ConfigurationManager.AppSettings["snsop_db_connStr_dev"].ToString();
            }
            _dbOperation = new SqlDbOperation(_connStr);
        }

        public void sendEmail(string to, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
            SmtpClient client = new SmtpClient();
            client.Port = 587;
            client.Host = smtpserver;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential("no-reply@southsudansafetynet.info", "2sOQenMaTV-?");

            MailAddress fromMail = new MailAddress("no-reply@southsudansafetynet.info");
            MailAddress toMail = new MailAddress(to);

            mail = new MailMessage(fromMail, toMail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;

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

        public void sendEmailToUserType(string emailSubject, string emailBody, string userType)
        {
            String sql3 = "select Email from tbl_Users where 1=1 " +
                    " and User_type='"+userType+ "' and Status='Y' and Email is not null and email_receiver=1";
            DataTable dt3 = _dbOperation.GetDataTable(sql3);
            if (dt3 != null && dt3.Rows != null && dt3.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt3.Rows)
                {
                    String toEmail = dataRow["Email"] != null ? dataRow["Email"].ToString() : null;
                    if (toEmail != null)
                    {
                        sendEmail(toEmail, emailSubject, emailBody);
                    }
                }
            }
        }

        public void sendEmailToIpApprover(string emailSubject, string emailBody)
        {
            String sql3 = "select Email from tbl_Users where 1=1 " +
                    " and User_type='IP' and IP_Approver=1 and Status='Y' and Email is not null and email_receiver=1";
            DataTable dt3 = _dbOperation.GetDataTable(sql3);
            if (dt3 != null && dt3.Rows != null && dt3.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt3.Rows)
                {
                    String toEmail = dataRow["Email"] != null ? dataRow["Email"].ToString() : null;
                    if (toEmail != null)
                    {
                        sendEmail(toEmail, emailSubject, emailBody);
                    }
                }
            }
        }

        public void sendEmailForWageRequest(string intWagePaymentReqid, string userType, string emailSubject, string emailBody)
        {
            string sql = "select tu.Email from td_WorkWageRequest t left join tbl_Users tu";
            if (userType == "Cordinator")
            {
                sql += " on t.intCoOrdinatorid=tu.intUserid ";
            }
            if (userType == "Specialist")
            {
                sql += " on t.intSpecialistid=tu.intUserid ";
            }
            if (userType == "Manager")
            {
                sql += " on t.intManagerid=tu.intUserid ";
            }
            sql += " and tu.Status='Y' and tu.Email is not null where tu.email_receiver=1 and t.intWagePaymentReqid='" + intWagePaymentReqid + "'";

            DataTable dt3 = _dbOperation.GetDataTable(sql);
            if (dt3 != null && dt3.Rows != null && dt3.Rows.Count > 0)
            {
                foreach (DataRow dataRow in dt3.Rows)
                {
                    String toEmail = dataRow["Email"] != null ? dataRow["Email"].ToString() : null;
                    if (toEmail != null)
                    {
                        sendEmail(toEmail, emailSubject, emailBody);
                    }
                }
            }
        }
    }
}
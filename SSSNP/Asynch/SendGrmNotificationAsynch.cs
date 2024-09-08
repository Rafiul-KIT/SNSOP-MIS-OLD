using SSSNP.Network;
using SSSNP_SNSOP.Core.DbHandler;
using SSSNP_SNSOP.Core.Threads;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace SSSNP.Asynch
{
    public class SendGrmNotificationAsynch : IThreadable
    {
        // TODO: based on the day count, notifications to be sent
        private const int SLEEP_TIME = 60 * 1000 * 60 * 24; // 30 minutes interval
        private const string NAME = "grm_notification_daemon";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        private IDbOperation _dbOperation = null;
        private string _connStr = "";
        private EmailService emailService;

        public SendGrmNotificationAsynch()
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
            emailService = new EmailService();
        }
        public string GetName()
        {
            return NAME;
        }

        public int GetSleepTime()
        {
            return SLEEP_TIME;
        }

        public AutoResetEvent GetWaitHandle()
        {
            return waitHandle;
        }

        public AutoResetEvent GetAbortHandle()
        {
            return abortHandle;
        }

        public void Task()
        {
            try
            {
                //logger.Debug("SEND GRM NOTIFICATION: task started!");
                Console.WriteLine("SEND GRM NOTIFICATION: task started!");
                while (true)
                {
                    if (!SendNotification())
                    {
                        // Only when thread stop request received
                        break;
                    }

                    //logger.Debug("SEND GRM NOTIFICATION: Going into sleep mode!");
                    Console.WriteLine("SEND GRM NOTIFICATION: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    //logger.Debug("SEND GRM NOTIFICATION: Woke up from sleep!");
                    Console.WriteLine("SEND GRM NOTIFICATION: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        //logger.Debug("SEND GRM NOTIFICATION: Abort request received. Getting out!");
                        Console.WriteLine("SEND GRM NOTIFICATION: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                //logger.Debug("SEND GRM NOTIFICATION: Force shutdown initiated! ThreadAbortException: " + e.Message);
                Console.WriteLine("SEND GRM NOTIFICATION: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        private bool SendNotification()
        {
            try
            {
                // TODO:
                // 1. Read data from table using _dbOperation
                // 2. Send email using smtp

                bool needToSend = false;
                string emailSubject = "Grievance Expiration Notice";
                string emailBody = "<H2>Dear GRM Manager (SNSOP MIS),</H2> <br><br>";

                String sql1 = "select count(*) count from NonGBVMain m where 1=1 " +
                    " and dateadd(DAY,9, [Date]) < getdate() " +
                    " and m.Status <> 5 and Deleted = 0";
                DataTable dt1 = _dbOperation.GetDataTable(sql1);
                if (dt1 != null && dt1.Rows != null && dt1.Rows.Count > 0)
                {
                    int count = -1;
                    foreach (DataRow row in dt1.Rows)
                    {
                        count = row["count"] != null ? int.Parse(row["count"].ToString()) : -1;
                    }
                    if (count > 0)
                    {
                        needToSend = true;
                        emailBody += "You have " + count + " Non-GBV grievance(s) which have expired<br>";
                    }
                }

                String sql2 = "select count(*) count from GBVMain m where 1=1 " +
                    " and dateadd(DAY,9, [Date]) < getdate() " +
                    " and m.Status <> 5 and Deleted = 0";
                DataTable dt2 = _dbOperation.GetDataTable(sql2);
                if (dt2 != null && dt2.Rows != null && dt2.Rows.Count > 0)
                {
                    int count = -1;
                    foreach (DataRow row in dt2.Rows)
                    {
                        count = row["count"] != null ? int.Parse(row["count"].ToString()) : -1;
                    }
                    if (count > 0)
                    {
                        needToSend = true;
                        emailBody += "You have " + count + " GBV grievance(s) which have expired<br>";
                    }
                }

                string sssnpweb = ConfigurationManager.AppSettings["SSSNPWEB"];
                emailBody += sssnpweb + "<br>Please login by clicking the above link<br>Thanks and regards.";

                if (needToSend == true)
                {
                    emailService.sendEmailToUserType(emailSubject, emailBody, "ManagerGRM");
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an unexpexted error when sending grm notitication. Error details:\n" + ex.ToString());
                return false;
            }
        }
    }
}
using SSSNP_SNSOP.App_Code;
using SSSNP_SNSOP.Core.Threads;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web;
using System.Web.Script.Serialization;

namespace SSSNP.Asynch
{
    public class AutoReconcilePayrollAsync : IThreadable
    {
        private int SLEEP_TIME = 0;
        private const string NAME = "reconcile_notification_daemon";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        General Gen = new General();
        private string PayrollReconBatchFlag;

        public AutoReconcilePayrollAsync()
        {
            PayrollReconBatchFlag = ConfigurationManager.AppSettings["PayrollReconBatchFlag"];
            SLEEP_TIME = int.Parse(ConfigurationManager.AppSettings["AutoReconcileInterval"].ToString());
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
                Console.WriteLine("AUTO PAYROLL RECONCILE NOTIFICATION: task started!");
                while (true)
                {
                    if (PayrollReconBatchFlag == null || PayrollReconBatchFlag.Equals("no"))
                    {
                        Console.WriteLine("no need");
                    }
                    else
                    {
                        InsertWageBenDetails();
                    }

                    Console.WriteLine("AUTO PAYROLL RECONCILE NOTIFICATION: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    Console.WriteLine("AUTO PAYROLL RECONCILE NOTIFICATION: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        Console.WriteLine("AUTO PAYROLL RECONCILE NOTIFICATION: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("AUTO PAYROLL RECONCILE NOTIFICATION: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }


        public string InsertWageBenDetails()
        {
            string mainresult = "";
            try
            {
                bool Test = true;

                try
                {
                    InsertWageBenDetailsThread insThread = new InsertWageBenDetailsThread();
                    Thread tIns = new Thread(new ThreadStart(insThread.ThreadProc));
                    tIns.Start();
                }
                catch (Exception ex)
                {
                    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"" + ex.ToString() + "\"}";
                }
                if (Test == false)
                {
                    mainresult = mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"url does not exist\"}";
                }
            }
            catch (Exception ex)
            {
                mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"" + ex.ToString() + "\"}";
            }
            int sas = Gen.RemoveUnpaidList();
            return mainresult;
        }

        public class Beneficiary_API_Data
        {
            public int id { get; set; }
            public string supervisor_id { get; set; }
            public string household_number { get; set; }
            public string paid_to { get; set; }
            public string enrolment_number { get; set; }
            public string date_from { get; set; }
            public string date_to { get; set; }
            public string exchange_rate { get; set; }
            public string wage { get; set; }
            public string amount { get; set; }
            public string work_id { get; set; }
            public string work_code { get; set; }
            public string attendance { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string surname { get; set; }
            public string trans_id { get; set; }
            public string uuid { get; set; }
            public string status { get; set; }
            public string generated_at { get; set; }
            public string created_at { get; set; }
            public string updated_at { get; set; }
            public string latitude { get; set; }
            public string longitude { get; set; }
        }

        public class RootObject
        {
            public bool error { get; set; }
            public int errorCode { get; set; }
            public List<Beneficiary_API_Data> data { get; set; }
        }

        public class InsertWageBenDetailsThread
        {
            private string strJson;
            ErrorLogger eLog = new ErrorLogger();

            public void ThreadProc()
            {
                General Gen = new General();

                try
                {
                    DataSet dsBomasWageApplied = new DataSet();
                    dsBomasWageApplied = Gen.GetBomaListWageApplied();
                    if (dsBomasWageApplied.Tables[0].Rows.Count > 0)
                    {
                        int iCntBomas = dsBomasWageApplied.Tables[0].Rows.Count;
                        int iIndexBoma = 0;

                        for (iIndexBoma = 0; iIndexBoma < iCntBomas; iIndexBoma++)
                        {
                            string strBomaId = dsBomasWageApplied.Tables[0].Rows[iIndexBoma][0].ToString();

                            string URI = ConfigurationManager.AppSettings["AfisDevBaseUrl"]
                                + ConfigurationManager.AppSettings["GetPayments"] + "/" + strBomaId;
                            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
                            {
                                URI = ConfigurationManager.AppSettings["AfisProdBaseUrl"]
                                + ConfigurationManager.AppSettings["GetPayments"] + "/" + strBomaId;
                            }

                            Uri urlCheck = new Uri(URI);
                            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

                            httpWReq.PreAuthenticate = true;

                            var token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();
                            httpWReq.Headers.Add("Authorization", "Bearer " + token);


                            httpWReq.ProtocolVersion = HttpVersion.Version11;
                            httpWReq.Method = "GET";
                            httpWReq.ContentType = "application/json; charset=utf-8";

                            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                            string s = response.ToString();
                            StreamReader reader = new StreamReader(response.GetResponseStream());
                            String jsonresponse = "";
                            String temp = null;
                            while ((temp = reader.ReadLine()) != null)
                            {
                                jsonresponse += temp;
                            }
                            strJson = jsonresponse;
                            JavaScriptSerializer js = new JavaScriptSerializer();
                            js.MaxJsonLength = Int32.MaxValue;
                            RootObject obj = js.Deserialize<RootObject>(strJson);
                            if (obj != null)
                            {
                                if (obj.error == false)
                                {
                                    List<Beneficiary_API_Data> objdata = new List<Beneficiary_API_Data>();
                                    objdata = obj.data;
                                    int Bene_ID = 0;
                                    int Workid = 0;
                                    int IPid = 0;
                                    int Bentransid = 0;

                                    foreach (Beneficiary_API_Data bad in objdata)
                                    {
                                        DataSet dsgs = new DataSet();
                                        dsgs = Gen.HouseHoldNumberVerificationWagesNewwithBoma(bad.household_number, bad.uuid, bad.work_id, strBomaId);

                                        if (dsgs.Tables[0].Rows.Count > 0)
                                        {
                                            Bene_ID = Convert.ToInt32(dsgs.Tables[0].Rows[0]["intBenid"].ToString().Trim());
                                            IPid = Convert.ToInt32(dsgs.Tables[0].Rows[0]["intIP"].ToString().Trim());
                                            Workid = Convert.ToInt32(bad.work_id);
                                            //save data
                                            if (dsgs.Tables[1].Rows.Count > 0)
                                            {

                                                Workid = Convert.ToInt32(dsgs.Tables[1].Rows[0]["intWorkid"].ToString().Trim());
                                                if (dsgs.Tables[5].Rows.Count == 0)
                                                {
                                                    Gen.InsertAPIFailure_StatusPayaments(bad.household_number, Workid.ToString() + " work is not under this boma code " + strBomaId, Convert.ToString(bad.created_at), bad.uuid, bad.work_id, bad.trans_id);
                                                }
                                                else if (dsgs.Tables[2].Rows.Count == 0)
                                                {
                                                    Gen.InsertAPIFailure_StatusPayaments(bad.household_number, "For this work request number is not compared", Convert.ToString(bad.created_at), bad.uuid, bad.work_id, bad.trans_id);
                                                }
                                                else
                                                {
                                                    if (dsgs.Tables[3].Rows.Count == 0)
                                                    {
                                                        Gen.InsertAPIFailure_StatusPayaments(bad.household_number, "Benficiary is not there in work with request number", Convert.ToString(bad.created_at), bad.uuid, bad.work_id, bad.trans_id);
                                                    }
                                                    else
                                                    {
                                                        try
                                                        {
                                                            Bentransid = Convert.ToInt32(bad.trans_id);
                                                            if (bad.status != null && (bad.status == "1" || bad.status.Equals("PAID")))
                                                            {
                                                                int resultwage = Gen.insertWageRequestPaymentService(bad.uuid, bad.exchange_rate, IPid.ToString());
                                                                int s2 = Gen.InsUpdBeneficiaryWagesRequestPayingAgent(Bentransid.ToString(), bad.uuid, Workid.ToString(), Bene_ID.ToString(), bad.attendance, bad.wage, bad.amount, Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage)).ToString("f4"), Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage) * Convert.ToDecimal(bad.attendance)).ToString("f4"), IPid.ToString(), resultwage.ToString());
                                                                int s1 = Gen.InsUpdBeneficiaryWagesFromToDateLatestFinalUSDServiceNewlatest(Bene_ID.ToString(), bad.date_from, bad.date_to, bad.updated_at, Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage) * Convert.ToDecimal(bad.attendance)).ToString("f4"), IPid.ToString(), Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage)).ToString("f4"), Workid.ToString(), bad.exchange_rate.ToString(), bad.amount.ToString(), bad.wage.ToString(), bad.uuid, bad.attendance, bad.generated_at, bad.paid_to, bad.trans_id, "", bad.supervisor_id, bad.latitude, bad.longitude);
                                                            }
                                                            else
                                                            {
                                                            }
                                                        }
                                                        catch (Exception ex)
                                                        {
                                                            Gen.InsertAPIFailure_StatusPayaments(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at), bad.uuid, bad.work_id, bad.trans_id);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                Gen.InsertAPIFailure_StatusPayaments(bad.household_number, "Work is not available for this Work ID", Convert.ToString(bad.created_at), bad.uuid, bad.work_id, bad.trans_id);
                                            }
                                        }
                                        else
                                        {
                                            Gen.InsertAPIFailure_StatusPayaments(bad.household_number, "Benficiary is not available for this house hold number", Convert.ToString(bad.created_at), bad.uuid, bad.work_id, bad.trans_id);
                                        }
                                    }
                                }
                                else
                                {
                                    Gen.InsertAPIFailure_StatusPayaments("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now), "", "", "");
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    eLog.LogError(ex);
                    throw;
                }
                int sas = Gen.RemoveUnpaidList();
            }

            public InsertWageBenDetailsThread()
            {

            }
        }
    }
}
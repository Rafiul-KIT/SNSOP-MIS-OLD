using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using SSSNP_SNSOP.Extensions;
using System.Threading;

public partial class ImportFromServicePaymentsByBoma : System.Web.UI.Page
{
    General Gen = new General();

    DataTable mydt = new DataTable();
    DataTable mydtp = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 500;

        if (!IsPostBack)
        {
            ImportNewPaymentsBoma();
        }
    }

    public void ImportNewPaymentsBoma()
    {
        try
        {
            ImportNewPaymentsByBoma impThread;
            if (!Request.QueryString.HasKeys())
            {
                impThread = new ImportNewPaymentsByBoma(null);
            }
            else
            {
                //Querystring
                impThread = new ImportNewPaymentsByBoma(Request.QueryString["bomaid"]);
            }
            Thread tImportPayments = new Thread(new ThreadStart(impThread.ThreadProcImportNewPayments));
            tImportPayments.Start();

        }
        catch (Exception ex)
        {
            //
        }
        //int sas = Gen.RemoveUnpaidList();
    }


    public class ImportNewPaymentsByBoma
    {
        //private string strJson;
        private string strBomaId;

        public void ThreadProcImportNewPayments()
        {
            General Gen = new General();

            try
            {
                DataSet dsBomaPayments = new DataSet();
                if (strBomaId == null)
                {
                    dsBomaPayments = Gen.GetPaymentsCountersBoma();
                    int iBomaPayed = dsBomaPayments.Tables[0].Rows.Count;

                    if (iBomaPayed > 0)
                    {
                        for (int iIndexBoma = 0; iIndexBoma < iBomaPayed; iIndexBoma++)
                        {
                            string strBomaIdTemp = dsBomaPayments.Tables[0].Rows[iIndexBoma]["BomaId"].ToString();
                            //ImportPaymentsBomaId(strBomaId);
                            int iPaidBenfSSSNP = Convert.ToInt32(dsBomaPayments.Tables[0].Rows[iIndexBoma]["BeneficiariesCount"]);
                            ImportFromServicePaymentsByBoma thisClass = new ImportFromServicePaymentsByBoma();
                            thisClass.ImportPaymentsBomaId(strBomaIdTemp, iPaidBenfSSSNP);
                        }
                    }
                }
                else
                {
                    ImportFromServicePaymentsByBoma thisClass = new ImportFromServicePaymentsByBoma();
                    thisClass.ImportPaymentsBomaId(strBomaId, null);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            Gen.RemoveUnpaidList();
        }
        public ImportNewPaymentsByBoma(string BomaId)
        {
            strBomaId = BomaId;
        }
    }

    //BeneficiariesCount null when it is not known: when the method is called for a single Boma directly 
    public void ImportPaymentsBomaId(string BomaId, int? BeneficiariesCount)
    {
        General Gen = new General();
        string strJson;

        try
        {
            string URI = System.Configuration.ConfigurationManager.AppSettings["GetPayments"] + "/" + BomaId;

            Uri urlCheck = new Uri(URI);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

            string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());

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
                if (BeneficiariesCount == null || obj.paidPayments > BeneficiariesCount)
                {
                    List<BeneficiaryPayment_API_Data> jsonPayments = new List<BeneficiaryPayment_API_Data>();
                    jsonPayments = obj.data;
                    //Import payments for boma
                    foreach (BeneficiaryPayment_API_Data BenfPayment in jsonPayments)
                    {
                        //Payment is imported only if in status "1": no actions needed for payment not done
                        if (BenfPayment.status == "1")
                        {
                            DataSet dsgs = new DataSet();
                            dsgs = Gen.HouseHoldNumberVerificationWagesNewwithBoma(BenfPayment.household_number, BenfPayment.uuid, BenfPayment.work_id, BomaId);

                            if (dsgs.Tables[0].Rows.Count > 0)
                            {
                                int Bene_ID = Convert.ToInt32(dsgs.Tables[0].Rows[0]["intBenid"].ToString().Trim());
                                int IPid = Convert.ToInt32(dsgs.Tables[0].Rows[0]["intIP"].ToString().Trim());
                                int Workid = Convert.ToInt32(BenfPayment.work_id);
                                int Bentransid = 0;

                                //save data
                                if (dsgs.Tables[1].Rows.Count > 0)
                                {
                                    Workid = Convert.ToInt32(dsgs.Tables[1].Rows[0]["intWorkid"].ToString().Trim());
                                    if (dsgs.Tables[5].Rows.Count == 0)
                                    {
                                        Gen.InsertAPIFailure_StatusPayaments(BenfPayment.household_number, Workid.ToString() + " work is not under this boma code " + BomaId, Convert.ToString(BenfPayment.created_at), BenfPayment.uuid, BenfPayment.work_id, BenfPayment.trans_id);
                                    }
                                    else if (dsgs.Tables[2].Rows.Count == 0)
                                    {
                                        Gen.InsertAPIFailure_StatusPayaments(BenfPayment.household_number, "For this work request number is not compared", Convert.ToString(BenfPayment.created_at), BenfPayment.uuid, BenfPayment.work_id, BenfPayment.trans_id);
                                    }
                                    else
                                    {
                                        if (dsgs.Tables[3].Rows.Count == 0)
                                        {
                                            Gen.InsertAPIFailure_StatusPayaments(BenfPayment.household_number, "Benficiary is not there in work with request number", Convert.ToString(BenfPayment.created_at), BenfPayment.uuid, BenfPayment.work_id, BenfPayment.trans_id);
                                        }
                                        else
                                        {
                                            try
                                            {
                                                Bentransid = Convert.ToInt32(BenfPayment.trans_id);
                                                if (BenfPayment.status == "1")
                                                {
                                                    int resultwage = Gen.insertWageRequestPaymentService(BenfPayment.uuid, BenfPayment.exchange_rate, IPid.ToString());
                                                    int s2 = Gen.InsUpdBeneficiaryWagesRequestPayingAgent(Bentransid.ToString(), BenfPayment.uuid, Workid.ToString(), Bene_ID.ToString(), BenfPayment.attendance, BenfPayment.wage, BenfPayment.amount, Convert.ToDecimal(Convert.ToDecimal(BenfPayment.exchange_rate) * Convert.ToDecimal(BenfPayment.wage)).ToString("f0"), Convert.ToDecimal(Convert.ToDecimal(BenfPayment.exchange_rate) * Convert.ToDecimal(BenfPayment.wage) * Convert.ToDecimal(BenfPayment.attendance)).ToString("f0"), IPid.ToString(), resultwage.ToString());
                                                    //int s1 = Gen.InsUpdBeneficiaryWagesFromToDateLatestFinalUSDServiceNewlatest(Bene_ID.ToString(), BenfPayment.date_from, BenfPayment.date_to, BenfPayment.updated_at, Convert.ToDecimal(Convert.ToDecimal(BenfPayment.exchange_rate) * Convert.ToDecimal(BenfPayment.wage) * Convert.ToDecimal(BenfPayment.attendance)).ToString("f0"), IPid.ToString(), Convert.ToDecimal(Convert.ToDecimal(BenfPayment.exchange_rate) * Convert.ToDecimal(BenfPayment.wage)).ToString("f0"), Workid.ToString(), BenfPayment.exchange_rate.ToString(), BenfPayment.amount.ToString(), BenfPayment.wage.ToString(), BenfPayment.uuid, BenfPayment.attendance, BenfPayment.generated_at, BenfPayment.paid_to, BenfPayment.trans_id, "", BenfPayment.supervisor_id, BenfPayment.latitude, BenfPayment.longitude);
                                                }
                                                else
                                                {
                                                }
                                            }
                                            catch (Exception ex)
                                            {
                                                Gen.InsertAPIFailure_StatusPayaments(BenfPayment.household_number, ex.ToString(), Convert.ToString(BenfPayment.created_at), BenfPayment.uuid, BenfPayment.work_id, BenfPayment.trans_id);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    Gen.InsertAPIFailure_StatusPayaments(BenfPayment.household_number, "Work is not available for this Work ID", Convert.ToString(BenfPayment.created_at), BenfPayment.uuid, BenfPayment.work_id, BenfPayment.trans_id);
                                }
                            }
                            else
                            {
                                Gen.InsertAPIFailure_StatusPayaments(BenfPayment.household_number, "Benficiary is not available for this house hold number", Convert.ToString(BenfPayment.created_at), BenfPayment.uuid, BenfPayment.work_id, BenfPayment.trans_id);
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public class BeneficiaryPayment_API_Data
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
        public int totalPayments { get; set; }
        public int paidPayments { get; set; }
        public int unpaidPayments { get; set; }
        public List<BeneficiaryPayment_API_Data> data { get; set; }
    }

}

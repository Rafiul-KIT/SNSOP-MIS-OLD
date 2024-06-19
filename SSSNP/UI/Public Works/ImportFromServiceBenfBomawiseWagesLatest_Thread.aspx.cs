using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using SSSNP_SNSOP.Extensions;
using System.Threading;
using System.Configuration;
using SSSNP_SNSOP.App_Code;

public partial class ImportFromServiceBenfBomawiseWagesLatest_Thread : System.Web.UI.Page
{
    General Gen = new General();

    DataTable mydt = new DataTable();
    static DataTable dtMyTable;
    static DataTable dtMyTableCertificate;
    DataTable mydtp = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 800;

        if (!IsPostBack)
        {
            Session.Remove("ErrorDataW");
            dtMyTableCertificate = createtablecrtificate();
            Session["ErrorDataW"] = dtMyTableCertificate;
            string result = "";

            try
            {
                result = InsertWageBenDetails();
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message );
                
            }
            
            Response.Write(result);
        }
    }

    protected DataTable createtablecrtificate()
    {
        dtMyTable = new DataTable("ErrorDataW");

        dtMyTable.Columns.Add("id", typeof(string));
        dtMyTable.Columns.Add("supervisor_id", typeof(string));
        dtMyTable.Columns.Add("household_number", typeof(string));
        dtMyTable.Columns.Add("paid_to", typeof(string));
        dtMyTable.Columns.Add("enrolment_number", typeof(string));
        dtMyTable.Columns.Add("date_from", typeof(string));
        dtMyTable.Columns.Add("date_to", typeof(string));
        dtMyTable.Columns.Add("exchange_rate", typeof(string));
        dtMyTable.Columns.Add("wage", typeof(string));
        dtMyTable.Columns.Add("amount", typeof(string));
        dtMyTable.Columns.Add("work_id", typeof(string));
        dtMyTable.Columns.Add("work_code", typeof(string));
        dtMyTable.Columns.Add("attendance", typeof(string));
        dtMyTable.Columns.Add("first_name", typeof(string));
        dtMyTable.Columns.Add("last_name", typeof(string));
        dtMyTable.Columns.Add("surname", typeof(string));
        dtMyTable.Columns.Add("uuid", typeof(string));
        dtMyTable.Columns.Add("generated_at", typeof(string));
        //    dtMyTable.Columns.Add("request_number", typeof(string));
        dtMyTable.Columns.Add("created_at", typeof(string));
        dtMyTable.Columns.Add("updated_at", typeof(string));
        dtMyTable.Columns.Add("Status", typeof(string));
        dtMyTable.Columns.Add("Reason", typeof(string));
        dtMyTable.Columns.Add("ReturnDate", typeof(string));
        dtMyTable.Columns.Add("benid", typeof(string));
        return dtMyTable;
    }

    private void AddDataToTableCeertificate(DataTable myTable, string id, string supervisor_id, string household_number,
        string paid_to, string enrolment_number, string date_from, string date_to, string exchange_rate,
        string wage, string amount, string work_id,
        string work_code, string attendance, string first_name, string last_name, string surname, string uuid, string generated_at,
        string created_at, string updated_at, string Status, string Reason, string ReturnDate, string enrollmentNo, string request_number)
    {
        try
        {
            DataRow Row;
            Row = myTable.NewRow();

            dtMyTable = new DataTable("ErrorDataW");

            Row["id"] = id;
            Row["supervisor_id"] = supervisor_id;
            Row["household_number"] = household_number;
            Row["paid_to"] = paid_to;
            Row["enrolment_number"] = enrolment_number;
            Row["date_from"] = date_from;
            Row["date_to"] = date_to;
            Row["exchange_rate"] = exchange_rate;
            Row["wage"] = wage;
            Row["amount"] = amount;
            Row["work_id"] = work_id;
            Row["work_code"] = work_code;
            Row["attendance"] = attendance;
            Row["first_name"] = first_name;
            Row["last_name"] = last_name;
            Row["surname"] = surname;
            Row["uuid"] = uuid;
            Row["generated_at"] = generated_at;
            //Row["matched_fp"] = matched_fp;
            Row["created_at"] = created_at;
            Row["updated_at"] = updated_at;
            Row["Status"] = Status;
            Row["Reason"] = Reason;
            Row["ReturnDate"] = ReturnDate;
            Row["benid"] = enrollmentNo;
            myTable.Rows.Add(Row);
        }
        catch (Exception ee)
        {

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
                //Empty querystring
                if (!Request.QueryString.HasKeys())
                {
                    InsertWageBenDetailsThread insThread = new InsertWageBenDetailsThread();
                    Thread tIns = new Thread(new ThreadStart(insThread.ThreadProc));
                    tIns.Start();
                }
                else// ?bomaid=[number]
                {
                    if (!String.IsNullOrWhiteSpace(Request.QueryString["bomaid"]))
                    {
                        //strBomaId = Request.QueryString["bomaid"];
                        InsertWageBenDetailsThreadBomaId insThread = new InsertWageBenDetailsThreadBomaId(Request.QueryString["bomaid"]);
                        Thread tIns = new Thread(new ThreadStart(insThread.ThreadProcBomaId));
                        tIns.Start();
                    }
                }
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
                        //string URI = System.Configuration.ConfigurationManager.AppSettings["GetPayments"] + "/" + strBomaId;

                        string URI = ConfigurationManager.AppSettings["AfisDevBaseUrl"] 
                            + ConfigurationManager.AppSettings["GetPayments"] + "/" + strBomaId;
                        if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
                        {
                            URI = ConfigurationManager.AppSettings["AfisProdBaseUrl"]
                            + ConfigurationManager.AppSettings["GetPayments"] + "/" + strBomaId;
                        }

                        Uri urlCheck = new Uri(URI);
                        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

                        //string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
                        httpWReq.PreAuthenticate = true;
                        //httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());

                        var token = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJzYWtpYiIsImlhdCI6MTcxNDk4MTEwOCwiZXhwIjoxODcyNjYxMTA4fQ.7FgalnXhBOcC7q-nYfONbxmc7My4iUOeWpN2eK3b7vc_br43NCEFiKWIrXs1cJTsU_Ds_ZZLHRB28c-jdgiBRw";
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

    public class InsertWageBenDetailsThreadBomaId
    {
        private string strJson;
        private string strBomaId;

        public void ThreadProcBomaId()
        {
            General Gen = new General();

            try
            {
                string URI = System.Configuration.ConfigurationManager.AppSettings["GetPayments"] + "/" + strBomaId;

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
                                                if (bad.status == "1")
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
            catch (Exception ex)
            {
                throw;
            }
            int sas = Gen.RemoveUnpaidList();
        }

        public InsertWageBenDetailsThreadBomaId(string BomaId)
        {
            strBomaId = BomaId;
        }
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

}

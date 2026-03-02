using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Net;
using System.IO;
using System.Web.Script.Serialization;
using SSSNP_SNSOP.Extensions;
using System.Threading;

public partial class ImportFromServiceBenfAlternateDetLatestwithValidation_Thread : System.Web.UI.Page
{
    General Gen = new General();

    DataTable mydt = new DataTable();
    static DataTable dtMyTable;
    static DataTable dtMyTableCertificate;
    DataTable mydtp = new DataTable();
    static DataTable dtMyTablep;
    static DataTable dtMyTableCertificatep;
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 500;

        if (!IsPostBack)
        {
            Session.Remove("ErrorDataA");
            dtMyTableCertificate = createtablecrtificate();
            Session["ErrorDataA"] = dtMyTableCertificate;
            string result = InsertBenAlternateDetails();
            Response.Write(result);
        }
    }

    protected DataTable createtablecrtificate()
    {
        dtMyTable = new DataTable("ErrorDataA");
        dtMyTable.Columns.Add("id", typeof(string));
        dtMyTable.Columns.Add("supervisor_id", typeof(string));
        dtMyTable.Columns.Add("household_number", typeof(string));
        dtMyTable.Columns.Add("alternate_number", typeof(string));
        dtMyTable.Columns.Add("first_name", typeof(string));
        dtMyTable.Columns.Add("last_name", typeof(string));
        dtMyTable.Columns.Add("surname", typeof(string));
        dtMyTable.Columns.Add("age", typeof(string));
        dtMyTable.Columns.Add("id_number", typeof(string));
        dtMyTable.Columns.Add("phone_number", typeof(string));
        dtMyTable.Columns.Add("gender", typeof(string));
        dtMyTable.Columns.Add("photo_url", typeof(string));
        dtMyTable.Columns.Add("id_url", typeof(string));
        dtMyTable.Columns.Add("created_at", typeof(string));
        dtMyTable.Columns.Add("updated_at", typeof(string));
        dtMyTable.Columns.Add("Status", typeof(string));
        dtMyTable.Columns.Add("Reason", typeof(string));
        dtMyTable.Columns.Add("ReturnDate", typeof(string));
        dtMyTable.Columns.Add("benid", typeof(string));
        return dtMyTable;
    }


    private void AddDataToTableCeertificate(DataTable myTable, string id, string supervisor_id, string household_number, string alternate_number, string first_name, string last_name, string surname, string age,
        string id_number, string phone_number, string gender,
        string photo_url, string id_url, string created_at, string updated_at, string Status, string Reason, string ReturnDate, string enrollmentNo
)
    {//totStartDate, string totEndDate, string totSummary,
        try
        {
            DataRow Row;
            Row = myTable.NewRow();
            dtMyTable = new DataTable("ErrorDataA");
            Row["id"] = id;
            Row["supervisor_id"] = supervisor_id;
            Row["household_number"] = household_number;
            Row["alternate_number"] = alternate_number;
            Row["first_name"] = first_name;
            Row["last_name"] = last_name;
            Row["surname"] = surname;
            Row["age"] = age;
            Row["id_number"] = id_number;
            Row["phone_number"] = phone_number;
            Row["gender"] = gender;
            Row["photo_url"] = photo_url;
            Row["id_url"] = id_url;
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
            //  ((DataTable)Session["myDatatable"]).Rows.Clear();
            //  Response.Redirect("~/EmpFacility.aspx");
        }
    }

    /// <summary>
    /// Insert Data Using API
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public string InsertBenAlternateDetails()
    {
        string URI = System.Configuration.ConfigurationManager.AppSettings["GetAlternates"]; // API_V_1.2      

        string mainresult = "";
        bool Test = true;
        Uri urlCheck = new Uri(URI);
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

        string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
        httpWReq.PreAuthenticate = true;
        httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());

        string HouseHold_Number = "";

        try
        {
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

                //Call to new Thread for inserting data in DB
                InsertBenAltThread insThread = new InsertBenAltThread(jsonresponse);

                Thread tIns = new Thread(new ThreadStart(insThread.ThreadProc));
                tIns.Start();

                       
        }
        catch (Exception ex)
        {
            mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
        }
        if (Test == false)
        {
            mainresult = mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"url does not exist\"}";
        }
        return mainresult;
    }

    public class InsertBenAltThread
    {
        private string strJson;

        public void ThreadProc()
        {
            General Gen = new General();

            try
            {
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

                        foreach (Beneficiary_API_Data bad in objdata)
                        {
                            try
                            {
                                DataSet ds_check = new DataSet();

                                string householdnumber = bad.household_number;

                                ds_check = Gen.HouseHoldNumberVerificationForAlternate(householdnumber);
                                if (ds_check.Tables[0].Rows.Count > 0)
                                {
                                    DataSet dscheckalternate = new DataSet();
                                    dscheckalternate = Gen.GetCheckalternatenumberinAPI(Convert.ToString(bad.alternate_number));
                                    if (dscheckalternate.Tables[0].Rows.Count == 0)
                                    {
                                        if (ds_check.Tables[2].Rows[0][0].ToString().Trim() == "0")
                                        {
                                            int Alt_update = Gen.UpdateAlternate(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), Convert.ToString(bad.alternate_number),
                                                                                                   Convert.ToString(bad.alternate_name),
                                                                                                   Convert.ToString(bad.age),
                                                                                                   Convert.ToString(bad.id_number),
                                                                                                   Convert.ToString(bad.gender),
                                                                                                   Convert.ToString(bad.phone_number),
                                                                                                   Convert.ToString(bad.photo_url));

                                        }
                                        if (ds_check.Tables[2].Rows[0][0].ToString().Trim() == "1")
                                        {
                                            int Alt_update = Gen.UpdatesecondAlternate(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), Convert.ToString(bad.alternate_number),
                                                                                                       Convert.ToString(bad.alternate_name),
                                                                                                       Convert.ToString(bad.age),
                                                                                                       Convert.ToString(bad.id_number),
                                                                                                       Convert.ToString(bad.gender),
                                                                                                       Convert.ToString(bad.phone_number),
                                                                                                       Convert.ToString(bad.photo_url));

                                        }
                                        if (ds_check.Tables[2].Rows[0][0].ToString().Trim() == "2")
                                        {
                                            Gen.InsertAPIFailure_StatusAlternate(bad.household_number, "Trying to pull more than two alternate number", Convert.ToString(bad.created_at), bad.alternate_number, bad.alternate_name);

                                        }

                                    }
                                    else
                                    {
                                        //Alternate number is already stored as beneficiary, 1st alternate or second alternate number
                                        Gen.InsertAPIFailure_StatusAlternate(bad.household_number, "Trying to pull duplicate alternate number", Convert.ToString(bad.created_at), bad.alternate_number, bad.alternate_name);

                                    }
                                }
                                else
                                {
                                    Gen.InsertAPIFailure_StatusAlternate(bad.household_number, "Household number not in or not ported earlier in MIS for Alternate", Convert.ToString(bad.created_at), bad.alternate_number, bad.alternate_name);
                                }
                            }
                            catch (Exception ex)
                            {
                                Gen.InsertAPIFailure_StatusAlternate(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at), bad.alternate_number, bad.alternate_name);
                            }
                        }
                    }
                    else
                    {
                        Gen.InsertAPIFailure_StatusAlternate("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now), "", "");
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public InsertBenAltThread(string jsonData)
        {
            strJson = jsonData;
        }
    }


    public class Beneficiary_API_Data
    {

        public int id { get; set; }
        public string supervisor_id { get; set; }
        public string household_number { get; set; }
        public string alternate_number { get; set; }
        public string alternate_name { get; set; }
        public string age { get; set; }
        public string id_number { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string photo_url { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
    }

    public class RootObject
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public List<Beneficiary_API_Data> data { get; set; }
    }
}

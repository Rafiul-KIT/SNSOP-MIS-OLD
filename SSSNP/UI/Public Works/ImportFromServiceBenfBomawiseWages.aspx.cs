using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.Text;
using System.IO;
using System.Web.Script.Serialization;
using SSSNP_SNSOP.Extensions;

public partial class ImportFromServiceBenfBomawiseWages : System.Web.UI.Page
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
        if (!IsPostBack)
        {
            Session.Remove("ErrorDataW");
            dtMyTableCertificate = createtablecrtificate();
            Session["ErrorDataW"] = dtMyTableCertificate;
            string result = InsertWageBenDetails();
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
        string created_at, string updated_at, string Status, string Reason, string ReturnDate, string enrollmentNo, string request_number
)
    {//totStartDate, string totEndDate, string totSummary,
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
            //  ((DataTable)Session["myDatatable"]).Rows.Clear();
            //  Response.Redirect("~/EmpFacility.aspx");
        }
    }

    /// <summary>
    /// Insert Data Using API
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public string InsertWageBenDetails()
    {
        //dtMyTableCertificate = createtablecrtificate();
        //Session["ErrorData"] = dtMyTableCertificate;
        string mainresult = "";

        try
        {
            
            DataSet dsFinger = new DataSet();
            dsFinger = Gen.GetBomaListWageApplied();
            if (dsFinger.Tables[0].Rows.Count > 0)
            {
              
                int cnt = dsFinger.Tables[0].Rows.Count;
                int f = 0;
                for (f = 0; f < cnt; f++)
                {


                    //string URI = "http:/54.68.125.132:8181/snsdp/index.php/api/v2/getPayments/" + dsFinger.Tables[0].Rows[f][0].ToString();
                    string URI = System.Configuration.ConfigurationManager.AppSettings["GetPayments"] + "/" + dsFinger.Tables[0].Rows[f][0].ToString(); // API_V_1.2
                    // string mainresult = "";
                    bool Test = true;
                    Uri urlCheck = new Uri(URI);
                    HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

                    string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
                    httpWReq.PreAuthenticate = true;
                    //httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());

                    var token = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJzYWtpYiIsImlhdCI6MTcxNDk4MTEwOCwiZXhwIjoxODcyNjYxMTA4fQ.7FgalnXhBOcC7q-nYfONbxmc7My4iUOeWpN2eK3b7vc_br43NCEFiKWIrXs1cJTsU_Ds_ZZLHRB28c-jdgiBRw";
                    httpWReq.Headers.Add("Authorization", "Bearer " + token);

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
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        js.MaxJsonLength = Int32.MaxValue;
                        RootObject obj = js.Deserialize<RootObject>(jsonresponse);
                        if (obj != null)
                        {
                            if (obj.error == false)
                            {
                                List<Beneficiary_API_Data> objdata = new List<Beneficiary_API_Data>();
                                objdata = obj.data;
                                string household_photo_path = "";
                                string House_Hold_Number = "";
                                int Bene_ID = 0;
                                int Workid = 0;
                                int IPid = 0;
                                int Bentransid = 0;

                           




                                foreach (Beneficiary_API_Data bad in objdata)
                                {

                                    //  DataSet ds_check = new DataSet();


                                    DataSet dsgs = new DataSet();
                                    dsgs = Gen.HouseHoldNumberVerificationWagesNew(bad.household_number, bad.uuid, bad.work_id);
                                    int GroupStrength = 0;
                                    if (dsgs.Tables[0].Rows.Count > 0)
                                    {
                                        Bene_ID = Convert.ToInt32(dsgs.Tables[0].Rows[0]["intBenid"].ToString().Trim());
                                        IPid = Convert.ToInt32(dsgs.Tables[0].Rows[0]["intIP"].ToString().Trim());
                                        Workid = Convert.ToInt32(bad.work_id);
                                        //save data
                                        //if (dsgs.Tables[1].Rows.Count > 0)
                                        //{

                                        //    Workid = Convert.ToInt32(dsgs.Tables[1].Rows[0]["intWorkid"].ToString().Trim());
                                        //    if (dsgs.Tables[2].Rows.Count == 0)
                                        //    {
                                        //      //  Gen.InsertAPIFailure_Status(bad.household_number, "For this work request number is not compared", Convert.ToString(bad.created_at));

                                        //     //   AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Failed", "For this work request number is not compared", System.DateTime.Now.ToString(), bad.household_number, bad.uuid);
                                        //    }
                                        //    else
                                        //    {
                                        //if (dsgs.Tables[3].Rows.Count == 0)
                                        //{
                                        // //   Gen.InsertAPIFailure_Status(bad.household_number, "Benficiary is not there in work with request number", Convert.ToString(bad.created_at));

                                        // //   AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Failed", "Benficiary is not there in work with request number", System.DateTime.Now.ToString(), bad.household_number, bad.uuid);
                                        //}
                                        //else
                                        //{

                                        try
                                        {

                                            Bentransid = Convert.ToInt32(bad.trans_id);
                                            if (bad.status == "1")
                                            {
                                                //   int s1 = Gen.BenefeciaryAttendance(dsgs.Tables[1].Rows[0]["intWorkid"].ToString().Trim(), dsgs.Tables[0].Rows[0]["intBenid"].ToString().Trim(), "Present", bad.attendance_date, dsgs.Tables[0].Rows[0]["intIP"].ToString().Trim());
                                               
                                                  int s1check = Gen.InsUpdBeneficiaryWagesFromToDateLatestFinalUSDServiceCheckingAlternate(Bene_ID.ToString(), bad.date_from, bad.date_to, bad.created_at, Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage) * Convert.ToDecimal(bad.attendance)).ToString("f0"), IPid.ToString(), Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage)).ToString("f0"), Workid.ToString(), bad.exchange_rate.ToString(), bad.amount.ToString(), bad.wage.ToString(), bad.uuid, bad.attendance, bad.generated_at, bad.paid_to, bad.trans_id, bad.matched_fp);

                                                  if (s1check == 1)
                                                  {

                                                      int resultwage = Gen.insertWageRequestPaymentService(bad.uuid, bad.exchange_rate, IPid.ToString());

                                                      int s2 = Gen.InsUpdBeneficiaryWagesRequestPayingAgent(Bentransid.ToString(), bad.uuid, Workid.ToString(), Bene_ID.ToString(), bad.attendance, bad.wage, bad.amount, Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage)).ToString("f0"), Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage) * Convert.ToDecimal(bad.attendance)).ToString("f0"), IPid.ToString(), resultwage.ToString());

                                                      int s1 = Gen.InsUpdBeneficiaryWagesFromToDateLatestFinalUSDService(Bene_ID.ToString(), bad.date_from, bad.date_to, bad.created_at, Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage) * Convert.ToDecimal(bad.attendance)).ToString("f0"), IPid.ToString(), Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage)).ToString("f0"), Workid.ToString(), bad.exchange_rate.ToString(), bad.amount.ToString(), bad.wage.ToString(), bad.uuid, bad.attendance, bad.generated_at, bad.paid_to, bad.trans_id, bad.matched_fp);
                                                  }

                                                //Return JSON - Session Tabe
                                                //////    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                                //////        bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                                //////        bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                                //////        bad.average_household, bad.state, bad.county, bad.payam, bad.boma, bad.latitude.ToString(), bad.longitude.ToString(), bad.legal_status,
                                                //////        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "", System.DateTime.Now.ToString(), Bene_ID.ToString());

                                                //   AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Success", "Payroll for household " + bad.household_number + " saved successfully", System.DateTime.Now.ToString(), bad.household_number, bad.uuid);

                                            }
                                            else
                                            {

                                                int s1 = Gen.InsUpdBeneficiaryWagesFromToDateLatestFinalUSDServiceNotPaid(Bene_ID.ToString(), bad.date_from, bad.date_to, bad.created_at, Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage) * Convert.ToDecimal(bad.attendance)).ToString("f0"), IPid.ToString(), Convert.ToDecimal(Convert.ToDecimal(bad.exchange_rate) * Convert.ToDecimal(bad.wage)).ToString("f0"), Workid.ToString(), bad.exchange_rate.ToString(), bad.amount.ToString(), bad.wage.ToString(), bad.uuid, bad.attendance, bad.generated_at, "", bad.trans_id);

                                                //     AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Not Paid", "Not Paid", System.DateTime.Now.ToString(), bad.household_number, bad.uuid);

                                            }
                                        }
                                        catch (Exception ex)
                                        {

                                            //  Gen.InsertAPIFailure_Status(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at));

                                            //   AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Failed", ex.ToString(), System.DateTime.Now.ToString(), bad.household_number, bad.uuid);
                                        }
                                        // }
                                        // }
                                        //}
                                        //else
                                        //{
                                        //  //  Gen.InsertAPIFailure_Status(bad.household_number, "Work is not available for this Work ID", Convert.ToString(bad.created_at));

                                        // //   AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Failed", "Work is not available for this Work ID", System.DateTime.Now.ToString(), bad.household_number, bad.uuid);

                                        //}

                                    }
                                    else
                                    {
                                        //  Gen.InsertAPIFailure_Status(bad.household_number, "Benficiary is not available for this house hold number", Convert.ToString(bad.created_at));
                                        //   AddDataToTableCeertificate((DataTable)Session["ErrorDataW"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.paid_to, bad.enrolment_number, bad.date_from, bad.date_to, bad.exchange_rate, bad.wage, bad.amount, bad.work_id, bad.work_code, bad.attendance, bad.first_name, bad.last_name, bad.surname, bad.uuid, bad.generated_at, bad.created_at, bad.updated_at, "Failed", "Benficiary is not available for this house hold number", System.DateTime.Now.ToString(), bad.household_number, bad.uuid);

                                    }







                                    //Check Boma Code


                                }
                                // success message


                                //DataSet dsa = new DataSet();

                                //dsa.Tables.Add((DataTable)Session["ErrorDataW"]);



                                // JSON Return Process

                                //if (dsa.Tables[0].Rows.Count > 0)
                                //{
                              
                                    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"Done\"}";
                                    //mainresult = mainresult + "{";
                                    //mainresult = "{ \"error\":\"false\",\"errorCode\":" + "\"" + "0" + "\",\"data\":[";
                                    //foreach (DataTable table in dsa.Tables)
                                    //{
                                    //    foreach (DataRow dr in table.Rows)
                                    //    {
                                    //        mainresult = mainresult + "{\"id\":\"" + dr["id"].ToString() + "\",";

                                    //        mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";

                                    //        mainresult = mainresult + "\"Status\":\"" + dr["Status"].ToString() + "\",";
                                    //        mainresult = mainresult + "\"Reason\":\"" + dr["Reason"].ToString() + "\",";
                                    //        mainresult = mainresult + "\"ReturnDate\":\"" + dr["ReturnDate"].ToString() + "\",";
                                    //        mainresult = mainresult + "\"benid\":\"" + dr["benid"].ToString() + "\"},";


                                    //    }
                                    //    mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                                    //}
                                //}
                                //else
                                //{
                                //    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
                                //}










                                //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('Data Inserted Successfully')", true);
                            }
                            else
                            {
                                Gen.InsertAPIFailure_Status("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now));

                                mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found-Error\"}";
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"" + ex.ToString() + "\"}";
                        // Test = false;
                        //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('url does not exist')", true);
                        // return;
                    }
                    if (Test == false)
                    {
                        mainresult = mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"url does not exist\"}";
                        //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('url does not exist')", true);
                        //  return;
                    }


                }

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
        public string matched_fp { get; set; }
        public string uuid { get; set; }
        public string status { get; set; }
        public string generated_at { get; set; }
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

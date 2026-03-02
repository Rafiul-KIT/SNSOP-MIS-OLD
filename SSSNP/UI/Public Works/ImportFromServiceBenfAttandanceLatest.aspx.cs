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

public partial class ImportFromServiceBenfAttandanceLatest : System.Web.UI.Page
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
            Session.Remove("ErrorDataB");
            dtMyTableCertificate = createtablecrtificate();
            Session["ErrorDataB"] = dtMyTableCertificate;
            string result = InsertBenDetails();
            Response.Write(result);
          
        }

    }

    protected DataTable createtablecrtificate()
    {
        dtMyTable = new DataTable("ErrorDataB");


        dtMyTable.Columns.Add("id", typeof(string));
        dtMyTable.Columns.Add("supervisor_id", typeof(string));        
        dtMyTable.Columns.Add("household_number", typeof(string));
        dtMyTable.Columns.Add("beneficiary_id", typeof(string));
        dtMyTable.Columns.Add("idnumber", typeof(string));
        dtMyTable.Columns.Add("matched_fp", typeof(string));
        dtMyTable.Columns.Add("work_id", typeof(string));
        dtMyTable.Columns.Add("uuid", typeof(string));
        dtMyTable.Columns.Add("clock", typeof(string));
        dtMyTable.Columns.Add("time", typeof(string));
        dtMyTable.Columns.Add("attendance_date", typeof(string));              
        dtMyTable.Columns.Add("created_at", typeof(string));
        dtMyTable.Columns.Add("updated_at", typeof(string));
        dtMyTable.Columns.Add("Status", typeof(string));
        dtMyTable.Columns.Add("Reason", typeof(string));
        dtMyTable.Columns.Add("ReturnDate", typeof(string));
        dtMyTable.Columns.Add("benid", typeof(string));
        return dtMyTable;
    }


    private void AddDataToTableCeertificate(DataTable myTable, string id, string supervisor_id, string household_number, string beneficiary_id, string idnumber,
        string matched_fp, string work_id, string uuid, string clock, string time, string attendance_date,
        string created_at, string updated_at, string Status, string Reason, string ReturnDate, string enrollmentNo
)
    {//totStartDate, string totEndDate, string totSummary,
        try
        {
            DataRow Row;
            Row = myTable.NewRow();

            dtMyTable = new DataTable("ErrorDataB");

            Row["id"] = id;
            Row["supervisor_id"] = supervisor_id;          
            Row["household_number"] = household_number;
            Row["beneficiary_id"] = beneficiary_id;
            Row["idnumber"] = idnumber;
            Row["matched_fp"] = matched_fp;
            Row["work_id"] = work_id;
            Row["uuid"] = uuid;
            Row["clock"] = clock;
            Row["time"] = time;
            Row["attendance_date"] = attendance_date;
           
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
    public string InsertBenDetails()
    {
        //dtMyTableCertificate = createtablecrtificate();
        //Session["ErrorData"] = dtMyTableCertificate;
        //string URI = "http:/54.68.125.132:8181/snsdp/index.php/api/v1/getAttendance";
        string URI = System.Configuration.ConfigurationManager.AppSettings["GetAttendance"]; // API_V_1.2
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

                    foreach (Beneficiary_API_Data bad in objdata)
                    {

                        //  DataSet ds_check = new DataSet();
                        if (bad.work_id == "null")
                        {
                            Gen.InsertAPIFailure_StatusAttendance(bad.household_number, "Work id should be manditory", Convert.ToString(bad.created_at), bad.attendance_date, bad.work_id);

                            AddDataToTableCeertificate((DataTable)Session["ErrorDataB"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.beneficiary_id, bad.idnumber, bad.matched_fp, bad.work_id, bad.uuid, bad.clock, bad.time, bad.attendance_date, bad.created_at, bad.updated_at, "Failed", "Work id should be manditory", System.DateTime.Now.ToString(), "");

                        }
                        else
                        {
                            if (bad.household_number == "null")
                            {
                                Gen.InsertAPIFailure_StatusAttendance(bad.household_number, "household_number should be manditory", Convert.ToString(bad.created_at), bad.attendance_date, bad.work_id);

                                AddDataToTableCeertificate((DataTable)Session["ErrorDataB"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.beneficiary_id, bad.idnumber, bad.matched_fp, bad.work_id, bad.uuid, bad.clock, bad.time, bad.attendance_date, bad.created_at, bad.updated_at, "Failed", "household_number should be manditory", System.DateTime.Now.ToString(), "");

                            }
                            else
                            {

                                DataSet dsgs = new DataSet();
                                dsgs = Gen.HouseHoldNumberVerificationAttedance(bad.household_number, bad.attendance_date, bad.work_id);
                                int GroupStrength = 0;
                                if (dsgs.Tables[0].Rows.Count > 0)
                                {

                                    //save data
                                    if (dsgs.Tables[1].Rows.Count > 0)
                                    {
                                        if (dsgs.Tables[2].Rows.Count > 0)
                                        {
                                            Gen.InsertAPIFailure_StatusAttendance(bad.household_number, "For this Benficiary and work attendance is already existed with this date", Convert.ToString(bad.created_at), bad.attendance_date, bad.work_id);

                                            AddDataToTableCeertificate((DataTable)Session["ErrorDataB"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.beneficiary_id, bad.idnumber, bad.matched_fp, bad.work_id, bad.uuid, bad.clock, bad.time, bad.attendance_date, bad.created_at, bad.updated_at, "Failed", "For this Benficiary and work attendance is already existed with this date", System.DateTime.Now.ToString(), "");
                                        }
                                        else
                                        {


                                            int s1 = Gen.BenefeciaryAttendanceAPI(dsgs.Tables[1].Rows[0]["intWorkid"].ToString().Trim(), dsgs.Tables[0].Rows[0]["intBenid"].ToString().Trim(), "Present", bad.attendance_date, dsgs.Tables[0].Rows[0]["intIP"].ToString().Trim());


                                            if (s1 == 100)
                                            {
                                                Gen.InsertAPIFailure_StatusAttendance(bad.household_number, "Beneficiary already applied wage request of this date", Convert.ToString(bad.created_at), bad.attendance_date, bad.work_id);

                                            }
                                            else
                                            {
                                                //Return JSON - Session Tabe
                                                //////    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                                //////        bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                                //////        bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                                //////        bad.average_household, bad.state, bad.county, bad.payam, bad.boma, bad.latitude.ToString(), bad.longitude.ToString(), bad.legal_status,
                                                //////        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "", System.DateTime.Now.ToString(), Bene_ID.ToString());

                                                AddDataToTableCeertificate((DataTable)Session["ErrorDataB"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.beneficiary_id, bad.idnumber, bad.matched_fp, bad.work_id, bad.uuid, bad.clock, bad.time, bad.attendance_date, bad.created_at, bad.updated_at, "Success", "", System.DateTime.Now.ToString(), "");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        Gen.InsertAPIFailure_StatusAttendance(bad.household_number, "Work is not available for this Work ID", Convert.ToString(bad.created_at), bad.attendance_date, bad.work_id);

                                        AddDataToTableCeertificate((DataTable)Session["ErrorDataB"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.beneficiary_id, bad.idnumber, bad.matched_fp, bad.work_id, bad.uuid, bad.clock, bad.time, bad.attendance_date, bad.created_at, bad.updated_at, "Failed", "Work is not available for this Work ID", System.DateTime.Now.ToString(), "");

                                    }

                                }
                                else
                                {
                                    Gen.InsertAPIFailure_StatusAttendance(bad.household_number, "Benficiary is not available for this house hold number", Convert.ToString(bad.created_at),bad.attendance_date,bad.work_id);
                                    AddDataToTableCeertificate((DataTable)Session["ErrorDataB"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.beneficiary_id, bad.idnumber, bad.matched_fp, bad.work_id, bad.uuid, bad.clock, bad.time, bad.attendance_date, bad.created_at, bad.updated_at, "Failed", "Benficiary is not available for this house hold number", System.DateTime.Now.ToString(), "");

                                }
                            }


                        }
                       

                  
                        //Check Boma Code
                       
                       
                    }
                    // success message


                    DataSet dsa = new DataSet();

                    dsa.Tables.Add((DataTable)Session["ErrorDataB"]); 
                    


                    // JSON Return Process

                    if (dsa.Tables[0].Rows.Count > 0)
                    {
                        mainresult = mainresult + "{";
                        mainresult = "{ \"error\":\"false\",\"errorCode\":" + "\"" + "0" + "\",\"data\":[";
                        foreach (DataTable table in dsa.Tables)
                        {
                            foreach (DataRow dr in table.Rows)
                            {
                                mainresult = mainresult + "{\"id\":\"" + dr["id"].ToString() + "\",";

                                mainresult = mainresult + "\"supervisor_id\":\"" + dr["supervisor_id"].ToString() + "\",";
                               
                                mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                                mainresult = mainresult + "\"beneficiary_id\":\"" + dr["beneficiary_id"].ToString() + "\",";
                                mainresult = mainresult + "\"idnumber\":\"" + dr["idnumber"].ToString() + "\",";
                                mainresult = mainresult + "\"matched_fp\":\"" + dr["matched_fp"].ToString() + "\",";
                                mainresult = mainresult + "\"work_id\":\"" + dr["work_id"].ToString() + "\",";
                                mainresult = mainresult + "\"uuid\":\"" + dr["uuid"].ToString() + "\",";
                                mainresult = mainresult + "\"clock\":\"" + dr["clock"].ToString() + "\",";
                                mainresult = mainresult + "\"time\":\"" + dr["time"].ToString() + "\",";
                                mainresult = mainresult + "\"attendance_date\":\"" + dr["attendance_date"].ToString() + "\",";                              
                                mainresult = mainresult + "\"created_at\":\"" + dr["created_at"].ToString() + "\",";
                                mainresult = mainresult + "\"updated_at\":\"" + dr["updated_at"].ToString() + "\",";
                                mainresult = mainresult + "\"Status\":\"" + dr["Status"].ToString() + "\",";
                                mainresult = mainresult + "\"Reason\":\"" + dr["Reason"].ToString() + "\",";
                                mainresult = mainresult + "\"ReturnDate\":\"" + dr["ReturnDate"].ToString() + "\",";

                                mainresult = mainresult + "\"benid\":\"" + dr["benid"].ToString() + "\"},";


                            }
                            mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                        }
                    }
                    else
                    {
                        mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
                    }





                   




                  //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('Data Inserted Successfully')", true);
                }
                else
                {
                    Gen.InsertAPIFailure_StatusAttendance("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now),"","");

                    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found-Error\"}";     
                }
            }
        }
        catch (Exception ex)
        {
           // mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
            mainresult = "{ \"error\":" + ex.ToString() + " \"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
         
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

        return mainresult;
    }

    public class Beneficiary_API_Data
    {

        public int id { get; set; }
        public string supervisor_id { get; set; }      
        public string household_number { get; set; }
        public string beneficiary_id { get; set; }
        public string idnumber { get; set; }
        public string matched_fp { get; set; }
        public string work_id { get; set; }
        public string uuid { get; set; }
        public string clock { get; set; }
        public string time { get; set; }
        public string attendance_date { get; set; }
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

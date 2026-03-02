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

public partial class ImportUpdateHouseHoldName : System.Web.UI.Page
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
            Session.Remove("ErrorData");
            dtMyTableCertificate = createtablecrtificate();
            Session["ErrorData"] = dtMyTableCertificate;
            string result = InsertBenDetails();
            Response.Write(result);
          
        }

    }

    protected DataTable createtablecrtificate()
    {
        dtMyTable = new DataTable("ErrorData");


        dtMyTable.Columns.Add("id", typeof(string));
        dtMyTable.Columns.Add("supervisor_id", typeof(string));
        dtMyTable.Columns.Add("partner_code", typeof(string));
        dtMyTable.Columns.Add("household_number", typeof(string));
        dtMyTable.Columns.Add("beneficiary_type", typeof(string));
        dtMyTable.Columns.Add("group_number", typeof(string));
        dtMyTable.Columns.Add("member_number", typeof(string));
        dtMyTable.Columns.Add("household_size", typeof(string));
        dtMyTable.Columns.Add("first_name", typeof(string));
        dtMyTable.Columns.Add("last_name", typeof(string));
        dtMyTable.Columns.Add("surname", typeof(string));
        dtMyTable.Columns.Add("Income", typeof(string));
        dtMyTable.Columns.Add("age", typeof(string));
        dtMyTable.Columns.Add("spouse_name", typeof(string));
        dtMyTable.Columns.Add("id_number", typeof(string));
        dtMyTable.Columns.Add("phone_number", typeof(string));
        dtMyTable.Columns.Add("gender", typeof(string));
        dtMyTable.Columns.Add("female_dependants", typeof(string));
        dtMyTable.Columns.Add("male_dependants", typeof(string));
        dtMyTable.Columns.Add("zero_five", typeof(string));
        dtMyTable.Columns.Add("six_eighteen", typeof(string));
        dtMyTable.Columns.Add("nineteen_forty_five", typeof(string));
        dtMyTable.Columns.Add("forty_six_sixty_five", typeof(string));
        dtMyTable.Columns.Add("sixty_six", typeof(string));
        dtMyTable.Columns.Add("income_household", typeof(string));
        dtMyTable.Columns.Add("average_household", typeof(string));
        dtMyTable.Columns.Add("state", typeof(string));
        dtMyTable.Columns.Add("county", typeof(string));
        dtMyTable.Columns.Add("payam", typeof(string));
        dtMyTable.Columns.Add("boma", typeof(string));
        dtMyTable.Columns.Add("latitude", typeof(string));
        dtMyTable.Columns.Add("longitude", typeof(string));
        dtMyTable.Columns.Add("legal_status", typeof(string));
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


    private void AddDataToTableCeertificate(DataTable myTable, string id, string supervisor_id, string partner_code, string household_number, string beneficiary_type, string group_number,
        string member_number, string household_size, string first_name, string last_name, string surname, string age,
        string spouse_name, string id_number, string phone_number, string gender, string female_dependants, string male_dependants,
        string zero_five, string six_eighteen, string nineteen_forty_five, string forty_six_sixty_five, string sixty_six, string income_household,
        string average_household, string state, string county, string payam, string boma, string latitude, string longitude, string legal_status,
        string photo_url, string id_url, string created_at, string updated_at, string Status, string Reason, string ReturnDate, string enrollmentNo
)
    {//totStartDate, string totEndDate, string totSummary,
        try
        {
            DataRow Row;
            Row = myTable.NewRow();

            dtMyTable = new DataTable("ErrorData");

            Row["id"] = id;
            Row["supervisor_id"] = supervisor_id;
            Row["partner_code"] = partner_code;
            Row["household_number"] = household_number;
            Row["beneficiary_type"] = beneficiary_type;
            Row["group_number"] = group_number;
            Row["member_number"] = member_number;
            Row["household_size"] = household_size;
            Row["first_name"] = first_name;
            Row["last_name"] = last_name;
            Row["surname"] = surname;
            Row["age"] = age;
            Row["spouse_name"] = spouse_name;
            Row["id_number"] = id_number;
            Row["phone_number"] = phone_number;
            Row["gender"] = gender;
            Row["female_dependants"] = female_dependants;
            Row["male_dependants"] = male_dependants;
            Row["zero_five"] = zero_five;
            Row["six_eighteen"] = six_eighteen;
            Row["nineteen_forty_five"] = nineteen_forty_five;
            Row["forty_six_sixty_five"] = forty_six_sixty_five;
            Row["sixty_six"] = sixty_six;
            Row["income_household"] = income_household;
            Row["average_household"] = average_household;
            Row["state"] = state;
            Row["county"] = county;
            Row["payam"] = payam;
            Row["boma"] = boma;
            Row["latitude"] = latitude;
            Row["longitude"] = longitude;
            Row["legal_status"] = legal_status;
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
    public string InsertBenDetails()
    {
        
        //dtMyTableCertificate = createtablecrtificate();
        //Session["ErrorData"] = dtMyTableCertificate;
        //string URI = "http:/54.68.125.132:8181/snsdp/index.php/api/v1/getHouseholds";
        string URI = System.Configuration.ConfigurationManager.AppSettings["GetHouseholds"]; // API_V_1.2
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


                        







                            try
                            {
                                Bene_ID = Gen.UpdateHouseholdname( Convert.ToString(bad.household_number),
                                                     Convert.ToString(bad.household_name),
                                                     "1000"
                                                     );
                            }

                            catch (Exception ex)
                            {

                             //   Gen.InsertAPIFailure_StatusIPid(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at), bad.partner_code.ToString());


                                //Return JSON - Session Tabe

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                      bad.household_size, bad.household_name, bad.household_name, bad.household_name, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                      bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                      bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                              (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                      bad.photo_url, "", bad.created_at, bad.updated_at, "Failed", ex.ToString(), System.DateTime.Now.ToString(), "");


                            }




                            //Return JSON - Session Tabe
                            //////    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                            //////        bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                            //////        bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                            //////        bad.average_household, bad.state, bad.county, bad.payam, bad.boma, bad.latitude.ToString(), bad.longitude.ToString(), bad.legal_status,
                            //////        bad.photo_url, "", bad.created_at, bad.updated_at, "Success", "", System.DateTime.Now.ToString(), Bene_ID.ToString());
                            // }
                            //}
                            //else
                            //{
                            //    Gen.InsertAPIFailure_Status(bad.household_number, "There is no group strength for this boma", Convert.ToString(bad.created_at));


                            //    //Return JSON - Session Tabe

                            //    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                            //      bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                            //      bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                            //      bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                            //          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                            //      bad.photo_url, "", bad.created_at, bad.updated_at, "Failed", "There is no group strength for this boma", System.DateTime.Now.ToString(), "");



                            //}



                            //Any Other Error
                            //if (Bene_ID == 0)
                            //{
                            //    Gen.InsertAPIFailure_Status(bad.household_number, "Data is not inserted", Convert.ToString(bad.created_at));
                            //}
                            //Check Boma Code
                            if (Bene_ID.ToString().Trim().Length >= 4)
                            {
                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                           bad.household_size, bad.household_name, bad.household_name, bad.household_name, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                           bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                           bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                           bad.photo_url, "", bad.created_at, bad.updated_at, "Success", "0", System.DateTime.Now.ToString(), Bene_ID.ToString());




                             










                            }
                            




                       
                       
                    }
                    




                        DataSet dsa = new DataSet();

                        dsa.Tables.Add((DataTable)Session["ErrorData"]);



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

                                 
                                    mainresult = mainresult + "\"partner_code\":\"" + dr["partner_code"].ToString() + "\",";
                                    mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                                   mainresult = mainresult + "\"Status\":\"" + dr["Status"].ToString() + "\",";
                                    mainresult = mainresult + "\"Reason\":\"" + dr["Reason"].ToString() + "\",";
                                  

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
                    Gen.InsertAPIFailure_Status("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now));

                    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found-Error\"}";     
                }
                
            }
        }
        catch (Exception ex)
        {
            mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \""+ ex.ToString() +"\"}";
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
        public string partner_code { get; set; }
        public string household_number { get; set; }
        public string beneficiary_type { get; set; }
        public string group_number { get; set; }
        public string member_number { get; set; }
        public string household_size { get; set; }
        public string household_name { get; set; }     
        public string age { get; set; }
        public string spouse_name { get; set; }
        public string id_number { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string female_dependants { get; set; }
        public string male_dependants { get; set; }
        public string zero_five { get; set; }
        public string six_eighteen { get; set; }
        public string nineteen_forty_five { get; set; }
        public string forty_six_sixty_five { get; set; }
        public string sixty_six { get; set; }
        public string income_household { get; set; }
        public string average_household { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string payam { get; set; }
        public string boma { get; set; }
        public object latitude { get; set; }
        public object longitude { get; set; }
        public string legal_status { get; set; }     
        public string photo_url { get; set; }
       
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        //ALternate     
        public string alternate_number { get; set; }
    }

    public class RootObject
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public List<Beneficiary_API_Data> data { get; set; }
    }
  
}

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
public partial class ImportFromServiceBenfDet : System.Web.UI.Page
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
        //string URI = "http:/54.68.125.132:8181/biometrics/index.php/api/v1/getHouseholds";
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

                        DataSet ds_check = new DataSet();
                        string intBomasid = bad.boma;
                        Bene_ID = 0;

                        string GroupId = bad.group_number;
                        if (GroupId != "")
                        {
                            string BeneficiaryType = "";
                            if (bad.beneficiary_type == "General Group")
                            {
                                BeneficiaryType = "Group";
                            }
                            else
                            {
                                BeneficiaryType = bad.beneficiary_type;
                            }
                            ds_check = Gen.CountBeneficiaryType(intBomasid, GroupId, BeneficiaryType);
                            if (ds_check.Tables[0].Rows.Count > 0)
                            {

                                if (bad.boma == "null")
                                {
                                    Gen.InsertAPIFailure_Status(bad.household_number, "Boma id is not valid - You have passed null", Convert.ToString(bad.created_at));

                                //Return JSON - Session Tabe

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                  bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                  bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                  bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                  bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Boma id is not valid - You have passed null", System.DateTime.Now.ToString(), "");

                                }
                                else{



                                //DataSet dsgs = new DataSet();
                                //dsgs = Gen.GetGroupStrength(intBomasid, GroupId, BeneficiaryType);
                                //int GroupStrength = 0;
                                //if (dsgs.Tables[0].Rows.Count > 0)
                                //{
                                //    GroupStrength = Convert.ToInt16(dsgs.Tables[0].Rows[0]["GroupStrength"].ToString());
                                //    int Beneficiary = Convert.ToInt16(ds_check.Tables[0].Rows[0]["CountBeneficiary"].ToString());
                                    //if (Beneficiary >= GroupStrength)
                                    //{
                                    //    Gen.InsertAPIFailure_Status(bad.household_number, "Group strength exceed", Convert.ToString(bad.created_at));

                                    //    //Return JSON - Session Tabe

                                    //    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                    //      bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                    //      bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                    //      bad.average_household, bad.state, bad.county, bad.payam, bad.boma,
                                    //      (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                    //      (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)),
                                    //       bad.legal_status,
                                    //      bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Group strength exceed", System.DateTime.Now.ToString(), "");

                                    //}
                                    //else
                                    //{
                                        //save data
                                     //   bad.partner_code = "1001";
                                            Bene_ID = Gen.InsertDataUsingAPI(Convert.ToString(bad.partner_code), Convert.ToString(bad.household_number),
                                                                 (Convert.ToString(bad.beneficiary_type) == "General Group" ? "Group" : Convert.ToString(bad.beneficiary_type))
                                                                 , Convert.ToString(bad.group_number),
                                                                 Convert.ToString(bad.supervisor_id), Convert.ToString(bad.member_number),
                                                                 Convert.ToString(bad.household_size),
                                                                 Convert.ToString(bad.surname + " " + bad.first_name + " " + bad.last_name),
                                                                 Convert.ToString(bad.age), Convert.ToString(bad.spouse_name),
                                                                 Convert.ToString(bad.id_number), Convert.ToString(bad.phone_number),
                                                                 Convert.ToString(bad.gender), Convert.ToString(bad.female_dependants),
                                                                 Convert.ToString(bad.male_dependants), Convert.ToString(bad.female_dependants + bad.male_dependants),
                                                                 Convert.ToString(bad.zero_five), Convert.ToString(bad.six_eighteen),
                                                                 Convert.ToString(bad.nineteen_forty_five), Convert.ToString(bad.forty_six_sixty_five),
                                                                 Convert.ToString(bad.sixty_six), Convert.ToString(bad.income_household),
                                                                 Convert.ToString(bad.average_household), Convert.ToString(bad.state),
                                                                 Convert.ToString(bad.county), Convert.ToString(bad.payam),
                                                                 Convert.ToString(bad.boma), Convert.ToString(bad.latitude),
                                                                 Convert.ToString(bad.longitude), Convert.ToString(bad.legal_status),
                                                                 Convert.ToString(bad.photo_url), Convert.ToString(bad.id_url),
                                                                 Convert.ToString(bad.created_at), Convert.ToString(bad.updated_at), Convert.ToString(bad.partner_code),
                                                                 Convert.ToString(bad.selection_criteria), Convert.ToString(bad.selection_reason)
                                                                 );
                                        

                                }

                                        //Return JSON - Session Tabe
                                        //////    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                        //////        bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                        //////        bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                        //////        bad.average_household, bad.state, bad.county, bad.payam, bad.boma, bad.latitude.ToString(), bad.longitude.ToString(), bad.legal_status,
                                        //////        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "", System.DateTime.Now.ToString(), Bene_ID.ToString());
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
                                //      bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "There is no group strength for this boma", System.DateTime.Now.ToString(), "");



                                //}
                            }
                            else
                            {
                                if (bad.boma == "null")
                                {
                                    Gen.InsertAPIFailure_Status(bad.household_number, "Boma id is not valid - You have passed null", Convert.ToString(bad.created_at));

                                    //Return JSON - Session Tabe

                                    AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                      bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                      bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                      bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                              (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                      bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Boma id is not valid - You have passed null", System.DateTime.Now.ToString(), "");

                                }
                                else
                                {
                                    Bene_ID = Gen.InsertDataUsingAPI(Convert.ToString(bad.partner_code), Convert.ToString(bad.household_number),
                                                                 (Convert.ToString(bad.beneficiary_type) == "General Group" ? "Group" : Convert.ToString(bad.beneficiary_type))
                                                                 , Convert.ToString(bad.group_number),
                                                                 Convert.ToString(bad.supervisor_id), Convert.ToString(bad.member_number),
                                                                 Convert.ToString(bad.household_size),
                                                                 Convert.ToString(bad.surname + " " + bad.first_name + " " + bad.last_name),
                                                                 Convert.ToString(bad.age), Convert.ToString(bad.spouse_name),
                                                                 Convert.ToString(bad.id_number), Convert.ToString(bad.phone_number),
                                                                 Convert.ToString(bad.gender), Convert.ToString(bad.female_dependants),
                                                                 Convert.ToString(bad.male_dependants), Convert.ToString(bad.female_dependants + bad.male_dependants),
                                                                 Convert.ToString(bad.zero_five), Convert.ToString(bad.six_eighteen),
                                                                 Convert.ToString(bad.nineteen_forty_five), Convert.ToString(bad.forty_six_sixty_five),
                                                                 Convert.ToString(bad.sixty_six), Convert.ToString(bad.income_household),
                                                                 Convert.ToString(bad.average_household), Convert.ToString(bad.state),
                                                                 Convert.ToString(bad.county), Convert.ToString(bad.payam),
                                                                 Convert.ToString(bad.boma), Convert.ToString(bad.latitude),
                                                                 Convert.ToString(bad.longitude), Convert.ToString(bad.legal_status),
                                                                 Convert.ToString(bad.photo_url), Convert.ToString(bad.id_url),
                                                                 Convert.ToString(bad.created_at), Convert.ToString(bad.updated_at), Convert.ToString(bad.partner_code),
                                                                 Convert.ToString(bad.selection_criteria), Convert.ToString(bad.selection_reason)
                                                                 );

                                }

                                //Return JSON - Session Tabe
                                //////////AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                //////////    bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                //////////    bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                //////////    bad.average_household, bad.state, bad.county, bad.payam, bad.boma, bad.latitude.ToString(), bad.longitude.ToString(), bad.legal_status,
                                //////////    bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "", System.DateTime.Now.ToString(), Bene_ID.ToString());

                            }

                            //Any Other Error
                            //if (Bene_ID == 0)
                            //{
                            //    Gen.InsertAPIFailure_Status(bad.household_number, "Data is not inserted", Convert.ToString(bad.created_at));
                            //}
                            //Check Boma Code
                            if (Bene_ID.ToString().Trim().Length >= 4)
                            {
                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                           bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                           bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                           bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                           bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "0", System.DateTime.Now.ToString(), Bene_ID.ToString());


                                 

                                string Directory_Path = Server.MapPath("~/Photo/" + Bene_ID);
                                string Path1 = Server.MapPath("~/Photo/" + Bene_ID + "/Household");
                                //string Path2 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/FirstAlternate");
                                //string Path3 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/SecondAlternate");
                                if (!Directory.Exists(Directory_Path))
                                {
                                    Directory.CreateDirectory(Directory_Path);

                                    Directory.CreateDirectory(Path1);
                                    //Directory.CreateDirectory(Path2);
                                    //Directory.CreateDirectory(Path3);
                                }
                                if (Convert.ToString(bad.photo_url) != "")
                                {
                                    try
                                    {
                                    //string File_Url = "http:/54.68.125.132:8181/biometrics/public/attachments/" + Convert.ToString(bad.photo_url);
                                    string File_Url = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + Convert.ToString(bad.photo_url); // API_V_1.2
                                        byte[] data;
                                    using (WebClient client = new WebClient())
                                    {
                                        data = client.DownloadData(File_Url);
                                    }
                                    household_photo_path = "~/Photo/" + Bene_ID + "/Household/" + Convert.ToString(bad.photo_url);
                                    string server_household_photo_path = Path1 + "/" + Convert.ToString(bad.photo_url);
                                    if (File.Exists(household_photo_path))
                                    {
                                        File.Delete(household_photo_path);
                                    }
                                    File.WriteAllBytes(server_household_photo_path, data);
                                    int p = Gen.UploadHouseHoldPhoto(Bene_ID, household_photo_path, "PhotoPath", bad.photo_url);

                                    }
                                     catch (Exception ex)
                                     {
                                         if (ex.ToString() == "The remote server returned an error: (404) Not Found")
                                         {
                                             Gen.InsertAPIFailure_Status(bad.household_number, "File is not in API URL (Payconnect Team)", Convert.ToString(bad.created_at));


                                             //Return JSON - Session Tabe

                                             AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                   bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                   bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                   bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                           (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                   bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "File is not in API URL (Payconnect Team)", System.DateTime.Now.ToString(), "");
                                         
                                         
                                         }
                                         else
                                         {
                                             Gen.InsertAPIFailure_Status(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at));


                                             //Return JSON - Session Tabe

                                             AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                   bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                   bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                   bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                           (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                   bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", ex.ToString(), System.DateTime.Now.ToString(), "");

                                         }

                                     
                                         
                                         }
                                }










                            }
                            else if (Bene_ID == 98)
                            {
                                Gen.InsertAPIFailure_Status(bad.household_number, "Boma id is not valid", Convert.ToString(bad.created_at));

                                //Return JSON - Session Tabe

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                  bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                  bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                  bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                  bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Boma id is not valid", System.DateTime.Now.ToString(), "");

                            }
                            //Duplicate Beneficiary Check
                            else if (Bene_ID == 99)
                            {
                                Gen.InsertAPIFailure_Status(bad.household_number, "Duplicate household number in enrolled benficiary data", Convert.ToString(bad.created_at));

                                //Return JSON - Session Tabe

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                  bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                  bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                  bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                  bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Duplicate household number", System.DateTime.Now.ToString(), "0");

                            }
                            else if (Bene_ID == 97)
                            {
                                Gen.InsertAPIFailure_Status(bad.household_number, "Duplicate household number in pre enrolled  benficiary data", Convert.ToString(bad.created_at));

                                //Return JSON - Session Tabe

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                  bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                  bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                  bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                  bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Duplicate household number in Service Benficiaries", System.DateTime.Now.ToString(), "0");

                            }
                            else
                            {

                                string message = "";

                                if (Bene_ID == 98)
                                {
                                    message = "Boma id is not valid";
                                }
                                else if (Bene_ID == 99)
                                {
                                    message = "Duplicate household number in enrolled benficiary data";
                                }
                                //Duplicate Beneficiary Check

                                else if (Bene_ID == 97)
                                {
                                    message = "Duplicate household number in pre enrolled  benficiary data";

                                }
                                else if (Bene_ID == 96)
                                {
                                    message = "Partner Code should be manditory";

                                }
                                else if (Bene_ID == 95)
                                {
                                    message = "Boma is Invalid or should be manditory";

                                }
                                else if (Bene_ID == 94)
                                {
                                    message = "household_number should be manditory";

                                }
                                else if (Bene_ID == 93)
                                {
                                    message = "State should be manditory";

                                }
                                else if (Bene_ID == 92)
                                {
                                    message = "Countie should be manditory";

                                }
                                else if (Bene_ID == 91)
                                {
                                    message = "Payam should be manditory";

                                }
                                else if (Bene_ID == 90)
                                {
                                    message = "Benficiary Name should be manditory";

                                }
                                else if (Bene_ID == 89)
                                {
                                    message = "Partner_code is not registered in MIS";

                                }
                                else if (Bene_ID == 88)
                                {
                                    message = "Boma is not belongs to Payam";

                                }
                                else if (Bene_ID == 87)
                                {
                                    message = "Payam is not belongs to Payam";

                                }
                                else if (Bene_ID == 86)
                                {
                                    message = "Countie is not belongs to Payam";

                                }
                                else if (Bene_ID == 85)
                                {
                                    message = "Boma should be Manditory";

                                }
                                else
                                {
                                    message = "";

                                }
                                Gen.InsertAPIFailure_Status(bad.household_number, message, Convert.ToString(bad.created_at));

                                //Return JSON - Session Tabe

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                  bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                  bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                  bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                  bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", message, System.DateTime.Now.ToString(), "");


                            }
                        }
                        else

                        {

                            Bene_ID = Gen.InsertDataUsingAPI(Convert.ToString(bad.partner_code), Convert.ToString(bad.household_number),
                                                              (Convert.ToString(bad.beneficiary_type) == "General Group" ? "Group" : Convert.ToString(bad.beneficiary_type))
                                                              , Convert.ToString(bad.group_number),
                                                              Convert.ToString(bad.supervisor_id), Convert.ToString(bad.member_number),
                                                              Convert.ToString(bad.household_size),
                                                              Convert.ToString(bad.surname + " " + bad.first_name + " " + bad.last_name),
                                                              Convert.ToString(bad.age), Convert.ToString(bad.spouse_name),
                                                              Convert.ToString(bad.id_number), Convert.ToString(bad.phone_number),
                                                              Convert.ToString(bad.gender), Convert.ToString(bad.female_dependants),
                                                              Convert.ToString(bad.male_dependants), Convert.ToString(bad.female_dependants + bad.male_dependants),
                                                              Convert.ToString(bad.zero_five), Convert.ToString(bad.six_eighteen),
                                                              Convert.ToString(bad.nineteen_forty_five), Convert.ToString(bad.forty_six_sixty_five),
                                                              Convert.ToString(bad.sixty_six), Convert.ToString(bad.income_household),
                                                              Convert.ToString(bad.average_household), Convert.ToString(bad.state),
                                                              Convert.ToString(bad.county), Convert.ToString(bad.payam),
                                                              Convert.ToString(bad.boma), Convert.ToString(bad.latitude),
                                                              Convert.ToString(bad.longitude), Convert.ToString(bad.legal_status),
                                                              Convert.ToString(bad.photo_url), Convert.ToString(bad.id_url),
                                                              Convert.ToString(bad.created_at), Convert.ToString(bad.updated_at), Convert.ToString(bad.partner_code),
                                                              Convert.ToString(bad.selection_criteria), Convert.ToString(bad.selection_reason)
                                                              );






                            if (Bene_ID.ToString().Trim().Length >= 4)
                            {
                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                                           bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                                           bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                                           bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                          (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                                           bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "0", System.DateTime.Now.ToString(), Bene_ID.ToString());




                                string Directory_Path = Server.MapPath("~/Photo/" + Bene_ID);
                                string Path1 = Server.MapPath("~/Photo/" + Bene_ID + "/Household");
                                //string Path2 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/FirstAlternate");
                                //string Path3 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/SecondAlternate");
                                if (!Directory.Exists(Directory_Path))
                                {
                                    Directory.CreateDirectory(Directory_Path);

                                    Directory.CreateDirectory(Path1);
                                    //Directory.CreateDirectory(Path2);
                                    //Directory.CreateDirectory(Path3);
                                }
                                if (Convert.ToString(bad.photo_url) != "")
                                {
                                    //string File_Url = "http:/54.68.125.132:8181/biometrics/public/attachments/" + Convert.ToString(bad.photo_url);
                                    string File_Url = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + Convert.ToString(bad.photo_url); // API_V_1.2
                                    byte[] data;
                                    using (WebClient client = new WebClient())
                                    {
                                        data = client.DownloadData(File_Url);
                                    }
                                    household_photo_path = "~/Photo/" + Bene_ID + "/Household/" + Convert.ToString(bad.photo_url);
                                    string server_household_photo_path = Path1 + "/" + Convert.ToString(bad.photo_url);
                                    if (File.Exists(household_photo_path))
                                    {
                                        File.Delete(household_photo_path);
                                    }
                                    File.WriteAllBytes(server_household_photo_path, data);
                                    int p = Gen.UploadHouseHoldPhoto(Bene_ID, household_photo_path, "PhotoPath", bad.photo_url);
                                }










                            }
                            else
                            {
                                string message = "";
                            
                             if (Bene_ID == 98)
                            {
                                message = "Boma id is not valid";
                            }
                             else if (Bene_ID == 99)
                             {
                                 message = "Duplicate household number in enrolled benficiary data";
                             }
                             //Duplicate Beneficiary Check
                             
                             else if (Bene_ID == 97)
                             {
                                 message="Duplicate household number in pre enrolled  benficiary data";
                                
                             }
                             else if (Bene_ID == 96)
                             {
                                 message = "Partner Code should be manditory";

                             }
                             else if (Bene_ID == 95)
                             {
                                 message = "Boma is Invalid or should be manditory";

                             }
                             else if (Bene_ID == 94)
                             {
                                 message = "household_number should be manditory";

                             }
                             else if (Bene_ID == 93)
                             {
                                 message = "State should be manditory";

                             }
                             else if (Bene_ID == 92)
                             {
                                 message = "Countie should be manditory";

                             }
                             else if (Bene_ID == 91)
                             {
                                 message = "Payam should be manditory";

                             }
                             else if (Bene_ID == 90)
                             {
                                 message = "Benficiary Name should be manditory";

                             }
                             else if (Bene_ID == 89)
                             {
                                 message = "Partner_code is not registered in MIS";

                             }
                             else if (Bene_ID == 87)
                             {
                                 message = "Payam is not belongs to Payam";

                             }
                             else if (Bene_ID == 86)
                             {
                                 message = "Countie is not belongs to Payam";

                             }
                             else if (Bene_ID == 85)
                             {
                                 message = "Boma should be Manditory";

                             }
                             else
                             {
                                   message = "";

                             }
                             Gen.InsertAPIFailure_Status(bad.household_number, message, Convert.ToString(bad.created_at));

                             //Return JSON - Session Tabe

                             AddDataToTableCeertificate((DataTable)Session["ErrorData"], bad.id.ToString(), bad.supervisor_id, bad.partner_code, bad.household_number, bad.beneficiary_type, bad.group_number, bad.member_number,
                               bad.household_size, bad.first_name, bad.last_name, bad.surname, bad.age, bad.spouse_name, bad.id_number, bad.phone_number, bad.gender, bad.female_dependants,
                               bad.male_dependants, bad.zero_five, bad.six_eighteen, bad.nineteen_forty_five, bad.forty_six_sixty_five, bad.sixty_six, bad.income_household,
                               bad.average_household, bad.state, bad.county, bad.payam, bad.boma, (Convert.ToString(bad.latitude) == null || Convert.ToString(bad.latitude) == "" ? "0" : Convert.ToString(bad.latitude)),
                                       (Convert.ToString(bad.longitude) == null || Convert.ToString(bad.longitude) == "" ? "0" : Convert.ToString(bad.longitude)), bad.legal_status,
                               bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", message, System.DateTime.Now.ToString(), "");

                            
                            }
                        
                        
                        }
                        // success message


                      
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

                                mainresult = mainresult + "\"supervisor_id\":\"" + dr["supervisor_id"].ToString() + "\",";
                                mainresult = mainresult + "\"partner_code\":\"" + dr["partner_code"].ToString() + "\",";
                                mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                                mainresult = mainresult + "\"beneficiary_type\":\"" + dr["beneficiary_type"].ToString() + "\",";
                                mainresult = mainresult + "\"group_number\":\"" + dr["group_number"].ToString() + "\",";
                                mainresult = mainresult + "\"member_number\":\"" + dr["member_number"].ToString() + "\",";
                                mainresult = mainresult + "\"household_size\":\"" + dr["household_size"].ToString() + "\",";
                                mainresult = mainresult + "\"first_name\":\"" + dr["first_name"].ToString() + "\",";
                                mainresult = mainresult + "\"last_name\":\"" + dr["last_name"].ToString() + "\",";
                                mainresult = mainresult + "\"surname\":\"" + dr["surname"].ToString() + "\",";
                                mainresult = mainresult + "\"age\":\"" + dr["age"].ToString() + "\",";
                                mainresult = mainresult + "\"spouse_name\":\"" + dr["spouse_name"].ToString() + "\",";
                                mainresult = mainresult + "\"id_number\":\"" + dr["id_number"].ToString() + "\",";
                                mainresult = mainresult + "\"phone_number\":\"" + dr["phone_number"].ToString() + "\",";
                                mainresult = mainresult + "\"gender\":\"" + dr["gender"].ToString() + "\",";
                                mainresult = mainresult + "\"female_dependants\":\"" + dr["female_dependants"].ToString() + "\",";
                                mainresult = mainresult + "\"male_dependants\":\"" + dr["male_dependants"].ToString() + "\",";
                                mainresult = mainresult + "\"zero_five\":\"" + dr["zero_five"].ToString() + "\",";
                                mainresult = mainresult + "\"six_eighteen\":\"" + dr["six_eighteen"].ToString() + "\",";
                                mainresult = mainresult + "\"nineteen_forty_five\":\"" + dr["nineteen_forty_five"].ToString() + "\",";
                                mainresult = mainresult + "\"forty_six_sixty_five\":\"" + dr["forty_six_sixty_five"].ToString() + "\",";
                                mainresult = mainresult + "\"sixty_six\":\"" + dr["sixty_six"].ToString() + "\",";
                                mainresult = mainresult + "\"income_household\":\"" + dr["income_household"].ToString() + "\",";
                                mainresult = mainresult + "\"average_household\":\"" + dr["average_household"].ToString() + "\",";
                                mainresult = mainresult + "\"state\":\"" + dr["state"].ToString() + "\",";
                                mainresult = mainresult + "\"county\":\"" + dr["county"].ToString() + "\",";
                                mainresult = mainresult + "\"payam\":\"" + dr["payam"].ToString() + "\",";
                                mainresult = mainresult + "\"boma\":\"" + dr["boma"].ToString() + "\",";
                                mainresult = mainresult + "\"latitude\":\"" + dr["latitude"].ToString() + "\",";
                                mainresult = mainresult + "\"longitude\":\"" + dr["longitude"].ToString() + "\",";
                                mainresult = mainresult + "\"legal_status\":\"" + dr["legal_status"].ToString() + "\",";
                                mainresult = mainresult + "\"photo_url\":\"" + dr["photo_url"].ToString() + "\",";
                                mainresult = mainresult + "\"id_url\":\"" + dr["id_url"].ToString() + "\",";
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
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string surname { get; set; }
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
        public string finger_print_one { get; set; }
        public string finger_print_two { get; set; }
        public string finger_print_three { get; set; }
        public string finger_print_four { get; set; }
        public string photo_url { get; set; }
        public string id_url { get; set; }
        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        //ALternate     
        public string alternate_number { get; set; }

        // new field
        public string selection_criteria { get; set; }
        public string selection_reason { get; set; }

    }

    public class RootObject
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public List<Beneficiary_API_Data> data { get; set; }
    }
  
}

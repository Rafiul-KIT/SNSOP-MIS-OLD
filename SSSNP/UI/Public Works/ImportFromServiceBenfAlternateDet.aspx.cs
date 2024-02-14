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
public partial class ImportFromServiceBenfAlternateDet : System.Web.UI.Page
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
        //dtMyTableCertificate = createtablecrtificate();
        //Session["ErrorData"] = dtMyTableCertificate;
        //string URI = "http:/54.68.125.132:8181/biometrics/index.php/api/v1/getAlternates";
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
            JavaScriptSerializer js = new JavaScriptSerializer();
   js.MaxJsonLength = Int32.MaxValue;
            RootObject obj = js.Deserialize<RootObject>(jsonresponse);
            if (obj != null)
            {
                if (obj.error == false)
                {
                    List<Beneficiary_API_Data> objdata = new List<Beneficiary_API_Data>();
                    objdata = obj.data;
                  
                    int Bene_ID = 0;

                    foreach (Beneficiary_API_Data bad in objdata)
                    {

                        DataSet ds_check = new DataSet();
                       
                        string householdnumber = bad.household_number;

                        ds_check = Gen.HouseHoldNumberVerification(householdnumber);
                        if (ds_check.Tables[0].Rows.Count > 0)
                        {
                             int check = 0;

                             if (ds_check.Tables[1].Rows.Count > 0)
                             {
                                 check = 1;
                             }
                             else
                             {
                                 check = 0;
                             }


                             if (check == 1)
                             {
                                 int Alt_update = Gen.UpdatesecondAlternate(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), Convert.ToString(bad.alternate_number),
                                                                                           Convert.ToString(bad.surname + " " + bad.first_name + " " + bad.last_name),
                                                                                           Convert.ToString(bad.age),
                                                                                           Convert.ToString(bad.id_number),
                                                                                           Convert.ToString(bad.gender),
                                                                                           Convert.ToString(bad.phone_number),
                                                                                           Convert.ToString(bad.photo_url));

                                 //Update Alternate Photo

                                 string Directory_Path = Server.MapPath("~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()));
                                 string Path1 = Server.MapPath("~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()));
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
                                         //string Alt_File_Url = "http:/54.68.125.132:8181/biometrics/public/attachments/" + Convert.ToString(bad.photo_url);
                                         string Alt_File_Url = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + Convert.ToString(bad.photo_url); // API_V_1.2
                                        byte[] Alt_data;
                                         using (WebClient Alt_client = new WebClient())
                                         {
                                             Alt_data = Alt_client.DownloadData(Alt_File_Url);
                                         }
                                         string household_photo_path = "~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()) + "/" + Convert.ToString(bad.photo_url);
                                         string alternate_photo_path = Path1 + "/" + Convert.ToString(bad.photo_url);
                                         if (File.Exists(household_photo_path))
                                         {
                                             File.Delete(household_photo_path);
                                         }
                                         File.WriteAllBytes(alternate_photo_path, Alt_data);
                                         int p = Gen.UploadHouseHoldPhoto(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), alternate_photo_path, "SecondAlternatePayeePhotoPath", bad.photo_url);
                                     }
                                     catch (Exception ex)
                                     {
                                         if (ex.ToString() == "The remote server returned an error: (404) Not Found")
                                         {
                                             Gen.InsertAPIFailure_Status(bad.household_number, "File is not in API URL (Payconnect Team)", Convert.ToString(bad.created_at));


                                             //Return JSON - Session Tabe

                                             AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.alternate_number,
                                                         bad.first_name, bad.last_name, bad.surname, bad.age, bad.id_number, bad.phone_number, bad.gender,
                                                        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "File is not in API URL (Payconnect Team)", System.DateTime.Now.ToString(), "");
                                         }
                                         else
                                         {
                                             Gen.InsertAPIFailure_Status(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at));


                                             //Return JSON - Session Tabe

                                             AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.alternate_number,
                                                         bad.first_name, bad.last_name, bad.surname, bad.age, bad.id_number, bad.phone_number, bad.gender,
                                                        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", ex.ToString(), System.DateTime.Now.ToString(), "");
                                       
                                         }

                                     
                                         
                                         }
                                     
                                     }
                             }
                             else
                             {

                                 int Alt_update = Gen.UpdateAlternate(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), Convert.ToString(bad.alternate_number),
                                                                                          Convert.ToString(bad.surname + " " + bad.first_name + " " + bad.last_name),
                                                                                          Convert.ToString(bad.age),
                                                                                          Convert.ToString(bad.id_number),
                                                                                          Convert.ToString(bad.gender),
                                                                                          Convert.ToString(bad.phone_number),
                                                                                          Convert.ToString(bad.photo_url));



                                 string Directory_Path = Server.MapPath("~/Alternateone/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()));
                                 string Path1 = Server.MapPath("~/Alternateone/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()));
                                 //string Path2 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/FirstAlternate");
                                 //string Path3 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/SecondAlternate");
                                 if (!Directory.Exists(Directory_Path))
                                 {
                                     Directory.CreateDirectory(Directory_Path);

                                     Directory.CreateDirectory(Path1);
                                     //Directory.CreateDirectory(Path2);
                                     //Directory.CreateDirectory(Path3);
                                 }

                                 //Update Alternate Photo
                                 if (Convert.ToString(bad.photo_url) != "")
                                 {
                                     try
                                     {
                                     //string Alt_File_Url = "http:/54.68.125.132:8181/biometrics/public/attachments/" + Convert.ToString(bad.photo_url);
                                     string Alt_File_Url = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + Convert.ToString(bad.photo_url); // API_V_1.2
                                        byte[] Alt_data;
                                     using (WebClient Alt_client = new WebClient())
                                     {
                                         Alt_data = Alt_client.DownloadData(Alt_File_Url);
                                     }
                                    string  household_photo_path = "~/Alternateone/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()) + "/" + Convert.ToString(bad.photo_url);
                                     string alternate_photo_path = Path1 +"/" + Convert.ToString(bad.photo_url);
                                     if (File.Exists(household_photo_path))
                                     {
                                         File.Delete(household_photo_path);
                                     }
                                     File.WriteAllBytes(alternate_photo_path, Alt_data);
                                     int p = Gen.UploadHouseHoldPhoto(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), alternate_photo_path, "FirstAlternatePayeePhotoPath", bad.photo_url);
                                 
                                  }
                                     catch (Exception ex)
                                     {
                                         if (ex.ToString() == "The remote server returned an error: (404) Not Found")
                                         {
                                             Gen.InsertAPIFailure_Status(bad.household_number, "File is not in API URL (Payconnect Team)", Convert.ToString(bad.created_at));


                                             //Return JSON - Session Tabe

                                             AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.alternate_number,
                                                         bad.first_name, bad.last_name, bad.surname, bad.age, bad.id_number, bad.phone_number, bad.gender,
                                                        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "File is not in API URL (Payconnect Team)", System.DateTime.Now.ToString(), "");
                                         }
                                         else
                                         {
                                             Gen.InsertAPIFailure_Status(bad.household_number, ex.ToString(), Convert.ToString(bad.created_at));


                                             //Return JSON - Session Tabe

                                             AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.alternate_number,
                                                         bad.first_name, bad.last_name, bad.surname, bad.age, bad.id_number, bad.phone_number, bad.gender,
                                                        bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", ex.ToString(), System.DateTime.Now.ToString(), "");
                                       
                                         }

                                     
                                         
                                         }
                                 
                                 
                                 }

                              //   check = 1;
                             }

                             AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id, bad.household_number, bad.alternate_number,
                                        bad.first_name, bad.last_name, bad.surname, bad.age, bad.id_number, bad.phone_number, bad.gender,
                                       bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Success", "0", System.DateTime.Now.ToString(), ds_check.Tables[0].Rows[0]["EnrollmentNo"].ToString().Trim());


                            
                        }
                        else
                        {
                            Gen.InsertAPIFailure_Status(bad.household_number, "Household number not in or not ported earlier in MIS for Alternate", Convert.ToString(bad.created_at));


                            //Return JSON - Session Tabe

                            AddDataToTableCeertificate((DataTable)Session["ErrorDataA"], bad.id.ToString(), bad.supervisor_id,  bad.household_number, bad.alternate_number, 
                                        bad.first_name, bad.last_name, bad.surname, bad.age, bad.id_number, bad.phone_number, bad.gender,
                                       bad.photo_url, bad.id_url, bad.created_at, bad.updated_at, "Failed", "Household number not in MIS or not ported in MIS for Alternate", System.DateTime.Now.ToString(), "");



                        }

                      
                    }
                    // success message


                    DataSet dsa = new DataSet();

                    dsa.Tables.Add((DataTable)Session["ErrorDataA"]); 
                    


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
                                mainresult = mainresult + "\"alternate_number\":\"" + dr["alternate_number"].ToString() + "\",";
                               
                                mainresult = mainresult + "\"first_name\":\"" + dr["first_name"].ToString() + "\",";
                                mainresult = mainresult + "\"last_name\":\"" + dr["last_name"].ToString() + "\",";
                                mainresult = mainresult + "\"surname\":\"" + dr["surname"].ToString() + "\",";
                                mainresult = mainresult + "\"age\":\"" + dr["age"].ToString() + "\",";
                               
                                mainresult = mainresult + "\"id_number\":\"" + dr["id_number"].ToString() + "\",";
                                mainresult = mainresult + "\"phone_number\":\"" + dr["phone_number"].ToString() + "\",";
                                mainresult = mainresult + "\"gender\":\"" + dr["gender"].ToString() + "\",";
                               
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
            mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
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
        public string alternate_number { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string surname { get; set; }
        public string age { get; set; }
        public string id_number { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string photo_url { get; set; }
        public string id_url { get; set; }
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

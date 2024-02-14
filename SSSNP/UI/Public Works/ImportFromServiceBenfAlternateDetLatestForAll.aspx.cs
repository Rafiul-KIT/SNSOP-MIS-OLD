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
public partial class ImportFromServiceBenfAlternateDetLatestForAll : System.Web.UI.Page
{
    General Gen = new General();

    DataTable mydt = new DataTable();
    //static DataTable dtMyTable;
    //static DataTable dtMyTableCertificate;
    DataTable mydtp = new DataTable();
    //static DataTable dtMyTablep;
    //static DataTable dtMyTableCertificatep;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //Session.Remove("ErrorDataA");
            //dtMyTableCertificate = createtablecrtificate();
            //Session["ErrorDataA"] = dtMyTableCertificate;
            string result = InsertBenAlternateDetails();
            Response.Write(result);
          
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
        //string URI = "http:/54.68.125.132:8181/snsdp/index.php/api/v1/getAlternates";
        string URI = System.Configuration.ConfigurationManager.AppSettings["GetAlternates"]; // API_V_1.2
        string mainresult = "";
        bool Test = true;
        Uri urlCheck = new Uri(URI);
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

        string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
        httpWReq.PreAuthenticate = true;
        httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());

        string benphotos = "";
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
                    int sas = Gen.RemoveAlternateTotalDump();
                    foreach (Beneficiary_API_Data bad in objdata)
                    {

                        //DataSet ds_check = new DataSet();
                        benphotos = "";
                        //string householdnumber = bad.household_number;
                        if(bad.photo_url==null)
                        {
                            benphotos = "";
                        }
                        else
                        {
                            benphotos = bad.photo_url;
                        }
                        int Alt_update = Gen.InsertAlternate(Convert.ToString(bad.household_number), Convert.ToString(bad.alternate_number),
                                                                                           Convert.ToString(bad.alternate_name),
                                                                                           Convert.ToString(bad.age),
                                                                                           Convert.ToString(bad.id_number),
                                                                                           Convert.ToString(bad.gender),
                                                                                           Convert.ToString(bad.phone_number),
                                                                                           Convert.ToString(benphotos), Convert.ToString(bad.id), Convert.ToString(bad.created_at), Convert.ToString(bad.supervisor_id));

                                 //Update Alternate Photo

                                 //string Directory_Path = Server.MapPath("~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()));
                                 //string Path1 = Server.MapPath("~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()));
                                 ////string Path2 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/FirstAlternate");
                                 ////string Path3 = Server.MapPath("~/Beneficiary/" + Bene_ID+ "/SecondAlternate");
                                 //if (!Directory.Exists(Directory_Path))
                                 //{
                                 //    Directory.CreateDirectory(Directory_Path);

                                 //    Directory.CreateDirectory(Path1);
                                 //    //Directory.CreateDirectory(Path2);
                                 //    //Directory.CreateDirectory(Path3);
                                 //}
                                 //if (Convert.ToString(bad.photo_url) != "")
                                 //{
                                 //    try
                                 //    {
                                 //        string Alt_File_Url = "http:/54.68.125.132:8181/biometrics/public/attachments/" + Convert.ToString(bad.photo_url);
                                 //        byte[] Alt_data;
                                 //        using (WebClient Alt_client = new WebClient())
                                 //        {
                                 //            Alt_data = Alt_client.DownloadData(Alt_File_Url);
                                 //        }
                                 //        string household_photo_path = "~/Alternatetwo/" + Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()) + "/" + Convert.ToString(bad.photo_url);
                                 //        string alternate_photo_path = Path1 + "/" + Convert.ToString(bad.photo_url);
                                 //        if (File.Exists(household_photo_path))
                                 //        {
                                 //            File.Delete(household_photo_path);
                                 //        }
                                 //        File.WriteAllBytes(alternate_photo_path, Alt_data);
                                 //        int p = Gen.UploadHouseHoldPhoto(Convert.ToInt32(ds_check.Tables[0].Rows[0]["intBenTempid"].ToString().Trim()), alternate_photo_path, "SecondAlternatePayeePhotoPath", bad.photo_url);
                                 //    }
                                 //    catch (Exception ex)
                                 //    {
                                 //        if (ex.ToString() == "The remote server returned an error: (404) Not Found")
                                 //        {
                                 //        }
                                 //        else
                                 //        {
                                       
                                 //        }

                                     
                                         
                                
                                     
                                 //    }
                                // }
                            
                    }
                    // success message

    
                        mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"Done\"}";
                  





                   




                  //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('Data Inserted Successfully')", true);
                }
                else
                {
                  //  Gen.InsertAPIFailure_Status("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now));

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

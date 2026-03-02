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
using System.Security.Cryptography;
using SSSNP_SNSOP.Extensions;
public partial class ImportBeneficiaryAll : System.Web.UI.Page
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

            string result = InsertBenDetails();
            Response.Write(result);
          
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
                            Bene_ID = Gen.InsertTotalBenDatathroughAPI(Convert.ToString(bad.id), Convert.ToString(bad.supervisor_id), 
                                Convert.ToString(bad.partner_code), Convert.ToString(bad.household_number),
                                                 Convert.ToString(bad.beneficiary_type) ,
                                                 Convert.ToString(bad.group_number),
                                                 Convert.ToString(bad.member_number),
                                                 Convert.ToString(bad.household_size),
                                                 Convert.ToString(bad.household_name),
                                                 Convert.ToString(bad.age), 
                                                 Convert.ToString(bad.marital_status),
                                                 Convert.ToString(bad.spouse_name),
                                                 Convert.ToString(bad.id_number), Convert.ToString(bad.phone_number),
                                                 Convert.ToString(bad.gender), Convert.ToString(bad.female_dependants),
                                                 Convert.ToString(bad.male_dependants),
                                                 Convert.ToString(bad.zero_five), Convert.ToString(bad.six_eighteen),
                                                 Convert.ToString(bad.nineteen_forty_five), Convert.ToString(bad.forty_six_sixty_five),
                                                 Convert.ToString(bad.sixty_six), 
                                                 Convert.ToString(bad.income_household),
                                                 Convert.ToString(bad.average_household), 
                                                  Convert.ToString(bad.state),
                                                 Convert.ToString(bad.county), Convert.ToString(bad.payam),
                                                 Convert.ToString(bad.boma),
                                                 
                                                 Convert.ToString(bad.latitude),
                                                 Convert.ToString(bad.longitude), Convert.ToString(bad.legal_status),
                                                  Convert.ToString(bad.status),
                                                 
                                                 Convert.ToString(bad.created_at), Convert.ToString(bad.updated_at),
                                                  Convert.ToString(bad.stored_at),
                                                 Convert.ToString(bad.photo_url)
                                                  
                                                 );
                        }

                        catch (Exception ex)
                        {


                        }




                      





                        //  ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "alert('Data Inserted Successfully')", true);


                    }

                    mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"Done\": \"data not found\"}";
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
        public string marital_status { get; set; }
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
        public string stored_at { get; set; }

    }

    public class RootObject
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public List<Beneficiary_API_Data> data { get; set; }
    }
  
}

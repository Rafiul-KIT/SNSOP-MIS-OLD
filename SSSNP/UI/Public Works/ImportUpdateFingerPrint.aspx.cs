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

public partial class ImportUpdateFingerPrint : System.Web.UI.Page
{
    General Gen = new General();

    DataTable mydt = new DataTable();
  
    DataTable mydtp = new DataTable();

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
        string mainresult = "";
        DataSet dsFinger=new DataSet();
        dsFinger=Gen.GetHouseHoldDetailsFingerPrint();
        if(dsFinger.Tables[0].Rows.Count>0)
        {

           int cnt=dsFinger.Tables[0].Rows.Count;
            int f=0;
            for(f=0;f<cnt;f++)
            {
            
            
            
            
        
        //dtMyTableCertificate = createtablecrtificate();
        //Session["ErrorData"] = dtMyTableCertificate;
        //string URI = "http:/54.68.125.132:8181/snsdp/index.php/api/v1/getFingerprints/" + dsFinger.Tables[0].Rows[f][0].ToString();
        string URI = System.Configuration.ConfigurationManager.AppSettings["GetFingerprints"] + dsFinger.Tables[0].Rows[f][0].ToString(); // API_V_1.2


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
                                Bene_ID = Gen.InsertFingerPrint(Convert.ToString(bad.beneficiary_id),
                                                     Convert.ToString(bad.fingerprint_number),  Convert.ToString(bad.fingerprint)
                                                     , Convert.ToString(bad.beneficiary_type)
                                                     );
                            }

                            catch (Exception ex)
                            {

                              Gen.InsertAPIFailure_StatusIPid(bad.beneficiary_id, ex.ToString(), Convert.ToString(bad.created_at), "");


                                //Return JSON - Session Tabe

                            }



                            




                       
                       
                    }





                    mainresult = "{ \"error\":\"false\",\"errorCode\":\"0\",\"data\": \"Done\"}";
                    


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
                

        
                }
        }
        return mainresult;
    }

    public class Beneficiary_API_Data
    {

        public int id { get; set; }
        public string supervisor_id { get; set; }
        public string beneficiary_type { get; set; }
        public string beneficiary_id { get; set; }
        public string fingerprint_number { get; set; }
        public string fingerprint { get; set; }
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

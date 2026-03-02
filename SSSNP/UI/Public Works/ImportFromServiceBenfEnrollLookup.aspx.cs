using SSSNP_SNSOP.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class ImportFromServiceBenfEnrollLookup : System.Web.UI.Page
    {
        General Gen = new General();


        public string DataTableToJsonObj(DataTable dt)
        {
            DataSet ds = new DataSet();
            ds.Merge(dt);
            StringBuilder JsonString = new StringBuilder();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                JsonString.Append("[");
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    JsonString.Append("{");
                    for (int j = 0; j < ds.Tables[0].Columns.Count; j++)
                    {
                        if (j < ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\",");
                        }
                        else if (j == ds.Tables[0].Columns.Count - 1)
                        {
                            JsonString.Append("\"" + ds.Tables[0].Columns[j].ColumnName.ToString() + "\":" + "\"" + ds.Tables[0].Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == ds.Tables[0].Rows.Count - 1)
                    {
                        JsonString.Append("}");
                    }
                    else
                    {
                        JsonString.Append("},");
                    }
                }
                JsonString.Append("]");
                return JsonString.ToString();
            }
            else
            {
                return null;
            }
        }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    if (Request.QueryString["bomacode"] == null) throw new ArgumentNullException("BomaCode in query string is null");
                    else if (string.IsNullOrEmpty(Request.QueryString["bomacode"])) throw new ArgumentNullException("BomaCode in query string is empty");

                    string bomaCode = Request.QueryString["bomacode"];
                    string result = PopulateData(bomaCode);
                    Response.Write(result);

                }
                catch (Exception ex)
                {
                    Gen.InsertAPIFailure_StatusAttendance("", "'Error : " + ex.Message + "'", Convert.ToString(DateTime.Now), "", "");
                    Response.Write("{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \""+ ex.Message +"\"}");
                }
            }
               
                    
        }

        private string PopulateData(string bomaCode)
        {

            try
            {

                DataSet dt = Gen.GetBeneficieryEnrollmentAPI(bomaCode);
                int tttt = dt.Tables[0].Rows.Count;

                string result = DataTableToJsonObj(dt.Tables[0]);

                return result;

                #region TEST

                //            var lst = dt.Tables[0].AsEnumerable()
                //.Select(r => r.Table.Columns.Cast<DataColumn>()
                //        .Select(c => new KeyValuePair<string, object>(c.ColumnName, r[c.Ordinal])
                //       ).ToDictionary(z => z.Key, z => z.Value)
                //).ToList();
                //            //now serialize it
                //            var serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
                //            return serializer.Serialize(lst);



                //List<Beneficiary_API_Data> objdata = new List<Beneficiary_API_Data>() {
                //                new Beneficiary_API_Data () {  id = 10 },
                //                new Beneficiary_API_Data () { id = 11},
                //                new Beneficiary_API_Data() { id = 15}
                //            };

                //        JavaScriptSerializer serializer = new JavaScriptSerializer();
                //        return serializer.Serialize(objdata);

                #endregion

            }
            catch (Exception ex)
            {
                Gen.InsertAPIFailure_StatusAttendance("", "'Error : " + ex.Message + "'", Convert.ToString(DateTime.Now), "", "");
                return "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
            }


        }
        public string InsertBenDetails()
        {
            //dtMyTableCertificate = createtablecrtificate();
            //Session["ErrorData"] = dtMyTableCertificate;
            //string URI = "http:/54.68.125.132:8181/snsdp/index.php/api/v1/getAttendance";
            string URI = System.Configuration.ConfigurationManager.AppSettings["GetBeneficiaryEnrollment"]; // API_V_1.2
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
                        List<Beneficiary_API_Data> objdata = new List<Beneficiary_API_Data>() {
                        new Beneficiary_API_Data () {  id = 10 },
                        new Beneficiary_API_Data () { id = 11},
                        new Beneficiary_API_Data() { id = 15}
                    };
                        objdata = obj.data;


                        // JSON Return Process

                        if (objdata.Count > 0)
                        {
                            mainresult = mainresult + "{";
                            mainresult = "{ \"error\":\"false\",\"errorCode\":" + "\"" + "0" + "\",\"data\":[";
                            foreach (Beneficiary_API_Data b in objdata)
                            {

                                mainresult = mainresult + "{\"id\":\"" + b.id.ToString() + "\"},";



                                //}
                                //mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                            }
                        }
                        else
                        {
                            mainresult = "{ \"error\":\"true\",\"errorCode\":\"1\",\"data\": \"data not found\"}";
                        }



                    }
                    else
                    {
                        //Gen.InsertAPIFailure_StatusAttendance("", "'Error : " + obj.errorCode + "'", Convert.ToString(DateTime.Now), "", "");

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

            }

            return mainresult;
        }



    }
    public class Beneficiary_API_Data
    {

        public int id { get; set; }


    }

    public class RootObject
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public List<Beneficiary_API_Data> data { get; set; }
    }

}
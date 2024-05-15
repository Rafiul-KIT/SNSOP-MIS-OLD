using SSSNP.Entity.Payroll;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using static ImportBeneficiaryAll;

namespace SSSNP.Service
{
    public class WageRequestApiManager
    {
        General Gen = new General();
        public void SendWageRequestByApi(string uid)
        {
            try
            {
                string result = "";
                DB.DB con = new DB.DB();
                con.OpenConnection();
                SqlDataAdapter da;
                DataSet dsn = new DataSet();
                try
                {
                    da = new SqlDataAdapter("getBenfWorkWageDetailsService", con.GetConnection);
                    da.SelectCommand.CommandType = CommandType.StoredProcedure;
                    da.Fill(dsn);

                    con.CloseConnection();
                    DataTable ds = dsn.Tables[0];

                    if (ds.Rows.Count > 0)
                    {
                        result = DataTableToJSONWithJavaScriptSerializer(ds, uid);
                    }
                    else
                    {
                        result = "{ \"BenfWorkWagesDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
                    }
                }
                catch (Exception ex)
                {
                    result = "{ \"BenfWorkWagesDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
                }


                string uri = System.Configuration.ConfigurationManager.AppSettings["PushApprovedPaymentsDevEndpoint"];
                if (System.Configuration.ConfigurationManager.AppSettings["AZUREAD_ENV"].Equals("prod"))
                {
                    uri = System.Configuration.ConfigurationManager.AppSettings["PushApprovedPaymentsProdEndpoint"];
                }
                Uri urlCheck = new Uri(uri);
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

                string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
                httpWReq.PreAuthenticate = true;
                httpWReq.Headers.Add("Authorization", "Bearer " + "apikey.HashSHA512()");

                try
                {
                    var data = Encoding.ASCII.GetBytes(result);
                    httpWReq.Method = "POST";
                    httpWReq.ContentType = "application/x-www-form-urlencoded";
                    httpWReq.ContentLength = data.Length;

                    using (var stream = httpWReq.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }

                    HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
            }
        }

        private string DataTableToJSONWithJavaScriptSerializer(DataTable table, string uid)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                if (row["intWagePaymentReqid"] != null && row["intWagePaymentReqid"].ToString() == uid)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        string key = col.ColumnName;
                        if (key.Equals("ReqFromDate"))
                        {
                            key = "reqFromDate";
                        }
                        else if (key.Equals("ReqToDate"))
                        {
                            key = "reqToDate";
                        }
                        else if (key.Equals("Exchangerateattime"))
                        {
                            key = "exchangerateattime";
                        }
                        else if (key.Equals("WageRateUSD"))
                        {
                            key = "wageRateUSD";
                        }
                        else if (key.Equals("ReqNo"))
                        {
                            key = "reqNo";
                        }
                        else if (key.Equals("EnrollmentNo"))
                        {
                            key = "enrollmentNo";
                        }
                        else if (key.Equals("WorkCode"))
                        {
                            key = "workCode";
                        }
                        else if (key.Equals("TotAttendance"))
                        {
                            key = "totAttendance";
                        }
                        else if (key.Equals("WageRateUSDTotal"))
                        {
                            key = "wageRateUSDTotal";
                        }
                        else if (key.Equals("DateofCreation"))
                        {
                            key = "dateofCreation";
                        }
                        else if (key.Equals("Status"))
                        {
                            key = "status";
                        }
                        else if (key.Equals("PaymentCycle"))
                        {
                            key = "paymentCycle";
                        }
                        else if (key.Equals("WageRateSSPTotal"))
                        {
                            key = "wageRateSSPTotal";
                        }
                        else if (key.Equals("household_number"))
                        {
                            continue;
                        }
                        else if (key.Equals("intBenid"))
                        {
                            DataSet dsChild = new DataSet();
                            dsChild = Gen.GetBenficiaryDetByID(row[col].ToString());

                            if (dsChild.Tables[0].Rows.Count > 0)
                            {
                                HouseholdDetailDto dto = new HouseholdDetailDto();
                                if (dsChild.Tables[0].Rows[0]["Age"].ToString() != "")
                                {
                                    dto.age = Convert.ToInt32(dsChild.Tables[0].Rows[0]["Age"].ToString());
                                }
                                dto.lastName = dsChild.Tables[0].Rows[0]["NameofHousehold"].ToString();
                                dto.phoneNumber = dsChild.Tables[0].Rows[0]["ContactNo"].ToString();

                                childRow.Add("houseHoldDetail", dto);
                            }
                        }

                        childRow.Add(key, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }

            return jsSerializer.Serialize(parentRow);
        }

    }
}
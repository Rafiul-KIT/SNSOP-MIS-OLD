using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Web.Services;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;

public partial class WSgetMastersDet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       


        //string result = getMasterDetails();
        //Response.Write(result);
        string result = "";
        string mainresult = "";
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            da = new SqlDataAdapter("getMasters", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;




            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(ds);
            con.CloseConnection();
            if (ds.Tables[0].Rows.Count > 0)
            {


                result = "{ \"MasterDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":" + DataTableToJSONWithJavaScriptSerializer(ds.Tables[0]) + "}";
            }
            else
            {
                result = "{ \"MasterDetails\":\"failure\",\"status\":\"0\",\"result\": \"Data not found\"}";
            }
        }
        catch
        {
            result = "{ \"MasterDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
        }
        Response.Write(result);

        //string result = getBenfWorkDetails();
        //Response.Write(result);
    }
    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = Int32.MaxValue;
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                childRow.Add(col.ColumnName, row[col]);
            }
            parentRow.Add(childRow);
        }
        return jsSerializer.Serialize(parentRow);
    } 

    public string getMasterDetails()
    {

        string mainresult = "";
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            da = new SqlDataAdapter("getMasters", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

          
            

            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                mainresult = mainresult + "{";
                mainresult = "{ \"MasterDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":[";
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        mainresult = mainresult + "{\"intStateid\":\"" + dr["intStateid"].ToString() + "\",";

                        mainresult = mainresult + "\"StateName\":\"" + dr["StateName"].ToString() + "\",";
                        mainresult = mainresult + "\"intCountieid\":\"" + dr["intCountieid"].ToString() + "\",";
                        mainresult = mainresult + "\"CountieName\":\"" + dr["CountieName"].ToString() + "\",";
                        mainresult = mainresult + "\"intPayamid\":\"" + dr["intPayamid"].ToString() + "\",";
                        mainresult = mainresult + "\"PayamName\":\"" + dr["PayamName"].ToString() + "\",";
                        mainresult = mainresult + "\"intBomasid\":\"" + dr["intBomasid"].ToString() + "\",";
                        
                        mainresult = mainresult + "\"BomaName\":\"" + dr["BomaName"].ToString() + "\"},";
                       

                    }
                    mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                }
            }
            else
            {
                mainresult = "{ \"MasterDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
            }
           

        }
        catch (Exception ex)
        {

        }
        // return result; 
        // result = "[{ \"Userdetails\":\"success\",\"status\":1,\"users\":[{\"intGrievanceid\":"+dr["intGrievanceid"].ToString()+"\"intMandalid\":"+dr["intMandalid"].ToString()+"\intVillageid\":"+dr["intVillageid"]+"\"ApplicantName\":"+dr["ApplicantName"]+"\MobileNumber\":"+dr["MobileNumber"]+"\"AadharNumber\":"+dr["AadharNumber"]+"\"ApplAddress\":"+dr["ApplAddress"+"\"Emailid\":"+dr["Emailid"]+"\"GrievanceDesc\":"+dr["GrievanceDesc"]+"}]}";

        return mainresult;

    }
}

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

public partial class WSPreEnrolledAlternateHouseHold : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
       


        //string result = getBenfDetails();
        //Response.Write(result);
        string result = "";
        string mainresult = "";
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            da = new SqlDataAdapter("getBenAlternatePayeePreEnrolled", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;




            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(ds);
            con.CloseConnection();
            if (ds.Tables[0].Rows.Count > 0)
            {


                result = "{ \"BenfPreEnrolledAlternateDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":" + DataTableToJSONWithJavaScriptSerializer(ds.Tables[0]) + "}";
            }
            else
            {
                result = "{ \"BenfPreEnrolledAlternateDetails\":\"failure\",\"status\":\"0\",\"result\": \"Data not found\"}";
            }
        }
        catch
        {
            result = "{ \"BenfPreEnrolledAlternateDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
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

    public string getBenfDetails()
    {

        string mainresult = "";
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            da = new SqlDataAdapter("getBenAlternatePayeePreEnrolled", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

          
            

            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                mainresult = mainresult + "{";
                mainresult = "{ \"BenfPreEnrolledAlternateDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":[";
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        mainresult = mainresult + "{\"intBenTempid\":\"" + dr["intBenTempid"].ToString() + "\",";

                        mainresult = mainresult + "\"EnrollmentNo\":\"" + dr["EnrollmentNo"].ToString() + "\",";
                        mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                        mainresult = mainresult + "\"member_number\":\"" + dr["member_number"].ToString() + "\",";
                        mainresult = mainresult + "\"Name\":\"" + dr["Name"].ToString() + "\",";
                        mainresult = mainresult + "\"Gender\":\"" + dr["Gender"].ToString() + "\",";
                        mainresult = mainresult + "\"Age\":\"" + dr["Age"].ToString() + "\",";
                        mainresult = mainresult + "\"NationalID\":\"" + dr["NationalID"].ToString() + "\",";
                        mainresult = mainresult + "\"Mobile\":\"" + dr["Mobile"].ToString() + "\",";

                        mainresult = mainresult + "\"PhotoURL\":\"" + dr["PhotoURL"].ToString() + "\"},";
                       

                    }
                    mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                }
            }
            else
            {
                mainresult = "{ \"BenfPreEnrolledAlternateDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
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

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
 
public partial class WSgetBenWorkWagesDetLatestNew : System.Web.UI.Page
{
    static DataTable dtMyTable;
    static DataTable dtMyTableCertificate;
    DataTable mydtp = new DataTable();
    static DataTable dtMyTablep;
    static DataTable dtMyTableCertificatep;
    protected void Page_Load(object sender, EventArgs e)
    {

        Session.Remove("ErrorDataW");
        dtMyTableCertificate = createtablecrtificate();
        Session["ErrorDataW"] = dtMyTableCertificate;
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet dsn = new DataSet();
        try
        {
            da = new SqlDataAdapter("getBenfWorkWageDetailsService", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;




            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(dsn);


            Session["ErrorDataW"] = dsn.Tables[0];
            da.Dispose();
            con.CloseConnection();
            DataTable ds = new DataTable();
            ds = (DataTable)Session["ErrorDataW"];
            string result = DataTableToJSONWithJavaScriptSerializer(ds);
            Response.Write(result);
        }
        catch (Exception ex)
        { }

        
    }

    protected DataTable createtablecrtificate()
    {
        dtMyTable = new DataTable("ErrorDataW");
        dtMyTable.Columns.Add("intWagePaymentReqid", typeof(string));
        dtMyTable.Columns.Add("ReqFromDate", typeof(string));
        dtMyTable.Columns.Add("ReqToDate", typeof(string));
        dtMyTable.Columns.Add("Exchangerateattime", typeof(string));
        dtMyTable.Columns.Add("WageRateUSD", typeof(string));
        dtMyTable.Columns.Add("ReqNo", typeof(string));
        dtMyTable.Columns.Add("intWagesReqBenTransid", typeof(string));
        dtMyTable.Columns.Add("intBenid", typeof(string));
        dtMyTable.Columns.Add("EnrollmentNo", typeof(string));
        dtMyTable.Columns.Add("household_number", typeof(string));
        dtMyTable.Columns.Add("intWorkid", typeof(string));
        dtMyTable.Columns.Add("WorkCode", typeof(string));
        dtMyTable.Columns.Add("TotAttendance", typeof(string));
        dtMyTable.Columns.Add("WageRateUSDTotal", typeof(string));
        dtMyTable.Columns.Add("DateofCreation", typeof(string));      
        return dtMyTable;
    }

    public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
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
    public string getBenfWorkDetails()
    {

        string mainresult = "";
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet dsn = new DataSet();
        try
        {
            da = new SqlDataAdapter("getBenfWorkWageDetailsService", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

          
            

            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(dsn);
           

            Session["ErrorDataW"] = dsn.Tables[0];
            da.Dispose();
            con.CloseConnection();
            DataTable ds = new DataTable();
            ds = (DataTable)Session["ErrorDataW"];

            //JavaScriptSerializer js = new JavaScriptSerializer();
            //Context.Response.Write(js.Serialize(ds));


            if (ds.Rows.Count > 0)
            {
                mainresult = mainresult + "{";
                mainresult = "{ \"BenfWorkWagesDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":[";

                foreach (DataRow dr in ds.Rows)
                {
                    mainresult = mainresult + "{\"intWagePaymentReqid\":\"" + dr["intWagePaymentReqid"].ToString() + "\",";

                    mainresult = mainresult + "\"ReqFromDate\":\"" + dr["ReqFromDate"].ToString() + "\",";
                    mainresult = mainresult + "\"ReqToDate\":\"" + dr["ReqToDate"].ToString() + "\",";
                    mainresult = mainresult + "\"Exchangerateattime\":\"" + dr["Exchangerateattime"].ToString() + "\",";
                    mainresult = mainresult + "\"WageRateUSD\":\"" + dr["WageRateUSD"].ToString() + "\",";
                    mainresult = mainresult + "\"ReqNo\":\"" + dr["ReqNo"].ToString() + "\",";
                    mainresult = mainresult + "\"intWagesReqBenTransid\":\"" + dr["intWagesReqBenTransid"].ToString() + "\",";
                    mainresult = mainresult + "\"intBenid\":\"" + dr["intBenid"].ToString() + "\",";
                    mainresult = mainresult + "\"EnrollmentNo\":\"" + dr["EnrollmentNo"].ToString() + "\",";
                    mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                    mainresult = mainresult + "\"intWorkid\":\"" + dr["intWorkid"].ToString() + "\",";
                    mainresult = mainresult + "\"WorkCode\":\"" + dr["WorkCode"].ToString() + "\",";
                    mainresult = mainresult + "\"TotAttendance\":\"" + dr["TotAttendance"].ToString() + "\",";
                    mainresult = mainresult + "\"WageRateUSDTotal\":\"" + dr["WageRateUSDTotal"].ToString() + "\",";
                    mainresult = mainresult + "\"Status\":\"" + "0" + "\",";
                    mainresult = mainresult + "\"DateofCreation\":\"" + dr["DateofCreation"].ToString() + "\"},";


                }
                mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";

            }
            else
            {
                mainresult = "{ \"BenfWorkWagesDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
            }
           
            
        }
        catch (Exception ex)
        {
            con.CloseConnection();
        }
        // return result; 
        // result = "[{ \"Userdetails\":\"success\",\"status\":1,\"users\":[{\"intGrievanceid\":"+dr["intGrievanceid"].ToString()+"\"intMandalid\":"+dr["intMandalid"].ToString()+"\intVillageid\":"+dr["intVillageid"]+"\"ApplicantName\":"+dr["ApplicantName"]+"\MobileNumber\":"+dr["MobileNumber"]+"\"AadharNumber\":"+dr["AadharNumber"]+"\"ApplAddress\":"+dr["ApplAddress"+"\"Emailid\":"+dr["Emailid"]+"\"GrievanceDesc\":"+dr["GrievanceDesc"]+"}]}";

       
        return mainresult;

        

    }
}

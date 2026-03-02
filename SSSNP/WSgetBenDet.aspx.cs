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

public partial class WSgetBenDet : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string result = "";
        string mainresult = "";
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        try
        {
            da = new SqlDataAdapter("getBenfDetailsService", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;




            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(ds);
            con.CloseConnection();
            if (ds.Tables[0].Rows.Count > 0)
            {


               result  = "{ \"BenfDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":"+ DataTableToJSONWithJavaScriptSerializer(ds.Tables[0])+"}";
            }
            else
            {
                result = "{ \"BenfDetails\":\"failure\",\"status\":\"0\",\"result\": \"Data not found\"}";
            }
        }
        catch
        {
                result ="{ \"BenfDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
        }
        Response.Write(result);
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
            da = new SqlDataAdapter("getBenfDetailsService", con.GetConnection);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;

          
            

            // ApplAadhar
            // ApplicantName
            // ApplMobile



            da.Fill(ds);

            if (ds.Tables[0].Rows.Count > 0)
            {
                mainresult = mainresult + "{";
                mainresult = "{ \"BenfDetails\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":[";
                foreach (DataTable table in ds.Tables)
                {
                    foreach (DataRow dr in table.Rows)
                    {
                        mainresult = mainresult + "{\"intBenid\":\"" + dr["intBenid"].ToString() + "\",";

                        mainresult = mainresult + "\"IPName\":\"" + dr["IPName"].ToString() + "\",";
                        mainresult = mainresult + "\"IPCode\":\"" + dr["IPCode"].ToString() + "\",";
                        mainresult = mainresult + "\"EnrollmentNo\":\"" + dr["EnrollmentNo"].ToString() + "\",";
                        mainresult = mainresult + "\"GroupId\":\"" + dr["GroupId"].ToString() + "\",";
                        mainresult = mainresult + "\"BeneficiaryType\":\"" + dr["BeneficiaryType"].ToString() + "\",";
                        mainresult = mainresult + "\"NameofHousehold\":\"" + dr["NameofHousehold"].ToString() + "\",";
                        mainresult = mainresult + "\"FamilySize\":\"" + dr["FamilySize"].ToString() + "\",";
                        mainresult = mainresult + "\"Gender\":\"" + dr["Gender"].ToString() + "\",";
                        mainresult = mainresult + "\"Age\":\"" + dr["Age"].ToString() + "\",";
                        mainresult = mainresult + "\"NameofSpouse\":\"" + dr["NameofSpouse"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberMaleAdults\":\"" + dr["NumberMaleAdults"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberFemaleAdults\":\"" + dr["NumberFemaleAdults"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberAdultsAgeAbove65\":\"" + dr["NumberAdultsAgeAbove65"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberAdultsAgeBetween45_64\":\"" + dr["NumberAdultsAgeBetween45_64"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberAdultsAgeBetween18_30\":\"" + dr["NumberAdultsAgeBetween18_30"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberAdultsAgeBetween30_45\":\"" + dr["NumberAdultsAgeBetween30_45"].ToString() + "\",";
                        mainresult = mainresult + "\"NoofDependents\":\"" + dr["NoofDependents"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberFemaleChildren\":\"" + dr["NumberFemaleChildren"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberMaleChildren\":\"" + dr["NumberMaleChildren"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberChildrenAgeAbove5years\":\"" + dr["NumberChildrenAgeAbove5years"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberChildrenAgeAbove12years\":\"" + dr["NumberChildrenAgeAbove12years"].ToString() + "\",";
                        mainresult = mainresult + "\"NumberChildrenAgeAbove18years\":\"" + dr["NumberChildrenAgeAbove18years"].ToString() + "\",";
                        mainresult = mainresult + "\"FamilyIncome\":\"" + dr["FamilyIncome"].ToString() + "\",";
                        mainresult = mainresult + "\"SOurceofIncome\":\"" + dr["SOurceofIncome"].ToString() + "\",";
                        mainresult = mainresult + "\"NationalID\":\"" + dr["NationalID"].ToString() + "\",";
                        mainresult = mainresult + "\"intStateid\":\"" + dr["intStateid"].ToString() + "\",";
                        mainresult = mainresult + "\"StateName\":\"" + dr["StateName"].ToString() + "\",";
                        mainresult = mainresult + "\"intCountieid\":\"" + dr["intCountieid"].ToString() + "\",";
                        mainresult = mainresult + "\"CountieName\":\"" + dr["CountieName"].ToString() + "\",";
                        mainresult = mainresult + "\"intPayamid\":\"" + dr["intPayamid"].ToString() + "\",";
                        mainresult = mainresult + "\"PayamName\":\"" + dr["PayamName"].ToString() + "\",";
                        mainresult = mainresult + "\"intBomasid\":\"" + dr["intBomasid"].ToString() + "\",";
                        mainresult = mainresult + "\"BomaName\":\"" + dr["BomaName"].ToString() + "\",";
                        mainresult = mainresult + "\"EthnicityTribeRgin\":\"" + dr["EthnicityTribeRgin"].ToString() + "\",";
                        mainresult = mainresult + "\"Address\":\"" + dr["Address"].ToString() + "\",";
                        mainresult = mainresult + "\"ContactNo\":\"" + dr["ContactNo"].ToString() + "\",";
                        mainresult = mainresult + "\"Email\":\"" + dr["Email"].ToString() + "\",";
                        mainresult = mainresult + "\"SecondLineHome\":\"" + dr["SecondLineHome"].ToString() + "\",";
                        mainresult = mainresult + "\"LegalStatus\":\"" + dr["LegalStatus"].ToString() + "\",";
                        mainresult = mainresult + "\"BiometricNo\":\"" + dr["BiometricNo"].ToString() + "\",";
                        mainresult = mainresult + "\"NameofBank\":\"" + dr["NameofBank"].ToString() + "\",";
                        mainresult = mainresult + "\"AccountNo\":\"" + dr["AccountNo"].ToString() + "\",";

                        mainresult = mainresult + "\"DateofCreation\":\"" + dr["DateofCreation"].ToString() + "\",";
                        mainresult = mainresult + "\"household_number\":\"" + dr["household_number"].ToString() + "\",";
                        mainresult = mainresult + "\"supervisor_id\":\"" + dr["supervisor_id"].ToString() + "\",";
                        mainresult = mainresult + "\"member_number\":\"" + dr["member_number"].ToString() + "\",";
                        mainresult = mainresult + "\"female_dependants\":\"" + dr["female_dependants"].ToString() + "\",";
                        mainresult = mainresult + "\"male_dependants\":\"" + dr["male_dependants"].ToString() + "\",";
                        mainresult = mainresult + "\"latitude\":\"" + dr["latitude"].ToString() + "\",";
                        mainresult = mainresult + "\"longitude\":\"" + dr["longitude"].ToString() + "\",";
                        mainresult = mainresult + "\"id_url\":\"" + dr["id_url"].ToString() + "\",";
                        mainresult = mainresult + "\"TransferDate\":\"" + dr["TransferDate"].ToString() + "\",";
                        mainresult = mainresult + "\"TransferUpdateat\":\"" + dr["TransferUpdateat"].ToString() + "\",";
                        mainresult = mainresult + "\"Photo\":\"" + dr["Photo"].ToString() + "\"},";
                       

                    }
                    mainresult = mainresult.Substring(0, mainresult.Length - 1) + "]}";
                }
            }
            else
            {
                mainresult = "{ \"BenfDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
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

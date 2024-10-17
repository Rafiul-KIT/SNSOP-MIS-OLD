using SSSNP_SNSOP.Core.DbHandler;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

public class LookupManagerService
{
    private readonly IDbOperation _dbOperation;
    public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
    public LookupManagerService(string connectionString)
    {
        _dbOperation = new SqlDbOperation(connectionString);
    }

    public DataTable GetStateList()
    {
        return _dbOperation.ExecuteDataTable("SELECT intStateid, StateName FROM tm_States", CommandType.Text);
    }

    public DataTable GetCountyListByStateId(int stateId)
    {
        SqlParameter param = new SqlParameter("@stateId", stateId);
        return _dbOperation.ExecuteDataTable("SELECT intCountieid, CountieName FROM tm_Counties WHERE intStateid = @stateId", CommandType.Text, param);
    }

    public DataTable GetPayamListByCountyId(int countyId)
    {
        SqlParameter param = new SqlParameter("@countyId", countyId);
        return _dbOperation.ExecuteDataTable("SELECT intPayamid, PayamName FROM tm_Payams WHERE intCountieid = @countyId", CommandType.Text, param);
    }

    public DataTable GetBomaListByPayamId(int payamId)
    {
        SqlParameter param = new SqlParameter("@payamId", payamId);
        return _dbOperation.ExecuteDataTable("SELECT intBomasid, BomaName FROM tm_Bomas WHERE intPayamid = @payamId", CommandType.Text, param);
    }

    public DataTable GetGroupIds()
    {
        DataTable groupIdsTable = new DataTable();
        try
        {
            string query = "SELECT Id, GroupName FROM tbl_com2_Household_Group_View";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(groupIdsTable);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Error fetching Group IDs: " + ex.Message);
            throw;
        }

        return groupIdsTable;
    }

    internal DataTable GetHouseholdNumbers(string state, string county, string payam, string boma)
    {
        throw new NotImplementedException();
    }
}

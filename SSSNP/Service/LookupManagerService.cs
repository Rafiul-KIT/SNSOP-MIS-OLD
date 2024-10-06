using SSSNP_SNSOP.Core.DbHandler;
using System;
using System.Data;
using System.Data.SqlClient;

public class LookupManagerService
{
    private readonly IDbOperation _dbOperation;

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

    internal DataTable GetHouseholdNumbers(string state, string county, string payam, string boma)
    {
        throw new NotImplementedException();
    }
}

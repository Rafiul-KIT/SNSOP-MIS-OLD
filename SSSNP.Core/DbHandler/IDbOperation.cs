using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.DbHandler
{
    public interface IDbOperation
    {
        bool InsertData(string tableName, Dictionary<string, object> data);
        bool UpdateData(string tableName, Dictionary<string, object> data, string wherePart);
        DataTable ExecuteDataTable(string v, CommandType text);
        DataTable ExecuteDataTable(string v, CommandType text, System.Data.SqlClient.SqlParameter param);
        DataTable GetDataTable(string sql);
        DataSet ExecuteCmdForSp(List<CmdParamModel> cmdParams, string spName);
        DataTable ExecuteDataTable(string query, CommandType commandType, List<SqlParameter> parameters);

    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.DbHandler
{
    public interface IDbOperation
    {
        bool InsertData(string tableName, Dictionary<string, object> data);
        bool UpdateData(string tableName, Dictionary<string, object> data, string wherePart);
        DataTable GetDataTable(string sql);
        DataSet ExecuteCmdForSp(List<CmdParamModel> cmdParams, string spName);
    }
}

using SSSNP_SNSOP.Core.DbHandler;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SSSNP.Service
{
    public class IndicatorResultService
    {
        private readonly IDbOperation _dbOperation;
        public string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        public IndicatorResultService(string connectionString)
        {
            _dbOperation = new SqlDbOperation(connectionString);
        }

        public DataTable GetData(IndicatorResultQuery request)
        {
            //return _dbOperation.ExecuteDataTable("SELECT intStateid, StateName FROM tm_States", CommandType.Text);
            return null;
        }

        public DataTable GetBeneficiariesOfSocialSafetyNetPrograms(IndicatorResultQuery request)
        {
            try
            {
                string sql = "select 'Total' as 'IndicatorKey', count(*) IndicatorValue " +
                    " from tbl_BeneficiaryDet where 1=1";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                //int count = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetBeneficiariesReceivingCash(IndicatorResultQuery request)
        {
            try
            {
                string sql = "select 'Total' as 'IndicatorKey', count(*) IndicatorValue " +
                    " from tbl_BeneficiaryDet b" +
                    " inner join tr_BenWagesPayment bwp on b.intBenid = bwp.intBenid" +
                    " inner join td_WageReqBenDet wrbd on b.intBenid = wrbd.intBenid and wrbd.intWorkid != 999" +
                    " where 1=1";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                //int count = int.Parse(dt.Rows[0].ItemArray[1].ToString());
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        private string modify(IndicatorResultQuery request, string sql, List<SqlParameter> list)
        {
            if (request.stateId != null)
            {
                sql += " AND intStateId = @stateId";
                SqlParameter param = new SqlParameter("@stateId", request.stateId);
                list.Add(param);
            }
            if (request.countyId != null)
            {
                sql += " AND intCountieid = @countyId";
                SqlParameter param = new SqlParameter("@countyId", request.countyId);
                list.Add(param);
            }
            if (request.payamId != null)
            {
                sql += " AND intPayamid = @payamId";
                SqlParameter param = new SqlParameter("@payamId", request.payamId);
                list.Add(param);
            }
            if (request.bomaId != null)
            {
                sql += " AND intBomasid = @bomaId";
                SqlParameter param = new SqlParameter("@bomaId", request.bomaId);
                list.Add(param);
            }
            if (!string.IsNullOrEmpty(request.legalStatus))
            {
                sql += " AND LegalStatus = @legalStatus";
                SqlParameter param = new SqlParameter("@legalStatus", request.legalStatus);
                list.Add(param);
            }
            if (!string.IsNullOrEmpty(request.supportType))
            {
                sql += " AND SelectionCriteria = @supportType";
                SqlParameter param = new SqlParameter("@supportType", request.supportType);
                list.Add(param);
            }
            if (!string.IsNullOrEmpty(request.sex))
            {
                sql += " AND Sex = @sex";
                SqlParameter param = new SqlParameter("@sex", request.sex);
                list.Add(param);
            }

            return sql;
        }
    }
}
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

        public DataTable GetBeneficiariesOfSocialSafetyNetPrograms(IndicatorResultQuery request)
        {
            try
            {
                string sql = "with cte as (select intBenid from tr_BenWagesPayment group by intBenid) " +
                    " select 'Total' as 'IndicatorKey', count(*) IndicatorValue " +
                    " from tbl_BeneficiaryDet b" +
                    " inner join cte on b.intBenid = cte.intBenid" +
                    " where 1=1";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetBeneficiariesReceivingCash(IndicatorResultQuery request, bool lipw)
        {
            try
            {
                string sql = "with cte as (select intBenid from tr_BenWagesPayment group by intBenid)" +
                    " select 'Total' as 'IndicatorKey', count(*) IndicatorValue " +
                    " from tbl_BeneficiaryDet b" +
                    " inner join cte on b.intBenid = cte.intBenid" +
                    " inner join td_WageReqBenDet wrbd on b.intBenid = wrbd.intBenid" +
                    ((lipw == false) ? " and wrbd.intWorkid = 999" : " and wrbd.intWorkid != 999") +
                    " where 1=1";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetBeneficiariesReceivingEconomicOpportunities(IndicatorResultQuery request)
        {
            try
            {
                string sql = "with cte as (select intBenid from tr_BenWagesPayment group by intBenid)" +
                    " select 'Total' as 'IndicatorKey', count(*) IndicatorValue " +
                    " from tbl_BeneficiaryDet b" +
                    " inner join cte on b.intBenid = cte.intBenid" +
                    " inner join tbl_com2_Household_Enrollment_Data hed on b.household_number = hed.HouseholdNumber" +
                    " where 1=1";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetBeneficiariesParticipatingInAllSessions(IndicatorResultQuery request)
        {
            try
            {
                string sql = "with cte as (select intBenid from tr_BenWagesPayment group by intBenid)," +
                    " cteat as (select intBenid from tr_BenAttendanceDet" +
                    " where cashP_WASH = 1 and cashP_ECD = 1 and cashP_FLI = 1" +
                    " group by intBenid)" +
                    " select 'Total' as 'IndicatorKey', count(*) IndicatorValue " +
                    " from tbl_BeneficiaryDet b" +
                    " inner join cte on b.intBenid = cte.intBenid" +
                    " inner join cteat on b.intBenid = cteat.intBenid" +
                    " where 1=1";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetClimateSmartWorkPercentage(IndicatorResultQuery request)
        {
            try
            {
                string sql = "select 'Total' as 'IndicatorKey'," +
                    " cast(round(count(distinct case when twa.climate_smart = 1 then tWM.intWorkid else 0 end)*100.0 / " +
                    " count(distinct tWM.intWorkid),3) as double precision) IndicatorValue " +
                    " from tbl_WorkMaster tWM " +
                    " left join tr_ProposedWorkAct tPWA on tWM.intWorkid = tPWA.intWorkid " +
                    " left join tm_WorkActivities tWA on tPWA.intWorkActivity = tWA.intWorkActivity " +
                    " where 1=1 and tWM.intWorkid not in (888, 999)";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetNumberOfLIPWDays(IndicatorResultQuery request)
        {
            try
            {
                string sql = "select 'Total' as 'IndicatorKey'," +
                    " count(distinct AttendanceDate) IndicatorValue from tr_BenAttendanceDet t" +
                    " inner join tbl_BeneficiaryDet b on t.intBenid=b.intBenid";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetGrievanceResolvedPercentage(IndicatorResultQuery request)
        {
            try
            {
                string sql = "select 'Total' as 'IndicatorKey'," +
                    " cast(round(sum(IIF(Status = 5, 1, 0))*100.0/count(*),2) as double precision) IndicatorValue" +
                    ((request.grievanceCategory != null && request.grievanceCategory.Equals("N")) ?
                    " from NonGBVMain where 1=1" : " from GBVMain where 1=1");
                List<SqlParameter> list = new List<SqlParameter>();

                sql = GBVmodify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
                return dt;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public DataTable GetCashTransferMadeOnTimePercentage(IndicatorResultQuery request)
        {
            try
            {
                string sql = "with cte as (select intBenid, max(AttendanceDate) attDate " +
                    " from tr_BenAttendanceDet group by intBenid)," +
                    " cte2 as (select intBenid, min(PaidDate)paidDate " +
                    " from tr_BenWagesPayment where intWorkid != 999 group by intBenid) " +
                    " select 'Total' as 'IndicatorKey'," +
                    " cast(round(sum(case when datediff(day, cte.attDate, cte2.paidDate) <= 20 then 1 else 0 end)" +
                    "*100.0/count(*),2) as double precision) IndicatorValue" +
                    " from cte " +
                    " inner join cte2 on cte.intBenid = cte2.intBenid" +
                    " inner join tbl_BeneficiaryDet b on cte.intBenid = b.intBenid";
                List<SqlParameter> list = new List<SqlParameter>();

                sql = modify(request, sql, list);

                DataTable dt = _dbOperation.ExecuteDataTable(sql, CommandType.Text, list);
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
                if (!request.legalStatus.Contains(","))
                {
                    sql += " AND b.LegalStatus = @legalStatus";
                    SqlParameter param = new SqlParameter("@legalStatus", request.legalStatus);
                    list.Add(param);
                }
                else
                {
                    sql += " AND b.LegalStatus in " + request.legalStatus;
                }
            }
            if (!string.IsNullOrEmpty(request.supportType))
            {
                sql += " AND b.SelectionCriteria = @supportType";
                SqlParameter param = new SqlParameter("@supportType", request.supportType);
                list.Add(param);
            }
            if (!string.IsNullOrEmpty(request.sex))
            {
                sql += " AND b.Sex = @sex";
                SqlParameter param = new SqlParameter("@sex", request.sex);
                list.Add(param);
            }
            if (!string.IsNullOrEmpty(request.ageRange))
            {
                String[] ages = request.ageRange.Split('-');
                if (ages != null && ages.Length == 2)
                {
                    sql += " AND b.age between " + int.Parse(ages[0]) + " and " + int.Parse(ages[1]);
                }
            }

            return sql;
        }

        private string GBVmodify(IndicatorResultQuery request, string sql, List<SqlParameter> list)
        {
            if (request.stateId != null)
            {
                sql += " AND StateID = @stateId";
                list.Add(new SqlParameter("@stateId", request.stateId));
            }

            if (request.countyId != null)
            {
                sql += " AND CountyID = @countyId";
                list.Add(new SqlParameter("@countyId", request.countyId));
            }

            if (request.payamId != null)
            {
                sql += " AND PayamID = @payamId";
                list.Add(new SqlParameter("@payamId", request.payamId));
            }

            if (request.bomaId != null)
            {
                sql += " AND BomaID = @bomaId";
                list.Add(new SqlParameter("@bomaId", request.bomaId));
            }

            return sql;
        }

    }
}
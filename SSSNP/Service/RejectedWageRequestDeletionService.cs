using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;
using SSSNP_SNSOP.Core.DbHandler;

namespace SSSNP.Service
{
    public class RejectedWageRequestDeletionService
    {
        private readonly IDbOperation _db;

        private readonly string _connStr;

        public RejectedWageRequestDeletionService(IDbOperation dbOperation, string connectionString)
        {
            _db = dbOperation;
            _connStr = connectionString;
        }


        public void DeleteRejectedWageRequest(int intWagePaymentReqid, string deletionCause, string deletedBy)
        {
            var queries = new List<string>
                {
                    @"INSERT INTO td_WageReqBenDetAudit
                        (intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance,
                         WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt,
                         Modified_by, Modified_dt, ExchangeRateOLD, ExchangeRateNew, CheckA, ReInitiate,
                         AmountInSSP, cashP_FLI, cashP_ECD, cashP_WASh, audit_cause)
                    SELECT intWagesReqBenTransid, intWagePaymentReqid, intWorkid, intBenid, TotAttendance,
                           WageRateUSD, WageRateUSDTotal, traineealternate, Status, Created_by, Created_dt,
                           Modified_by, Modified_dt, ExchangeRateOLD, ExchangeRateNew, CheckA, ReInitiate,
                           AmountInSSP, cashP_FLI, cashP_ECD, cashP_WASh, 2
                    FROM td_WageReqBenDet
                    WHERE intWagePaymentReqid = @intWagePaymentReqid;",

                    @"INSERT INTO td_WageReqTransAudit
                      (intWagesReqTransid, intWagePaymentReqid, intWorkid, intFromid, intToid, Status,
                       TransDate, Remarks, Created_by, Created_dt, Modified_by, Modified_dt)
                      SELECT * FROM td_WageReqTrans WHERE intWagePaymentReqid = @intWagePaymentReqid",

                    @"INSERT INTO td_WorkWageRequestAudit
                      (deleted_at, intWagePaymentReqid, intWorkid, WageRateUSD, ReqFromDate, ReqToDate,
                       ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid,
                       Exchangerateattime, ReqNo, Status, Remarks, Created_by, Created_dt, Modified_by,
                       Modified_dt, ExchangeRateOLD, CApproveDate, SApproveDate, MApproveDate,
                       PaymentCycle, AmountInSSP, Component, deleted_by, deletion_cause)
                      SELECT GETDATE(), intWagePaymentReqid, intWorkid, WageRateUSD, ReqFromDate, ReqToDate,
                       ReqYear, ReqMonth, intPayingAgentid, intCoOrdinatorid, intSpecialistid, intManagerid,
                       Exchangerateattime, ReqNo, Status, Remarks, Created_by, Created_dt, Modified_by,
                       Modified_dt, ExchangeRateOLD, CApproveDate, SApproveDate, MApproveDate,
                       PaymentCycle, AmountInSSP, Component, @intUserid, @deletion_cause
                      FROM td_WorkWageRequest WHERE intWagePaymentReqid = @intWagePaymentReqid",

                    @"INSERT INTO tr_BenAttendanceDetAudit
                        (intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status,
                         Created_by, Created_dt, Modified_by, Modified_dt, AttendanceYear, PortYear,
                         PortPhase, PortedSheetName, PaymentFlag, WageApplyFlag, intWageReqid,
                         cashP_FLI, cashP_ECD, cashP_WASH, audit_cause)
                    SELECT intAttendanceid, intWorkActivityId, intBenid, AttendanceDate, Status,
                           Created_by, Created_dt, Modified_by, Modified_dt, AttendanceYear, PortYear,
                           PortPhase, PortedSheetName, PaymentFlag, WageApplyFlag, intWageReqid,
                           cashP_FLI, cashP_ECD, cashP_WASH, 2
                    FROM tr_BenAttendanceDet
                    WHERE intWageReqid = @intWagePaymentReqid;",

                    @"DELETE FROM td_WageReqTrans WHERE intWagePaymentReqid = @intWagePaymentReqid",
                    @"DELETE FROM td_WageReqBenDet WHERE intWagePaymentReqid = @intWagePaymentReqid",
                    @"UPDATE tr_BenAttendanceDet SET WageApplyFlag = NULL, intWageReqid = NULL
                      WHERE intWageReqid = @intWagePaymentReqid",
                    @"DELETE FROM td_WorkWageRequest WHERE intWagePaymentReqid = @intWagePaymentReqid"
                };

            using (var connection = new SqlConnection(_connStr))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (var rawQuery in queries)
                        {
                            string currentQuery = rawQuery;

                            List<SqlParameter> paramList;

                            if (currentQuery.Contains("@intWagePaymentReqid") && currentQuery.Contains("@intUserid") && currentQuery.Contains("@deletion_cause"))
                            {
                                paramList = new List<SqlParameter>
                                {
                                    new SqlParameter("@intWagePaymentReqid", intWagePaymentReqid),
                                    new SqlParameter("@intUserid", deletedBy),
                                    new SqlParameter("@deletion_cause", deletionCause)
                                };
                            }
                            else if (currentQuery.Contains("@intWagePaymentReqid"))
                            {
                                paramList = new List<SqlParameter>
                                {
                                    new SqlParameter("@intWagePaymentReqid", intWagePaymentReqid)
                                };
                            }
                            else
                            {
                                paramList = new List<SqlParameter>();
                            }

                            try
                            {
                                int affected = _db.ExecuteNonQuery(currentQuery, CommandType.Text, paramList, connection, transaction);

                                if ((currentQuery.StartsWith("DELETE", StringComparison.OrdinalIgnoreCase) ||
                                     currentQuery.StartsWith("UPDATE", StringComparison.OrdinalIgnoreCase)) && affected == 0)
                                {
                                    throw new InvalidOperationException("Query executed but affected 0 rows:\n" + currentQuery);
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception("Error executing query:\n" + currentQuery, ex);
                            }
                        }


                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        try { transaction.Rollback(); }
                        catch (Exception rollbackEx)
                        {
                            throw new AggregateException("Rollback failed", ex, rollbackEx);
                        }

                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

    }
}

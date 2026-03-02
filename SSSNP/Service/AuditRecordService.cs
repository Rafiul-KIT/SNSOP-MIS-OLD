using SSSNP_SNSOP.Core.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace SSSNP.Service
{
    public class AuditRecordService : IAuditRecordService
    {
        private readonly IAuditRecordRepository _auditRecordRepository;

        public AuditRecordService(IAuditRecordRepository auditRecordRepository)
        {
            _auditRecordRepository = auditRecordRepository;
        }

        public DataTable GetHouseholdEnrollmentAudit(DateTime? fromDate, DateTime? toDate)
        {
            return _auditRecordRepository.GetHouseholdEnrollmentAudit(fromDate, toDate);
        }

        public DataTable GetTrainingAttendanceAudit(DateTime? fromDate, DateTime? toDate)
        {
            return _auditRecordRepository.GetTrainingAttendanceAudit(fromDate, toDate);
        }

        public DataTable GetLivelihoodGrantDisbursementAudit(DateTime? fromDate, DateTime? toDate)
        {
            return _auditRecordRepository.GetLivelihoodGrantDisbursementAudit(fromDate, toDate);
        }

        public DataTable GetFinancialServicesLinkageAudit(DateTime? fromDate, DateTime? toDate)
        {
            return _auditRecordRepository.GetFinancialServicesLinkageAudit(fromDate, toDate);
        }

        public DataTable GetBusinessPlanDevelopmentAudit(DateTime? fromDate, DateTime? toDate)
        {
            return _auditRecordRepository.GetBusinessPlanDevelopmentAudit(fromDate, toDate);
        }
    }


}
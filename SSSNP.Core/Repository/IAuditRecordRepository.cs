using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Repository
{
    public interface IAuditRecordRepository
    {
        DataTable GetHouseholdEnrollmentAudit(DateTime? fromDate, DateTime? toDate);
        DataTable GetTrainingAttendanceAudit(DateTime? fromDate, DateTime? toDate);
        DataTable GetLivelihoodGrantDisbursementAudit(DateTime? fromDate, DateTime? toDate);
        DataTable GetFinancialServicesLinkageAudit(DateTime? fromDate, DateTime? toDate);
        DataTable GetBusinessPlanDevelopmentAudit(DateTime? fromDate, DateTime? toDate);
    }

}

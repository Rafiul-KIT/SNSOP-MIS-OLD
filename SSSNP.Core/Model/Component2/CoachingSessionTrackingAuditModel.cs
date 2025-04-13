using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class CoachingSessionTrackingAuditModel : GenericAuditModel
    {
        public string HouseholdNumber { get; set; }
        public int CoachingDetailId { get; set; }
        public DateTime? ClassAttendanceDate { get; set; }
    }
}

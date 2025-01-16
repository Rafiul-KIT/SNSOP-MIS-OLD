using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class BeneficiaryEnrollmentAuditModel : GenericAuditModel
    {
        public string HouseholdNumber { get; set; }
        public bool? InterestedInParticipating { get; set; }
        public bool? SurvivesTargeting { get; set; }
        public bool? TreatmentArm { get; set; }
        public bool? HHEnrollmentStatus { get; set; }
        public int? GroupID { get; set; }
        public int? EnrolledMemberID { get; set; }
        public string ReasonForWhyNotEnrolled { get; set; }
    }
}

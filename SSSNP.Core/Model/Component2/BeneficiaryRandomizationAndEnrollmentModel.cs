using System;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class BeneficiaryRandomizationAndEnrollmentModel
    {
        public string HouseholdNumber { get; set; }
        public bool? InterestedInParticipating { get; set; }
        public bool? SurvivesTargeting { get; set; }
        public bool? TreatmentArm { get; set; }
        public bool? HHEnrollmentStatus { get; set; }
        public int? GroupID { get; set; }
        public int? EnrolledMemberID { get; set; }

        // Nested class instances as properties
        public DenormalizedDataModel DenormalizedData { get; set; }
        public CommonModel CommonData { get; set; }

        // Constructor to initialize nested class instances
        public BeneficiaryRandomizationAndEnrollmentModel()
        {
            DenormalizedData = new DenormalizedDataModel();
            CommonData = new CommonModel();
        }
    }
}

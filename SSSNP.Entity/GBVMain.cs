using SSSNP_SNSOP.Entity.MappingProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class GBVMain
    {
        [AttributeMappingProperty(ColumnName = "S#NO", PropertyName = "GBVMainID", IsMandatory = true)]
        public int GBVMainID { get; set; }
        public DateTime Date { get; set; }
        public string SerialNumber { get; set; }

        [AttributeMappingProperty(ColumnName = "State", PropertyName = "StateID", ApplyLookupMapping = true, LookupMappingName = "LK_State_To_StateID", IsMandatory = true)]
        public int StateID { get; set; }
        [AttributeMappingProperty(ColumnName = "County", PropertyName = "CountyID", ApplyLookupMapping = true, LookupMappingName = "LK_Countie_To_CountieID", IsMandatory = true)]
        public int CountyID { get; set; }
        [AttributeMappingProperty(ColumnName = "Payam", PropertyName = "PayamID", ApplyLookupMapping = true, LookupMappingName = "LK_PayamName_To_PayamID", IsMandatory = true)]
        public int PayamID { get; set; }
        [AttributeMappingProperty(ColumnName = "Boma", PropertyName = "BomaID", ApplyLookupMapping = true, LookupMappingName = "LK_BomaName_To_BomaID", IsMandatory = true)]
        public int BomaID { get; set; }
        [AttributeMappingProperty(ColumnName = "Status", PropertyName = "Status", ApplyLookupMapping = true, LookupMappingName = "LK_StatusIdToStatusName", IsMandatory = true)]
        public int Status { get; set; }
        [AttributeMappingProperty(ColumnName = "Date Of Incident", PropertyName = "DateOfIncident", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping", IsMandatory = true)]
        public DateTime DateOfIncident { get; set; } // added 
        [AttributeMappingProperty(ColumnName = "Focal Point Name", PropertyName = "ContactFocalPointPersonName", IsMandatory = true)]
        public string ContactFocalPointPersonName { get; set; }
        public bool? ConsentSigned { get; set; } // modified /
        [AttributeMappingProperty(ColumnName = "Incident Reported By", PropertyName = "IncidentReportedBy", IsMandatory = true)]
        public string IncidentReportedBy { get; set; }
        [AttributeMappingProperty(ColumnName = "Incident Reporter Other", PropertyName = "IncidentReportedByOther")]
        public string IncidentReportedByOther { get; set; } // added /
        [AttributeMappingProperty(ColumnName = "Contact Number", PropertyName = "ContactNumber", IsMandatory = true)]
        public string ContactNumber { get; set; }
        [AttributeMappingProperty(ColumnName = "Gender", PropertyName = "Gender", IsMandatory = true)]
        public string Gender { get; set; }
        [AttributeMappingProperty(ColumnName = "Age", PropertyName = "AgeOfSurvivor", IsMandatory = true)]
        public string AgeOfSurvivor { get; set; }
        [AttributeMappingProperty(ColumnName = "SNSOP Beneficiary", PropertyName = "SSSNPBeneficiary", IsMandatory = true)]
        public string SSSNPBeneficiary { get; set; }
        [AttributeMappingProperty(ColumnName = "Suspect Associated", PropertyName = "SuspectAssociatedWithProject", ApplyLookupMapping = true, LookupMappingName = "LK_TrueFalseMapping")]
        public bool? SuspectAssociatedWithProject { get; set; }
        //[AttributeMappingProperty(ColumnName = "Suspect Associated Details", PropertyName = "SuspectAssosiatedWithProjectNotes")]
        public string SuspectAssosiatedWithProjectNotes { get; set; } // added /
        [AttributeMappingProperty(ColumnName = "Suspect Known", PropertyName = "SuspectKnown", ApplyLookupMapping = true, LookupMappingName = "LK_TrueFalseMapping")]
        public bool? SuspectKnown { get; set; }
        [AttributeMappingProperty(ColumnName = "Suspect's Details", PropertyName = "SuspectKnownDetail")]
        public string SuspectKnownDetail { get; set; } // added
        [AttributeMappingProperty(ColumnName = "Referral Service Recommended", PropertyName = "ReferralServicesRecommended")]
        public string ReferralServicesRecommended { get; set; }
        public int? CreationUserID { get; set; }
        public DateTime? LastModificationTimestamp { get; set; }
        public int? LastModificationUserID { get; set; }

        public string DescriptionOfTheIncident { get; set; }
        public int DescriptionOfTheIncidentID { get; set; }
        public string ReportingChannel { get; set; }
        public string ReportingChannelOther { get; set; }
        public int ConsentSignedID { get; set; }
        public string StatusAfterReferral { get; set; }
        public string RecommendedDetails { get; set; }
        public bool? VerbalConsentGranted { get; set; }

    }
}

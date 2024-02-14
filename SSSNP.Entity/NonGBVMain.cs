using SSSNP_SNSOP.Entity.MappingProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class NonGBVMain
    {
        [AttributeMappingProperty(ColumnName = "S#NO", PropertyName = "NonGBVMainID", IsMandatory = true)]
        public int NonGBVMainID { get; set; }
        [AttributeMappingProperty(ColumnName = "DATE INSERT", PropertyName = "Date", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping", IsMandatory = true)]
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
        [AttributeMappingProperty(ColumnName = "Date Of Complaint", PropertyName = "DateOfComplaint", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping", IsMandatory = true)]
        public DateTime DateOfComplaint { get; set; }
        [AttributeMappingProperty(ColumnName = "Reporting Channel", PropertyName = "ReportingChannel", IsMandatory = true)]
        public string ReportingChannel { get; set; }
        [AttributeMappingProperty(ColumnName = "Reporting Channel Other", PropertyName = "ReportingChannelOther")]
        public string ReportingChannelOther { get; set; }
        [AttributeMappingProperty(ColumnName = "Complainant Name", PropertyName = "ComplainantName", IsMandatory = true)]
        public string ComplainantName { get; set; }
        [AttributeMappingProperty(ColumnName = "Contact Number", PropertyName = "ContactNumber")]
        public string ContactNumber { get; set; }
        [AttributeMappingProperty(ColumnName = "Physical Address", PropertyName = "PhysicalAddress")]   
        public string PhysicalAddress { get; set; }
        [AttributeMappingProperty(ColumnName = "Gender", PropertyName = "Gender")]
        public string Gender { get; set; }
        [AttributeMappingProperty(ColumnName = "Age", PropertyName = "Age")]
        public string Age { get; set; }
        [AttributeMappingProperty(ColumnName = "SSSNP Beneficiary", PropertyName = "SSSNPBeneficiary")]
        public string SSSNPBeneficiary { get; set; }
        [AttributeMappingProperty(ColumnName = "Category Of Grievance", PropertyName = "TypeOfGrievanceID", ApplyLookupMapping = true, LookupMappingName = "LK_TypeOfGrievanceID")]
        public int? TypeOfGrievanceID { get; set; }
        [AttributeMappingProperty(ColumnName = "Type Of Grievance", PropertyName = "TypeOfGrievanceDetailID", ApplyLookupMapping = true, LookupMappingName = "LK_TypeOfGrievanceDetailID")]
        public int? TypeOfGrievanceDetailID { get; set; }
        public string TypeOfGrievanceOther { get; set; }

        [AttributeMappingProperty(ColumnName = "Status", PropertyName = "Status", ApplyLookupMapping = true, LookupMappingName = "LK_StatusIdToStatusName", IsMandatory = true)]
        public int Status { get; set; }
        public DateTime? StatusLastModificationDate { get; set; }
        public int? CreationUserID { get; set; }
        public DateTime? LastModificationTimestamp { get; set; }
        public int? LastModificationUserID { get; set; }
        public string DescriptionCompliant { get; set; }
        public int DescriptionCompliantAttachmentID { get; set; }
        public int TypeOfComplaintID { get; set; }


    }
}

using SSSNP_SNSOP.Entity.MappingProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class GBVDetail
    {
        public int GBVDetailID { get; set; }
        [AttributeMappingProperty(ColumnName = "S#NO", PropertyName = "GBVMainID", IsMandatory = true)]
        public int GBVMainID { get; set; }
        //[AttributeMappingProperty(ColumnName = "Description of the Incident", PropertyName = "DescriptionOfTheIncident", IsMandatory =true)]
        //public string DescriptionOfTheIncident { get; set; }
        [AttributeMappingProperty(ColumnName = "Reported To Management HR", PropertyName = "ReportedToManagementHR", ApplyLookupMapping = true, LookupMappingName = "LK_TrueFalseMapping")]
        public bool? ReportedToManagementHR { get; set; }
        [AttributeMappingProperty(ColumnName = "Reported Details", PropertyName = "ReportedDetails")]
        public string ReportedDetails { get; set; }
        [AttributeMappingProperty(ColumnName = "FollowUp Action Feedback", PropertyName = "FollowUpActionFeedback", IsMandatory = true)]
        public string FollowUpActionFeedback { get; set; }
        [AttributeMappingProperty(ColumnName = "Is Survivor Satisfied", PropertyName = "IsSurvivorSatisfied", ApplyLookupMapping = true, LookupMappingName = "LK_TrueFalseMapping")]
        public bool? IsSurvivorSatisfied { get; set; }
        [AttributeMappingProperty(ColumnName = "Survivor Details", PropertyName = "SurvivorDetails")]
        public string SurvivorDetails { get; set; }
        [AttributeMappingProperty(ColumnName = "Closing Date", PropertyName = "ClosingDateAfterManagementAction", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping")]
        public DateTime? ClosingDateAfterManagementAction { get; set; }
        public int? CreationUserID { get; set; }
        public DateTime? LastModificationTimestamp { get; set; }
        public int? LastModificationUserID { get; set; }


    }
}

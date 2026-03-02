using SSSNP_SNSOP.Entity.MappingProperty;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
	[Serializable]
	public class NonGBVDetail
	{
		public int NonGBVDetailID { get; set; }
		[AttributeMappingProperty(ColumnName = "S#NO", PropertyName = "NonGBVMainID", IsMandatory = true)]
		public int NonGBVMainID { get; set; }
		//[AttributeMappingProperty(ColumnName = "Description Of Complaint", PropertyName = "DescriptionOfComplaint", IsMandatory = true)]
		//public string DescriptionOfComplaint { get; set; }
		[AttributeMappingProperty(ColumnName = "Action Taken", PropertyName = "ActionResponseComplaint", IsMandatory = true)]
		public string ActionResponseComplaint { get; set; }
		[AttributeMappingProperty(ColumnName = "Resolution by", PropertyName = "ActionResolutionDesignation", IsMandatory = true)]
		public string ActionResolutionDesignation { get; set; }
		[AttributeMappingProperty(ColumnName = "Feedback to complainant", PropertyName = "FeedbackComplainant", IsMandatory = true)]
		public string FeedbackComplainant { get; set; }
		[AttributeMappingProperty(ColumnName = "Date Of Resolution", PropertyName = "DataOfResolution", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping", IsMandatory = true)]
		public DateTime DataOfResolution { get; set; }
		[AttributeMappingProperty(ColumnName = "Pending Action Required", PropertyName = "PendingActionRequered", IsMandatory = true)]
		public bool PendingActionRequered { get; set; }
		[AttributeMappingProperty(ColumnName = "Escalated Referred Externally", PropertyName = "EscaletedReferredExternally", IsMandatory = true)]
		public bool EscaletedReferredExternally { get; set; }
		[AttributeMappingProperty(ColumnName = "Appeal Lodged", PropertyName = "AppealLodged", IsMandatory = true)]
		public bool AppealLodged { get; set; }
		[AttributeMappingProperty(ColumnName = "Appeal Date", PropertyName = "AppealDate", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping")]
		public DateTime? AppealDate { get; set; }
		[AttributeMappingProperty(ColumnName = "Appeal Outcome Satisfactory", PropertyName = "AppealOutcomeSatisfactory")]
		public bool? AppealOutcomeSatisfactory { get; set; }
		[AttributeMappingProperty(ColumnName = "Unresolved Recommended", PropertyName = "UnresolvedReccommended")]
		public string UnresolvedReccommended { get; set; }
		[AttributeMappingProperty(ColumnName = "Closing Date", PropertyName = "ClosingDate", ApplyLookupMapping = true, LookupMappingName = "LK_DatetimeMapping")]
		//public int? TypeOfComplaintID { get; set; }
		public DateTime? ClosingDate { get; set; }
		public int? CreationUserID { get; set; }
		public DateTime? LastModificationTimestamp { get; set; }
		public int? LastModificationUserID { get; set; }
		[AttributeMappingProperty(ColumnName = "Pending Action Note", PropertyName = "PendingActionRequeredNote")]
		public string PendingActionRequeredNote { get; set; }
		[AttributeMappingProperty(ColumnName = "DetailOfTheAccused", PropertyName = "Detail Of The Accused")]
		public string DetailOfTheAccused { get; set; }
		public string Status { get; set; }
		public int ActionResolutionDesignationID { get; set; }
		public int FeedbackComplainantID { get; set; }
		public bool? HasInvestigation { get; set; }
		public string NameInvestigation { get; set; }
		public string DesignationInvestigation { get; set; }
		public DateTime? InvestigationCompletedDate { get; set; }
		public string MainFindingsInvestigation { get; set; }
		public int InvestigationAttachID { get; set; }
		public string InvestigationAction { get; set; }
		public string NameOfTheAccused { get; set; }
		public int StateID { get; set; }
		public int CountyID { get; set; }
		public int PayamID { get; set; }
		public int BomaID { get; set; }
		public string SSSNPAccused { get; set; }
		public string DetailOfTheAccusedOther { get; set; }

	}
}

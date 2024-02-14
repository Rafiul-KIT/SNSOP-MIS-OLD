using SSSNP_SNSOP.Entity.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.Interfaces
{
    public interface ISSSNPManager
    {
        #region Lookup
        List<LK_GBVStatus> GetStatusAll();
        List<LK_GBVStatus> GetNonGBVStatus();
        LK_GBVStatus GetGBVStatus(int statusID);
        List<LK_TypeOfGrievance> GetTypeOfGrievances();
        LK_TypeOfGrievance GetTypeOfGrievance(int typeOfGrievanceID);
        List<LK_TypeOfGrievanceDetail> GetTypeOfGrievancesDetails(int typeOfGrievanceMainID);
        LK_TypeOfGrievanceDetail GetTypeOfGrievancesDetail(int typeOfGrievanceDetailID);
        List<LK_TypeOfGrievanceDetail> GetTypeOfGrievancesDetails();
        List<LK_GBVStatus> GetGBVStatus();
        List<LK_TypeOfComplaint> GetTypeOfComplaint();
        LK_TypeOfComplaint GetTypeOfComplaint(int typeOfComplaintID);

        #endregion

        #region GBV
        int CreateGBVMain(GBVMain main);
        int UpdateGBVMain(GBVMain main);
        GBVMain GetGBVMain(int GBVMainID);
        int DeleteGBVMain(int GBVMainID, int userID);
        int UpdateGBVStatus(int GBVMainID, int status, int userID);
        int GetGBVMainIDByDetailID(int detailID);
        List<GBVMainExtended> GetGBVMainExtended(GBVLandingCriteria filter);

        int CreateGBVDetail(GBVDetail detail);
        int UpdateGBVDetail(GBVDetail detail);
        List<GBVDetail> GetGBVDetails(int GBVMainID);
        GBVDetail GetGBVDetail(int GBVDetailID);
        int DeleteGBVDetail(int GBVDetailID, int userID);
        #endregion

        #region GBV Attachment
        int InsertGBVAttachment(GBVAttachment attachment);
        List<GBVAttachment> GetGBVAttachments(int GBVMainID);
        int DeleteGBVAttachment(int GBVMainID, int userID);
        GBVAttachment GetGBVAttachment(int GBVMainID);

        #endregion

        #region Non-GBV
        int CreateNonGBVMain(NonGBVMain main);
        int UpdateNonGBVMain(NonGBVMain main);
        NonGBVMain GetNonGBVMain(int nonGBVMainID);
        int DeleteNonGBVMain(int nonGBVMainID, int userID);
        int CreateNonGBVDetail(NonGBVDetail detail);
        List<NonGBVDetail> GetNonGBVDetails(int nonGBVMainID);
        NonGBVDetail GetNonGBVDetail(int nonGBVDetailID);
        int UpdateNonGBVDetail(NonGBVDetail detail);
        int DeleteNonGBVDetail(int nonGBVDetailID, int userID);
        List<NonGBVMainExtended> GetNonGBVMainExtended(NonGBVLandingCriteria filter);
        int UpdateNonGBVStatus(int nonGBVMainID, int status, int userID);
        int ChangeNonGBVStatusByDetails(NonGBVDetail details, int userID);
        int GetNonGBVMainIDByDetailID(int detailID);

        #endregion

        #region Non-GBV Attachment
        int InsertNonGBVAttachment(NonGBVAttachment attachment);
        int UpdateNonGBVAttachment(NonGBVAttachment attachment);
        int DeleteNonGBVAttachment(int nonGBVAttachmentID, int userID);
        List<NonGBVAttachment> GetNonGBVAttachments(int nonGBVDetailID);
        NonGBVAttachment GetNonGBVAttachment(int nonGBVAttachmentID);

        #endregion

        #region User
        tblUser GetUser(int userID);


        #endregion

        #region Dashboard
        List<LabelValue> GetNonGBVDashboardStatusCount();

        List<LabelValue> GetGBVDashboardStatusCount();

        List<LabelValue> GetTypesOfReferral();

        List<LabelValue> GetSentToReferralService();

        List<LabelValue> GetGBVPerpetratorProject();

        List<LabelValue> GetNonGBVDashboardTypeCount();

        List<LabelValue> GetNonGBVDashboardTypeDelayedCount();

        List<LabelValue> GetGBVDashboardTypeDelayedCount();

        List<LabelValue> GetGBVDashboardByCountyAll();

        List<LabelValue> GetGBVDashboardByStatusAll();

        List<LabelValue> GetGBVDashboardAvgToCloseByCountyAll();

        List<NonGBVByLocation> GetNonGBVDashboardByLocation();

        List<GBVByLocation> GetGBVDashboardByLocation();

        List<NonGBVByLocation> GetNonGBVDashboardByLocationResTimeAVG();

        List<GBVByLocation> GetGBVDashboardByLocationResTimeAVG();

        //List<LabelValue> GetTypesOfReferral();
        #endregion

        #region Imported File
        int CreateGBVImportedFile(GBVImportFile iFile);
        GBVImportFile GetGBVImportedFile(int id);
        int CheckGBVImportedFile(GBVImportFile iFile);
        int UpdateStatusGBVImportedFile(int id);
        #endregion

        #region Lookup

        LK_State GetStateLookup(string stateName);
        LK_State GetStateLookup(int stateID);

        LK_County GetCountyLookup(string countyName);
        LK_County GetCountyLookup(int countyID);

        LK_Payam GetPayamLookup(string payamName);
        LK_Payam GetPayamLookup(int payamID);

        LK_Boma GetBomaLookup(string bomaName);
        LK_Boma GetBomaLookup(int bomaID);
        List<LK_Boma> GetBomasLookup(int payamID);

        #endregion

        #region System Report GBV
        List<LabelPropertyThreeTable> GetGrievanceByStatus();

        List<LabelPropertyThreeTable> GetGrievanceByTypeGBV_NonGBV();
        List<LabelPropertyThreeTable> GetNon_GBVGrievanceByType();
        List<LabelPropertyThreeTable> GetGrievancesByProgram();
        List<LabelPropertySixTable> GetGrievancesByLocationAndType();
        List<LabelPropertyFourTable> GetGBVByProgramStatus();
        List<LabelPropertyThreeTable> GetAgeBreakdownSSSNP();
        List<LabelPropertyFourTable> GetGBVLocationAgeGroup();
        List<LabelPropertyFiveTable> GetSSNPRelatedComplaintLog();
        List<LabelPropertyFourTable> GetResolutionTimes();
        List<LabelPropertyThreeTable> GetAverageResolutionTimeByLocation();
        List<LabelPropertyTwoTable> DelayedGrievanceByType();
        #endregion

        #region Attachment Draft
        Guid InsertAttachmentDraft(AttachmentDraft attach);
        AttachmentDraft GetAttachmentDraft(Guid id);

        #endregion

        #region Non-GBV Attachment Dedicated
        int InsertNonGBVAttachmentDedicated(NonGBVAttachmentDedicated attachment);
        int DeleteNonGBVAttachmentDedicated(int nonGBVAttachmentID, int userID);
        NonGBVAttachmentDedicated GetNonGBVAttachmentDedicated(int nonGBVAttachmentID);

        #endregion

        #region GBV Attachment Dedicated
        int InsertGBVAttachmentDedicated(GBVAttachmentDedicated attachment);
        int DeleteGBVAttachmentDedicated(int GBVAttachmentID, int userID);
        GBVAttachmentDedicated GetGBVAttachmentDedicated(int GBVAttachmentID);

        #endregion

        #region DIS
        int CreateAttendanceDIS(BenAttendanceDetDIS attendance);
        int UpdateAttendanceDIS(BenAttendanceDetDIS attendance);
        BenAttendanceDetDIS GetAttendanceDIS(int intAttendanceID);
        List<BenAttendanceDetDIS> GetAttendanceDISByWorkActivity(int intWorkActivity);
        int CreateAttendanceDIS(BenAttendanceDetDIS attendance, string ids);

        List<BenAttendanceDet> GetAttendanceByWorkActivity(int intWorkActivity);
        List<BeneficiaryDIS> GetBeneficiariesDIS(BeneficiaryDISCriteria filter);
        #endregion
    }
}


using SSSNP_SNSOP.Core.DataAccess;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.Entity.FilterCriteria;
using SSSNP_SNSOP.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Business
{
    public class SSSNPManager : ISSSNPManager
    {
        private readonly string context = "SSSNPManager";

        #region Lookup
        public List<LK_GBVStatus> GetStatusAll()
        {
            List<LK_GBVStatus> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetStatusAll();
                ds.AcceptChanges();
            }

            return result;

        }
        public List<LK_GBVStatus> GetNonGBVStatus()
        {
            List<LK_GBVStatus> result = this.GetStatusAll();
            if (result == null || result.Count == 0) return new List<LK_GBVStatus>();

            return result.Where(x => x.NonGBVStatus).ToList();

        }
        public LK_GBVStatus GetGBVStatus(int statusID)
        {
            LK_GBVStatus result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVStatus(statusID);
                ds.AcceptChanges();
            }

            return result;

        }
        public List<LK_TypeOfGrievance> GetTypeOfGrievances()
        {
            List<LK_TypeOfGrievance> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfGrievances();
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_TypeOfGrievance GetTypeOfGrievance(int typeOfGrievanceID)
        {
            LK_TypeOfGrievance result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfGrievance(typeOfGrievanceID);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<LK_TypeOfGrievanceDetail> GetTypeOfGrievancesDetails(int typeOfGrievanceMainID)
        {
            List<LK_TypeOfGrievanceDetail> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfGrievancesDetails(typeOfGrievanceMainID);
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_TypeOfGrievanceDetail GetTypeOfGrievancesDetail(int typeOfGrievanceDetailID)
        {
            LK_TypeOfGrievanceDetail result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfGrievancesDetail(typeOfGrievanceDetailID);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<LK_TypeOfGrievanceDetail> GetTypeOfGrievancesDetails()
        {
            List<LK_TypeOfGrievanceDetail> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfGrievancesDetails();
                ds.AcceptChanges();
            }

            return result;
        }
        public List<LK_GBVStatus> GetGBVStatus()
        {
            List<LK_GBVStatus> result = this.GetStatusAll();
            if (result == null || result.Count == 0) return new List<LK_GBVStatus>();

            return result.Where(x => x.GBVStatus).ToList();
        }
        public List<LK_TypeOfComplaint> GetTypeOfComplaint()
        {
            List<LK_TypeOfComplaint> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfComplaint();
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_TypeOfComplaint GetTypeOfComplaint(int typeOfComplaintID)
        {
            LK_TypeOfComplaint result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypeOfComplaint(typeOfComplaintID);
                ds.AcceptChanges();
            }

            return result;
        }
        #endregion

        #region GBV
        public int CreateGBVMain(GBVMain main)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateGBVMain(main);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateGBVMain(GBVMain main)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateGBVMain(main);
                ds.AcceptChanges();
            }

            return result;
        }
        public GBVMain GetGBVMain(int GBVMainID)
        {
            GBVMain result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVMain(GBVMainID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteGBVMain(int GBVMainID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteGBVMain(GBVMainID, userID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateGBVStatus(int GBVMainID, int status, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateGBVStatus(GBVMainID, status, userID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int GetGBVMainIDByDetailID(int detailID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVMainIDByDetailID(detailID);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<GBVMainExtended> GetGBVMainExtended(GBVLandingCriteria filter)
        {
            List<GBVMainExtended> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVMainExtended(filter);
                ds.AcceptChanges();
            }

            return result;
        }
        public int CreateGBVDetail(GBVDetail detail)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateGBVDetail(detail);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateGBVDetail(GBVDetail detail)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateGBVDetail(detail);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<GBVDetail> GetGBVDetails(int GBVMainID)
        {
            List<GBVDetail> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDetails(GBVMainID);
                ds.AcceptChanges();
            }

            return result;
        }
        public GBVDetail GetGBVDetail(int GBVDetailID)
        {
            GBVDetail result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDetail(GBVDetailID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteGBVDetail(int GBVDetailID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteGBVDetail(GBVDetailID, userID);
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region GBV Attachment
        public int InsertGBVAttachment(GBVAttachment attachment)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.InsertGBVAttachment(attachment);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteGBVAttachment(int GBVMainID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteGBVAttachment(GBVMainID, userID);
                ds.AcceptChanges();
            }

            return result;

        }
        public List<GBVAttachment> GetGBVAttachments(int GBVMainID)
        {
            List<GBVAttachment> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVAttachments(GBVMainID);
                ds.AcceptChanges();
            }

            return result;
        }
        public GBVAttachment GetGBVAttachment(int GBVMainID)
        {
            GBVAttachment result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVAttachment(GBVMainID);
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region Non-GBV
        public int CreateNonGBVMain(NonGBVMain main)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateNonGBVMain(main);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateNonGBVMain(NonGBVMain main)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateNonGBVMain(main);
                ds.AcceptChanges();
            }

            return result;
        }
        public NonGBVMain GetNonGBVMain(int nonGBVMainID)
        {
            NonGBVMain result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVMain(nonGBVMainID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteNonGBVMain(int nonGBVMainID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteNonGBVMain(nonGBVMainID, userID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int CreateNonGBVDetail(NonGBVDetail detail)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateNonGBVDetail(detail);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<NonGBVDetail> GetNonGBVDetails(int nonGBVMainID)
        {
            List<NonGBVDetail> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDetails(nonGBVMainID);
                ds.AcceptChanges();
            }

            return result;
        }
        public NonGBVDetail GetNonGBVDetail(int nonGBVDetailID)
        {
            NonGBVDetail result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDetail(nonGBVDetailID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateNonGBVDetail(NonGBVDetail detail)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateNonGBVDetail(detail);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteNonGBVDetail(int nonGBVDetailID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteNonGBVDetail(nonGBVDetailID, userID);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<NonGBVMainExtended> GetNonGBVMainExtended(NonGBVLandingCriteria filter)
        {
            List<NonGBVMainExtended> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVMainExtended(filter);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateNonGBVStatus(int nonGBVMainID, int status, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateNonGBVStatus(nonGBVMainID, status, userID);
                ds.AcceptChanges();
            }

            return result;
        }
        public int ChangeNonGBVStatusByDetails(NonGBVDetail details, int userID)
        {
            int status = 1;

            if (details.PendingActionRequered) { status = 2; }
            else if (details.EscaletedReferredExternally) { status = 3; }
            else if (details.AppealLodged) { status = 6; }
            else if (details.AppealOutcomeSatisfactory != null && (bool)!details.AppealOutcomeSatisfactory) { status = 7; }
            else { status = 0; }

            if (status != 0)
            {
                return UpdateNonGBVStatus(details.NonGBVMainID, status, userID);
            }
            return 0;

        }
        public int GetNonGBVMainIDByDetailID(int detailID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVMainIDByDetailID(detailID);
                ds.AcceptChanges();
            }

            return result;
        }
        #endregion

        #region Non-GBV Attachment
        public int InsertNonGBVAttachment(NonGBVAttachment attachment)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.InsertNonGBVAttachment(attachment);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateNonGBVAttachment(NonGBVAttachment attachment)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateNonGBVAttachment(attachment);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteNonGBVAttachment(int nonGBVAttachmentID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteNonGBVAttachment(nonGBVAttachmentID, userID);
                ds.AcceptChanges();
            }

            return result;

        }
        public List<NonGBVAttachment> GetNonGBVAttachments(int nonGBVDetailID)
        {
            List<NonGBVAttachment> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVAttachments(nonGBVDetailID);
                ds.AcceptChanges();
            }

            return result;
        }
        public NonGBVAttachment GetNonGBVAttachment(int nonGBVAttachmentID)
        {
            NonGBVAttachment result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVAttachment(nonGBVAttachmentID);
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region User
        public tblUser GetUser(int userID)
        {
            tblUser result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetUser(userID);
                ds.AcceptChanges();
            }

            return result;
        }
        #endregion

        #region Dashboard
        public List<LabelValue> GetNonGBVDashboardStatusCount()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDashboardStatusCount();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetGBVDashboardByCountyAll()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardByCountyAll();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetGBVDashboardByStatusAll()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardByStatusAll();
                ds.AcceptChanges();
            }
            return result;
        }


        public List<LabelValue> GetGBVDashboardAvgToCloseByCountyAll()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardAvgToCloseByCountyAll();
                ds.AcceptChanges();
            }
            return result;
        }


        public List<LabelValue> GetGBVDashboardStatusCount()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardStatusCount();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetTypesOfReferral()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetTypesOfReferral();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetSentToReferralService()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetSentToReferralService();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetGBVPerpetratorProject()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVPerpetratorProject();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetNonGBVDashboardTypeCount()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDashboardTypeCount();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetNonGBVDashboardTypeDelayedCount()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDashboardTypeDelayedCount();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<LabelValue> GetGBVDashboardTypeDelayedCount()
        {
            List<LabelValue> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardTypeDelayedCount();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<NonGBVByLocation> GetNonGBVDashboardByLocation()
        {
            List<NonGBVByLocation> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDashboardByLocation();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<GBVByLocation> GetGBVDashboardByLocation()
        {
            List<GBVByLocation> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardByLocation();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<NonGBVByLocation> GetNonGBVDashboardByLocationResTimeAVG()
        {
            List<NonGBVByLocation> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVDashboardByLocationResTimeAVG();
                ds.AcceptChanges();
            }
            return result;
        }

        public List<GBVByLocation> GetGBVDashboardByLocationResTimeAVG()
        {
            List<GBVByLocation> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVDashboardByLocationResTimeAVG();
                ds.AcceptChanges();
            }
            return result;
        }

        #endregion

        #region Imported File
        public int CreateGBVImportedFile(GBVImportFile iFile)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateGBVImportedFile(iFile);
                ds.AcceptChanges();
            }

            return result;
        }
        public GBVImportFile GetGBVImportedFile(int id)
        {
            GBVImportFile result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVImportedFile(id);
                ds.AcceptChanges();
            }

            return result;
        }
        public int CheckGBVImportedFile(GBVImportFile iFile)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CheckGBVImportedFile(iFile);
                ds.AcceptChanges();
            }

            return result;
        }
        public int UpdateStatusGBVImportedFile(int id)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateStatusGBVImportedFile(id);
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region Lookup

        public LK_State GetStateLookup(string stateName)
        {
            LK_State result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetStateLookup(stateName);
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_State GetStateLookup(int stateID)
        {
            LK_State result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetStateLookup(stateID);
                ds.AcceptChanges();
            }

            return result;
        }

        public LK_County GetCountyLookup(string countyName)
        {
            LK_County result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetCountyLookup(countyName);
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_County GetCountyLookup(int countyID)
        {
            LK_County result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetCountyLookup(countyID);
                ds.AcceptChanges();
            }

            return result;
        }

        public LK_Payam GetPayamLookup(string payamName)
        {
            LK_Payam result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetPayamLookup(payamName);
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_Payam GetPayamLookup(int payamID)
        {
            LK_Payam result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetPayamLookup(payamID);
                ds.AcceptChanges();
            }

            return result;
        }

        public LK_Boma GetBomaLookup(string BomaName)
        {
            LK_Boma result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetBomaLookup(BomaName);
                ds.AcceptChanges();
            }

            return result;
        }
        public LK_Boma GetBomaLookup(int BomaID)
        {
            LK_Boma result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetBomaLookup(BomaID);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<LK_Boma> GetBomasLookup(int payamID)
        {
            List<LK_Boma> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetBomasLookup(payamID);
                ds.AcceptChanges();
            }

            return result;
        }



        #endregion

        #region System Report
        public List<Entity.LabelPropertyThreeTable> GetGrievanceByStatus()
        {
            List<Entity.LabelPropertyThreeTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGrievanceByStatus();
                ds.AcceptChanges();
            }

            return result;
        }
        public List<LabelPropertyThreeTable> GetGrievanceByTypeGBV_NonGBV()
        {
            List<Entity.LabelPropertyThreeTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGrievanceByTypeGBV_NonGBV();
                ds.AcceptChanges();
            }

            return result;
        }
        public List<LabelPropertyThreeTable> GetNon_GBVGrievanceByType()
        {
            List<Entity.LabelPropertyThreeTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNon_GBVGrievanceByType();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyThreeTable> GetGrievancesByProgram()
        {
            List<Entity.LabelPropertyThreeTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGrievancesByProgram();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertySixTable> GetGrievancesByLocationAndType()
        {
            List<Entity.LabelPropertySixTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGrievancesByLocationAndType();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyFourTable> GetGBVByProgramStatus()
        {
            List<Entity.LabelPropertyFourTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVByProgramStatus();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyThreeTable> GetAgeBreakdownSSSNP()
        {
            List<Entity.LabelPropertyThreeTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetAgeBreakdownSSSNP();
                ds.AcceptChanges();
            }

            return result;

        }
        public List<LabelPropertyFourTable> GetGBVLocationAgeGroup()
        {
            List<Entity.LabelPropertyFourTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVLocationAgeGroup();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyFiveTable> GetSSNPRelatedComplaintLog()
        {
            List<Entity.LabelPropertyFiveTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetSSNPRelatedComplaintLog();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyFourTable> GetResolutionTimes()
        {
            List<Entity.LabelPropertyFourTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetResolutionTimes();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyThreeTable> GetAverageResolutionTimeByLocation()
        {
            List<Entity.LabelPropertyThreeTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetAverageResolutionTimeByLocation();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<LabelPropertyTwoTable> DelayedGrievanceByType()
        {
            List<Entity.LabelPropertyTwoTable> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DelayedGrievanceByType();
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region Attachment Draft

        public Guid InsertAttachmentDraft(AttachmentDraft attach)
        {
            Guid result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.InsertAttachmentDraft(attach);
                ds.AcceptChanges();
            }

            return result;
        }
        public AttachmentDraft GetAttachmentDraft(Guid id)
        {
            AttachmentDraft result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetAttachmentDraft(id);
                ds.AcceptChanges();
            }

            return result;
        }
        #endregion

        #region Non-GBV Attachment Dedicated
        public int InsertNonGBVAttachmentDedicated(NonGBVAttachmentDedicated attachment)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.InsertNonGBVAttachmentDedicated(attachment);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteNonGBVAttachmentDedicated(int nonGBVAttachmentID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteNonGBVAttachmentDedicated(nonGBVAttachmentID, userID);
                ds.AcceptChanges();
            }

            return result;

        }
        public NonGBVAttachmentDedicated GetNonGBVAttachmentDedicated(int nonGBVAttachmentID)
        {
            NonGBVAttachmentDedicated result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetNonGBVAttachmentDedicated(nonGBVAttachmentID);
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region GBV Attachment Dedicated
        public int InsertGBVAttachmentDedicated(GBVAttachmentDedicated attachment)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.InsertGBVAttachmentDedicated(attachment);
                ds.AcceptChanges();
            }

            return result;
        }
        public int DeleteGBVAttachmentDedicated(int GBVAttachmentID, int userID)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.DeleteGBVAttachmentDedicated(GBVAttachmentID, userID);
                ds.AcceptChanges();
            }

            return result;

        }
        public GBVAttachmentDedicated GetGBVAttachmentDedicated(int GBVAttachmentID)
        {
            GBVAttachmentDedicated result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetGBVAttachmentDedicated(GBVAttachmentID);
                ds.AcceptChanges();
            }

            return result;
        }

        #endregion

        #region DIS
        public int CreateAttendanceDIS(BenAttendanceDetDIS attendance)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateAttendanceDIS(attendance);
                ds.AcceptChanges();
            }

            return result;
        }
        public int CreateAttendanceDIS(BenAttendanceDetDIS attendance, string ids)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.CreateAttendanceDIS(attendance, ids);
                ds.AcceptChanges();
            }

            return result;
        }

        public int UpdateAttendanceDIS(BenAttendanceDetDIS attendance)
        {
            int result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.UpdateAttendanceDIS(attendance);
                ds.AcceptChanges();
            }

            return result;
        }
        public BenAttendanceDetDIS GetAttendanceDIS(int intAttendanceID)
        {
            BenAttendanceDetDIS result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetAttendanceDIS(intAttendanceID);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<BenAttendanceDetDIS> GetAttendanceDISByWorkActivity(int intWorkActivity)
        {
            List<BenAttendanceDetDIS> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetAttendanceDISByWorkActivity(intWorkActivity);
                ds.AcceptChanges();
            }

            return result;

        }

        public List<BenAttendanceDet> GetAttendanceByWorkActivity(int intWorkActivity)
        {
            List<BenAttendanceDet> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetAttendanceByWorkActivity(intWorkActivity);
                ds.AcceptChanges();
            }

            return result;
        }

        public List<BeneficiaryDIS> GetBeneficiariesDIS(BeneficiaryDISCriteria filter)
        {
            List<BeneficiaryDIS> result;

            using (SSSNPDataAccess ds = new SSSNPDataAccess(this.context))
            {
                result = ds.GetBeneficiariesDIS(filter);
                ds.AcceptChanges();
            }

            return result;
        }
        #endregion      
    }
}

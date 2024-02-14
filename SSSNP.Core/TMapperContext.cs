using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core
{
    public static class TMapperContext
    {
        private static readonly string namespaceRootApplication = "SSSNP_SNSOP.Core.DataAccess.Typemappers.";

        // Typemappers Core
        public static readonly string GBVStatusTypemapper = namespaceRootApplication + "LK_GBVStatusTypeMapper.xml";
        public static readonly string TypeOfGrievance = namespaceRootApplication + "LK_TypeOfGrievanceTypeMapper.xml";
        public static readonly string TypeOfGrievanceDetail = namespaceRootApplication + "LK_TypeOfGrievanceDetailTypeMapper.xml";
        public static readonly string NonGBVMain = namespaceRootApplication + "NonGBVMainTypeMapper.xml";
        public static readonly string NonGBVDetail = namespaceRootApplication + "NonGBVDetailTypeMapper.xml";
        public static readonly string NonGBVAttachment = namespaceRootApplication + "NonGBVAttachmentTypeMapper.xml";
        public static readonly string User = namespaceRootApplication + "tblUserTypeMapper.xml";
        public static readonly string NonGBVMainExtended = namespaceRootApplication + "NonGBVMainExtendedTypeMapper.xml";
        public static readonly string LabelValue = namespaceRootApplication + "LabelValueTypeMapper.xml";
        public static readonly string NonGBVByLocation = namespaceRootApplication + "NonGBVByLocationTypeMapper.xml";
        public static readonly string GBVByLocation = namespaceRootApplication + "GBVByLocationTypeMapper.xml";
        public static readonly string GBVImportedFile = namespaceRootApplication + "GBVImportedFileTypeMapper.xml";
        public static readonly string StateLookup = namespaceRootApplication + "StateLookupTypeMapper.xml";
        public static readonly string CountyLookup = namespaceRootApplication + "CountyLookupTypeMapper.xml";
        public static readonly string PayamLookup = namespaceRootApplication + "PayamLookupTypeMapper.xml";
        public static readonly string BomaLookup = namespaceRootApplication + "BomaLookupTypeMapper.xml";
        public static readonly string GBVMain = namespaceRootApplication + "GBVMainTypeMapper.xml";
        public static readonly string GBVDetail = namespaceRootApplication + "GBVDetailTypeMapper.xml";
        public static readonly string GBVMainExtended = namespaceRootApplication + "GBVMainExtendedTypeMapper.xml";
        public static readonly string GBVAttachment = namespaceRootApplication + "GBVAttachmentTypeMapper.xml";
        public static readonly string TypeOfComplaint = namespaceRootApplication + "LK_TypeOfComplaintTypeMapper.xml";
        public static readonly string SystemReportGBV = namespaceRootApplication + "SystemReportGBVTypemapper.xml";
        public static readonly string AttachmentDraft = namespaceRootApplication + "AttachmentDraftTypemapper.xml";
        public static readonly string NonGBVAttachmentDedicated = namespaceRootApplication + "NonGBVAttachmentDedicatedTypeMapper.xml";
        public static readonly string GBVAttachmentDedicated = namespaceRootApplication + "GBVAttachmentDedicatedTypeMapper.xml";
        public static readonly string BenAttendanceDetDIS = namespaceRootApplication + "BenAttendanceDetDISTypeMapper.xml";
        public static readonly string BenAttendanceDet_LOC = namespaceRootApplication + "BenAttendanceDISTypeMapper_LOC.xml";
        public static readonly string BenAttendanceDet = namespaceRootApplication + "BenAttendanceDetTypeMapper.xml";
        public static readonly string BeneficiaryDIS = namespaceRootApplication + "BeneficiaryDISTypeMapper.xml";
        public static readonly string Beneficiary_LOC = namespaceRootApplication + "BeneficiaryTypeMapper_LOC.xml";

        //ELRP
        public static readonly string NonGBVMain_ELRP = namespaceRootApplication + "NonGBVMainTypeMapper_ELRP.xml";
        public static readonly string NonGBVDetail_ELRP = namespaceRootApplication + "NonGBVDetailTypeMapper_ELRP.xml";
        public static readonly string NonGBVAttachment_ELRP = namespaceRootApplication + "NonGBVAttachmentTypeMapper_ELRP.xml";
        public static readonly string NonGBVMainExtended_ELRP = namespaceRootApplication + "NonGBVMainExtendedTypeMapper_ELRP.xml";
        public static readonly string NonGBVByLocation_ELRP = namespaceRootApplication + "NonGBVByLocationTypeMapper_ELRP.xml";
        public static readonly string GBVByLocation_ELRP = namespaceRootApplication + "GBVByLocationTypeMapper_ELRP.xml";
        public static readonly string GBVImportedFile_ELRP = namespaceRootApplication + "GBVImportedFileTypeMapper_ELRP.xml";
        public static readonly string GBVMain_ELRP = namespaceRootApplication + "GBVMainTypeMapper_ELRP.xml";
        public static readonly string GBVDetail_ELRP = namespaceRootApplication + "GBVDetailTypeMapper_ELRP.xml";
        public static readonly string GBVMainExtended_ELRP = namespaceRootApplication + "GBVMainExtendedTypeMapper_ELRP.xml";
        public static readonly string GBVAttachment_ELRP = namespaceRootApplication + "GBVAttachmentTypeMapper_ELRP.xml";
        public static readonly string SystemReportGBV_ELRP = namespaceRootApplication + "SystemReportGBVTypemapper_ELRP.xml";
        public static readonly string NonGBVAttachmentDedicated_ELRP = namespaceRootApplication + "NonGBVAttachmentDedicatedTypeMapper_ELRP.xml";
        public static readonly string GBVAttachmentDedicated_ELRP = namespaceRootApplication + "GBVAttachmentDedicatedTypeMapper_ELRP.xml";
    }
}

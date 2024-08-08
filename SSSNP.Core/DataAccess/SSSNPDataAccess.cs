using CSDS.Core.Framework.Common.Filtering;
using CSDS.Core.Framework.DataAccess.BaseClasses;
using CSDS.Core.Framework.DataMapper;
using SSSNP_SNSOP.Core.BuilderPdfGenerator;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.Entity.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.DataAccess
{
    public class SSSNPDataAccess : DataAccessBase
    {
        public SSSNPDataAccess(string dataPartition) : base(dataPartition)
        {
            // do nothing
        }
        public SSSNPDataAccess(string dataPartition, IsolationLevel isolationLevel) : base(dataPartition, isolationLevel)
        {
            // do nothing
        }

        #region Lookup
        public List<LK_GBVStatus> GetStatusAll()
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVStatusTypemapper, "GetStatusAll");
            return ExecuteOperation<LK_GBVStatus>(tm, (IFilterCriteria)null);

        }
        public LK_GBVStatus GetGBVStatus(int statusID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVStatusTypemapper, "GetGBVStatus");
            return ExecuteOperation<LK_GBVStatus>(tm, CreateParameter("StatusID", statusID));

        }
        public List<LK_TypeOfGrievance> GetTypeOfGrievances()
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfGrievance, "GetTypeOfGrievances");
            return ExecuteOperation<LK_TypeOfGrievance>(tm, (IFilterCriteria)null);
        }
        public LK_TypeOfGrievance GetTypeOfGrievance(int typeOfGrievanceID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfGrievance, "GetTypeOfGrievance");
            return ExecuteOperation<LK_TypeOfGrievance>(tm, CreateParameter("TypeOfGrievanceID", typeOfGrievanceID));
        }
        public List<LK_TypeOfGrievanceDetail> GetTypeOfGrievancesDetails(int typeOfGrievanceMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfGrievanceDetail, "GetTypeOfGrievances");
            return ExecuteOperation<LK_TypeOfGrievanceDetail>(tm, (IFilterCriteria)null, CreateParameter("TypeOfGrievanceMainID", typeOfGrievanceMainID));
        }
        public List<LK_TypeOfGrievanceDetail> GetTypeOfGrievancesDetails()
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfGrievanceDetail, "GetTypeOfGrievancesAll");
            return ExecuteOperation<LK_TypeOfGrievanceDetail>(tm, (IFilterCriteria)null);

        }
        public LK_TypeOfGrievanceDetail GetTypeOfGrievancesDetail(int typeOfGrievanceDetailID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfGrievanceDetail, "GetTypeOfGrievance");
            return ExecuteOperation<LK_TypeOfGrievanceDetail>(tm, CreateParameter("TypeOfGrievanceDetailID", typeOfGrievanceDetailID));

        }
        public List<LK_TypeOfComplaint> GetTypeOfComplaint()
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfComplaint, "GetTypeOfComplaint");
            return ExecuteOperation<LK_TypeOfComplaint>(tm, (IFilterCriteria)null);
        }
        public LK_TypeOfComplaint GetTypeOfComplaint(int typeOfComplaintID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.TypeOfComplaint, "GetTypeOfComplaintByID");
            return ExecuteOperation<LK_TypeOfComplaint>(tm, CreateParameter("typeOfComplaintID", typeOfComplaintID));
        }
        #endregion

        #region GBV
        public int CreateGBVMain(GBVMain main)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMain, "CreateGBVMain");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVMain>(tm, main, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int UpdateGBVMain(GBVMain main)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMain, "UpdateGBVMain");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVMain>(tm, main, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;

        }
        public GBVMain GetGBVMain(int GBVMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMain, "GetGBVMain");
            return ExecuteOperation<GBVMain>(tm, CreateParameter("GBVMainID", GBVMainID));
        }
        public int DeleteGBVMain(int GBVMainID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMain, "DeleteGBVMain");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, returnValue,
                                CreateParameter("GBVMainID", GBVMainID),
                                CreateParameter("UserID", userID));

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int UpdateGBVStatus(int GBVMainID, int status, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMain, "UpdateGBVStatus");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("GBVMainID", GBVMainID),
                                                   CreateParameter("Status", status),
                                                   CreateParameter("UserID", userID),
                                                   returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int GetGBVMainIDByDetailID(int detailID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMain, "GetGBVMainIDByDetailID");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("GBVDetailID", detailID),
                                                                    returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public List<GBVMainExtended> GetGBVMainExtended(GBVLandingCriteria filter)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVMainExtended, "GetGBVMainExtended");
            return ExecuteOperation<GBVMainExtended>(tm, SearchFilterCriteria(filter));

        }
        public int CreateGBVDetail(GBVDetail detail)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVDetail, "CreateGBVDetail");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVDetail>(tm, detail, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int UpdateGBVDetail(GBVDetail detail)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVDetail, "UpdateGBVDetail");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVDetail>(tm, detail, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;

        }
        public List<GBVDetail> GetGBVDetails(int GBVMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVDetail, "GetGBVDetails");
            return ExecuteOperation<GBVDetail>(tm, (IFilterCriteria)null, CreateParameter("GBVMainID", GBVMainID));

        }
        public GBVDetail GetGBVDetail(int GBVDetailID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVDetail, "GetGBVDetail");
            return ExecuteOperation<GBVDetail>(tm, CreateParameter("GBVDetailID", GBVDetailID));

        }
        public int DeleteGBVDetail(int GBVDetailID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVDetail, "DeleteGBVDetail");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, returnValue,
                                CreateParameter("GBVDetailID", GBVDetailID),
                                CreateParameter("UserID", userID));

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }

        #endregion

        #region GBV Attachment
        public int InsertGBVAttachment(GBVAttachment attachment)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachment, "InsertGBVAttachment");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVAttachment>(tm, attachment, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public List<GBVAttachment> GetGBVAttachments(int GBVMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachment, "GetGBVAttachments");
            return ExecuteOperation<GBVAttachment>(tm, (IFilterCriteria)null, CreateParameter("GBVMainID", GBVMainID));
        }
        public GBVAttachment GetGBVAttachment(int GBVMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachment, "GetGBVAttachment");
            return ExecuteOperation<GBVAttachment>(tm, CreateParameter("GBVMainID", GBVMainID));
        }
        public int DeleteGBVAttachment(int GBVMainID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachment, "DeleteGBVAttachment");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("GBVMainID", GBVMainID),
                                    CreateParameter("UserID", userID), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }



        #endregion

        #region Non-GBV
        public int CreateNonGBVMain(NonGBVMain main)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMain, "CreateNonGBVMain");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVMain>(tm, main, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int UpdateNonGBVMain(NonGBVMain main)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMain, "UpdateNonGBVMain");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVMain>(tm, main, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;

        }
        public NonGBVMain GetNonGBVMain(int nonGBVMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMain, "GetNonGBVMain");
            return ExecuteOperation<NonGBVMain>(tm, CreateParameter("nonGBVMainID", nonGBVMainID));
        }
        public int DeleteNonGBVMain(int nonGBVMainID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMain, "DeleteNonGBVMain");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, returnValue,
                                CreateParameter("NonGBVMainID", nonGBVMainID),
                                CreateParameter("UserID", userID));

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int CreateNonGBVDetail(NonGBVDetail detail)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVDetail, "CreateNonGBVDetail");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVDetail>(tm, detail, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public List<NonGBVDetail> GetNonGBVDetails(int nonGBVMainID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVDetail, "GetNonGBVDetails");
            return ExecuteOperation<NonGBVDetail>(tm, (IFilterCriteria)null, CreateParameter("NonGBVMainID", nonGBVMainID));

        }
        public NonGBVDetail GetNonGBVDetail(int nonGBVDetailID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVDetail, "GetNonGBVDetail");
            return ExecuteOperation<NonGBVDetail>(tm, CreateParameter("NonGBVDetailID", nonGBVDetailID));

        }
        public int UpdateNonGBVDetail(NonGBVDetail detail)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVDetail, "UpdateNonGBVDetail");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVDetail>(tm, detail, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;

        }
        public int DeleteNonGBVDetail(int nonGBVDetailID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVDetail, "DeleteNonGBVDetail");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, returnValue,
                                CreateParameter("NonGBVDetailID", nonGBVDetailID),
                                CreateParameter("UserID", userID));

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public List<NonGBVMainExtended> GetNonGBVMainExtended(NonGBVLandingCriteria filter)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMainExtended, "GetNonGBVMainExtended");
            return ExecuteOperation<NonGBVMainExtended>(tm, SearchFilterCriteria(filter));

        }
        public int UpdateNonGBVStatus(int nonGBVMainID, int status, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMain, "UpdateNonGBVStatus");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("NonGBVMainID", nonGBVMainID),
                                                   CreateParameter("Status", status),
                                                   CreateParameter("UserID", userID),
                                                   returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int GetNonGBVMainIDByDetailID(int detailID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVMain, "GetNonGBVMainIDByDetailID");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("NonGBVDetailID", detailID),
                                                                           returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        #endregion

        #region Non-GBV Attachment
        public List<NonGBVAttachment> GetNonGBVAttachments(int nonGBVDetailID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachment, "GetNonGBVAttachments");
            return ExecuteOperation<NonGBVAttachment>(tm, (IFilterCriteria)null, CreateParameter("NonGBVDetailID", nonGBVDetailID));
        }
        public NonGBVAttachment GetNonGBVAttachment(int nonGBVAttachmentID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachment, "GetNonGBVAttachment");
            return ExecuteOperation<NonGBVAttachment>(tm, CreateParameter("nonGBVAttachmentID", nonGBVAttachmentID));
        }
        public int InsertNonGBVAttachment(NonGBVAttachment attachment)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachment, "InsertNonGBVAttachment");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVAttachment>(tm, attachment, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int UpdateNonGBVAttachment(NonGBVAttachment attachment)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachment, "UpdateNonGBVAttachment");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVAttachment>(tm, attachment, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int DeleteNonGBVAttachment(int nonGBVAttachmentID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachment, "DeleteNonGBVAttachment");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("NonGBVAttachmentID", nonGBVAttachmentID),
                                    CreateParameter("UserID", userID), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        #endregion

        #region User
        public tblUser GetUser(int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.User, "GetUserByID");
            return ExecuteOperation<tblUser>(tm, CreateParameter("userID", userID));
        }
        #endregion

        #region FilterCriteria
        private IFilterCriteria SearchFilterCriteria(NonGBVLandingCriteria filter)
        {
            FilterCriteriaBase criteria = new FilterCriteriaBase();



            if (string.IsNullOrEmpty(filter.StatusID) &&
                string.IsNullOrEmpty(filter.StateID) &&
                string.IsNullOrEmpty(filter.CountyID) &&
                string.IsNullOrEmpty(filter.PayamID) &&
                string.IsNullOrEmpty(filter.BomaID) &&
                string.IsNullOrEmpty(filter.Delayed)
              ) return (IFilterCriteria)null;

            if (!string.IsNullOrEmpty(filter.StatusID) && filter.StatusID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("Status",
                                                filter.StatusID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.StateID) && filter.StateID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("StateID",
                                                filter.StateID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.CountyID) && filter.CountyID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("CountyID",
                                                filter.CountyID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.PayamID) && filter.PayamID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("PayamID",
                                                filter.PayamID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }


            if (!string.IsNullOrEmpty(filter.BomaID) && filter.BomaID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("BomaID",
                                                filter.BomaID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.Delayed) && filter.Delayed != "0")
            {
                criteria.Add(new ConditionFilterCriteria("Expired",
                                                filter.Delayed,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            TopClause topClause = new TopClause { LimitValue = 100000 };
            criteria.AddTopClause(topClause);

            OrderByClause orderByClause = new OrderByClause
            {
                Desc = false,
                PropertyName = "OrderById"
            };
            criteria.AddOrderByClause(orderByClause);

            return (IFilterCriteria)criteria;

        }
        private IFilterCriteria SearchFilterCriteria(GBVLandingCriteria filter)
        {
            FilterCriteriaBase criteria = new FilterCriteriaBase();



            if (string.IsNullOrEmpty(filter.StatusID) &&
                string.IsNullOrEmpty(filter.StateID) &&
                string.IsNullOrEmpty(filter.CountyID) &&
                string.IsNullOrEmpty(filter.PayamID) &&
                string.IsNullOrEmpty(filter.BomaID)
              ) return (IFilterCriteria)null;

            if (!string.IsNullOrEmpty(filter.StatusID) && filter.StatusID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("Status",
                                                filter.StatusID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.StateID) && filter.StateID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("StateID",
                                                filter.StateID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.CountyID) && filter.CountyID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("CountyID",
                                                filter.CountyID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (!string.IsNullOrEmpty(filter.PayamID) && filter.PayamID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("PayamID",
                                                filter.PayamID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }


            if (!string.IsNullOrEmpty(filter.BomaID) && filter.BomaID != "0")
            {
                criteria.Add(new ConditionFilterCriteria("BomaID",
                                                filter.BomaID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            TopClause topClause = new TopClause { LimitValue = 100000 };
            criteria.AddTopClause(topClause);

            OrderByClause orderByClause = new OrderByClause
            {
                Desc = false,
                PropertyName = "OrderById"
            };
            criteria.AddOrderByClause(orderByClause);

            return (IFilterCriteria)criteria;

        }

        private IFilterCriteria SearchFilterCriteriaBeneficiariesDIS(BeneficiaryDISCriteria filter)
        {
            FilterCriteriaBase criteria = new FilterCriteriaBase();


            if (filter.intStateID == 0 &&
                filter.intCountieID == 0 &&
                filter.intPayamID == 0 &&
                filter.intBomasID == 0
              ) return (IFilterCriteria)null;


            if (filter.intBomasID != 0)
            {
                criteria.Add(new ConditionFilterCriteria("intBomasID",
                                                filter.intBomasID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }



            if (filter.intStateID != 0)
            {
                criteria.Add(new ConditionFilterCriteria("intStateID",
                                                filter.intStateID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (filter.intCountieID != 0)
            {
                criteria.Add(new ConditionFilterCriteria("intCountieID",
                                                filter.intCountieID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }

            if (filter.intPayamID != 0)
            {
                criteria.Add(new ConditionFilterCriteria("intPayamID",
                                                filter.intPayamID,
                                                ConditionFilterCriteriaOperator.CriteriaOperator.EQUAL,
                                                ConditionFilterCriteriaOperator.QueryOperator.AND));
            }


            return (IFilterCriteria)criteria;

        }
        #endregion

        #region Dashboard
        public List<LabelValue> GetNonGBVDashboardStatusCount()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetNonGBVDashboardStatusCount");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetGBVDashboardByCountyAll()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetGBVDashboardByCountyAll");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetGBVDashboardByStatusAll()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetGBVDashboardByStatusAll");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetGBVDashboardAvgToCloseByCountyAll()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetAvgToCloseByCountyAll");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetGBVDashboardStatusCount()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetGBVDashboardStatusCount");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetTypesOfReferral()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetTypesOfReferral");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetGBVPerpetratorProject()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetGBVPerpetratorProject");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetSentToReferralService()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetSentToReferralService");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetNonGBVDashboardTypeCount()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetNonGBVDashboardTypeCount");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }


        public List<LabelValue> GetNonGBVDashboardTypeDelayedCount()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetNonGBVDashboardTypeDelayedCount");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<LabelValue> GetGBVDashboardTypeDelayedCount()
        {
            ITypemapper tm = new Typemapper(TMapperContext.LabelValue, "GetGBVDashboardTypeDelayedCount");
            return ExecuteOperation<LabelValue>(tm, (IFilterCriteria)null);
        }

        public List<NonGBVByLocation> GetNonGBVDashboardByLocation()
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVByLocation, "GetNonGBVDashboardByLocation");
            return ExecuteOperation<NonGBVByLocation>(tm, (IFilterCriteria)null);
        }

        public List<GBVByLocation> GetGBVDashboardByLocation()
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVByLocation, "GetGBVDashboardByLocation");
            return ExecuteOperation<GBVByLocation>(tm, (IFilterCriteria)null);
        }

        public List<NonGBVByLocation> GetNonGBVDashboardByLocationResTimeAVG()
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVByLocation, "GetNonGBVDashboardByLocationResTimeAVG");
            return ExecuteOperation<NonGBVByLocation>(tm, (IFilterCriteria)null);
        }

        public List<GBVByLocation> GetGBVDashboardByLocationResTimeAVG()
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVByLocation, "GetGBVDashboardByLocationResTimeAVG");
            return ExecuteOperation<GBVByLocation>(tm, (IFilterCriteria)null);
        }
        #endregion

        #region Imported File
        public int CreateGBVImportedFile(GBVImportFile iFile)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVImportedFile, "CreateGBVImportedFile");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVImportFile>(tm, iFile, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public GBVImportFile GetGBVImportedFile(int id)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVImportedFile, "GetGBVImportedFile");
            return ExecuteOperation<GBVImportFile>(tm, CreateParameter("ID", id));
        }
        public int CheckGBVImportedFile(GBVImportFile iFile)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVImportedFile, "CheckGBVImportedFile");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVImportFile>(tm, iFile, returnValue);

            if (returnValue.Value == DBNull.Value) return 0;

            return (int)returnValue.Value;

        }
        public int UpdateStatusGBVImportedFile(int id)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVImportedFile, "UpdateStatusGBVImportedFile");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("@ID", id), returnValue);

            if (returnValue.Value == DBNull.Value) return 0;

            return (int)returnValue.Value;
        }

        #endregion

        #region Lookup

        public LK_State GetStateLookup(string stateName)
        {
            ITypemapper tm = new Typemapper(TMapperContext.StateLookup, "GetStateLookupByName");
            return ExecuteOperation<LK_State>(tm, CreateParameter("StateName", stateName));

        }
        public LK_State GetStateLookup(int stateID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.StateLookup, "GetStateLookupByID");
            return ExecuteOperation<LK_State>(tm, CreateParameter("StateID", stateID));

        }

        public LK_County GetCountyLookup(string countyName)
        {
            ITypemapper tm = new Typemapper(TMapperContext.CountyLookup, "GetCountyLookupByName");
            return ExecuteOperation<LK_County>(tm, CreateParameter("CountyName", countyName));

        }
        public LK_County GetCountyLookup(int countyID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.CountyLookup, "GetCountyLookupByID");
            return ExecuteOperation<LK_County>(tm, CreateParameter("CountyID", countyID));

        }

        public LK_Payam GetPayamLookup(string payamName)
        {
            ITypemapper tm = new Typemapper(TMapperContext.PayamLookup, "GetPayamLookupByName");
            return ExecuteOperation<LK_Payam>(tm, CreateParameter("PayamName", payamName));

        }
        public LK_Payam GetPayamLookup(int payamID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.PayamLookup, "GetPayamLookupByID");
            return ExecuteOperation<LK_Payam>(tm, CreateParameter("PayamID", payamID));

        }

        public LK_Boma GetBomaLookup(string BomaName)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BomaLookup, "GetBomaLookupByName");
            return ExecuteOperation<LK_Boma>(tm, CreateParameter("BomaName", BomaName));

        }
        public LK_Boma GetBomaLookup(int BomaID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BomaLookup, "GetBomaLookupByID");
            return ExecuteOperation<LK_Boma>(tm, CreateParameter("BomaID", BomaID));

        }
        public List<LK_Boma> GetBomasLookup(int payamID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BomaLookup, "GetBomasLookup");
            return ExecuteOperation<LK_Boma>(tm, (IFilterCriteria)null, CreateParameter("payamID", payamID));
        }
        #endregion

        #region System Report
        public List<LabelPropertyThreeTable> GetGrievanceByStatus()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetGrievanceByStatus");
            return ExecuteOperation<LabelPropertyThreeTable>(tm, (IFilterCriteria)null);
        }
        public List<LabelPropertyThreeTable> GetGrievanceByTypeGBV_NonGBV()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetGrievanceByTypeGBV_NonGBV");
            return ExecuteOperation<LabelPropertyThreeTable>(tm, (IFilterCriteria)null);
        }
        public List<LabelPropertyThreeTable> GetNon_GBVGrievanceByType()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetNon_GBVGrievanceByType");
            return ExecuteOperation<LabelPropertyThreeTable>(tm, (IFilterCriteria)null);
        }
        public List<LabelPropertyThreeTable> GetGrievancesByProgram()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetGrievancesByProgram");
            return ExecuteOperation<LabelPropertyThreeTable>(tm, (IFilterCriteria)null);
        }

        public List<LabelPropertySixTable> GetGrievancesByLocationAndType()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetGrievancesByLocationAndType");
            return ExecuteOperation<LabelPropertySixTable>(tm, (IFilterCriteria)null);
        }

        public List<LabelPropertyFourTable> GetGBVByProgramStatus()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetGBVByProgramStatus");
            return ExecuteOperation<LabelPropertyFourTable>(tm, (IFilterCriteria)null);
        }

        public List<LabelPropertyThreeTable> GetAgeBreakdownSSSNP()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetAgeBreakdownSSSNP");
            return ExecuteOperation<LabelPropertyThreeTable>(tm, (IFilterCriteria)null);
        }
        public List<LabelPropertyFourTable> GetGBVLocationAgeGroup()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetGBVLocationAgeGroup");
            return ExecuteOperation<LabelPropertyFourTable>(tm, (IFilterCriteria)null);

        }

        public List<LabelPropertyFiveTable> GetSSNPRelatedComplaintLog()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetSSNPRelatedComplaintLog");
            return ExecuteOperation<LabelPropertyFiveTable>(tm, (IFilterCriteria)null);

        }


        public List<LabelPropertyFourTable> GetResolutionTimes()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetResolutionTimes");
            return ExecuteOperation<LabelPropertyFourTable>(tm, (IFilterCriteria)null);
        }

        public List<LabelPropertyThreeTable> GetAverageResolutionTimeByLocation()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "GetAverageResolutionTimeByLocation");
            return ExecuteOperation<LabelPropertyThreeTable>(tm, (IFilterCriteria)null);
        }

        public List<LabelPropertyTwoTable> DelayedGrievanceByType()
        {
            ITypemapper tm = new Typemapper(TMapperContext.SystemReportGBV, "DelayedGrievanceByType");
            return ExecuteOperation<LabelPropertyTwoTable>(tm, (IFilterCriteria)null);
        }

        #endregion

        #region Attachment Draft
        public Guid InsertAttachmentDraft(AttachmentDraft attach)
        {
            ITypemapper tm = new Typemapper(TMapperContext.AttachmentDraft, "InsertAttachmentDraft");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue", DbType.Guid);

            this.ExecuteNonQuery<AttachmentDraft>(tm, attach, returnValue);

            if (returnValue.Value == DBNull.Value) return Guid.Empty;

            return (Guid)returnValue.Value;
        }
        public AttachmentDraft GetAttachmentDraft(Guid id)
        {
            ITypemapper tm = new Typemapper(TMapperContext.AttachmentDraft, "GetAttachmentDraft");
            return ExecuteOperation<AttachmentDraft>(tm, CreateParameter("AttachmentID", id));
        }


        #endregion

        #region Non-GBV Attachment Dedicated

        public NonGBVAttachmentDedicated GetNonGBVAttachmentDedicated(int nonGBVAttachmentID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachmentDedicated, "GetNonGBVAttachmentDedicated");
            return ExecuteOperation<NonGBVAttachmentDedicated>(tm, CreateParameter("nonGBVAttachmentID", nonGBVAttachmentID));
        }
        public int InsertNonGBVAttachmentDedicated(NonGBVAttachmentDedicated attachment)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachmentDedicated, "InsertNonGBVAttachmentDedicated");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NonGBVAttachmentDedicated>(tm, attachment, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int DeleteNonGBVAttachmentDedicated(int nonGBVAttachmentID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.NonGBVAttachmentDedicated, "DeleteNonGBVAttachmentDedicated");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("NonGBVAttachmentID", nonGBVAttachmentID),
                                    CreateParameter("UserID", userID), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }

        #endregion

        #region GBV Attachment Dedicated

        public GBVAttachmentDedicated GetGBVAttachmentDedicated(int GBVAttachmentID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachmentDedicated, "GetGBVAttachmentDedicated");
            return ExecuteOperation<GBVAttachmentDedicated>(tm, CreateParameter("GBVAttachmentID", GBVAttachmentID));
        }
        public int InsertGBVAttachmentDedicated(GBVAttachmentDedicated attachment)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachmentDedicated, "InsertGBVAttachmentDedicated");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<GBVAttachmentDedicated>(tm, attachment, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int DeleteGBVAttachmentDedicated(int GBVAttachmentID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachmentDedicated, "DeleteGBVAttachmentDedicated");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("GBVAttachmentID", GBVAttachmentID),
                                    CreateParameter("UserID", userID), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }

        #endregion

        #region DIS
        public int CreateAttendanceDIS(BenAttendanceDetDIS attendance)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BenAttendanceDetDIS, "CreateAttendanceDIS");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<BenAttendanceDetDIS>(tm, attendance, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int CreateAttendanceDIS(BenAttendanceDetDIS attendance, string ids)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BenAttendanceDetDIS, "CreateAttendanceDISBulk");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<BenAttendanceDetDIS>(tm, attendance, CreateParameter("ids", ids), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;

        }
        public int UpdateAttendanceDIS(BenAttendanceDetDIS attendance)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BenAttendanceDetDIS, "UpdateAttendanceDIS");
            this.ExecuteNonQuery<BenAttendanceDetDIS>(tm, attendance);

            return 1;

        }
        public BenAttendanceDetDIS GetAttendanceDIS(int intAttendanceID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BenAttendanceDetDIS, "GetAttendanceDIS");
            return ExecuteOperation<BenAttendanceDetDIS>(tm, CreateParameter("IntAttendanceID", intAttendanceID));
        }
        public List<BenAttendanceDetDIS> GetAttendanceDISByWorkActivity(int intWorkActivity)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BenAttendanceDetDIS, "GetAttendanceDISByWorkActivity");
            return ExecuteOperation<BenAttendanceDetDIS>(tm, (IFilterCriteria)null, CreateParameter("intWorkActivityID", intWorkActivity));
        }
        public List<BenAttendanceDet> GetAttendanceByWorkActivity(int intWorkActivity)
        {
            ITypemapper tm = new Typemapper(TMapperContext.BenAttendanceDet, "GetAttendanceByWorkActivity");
            return ExecuteOperation<BenAttendanceDet>(tm, (IFilterCriteria)null, CreateParameter("intWorkActivityID", intWorkActivity));
        }
        public List<BeneficiaryDIS> GetBeneficiariesDIS(BeneficiaryDISCriteria filter)
        {
            if (string.IsNullOrEmpty(filter.Year)) throw new Exception("Year Parameter is null");
            if (filter.Month == 0 || filter.Month > 12) throw new Exception("Month Parameter not valid");

            ITypemapper tm = new Typemapper(TMapperContext.BeneficiaryDIS, "GetBeneficiariesDIS");
            return ExecuteOperation<BeneficiaryDIS>(tm, SearchFilterCriteriaBeneficiariesDIS(filter),
                                                    CreateParameter("Month", filter.Month),
                                                    CreateParameter("Year", filter.Year),
                                                    CreateParameter("PaymentCycle", filter.PaymentCycle));
        }

        #endregion



        #region GBV Attachment
 
        public int DeleteGBVAttachment_ELRP(int GBVMainID, int userID)
        {
            ITypemapper tm = new Typemapper(TMapperContext.GBVAttachment, "DeleteGBVAttachment_ELRP");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");
            this.ExecuteNonQuery(tm, CreateParameter("GBVMainID", GBVMainID),
                                    CreateParameter("UserID", userID), returnValue);
            if (returnValue.Value == DBNull.Value) return -1;
            return (int)returnValue.Value;
        }
        #endregion

       
    }
}
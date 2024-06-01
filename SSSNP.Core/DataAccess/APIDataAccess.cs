using CSDS.Core.Framework.Common.Filtering;
using CSDS.Core.Framework.DataAccess.BaseClasses;
using CSDS.Core.Framework.DataMapper;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.DataAccess
{
    public class APIDataAccess : DataAccessBase
    {

        public APIDataAccess(string dataPartition) : base(dataPartition)
        {
            // do nothing
        }
        public APIDataAccess(string dataPartition, IsolationLevel isolationLevel) : base(dataPartition, isolationLevel)
        {
            // do nothing
        }

        public BeneficiaryAPI GetBeneficiary(int intID) 
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiary");
            return ExecuteOperation<BeneficiaryAPI>(tm, CreateParameter("intID", intID));
        }
        public BeneficiaryAPI GetBeneficiaryLoc(int intID)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiaryLoc");
            return ExecuteOperation<BeneficiaryAPI>(tm, CreateParameter("intID", intID));
        }

        public List<BeneficiaryAPI> GetBeneficiaries()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiaries");
            return ExecuteOperation<BeneficiaryAPI>(tm, (IFilterCriteria)null);
        }
        public List<BeneficiaryAPI> GetBeneficiariesLoc()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiariesLoc");
            return ExecuteOperation<BeneficiaryAPI>(tm, (IFilterCriteria)null);
        }

        public int AddBeneficiary(BeneficiaryAPI beneficiaryAPI)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "AddBeneficiary");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<BeneficiaryAPI>(tm, beneficiaryAPI, returnValue);

            if (returnValue.Value == DBNull.Value) return -2;

            return (int)returnValue.Value;
        }

        public int AddNomineeInfo(NomineeDto nominee)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "AddNomineeInfo");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<NomineeDto>(tm, nominee, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }

        public int AddBeneficiaryLoc(BeneficiaryAPI beneficiaryAPI)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "AddBeneficiaryLoc");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<BeneficiaryAPI>(tm, beneficiaryAPI, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        
        public int CheckHouseHoldNumber(string houseHoldNumber)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "CheckHouseHoldNumber");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("houseHoldNumber", houseHoldNumber), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int CheckHouseHoldNumberLoc(string houseHoldNumber)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "CheckHouseHoldNumberLoc");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, CreateParameter("houseHoldNumber", houseHoldNumber), returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }

        public int CountBeneficiary()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "CountBeneficiary");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }
        public int CountBeneficiaryLoc()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "CountBeneficiaryLoc");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery(tm, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
        }

        public int AddAPITest(APITest apiTest)
        {

            ITypemapper tm = new Typemapper(TMapperAPIContext.APITest, "AddAPITest");
            IDbDataParameter returnValue = CreateOutputParameter("@ReturnValue");

            this.ExecuteNonQuery<APITest>(tm, apiTest, returnValue);

            if (returnValue.Value == DBNull.Value) return -1;

            return (int)returnValue.Value;
            
          
        }
        public List<APITest> GetAPITest()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.APITest, "GetAPITest");
            return ExecuteOperation<APITest>(tm, (IFilterCriteria)null);
        }

        public List<BeneficiaryAPI> GetBeneficiariesWithoutPhotoPath()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiariesWithoutPhotoPath");
            return ExecuteOperation<BeneficiaryAPI>(tm, (IFilterCriteria)null);
        }
        public List<BeneficiaryAPI> GetBeneficiariesWithoutPhotoPathLoc()
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiariesWithoutPhotoPathLoc");
            return ExecuteOperation<BeneficiaryAPI>(tm, (IFilterCriteria)null);
        }
        
        public List<BeneficiaryAPI> GetBeneficiaries(int countieId)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiariesByCountiId");
            return ExecuteOperation<BeneficiaryAPI>(tm, (IFilterCriteria)null, CreateParameter("countieId", countieId));
        }
        public List<BeneficiaryAPI> GetBeneficiariesLoc(int countieId)
        {
            ITypemapper tm = new Typemapper(TMapperAPIContext.BeneficiaryAPI, "GetBeneficiariesByCountiIdLoc");
            return ExecuteOperation<BeneficiaryAPI>(tm, (IFilterCriteria)null, CreateParameter("countieId", countieId));
        }


    }
}

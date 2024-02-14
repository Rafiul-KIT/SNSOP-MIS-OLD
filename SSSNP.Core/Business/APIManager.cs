using SSSNP_SNSOP.Core.DataAccess;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Business
{
    
    public class APIManager: IAPIManager
    {
        private readonly string context = "APIManager";

        public int CheckHouseHoldNumber(string houseHoldNumber)
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.CheckHouseHoldNumber(houseHoldNumber);
                ds.AcceptChanges();
            }

            return result;
        }
        public int CheckHouseHoldNumberFAO(string houseHoldNumber)
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess("APIManagerFAO"))
            {
                result = ds.CheckHouseHoldNumber(houseHoldNumber);
                ds.AcceptChanges();
            }

            return result;
        }

        public int CheckHouseHoldNumberSNSOP(string houseHoldNumber)
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess("APIManagerSNSOP"))
            {
                result = ds.CheckHouseHoldNumber(houseHoldNumber);
                ds.AcceptChanges();
            }

            return result;
        }

        public int CheckHouseHoldNumberLoc(string houseHoldNumber)
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.CheckHouseHoldNumberLoc(houseHoldNumber);
                ds.AcceptChanges();
            }

            return result;
        }

        public List<BeneficiaryAPI> GetBeneficiaries()
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiaries();
                ds.AcceptChanges();
            }

            return result;
        }
        public List<BeneficiaryAPI> GetBeneficiariesLoc()
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiariesLoc();
                ds.AcceptChanges();
            }

            return result;
        }

        public BeneficiaryAPI GetBeneficiary(int intID)
        {
            BeneficiaryAPI result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiary(intID);
                ds.AcceptChanges();
            }

            return result;
        }
        public BeneficiaryAPI GetBeneficiaryLoc(int intID)
        {
            BeneficiaryAPI result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiaryLoc(intID);
                ds.AcceptChanges();
            }

            return result;
        }

        public int AddBeneficiary(BeneficiaryAPI beneficiaryAPI)
        {
            int result;

            int checkBenId = 0;

            checkBenId = CheckHouseHoldNumber(beneficiaryAPI.household_number);
            if (checkBenId > 0) return -1;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.AddBeneficiary(beneficiaryAPI);
                ds.AcceptChanges();
            }

            return result;

        }
        public int AddBeneficiaryLoc(BeneficiaryAPI beneficiaryAPI)
        {
            int result;

            int checkBenId = 0;

            checkBenId = CheckHouseHoldNumberLoc(beneficiaryAPI.household_number);
            if (checkBenId > 0) return -1;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.AddBeneficiaryLoc(beneficiaryAPI);
                ds.AcceptChanges();
            }

            return result;

        }
        public int AddBeneficiaryFAO(BeneficiaryAPI beneficiaryAPI)
        {
            int result;

            int checkBenId = 0;

            checkBenId = CheckHouseHoldNumberFAO(beneficiaryAPI.household_number);
            if (checkBenId > 0) return -1;

            using (APIDataAccess ds = new APIDataAccess("APIManagerFAO"))
            {
                result = ds.AddBeneficiary(beneficiaryAPI);
                ds.AcceptChanges();
            }

            return result;

        }

        public int AddBeneficiarySNSOP(BeneficiaryAPI beneficiaryAPI)
        {
            int result;

            int checkBenId = 0;

            checkBenId = CheckHouseHoldNumberSNSOP(beneficiaryAPI.household_number);
            if (checkBenId > 0) return -1;

            using (APIDataAccess ds = new APIDataAccess("APIManagerSNSOP"))
            {
                result = ds.AddBeneficiary(beneficiaryAPI);
                ds.AcceptChanges();
            }

            return result;

        }

        public int CountBeneficiary()
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.CountBeneficiary();
                ds.AcceptChanges();
            }

            return result;
        }
        public int CountBeneficiaryLoc()
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.CountBeneficiaryLoc();
                ds.AcceptChanges();
            }

            return result;
        }


        public List<BeneficiaryAPI> GetBeneficiariesWithoutPhotoPath()
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiariesWithoutPhotoPath();
                ds.AcceptChanges();
            }

            return result;
        }
        public List<BeneficiaryAPI> GetBeneficiariesWithoutPhotoPathLoc()
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiariesWithoutPhotoPathLoc();
                ds.AcceptChanges();
            }

            return result;
        }
        public List<BeneficiaryAPI> GetBeneficiariesFAOWithoutPhotoPath()
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess("APIManagerFAO"))
            {
                result = ds.GetBeneficiariesWithoutPhotoPath();
                ds.AcceptChanges();
            }

            return result;
        }

        public List<BeneficiaryAPI> GetBeneficiariesSNSOPWithoutPhotoPath()
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess("APIManagerSNSOP"))
            {
                result = ds.GetBeneficiariesWithoutPhotoPath();
                ds.AcceptChanges();
            }

            return result;
        }


        public List<BeneficiaryAPI> GetBeneficiaries(int countieId)
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiaries(countieId);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<BeneficiaryAPI> GetBeneficiariesLoc(int countieId)
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetBeneficiariesLoc(countieId);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<BeneficiaryAPI> GetBeneficiariesFAO(int countieId)
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess("APIManagerFAO"))
            {
                result = ds.GetBeneficiaries(countieId);
                ds.AcceptChanges();
            }

            return result;
        }
        
        public List<BeneficiaryAPI> GetBeneficiariesSNSOP(int countieId)
        {
            List<BeneficiaryAPI> result;

            using (APIDataAccess ds = new APIDataAccess("APIManagerSNSOP"))
            {
                result = ds.GetBeneficiaries(countieId);
                ds.AcceptChanges();
            }

            return result;
        }

        #region Test Methods
        public int AddAPITest(APITest apiTest)
        {
            int result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.AddAPITest(apiTest);
                ds.AcceptChanges();
            }

            return result;
        }
        public List<APITest> GetAPITest()
        {
            List<APITest> result;

            using (APIDataAccess ds = new APIDataAccess(this.context))
            {
                result = ds.GetAPITest();
                ds.AcceptChanges();
            }

            return result;
        }
        #endregion

    }
}

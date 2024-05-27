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

        private BeneficiaryAPI GetBeneficiaryData(BeneficiaryAPIExt beneficiaryAPIExt)
        {
            BeneficiaryAPI beneficiaryAPI = new BeneficiaryAPI();

            beneficiaryAPI.id = beneficiaryAPIExt.id;
            beneficiaryAPI.supervisor_id = beneficiaryAPIExt.supervisor_id;
            beneficiaryAPI.partner_code = beneficiaryAPIExt.partner_code;
            beneficiaryAPI.household_number = beneficiaryAPIExt.household_number;
            beneficiaryAPI.beneficiary_type = beneficiaryAPIExt.beneficiary_type;
            beneficiaryAPI.group_number = beneficiaryAPIExt.group_number;
            beneficiaryAPI.member_number = beneficiaryAPIExt.member_number;
            beneficiaryAPI.household_size = beneficiaryAPIExt.household_size;
            beneficiaryAPI.household_name = beneficiaryAPIExt.household_name;
            beneficiaryAPI.age = beneficiaryAPIExt.age;
            beneficiaryAPI.spouse_name = beneficiaryAPIExt.spouse_name;
            beneficiaryAPI.id_number = beneficiaryAPIExt.id_number;
            beneficiaryAPI.phone_number = beneficiaryAPIExt.phone_number;
            beneficiaryAPI.gender = beneficiaryAPIExt.gender;
            beneficiaryAPI.female_dependants = beneficiaryAPIExt.female_dependants;
            beneficiaryAPI.male_dependants = beneficiaryAPIExt.male_dependants;
            beneficiaryAPI.zero_five = beneficiaryAPIExt.zero_five;
            beneficiaryAPI.six_eighteen = beneficiaryAPIExt.six_eighteen;
            beneficiaryAPI.nineteen_forty_five = beneficiaryAPIExt.nineteen_forty_five;
            beneficiaryAPI.forty_six_sixty_five = beneficiaryAPIExt.forty_six_sixty_five;
            beneficiaryAPI.sixty_six = beneficiaryAPIExt.sixty_six;
            beneficiaryAPI.income_household = beneficiaryAPIExt.income_household;
            beneficiaryAPI.average_household = beneficiaryAPIExt.average_household;
            beneficiaryAPI.state = beneficiaryAPIExt.state;
            beneficiaryAPI.county = beneficiaryAPIExt.county;
            beneficiaryAPI.payam = beneficiaryAPIExt.payam;
            beneficiaryAPI.boma = beneficiaryAPIExt.boma;
            beneficiaryAPI.latitude = beneficiaryAPIExt.latitude;
            beneficiaryAPI.longitude = beneficiaryAPIExt.longitude;
            beneficiaryAPI.legal_status = beneficiaryAPIExt.legal_status;
            beneficiaryAPI.photo_url = beneficiaryAPIExt.photo_url;
            beneficiaryAPI.status = beneficiaryAPIExt.status;
            beneficiaryAPI.created_at = beneficiaryAPIExt.created_at;
            beneficiaryAPI.updated_at = beneficiaryAPIExt.updated_at;
            beneficiaryAPI.alternate_number = beneficiaryAPIExt.alternate_number;
            beneficiaryAPI.selection_criteria = beneficiaryAPIExt.selection_criteria;
            beneficiaryAPI.selection_reason = beneficiaryAPIExt.selection_reason;
            beneficiaryAPI.liTemplate = beneficiaryAPIExt.liTemplate;
            beneficiaryAPI.lmTemplate = beneficiaryAPIExt.lmTemplate;
            beneficiaryAPI.lrTemplate = beneficiaryAPIExt.lrTemplate;
            beneficiaryAPI.lsTemplate = beneficiaryAPIExt.lsTemplate;
            beneficiaryAPI.ltTemplate = beneficiaryAPIExt.ltTemplate;
            beneficiaryAPI.riTemplate = beneficiaryAPIExt.riTemplate;
            beneficiaryAPI.rmTemplate = beneficiaryAPIExt.rmTemplate;
            beneficiaryAPI.rrTemplate = beneficiaryAPIExt.rrTemplate;
            beneficiaryAPI.rsTemplate = beneficiaryAPIExt.rsTemplate;
            beneficiaryAPI.rtTemplate = beneficiaryAPIExt.rtTemplate;
            beneficiaryAPI.household_photo_url = beneficiaryAPIExt.household_photo_url;
            beneficiaryAPI.alt1_photo_url = beneficiaryAPIExt.alt1_photo_url;
            beneficiaryAPI.alt2_photo_url = beneficiaryAPIExt.alt2_photo_url;
            beneficiaryAPI.alt1Name = beneficiaryAPIExt.alt1Name;
            beneficiaryAPI.alt1Gender = beneficiaryAPIExt.alt1Gender;
            beneficiaryAPI.alt1Age = beneficiaryAPIExt.alt1Age;
            beneficiaryAPI.alt1Nid = beneficiaryAPIExt.alt1Nid;
            beneficiaryAPI.alt1Mobile = beneficiaryAPIExt.alt1Mobile;
            beneficiaryAPI.alt2Name = beneficiaryAPIExt.alt2Name;
            beneficiaryAPI.alt2Gender = beneficiaryAPIExt.alt2Gender;
            beneficiaryAPI.alt2Age = beneficiaryAPIExt.alt2Age;
            beneficiaryAPI.alt2Nid = beneficiaryAPIExt.alt2Nid;
            beneficiaryAPI.alt2Mobile = beneficiaryAPIExt.alt2Mobile;
            return beneficiaryAPI;
        }

        public int AddBeneficiarySNSOP(BeneficiaryAPIExt beneficiaryAPIExt)
        {
            int result;

            int checkBenId = 0;

            checkBenId = CheckHouseHoldNumberSNSOP(beneficiaryAPIExt.household_number);
            if (checkBenId > 0) return -1;

            BeneficiaryAPI beneficiaryAPI = GetBeneficiaryData(beneficiaryAPIExt);

            using (APIDataAccess ds = new APIDataAccess("APIManagerSNSOP"))
            {
                result = ds.AddBeneficiary(beneficiaryAPI);
                ds.AcceptChanges();
            }

            AddNomineeInfo(beneficiaryAPIExt.nomineeList);

            return result;

        }

        public void AddNomineeInfo(List<NomineeDto> nomList)
        {
            using (APIDataAccess ds = new APIDataAccess("APIManagerSNSOP"))
            {
                foreach(var obj in nomList)
                {
                    var result = ds.AddNomineeInfo(obj);
                    ds.AcceptChanges();
                }
            }
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.Interfaces
{
    public interface IAPIManager
    {
        List<BeneficiaryAPI> GetBeneficiaries();
        List<BeneficiaryAPI> GetBeneficiariesLoc();

        BeneficiaryAPI GetBeneficiary(int intID);
        BeneficiaryAPI GetBeneficiaryLoc(int intID);

        int AddBeneficiary(BeneficiaryAPI beneficiaryAPI);
        int AddBeneficiaryLoc(BeneficiaryAPI beneficiaryAPI);
        int AddBeneficiaryFAO(BeneficiaryAPI beneficiaryAPI);
        int AddBeneficiarySNSOP(BeneficiaryAPIExt beneficiaryAPI);

        int CheckHouseHoldNumber(string houseHoldNumber);
        int CheckHouseHoldNumberLoc(string houseHoldNumber);

        int CountBeneficiary();
        int CountBeneficiaryLoc();

        List<BeneficiaryAPI> GetBeneficiariesWithoutPhotoPath();
        List<BeneficiaryAPI> GetBeneficiariesWithoutPhotoPathLoc();
        List<BeneficiaryAPI> GetBeneficiariesFAOWithoutPhotoPath();
        List<BeneficiaryAPI> GetBeneficiariesSNSOPWithoutPhotoPath();

        List<BeneficiaryAPI> GetBeneficiaries(int countieId);
        List<BeneficiaryAPI> GetBeneficiariesLoc(int countieId);
        List<BeneficiaryAPI> GetBeneficiariesFAO(int countieId);
        List<BeneficiaryAPI> GetBeneficiariesSNSOP(int countieId);


        #region Test Methods
        int AddAPITest(APITest apiTest);
        List<APITest> GetAPITest();
        #endregion

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SSSNP_SNSOP.API.AuthorizationFilter;
using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SSSNP_SNSOP.API;
using CSDS.Core.Framework.Configuration;

namespace SSSNP_SNSOP.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
       
        private readonly ILogger<BeneficiaryController> _logger;

        public BeneficiaryController(ILogger<BeneficiaryController> logger)
        {
            _logger = logger;
        }

        [Route("getBeneficiary/{intID}")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public BeneficiaryAPI GetBeneficiary([FromRoute] int intID)
        {
            
                //int param_IntID = 0;
                //int.TryParse(intID, out param_IntID);

                BeneficiaryAPI beneficiary = APICoreContext.APIManager.GetBeneficiary(intID);

                return beneficiary;

        }
        [Route("getBeneficiaryLoc/{intID}")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public BeneficiaryAPI GetBeneficiaryLoc([FromRoute] int intID)
        {

            //int param_IntID = 0;
            //int.TryParse(intID, out param_IntID);

            BeneficiaryAPI beneficiary = APICoreContext.APIManager.GetBeneficiaryLoc(intID);

            return beneficiary;

        }


        [Route("getBeneficiaries")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> GetBeneficiaries()
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiaries();
            return beneficiaries.ToArray();

        }
        [Route("getBeneficiariesLoc")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> GetBeneficiariesLoc()
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesLoc();
            return beneficiaries.ToArray();

        }


        [Route("getBeneficiariesWithoutPhotoPath")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesWithoutPhotoPath()
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesWithoutPhotoPath();
            return beneficiaries.ToArray();

        }

        [Route("getBeneficiariesWithoutPhotoPathLoc")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesWithoutPhotoPathLoc()
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesWithoutPhotoPathLoc();
            return beneficiaries.ToArray();

        }

        [Route("getBeneficiariesWithoutPhotoPath/{countieId}")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesWithoutPhotoPath([FromRoute] int countieId)
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiaries(countieId);
            return beneficiaries.ToArray();


        }

        [Route("getBeneficiariesWithoutPhotoPathLoc/{countieId}")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesWithoutPhotoPathLoc([FromRoute] int countieId)
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesLoc(countieId);
            return beneficiaries.ToArray();


        }


        [Route("addBeneficiary")]
        [HttpPost]
        [AuthorizationAPIFilter]
        public APIResultObject AddBeneficiary([FromBody] BeneficiaryAPI beneficiaryAPI)
        {
            APIResultObject result = new APIResultObject();

            try
            {
                // Replace Beneficiary Type Value
                string _beneficiaryType = beneficiaryAPI.beneficiary_type == "General Group" ? "Group" : beneficiaryAPI.beneficiary_type;
                beneficiaryAPI.beneficiary_type = _beneficiaryType;
          
                beneficiaryAPI.updated_at = beneficiaryAPI.updated_at.ConvertDateIndia();
                beneficiaryAPI.created_at = beneficiaryAPI.created_at.ConvertDateIndia();

                int returnValue = APICoreContext.APIManager.AddBeneficiary(beneficiaryAPI);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue , householdNumber = beneficiaryAPI.household_number};


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber="" };
            }

        }

        [Route("addBeneficiaryLoc")]
        [HttpPost]
        [AuthorizationAPIFilter]
        public APIResultObject AddBeneficiaryLoc([FromBody] BeneficiaryAPI beneficiaryAPI)
        {
            APIResultObject result = new APIResultObject();

            try
            {
                // Replace Beneficiary Type Value
                string _beneficiaryType = beneficiaryAPI.beneficiary_type == "General Group" ? "Group" : beneficiaryAPI.beneficiary_type;
                beneficiaryAPI.beneficiary_type = _beneficiaryType;

                beneficiaryAPI.updated_at = beneficiaryAPI.updated_at.ConvertDateIndia();
                beneficiaryAPI.created_at = beneficiaryAPI.created_at.ConvertDateIndia();

                int returnValue = APICoreContext.APIManager.AddBeneficiaryLoc(beneficiaryAPI);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue, householdNumber = beneficiaryAPI.household_number };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }


        [Route("checkHouseholdNumber/{householdNumber}")]
        [HttpGet]
        [AuthorizationAPIFilter]
        public APIResultObject CheckHouseholdNumber([FromRoute] string householdNumber)
        {
            APIResultObject result = new APIResultObject();

            try
            {
                
                int returnValue = APICoreContext.APIManager.CheckHouseHoldNumber (householdNumber);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue, householdNumber = householdNumber };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }

        [Route("checkHouseholdNumberLoc/{householdNumber}")]
        [HttpGet]
        [AuthorizationAPIFilter]
        public APIResultObject CheckHouseholdNumberLoc([FromRoute] string householdNumber)
        {
            APIResultObject result = new APIResultObject();

            try
            {

                int returnValue = APICoreContext.APIManager.CheckHouseHoldNumberLoc(householdNumber);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue, householdNumber = householdNumber };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }

        [Route("statusHealth")]
        [HttpGet]
        public APIResultObject StatusHealth()
        {
            APIResultObject result = new APIResultObject();

            try
            {

                int returnValue = APICoreContext.APIManager.CountBeneficiary ();
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue, householdNumber = "" };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }

        [Route("statusHealthFAO")]
        [HttpGet]
        public APIResultObject StatusHealthFAO()
        {
            APIResultObject result = new APIResultObject();

            try
            {
                var valueDP = DataPartitionManagers.Instance.GetDataPartition("APIManagerFAO");
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = 1, householdNumber = valueDP.ConnectionString };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }

        [Route("statusHealthSNSOP")]
        [HttpGet]
        public APIResultObject StatusHealthSNSOP()
        {
            APIResultObject result = new APIResultObject();

            try
            {
                var valueDP = DataPartitionManagers.Instance.GetDataPartition("APIManagerSNSOP");
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = 1, householdNumber = valueDP.ConnectionString };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }


        [Route("addAPITest")]
        [HttpPost]
        public APIResultObject AddAPITest([FromBody] APITest apiTest)
        {
            APIResultObject result = new APIResultObject();

            try
            {
                int returnValue = APICoreContext.APIManager.AddAPITest(apiTest);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue };
                
            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1 };
            }

        }
        
        [HttpGet]
        [Route("GetAPITest")]
        public IEnumerable<APITest> GetAPITest()
        {
            List<APITest> result = APICoreContext.APIManager.GetAPITest ();
            return result.ToArray();

        }


        [Route("addBeneficiaryFAO")]
        [HttpPost]
        [AuthorizationAPIFilter]
        public APIResultObject AddBeneficiaryFAO([FromBody] BeneficiaryAPI beneficiaryAPI)
        {
            APIResultObject result = new APIResultObject();

            try
            {
                // Replace Beneficiary Type Value
                string _beneficiaryType = beneficiaryAPI.beneficiary_type == "General Group" ? "Group" : beneficiaryAPI.beneficiary_type;
                beneficiaryAPI.beneficiary_type = _beneficiaryType;

                beneficiaryAPI.updated_at = beneficiaryAPI.updated_at.ConvertDateIndia();
                beneficiaryAPI.created_at = beneficiaryAPI.created_at.ConvertDateIndia();

                int returnValue = APICoreContext.APIManager.AddBeneficiaryFAO(beneficiaryAPI);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue, householdNumber = beneficiaryAPI.household_number };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }

        [Route("addBeneficiarySNSOP")]
        [HttpPost]
        [AuthorizationAPIFilter]
        public APIResultObject AddBeneficiarySNSOP([FromBody] BeneficiaryAPIExt beneficiaryAPI)
        {
            APIResultObject result = new APIResultObject();
           

            try
            {
                // Replace Beneficiary Type Value
                string _beneficiaryType = beneficiaryAPI.beneficiary_type == "General Group" ? "Group" : beneficiaryAPI.beneficiary_type;
                beneficiaryAPI.beneficiary_type = _beneficiaryType;

                beneficiaryAPI.updated_at = beneficiaryAPI.updated_at.ConvertDateIndia();
                beneficiaryAPI.created_at = beneficiaryAPI.created_at.ConvertDateIndia();

                int returnValue = APICoreContext.APIManager.AddBeneficiarySNSOP(beneficiaryAPI);
                return new APIResultObject() { error = false, errorCode = 0, errorMessage = "", returnId = returnValue, householdNumber = beneficiaryAPI.household_number };


            }
            catch (Exception ex)
            {

                return new APIResultObject() { error = true, errorCode = 500, errorMessage = ex.Message, returnId = -1, householdNumber = "" };
            }

        }


        [Route("getBeneficiariesFAOWithoutPhotoPath")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesFAOWithoutPhotoPath()
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesFAOWithoutPhotoPath();
            return beneficiaries.ToArray();

        }

        [Route("getBeneficiariesSNSOPWithoutPhotoPath")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesSNSOPWithoutPhotoPath()
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesSNSOPWithoutPhotoPath();
            return beneficiaries.ToArray();
        }


        [Route("getBeneficiariesFAOWithoutPhotoPath/{countieId}")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesFAOWithoutPhotoPath([FromRoute] int countieId)
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesFAO(countieId);
            return beneficiaries.ToArray();
        }

        [Route("getBeneficiariesSNSOPWithoutPhotoPath/{countieId}")]
        [HttpGet]
        [Produces("application/json")]
        [AuthorizationAPIFilter]
        public IEnumerable<BeneficiaryAPI> getBeneficiariesSNSOPWithoutPhotoPath([FromRoute] int countieId)
        {

            List<BeneficiaryAPI> beneficiaries = APICoreContext.APIManager.GetBeneficiariesSNSOP(countieId);
            return beneficiaries.ToArray();
        }
    }
}

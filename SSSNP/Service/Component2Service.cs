using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using SSSNP_SNSOP.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace SSSNP.Service
{
    public class Component2Service
    {
        private Component2Repository _component2Repository;
        public Component2Service()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            _component2Repository = new Component2Repository(connectionString);
        }
        public List<ValidHouseholdNumberModel> GetValidHouseholdNumbers()
        {
            return _component2Repository.GetValidHouseholdNumbers();
        }
        public ResponseModel SaveTrainingAndAttendanceTracking(List<TrainingAndAttendanceModel> request)
        {
            var response = new ResponseModel();
            var errorMessages = new List<string>(); // To store validation error messages

            try
            {
                // Fetch valid household numbers from the repository
                var validHouseholdNumbers = _component2Repository.GetValidHouseholdNumbers();

                foreach (var obj in request)
                {
                    // Pass valid household numbers to IsValidData
                    var validationResult = IsValidData(obj, validHouseholdNumbers); // Get validation result

                    if (!validationResult.isValid) // If not valid, store the error message and skip saving
                    {
                        errorMessages.Add(validationResult.errorMessage);
                        continue;
                    }

                    // Save the valid record
                    response = _component2Repository.SaveTrainingAndAttendanceTracking(obj);
                    
                    if(!response.IsSuccess) errorMessages.Add(response.Message);
                }

                // If there are any validation errors, return them in the response
                if (errorMessages.Count > 0)
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("; ", errorMessages); // Join error messages as a single string
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "All records saved successfully.";
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when saving Training and Attendance Tracking data. Error: " + ex.ToString());
            }
        }


        public (bool isValid, string errorMessage) IsValidData(TrainingAndAttendanceModel obj, List<ValidHouseholdNumberModel> validHouseholdNumbers)
        {
            // Predefined valid course codes
            var validCourseCodes = new List<string> { "FINLIT", "LIVETECH", "LIFESKILL", "SAVINGS" };

            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is required.");
            }

            // Check if the household number is valid
            if (!validHouseholdNumbers.Any(hh => hh.HouseholdNumber.Equals(obj.HouseholdNumber?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "Household Number is not valid.");
            }

            if (string.IsNullOrWhiteSpace(obj?.CourseCode?.Trim()))
            {
                return (false, "Course Code is required.");
            }

            // Validate CourseCode
            if (!validCourseCodes.Contains(obj.CourseCode.Trim().ToUpper()))
            {
                return (false, $"Course Code '{obj.CourseCode}' is not valid. Valid values are: {string.Join(", ", validCourseCodes)}.");
            }

            if (obj?.CourseDuration <= 0)
            {
                return (false, "Course Duration must be a positive integer.");
            }

            if (obj?.ClassesAttended > obj?.ClassesHeld)
            {
                return (false, "Classes Attended cannot exceed Classes Held.");
            }

            // Add more validation as needed
            return (true, string.Empty);
        }






        public ResponseModel SaveLivelihoodGrantDisbursement(List<LivelihoodGrantDisbursementModel> request)
        {
            var response = new ResponseModel();
            var errorMessages = new List<string>(); // To store validation error messages

            try
            {
                // Fetch valid household numbers from the repository
                var validHouseholdNumbers = _component2Repository.GetValidHouseholdNumbers();

                foreach (var obj in request)
                {
                    // Pass valid household numbers to IsValidData
                    var validationResult = IsValidData(obj, validHouseholdNumbers); // Get validation result

                    if (!validationResult.isValid) // If not valid, store the error message and skip saving
                    {
                        errorMessages.Add(validationResult.errorMessage);
                        continue;
                    }

                    // Save the valid record
                    response = _component2Repository.SaveLivelihoodGrantDisbursement(obj);
                }

                // If there are any validation errors, return them in the response
                if (errorMessages.Count > 0)
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("; ", errorMessages); // Join error messages as a single string
                }
                else
                {
                    response.IsSuccess = true;
                    response.Message = "All records saved successfully.";
                }

                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when saving Livelihood Grant Disbursement data. Error: " + ex.ToString());
            }
        }

        public (bool isValid, string errorMessage) IsValidData(LivelihoodGrantDisbursementModel obj, List<ValidHouseholdNumberModel> validHouseholdNumbers)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is required.");
            }

            // Check if the household number is valid
            if (!validHouseholdNumbers.Any(hh => hh.HouseholdNumber.Equals(obj.HouseholdNumber?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "Household Number is not valid.");
            }

            // Check for negative PaymentAmount
            if (obj.PaymentAmount < 0)
            {
                return (false, "Payment Amount cannot be negative.");
            }

            if (string.IsNullOrWhiteSpace(obj.PaymentCycle))
            {
                return (false, "Payment Cycle is required.");
            }

            // Validate the PaymentCycle (for example, if you want to ensure valid cycles)
            var validPaymentCycles = new List<string> { "Installment One", "Installment Two" };
            if (!validPaymentCycles.Contains(obj.PaymentCycle.Trim()))
            {
                return (false, $"Payment Cycle '{obj.PaymentCycle}' is not valid. Valid values are: {string.Join(", ", validPaymentCycles)}.");
            }

            // Add more validation if needed
            return (true, string.Empty);
        }


        public ResponseModel SaveHouseholdGroups(List<HouseholdGroupModel> groups)
        {
            var response = new ResponseModel();
            int insertCount = 0;  
            int skipCount = 0;   

            try
            {
                foreach (var group in groups)
                {

                    bool isValid = IsValidData(group).isValid;
                    if (!isValid)
                    {
                        skipCount++;
                        continue;
                    }

                    response = _component2Repository.SaveHouseholdGroup(group);

                    if (response.IsSuccess)
                    {
                        insertCount++;  
                    }
                }

                response.Message = $"{insertCount} records inserted/updated successfully.";
                if (skipCount > 0)
                {
                    response.Message += $" {skipCount} records were skipped due to incomplete data.";
                }

                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error saving Household Group data: " + ex.Message;
                return response;
            }
        }


        public (bool isValid, string errorMessage) IsValidData(HouseholdGroupModel group)
        {
            // Check for missing or invalid Group Name
            if (string.IsNullOrWhiteSpace(group?.GroupName?.Trim()))
            {
                return (false, "Group Name is not valid.");
            }

            // Check for missing Latitude
            if (group.Latitude == null)
            {
                return (false, "Latitude is missing.");
            }

            // Check for missing Longitude
            if (group.Longitude == null)
            {
                return (false, "Longitude is missing.");
            }

            // If all validations pass
            return (true, "Valid");
        }

        public Dictionary<string, int> GetExistingGroupNames()
        {
            return _component2Repository.GetExistingGroupNames();
        }

        public ResponseModel SaveBusinessPlanDevelopment(List<BusinessPlanDevelopmentModel> request)
        {
            var response = new ResponseModel();
            try
            {
                foreach (var obj in request)
                {
                    if (!IsValidData(obj)) continue;
                    response = _component2Repository.SaveBusinessPlanDevelopment(obj);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when saving Business Plan Development data. Error: " + ex.ToString());
            }
        }

        private bool IsValidData(BusinessPlanDevelopmentModel obj)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return false;
            }
            if (obj?.BusinessPlanStatus == null)
            {
                return false;
            }
            if (obj?.BusinessType == null)
            {
                return false;
            }
            return true;
        }

        public Dictionary<string, int> GetExistingHouseholdNumbers()
        {
            try
            {
                return _component2Repository.GetExistingHouseholdNumbers();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving existing household numbers: " + ex.Message);
            }
        }

        public ResponseModel SaveBeneficiaries(List<BeneficiaryRandomizationAndEnrollmentModel> beneficiaries)
        {
            var response = new ResponseModel();
            int insertCount = 0;  
            int skipCount = 0;  

            try
            {
                foreach (var beneficiary in beneficiaries)
                {

                    bool isValid = IsValidData(beneficiary).isValid;
                    if (!isValid)
                    {
                        skipCount++;
                        continue;
                    }


                    var saveResponse = _component2Repository.SaveBeneficiary(beneficiary);

                    if (saveResponse.IsSuccess)
                    {
                        insertCount++;  
                    }
                }

                
                response.IsSuccess = true;
                response.Message = $"{insertCount} records inserted/updated successfully.";
                if (skipCount > 0)
                {
                    response.Message += $" {skipCount} records were skipped due to incomplete or invalid data.";
                }

                return response;  
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error saving Beneficiary data: " + ex.Message;
                return response;  
            }
        }


        public ResponseModel SaveFinancialServiceLinkage(List<FinancialServiceLinkageTrackingModel> request)
        {
            var response = new ResponseModel();
            try
            {
                foreach (var obj in request)
                {
                    if (!IsValidData(obj)) continue;
                    response = _component2Repository.SaveFinancialServiceLinkage(obj);
                }
                return response;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error saving Beneficiary data: " + ex.Message;
                return response;
            }
        }

        public (bool isValid, string errorMessage) IsValidData(BeneficiaryRandomizationAndEnrollmentModel beneficiary)
        {
            // Check for missing Household Number
            if (string.IsNullOrWhiteSpace(beneficiary?.HouseholdNumber))
            {
                return (false, "Household Number is not valid");
            }

            // Check for missing InterestedInParticipating value
            if (beneficiary.InterestedInParticipating == null)
            {
                return (false, "Interested In Participating is not valid");
            }

            // Check for missing SurvivesTargeting value
            if (beneficiary.SurvivesTargeting == null)
            {
                return (false, "Survives Targeting is not valid");
            }

            // Check for missing TreatmentArm value
            if (beneficiary.TreatmentArm == null)
            {
                return (false, "Treatment Arm is not valid");
            }

            // Check for missing HHEnrollmentStatus value
            if (beneficiary.HHEnrollmentStatus == null)
            {
                return (false, "Enrollment Status is not valid");
            }

            // All validations passed
            return (true, "Valid");
        }




        private bool IsValidData(FinancialServiceLinkageTrackingModel obj)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return false;
            }
            if (obj?.ConnectedFinancialServices == null)
            {
                return false;
            }
            return true;
        }
    }
}
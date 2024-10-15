using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using SSSNP_SNSOP.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
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

        public HashSet<string> GetValidHouseholdNumberValues()
        {
            return _component2Repository.GetValidHouseholdNumberValues();
        }

        public HashSet<int> GetValidGroupId()
        {
            return _component2Repository.GetValidGroupId();
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

                    if (!response.IsSuccess) errorMessages.Add(response.Message);
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
            var validCourseCodes = new List<string> { "FINLIT", "LIVETECH", "LIFESKILL", "SAVINGS" };

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
            if (obj.PaymentAmount <= 0)
            {
                return (false, "Payment Amount cannot be zero or negative.");
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

            if (!obj.PaymentDate.HasValue)
            {
                return (false, "Payment Date is required.");
            }

            // Ensure the date is not in the future and within a reasonable range (for example, between 1900 and today)
            var minDate = new DateTime(1900, 1, 1);
            var maxDate = DateTime.Today;

            if (obj.PaymentDate.Value < minDate || obj.PaymentDate.Value > maxDate)
            {
                return (false, $"Payment Date must be between {minDate.ToString("MM-dd-yyyy")} and {maxDate.ToString("MM-dd-yyyy")}.");
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

        // Validate household group data
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

        public ResponseModel SaveBusinessPlanDevelopment(List<BusinessPlanDevelopmentModel> request, HashSet<string> validList)
        {
            var response = new ResponseModel();
            var errorMessages = new List<string>();
            try
            {
                foreach (var obj in request)
                {
                    var validationResult = (IsValidData(obj, validList));
                    if (!validationResult.isValid)
                    {
                        errorMessages.Add(validationResult.errorMessage);
                        continue;
                    }
                    response = _component2Repository.SaveBusinessPlanDevelopment(obj);
                }

                if (errorMessages.Count > 0)
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("\n", errorMessages);
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
                throw new Exception("Error when saving Business Plan Development data. Error: " + ex.ToString());
            }
        }

        public (bool isValid, string errorMessage) IsValidData(BusinessPlanDevelopmentModel obj, HashSet<string> validList)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is required");
            }
            if (!validList.Contains(obj.HouseholdNumber))
            {
                return (false, "Household Number is not valid");
            }
            if (obj?.BusinessPlanStatus == null)
            {
                return (false, "Business Plan Status is required");
            }
            if (obj?.BusinessType == null)
            {
                return (false, "Business Type is required");
            }
            return (true, null);
        }

        public ResponseModel SaveFinancialServiceLinkage(List<FinancialServiceLinkageTrackingModel> request, HashSet<string> validList)
        {
            var response = new ResponseModel();
            var errorMessages = new List<string>();
            try
            {
                foreach (var obj in request)
                {
                    var validationResult = (IsValidData(obj, validList));
                    if (!validationResult.isValid)
                    {
                        errorMessages.Add(validationResult.errorMessage);
                        continue;
                    }
                    response = _component2Repository.SaveFinancialServiceLinkage(obj);
                }

                if (errorMessages.Count > 0)
                {
                    response.IsSuccess = false;
                    response.Message = string.Join("\n", errorMessages);
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
                throw new Exception("Error when saving Financial Service Linkage Tracking data. Error: " + ex.ToString());
            }
        }

        public (bool isValid, string errorMessage) IsValidData(FinancialServiceLinkageTrackingModel obj, HashSet<string> validList)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is required");
            }
            if (!validList.Contains(obj.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is not valid");
            }
            if (obj?.ConnectedFinancialServices == null)
            {
                return (false, "Connected Financial Service is required");
            }
            return (true, null);
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

            // Fetch valid household numbers as a HashSet for faster lookup
            var validHouseholdNumbers = _component2Repository.GetValidHouseholdNumberValues(); // Assuming it returns HashSet<string>
            var validGroupId = _component2Repository.GetValidGroupId();

            try
            {
                foreach (var beneficiary in beneficiaries)
                {
                    // Pass valid household numbers to IsValidData
                    var validationResult = IsValidData(beneficiary, validHouseholdNumbers,validGroupId); // Pass HashSet to validation
                    if (!validationResult.isValid)
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


        public (bool isValid, string errorMessage) IsValidData(BeneficiaryRandomizationAndEnrollmentModel beneficiary, HashSet<string> validHouseholdNumbers, HashSet<int> validGroupId)
        {
            // Check for missing Household Number
            if (string.IsNullOrWhiteSpace(beneficiary?.HouseholdNumber))
            {
                return (false, "Household Number is required.");
            }

            // Check if the household number is valid using the HashSet for faster lookup
            if (!validHouseholdNumbers.Contains(beneficiary.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is not valid.");
            }

            // Check for missing InterestedInParticipating value
            if (beneficiary.InterestedInParticipating == null)
            {
                return (false, "Interested In Participating is required.");
            }

            // Check for missing SurvivesTargeting value
            if (beneficiary.SurvivesTargeting == null)
            {
                return (false, "Survives Targeting is required.");
            }

            // Check for missing TreatmentArm value
            if (beneficiary.TreatmentArm == null)
            {
                return (false, "Treatment Arm is required.");
            }

            // Check for missing HHEnrollmentStatus value
            if (beneficiary.HHEnrollmentStatus == null)
            {
                return (false, "Enrollment Status is required.");
            }

            if (beneficiary.GroupID == null)
            {
                return (false, "GroupID is required.");
            }

            if (!validGroupId.Contains(beneficiary.GroupID.Value))
            {
                return (false, "GroupID is not valid.");
            }

            if (beneficiary.EnrolledMemberID == null)
            {
                return (false, "EnrolledMemberID is required.");
            }


            // All validations passed
            return (true, "Valid");
        }

        public HashSet<string> GetValidHouseholdNumberValues(int? stateId, int? countyId, int? payamId, int? bomaId)
        {
            try
            {
                // Call the repository method with the passed parameters
                return _component2Repository.GetValidHouseholdNumberValues(stateId, countyId, payamId, bomaId);
            }
            catch (Exception ex)
            {
                // Handle any potential errors here (e.g., logging, throwing)
                Debug.WriteLine($"Error in service method GetValidHouseholdNumberValues: {ex.Message}");
                return new HashSet<string>();
            }
        }

        public List<HouseholdGroupLinkageModel> GetMappedHouseholdNumberValues(int? stateId, int? countyId, int? payamId, int? bomaId)
        {
            try
            {
                // Call the updated repository method with the passed parameters
                return _component2Repository.GetMappedHouseholdNumberWithGroupValues(stateId, countyId, payamId, bomaId);
            }
            catch (Exception ex)
            {
                // Handle any potential errors here (e.g., logging, throwing)
                Debug.WriteLine($"Error in service method GetMappedHouseholdNumberValues: {ex.Message}");
                return new List<HouseholdGroupLinkageModel>(); // Return an empty list in case of error
            }
        }


    }
}
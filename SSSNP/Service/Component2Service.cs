using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using SSSNP_SNSOP.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
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

        public List<string> GetValidHouseholdNumberValues()
        {
            return _component2Repository.GetValidHouseholdNumberValues();
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
            try
            {
                foreach (var group in groups)
                {
                    if (!IsValidData(group)) continue; // Only process valid data
                    response = _component2Repository.SaveHouseholdGroup(group);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Household Group data: " + ex.Message);
            }
        }

        // Validate household group data
        private bool IsValidData(HouseholdGroupModel group)
        {
            if (string.IsNullOrWhiteSpace(group?.GroupName?.Trim()))
            {
                return false; // GroupName is mandatory
            }
            // Add more validation if necessary
            return true;
        }

        public Dictionary<string, int> GetExistingGroupNames()
        {
            return _component2Repository.GetExistingGroupNames();
        }

        public ResponseModel SaveBusinessPlanDevelopment(List<BusinessPlanDevelopmentModel> request, List<string> validList)
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

        public (bool isValid, string errorMessage) IsValidData(BusinessPlanDevelopmentModel obj, List<string> validList)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is required");
            }
            if (!validList.Any(hh => hh.Equals(obj.HouseholdNumber?.Trim(), StringComparison.OrdinalIgnoreCase)))
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

        public ResponseModel SaveFinancialServiceLinkage(List<FinancialServiceLinkageTrackingModel> request, List<string> validList)
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

        public (bool isValid, string errorMessage) IsValidData(FinancialServiceLinkageTrackingModel obj, List<string> validList)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return (false, "Household Number is required");
            }
            if (!validList.Any(hh => hh.Equals(obj.HouseholdNumber?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "Household Number is not valid");
            }
            if (obj?.ConnectedFinancialServices == null)
            {
                return (false, "Connected Financial Service is required");
            }
            return (true, null);
        }
    }
}
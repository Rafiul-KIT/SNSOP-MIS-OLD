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
            try
            {
                foreach (var obj in request)
                {
                    if (!IsValidData(obj)) continue; // Ensure data validation
                    response = _component2Repository.SaveLivelihoodGrantDisbursement(obj); // Save each record
                }
                return response; // Return the response after all records are processed
            }
            catch (Exception ex)
            {
                throw new Exception("Error when saving Livelihood Grant Disbursement data. Error: " + ex.ToString());
            }
        }

        private bool IsValidData(LivelihoodGrantDisbursementModel obj)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return false;
            }
            // TODO: add more validation if needed
            return true;
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
    }
}
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
        public ResponseModel SaveTrainingAndAttendanceTracking(List<TrainingAndAttendanceModel> request)
        {
            var response = new ResponseModel();
            try
            {
                foreach (var obj in request)
                {
                    if (!IsValidData(obj)) continue;
                    response = _component2Repository.SaveTrainingAndAttendanceTracking(obj);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error when saving Training and Attendance Tracking data. Error: " + ex.ToString());
            }
        }

        private bool IsValidData(TrainingAndAttendanceModel obj)
        {
            if(string.IsNullOrWhiteSpace(obj?.HouseholdNumber?.Trim()))
            {
                return false;
            }
            if (string.IsNullOrWhiteSpace(obj?.CourseCode?.Trim()))
            {
                return false;
            }
            // TODO: add more validation if needed
            return true;
        }

        public ResponseModel SaveHouseholdGroups(List<HouseholdGroupModel> groups)
        {
            var response = new ResponseModel();
            int insertCount = 0;  // Counter for successfully inserted records
            int skipCount = 0;    // Counter for skipped records due to invalid data

            try
            {
                foreach (var group in groups)
                {
                    // Validate data before processing
                    if (!IsValidData(group))
                    {
                        skipCount++;  // Increment the skip counter
                        continue;     // Skip this record and continue with the next one
                    }

                    // Process the valid record
                    response = _component2Repository.SaveHouseholdGroup(group);

                    if (response.IsSuccess)
                    {
                        insertCount++;  // Increment the insert counter for successful insertions
                    }
                }

                // Prepare the response message
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
        private bool IsValidData(HouseholdGroupModel group)
        {
            if (string.IsNullOrWhiteSpace(group?.GroupName?.Trim()))
            {
                return false; // GroupName is mandatory
            }
            if (group.Latitude == null || group.Longitude == null)
            {
                return false; // Latitude and Longitude must not be null
            }
            // Add more validation rules as needed
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
            int insertCount = 0;  // Counter for successfully inserted records
            int skipCount = 0;    // Counter for skipped records due to invalid data

            try
            {
                foreach (var beneficiary in beneficiaries)
                {
                    // Validate data before processing
                    if (!IsValidData(beneficiary))
                    {
                        skipCount++;  // Increment the skip counter
                        continue;     // Skip this record and continue with the next one
                    }

                    // Process the valid record
                    var saveResponse = _component2Repository.SaveBeneficiary(beneficiary);

                    if (saveResponse.IsSuccess)
                    {
                        insertCount++;  // Increment the insert counter for successful insertions
                    }
                }

                // Prepare the response message
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

        private bool IsValidData(BeneficiaryRandomizationAndEnrollmentModel beneficiary)
        {
            if (string.IsNullOrWhiteSpace(beneficiary?.HouseholdNumber))
            {
                Debug.WriteLine($"Validation failed: HouseholdNumber is missing for beneficiary.");
                return false; // HouseholdNumber is mandatory
            }

           /* // Booleans must not be null
            if (beneficiary.InterestedInParticipating == null ||
                beneficiary.SurvivesTargeting == null ||
                beneficiary.TreatmentArm == null ||
                beneficiary.HHEnrollmentStatus == null)
            {
                Debug.WriteLine($"Validation failed: Boolean value is missing for beneficiary with HouseholdNumber {beneficiary.HouseholdNumber}.");
                return false;
            }*/

            // Log successful validation
            Debug.WriteLine($"Validation passed for HouseholdNumber {beneficiary.HouseholdNumber}.");

            return true; // If all mandatory conditions pass, return true
        }




    }
}
using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using SSSNP_SNSOP.Core.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        /*public HashSet<int> GetValidGroupId()
        {
            return _component2Repository.GetValidGroupId();
        }*/

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
            var validPaymentCycles = new List<string>
            {
                "Installment 1",
                "Installment 2",
                "Installment 3",
                "Installment 4",
                "Installment 5",
                "Installment 6",
                "Installment 7",
                "Installment 8",
                "Installment 9"
            };
            if (!validPaymentCycles.Contains(obj.PaymentCycle.Trim()))
            {
                return (false, $"Payment Cycle '{obj.PaymentCycle}' is not valid. Valid values are: {string.Join(", ", validPaymentCycles)}.");
            }

            if (!obj.PaymentDate.HasValue)
            {
                return (false, "Payment Date is required.");
            }

            // Ensure the date is not in the future and within a reasonable range (for example, between 1900 and today)
            var minDate = new DateTime(2020, 1, 1);
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

        public void SaveAuditLog<T>(List<T> list, string tableName)
        {
            //_component2Repository.SaveAuditLog(list, tableName);
            RunAuditLogSave(list, tableName);
        }

        public void RunAuditLogSave<T>(List<T> list, string tableName)
        {
            Task t1 = Task.Run(() => _component2Repository.SaveAuditLog(list, tableName));
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
            if (obj?.FinancialServiceConnectionDate == null)
            {
                return (false, "Financial Service Connection Date is required");
            }
            if (obj.DepositAccountOpened == null)
            {
                return (false, "Deposit Account Opened is required");
            }
            if (obj.DepositAccountOpened == true && obj.DepositAccountOpeningDate == null)
            {
                return (false, "Deposit Account Opening Date is required if Deposit Account is opened");
            }
            if (obj.AccountLastActivityDate != null && obj.AccountLastActivityDate > DateTime.Now)
            {
                return (false, "Account Last Activity Date cannot be in the future");
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
            //var validHouseholdNumbers = _component2Repository.GetValidHouseholdNumberValues(); // Assuming it returns HashSet<string>
            //var validGroupId = _component2Repository.GetValidGroupId();

            try
            {
                foreach (var beneficiary in beneficiaries)
                {
                    // Pass valid household numbers to IsValidData
                    var validationResult = IsValidData(beneficiary); // Pass HashSet to validation
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
                Debug.WriteLine(ex.Message);
                return response;
            }
        }


        public (bool isValid, string errorMessage) IsValidData(BeneficiaryRandomizationAndEnrollmentModel beneficiary)
        {
            // Check for missing Household Number
            if (string.IsNullOrWhiteSpace(beneficiary?.HouseholdNumber))
            {
                return (false, "Household Number is required.");
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

            // Treatment Arm should only be specified if HH Enrollment Status is "Yes"
            if (beneficiary.HHEnrollmentStatus == true && beneficiary.TreatmentArm == null)
            {
                return (false, "Treatment Arm is required.");
            }
            else if (beneficiary.HHEnrollmentStatus == false && beneficiary.TreatmentArm != null)
            {
                return (false, "Treatment Arm should only be specified if HH Enrollment Status is 'Yes'.");
            }

            // Check for missing HHEnrollmentStatus value
            if (beneficiary.HHEnrollmentStatus == null)
            {
                return (false, "Enrollment Status is required.");
            }

            // Enrolled Member ID should only be specified if HH Enrollment Status is "Yes"
            if (beneficiary.HHEnrollmentStatus == true && beneficiary.EnrolledMemberID == null)
            {
                return (false, "Enrolled Member ID is required.");
            }
            else if (beneficiary.HHEnrollmentStatus == false && beneficiary.EnrolledMemberID != null)
            {
                return (false, "Enrolled Member ID should only be specified if HH Enrollment Status is 'Yes'.");
            }

            /*if (beneficiary.GroupID == null || beneficiary.GroupID != null)
            {
                return (true, "Valid");
            }*/

            /*if (!validGroupId.Contains(beneficiary.GroupID.Value))
            {
                //return (false, "GroupID is not valid.");
            }

            if (beneficiary.EnrolledMemberID == null)
            {
                //return (false, "Enrolled Member ID is required.");
            }*/
            if (beneficiary.HHEnrollmentStatus == true && !string.IsNullOrWhiteSpace(beneficiary.ReasonForWhyNotEnrolled))
            {
                return (false, "Reason for why not should be empty when HH Enrollment Status is 'Yes'.");
            }
            else if (beneficiary.HHEnrollmentStatus == false && string.IsNullOrWhiteSpace(beneficiary.ReasonForWhyNotEnrolled))
            {
                return (false, "Reason for why not is required when HH Enrollment Status is 'No'.");
            }


            // All validations passed
            return (true, "Valid");
        }

        public ResponseModel SaveHouseholdGroupLinkage(SSSNP_SNSOP.Core.Model.Component2.HouseholdGroupLinkageModel request)
        {
            var response = new ResponseModel();
            var errorMessages = new List<string>(); // To store validation error messages
            try
            {
                foreach (var householdNumber in request?.HouseholdNumbers)
                {
                    request.HouseholdNumber = householdNumber;
                    var validationResult = IsValidData(request);

                    if (!validationResult.isValid) // If not valid, store the error message and skip saving
                    {
                        errorMessages.Add(validationResult.errorMessage);
                        continue;
                    }

                    // Save the valid record
                    response = _component2Repository.SaveHouseholdGroupLinkage(request);
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
                throw new Exception("Error when saving Household Group linkage. Error: " + ex.ToString());
            }
        }


        public (bool isValid, string errorMessage) IsValidData(SSSNP_SNSOP.Core.Model.Component2.HouseholdGroupLinkageModel obj)
        {
            if (string.IsNullOrWhiteSpace(obj?.HouseholdNumber))
            {
                return (false, "Household Number is missing.");
            }
            if (obj?.GroupId <= 0)
            {
                return (false, "Group Id is missing.");
            }
            return (true, string.Empty);
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

        public List<SSSNP_SNSOP.Core.Model.HouseholdGroupLinkageModel> GetMappedHouseholdNumberValues(int? stateId, int? countyId, int? payamId, int? bomaId)
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
                return new List<SSSNP_SNSOP.Core.Model.HouseholdGroupLinkageModel>(); // Return an empty list in case of error
            }
        }

        public Dictionary<string, int> GetExistingCourseCode()
        {
            return _component2Repository.GetExistingCourseCode();
        }

        public (bool isValid, string errorMessage) IsValidData(CourseEntityModel course)
        {
            if (string.IsNullOrWhiteSpace(course?.CourseName?.Trim()))
            {
                return (false, "Course Name is not valid.");
            }

            if (string.IsNullOrWhiteSpace(course?.CourseCode?.Trim()))
            {
                return (false, "Course Code is not valid.");
            }

            if (course.CourseDuration == 0)
            {
                return (false, "Course Duration is missing.");
            }


            return (true, "Valid");
        }

        public ResponseModel SaveCourseEntity(List<CourseEntityModel> Course)
        {
            var response = new ResponseModel();
            int insertCount = 0;
            int skipCount = 0;

            try
            {
                foreach (var course in Course)
                {

                    bool isValid = IsValidData(course).isValid;
                    if (!isValid)
                    {
                        skipCount++;
                        continue;
                    }

                    response = _component2Repository.SaveCourseEntity(course);

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
                response.Message = "Error saving Course Entity data: " + ex.Message;
                return response;
            }
        }

        public (bool isValid, string errorMessage) IsValidData(CourseDetailsModel obj, List<CourseDetailsModel> validCourseCode)
        {

            if (string.IsNullOrWhiteSpace(obj?.CourseCode?.Trim()))
            {
                return (false, "Course Code is required.");
            }

            if (!validCourseCode.Any(hh => hh.CourseCode.Equals(obj.CourseCode?.Trim(), StringComparison.OrdinalIgnoreCase)))
            {
                return (false, "Course Code is not valid.");
            }

            if (obj?.ClassHoldingDate == null)
            {
                return (false, "Class Holding Date is required.");
            }

            if (obj?.ClassDurationMinutes == null)
            {
                return (false, "Class Duration Minutes is required.");
            }

            int classDurationMinutes = obj.ClassDurationMinutes.Value;

            if (classDurationMinutes <= 0)
            {
                return (false, "Class Duration Minutes must be greater than zero.");
            }

            // Add more validation as needed
            return (true, "Valid");
        }

        public ResponseModel SaveCourseDetails(List<CourseDetailsModel> Course, List<CourseDetailsModel> validCourseCode)
        {
            var response = new ResponseModel();
            int insertCount = 0;
            int skipCount = 0;

            try
            {
                foreach (var course in Course)
                {

                    bool isValid = IsValidData(course, validCourseCode).isValid;
                    if (!isValid)
                    {
                        skipCount++;
                        continue;
                    }

                    response = _component2Repository.SaveCourseDetails(course);

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
                response.Message = "Error saving Course Entity data: " + ex.Message;
                return response;
            }
        }
        public bool IsRecordExist_CourseDetails(string courseCode, DateTime classHoldingDate)
        {

            return _component2Repository.IsRecordExist_CourseDetails(courseCode, classHoldingDate);

        }

        public Dictionary<string, int> GetExistingCourseDetails()
        {
            return _component2Repository.GetExistingCourseDetails();
        }
    }
}
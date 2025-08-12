using SSSNP_SNSOP.Core.DbHandler;
using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Repository
{
    public class Component2Repository
    {
        private readonly IDbOperation _dbOperation;
        private static readonly object _locker = new object();

        string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
        public Component2Repository(string connectionString)
        {
            _dbOperation = new SqlDbOperation(connectionString);
        }
        public ResponseModel SaveTrainingAndAttendanceTracking(TrainingAndAttendanceModel obj)
        {
            var response = new ResponseModel();
            try
            {
                lock (_locker)
                {
                    var isExist = IsRecordExistInTrainingAndAttendence(obj.HouseholdNumber?.Trim());
                    var denormalizedData = GetDenormalizedBeneficiaryDataByHouseholdNumber(obj.HouseholdNumber?.Trim());

                    if (denormalizedData == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Nominee info not found for this household number: " + obj.HouseholdNumber?.Trim();
                        return response;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                    data.Add("CourseId", GetCourseIdByCode(obj.CourseCode));
                    data.Add("CourseCode", obj.CourseCode ?? (object)DBNull.Value);
                    data.Add("ClassAttendanceDate", obj.ClassAttendanceDate);
                    data.Add("StateID", denormalizedData.StateID);
                    data.Add("CountyID", denormalizedData.CountyID);
                    data.Add("PayamID", denormalizedData.PayamID);
                    data.Add("BomaID", denormalizedData.BomaID);
                    data.Add("FirstName", denormalizedData.FirstName ?? (object)DBNull.Value);
                    data.Add("MiddleName", denormalizedData.MiddleName ?? (object)DBNull.Value);
                    data.Add("NickName", denormalizedData.NickName ?? (object)DBNull.Value);
                    data.Add("LastName", denormalizedData.LastName ?? (object)DBNull.Value);
                    data.Add("Gender", denormalizedData.Gender ?? (object)DBNull.Value);
                    data.Add("Age", denormalizedData.Age);
                    if (!isExist)
                    {
                        data.Add("CreatedBy", obj.CreatedBy);
                        data.Add("CreatedDate", obj.CreatedDate);
                    }
                    if (isExist)
                    {
                        data.Add("ModifiedBy", obj.ModifiedBy);
                        data.Add("ModifiedDate", obj.ModifiedDate);
                    }
                    if (isExist)
                    {
                        var isAdded = _dbOperation.UpdateData("tbl_com2_Training_Attendance", data, " HouseholdNumber = '" + obj.HouseholdNumber?.Trim() + "'");
                    }
                    else
                    {
                        var isAdded = _dbOperation.InsertData("tbl_com2_Training_Attendance", data);
                    }
                    response.Message = "Training and Attendance Tracking data saved successfully.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving Training and Attendance Tracking data. Error: " + ex.ToString());
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Error when saving Training and Attendance Tracking data for this household number: " + obj.HouseholdNumber?.Trim()
                };
            }
        }

        private bool IsRecordExistInTrainingAndAttendence(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT HouseholdNumber FROM tbl_com2_Training_Attendance WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseModel SaveCoachingSessionTracking(CoachingSessionTrackingModel obj)
        {
            var response = new ResponseModel();
            try
            {
                lock (_locker)
                {
                    var isExist = IsRecordExistInCoachingSessionAttendance(obj.HouseholdNumber?.Trim());
                    var denormalizedData = GetDenormalizedBeneficiaryDataByHouseholdNumber(obj.HouseholdNumber?.Trim());

                    if (denormalizedData == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Nominee info not found for this household number: " + obj.HouseholdNumber?.Trim();
                        return response;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        ["HouseholdNumber"] = obj.HouseholdNumber?.Trim(),
                        ["CoachingDetailId"] = obj.CoachingDetailId,
                        ["CoachingAttendanceDate"] = obj.ClassAttendanceDate ?? (object)DBNull.Value
                    };

                    if (!isExist)
                    {
                        data["CreatedBy"] = obj.CreatedBy;
                        data["CreatedDate"] = obj.CreatedDate;
                    }
                    else
                    {
                        data["ModifiedBy"] = obj.ModifiedBy;
                        data["ModifiedDate"] = obj.ModifiedDate;
                    }

                    if (isExist)
                    {
                        _dbOperation.UpdateData(
                            "tbl_com2_Coaching_Session_Attendance",
                            data,
                            $"HouseholdNumber = '{obj.HouseholdNumber?.Trim()}' AND CoachingDetailId = {obj.CoachingDetailId}"
                        );
                    }
                    else
                    {
                        _dbOperation.InsertData("tbl_com2_Coaching_Session_Attendance", data);
                    }

                    response.IsSuccess = true;
                    response.Message = "Coaching Session Tracking data saved successfully.";
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving Coaching Session Tracking data. Error: " + ex.ToString());
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Error when saving Coaching Session Tracking data for this household number: " + obj.HouseholdNumber?.Trim()
                };
            }
        }


        private bool IsRecordExistInCoachingSessionAttendance(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable(
                    "SELECT HouseholdNumber FROM tbl_com2_Coaching_Session_Attendance WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public ResponseModel SaveLivelihoodGrantDisbursement(LivelihoodGrantDisbursementModel obj)
        {
            var response = new ResponseModel();
            try
            {
                lock (_locker)
                {
                    // Check if the record already exists
                    var isExist = IsRecordExistLivelihoodDisbursement(obj.HouseholdNumber?.Trim());

                    // Retrieve denormalized data associated with the household number
                    var denormalizedData = GetDenormalizedBeneficiaryDataByHouseholdNumber(obj.HouseholdNumber?.Trim());
                    if (denormalizedData == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Nominee info not found for this beneficiary.";
                        return response;
                    }

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                    data.Add("PaymentCycle", obj.PaymentCycle);
                    data.Add("PaymentAmount", obj.PaymentAmount);
                    data.Add("PaymentDate", obj.PaymentDate);
                    data.Add("ActualPaymentDate", obj.ActualPaymentDate);
                    data.Add("StateID", denormalizedData.StateID);
                    data.Add("CountyID", denormalizedData.CountyID);
                    data.Add("PayamID", denormalizedData.PayamID);
                    data.Add("BomaID", denormalizedData.BomaID);
                    data.Add("FirstName", denormalizedData.FirstName ?? (object)DBNull.Value);
                    data.Add("MiddleName", denormalizedData.MiddleName ?? (object)DBNull.Value);
                    data.Add("NickName", denormalizedData.NickName ?? (object)DBNull.Value);
                    data.Add("LastName", denormalizedData.LastName ?? (object)DBNull.Value);
                    data.Add("Gender", denormalizedData.Gender ?? (object)DBNull.Value);
                    data.Add("Age", denormalizedData.Age);

                    if (!isExist)
                    {
                        data.Add("CreatedBy", obj.CreatedBy);
                        data.Add("CreatedDate", obj.CreatedDate);
                    }

                    // Add ModifiedBy and ModifiedDate if it's an existing record
                    if (isExist)
                    {
                        data.Add("ModifiedBy", obj.ModifiedBy);
                        data.Add("ModifiedDate", obj.ModifiedDate);
                    }

                    // Perform Insert or Update based on whether the record already exists
                    bool isAdded;
                    if (isExist)
                    {
                        isAdded = _dbOperation.UpdateData("tbl_com2_Livelihood_Grant_Disbursement", data, " HouseholdNumber = '" + obj.HouseholdNumber?.Trim() + "'");
                    }
                    else
                    {
                        isAdded = _dbOperation.InsertData("tbl_com2_Livelihood_Grant_Disbursement", data);
                    }

                    // Set the success message in response
                    response.Message = "Livelihood Grant Disbursement data saved successfully.";
                    response.IsSuccess = isAdded;
                    return response;
                }
            }
            catch (Exception ex)
            {
                // Log the error and return failure response
                Console.WriteLine("Error when saving Livelihood Grant Disbursement data. Error: " + ex.ToString());
                return new ResponseModel
                {
                    IsSuccess = false,
                    Message = "Error when saving Livelihood Grant Disbursement data. Error: " + ex.ToString()
                };
            }
        }
        private bool IsRecordExistLivelihoodDisbursement(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT HouseholdNumber FROM tbl_com2_Livelihood_Grant_Disbursement WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private int GetCourseIdByCode(string code)
        {
            int courseId = 0;
            try
            {
                SqlParameter param = new SqlParameter("@code", code);
                var dt = _dbOperation.ExecuteDataTable("SELECT Id FROM tbl_com2_course WHERE CourseCode = @code",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    courseId = Convert.ToInt32(dt.Rows[0][0]?.ToString());
                    return courseId;
                }
                return courseId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Dictionary<string, int> GetExistingHouseholdNumbers()
        {
            Dictionary<string, int> householdNumbers = new Dictionary<string, int>();

            try
            {
                // Execute the query to get existing household numbers and their IDs
                var dt = _dbOperation.ExecuteDataTable("SELECT Id, HouseholdNumber FROM tbl_com2_Household_Enrollment_Data", CommandType.Text);

                // Check if any rows were returned
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string householdNumber = row["HouseholdNumber"].ToString();
                        int id = Convert.ToInt32(row["Id"]);

                        // Add the household number and ID to the dictionary
                        householdNumbers.Add(householdNumber, id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching existing household numbers: " + ex.Message);
            }

            return householdNumbers;
        }


        public ResponseModel SaveBeneficiary(BeneficiaryRandomizationAndEnrollmentModel model)
        {
            var response = new ResponseModel();
            int insertCount = 0; // Counter for successfully inserted records
            try
            {
                lock (_locker)
                {
                    // Check if the record already exists based on HouseholdNumber (unique constraint)
                    var isExist = IsRecordExist_HouseholdNumber(model.HouseholdNumber?.Trim());
                    var denormalizedData = GetDenormalizedBeneficiaryDataByHouseholdNumber(model.HouseholdNumber?.Trim());

                    if (denormalizedData == null)
                    {
                        response.IsSuccess = false;
                        response.Message = "Nominee info not found for this household number: " + model.HouseholdNumber?.Trim();
                        return response;
                    }
                    // Prepare data to insert/update
                    Dictionary<string, object> data = new Dictionary<string, object>();

                    data.Add("HouseholdNumber", model.HouseholdNumber?.Trim());
                    data.Add("InterestedInParticipating", model.InterestedInParticipating);
                    data.Add("SurvivesTargeting", model.SurvivesTargeting);
                    data.Add("TreatmentArm", model.TreatmentArm ?? (object)DBNull.Value);
                    data.Add("HHEnrollmentStatus", model.HHEnrollmentStatus);
                    data.Add("GroupID", denormalizedData.GroupId ?? (object)DBNull.Value);
                    data.Add("EnrolledMemberID", model.EnrolledMemberID ?? (object)DBNull.Value);
                    data.Add("ReasonForWhyNotEnrolled", model.ReasonForWhyNotEnrolled);
                    data.Add("StateID", denormalizedData.StateID);
                    data.Add("CountyID", denormalizedData.CountyID);
                    data.Add("PayamID", denormalizedData.PayamID);
                    data.Add("BomaID", denormalizedData.BomaID);
                    data.Add("FirstName", denormalizedData.FirstName ?? (object)DBNull.Value);
                    data.Add("MiddleName", denormalizedData.MiddleName ?? (object)DBNull.Value);
                    data.Add("NickName", denormalizedData.NickName ?? (object)DBNull.Value);
                    data.Add("LastName", denormalizedData.LastName ?? (object)DBNull.Value);
                    data.Add("Gender", denormalizedData.Gender ?? (object)DBNull.Value);
                    data.Add("Age", denormalizedData.Age);

                    // Add audit information based on whether the record is new or existing
                    if (!isExist)
                    {
                        data.Add("CreatedBy", model.CreatedBy);
                        data.Add("CreatedDate", model.CreatedDate);
                    }
                    else
                    {
                        data.Add("ModifiedBy", model.ModifiedBy);
                        data.Add("ModifiedDate", model.ModifiedDate);
                    }

                    // Insert or update the data
                    if (isExist)
                    {
                        try
                        {
                            _dbOperation.UpdateData("tbl_com2_Household_Enrollment_Data", data, " HouseholdNumber = '" + model.HouseholdNumber?.Trim() + "'");
                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "Error updating Beneficiary data for Household Number: " + model.HouseholdNumber?.Trim() + ". " + ex.Message;
                            Debug.WriteLine($"Update Exception: {ex}");
                            return response; // Return immediately if update fails
                        }
                    }
                    else
                    {
                        try
                        {
                            _dbOperation.InsertData("tbl_com2_Household_Enrollment_Data", data);
                            insertCount++; // Increment the counter for successful insertion
                        }
                        catch (Exception ex)
                        {
                            response.IsSuccess = false;
                            response.Message = "Error inserting Beneficiary data for Household Number: " + model.HouseholdNumber?.Trim() + ". " + ex.Message;
                            Debug.WriteLine($"Insert Exception: {ex}");
                            return response; // Return immediately if insert fails
                        }
                    }


                    // Include insert count in the success message if insertions were made
                    if (insertCount > 0)
                    {
                        response.Message = $"Beneficiary data saved successfully. {insertCount} new records were inserted.";
                    }
                    else
                    {
                        response.Message = "Beneficiary data updated successfully.";
                    }

                    response.IsSuccess = true;
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error when saving Beneficiary data: " + ex.Message + model.HouseholdNumber;
                Debug.WriteLine($"Exception: {ex}");
                return response;
            }
        }


        public bool IsRecordExist_HouseholdNumber(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable(
                    "SELECT HouseholdNumber FROM tbl_com2_Household_Enrollment_Data WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true; // The record exists
                }
                return false; // No matching record
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking if HouseholdNumber exists: " + ex.Message);
            }
        }

        private bool IsRecordExistInBusinessPlanDevelopment(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT HouseholdNumber FROM tbl_com2_Business_Plan_Development WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResponseModel SaveHouseholdGroup(HouseholdGroupModel model)
        {
            var response = new ResponseModel();
            int insertCount = 0;  // Counter for successfully inserted records
            try
            {
                lock (_locker)
                {
                    // Check if the record already exists
                    var isExist = IsRecordExist_GroupName(model.GroupName?.Trim());

                    // Prepare data to insert/update
                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "GroupName", model.GroupName?.Trim() },
                        { "Latitude", model.Latitude },
                        { "Longitude", model.Longitude },
                        { "VSLA_formed", model.VSLA_formed }
                    };

                    // Add audit information based on whether the record is new or existing
                    if (!isExist)
                    {
                        data.Add("CreatedBy", model.CreatedBy);
                        data.Add("CreatedDate", model.CreatedDate);
                    }
                    else
                    {
                        data.Add("ModifiedBy", model.ModifiedBy);
                        data.Add("ModifiedDate", model.ModifiedDate);
                    }

                    // Insert or update the data
                    if (isExist)
                    {
                        _dbOperation.UpdateData("tbl_com2_Household_Group_View", data, "GroupName = '" + model.GroupName?.Trim() + "'");
                    }
                    else
                    {
                        _dbOperation.InsertData("tbl_com2_Household_Group_View", data);
                        insertCount++; // Increment the counter for successful insertion
                    }

                    // Include insert count in the success message if insertions were made
                    if (insertCount > 0)
                    {
                        response.Message = $"Household Group data saved successfully. {insertCount} new records were inserted.";
                    }
                    else
                    {
                        response.Message = "Household Group data updated successfully.";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error when saving Household Group data: " + ex.Message;
                return response;
            }
        }
        public bool IsRecordExist_GroupName(string groupName)
        {
            try
            {
                SqlParameter param = new SqlParameter("@groupName", groupName);
                var dt = _dbOperation.ExecuteDataTable(
                    "SELECT GroupName FROM tbl_com2_Household_Group_View WHERE GroupName = @groupName",
                    CommandType.Text, param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true; // The record exists
                }
                return false; // No matching record
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking if HouseholdGroup exists: " + ex.Message);
            }
        }
        public Dictionary<string, int> GetExistingGroupNames()
        {
            Dictionary<string, int> groupNames = new Dictionary<string, int>();
            try
            {
                var dt = _dbOperation.ExecuteDataTable("SELECT Id, GroupName FROM tbl_com2_Household_Group_View", CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string groupName = row["GroupName"].ToString();
                        int Id = Convert.ToInt32(row["Id"]);
                        groupNames.Add(groupName, Id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching group names: " + ex.Message);
            }

            return groupNames;
        }
        public DenormalizedDataModel GetDenormalizedBeneficiaryDataByHouseholdNumber(string householdNumber)
        {
            DenormalizedDataModel result = null;
            try
            {
                SqlParameter param = new SqlParameter("@HouseholdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT top 1 household_number, Age, Sex, NameofHousehold, " +
                    "intStateid, intCountieid, intPayamid, intBomasid, link.GroupId " +
                    "FROM tbl_BeneficiaryServiceDet b " +
                    "INNER JOIN tbl_Nominee n on b.household_number=n.application_id " +
                    "LEFT JOIN tbl_com2_Household_Group_Linkage link on b.household_number=link.HouseholdNumber " +
                    "WHERE household_number = @HouseholdNumber "
                    ,
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = new DenormalizedDataModel();

                    result.StateID = dt.Rows[0]["intStateid"] != null ? Convert.ToInt32(dt.Rows[0]["intStateid"].ToString()) : 0;
                    result.CountyID = dt.Rows[0]["intCountieid"] != null ? Convert.ToInt32(dt.Rows[0]["intCountieid"].ToString()) : 0;
                    result.PayamID = dt.Rows[0]["intPayamid"] != null ? Convert.ToInt32(dt.Rows[0]["intPayamid"].ToString()) : 0;
                    result.BomaID = dt.Rows[0]["intBomasid"] != null ? Convert.ToInt32(dt.Rows[0]["intBomasid"].ToString()) : 0;

                    string NameofHousehold = dt.Rows[0]["NameofHousehold"] != null ? dt.Rows[0]["NameofHousehold"].ToString() : string.Empty;
                    if (NameofHousehold != string.Empty)
                    {
                        string[] names = NameofHousehold.Split(' ');
                        if (names.Length > 2)
                        {
                            result.FirstName = names[0];
                            result.MiddleName = names[1];
                            result.LastName = names[2];
                        }
                    }

                    result.Gender = dt.Rows[0]["Sex"] != null ? dt.Rows[0]["Sex"].ToString() : string.Empty;
                    result.Age = dt.Rows[0]["Age"] != null ? Convert.ToInt32(dt.Rows[0]["Age"].ToString()) : 0;
                    result.GroupId = dt.Rows[0]["GroupId"] != DBNull.Value ? Convert.ToInt32(dt.Rows[0]["GroupId"]) : (int?)null;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public ResponseModel SaveBusinessPlanDevelopment(BusinessPlanDevelopmentModel obj)
        {
            var response = new ResponseModel();
            try
            {
                lock (_locker)
                {
                    var isExist = IsRecordExistInBusinessPlanDevelopment(obj.HouseholdNumber?.Trim());
                    var denormalizedData = GetDenormalizedBeneficiaryDataByHouseholdNumber(obj.HouseholdNumber?.Trim());
                    if (denormalizedData != null)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                        data.Add("BusinessPlanStatus", obj.BusinessPlanStatus);
                        data.Add("BusinessType", obj.BusinessType);
                        data.Add("PlanSubmitted", obj.PlanSubmitted);
                        data.Add("PlanScreened", obj.PlanScreened);
                        data.Add("PlanApproved", obj.PlanApproved);
                        data.Add("WhyRejected", obj.WhyRejected ?? (object)DBNull.Value);
                        data.Add("BusinessDescription", obj.BusinessDescription ?? (object)DBNull.Value);
                        data.Add("HowSpent", obj.HowSpent ?? (object)DBNull.Value);
                        data.Add("InPersonPercentage", obj.InPersonPercentage);
                        data.Add("GroupPercentage", obj.GroupPercentage);
                        data.Add("StateID", denormalizedData.StateID);
                        data.Add("CountyID", denormalizedData.CountyID);
                        data.Add("PayamID", denormalizedData.PayamID);
                        data.Add("BomaID", denormalizedData.BomaID);
                        data.Add("FirstName", denormalizedData.FirstName);
                        data.Add("MiddleName", denormalizedData.MiddleName);
                        data.Add("NickName", denormalizedData.NickName ?? (object)DBNull.Value);
                        data.Add("LastName", denormalizedData.LastName);
                        data.Add("Gender", denormalizedData.Gender);
                        data.Add("Age", denormalizedData.Age);
                        if (!isExist)
                        {
                            data.Add("CreatedBy", obj.CreatedBy);
                            data.Add("CreatedDate", obj.CreatedDate);
                        }
                        if (isExist)
                        {
                            data.Add("ModifiedBy", obj.ModifiedBy);
                            data.Add("ModifiedDate", obj.ModifiedDate);
                        }
                        if (isExist)
                        {
                            var isAdded = _dbOperation.UpdateData("tbl_com2_Business_Plan_Development", data, " HouseholdNumber = '" + obj.HouseholdNumber?.Trim() + "'");
                        }
                        else
                        {
                            var isAdded = _dbOperation.InsertData("tbl_com2_Business_Plan_Development", data);
                        }
                        response.Message = "Business Plan Development Data saved successfully.";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Invalid Household Number";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving Business Plan Development data. Error: " + ex.ToString());
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Error when saving Business Plan Development data. Error: " + ex.ToString()
                };
            }
        }

        public void SaveAuditLog<T>(List<T> list, string tableName)
        {
            try
            {
                //foreach (T dto in list)
                //{
                //    _dbOperation.GenericInsertData(dto, tableName);
                //}
                _dbOperation.GenericInsertDataList(list, tableName);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving Business Plan Development Audit data. Error: " + ex.ToString());
            }
        }

        public ResponseModel SaveFinancialServiceLinkage(FinancialServiceLinkageTrackingModel obj)
        {
            var response = new ResponseModel();
            try
            {
                lock (_locker)
                {
                    var isExist = IsRecordExistInFinancialServiceLinkage(obj.HouseholdNumber?.Trim());
                    var denormalizedData = GetDenormalizedBeneficiaryDataByHouseholdNumber(obj.HouseholdNumber?.Trim());
                    if (denormalizedData != null)
                    {
                        Dictionary<string, object> data = new Dictionary<string, object>();
                        data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                        data.Add("ConnectedFinancialServices", obj.ConnectedFinancialServices);
                        data.Add("EntityName", obj.EntityName);
                        data.Add("StateID", denormalizedData.StateID);
                        data.Add("CountyID", denormalizedData.CountyID);
                        data.Add("PayamID", denormalizedData.PayamID);
                        data.Add("BomaID", denormalizedData.BomaID);
                        data.Add("FirstName", denormalizedData.FirstName);
                        data.Add("MiddleName", denormalizedData.MiddleName);
                        data.Add("NickName", denormalizedData.NickName ?? (object)DBNull.Value);
                        data.Add("LastName", denormalizedData.LastName);
                        data.Add("Gender", denormalizedData.Gender);
                        data.Add("Age", denormalizedData.Age);
                        data.Add("FinancialServiceConnectionDate", obj.FinancialServiceConnectionDate ?? (object)DBNull.Value);
                        data.Add("DepositAccountOpened", obj.DepositAccountOpened ?? (object)DBNull.Value);
                        data.Add("DepositAccountOpeningDate", obj.DepositAccountOpeningDate ?? (object)DBNull.Value);
                        data.Add("AccountLastActivityDate", obj.AccountLastActivityDate ?? (object)DBNull.Value);
                        if (!isExist)
                        {
                            data.Add("CreatedBy", obj.CreatedBy);
                            data.Add("CreatedDate", obj.CreatedDate);
                        }
                        if (isExist)
                        {
                            data.Add("ModifiedBy", obj.ModifiedBy);
                            data.Add("ModifiedDate", obj.ModifiedDate);
                        }
                        if (isExist)
                        {
                            var isAdded = _dbOperation.UpdateData("tbl_com2_Financial_Services_Linkage_Tracking", data, " HouseholdNumber = '" + obj.HouseholdNumber?.Trim() + "'");
                        }
                        else
                        {
                            var isAdded = _dbOperation.InsertData("tbl_com2_Financial_Services_Linkage_Tracking", data);
                        }
                        response.Message = "Financial Service Linkage Tracking Data saved successfully.";
                    }
                    else
                    {
                        response.IsSuccess = false;
                        response.Message = "Invalid Household Number";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when saving Financial Service Linkage Tracking data. Error: " + ex.ToString());
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Error when saving Financial Service Linkage Tracking. Error: " + ex.ToString()

                };
            }
        }

        private bool IsRecordExistInFinancialServiceLinkage(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT HouseholdNumber FROM tbl_com2_Financial_Services_Linkage_Tracking WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ResponseModel SaveHouseholdGroupLinkage(SSSNP_SNSOP.Core.Model.Component2.HouseholdGroupLinkageModel obj)
        {
            var response = new ResponseModel();
            try
            {
                lock (_locker)
                {

                    var isExist = IsRecordExistInHouseholdGroupLinkage(obj.HouseholdNumber?.Trim());

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                    data.Add("GroupId", obj.GroupId);

                    if (!isExist)
                    {
                        data.Add("CreatedBy", obj.CreatedBy);
                        data.Add("CreatedDate", obj.CreatedDate);
                        var isAdded = _dbOperation.InsertData("tbl_com2_Household_Group_Linkage", data);
                    }
                    else
                    {
                        data.Add("ModifiedBy", obj.ModifiedBy);
                        data.Add("ModifiedDate", obj.ModifiedDate);
                        var isAdded = _dbOperation.UpdateData("tbl_com2_Household_Group_Linkage", data, " HouseholdNumber = '" + obj.HouseholdNumber?.Trim() + "'");
                    }

                    response.Message = "Household Group linkage is saved successfully.";
                    response.IsSuccess = true; // Add this to signify success
                    return response;
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Error when saving Household Group linkage. Error: " + ex.ToString()
                };
            }
        }




        private bool IsRecordExistInHouseholdGroupLinkage(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT HouseholdNumber FROM tbl_com2_Household_Group_Linkage WHERE HouseholdNumber = @householdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public HashSet<string> GetValidHouseholdNumberValues()
        {
            try
            {
                var householdNumbers = new HashSet<string>();

                var dt = _dbOperation.ExecuteDataTable("SELECT b.household_number FROM tbl_BeneficiaryServiceDet b " +
                    "INNER JOIN tbl_Nominee n on b.household_number=n.application_id " +
                    "GROUP BY b.household_number", CommandType.Text);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        householdNumbers.Add(row["household_number"] != DBNull.Value ? row["household_number"].ToString() : null);
                    }
                }

                return householdNumbers;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when retrieving valid household numbers. Error: " + ex.ToString());
                return new HashSet<string>();
            }
        }

        public List<ValidHouseholdNumberModel> GetValidHouseholdNumbers()
        {
            try
            {
                // Initialize a list to hold valid household numbers
                var householdNumbers = new List<ValidHouseholdNumberModel>();

                // Execute SQL command to retrieve household numbers
                var dt = _dbOperation.ExecuteDataTable("SELECT b.household_number FROM tbl_BeneficiaryServiceDet b " +
                    "INNER JOIN tbl_Nominee n on b.household_number=n.application_id", CommandType.Text);

                // Check if the DataTable is not null and contains rows
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        // Create a new model instance and add the household number
                        householdNumbers.Add(new ValidHouseholdNumberModel
                        {
                            HouseholdNumber = row["household_number"] != DBNull.Value ? row["household_number"].ToString() : null
                        });
                    }
                }

                return householdNumbers; // Return the list of valid household numbers
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when retrieving valid household numbers. Error: " + ex.ToString());
                return new List<ValidHouseholdNumberModel>(); // Return an empty list in case of an error
            }
        }

        /*public HashSet<int> GetValidGroupId()
        {
            try
            {
                var groupIds = new HashSet<int>();

                // SQL Query: Retrieve unique Group IDs from the tbl_Household_Group_View table
                string query = @"
            SELECT Id
            FROM tbl_com2_Household_Group_View
            GROUP BY Id";

                var dt = _dbOperation.ExecuteDataTable(query, CommandType.Text);

                // Check if the query returned results
                if (dt != null && dt.Rows.Count > 0)
                {
                    // Loop through the rows and add to the HashSet
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["Id"] != DBNull.Value && int.TryParse(row["Id"].ToString(), out int groupId))
                        {
                            groupIds.Add(groupId);
                        }
                    }
                }

                return groupIds;
            }
            catch (Exception ex)
            {
                // Log the error for debugging purposes
                Console.WriteLine("Error when retrieving valid Group IDs. Error: " + ex.ToString());
                return new HashSet<int>();
            }
        }*/

        public DataTable GetHouseholdDataByLocation(SqlParameter stateParam, SqlParameter countyParam, SqlParameter payamParam, SqlParameter bomaParam)
        {
            DataTable householdsTable = new DataTable();

            try
            {
                string query = @"
            SELECT b.household_number, b.NameofHousehold, b.Sex, b.Age, g.Id as GroupId, g.GroupName
            FROM tbl_BeneficiaryDet b
            INNER JOIN tbl_Nominee n 
            ON b.household_number = n.application_id
            LEFT JOIN tbl_com2_Household_Group_Linkage l
            ON l.HouseholdNumber = b.household_number
            LEFT JOIN tbl_com2_Household_Group_View g
            ON g.Id = l.GroupId 
            WHERE (@StateId IS NULL OR b.intStateid = @StateId)
            AND (@CountyId IS NULL OR b.intCountieid = @CountyId)
            AND (@PayamId IS NULL OR b.intPayamid = @PayamId)
            AND (@BomaId IS NULL OR b.intBomasid = @BomaId) GROUP BY b.household_number, b.NameofHousehold, b.Sex, b.Age, g.Id, g.GroupName";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(stateParam);
                        cmd.Parameters.Add(countyParam);
                        cmd.Parameters.Add(payamParam);
                        cmd.Parameters.Add(bomaParam);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(householdsTable); // Fill the DataTable with the query result
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return householdsTable;
        }

        public void InsertHouseholdGroupLinkage(string householdNumber, int groupId)
        {
            try
            {
                string checkQuery = "SELECT COUNT(1) FROM tbl_com2_Household_Group_Linkage WHERE HouseholdNumber = @HouseholdNumber AND GroupId = @GroupId";
                string insertQuery = "INSERT INTO tbl_com2_Household_Group_Linkage (HouseholdNumber, GroupId) VALUES (@HouseholdNumber, @GroupId)";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Check if the record already exists
                    using (SqlCommand checkCmd = new SqlCommand(checkQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@HouseholdNumber", householdNumber);
                        checkCmd.Parameters.AddWithValue("@GroupId", groupId);
                        int exists = (int)checkCmd.ExecuteScalar();

                        // If the record doesn't exist, insert it
                        if (exists == 0)
                        {
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                            {
                                insertCmd.Parameters.AddWithValue("@HouseholdNumber", householdNumber);
                                insertCmd.Parameters.AddWithValue("@GroupId", groupId);
                                insertCmd.ExecuteNonQuery();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public HashSet<string> GetValidHouseholdNumberValues(int? stateId, int? countyId, int? payamId, int? bomaId)
        {
            var householdNumbers = new HashSet<string>();

            // Prepare query
            string query = @"
            SELECT b.household_number 
            FROM tbl_BeneficiaryServiceDet b
            INNER JOIN tbl_Nominee n ON b.household_number = n.application_id 
            WHERE (@StateId IS NULL OR b.intStateid = @StateId)
            AND (@CountyId IS NULL OR b.intCountieid = @CountyId)
            AND (@PayamId IS NULL OR b.intPayamid = @PayamId)
            AND (@BomaId IS NULL OR b.intBomasid = @BomaId)
            GROUP BY b.household_number";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@StateId", stateId.HasValue ? (object)stateId.Value : DBNull.Value),
                new SqlParameter("@CountyId", countyId.HasValue ? (object)countyId.Value : DBNull.Value),
                new SqlParameter("@PayamId", payamId.HasValue ? (object)payamId.Value : DBNull.Value),
                new SqlParameter("@BomaId", bomaId.HasValue ? (object)bomaId.Value : DBNull.Value)
            };
            // Execute the query and get the result as a DataTable
            DataTable dt = _dbOperation.ExecuteDataTable(query, CommandType.Text, parameters);

            // Extract household numbers from DataTable
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    householdNumbers.Add(row["household_number"].ToString());
                }
            }

            return householdNumbers;
        }

        public List<SSSNP_SNSOP.Core.Model.HouseholdGroupLinkageModel> GetMappedHouseholdNumberWithGroupValues(int? stateId, int? countyId, int? payamId, int? bomaId)
        {
            var householdGroupLinkageList = new List<SSSNP_SNSOP.Core.Model.HouseholdGroupLinkageModel>();

            // Prepare the query with the necessary joins
            string query = @"
            SELECT b.household_number, l.GroupId, g.GroupName
            FROM tbl_BeneficiaryServiceDet b
            INNER JOIN tbl_Nominee n ON b.household_number = n.application_id
            LEFT JOIN tbl_com2_Household_Group_Linkage l ON b.household_number = l.HouseholdNumber
            LEFT JOIN tbl_com2_Household_Group_View g ON l.GroupId = g.Id
            WHERE (@StateId IS NULL OR b.intStateid = @StateId)
            AND (@CountyId IS NULL OR b.intCountieid = @CountyId)
            AND (@PayamId IS NULL OR b.intPayamid = @PayamId)
            AND (@BomaId IS NULL OR b.intBomasid = @BomaId)
            GROUP BY b.household_number, l.GroupId, g.GroupName";

            List<SqlParameter> parameters = new List<SqlParameter>
            {
                new SqlParameter("@StateId", stateId.HasValue ? (object)stateId.Value : DBNull.Value),
                new SqlParameter("@CountyId", countyId.HasValue ? (object)countyId.Value : DBNull.Value),
                new SqlParameter("@PayamId", payamId.HasValue ? (object)payamId.Value : DBNull.Value),
                new SqlParameter("@BomaId", bomaId.HasValue ? (object)bomaId.Value : DBNull.Value)
            };

            // Execute the query and get the result as a DataTable
            DataTable dt = _dbOperation.ExecuteDataTable(query, CommandType.Text, parameters);

            // Extract household numbers, group IDs, and group names from DataTable
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    SSSNP_SNSOP.Core.Model.HouseholdGroupLinkageModel linkage = new SSSNP_SNSOP.Core.Model.HouseholdGroupLinkageModel
                    {
                        HouseholdNumber = row["household_number"].ToString(),
                        GroupId = row["GroupId"] != DBNull.Value ? (int?)Convert.ToInt32(row["GroupId"]) : null,
                        GroupName = row["GroupName"] != DBNull.Value ? row["GroupName"].ToString() : null
                    };
                    householdGroupLinkageList.Add(linkage);
                }
            }

            return householdGroupLinkageList;
        }

        public Dictionary<string, int> GetExistingCourseCode()
        {
            Dictionary<string, int> CourseCode = new Dictionary<string, int>();
            try
            {
                var dt = _dbOperation.ExecuteDataTable("SELECT Id, CourseCode FROM tbl_com2_course", CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string courseCode = row["CourseCode"].ToString();
                        int Id = Convert.ToInt32(row["Id"]);
                        CourseCode.Add(courseCode, Id);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching Course Code: " + ex.Message);
            }

            return CourseCode;
        }


        public ResponseModel SaveCourseEntity(CourseEntityModel course)
        {
            var response = new ResponseModel();
            int insertCount = 0;  // Counter for successfully inserted records
            try
            {
                lock (_locker)
                {
                    // Check if the record already exists
                    var isExist = IsRecordExist_CourseCode(course.CourseCode?.Trim());

                    // Prepare data to insert/update
                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "CourseName", course.CourseName?.Trim() },
                        { "CourseCode", course.CourseCode },
                        { "CourseDuration", course.CourseDuration }
                    };

                    // Add audit information based on whether the record is new or existing
                    if (!isExist)
                    {
                        data.Add("CreatedBy", course.CreatedBy);
                        data.Add("CreatedDate", course.CreatedDate);
                    }
                    else
                    {
                        data.Add("ModifiedBy", course.ModifiedBy);
                        data.Add("ModifiedDate", course.ModifiedDate);
                    }

                    try
                    {
                        if (isExist)
                        {
                            _dbOperation.UpdateData("tbl_com2_course", data, "CourseCode = '" + course.CourseCode?.Trim() + "'");
                        }
                        else
                        {
                            _dbOperation.InsertData("tbl_com2_course", data);
                            insertCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error occurred while processing the course data: " + ex.Message);
                        throw;
                    }

                    if (insertCount > 0)
                    {
                        response.Message = $"Course Entity data saved successfully. {insertCount} new records were inserted.";
                    }
                    else
                    {
                        response.Message = "Course Entity data updated successfully.";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error when saving Course Entity data: " + ex.Message;
                return response;
            }
        }

        public bool IsRecordExist_CourseCode(string courseCode)
        {
            try
            {
                SqlParameter param = new SqlParameter("@courseCode", courseCode);
                var dt = _dbOperation.ExecuteDataTable(
                    "SELECT CourseCode FROM tbl_com2_course WHERE CourseCode = @courseCode",
                    CommandType.Text, param);

                if (dt != null && dt.Rows.Count > 0)
                {
                    return true; // The record exists
                }
                return false; // No matching record
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking if Course Entity exists: " + ex.Message);
            }
        }

        public ResponseModel SaveCourseDetails(CourseDetailsModel course)
        {
            var response = new ResponseModel();
            int insertCount = 0;  // Counter for successfully inserted records
            try
            {
                lock (_locker)
                {
                    // Check if the record already exists
                    var isExist = IsRecordExist_CourseDetails(course.CourseCode?.Trim(), course.ClassHoldingDate);

                    // Prepare data to insert/update
                    Dictionary<string, object> data = new Dictionary<string, object>
                    {
                        { "CourseCode", course.CourseCode },
                        { "ClassDurationMinutes", course.ClassDurationMinutes },
                        { "ClassHoldingDate", course.ClassHoldingDate}
                    };

                    // Add audit information based on whether the record is new or existing
                    if (!isExist)
                    {
                        data.Add("CreatedBy", course.CreatedBy);
                        data.Add("CreatedDate", course.CreatedDate);
                    }
                    else
                    {
                        data.Add("ModifiedBy", course.ModifiedBy);
                        data.Add("ModifiedDate", course.ModifiedDate);
                    }

                    try
                    {

                        if (isExist)
                        {
                            _dbOperation.UpdateData("tbl_com2_Course_Details", data, "CourseCode = '" + course.CourseCode?.Trim() + "'");
                        }
                        else
                        {
                            _dbOperation.InsertData("tbl_com2_Course_Details", data);
                            insertCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error during database operation (Insert/Update): " + ex.Message);
                    }

                    if (insertCount > 0)
                    {
                        response.Message = $"Course Entity data saved successfully. {insertCount} new records were inserted.";
                    }
                    else
                    {
                        response.Message = "Course Entity data updated successfully.";
                    }
                    return response;
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = "Error when saving Course Entity data: " + ex.Message;
                return response;
            }
        }

        public bool IsRecordExist_CourseDetails(string courseCode, DateTime classHoldingDate)
        {
            try
            {
                // Create a list of SqlParameter objects
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@courseCode", courseCode),
                    new SqlParameter("@classHoldingDate", classHoldingDate)
                };

                // Use the ExecuteDataTable method with List<SqlParameter>
                var dt = _dbOperation.ExecuteDataTable(
                    @"SELECT CourseCode, ClassHoldingDate 
                      FROM tbl_com2_Course_Details 
                      WHERE CourseCode = @courseCode AND ClassHoldingDate = @classHoldingDate",
                    CommandType.Text,
                    parameters);

                // Check if the DataTable has rows (record exists)
                if (dt != null && dt.Rows.Count > 0)
                {
                    return true; // Record exists
                }
                return false; // No matching record
            }
            catch (Exception ex)
            {
                throw new Exception("Error checking if Course Code and Class Holding Date exist: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetExistingCourseDetails()
        {
            Dictionary<string, int> CourseDetails = new Dictionary<string, int>();
            try
            {
                var dt = _dbOperation.ExecuteDataTable("SELECT Id, CourseCode, ClassHoldingDate FROM tbl_com2_Course_Details", CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string courseCode = row["CourseCode"].ToString();
                        DateTime date = Convert.ToDateTime(row["ClassHoldingDate"]);
                        int id = Convert.ToInt32(row["Id"]);

                        string key = $"{courseCode}|{date:yyyy-MM-dd}";
                        if (!CourseDetails.ContainsKey(key))
                        {
                            CourseDetails.Add(key, id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching Course Code: " + ex.Message);
            }

            return CourseDetails;
        }


        public List<DateTime> GetValidCourseDates(string courseCode)
        {
            List<DateTime> validDates = new List<DateTime>();

            try
            {
                // Define the SQL query
                string query = "SELECT ClassHoldingDate FROM tbl_com2_Course_Details WHERE CourseCode = @CourseCode";

                // Create SQL parameters
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@CourseCode", courseCode)
                };

                // Execute query using IDbOperation
                var dt = _dbOperation.ExecuteDataTable(query, CommandType.Text, parameters);

                // Process results
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (DateTime.TryParse(row["ClassHoldingDate"].ToString(), out DateTime parsedDate))
                        {
                            validDates.Add(parsedDate.Date); // Store only the date part
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching valid class dates for course '{courseCode}': {ex.Message}");
            }

            return validDates;
        }

        public Dictionary<string, int> GetExistingCoachingId()
        {
            Dictionary<string, int> coachingDetailId = new Dictionary<string, int>();
            try
            {
                // Assuming CoachingId is the business identifier and Id is the primary key
                var dt = _dbOperation.ExecuteDataTable("SELECT Id, CoachingId FROM tbl_com2_Coaching_Details", CommandType.Text);
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string coachingId = row["CoachingId"].ToString();
                        int id = Convert.ToInt32(row["Id"]);
                        if (!coachingDetailId.ContainsKey(coachingId))
                        {
                            coachingDetailId.Add(coachingId, id);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching Coaching Detail Codes: " + ex.Message);
            }

            return coachingDetailId;
        }

        public List<DateTime> GetValidCoachingDates(int coachingDetailId)
        {
            List<DateTime> validDates = new List<DateTime>();

            try
            {
                string query = @"
            SELECT CoachingSessionHoldingDate 
            FROM tbl_com2_Coaching_Details 
            WHERE CoachingId = @CoachingDetailId";

                var parameters = new List<SqlParameter>
        {
            new SqlParameter("@CoachingDetailId", coachingDetailId)
        };

                var dt = _dbOperation.ExecuteDataTable(query, CommandType.Text, parameters);

                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        if (DateTime.TryParse(row["CoachingSessionHoldingDate"].ToString(), out DateTime parsedDate))
                        {
                            validDates.Add(parsedDate.Date);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching valid coaching session dates for CoachingDetailId '{coachingDetailId}': {ex.Message}");
            }

            return validDates;
        }



    }

}

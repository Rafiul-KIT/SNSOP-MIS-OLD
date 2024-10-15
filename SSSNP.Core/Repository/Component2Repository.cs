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
                    var isExist = IsRecordExist(obj.HouseholdNumber?.Trim());
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
                    data.Add("CourseCode", obj.CourseCode);
                    data.Add("CourseDuration", obj.CourseDuration);
                    data.Add("ClassesAttended", obj.ClassesAttended);
                    data.Add("ClassesHeld", obj.ClassesHeld);
                    data.Add("ClassesMissed", obj.ClassesMissed);
                    data.Add("WhyMissed", obj.WhyMissed);
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



        public DenormalizedDataModel GetDenormalizedDataByHouseholdNumber(string householdNumber)
        {
            DenormalizedDataModel result = null;
            try
            {
                SqlParameter param = new SqlParameter("@HouseholdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT * FROM tbl_com2_Household_Enrollment_Data WHERE HouseholdNumber = @HouseholdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
                    result = new DenormalizedDataModel();
                    result.StateID = dt.Rows[0]["StateID"] != null ? Convert.ToInt32(dt.Rows[0]["StateID"].ToString()) : 0;
                    result.CountyID = dt.Rows[0]["CountyID"] != null ? Convert.ToInt32(dt.Rows[0]["CountyID"].ToString()) : 0;
                    result.PayamID = dt.Rows[0]["PayamID"] != null ? Convert.ToInt32(dt.Rows[0]["PayamID"].ToString()) : 0;
                    result.BomaID = dt.Rows[0]["BomaID"] != null ? Convert.ToInt32(dt.Rows[0]["BomaID"].ToString()) : 0;
                    result.FirstName = dt.Rows[0]["FirstName"] != null ? dt.Rows[0]["FirstName"].ToString() : string.Empty;
                    result.MiddleName = dt.Rows[0]["MiddleName"] != null ? dt.Rows[0]["MiddleName"].ToString() : string.Empty;
                    result.LastName = dt.Rows[0]["LastName"] != null ? dt.Rows[0]["LastName"].ToString() : string.Empty;
                    result.NickName = dt.Rows[0]["NickName"] != null ? dt.Rows[0]["NickName"].ToString() : string.Empty;
                    result.Gender = dt.Rows[0]["Gender"] != null ? dt.Rows[0]["Gender"].ToString() : string.Empty;
                    result.Age = dt.Rows[0]["Age"] != null ? Convert.ToInt32(dt.Rows[0]["Age"].ToString()) : 0;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
                    data.Add("TreatmentArm", model.TreatmentArm);
                    data.Add("HHEnrollmentStatus", model.HHEnrollmentStatus);
                    data.Add("GroupID", model.GroupID); // Assuming 0 is an invalid GroupID
                    data.Add("EnrolledMemberID", model.EnrolledMemberID); // Assuming 0 is invalid
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
                        _dbOperation.UpdateData("tbl_com2_Household_Enrollment_Data", data, " HouseholdNumber = '" + model.HouseholdNumber?.Trim() + "'");
                    }
                    else
                    {
                        _dbOperation.InsertData("tbl_com2_Household_Enrollment_Data", data);
                        insertCount++; // Increment the counter for successful insertion
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
                response.Message = "Error when saving Beneficiary data: " + ex.Message;
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


        private bool IsRecordExist(string householdNumber)
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
                        { "Longitude", model.Longitude }
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
                    "intStateid, intCountieid, intPayamid, intBomasid " +
                    "FROM tbl_BeneficiaryServiceDet b " +
                    "INNER JOIN tbl_Nominee n on b.household_number=n.application_id " +
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

        public HashSet<int> GetValidGroupId()
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

        public List<HouseholdGroupLinkageModel> GetMappedHouseholdNumberWithGroupValues(int? stateId, int? countyId, int? payamId, int? bomaId)
        {
            var householdGroupLinkageList = new List<HouseholdGroupLinkageModel>();

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
                    HouseholdGroupLinkageModel linkage = new HouseholdGroupLinkageModel
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



    }

}
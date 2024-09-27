using SSSNP_SNSOP.Core.DbHandler;
using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                    data.Add("CourseId", GetCourseIdByCode(obj.CourseCode));
                    data.Add("CourseCode", obj.CourseCode);
                    data.Add("CourseDuration", obj.CourseDuration);
                    data.Add("ClassesAttended", obj.ClassesAttended);
                    data.Add("ClassesHeld", obj.ClassesHeld);
                    data.Add("ClassesMissed", obj.ClassesHeld);
                    data.Add("WhyMissed", obj.ClassesMissed);
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
                    if(isExist)
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
            catch(Exception ex)
            {
                Console.WriteLine("Error when saving Training and Attendance Tracking data. Error: " + ex.ToString());
                return new ResponseModel()
                {
                    IsSuccess = false,
                    Message = "Error when saving Training and Attendance Tracking data. Error: " + ex.ToString()

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

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                    data.Add("PaymentCycle", obj.PaymentCycle);
                    data.Add("PaymentAmount", obj.PaymentAmount);
                    data.Add("PaymentDate", obj.PaymentDate);
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


        private bool IsRecordExist(string householdNumber)
        {
            try
            {
                SqlParameter param = new SqlParameter("@householdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT HouseholdNumber FROM tbl_com2_Training_Attendance WHERE HouseholdNumber = @householdNumber", 
                    CommandType.Text, param);
                if(dt != null && dt.Rows.Count > 0)
                {
                    return true;
                }
                return false;
            }
            catch(Exception ex)
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
                    }

                    response.Message = "Household Group data saved successfully.";
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
            DenormalizedDataModel result = new DenormalizedDataModel();
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
    }
}

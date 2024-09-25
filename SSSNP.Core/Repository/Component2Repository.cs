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
                    var denormalizedData = GetDenormalizedDataByHouseholdNumber(obj.HouseholdNumber?.Trim());

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
                    data.Add("NickName", denormalizedData.NickName);
                    data.Add("LastName", denormalizedData.LastName);
                    data.Add("Gender", denormalizedData.Gender);
                    data.Add("Age", denormalizedData.Age);
                    if (!isExist)
                    {
                        data.Add("CreatedBy", obj.CretedBy);
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
            DenormalizedDataModel result = new DenormalizedDataModel();
            try
            {
                SqlParameter param = new SqlParameter("@HouseholdNumber", householdNumber);
                var dt = _dbOperation.ExecuteDataTable("SELECT * FROM tbl_com2_Household_Enrollment_Data WHERE HouseholdNumber = @HouseholdNumber",
                    CommandType.Text, param);
                if (dt != null && dt.Rows.Count > 0)
                {
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

    }
}

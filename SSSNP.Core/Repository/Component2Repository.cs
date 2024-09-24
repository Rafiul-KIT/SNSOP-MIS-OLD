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

                    Dictionary<string, object> data = new Dictionary<string, object>();
                    data.Add("HouseholdNumber", obj.HouseholdNumber?.Trim());
                    data.Add("CourseId", GetCourseIdByCode(obj.CourseCode));
                    data.Add("CourseCode", obj.CourseCode);
                    data.Add("CourseDuration", obj.CourseDuration);
                    data.Add("ClassesAttended", obj.ClassesAttended);
                    data.Add("ClassesHeld", obj.ClassesHeld);
                    data.Add("ClassesMissed", obj.ClassesHeld);
                    data.Add("WhyMissed", obj.ClassesMissed);
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

    }
}

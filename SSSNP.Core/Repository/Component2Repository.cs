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
    }
}

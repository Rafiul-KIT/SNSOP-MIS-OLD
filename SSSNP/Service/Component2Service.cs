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
    }
}
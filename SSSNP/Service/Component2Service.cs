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
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SSSNP.Component_2.HouseholdGroupView
{
    public class HouseholdGroupService
    {
        private HouseholdGroupRepository _householdGroupRepository;

        public HouseholdGroupService()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            _householdGroupRepository = new HouseholdGroupRepository(connectionString);
        }

        public ResponseModel SaveHouseholdGroups(List<HouseholdGroupModel> groups)
        {
            var response = new ResponseModel();
            try
            {
                foreach (var group in groups)
                {
                    if (!IsValidData(group)) continue; // Only process valid data
                    response = _householdGroupRepository.SaveHouseholdGroup(group);
                }
                return response;
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving Household Group data: " + ex.Message);
            }
        }

        public Dictionary<string, int> GetExistingGroupNames()
        {
            return _householdGroupRepository.GetExistingGroupNames();
        }


        private bool IsValidData(HouseholdGroupModel group)
        {
            if (string.IsNullOrWhiteSpace(group?.GroupName?.Trim()))
            {
                return false; // GroupName is mandatory
            }
            // Add more validation if necessary
            return true;
        }

    }
}
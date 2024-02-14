using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class BeneficiaryAPI
    {
        public int id { get; set; }
        public string supervisor_id { get; set; }
        public string partner_code { get; set; }
        public string household_number { get; set; }
        public string beneficiary_type { get; set; }
        public string group_number { get; set; }
        public string member_number { get; set; }
        public string household_size { get; set; }
        public string household_name { get; set; }
        public string age { get; set; }
        public string spouse_name { get; set; }
        public string id_number { get; set; }
        public string phone_number { get; set; }
        public string gender { get; set; }
        public string female_dependants { get; set; }
        public string male_dependants { get; set; }
        public string zero_five { get; set; }
        public string six_eighteen { get; set; }
        public string nineteen_forty_five { get; set; }
        public string forty_six_sixty_five { get; set; }
        public string sixty_six { get; set; }
        public string income_household { get; set; }
        public string average_household { get; set; }
        public string state { get; set; }
        public string county { get; set; }
        public string payam { get; set; }
        public string boma { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string legal_status { get; set; }
        public string photo_url { get; set; }

        public string status { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

        //ALternate     
        public string alternate_number { get; set; }
        public string selection_criteria { get; set; }
        public string selection_reason { get; set; } 
              
    }
}

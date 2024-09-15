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
        public int? id { get; set; }
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
        public string liTemplate { get; set; }
        public string lmTemplate { get; set; }
        public string lrTemplate { get; set; }
        public string lsTemplate { get; set; }
        public string ltTemplate { get; set; }
        public string riTemplate { get; set; }
        public string rmTemplate { get; set; }
        public string rrTemplate { get; set; }
        public string rsTemplate { get; set; }
        public string rtTemplate { get; set; }
        public string household_photo_url { get; set; }
        public string alt1_photo_url { get; set; }
        public string alt2_photo_url { get; set; }
        public string alt1Name { get; set; }
        public string alt1Gender { get; set; }
        public int? alt1Age { get; set; }
        public string alt1Nid { get; set; }
        public string alt1Mobile { get; set; }
        public string alt2Name { get; set; }
        public string alt2Gender { get; set; }
        public int? alt2Age { get; set; }
        public string alt2Nid { get; set; }
        public string alt2Mobile { get; set; }
    }

    [Serializable]
    public class BeneficiaryAPIExt
    {
        public int? id { get; set; }
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
        public string liTemplate { get; set; }
        public string lmTemplate { get; set; }
        public string lrTemplate { get; set; }
        public string lsTemplate { get; set; }
        public string ltTemplate { get; set; }
        public string riTemplate { get; set; }
        public string rmTemplate { get; set; }
        public string rrTemplate { get; set; }
        public string rsTemplate { get; set; }
        public string rtTemplate { get; set; }
        public string household_photo_url { get; set; }
        public string alt1_photo_url { get; set; }
        public string alt2_photo_url { get; set; }
        public string alt1Name { get; set; }
        public string alt1Gender { get; set; }
        public int? alt1Age { get; set; }
        public string alt1Nid { get; set; }
        public string alt1Mobile { get; set; }
        public string alt2Name { get; set; }
        public string alt2Gender { get; set; }
        public int? alt2Age { get; set; }
        public string alt2Nid { get; set; }
        public string alt2Mobile { get; set; }
        public List<NomineeDto> nomineeList { get; set; } = new List<NomineeDto>();
    }

    public class NomineeDto
    {
        public Int64 afisNomineeId { get; set; }
        public string applicationId { get; set; }
        public string nomineeFirstName { get; set; }
        public string nomineeMiddleName { get; set; }
        public string nomineeLastName { get; set; }
        public string nomineeNickName { get; set; }
        public int? relationshipWithHouseholdHead { get; set; }
        public string relationshipOther { get; set; }
        public int? nomineeAge { get; set; }
        public int? nomineeGender { get; set; }
        public int? isReadWrite { get; set; }
        public int? nomineeOccupation { get; set; }
        public string otherOccupation { get; set; }
    }
}

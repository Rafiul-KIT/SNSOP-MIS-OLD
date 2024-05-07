using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity
{
    [Serializable]
    public class NewIdCardInfo
    {
        public string HouseholdNumber { get; set; }
        public string HouseholdName { get; set; }
        public int HouseholdAge { get; set; }
        public string HouseholdGender { get; set; }
        public string HouseholdPhone { get; set; }
        public List<AlternateNewCardInfo> Alternates { get; set; }
        public string Signature { get; set; }

        public NewIdCardInfo()
        {
            Alternates = new List<AlternateNewCardInfo>();
        }
    }
}

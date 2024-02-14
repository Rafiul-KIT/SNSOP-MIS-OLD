using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class GenerateCardID
    {
        public string NumberID { get; set; }
        public string BeneficiaryName { get; set; }
        public string BeneficiaryPhoto { get; set; }
        public string CountryName { get; set; }
        public string ValidTo { get; set; }
        public string AlternateName1 { get; set; }
        public string AlternatePhoto1 { get; set; }
        public string AlternateName2 { get; set; }
        public string AlternatePhoto2 { get; set; }


    }
}
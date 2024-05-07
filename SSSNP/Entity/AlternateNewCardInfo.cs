using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity
{
    [Serializable]
    public class AlternateNewCardInfo
    {
        public string AlternateName { get; set; }
        public int AlternateAge { get; set; }
        public string AlternateGender { get; set; }
        public string AlternatePhone { get; set; }
    }
}

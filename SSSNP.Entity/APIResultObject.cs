using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class APIResultObject
    {
            public bool error { get; set; }
            public int errorCode { get; set; }
            public string errorMessage { get; set; }
            public int returnId { get; set; }
            public string householdNumber { get; set; }
                
    }
}

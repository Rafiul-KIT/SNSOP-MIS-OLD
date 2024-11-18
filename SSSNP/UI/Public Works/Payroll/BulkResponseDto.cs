using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.UI.Public_Works.Payroll
{
    public class BulkResponseDto
    {
        public string applicationId { get; set; }
        public string errorMessage { get; set; }
        public string result { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model
{
    public class PayrollReconcileRequestDto : GeoLocationDto
    {
        public string ReqNo { get; set; }
        public string WorkCode { get; set; }
    }
}

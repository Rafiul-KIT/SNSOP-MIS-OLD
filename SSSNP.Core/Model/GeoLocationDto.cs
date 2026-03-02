using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model
{
    public class GeoLocationDto
    {
        public int? StateId { get; set; }
        public int? CountyId { get; set; }
        public int? PayamId { get; set; }
        public int? BomaId { get; set; }

        public string StateName { get; set; }
        public string CountyName { get; set; }
        public string PayamName { get; set; }
        public string BomaName { get; set; }
    }
}

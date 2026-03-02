using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Service
{
    public class IndicatorResultQuery
    {
        public int indicatorId { get; set; }
        public int? stateId { get; set; }
        public int? countyId { get; set; }
        public int? payamId { get; set; }
        public int? bomaId { get; set; }
        public string supportType { get; set; }
        public string legalStatus { get; set; }
        public string paymentCycle { get; set; }
        public string sex { get; set; }
        public string ageRange { get; set; }
        public string grievanceCategory { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class BenAttendanceDet
    {
        public int IntAttendanceID { get; set; }
        public int IntWorkActivityID { get; set; }
        public int IntBenID { get; set; }
        public  DateTime AttendanceDate { get; set; }
        public string Status { get; set; }
        public int? Created_by { get; set; }
        public DateTime? Created_dt { get; set; }
        public int? Modified_by { get; set; }
        public DateTime? Modified_dt { get; set; }
        public int? AttendanceYear { get; set; }
        public int? PortYear { get; set; }
        public int? PortPhase { get; set; }
        public string PortedSheetName { get; set; }
        public int? PaymentFlag { get; set; }
        public int? WageApplyFlag { get; set; }
        public int? IntWageReqID { get; set; }

    }
}

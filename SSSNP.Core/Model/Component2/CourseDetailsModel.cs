using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class CourseDetailsModel : CommonModel
    {
        public string CourseCode { get; set; }
        public DateTime ClassHoldingDate { get; set; }
        public int? ClassDurationMinutes { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class TrainingAndAttendanceModel : CommonModel
    {
        public int Id { get; set; }
        public string HouseholdNumber { get; set; }
        public string CourseCode { get; set; }
        public int CourseDuration { get; set; }
        public int ClassesAttended { get; set; }
        public int ClassesHeld { get; set; }
        public int ClassesMissed { get; set; }
        public string WhyMissed { get; set; }
        public int CourseId { get; set; }
    }
}

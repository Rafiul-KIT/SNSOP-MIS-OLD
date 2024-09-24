using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Component_2.AttendanceTrackingGridView
{
    public class TrainingAndAttendanceTracking
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
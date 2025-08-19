using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class CoachingDetailsModel : CommonModel
    {
        public int Id { get; set; }
        public string CoachingName { get; set; }
        public DateTime? CoachingSessionHoldingDate { get; set; }
        public int? CoachingSessionDurationMinutes { get; set; }
    }
}

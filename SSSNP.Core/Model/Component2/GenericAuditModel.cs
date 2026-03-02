using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class GenericAuditModel
    {
        public string FileId { get; set; }
        public int UploadedBy { get; set; }
        public DateTime UploadDate { get; set; } = DateTime.Now;
        public Boolean Validity { get; set; }
        public string InvalidReason { get; set; }
    }
}

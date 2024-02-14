using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class ErrorMessage
    {
        public int SequenceNumber { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}

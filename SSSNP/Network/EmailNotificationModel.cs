using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Network
{
    public class EmailNotificationModel
    {
        public string email_to { get; set; }
        public string subject { get; set; }
        public string body { get; set; }
        public DateTime sent_at { get; set; }
        public bool send_status { get; set; }
        public string send_result_message { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll
{
    [Serializable]
    public class PayrollAlternateDto
    {
        public int age { get; set; }
        public string firstName { get; set; }
        public int gender { get; set; }
        public string lastName { get; set; }
        public string middleName { get; set; }
        public string nickName { get; set; }
        public string phoneNumber { get; set; }
    }
}
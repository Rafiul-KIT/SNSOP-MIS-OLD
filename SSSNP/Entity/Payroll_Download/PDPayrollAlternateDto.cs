using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll_Download
{
    [Serializable]
    public class PDPayrollAlternateDto
    {
        public long id { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string nickName { get; set; }
        public int age { get; set; }
        public int gender { get; set; }
        public string phoneNumber { get; set; }
        public string nationalId { get; set; }
        public PDBiometricDto biometric { get; set; }

        public PDPayrollAlternateDto()
        {
            biometric = new PDBiometricDto();
        }
    }
}
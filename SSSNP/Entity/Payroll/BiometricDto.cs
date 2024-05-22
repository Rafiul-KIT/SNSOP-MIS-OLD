using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll
{
    public class BiometricDto
    {
       string photo { get; set; }
       string leftSmall { get; set; }
       string leftRing { get; set; }
       string leftMiddle { get; set; }
       string leftIndex { get; set; }
       string leftThumb { get; set; }
       string rightSmall { get; set; }
       string rightRing { get; set; }
       string rightMiddle { get; set; }
       string rightIndex { get; set; }
       string rightThumb { get; set; }
    }
}
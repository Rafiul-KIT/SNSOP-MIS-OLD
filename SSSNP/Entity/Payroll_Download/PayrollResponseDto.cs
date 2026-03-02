using System.Collections.Generic;

namespace SSSNP.Entity.Payroll_Download
{
    public class PayrollResponseDto
    {
        public List<PDPayrollDataDto> data { get; set; }
        public int errorCode { get; set; }
        public string errorMsg { get; set; }
        public bool operationResult { get; set; }
        public int totalCount { get; set; }

        public PayrollResponseDto()
        {
            data = new List<PDPayrollDataDto>();
        }
    }
}

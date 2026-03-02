using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll_Download
{
    [Serializable]
    public class PDPayrollDataDto
    {
        public Int32 intWagePaymentReqid { get; set; }
        public string reqFromDate { get; set; }
        public string reqToDate { get; set; }
        public double exchangerateattime { get; set; }
        public double wageRateUSD { get; set; }
        public string reqNo { get; set; }
        public Int32 intWagesReqBenTransid { get; set; }
        public Int32 intBenid { get; set; }
        public string enrollmentNo { get; set; }
        public Int32 intWorkid { get; set; }
        public string workCode { get; set; }
        public Int32 totAttendance { get; set; }
        public double wageRateUSDTotal { get; set; }
        public string dateofCreation { get; set; }
        public string status { get; set; }
        public string paymentCycle { get; set; }
        public double wageRateSSPTotal { get; set; }
        public string supportType { get; set; }
        public PDHouseholdDetailDto houseHoldDetail { get; set; }
        public List<PDPayrollAlternateDto> alternates { get; set; }
        
        public PDPayrollDataDto()
        {
            alternates = new List<PDPayrollAlternateDto>();
            houseHoldDetail = new PDHouseholdDetailDto();
        }
    }
}
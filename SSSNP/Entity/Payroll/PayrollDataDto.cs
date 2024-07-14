using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Entity.Payroll
{
    [Serializable]
    public class PayrollDataDto
    {
        public Int64 state { get; set; }
        public Int64 county { get; set; }
        public Int64 payam { get; set; }
        public Int64 boma { get; set; }
        public List<PayrollAlternateDto> alternates { get; set; }
        public string dateofCreation { get; set; }
        public string enrollmentNo { get; set; }
        public double exchangerateattime { get; set; }
        public HouseholdDetailDto houseHoldDetail { get; set; }
        public Int32 intBenid { get; set; }
        public Int32 intWagePaymentReqid { get; set; }
        public Int32 intWagesReqBenTransid { get; set; }
        public Int32 intWorkid { get; set; }
        public string paymentCycle { get; set; }
        public string reqFromDate { get; set; }
        public string reqNo { get; set; }
        public string reqToDate { get; set; }
        public string status { get; set; }
        public string supportType { get; set; }
        public Int32 totAttendance { get; set; }
        public double wageRateSSPTotal { get; set; }
        public double wageRateUSD { get; set; }
        public double wageRateUSDTotal { get; set; }
        public string workCode { get; set; }

        public PayrollDataDto()
        {
            alternates = new List<PayrollAlternateDto>();
            houseHoldDetail = new HouseholdDetailDto();
        }
    }
}
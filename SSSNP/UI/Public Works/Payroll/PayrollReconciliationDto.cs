using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.UI.Public_Works.Payroll
{
    public class PayrollReconciliationDto
    {
        public string amount { get; set; }
        public string attendance { get; set; }
        public string created_at { get; set; }
        public string date_from { get; set; }
        public string date_to { get; set; }
        public string enrolment_number { get; set; }
        public string exchange_rate { get; set; }
        public string first_name { get; set; }
        public string generated_at { get; set; }
        public string household_number { get; set; }
        public string last_name { get; set; }
        public string latitude { get; set; }
        public string longitude { get; set; }
        public string paid_to { get; set; }
        public string paymentCenter { get; set; }
        public string status { get; set; }
        public string supervisor_id { get; set; }
        public string surname { get; set; }
        public string trans_id { get; set; }
        public string updated_at { get; set; }
        public string uuid { get; set; }
        public string wage { get; set; }
        public string work_code { get; set; }
        public string work_id { get; set; }
    }
}
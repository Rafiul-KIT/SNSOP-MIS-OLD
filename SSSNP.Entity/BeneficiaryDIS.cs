using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class BeneficiaryDIS
    {
        public int intBenID { get; set; }
        public string EnrollmentNo { get; set; }
		public string NameofHousehold { get; set; }
		public string Sex { get; set; }
		public int Age { get; set; }
		public string SelectionCriteria { get; set; }
		public string ContactNo { get; set; }
		public string Email { get; set; }
		public string FamilyIncome { get; set; }
		public int intStateID { get; set; }
		public int intCountieID { get; set; }
		public int intPayamID { get; set; }
		public int intBomasID { get; set; }
		public string StateName { get; set; }
		public string CountieName { get; set; }
		public string PayamName { get; set; }
		public string BomaName { get; set; }
		
	}
}

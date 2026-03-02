using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.Model.Component2
{
    public class DenormalizedDataModel
    {
        public int StateID { get; set; }
        public int CountyID { get; set; }
        public int PayamID { get; set; }
        public int BomaID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int? GroupId { get; set; }
    }
}

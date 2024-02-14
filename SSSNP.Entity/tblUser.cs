using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class tblUser
    {
        public int IntUserId { get; set; }
        public string User_name { get; set; }
        public string Email { get; set; }
        public string User_Id { get; set; }
        public string User_Level { get; set; }
        public string User_Type { get; set; }
        public string User_Code { get; set; }
        public bool GBVUser { get; set; }


    }
}

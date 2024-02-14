using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class APITest
    {
        public int id { get; set; }
        public string nome_api { get; set; } 
        public string cognome_api { get; set; }
        public int eta_api { get; set; }

    }
}

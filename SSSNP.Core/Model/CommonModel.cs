using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSSNP_SNSOP.Core.Model
{
    public class CommonModel
    {
        public int CreatedBy { get; set; }  // = HttpContext.Current.Session["uid"] != null ? Convert.ToInt32(HttpContext.Current.Session["uid"]) : 0;  // TODO: set user id from session
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; } //= HttpContext.Current.Session["uid"] != null ? Convert.ToInt32(HttpContext.Current.Session["uid"]) : 0; // TODO: set user id from session
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}

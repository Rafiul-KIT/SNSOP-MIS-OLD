using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Component_2
{
    public class AuditInformation
    {
        public int CreatedBy { get; set; }  // = HttpContext.Current.Session["uid"] != null ? Convert.ToInt32(HttpContext.Current.Session["uid"]) : 0;  // TODO: set user id from session
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public int ModifiedBy { get; set; } //= HttpContext.Current.Session["uid"] != null ? Convert.ToInt32(HttpContext.Current.Session["uid"]) : 0; // TODO: set user id from session
        public DateTime ModifiedDate { get; set; } = DateTime.Now;
    }
}
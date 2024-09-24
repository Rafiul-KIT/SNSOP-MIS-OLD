using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP.Component_2
{
    public class ResponseModel
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; }
        public int? ErrorCode { get; set; }
        public object ModelData { get; set; }
    }
}
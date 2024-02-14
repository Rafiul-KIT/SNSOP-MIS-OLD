using CSDS.Core.Framework.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SSSNP_SNSOP
{
    public static class SSSNPHelper
    {
        public static int TryParseInt(string value)
        {
            int result;
            int.TryParse(value.CheckSafeString(), out result);

            return result;
        }
        public static string TryParseString(string value)
        {
            return value.CheckSafeString();
        }
        public static bool TryParseBool(string value)
        {
            bool result;
            bool.TryParse(value.CheckSafeString(), out result);

            return result;
        }
        public static bool TryParseYesNo(string value)
        {
            if (string.IsNullOrEmpty(value)) return false;
            if (value.ToLower() == "yes") return true;
            else { return false; }
            
        }
        public static bool? TryParseYesNo (string value, bool nullSafe)
        {
            if (nullSafe)
            {
                if (value == null) return null;
                if (string.IsNullOrEmpty(value)) return null;
                return TryParseYesNo(value);
            }
            else { return TryParseYesNo(value); }
            
        }
        public static DateTime TryParseDateTime(string value)
        {
            DateTime result;
            value = value.Replace("-", "/");
            DateTime.TryParseExact(value.CheckSafeString(), "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out result);

            return result; // (result != DateTime.MinValue) ? (DateTime?)result : null;
        }
        public static DateTime? TryParseDateTime(string value, bool nullSafe)
        {
            DateTime? result;
            result = TryParseDateTime(value);
            if (nullSafe ) return  (result != DateTime.MinValue) ? (DateTime?)result : null;
            return result; 

        }

    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP
{
    public partial class WSGetBenEnrollment : System.Web.UI.Page
    {
        General Gen = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            string result;
            Response.Clear();
            Response.ClearHeaders();

            try
            {

                if (Request.QueryString["bomacode"] == null) throw new ArgumentNullException("BomaCode in query string is null");
                else if (string.IsNullOrEmpty(Request.QueryString["bomacode"])) throw new ArgumentNullException("BomaCode in query string is empty");

                string bomaCode = Request.QueryString["bomacode"];

                DataSet dt = Gen.GetBeneficieryEnrollmentAPI(bomaCode);

                if (dt.Tables[0].Rows.Count > 0)
                {

                    result = "{ \"WSGetBenEnrollment\":\"success\",\"status\":" + "\"" + "1" + "\",\"result\":" + DataTableToJSONWithJavaScriptSerializer(dt.Tables[0]) + "}";
                }
                else
                {
                    result = "{ \"WSGetBenEnrollment\":\"failure\",\"status\":\"0\",\"result\": \"Data not found\"}";
                }

            }
            catch (Exception ex)
            {
                result = "{ \"WSGetBenEnrollment\":\"failure\",\"status\":\"0\",\"result\": \"" + ex.Message  + "\"}";

            }

            Response.Write(result);

        }
        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            jsSerializer.MaxJsonLength = Int32.MaxValue;
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }

    }
}
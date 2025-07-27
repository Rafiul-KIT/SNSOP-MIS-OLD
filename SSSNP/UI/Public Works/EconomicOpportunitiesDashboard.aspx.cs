using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works
{
    public partial class EconomicOpportunitiesDashboard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public class CountyEnrollmentData
        {
            public string CountyName { get; set; }
            public int TotalEnrolled { get; set; }
        }

        [WebMethod]
        public static List<CountyEnrollmentData> GetEnrollmentData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    SELECT 
        c.CountieName AS CountyName, 
        ISNULL(COUNT(h.Id), 0) AS TotalEnrolled
    FROM tm_Counties c
    LEFT JOIN tbl_com2_Household_Enrollment_Data h ON h.CountyID = c.intCountieid 
        AND h.HHEnrollmentStatus = 1
    WHERE c.intCountieid IN (9202, 9311, 7301)
    GROUP BY c.CountieName";


            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(new CountyEnrollmentData
                    {
                        CountyName = reader["CountyName"].ToString(),
                        TotalEnrolled = Convert.ToInt32(reader["TotalEnrolled"])
                    });
                }
            }

            return result;
        }

    }
}
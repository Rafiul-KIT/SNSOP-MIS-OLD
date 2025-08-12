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
    ISNULL(COUNT(bd.household_number), 0) AS TotalEnrolled
FROM tm_Counties c
LEFT JOIN tbl_BeneficiaryDet bd ON bd.intCountieid = c.intCountieid
    AND bd.household_number IS NOT NULL
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

        [WebMethod]
        public static List<CountyEnrollmentData> GetBeneficiaryAssignedGroupData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    SELECT
c.CountieName AS CountyName,
ISNULL(COUNT(h.Id), 0) AS TotalEnrolled
FROM tm_Counties c
left join tbl_BeneficiaryDet bd  on c.intCountieid = bd.intCountieid
LEFT JOIN tbl_com2_Household_Group_Linkage h ON h.HouseholdNumber = bd.household_number
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

        [WebMethod]
        public static List<CountyEnrollmentData> GetFinlitData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    with cte as (
select c.id, count(*) cou from tbl_com2_Course_Details cd
join tbl_com2_course c on cd.CourseCode = c.CourseCode
where c.id=1 -- finlit
group by c.id
) select c.CountieName CountyName,  count(*) * 100 /  cte.cou TotalEnrolled 
from cte
left join tbl_com2_Training_Attendance ta on ta.CourseId = cte.id
left join tm_Counties c on ta.CountyID = c.intCountieid
where ta.CourseId=1 -- finlit
group by c.intCountieid, c.CountieName, cte.cou";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetLivelihoodTrainingData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    with cte as (
select c.id, count(*) cou from tbl_com2_Course_Details cd
join tbl_com2_course c on cd.CourseCode = c.CourseCode
where c.id=2
group by c.id
) select c.CountieName CountyName,  count(*) * 100 /  cte.cou TotalEnrolled
from cte
left join tbl_com2_Training_Attendance ta on ta.CourseId = cte.id
left join tm_Counties c on ta.CountyID = c.intCountieid
where ta.CourseId=2
group by c.intCountieid, c.CountieName, cte.cou";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetLifeskillData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    with cte as (
select c.id, count(*) cou from tbl_com2_Course_Details cd
join tbl_com2_course c on cd.CourseCode = c.CourseCode
where c.id=3
group by c.id
) select c.CountieName CountyName, count(*) * 100 / cte.cou TotalEnrolled
from cte
left join tbl_com2_Training_Attendance ta on ta.CourseId = cte.id
left join tm_Counties c on ta.CountyID = c.intCountieid
where ta.CourseId=3
group by c.intCountieid, c.CountieName, cte.cou";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetSavingsData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    with cte as (
select c.id, count(*) cou from tbl_com2_Course_Details cd
join tbl_com2_course c on cd.CourseCode = c.CourseCode
where c.id=4
group by c.id
) select c.CountieName CountyName, count(*) * 100 / cte.cou TotalEnrolled
from cte
left join tbl_com2_Training_Attendance ta on ta.CourseId = cte.id
left join tm_Counties c on ta.CountyID = c.intCountieid
where ta.CourseId=4
group by c.intCountieid, c.CountieName, cte.cou";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetNonFarmData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    select c.CountieName CountyName, sum(case when bpd.BusinessPlanStatus=1 then 1 else 0 end) TotalEnrolled
from tbl_com2_Business_Plan_Development bpd
join tbl_BeneficiaryDet tbd on bpd.HouseholdNumber=tbd.household_number
join tm_Counties c on tbd.intCountieid = c.intCountieid
and c.intCountieid IN (9202, 9311, 7301)
where bpd.BusinessType=2 group by c.CountieName";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetFarmData()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    select c.CountieName CountyName, sum(case when bpd.BusinessPlanStatus=1 then 1 else 0 end) TotalEnrolled
from tbl_com2_Business_Plan_Development bpd
join tbl_BeneficiaryDet tbd on bpd.HouseholdNumber=tbd.household_number
join tm_Counties c on tbd.intCountieid = c.intCountieid
and c.intCountieid IN (9202, 9311, 7301)
where bpd.BusinessType=1 group by c.CountieName";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetInstallment1Data()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    select c.CountieName CountyName, sum(case when bpd.PaymentCycle=1 then 1 else 0 end) TotalEnrolled
from tbl_com2_Livelihood_Grant_Disbursement bpd
join tbl_BeneficiaryDet tbd on bpd.HouseholdNumber=tbd.household_number
join tm_Counties c on tbd.intCountieid = c.intCountieid
and c.intCountieid IN (9202, 9311, 7301)
group by c.CountieName";


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

        [WebMethod]
        public static List<CountyEnrollmentData> GetInstallment2Data()
        {
            List<CountyEnrollmentData> result = new List<CountyEnrollmentData>();

            string query = @"
    select c.CountieName CountyName, sum(case when bpd.PaymentCycle=2 then 1 else 0 end) TotalEnrolled
from tbl_com2_Livelihood_Grant_Disbursement bpd
join tbl_BeneficiaryDet tbd on bpd.HouseholdNumber=tbd.household_number
join tm_Counties c on tbd.intCountieid = c.intCountieid
and c.intCountieid IN (9202, 9311, 7301)
group by c.CountieName";


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
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace SSSNP_SNSOP.Core.Repository
{
    public class AuditRecordRepository : IAuditRecordRepository
    {
        private readonly string _connectionString;

        public AuditRecordRepository()
        {
            // Fetch connection string from the config file
            _connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
        }

        // Method for Household Enrollment Audit
        public DataTable GetHouseholdEnrollmentAudit(DateTime? fromDate, DateTime? toDate)
        {
            string query = @"
        SELECT 
            audit.Id AS Id,
            audit.HouseholdNumber AS [Household Number], 
            CASE 
                WHEN audit.InterestedInParticipating = 1 THEN 'Yes' 
                ELSE 'No' 
            END AS [Interested In Participating],
            CASE 
                WHEN audit.SurvivesTargeting = 1 THEN 'Yes' 
                ELSE 'No' 
            END AS [Survives Targeting],
            CASE 
                WHEN audit.TreatmentArm = 1 THEN 'Yes' 
                ELSE 'No' 
            END AS [Treatment Arm],
            CASE 
                WHEN audit.HHEnrollmentStatus = 1 THEN 'Yes' 
                ELSE 'No' 
            END AS [HH Enrollment Status],
            audit.GroupID AS [Group ID],
            audit.EnrolledMemberID AS [Enrolled Member ID],
            users.User_name AS [Uploaded By],
            audit.UploadDate AS [Upload Date],
            audit.Validity AS [Validity],
            audit.InvalidReason AS [Invalid Reason]
        FROM tbl_com2_Household_Enrollment_Data_Audit AS audit
        LEFT JOIN tbl_Users AS users ON audit.UploadedBy = users.intUserid
        WHERE 1 = 1";

            return ExecuteAuditQuery(query, fromDate, toDate);
        }


        // Method for Training Attendance Audit
        public DataTable GetTrainingAttendanceAudit(DateTime? fromDate, DateTime? toDate)
        {
            string query = @"
        SELECT 
            audit.Id AS Id,
            audit.HouseholdNumber AS [Household Number], 
            audit.CourseCode AS [Course Code],
            audit.CourseDuration AS [Course Duration],
            audit.ClassesAttended AS [Classes Attended],
            audit.ClassesHeld AS [Classes Held],
            audit.ClassesMissed AS [Classes Missed],
            audit.WhyMissed AS [Why Missed],
            users.User_name AS [Uploaded By],
            audit.UploadDate AS [Upload Date],
            audit.Validity AS [Validity],
            audit.InvalidReason AS [Invalid Reason]
        FROM tbl_com2_Training_Attendance_Audit AS audit
        LEFT JOIN tbl_Users AS users ON audit.UploadedBy = users.intUserid
        WHERE 1 = 1";

            return ExecuteAuditQuery(query, fromDate, toDate);
        }

        // Method for Livelihood Grant Disbursement Audit
        public DataTable GetLivelihoodGrantDisbursementAudit(DateTime? fromDate, DateTime? toDate)
        {
            string query = @"
        SELECT 
            audit.Id AS Id,
            audit.HouseholdNumber AS [Household Number], 
            audit.PaymentCycle AS [Payment Cycle], 
            audit.PaymentAmount AS [Payment Amount], 
            audit.PaymentDate AS [Payment Date],
            audit.ActualPaymentDate AS [Actual Payment Date],
            users.User_name AS [Uploaded By],
            audit.UploadDate AS [Upload Date],
            audit.Validity AS [Validity],
            audit.InvalidReason AS [Invalid Reason]
        FROM tbl_com2_Livelihood_Grant_Disbursement_Audit AS audit
        LEFT JOIN tbl_Users AS users ON audit.UploadedBy = users.intUserid
        WHERE 1 = 1";

            return ExecuteAuditQuery(query, fromDate, toDate);
        }


        // Method for Financial Services Linkage Audit
        public DataTable GetFinancialServicesLinkageAudit(DateTime? fromDate, DateTime? toDate)
        {
            string query = @"
        SELECT 
            audit.Id AS Id,
            audit.HouseholdNumber AS [Household Number], 
            CASE 
                WHEN audit.ConnectedFinancialServices = 1 THEN 'Yes' 
                ELSE 'No' 
            END AS [Connected Financial Services],
            audit.EntityName AS [Entity Name],
            users.User_name AS [Uploaded By],
            audit.UploadDate AS [Upload Date],
            audit.Validity AS [Validity],
            audit.InvalidReason AS [Invalid Reason]
        FROM tbl_com2_Financial_Services_Linkage_Tracking_Audit AS audit
        LEFT JOIN tbl_Users AS users ON audit.UploadedBy = users.intUserid
        WHERE 1 = 1";

            return ExecuteAuditQuery(query, fromDate, toDate);
        }

        // Method for Business Plan Development Audit
        public DataTable GetBusinessPlanDevelopmentAudit(DateTime? fromDate, DateTime? toDate)
        {
            string query = @"
        SELECT 
            audit.Id AS Id,
            audit.HouseholdNumber AS [Household Number], 
            CASE 
        WHEN audit.BusinessPlanStatus = 1 THEN 'Yes' 
        WHEN audit.BusinessPlanStatus = 0 THEN 'No' 
        END AS [Business Plan Status],
            CASE
        WHEN audit.BusinessType = 1 THEN 'Farm'
        WHEN audit.BusinessType = 2 THEN 'Non-Farm'
        END AS [Business Type],
            users.User_name AS [Uploaded By],
            audit.UploadDate AS [Upload Date],
            audit.Validity AS [Validity],
            audit.InvalidReason AS [Invalid Reason]
        FROM tbl_com2_Business_Plan_Development_Audit AS audit
        LEFT JOIN tbl_Users AS users ON audit.UploadedBy = users.intUserid
        WHERE 1 = 1";

            // Add date filters if applicable
            return ExecuteAuditQuery(query, fromDate, toDate);
        }


        // Common method to execute the SQL query
        private DataTable ExecuteAuditQuery(string query, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dt = new DataTable();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    // Add date filters if dates are provided
                    if (fromDate.HasValue)
                        query += " AND UploadDate >= @FromDate";
                    if (toDate.HasValue)
                        query += " AND UploadDate < @ToDate";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Pass date parameters to the SQL query
                        if (fromDate.HasValue)
                            cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                        if (toDate.HasValue)
                            cmd.Parameters.AddWithValue("@ToDate", toDate.Value);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            conn.Open();
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching audit records: {ex.Message}", ex);
            }

            return dt;
        }

    }
}

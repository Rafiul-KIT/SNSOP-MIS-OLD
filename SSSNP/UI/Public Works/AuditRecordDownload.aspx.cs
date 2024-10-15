using System;
using System.Data;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;

namespace SSSNP.UI.Public_Works
{
    public partial class AuditRecordDownload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string selectedAuditTable = ddlAuditRecordType.SelectedValue;

            if (string.IsNullOrEmpty(selectedAuditTable))
            {
                
                lblMessage.Text = "Please select an audit record.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
                return;
            }

           
            DateTime? fromDate = null;
            DateTime? toDate = null;

            if (DateTime.TryParse(txtFromDate.Text, out DateTime parsedFromDate))
            {
                fromDate = parsedFromDate;
            }

            if (DateTime.TryParse(txtToDate.Text, out DateTime parsedToDate))
            {
                toDate = parsedToDate;
            }

            
            DataTable auditRecords = GetAuditRecords(selectedAuditTable, fromDate, toDate);

           
            if (auditRecords == null || auditRecords.Rows.Count == 0)
            {
                lblMessage.Text = "No records found for the selected audit and date range.";
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;


                gvAuditRecords.DataSource = auditRecords;
                gvAuditRecords.DataBind();
            }
        }

        
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            
        }

        
        private DataTable GetAuditRecords(string tableName, DateTime? fromDate, DateTime? toDate)
        {
            DataTable dt = new DataTable();
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = $"SELECT * FROM {tableName} WHERE 1 = 1";

                   
                    if (fromDate.HasValue)
                    {
                        query += " AND UploadDate >= @FromDate";
                    }

                    if (toDate.HasValue)
                    {
                        query += " AND UploadDate <= @ToDate";
                    }

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        if (fromDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@FromDate", fromDate.Value);
                        }

                        if (toDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@ToDate", toDate.Value);
                        }

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
                
                lblMessage.Text = "Error fetching audit records: " + ex.Message;
                lblMessage.CssClass = "text-danger";
                lblMessage.Visible = true;
            }

            return dt;
        }

        protected void txtFromDate_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void txtToDate_TextChanged(object sender, EventArgs e)
        {
            
        }

        protected void ddlAuditRecordType_SelectedIndexChanged(object sender, EventArgs e)
        {
           
        }

        protected void rgAuditRecords_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            
        }
    }
}

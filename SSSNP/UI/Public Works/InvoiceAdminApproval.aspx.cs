using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works
{
    public partial class InvoiceAdminApproval : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadInvoiceHistory();
            }
        }

        private void LoadInvoiceHistory()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM invoice_history", conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvInvoiceHistory.DataSource = dt;
                gvInvoiceHistory.DataBind();
            }
        }

        protected void gvInvoiceHistory_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = gvInvoiceHistory.Rows[index];
            int id = Convert.ToInt32(row.Cells[0].Text);

            DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
            int status = Convert.ToInt32(ddlStatus.SelectedValue);

            UpdateInvoiceStatus(id, status);
        }

        private void UpdateInvoiceStatus(int id, int status)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE invoice_history SET status = @status, action_at = GETDATE(), action_by = @userId WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("@status", status);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@userId", 1); // Replace with logged-in user ID
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
            LoadInvoiceHistory();
        }
    }
}
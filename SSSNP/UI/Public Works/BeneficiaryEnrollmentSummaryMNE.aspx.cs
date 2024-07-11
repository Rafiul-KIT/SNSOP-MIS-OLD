using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI;

public partial class BeneficiaryEnrollmentSummaryMNE : Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Log("Page_Load started.");
            if (!IsPostBack)
            {
                Log("IsPostBack is false.");
                // You can add any initial data loading here if needed
            }
        }
        catch (Exception ex)
        {
            LogError("Error in Page_Load: " + ex.Message, ex);
            throw;
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Log("BtnSearch_Click started.");
            rgResults.Rebind();
            Log("Search button clicked.");
        }
        catch (Exception ex)
        {
            LogError("Error in BtnSearch_Click: " + ex.Message, ex);
            throw;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Implement your logic to load counties based on selected state
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Implement your logic to load payams based on selected county
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Implement your logic to load bomas based on selected payam
    }

    protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        try
        {
            Log("rgResults_NeedDataSource started.");
            string state = ddlState.SelectedValue;
            string county = ddlCounty.SelectedValue;
            string payam = ddlPayam.SelectedValue;
            string boma = ddlBoma.SelectedValue;
            string age = txtAge.Text;
            string gender = ddlGender.SelectedValue;
            string component = ddlComponent.SelectedValue;

            DataTable dt = GetEnrolledBeneficiaries(state, county, payam, boma, age, gender, component);
            rgResults.DataSource = dt;
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    private DataTable GetEnrolledBeneficiaries(string state, string county, string payam, string boma, string age, string gender, string component)
    {
        DataTable dt = new DataTable();
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand("GetEnrolledBeneficiaries", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@State", string.IsNullOrEmpty(state) ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@County", string.IsNullOrEmpty(county) ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@Payam", string.IsNullOrEmpty(payam) ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@Boma", string.IsNullOrEmpty(boma) ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(age) ? (object)DBNull.Value : (object)int.Parse(age));
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
                    cmd.Parameters.AddWithValue("@Component", string.IsNullOrEmpty(component) ? (object)DBNull.Value : component);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Error in GetEnrolledBeneficiaries: " + ex.Message, ex);
            throw;
        }

        return dt;
    }

    private void Log(string message)
    {
        // Implement your logging logic here
        System.Diagnostics.Debug.WriteLine(message);
    }

    private void LogError(string message, Exception ex)
    {
        // Implement your error logging logic here
        System.Diagnostics.Debug.WriteLine(message);
        // Example: ErrorLogger.LogError(ex);
    }
}

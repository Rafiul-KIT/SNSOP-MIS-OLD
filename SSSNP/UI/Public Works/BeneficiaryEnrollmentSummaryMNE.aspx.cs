using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BeneficiaryEnrollmentSummaryMNE : Page
{
    private LookupManagerService _lookupManagerService;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            Log("Page_Load started.");
            InitializeServices();
            if (!IsPostBack)
            {
                Log("IsPostBack is false.");
                LoadStateDropdown();
                LoadGenderDropdown();
            }
        }
        catch (Exception ex)
        {
            LogError("Error in Page_Load: " + ex.Message, ex);
            throw;
        }
    }

    private void InitializeServices()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string 'SNSDPSkils' is not found or empty.");
            }

            _lookupManagerService = new LookupManagerService(connectionString);
        }
        catch (Exception ex)
        {
            LogError(new Exception("Error initializing services: " + ex.Message, ex));
        }
    }

    private void LogError(Exception exception)
    {
        throw new NotImplementedException();
    }

    private void LoadStateDropdown()
    {
        string query = "SELECT intStateid, StateName FROM tm_States";
        LoadDropdownData(ddlState, query, "StateName", "intStateid");
    }

    private void LoadCountyDropdown(int stateId)
    {
        string query = "SELECT intCountieid, CountieName FROM tm_Counties WHERE intStateid = @StateId";
        LoadDropdownData(ddlCounty, query, "CountieName", "intCountieid", new SqlParameter("@StateId", stateId));
    }

    private void LoadPayamDropdown(int countyId)
    {
        string query = "SELECT intPayamid, PayamName FROM tm_Payams WHERE intCountieid = @CountyId";
        LoadDropdownData(ddlPayam, query, "PayamName", "intPayamid", new SqlParameter("@CountyId", countyId));
    }

    private void LoadBomaDropdown(int payamId)
    {
        string query = "SELECT intBomasid, BomaName FROM tm_Bomas WHERE intPayamid = @PayamId";
        LoadDropdownData(ddlBoma, query, "BomaName", "intBomasid", new SqlParameter("@PayamId", payamId));
    }

    private void LoadGenderDropdown()
    {
        ddlGender.Items.Add(new ListItem("Male", "M"));
        ddlGender.Items.Add(new ListItem("Female", "F"));
    }

    private void LoadDropdownData(DropDownList dropdown, string query, string textField, string valueField, params SqlParameter[] parameters)
    {
        try
        {
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        cmd.Parameters.AddRange(parameters);
                    }
                    conn.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dropdown.DataSource = dr;
                        dropdown.DataTextField = textField;
                        dropdown.DataValueField = valueField;
                        dropdown.DataBind();
                    }
                }
            }
            dropdown.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch (Exception ex)
        {
            LogError("Error in LoadDropdownData: " + ex.Message, ex);
            throw;
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        int stateId;
        if (int.TryParse(ddlState.SelectedValue, out stateId))
        {
            LoadCountyDropdown(stateId);
        }
        ddlCounty.SelectedIndex = 0;
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        int countyId;
        if (int.TryParse(ddlCounty.SelectedValue, out countyId))
        {
            LoadPayamDropdown(countyId);
        }
        ddlPayam.SelectedIndex = 0;
        ddlBoma.Items.Clear();
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        int payamId;
        if (int.TryParse(ddlPayam.SelectedValue, out payamId))
        {
            LoadBomaDropdown(payamId);
        }
        ddlBoma.SelectedIndex = 0;
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

            DataTable dt = GetEnrolledBeneficiaries(state, county, payam, boma, age, gender);
            rgResults.DataSource = dt;
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    private DataTable GetEnrolledBeneficiaries(string state, string county, string payam, string boma, string age, string gender)
    {
        DataTable dt = new DataTable();
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                string query = "SELECT intBenid AS BeneficiaryId, EnrollmentNo, NameofHousehold, FamilySize, Sex AS Gender, Age, " +
                               "intStateid, intCountieid, intPayamid, intBomasid, WorkCode AS Component, Created_dt AS EnrollmentDate " +
                               "FROM tbl_BeneficiaryDet " +
                               "WHERE (@StateId IS NULL OR @StateId = '' OR intStateid = @StateId) " +
                               "AND (@CountyId IS NULL OR @CountyId = '' OR intCountieid = @CountyId) " +
                               "AND (@PayamId IS NULL OR @PayamId = '' OR intPayamid = @PayamId) " +
                               "AND (@BomaId IS NULL OR @BomaId = '' OR intBomasid = @BomaId) " +
                               "AND (@Age IS NULL OR @Age = '' OR Age = @Age) " +
                               "AND (@Gender IS NULL OR @Gender = '' OR Sex = @Gender)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    int parsedValue;
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(age) ? (object)DBNull.Value : age);
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);

                    Log($"Executing query: {query} with parameters: " +
                        $"StateId = {state}, CountyId = {county}, PayamId = {payam}, BomaId = {boma}, Age = {age}, Gender = {gender}");

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Error in BeneficiaryEnrollmentSummary: " + ex.Message, ex);
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

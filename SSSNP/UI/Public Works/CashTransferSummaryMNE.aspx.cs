using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CashTransferSummaryMNE : Page
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
                LoadInitialData();
                rgResults.Visible = false; // Initially hide the grid
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

    private void LoadInitialData()
    {
        LoadDropdownData(ddlState, "SELECT intStateid, StateName FROM tm_States", "StateName", "intStateid");
        /*LoadDropdownData(ddlCurrency, "SELECT DISTINCT Currency FROM td_WorkWageRequest", "Currency", "Currency");
        LoadDropdownData(ddlPaymentPartner, "SELECT intPayingAgentid, AgentName FROM tm_PayingAgents", "AgentName", "intPayingAgentid");
*/
        // Populate fixed dropdowns
        ddlPaymentCycle.Items.Add(new ListItem("--Select--", ""));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 1", "1"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 2", "2"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 3", "3"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 4", "4"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 5", "5"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 6", "6"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 7", "7"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 8", "8"));
        ddlPaymentCycle.Items.Add(new ListItem("Payment Cycle 9", "9"));

        ddlStatus.Items.Add(new ListItem("--Select--", ""));
        ddlStatus.Items.Add(new ListItem("IP Applied", ""));
        ddlStatus.Items.Add(new ListItem("IP Approver Approved", "1"));
        ddlStatus.Items.Add(new ListItem("Initial Reviewer Approved", "2"));
        ddlStatus.Items.Add(new ListItem("Finantial Approver Approved", "3"));
        ddlStatus.Items.Add(new ListItem("Final Payment Authorizer Approved", "4"));
        ddlStatus.Items.Add(new ListItem("Reconciled", "5"));
        ddlStatus.Items.Add(new ListItem("IP Approver Rejected", "11"));
        ddlStatus.Items.Add(new ListItem("Initial Reviewer Rejected", "12"));
        ddlStatus.Items.Add(new ListItem("Finantial Approver Rejected", "13"));
        ddlStatus.Items.Add(new ListItem("Final Payment Authorizer Rejected", "14"));

        ddlComponent.Items.Add(new ListItem("--Select--", ""));
        ddlComponent.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlComponent.Items.Add(new ListItem("DIS", "DIS"));
    }

    private void LoadDropdownData(DropDownList ddl, string query, string textField, string valueField)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            conn.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("--Select--", ""));
            while (reader.Read())
            {
                ddl.Items.Add(new ListItem(reader[textField].ToString(), reader[valueField].ToString()));
            }
            reader.Close();
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCounty.Items.Clear();
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();

        ddlCounty.Items.Add(new ListItem("--Select--", ""));
        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        ddlBoma.Items.Add(new ListItem("--Select--", ""));

        if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "--Select--")
        {
            int stateId;
            if (int.TryParse(ddlState.SelectedValue, out stateId))
            {
                try
                {
                    LoadDropdownData(ddlCounty, $"SELECT intCountieid, CountyName FROM tm_Counties WHERE intStateid = {stateId}", "CountyName", "intCountieid");
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();

        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        ddlBoma.Items.Add(new ListItem("--Select--", ""));

        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue) && ddlCounty.SelectedValue != "--Select--")
        {
            int countyId;
            if (int.TryParse(ddlCounty.SelectedValue, out countyId))
            {
                try
                {
                    LoadDropdownData(ddlPayam, $"SELECT intPayamid, PayamName FROM tm_Payams WHERE intCountieid = {countyId}", "PayamName", "intPayamid");
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBoma.Items.Clear();
        ddlBoma.Items.Add(new ListItem("--Select--", ""));

        if (!string.IsNullOrEmpty(ddlPayam.SelectedValue) && ddlPayam.SelectedValue != "--Select--")
        {
            int payamId;
            if (int.TryParse(ddlPayam.SelectedValue, out payamId))
            {
                try
                {
                    LoadDropdownData(ddlBoma, $"SELECT intBomasid, BomaName FROM tm_Bomas WHERE intPayamid = {payamId}", "BomaName", "intBomasid");
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }
    }

    private void LogError(Exception ex)
    {
        throw new NotImplementedException();
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            Log("BtnSearch_Click started.");

            rgResults.Visible = true; // Show the grid when the search button is clicked

            // Initialize total record count
            string state = ddlState.SelectedValue;
            string county = ddlCounty.SelectedValue;
            string payam = ddlPayam.SelectedValue;
            string boma = ddlBoma.SelectedValue;
            string paymentCycle = ddlPaymentCycle.SelectedValue;
            string Status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;

            Session["TotalRecordCount"] = GetTotalPaymentRecordsCount(state, county, payam, boma, paymentCycle, Status, component);

            rgResults.CurrentPageIndex = 0; // Reset to the first page
            rgResults.Rebind();
            Log("Search button clicked.");
        }
        catch (Exception ex)
        {
            LogError("Error in BtnSearch_Click: " + ex.Message, ex);
            throw;
        }
    }

    private int GetTotalPaymentRecordsCount(string state, string county, string payam, string boma, string paymentCycle, string Status, string component)
    {
        int totalRecords = 0;
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                string query = @"
                SELECT COUNT(*)
                FROM tr_BenWagesPayment p
                INNER JOIN td_WorkWageRequest w ON p.intWorkid = w.intWorkid
                INNER JOIN tbl_WorkMaster m ON w.intWorkid = m.intWorkid
                WHERE (@StateId IS NULL OR @StateId = '' OR m.intStateid = @StateId)
                AND (@CountyId IS NULL OR @CountyId = '' OR m.intCountieid = @CountyId)
                AND (@PayamId IS NULL OR @PayamId = '' OR m.intPayamid = @PayamId)
                AND (@BomaId IS NULL OR @BomaId = '' OR m.intBomasid = @BomaId)
                AND (@PaymentCycle IS NULL OR @PaymentCycle = '' OR w.PaymentCycle = @PaymentCycle)
                AND (@Status IS NULL OR @Status = '' OR w.Status = @Status)
                AND (@Component IS NULL OR @Component = '' OR w.Component = @Component);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@PaymentCycle", string.IsNullOrEmpty(paymentCycle) ? (object)DBNull.Value : paymentCycle);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(Status) ? (object)DBNull.Value : Status);
                    cmd.Parameters.AddWithValue("@Component", string.IsNullOrEmpty(component) || component == "--Select--" ? (object)DBNull.Value : component);
                    
                    totalRecords = (int)cmd.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Error in GetTotalPaymentRecordsCount: " + ex.Message, ex);
            throw;
        }

        return totalRecords;
    }

    protected void BtnDownload_Click(object sender, EventArgs e)
    { 
        
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
            string paymentCycle = ddlPaymentCycle.SelectedValue;
            string Status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;

            int totalRecords = (int)Session["TotalRecordCount"]; // Use cached total record count
            DataTable dt = GetPaymentData(state, county, payam, boma, paymentCycle, Status, component, rgResults.CurrentPageIndex, rgResults.PageSize);
            rgResults.DataSource = dt;
            rgResults.VirtualItemCount = totalRecords; // Set the total number of records
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    private DataTable GetPaymentData(string state, string county, string payam, string boma, string paymentCycle, string Status, string component, int pageIndex, int pageSize)
    {
        DataTable dt = new DataTable();
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                string query = @"
                SELECT p.intWageid AS intPaymentId,
                       CONCAT(s.StateName, ' / ', c.CountyName, ' / ', y.PayamName, ' / ', b.BomaName) AS geoLocation,
                       w.Currency,
                       w.PaymentCycle,
                       CASE WHEN p.PaidAmount IS NOT NULL THEN 'Paid' ELSE 'Not Paid' END AS paymentStatus,
                       w.Component,
                       a.AgentName AS paymentPartner,
                       p.PaidAmount AS paymentAmount,
                       p.PaidDate AS paymentDate
                FROM tr_BenWagesPayment p
                INNER JOIN td_WorkWageRequest w ON p.intWorkid = w.intWorkid
                INNER JOIN tbl_WorkMaster m ON w.intWorkid = m.intWorkid
                INNER JOIN tm_States s ON m.intStateid = s.intStateid
                INNER JOIN tm_Counties c ON m.intCountieid = c.intCountieid
                INNER JOIN tm_Payams y ON m.intPayamid = y.intPayamid
                INNER JOIN tm_Bomas b ON m.intBomasid = b.intBomasid
                INNER JOIN tm_PayingAgents a ON w.intPayingAgentid = a.intPayingAgentid
                WHERE (@StateId IS NULL OR @StateId = '' OR m.intStateid = @StateId)
                AND (@CountyId IS NULL OR @CountyId = '' OR m.intCountieid = @CountyId)
                AND (@PayamId IS NULL OR @PayamId = '' OR m.intPayamid = @PayamId)
                AND (@BomaId IS NULL OR @BomaId = '' OR m.intBomasid = @BomaId)
                AND (@Currency IS NULL OR @Currency = '' OR w.Currency = @Currency)
                AND (@PaymentCycle IS NULL OR @PaymentCycle = '' OR w.PaymentCycle = @PaymentCycle)
                AND (@PaymentStatus IS NULL OR @PaymentStatus = '' OR (CASE WHEN p.PaidAmount IS NOT NULL THEN 'Paid' ELSE 'Not Paid' END) = @PaymentStatus)
                AND (@Component IS NULL OR @Component = '' OR w.Component = @Component)
                AND (@PaymentPartner IS NULL OR @PaymentPartner = '' OR w.intPayingAgentid = @PaymentPartner)
                ORDER BY p.intWageid
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@PaymentCycle", string.IsNullOrEmpty(paymentCycle) ? (object)DBNull.Value : paymentCycle);
                    cmd.Parameters.AddWithValue("@PaymentStatus", string.IsNullOrEmpty(Status) ? (object)DBNull.Value : Status);
                    cmd.Parameters.AddWithValue("@Component", string.IsNullOrEmpty(component) || component == "--Select--" ? (object)DBNull.Value : component);
                    cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                    cmd.Parameters.AddWithValue("@PageSize", pageSize);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Error in GetPaymentData: " + ex.Message, ex);
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

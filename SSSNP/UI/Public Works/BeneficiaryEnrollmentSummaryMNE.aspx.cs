using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;

public partial class BeneficiaryEnrollmentSummaryMNE : Page
{
    private LookupManagerService _lookupManagerService;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            InitializeServices();
            if (!IsPostBack)
            {
                LoadStates();
                LoadGenderDropdown();
                LoadComponentDropdown();
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

    private void LogError(Exception exception)
    {
        throw new NotImplementedException();
    }

    private void LoadStates()
    {
        try
        {
            DataTable dtStates = _lookupManagerService.GetStateList();
            ddlState.DataSource = dtStates;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "intStateid";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    private void LoadGenderDropdown()
    {
        ddlGender.Items.Add(new ListItem("Male", "M"));
        ddlGender.Items.Add(new ListItem("Female", "F"));
        ddlGender.Items.Insert(0, new ListItem("--Select--", ""));
    }

    private void LoadComponentDropdown()
    {
        try
        {
            ddlComponent.Items.Add(new ListItem("LIPW", "Component 1 (Public Works)"));
            ddlComponent.Items.Add(new ListItem("DIS", "Component 1 (Direct Income Support)"));
            ddlComponent.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch (Exception ex)
        {
            LogError(ex);
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
                    DataTable dtCounties = _lookupManagerService.GetCountyListByStateId(stateId);
                    ddlCounty.DataSource = dtCounties;
                    ddlCounty.DataTextField = "CountieName";
                    ddlCounty.DataValueField = "intCountieid";
                    ddlCounty.DataBind();
                    ddlCounty.Items.Insert(0, new ListItem("--Select--", ""));
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
                    DataTable dtPayams = _lookupManagerService.GetPayamListByCountyId(countyId);
                    ddlPayam.DataSource = dtPayams;
                    ddlPayam.DataTextField = "PayamName";
                    ddlPayam.DataValueField = "intPayamid";
                    ddlPayam.DataBind();
                    ddlPayam.Items.Insert(0, new ListItem("--Select--", ""));
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
                    DataTable dtBomas = _lookupManagerService.GetBomaListByPayamId(payamId);
                    ddlBoma.DataSource = dtBomas;
                    ddlBoma.DataTextField = "BomaName";
                    ddlBoma.DataValueField = "intBomasid";
                    ddlBoma.DataBind();
                    ddlBoma.Items.Insert(0, new ListItem("--Select--", ""));
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            int parsedAge;
            if (!string.IsNullOrEmpty(txtAge.Text) && !int.TryParse(txtAge.Text, out parsedAge))
            {
                // Show a warning message
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Please enter a valid integer for Age.');", true);
                return;
            }

            rgResults.Visible = true; // Show the grid when the search button is clicked

            // Initialize total record count
            string state = ddlState.SelectedValue;
            string county = ddlCounty.SelectedValue;
            string payam = ddlPayam.SelectedValue;
            string boma = ddlBoma.SelectedValue;
            string age = txtAge.Text;
            string gender = ddlGender.SelectedValue;
            string component = ddlComponent.SelectedValue;

            Session["TotalRecordCount"] = GetTotalEnrolledBeneficiariesCount(state, county, payam, boma, age, gender, component);

            rgResults.CurrentPageIndex = 0; // Reset to the first page
            rgResults.Rebind();

            BtnDownloadExl.Visible = (int)Session["TotalRecordCount"] > 0;
            BtnDownloadPdf.Visible = (int)Session["TotalRecordCount"] > 0;
        }
        catch (Exception ex)
        {
            LogError("Error in BtnSearch_Click: " + ex.Message, ex);
            throw;
        }
    }

    protected void BtnDownload_Pdf(object sender, EventArgs e)
    {
        try
        {
            string state = ddlState.SelectedValue;
            string county = ddlCounty.SelectedValue;
            string payam = ddlPayam.SelectedValue;
            string boma = ddlBoma.SelectedValue;
            string age = txtAge.Text;
            string gender = ddlGender.SelectedValue;
            string component = ddlComponent.SelectedValue;

            DataTable totaldt = GetAllEnrolledBeneficiaries(state, county, payam, boma, age, gender, component);//to store total data
            Session["ReportPdf"] = totaldt; // Store the data in session

            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=11";
            Response.Redirect(redirectUrl, false); // Redirect to handler with the type for cash transfer summary
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            LogError("Error in BtnDownloadPdf: " + ex.Message, ex);
            throw;
        }
    }
    protected void BtnDownload_Exl(object sender, EventArgs e)
    {
        try
        {
            string state = ddlState.SelectedValue;
            string county = ddlCounty.SelectedValue;
            string payam = ddlPayam.SelectedValue;
            string boma = ddlBoma.SelectedValue;
            string age = txtAge.Text;
            string gender = ddlGender.SelectedValue;
            string component = ddlComponent.SelectedValue;

            DataTable totaldt = GetAllEnrolledBeneficiaries(state, county, payam, boma, age, gender, component);//to store total data
            Session["ReportExl"] = totaldt; // Store the data in session

            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=12";
            Response.Redirect(redirectUrl, false); // Redirect to handler with the type for cash transfer summary
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            LogError("Error in BtnDownload_Click: " + ex.Message, ex);
            throw;
        }
    }


    //Add Method to Get Total Record Count
    private int GetTotalEnrolledBeneficiariesCount(string state, string county, string payam, string boma, string age, string gender, string component)
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
                FROM tbl_BeneficiaryDet
                WHERE (@StateId IS NULL OR @StateId = '' OR intStateid = @StateId)
                AND (@CountyId IS NULL OR @CountyId = '' OR intCountieid = @CountyId)
                AND (@PayamId IS NULL OR @PayamId = '' OR intPayamid = @PayamId)
                AND (@BomaId IS NULL OR @BomaId = '' OR intBomasid = @BomaId)
                AND (@Age IS NULL OR @Age = '' OR Age = @Age)
                AND (@Gender IS NULL OR @Gender = '' OR Sex = @Gender)
                AND (@Component IS NULL OR @Component = '' OR SelectionCriteria = @Component);";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(age) ? (object)DBNull.Value : age);
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
                    cmd.Parameters.AddWithValue("@Component", string.IsNullOrEmpty(component) || component == "--Select--" ? (object)DBNull.Value : component);

                    totalRecords = (int)cmd.ExecuteScalar();
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Error in GetTotalEnrolledBeneficiariesCount: " + ex.Message, ex);
            throw;
        }

        return totalRecords;
    }

    //Update rgResults_NeedDataSource to Use Cached Total Record Count
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

            int totalRecords = (int)Session["TotalRecordCount"]; // Use cached total record count
            DataTable dt = GetEnrolledBeneficiaries(state, county, payam, boma, age, gender, component, rgResults.CurrentPageIndex, rgResults.PageSize);
            rgResults.DataSource = dt;
            rgResults.VirtualItemCount = totalRecords; // Set the total number of records
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    // Update GetEnrolledBeneficiaries to Fetch Only Page Data
    private DataTable GetEnrolledBeneficiaries(string state, string county, string payam, string boma, string age, string gender, string component, int pageIndex, int pageSize)
    {
        DataTable dt = new DataTable();
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                string query = @"
                SELECT 
                    b.intBenid AS BeneficiaryId, 
                    b.EnrollmentNo, 
                    b.NameofHousehold, 
                    b.FamilySize, 
                    b.Sex AS Gender, 
                    b.Age,
                    s.StateName,
                    c.CountieName,
                    p.PayamName,
                    bm.BomaName,
                    b.SelectionCriteria AS Component, 
                    b.Created_dt AS EnrollmentDate
                FROM tbl_BeneficiaryDet b
                LEFT JOIN tm_States s ON b.intStateid = s.intStateid
                LEFT JOIN tm_Counties c ON b.intCountieid = c.intCountieid
                LEFT JOIN tm_Payams p ON b.intPayamid = p.intPayamid
                LEFT JOIN tm_Bomas bm ON b.intBomasid = bm.intBomasid
                WHERE (@StateId IS NULL OR @StateId = '' OR b.intStateid = @StateId)
                AND (@CountyId IS NULL OR @CountyId = '' OR b.intCountieid = @CountyId)
                AND (@PayamId IS NULL OR @PayamId = '' OR b.intPayamid = @PayamId)
                AND (@BomaId IS NULL OR @BomaId = '' OR b.intBomasid = @BomaId)
                AND (@Age IS NULL OR @Age = '' OR b.Age = @Age)
                AND (@Gender IS NULL OR @Gender = '' OR b.Sex = @Gender)
                AND (@Component IS NULL OR @Component = '' OR b.SelectionCriteria = @Component)
                ORDER BY b.intBenid
                OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(age) ? (object)DBNull.Value : age);
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
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
            LogError("Error in GetEnrolledBeneficiaries: " + ex.Message, ex);
            throw;
        }

        return dt;
    }


    private DataTable GetAllEnrolledBeneficiaries(string state, string county, string payam, string boma, string age, string gender, string component)
    {
        DataTable dt = new DataTable();
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                string query = @"
                SELECT 
                    b.intBenid AS BeneficiaryId, 
                    b.EnrollmentNo, 
                    b.NameofHousehold, 
                    b.FamilySize, 
                    b.Sex AS Gender, 
                    b.Age,
                    s.StateName,
                    c.CountieName,
                    p.PayamName,
                    bm.BomaName,
                    b.SelectionCriteria AS Component, 
                    b.Created_dt AS EnrollmentDate
                FROM tbl_BeneficiaryDet b
                LEFT JOIN tm_States s ON b.intStateid = s.intStateid
                LEFT JOIN tm_Counties c ON b.intCountieid = c.intCountieid
                LEFT JOIN tm_Payams p ON b.intPayamid = p.intPayamid
                LEFT JOIN tm_Bomas bm ON b.intBomasid = bm.intBomasid
                WHERE (@StateId IS NULL OR @StateId = '' OR b.intStateid = @StateId)
                AND (@CountyId IS NULL OR @CountyId = '' OR b.intCountieid = @CountyId)
                AND (@PayamId IS NULL OR @PayamId = '' OR b.intPayamid = @PayamId)
                AND (@BomaId IS NULL OR @BomaId = '' OR b.intBomasid = @BomaId)
                AND (@Age IS NULL OR @Age = '' OR b.Age = @Age)
                AND (@Gender IS NULL OR @Gender = '' OR b.Sex = @Gender)
                AND (@Component IS NULL OR @Component = '' OR b.SelectionCriteria = @Component)
                ORDER BY intBenid;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@Age", string.IsNullOrEmpty(age) ? (object)DBNull.Value : age);
                    cmd.Parameters.AddWithValue("@Gender", string.IsNullOrEmpty(gender) ? (object)DBNull.Value : gender);
                    cmd.Parameters.AddWithValue("@Component", string.IsNullOrEmpty(component) || component == "--Select--" ? (object)DBNull.Value : component);

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

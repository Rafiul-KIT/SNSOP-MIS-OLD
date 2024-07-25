using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using ListItem = System.Web.UI.WebControls.ListItem;
using System.Web;
using System.Collections.Generic;

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

        // Populate fixed dropdowns for Payment Cycle and Status
        ddlPaymentCycle.Items.Add(new ListItem("--Select--", ""));
        for (int i = 1; i <= 9; i++)
        {
            ddlPaymentCycle.Items.Add(new ListItem($"Payment Cycle {i}", i.ToString()));
        }

        ddlStatus.Items.Add(new ListItem("--Select--", ""));
        ddlStatus.Items.Add(new ListItem("IP Applied", ""));
        ddlStatus.Items.Add(new ListItem("IP Approver Approved", "1"));
        ddlStatus.Items.Add(new ListItem("Initial Reviewer Approved", "2"));
        ddlStatus.Items.Add(new ListItem("Financial Approver Approved", "3"));
        ddlStatus.Items.Add(new ListItem("Final Payment Authorizer Approved", "4"));
        ddlStatus.Items.Add(new ListItem("Reconciled", "5"));
        ddlStatus.Items.Add(new ListItem("IP Approver Rejected", "11"));
        ddlStatus.Items.Add(new ListItem("Initial Reviewer Rejected", "12"));
        ddlStatus.Items.Add(new ListItem("Financial Approver Rejected", "13"));
        ddlStatus.Items.Add(new ListItem("Final Payment Authorizer Rejected", "14"));

        ddlComponent.Items.Add(new ListItem("--Select--", ""));
        ddlComponent.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlComponent.Items.Add(new ListItem("DIS", "DIS"));
    }

    private void LoadDropdownData(DropDownList ddl, string query, string textField, string valueField)
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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCounty.Items.Clear();
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();

        ddlCounty.Items.Add(new ListItem("--Select--", ""));
        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        ddlBoma.Items.Add(new ListItem("--Select--", ""));

        if (!string.IsNullOrEmpty(ddlState.SelectedValue))
        {
            try
            {
                int stateId;
                bool isParsed = int.TryParse(ddlState.SelectedValue, out stateId);
                if (isParsed)
                {
                    DataTable dtCounties = _lookupManagerService.GetCountyListByStateId(stateId);
                    ddlCounty.DataSource = dtCounties;
                    ddlCounty.DataTextField = "CountieName";
                    ddlCounty.DataValueField = "intCountieid";
                    ddlCounty.DataBind();
                    ddlCounty.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    LogError(new Exception("ddlState.SelectedValue is not a valid integer: " + ddlState.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();

        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        ddlBoma.Items.Add(new ListItem("--Select--", ""));

        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            try
            {
                int countyId;
                bool isParsed = int.TryParse(ddlCounty.SelectedValue, out countyId);
                if (isParsed)
                {
                    DataTable dtPayams = _lookupManagerService.GetPayamListByCountyId(countyId);
                    ddlPayam.DataSource = dtPayams;
                    ddlPayam.DataTextField = "PayamName";
                    ddlPayam.DataValueField = "intPayamid";
                    ddlPayam.DataBind();
                    ddlPayam.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    LogError(new Exception("ddlCounty.SelectedValue is not a valid integer: " + ddlCounty.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBoma.Items.Clear();
        ddlBoma.Items.Add(new ListItem("--Select--", ""));
        if (!string.IsNullOrEmpty(ddlPayam.SelectedValue))
        {
            try
            {
                int payamId;
                bool isParsed = int.TryParse(ddlPayam.SelectedValue, out payamId);
                if (isParsed)
                {
                    DataTable dtBomas = _lookupManagerService.GetBomaListByPayamId(payamId);
                    ddlBoma.DataSource = dtBomas;
                    ddlBoma.DataTextField = "BomaName";
                    ddlBoma.DataValueField = "intBomasid";
                    ddlBoma.DataBind();
                    ddlBoma.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    LogError(new Exception("ddlPayam.SelectedValue is not a valid integer: " + ddlPayam.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }

    /*private void LoadDropdownData(DropDownList ddl, string query, string textField, string valueField, int parameterValue)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
        {
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@stateId", parameterValue);
            cmd.Parameters.AddWithValue("@countyId", parameterValue);
            cmd.Parameters.AddWithValue("@payamId", parameterValue);

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
    }*/

    private void LogError(Exception ex)
    {
        // Log error details to a file, event log, or database.
        System.Diagnostics.Debug.WriteLine(ex.ToString());
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
            string status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;

            // Store search parameters in session
            var searchParams = new Dictionary<string, string>
        {
            { "state", state },
            { "county", county },
            { "payam", payam },
            { "boma", boma },
            { "paymentCycle", paymentCycle },
            { "status", status },
            { "component", component }
        };
            Session["CashTransferSummarySearchParams"] = searchParams;

            DataTable dt = GetPaymentData(state, county, payam, boma, paymentCycle, status, component, 0, rgResults.PageSize);
            Session["CashTransferSummaryData"] = dt; // Store the data in session

            Session["TotalRecordCount"] = dt.Rows.Count;

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

    private int GetTotalPaymentRecordsCount(string state, string county, string payam, string boma, string paymentCycle, string status, string component)
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
                FROM td_WorkWageRequest w
                INNER JOIN tbl_BeneficiaryDet b ON w.intWagePaymentReqid = b.intBenid
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
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(status) ? (object)DBNull.Value : status);
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
        try
        {
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=11";
            Response.Redirect(redirectUrl, false); // Redirect to handler with the type for cash transfer summary
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            LogError("Error in BtnDownload_Click: " + ex.Message, ex);
            throw;
        }
    }

    private void GeneratePDF(DataTable dt)
    {
        using (MemoryStream ms = new MemoryStream())
        {
            Document document = new Document();
            PdfWriter.GetInstance(document, ms);
            document.Open();
            PdfPTable table = new PdfPTable(dt.Columns.Count);

            // Add the headers
            foreach (DataColumn column in dt.Columns)
            {
                table.AddCell(new Phrase(column.ColumnName));
            }

            // Add the data
            foreach (DataRow row in dt.Rows)
            {
                foreach (DataColumn column in dt.Columns)
                {
                    table.AddCell(new Phrase(row[column].ToString()));
                }
            }

            document.Add(table);
            document.Close();

            // Ensure the memory stream is at the beginning
            ms.Position = 0;

            // Clear the response before writing the PDF
            Response.Clear();
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=CashTransferSummary.pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.End();
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
            string paymentCycle = ddlPaymentCycle.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;

            int totalRecords = (int)Session["TotalRecordCount"]; // Use cached total record count
            DataTable dt = GetPaymentData(state, county, payam, boma, paymentCycle, status, component, rgResults.CurrentPageIndex, rgResults.PageSize);
            rgResults.DataSource = dt;
            rgResults.VirtualItemCount = totalRecords; // Set the total number of records
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    private DataTable GetPaymentData(string state, string county, string payam, string boma, string paymentCycle, string status, string component, int pageIndex, int pageSize)
    {
        DataTable dt = new DataTable();
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                string query = @"
            SELECT w.intWagePaymentReqid AS intPaymentId,
                   w.PaymentCycle,
                   w.Status,
                   w.Component,
                   bw.PaidAmountUSD AS paymentAmount,
                   bw.PaidDate AS paymentDate
            FROM td_WorkWageRequest w
            INNER JOIN tbl_WorkMaster m ON w.intWorkid = m.intWorkid
            INNER JOIN tm_States s ON m.intStateid = s.intStateid
            INNER JOIN tm_Counties c ON m.intCountieid = c.intCountieid
            INNER JOIN tm_Payams p ON m.intPayamid = p.intPayamid
            INNER JOIN tm_Bomas b ON m.intBomasid = b.intBomasid
            LEFT JOIN tr_BenWagesPayment bw ON w.intWagePaymentReqid = bw.intWageid
            WHERE (@StateId IS NULL OR @StateId = '' OR m.intStateid = @StateId)
            AND (@CountyId IS NULL OR @CountyId = '' OR m.intCountieid = @CountyId)
            AND (@PayamId IS NULL OR @PayamId = '' OR m.intPayamid = @PayamId)
            AND (@BomaId IS NULL OR @BomaId = '' OR m.intBomasid = @BomaId)
            AND (@PaymentCycle IS NULL OR @PaymentCycle = '' OR w.PaymentCycle = @PaymentCycle)
            AND (@Status IS NULL OR @Status = '' OR w.Status = @Status)
            AND (@Component IS NULL OR @Component = '' OR w.Component = @Component)
            ORDER BY w.intWagePaymentReqid
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 120; // Set the command timeout to 120 seconds
                    cmd.Parameters.AddWithValue("@StateId", string.IsNullOrEmpty(state) || state == "--Select--" ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", string.IsNullOrEmpty(county) || county == "--Select--" ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", string.IsNullOrEmpty(payam) || payam == "--Select--" ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", string.IsNullOrEmpty(boma) || boma == "--Select--" ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@PaymentCycle", string.IsNullOrEmpty(paymentCycle) ? (object)DBNull.Value : paymentCycle);
                    cmd.Parameters.AddWithValue("@Status", string.IsNullOrEmpty(status) ? (object)DBNull.Value : status);
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

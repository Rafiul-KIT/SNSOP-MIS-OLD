using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using ListItem = System.Web.UI.WebControls.ListItem;

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
        LoadDropdownData();

        // Populate fixed dropdowns for Payment Cycle and Status
        ddlPaymentCycle.Items.Add(new ListItem("--Select--", ""));
        for (int i = 1; i <= 9; i++)
        {
            ddlPaymentCycle.Items.Add(new ListItem($"Payment Cycle {i}", i.ToString()));
        }

        ddlStatus.Items.Add(new ListItem("--Select--", ""));
        ddlStatus.Items.Add(new ListItem("IP Applied", "0"));
        ddlStatus.Items.Add(new ListItem("IP Approver Approved", "1"));
        ddlStatus.Items.Add(new ListItem("Initial Reviewer Approved", "2"));
        ddlStatus.Items.Add(new ListItem("Financial Approver Approved", "3"));
        ddlStatus.Items.Add(new ListItem("Final Payment Authorizer Approved", "4"));
        ddlStatus.Items.Add(new ListItem("Reconciled", "6"));
        ddlStatus.Items.Add(new ListItem("IP Approver Rejected", "105"));
        ddlStatus.Items.Add(new ListItem("Initial Reviewer Rejected", "100"));
        ddlStatus.Items.Add(new ListItem("Financial Approver Rejected", "101"));
        ddlStatus.Items.Add(new ListItem("Final Payment Authorizer Rejected", "102"));

        ddlComponent.Items.Add(new ListItem("--Select--", ""));
        ddlComponent.Items.Add(new ListItem("LIPW", "Component 1 (Public Works)"));
        ddlComponent.Items.Add(new ListItem("DIS", "Component 2 (Direct Income Support)"));

        ddlPaidStatus.Items.Add(new ListItem("--Select--", ""));
        ddlPaidStatus.Items.Add(new ListItem("Paid", "Paid"));
        ddlPaidStatus.Items.Add(new ListItem("Unpaid", "Unpaid"));
    }

    private void LoadDropdownData()
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

    private void LogError(Exception ex)
    {
        // Log error details to a file, event log, or database.
        System.Diagnostics.Debug.WriteLine(ex.ToString());
    }

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            rgResults.Visible = true; // Show the grid when the search button is clicked

            // Get total record count
            string state = ddlState.SelectedValue;
            string county = ddlCounty.SelectedValue;
            string payam = ddlPayam.SelectedValue;
            string boma = ddlBoma.SelectedValue;
            string paymentCycle = ddlPaymentCycle.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;
            string paidStatus = ddlPaidStatus.SelectedValue;

            int totalCount = GetTotalPaymentRecordsCount(state, county, payam, boma, paymentCycle, status, component, paidStatus);
            Session["TotalRecordCount"] = totalCount;

            // Declare variables to hold the totals
            int totalPaidBeneficiaries, totalUnpaidBeneficiaries;
            decimal totalPaidAmount;

            // Fetch the payment data and totals in a single call
            DataTable dt = GetAllPaymentData(state, county, payam, boma, paymentCycle, status, component, paidStatus, out totalPaidBeneficiaries, out totalUnpaidBeneficiaries, out totalPaidAmount);

            // Store the DataTable and labels inside a CashTransferSummaryData object
            var cashTransferSummaryData = new CashTransferSummaryData
            {
                ReportData = dt,
                TotalPaidBeneficiaries = totalPaidBeneficiaries,
                TotalUnpaidBeneficiaries = totalUnpaidBeneficiaries,
                TotalPaidAmount = totalPaidAmount
            };

            Session["CashTransferSummary"] = cashTransferSummaryData;

            // Update the labels with the calculated totals
            lblTotalPaidBeneficiary.Text = $"Total Paid Beneficiary: {totalPaidBeneficiaries}";
            lblTotalUnpaidBeneficiary.Text = $"Total Unpaid Beneficiary: {totalUnpaidBeneficiaries}";
            lblTotalPaidAmount.Text = $"Total Paid Amount(USD): {totalPaidAmount:N2}";

            // Show the total panel only if there are records
            pnlTotals.Visible = dt.Rows.Count > 0;

            rgResults.CurrentPageIndex = 0; // Reset to the first page
            rgResults.Rebind();

            // Show the download button if there are records
            BtnDownloadPdf.Visible = totalCount > 0;
            BtnDownloadExl.Visible = totalCount > 0;

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
            string paymentCycle = ddlPaymentCycle.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;
            string paidStatus = ddlPaidStatus.SelectedValue;

            /*// Declare variables to hold the totals
            int totalPaidBeneficiaries, totalUnpaidBeneficiaries;
            decimal totalPaidAmount;

            DataTable totaldt = GetAllPaymentData(state, county, payam, boma, paymentCycle, status, component, paidStatus, out totalPaidBeneficiaries, out totalUnpaidBeneficiaries, out totalPaidAmount);//to store total data
            Session["ReportPdf"] = totaldt; // Store the data in session
*/
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=14";
            Response.Redirect(redirectUrl, false); // Redirect to handler with the type for cash transfer summary
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            LogError("Error in BtnDownload_Pdf: " + ex.Message, ex);
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
            string paymentCycle = ddlPaymentCycle.SelectedValue;
            string status = ddlStatus.SelectedValue;
            string component = ddlComponent.SelectedValue;
            string paidStatus = ddlPaidStatus.SelectedValue;

            /*// Declare variables to hold the totals
            int totalPaidBeneficiaries, totalUnpaidBeneficiaries;
            decimal totalPaidAmount;

            DataTable totaldt = GetAllPaymentData(state, county, payam, boma, paymentCycle, status, component, paidStatus, out totalPaidBeneficiaries, out totalUnpaidBeneficiaries, out totalPaidAmount);//to store total data
            Session["ReportExl"] = totaldt; // Store the data in session
*/
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=15";
            Response.Redirect(redirectUrl, false); // Redirect to handler with the type for cash transfer summary
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
        catch (Exception ex)
        {
            LogError("Error in BtnDownload_Exl: " + ex.Message, ex);
            throw;
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        // Clear all dropdowns and fields
        ddlState.ClearSelection();
        ddlCounty.ClearSelection();
        ddlPayam.ClearSelection();
        ddlBoma.ClearSelection();
        ddlPaymentCycle.ClearSelection();
        ddlStatus.ClearSelection();
        ddlComponent.ClearSelection();
        ddlPaidStatus.ClearSelection();

        // Reset totals
        lblTotalPaidBeneficiary.Text = "Total Paid Beneficiary: 0";
        lblTotalUnpaidBeneficiary.Text = "Total Unpaid Beneficiary: 0";
        lblTotalPaidAmount.Text = "Total Paid Amount: 0.00";

        // Hide the totals panel and grid
        pnlTotals.Visible = false;
        rgResults.Visible = false;
    }

    private int GetTotalPaymentRecordsCount(string state, string county, string payam, string boma, string paymentCycle, string status, string component, string paidStatus)
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
                FROM 
                    tbl_WorkMaster wm
                INNER JOIN 
                    td_WorkWageRequest wwr ON wm.intWorkid = wwr.intWorkid
                INNER JOIN 
                    td_WageReqBenDet wbd ON wwr.intWagePaymentReqid = wbd.intWagePaymentReqid
                LEFT JOIN 
                    tr_BenWagesPayment bwp ON wbd.intBenid = bwp.intBenid AND wwr.intWorkid = bwp.intWorkid
                WHERE 
                    (@StateId IS NULL OR wm.intStateid = @StateId)
                    AND (@CountyId IS NULL OR wm.intCountieid = @CountyId)
                    AND (@PayamId IS NULL OR wm.intPayamid = @PayamId)
                    AND (@BomaId IS NULL OR wm.intBomasid = @BomaId)
                    AND (@PaymentCycle IS NULL OR wwr.PaymentCycle = @PaymentCycle)
                    AND (@Status IS NULL OR wwr.Status = @Status)
                    AND (@Component IS NULL OR wwr.Component = @Component)
                    AND (@PaidStatus IS NULL 
                             OR (@PaidStatus = 'Paid' AND bwp.PaidAmountUSD IS NOT NULL)
                             OR (@PaidStatus = 'Unpaid' AND bwp.PaidAmountUSD IS NULL));";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@StateId", (string.IsNullOrEmpty(state) || state == "--Select--") ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", (string.IsNullOrEmpty(county) || county == "--Select--") ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", (string.IsNullOrEmpty(payam) || payam == "--Select--") ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", (string.IsNullOrEmpty(boma) || boma == "--Select--") ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@PaymentCycle", (string.IsNullOrEmpty(paymentCycle) || paymentCycle == "--Select--") ? (object)DBNull.Value : paymentCycle);
                    cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(status) || status == "--Select--") ? (object)DBNull.Value : status);
                    cmd.Parameters.AddWithValue("@Component", (string.IsNullOrEmpty(component) || component == "--Select--") ? (object)DBNull.Value : component);
                    cmd.Parameters.AddWithValue("@PaidStatus", (string.IsNullOrEmpty(paidStatus) || paidStatus == "--Select--") ? (object)DBNull.Value : paidStatus);


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
            string paidStatus = ddlPaidStatus.SelectedValue;

            DataTable dt = GetPaymentData(state, county, payam, boma, paymentCycle, status, component, paidStatus, rgResults.CurrentPageIndex, rgResults.PageSize);//to store only grid data
            rgResults.DataSource = dt;
            rgResults.VirtualItemCount = (int)Session["TotalRecordCount"]; // Set the total number of records
        }
        catch (Exception ex)
        {
            LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
            throw;
        }
    }

    private DataTable GetPaymentData(string state, string county, string payam, string boma, string paymentCycle, string status, string component, string paidStatus, int pageIndex, int pageSize)
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
                    wwr.ReqNo AS ReqNo,
                    tbd.NameofHousehold,
                    tbd.household_number,
                    wwr.PaymentCycle,
                    CASE 
                        WHEN wwr.Status = '0' THEN 'IP Applied'
                        WHEN wwr.Status = '1' THEN 'IP Approver Approved'
                        WHEN wwr.Status = '2' THEN 'Initial Reviewer Approved'
                        WHEN wwr.Status = '3' THEN 'Financial Approver Approved'
                        WHEN wwr.Status = '4' THEN 'Final Payment Authorizer Approved'
                        WHEN wwr.Status = '6' THEN 'Reconciled'
                        WHEN wwr.Status = '105' THEN 'IP Approver Rejected'
                        WHEN wwr.Status = '100' THEN 'Initial Reviewer Rejected'
                        WHEN wwr.Status = '101' THEN 'Financial Approver Rejected'
                        WHEN wwr.Status = '102' THEN 'Final Payment Authorizer Rejected'
                        ELSE 'Unknown Status'
                    END AS PaymentStatus,
                    wwr.Component,
                    ISNULL(CONVERT(VARCHAR, bwp.PaidAmountUSD), '0.00') AS PaymentAmount,
                    ISNULL(CONVERT(VARCHAR, bwp.PaidDate, 101), 'No Date') AS PaymentDate
                FROM 
                    tbl_WorkMaster wm
                INNER JOIN 
                    td_WorkWageRequest wwr ON wm.intWorkid = wwr.intWorkid
                INNER JOIN 
                    td_WageReqBenDet wbd ON wwr.intWagePaymentReqid = wbd.intWagePaymentReqid
                INNER JOIN
                    tbl_BeneficiaryDet tbd on tbd.intBenid = wbd.intBenid
                LEFT JOIN 
                    tr_BenWagesPayment bwp ON wbd.intBenid = bwp.intBenid AND wwr.intWagePaymentReqid = bwp.uuid
                WHERE 
                    (@StateId IS NULL OR tbd.intStateid = @StateId)
                    AND (@CountyId IS NULL OR tbd.intCountieid = @CountyId)
                    AND (@PayamId IS NULL OR tbd.intPayamid = @PayamId)
                    AND (@BomaId IS NULL OR tbd.intBomasid = @BomaId)
                    AND (@PaymentCycle IS NULL OR wwr.PaymentCycle = @PaymentCycle)
                    AND (@Status IS NULL OR wwr.Status = @Status)
                    AND (@Component IS NULL OR wwr.Component = @Component)
                    AND (@PaidStatus IS NULL 
                         OR (@PaidStatus = 'Paid' AND bwp.PaidAmountUSD IS NOT NULL)
                         OR (@PaidStatus = 'Unpaid' AND bwp.PaidAmountUSD IS NULL))
                ORDER BY 
                    wwr.intWagePaymentReqid
                OFFSET 
                    @Offset ROWS 
                FETCH NEXT 
                    @PageSize ROWS ONLY;";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.CommandTimeout = 120; // Set the command timeout to 120 seconds
                    cmd.Parameters.AddWithValue("@StateId", (string.IsNullOrEmpty(state) || state == "--Select--") ? (object)DBNull.Value : state);
                    cmd.Parameters.AddWithValue("@CountyId", (string.IsNullOrEmpty(county) || county == "--Select--") ? (object)DBNull.Value : county);
                    cmd.Parameters.AddWithValue("@PayamId", (string.IsNullOrEmpty(payam) || payam == "--Select--") ? (object)DBNull.Value : payam);
                    cmd.Parameters.AddWithValue("@BomaId", (string.IsNullOrEmpty(boma) || boma == "--Select--") ? (object)DBNull.Value : boma);
                    cmd.Parameters.AddWithValue("@PaymentCycle", (string.IsNullOrEmpty(paymentCycle) || paymentCycle == "--Select--") ? (object)DBNull.Value : paymentCycle);
                    cmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(status) || status == "--Select--") ? (object)DBNull.Value : status);
                    cmd.Parameters.AddWithValue("@Component", (string.IsNullOrEmpty(component) || component == "--Select--") ? (object)DBNull.Value : component);
                    cmd.Parameters.AddWithValue("@PaidStatus", (string.IsNullOrEmpty(paidStatus) || paidStatus == "--Select--") ? (object)DBNull.Value : paidStatus);
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




    private DataTable GetAllPaymentData(string state, string county, string payam, string boma, string paymentCycle, string status, string component, string paidStatus,
                                     out int totalPaidBeneficiaries, out int totalUnpaidBeneficiaries, out decimal totalPaidAmount)
    {
        DataTable dt = new DataTable();
        totalPaidBeneficiaries = 0;
        totalUnpaidBeneficiaries = 0;
        totalPaidAmount = 0;
        string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

        try
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open(); // Ensure the connection is opened

                // Step 1: Calculate the totals using an aggregate query
                string totalsQuery = @"
                SELECT 
                    COUNT(CASE WHEN bwp.PaidAmountUSD IS NOT NULL THEN 1 END) AS TotalPaidBeneficiaries,
                    SUM(CASE WHEN bwp.PaidAmountUSD IS NOT NULL THEN bwp.PaidAmountUSD ELSE 0 END) AS TotalPaidAmount,
                    COUNT(*) AS TotalRecords
                FROM 
                    tbl_WorkMaster wm
                INNER JOIN 
                    td_WorkWageRequest wwr ON wm.intWorkid = wwr.intWorkid
                INNER JOIN 
                    td_WageReqBenDet wbd ON wwr.intWagePaymentReqid = wbd.intWagePaymentReqid
                LEFT JOIN 
                    tr_BenWagesPayment bwp ON wbd.intBenid = bwp.intBenid AND wwr.intWagePaymentReqid = bwp.uuid
                WHERE 
                    (@StateId IS NULL OR wm.intStateid = @StateId)
                    AND (@CountyId IS NULL OR wm.intCountieid = @CountyId)
                    AND (@PayamId IS NULL OR wm.intPayamid = @PayamId)
                    AND (@BomaId IS NULL OR wm.intBomasid = @BomaId)
                    AND (@PaymentCycle IS NULL OR wwr.PaymentCycle = @PaymentCycle)
                    AND (@Status IS NULL OR wwr.Status = @Status)
                    AND (@Component IS NULL OR wwr.Component = @Component)
                    AND (@PaidStatus IS NULL 
                         OR (@PaidStatus = 'Paid' AND bwp.PaidAmountUSD IS NOT NULL)
                         OR (@PaidStatus = 'Unpaid' AND bwp.PaidAmountUSD IS NULL));";

                using (SqlCommand totalsCmd = new SqlCommand(totalsQuery, conn))
                {
                    totalsCmd.CommandTimeout = 120; // Set the command timeout to 120 seconds
                    totalsCmd.Parameters.AddWithValue("@StateId", (string.IsNullOrEmpty(state) || state == "--Select--") ? (object)DBNull.Value : state);
                    totalsCmd.Parameters.AddWithValue("@CountyId", (string.IsNullOrEmpty(county) || county == "--Select--") ? (object)DBNull.Value : county);
                    totalsCmd.Parameters.AddWithValue("@PayamId", (string.IsNullOrEmpty(payam) || payam == "--Select--") ? (object)DBNull.Value : payam);
                    totalsCmd.Parameters.AddWithValue("@BomaId", (string.IsNullOrEmpty(boma) || boma == "--Select--") ? (object)DBNull.Value : boma);
                    totalsCmd.Parameters.AddWithValue("@PaymentCycle", (string.IsNullOrEmpty(paymentCycle) || paymentCycle == "--Select--") ? (object)DBNull.Value : paymentCycle);
                    totalsCmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(status) || status == "--Select--") ? (object)DBNull.Value : status);
                    totalsCmd.Parameters.AddWithValue("@Component", (string.IsNullOrEmpty(component) || component == "--Select--") ? (object)DBNull.Value : component);
                    totalsCmd.Parameters.AddWithValue("@PaidStatus", (string.IsNullOrEmpty(paidStatus) || paidStatus == "--Select--") ? (object)DBNull.Value : paidStatus);


                    using (SqlDataReader reader = totalsCmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            totalPaidBeneficiaries = Convert.ToInt32(reader["TotalPaidBeneficiaries"]);
                            totalPaidAmount = reader["TotalPaidAmount"] != DBNull.Value ? Convert.ToDecimal(reader["TotalPaidAmount"]) : 0;
                            int totalRecords = Convert.ToInt32(reader["TotalRecords"]);
                            totalUnpaidBeneficiaries = totalRecords - totalPaidBeneficiaries;
                        }
                    }
                }

                // Step 2: Fetch the grid data
                string gridQuery = @"
                SELECT 
                    wwr.ReqNo AS ReqNo,
                    tbd.NameofHousehold,
                    tbd.household_number,
                    wwr.PaymentCycle,
                    CASE 
                        WHEN wwr.Status = '0' THEN 'IP Applied'
                        WHEN wwr.Status = '1' THEN 'IP Approver Approved'
                        WHEN wwr.Status = '2' THEN 'Initial Reviewer Approved'
                        WHEN wwr.Status = '3' THEN 'Financial Approver Approved'
                        WHEN wwr.Status = '4' THEN 'Final Payment Authorizer Approved'
                        WHEN wwr.Status = '6' THEN 'Reconciled'
                        WHEN wwr.Status = '105' THEN 'IP Approver Rejected'
                        WHEN wwr.Status = '100' THEN 'Initial Reviewer Rejected'
                        WHEN wwr.Status = '101' THEN 'Financial Approver Rejected'
                        WHEN wwr.Status = '102' THEN 'Final Payment Authorizer Rejected'
                        ELSE 'Unknown Status'
                    END AS PaymentStatus,
                    wwr.Component,
                    ISNULL(CONVERT(VARCHAR, bwp.PaidAmountUSD), '0.00') AS PaymentAmount,
                    ISNULL(CONVERT(VARCHAR, bwp.PaidDate, 101), 'No Date') AS PaymentDate
                FROM 
                    tbl_WorkMaster wm
                INNER JOIN 
                    td_WorkWageRequest wwr ON wm.intWorkid = wwr.intWorkid
                INNER JOIN 
                    td_WageReqBenDet wbd ON wwr.intWagePaymentReqid = wbd.intWagePaymentReqid
                INNER JOIN
                    tbl_BeneficiaryDet tbd ON tbd.intBenid = wbd.intBenid
                LEFT JOIN 
                    tr_BenWagesPayment bwp ON wbd.intBenid = bwp.intBenid AND wwr.intWagePaymentReqid = bwp.uuid
                WHERE 
                    (@StateId IS NULL OR tbd.intStateid = @StateId)
                    AND (@CountyId IS NULL OR tbd.intCountieid = @CountyId)
                    AND (@PayamId IS NULL OR tbd.intPayamid = @PayamId)
                    AND (@BomaId IS NULL OR tbd.intBomasid = @BomaId)
                    AND (@PaymentCycle IS NULL OR wwr.PaymentCycle = @PaymentCycle)
                    AND (@Status IS NULL OR wwr.Status = @Status)
                    AND (@Component IS NULL OR wwr.Component = @Component)
                    AND (@PaidStatus IS NULL 
                         OR (@PaidStatus = 'Paid' AND bwp.PaidAmountUSD IS NOT NULL)
                         OR (@PaidStatus = 'Unpaid' AND bwp.PaidAmountUSD IS NULL))
                ORDER BY 
                    wwr.intWagePaymentReqid;";

                using (SqlCommand gridCmd = new SqlCommand(gridQuery, conn))
                {
                    gridCmd.Parameters.AddWithValue("@StateId", (string.IsNullOrEmpty(state) || state == "--Select--") ? (object)DBNull.Value : state);
                    gridCmd.Parameters.AddWithValue("@CountyId", (string.IsNullOrEmpty(county) || county == "--Select--") ? (object)DBNull.Value : county);
                    gridCmd.Parameters.AddWithValue("@PayamId", (string.IsNullOrEmpty(payam) || payam == "--Select--") ? (object)DBNull.Value : payam);
                    gridCmd.Parameters.AddWithValue("@BomaId", (string.IsNullOrEmpty(boma) || boma == "--Select--") ? (object)DBNull.Value : boma);
                    gridCmd.Parameters.AddWithValue("@PaymentCycle", (string.IsNullOrEmpty(paymentCycle) || paymentCycle == "--Select--") ? (object)DBNull.Value : paymentCycle);
                    gridCmd.Parameters.AddWithValue("@Status", (string.IsNullOrEmpty(status) || status == "--Select--") ? (object)DBNull.Value : status);
                    gridCmd.Parameters.AddWithValue("@Component", (string.IsNullOrEmpty(component) || component == "--Select--") ? (object)DBNull.Value : component);
                    gridCmd.Parameters.AddWithValue("@PaidStatus", (string.IsNullOrEmpty(paidStatus) || paidStatus == "--Select--") ? (object)DBNull.Value : paidStatus);


                    SqlDataAdapter da = new SqlDataAdapter(gridCmd);
                    da.Fill(dt);
                }
            }
        }
        catch (Exception ex)
        {
            LogError("Error in GetAllPaymentData: " + ex.Message, ex);
            throw;
        }

        return dt;
    }


    // Define the CashTransferSummaryData class inside the same file
    public class CashTransferSummaryData
    {
        public DataTable ReportData { get; set; }
        public int TotalPaidBeneficiaries { get; set; }
        public int TotalUnpaidBeneficiaries { get; set; }
        public decimal TotalPaidAmount { get; set; }
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

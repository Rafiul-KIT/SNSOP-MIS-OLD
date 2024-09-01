using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using SSSNP.Entity.Payroll_Download;

public partial class PayrollDownload : System.Web.UI.Page
{
    private LookupManagerService _lookupManagerService;
    private string accessToken;
    private string apiUrl;
    private PayrollDownloadResponse pdResponse;
    private PayrollDownloadRequest pdRequest;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeServices();
        if (!IsPostBack)
        {
            LoadStates();
        }

        accessToken = "";
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

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCounty.Items.Clear();
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();

        ddlCounty.Items.Add(new ListItem("--Select--", ""));
        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        ddlBoma.Items.Add(new ListItem("--Select--", ""));

        ddlSupportType.ClearSelection();
        ddlSupportType.Items.Clear();
        ddlSupportType.Items.Add(new ListItem("--Select--", ""));
        ddlSupportType.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlSupportType.Items.Add(new ListItem("DIS", "DIS"));

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

        ddlSupportType.ClearSelection();
        ddlSupportType.Items.Clear();
        ddlSupportType.Items.Add(new ListItem("--Select--", ""));
        ddlSupportType.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlSupportType.Items.Add(new ListItem("DIS", "DIS"));

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

        ddlSupportType.ClearSelection();
        ddlSupportType.Items.Clear();
        ddlSupportType.Items.Add(new ListItem("--Select--", ""));
        ddlSupportType.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlSupportType.Items.Add(new ListItem("DIS", "DIS"));


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

    protected void ddlBoma_SelectedIndexChanged(object sender, EventArgs e)
    {
        // Reset the SupportType dropdown
        ddlSupportType.ClearSelection();
        ddlSupportType.Items.Clear();
        ddlSupportType.Items.Add(new ListItem("--Select--", ""));
        ddlSupportType.Items.Add(new ListItem("LIPW", "LIPW"));
        ddlSupportType.Items.Add(new ListItem("DIS", "DIS"));

    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        // Clear session data before starting a new search
        Session["PayrollData"] = null;
        Session["TotalItemCount"] = null;

        // Validate dropdown selections
        if (string.IsNullOrEmpty(ddlState.SelectedValue) || ddlState.SelectedValue == "0" ||
        string.IsNullOrEmpty(ddlCounty.SelectedValue) || ddlCounty.SelectedValue == "0" ||
        string.IsNullOrEmpty(ddlPayam.SelectedValue) || ddlPayam.SelectedValue == "0" ||
        string.IsNullOrEmpty(ddlBoma.SelectedValue) || ddlBoma.SelectedValue == "0" ||
        string.IsNullOrEmpty(ddlSupportType.SelectedValue) || ddlSupportType.SelectedValue == "")
        {
            LblWarning.Text = "Please select all the Geo Location fields and Support Type.";
            LblWarning.Visible = true;
            return;
        }

        // Hide the warning label if validation passes
        LblWarning.Visible = false;

        pnlResults.Visible = true;
        tblResults.Visible = true;

        var request = new PayrollDownloadRequest
        {
            state = int.Parse(ddlState.SelectedValue),
            county = int.Parse(ddlCounty.SelectedValue),
            payam = int.Parse(ddlPayam.SelectedValue),
            boma = int.Parse(ddlBoma.SelectedValue),
            supportType = ddlSupportType.SelectedValue
        };

        string uri = ConfigurationManager.AppSettings["DownloadPayroll"];
        String token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

        uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["DownloadPayroll"];
        /*if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
        {
            uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["DownloadPayroll"];
        }*/
        Uri urlCheck = new Uri(uri);
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
        httpWReq.PreAuthenticate = true;
        httpWReq.Headers.Add("Authorization", "Bearer " + token);
        httpWReq.Headers.Add("DeviceId", "29feb6211b04-a56d-7d6a-e79e-018b2f19");

        string reqStr = "";

        try
        {
            reqStr = JsonConvert.SerializeObject(request);
            var data = Encoding.ASCII.GetBytes(reqStr);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/json";
            httpWReq.ContentLength = data.Length;

            var newStream = httpWReq.GetRequestStream();
            newStream.Write(data, 0, data.Length);
            newStream.Flush();
            newStream.Close();

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                pdResponse = JsonConvert.DeserializeObject<PayrollDownloadResponse>(jsonResponse);
            }

            // Log the response
            // System.Diagnostics.Debug.WriteLine("API Response: " + JsonConvert.SerializeObject(apiResponse, Formatting.Indented));

            Session["PDResponse"] = pdResponse;
            int totalCount = pdResponse.totalCount;
            Session["TotalItemCount"] = totalCount;

            if (pdResponse != null && pdResponse.data != null && pdResponse.data.Count > 0)
            {
                Session["PayrollData"] = pdResponse.data.SelectMany(c => c.payrollData).ToList();
                rgResults.Rebind();  // Trigger the NeedDataSource event

                // Update button visibility based on lockedStatus
                var firstData = pdResponse.data.FirstOrDefault();
                var lockedStatus = firstData?.lockedStatus;
                HiddenLockedStatus.Value = lockedStatus;


                // Store paymentCycle and wagePaymentReqId in session
                var paymentCycle = firstData?.payrollData.FirstOrDefault()?.paymentCycle;
                var wagePaymentReqId = firstData?.payrollData.FirstOrDefault()?.intWagePaymentReqid;
                Session["PaymentCycle"] = paymentCycle;
                Session["WagePaymentReqId"] = wagePaymentReqId;

                BtnDownload.Visible = true;
                BtnUnlock.Visible = true;
                BtnDownloadPdf.Visible = true;

                LblLockedStatus.Visible = true;
                LblLockedStatus.Text = "Locked Status: " + lockedStatus;

                if (lockedStatus == "UNLOCKED")
                {
                    BtnDownload.Visible = true;
                    BtnDownloadPdf.Visible = true;
                }
                else if (lockedStatus == "LOCKED")
                {
                    BtnUnlock.Visible = true;
                    BtnDownloadPdf.Visible = false;
                    BtnDownload.Visible = false;
                }
            }

            else
            {
                // If there is no data, clear the grid's data source
                Session["PayrollData"] = null;
                rgResults.DataSource = null;
                rgResults.DataBind();
                rgResults.Rebind();

                // Update button visibility and locked status label
                BtnDownload.Visible = false;
                BtnUnlock.Visible = false;
                BtnDownloadPdf.Visible = false;
                LblLockedStatus.Visible = false;
            }
        }
       
        catch (Exception ex)
        {
            LogError(ex);
        }
    }



    private void BindResultsToGrid(List<PDPayrollDataDto> data)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("EnrollmentNo");
        dt.Columns.Add("DateOfCreation");
        dt.Columns.Add("ExchangeRateAtTime");
        dt.Columns.Add("IntBenId");
        dt.Columns.Add("IntWagePaymentReqId");
        dt.Columns.Add("IntWagesReqBenTransId");
        dt.Columns.Add("IntWorkId");
        dt.Columns.Add("PaymentCycle");
        dt.Columns.Add("ReqFromDate");
        dt.Columns.Add("ReqNo");
        dt.Columns.Add("ReqToDate");
        dt.Columns.Add("Status");
        dt.Columns.Add("SupportType");
        dt.Columns.Add("TotAttendance");
        dt.Columns.Add("WageRateSSPTotal");
        dt.Columns.Add("WageRateUSD");
        dt.Columns.Add("WageRateUSDTotal");
        dt.Columns.Add("WorkCode");

        foreach (var item in data)
        {
            dt.Rows.Add(
                item.enrollmentNo,
                item.dateofCreation,
                item.exchangerateattime,
                item.intBenid,
                item.intWagePaymentReqid,
                item.intWagesReqBenTransid,
                item.intWorkid,
                item.paymentCycle,
                item.reqFromDate,
                item.reqNo,
                item.reqToDate,
                item.status,
                item.supportType,
                item.totAttendance,
                item.wageRateSSPTotal,
                item.wageRateUSD,
                item.wageRateUSDTotal,
                item.workCode
            );
        }

        rgResults.DataSource = dt;
        rgResults.DataBind();
    }


    protected void rgResults_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["PayrollData"] != null)
        {
            rgResults.DataSource = Session["PayrollData"];
            int totalCount = (int)Session["TotalItemCount"];
            rgResults.VirtualItemCount = totalCount;
        }
        else
        {
            rgResults.DataSource = new List<PDPayrollDataDto>();
            rgResults.VirtualItemCount = 0;
        }
    }


    protected void rgResults_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
    }


    protected void BtnUnlock_Click(object sender, EventArgs e)
    {
        var paymentCycle = Session["PaymentCycle"]?.ToString();
        var wagePaymentReqId = Session["WagePaymentReqId"] != null ? int.Parse(Session["WagePaymentReqId"].ToString()) : 0;

        var request = new
        {
            boma = int.Parse(ddlBoma.SelectedValue),
            county = int.Parse(ddlCounty.SelectedValue),
            payam = int.Parse(ddlPayam.SelectedValue),
            paymentCycle = paymentCycle,
            state = int.Parse(ddlState.SelectedValue),
            supportType = ddlSupportType.SelectedValue,
            wagePaymentReqId = wagePaymentReqId
        };

        //string uri = ConfigurationManager.AppSettings["UnlockPayroll"];
        String token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

        var uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["UnlockPayroll"];
        /*if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
        {
            uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["UnlockPayroll"];
        }*/

        Uri urlCheck = new Uri(uri);
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
        httpWReq.PreAuthenticate = true;
        httpWReq.Headers.Add("Authorization", "Bearer " + token);
        httpWReq.Headers.Add("DeviceId", "29feb6211b04-a56d-7d6a-e79e-018b2f19");

        try
        {
            string reqStr = JsonConvert.SerializeObject(request);
            var data = Encoding.ASCII.GetBytes(reqStr);
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/json";
            httpWReq.ContentLength = data.Length;

            using (var newStream = httpWReq.GetRequestStream())
            {
                newStream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string jsonResponse = reader.ReadToEnd();
                var unlockResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);
                // Handle the response if necessary
            }

            // After unlocking, refresh the data
            BtnSearch_Click(sender, e);
        }

        catch (WebException webEx)
        {
            LogError(webEx);
            using (var response = webEx.Response)
            {
                var httpResponse = (HttpWebResponse)response;
                using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    string responseText = reader.ReadToEnd();
                    // Log the response details for debugging
                    LogError(new Exception($"Error Response: {responseText}"));
                }
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }


    /*protected void BtnDownload_Click(object sender, EventArgs e)
    {
        if (Session["PDResponse"] != null)
        {
            int userId = GetCurrentUserId(); // Retrieve the current user's ID from the session
            int stateId = int.Parse(ddlState.SelectedValue);
            int countyId = int.Parse(ddlCounty.SelectedValue);
            int payamId = int.Parse(ddlPayam.SelectedValue);
            int bomaId = int.Parse(ddlBoma.SelectedValue);

            // Insert the audit record into the database
            InsertAuditRecord(userId, stateId, countyId, payamId, bomaId);

            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=10";
            Response.Redirect(redirectUrl, false); // Redirect to handler with the type for encrypted payroll data
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }
    }*/

    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        if (Session["PDResponse"] != null)
        {
            int userId = GetCurrentUserId(); // Retrieve the current user's ID from the session
            int stateId = int.Parse(ddlState.SelectedValue);
            int countyId = int.Parse(ddlCounty.SelectedValue);
            int payamId = int.Parse(ddlPayam.SelectedValue);
            int bomaId = int.Parse(ddlBoma.SelectedValue);

            var paymentCycle = Session["PaymentCycle"]?.ToString();
            var wagePaymentReqId = Session["WagePaymentReqId"] != null ? int.Parse(Session["WagePaymentReqId"].ToString()) : 0;

            var request = new
            {
                boma = bomaId,
                county = countyId,
                payam = payamId,
                paymentCycle = paymentCycle,
                state = stateId,
                supportType = ddlSupportType.SelectedValue,
                wagePaymentReqId = wagePaymentReqId
            };

            // Define the API endpoint and access token
            String token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

            var uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["LockPayroll"];
            /*if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["LockPayroll"];
            }*/

            Uri urlCheck = new Uri(uri);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + token);
            httpWReq.Headers.Add("DeviceId", "29feb6211b04-a56d-7d6a-e79e-018b2f19");

            try
            {
                string reqStr = JsonConvert.SerializeObject(request);
                var data = Encoding.ASCII.GetBytes(reqStr);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";
                httpWReq.ContentLength = data.Length;

                using (var newStream = httpWReq.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = reader.ReadToEnd();
                    var downloadResponse = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                    // Check the responseCode
                    int responseCode = downloadResponse.responseCode;
                    string responseMessage = downloadResponse.responseMessage;

                    if (responseCode == 0) // 0 means "Unlocked" and successfully locked now
                    {
                        // Redirect after successful API call
                        string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=10";
                        Response.Redirect(redirectUrl, false); // Redirect to handler with the type for encrypted payroll data
                        HttpContext.Current.ApplicationInstance.CompleteRequest();

                        // Insert the audit record into the database
                        InsertAuditRecord(userId, stateId, countyId, payamId, bomaId);
                    }
                    else if (responseCode == 1) // 1 means "Locked"
                    {
                        // Display a warning that the payroll is locked
                        LblWarning.Text = responseMessage;
                        LblWarning.Visible = true;
                    }
                    else
                    {
                        // Handle unexpected response codes
                        System.Diagnostics.Debug.WriteLine($"Unexpected response code: {responseCode}. Message: {responseMessage}");
                    }
                }
            }
            catch (WebException webEx)
            {
                LogError(webEx);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }


    protected void BtnDownloadPdf_Click(object sender, EventArgs e)
    {
        if (Session["PDResponse"] != null)
        {
            try
            {
                /*// Prepare the necessary parameters for PDF generation
                int userId = GetCurrentUserId(); // Retrieve the current user's ID from the session
                int stateId = int.Parse(ddlState.SelectedValue);
                int countyId = int.Parse(ddlCounty.SelectedValue);
                int payamId = int.Parse(ddlPayam.SelectedValue);
                int bomaId = int.Parse(ddlBoma.SelectedValue);

                // Insert the audit record into the database (optional step)
                InsertAuditRecord(userId, stateId, countyId, payamId, bomaId);*/

                // Redirect to ViewAttachmentHandler with the appropriate query parameters for PDF download
                string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=13"; // AttachmentType=13 for PDF_PAYROLL_DATA
                Response.Redirect(redirectUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
        else
        {
            // Display a message indicating that there is no data to download
            LblWarning.Text = "No payroll data available to download.";
            LblWarning.Visible = true;
        }
    }



    private void InsertAuditRecord(int userId, int stateId, int countyId, int payamId, int bomaId)
    {
        string query = "INSERT INTO tbl_audit_payroll_download (AuditId, UserId, DownloadTime, intStateid, intCountieid, intPayamid, intBomasid) " +
                       "VALUES (@AuditId, @UserId, @DownloadTime, @StateId, @CountyId, @PayamId, @BomaId)";

        using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
        {
            using (var cmd = new SqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@AuditId", Guid.NewGuid().ToString("N").Substring(0, 10)); // Generate a unique AuditId
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@DownloadTime", DateTime.Now); // Use DateTime.Now to get the current date and time
                cmd.Parameters.AddWithValue("@StateId", stateId);
                cmd.Parameters.AddWithValue("@CountyId", countyId);
                cmd.Parameters.AddWithValue("@PayamId", payamId);
                cmd.Parameters.AddWithValue("@BomaId", bomaId);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }


    private int GetCurrentUserId()
    {
        // Check if the session contains the user ID
        if (Session["uid"] != null)
        {
            return Convert.ToInt32(Session["uid"]);
        }
        else
        {
            // Handle the case where the user ID is not found in the session
            // You can throw an exception or return a default value
            throw new Exception("User ID not found in session.");
        }
    }


    private void LogError(Exception ex)
    {
        System.Diagnostics.Debug.WriteLine("Error Message: " + ex.Message);
        System.Diagnostics.Debug.WriteLine("Stack Trace: " + ex.StackTrace);
        if (ex.InnerException != null)
        {
            System.Diagnostics.Debug.WriteLine("Inner Exception: " + ex.InnerException.Message);
            System.Diagnostics.Debug.WriteLine("Inner Exception Stack Trace: " + ex.InnerException.StackTrace);
        }
    }
}

using Newtonsoft.Json;
using SSSNP.Entity.Payroll;
using SSSNP.Entity.Payroll_Download;
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

namespace SSSNP.UI.Public_Works
{
    public partial class UnpaidPayrollDownload : System.Web.UI.Page
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
                LoadPaymentCycle();
                LoadCycleCheckboxList();

                foreach (ListItem item in cblCycles.Items)
                {
                    item.Attributes["onclick"] = "limitCheckboxSelection(this)";
                }
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
                ddlPaymentCycle.ClearSelection();
                ddlPaymentCycle.Items.Clear();
                LoadPaymentCycle();
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;

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

            ddlPaymentCycle.ClearSelection();
            ddlPaymentCycle.Items.Clear();
            LoadPaymentCycle();
            ddlSupportType.ClearSelection();
            ddlSupportType.Items.Clear();
            LoadSupportTypes();

        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;

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

            ddlPaymentCycle.ClearSelection();
            ddlPaymentCycle.Items.Clear();
            LoadPaymentCycle();
            ddlSupportType.ClearSelection();
            ddlSupportType.Items.Clear();
            LoadSupportTypes();
        }

        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;

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

            ddlPaymentCycle.ClearSelection();
            ddlPaymentCycle.Items.Clear();
            LoadPaymentCycle();
            ddlSupportType.ClearSelection();
            ddlSupportType.Items.Clear();
            LoadSupportTypes();
        }

        protected void ddlBoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;

            ddlSupportType.ClearSelection();
            ddlSupportType.Items.Clear();
            LoadSupportTypes();
            ddlPaymentCycle.ClearSelection();
            ddlPaymentCycle.Items.Clear();
            LoadPaymentCycle();
        }

        private void LoadSupportTypes()
        {
            ddlSupportType.Items.Clear();
            ddlSupportType.Items.Add(new ListItem("--Select--", ""));
            ddlSupportType.Items.Add(new ListItem("LIPW", "LIPW"));
            ddlSupportType.Items.Add(new ListItem("DIS", "DIS"));
            ddlSupportType.Items.Add(new ListItem("ALL", "ALL"));
        }

        private void LoadPaymentCycle()
        {
            try
            {
                ddlPaymentCycle.Items.Clear();
                ddlPaymentCycle.Items.Add(new ListItem("--Select--", ""));
                for (int i = 1; i <= 10; i++)
                {
                    string text = "Payment Cycle " + i;
                    ddlPaymentCycle.Items.Add(new ListItem(text, text));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        private void LoadCycleCheckboxList()
        {
            cblCycles.Items.Clear();
            for (int i = 1; i <= 9; i++)
            {
                string text = "Payment Cycle " + i;
                cblCycles.Items.Add(new ListItem(text, text));
            }
        }

        protected void rblPayrollType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;

            bool isCombined = rblPayrollType.SelectedValue == "Combined";

            ddlPaymentCycle.Visible = !isCombined;
            LabelPaymentCycle.Visible = !isCombined;

            trCycleMulti.Visible = isCombined;

            if (isCombined)
            {
                LoadCycleCheckboxList();
            }
        }

        protected void ddlSupportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;
        }

        protected void ddlPaymentCycle_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;
        }

        protected void cblCycles_Changed(object sender, EventArgs e)
        {
            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            // Clear session data before starting a new search
            Session["PayrollData"] = null;
            Session["TotalItemCount"] = null;
            LblDownloadWarning.Text = null;
            LblWarning.Text = null;

            BtnDownload.Visible = false;
            BtnDownloadPdf.Visible = false;

            // Validate dropdown selections
            if (string.IsNullOrEmpty(ddlState.SelectedValue) || ddlState.SelectedValue == "0" ||
            string.IsNullOrEmpty(ddlCounty.SelectedValue) || ddlCounty.SelectedValue == "0" ||
            string.IsNullOrEmpty(ddlPayam.SelectedValue) || ddlPayam.SelectedValue == "0" ||
            string.IsNullOrEmpty(ddlBoma.SelectedValue) || ddlBoma.SelectedValue == "0" ||
            string.IsNullOrEmpty(ddlSupportType.SelectedValue) || ddlSupportType.SelectedValue == "")
            {
                LblWarning.Text = "Please select all the Geo Location fields, Support Type and Payment Cycle";
                LblWarning.Visible = true;
                return;
            }

            // Save selected state to session
            Session["SelectedStateId"] = ddlState.SelectedValue;
            Session["SelectedStateName"] = ddlState.SelectedItem.Text;
            // Save selected county to session
            Session["SelectedCountyId"] = ddlCounty.SelectedValue;
            Session["SelectedCountyName"] = ddlCounty.SelectedItem.Text;
            // Save selected payam to session
            Session["SelectedPayamId"] = ddlPayam.SelectedValue;
            Session["SelectedPayamName"] = ddlPayam.SelectedItem.Text;
            // Save selected boma to session
            Session["SelectedBomaId"] = ddlBoma.SelectedValue;
            Session["SelectedBomaName"] = ddlBoma.SelectedItem.Text;

            Session["SelectedSupportType"] = ddlSupportType.SelectedValue;
            // Identify selected mode
            bool isCombined = rblPayrollType.SelectedValue == "Combined";
            Session["isCombined"] = isCombined;

            // Handle single payroll mode
            string selectedPaymentCycle = null;
            List<string> selectedCycles = null;

            if (!isCombined)
            {
                if (string.IsNullOrEmpty(ddlPaymentCycle.SelectedValue))
                {
                    LblWarning.Text = "Please select a Payment Cycle.";
                    LblWarning.Visible = true;
                    return;
                }

                selectedPaymentCycle = ddlPaymentCycle.SelectedValue;
                Session["SelectedPaymentCycle"] = selectedPaymentCycle;

                if (!string.IsNullOrWhiteSpace(ddlPaymentCycle.SelectedValue))
                {
                    string[] parts = ddlPaymentCycle.SelectedValue.Split(' ');
                    string cycleNumber = parts.Last();

                    Session["SelectedPaymentCycle"] = cycleNumber;
                }
                else
                {
                    Session["SelectedPaymentCycle"] = null;
                }

            }
            else // Combined payroll mode
            {
                selectedCycles = cblCycles.Items.Cast<ListItem>()
                    .Where(li => li.Selected)
                    .Select(li => li.Value)
                    .ToList();

                if (selectedCycles.Count < 2)
                {
                    LblWarning.Text = "Please select at least 2 payment cycles.";
                    LblWarning.Visible = true;
                    return;
                }

                var cycleShortString = string.Join("_", selectedCycles.Select(v =>
                {
                    var parts = v.Split(' ');
                    return parts.Length > 2 ? parts[2] : parts.Last(); // Gets the number part
                }));

                Session["SelectedPaymentCycle"] = cycleShortString;
            }
           

            // Hide the warning label if validation passes
            LblWarning.Visible = false;

            pnlResults.Visible = true;
            tblResults.Visible = true;

            PayrollDownloadRequest singleRequest = null;
            PayrollMergeRequest mergeRequest = null;

            if (!isCombined)
            {
                singleRequest = new PayrollDownloadRequest
                {
                    state = int.Parse(ddlState.SelectedValue),
                    county = int.Parse(ddlCounty.SelectedValue),
                    payam = int.Parse(ddlPayam.SelectedValue),
                    boma = int.Parse(ddlBoma.SelectedValue),
                    supportType = ddlSupportType.SelectedValue,
                    paymentCycle = ddlPaymentCycle.SelectedValue
                };
            }
            else
            {
                selectedCycles = cblCycles.Items.Cast<ListItem>()
                .Where(li => li.Selected)
                .Select(li => li.Value)
                .ToList();

                mergeRequest = new PayrollMergeRequest
                {
                    state = int.Parse(ddlState.SelectedValue),
                    county = int.Parse(ddlCounty.SelectedValue),
                    payam = int.Parse(ddlPayam.SelectedValue),
                    boma = int.Parse(ddlBoma.SelectedValue),
                    supportType = ddlSupportType.SelectedValue,
                    paymentCycleList = selectedCycles
                };
            }

            string reqStr = isCombined
            ? JsonConvert.SerializeObject(mergeRequest)
            : JsonConvert.SerializeObject(singleRequest);

            string apiPath = isCombined
            ? ConfigurationManager.AppSettings["GetMergedPayroll"]
            : ConfigurationManager.AppSettings["GetPayrollV2"];

            string baseUrl = ConfigurationManager.AppSettings["AfisDevBaseUrl"];
            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                baseUrl = ConfigurationManager.AppSettings["AfisProdBaseUrl"];
            }

            string uri = baseUrl + apiPath;
            string token = ConfigurationManager.AppSettings["AfisApiAccessToken"];

            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(new Uri(uri));
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + token);
            httpWReq.Headers.Add("DeviceId", "29feb6211b04-a56d-7d6a-e79e-018b2f19");
            httpWReq.Method = "POST";
            httpWReq.ContentType = "application/json";
            httpWReq.Timeout = int.Parse(ConfigurationManager.AppSettings["AfisRequestTimeout"]);
            httpWReq.ReadWriteTimeout = int.Parse(ConfigurationManager.AppSettings["AfisRequestReadTimeout"]);

            try
            {
                byte[] data = Encoding.UTF8.GetBytes(reqStr);
                httpWReq.ContentLength = data.Length;

                using (var newStream = httpWReq.GetRequestStream())
                {
                    newStream.Write(data, 0, data.Length);
                }

                using (HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse())
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = reader.ReadToEnd();
                    pdResponse = JsonConvert.DeserializeObject<PayrollDownloadResponse>(jsonResponse);
                }

                Session["PDResponse"] = pdResponse;

                if (pdResponse == null || !pdResponse.operationResult)
                {
                    LblDownloadWarning.Text = pdResponse?.errorMsg ?? "Unknown error occurred while retrieving payroll data.";
                    LblDownloadWarning.Visible = true;

                    Session["PayrollData"] = null;
                    rgResults.DataSource = null;
                    rgResults.DataBind();
                    rgResults.Rebind();
                    BtnDownload.Visible = false;
                    BtnDownloadPdf.Visible = false;
                    return;
                }

                if (pdResponse != null && pdResponse.data != null && pdResponse.data.Count > 0)
                {
                    var allPayrollData = pdResponse.data.SelectMany(c => c.payrollData).ToList();
                    Session["PayrollData"] = allPayrollData;
                    Session["TotalItemCount"] = allPayrollData.Count;
                    rgResults.Rebind();
                    BtnDownloadPdf.Visible = true;
                    BtnDownload.Visible = true;
                }
                else
                {
                    LblDownloadWarning.Text = "No payroll data available for the selected filters.";
                    LblDownloadWarning.Visible = true;

                    Session["PayrollData"] = null;
                    rgResults.DataSource = null;
                    rgResults.DataBind();
                    rgResults.Rebind();
                    BtnDownload.Visible = false;
                    BtnDownloadPdf.Visible = false;
                }
            }

            catch (Exception ex)
            {
                LogError(ex);
            }
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


        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            if (Session["PDResponse"] != null)
            {
                int userId = GetCurrentUserId();
                int stateId = int.Parse(ddlState.SelectedValue);
                int countyId = int.Parse(ddlCounty.SelectedValue);
                int payamId = int.Parse(ddlPayam.SelectedValue);
                int bomaId = int.Parse(ddlBoma.SelectedValue);

                // Determine payroll mode
                bool isCombined = rblPayrollType.SelectedValue == "Combined";

                if (!isCombined)
                {
                    string paymentCycle = ddlPaymentCycle.SelectedValue;
                    InsertAuditRecord(userId, stateId, countyId, payamId, bomaId, paymentCycle);
                }
                else
                {
                    var selectedCycles = Session["SelectedCombinedCycles"] as List<string>;
                    if (selectedCycles != null && selectedCycles.Count > 0)
                    {
                        string combinedCycleText = string.Join(", ", selectedCycles);
                        InsertAuditRecord(userId, stateId, countyId, payamId, bomaId, combinedCycleText);
                    }
                }

                string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=10";
                Response.Redirect(redirectUrl, false); // Redirect to handler with the type for encrypted payroll data
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
        }



        protected void BtnDownloadPdf_Click(object sender, EventArgs e)
        {
            if (Session["PDResponse"] != null)
            {
                try
                {
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



        private void InsertAuditRecord(int userId, int stateId, int countyId, int payamId, int bomaId, string paymentCycle)
        {
            string query = "INSERT INTO tbl_audit_payroll_download (AuditId, UserId, DownloadTime, intStateid, intCountieid, intPayamid, intBomasid, PaymentCycle) " +
                           "VALUES (@AuditId, @UserId, @DownloadTime, @StateId, @CountyId, @PayamId, @BomaId, @PaymentCycle)";

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
                    cmd.Parameters.AddWithValue("@PaymentCycle", paymentCycle);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private int GetCurrentUserId()
        {
            if (Session["uid"] != null)
            {
                return Convert.ToInt32(Session["uid"]);
            }
            else
            {
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
}
using Newtonsoft.Json;
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
            ddlSupportType.Items.Add(new ListItem("ALL", "ALL"));

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
            ddlSupportType.Items.Add(new ListItem("ALL", "ALL"));

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
            ddlSupportType.Items.Add(new ListItem("ALL", "ALL"));


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
        }

        protected void ddlBoma_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Reset the SupportType dropdown
            ddlSupportType.ClearSelection();
            ddlSupportType.Items.Clear();
            ddlSupportType.Items.Add(new ListItem("--Select--", ""));
            ddlSupportType.Items.Add(new ListItem("LIPW", "LIPW"));
            ddlSupportType.Items.Add(new ListItem("DIS", "DIS"));
            ddlSupportType.Items.Add(new ListItem("ALL", "ALL"));
            ddlPaymentCycle.ClearSelection();
            ddlPaymentCycle.Items.Clear();
            LoadPaymentCycle();
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
            string.IsNullOrEmpty(ddlSupportType.SelectedValue) || ddlSupportType.SelectedValue == "" ||
            string.IsNullOrEmpty(ddlPaymentCycle.SelectedValue) || ddlPaymentCycle.SelectedValue == "")
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

            Session["SelectedSupportType"] = ddlSupportType.SelectedValue;

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
                supportType = ddlSupportType.SelectedValue,
                paymentCycle = ddlPaymentCycle.SelectedValue
            };

            string uri = ConfigurationManager.AppSettings["GetPayrollV2"];
            String token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

            uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["GetPayrollV2"];
            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["GetPayrollV2"];
            }
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

                Session["PDResponse"] = pdResponse;
                
                if (pdResponse != null && pdResponse.data != null && pdResponse.data.Count > 0)
                {
                    //Session["PayrollData"] = pdResponse.data.SelectMany(c => c.payrollData).ToList();
                    var allPayrollData = pdResponse.data.SelectMany(c => c.payrollData).ToList();
                    int totalCount = allPayrollData.Count; // Get the total count from all payroll data

                    Session["PayrollData"] = allPayrollData;
                    Session["TotalItemCount"] = totalCount;
                    rgResults.Rebind();  // Trigger the NeedDataSource event
                    BtnDownloadPdf.Visible = true;
                    BtnDownload.Visible = true;
                    
                }

                else
                {
                    // If there is no data, clear the grid's data source
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


        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            if (Session["PDResponse"] != null)
            {
                int userId = GetCurrentUserId(); // Retrieve the current user's ID from the session
                int stateId = int.Parse(ddlState.SelectedValue);
                int countyId = int.Parse(ddlCounty.SelectedValue);
                int payamId = int.Parse(ddlPayam.SelectedValue);
                int bomaId = int.Parse(ddlBoma.SelectedValue);
                string paymentCycle = ddlPaymentCycle.SelectedValue;

                // Insert the audit record into the database
                InsertAuditRecord(userId, stateId, countyId, payamId, bomaId, paymentCycle);

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
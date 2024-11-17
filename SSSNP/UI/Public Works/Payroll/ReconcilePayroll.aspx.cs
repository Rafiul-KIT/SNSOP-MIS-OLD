using Newtonsoft.Json;
using SSSNP_SNSOP.App_Code;
using SSSNP_SNSOP.Core.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works.Payroll
{
    public partial class ReconcilePayroll : System.Web.UI.Page
    {
        private LookupManagerService _lookupManagerService;
        ErrorLogger eLog = new ErrorLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeServices();
            if (!IsPostBack)
            {
                LblWarning.Visible = false;  // Hide the warning initially
                LoadStates();
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
            }
        }

        // Method to load states into the ddlState dropdown
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
            }
        }

        // Event handler for state selection change
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
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        // Event handler for county selection change
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
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        // Event handler for payam selection change
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
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            LblWarning.Visible = false;
            
            LblWarning.Text = "";

            GeoLocationDto geoLocation = new GeoLocationDto
            {
                StateId = int.TryParse(ddlState.SelectedValue, out int stateIdValue) ? (int?)stateIdValue : null,
                CountyId = int.TryParse(ddlCounty.SelectedValue, out int countyIdValue) ? (int?)countyIdValue : null,
                PayamId = int.TryParse(ddlPayam.SelectedValue, out int payamIdValue) ? (int?)payamIdValue : null,
                BomaId = int.TryParse(ddlBoma.SelectedValue, out int bomaIdValue) ? (int?)bomaIdValue : null
            };

            // Save the GeoLocationDto object in the session
            Session["GeoLocation"] = geoLocation;

            int attachmentType = 30;

            // Prepare redirect URL with parameters
            string redirectUrl = $"~/ViewAttachmentHandler.aspx?AttachmentType={attachmentType}";

                // Redirect with query parameters
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                if (!fileUpload.HasFile)
                {
                    AddErrorMessage("Please select a file to upload.");
                    return;
                }

                string extension = Path.GetExtension(fileUpload.FileName);
                if (extension != ".xlsx")
                {
                    AddErrorMessage("Invalid file format. Please upload an Excel (.xlsx) file.");
                    return;
                }

                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.FileContent))
                {
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    List<PayrollReconciliationDto> list = new List<PayrollReconciliationDto>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string household_number = worksheet.Cells[row, 1].Text;
                        string enrolment_number = worksheet.Cells[row, 2].Text;
                        string first_name = worksheet.Cells[row, 3].Text;
                        string surname = worksheet.Cells[row, 4].Text;
                        string last_name = worksheet.Cells[row, 5].Text;
                        string status = worksheet.Cells[row, 6].Text;
                        string paymentCenter = worksheet.Cells[row, 7].Text;
                        string paid_to = worksheet.Cells[row, 8].Text;
                        string wage = worksheet.Cells[row, 9].Text;
                        string attendance = worksheet.Cells[row, 10].Text;
                        string exchange_rate = worksheet.Cells[row, 11].Text;
                        string amount = worksheet.Cells[row, 12].Text;
                        string work_code = worksheet.Cells[row, 13].Text;
                        string work_id = worksheet.Cells[row, 14].Text;
                        string trans_id = worksheet.Cells[row, 15].Text;
                        string uuid = worksheet.Cells[row, 16].Text;
                        string date_from = worksheet.Cells[row, 17].Text;
                        string date_to = worksheet.Cells[row, 18].Text;
                        string created_at = worksheet.Cells[row, 19].Text;
                        string generated_at = worksheet.Cells[row, 20].Text;
                        string latitude = worksheet.Cells[row, 21].Text;
                        string longitude = worksheet.Cells[row, 22].Text;

                        if (string.IsNullOrEmpty(household_number) || string.IsNullOrEmpty(trans_id) || !status.Equals("PAID"))
                        {
                            continue;
                        }

                        list.Add(new PayrollReconciliationDto
                        {
                            household_number = household_number,
                            enrolment_number = enrolment_number,
                            first_name = first_name,
                            surname = surname,
                            last_name = last_name,
                            status = status,
                            paymentCenter = paymentCenter,
                            paid_to = paid_to,
                            wage = wage,
                            attendance = attendance,
                            exchange_rate = exchange_rate,
                            amount = amount,
                            work_code = work_code,
                            work_id = work_id,
                            trans_id = trans_id,
                            uuid = uuid,
                            date_from = date_from,
                            date_to = date_to,
                            created_at = created_at,
                            generated_at = generated_at,
                            latitude = latitude,
                            longitude = longitude,
                            updated_at = DateTime.Now.ToString()
                        });
                    }

                    if (list != null && list.Count > 0)
                    {
                        PayrollReconciliationRequest payrollReconciliationRequest = new PayrollReconciliationRequest();
                        payrollReconciliationRequest.dataList = list;

                        bool b = SendPayrollReconcileDataByApi(payrollReconciliationRequest);
                        if (b)
                        {
                            AddSuccessMessage("Successfully sent for reconciliation");
                        } else
                        {
                            AddErrorMessage("Could not send payroll data successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("File upload failed: " + ex.Message);
            }
        }

        private bool SendPayrollReconcileDataByApi(PayrollReconciliationRequest request)
        {
            try
            {
                string uri = ConfigurationManager.AppSettings["PayrollReconBatchSaveEndpoint"];

                string token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

                uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["PayrollReconBatchSaveEndpoint"];
                if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
                {
                    uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["PushApprovedPaymentsEndpoint"];
                }
                Uri urlCheck = new Uri(uri);
                HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
                httpWReq.PreAuthenticate = true;
                httpWReq.Headers.Add("Authorization", "Bearer " + token);

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
                    StreamReader reader = new StreamReader(response.GetResponseStream());
                    string jsonresponse = "";
                    string temp = null;
                    while ((temp = reader.ReadLine()) != null)
                    {
                        jsonresponse += temp;
                    }
                    PayrollReconciliationResponse apiResponse = 
                        JsonConvert.DeserializeObject<PayrollReconciliationResponse>(jsonresponse);

                    return true;
                }
                catch (Exception ex)
                {
                    eLog.LogError(ex);
                    Console.WriteLine("Error when pushing payment reconciliation data by api. Error:\n" + ex.ToString());
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when pushing payment reconciliation data by api. Error:\n" + ex.ToString());
                return false;
            }
        }

        private void AddSuccessMessage(string message)
        {
            LblWarning.Visible = true;
            LblWarning.Text = message;
            LblWarning.ForeColor = System.Drawing.Color.Green;
        }

        private void AddErrorMessage(string message)
        {
            LblWarning.Visible = true;
            LblWarning.Text = message;
            LblWarning.ForeColor = System.Drawing.Color.Red;
        }
    }
}
using System;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.UI.WebControls;
using SSSNP.Service;
using SSSNP_SNSOP.Core.Model;

namespace SSSNP.UI.Public_Works
{
    public partial class Component2TemplateImporting : System.Web.UI.Page
    {
        private LookupManagerService _lookupManagerService;

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
                    throw new Exception("Connection string is not found or empty.");
                }

                _lookupManagerService = new LookupManagerService(connectionString);
            }
            catch (Exception ex)
            {
                LogError(new Exception("Error initializing services: " + ex.Message, ex));
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
                LogError(ex);
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
                        LogError(new Exception("ddlState.SelectedValue is not a valid integer: " + ddlState.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
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
                        LogError(new Exception("ddlCounty.SelectedValue is not a valid integer: " + ddlCounty.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
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
                        LogError(new Exception("ddlPayam.SelectedValue is not a valid integer: " + ddlPayam.SelectedValue));
                    }
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }

        protected void ddlTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTemplate.SelectedValue != "--Select--")
            {
                LblWarning.Visible = false;  // Hide warning when valid template is selected
                LblWarning.Text = "";  // Clear the warning text

                int val = ddlTemplate.SelectedValue?.ToString() != null ?
                    Convert.ToInt32(ddlTemplate.SelectedValue.ToString()) : 0;
                if (val == 16 || val == 31 || val == 32 || val == 50 || val == 52)
                {
                    LabelState.Visible = false;
                    ddlState.Visible = false;

                    LabelCounty.Visible = false;
                    ddlCounty.Visible = false;

                    LabelPayam.Visible = false;
                    ddlPayam.Visible = false;

                    LabelBoma.Visible = false;
                    ddlBoma.Visible = false;
                } else
                {
                    LabelState.Visible = true;
                    ddlState.Visible = true;

                    LabelCounty.Visible = true;
                    ddlCounty.Visible = true;

                    LabelPayam.Visible = true;
                    ddlPayam.Visible = true;

                    LabelBoma.Visible = true;
                    ddlBoma.Visible = true;
                }
                if (ddlTemplate.SelectedValue == "17") // Training Attendance Tracking Template
                {
                    trTrainingDate.Style["display"] = "table-row"; // Show Date Field
                }
                else
                {
                    trTrainingDate.Style["display"] = "none"; // Hide Date Field
                    txtTrainingDate.Text = string.Empty; // Clear value when hidden
                }
            }
        }

        private void LogError(Exception ex)
        {
            // Add appropriate logging here (for now, using console output)
            System.Diagnostics.Debug.WriteLine("Error Message: " + ex.Message);
            System.Diagnostics.Debug.WriteLine("Stack Trace: " + ex.StackTrace);
        }

        // Download button functionality commented out for now
        protected void BtnDownload_Click(object sender, EventArgs e)
        {
            LblWarning.Visible = false;

            if (ddlTemplate.SelectedValue == "--Select--" || string.IsNullOrEmpty(ddlTemplate.SelectedValue))
            {
                LblWarning.Visible = true;
                LblWarning.Text = "Please Select a Template";
                return;
            }            
            else
            {
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

                string trainingDate = txtTrainingDate.Text.Trim();
                // If Training Attendance is selected but no date is provided, show an error
                if (ddlTemplate.SelectedValue == "17" && string.IsNullOrEmpty(trainingDate))
                {
                    LblWarning.Text = "Please select a training date.";
                    LblWarning.Visible = true;
                    return;
                }

                // Prepare redirect URL with parameters
                if (int.TryParse(ddlTemplate.SelectedValue, out int attachmentType))
                {
                    string redirectUrl = $"~/ViewAttachmentHandler.aspx?AttachmentType={attachmentType}";
                    if (ddlTemplate.SelectedValue == "17" && !string.IsNullOrEmpty(trainingDate))
                    {
                        redirectUrl += $"&AttachmentValue={HttpUtility.UrlEncode(trainingDate)}";
                    }

                    // Redirect with query parameters
                    Response.Redirect(redirectUrl, false);
                    HttpContext.Current.ApplicationInstance.CompleteRequest();
                }
            }
            
        }
    }
}

using SSSNP.Service;
using SSSNP_SNSOP.Core.Model.Component2;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class LivelihoodGrantDisbursement : Page
    {
        
        private Component2Service _component2Service;
        private bool isAnalyzed = false;
        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the service
            _component2Service = new Component2Service();

            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
                RadGrid.Visible = false;
            }
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            // Redirect to download Excel template
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=19";
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
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

                // Parse the Excel file into a list of LivelihoodGrantDisbursementModel objects
                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.PostedFile.InputStream))
                {
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // Create a list to hold the parsed data
                    List<LivelihoodGrantDisbursementModel> grantList = new List<LivelihoodGrantDisbursementModel>();

                    // Loop through the worksheet rows and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 contains headers
                    {
                        // Handle null or empty values for each column
                        var paymentAmount = decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal amount) ? amount : 0m; // Default to 0 if null or invalid
                        var paymentDate = DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime date) ? date : DateTime.MinValue; // Default to DateTime.MinValue if null or invalid

                        LivelihoodGrantDisbursementModel grant = new LivelihoodGrantDisbursementModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            PaymentCycle = worksheet.Cells[row, 2].Text, // String type, no conversion needed
                            PaymentAmount = paymentAmount, // Parsed or default value
                            PaymentDate = paymentDate, // Parsed or default value
                            CreatedBy = userId,
                            ModifiedBy = userId
                        };

                        grantList.Add(grant);
                    }

                    // Store the list in session for further processing
                    Session["UploadedGrantData"] = grantList;

                    AddSuccessMessage("File uploaded and data parsed successfully. You can now analyze it.");
                    btnAnalyze.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("File upload failed: " + ex.Message);
            }
        }


        protected void btnAnalyze_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            var _component2Service = new Component2Service();

            try
            {
                // Retrieve the list of LivelihoodGrantDisbursement objects from session
                var grantList = Session["UploadedGrantData"] as List<LivelihoodGrantDisbursementModel>;
                if (grantList == null || grantList.Count == 0)
                {
                    AddErrorMessage("No file found for analysis.");
                    return;
                }

                // Create a DataTable to hold the analyzed data (for RadGrid display)
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("HouseholdNumber", typeof(string));
                dt.Columns.Add("PaymentCycle", typeof(string));
                dt.Columns.Add("PaymentAmount", typeof(object)); // Change to object to support null values
                dt.Columns.Add("PaymentDate", typeof(object)); // Change to object to support null values
                dt.Columns.Add("ValidationStatus", typeof(string)); // For error messages

                int serial = 1; // For numbering the entries

                // Fetch valid household numbers for validation
                var validHouseholdNumbers = _component2Service.GetValidHouseholdNumbers();

                // Filter out garbage values from grantList
                var validGrantList = grantList.Where(grant => !string.IsNullOrWhiteSpace(grant.HouseholdNumber)).ToList();

                // Iterate over valid grantList and validate each entry
                foreach (var grant in validGrantList)
                {
                    var validationResult = _component2Service.IsValidData(grant, validHouseholdNumbers); // Call validation

                    // Populate DataTable with the data
                    DataRow dr = dt.NewRow();
                    dr["Id"] = serial;
                    dr["HouseholdNumber"] = grant.HouseholdNumber;
                    dr["PaymentCycle"] = grant.PaymentCycle;
                    dr["PaymentAmount"] = grant.PaymentAmount ?? (object)DBNull.Value; // Handle null values
                                                                                       // Handle null or invalid PaymentDate
                    dr["PaymentDate"] = grant.PaymentDate.HasValue && grant.PaymentDate.Value != DateTime.MinValue
                                        ? grant.PaymentDate.Value.ToString("MM-dd-yyyy")
                                        : "N/A";

                    // Set ValidationStatus based on validation result
                    if (!validationResult.isValid)
                    {
                        dr["ValidationStatus"] = validationResult.errorMessage; // Store error message
                    }
                    else
                    {
                        dr["ValidationStatus"] = "Valid"; // Set to "Valid" if no errors
                    }

                    dt.Rows.Add(dr);
                    serial++;
                }

                // Bind the data to RadGrid for display
                RadGrid.DataSource = dt;
                RadGrid.Visible = true;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.VirtualItemCount = dt.Rows.Count;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = true;
                isAnalyzed = true;
                RadGrid.DataBind();

                AddSuccessMessage("File analyzed successfully. You can now import it.");
                btnImport.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("File analysis failed: " + ex.Message);
            }
        }





        protected void btnImport_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages
            try
            {
                // Retrieve list of grant objects from session
                var grantList = Session["UploadedGrantData"] as List<LivelihoodGrantDisbursementModel>;
                if (grantList == null || grantList.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                var validEntries = grantList.Where(a => _component2Service.IsValidData(a, _component2Service.GetValidHouseholdNumbers()).isValid).ToList();
                if (validEntries.Count == 0)
                {
                    AddErrorMessage("No valid data available for import.");
                    return;
                }
                // Save data using service layer
                var response = _component2Service.SaveLivelihoodGrantDisbursement(validEntries);

                if (response.IsSuccess)
                {
                    AddSuccessMessage($"{validEntries.Count} valid entries imported successfully.");
                    btnImport.Enabled = false;
                    btnAnalyze.Enabled = false;
                }
                else
                {
                    AddErrorMessage("Import failed: " + response.Message);
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data import failed: " + ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClearMessages(); // Clear any previous messages

            try
            {
                RadGrid.CurrentPageIndex = 0;
                int pageSize = RadGrid.PageSize;
                
                Session["TotalRecordCount"] = GetLivelihoodGrantDisbursementDataCount();
                RadGrid.DataSource = GetLivelihoodGrantDisbursementData(RadGrid.CurrentPageIndex, pageSize);
                RadGrid.Visible = true;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = false;
                RadGrid.DataBind();
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = GetLivelihoodGrantDisbursementData(RadGrid.CurrentPageIndex, RadGrid.PageSize); // Fetch all data
            RadGrid.DataSource = dt;
            int totalRecordCount = GetLivelihoodGrantDisbursementDataCount();
            RadGrid.VirtualItemCount = totalRecordCount;
        }


        private int GetLivelihoodGrantDisbursementDataCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open(); // Ensure the connection is opened
                    
                    string query = @"SELECT COUNT(*) FROM tbl_com2_Livelihood_Grant_Disbursement WHERE 1=1;";
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("An error occurred while counting records: " + ex.Message);
            }

            return totalRecords;
        }

        private DataTable GetLivelihoodGrantDisbursementData(int pageIndex, int pageSize)
        {
            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                string query = @"SELECT Id, HouseholdNumber, PaymentCycle, PaymentAmount, PaymentDate 
                         FROM tbl_com2_Livelihood_Grant_Disbursement 
                         ORDER BY Id 
                         OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }

                RadGrid.VirtualItemCount = GetLivelihoodGrantDisbursementDataCount();
            }
            catch (Exception ex)
            {
                AddErrorMessage("An error occurred while retrieving Livelihood Grant Disbursement data: " + ex.Message);
            }

            return dt;
        }

        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            // Check if the item is a data row
            if (e.Item is GridDataItem dataItem)
            {
                // Only change color if the data has been analyzed
                if (isAnalyzed)
                {
                    // Get the validation status from the row
                    string validationStatus = dataItem["ValidationStatus"].Text;

                    // If the status is not valid, change the background color
                    if (validationStatus != "Valid")
                    {
                        dataItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB1B4");
                    }
                }
            }
        }


        private void AddSuccessMessage(string message)
        {
            DivSuccess.Visible = true;
            lblMessageSuccess.Text = message;

            // Hide the success message after 5 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), "HideSuccess",
                "setTimeout(function() { document.getElementById('" + DivSuccess.ClientID + "').style.display = 'none'; }, 5000);", true);
        }

        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;

            // Hide the error message after 10 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), "HideError",
                "setTimeout(function() { document.getElementById('" + DivFailure.ClientID + "').style.display = 'none'; }, 10000);", true);
        }

        protected void ClearMessages()
        {
            DivSuccess.Visible = false;
            DivFailure.Visible = false;
            lblMessageSuccess.Text = string.Empty;
            lblMessageError.Text = string.Empty;
        }
    }
}

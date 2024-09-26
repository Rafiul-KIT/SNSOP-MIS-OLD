using SSSNP.Service;
using SSSNP_SNSOP.Core.Model.Component2;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class LivelihoodGrantDisbursement : Page
    {
        private bool isSearchClicked = false;
        private Component2Service _component2Service;
        private int totalRecords;

        protected void Page_Load(object sender, EventArgs e)
        {
            // Initialize the service
            _component2Service = new Component2Service();

            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
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

                // Parse the Excel file
                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.PostedFile.InputStream))
                {
                    int userId = 0;
                    userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // Create a list to hold parsed data
                    List<LivelihoodGrantDisbursementModel> grantList = new List<LivelihoodGrantDisbursementModel>();

                    // Loop through worksheet rows and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 contains headers
                    {
                        LivelihoodGrantDisbursementModel grant = new LivelihoodGrantDisbursementModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            PaymentCycle = worksheet.Cells[row, 2].Text, // No conversion needed as it's a string
                            PaymentAmount = decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal paymentAmount) ? paymentAmount : 0m, // Assign default value (0m) if parsing fails
                            PaymentDate = DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime paymentDate) ? paymentDate : DateTime.MinValue, // Assign default date (DateTime.MinValue) if parsing fails
                            CreatedBy = userId,
                            ModifiedBy = userId
                        };




                        grantList.Add(grant);
                    }

                    // Store list in session for further processing
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
            try
            {
                // Retrieve list of grant objects from session
                var grantList = Session["UploadedGrantData"] as List<LivelihoodGrantDisbursementModel>;
                if (grantList == null || grantList.Count == 0)
                {
                    AddErrorMessage("No file found for analysis.");
                    return;
                }

                // Create a DataTable for analyzed data (to display in RadGrid)
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("HouseholdNumber", typeof(string));
                dt.Columns.Add("PaymentCycle", typeof(string));
                dt.Columns.Add("PaymentAmount", typeof(decimal));
                dt.Columns.Add("PaymentDate", typeof(DateTime));

                // Populate DataTable with data from the list
                foreach (var grant in grantList)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = grant.Id;
                    dr["HouseholdNumber"] = grant.HouseholdNumber;
                    dr["PaymentCycle"] = grant.PaymentCycle;
                    dr["PaymentAmount"] = grant.PaymentAmount;
                    dr["PaymentDate"] = grant.PaymentDate;
                    dt.Rows.Add(dr);
                }

                // Bind the data to RadGrid
                RadGrid.DataSource = dt;
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

                // Save data using service layer
                var response = _component2Service.SaveLivelihoodGrantDisbursement(grantList);

                if (response.IsSuccess)
                {
                    AddSuccessMessage("Data imported successfully.");
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
                // Fetch all data without any filters
                DataTable dt = GetGrantDisbursementData(true);

                // Bind the data to RadGrid
                RadGrid.DataSource = dt;
                RadGrid.VirtualItemCount = totalRecords; // Update total record count for pagination
                RadGrid.Rebind();

                if (dt.Rows.Count == 0)
                {
                    AddErrorMessage("No records found.");
                }

                // Set the flag to true indicating the search was performed
                isSearchClicked = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            if (isSearchClicked)
            {
                DataTable dt = GetGrantDisbursementData(true); // Fetch all data
                RadGrid.VirtualItemCount = totalRecords; // Set total number of records for paging
                RadGrid.DataSource = dt;
            }
        }

        protected void RadGrid_PageSizeChanged(object sender, GridPageSizeChangedEventArgs e)
        {
            RadGrid.PageSize = e.NewPageSize;
            RadGrid.Rebind();
        }

        protected void RadGrid_PageIndexChanged(object sender, GridPageChangedEventArgs e)
        {
            RadGrid.CurrentPageIndex = e.NewPageIndex;
            RadGrid.Rebind();
        }

        private DataTable GetGrantDisbursementData(bool fetchAll = false)
        {
            string query;
            if (fetchAll)
            {
                query = @"SELECT Id, HouseholdNumber, PaymentCycle, PaymentAmount, PaymentDate 
                          FROM tbl_com2_Livelihood_Grant_Disbursement
                          ORDER BY Id;";
                totalRecords = 0; // Reset total records to 0 since we'll fetch all
            }
            else
            {
                query = @"SELECT Id, HouseholdNumber, PaymentCycle, PaymentAmount, PaymentDate 
                          FROM tbl_com2_Livelihood_Grant_Disbursement
                          ORDER BY Id
                          OFFSET (@PageIndex - 1) * @PageSize ROWS FETCH NEXT @PageSize ROWS ONLY;

                          -- Total record count
                          SELECT COUNT(*) FROM tbl_com2_Livelihood_Grant_Disbursement;";
            }

            DataTable dt = new DataTable();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (!fetchAll)
                    {
                        cmd.Parameters.AddWithValue("@PageIndex", RadGrid.CurrentPageIndex + 1);
                        cmd.Parameters.AddWithValue("@PageSize", RadGrid.PageSize);
                    }

                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        dt.Load(reader);

                        if (fetchAll)
                        {
                            totalRecords = dt.Rows.Count;
                        }
                        else if (reader.NextResult() && reader.Read())
                        {
                            totalRecords = reader.GetInt32(0);
                        }
                    }
                }
            }

            RadGrid.VirtualItemCount = totalRecords;

            return dt;
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

            // Hide the error message after 5 seconds
            ScriptManager.RegisterStartupScript(this, GetType(), "HideError",
                "setTimeout(function() { document.getElementById('" + DivFailure.ClientID + "').style.display = 'none'; }, 5000);", true);
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

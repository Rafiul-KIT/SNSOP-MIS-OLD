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
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class BeneficiaryRandomizationandEnrollment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
            }
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            // Redirect to the ViewAttachmentHandler to download the Excel file
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=16";
            Response.Redirect(redirectUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            ClearMessages();
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
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    // List to hold beneficiary data
                    List<BeneficiaryRandomizationAndEnrollmentModel> beneficiaries = new List<BeneficiaryRandomizationAndEnrollmentModel>();

                    // Loop through the rows and map the data
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        string householdNumber = worksheet.Cells[row, 1].Text;
                        bool.TryParse(worksheet.Cells[row, 2].Text, out bool interestedInParticipating);
                        bool.TryParse(worksheet.Cells[row, 3].Text, out bool survivesTargeting);
                        bool.TryParse(worksheet.Cells[row, 4].Text, out bool treatmentArm);
                        bool.TryParse(worksheet.Cells[row, 5].Text, out bool hHEnrollmentStatus);

                        int.TryParse(worksheet.Cells[row, 6].Text, out int groupID);
                        int.TryParse(worksheet.Cells[row, 7].Text, out int enrolledMemberID);

                        // Add to the list
                        beneficiaries.Add(new BeneficiaryRandomizationAndEnrollmentModel
                        {
                            HouseholdNumber = householdNumber,
                            InterestedInParticipating = interestedInParticipating,
                            SurvivesTargeting = survivesTargeting,
                            TreatmentArm = treatmentArm,
                            HHEnrollmentStatus = hHEnrollmentStatus,
                            GroupID = groupID,
                            EnrolledMemberID = enrolledMemberID
                        });
                    }

                    // Store in session for later use
                    Session["Beneficiaries"] = beneficiaries;
                    AddSuccessMessage("File uploaded successfully. You can now analyze the data.");
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
                Component2Service service = new Component2Service();

                // Retrieve the list of beneficiaries from session
                List<BeneficiaryRandomizationAndEnrollmentModel> beneficiaries = Session["Beneficiaries"] as List<BeneficiaryRandomizationAndEnrollmentModel>;
                if (beneficiaries == null || beneficiaries.Count == 0)
                {
                    AddErrorMessage("No data available for analysis.");
                    return;
                }

                // Fetch existing HouseholdNumbers from the service to check for redundancy
                Dictionary<string, int> existingHouseholds = service.GetExistingHouseholdNumbers();

                // Create a DataTable to hold the data for display in the grid
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));  // HouseholdId from database (if redundant)
                dt.Columns.Add("HouseholdNumber", typeof(string));
                dt.Columns.Add("InterestedInParticipating", typeof(bool));
                dt.Columns.Add("SurvivesTargeting", typeof(bool));
                dt.Columns.Add("TreatmentArm", typeof(bool));
                dt.Columns.Add("HHEnrollmentStatus", typeof(bool));
                dt.Columns.Add("GroupID", typeof(int));  // Add GroupID
                dt.Columns.Add("EnrolledMemberID", typeof(int));  // Add EnrolledMemberID

                // Loop through the list and dynamically check for redundancy
                foreach (var beneficiary in beneficiaries)
                {
                    DataRow dr = dt.NewRow();
                    dr["HouseholdNumber"] = beneficiary.HouseholdNumber;
                    dr["InterestedInParticipating"] = beneficiary.InterestedInParticipating;
                    dr["SurvivesTargeting"] = beneficiary.SurvivesTargeting;
                    dr["TreatmentArm"] = beneficiary.TreatmentArm;
                    dr["HHEnrollmentStatus"] = beneficiary.HHEnrollmentStatus;
                    dr["GroupID"] = beneficiary.GroupID;  // Add GroupID to DataTable
                    dr["EnrolledMemberID"] = beneficiary.EnrolledMemberID;  // Add EnrolledMemberID to DataTable

                    // Check if the household is redundant
                    if (!string.IsNullOrEmpty(beneficiary.HouseholdNumber) && existingHouseholds.ContainsKey(beneficiary.HouseholdNumber))
                    {
                        dr["Id"] = existingHouseholds[beneficiary.HouseholdNumber]; // Set the HouseholdId for redundant households
                    }
                    else
                    {
                        dr["Id"] = DBNull.Value; // Not redundant, no HouseholdId
                    }

                    dt.Rows.Add(dr);
                }

                // Bind the DataTable to the RadGrid for display
                RadGrid.DataSource = dt;
                RadGrid.DataBind();

                AddSuccessMessage("Data analyzed successfully.");
                btnImport.Enabled = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data analysis failed: " + ex.Message);
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            ClearMessages();
            try
            {
                Component2Service service = new Component2Service();
                List<BeneficiaryRandomizationAndEnrollmentModel> beneficiaries = Session["Beneficiaries"] as List<BeneficiaryRandomizationAndEnrollmentModel>;

                if (beneficiaries == null || beneficiaries.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                // Pass the entire list to the service for processing
                var response = service.SaveBeneficiaries(beneficiaries);
                if (!response.IsSuccess)
                {
                    AddErrorMessage(response.Message);
                }
                else
                {
                    AddSuccessMessage("Data imported successfully.");
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data import failed: " + ex.Message);
            }
        }

        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem) // Check if the item is a data row
            {
                GridDataItem dataItem = (GridDataItem)e.Item;

                // Get the value of the "HouseholdId" field directly
                string householdIdText = dataItem["Id"].Text;

                // If the householdId is not empty or null, mark the row as redundant
                if (!string.IsNullOrEmpty(householdIdText) && householdIdText != "&nbsp;") // Check if householdId is valid
                {
                    dataItem.CssClass = "redundant-row"; // Apply the CSS class for redundant rows
                }
            }
        }

        private void AddSuccessMessage(string message)
        {
            DivSuccess.Visible = true;
            lblMessageSuccess.Text = message;
        }

        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;
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

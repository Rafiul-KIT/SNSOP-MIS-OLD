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
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class BeneficiaryRandomizationandEnrollment : System.Web.UI.Page
    {
        private Component2Service _component2Service = new Component2Service();
        private bool isAnalyzed = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Disable buttons initially
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
                RadGrid.Visible = false;
            }

            btnSearch.Enabled = true;
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            // Redirect to the ViewAttachmentHandler to download the Excel file
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=20";
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
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
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

                        // Use ParseBoolean to parse the Yes/No or 1/0 into a boolean value
                        bool? interestedInParticipating = ParseBoolean(worksheet.Cells[row, 2].Text);
                        bool? survivesTargeting = ParseBoolean(worksheet.Cells[row, 3].Text);
                        bool? treatmentArm = ParseBoolean(worksheet.Cells[row, 4].Text);
                        bool? hHEnrollmentStatus = ParseBoolean(worksheet.Cells[row, 5].Text);

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
                            EnrolledMemberID = enrolledMemberID,
                            CreatedBy = userId,
                            ModifiedBy = userId
                            
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
            Session["IsAnalyzing"] = true;
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
                dt.Columns.Add("ValidationStatus", typeof(string));

                var validHouseholdNumbers = _component2Service.GetValidHouseholdNumberValues();
                var validGroupId = _component2Service.GetValidGroupId();

                var validBeneficiary = new List<BeneficiaryRandomizationAndEnrollmentModel>();
                var uploadedDataList = new List<BeneficiaryEnrollmentAuditModel>();
                int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;

                // Loop through the list and dynamically check for redundancy
                foreach (var beneficiary in beneficiaries)
                {
                    DataRow dr = dt.NewRow();
                    BeneficiaryEnrollmentAuditModel auditEntityObj = new BeneficiaryEnrollmentAuditModel();

                    dr["HouseholdNumber"] = beneficiary.HouseholdNumber ?? (object)DBNull.Value;
                    dr["InterestedInParticipating"] = beneficiary.InterestedInParticipating.HasValue ? (object)beneficiary.InterestedInParticipating.Value : DBNull.Value;
                    dr["SurvivesTargeting"] = beneficiary.SurvivesTargeting.HasValue ? (object)beneficiary.SurvivesTargeting.Value : DBNull.Value;
                    dr["TreatmentArm"] = beneficiary.TreatmentArm.HasValue ? (object)beneficiary.TreatmentArm.Value : DBNull.Value;
                    dr["HHEnrollmentStatus"] = beneficiary.HHEnrollmentStatus.HasValue ? (object)beneficiary.HHEnrollmentStatus.Value : DBNull.Value;
                    dr["GroupID"] = beneficiary.GroupID.HasValue ? (object)beneficiary.GroupID.Value : DBNull.Value;
                    dr["EnrolledMemberID"] = beneficiary.EnrolledMemberID.HasValue ? (object)beneficiary.EnrolledMemberID.Value : DBNull.Value;

                    auditEntityObj.HouseholdNumber = beneficiary.HouseholdNumber;
                    auditEntityObj.InterestedInParticipating = beneficiary.InterestedInParticipating;
                    auditEntityObj.SurvivesTargeting = beneficiary.SurvivesTargeting;
                    auditEntityObj.TreatmentArm = beneficiary.TreatmentArm;
                    auditEntityObj.HHEnrollmentStatus = beneficiary.HHEnrollmentStatus;
                    auditEntityObj.GroupID = beneficiary.GroupID;
                    auditEntityObj.EnrolledMemberID = beneficiary.EnrolledMemberID;

                    // Check if the household is redundant
                    if (!string.IsNullOrEmpty(beneficiary.HouseholdNumber) && existingHouseholds.ContainsKey(beneficiary.HouseholdNumber))
                    {
                        dr["Id"] = existingHouseholds[beneficiary.HouseholdNumber]; // Set the HouseholdId for redundant households
                    }
                    else
                    {
                        dr["Id"] = DBNull.Value; // Not redundant, no HouseholdId
                    }

                    var validationResult = service.IsValidData(beneficiary, validHouseholdNumbers,validGroupId);


                    if (!validationResult.isValid)
                    {
                        auditEntityObj.Validity = false;
                        auditEntityObj.InvalidReason = validationResult.errorMessage;
                        dr["ValidationStatus"] = validationResult.errorMessage; // Store the error message
                    }
                    else
                    {
                        auditEntityObj.Validity = true;
                        dr["ValidationStatus"] = "Valid"; // No error
                        validBeneficiary.Add(beneficiary);
                    }

                    auditEntityObj.UploadedBy = userId;
                    uploadedDataList.Add(auditEntityObj);

                    dt.Rows.Add(dr);
                }


               // Store the DataTable in session for pagination
                Session["AnalyzedData"] = validBeneficiary;
                Session["UploadedBeneficiaryEnrollmentAllData"] = uploadedDataList;

                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = true;

                // Set total record count for analysis
                int totalRecordCount = dt.Rows.Count;
                Session["TotalRecordCount"] = totalRecordCount;

                // Trigger pagination setup for analysis results
                RadGrid.VirtualItemCount = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;

                // Bind the DataTable to the RadGrid for display
                RadGrid.DataSource = null;
                RadGrid.DataSource = dt;
                
                RadGrid.Visible = true;

                AddSuccessMessage("Data analyzed successfully.");
                btnImport.Enabled = true;
                isAnalyzed = true;
                RadGrid.DataBind();
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
                List<BeneficiaryRandomizationAndEnrollmentModel> beneficiaries = Session["AnalyzedData"] as List<BeneficiaryRandomizationAndEnrollmentModel>;

                if (beneficiaries == null || beneficiaries.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    btnImport.Enabled = false;
                    btnAnalyze.Enabled = false;
                    return;
                }

                var allDataList = Session["UploadedBeneficiaryEnrollmentAllData"] as List<BeneficiaryEnrollmentAuditModel>;
                if (allDataList != null && allDataList.Count > 0)
                {
                    _component2Service.SaveAuditLog(allDataList, "tbl_com2_Household_Enrollment_Data_Audit");
                }
                Session["UploadedBeneficiaryEnrollmentAllData"] = null;

                // Pass the entire list to the service for processing
                var response = service.SaveBeneficiaries(beneficiaries);
                if (!response.IsSuccess)
                {
                    AddErrorMessage(response.Message);
                }
                else
                {
                    AddSuccessMessage(response.Message);
                }

                btnImport.Enabled = false;
                btnAnalyze.Enabled = false;

            }
            catch (Exception ex)
            {
                AddErrorMessage("Data import failed: " + ex.Message);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ClearMessages();
            Session["IsAnalyzing"] = false;
            RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = false;
            try
            {
                int totalRecordCount = GetBeneficiaryRandomizationandEnrollmentCount();
                Session["TotalRecordCount"] = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.VirtualItemCount = totalRecordCount; // Set total item count for pagination
                RadGrid.DataSource = null;
                RadGrid.Rebind();
                RadGrid.Visible = true;

            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        private int GetBeneficiaryRandomizationandEnrollmentCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open(); // Ensure the connection is opened

                    string query = @"SELECT COUNT(*) FROM tbl_com2_Household_Enrollment_Data WHERE 1=1;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Error retrieving total record count: " + ex.Message);
            }

            return totalRecords;
        }
        private DataTable GetBeneficiaryRandomizationandEnrollmentData(int pageIndex, int pageSize)
        {
            string query = @"SELECT Id, HouseholdNumber, InterestedInParticipating, SurvivesTargeting, TreatmentArm, HHEnrollmentStatus, GroupID, EnrolledMemberID
                     FROM tbl_com2_Household_Enrollment_Data 
                     ORDER BY Id 
                     OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            DataTable dt = new DataTable();
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        // Set OFFSET and PageSize for pagination
                        cmd.Parameters.AddWithValue("@Offset", pageIndex * pageSize);
                        cmd.Parameters.AddWithValue("@PageSize", pageSize);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Error retrieving data: " + ex.Message);
            }

            return dt;
        }

        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataTable dt;

                if (Session["IsAnalyzing"] != null && (bool)Session["IsAnalyzing"])
                {
                    // Fetch analyzed data from session
                    dt = Session["Beneficiaries"] as DataTable;
                }
                else
                {
                    // Fetch the data for search result
                    dt = GetBeneficiaryRandomizationandEnrollmentData(RadGrid.CurrentPageIndex, RadGrid.PageSize);
                }

                RadGrid.DataSource = dt;

                if (Session["TotalRecordCount"] != null && int.TryParse(Session["TotalRecordCount"].ToString(), out int totalRecordCount))
                {
                    // Set total item count for pagination
                    RadGrid.VirtualItemCount = totalRecordCount;
                }
                else
                {
                    RadGrid.VirtualItemCount = 0; // Default to 0 if no valid count is found
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data binding failed: " + ex.Message);
            }
        }

        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem dataItem)
            {
                // InterestedInParticipating column
                string interestedInParticipating = dataItem["InterestedInParticipating"].Text;
                dataItem["InterestedInParticipating"].Text = interestedInParticipating == "True" ? "Yes" : "No";

                // SurvivesTargeting column
                string survivesTargeting = dataItem["SurvivesTargeting"].Text;
                dataItem["SurvivesTargeting"].Text = survivesTargeting == "True" ? "Yes" : "No";

                // TreatmentArm column
                string treatmentArm = dataItem["TreatmentArm"].Text;
                dataItem["TreatmentArm"].Text = treatmentArm == "True" ? "Yes" : "No";

                // HHEnrollmentStatus column
                string hHEnrollmentStatus = dataItem["HHEnrollmentStatus"].Text;
                dataItem["HHEnrollmentStatus"].Text = hHEnrollmentStatus == "True" ? "Yes" : "No";

                if (isAnalyzed)
                {
                    string validationStatus = dataItem["ValidationStatus"].Text;

                    if (validationStatus != "Valid")
                    {
                        dataItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB1B4");
                    }
                }
            }
        }


        private bool? ParseBoolean(string value)
        {
            if (string.Equals(value, "Yes", StringComparison.OrdinalIgnoreCase) ||
                string.Equals(value, "1", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            else if (string.Equals(value, "No", StringComparison.OrdinalIgnoreCase) ||
                     string.Equals(value, "0", StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }
            else if (bool.TryParse(value, out bool result))
            {
                return result;
            }
            return null; // Return null if the value cannot be parsed
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

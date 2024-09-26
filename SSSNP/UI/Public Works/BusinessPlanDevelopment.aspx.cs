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
    public partial class BusinessPlanDevelopment : Page
    {
        private Component2Service _component2Service;

        protected void Page_Load(object sender, EventArgs e)
        {
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
            // Redirect to the ViewAttachmentHandler to download the Excel file
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=18";
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
                    List<BusinessPlanDevelopmentModel> attendanceList = new List<BusinessPlanDevelopmentModel>();

                    // Loop through the worksheet rows and populate the list
                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++) // Assuming row 1 contains headers
                    {
                        int BusinessTypeVal = 0;
                        string cell3 = (worksheet.Cells[row, 3] != null) ? worksheet.Cells[row, 3].Text.ToString() : null;
                        if (cell3 != null)
                        {
                            if (cell3.ToLower().Equals("farm"))
                            {
                                BusinessTypeVal = 1;
                            } 
                            else if (cell3.ToLower().Equals("non-farm"))
                            {
                                BusinessTypeVal = 2;
                            }
                        }
                        BusinessPlanDevelopmentModel attendance = new BusinessPlanDevelopmentModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            BusinessPlanStatus = (worksheet.Cells[row, 2].Text != null 
                            && (worksheet.Cells[row, 2].Text.ToLower().Equals("yes"))) ? true: false,
                            BusinessType = BusinessTypeVal,
                            CreatedBy = userId,
                            ModifiedBy = userId
                        };

                        attendanceList.Add(attendance);
                    }

                    // Store the list in session for further processing
                    Session["UploadedBusinessPlanData"] = attendanceList;

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
                var list = Session["UploadedBusinessPlanData"] as List<BusinessPlanDevelopmentModel>;
                if (list == null || list.Count == 0)
                {
                    AddErrorMessage("No data found for analysis.");
                    return;
                }

                // Create a DataTable to hold the analyzed data (for RadGrid display)
                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("HouseholdNumber", typeof(string));
                dt.Columns.Add("BusinessPlanStatus", typeof(string));
                dt.Columns.Add("BusinessType", typeof(string));

                // Populate DataTable with the data from the list
                int serial = 1;
                foreach (var obj in list)
                {
                    DataRow dr = dt.NewRow();
                    dr["Id"] = serial;
                    dr["HouseholdNumber"] = obj.HouseholdNumber;
                    dr["BusinessPlanStatus"] = (obj.BusinessPlanStatus == true) ? "YES" : "NO";
                    dr["BusinessType"] = (obj.BusinessType == 1) ? "Farm" : "Non-farm";
                    dt.Rows.Add(dr);

                    serial++;
                }

                // Bind the data to RadGrid for display
                RadGrid.DataSource = dt;
                RadGrid.Visible = true;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.DataBind();
                RadGrid.VirtualItemCount = dt.Rows.Count;

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
                var list = Session["UploadedBusinessPlanData"] as List<BusinessPlanDevelopmentModel>;
                if (list == null || list.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                // Save the data using the service layer
                var response = _component2Service.SaveBusinessPlanDevelopment(list);

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
            ClearMessages();
            try
            {
                RadGrid.Visible = true;
                Session["TotalRecordCount"] = GetBusinessPlanDevelopmentDataCount();
                RadGrid.CurrentPageIndex = 0;
                RadGrid.DataBind();             
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            DataTable dt = GetBusinessPlanDevelopmentData(RadGrid.CurrentPageIndex, RadGrid.PageSize); // Fetch all data
            RadGrid.DataSource = dt;
            Session["TotalRecordCount"] = GetBusinessPlanDevelopmentDataCount();
            RadGrid.VirtualItemCount = (int) Session["TotalRecordCount"];
        }

        private int GetBusinessPlanDevelopmentDataCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open(); // Ensure the connection is opened

                    string query = @"SELECT COUNT(*) FROM tbl_com2_Business_Plan_Development WHERE 1=1;";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        totalRecords = (int)cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return totalRecords;
        }

        private DataTable GetBusinessPlanDevelopmentData(int pageIndex, int pageSize)
        {
            string query = @"SELECT Id, HouseholdNumber, 
            case when BusinessPlanStatus = 1 then 'YES' else 'NO' end as BusinessPlanStatus,
            case when BusinessType = 1 then 'Farm' else 'Non-Farm' end as BusinessType 
            FROM tbl_com2_Business_Plan_Development ORDER BY Id 
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;";

            DataTable dt = new DataTable();
            string connStr = System.Configuration.ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

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

            RadGrid.VirtualItemCount = (int) Session["TotalRecordCount"];

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
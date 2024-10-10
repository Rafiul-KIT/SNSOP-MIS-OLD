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
        protected void Page_Load(object sender, EventArgs e)
        {
            _component2Service = new Component2Service();
            
            if (!IsPostBack)
            {
                btnAnalyze.Enabled = false;
                btnImport.Enabled = false;
                RadGrid.Visible = false;
            }
            btnSearch.Enabled = true;
        }

        protected void lnkDownloadTemplate_Click(object sender, EventArgs e)
        {
            string redirectUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=19";
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

                using (var package = new OfficeOpenXml.ExcelPackage(fileUpload.PostedFile.InputStream))
                {
                    int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                    {
                        AddErrorMessage("The uploaded file does not contain any worksheet.");
                        return;
                    }

                    List<LivelihoodGrantDisbursementModel> grantList = new List<LivelihoodGrantDisbursementModel>();

                    for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                    {
                        var paymentAmount = decimal.TryParse(worksheet.Cells[row, 3].Text, out decimal amount) ? amount : 0m; // Default to 0 if null or invalid
                        var paymentDate = DateTime.TryParse(worksheet.Cells[row, 4].Text, out DateTime date) ? date : DateTime.MinValue; // Default to DateTime.MinValue if null or invalid

                        LivelihoodGrantDisbursementModel grant = new LivelihoodGrantDisbursementModel
                        {
                            HouseholdNumber = worksheet.Cells[row, 1].Text,
                            PaymentCycle = worksheet.Cells[row, 2].Text,
                            PaymentAmount = paymentAmount,
                            PaymentDate = paymentDate,
                            CreatedBy = userId,
                            ModifiedBy = userId
                        };

                        grantList.Add(grant);
                    }

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
            ClearMessages();
            var _component2Service = new Component2Service();
            Session["IsAnalyzing"] = true;

            try
            {
                var grantList = Session["UploadedGrantData"] as List<LivelihoodGrantDisbursementModel>;
                if (grantList == null || grantList.Count == 0)
                {
                    AddErrorMessage("No file found for analysis.");
                    return;
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("HouseholdNumber", typeof(string));
                dt.Columns.Add("PaymentCycle", typeof(string));
                dt.Columns.Add("PaymentAmount", typeof(object));
                dt.Columns.Add("PaymentDate", typeof(object));
                dt.Columns.Add("ValidationStatus", typeof(string));

                int serial = 1;

                var validHouseholdNumbers = _component2Service.GetValidHouseholdNumbers();

                var validGrantList = grantList.Where(grant => !string.IsNullOrWhiteSpace(grant.HouseholdNumber)).ToList();
                var uploadedDataList = new List<LivelihoodGrantDisbursementAuditModel>();
                int userId = Session["uid"] != null ? Convert.ToInt32(Session["uid"].ToString()) : 0;

                foreach (var grant in validGrantList)
                {
                    var validationResult = _component2Service.IsValidData(grant, validHouseholdNumbers);

                    DataRow dr = dt.NewRow();
                    dr["Id"] = serial;
                    dr["HouseholdNumber"] = grant.HouseholdNumber;
                    dr["PaymentCycle"] = grant.PaymentCycle;
                    dr["PaymentAmount"] = grant.PaymentAmount ?? (object)DBNull.Value;
                    dr["PaymentDate"] = grant.PaymentDate.HasValue && grant.PaymentDate.Value != DateTime.MinValue
                                        ? grant.PaymentDate.Value.ToString("MM-dd-yyyy")
                                        : "N/A";

                    LivelihoodGrantDisbursementAuditModel auditEntityObj = new LivelihoodGrantDisbursementAuditModel();
                    auditEntityObj.HouseholdNumber = grant.HouseholdNumber;
                    auditEntityObj.PaymentCycle = grant.PaymentCycle;
                    auditEntityObj.PaymentAmount = grant.PaymentAmount;
                    auditEntityObj.PaymentDate = grant.PaymentDate.Value;

                    if (!validationResult.isValid)
                    {
                        auditEntityObj.Validity = false;
                        auditEntityObj.InvalidReason = validationResult.errorMessage;
                        dr["ValidationStatus"] = validationResult.errorMessage;
                    }
                    else
                    {
                        auditEntityObj.Validity = true;
                        dr["ValidationStatus"] = "Valid";
                    }

                    auditEntityObj.UploadedBy = userId;
                    uploadedDataList.Add(auditEntityObj);

                    dt.Rows.Add(dr);
                    serial++;
                }

                Session["UploadedLivelihoodGrantDisbursementAllData"] = uploadedDataList;

                Session["AnalyzedData"] = dt;
                int totalRecordCount = dt.Rows.Count;
                Session["TotalRecordCount"] = totalRecordCount;
                RadGrid.VirtualItemCount = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.DataSource = null;
                RadGrid.DataSource = dt;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = true;
                RadGrid.DataBind();
                RadGrid.Visible = true;
                

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
            ClearMessages();
            try
            {
                var grantList = Session["UploadedGrantData"] as List<LivelihoodGrantDisbursementModel>;
                if (grantList == null || grantList.Count == 0)
                {
                    AddErrorMessage("No data available for import.");
                    return;
                }

                var allDataList = Session["UploadedLivelihoodGrantDisbursementAllData"] as List<LivelihoodGrantDisbursementAuditModel>;
                if (allDataList != null && allDataList.Count > 0)
                {
                    _component2Service.SaveAuditLog(allDataList, "tbl_com2_Livelihood_Grant_Disbursement_Audit");
                }
                Session["UploadedLivelihoodGrantDisbursementAllData"] = null;

                var validEntries = grantList.Where(a => _component2Service.IsValidData(a, _component2Service.GetValidHouseholdNumbers()).isValid).ToList();
                if (validEntries.Count == 0)
                {
                    AddErrorMessage("No valid data available for import.");
                    return;
                }
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
            ClearMessages();
            Session["IsAnalyzing"] = false;
            try
            {
                int totalRecordCount = GetLivelihoodGrantDisbursementDataCount();
                Session["TotalRecordCount"] = totalRecordCount;
                RadGrid.CurrentPageIndex = 0;
                RadGrid.VirtualItemCount = totalRecordCount;
                RadGrid.DataSource = null;
                RadGrid.MasterTableView.GetColumn("ValidationStatus").Visible = false;
                RadGrid.Rebind();
                RadGrid.Visible = true;
            }
            catch (Exception ex)
            {
                AddErrorMessage("Search failed: " + ex.Message);
            }
        }

        protected void RadGrid_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem dataItem)
            {
                DataTable analyzedData = Session["AnalyzedData"] as DataTable;
                if (Session["IsAnalyzing"] != null && (bool)Session["IsAnalyzing"])
                {
                    
                    if (analyzedData != null)
                    {
                        string householdNumber = dataItem["HouseholdNumber"].Text;

                        var currentHouseholdNumber = householdNumber;

                        var rows = analyzedData.AsEnumerable()
                                               .Where(r => r.Field<string>("HouseholdNumber") == currentHouseholdNumber);

                        if (rows.Any(r => r.Field<string>("ValidationStatus") != "Valid"))
                        {
                            dataItem.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFB1B4");
                        }
                    }
                }
            }
        }



        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                DataTable dt;

                if (Session["IsAnalyzing"] != null && (bool)Session["IsAnalyzing"])
                {
                    dt = Session["AnalyzedData"] as DataTable;
                }
                else
                {
                    dt = GetLivelihoodGrantDisbursementData(RadGrid.CurrentPageIndex, RadGrid.PageSize);
                }
                RadGrid.DataSource = dt;

                if (Session["TotalRecordCount"] != null && int.TryParse(Session["TotalRecordCount"].ToString(), out int totalRecordCount))
                {
                    RadGrid.VirtualItemCount = totalRecordCount;
                }
                else
                {
                    RadGrid.VirtualItemCount = 0;
                }
            }
            catch (Exception ex)
            {
                AddErrorMessage("Data binding failed: " + ex.Message);
            }
        }



        private int GetLivelihoodGrantDisbursementDataCount()
        {
            int totalRecords = 0;
            string connStr = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;

            try
            {
                using (SqlConnection conn = new SqlConnection(connStr))
                {
                    conn.Open();

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

                //RadGrid.VirtualItemCount = GetLivelihoodGrantDisbursementDataCount();
            }
            catch (Exception ex)
            {
                AddErrorMessage("An error occurred while retrieving Livelihood Grant Disbursement data: " + ex.Message);
            }

            return dt;
        }


        private void AddSuccessMessage(string message)
        {
            DivSuccess.Visible = true;
            lblMessageSuccess.Text = message;

            ScriptManager.RegisterStartupScript(this, GetType(), "HideSuccess",
                "setTimeout(function() { document.getElementById('" + DivSuccess.ClientID + "').style.display = 'none'; }, 5000);", true);
        }

        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;

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
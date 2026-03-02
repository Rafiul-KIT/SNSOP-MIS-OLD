using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works
{

    public partial class BeneficiaryList : System.Web.UI.Page
    {
        private LookupManagerService _lookupManagerService;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                InitializeServices();
                if (!IsPostBack)
                {
                    LoadStates();
                    RadGrid.Visible = false;
                }
            }
            catch (Exception ex)
            {
                LogError("Error in Page_Load: " + ex.Message, ex);
                throw;
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
                LogError(new Exception("Error initializing services: " + ex.Message, ex));
            }
        }

        private void LogError(Exception exception)
        {
            throw new NotImplementedException();
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
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                // Create and populate the filter object
                var filters = new BeneficiaryListFilters
                {
                    StateId = !string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "--Select--"
                              ? int.Parse(ddlState.SelectedValue) : (int?)null,
                    CountyId = !string.IsNullOrEmpty(ddlCounty.SelectedValue) && ddlCounty.SelectedValue != "--Select--"
                               ? int.Parse(ddlCounty.SelectedValue) : (int?)null,
                    PayamId = !string.IsNullOrEmpty(ddlPayam.SelectedValue) && ddlPayam.SelectedValue != "--Select--"
                              ? int.Parse(ddlPayam.SelectedValue) : (int?)null,
                    BomaId = !string.IsNullOrEmpty(ddlBoma.SelectedValue) && ddlBoma.SelectedValue != "--Select--"
                             ? int.Parse(ddlBoma.SelectedValue) : (int?)null,
                    EnrollmentNumber = txtEnrollmentNumber.Text.Trim(),
                    Name = txtName.Text.Trim(),
                    HouseholdNumber = txtHouseholdNumber.Text.Trim()
                };

                Session["BeneficiaryListFilters"] = filters;
                ViewState["TotalRecordCount"] = GetTotalRecordCount(filters);

                RadGrid.CurrentPageIndex = 0;
                RadGrid.Rebind();
                RadGrid.Visible = true;
            }
            catch (Exception ex)
            {
                LogError("Error in BtnSearch_Click: " + ex.Message, ex);
            }
        }



        protected void ddlGroup_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void txtEnrollmentNumber_TextChanged(object sender, EventArgs e)
        {

        }

        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCounty.Items.Clear();
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();


            ddlCounty.Items.Add(new ListItem("--Select--", ""));
            ddlPayam.Items.Add(new ListItem("--Select--", ""));
            ddlBoma.Items.Add(new ListItem("--Select--", ""));

            if (!string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "--Select--")
            {
                int stateId;
                if (int.TryParse(ddlState.SelectedValue, out stateId))
                {
                    try
                    {
                        DataTable dtCounties = _lookupManagerService.GetCountyListByStateId(stateId);
                        ddlCounty.DataSource = dtCounties;
                        ddlCounty.DataTextField = "CountieName";
                        ddlCounty.DataValueField = "intCountieid";
                        ddlCounty.DataBind();
                        ddlCounty.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                }
            }
        }

        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();

            ddlPayam.Items.Add(new ListItem("--Select--", ""));
            ddlBoma.Items.Add(new ListItem("--Select--", ""));

            if (!string.IsNullOrEmpty(ddlCounty.SelectedValue) && ddlCounty.SelectedValue != "--Select--")
            {
                int countyId;
                if (int.TryParse(ddlCounty.SelectedValue, out countyId))
                {
                    try
                    {
                        DataTable dtPayams = _lookupManagerService.GetPayamListByCountyId(countyId);
                        ddlPayam.DataSource = dtPayams;
                        ddlPayam.DataTextField = "PayamName";
                        ddlPayam.DataValueField = "intPayamid";
                        ddlPayam.DataBind();
                        ddlPayam.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                }
            }
        }

        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlBoma.Items.Clear();
            ddlBoma.Items.Add(new ListItem("--Select--", ""));

            if (!string.IsNullOrEmpty(ddlPayam.SelectedValue) && ddlPayam.SelectedValue != "--Select--")
            {
                int payamId;
                if (int.TryParse(ddlPayam.SelectedValue, out payamId))
                {
                    try
                    {
                        DataTable dtBomas = _lookupManagerService.GetBomaListByPayamId(payamId);
                        ddlBoma.DataSource = dtBomas;
                        ddlBoma.DataTextField = "BomaName";
                        ddlBoma.DataValueField = "intBomasid";
                        ddlBoma.DataBind();
                        ddlBoma.Items.Insert(0, new ListItem("--Select--", ""));
                    }
                    catch (Exception ex)
                    {
                        LogError(ex);
                    }
                }
            }
        }

        protected void txtName_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtHouseholdNumber_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogError(string message, Exception ex)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                txtEnrollmentNumber.Text = string.Empty;
                txtName.Text = string.Empty;
                txtHouseholdNumber.Text = string.Empty;

                ddlState.ClearSelection();
                ddlState.Items[0].Selected = true;

                ddlCounty.Items.Clear();
                ddlCounty.Items.Add(new ListItem("--Select--", ""));

                ddlPayam.Items.Clear();
                ddlPayam.Items.Add(new ListItem("--Select--", ""));

                ddlBoma.Items.Clear();
                ddlBoma.Items.Add(new ListItem("--Select--", ""));

                RadGrid.DataSource = null;
                RadGrid.DataBind();
                RadGrid.Visible = false;

                Session["RadGridDataSource"] = null;
                Session["BeneficiaryListFilters"] = null;
                ViewState["TotalRecordCount"] = null;

                lblMessageSuccess.Text = string.Empty;
                lblMessageError.Text = string.Empty;

                ddlState.Focus();
            }
            catch (Exception ex)
            {
                LogError("Error in BtnClear_Click: " + ex.Message, ex);
            }
        }


        protected void ddlBoma_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void RadGrid_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                int pageSize = RadGrid.PageSize;
                int currentPageIndex = RadGrid.CurrentPageIndex;
                int offset = currentPageIndex * pageSize;

                var filters = Session["BeneficiaryListFilters"] as BeneficiaryListFilters;

                if (filters == null)
                {
                    filters = new BeneficiaryListFilters(); 
                }

                string query = @"SELECT
                            bd.intBenid,
                            s.StateName,
                            c.CountieName AS CountyName,
                            p.PayamName,
                            b.BomaName,
                            bd.NameofHousehold AS Name,
                            bd.Sex,
                            bd.Age,
                            bd.ContactNo,
                            bd.Email,
                            bd.EnrollmentNo,
                            bd.BeneficiaryType AS Component
                        FROM 
                            tbl_BeneficiaryDet bd
                        LEFT JOIN tm_States s ON bd.intStateid = s.intStateid
                        LEFT JOIN tm_Counties c ON bd.intCountieid = c.intCountieid
                        LEFT JOIN tm_Payams p ON bd.intPayamid = p.intPayamid
                        LEFT JOIN tm_Bomas b ON bd.intBomasid = b.intBomasid
                        WHERE id_card_generation_status = 1";

                var parameters = new List<SqlParameter>();

                if (filters.StateId.HasValue)
                {
                    query += " AND bd.intStateid = @StateId";
                    parameters.Add(new SqlParameter("@StateId", filters.StateId.Value));
                }
                if (filters.CountyId.HasValue)
                {
                    query += " AND bd.intCountieid = @CountyId";
                    parameters.Add(new SqlParameter("@CountyId", filters.CountyId.Value));
                }
                if (filters.PayamId.HasValue)
                {
                    query += " AND bd.intPayamid = @PayamId";
                    parameters.Add(new SqlParameter("@PayamId", filters.PayamId.Value));
                }
                if (filters.BomaId.HasValue)
                {
                    query += " AND bd.intBomasid = @BomaId";
                    parameters.Add(new SqlParameter("@BomaId", filters.BomaId.Value));
                }
                if (!string.IsNullOrEmpty(filters.EnrollmentNumber))
                {
                    query += " AND bd.EnrollmentNo LIKE @EnrollmentNumber";
                    parameters.Add(new SqlParameter("@EnrollmentNumber", $"%{filters.EnrollmentNumber}%"));
                }
                if (!string.IsNullOrEmpty(filters.Name))
                {
                    query += " AND bd.NameofHousehold LIKE @Name";
                    parameters.Add(new SqlParameter("@Name", $"%{filters.Name}%"));
                }
                if (!string.IsNullOrEmpty(filters.HouseholdNumber))
                {
                    query += " AND bd.household_number LIKE @HouseholdNumber";
                    parameters.Add(new SqlParameter("@HouseholdNumber", $"%{filters.HouseholdNumber}%"));
                }

                query += " ORDER BY bd.intBenid OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
                parameters.Add(new SqlParameter("@Offset", offset));
                parameters.Add(new SqlParameter("@PageSize", pageSize));

                DataTable dtResults = _lookupManagerService.ExecuteQuery(query, CommandType.Text, parameters);

                RadGrid.DataSource = dtResults;

                RadGrid.VirtualItemCount = (int)ViewState["TotalRecordCount"];
            }
            catch (Exception ex)
            {
                LogError("Error in RadGrid_NeedDataSource: " + ex.Message, ex);
            }
        }


        protected void RadGrid_ItemCommand(object sender, Telerik.Web.UI.GridCommandEventArgs e)
        {
            if (e.CommandName == "Download")
            {
                try
                {
                    string commandArgument = e.CommandArgument.ToString();

                    if (int.TryParse(commandArgument, out int benId))
                    {
                        DownloadIdCard(benId);
                    }
                    else
                    {
                        lblMessageError.Text = "Invalid ID value. Unable to process the download.";
                        lblMessageError.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    LogError("Error in RadGrid_ItemCommand: " + ex.Message, ex);
                }
            }
        }


        private int GetTotalRecordCount(BeneficiaryListFilters filters)
        {
            try
            {
                string query = @"SELECT COUNT(*)
                         FROM tbl_BeneficiaryDet bd
      
                         WHERE id_card_generation_status = 1";

                var parameters = new List<SqlParameter>();

                if (filters.StateId.HasValue)
                {
                    query += " AND bd.intStateid = @StateId";
                    parameters.Add(new SqlParameter("@StateId", filters.StateId.Value));
                }
                if (filters.CountyId.HasValue)
                {
                    query += " AND bd.intCountieid = @CountyId";
                    parameters.Add(new SqlParameter("@CountyId", filters.CountyId.Value));
                }
                if (filters.PayamId.HasValue)
                {
                    query += " AND bd.intPayamid = @PayamId";
                    parameters.Add(new SqlParameter("@PayamId", filters.PayamId.Value));
                }
                if (filters.BomaId.HasValue)
                {
                    query += " AND bd.intBomasid = @BomaId";
                    parameters.Add(new SqlParameter("@BomaId", filters.BomaId.Value));
                }
                if (!string.IsNullOrEmpty(filters.EnrollmentNumber))
                {
                    query += " AND bd.EnrollmentNo LIKE @EnrollmentNumber";
                    parameters.Add(new SqlParameter("@EnrollmentNumber", $"%{filters.EnrollmentNumber}%"));
                }
                if (!string.IsNullOrEmpty(filters.Name))
                {
                    query += " AND bd.NameofHousehold LIKE @Name";
                    parameters.Add(new SqlParameter("@Name", $"%{filters.Name}%"));
                }
                if (!string.IsNullOrEmpty(filters.HouseholdNumber))
                {
                    query += " AND bd.household_number LIKE @HouseholdNumber";
                    parameters.Add(new SqlParameter("@HouseholdNumber", $"%{filters.HouseholdNumber}%"));
                }


                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.CommandType = CommandType.Text;

                        if (parameters != null)
                        {
                            foreach (var parameter in parameters)
                            {
                                command.Parameters.Add(parameter);
                            }
                        }
                        connection.Open();
                        return (int)command.ExecuteScalar();
                    } 
                } 

            }
            catch (Exception ex)
            {
                LogError("Error in GetTotalRecordCount: " + ex.Message, ex);
                return 0;
            }
        }



        private void DownloadIdCard(int benId)
        {
            try
            {
                int attachmentId = benId;
                int attachmentType = 24;

                string redirectUrl = $"~/ViewAttachmentHandler.aspx?AttachmentType={attachmentType}&AttachmentValue={attachmentId}";

                Response.Redirect(redirectUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMessageError.Text = "An issue occurred during file download redirection.";
                lblMessageError.Visible = true;

                Console.WriteLine("An issue occurred during file download redirection." + ex.Message); // Replace with actual logging
            }
        }

        private class BeneficiaryListFilters
        {
            public int? StateId { get; set; }
            public int? CountyId { get; set; }
            public int? PayamId { get; set; }
            public int? BomaId { get; set; }
            public string EnrollmentNumber { get; set; }
            public string Name { get; set; }
            public string HouseholdNumber { get; set; }
        }


    }
}
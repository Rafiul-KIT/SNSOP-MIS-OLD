using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

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
                // Collect filter values
                int stateId = !string.IsNullOrEmpty(ddlState.SelectedValue) && ddlState.SelectedValue != "--Select--"
                              ? int.Parse(ddlState.SelectedValue) : 0;

                int countyId = !string.IsNullOrEmpty(ddlCounty.SelectedValue) && ddlCounty.SelectedValue != "--Select--"
                               ? int.Parse(ddlCounty.SelectedValue) : 0;

                int payamId = !string.IsNullOrEmpty(ddlPayam.SelectedValue) && ddlPayam.SelectedValue != "--Select--"
                              ? int.Parse(ddlPayam.SelectedValue) : 0;

                int bomaId = !string.IsNullOrEmpty(ddlBoma.SelectedValue) && ddlBoma.SelectedValue != "--Select--"
                             ? int.Parse(ddlBoma.SelectedValue) : 0;

                string enrollmentNumber = txtEnrollmentNumber.Text.Trim();
                //string groupType = ddlGroup.SelectedValue.Trim();
                string name = txtName.Text.Trim();
                string householdNumber = txtHouseholdNumber.Text.Trim();

                // Base query
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
                            WHERE 1=1";

                // Add filters dynamically
                var parameters = new List<SqlParameter>();

                if (stateId > 0)
                {
                    query += " AND bd.intStateid = @StateId";
                    parameters.Add(new SqlParameter("@StateId", stateId));
                }
                if (countyId > 0)
                {
                    query += " AND bd.intCountieid = @CountyId";
                    parameters.Add(new SqlParameter("@CountyId", countyId));
                }
                if (payamId > 0)
                {
                    query += " AND bd.intPayamid = @PayamId";
                    parameters.Add(new SqlParameter("@PayamId", payamId));
                }
                if (bomaId > 0)
                {
                    query += " AND bd.intBomasid = @BomaId";
                    parameters.Add(new SqlParameter("@BomaId", bomaId));
                }
                if (!string.IsNullOrEmpty(enrollmentNumber))
                {
                    query += " AND bd.EnrollmentNo LIKE @EnrollmentNumber";
                    parameters.Add(new SqlParameter("@EnrollmentNumber", $"%{enrollmentNumber}%"));
                }
                /*if (!string.IsNullOrEmpty(groupType) && groupType != "--Select--")
                {
                    query += " AND bd.BeneficiaryType = @GroupType";
                    parameters.Add(new SqlParameter("@GroupType", groupType));
                }*/
                if (!string.IsNullOrEmpty(name))
                {
                    query += " AND bd.NameofHousehold LIKE @Name";
                    parameters.Add(new SqlParameter("@Name", $"%{name}%"));
                }
                if (!string.IsNullOrEmpty(householdNumber))
                {
                    query += " AND bd.household_number LIKE @HouseholdNumber";
                    parameters.Add(new SqlParameter("@HouseholdNumber", $"%{householdNumber}%"));
                }

                // Execute query using _dbOperation
                DataTable dtResults = _lookupManagerService.ExecuteQuery(query, CommandType.Text, parameters);

                // Store results in session
                Session["RadGridDataSource"] = dtResults;

                // Rebind RadGrid
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
            // Implement your error logging logic here
            System.Diagnostics.Debug.WriteLine(message);
            // Example: ErrorLogger.LogError(ex);
        }

        protected void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                // Clear all textboxes
                txtEnrollmentNumber.Text = string.Empty;
                txtName.Text = string.Empty;
                txtHouseholdNumber.Text = string.Empty;

                // Reset all dropdowns to default
                ddlState.ClearSelection();
                ddlState.Items[0].Selected = true;

                ddlCounty.Items.Clear();
                ddlCounty.Items.Add(new ListItem("--Select--", ""));

                ddlPayam.Items.Clear();
                ddlPayam.Items.Add(new ListItem("--Select--", ""));

                ddlBoma.Items.Clear();
                ddlBoma.Items.Add(new ListItem("--Select--", ""));

                /*ddlGroup.ClearSelection();
                ddlGroup.Items[0].Selected = true;*/

                // Clear any grid data
                RadGrid.DataSource = null;
                RadGrid.DataBind();
                RadGrid.Visible = false;

                // Optionally clear any session data for the grid
                Session["RadGridDataSource"] = null;

                // Clear messages (success/error labels)
                lblMessageSuccess.Text = string.Empty;
                lblMessageError.Text = string.Empty;

                // Optionally set focus to the first field
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

        protected void RadGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            try
            {
                // Check if the data is already stored in the session
                if (Session["RadGridDataSource"] != null)
                {
                    RadGrid.DataSource = Session["RadGridDataSource"] as DataTable;
                }
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
                    // Get CommandArgument as a string
                    string commandArgument = e.CommandArgument.ToString();

                    // Parse the CommandArgument into an integer
                    if (int.TryParse(commandArgument, out int benId))
                    {
                        // Successfully parsed, proceed with downloading
                        DownloadIdCard(benId);
                    }
                    else
                    {
                        // Handle cases where parsing fails
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

        private void DownloadIdCard(int benId)
        {
            try
            {
                int attachmentId = benId;
                int attachmentType = 24;

                string redirectUrl = $"~/ViewAttachmentHandler.aspx?AttachmentType={attachmentType}&AttachmentValue={attachmentId}";

                // Perform the redirect
                Response.Redirect(redirectUrl, false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                lblMessageError.Text = "An issue occurred during file download redirection.";
                lblMessageError.Visible = true;
                // Handle any unexpected issues gracefully
                Console.WriteLine("An issue occurred during file download redirection." + ex.Message); // Replace with actual logging
            }
        }


    }
}
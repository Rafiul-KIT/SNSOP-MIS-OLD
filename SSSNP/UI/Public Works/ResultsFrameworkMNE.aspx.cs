using SSSNP.Service;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class ResultsFrameworkMNE : System.Web.UI.Page
    {
        private LookupManagerService _lookupManagerService;
        private IndicatorResultService indicatorResultService;

        protected void Page_Load(object sender, EventArgs e)
        {
            InitializeServices();
            if (!IsPostBack)
            {
                LoadInitialData();
                rgResults.Visible = false; // Initially hide the grid
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
                indicatorResultService = new IndicatorResultService(connectionString);
            }
            catch (Exception ex)
            {
                // Log the error instead of ignoring it
                System.Diagnostics.Debug.WriteLine("Error in InitializeServices: " + ex.Message);
                throw; // Rethrow the exception to see the actual problem
            }
        }

        private void LoadInitialData()
        {
            LoadDropdownData();

            // Populate fixed dropdowns for Payment Cycle and Status
            for (int i = 1; i <= 9; i++)
            {
                ddlPaymentCycle.Items.Add(new ListItem($"Payment Cycle {i}", i.ToString()));
            }

            ddlIndicatorType.Items.Add(new ListItem("Project Development Objective Indicators by Objectives/Outcomes", "1"));
            ddlIndicatorType.Items.Add(new ListItem("Monitoring & Evaluation Plan: Intermediate Results Indicators", "2"));

            LoadLegalStatus();
        }

        private void LoadLegalStatus()
        {
            ddlLegalStatus.Items.Add(new ListItem("Host", "HOST"));
            ddlLegalStatus.Items.Add(new ListItem("Refugee", "REFUGEE"));
            ddlLegalStatus.Items.Add(new ListItem("Returnee", "RETURNEE"));
            ddlLegalStatus.Items.Add(new ListItem("IDP", "IDP"));

            ddlSex.Items.Add(new ListItem("Male", "M"));
            ddlSex.Items.Add(new ListItem("Female", "F"));

            ddlSupportType.Items.Add(new ListItem("Component 1 (Public Works)", "Component 1 (Public Works)"));
            ddlSupportType.Items.Add(new ListItem("Component 1 (Direct Income Support)", "Component 1 (Direct Income Support)"));

            ddlAge.Items.Add(new ListItem("Child (0-17)", "0-17"));
            ddlAge.Items.Add(new ListItem("Youth (18-35)", "18-35"));
            ddlAge.Items.Add(new ListItem("Middle Age (36-53)", "36-53"));
            ddlAge.Items.Add(new ListItem("Old (54+)", "54-125"));

            ddlGrievanceCategory.Items.Add(new ListItem("Non-GBV", "N"));
            ddlGrievanceCategory.Items.Add(new ListItem("GBV", "G"));
        }

        private void LoadDropdownData()
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

        protected void ddlIndicatorType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlIndicator.Items.Clear();
            ddlIndicator.Items.Add(new ListItem("--Select--", ""));
            if (!string.IsNullOrEmpty(ddlIndicatorType.SelectedValue))
            {
                try
                {
                    //int indicatorType = int.Parse(ddlIndicatorType.SelectedValue?.ToString());
                    int indicatorType;
                    if (!int.TryParse(ddlIndicatorType.SelectedValue, out indicatorType))
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid Indicator Type: " + ddlIndicatorType.SelectedValue);
                        return;
                    }
                    if (indicatorType == 1)
                    {
                        ddlIndicator.Items.Add(new ListItem("Beneficiaries of social safety net programs", "1"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiaries of social safety net programs - Female", "2"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiary households of social safety net programs- Refugees", "3"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiary households of social safety net programs- Host Communities", "4"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiary households of social safety net programs- Female Refugees (Number)", "5"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiary households of social safety net programs- Female Host Communities (Number)", "6"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiary and non-beneficiary households reporting satisfaction with community assets created through LIPW (Outside MIS-Survey)", "7"));
                        ddlIndicator.Items.Add(new ListItem("Beneficiary households receiving economic opportunities", "8"));
                        ddlIndicator.Items.Add(new ListItem("Eligible beneficiary households with functional income-generating investments four months after the receipt of the full economic opportunities package", "9"));
                        ddlIndicator.Items.Add(new ListItem("Percentage of beneficiaries paid using the enhanced integrated biometric and Management Information Systems", "10"));
                    }
                    else if (indicatorType == 2)
                    {
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households receiving cash for performing labor intensive public works", "11"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiaries receiving cash for performing labor intensive public works who are female", "12"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households participating in all sessions of the in-depth Cash Plus training", "13"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households participating in all the sessions of the in-depth Cash Plus training - refugees number", "14"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households participating in all the sessions of the in-depth Cash Plus training - host communities", "15"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households receiving cash transfer for participating in the behavioral change communication training", "16"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households receiving Direct Income Support", "17"));
                        ddlIndicator.Items.Add(new ListItem("Percentage of Labor Intensive Public Works subprojects that mitigate climate-induced shocks", "18"));
                        ddlIndicator.Items.Add(new ListItem("Number of LIPW work days created", "19"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiaries receiving cash for performing labor intensive public works who are refugees or host communities", "20"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiary households receiving Direct Income Support who have a female primary beneficiary (Number)", "21"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiaries receiving economic opportunities who are youth", "22"));
                        ddlIndicator.Items.Add(new ListItem("Number of beneficiaries receiving Economic Opportunities who are female youth", "23"));
                        ddlIndicator.Items.Add(new ListItem("Percentage of grievances resolved through the GRM", "24"));
                        ddlIndicator.Items.Add(new ListItem("Percentage of cash transfers to beneficiary households made on time", "25"));
                        ddlIndicator.Items.Add(new ListItem("Increased participation by women in community level governance and coordination structures", "26"));
                        ddlIndicator.Items.Add(new ListItem("Capacity Building Plan for Safety Net is implemented by the Ministry of Gender, Child and Social Welfare", "27"));
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error in ddlIndicatorType_SelectedIndexChanged: " + ex.Message);
                    throw;
                }
            }
        }

        private void initSearchCriteria()
        {
            ddlState.SelectedIndex = 0;
            ddlState_SelectedIndexChanged(null, null);
            ddlCounty_SelectedIndexChanged(null, null);
            ddlPayam_SelectedIndexChanged(null, null);
            ddlSex.SelectedIndex = 0;
            ddlPaymentCycle.SelectedIndex = 0;
            ddlAge.SelectedIndex = 0;
            ddlGrievanceCategory.SelectedIndex = 0;
            ddlLegalStatus.SelectedIndex = 0;
            ddlSupportType.SelectedIndex = 0;
        }

        protected void ddlIndicator_SelectedIndexChanged(object sender, EventArgs e)
        {
            changeAllFilterVisibility(false);

            if (!string.IsNullOrEmpty(ddlIndicator.SelectedValue))
            {
                try
                {
                    initSearchCriteria();
                    //int indicator = int.Parse(ddlIndicator.SelectedValue?.ToString());
                    int indicator;
                    if (!int.TryParse(ddlIndicator.SelectedValue, out indicator))
                    {
                        System.Diagnostics.Debug.WriteLine("Invalid Indicator Value: " + ddlIndicator.SelectedValue);
                        return; // Stop execution if parsing fails
                    }
                    switch (indicator)
                    {
                        case 1:
                        case 10:
                            SexTR.Visible = true;
                            AgeRangeTR.Visible = true;
                            SupportTypeTR.Visible = true;
                            break;
                        case 2:
                        case 3:
                        case 4:
                            AgeRangeTR.Visible = true;
                            SupportTypeTR.Visible = true;
                            break;
                        case 5:
                        case 6:
                            SupportTypeTR.Visible = true;
                            break;
                        case 8:
                            SexTR.Visible = true;
                            SupportTypeTR.Visible = true;
                            break;
                        case 9:
                        case 14:
                        case 15:
                            SexTR.Visible = true;
                            AgeRangeTR.Visible = true;
                            break;
                        case 11:
                            PaymentCycleTR.Visible = true;
                            AgeRangeTR.Visible = true;
                            SexTR.Visible = true;
                            break;
                        case 12:
                        case 19:
                            PaymentCycleTR.Visible = true;
                            break;
                        case 13:
                            PaymentCycleTR.Visible = true;
                            AgeRangeTR.Visible = true;
                            SexTR.Visible = true;
                            LegalStatusTR.Visible = true;
                            break;
                        case 16:
                            SexTR.Visible = true;
                            break;
                        case 17:
                            SexTR.Visible = true;
                            PaymentCycleTR.Visible = true;
                            break;
                        case 20:
                        case 22:
                            SexTR.Visible = true;
                            LegalStatusTR.Visible = true;
                            break;
                        case 21:
                        case 23:
                            LegalStatusTR.Visible = true;
                            break;
                        case 24:
                            GrievanceCategoryTR.Visible = true;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine("Error in ddlIndicator_SelectedIndexChanged: " + ex.Message);
                    throw;
                }
            }
        }

        private void changeAllFilterVisibility(bool value)
        {
            SexTR.Visible = value;
            AgeRangeTR.Visible = value;
            SupportTypeTR.Visible = value;
            LegalStatusTR.Visible = value;
            PaymentCycleTR.Visible = value;
            GrievanceCategoryTR.Visible = value;
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            rgResults.DataSource = null;
            rgResults.Rebind();

            if (ddlIndicator.SelectedValue?.ToString() != null && !ddlIndicator.SelectedValue.ToString().Equals("--Select Indicator--"))
            {
                DataTable dt = new DataTable();
                IndicatorResultQuery request = prepareRequestModel();
                if (request == null || request.indicatorId == null || request.indicatorId == 0)
                {
                    return;
                }
                switch (request.indicatorId)
                {
                    case 1:
                        dt = indicatorResultService.GetBeneficiariesOfSocialSafetyNetPrograms(request);
                        rgResults.DataSource = dt;
                        break;
                    case 2:
                        request.sex = "F";
                        dt = indicatorResultService.GetBeneficiariesOfSocialSafetyNetPrograms(request);
                        rgResults.DataSource = dt;
                        break;
                    case 3:
                        request.legalStatus = "REFUGEE";
                        dt = indicatorResultService.GetBeneficiariesOfSocialSafetyNetPrograms(request);
                        rgResults.DataSource = dt;
                        break;
                    case 4:
                        request.legalStatus = "HOST";
                        dt = indicatorResultService.GetBeneficiariesOfSocialSafetyNetPrograms(request);
                        rgResults.DataSource = dt;
                        break;
                    case 5:
                        request.legalStatus = "REFUGEE";
                        request.sex = "F";
                        dt = indicatorResultService.GetBeneficiariesOfSocialSafetyNetPrograms(request);
                        rgResults.DataSource = dt;
                        break;
                    case 6:
                        request.legalStatus = "HOST";
                        request.sex = "F";
                        dt = indicatorResultService.GetBeneficiariesOfSocialSafetyNetPrograms(request);
                        rgResults.DataSource = dt;
                        break;
                    case 7:
                        break; // Not in db yet
                    case 8:
                        dt = indicatorResultService.GetBeneficiariesReceivingEconomicOpportunities(request);
                        rgResults.DataSource = dt;
                        break;
                    case 9:
                        break; // Not in db yet
                    case 10:
                        break; // Not in db yet
                    case 11:
                        dt = indicatorResultService.GetBeneficiariesReceivingCash(request, true);
                        rgResults.DataSource = dt;
                        break;
                    case 12:
                        request.sex = "F";
                        dt = indicatorResultService.GetBeneficiariesReceivingCash(request, true);
                        rgResults.DataSource = dt;
                        break;
                    case 13:
                        dt = indicatorResultService.GetBeneficiariesParticipatingInAllSessions(request);
                        rgResults.DataSource = dt;
                        break;
                    case 14:
                        request.legalStatus = "REFUGEE";
                        dt = indicatorResultService.GetBeneficiariesParticipatingInAllSessions(request);
                        rgResults.DataSource = dt;
                        break;
                    case 15:
                        request.legalStatus = "HOST";
                        dt = indicatorResultService.GetBeneficiariesParticipatingInAllSessions(request);
                        rgResults.DataSource = dt;
                        break;
                    case 16:
                        rgResults.DataSource = dt;
                        break; // Not in db yet
                    case 17:
                        dt = indicatorResultService.GetBeneficiariesReceivingCash(request, false);
                        rgResults.DataSource = dt;
                        break;
                    case 18:
                        dt = indicatorResultService.GetClimateSmartWorkPercentage(request);
                        rgResults.DataSource = dt;
                        break;
                    case 19:
                        dt = indicatorResultService.GetNumberOfLIPWDays(request);
                        rgResults.DataSource = dt;
                        break;
                    case 20:
                        request.legalStatus = "('REFUGEE', 'HOST')";
                        dt = indicatorResultService.GetBeneficiariesReceivingCash(request, true);
                        rgResults.DataSource = dt;
                        break;
                    case 21:
                        request.sex = "F";
                        dt = indicatorResultService.GetBeneficiariesReceivingCash(request, false);
                        rgResults.DataSource = dt;
                        break;
                    case 22:
                        request.ageRange = "18-35";
                        dt = indicatorResultService.GetBeneficiariesReceivingEconomicOpportunities(request);
                        rgResults.DataSource = dt;
                        break;
                    case 23:
                        request.ageRange = "18-35";
                        request.sex = "F";
                        dt = indicatorResultService.GetBeneficiariesReceivingEconomicOpportunities(request);
                        rgResults.DataSource = dt;
                        break;
                    case 24:
                        dt = indicatorResultService.GetGrievanceResolvedPercentage(request);
                        rgResults.DataSource = dt;
                        break;
                    case 25:
                        dt = indicatorResultService.GetCashTransferMadeOnTimePercentage(request);
                        rgResults.DataSource = dt;
                        break;
                    case 26:
                        break; // Not in db yet
                }

                rgResults.Visible = true;
                rgResults.VirtualItemCount = 1;
                rgResults.CurrentPageIndex = 0;
                rgResults.Rebind();
            }
        }

        private IndicatorResultQuery prepareRequestModel()
        {
            try
            {
                IndicatorResultQuery request = new IndicatorResultQuery();

                if (int.TryParse(ddlIndicator.SelectedValue, out int indicatorId) && indicatorId != 0)
                {
                    request.indicatorId = indicatorId;
                }

                if (int.TryParse(ddlState.SelectedValue, out int stateId) && stateId != 0)
                {
                    request.stateId = stateId;
                }

                if (int.TryParse(ddlCounty.SelectedValue, out int countyId) && countyId != 0)
                {
                    request.countyId = countyId;
                }

                if (int.TryParse(ddlPayam.SelectedValue, out int payamId) && payamId != 0)
                {
                    request.payamId = payamId;
                }

                if (int.TryParse(ddlBoma.SelectedValue, out int bomaId) && bomaId != 0)
                {
                    request.bomaId = bomaId;
                }

                request.supportType = !string.IsNullOrEmpty(ddlSupportType.SelectedValue) && ddlSupportType.SelectedValue != "--Select--"
                    ? ddlSupportType.SelectedValue
                    : null;

                request.legalStatus = !string.IsNullOrEmpty(ddlLegalStatus.SelectedValue) && ddlLegalStatus.SelectedValue != "--Select--"
                    ? ddlLegalStatus.SelectedValue
                    : null;

                request.paymentCycle = !string.IsNullOrEmpty(ddlPaymentCycle.SelectedValue) && ddlPaymentCycle.SelectedValue != "--Select--"
                    ? ddlPaymentCycle.SelectedValue
                    : null;

                request.sex = !string.IsNullOrEmpty(ddlSex.SelectedValue) && ddlSex.SelectedValue != "--Select--"
                    ? ddlSex.SelectedValue
                    : null;

                request.ageRange = !string.IsNullOrEmpty(ddlAge.SelectedValue) && ddlAge.SelectedValue != "--Select--"
                    ? ddlAge.SelectedValue
                    : null;

                request.grievanceCategory = !string.IsNullOrEmpty(ddlGrievanceCategory.SelectedValue) && ddlGrievanceCategory.SelectedValue != "--Select--"
                    ? ddlGrievanceCategory.SelectedValue
                    : null;

                return request;
            }
            catch (Exception e)
            {

            }
            return null;
        }

        protected void rgResults_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            try
            {
                string state = ddlState.SelectedValue;
                string county = ddlCounty.SelectedValue;
                string payam = ddlPayam.SelectedValue;
                string boma = ddlBoma.SelectedValue;
                string paymentCycle = ddlPaymentCycle.SelectedValue;

                //DataTable dt = GetPaymentData(state, county, payam, boma, paymentCycle, status, component, paidStatus, rgResults.CurrentPageIndex, rgResults.PageSize);//to store only grid data
                //rgResults.DataSource = dt;
                //rgResults.VirtualItemCount = (int)Session["TotalRecordCount"]; // Set the total number of records
            }
            catch (Exception ex)
            {
                //LogError("Error in rgResults_NeedDataSource: " + ex.Message, ex);
                throw;
            }
        }
    }
}
using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static SSSNP_SNSOP.UI.Public_Works.GBVManagement;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class GBVDetailManagement : System.Web.UI.Page
    {
        private GBVMain GetGBVMain
        {
            get
            {
                if (this.GBVMainId != 0)
                {
                    return SSSNPContext.SSSNPManager.GetGBVMain(this.GBVMainId);
                }
                else if (this.GBVDetailId != 0)
                {
                    GBVDetail _detail = SSSNPContext.SSSNPManager.GetGBVDetail(this.GBVDetailId);
                    if (_detail == null || _detail.GBVMainID == 0) return null;
                    return SSSNPContext.SSSNPManager.GetGBVMain(_detail.GBVMainID);
                }

                return null;
            }
        }
        private RequestOperationTypePage OperationType
        {
            get
            {
                if (Request.QueryString["oType"] == null) return RequestOperationTypePage.NEW;
                if (Request.QueryString["oType"] == "2") return RequestOperationTypePage.EDIT;
                return RequestOperationTypePage.NEW;
            }
        }
        private int GBVMainId
        {
            get
            {
                int result = 0;
                if (Request.QueryString["mainId"] == null) return 0;
                int.TryParse(Request.QueryString["mainId"].ToString(), out result);
                return result;
            }
        }
        private int GBVDetailId
        {
            get
            {
                int result = 0;
                if (Request.QueryString["Id"] == null) return 0;
                int.TryParse(Request.QueryString["Id"].ToString(), out result);
                return result;
            }
        }
        private void ClearControls()
        {
            
            txtReportedDetails.Text = "";
            txtSurvivorDetails.Text = "";
            txtDateOfClosed.Text = "";
            txtFollowUpAction.Text = "";
            ddlIsSurvivorSatisfied.SelectedValue = "0";
            ddlRepotedTo.SelectedValue = "0";

        }
        private void DisableControls(bool value)
        {
            
            txtReportedDetails.Enabled = !value;
            txtSurvivorDetails.Enabled = !value;
            txtDateOfClosed.Enabled = !value;
            txtFollowUpAction.Enabled = !value;
            ddlIsSurvivorSatisfied.Enabled = !value;
            ddlRepotedTo.Enabled = !value;

        }
        public string DisableTheButton(Control pge, Control btn, string vldGroup)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
            if (vldGroup.Trim() == "")
            {
                sb.Append("if (Page_ClientValidate() == false) { return false; }} ");

            }
            else
            {
                sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");

            }
            sb.Append("this.value = 'Please wait...';");
            sb.Append("this.disabled = true;");
            sb.Append(pge.Page.GetPostBackEventReference(btn));
            sb.Append(";");
            return sb.ToString();
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
          

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count <= 0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);
                return;
                //Response.Redirect("../../frmUserLogin.aspx"); 
            }

            this.BtnSave.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave, "group"));

      
            if (!Page.IsPostBack)
            {
                if (OperationType == RequestOperationTypePage.NEW && GBVMainId != 0)
                {
                    PrepareNewEnvironmentGBV();

                    if (CUser.Current.IsIPRole || CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole || CUser.Current.IsGenderSpecRole)
                    {
                        BtnSave.Enabled = true;
                    }
                    else
                    {

                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Create GBV Detail";
                    }

                }
                else if (OperationType == RequestOperationTypePage.EDIT && GBVDetailId != 0)
                {
                    LoadEnvironmentGBV();
                    if (CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole || CUser.Current.IsGenderSpecRole)
                    {
                        BtnSave.Enabled = true;

                    }
                    else
                    {

                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Edit Non-GBV Detail";
                    }
                }
                else
                {
                    ClearControls();
                    DisableControls(true);
                    // not access
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    lblMessageError.Text = "Parameters are not valid";

                }
            }
        }
        private void LoadEnvironmentGBV()
        {
            try
            {
                GBVDetail detail = SSSNPContext.SSSNPManager.GetGBVDetail(GBVDetailId);
                if (detail == null || detail.GBVDetailID == 0)
                {
                    ClearControls();
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    lblMessageError.Text = "DGV Detail not valid";
                    return;
                }

                
                txtFollowUpAction.Text = detail.FollowUpActionFeedback;
                txtDateOfClosed.Text = (detail.ClosingDateAfterManagementAction == null) ? "" : ((DateTime)detail.ClosingDateAfterManagementAction).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
               
                ddlIsSurvivorSatisfied .SelectedValue = detail.IsSurvivorSatisfied == null ? "0" : (bool)detail.IsSurvivorSatisfied ? "YES" : "NO";
                if (detail.IsSurvivorSatisfied != null &&  (bool)!detail.IsSurvivorSatisfied) { txtSurvivorDetails.Enabled = true; txtSurvivorDetails.Text = detail.SurvivorDetails; }
                else { txtSurvivorDetails.Enabled = false; }

                ddlRepotedTo.SelectedValue = detail.ReportedToManagementHR == null ? "0" : (bool) detail.ReportedToManagementHR ? "YES" : "NO"; 
                if (detail.ReportedToManagementHR !=null && (bool) detail.ReportedToManagementHR ) { txtReportedDetails.Enabled = true; txtReportedDetails.Text = detail.ReportedDetails; }
                else { txtReportedDetails.Enabled = false; }



            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;
            }
        }
        private void PrepareNewEnvironmentGBV()
        {
            try
            {
                ClearControls();
               
                // check if already exists details
                List<GBVDetail> lst = SSSNPContext.SSSNPManager.GetGBVDetails(GBVMainId);
                if (lst == null || lst.Count != 0)
                {
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    lblMessageError.Text = "Already Exists detail for this GBV";
                    return;

                }

                BtnSave.Enabled = true;


            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;

            }
        }
        private GBVDetail MappingDetailProperties()
        {
            GBVDetail result = new GBVDetail();


            result.GBVDetailID = GBVDetailId;
            result.GBVMainID = GBVMainId;

            result.FollowUpActionFeedback = txtFollowUpAction.Text.Trim();

            result.ReportedToManagementHR  = ddlRepotedTo.SelectedValue == "0" ? null : SSSNPHelper.TryParseYesNo(ddlRepotedTo.SelectedValue, true);
            result.ReportedDetails = txtReportedDetails.Text.Trim();

            result.IsSurvivorSatisfied = ddlIsSurvivorSatisfied.SelectedValue == "0" ? null : SSSNPHelper.TryParseYesNo(ddlIsSurvivorSatisfied.SelectedValue, true);
            result.SurvivorDetails = txtSurvivorDetails.Text.Trim();
            result.ClosingDateAfterManagementAction = SSSNPHelper.TryParseDateTime(txtDateOfClosed.Text.Trim(), true);
            result.LastModificationTimestamp = DateTime.Now;
            result.LastModificationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            
            return result;

        }
        private bool MandatoryFieldCheck(GBVDetail detail, out string errorMessage)
        {
            errorMessage = "";
            if (detail == null) { errorMessage = "Detail Entity Null"; return false; }

            if (OperationType == RequestOperationTypePage.NEW)
            {
                if (detail.GBVMainID == 0) { errorMessage = "GBV Main ID Missed"; return false; }

            }
            else if (OperationType == RequestOperationTypePage.NEW)
            {
                if (detail.GBVDetailID == 0) { errorMessage = "GBV Detail ID Missed"; return false; }
            }


            if (string.IsNullOrEmpty(detail.FollowUpActionFeedback)) { errorMessage = "Insert FollowUp Action/Feedback"; return false; }
          
            if (detail.IsSurvivorSatisfied  != null && !(bool)detail.IsSurvivorSatisfied)
            {
                if (string.IsNullOrEmpty(detail.SurvivorDetails )) { errorMessage = "Insert Survivor Details"; return false; }
            }

            errorMessage = "";
            return true;
        }
        private bool CheckDateFormat(GBVDetail detail, out string errorMessage)
        {
            errorMessage = "";

            if (txtDateOfClosed.Text != null && detail.ClosingDateAfterManagementAction !=null )
            {
                if (SSSNPHelper.TryParseDateTime(txtDateOfClosed.Text) == DateTime.MinValue)
                {
                    errorMessage = "Wrong Date Format: Closing date after management action";
                    return false;
                }
            }
            

            errorMessage = "";
            return true;
        }
        private int CreateNewGBVDetail(GBVDetail detail)
        {
            try
            {
                int result = 0;
                result = SSSNPContext.SSSNPManager.CreateGBVDetail(detail);
                           
                if (result != 0)
                {
                   // SSSNPContext.SSSNPManager.UpdateNonGBVStatus(detail.GBVMainID, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                    DivFailure.Visible = false;
                    lblMessageError.Text = "";
                    
                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Create Successfully";

                }

                return result;
            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return -1;
            }
        }
        private int UpdateGBVDetail(GBVDetail detail)
        {
            try
            {

                int result = 0;
                result = SSSNPContext.SSSNPManager.UpdateGBVDetail(detail);

               
                //detail.NonGBVMainID = SSSNPContext.SSSNPManager.GetNonGBVMainIDByDetailID(detail.NonGBVDetailID);

                //if (detail.NonGBVMainID != 0)
                //{
                //    SSSNPContext.SSSNPManager.UpdateNonGBVStatus(detail.NonGBVMainID, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                //    // SSSNPContext.SSSNPManager.ChangeNonGBVStatusByDetails(detail, SSSNPHelper.TryParseInt(Session["uid"].ToString()));
                //}

                if (result != 0)
                {
                    DivFailure.Visible = false;
                    lblMessageError.Text = "";

                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Update Successfully";

                }

                return result;
            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return -1;

            }

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            DivFailure.Visible = false;
            DivSuccess.Visible = false;
            lblMessageError.Text = "";

            string errorMessage = string.Empty;

            GBVDetail detail = MappingDetailProperties();

            if (!CheckDateFormat(detail, out errorMessage))
            {
                DivFailure.Visible = true;
                lblMessageError.Text = errorMessage;
                return;
            }

            if (!MandatoryFieldCheck(detail, out errorMessage))
            {
                DivFailure.Visible = true;
                lblMessageError.Text = errorMessage;
                return;
            }

            if (this.OperationType == RequestOperationTypePage.NEW && detail.GBVMainID != 0)
            {

                int result = CreateNewGBVDetail(detail);

                if (result != 0)

                {
                    Response.Redirect("GBVDetailManagement.aspx?oType=2&id=" + result, false);
                }

            }
            else if (this.OperationType == RequestOperationTypePage.EDIT && detail.GBVDetailID != 0)
            {
                int result = UpdateGBVDetail(detail);
                if (result != 0)
                {
                    LoadEnvironmentGBV();
                }
            }

        }
        protected void BtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);
        }
        protected void ddlRepotedTo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlRepotedTo.SelectedValue=="YES")
            {
                txtReportedDetails.Text = "";
                txtReportedDetails.Enabled = true;
            }
            else
            {
                txtReportedDetails.Text = "";
                txtReportedDetails.Enabled = false;
            }
        }
        protected void ddlIsSurvivorSatisfied_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIsSurvivorSatisfied.SelectedValue == "NO")
            {
                txtSurvivorDetails.Text = "";
                txtSurvivorDetails.Enabled = true;

            }
            else
            {
                txtSurvivorDetails.Text = "";
                txtSurvivorDetails.Enabled = false;
            }
        }
        protected void txtDateOfClosed_CalendarExtender_PreRender(object sender, EventArgs e)
        {
            txtDateOfClosed_CalendarExtender.EndDate = DateTime.Today;

        }

    }
}

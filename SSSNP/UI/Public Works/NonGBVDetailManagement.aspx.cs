using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;
using static SSSNP_SNSOP.UI.Public_Works.GBVManagement;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class NonGBVDetailManagement : System.Web.UI.Page
    {
        General Gen = new General();
        private int maximumSizeUploadFile = 25000000;
        public Guid GetAttachment_ActionResolution
        {
            get
            {
                if (hfAttach_ActionResolution .Value == null) return Guid.Empty;
                if (string.IsNullOrEmpty(hfAttach_ActionResolution.Value)) return Guid.Empty;
                Guid returnValue = Guid.Empty;
                Guid.TryParse(hfAttach_ActionResolution.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public int GetAttachment_ActionResolutionID
        {
            get
            {
                if (hfAttach_ActionResolutionID.Value == null) return 0;
                if (string.IsNullOrEmpty(hfAttach_ActionResolutionID.Value)) return 0;
                int returnValue = 0;
                int.TryParse(hfAttach_ActionResolutionID.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public Guid GetAttachment_Feedback
        {
            get
            {
                if (hfAttach_UploadFeedback .Value == null) return Guid.Empty;
                if (string.IsNullOrEmpty(hfAttach_UploadFeedback.Value)) return Guid.Empty;
                Guid returnValue = Guid.Empty;
                Guid.TryParse(hfAttach_UploadFeedback.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public int GetAttachment_FeedbackID
        {
            get
            {
                if (hfAttach_UploadFeedbackID.Value == null) return 0;
                if (string.IsNullOrEmpty(hfAttach_UploadFeedbackID.Value)) return 0;
                int returnValue = 0;
                int.TryParse(hfAttach_UploadFeedbackID.Value.ToString(), out returnValue);
                return returnValue;

            }
        }


        public Guid GetAttachment_Investigation
        {
            get
            {
                if (hfAttach_UploadInvestigation .Value == null) return Guid.Empty;
                if (string.IsNullOrEmpty(hfAttach_UploadInvestigation.Value)) return Guid.Empty;
                Guid returnValue = Guid.Empty;
                Guid.TryParse(hfAttach_UploadInvestigation.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public int GetAttachment_InvestigationID
        {
            get
            {
                if (hfAttach_UploadInvestigationID.Value == null) return 0;
                if (string.IsNullOrEmpty(hfAttach_UploadInvestigationID.Value)) return 0;
                int returnValue = 0;
                int.TryParse(hfAttach_UploadInvestigationID.Value.ToString(), out returnValue);
                return returnValue;

            }
        }

        private NonGBVMain GetNonGBVMain
        {
            get
            {
                if (this.NonGBVMainId !=0)
                {
                    return SSSNPContext.SSSNPManager.GetNonGBVMain(this.NonGBVMainId);
                }
                else if (this.NonGBVDetailId != 0)
                {
                    NonGBVDetail _detail = SSSNPContext.SSSNPManager.GetNonGBVDetail(this.NonGBVDetailId);
                    if (_detail == null || _detail.NonGBVMainID == 0) return null;
                    return SSSNPContext.SSSNPManager.GetNonGBVMain(_detail.NonGBVMainID);
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
        private int NonGBVMainId
        {
            get
            {
                int result = 0;
                if (Request.QueryString["mainId"] == null) return 0;
                int.TryParse(Request.QueryString["mainId"].ToString(), out result);
                return result;
            }
        }
        private int NonGBVDetailId
        {
            get
            {
                int result = 0;
                if (Request.QueryString["Id"] == null) return 0;
                int.TryParse(Request.QueryString["Id"].ToString(), out result);
                return result;
            }
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
        private void ClearControls()
        {
            ddlCounty.Items.Clear();
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();

            txtActionResolutionDesignation.Text = "";
            txtActionResponseComplaint.Text = "";
            txtAppealDate.Text = "";
            txtClosingDate.Text = "";
            txtDataOfResolution.Text = "";
            //txtDescriptionOfComplaint.Text = "";
            txtFeedbackComplainant.Text = "";
            ddlPendingActionRequered.SelectedValue = "0";
            ddlAppealLodged.SelectedValue = "0";
            ddlAppealOutcomeSatisfactory.SelectedValue = "0";
            txtUnresolvedReccommended.Enabled = false;
            txtUnresolvedReccommended.Text = "";
            ddlEscaletedReferredExternally.SelectedValue = "0";
            txtPendingActionDetail.Text = "";
            txtDetailOfTheAccused.Text = "";
            txtDetailOfTheAccusedOther.Text = "";
            ddlInvestigation.SelectedValue = "0";
            txtNameInvestigation.Text = "";
            txtDesignation.Text = "";
            txtMainFindings.Text = "";
            txtDataInvestigation.Text = "";
            txtInvestigationAction.Text = "";
            txtAccusedName.Text = "";
            ddlSSSNPAccused.SelectedValue = "0";

        }
        private void DisableControls(bool value)
        {
            ddlState.Enabled = !value;
            ddlCounty.Enabled = !value;
            ddlPayam.Enabled = !value;
            ddlBoma.Enabled = !value;
            txtActionResolutionDesignation.Enabled = !value;
            txtActionResponseComplaint.Enabled = !value;
            txtAppealDate.Enabled = !value;
            txtClosingDate.Enabled = !value;
            txtDataOfResolution.Enabled = !value;
            //txtDescriptionOfComplaint.Enabled = !value;
            txtFeedbackComplainant.Enabled = !value;
            //ddlTypeOfComplaint.Enabled = !value;
            ddlPendingActionRequered.Enabled = !value;
            ddlAppealLodged.Enabled = !value;
            ddlAppealOutcomeSatisfactory.Enabled = !value;
            txtUnresolvedReccommended.Enabled = false;
            ddlEscaletedReferredExternally.Enabled = !value;
            upFile.Enabled = !value;
            btnUploadFile.Enabled = !value;
            txtPendingActionDetail.Enabled = !value;
            txtDetailOfTheAccused.Enabled = !value;
            txtDetailOfTheAccusedOther.Enabled = !value;
            ddlStatus.Enabled = !value;
            txtAccusedName.Enabled = !value;
            ddlSSSNPAccused.Enabled = !value;
            txtNameInvestigation.Enabled = !value;
            txtDesignation.Enabled = !value;
            txtDataInvestigation.Enabled = !value;
            txtMainFindings.Enabled = !value;
            ddlInvestigation.Enabled = !value;

        }
        private void PopulateDropDownList()
        {
            if (!Page.IsPostBack)
            {

                Gen.getStates(ddlState);

                List<LK_GBVStatus> lk_status = SSSNPContext.SSSNPManager.GetNonGBVStatus();
                lk_status.Insert(0, new LK_GBVStatus() { StatusName = "--Select--", StatusID = 0 });
                ddlStatus.DataSource = lk_status;
                ddlStatus.DataTextField = "StatusName";
                ddlStatus.DataValueField = "StatusID";
                ddlStatus.DataBind();


                //List<LK_TypeOfComplaint> lk_typeOfComplaint = SSSNPContext.SSSNPManager.GetTypeOfComplaint();
                //lk_typeOfComplaint.Insert(0, new LK_TypeOfComplaint() { Name = "--Select--", TypeOfComplaintID = 0 });
                //ddlTypeOfComplaint.DataSource = lk_typeOfComplaint;
                //ddlTypeOfComplaint.DataTextField = "Name";
                //ddlTypeOfComplaint.DataValueField = "TypeOfComplaintID";
                //ddlTypeOfComplaint.DataBind();

            }
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool actionResolutionValue = false;
            bool feedbackValue = false;
            bool investigationValue = false;

            lblActionResolutionFilename.Text = "";
            lblFeedbackFilename.Text = "";
            lblInvestigationFilename.Text = "";            

            #region Action Resolution
            if (GetAttachment_ActionResolutionID != 0 && GetAttachment_ActionResolution == Guid.Empty)
            {
                // Prendo l attachment abbinato al NonGBV
                actionResolutionValue = true;
                lblActionResolutionFilename.Text = "File Attached";
            }
            else if (GetAttachment_ActionResolution != Guid.Empty)
            {
                actionResolutionValue = true;
                lblActionResolutionFilename.Text = "File Attached";
            }

            btnDownloadActionResolution.Enabled = actionResolutionValue;
            if (GetNonGBVMain !=null && GetNonGBVMain.Status == 5)
            {
                btnRemoveActionResolution.Enabled = false;
            } else { 
                    btnRemoveActionResolution.Enabled = actionResolutionValue; 
            }
            
            BtnUploadActionResolution.Enabled = !actionResolutionValue;
            #endregion

            #region Feedback
            if (GetAttachment_FeedbackID != 0 && GetAttachment_Feedback == Guid.Empty)
            {
                // Prendo l attachment abbinato al NonGBV
                feedbackValue = true;
                lblFeedbackFilename .Text = "File Attached";
            }
            else if (GetAttachment_Feedback != Guid.Empty)
            {
                feedbackValue = true;
                lblFeedbackFilename.Text = "File Attached";
            }

            btnDownloadFeedback .Enabled = feedbackValue;
            if (GetNonGBVMain != null && GetNonGBVMain.Status == 5)
            {
                btnRemoveFeedback.Enabled = false;
            }
            else { btnRemoveFeedback.Enabled = feedbackValue; }
                      
            btnUploadFeedback.Enabled = !feedbackValue;
            #endregion

            #region Investigation
            if (GetAttachment_InvestigationID != 0 && GetAttachment_Investigation == Guid.Empty)
            {
                // Prendo l attachment abbinato al NonGBV
                investigationValue = true;
                lblInvestigationFilename.Text = "File Attached";
            }
            else if (GetAttachment_Investigation != Guid.Empty)
            {
                investigationValue = true;
                lblInvestigationFilename.Text = "File Attached";
            }

            BtnDownloadInvestigation.Enabled = investigationValue;
            if (GetNonGBVMain != null && GetNonGBVMain.Status == 5)
            {
                BtnRemoveInvestigation.Enabled = false;
            }
            else { BtnRemoveInvestigation.Enabled = investigationValue; }

           
            BtnUploadInvestigation .Enabled = !investigationValue;
            #endregion

            if (!BtnSave.Enabled) {
                btnRemoveActionResolution.Enabled = false;
                BtnUploadActionResolution.Enabled = false;

                btnRemoveFeedback.Enabled = false;
                btnUploadFeedback.Enabled = false;

                BtnRemoveInvestigation.Enabled = false;
                BtnUploadInvestigation.Enabled = false;

            }
       

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

            PopulateDropDownList();


            if (!Page.IsPostBack)
            {
                if (OperationType == RequestOperationTypePage.NEW && NonGBVMainId != 0)
                {
                    PrepareNewEnvironmentNonGBV();

                    if (CUser.Current.IsIPRole || CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole || CUser.Current.IsSocDevSpecRole)
                    {
                        BtnSave.Enabled = true;
                    }
                    else
                    {

                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Create Non-GBV Detail";
                    }

                }
                else if (OperationType == RequestOperationTypePage.EDIT && NonGBVDetailId != 0)
                {
                    LoadEnvironmentNonGBV();

                    if (CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole || CUser.Current.IsSocDevSpecRole)
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
        private void LoadEnvironmentNonGBV()
        {
            try
            {
                NonGBVDetail detail = SSSNPContext.SSSNPManager.GetNonGBVDetail(NonGBVDetailId);
                if (detail == null || detail.NonGBVDetailID == 0)
                {
                    ClearControls();
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    lblMessageError.Text = "Non-DGV Detail not valid";
                    return;
                }

                upFile.Enabled = true;
                btnUploadFile.Enabled = true;

                txtActionResolutionDesignation.Text = detail.ActionResolutionDesignation;
                txtActionResponseComplaint.Text = detail.ActionResponseComplaint;

                if (detail.ActionResolutionDesignationID != 0)
                {
                    hfAttach_ActionResolution.Value = Guid.Empty.ToString();
                    hfAttach_ActionResolutionID.Value = detail.ActionResolutionDesignationID.ToString();
                }
                else
                {
                    hfAttach_ActionResolution.Value = Guid.Empty.ToString();
                    hfAttach_ActionResolution.Value = "0";
                }

                txtDetailOfTheAccused.Text = detail.DetailOfTheAccused;
                txtDetailOfTheAccusedOther.Text = detail.DetailOfTheAccusedOther;

                txtAccusedName.Text = detail.NameOfTheAccused;
                Gen.getStates(ddlState);
                if (detail.StateID == 0) { ddlState.SelectedIndex = 0; }
                else { ddlState.SelectedValue = detail.StateID.ToString(); }
                ddlState.Enabled = false;

                GetCounties();
                if (detail.CountyID == 0) { ddlCounty.SelectedIndex = 0; }
                else { ddlCounty.SelectedValue = detail.CountyID.ToString(); }
                ddlCounty.Enabled = false;

                getPayams();
                if (detail.PayamID == 0) { ddlPayam.SelectedIndex = 0; }
                else { ddlPayam.SelectedValue = detail.PayamID.ToString(); }

                getBomas();
                if (detail.BomaID == 0) { ddlBoma.SelectedIndex = 0; }
                else { ddlBoma.SelectedValue = detail.BomaID.ToString(); }
                
                string valueSSSNP = "0";
                if (detail.SSSNPAccused == null || string.IsNullOrEmpty(detail.SSSNPAccused))
                {
                    valueSSSNP ="0";
                } else { valueSSSNP = detail.SSSNPAccused; }

                ddlSSSNPAccused.SelectedValue = valueSSSNP;
                txtPendingActionDetail.Text = detail.PendingActionRequeredNote;
                txtAppealDate.Text = detail.AppealDate != null ? ((DateTime)detail.AppealDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                txtClosingDate.Text = detail.ClosingDate != null ? ((DateTime)detail.ClosingDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                txtDataOfResolution.Text = detail.DataOfResolution.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                //txtDescriptionOfComplaint.Text = detail.DescriptionOfComplaint;
                txtFeedbackComplainant.Text = detail.FeedbackComplainant;

                if (detail.FeedbackComplainantID != 0)
                {
                    hfAttach_UploadFeedback.Value = Guid.Empty.ToString();
                    hfAttach_UploadFeedbackID.Value = detail.FeedbackComplainantID.ToString();
                }
                else
                {
                    hfAttach_UploadFeedback.Value = Guid.Empty.ToString();
                    hfAttach_UploadFeedbackID.Value = "0";
                }

                //ddlTypeOfComplaint.SelectedValue = detail.TypeOfComplaintID != null ? detail.TypeOfComplaintID.ToString() : "0";
                ddlPendingActionRequered.SelectedValue = detail.PendingActionRequered ? "YES" : "NO";
                if (detail.PendingActionRequered) { txtPendingActionDetail.Enabled = true; txtPendingActionDetail.Text = detail.PendingActionRequeredNote; }
                ddlAppealLodged.SelectedValue = detail.AppealLodged ? "YES" : "NO";

                ddlAppealOutcomeSatisfactory.SelectedValue = detail.AppealOutcomeSatisfactory == null ? "0" : ((bool)detail.AppealOutcomeSatisfactory ? "YES" : "NO");
                if (ddlAppealOutcomeSatisfactory.SelectedValue == "YES") { txtUnresolvedReccommended.Enabled = true; txtUnresolvedReccommended.Text = detail.UnresolvedReccommended; } else { txtUnresolvedReccommended.Enabled = false; txtUnresolvedReccommended.Text = ""; }

                ddlEscaletedReferredExternally.SelectedValue = detail.EscaletedReferredExternally ? "YES" : "NO";

                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";

                ddlInvestigation.SelectedValue = detail.HasInvestigation == null ? "0" : ((bool)detail.HasInvestigation ? "YES" : "NO");
                             
                if (detail.HasInvestigation !=null && (bool)detail.HasInvestigation)
                {

                    txtNameInvestigation.Text = detail.NameInvestigation;
                    txtNameInvestigation.Enabled = true;
                    txtDesignation.Text = detail.DesignationInvestigation;
                    txtDesignation.Enabled = true;
                    txtMainFindings.Text = detail.MainFindingsInvestigation;
                    txtMainFindings.Enabled = true;
                    txtDataInvestigation.Text = detail.InvestigationCompletedDate  != null ? ((DateTime)detail.InvestigationCompletedDate).ToString("dd/MM/yyyy", CultureInfo.InvariantCulture) : "";
                    txtDataInvestigation.Enabled = true;
                    CalDateInvestigation.Enabled = true;
                   // txtInvestigationAction.Enabled = false;

                } else if (detail.HasInvestigation != null && !(bool)detail.HasInvestigation)
                {
                   // txtInvestigationAction.Text = detail.InvestigationAction;
                   // txtInvestigationAction.Enabled = true;
                }

                txtInvestigationAction.Text = detail.InvestigationAction;

                if (detail.InvestigationAttachID  != 0)
                {
                    hfAttach_UploadInvestigation .Value = Guid.Empty.ToString();
                    hfAttach_UploadInvestigationID.Value = detail.InvestigationAttachID.ToString();
                }
                else
                {
                    hfAttach_UploadInvestigation.Value = Guid.Empty.ToString();
                    hfAttach_UploadInvestigationID.Value = "0";
                }

                if (GetNonGBVMain != null && GetNonGBVMain.Status.ToString() == "5")
                {
                  
                   
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = false;
                  //  lblMessageError.Text = "Non-DGV Detail not valid";
                    return;


                }

            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;
            }


        }
        private void PrepareNewEnvironmentNonGBV()
        {
            try
            {
                ClearControls();
                upFile.Enabled = false;
                btnUploadFile.Enabled = false;

                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";

                hfAttach_ActionResolution .Value = Guid.Empty.ToString();
                hfAttach_ActionResolutionID.Value = "0";

                hfAttach_UploadFeedback.Value = Guid.Empty.ToString();
                hfAttach_UploadFeedbackID.Value = "0";

                hfAttach_UploadInvestigation.Value = Guid.Empty.ToString();
                hfAttach_UploadInvestigationID.Value = "0";

                // check if already exists details
                List<NonGBVDetail> lst = SSSNPContext.SSSNPManager.GetNonGBVDetails(NonGBVMainId);
                if (lst ==null || lst.Count != 0)
                {
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    lblMessageError.Text = "Already Exists detail for this Non-GBV";
                    return;

                }
                NonGBVMain _main = GetNonGBVMain;
                if (_main != null)
                {
                    
                    ddlState.SelectedValue = _main.StateID.ToString();
                    GetCounties();
                    ddlCounty.SelectedValue = _main .CountyID .ToString ();
                    getPayams();
                    ddlState.Enabled = false;
                    ddlCounty.Enabled = false;

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
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            DivFailure.Visible = false;
            DivSuccess.Visible = false;
            lblMessageError.Text = "";

            string errorMessage = string.Empty;

            NonGBVDetail detail = MappingDetailProperties();

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

            if (this.OperationType == RequestOperationTypePage.NEW && detail.NonGBVMainID != 0)
            {

                int result = CreateNewNonGBVDetail(detail);
               
                if (result != 0)
                {
                    Response.Redirect("NonGBVDetailManagement.aspx?oType=2&id=" + result, false);
                }

            }
            else if (this.OperationType == RequestOperationTypePage.EDIT && detail.NonGBVDetailID != 0)
            {
                int result = UpdateNonGBVDetail(detail);
                if (result != 0)
                {
                    LoadEnvironmentNonGBV();
                }
            }

        }
        private int UpdateNonGBVDetail(NonGBVDetail detail)
        {
            try
            {

                int result = 0;
                result = SSSNPContext.SSSNPManager.UpdateNonGBVDetail(detail);
                detail.NonGBVMainID = SSSNPContext.SSSNPManager.GetNonGBVMainIDByDetailID(detail.NonGBVDetailID);

                if (detail.NonGBVMainID != 0)
                {
                    SSSNPContext.SSSNPManager.UpdateNonGBVStatus(detail.NonGBVMainID, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                   
                    AssociateAttachmentToNonGBV(detail.NonGBVDetailID );
                   

                    // SSSNPContext.SSSNPManager.ChangeNonGBVStatusByDetails(detail, SSSNPHelper.TryParseInt(Session["uid"].ToString()));
                }

                if (result != 0)
                {
                    DivFailure.Visible = false;
                    lblMessageError.Text = "";

                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Updated Successfully";

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
        private int CreateNewNonGBVDetail(NonGBVDetail detail)
        {
            try
            {
                int result = 0;
                result = SSSNPContext.SSSNPManager.CreateNonGBVDetail(detail);
                
                //SSSNPContext.SSSNPManager.ChangeNonGBVStatusByDetails(detail, SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                if (result != 0)
                {
                    SSSNPContext.SSSNPManager.UpdateNonGBVStatus(detail.NonGBVMainID, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));
                                       
                    AssociateAttachmentToNonGBV(result);
                   
                    DivFailure.Visible = false;
                    lblMessageError.Text = "";

                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Created Successfully";

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
        private bool CheckDateFormat(NonGBVDetail detail, out string errorMessage)
        {
            errorMessage = "";
            if (txtDataOfResolution.Text != null && txtDataOfResolution.Text != "")
            {
                if (SSSNPHelper.TryParseDateTime(txtDataOfResolution.Text) == DateTime.MinValue)
                {
                    errorMessage = "Wrong Date Format: Date of Resolution";
                    return false;
                }
            }
            if (txtClosingDate.Text != null && txtClosingDate.Text != "")
            {
                if (SSSNPHelper.TryParseDateTime(txtClosingDate.Text) == DateTime.MinValue)
                {
                    errorMessage = "Wrong Date Format: Closing Date";
                    return false;
                }
            }
            if (txtAppealDate.Text != null && txtAppealDate.Text != "")
            {
                if (SSSNPHelper.TryParseDateTime(txtAppealDate.Text) == DateTime.MinValue)
                {
                    errorMessage = "Wrong Date Format: Appeal Date";
                    return false;
                }
            }

            errorMessage = "";
            return true;
        }
        private bool MandatoryFieldCheck(NonGBVDetail detail, out string errorMessage)
        {
            errorMessage = "";
            if (detail == null) { errorMessage = "Detail Entity Null"; return false; }

            if (OperationType == RequestOperationTypePage.NEW)
            {
                if (detail.NonGBVMainID == 0) { errorMessage = "Non-GBV Main ID Missed"; return false; }

            }
            else if (OperationType == RequestOperationTypePage.NEW)
            {
                if (detail.NonGBVDetailID == 0) { errorMessage = "Non-GBV Detail ID Missed"; return false; }
            }

            // if (string.IsNullOrEmpty(detail.DescriptionOfComplaint)) { errorMessage = "Insert Description of Complaint"; return false; }
            if (string.IsNullOrEmpty(detail.ActionResponseComplaint)) { errorMessage = "Insert Action in response of complaint"; return false; }
            if (string.IsNullOrEmpty(detail.ActionResolutionDesignation)) { errorMessage = "Insert Action resolution designation"; return false; }
            if (string.IsNullOrEmpty(detail.FeedbackComplainant)) { errorMessage = "Insert feedback of complainant"; return false; }
            if (ddlPendingActionRequered.SelectedValue == "0") { errorMessage = "Select Pending Action required"; return false; }
            if (ddlAppealLodged.SelectedValue == "0") { errorMessage = "Select Appeal Lodged"; return false; }
           // if (ddlAppealOutcomeSatisfactory.SelectedValue == "0") { errorMessage = "Select Appeal outcome satisfactory"; return false; }
            if (ddlEscaletedReferredExternally.SelectedValue == "0") { errorMessage = "Select Escaleted/Referred externally"; return false; }
            if (detail.DataOfResolution == DateTime.MinValue) { errorMessage = "Insert a date for resolution"; return false; }

            int dateDifference = CheckDateValid(detail.DataOfResolution);
            if (dateDifference > 0) { errorMessage = @"'Date for Resolution' cannot be before 'Date of Complaint'"; return false; }
           
            if (detail.ClosingDate !=null && detail.ClosingDate != DateTime.MinValue)
            {
                dateDifference = detail.DataOfResolution.Subtract((DateTime)detail.ClosingDate).Days;
                if (dateDifference > 0) { errorMessage = @"'Closing date' cannot be before 'Date of Resolution'"; return false; }

            }

            if (ddlInvestigation.SelectedValue=="YES")
            {
                if (string.IsNullOrEmpty(detail.NameInvestigation)) { errorMessage = "Insert Name Investigation"; return false; }
                if (string.IsNullOrEmpty(detail.DesignationInvestigation )) { errorMessage = "Insert Designed Investigation"; return false; }
                if (string.IsNullOrEmpty(detail.MainFindingsInvestigation)) { errorMessage = "Insert Main Findings Investigation"; return false; }

                if (detail.InvestigationCompletedDate == null) { errorMessage = "Insert Date Investigation Completed"; return false; }
                if (detail.InvestigationCompletedDate == DateTime.MinValue) { errorMessage = "Insert Date Investigation Completed"; return false; }

                // add attachment
                if (GetAttachment_Investigation == Guid.Empty && GetAttachment_InvestigationID == 0)  { errorMessage = "Select Investigation Attachment"; return false;                }
            }

            if (SSSNPHelper.TryParseInt(ddlStatus.SelectedValue) == 0)
            {
                errorMessage = "Please Select Status";
                return false;
            }

            errorMessage = "";
            return true;
        }
        private NonGBVDetail MappingDetailProperties()
        {
            NonGBVDetail result = new NonGBVDetail();

            result.ActionResolutionDesignation = SSSNPHelper.TryParseString(txtActionResolutionDesignation.Text.Trim());
            result.ActionResponseComplaint = SSSNPHelper.TryParseString(txtActionResponseComplaint.Text.Trim());
            result.ActionResolutionDesignationID  = GetAttachment_ActionResolutionID ;
            result.DetailOfTheAccused = SSSNPHelper.TryParseString(txtDetailOfTheAccused.Text.Trim());
            result.DetailOfTheAccusedOther = SSSNPHelper.TryParseString(txtDetailOfTheAccusedOther.Text.Trim());
            result.PendingActionRequeredNote = SSSNPHelper.TryParseString(txtPendingActionDetail.Text.Trim());
            result.AppealDate = SSSNPHelper.TryParseDateTime(txtAppealDate.Text, true);
            result.AppealLodged = SSSNPHelper.TryParseYesNo(ddlAppealLodged.SelectedValue);
            //result.TypeOfComplaintID = SSSNPHelper.TryParseInt(ddlTypeOfComplaint.SelectedValue);

            result.AppealOutcomeSatisfactory = ddlAppealOutcomeSatisfactory.SelectedValue == "0"? null:  SSSNPHelper.TryParseYesNo(ddlAppealOutcomeSatisfactory.SelectedValue, true);
            result.ClosingDate = SSSNPHelper.TryParseDateTime(txtClosingDate.Text.Trim(), true); ;
            result.CreationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            result.DataOfResolution = SSSNPHelper.TryParseDateTime(txtDataOfResolution.Text.Trim());
            //result.DescriptionOfComplaint = SSSNPHelper.TryParseString(txtDescriptionOfComplaint.Text.Trim());
            result.EscaletedReferredExternally = SSSNPHelper.TryParseYesNo(ddlEscaletedReferredExternally.SelectedValue);
            result.FeedbackComplainant = SSSNPHelper.TryParseString(txtFeedbackComplainant.Text);
            result.FeedbackComplainantID  = GetAttachment_FeedbackID ;
            result.LastModificationTimestamp = DateTime.Now;
            result.LastModificationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            result.NonGBVMainID = NonGBVMainId;
            result.PendingActionRequered = SSSNPHelper.TryParseYesNo(ddlPendingActionRequered.SelectedValue);
            result.UnresolvedReccommended = SSSNPHelper.TryParseString(txtUnresolvedReccommended.Text.Trim());
            result.NonGBVDetailID = NonGBVDetailId;

            result.HasInvestigation = null;
            if (ddlInvestigation.SelectedValue == "0") { result.HasInvestigation = null; }
            else if (ddlInvestigation.SelectedValue== "NO") 
            {
                result.HasInvestigation = false;
            //    result.InvestigationAction = SSSNPHelper.TryParseString(txtInvestigationAction.Text.Trim());

            }

            result.InvestigationAction = SSSNPHelper.TryParseString(txtInvestigationAction.Text.Trim());

            result.NameInvestigation = "";
            result.DesignationInvestigation = "";
            result.MainFindingsInvestigation = "";
            result.InvestigationCompletedDate = null;
            result.InvestigationAttachID = 0;

            if (ddlInvestigation.SelectedValue == "YES")
            {
                result.HasInvestigation = true;
                result.NameInvestigation = SSSNPHelper.TryParseString(txtNameInvestigation.Text); 
                result.DesignationInvestigation = SSSNPHelper.TryParseString(txtDesignation .Text); 
                result.MainFindingsInvestigation = SSSNPHelper.TryParseString(txtMainFindings .Text); 
                result.InvestigationCompletedDate = SSSNPHelper.TryParseDateTime(txtDataInvestigation .Text.Trim());
                result.InvestigationAttachID = GetAttachment_InvestigationID;
            }

            result.BomaID = (ddlBoma.SelectedIndex == 0) ? 0 : SSSNPHelper.TryParseInt(ddlBoma.SelectedValue);
            result.StateID = (ddlState.SelectedIndex == 0) ? 0 : SSSNPHelper.TryParseInt(ddlState.SelectedValue);
            result.CountyID = (ddlCounty .SelectedIndex == 0) ? 0 : SSSNPHelper.TryParseInt(ddlCounty.SelectedValue);
            result.PayamID = (ddlPayam .SelectedIndex == 0) ? 0 : SSSNPHelper.TryParseInt(ddlPayam.SelectedValue);
            result.NameOfTheAccused = SSSNPHelper.TryParseString(txtAccusedName.Text.Trim());
            result.SSSNPAccused = SSSNPHelper.TryParseString(ddlSSSNPAccused.SelectedValue );

            return result;

        }
        protected void ddlAppealLodged_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAppealLodged.SelectedValue == "YES")
            {
                txtAppealDate.Text = "";
                txtAppealDate.Enabled = true;

                ddlPendingActionRequered.SelectedValue = "NO";
                ddlPendingActionRequered.Enabled = false;
                ddlEscaletedReferredExternally.SelectedValue = "NO";
                ddlEscaletedReferredExternally.Enabled = false;
                ddlAppealOutcomeSatisfactory.SelectedValue = "0";
                ddlAppealOutcomeSatisfactory.Enabled = false;
                ddlStatus.SelectedValue = "6";

                ddlAppealOutcomeSatisfactory.Enabled = true;
                ddlAppealOutcomeSatisfactory.SelectedValue = "0";
                txtClosingDate.Enabled = true;
                txtClosingDate.Text = "";
            }
            else
            {
                txtAppealDate.Text = "";
                txtAppealDate.Enabled = false;

                ddlPendingActionRequered.Enabled = true;
                ddlEscaletedReferredExternally.Enabled = true;
                ddlAppealOutcomeSatisfactory.Enabled = true;
                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";

                ddlAppealOutcomeSatisfactory.Enabled = false;
                ddlAppealOutcomeSatisfactory.SelectedValue = "0";
                txtClosingDate.Enabled = false;
                txtClosingDate.Text = "";
            }
                    
        }
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            if (upFile.HasFile)
            {
                HttpPostedFile attachment = upFile.PostedFile;

                if (upFile.PostedFile.FileName != "")
                {
                    int intFileLen = attachment.ContentLength;


                    if (intFileLen == 0)
                    {
                        // error lunghezza del file e' 0;
                        return;
                    }

                    if (upFile.PostedFile.ContentLength < maximumSizeUploadFile)
                    {
                        //Find file's extension 
                        string strFileName = Path.GetFileName(upFile.FileName);
                        string strExtension = Path.GetExtension(upFile.FileName).Replace(".", "");


                        if (true)
                        {
                            NonGBVAttachment attachFile = new NonGBVAttachment();


                            byte[] myData = new byte[intFileLen];
                            attachment.InputStream.Read(myData, 0, intFileLen);

                            attachFile.NonGBVDetailID = NonGBVDetailId;
                            attachFile.Filename = upFile.FileName;
                            attachFile.FileAttached = myData;
                            attachFile.ContentType = "application/octet-stream";

                            attachFile.CreationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
                            attachFile.LastModificationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
                            attachFile.LastModificationTimestamp = DateTime.Now;

                            int result = SSSNPContext.SSSNPManager.InsertNonGBVAttachment(attachFile);
                            if (result == 0)
                            {
                                // error
                            }
                            else
                            {
                                RadGridAttachment.Rebind();
                            }


                        }
                        else
                        {
                            // formato non valido
                        }
                    }
                    else
                    {
                        // dimensione del file troppo grande
                    }
                }

            }
        }
        protected void RadGridAttachment_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridAttachments();
        }
        private void PopulateGridAttachments()
        {
            if (this.NonGBVDetailId == 0)
            {
                RadGridAttachment.DataSource = new List<NonGBVAttachment>();
                RadGridAttachment.Visible = false;
            }
            else
            {
                List<NonGBVAttachment> attachmentsList = new List<NonGBVAttachment>();
                attachmentsList = SSSNPContext.SSSNPManager.GetNonGBVAttachments(this.NonGBVDetailId);
                if (attachmentsList.Count > 0)
                {
                    RadGridAttachment.DataSource = attachmentsList;
                    RadGridAttachment.Visible = true;
                }
                else
                {
                    RadGridAttachment.DataSource = new List<NonGBVAttachment>();
                    RadGridAttachment.Visible = false;
                }
            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            LinkButton btnDelete = (LinkButton)sender;
            GridDataItem row = (GridDataItem)btnDelete.NamingContainer;

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("NonGBVAttachmentID").ToString());
            int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());


            if (id != 0)
            {
                int result = SSSNPContext.SSSNPManager.DeleteNonGBVAttachment(id, userID);
                RadGridAttachment.Rebind();
            }
        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            LinkButton btnDownload = (LinkButton)sender;
            GridDataItem row = (GridDataItem)btnDownload.NamingContainer;

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("NonGBVAttachmentID").ToString());
            int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());


            if (id != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=1&AttachmentValue=" + id);
            }
        }
        protected void ddlAppealOutcomeSatisfactory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAppealOutcomeSatisfactory.SelectedValue == "NO")
            {
                txtUnresolvedReccommended.Text = "";
                txtUnresolvedReccommended.Enabled = true;

                ddlPendingActionRequered.SelectedValue = "NO";
                ddlPendingActionRequered.Enabled = false;
                ddlEscaletedReferredExternally.SelectedValue = "NO";
                ddlEscaletedReferredExternally.Enabled = false;
                //ddlAppealLodged.SelectedValue = "NO";
                //ddlAppealLodged.Enabled = true;

                ddlStatus.SelectedValue = "7";

            }
            else
            {
                txtUnresolvedReccommended.Text = "";
                txtUnresolvedReccommended.Enabled = false;
                
                ddlPendingActionRequered.Enabled = true;
                ddlEscaletedReferredExternally.Enabled = true;
                //ddlAppealLodged.Enabled = true;
                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";


            }
        }
        protected void BtnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);
        }
        protected void ddlPendingActionRequered_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPendingActionRequered.SelectedValue == "YES")
            {
                txtPendingActionDetail.Text = "";
                txtPendingActionDetail.Enabled = true;
                ddlEscaletedReferredExternally.SelectedValue = "NO";
                ddlEscaletedReferredExternally.Enabled = false;

                ddlAppealLodged.SelectedValue = "NO";
                ddlAppealLodged.Enabled = false;

                ddlAppealOutcomeSatisfactory.SelectedValue = "0";
                ddlAppealOutcomeSatisfactory.Enabled = false;

                ddlStatus.SelectedValue = "2";
            }
            else 
            {
                txtPendingActionDetail.Text = "";
                txtPendingActionDetail.Enabled = false;

                ddlEscaletedReferredExternally.Enabled = true;
                ddlAppealLodged.Enabled = true;
                ddlAppealOutcomeSatisfactory.Enabled = false;
                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";


            }
        }
        protected void ddlEscaletedReferredExternally_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEscaletedReferredExternally.SelectedValue == "YES")
            {
                ddlPendingActionRequered.SelectedValue = "NO";
                ddlPendingActionRequered.Enabled = false;

                ddlAppealLodged.SelectedValue = "NO";
                ddlAppealLodged.Enabled = false;

                ddlAppealOutcomeSatisfactory.SelectedValue = "0";
                ddlAppealOutcomeSatisfactory.Enabled = false;

                ddlStatus.SelectedValue = "3";

            }
            else
            {

                ddlPendingActionRequered.Enabled = true;
                ddlAppealLodged.Enabled = true;
                ddlAppealOutcomeSatisfactory.Enabled = true;
                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";

            }
        }
        private int CheckDateValid(DateTime destinationDate)
        {
            int result = 0;

            if (this.NonGBVMainId != 0)
            {
                NonGBVMain main = SSSNPContext.SSSNPManager.GetNonGBVMain(this.NonGBVMainId);
                if (main != null && main.NonGBVMainID != 0)
                {
                    return main.DateOfComplaint.Subtract(destinationDate).Days;
                }

            }
            else if (this.NonGBVDetailId != 0)
            {
                NonGBVDetail detail = SSSNPContext.SSSNPManager.GetNonGBVDetail(NonGBVDetailId);
                if (detail != null && detail.NonGBVMainID != 0)
                {
                    NonGBVMain main = SSSNPContext.SSSNPManager.GetNonGBVMain(detail.NonGBVMainID);
                    if (main != null && main.NonGBVMainID != 0)
                    {
                        return main.DateOfComplaint.Subtract(destinationDate).Days;
                    }

                }

            }

            return 0;

        }
        protected void RadGridAttachment_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                LinkButton btDelete = item.FindControl("btnDelete") as LinkButton;
            
                if (CUser.Current.IsIPRole || CUser.Current.IsManagerGRMRole || CUser.Current.IsSocDevSpecRole)
                {
                    if (GetNonGBVMain != null && GetNonGBVMain.Status.ToString() == "5")
                    {
                        btDelete.Visible = false;
                    }
                    else
                    {
                        btDelete.Visible = true;
                    }

                } else { btDelete.Visible = false; }
                

            }
                         
        }

        protected void BtnUploadActionResolution_Click(object sender, EventArgs e)
        {
            try
            {

                AttachmentDraft _contextFile = CreateAttachmentFile(fuActionResolution );
                if (_contextFile == null) return;
                hfAttach_ActionResolution.Value = _contextFile.AttachmentID.ToString();

                if (_contextFile == null || _contextFile.AttachmentID == Guid.Empty) return;

                btnDownloadActionResolution.Enabled = true;
                btnRemoveActionResolution.Enabled = true;
                BtnUploadActionResolution.Enabled = false;
                lblActionResolutionFilename.Text = _contextFile.Filename;


            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;

            }
        }
        private AttachmentDraft CreateAttachmentFile(FileUpload fuControl)
        {
            if (fuControl.HasFile)
            {
                HttpPostedFile attachment = fuControl.PostedFile;

                if (fuControl.PostedFile.FileName != "")
                {
                    int intFileLen = attachment.ContentLength;


                    if (intFileLen == 0) { throw new Exception("File is empty. Please choose a valid file"); }

                    if (fuControl.PostedFile.ContentLength < maximumSizeUploadFile)
                    {
                        //Find file's extension 
                        string strFileName = Path.GetFileName(fuControl.FileName);
                        string strExtension = Path.GetExtension(fuControl.FileName).Replace(".", "");

                        if (true)
                        {

                            byte[] myData = new byte[intFileLen];
                            attachment.InputStream.Read(myData, 0, intFileLen);

                            AttachmentDraft attachFile = new AttachmentDraft();

                            attachFile.Filename = fuControl.FileName;
                            attachFile.FileAttached = myData;
                            attachFile.ContentType = "application/octet-stream";

                            attachFile.CreationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
                            attachFile.LastModificationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
                            attachFile.LastModificationTimestamp = DateTime.Now;

                            Guid returnID = SSSNPContext.SSSNPManager.InsertAttachmentDraft(attachFile);

                            attachFile.AttachmentID = returnID;
                            return attachFile;

                        }
                        else
                        {
                            throw new Exception("File format not accepted");
                        }
                    }
                    else
                    {
                        throw new Exception("File exceeds maximum size allowed");
                    }
                }


            }

            return null;

        }
        protected void btnDownloadActionResolution_Click(object sender, EventArgs e)
        {
            if (GetAttachment_ActionResolutionID != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=5&AttachmentValue=" + GetAttachment_ActionResolutionID);
            }
            else if (GetAttachment_ActionResolution != Guid.Empty)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=6&AttachmentValue=" + GetAttachment_ActionResolution);
            }

        }
        protected void btnRemoveActionResolution_Click(object sender, EventArgs e)
        {
            if (GetAttachment_ActionResolutionID != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                NonGBVAttachmentDedicated attach = SSSNPContext.SSSNPManager.GetNonGBVAttachmentDedicated(GetAttachment_ActionResolutionID);
                if (attach == null || attach.NonGBVAttachmentID == 0) return;

                // Update Non-GBV Detail
                if (attach.NonGBVID != 0)
                {
                    NonGBVDetail _detail = SSSNPContext.SSSNPManager.GetNonGBVDetail(attach.NonGBVID);
                    if (_detail == null || _detail.NonGBVDetailID == 0) return;
                    _detail.ActionResolutionDesignationID = 0;
                    SSSNPContext.SSSNPManager.UpdateNonGBVDetail(_detail);
                }

                // Remove Attachment
                int result = SSSNPContext.SSSNPManager.DeleteNonGBVAttachmentDedicated(GetAttachment_ActionResolutionID, userID);

                if (result > 0)
                {
                    hfAttach_ActionResolution.Value = Guid.Empty.ToString();
                    hfAttach_ActionResolutionID.Value = "0";

                }
            }
            else if (GetAttachment_ActionResolution != Guid.Empty)
            {
                hfAttach_ActionResolution .Value = Guid.Empty.ToString();
                hfAttach_ActionResolutionID.Value = "0";
            }
        }
        private void AssociateAttachmentToNonGBV(int nonGBVID)
        {
            bool updateCommand = false;

            NonGBVDetail  _detail = SSSNPContext.SSSNPManager.GetNonGBVDetail  (nonGBVID);
            if (_detail == null || _detail.NonGBVMainID == 0) return;


            #region Action Resolution
            Guid actionResolutionID = GetAttachment_ActionResolution ;
            if (actionResolutionID != Guid.Empty)
            {
                AttachmentDraft attach = SSSNPContext.SSSNPManager.GetAttachmentDraft(actionResolutionID);
                if (attach != null && attach.AttachmentID != Guid.Empty)
                {
                    NonGBVAttachmentDedicated nonGBVAttach = new NonGBVAttachmentDedicated();

                    nonGBVAttach.Filename = attach.Filename;
                    nonGBVAttach.ContentType = attach.ContentType;
                    nonGBVAttach.FileAttached = attach.FileAttached;
                    nonGBVAttach.NonGBVID = nonGBVID;
                    nonGBVAttach.CreationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;
                    nonGBVAttach.LastModificationTimestamp = DateTime.Now;
                    nonGBVAttach.LastModificationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;

                    int idActionResolution = SSSNPContext.SSSNPManager.InsertNonGBVAttachmentDedicated(nonGBVAttach);
                    if (idActionResolution > 0)
                    {
                        updateCommand = true;
                        _detail.ActionResolutionDesignationID  = idActionResolution;
                    }

                }
            }

            #endregion

            #region Feedback
            Guid feedbackID = GetAttachment_Feedback ;
            if (feedbackID != Guid.Empty)
            {
                AttachmentDraft attach = SSSNPContext.SSSNPManager.GetAttachmentDraft(feedbackID);
                if (attach != null && attach.AttachmentID != Guid.Empty)
                {
                    NonGBVAttachmentDedicated nonGBVAttach = new NonGBVAttachmentDedicated();

                    nonGBVAttach.Filename = attach.Filename;
                    nonGBVAttach.ContentType = attach.ContentType;
                    nonGBVAttach.FileAttached = attach.FileAttached;
                    nonGBVAttach.NonGBVID = nonGBVID;
                    nonGBVAttach.CreationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;
                    nonGBVAttach.LastModificationTimestamp = DateTime.Now;
                    nonGBVAttach.LastModificationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;

                    int idFeedback = SSSNPContext.SSSNPManager.InsertNonGBVAttachmentDedicated(nonGBVAttach);
                    if (idFeedback > 0)
                    {
                        updateCommand = true;
                        _detail.FeedbackComplainantID  = idFeedback;
                    }

                }
            }


            #endregion

            #region Investigation
            Guid investigationID = GetAttachment_Investigation;
            if (investigationID != Guid.Empty)
            {
                AttachmentDraft attach = SSSNPContext.SSSNPManager.GetAttachmentDraft(investigationID);
                if (attach != null && attach.AttachmentID != Guid.Empty)
                {
                    NonGBVAttachmentDedicated nonGBVAttach = new NonGBVAttachmentDedicated();

                    nonGBVAttach.Filename = attach.Filename;
                    nonGBVAttach.ContentType = attach.ContentType;
                    nonGBVAttach.FileAttached = attach.FileAttached;
                    nonGBVAttach.NonGBVID = nonGBVID;
                    nonGBVAttach.CreationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;
                    nonGBVAttach.LastModificationTimestamp = DateTime.Now;
                    nonGBVAttach.LastModificationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;

                    int idInvestigation = SSSNPContext.SSSNPManager.InsertNonGBVAttachmentDedicated(nonGBVAttach);
                    if (idInvestigation > 0)
                    {
                        updateCommand = true;
                        _detail.InvestigationAttachID  = idInvestigation;
                    }

                }
            }


            #endregion

            if (updateCommand)
            {
                int result = SSSNPContext.SSSNPManager.UpdateNonGBVDetail(_detail );
                if (result <= 0)
                {
                    throw new Exception("AssociateAttachmentToNonGBV Update Failed");
                }
            }
        }

        protected void btnDownloadFeedback_Click(object sender, EventArgs e)
        {
            if (GetAttachment_FeedbackID != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=5&AttachmentValue=" + GetAttachment_FeedbackID);
            }
            else if (GetAttachment_Feedback != Guid.Empty)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=6&AttachmentValue=" + GetAttachment_Feedback);
            }
        }
        protected void btnRemoveFeedback_Click(object sender, EventArgs e)
        {
            if (GetAttachment_FeedbackID != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                NonGBVAttachmentDedicated attach = SSSNPContext.SSSNPManager.GetNonGBVAttachmentDedicated(GetAttachment_FeedbackID);
                if (attach == null || attach.NonGBVAttachmentID == 0) return;

                // Update Non-GBV Detail
                if (attach.NonGBVID != 0)
                {
                    NonGBVDetail _detail = SSSNPContext.SSSNPManager.GetNonGBVDetail(attach.NonGBVID);
                    if (_detail == null || _detail.NonGBVDetailID == 0) return;
                    _detail.FeedbackComplainantID  = 0;
                    SSSNPContext.SSSNPManager.UpdateNonGBVDetail(_detail);
                }

                // Remove Attachment
                int result = SSSNPContext.SSSNPManager.DeleteNonGBVAttachmentDedicated(GetAttachment_FeedbackID, userID);

                if (result > 0)
                {
                    hfAttach_UploadFeedback .Value = Guid.Empty.ToString();
                    hfAttach_UploadFeedbackID.Value = "0";

                }
            }
            else if (GetAttachment_Feedback != Guid.Empty)
            {
                hfAttach_UploadFeedback .Value = Guid.Empty.ToString();
                hfAttach_UploadFeedbackID.Value = "0";
            }
        }
        protected void btnUploadFeedback_Click(object sender, EventArgs e)
        {
            try
            {

                AttachmentDraft _contextFile = CreateAttachmentFile(fuFeedback);
                if (_contextFile == null) return;
                hfAttach_UploadFeedback .Value = _contextFile.AttachmentID.ToString();

                if (_contextFile == null || _contextFile.AttachmentID == Guid.Empty) return;

                btnDownloadFeedback .Enabled = true;
                btnRemoveFeedback.Enabled = true;
                btnUploadFeedback.Enabled = false;
                lblFeedbackFilename.Text = _contextFile.Filename;


            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;

            }
        }

        protected void ddlInvestigation_SelectedIndexChanged(object sender, EventArgs e)
        {
           // txtNameInvestigation.Text = "";
            txtDesignation.Text = "";
            txtMainFindings.Text = "";
            txtDataInvestigation.Text = "";

            if (ddlInvestigation.SelectedValue == "YES")
            {
                
                txtNameInvestigation.Enabled = true;
                txtDesignation.Enabled = true;
                txtDataInvestigation.Enabled = true;
                txtMainFindings.Enabled = true;
                CalDateInvestigation.Enabled = true;
               // txtInvestigationAction.Enabled = false;

            }
            else
            {
               // txtInvestigationAction.Enabled = true;
                txtNameInvestigation.Enabled = false;
                txtDesignation.Enabled = false;
                txtDataInvestigation.Enabled = false;
                txtMainFindings.Enabled = false;
                CalDateInvestigation.Enabled = false;
            }
        }
        protected void BtnUploadInvestigation_Click(object sender, EventArgs e)
        {
            try
            {

                AttachmentDraft _contextFile = CreateAttachmentFile(fuInvestigation);
                if (_contextFile == null) return;
                hfAttach_UploadInvestigation.Value = _contextFile.AttachmentID.ToString();

                if (_contextFile == null || _contextFile.AttachmentID == Guid.Empty) return;

                BtnDownloadInvestigation.Enabled = true;
                BtnRemoveInvestigation.Enabled = true;
                BtnUploadInvestigation.Enabled = false;
                lblInvestigationFilename.Text = _contextFile.Filename;


            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;

            }
        }
        protected void BtnDownloadInvestigation_Click(object sender, EventArgs e)
        {
            if (GetAttachment_InvestigationID != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=5&AttachmentValue=" + GetAttachment_InvestigationID);
            }
            else if (GetAttachment_Investigation != Guid.Empty)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=6&AttachmentValue=" + GetAttachment_Investigation);
            }
        }
        protected void BtnRemoveInvestigation_Click(object sender, EventArgs e)
        {
            if (GetAttachment_InvestigationID != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                NonGBVAttachmentDedicated attach = SSSNPContext.SSSNPManager.GetNonGBVAttachmentDedicated(GetAttachment_InvestigationID);
                if (attach == null || attach.NonGBVAttachmentID == 0) return;

                // Update Non-GBV Detail
                if (attach.NonGBVID != 0)
                {
                    NonGBVDetail _detail = SSSNPContext.SSSNPManager.GetNonGBVDetail(attach.NonGBVID);
                    if (_detail == null || _detail.NonGBVDetailID == 0) return;
                    _detail.InvestigationAttachID  = 0;
                    SSSNPContext.SSSNPManager.UpdateNonGBVDetail(_detail);
                }

                // Remove Attachment
                int result = SSSNPContext.SSSNPManager.DeleteNonGBVAttachmentDedicated(GetAttachment_InvestigationID, userID);

                if (result > 0)
                {
                    hfAttach_UploadInvestigation .Value = Guid.Empty.ToString();
                    hfAttach_UploadInvestigationID.Value = "0";

                }
            } else if (GetAttachment_Investigation != Guid.Empty )
                {
                hfAttach_UploadInvestigation.Value = Guid.Empty.ToString();
                hfAttach_UploadInvestigationID.Value = "0";
            }

        }
        private void GetCounties()
        {
            ddlCounty.Items.Clear();
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();
            if (ddlState.SelectedIndex != 0)
            {
                Gen.getCounties(ddlCounty, ddlState.SelectedValue);
                ddlCounty.Enabled = true;
                ddlPayam.Items.Insert(0, " --Select--");
                ddlBoma.Items.Insert(0, " --Select--");
            }
            else
            {
                ddlCounty.Enabled = false;
                ddlCounty.Items.Insert(0, " --Select--");
                ddlPayam.Items.Insert(0, " --Select--");
                ddlPayam.Enabled = false;
                ddlBoma.Items.Insert(0, " --Select--");
                ddlBoma.Enabled = false;

            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCounties();

        }
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPayams();
        }
        private void getPayams()
        {
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();
            if (ddlCounty.SelectedIndex != 0)
            {
                Gen.getPayams(ddlPayam, ddlCounty.SelectedValue);

                ddlPayam.Enabled = true;
                ddlBoma.Items.Insert(0, "--Select--");
                ddlBoma.Enabled = false;
            }
            else
            {
                ddlPayam.Items.Insert(0, "--Select--");
                ddlPayam.Enabled = false;
                ddlBoma.Items.Insert(0, "--Select--");
                ddlBoma.Enabled = false;

            }
        }
        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {
            getBomas();
        }
        void getBomas()
        {
            ddlBoma.Items.Clear();
            if (ddlPayam.SelectedIndex != 0)
            {
                Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
                ddlBoma.Enabled = true;
            }
            else
            {
                ddlBoma.Items.Insert(0, "--Select--");
                ddlBoma.Enabled = false;
            }
        }
        protected void txtDateOfResolution_CalendarExtender_PreRender(object sender, EventArgs e)
        {
            txtDateOfResolution_CalendarExtender.EndDate = DateTime.Today;

        }
        protected void txtAppealDate_CalendarExtender_PreRender(object sender, EventArgs e)
        {
            txtAppealDate_CalendarExtender.EndDate = DateTime.Today;
        }
        protected void CalDateInvestigation_PreRender(object sender, EventArgs e)
        {
            CalDateInvestigation.EndDate = DateTime.Today;
        }
        protected void txtClosingDate_CalendarExtender_PreRender(object sender, EventArgs e)
        {
            txtClosingDate_CalendarExtender.EndDate = DateTime.Today;
        }

        protected void txtDataOfResolution_TextChanged(object sender, EventArgs e)
        {
            if (txtDataOfResolution.Text != null && txtDataOfResolution.Text != "")
            {
                if (SSSNPHelper.TryParseDateTime(txtDataOfResolution.Text) == DateTime.MinValue)
                {
                    //string errorMessage = "Wrong Date Format: Date of Resolution";
                    //return ;
                }
                else
                {
                    // set status as Closed
                    ddlStatus.SelectedValue = "5";
                }
            }
            else
            {
                ddlStatus.SelectedValue = GetNonGBVMain != null ? GetNonGBVMain.Status.ToString() : "0";
            }
        }
    }
}
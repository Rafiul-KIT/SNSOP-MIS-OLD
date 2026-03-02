using SSSNP.Network;
using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class GBVManagement : System.Web.UI.Page
    {
        General Gen = new General();
        private int maximumSizeUploadFile = 25000000;
        private EmailService emailService = new EmailService();

        public Guid GetAttachment_DescriptionIncident
        {
            get
            {
                if (hfAttach_DescriptionIncident.Value == null) return Guid.Empty;
                if (string.IsNullOrEmpty(hfAttach_DescriptionIncident.Value)) return Guid.Empty;
                Guid returnValue = Guid.Empty;
                Guid.TryParse(hfAttach_DescriptionIncident.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public int GetAttachment_DescriptionIncidentID
        {
            get
            {
                if (hfAttach_DescriptionIncidentID.Value == null) return 0;
                if (string.IsNullOrEmpty(hfAttach_DescriptionIncidentID.Value)) return 0;
                int returnValue = 0;
                int.TryParse(hfAttach_DescriptionIncidentID.Value.ToString(), out returnValue);
                return returnValue;

            }
        }

        public Guid GetAttachment_ConsentSigned
        {
            get
            {
                if (hfAttach_ConsentSigned.Value == null) return Guid.Empty;
                if (string.IsNullOrEmpty(hfAttach_ConsentSigned.Value)) return Guid.Empty;
                Guid returnValue = Guid.Empty;
                Guid.TryParse(hfAttach_ConsentSigned.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public int GetAttachment_ConsentSignedID
        {
            get
            {
                if (hfAttach_ConsentSignedID.Value == null) return 0;
                if (string.IsNullOrEmpty(hfAttach_ConsentSignedID.Value)) return 0;
                int returnValue = 0;
                int.TryParse(hfAttach_ConsentSignedID.Value.ToString(), out returnValue);
                return returnValue;

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
            //sb.Append(pge.Page.GetPostBackEventReference(btn));
            sb.Append(";");
            return sb.ToString();
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
        private int GBVId
        {
            get
            {
                int result = 0;
                if (Request.QueryString["id"] == null) return 0;
                int.TryParse(Request.QueryString["id"].ToString(), out result);
                return result;
            }
        }
        private void ClearControls()
        {
            txtSerialNumber.Text = "";
            txtDate.Text = "";
            //  ddlState.Items.Clear();
            ddlCounty.Items.Clear();
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();
            txtDateofIncident.Text = "";
            ddlReportingChannel.SelectedValue = "0";
            txtOtherReportingChannel.Text = "";
            ddlVerbalConsent.SelectedValue = "0";

            txtDescriptionOfIncident.Text = "";
            ddlIncidentReportedBy.SelectedValue = "0";
            txtOtherIncidentReportedBy.Text = "";
            txtContactNumber.Text = "";
            ddlGender.SelectedValue = "0";
            txtAge.Text = "";
            ddlSNSOPBeneficiary.SelectedValue = "1";
            ddlSupportType.SelectedValue = "0";
            ddlSuspect.SelectedValue = "0";
            txtSuspectNotes.Text = "";
            ddlSuspectKnown.SelectedValue = "0";
            txtSuspectDetails.Text = "";
            ddlReferralServiceRecommended.SelectedValue = "0";
            ddlConsentSigned.SelectedValue = "0";
            txtStatusAfterReferral.Text = "";
            txtRecommendedDetails.Text = "";
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool descriptionIncident = false;
            bool consentSigned = false;

            lblDescriptionIncidentFilename.Text = "";
            lblAttachConsentSignedFilename.Text = "";

            GBVMain GBVmain = SSSNPContext.SSSNPManager.GetGBVMain(GBVId);


            #region DescriptionIncident
            if (GetAttachment_DescriptionIncidentID != 0 && GetAttachment_DescriptionIncident == Guid.Empty)
            {
                // I Take the GBV Attachement assosiated
                descriptionIncident = true;
                lblDescriptionIncidentFilename.Text = "File Attached";
            }
            else if (GetAttachment_DescriptionIncident != Guid.Empty)
            {
                descriptionIncident = true;
                lblDescriptionIncidentFilename.Text = "File Attached";
            }

            btnDownloadDescriptionIncident.Enabled = descriptionIncident;
            if (GBVmain != null && GBVmain.Status == 5)
            {
                btnRemoveDescriptionIncident.Enabled = false;
            }
            else { btnRemoveDescriptionIncident.Enabled = descriptionIncident; }
            BtnUploadDescriptionIncident.Enabled = !descriptionIncident;

            #endregion

            #region Consent Signed

            if (ddlConsentSigned.SelectedValue == "YES")
            {
                fuAttachConsentSigned.Enabled = true;
                BtnUploadConsentSigned.Enabled = true;

                if (GetAttachment_ConsentSignedID != 0 && GetAttachment_ConsentSigned == Guid.Empty)
                {
                    // I Take the GBV Attachement assosiated
                    consentSigned = true;
                    lblAttachConsentSignedFilename.Text = "File Attached";
                }
                else if (GetAttachment_ConsentSigned != Guid.Empty)
                {
                    consentSigned = true;
                    lblAttachConsentSignedFilename.Text = "File Attached";
                }

                BtnDownloadAttachConsentSigned.Enabled = consentSigned;
                if (GBVmain != null && GBVmain.Status == 5)
                {
                    BtnRemoveAttachConsentSigned.Enabled = false;
                }
                else { BtnRemoveAttachConsentSigned.Enabled = consentSigned; }

               
                BtnUploadConsentSigned.Enabled = !consentSigned;

            }

            #endregion


            if (OperationType == RequestOperationTypePage.EDIT && GBVId != 0)
            {
                BtnUploadConsentSigned .Enabled = false;
                BtnRemoveAttachConsentSigned .Enabled = false;

                BtnUploadDescriptionIncident.Enabled = false;
                btnRemoveDescriptionIncident.Enabled = false;
            }

            if (CUser.Current.IsIPRole || CUser.Current.IsManagerGRMRole)
            {
                BtnDelete.Enabled = true;

            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count <= 0) { Response.Redirect("../../frmUserLogin.aspx"); }

            //AvoidMultipleSubmit(BtnSave);

            this.BtnSave.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave, ""));

            PopulateDropDownList();
            //if (Session["SSSNP_SaveFlagOK"] != null)
            //{
            //    short valueSuccess = 0;
            //    short.TryParse(Session["SSSNP_SaveFlagOK"].ToString(), out valueSuccess);
            //    if (valueSuccess == 1)
            //    {
            //        DivSuccess.Visible = true;
            //        lblMessageSuccess.Text = "Create Successfully";
            //        Session["SSSNP_SaveFlagOK"] = null;
            //    }

            //}

            if (!Page.IsPostBack)
            {
                if (CUser.Current.IsPcuSpecRole || CUser.Current.IsWBGBVManagerRole)
                {
                    lblSNNote.Text += "<br/>You have only viewing privilege";
                    btnNewDetails.Enabled = false;
                }
                if (OperationType == RequestOperationTypePage.NEW)
                {
                    PrepareNewEnvironmentGBV();

                    if (CUser.Current.IsIPRole || CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole)
                    {
                        BtnSave.Enabled = true;
                        DivFailure.Visible = false;
                    }
                    else
                    {
                        BtnDelete.Enabled = false;
                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Create GBV";
                    }
                }
                else if (OperationType == RequestOperationTypePage.EDIT && GBVId != 0)
                {
                    LoadEnvironmentGBV();
                   // DisableControls(true);
                   // ddlStatus.Enabled = true;
                    if (CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole)
                    {
                        BtnSave.Enabled = true;
                        DivFailure.Visible = false;
                    }
                    else
                    {
                        BtnDelete.Enabled = false;
                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Edit GBV";
                    }

                }
                else
                {
                    // not access
                    ClearControls();
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    BtnDelete.Enabled = false;
                    btnNewDetails.Enabled = false;
                }
            }

        }
        private void DisableControls(bool value)
        {
            //txtSerialNumber.Enabled = false;
            txtSerialNumber.ReadOnly = true;
            //txtDate.Enabled = false;
            txtContfocalPersName.Enabled = !value;
            txtDate.ReadOnly = true;
            ddlState.Enabled = !value;
            ddlCounty.Enabled = !value;
            ddlPayam.Enabled = !value;
            ddlBoma.Enabled = !value;
            txtDateofIncident.Enabled = !value;
            txtDescriptionOfIncident.Enabled = !value;
            ddlReportingChannel.Enabled = !value;
            txtOtherReportingChannel.Enabled = !value;
            ddlVerbalConsent.Enabled = !value;

            ddlIncidentReportedBy.Enabled = !value;
            txtOtherIncidentReportedBy.Enabled = false;
            txtContactNumber.Enabled = !value;
            ddlGender.Enabled = !value;
            txtAge.Enabled = !value;
            ddlSNSOPBeneficiary.Enabled = !value;
            ddlSupportType.Enabled = !value;
            ddlSuspect.Enabled = !value;
            txtSuspectNotes.Enabled = false;
            ddlSuspectKnown.Enabled = !value;
            txtSuspectDetails.Enabled = false;
            ddlReferralServiceRecommended.Enabled = !value;
            ddlConsentSigned.Enabled = !value;
            ddlStatus.Enabled = !value;
            txtStatusAfterReferral.Enabled = !value;
            txtRecommendedDetails.Enabled = false;

        }
        private void PopulateDropDownList()
        {
            if (!Page.IsPostBack)
            {

                Gen.getStates(ddlState);


                List<LK_GBVStatus> lk_status = SSSNPContext.SSSNPManager.GetGBVStatus();
                lk_status.Insert(0, new LK_GBVStatus() { StatusName = "--Select--", StatusID = 0 });
                ddlStatus.DataSource = lk_status;
                ddlStatus.DataTextField = "StatusName";
                ddlStatus.DataValueField = "StatusID";
                ddlStatus.DataBind();
            }
        }
        private void LoadEnvironmentGBV()
        {

            try
            {
                ClearControls();

                GBVMain main = SSSNPContext.SSSNPManager.GetGBVMain(GBVId);
                if (main == null || main.GBVMainID == 0)
                {

                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    btnNewDetails.Visible = false;
                    BtnDelete.Enabled = false;

                    lblMessageError.Text = "DGV Main not valid";
                    return;
                }
               

                // btnNewDetails.Visible = true;
               // BtnDelete.Enabled = true;

                txtSerialNumber.Text = main.SerialNumber;
                txtDate.Text = main.Date.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);

                Gen.getStates(ddlState);
                ddlState.SelectedValue = main.StateID.ToString();

                GetCounties();
                ddlCounty.SelectedValue = main.CountyID.ToString();

                getPayams();
                ddlPayam.SelectedValue = main.PayamID.ToString();
                getBomas();

                ddlBoma.SelectedValue = main.BomaID.ToString();
                ddlStatus.SelectedValue = main.Status.ToString();

                txtDateofIncident.Text = main.DateOfIncident.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                txtContfocalPersName.Text = main.ContactFocalPointPersonName;

                txtDescriptionOfIncident.Text = main.DescriptionOfTheIncident;
                if (main.DescriptionOfTheIncidentID != 0)
                {
                    hfAttach_DescriptionIncident.Value = Guid.Empty.ToString();
                    hfAttach_DescriptionIncidentID.Value = main.DescriptionOfTheIncidentID.ToString();
                }
                else
                {
                    hfAttach_DescriptionIncident.Value = Guid.Empty.ToString();
                    hfAttach_DescriptionIncidentID.Value = main.DescriptionOfTheIncidentID.ToString();
                }

                ddlReportingChannel.SelectedValue = main.ReportingChannel;
                txtOtherReportingChannel.Text = main.ReportingChannelOther;

                ddlIncidentReportedBy.SelectedValue = main.IncidentReportedBy != null ? main.IncidentReportedBy : "0";
                if (ddlIncidentReportedBy.SelectedValue == "Other") txtOtherIncidentReportedBy.Enabled = true;
                txtOtherIncidentReportedBy.Text = main.IncidentReportedByOther;

                txtContactNumber.Text = main.ContactNumber;
                ddlGender.SelectedValue = main.Gender != null ? main.Gender.ToString() : "0";
                txtAge.Text = main.AgeOfSurvivor;
                if (main.SSSNPBeneficiary != null && main.SSSNPBeneficiary != "0")
                {
                    ddlSNSOPBeneficiary.SelectedValue = "1";
                    ddlSNSOPBeneficiary_SelectedIndexChanged(null, null);
                    ddlSupportType.SelectedValue = main.SSSNPBeneficiary;
                }
                else
                {
                    ddlSNSOPBeneficiary.SelectedValue = "2";
                    ddlSNSOPBeneficiary_SelectedIndexChanged(null, null);
                }
                ddlSuspect.SelectedValue = main.SuspectAssociatedWithProject == null ? "0" : (main.SuspectAssociatedWithProject == true ? "YES" : "NO");
                if (main.SuspectAssociatedWithProject != null && (bool)main.SuspectAssociatedWithProject) txtSuspectNotes.Enabled = true;

                txtSuspectNotes.Text = main.SuspectAssosiatedWithProjectNotes;
                txtStatusAfterReferral.Text = main.StatusAfterReferral;

                ddlSuspectKnown.SelectedValue = main.SuspectKnown == null ? "0" : (main.SuspectKnown == true ? "YES" : "NO");
                if (main.SuspectKnown != null && (bool)main.SuspectKnown) txtSuspectDetails.Enabled = true;

                txtSuspectDetails.Text = main.SuspectKnownDetail;
                ddlReferralServiceRecommended.SelectedValue = main.ReferralServicesRecommended != null ? main.ReferralServicesRecommended.ToString() : "0";
                ddlConsentSigned.SelectedValue = main.ConsentSigned == null ? "0" : (main.ConsentSigned == true ? "YES" : "NO");
                ddlVerbalConsent.SelectedValue = main.VerbalConsentGranted == null ? "0" : (main.VerbalConsentGranted == true ? "YES" : "NO");

                txtRecommendedDetails.Text = main.RecommendedDetails;

                if (main.ConsentSignedID != 0)
                {
                    hfAttach_ConsentSigned.Value = Guid.Empty.ToString();
                    hfAttach_ConsentSignedID.Value = main.ConsentSignedID.ToString();
                }
                else
                {
                    hfAttach_ConsentSigned.Value = Guid.Empty.ToString();
                    hfAttach_ConsentSignedID.Value = main.ConsentSignedID.ToString();
                }

                RadGridDetails.Rebind();


                if (main.GBVMainID != 0 && main.Status == 5)
                {
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    btnNewDetails.Visible = false;
                    BtnDelete.Enabled = false;

                    lblMessageError.Text = "Grievance not editable";
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
        private void PrepareNewEnvironmentGBV()
        {
            try
            {
                ClearControls();

                //Generate new Serial Number
                string strNewSerialNumber = "";
                strNewSerialNumber = ""; // Gen.GenerateGRNewSerialNumber("NGBV");
                txtSerialNumber.Text = strNewSerialNumber;
                txtSerialNumber.Enabled = false;

                //Generate and set timestamp
                txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDate.Enabled = false;

                //Set default status "Received"
                ddlStatus.SelectedValue = ddlStatus.Items.FindByText("Unresolved").Value;
                ddlStatus.Enabled = false;

                hfAttach_DescriptionIncident.Value = Guid.Empty.ToString();
                hfAttach_DescriptionIncidentID.Value = "0";

                hfAttach_ConsentSigned.Value = Guid.Empty.ToString();
                hfAttach_ConsentSignedID.Value = "0";

                btnNewDetails.Visible = false;

            }
            catch (Exception ex)
            {
                throw;
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
        void getPayams()
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

        protected void ddlSNSOPBeneficiary_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSNSOPBeneficiary.SelectedValue != null && ddlSNSOPBeneficiary.SelectedValue.ToString().Equals("1"))
            {
                supportTypeLbl.Visible = true;
                supportTypeColonLbl.Visible = true;
                ddlSupportType.Visible = true;
                ddlSupportType.Enabled = true;
            }
            else
            {
                supportTypeLbl.Visible = false;
                supportTypeColonLbl.Visible = false;
                ddlSupportType.Visible = false;
                ddlSupportType.Enabled = false;
                ddlSupportType.SelectedValue = "0";
            }
        }

        protected void ddlReportingChannel_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlReportingChannel.SelectedValue == "Other")
            {
                txtOtherReportingChannel.Text = "";
                txtOtherReportingChannel.Enabled = true;
            }
            else
            {
                txtOtherReportingChannel.Text = "";
                txtOtherReportingChannel.Enabled = false;
            }
        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                DivFailure.Visible = false;
                DivSuccess.Visible = false;
                lblMessageError.Text = "";
                lblMessageSuccess.Text = "";

                string errorMessage = string.Empty;

                GBVMain main = MappingMainProperties();

                if (!MandatoryFieldCheck(main, out errorMessage))
                {
                    DivFailure.Visible = true;
                    lblMessageError.Text = errorMessage;
                    return;
                }

                if (this.OperationType == RequestOperationTypePage.NEW)
                {
                    int result = CreateNewGBV(main);

                    if (result != 0)
                    {
                        Response.Redirect($"GBVManagement.aspx?oType=2&id={result.ToString()}", false);

                    }

                }
                else if (this.OperationType == RequestOperationTypePage.EDIT && GBVId != 0)
                {

                    int result = UpdateNewGBV(main);
                    if (result != 0)
                    {
                        LoadEnvironmentGBV();
                        DivSuccess.Visible = true;

                        lblMessageSuccess.Text = "Updated Successfully";
                    }
                }


            }
            catch (Exception ex)
            {
                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;

            }

        }
        private int UpdateNewGBV(GBVMain main)
        {
            try
            {

                int result = 0;
                result = SSSNPContext.SSSNPManager.UpdateGBVMain(main);

                if (main.GBVMainID != 0)
                {
                    SSSNPContext.SSSNPManager.UpdateGBVStatus(main.GBVMainID, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                }
                //main.NonGBVMainID = SSSNPContext.SSSNPManager.GetNonGBVMainIDByDetailID(detail.NonGBVDetailID);

                //if (detail.NonGBVMainID != 0)
                //{
                //    SSSNPContext.SSSNPManager.UpdateNonGBVStatus(detail.NonGBVMainID, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                //    // SSSNPContext.SSSNPManager.ChangeNonGBVStatusByDetails(detail, SSSNPHelper.TryParseInt(Session["uid"].ToString()));
                //}

                if (result != 0)
                {

                    AssociateAttachmentToGBV(main.GBVMainID);

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
        private int CreateNewGBV(GBVMain main)
        {
            try
            {
                int result = 0;
                result = SSSNPContext.SSSNPManager.CreateGBVMain(main);

                if (result != 0)
                {
                    SSSNPContext.SSSNPManager.UpdateNonGBVStatus(result, SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToString()), SSSNPHelper.TryParseInt(Session["uid"].ToString()));

                    AssociateAttachmentToGBV(result);

                    DivFailure.Visible = false;
                    lblMessageError.Text = "";
                    Session["SSSNP_SaveFlagOK"] = 1;
                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Create Successfully";

                    string subject = "New GBV Grievance in SNSOP MIS";
                    string body = "<H2>Dear GRM Manager (SNSOP MIS),</H2> <br><br>" +
                        "A new Gender-Based Violence Grievance has been registered.<br>" +
                        ConfigurationManager.AppSettings["SSSNPWEB"] +
                        "<br>Please login by clicking the above link<br>Thanks and regards.";

                    emailService.sendEmailToUserType(subject, body, "ManagerGRM");
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
        private bool MandatoryFieldCheck(GBVMain main, out string errorMessage)
        {
            errorMessage = "General Error";

            if (main == null) { errorMessage = "Main Entity Null"; return false; }
            if (main.StateID == 0) { errorMessage = "Please select State"; return false; }
            if (main.CountyID == 0) { errorMessage = "Please select County"; return false; }
            if (main.PayamID == 0) { errorMessage = "Please select Payam"; return false; }
            if (main.BomaID == 0) { errorMessage = "Please select State Boma"; return false; }
            if (main.Status == 0) { errorMessage = "Please Select Status"; return false; }
            if (main.DateOfIncident == null || main.DateOfIncident == DateTime.MinValue) { errorMessage = "Please select Date of Incident"; return false; }
            if (string.IsNullOrEmpty(main.DescriptionOfTheIncident)) { errorMessage = "Please Insert Description of Incident"; return false; }
            if (string.IsNullOrEmpty(main.ReportingChannel) || main.ReportingChannel == "0") { errorMessage = "Please select Reporting Channel"; return false; }


            if (string.IsNullOrEmpty(main.ContactFocalPointPersonName)) { errorMessage = "Please Insert Contact Focal Point Name"; return false; }
            if (string.IsNullOrEmpty(main.Gender) || main.Gender == "0") { errorMessage = "Please select Gender"; return false; }
            if (string.IsNullOrEmpty(main.AgeOfSurvivor)) { errorMessage = "Please Insert Age of Survivor"; return false; }

            //if (string.IsNullOrEmpty(main.SSSNPBeneficiary) || main.SSSNPBeneficiary == "0") { errorMessage = "Please select SSSNP Beneficiary"; return false; }

            if (main.ConsentSigned != null && (bool)main.ConsentSigned)
            {

                if (GetAttachment_ConsentSignedID == 0 && GetAttachment_ConsentSigned == Guid.Empty)
                {
                    errorMessage = "Please select Consent Signed Attachment";
                    return false;
                }

            }

            if (ddlSNSOPBeneficiary.SelectedValue != null && ddlSNSOPBeneficiary.SelectedValue.ToString().Equals("1"))
            {
                if (main.SSSNPBeneficiary == null || main.SSSNPBeneficiary == "0")
                {
                    errorMessage = "Please select support type";
                    return false;
                }
            }

            errorMessage = "";
            return true;
        }
        private void PopulateGridDetails()
        {
            if (this.GBVId == 0)
            {
                RadGridDetails.DataSource = new List<GBVDetail>();
                RadGridDetails.Visible = false;
            }
            else
            {
                List<GBVDetail> list = new List<GBVDetail>();
                list = SSSNPContext.SSSNPManager.GetGBVDetails(this.GBVId);

                RadGridDetails.DataSource = list;

                if (list.Count == 0) { RadGridDetails.Visible = false; btnNewDetails.Visible = true; }
                else
                {
                    RadGridDetails.Visible = true;
                    btnNewDetails.Visible = false;
                }


            }

        }
        protected void btnNewDetails_Click(object sender, EventArgs e)
        {
            RadWindow newWindow = new RadWindow();
            newWindow.NavigateUrl = "GBVDetailManagement.aspx?oType=1&mainId=" + GBVId.ToString();
            newWindow.VisibleOnPageLoad = true;
            newWindow.Width = 1440;
            newWindow.Height = 700;
            newWindow.VisibleStatusbar = false;

            RadWindowManager1.Windows.Add(newWindow);


        }
        protected void RadGridDetails_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
        {
            PopulateGridDetails();
        }
        protected void btnEditDetail_Click(object sender, EventArgs e)
        {
            RadButton btEdit = (RadButton)sender;
            GridDataItem row = (GridDataItem)btEdit.NamingContainer;

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("GBVDetailID").ToString());

            if (id != 0)
            {
                RadWindow newWindow = new RadWindow();
                newWindow.NavigateUrl = "GBVDetailManagement.aspx?oType=2&Id=" + id.ToString();
                newWindow.VisibleOnPageLoad = true;
                newWindow.Width = 1440;
                newWindow.Height = 700;
                newWindow.VisibleStatusbar = false;
                RadWindowManager1.Windows.Add(newWindow);

                RadGridDetails.Rebind();
            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //your code here
            RadGridDetails.Rebind();
            LoadEnvironmentGBV();

            if (CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole)
            {
                BtnSave.Enabled = true;
                DivFailure.Visible = false;
            }
            else
            {
                BtnDelete.Enabled = false;
                BtnSave.Enabled = false;
                DisableControls(true);
                DivFailure.Visible = true;
                lblMessageError.Text = "No Role for Edit GBV";
            }


            RadWindowManager1.Windows.Clear();
            //DivFailure.Visible = false;
            //DivSuccess.Visible = false;

        }
        protected void btnDelateDetail_Click(object sender, EventArgs e)
        {
            RadButton btDelete = (RadButton)sender;
            GridDataItem row = (GridDataItem)btDelete.NamingContainer;

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("GBVDetailID").ToString());
            int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

            if (id != 0)
            {
                SSSNPContext.SSSNPManager.DeleteGBVDetail(id, userID);
                RadGridDetails.Rebind();
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (GBVId != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                int result = SSSNPContext.SSSNPManager.DeleteGBVMain(GBVId, userID);
                if (result != 0)
                {

                    tbMain.Visible = false;
                    DisableControls(true);
                    BtnSave.Visible = false;
                    BtnDelete.Visible = false;
                    btnNewDetails.Visible = false;
                    RadGridDetails.Visible = false;
                    BtnHome.Text = "Search";

                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Remove Successfully";

                }
            }

        }
        protected void BtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("GBVLanding.aspx");
        }
        protected void ddlConsentSigned_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlConsentSigned.SelectedValue == "YES")
            {
                pnlAttachConsentSigned.Enabled = true;
                fuAttachConsentSigned.Enabled = true;

            }
            else
            {
                pnlAttachConsentSigned.Enabled = false;
                fuAttachConsentSigned.Enabled = false;
                hfAttach_ConsentSigned.Value = Guid.Empty.ToString();
                hfAttach_ConsentSignedID.Value = "0";
            }
        }
        protected void ddlIncidentReportedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlIncidentReportedBy.SelectedValue == "Other")
            {
                txtOtherIncidentReportedBy.Enabled = true;
                txtOtherIncidentReportedBy.Text = "";
            }
            else
            {
                txtOtherIncidentReportedBy.Enabled = false;
                txtOtherIncidentReportedBy.Text = "";
            }
        }
        protected void ddlSuspectKnown_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSuspectKnown.SelectedValue == "YES")
            {
                txtSuspectDetails.Enabled = true;
                txtSuspectDetails.Text = "";
            }
            else
            {
                txtSuspectDetails.Enabled = false;
                txtSuspectDetails.Text = "";
            }
        }
        protected void ddlSuspect_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSuspect.SelectedValue == "YES")
            {
                txtSuspectNotes.Text = "";
                txtSuspectNotes.Enabled = true;

            }
            else
            {
                txtSuspectNotes.Text = "";
                txtSuspectNotes.Enabled = false;
            }
        }
        private GBVMain MappingMainProperties()
        {
            GBVMain result = new GBVMain();

            result.Date = SSSNPHelper.TryParseDateTime(txtDate.Text.Trim());


            result.BomaID = SSSNPHelper.TryParseInt(ddlBoma.SelectedValue);
            result.StateID = SSSNPHelper.TryParseInt(ddlState.SelectedValue);
            result.CountyID = SSSNPHelper.TryParseInt(ddlCounty.SelectedValue);
            result.PayamID = SSSNPHelper.TryParseInt(ddlPayam.SelectedValue);

            result.ContactNumber = SSSNPHelper.TryParseString(txtContactNumber.Text.Trim());
            result.DateOfIncident = SSSNPHelper.TryParseDateTime(txtDateofIncident.Text.Trim());
            result.Status = SSSNPHelper.TryParseInt(ddlStatus.SelectedValue);
            result.DescriptionOfTheIncident = SSSNPHelper.TryParseString(txtDescriptionOfIncident.Text);
            result.DescriptionOfTheIncidentID = GetAttachment_DescriptionIncidentID;

            result.ReportingChannel = SSSNPHelper.TryParseString(ddlReportingChannel.SelectedValue);
            result.ReportingChannelOther = SSSNPHelper.TryParseString(txtOtherReportingChannel.Text.Trim());

            result.ContactFocalPointPersonName = SSSNPHelper.TryParseString(txtContfocalPersName.Text.Trim());
            result.IncidentReportedBy = SSSNPHelper.TryParseString(ddlIncidentReportedBy.Text.Trim());
            result.IncidentReportedByOther = SSSNPHelper.TryParseString(txtOtherIncidentReportedBy.Text.Trim());
            result.ContactNumber = SSSNPHelper.TryParseString(txtContactNumber.Text.Trim());
            result.Gender = SSSNPHelper.TryParseString(ddlGender.SelectedValue);
            result.AgeOfSurvivor = SSSNPHelper.TryParseString(txtAge.Text.Trim());
            result.SSSNPBeneficiary = SSSNPHelper.TryParseString(ddlSupportType.SelectedValue);
            result.SuspectAssociatedWithProject = ddlSuspect.SelectedValue == "0" ? null : SSSNPHelper.TryParseYesNo(ddlSuspect.SelectedValue, true);
            result.SuspectAssosiatedWithProjectNotes = SSSNPHelper.TryParseString(txtSuspectNotes.Text.Trim());

            result.VerbalConsentGranted = ddlVerbalConsent .SelectedValue == "0" ? null : SSSNPHelper.TryParseYesNo(ddlVerbalConsent.SelectedValue, true);

            result.SuspectKnown = ddlSuspectKnown.SelectedValue == "0" ? null : SSSNPHelper.TryParseYesNo(ddlSuspectKnown.SelectedValue, true);
            result.SuspectKnownDetail = SSSNPHelper.TryParseString(txtSuspectDetails.Text.Trim());
            result.ReferralServicesRecommended = SSSNPHelper.TryParseString(ddlReferralServiceRecommended.Text.Trim());
            result.ConsentSigned = ddlConsentSigned.SelectedValue == "0" ? null : SSSNPHelper.TryParseYesNo(ddlConsentSigned.SelectedValue, true);
            result.ConsentSignedID = GetAttachment_ConsentSignedID;

            result.CreationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            result.LastModificationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            result.LastModificationTimestamp = DateTime.Now;
            result.StatusAfterReferral = SSSNPHelper.TryParseString(txtStatusAfterReferral.Text.Trim());
            result.RecommendedDetails = SSSNPHelper.TryParseString(txtRecommendedDetails .Text.Trim());

            if (OperationType == RequestOperationTypePage.EDIT && GBVId != 0)
            {
                result.GBVMainID = GBVId;
            }

            return result;
        }
        public enum RequestOperationTypePage
        {
            NONE = 0,
            NEW = 1,
            EDIT = 2
        }
        protected void ddlReferralServiceRecommended_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtRecommendedDetails.Text = "";
            txtRecommendedDetails.Enabled = false;

            if (ddlReferralServiceRecommended.SelectedValue == "NO")
            {
                string idSelected = ddlStatus.Items.FindByText("Referred")?.Value;
                if (idSelected != null) ddlStatus.SelectedValue = idSelected;
                

            }
            else if (ddlReferralServiceRecommended.SelectedValue == "Other")
            {
                txtRecommendedDetails.Text = "";
                txtRecommendedDetails.Enabled = true;
            }
            else
            {
                if (OperationType == RequestOperationTypePage.NEW)
                {
                    string idSelected = ddlStatus.Items.FindByText("Unresolved")?.Value;
                    if (idSelected != null) ddlStatus.SelectedValue = idSelected;
                }
                else { ddlStatus.SelectedIndex = 0; }
            }
        }
        protected void RadGridDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = e.Item as GridDataItem;
                TableCell cell = item["ReportedToManagementHR"];

                if (cell.Text != null && cell.Text.ToLower() == "false")
                {
                    cell.Text = "NO";
                }
                else if (cell.Text != null && cell.Text.ToLower() == "true")
                {
                    cell.Text = "YES";
                }

                TableCell cell2 = item["IsSurvivorSatisfied"];
                if (cell2.Text != null && cell2.Text.ToLower() == "false")
                {
                    cell2.Text = "NO";
                }
                else if (cell2.Text != null && cell2.Text.ToLower() == "true")
                {
                    cell2.Text = "YES";
                }
                                
                RadButton btDelete = item.FindControl("btnDeleteDetail") as RadButton;

                if (CUser.Current.IsIPRole || CUser.Current.IsManagerGRMRole)
                {
                     btDelete.Visible = true; 

                }
                else
                {
                    btDelete.Visible = false;
                }



            }
        }

        protected void btnRemoveAttach_Click(object sender, EventArgs e)
        {
            int userID = 0;
            int.TryParse(Session["uid"].ToString(), out userID);

            int result = SSSNPContext.SSSNPManager.DeleteGBVAttachment(GBVId, userID);

            if (result != 0)
            {
                DivFailure.Visible = false;
                lblMessageError.Text = "";
                DivSuccess.Visible = true;
                lblMessageSuccess.Text = "Attachment Removed";

                LoadEnvironmentGBV();


            }

        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            List<GBVAttachment> lstAttach = SSSNPContext.SSSNPManager.GetGBVAttachments(GBVId);
            if (lstAttach == null || lstAttach.Count == 0) return;

            if (GBVId != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=3&AttachmentValue=" + GBVId);
            }


        }

        protected void txtDateOfIncident_CalendarExtender_PreRender(object sender, EventArgs e)
        {
            txtDateOfIncident_CalendarExtender.EndDate = DateTime.Today;
        }
        private void AssociateAttachmentToGBV(int gbvID)
        {
            bool updateCommand = false;

            GBVMain _main = SSSNPContext.SSSNPManager.GetGBVMain(gbvID);
            if (_main == null || _main.GBVMainID == 0) return;

            #region Description Incident
            Guid descriptionIncidentID = GetAttachment_DescriptionIncident;
            if (descriptionIncidentID != Guid.Empty)
            {
                AttachmentDraft attach = SSSNPContext.SSSNPManager.GetAttachmentDraft(descriptionIncidentID);
                if (attach != null && attach.AttachmentID != Guid.Empty)
                {
                    GBVAttachmentDedicated GBVAttach = new GBVAttachmentDedicated();

                    GBVAttach.Filename = attach.Filename;
                    GBVAttach.ContentType = attach.ContentType;
                    GBVAttach.FileAttached = attach.FileAttached;
                    GBVAttach.GBVID = gbvID;
                    GBVAttach.CreationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;
                    GBVAttach.LastModificationTimestamp = DateTime.Now;
                    GBVAttach.LastModificationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;

                    int idDescriptionIncident = SSSNPContext.SSSNPManager.InsertGBVAttachmentDedicated(GBVAttach);
                    if (idDescriptionIncident > 0)
                    {
                        updateCommand = true;
                        _main.DescriptionOfTheIncidentID = idDescriptionIncident;
                    }

                }
            }
            #endregion

            #region Consent Signed
            Guid consentSignedID = GetAttachment_ConsentSigned;
            if (consentSignedID != Guid.Empty)
            {
                AttachmentDraft attach = SSSNPContext.SSSNPManager.GetAttachmentDraft(consentSignedID);
                if (attach != null && attach.AttachmentID != Guid.Empty)
                {
                    GBVAttachmentDedicated GBVAttach = new GBVAttachmentDedicated();

                    GBVAttach.Filename = attach.Filename;
                    GBVAttach.ContentType = attach.ContentType;
                    GBVAttach.FileAttached = attach.FileAttached;
                    GBVAttach.GBVID = gbvID;
                    GBVAttach.CreationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;
                    GBVAttach.LastModificationTimestamp = DateTime.Now;
                    GBVAttach.LastModificationUserID = attach.CreationUserID == null ? 0 : (int)attach.CreationUserID;

                    int idConsentSigned = SSSNPContext.SSSNPManager.InsertGBVAttachmentDedicated(GBVAttach);
                    if (idConsentSigned > 0)
                    {
                        updateCommand = true;
                        _main.ConsentSignedID = idConsentSigned;
                    }

                }
            }
            #endregion


            if (updateCommand)
            {
                int result = SSSNPContext.SSSNPManager.UpdateGBVMain(_main);
                if (result <= 0)
                {
                    throw new Exception("AssociateAttachmentToGBV Update Failed");
                }
            }
        }
        protected void BtnUploadDescriptionIncident_Click(object sender, EventArgs e)
        {
            try
            {

                AttachmentDraft _contextFile = CreateAttachmentFile(fuDescriptionIncident);
                if (_contextFile == null) return;
                hfAttach_DescriptionIncident.Value = _contextFile.AttachmentID.ToString();

                if (_contextFile == null || _contextFile.AttachmentID == Guid.Empty) return;

                btnDownloadDescriptionIncident.Enabled = true;
                btnRemoveDescriptionIncident.Enabled = true;
                BtnUploadDescriptionIncident.Enabled = false;
                lblDescriptionIncidentFilename.Text = _contextFile.Filename;


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
        protected void btnDownloadDescriptionIncident_Click(object sender, EventArgs e)
        {
            if (GetAttachment_DescriptionIncidentID != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=7&AttachmentValue=" + GetAttachment_DescriptionIncidentID);
            }
            else if (GetAttachment_DescriptionIncident != Guid.Empty)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=6&AttachmentValue=" + GetAttachment_DescriptionIncident);
            }
        }
        protected void btnRemoveDescriptionIncident_Click(object sender, EventArgs e)
        {
            if (GetAttachment_DescriptionIncidentID != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                GBVAttachmentDedicated attach = SSSNPContext.SSSNPManager.GetGBVAttachmentDedicated(GetAttachment_DescriptionIncidentID);
                if (attach == null || attach.GBVAttachmentID == 0) return;

                // Update GBV
                if (attach.GBVID != 0)
                {
                    GBVMain _main = SSSNPContext.SSSNPManager.GetGBVMain(attach.GBVID);
                    if (_main == null || _main.GBVMainID == 0) return;
                    _main.DescriptionOfTheIncidentID = 0;
                    SSSNPContext.SSSNPManager.UpdateGBVMain(_main);
                }

                // Remove Attachment
                int result = SSSNPContext.SSSNPManager.DeleteGBVAttachmentDedicated(GetAttachment_DescriptionIncidentID, userID);

                if (result > 0)
                {
                    hfAttach_DescriptionIncident.Value = Guid.Empty.ToString();
                    hfAttach_DescriptionIncidentID.Value = "0";

                }
            }
            else if (GetAttachment_DescriptionIncident != Guid.Empty)
            {
                hfAttach_DescriptionIncident.Value = Guid.Empty.ToString();
                hfAttach_DescriptionIncidentID.Value = "0";
            }
        }
        protected void BtnUploadConsentSigned_Click(object sender, EventArgs e)
        {
            try
            {

                AttachmentDraft _contextFile = CreateAttachmentFile(fuAttachConsentSigned);
                if (_contextFile == null) return;
                hfAttach_ConsentSigned.Value = _contextFile.AttachmentID.ToString();

                if (_contextFile == null || _contextFile.AttachmentID == Guid.Empty) return;

                BtnDownloadAttachConsentSigned.Enabled = true;
                BtnRemoveAttachConsentSigned.Enabled = true;
                BtnUploadConsentSigned.Enabled = false;
                lblAttachConsentSignedFilename.Text = _contextFile.Filename;

            }
            catch (Exception ex)
            {

                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;
                return;

            }
        }
        protected void BtnDownloadAttachConsentSigned_Click(object sender, EventArgs e)
        {
            if (GetAttachment_ConsentSignedID != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=7&AttachmentValue=" + GetAttachment_ConsentSignedID);
            }
            else if (GetAttachment_ConsentSigned != Guid.Empty)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=6&AttachmentValue=" + GetAttachment_ConsentSigned);
            }
        }
        protected void BtnRemoveAttachConsentSigned_Click(object sender, EventArgs e)
        {
            if (GetAttachment_ConsentSignedID != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                GBVAttachmentDedicated attach = SSSNPContext.SSSNPManager.GetGBVAttachmentDedicated(GetAttachment_ConsentSignedID);
                if (attach == null || attach.GBVAttachmentID == 0) return;

                // Update GBV 
                if (attach.GBVID != 0)
                {
                    GBVMain _main = SSSNPContext.SSSNPManager.GetGBVMain(attach.GBVID);
                    if (_main == null || _main.GBVMainID == 0) return;
                    _main.ConsentSignedID = 0;
                    SSSNPContext.SSSNPManager.UpdateGBVMain(_main);
                }

                // Remove Attachment
                int result = SSSNPContext.SSSNPManager.DeleteGBVAttachmentDedicated(GetAttachment_ConsentSignedID, userID);

                if (result > 0)
                {
                    hfAttach_ConsentSigned.Value = Guid.Empty.ToString();
                    hfAttach_ConsentSignedID.Value = "0";

                }
            }
            else if (GetAttachment_ConsentSigned != Guid.Empty)
            {
                hfAttach_ConsentSigned.Value = Guid.Empty.ToString();
                hfAttach_ConsentSignedID.Value = "0";
            }

        }


    }


}
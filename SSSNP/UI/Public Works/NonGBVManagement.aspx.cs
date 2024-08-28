using SSSNP.Network;
using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class NonGBVManagement : System.Web.UI.Page
    {
        General Gen = new General();
        private int maximumSizeUploadFile = 25000000;
        private EmailService emailService = new EmailService();

        public Guid GetAttachment_DescriptionCompliant
        {
            get
            {
                if (hfAttach_DescriptionCompliant.Value == null) return Guid.Empty;
                if (string.IsNullOrEmpty(hfAttach_DescriptionCompliant.Value)) return Guid.Empty;
                Guid returnValue = Guid.Empty;
                Guid.TryParse(hfAttach_DescriptionCompliant.Value.ToString(), out returnValue);
                return returnValue;

            }
        }
        public int GetAttachment_DescriptionCompliantID
        {
            get
            {
                if (hfAttach_DescriptionCompliantID.Value == null) return 0;
                if (string.IsNullOrEmpty(hfAttach_DescriptionCompliantID.Value)) return 0;
                int returnValue = 0;
                int.TryParse(hfAttach_DescriptionCompliantID.Value.ToString(), out returnValue);
                return returnValue;

            }
        }

        private NonGBVMain GetNonGBVMain
        {
            get
            {
                if (this.NonGBVId != 0)
                {
                    return SSSNPContext.SSSNPManager.GetNonGBVMain(this.NonGBVId);
                }

                return null;
            }
        }

        public void AvoidMultipleSubmit(Button button, string text = "wait...")
        {
            Page.ClientScript.RegisterOnSubmitStatement(GetType(), "ServerForm", "if(this.submitted) return false; this.submitted = true;");
            button.Attributes.Add("onclick", string.Format("if(typeof(Page_ClientValidate)=='function' && !Page_ClientValidate()){{return true;}}this.value='{1}';this.disabled=true;{0}",
                ClientScript.GetPostBackEventReference(button, string.Empty), text));
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

            sb.Append(pge.Page.GetPostBackEventReference(btn, "OK"));
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
        private int NonGBVId
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

            txtDateCompliant.Text = "";
            ddlReportingChannel.SelectedValue = "0";
            txtOtherReportingChannel.Text = "";
            txtComplaiantName.Text = "";
            txtContractNumber.Text = "";
            txtAge.Text = "";
            ddlGender.SelectedValue = "0";
            ddlSSSNPBeneficiary.SelectedValue = "0";
            ddlTypeOfGrievance.SelectedValue = "0";
            ddlTypeOfGrievanceDetail.SelectedValue = "0";
            txtTypeOfGrievanceOther.Text = "";

            txtPhysicalAddress.Text = "";
        }
        protected void Page_PreRender(object sender, EventArgs e)
        {
            bool descriptionComplaint = false;
            lblDescriptionCompliantFilename.Text = "";

            if (GetAttachment_DescriptionCompliantID != 0 && GetAttachment_DescriptionCompliant == Guid.Empty)
            {
                // Prendo l attachment abbinato al NonGBV
                descriptionComplaint = true;
                lblDescriptionCompliantFilename.Text = "File Attached";
            }
            else if (GetAttachment_DescriptionCompliant != Guid.Empty)
            {
                descriptionComplaint = true;
                lblDescriptionCompliantFilename.Text = "File Attached";
            }

            btnDownloadDescriptionComplaint.Enabled = descriptionComplaint;
            if (GetNonGBVMain != null && GetNonGBVMain.Status == 5)
            {
                btnRemoveDescriptionComplaint.Enabled = false;
            }
            else { btnRemoveDescriptionComplaint.Enabled = descriptionComplaint; }


            BtnUploadDescriptionComplaint.Enabled = !descriptionComplaint;

            if (OperationType == RequestOperationTypePage.EDIT && NonGBVId != 0)
            {
                BtnUploadDescriptionComplaint.Enabled = true;
                btnRemoveDescriptionComplaint.Enabled = false;
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

            if (!Page.IsPostBack)
            {
                if (CUser.Current.IsPcuSpecRole || CUser.Current.IsWBGBVManagerRole)
                {
                    lblSNNote.Text += "<br/>You have only viewing privilege";
                    btnNewDetails.Enabled = false;
                }
                if (OperationType == RequestOperationTypePage.NEW)
                {
                    PrepareNewEnvironmentNonGBV();

                    if (CUser.Current.IsIPRole || CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole)
                    {
                        BtnSave.Enabled = true;
                        DivFailure.Visible = false;
                        // BtnDelete.Enabled = true;
                    } else {
                        BtnDelete.Enabled = false;
                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Create Non-GBV";
                    }

                }
                else if (OperationType == RequestOperationTypePage.EDIT && NonGBVId != 0)
                {
                    LoadEnvironmentNonGBV();


                    if (CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole)
                    {
                        BtnSave.Enabled = true;
                       // BtnDelete.Enabled = true;
                        DivFailure.Visible = false;
                    }
                    else
                    {
                        BtnDelete.Enabled = false;
                        BtnSave.Enabled = false;
                        DisableControls(true);
                        DivFailure.Visible = true;
                        lblMessageError.Text = "No Role for Edit Non-GBV";
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
            //txtSerialNumber.Enabled = !value;
            txtSerialNumber.ReadOnly = true;
            //txtDate.Enabled = !value;
            //  ddlTypeOfComplaint.Enabled = !value;
            txtDate.ReadOnly = true;
            ddlState.Enabled = !value;
            ddlCounty.Enabled = !value;
            ddlPayam.Enabled = !value;
            ddlBoma.Enabled = !value;
            txtDateCompliant.Enabled = !value;
            ddlReportingChannel.Enabled = !value;
            txtOtherReportingChannel.Enabled = !value;
            txtComplaiantName.Enabled = !value;
            txtContractNumber.Enabled = !value;
            txtAge.Enabled = !value;
            ddlGender.Enabled = !value;
            ddlSSSNPBeneficiary.Enabled = !value;
            ddlTypeOfGrievance.Enabled = !value;
            ddlTypeOfGrievanceDetail.Enabled = !value;
            txtTypeOfGrievanceOther.Enabled = !value;
            txtDescriptionOfComplaint.Enabled = !value;
            // ddlStatus.Enabled = !value;
            txtPhysicalAddress.Enabled = !value;

        }
        private void PopulateDropDownList()
        {
            if (!Page.IsPostBack)
            {

                Gen.getStates(ddlState);

                List<LK_TypeOfGrievance> lk_TypeGrievance = SSSNPContext.SSSNPManager.GetTypeOfGrievances();
                lk_TypeGrievance.Insert(0, new LK_TypeOfGrievance() { Active = true, Name = "--Select--", TypeOfGrievanceID = 0 });
                ddlTypeOfGrievance.DataSource = lk_TypeGrievance;
                ddlTypeOfGrievance.DataTextField = "Name";
                ddlTypeOfGrievance.DataValueField = "TypeOfGrievanceID";
                ddlTypeOfGrievance.DataBind();

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
        private void LoadEnvironmentNonGBV()
        {

            try
            {
                ClearControls();

                NonGBVMain main = SSSNPContext.SSSNPManager.GetNonGBVMain(NonGBVId);
                if (main == null || main.NonGBVMainID == 0)
                {

                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = true;
                    btnNewDetails.Visible = false;
                    BtnDelete.Enabled = false;

                    lblMessageError.Text = "Non-DGV Main not valid";
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

                txtDateCompliant.Text = main.DateOfComplaint.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture);
                ddlReportingChannel.SelectedValue = main.ReportingChannel;
                txtOtherReportingChannel.Text = main.ReportingChannelOther;
                txtComplaiantName.Text = main.ComplainantName;
                txtContractNumber.Text = main.ContactNumber;
                txtAge.Text = main.Age;
                ddlGender.SelectedValue = main.Gender;
                ddlSSSNPBeneficiary.SelectedValue = main.SSSNPBeneficiary;
                ddlTypeOfGrievance.SelectedValue = main.TypeOfGrievanceID != null ? main.TypeOfGrievanceID.ToString() : "0";

                // ddlTypeOfComplaint.SelectedValue = main.TypeOfComplaintID.ToString();

                GetTypeOfGrievanceDetail();

                ddlTypeOfGrievanceDetail.SelectedValue = main.TypeOfGrievanceDetailID != null ? main.TypeOfGrievanceDetailID.ToString() : "0";
                txtTypeOfGrievanceOther.Text = main.TypeOfGrievanceOther;
                ddlStatus.SelectedValue = main.Status.ToString();
                txtPhysicalAddress.Text = main.PhysicalAddress;

                txtDescriptionOfComplaint.Text = main.DescriptionCompliant;
                if (main.DescriptionCompliantAttachmentID != 0)
                {
                    hfAttach_DescriptionCompliant.Value = Guid.Empty.ToString();
                    hfAttach_DescriptionCompliantID.Value = main.DescriptionCompliantAttachmentID.ToString();
                }
                else
                {
                    hfAttach_DescriptionCompliant.Value = Guid.Empty.ToString();
                    hfAttach_DescriptionCompliantID.Value = main.DescriptionCompliantAttachmentID.ToString();
                }

                RadGridDetails.Rebind();

                if (main.NonGBVMainID != 0 && main.Status == 5)
                {
                    DisableControls(true);
                    BtnSave.Enabled = false;
                    DivFailure.Visible = false;
                    btnNewDetails.Visible = false;
                    BtnDelete.Enabled = false;

                    // lblMessageError.Text = "Non-DGV not editable. Status Closed";
                    // return;
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

                //Generate new Serial Number
                string strNewSerialNumber = "";
                strNewSerialNumber = ""; // Gen.GenerateGRNewSerialNumber("NGBV");
                txtSerialNumber.Text = strNewSerialNumber;
                txtSerialNumber.Enabled = false;

                //Generate and set timestamp
                txtDate.Text = DateTime.Today.ToString("dd/MM/yyyy");
                txtDate.Enabled = false;

                //Set default status "Received"
                ddlStatus.SelectedValue = ddlStatus.Items.FindByText("Received").Value;
                ddlStatus.Enabled = false;

                hfAttach_DescriptionCompliant.Value = Guid.Empty.ToString();
                hfAttach_DescriptionCompliantID.Value = "0";

                btnNewDetails.Visible = false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        protected void ddlTypeOfGrievance_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTypeOfGrievanceDetail();
            if (ddlTypeOfGrievance.SelectedItem.Text == "Other")
            {
                txtTypeOfGrievanceOther.Text = "";
                txtTypeOfGrievanceOther.Enabled = true;
            }
            else
            {
                txtTypeOfGrievanceOther.Text = "";
                txtTypeOfGrievanceOther.Enabled = false;
            }

        }
        private void GetTypeOfGrievanceDetail()
        {
            string valueSelected = ddlTypeOfGrievance.SelectedValue;
            int value = 0;
            int.TryParse(valueSelected, out value);

            List<LK_TypeOfGrievanceDetail> details = SSSNPContext.SSSNPManager.GetTypeOfGrievancesDetails(value);
            if (details == null || details.Count == 0)
            {
                ddlTypeOfGrievanceDetail.Items.Clear();
                details = new List<LK_TypeOfGrievanceDetail>();
                details.Insert(0, new LK_TypeOfGrievanceDetail() { Active = true, Name = "--Select--", TypeOfGrievanceDetailID = 0 });
                ddlTypeOfGrievanceDetail.DataSource = details;
                ddlTypeOfGrievanceDetail.DataValueField = "TypeOfGrievanceDetailID";
                ddlTypeOfGrievanceDetail.DataTextField = "Name";
                ddlTypeOfGrievanceDetail.DataBind();
                ddlTypeOfGrievanceDetail.Enabled = false;
            }
            else
            {
                ddlTypeOfGrievanceDetail.DataSource = details;
                details.Insert(0, new LK_TypeOfGrievanceDetail() { Active = true, Name = "--Select--", TypeOfGrievanceDetailID = 0 });
                ddlTypeOfGrievanceDetail.DataValueField = "TypeOfGrievanceDetailID";
                ddlTypeOfGrievanceDetail.DataTextField = "Name";
                ddlTypeOfGrievanceDetail.Enabled = true;
                ddlTypeOfGrievanceDetail.DataBind();
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

                NonGBVMain main = MappingMainProperties();

                if (!MandatoryFieldCheck(main, out errorMessage))
                {
                    DivFailure.Visible = true;
                    lblMessageError.Text = errorMessage;
                    return;
                }

                if (this.OperationType == RequestOperationTypePage.NEW)
                {
                    int result = CreateNewNonGBV(main);

                    if (result != 0)
                    {
                        Response.Redirect($"NonGBVManagement.aspx?oType=2&id={result.ToString()}", false);

                    }

                }
                else if (this.OperationType == RequestOperationTypePage.EDIT && NonGBVId != 0)
                {

                    int result = UpdateNewNonGBV(main);
                    if (result != 0)
                    {
                        LoadEnvironmentNonGBV();
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
        private int UpdateNewNonGBV(NonGBVMain main)
        {
            try
            {

                int result = 0;
                result = SSSNPContext.SSSNPManager.UpdateNonGBVMain(main);

                if (result != 0)
                {

                    if (GetAttachment_DescriptionCompliant != Guid.Empty)
                    {
                        AssociateAttachmentToNonGBV(main.NonGBVMainID);
                    }


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
        private int CreateNewNonGBV(NonGBVMain main)
        {
            try
            {
                int newID = 0;
                newID = SSSNPContext.SSSNPManager.CreateNonGBVMain(main);
                if (newID == 0) throw new Exception("CreateNonGBVMain failed");

                if (newID != 0)
                {
                    if (GetAttachment_DescriptionCompliant != Guid.Empty)
                    {
                        AssociateAttachmentToNonGBV(newID);
                    }

                    DivFailure.Visible = false;
                    lblMessageError.Text = "";

                    DivSuccess.Visible = true;
                    lblMessageSuccess.Text = "Updated Successfully";

                    string subject = "New Non-GBV Grievance in SNSOP MIS";
                    string body = "<H2>Dear GRM Manager (SNSOP MIS),</H2> <br><br>" +
                        "A new Non-Gender Based Violence Grievance has been registered.<br>" +
                        ConfigurationManager.AppSettings["SSSNPWEB"] +
                        "<br>Please login by clicking the above link<br>Thanks and regards.";

                    emailService.sendEmailToUserType(subject, body, "ManagerGRM");
                }

                return newID;
            }
            catch (Exception ex)
            {
                DivFailure.Visible = true;
                lblMessageError.Text = ex.Message;

                return -1;

            }
        }
        private bool MandatoryFieldCheck(NonGBVMain main, out string errorMessage)
        {
            errorMessage = "General Error";

            if (main == null)
            {
                errorMessage = "Main Entity Null";
                return false;
            }
            if (main.StateID == 0)
            {
                errorMessage = "Please select State";
                return false;
            }
            if (main.CountyID == 0)
            {
                errorMessage = "Please select County";
                return false;
            }
            if (main.PayamID == 0)
            {
                errorMessage = "Please select Payam";
                return false;
            }
            if (main.BomaID == 0)
            {
                errorMessage = "Please select State Boma";
                return false;
            }
            if (main.TypeOfGrievanceID == 0)
            {
                errorMessage = "Please select Type of Grievance";
                return false;
            }
            if (main.DateOfComplaint == null || main.DateOfComplaint == DateTime.MinValue)
            {
                errorMessage = "Please select Date of Complaint";
                return false;
            }
            if (string.IsNullOrEmpty(main.ReportingChannel) || main.ReportingChannel == "0")
            {
                errorMessage = "Please select Reporting Channel";
                return false;
            }
            if (string.IsNullOrEmpty(txtDescriptionOfComplaint.Text))
            {
                errorMessage = "Please insert Description of Complaint";
                return false;
            }
            if (ddlTypeOfGrievance.SelectedItem.Text == "Other" && txtTypeOfGrievanceOther.Text.Trim() == "")
            {
                errorMessage = "Please insert Type of Grievance (Other) description";
                return false;
            }
            if (ddlTypeOfGrievanceDetail.SelectedItem.Text == "Other" && txtTypeOfGrievanceOther.Text.Trim() == "")
            {
                errorMessage = "Please insert Type of Grievance (Other) description";
                return false;
            }
            //if (main.TypeOfComplaintID == 0 )
            //{
            //    errorMessage = "Please select Type of Complaint";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(main.ContactNumber))
            //{
            //    errorMessage = "Please Insert Contact Number";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(main.PhysicalAddress))
            //{
            //    errorMessage = "Please insert Physical address";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(main.Gender) ||main.Gender == "0")
            //{
            //    errorMessage = "Please select Gender";
            //    return false;
            //}
            //if (string.IsNullOrEmpty(main.Age))
            //{
            //    errorMessage = "Please insert Age";
            //    return false;
            //}


            if (main.Status == 0)
            {
                errorMessage = "Please Select Status";
                return false;
            }
            errorMessage = "";
            return true;
        }
        private NonGBVMain MappingMainProperties()
        {
            NonGBVMain result = new NonGBVMain();
            // result.SerialNumber = Gen.GenerateGRNewSerialNumber("NGBV");
            result.Age = txtAge.Text;
            result.BomaID = SSSNPHelper.TryParseInt(ddlBoma.SelectedValue);
            result.StateID = SSSNPHelper.TryParseInt(ddlState.SelectedValue);
            result.CountyID = SSSNPHelper.TryParseInt(ddlCounty.SelectedValue);
            result.PayamID = SSSNPHelper.TryParseInt(ddlPayam.SelectedValue);
            result.TypeOfGrievanceID = SSSNPHelper.TryParseInt(ddlTypeOfGrievance.SelectedValue);
            result.TypeOfGrievanceDetailID = SSSNPHelper.TryParseInt(ddlTypeOfGrievanceDetail.SelectedValue);
            result.ComplainantName = SSSNPHelper.TryParseString(txtComplaiantName.Text.Trim());
            result.ContactNumber = SSSNPHelper.TryParseString(txtContractNumber.Text.Trim());
            result.CreationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            result.Date = SSSNPHelper.TryParseDateTime(txtDate.Text.Trim());
            result.DateOfComplaint = SSSNPHelper.TryParseDateTime(txtDateCompliant.Text.Trim());
            result.TypeOfComplaintID = 0;
            result.Gender = SSSNPHelper.TryParseString(ddlGender.SelectedValue);
            result.LastModificationUserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
            result.LastModificationTimestamp = DateTime.Now;
            result.PhysicalAddress = SSSNPHelper.TryParseString(txtPhysicalAddress.Text.Trim());
            result.ReportingChannel = SSSNPHelper.TryParseString(ddlReportingChannel.SelectedValue);
            result.ReportingChannelOther = SSSNPHelper.TryParseString(txtOtherReportingChannel.Text.Trim());
            result.SSSNPBeneficiary = SSSNPHelper.TryParseString(ddlSSSNPBeneficiary.SelectedValue);
            result.Status = SSSNPHelper.TryParseInt(ddlStatus.SelectedValue);
            // result.StatusLastModificationDate = (DateTime) DateTime.Now ;
            result.TypeOfGrievanceOther = SSSNPHelper.TryParseString(txtTypeOfGrievanceOther.Text.Trim());
            result.DescriptionCompliant = SSSNPHelper.TryParseString(txtDescriptionOfComplaint.Text.Trim());
            result.DescriptionCompliantAttachmentID = GetAttachment_DescriptionCompliantID;

            if (OperationType == RequestOperationTypePage.EDIT && NonGBVId != 0)
            {
                result.NonGBVMainID = NonGBVId;
            }

            return result;
        }
        protected void ddlTypeOfGrievanceDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTypeOfGrievanceDetail.SelectedItem.Text == "Other")
            {
                txtTypeOfGrievanceOther.Text = "";
                txtTypeOfGrievanceOther.Enabled = true;
            }
            else
            {
                txtTypeOfGrievanceOther.Text = "";
                txtTypeOfGrievanceOther.Enabled = false;
            }
        }
        private void PopulateGridDetails()
        {
            if (this.NonGBVId == 0)
            {
                RadGridDetails.DataSource = new List<NonGBVDetail>();
                RadGridDetails.Visible = false;
            }
            else
            {
                List<NonGBVDetail> list = new List<NonGBVDetail>();
                list = SSSNPContext.SSSNPManager.GetNonGBVDetails(this.NonGBVId);

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
            newWindow.NavigateUrl = "NonGBVDetailManagement.aspx?oType=1&mainId=" + NonGBVId.ToString();
            newWindow.VisibleOnPageLoad = true;
            newWindow.Width = 1440;
            newWindow.Height = 900;
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

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("NonGBVDetailID").ToString());

            if (id != 0)
            {
                RadWindow newWindow = new RadWindow();
                newWindow.NavigateUrl = "NonGBVDetailManagement.aspx?oType=2&Id=" + id.ToString();
                newWindow.VisibleOnPageLoad = true;
                newWindow.Width = 1440;
                newWindow.Height = 900;
                newWindow.VisibleStatusbar = false;
                RadWindowManager1.Windows.Add(newWindow);

                RadGridDetails.Rebind();
            }
        }
        protected void RadAjaxManager1_AjaxRequest(object sender, AjaxRequestEventArgs e)
        {
            //your code here
            RadGridDetails.Rebind();
            LoadEnvironmentNonGBV();

            if (OperationType == RequestOperationTypePage.EDIT && NonGBVId != 0)
            {
                if (CUser.Current.IsEntryGRMRole || CUser.Current.IsManagerGRMRole)
                {
                    BtnSave.Enabled = true;
                    // BtnDelete.Enabled = true;
                    DivFailure.Visible = false;
                }
                else
                {
                    BtnDelete.Enabled = false;
                    BtnSave.Enabled = false;
                    DisableControls(true);
                    DivFailure.Visible = true;
                    lblMessageError.Text = "No Role for Edit Non-GBV";
                }

            }

            RadWindowManager1.Windows.Clear();
            //DivFailure.Visible = false;
            //DivSuccess.Visible = false;


        }
        protected void btnDelateDetail_Click(object sender, EventArgs e)
        {
            RadButton btDelete = (RadButton)sender;
            GridDataItem row = (GridDataItem)btDelete.NamingContainer;

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("NonGBVDetailID").ToString());
            int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

            if (id != 0)
            {
                SSSNPContext.SSSNPManager.DeleteNonGBVDetail(id, userID);
                RadGridDetails.Rebind();
            }
        }
        protected void BtnDelete_Click(object sender, EventArgs e)
        {
            if (NonGBVId != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                int result = SSSNPContext.SSSNPManager.DeleteNonGBVMain(NonGBVId, userID);
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
                    lblMessageSuccess.Text = "Removed Successfully";

                }
            }
        }
        protected void BtnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect("NonGBVLanding.aspx");
        }

        protected void valCustom_ServerValidate(object source, ServerValidateEventArgs args)
        {
            args.IsValid = false;
        }
        public enum RequestOperationTypePage
        {
            NONE = 0,
            NEW = 1,
            EDIT = 2
        }

        protected void RadGridDetails_ItemDataBound(object sender, GridItemEventArgs e)
        {
            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadButton btDelete = item.FindControl("btnDeleteDetail") as RadButton;

                if (CUser.Current.IsIPRole || CUser.Current.IsManagerGRMRole)
                {
                    if (item.GetDataKeyValue("Status").ToString() == "5")
                    {
                        btDelete.Visible = false;
                    } else { btDelete.Visible = true; }
                
                }else
                {
                    btDelete.Visible = false;
                }

            }
        }
        protected void txtDateCompliant_CalendarExtender_PreRender(object sender, EventArgs e)
        {
            txtDateCompliant_CalendarExtender.EndDate = DateTime.Today;
        }

        private bool IsExeHeaderFile(HttpPostedFile fuAttach)
        {
            try
            {
                var twoBytes = new byte[2];
                if (fuAttach == null) throw new ArgumentNullException ("fuAttach null");

                fuAttach.InputStream.Read(twoBytes, 0, 2);

                string twoBytesHeader = Encoding.UTF8.GetString(twoBytes);
                fuAttach.InputStream.Position = 0;

                if (twoBytesHeader == "MZ" || twoBytesHeader == "ZM") return true;

                return false;

            }
            catch (Exception)
            {

                throw;
            }
                        
        }

        protected void BtnUploadDescriptionComplaint_Click(object sender, EventArgs e)
        {
            try
            {
                //string mimeType = MimeMapping.GetMimeMapping(fuDescriptionComplaint.FileName);

                bool result = IsExeHeaderFile(fuDescriptionComplaint.PostedFile);

                //MemoryStream memoryStream = new MemoryStream();

                //fuDescriptionComplaint.FileContent.CopyTo(memoryStream);

                //memoryStream.Read(twoBytes, 0, 2);


                //if (Encoding.UTF8.GetString(twoBytes) == "MZ")
                //{
                //    //
                //    var ext = true;
                //}


                AttachmentDraft _contextFile = CreateAttachmentFile(fuDescriptionComplaint);
                if (_contextFile == null) return;
                hfAttach_DescriptionCompliant.Value = _contextFile.AttachmentID.ToString();

                if (_contextFile == null || _contextFile.AttachmentID == Guid.Empty) return;

                btnDownloadDescriptionComplaint.Enabled = true;
                btnRemoveDescriptionComplaint.Enabled = true;
                BtnUploadDescriptionComplaint.Enabled = false;
                lblDescriptionCompliantFilename.Text = _contextFile.Filename;


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
        private void AssociateAttachmentToNonGBV(int nonGBVID)
        {
            bool updateCommand = false;

            NonGBVMain _main = SSSNPContext.SSSNPManager.GetNonGBVMain(nonGBVID);
            if (_main == null || _main.NonGBVMainID == 0) return;

            Guid descriptionComplaintID = GetAttachment_DescriptionCompliant;
            if (descriptionComplaintID != Guid.Empty)
            {
                AttachmentDraft attach = SSSNPContext.SSSNPManager.GetAttachmentDraft(descriptionComplaintID);
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

                    int idDescriptionComplaint = SSSNPContext.SSSNPManager.InsertNonGBVAttachmentDedicated(nonGBVAttach);
                    if (idDescriptionComplaint > 0)
                    {
                        updateCommand = true;
                        _main.DescriptionCompliantAttachmentID = idDescriptionComplaint;
                    }

                }
            }





            if (updateCommand)
            {
                int result = SSSNPContext.SSSNPManager.UpdateNonGBVMain(_main);
                if (result <= 0)
                {
                    throw new Exception("AssociateAttachmentToNonGBV Update Failed");
                }
            }
        }
        protected void btnDownloadDescriptionComplaint_Click(object sender, EventArgs e)
        {
            if (GetAttachment_DescriptionCompliantID != 0)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=5&AttachmentValue=" + GetAttachment_DescriptionCompliantID);
            }
            else if (GetAttachment_DescriptionCompliant != Guid.Empty)
            {
                Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=6&AttachmentValue=" + GetAttachment_DescriptionCompliant);
            }

        }
        protected void btnRemoveDescriptionComplaint_Click(object sender, EventArgs e)
        {
            if (GetAttachment_DescriptionCompliantID != 0)
            {
                int userID = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                NonGBVAttachmentDedicated attach = SSSNPContext.SSSNPManager.GetNonGBVAttachmentDedicated(GetAttachment_DescriptionCompliantID);
                if (attach == null || attach.NonGBVAttachmentID == 0) return;

                // Update Non-GBV
                if (attach.NonGBVID != 0)
                {
                    NonGBVMain _main = SSSNPContext.SSSNPManager.GetNonGBVMain(attach.NonGBVID);
                    if (_main == null || _main.NonGBVMainID == 0) return;
                    _main.DescriptionCompliantAttachmentID = 0;
                    SSSNPContext.SSSNPManager.UpdateNonGBVMain(_main);
                }

                // Remove Attachment
                int result = SSSNPContext.SSSNPManager.DeleteNonGBVAttachmentDedicated(GetAttachment_DescriptionCompliantID, userID);

                if (result > 0)
                {
                    hfAttach_DescriptionCompliant.Value = Guid.Empty.ToString();
                    hfAttach_DescriptionCompliantID.Value = "0";

                }
            }
            else if (GetAttachment_DescriptionCompliant != Guid.Empty)
            {
                hfAttach_DescriptionCompliant.Value = Guid.Empty.ToString();
                hfAttach_DescriptionCompliantID.Value = "0";
            }
        }
    }

}
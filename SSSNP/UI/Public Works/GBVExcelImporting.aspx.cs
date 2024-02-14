using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Core.OleDbMapping;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class GBVExcelImporting : System.Web.UI.Page
    {
        General Gen = new General();
        private void NoAccessPage(bool noAccess)
        {

            ddlGBVType.Enabled = !noAccess;

            BtnAnalyze.Enabled = false;
            btnImport.Enabled = false;

            
        }
        private long maximumSizeUploadFile = 25000000;
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
        private void AddErrorMessage(string message)
        {
            DivFailure.Visible = true;
            lblMessageError.Text = message;

            var functionScript = @"window.setTimeout(function () {
                var objControl = document.getElementById('DivFailure');
                    
                if (objControl != null) {
                    objControl.style.display = 'none';
                }
            }, 5000); ";

            ScriptManager.RegisterStartupScript(this, GetType(), "functionScriptRun", functionScript    , true);
        }
        private void AddSuccessMessage(string message, bool setTimeout)
        {
            DivSuccess.Visible = true;
            lblMessageSuccess.Text = message;

            if (setTimeout)
            {
                var functionScript = @"window.setTimeout(function () {
                var objControl = document.getElementById('DivSuccess');
                    
                if (objControl != null) {
                    objControl.style.display = 'none';
                }
                 }, 5000); ";

                ScriptManager.RegisterStartupScript(this, GetType(), "functionScriptRun", functionScript, true);
            }
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count <= 0) { Response.Redirect("../../frmUserLogin.aspx"); }
            this.BtnAnalyze.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnAnalyze, "group"));
            this.btnImport.Attributes.Add("onclick", DisableTheButton(this.Page, this.btnImport, "group"));

            if (CUser.Current.IsIPRole || CUser.Current.IsManagerGRMRole || CUser.Current.IsEntryGRMRole)
            {
                NoAccessPage(false);
            }
            else
            {
                NoAccessPage(true);
            }

        }
        protected void btnUploadFile_Click(object sender, EventArgs e)
        {
            try
            {
                RadGridLanding.DataSource = null;
                RadGridLanding.Rebind();

                if (!upFile.HasFile)
                {
                    AddErrorMessage("Please, select a file to upload");
                    return;
                }

                GBVImportFile iFile;
                bool uploadResult = UploadFile(out iFile);

                if (uploadResult && iFile != null)
                {

                    int uploadID = SSSNPContext.SSSNPManager.CreateGBVImportedFile(iFile);

                    if (uploadID != 0)
                    {
                        hdnUploadedFile.Value = uploadID.ToString ();
                        lblFilenameUploaded.Text = iFile.SourceFilename.ToLower();
                        AddSuccessMessage("File uploaded Successfully. You can proceed to Analyze file", false);
                        BtnAnalyze.Enabled = true;

                    }
                    else
                    {
                        hdnUploadedFile.Value = "0";
                        AddErrorMessage("Imported in MIS Failed.");
                        BtnAnalyze.Enabled = false;

                    }
                    
                    
                }

            }
            catch (Exception ex)
            {

                AddErrorMessage("Upload file Failed - " + ex.Message);
                return;
            }
        }
        public bool UploadFile(out GBVImportFile iFile)
        {
            iFile = null;
            bool result = false;
            DivSuccess.Visible = false;
            DivFailure.Visible = false;
            lblMessageError.Text = "";
            lblMessageSuccess.Text = "";


            string serverPath = Server.MapPath("~\\GBVExcelImportingTemp");
            if ((upFile.PostedFile != null) && (upFile.PostedFile.ContentLength > 0) && !Gen.IsExecutable(upFile.PostedFile))
            {
                string sFileName = System.IO.Path.GetFileName(upFile.PostedFile.FileName);
                try
                {
                    string[] fileType = upFile.PostedFile.FileName.Split('.');
                    int i = fileType.Length;
                    if (fileType[i - 1].ToUpper().Trim() == "XLSX")
                    {
                        string extension = fileType[i - 1];
                        string newFilename = "";
                        newFilename = $"{SSSNPHelper.TryParseInt(Session["uid"].ToString())}_{System.DateTime.Now.ToString("ddMMyyyyhhmmss")}.{extension}";

                        string fullPath = Path.Combine(serverPath, newFilename);
                        if (File.Exists(fullPath)) { File.Delete(fullPath); }

                        byte[] _fileUpload = new byte[upFile.PostedFile.ContentLength];
                        upFile.PostedFile.InputStream.Read(_fileUpload, 0, upFile.PostedFile.ContentLength);

                        upFile.PostedFile.SaveAs(fullPath);

                        iFile = new GBVImportFile();
                        iFile.CreationTimestamp = DateTime.Now;
                        iFile.SourceFilename = sFileName;
                        iFile.DestinationFilename = newFilename;
                        iFile.UploadedFile = _fileUpload;
                        iFile.UserID = SSSNPHelper.TryParseInt(Session["uid"].ToString());
                        iFile.GBVType = ddlGBVType.SelectedValue;


                        if (SSSNPContext.SSSNPManager.CheckGBVImportedFile(iFile) !=0)
                        {
                            AddErrorMessage("This file has already been imported. Please choose another file");
                            iFile = null;
                            return false;
                        }

                        return true;

                    }
                    else
                    {
                        AddErrorMessage("Wrong file format. Please choose a XLSX");
                        iFile = null;
                        return false;

                    }
                }
                catch (Exception ex) //in case of an error
                {
                    throw;
                    // DeleteFile(sFileDir + sFileName);
                }
            }

            return result;

        }
        protected void BtnAnalyze_Click(object sender, EventArgs e)
        {
            Session["_MainImport"] = null;
            Session["_DetailImport"] = null;
            DivSuccess.Visible = false;
            DivFailure.Visible = false;
            lblMessageError.Text = "";
            lblMessageSuccess.Text = "";


            int importedId = SSSNPHelper.TryParseInt(hdnUploadedFile.Value .ToString ());
            if (importedId == 0)
            {
                AddErrorMessage("No imported file");
                return;
            }

            try
            {
                DivSuccess.Visible = false;

                GBVImportFile iFile = SSSNPContext.SSSNPManager.GetGBVImportedFile(importedId);
                if (iFile == null || iFile.ImportedID == 0) throw new NullReferenceException("GBVImportFile null");

                // Analyze the file imported
                GBVGenerate g = new GBVGenerate(iFile, Server.MapPath("../../GBVExcelImportingTemp"));

                List<ErrorMessage> errorList = new List<ErrorMessage>();

                bool validateResult = g.Validate(out errorList);

                if (!validateResult) 
                {
                    RadGridLanding.DataSource = errorList;
                    RadGridLanding.DataBind();

                    AddErrorMessage("Validation Errors");
                    btnImport.Enabled = false;
                    return;
                }
                else
                {
                    Session["_MainImport"] = null;
                    Session["_DetailImport"] = null;

                    if (iFile.GBVType == "NON-GBV")
                    {
                        Session["_MainImport"] = g.NonGBVMainListMapped;
                        Session["_DetailImport"] = g.NonGBVDetailListMapped;

                        btnImport.Enabled = true;
                        AddSuccessMessage("Validation Successfull", true);
                        RadGridLanding.DataSource = g.NonGBVMainListMapped;
                        RadGridLanding.DataBind();
                    } else
                    {
                        Session["_MainImport"] = g.GBVMainListMapped;
                        Session["_DetailImport"] = g.GBVDetailListMapped;

                        btnImport.Enabled = true;
                        AddSuccessMessage("Validation Successfull", true);
                        RadGridLanding.DataSource = g.GBVMainListMapped;
                        RadGridLanding.DataBind();
                    }
                   
                }
                
            }
            catch (Exception ex)
            {

                AddErrorMessage(ex.Message);

            }
            
        }
        protected void btnImport_Click(object sender, EventArgs e)
        {
            if (ddlGBVType.SelectedValue == "NON-GBV")
            {
                ImportedNonGBV();

            } else if (ddlGBVType.SelectedValue == "GBV")
            {
                ImportedGBV();
            }
            
        }
        protected void ddlGBVType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlGBVType.SelectedValue == "0")
            {
                btnUploadFile.Enabled = false;
                upFile.Enabled = false;
                btnImport.Enabled = false;
                BtnAnalyze.Enabled = false;
                lblFilenameUploaded.Text = "";
                RadGridLanding.Visible = false;
                DivSuccess.Visible = false;
                lnkDownloadTemplateFile.Text = "No Template File selected";
                lnkDownloadTemplateFile.Enabled = false;

            } else
            {
                if (ddlGBVType.SelectedValue == "GBV")
                {
                    lnkDownloadTemplateFile.Text = "GBV Template File selected";
                    lnkDownloadTemplateFile.NavigateUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=4";
                } else
                {
                    lnkDownloadTemplateFile.Text = "Non-GBV Template File selected";
                    lnkDownloadTemplateFile.NavigateUrl = "~/ViewAttachmentHandler.aspx?AttachmentType=2";
                }
                lnkDownloadTemplateFile.Enabled = true;
                btnUploadFile.Enabled = true;
                upFile.Enabled = true;
                DivSuccess.Visible = false;
            }

        }
        protected void RadGridLanding_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {

        }
        private void ImportedNonGBV()
        {
            if (Session["_MainImport"] != null && Session["_DetailImport"] != null)
            {
                List<NonGBVMain> mainList = Session["_MainImport"] as List<NonGBVMain>;
                List<NonGBVDetail> detailList = Session["_DetailImport"] as List<NonGBVDetail>;


                if (mainList != null && mainList.Count != 0 && detailList != null && detailList.Count != 0)
                {
                    // Imported Excel 
                    try
                    {
                        int userId = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                        CreateNonGBVImported cRecord = new CreateNonGBVImported(mainList, detailList, userId);
                        int result = cRecord.CreateNonGBV();

                        if (result !=0)
                        {

                            Session["_MainImport"] = null;
                            Session["_DetailImport"] = null;
                         
                            int importedId = SSSNPHelper.TryParseInt(hdnUploadedFile.Value.ToString());

                            GBVImportFile iFile = SSSNPContext.SSSNPManager.GetGBVImportedFile(importedId);
                            if (iFile != null || iFile.ImportedID != 0) 
                            {
                                SSSNPContext.SSSNPManager.UpdateStatusGBVImportedFile(iFile.ImportedID);
                            }
                            
                            AddSuccessMessage("Non-GBV Imported Successfully", true);
                            btnImport.Enabled = false;
                            RadGridLanding.DataSource = null;
                            RadGridLanding.DataBind();
                            RadGridLanding.Visible = false;
                            BtnAnalyze.Enabled = false;
                            btnUploadFile.Enabled = false;
                            upFile.Enabled = false;
                            lblFilenameUploaded.Text = "";
                            ddlGBVType.SelectedIndex = 0;
                        }
                       
                    }
                    catch (Exception ex)
                    {

                        AddErrorMessage(ex.Message);
                        return;
                    }

                }
            }
        }
        private void ImportedGBV()
        {
            if (Session["_MainImport"] != null && Session["_DetailImport"] != null)
            {
                List<GBVMain> mainList = Session["_MainImport"] as List<GBVMain>;
                List<GBVDetail> detailList = Session["_DetailImport"] as List<GBVDetail>;


                if (mainList != null && mainList.Count != 0 && detailList != null && detailList.Count != 0)
                {
                    // Imported Excel 
                    try
                    {
                        int userId = SSSNPHelper.TryParseInt(Session["uid"].ToString());

                        CreateGBVImported cRecord = new CreateGBVImported(mainList, detailList, userId);
                        int result = cRecord.CreateGBV();

                        if (result != 0)
                        {

                            Session["_MainImport"] = null;
                            Session["_DetailImport"] = null;

                            int importedId = SSSNPHelper.TryParseInt(hdnUploadedFile.Value.ToString());

                            GBVImportFile iFile = SSSNPContext.SSSNPManager.GetGBVImportedFile(importedId);
                            if (iFile != null || iFile.ImportedID != 0)
                            {
                                SSSNPContext.SSSNPManager.UpdateStatusGBVImportedFile(iFile.ImportedID);
                            }

                            AddSuccessMessage("GBV Imported Successfully", true);
                            btnImport.Enabled = false;
                            RadGridLanding.DataSource = null;
                            RadGridLanding.DataBind();
                            RadGridLanding.Visible = false;
                            BtnAnalyze.Enabled = false;
                            btnUploadFile.Enabled = false;
                            upFile.Enabled = false;
                            lblFilenameUploaded.Text = "";
                            ddlGBVType.SelectedIndex = 0;
                        }

                    }
                    catch (Exception ex)
                    {

                        AddErrorMessage(ex.Message);
                        return;
                    }

                }
            }
        }

    }
}
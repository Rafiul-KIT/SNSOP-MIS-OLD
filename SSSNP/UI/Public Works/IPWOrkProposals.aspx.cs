using System;
using System.Collections.Generic;
using System.Linq;
#region Using/Inherits Classess
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;
using SSSNP.Network;
#endregion

#region Amendment History Begin
//Form Name : To Add and Approval Work Proposal Registrations
//Purpose : To Add and Approval Work Proposal Registrations Details
//Tables used :  tbl_WorkMaster,tr_MemsInvolved,tr_ProposedWorkAct
//Stored procedures Used : InsertUpdWorkProposal,saveMemsInvolvedinWorksProposal,GetWorkProposalByid,sp_getCounties,sp_getPayams,getBomasbyID,sp_getStates
#endregion Amendment History End
public partial class IPWOrkProposals : System.Web.UI.Page
{

    #region reference class calling and Variable declaration
    int delete = 0;
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    DataRow dtrdr;
    DataTable myDtNewRecdr = new DataTable();
    private EmailService emailService = new EmailService();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        // Is not Postback Statement
        if (!IsPostBack)
        {
            // Look up Click Function Intialization
            btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

            //Estimated Work Start date
            CalendarExtender2.StartDate = DateTime.Now;

            //Estimated Work End date
            CalendarExtender3.StartDate = DateTime.Now;

            ddlScreeningStatus.Items.Add("Accepted");
            ddlScreeningStatus.Items.Add("Rejected");

            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);

            // User wise Dropdown allotement
            if (Session["user_type"].ToString() == "TST")
            {
                lblheading.Text = "Work Approvals";
                lblSite2.Text = "Work Approvals";
                lblSitemap.Text = "TST";

                screeningStatusLabel.Visible = false;
                ddlScreeningStatus.Visible = false;
            }
            else
            {
                lblheading.Text = "Work Proposals";
                lblSitemap.Text = "IP";
                lblSite2.Text = "Work Proposals";
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString()).Value;
                ddlIP.Enabled = false;
                Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            }

            if (Session["user_type"].ToString() == "IP")
            {
                ddlstatus.Items.Add("QE-Rejected");
            }

                // Checkbox Binding
                Gen.getCouncilMems(chkTst, "TST");
            Gen.getCouncilMems(ChkCA, "CA");
            Gen.getCouncilMems(ChkPDC, "PDC");
            Gen.getCouncilMems(ChkBDC, "BDC");

            Session["dtMyTabledr"] = createtabledr();
            Session["tmpdrDataTable"] = ((DataTable)Session["dtMyTabledr"]);
            #region fill details Calling using Request query string
            if (Request.QueryString.Count > 0)
            {
                if (Session["user_type"].ToString() == "TST")
                {
                    trTSTApproval.Visible = true;
                }
                else
                {
                    trTSTApproval.Visible = false;
                }
                trworkcode.Visible = true;
                Failure.Visible = false;
                success.Visible = false;
                
                FillDetails();
                if (Session["userlevel"].ToString() == "1")
                {
                    BtnDelete.Visible = true;
                }
                else
                {
                    BtnDelete.Visible = false;
                }
                BtnSave1.Text = "Update";
            }
            #endregion
        }

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            if (Session["user_type"].ToString() == "TST")
            {
                trTSTApproval.Visible = true;
            }
            else
            {
                trTSTApproval.Visible = false;
            }
            trworkcode.Visible = true;
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            if (Session["userlevel"].ToString() == "1")
            {
                BtnDelete.Visible = true;
            }
            else
            {
                BtnDelete.Visible = false;
            }
            BtnSave1.Text = "Update";
        }
        #endregion
    }
    #endregion

    #region Upload Files
    void UploadImage()
    {
        //PhotoFilename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        Session["PhotoFileName"] = System.IO.Path.GetFileName(FileWorkPhotoUpload.PostedFile.FileName);
        try
        {
            string[] fileType = FileWorkPhotoUpload.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;

            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {
                Session["Photo"] = new byte[FileWorkPhotoUpload.PostedFile.ContentLength];
                HttpPostedFile Image = FileWorkPhotoUpload.PostedFile;
                Image.InputStream.Read((byte[])Session["Photo"], 0, (int)FileWorkPhotoUpload.PostedFile.ContentLength);
                lblFileName.Text = Session["PhotoFileName"].ToString();
            }
            else
            {
                lblmsg.Text = "Image format should be JPG or JPEG or GIF or PNG";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "An Error Occured. Please Try Again!" + ex.Message;
        }
        finally
        {
        }
        FileWorkPhotoUpload.Focus();
    }

    void UploadEquipmentImage()
    {
        Session["EquipmentPhotoFileName"] = System.IO.Path.GetFileName(FileEquipmentPhoto.PostedFile.FileName);

        try
        {
            string[] fileType = FileEquipmentPhoto.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;

            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {
                Session["EquipmentPhoto"] = new byte[FileEquipmentPhoto.PostedFile.ContentLength];
                HttpPostedFile Image = FileEquipmentPhoto.PostedFile;
                Image.InputStream.Read((byte[])Session["EquipmentPhoto"], 0, (int)FileEquipmentPhoto.PostedFile.ContentLength);
                lblEquipment.Text = Session["EquipmentPhotoFileName"].ToString();
            }
            else
            {
                lblmsg.Text = "Image format should be JPG or JPEG or GIF or PNG";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "An Error Occured. Please Try Again!" + ex.Message;
        }
        finally
        {
        }
        FileWorkPhotoUpload.Focus();
    }

    void UploadMaterialImage()
    {
        Session["MaterialPhotoFileName"] = System.IO.Path.GetFileName(FileUploadMaterialsPhoto.PostedFile.FileName);

        try
        {
            string[] fileType = FileUploadMaterialsPhoto.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;
            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {
                Session["MaterialPhoto"] = new byte[FileUploadMaterialsPhoto.PostedFile.ContentLength];
                HttpPostedFile Image = FileUploadMaterialsPhoto.PostedFile;
                Image.InputStream.Read((byte[])Session["MaterialPhoto"], 0, (int)FileUploadMaterialsPhoto.PostedFile.ContentLength);
                lblMaterialFileName.Text = Session["MaterialPhotoFileName"].ToString();
                ImageMaterial.ImageUrl = "ScanImage.aspx";
            }
            else
            {
                lblmsg.Text = "Image format should be JPG or JPEG or GIF or PNG";
            }
        }
        catch (Exception ex)
        {
            lblmsg.Text = "An Error Occured. Please Try Again!" + ex.Message;
        }
        finally
        {
        }
        FileWorkPhotoUpload.Focus();
    }

    #endregion

    #region Reset Controls
    public void ResetFormControlDisableEnable(Control parent, bool s)
    {
        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlDisableEnable(c, s);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Enabled = s;
                        break;
                    case "System.Web.UI.WebControls.DropDownList":
                        ((DropDownList)c).Enabled = s;
                        break;
                    case "System.Web.UI.WebControls.CheckBoxList":
                        ((CheckBoxList)c).Enabled = s;
                        break;
                }
            }
        }

        chkTst.Enabled = false;
        ChkCA.Enabled = false;
        ChkPDC.Enabled = false;
        ChkBDC.Enabled = false;
    }
    #endregion

    #region Fill Details from Look up
    void FillDetails()
    {
        string ids = "";
        if (Request.QueryString.Count > 0)
        {
            ids = Request.QueryString[0].ToString();
        }
        else
        {
            ids = hdfID.Value;
        }
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

        Session["intWorkId"] = ids.ToString();
        ds = Gen.GetWorkProposalByid(ids.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            hdfTSTID.Value = ds.Tables[0].Rows[0]["intTST"].ToString();
            hdfTSTEmail.Value = ds.Tables[0].Rows[0]["TSTemail"].ToString();
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["Ipid"].ToString()).Value;
            ddlIP.Enabled = false;
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["intprjid"].ToString();

            getworksbyPojects();

            if (ds.Tables[0].Rows[0]["status"].ToString() != "") 
            {
                try
                {
                    ddlstatus.SelectedValue = ddlstatus.Items.FindByValue(ds.Tables[0].Rows[0]["status"].ToString()).Value;
                } catch (Exception e)
                {

                }
            }

            if (ddlstatus.SelectedValue == "InProgress" || ddlstatus.SelectedValue == "Closed")
            {
                BtnSave1.Enabled = true;
            }
            else
            {
                BtnSave1.Enabled = true;
            }

            if (ddlstatus.SelectedValue != "New Proposal")
            {
            }
            else
            {
            }
            if (ddlstatus.SelectedValue == "Approved")
            {
                Label426.Text = "Approved Date";
            }
            else if (ddlstatus.SelectedValue == "TST-Rejected")
            {
                Label426.Text = "Rejected Date";
            }
            else
            {
                Label426.Text = "Date";
            }
            if (ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString() != "")
                txtPDCApprvDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[0].Rows[0]["StatusDate"].ToString() != "")
                txtApprRejDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["StatusDate"].ToString()).ToString("dd-MM-yyyy");
            txtRemarks.Text = ds.Tables[0].Rows[0]["Remarks"].ToString();
            if (ds.Tables[0].Rows[0]["TypeofMeeting"].ToString() != "")
                ddlMeeting.SelectedValue = ds.Tables[0].Rows[0]["TypeofMeeting"].ToString();
            getprojectDet();
            txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["MeetingDate"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[0].Rows[0]["screening_status"].ToString() != "")
            {
                if (ds.Tables[0].Rows[0]["screening_status"].ToString() == "1")
                {
                    ddlScreeningStatus.SelectedValue = "Accepted";
                }
                else if (ds.Tables[0].Rows[0]["screening_status"].ToString() == "2")
                {
                    ddlScreeningStatus.SelectedValue = "Rejected";
                }
                else
                {
                    ddlScreeningStatus.SelectedIndex = 0;
                }
            }
            else
            {
                ddlScreeningStatus.SelectedIndex = 0;
            }
            if (ds.Tables[0].Rows[0]["intStateid"].ToString() != "")
            {
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["intStateid"].ToString();
                getcountiesbyProjectid();
            }
            if (ds.Tables[0].Rows[0]["intCountieid"].ToString() != "")
            {
                ddlCounties.SelectedValue = ds.Tables[0].Rows[0]["intCountieid"].ToString();
                getPayams();
            }
            if (ds.Tables[0].Rows[0]["intPayamid"].ToString() != "")
            {
                ddlPayam.SelectedValue = ds.Tables[0].Rows[0]["intPayamid"].ToString();
                getBomas();
            }
            if (ds.Tables[0].Rows[0]["intBomasid"].ToString() != "")
                ddlBoma.SelectedValue = ds.Tables[0].Rows[0]["intBomasid"].ToString();

            lblworkcode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();

            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTableReader rdp = new DataTableReader(ds.Tables[1]);
                IDataReader readerp = rdp;
                ((DataTable)Session["tmpdrDataTable"]).Clear();
                ((DataTable)Session["tmpdrDataTable"]).Load(readerp);
                gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                GetFileAttch();
                gvpractical0.DataBind();
                gvpractical0.Columns[0].Visible = true;
            }
            else
            {
                gvpractical0.DataSource = null;
                gvpractical0.DataBind();
            }
            if (ds.Tables[2].Rows.Count > 0)
            {
                foreach (ListItem item in chkTst.Items)
                {
                    for (int n = 0; n < ds.Tables[2].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[2].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            if (ds.Tables[3].Rows.Count > 0)
            {
                foreach (ListItem item in ChkCA.Items)
                {
                    for (int n = 0; n < ds.Tables[3].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[3].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }

            if (ds.Tables[4].Rows.Count > 0)
            {
                foreach (ListItem item in ChkPDC.Items)
                {
                    for (int n = 0; n < ds.Tables[4].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[4].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
            if (ds.Tables[5].Rows.Count > 0)
            {
                foreach (ListItem item in ChkBDC.Items)
                {
                    for (int n = 0; n < ds.Tables[5].Rows.Count; n++)
                    {
                        if (item.Value == ds.Tables[5].Rows[n]["intMemid"].ToString().Trim())
                        {
                            item.Selected = true;
                        }
                    }
                }
            }
        }

        DataSet dsWorkProposalAttachments = Gen.GetWorkProposalAttachments(ids);
        if (dsWorkProposalAttachments.Tables[0].Rows.Count > 0)
        {
            gvWorkProposalAttachments.DataSource = dsWorkProposalAttachments.Tables[0];
            gvWorkProposalAttachments.DataBind();
        }
        else
        {
            gvWorkProposalAttachments.DataSource = null;
            gvWorkProposalAttachments.DataBind();
        }

        if (ds.Tables[1].Rows.Count > 0)
        {
            try
            {
                txtWorkName.Text = ds.Tables[1].Rows[0]["WorkName"].ToString();
                txtWorkCode.Text = ds.Tables[1].Rows[0]["WorkCode"].ToString();
                ddlAreaofWork.SelectedValue = ds.Tables[1].Rows[0]["intAreaofWork"].ToString();
                ddlAreasofWork_SelectedIndexChanged(null, null);
                ddlWorkActivity.SelectedValue = ds.Tables[1].Rows[0]["intWorkActivity"].ToString();
                txtManpower.Text = ds.Tables[1].Rows[0]["EstManPower"].ToString();
                txtEstDays.Text = ds.Tables[1].Rows[0]["EstDays"].ToString();
                txtEstquantity1.Text = ds.Tables[1].Rows[0]["EquipmntQuantity"].ToString();
                txtEstquantity2.Text = ds.Tables[1].Rows[0]["MaterialQuantity"].ToString();
                txtSkillLabour.Text = ds.Tables[1].Rows[0]["EstSkilLabour"].ToString();
                txtWorkStartDate.Text = ds.Tables[1].Rows[0]["EstWorkStartDate"].ToString();
                txtEndDate.Text = ds.Tables[1].Rows[0]["EstWorkEndDate"].ToString();
                txtLatitude.Text = ds.Tables[1].Rows[0]["Latitude"].ToString();
                txtLongitude.Text = ds.Tables[1].Rows[0]["Longitude"].ToString();
                txtEstCost.Text = ds.Tables[1].Rows[0]["EstCost"].ToString();
            }
            catch (Exception e)
            {

            }
        }
    }
    #endregion

    private void GetFileAttch()
    {
    }

    #region Gridview Events
    protected void gvpractical0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblproposedID = (e.Row.FindControl("lblproposedID") as Label);
            //Access Cell values.
            int proposedId = Convert.ToInt32(lblproposedID.Text);
            string filename = "";
            proposedId = Convert.ToInt32(lblproposedID.Text);
            LinkButton lnkDownloadWorkRequire = (LinkButton)e.Row.FindControl("lnkDownloadWorkRequire");
            LinkButton lnkDownloadMaterialRequire = (LinkButton)e.Row.FindControl("lnkDownloadMaterialRequire");
            LinkButton lnkDownloadEquipmentRequire = (LinkButton)e.Row.FindControl("lnkDownloadEquipmentRequire");
            DataSet ds = new DataSet();
            ds = Gen.Sp_GetFileAttchment(Convert.ToString(proposedId));
            if (ds.Tables.Count > 0)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lnkDownloadWorkRequire.Text = ds.Tables[0].Rows[0][0].ToString();
                    lnkDownloadWorkRequire.CommandArgument = ds.Tables[0].Rows[0][1].ToString();
                }
                else
                {
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    lnkDownloadMaterialRequire.Text = ds.Tables[2].Rows[0][0].ToString();
                    lnkDownloadMaterialRequire.CommandArgument = ds.Tables[2].Rows[0][1].ToString();
                }
                else
                {
                }
                if (ds.Tables[1].Rows.Count > 0)
                {
                    lnkDownloadEquipmentRequire.Text = ds.Tables[1].Rows[0][0].ToString();
                    lnkDownloadEquipmentRequire.CommandArgument = ds.Tables[1].Rows[0][1].ToString();
                }
                else
                {
                }
            }
        }
    }

    protected void gvpractical0_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (BtnSave1.Text == "Save")
            {
                ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
                this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                this.gvpractical0.DataBind();
            }
            else
            {
                if (((DataTable)Session["tmpdrDataTable"]).Rows[e.RowIndex]["new"].ToString().Trim() == "")
                {
                    int i = Gen.DeleteWorkProposal(gvpractical0.DataKeys[e.RowIndex].Values["intWorkActivityId"].ToString());
                    ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();
                    txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = ""; txtLongitude.Text = ""; txtLatitude.Text = "";
                }
                else
                {
                    ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();
                    txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }

    protected void gvpractical0_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int rowval;
            rowval = gvpractical0.SelectedIndex;
            hdfRowIndex.Value = rowval.ToString();
            hdn_work_id.Value = gvpractical0.SelectedDataKey.Values["intWorkid"].ToString();
            txtWorkCode.Text = gvpractical0.SelectedDataKey.Values["WorkCode"].ToString();
            txtWorkName.Text = gvpractical0.SelectedDataKey.Values["WorkName"].ToString();
            ddlAreaofWork.SelectedValue = gvpractical0.SelectedDataKey.Values["intAreaofWork"].ToString();
            getWorkActivities();
            ddlWorkActivity.SelectedValue = gvpractical0.SelectedDataKey.Values["intWorkActivity"].ToString();
            txtManpower.Text = gvpractical0.SelectedDataKey.Values["EstManPower"].ToString();
            txtEstDays.Text = gvpractical0.SelectedDataKey.Values["EstDays"].ToString();
            txtEstEquipmntReq.Text = gvpractical0.SelectedDataKey.Values["EstEquipmnts"].ToString();
            txtEstiMaterial.Text = gvpractical0.SelectedDataKey.Values["EstMaterial"].ToString();
            txtSkillLabour.Text = gvpractical0.SelectedDataKey.Values["EstSkilLabour"].ToString();
            txtWorkStartDate.Text = Convert.ToDateTime(gvpractical0.SelectedDataKey.Values["EstWorkStartDate"].ToString()).ToString("dd-MM-yyyy");
            txtEndDate.Text = Convert.ToDateTime(gvpractical0.SelectedDataKey.Values["EstWorkEndDate"].ToString()).ToString("dd-MM-yyyy");
            txtEstCost.Text = gvpractical0.SelectedDataKey.Values["EstCost"].ToString();
            txtEstquantity1.Text = gvpractical0.SelectedDataKey.Values["EquipmntQuantity"].ToString();
            txtEstquantity2.Text = gvpractical0.SelectedDataKey.Values["MaterialQuantity"].ToString();
            txtLongitude.Text = gvpractical0.SelectedDataKey.Values["Longitude"].ToString();
            txtLatitude.Text = gvpractical0.SelectedDataKey.Values["Latitude"].ToString();

            //Set file Name File Upload control
            string work_id = gvpractical0.SelectedDataKey.Values["intWorkid"].ToString();
            DataSet ds_file_name = new DataSet();
            ds_file_name = Gen.GetFileNameByWorkId_Sp(work_id);
            for (int j = 0; j < ds_file_name.Tables[0].Rows.Count; j++)
            {
                string valueStr = ds_file_name.Tables[0].Rows[j]["PhotoFileName"].ToString();
                var vals = valueStr.Split('_')[0];
                var file_name = ds_file_name.Tables[0].Rows[j]["PhotoFileName"].ToString();
                var propose_id = ds_file_name.Tables[0].Rows[j]["IntWorkDetailsTypeId"].ToString();
                if (vals == "WorkDocument")
                {
                    hdn_Work_Photo.Value = file_name;
                }

                if (vals == "MaterialDocument")
                {
                    hdn_Material_Photo.Value = file_name;
                }

                if (vals == "EquipmentDocument")
                {
                    hdn_Equipment_Photo.Value = file_name;
                }
                Gen.Deletetbl_Attachment_Sp(work_id);
            }
            BtnSave2.Text = "Update";
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
    #endregion

    #region Button Click functionalities

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    private void sendEmailToQualityEngineer()
    {
        try
        {
            string emailSubject = "Sub-project Approval";
            string emailBody = "<H2>Dear Quality Engineer (SNSOP MIS),</H2> <br><br>" +
                "A new sub-project has been submitted which needs your approval to move forward. Please review.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmailToUserType(emailSubject, emailBody, "QualEng");
        } catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending email to Quality Engineer. Error details:\n" + ex.ToString());
            return;
        }
    }

    private void sendEmailToEnvironmentSpecialistAboutRejection(string workId)
    {
        try
        {
            string emailSubject = "Sub-project Rejection by TST";
            string emailBody = "<H2>Dear Environment Specialist (SNSOP MIS),</H2> <br><br>" +
                "The Work proposal with ID "+workId+" has been rejected by the TST. Please review.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmailToUserType(emailSubject, emailBody, "EnvSpec");
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending rejection email to Environment Specialist. Error details:\n" + ex.ToString());
            return;
        }
    }

    private void sendApprovalMailToIpByTST(string ipMail, string workId)
    {
        try
        {
            string emailSubject = "Sub-project Approval";
            string emailBody = "<H2>Dear Implementing Partner (SNSOP MIS),</H2> <br><br>" +
                "A sub-project that you created has been approved by TST. Please take further actions.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmail(ipMail, emailSubject, emailBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending email to IP. Error details:\n" + ex.ToString());
            return;
        }
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        var vals = hdfID.Value.Split(',')[0];
        string ids = "";
        if (Request.QueryString.Count > 0)
        {
            ids = Request.QueryString[0].ToString();
        }
        else
        {
            ids = vals;
        }

        if (BtnSave1.Text == "Save")
        {
            string screeningStatus = null;
            if (ddlScreeningStatus != null)
            {
                if (ddlScreeningStatus.SelectedValue.ToString() == "Accepted")
                {
                    screeningStatus = "1";
                }
                else if (ddlScreeningStatus.SelectedValue.ToString() == "Rejected")
                {
                    screeningStatus = "2";
                }
            }
            //New step for approving work proposals
            int i = Gen.InsertUpdWorkProposal("0", "", Session["User_Code"].ToString(), ddlProject.SelectedValue, txtStartDate.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, (screeningStatus == "1") ? "IP-Submitted" : null, txtApprRejDate.Text, txtPDCApprvDate.Text, txtRemarks.Text, Session["uid"].ToString(), ddlMeeting.SelectedValue, screeningStatus);

            if (ddlstatus.SelectedValue == "TST-Rejected")
            {
                Gen.insertWorkComment(ids.ToString(), txtRemarks.Text, ddlstatus.SelectedValue, Session["uid"].ToString());
                DataSet dsEnvironmentSpecialistEmails = Gen.GetUsersDetailsByRole("EnvSpec");
                string strEmailList = string.Empty;
                int icountUsers = dsEnvironmentSpecialistEmails.Tables[0].Rows.Count;
                for (int iCounter = 0; iCounter < icountUsers; iCounter++)
                {
                    if (iCounter == 0)
                    {
                        strEmailList = dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        strEmailList = strEmailList + "," + dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    }
                }
                Gen.SendMail(strEmailList, "SNSOP Notifications - Work Proposal Status", string.Format("Notification <br>: the Work proposal with ID {0} has been rejected by the TST. <br> Please check and review it according to comments provided.", Session["intWorkId"].ToString()));
            }

            if (fuWorkProposalAttachment.HasFile)
            {
                string strWork_Photo_Name = "WorkProposalAttachment_";
                string strWorkProposalAttachmentFileName = fuWorkProposalAttachment.FileName;
                Gen.InsertAttachements_Sp("WorkProposalAttachment", ids, strWork_Photo_Name, Session["uid"].ToString(), strWorkProposalAttachmentFileName, ids);
                string WorkPhotoStr = ("~/WorkProposalAttachment/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName;
                if (!Directory.Exists(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/"));
                }
                fuWorkProposalAttachment.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName);
            }

            if (i != 999)
            {
                string appNo = "";
                appNo = DateTime.Now.Year.ToString() + "-" + "SNSOP" + "-" + ddlProject.SelectedItem.Text + "-" + i.ToString();

                if (i >= 0)
                {
                    int y = Gen.UpdWorkCode(i.ToString(), appNo.ToString());
                }
                if (((DataTable)Session["tmpdrDataTable"]).Rows.Count > 0)
                {
                    for (int m = 0; m < ((DataTable)Session["tmpdrDataTable"]).Rows.Count; m++)
                    {
                        if (((DataTable)Session["tmpdrDataTable"]).Rows[m]["intWorkActivityId"].ToString().Trim() == "0")//intWorkid,intWorkActivityId
                        {
                            ((DataTable)Session["tmpdrDataTable"]).Rows[m]["intWorkid"] = Convert.ToString(i);
                        }
                    }
                    GetNewRectoInsertdr();
                    int statuspr = Gen.bulkInsertdrNewDetails(myDtNewRecdr);

                    if (((DataTable)Session["tmpdrDataTable"]).Rows.Count > 0)
                    {
                        DataSet dsProposed_Work = new DataSet();
                        dsProposed_Work = Gen.GetProposedWorkActId_Sp(i);
                        if (dsProposed_Work.Tables[0].Rows.Count > 0)
                        {
                            for (int j = 0; j < dsProposed_Work.Tables[0].Rows.Count; j++)
                            {
                                hdn_work_activity_id.Value = dsProposed_Work.Tables[0].Rows[j]["intWorkActivityId"].ToString();
                                hdn_work_code.Value = dsProposed_Work.Tables[0].Rows[j]["WorkCode"].ToString();
                                hdn_return_value.Value = Convert.ToString(i);

                                string WorkPhotoStr = "";
                                string EquipmentPhotoStr = "";
                                string MaterialPhotoStr = "";

                                //1. work photo upload
                                if (hdn_Work_Photo.Value != "")
                                {
                                    string filename = "";
                                    if (((DataTable)Session["tmpdrDataTable"]).Rows[j]["WorkPhoto"].ToString().Trim() != "")//intWorkid,intWorkActivityId
                                    {
                                        string Work_Photo_Name = "WorkDocument_";
                                        filename = Convert.ToString(((DataTable)Session["tmpdrDataTable"]).Rows[j]["WorkPhoto"]);
                                        WorkPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value + "/") + filename;
                                        Gen.InsertAttachements_Sp("Proposal", hdn_return_value.Value, WorkPhotoStr, Session["uid"].ToString(), filename, hdn_work_activity_id.Value);
                                    }
                                    else
                                    {
                                        filename = "";
                                    }
                                }
                                //2. equipment photo upload
                                if (hdn_Equipment_Photo.Value != "")
                                {
                                    string filename = "";
                                    if (((DataTable)Session["tmpdrDataTable"]).Rows[j]["EquipmentPhoto"].ToString().Trim() != "")//intWorkid,intWorkActivityId
                                    {
                                        string Equipment_Photo_Name = "EquipmentDocument_";
                                        filename = Convert.ToString(((DataTable)Session["tmpdrDataTable"]).Rows[j]["EquipmentPhoto"]);
                                        EquipmentPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value + "/") + filename;
                                        Gen.InsertAttachements_Sp("Proposal", hdn_return_value.Value, EquipmentPhotoStr, Session["uid"].ToString(), filename, hdn_work_activity_id.Value);
                                    }
                                    else
                                    {
                                        filename = "";
                                    }


                                }
                                //3. Material photo upload
                                if (hdn_Material_Photo.Value != "")
                                {
                                    string filename = "";
                                    if (((DataTable)Session["tmpdrDataTable"]).Rows[j]["MaterialsPhoto"].ToString().Trim() != "")//intWorkid,intWorkActivityId
                                    {
                                        string Material_Photo_Name = "MaterialDocument_";
                                        filename = Convert.ToString(((DataTable)Session["tmpdrDataTable"]).Rows[j]["MaterialsPhoto"]);
                                        MaterialPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value + "/") + filename;
                                        Gen.InsertAttachements_Sp("Proposal", hdn_return_value.Value, MaterialPhotoStr, Session["uid"].ToString(), filename, hdn_work_activity_id.Value);
                                    }
                                    else
                                    {
                                        filename = "";
                                    }
                                }
                            }
                            hdn_work_activity_id.Value = "";
                        }
                    }
                }

                #region Inserting of Members Involved
                //TST
                foreach (ListItem objItem in chkTst.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(Convert.ToString(i), objItem.Value, "TST", Session["uid"].ToString());
                    }
                }
                //CA
                foreach (ListItem objItem in ChkCA.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(Convert.ToString(i), objItem.Value, "CA", Session["uid"].ToString());
                    }
                }
                //PDC
                foreach (ListItem objItem in ChkPDC.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(Convert.ToString(i), objItem.Value, "PDC", Session["uid"].ToString());
                    }
                }

                //BDC
                foreach (ListItem objItem in ChkBDC.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(Convert.ToString(i), objItem.Value, "BDC", Session["uid"].ToString());
                    }
                }

                #endregion
                DataSet ds = Gen.GetWorkProposalByid(i.ToString());

                if (ds.Tables[0].Rows.Count > 0)
                {

                    hdfTSTID.Value = ds.Tables[0].Rows[0]["intTST"].ToString();
                    hdfTSTEmail.Value = ds.Tables[0].Rows[0]["TSTemail"].ToString();
                    hdfTSTName.Value = ds.Tables[0].Rows[0]["TstName"].ToString();
                    hdfWorkCode.Value = ds.Tables[0].Rows[0]["WorkCode"].ToString();
                }
                SendMailAnother(hdfTSTEmail.Value, hdfWorkCode.Value);

                lblmsg.Text = "Submited Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();

                if (screeningStatus == "1")
                {
                    sendEmailToQualityEngineer();
                }

                Response.Redirect("Result.aspx");
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
            }
        }
        else
        {
            string screeningStatus = null;
            if (ddlScreeningStatus != null)
            {
                if (ddlScreeningStatus.SelectedValue.ToString() == "Accepted")
                {
                    screeningStatus = "1";
                }
                else if (ddlScreeningStatus.SelectedValue.ToString() == "Rejected")
                {
                    screeningStatus = "2";
                }
            }
            int i = Gen.InsertUpdWorkProposal(ids.ToString(), lblworkcode.Text, Session["User_Code"].ToString(), ddlProject.SelectedValue, txtStartDate.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, ddlstatus.SelectedValue, txtApprRejDate.Text, txtPDCApprvDate.Text, txtRemarks.Text, Session["uid"].ToString(), ddlMeeting.SelectedValue, screeningStatus);
            if (ddlstatus.SelectedValue == "TST-Rejected")
            {
                Gen.insertWorkComment(ids.ToString(), txtRemarks.Text, ddlstatus.SelectedValue, Session["uid"].ToString());
                DataSet dsEnvironmentSpecialistEmails = Gen.GetUsersDetailsByRole("EnvSpec");
                string strEmailList = string.Empty;
                int icountUsers = dsEnvironmentSpecialistEmails.Tables[0].Rows.Count;
                for (int iCounter = 0; iCounter < icountUsers; iCounter++)
                {
                    if (iCounter == 0)
                    {
                        strEmailList = dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        strEmailList = strEmailList + "," + dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    }
                }
                Gen.SendMail(strEmailList, "SNSOP Notifications - Work Proposal Status", string.Format("Notification <br>: the Work proposal with ID {0} has been rejected by the TST. <br> Please check and review it according to comments provided.", Session["intWorkId"].ToString()));

                sendEmailToEnvironmentSpecialistAboutRejection(Session["intWorkId"].ToString());

            }
            if (fuWorkProposalAttachment.HasFile)
            {
                string strWork_Photo_Name = "WorkProposalAttachment_";
                string strWorkProposalAttachmentFileName = fuWorkProposalAttachment.FileName;
                Gen.InsertAttachements_Sp("WorkProposalAttachment", ids, strWork_Photo_Name, Session["uid"].ToString(), strWorkProposalAttachmentFileName, ids);
                string WorkPhotoStr = ("~/WorkProposalAttachment/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName;
                if (!Directory.Exists(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/")))
                {
                    Directory.CreateDirectory(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/"));
                }
                fuWorkProposalAttachment.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName);
            }

            if (i != 999)
            {
                hdfID.Value = hdfID.Value.Split(',')[0];
                if (((DataTable)Session["tmpdrDataTable"]).Rows.Count > 0)
                {
                    for (int m = 0; m < ((DataTable)Session["tmpdrDataTable"]).Rows.Count; m++)
                    {
                        if (((DataTable)Session["tmpdrDataTable"]).Rows[m]["intWorkActivityId"].ToString().Trim() == "0")//intWorkid
                        {
                            ((DataTable)Session["tmpdrDataTable"]).Rows[m]["intWorkid"] = Convert.ToString(hdfID.Value);
                        }
                    }
                    GetNewRectoInsertdr();
                    int statuspr = Gen.bulkInsertdrNewDetails(myDtNewRecdr);
                    DataSet dsProposed_Work = new DataSet();

                    dsProposed_Work = Gen.GetProposedWorkActId_Sp(Convert.ToInt32(hdfID.Value));
                    if (dsProposed_Work.Tables[0].Rows.Count > 0)
                    {
                        for (int j = 0; j < dsProposed_Work.Tables[0].Rows.Count; j++)
                        {
                            hdn_work_activity_id.Value = dsProposed_Work.Tables[0].Rows[j]["intWorkActivityId"].ToString();
                            hdn_work_code.Value = dsProposed_Work.Tables[0].Rows[j]["WorkCode"].ToString();
                            hdn_return_value.Value = Convert.ToString(hdfID.Value);
                            hdn_work_code.Value = txtWorkCode.Text;

                            string Directory_Path = Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value);
                            string WorkPhotoStr = "";
                            string EquipmentPhotoStr = "";
                            string MaterialPhotoStr = "";

                            //1. work photo upload
                            if (hdn_Work_Photo.Value != "")
                            {
                                string filename = "";
                                if (((DataTable)Session["tmpdrDataTable"]).Rows[j]["WorkPhoto"].ToString().Trim() != "")//intWorkid,intWorkActivityId
                                {
                                    string Work_Photo_Name = "WorkDocument_";
                                    filename = Convert.ToString(((DataTable)Session["tmpdrDataTable"]).Rows[j]["WorkPhoto"]);
                                    WorkPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value + "/") + filename;
                                    Gen.InsertAttachements_Sp("Proposal", hdn_return_value.Value, WorkPhotoStr, Session["uid"].ToString(), filename, hdn_work_activity_id.Value);
                                }
                                else
                                {
                                    filename = "";
                                }
                            }

                            //3. Material photo upload
                            if (hdn_Material_Photo.Value != "")
                            {
                                string filename = "";
                                if (((DataTable)Session["tmpdrDataTable"]).Rows[j]["MaterialsPhoto"].ToString().Trim() != "")//intWorkid,intWorkActivityId
                                {
                                    string Material_Photo_Name = "MaterialDocument_";
                                    filename = Convert.ToString(((DataTable)Session["tmpdrDataTable"]).Rows[j]["MaterialsPhoto"]);
                                    MaterialPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value + "/") + filename;
                                    Gen.InsertAttachements_Sp("Proposal", hdn_return_value.Value, MaterialPhotoStr, Session["uid"].ToString(), filename, hdn_work_activity_id.Value);
                                }
                                else
                                {
                                    filename = "";
                                }
                            }
                            //2. equipment photo upload
                            if (hdn_Equipment_Photo.Value != "")
                            {
                                string filename = "";
                                if (((DataTable)Session["tmpdrDataTable"]).Rows[j]["EquipmentPhoto"].ToString().Trim() != "")//intWorkid,intWorkActivityId
                                {
                                    string Equipment_Photo_Name = "EquipmentDocument_";
                                    filename = Convert.ToString(((DataTable)Session["tmpdrDataTable"]).Rows[j]["EquipmentPhoto"]);
                                    EquipmentPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + hdn_work_code.Value + "/") + filename;
                                    Gen.InsertAttachements_Sp("Proposal", hdn_return_value.Value, EquipmentPhotoStr, Session["uid"].ToString(), filename, hdn_work_activity_id.Value);
                                }
                                else
                                {
                                    filename = "";
                                }
                            }
                        }
                    }
                }
                DataSet ds = Gen.GetWorkProposalByid(hdfID.Value.ToString());
                if (ds.Tables[2].Rows.Count > 1)
                {
                    delete = Gen.deleteMemInvolved(hdfID.Value.ToString(), "TST");
                }
                if (ds.Tables[3].Rows.Count > 1)
                {
                    delete = Gen.deleteMemInvolved(hdfID.Value.ToString(), "CA");
                }
                if (ds.Tables[4].Rows.Count > 1)
                {
                    delete = Gen.deleteMemInvolved(hdfID.Value.ToString(), "PDC");
                }
                if (ds.Tables[5].Rows.Count > 1)
                {
                    delete = Gen.deleteMemInvolved(hdfID.Value.ToString(), "BDC");
                }
                #region Inserting of Members Involved
                //TST
                foreach (ListItem objItem in chkTst.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(hdfID.Value, objItem.Value, "TST", Session["uid"].ToString());
                    }
                }
                //CA
                foreach (ListItem objItem in ChkCA.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(hdfID.Value, objItem.Value, "CA", Session["uid"].ToString());
                    }
                }
                //PDC
                foreach (ListItem objItem in ChkPDC.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(hdfID.Value, objItem.Value, "PDC", Session["uid"].ToString());
                    }
                }
                //BDC
                foreach (ListItem objItem in ChkBDC.Items)
                {
                    if (objItem.Selected)
                    {
                        Gen.saveMemsInvolvedinWorksProposal(hdfID.Value, objItem.Value, "BDC", Session["uid"].ToString());
                    }
                }
                #endregion

                DataSet dsIp = Gen.GetWorkProposalByid(hdfID.Value.ToString());

                if (dsIp.Tables[0].Rows.Count > 0)
                {
                    hdfTSTID.Value = dsIp.Tables[0].Rows[0]["intTST"].ToString();
                    hdfTSTEmail.Value = dsIp.Tables[0].Rows[0]["TSTemail"].ToString();
                    hdfTSTName.Value = dsIp.Tables[0].Rows[0]["TstName"].ToString();
                    hdfWorkCode.Value = dsIp.Tables[0].Rows[0]["WorkCode"].ToString();
                    hdfIPEmail.Value = dsIp.Tables[0].Rows[0]["IPemaill"].ToString();
                }
                if (ddlstatus.SelectedValue == "Approved")
                {
                    SendMailAnotherApprv(hdfIPEmail.Value, hdfWorkCode.Value, ddlstatus.SelectedValue, hdfTSTName.Value);

                    sendApprovalMailToIpByTST(hdfIPEmail.Value, hdfWorkCode.Value);
                }

                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
                Response.Redirect("Result.aspx");
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
            }
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void BtnClear0_Click(object sender, EventArgs e)
    {
    }

    void clear()
    {
        trworkcode.Visible = false;
        trTSTApproval.Visible = false;
        txtRemarks.Text = "";
        txtPDCApprvDate.Text = "";
        txtApprRejDate.Text = "";
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        ddlstatus.SelectedIndex = 0;
        Gen.getCouncilMems(chkTst, "TST");
        Gen.getCouncilMems(ChkCA, "CA");
        Gen.getCouncilMems(ChkPDC, "PDC");
        Gen.getCouncilMems(ChkBDC, "BDC");
        ddlProject.SelectedIndex = 0;
        lblworkcode.Text = "";
        lblTargetBenefeciaries.Text = "";
        lblCost.Text = "";
        BtnSave1.Text = "Save";
        ddlMeeting.SelectedIndex = 0;
        txtStartDate.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCounties.Items.Clear();
        ddlCounties.Items.Insert(0, "--Select--");
        ddlCounties.SelectedIndex = 0;
        ddlPayam.Items.Clear();
        ddlPayam.Items.Insert(0, "--Select--");
        ddlPayam.SelectedIndex = 0;
        ddlBoma.Items.Clear();
        ddlBoma.Items.Insert(0, "--Select--");
        ddlBoma.SelectedIndex = 0;
        BtnDelete.Visible = false;
        ((DataTable)Session["tmpdrDataTable"]).Clear();
        gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
        gvpractical0.DataBind();
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        string Directory_Path = Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text);
        string WorkPhotoStr = "";
        string EquipmentPhotoStr = "";
        string MaterialPhotoStr = "";
        string work_photo = "";
        string equipment_photo = "";
        string material_photo = "";
        hdn_work_code.Value = txtWorkCode.Text;

        if (FileWorkPhotoUpload.PostedFile != null && FileWorkPhotoUpload.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileWorkPhotoUpload.PostedFile))
        {
            if (!Directory.Exists(Directory_Path))
            {
                Directory.CreateDirectory(Directory_Path);
                //1. work photo upload
                if (FileWorkPhotoUpload.HasFile == true)
                {
                    string Work_Photo_Name = "WorkDocument_";
                    string filename = FileWorkPhotoUpload.PostedFile.FileName;
                    filename = Work_Photo_Name + filename;
                    work_photo = filename;
                    WorkPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename;
                    FileWorkPhotoUpload.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename);
                    hdn_Work_Photo.Value = filename;
                    hdn_work_photo_download.Value = WorkPhotoStr;
                }
                //2. equipment photo upload
                if (FileEquipmentPhoto.HasFile == true)
                {
                    string Equipment_Photo_Name = "EquipmentDocument_";
                    string filename = FileEquipmentPhoto.PostedFile.FileName;
                    filename = Equipment_Photo_Name + filename;
                    equipment_photo = filename;
                    EquipmentPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename;
                    FileEquipmentPhoto.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename);
                    hdn_Equipment_Photo.Value = filename;
                    hdn_equipment_photo_download.Value = EquipmentPhotoStr;
                }
                //3. Material photo upload
                if (FileUploadMaterialsPhoto.HasFile == true)
                {
                    string Material_Photo_Name = "MaterialDocument_";
                    string filename = FileUploadMaterialsPhoto.PostedFile.FileName;
                    filename = Material_Photo_Name + filename;
                    material_photo = filename;
                    MaterialPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename;
                    FileUploadMaterialsPhoto.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename);
                    hdn_Material_Photo.Value = filename;
                    hdn_material_photo_download.Value = MaterialPhotoStr;
                }
            }
            else
            {
                //file check 
                //1. Work Photo upload
                DirectoryInfo di_files = new DirectoryInfo(Directory_Path);
                if (FileWorkPhotoUpload.HasFile == true)
                {
                    foreach (FileInfo file in di_files.GetFiles())
                    {
                        if (file.Name.Contains("WorkDocument_"))
                        {
                            hdn_work_photo_file.Value = Convert.ToString(file);
                            file.Delete();
                        }
                    }
                    string Work_Photo_Name = "WorkDocument_";
                    string filename = FileWorkPhotoUpload.PostedFile.FileName;
                    filename = Work_Photo_Name + filename;
                    work_photo = filename;
                    WorkPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename;
                    FileWorkPhotoUpload.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename);
                    hdn_Work_Photo.Value = filename;
                    hdn_work_photo_download.Value = WorkPhotoStr;
                }

                if (FileUploadMaterialsPhoto.HasFile == true)
                {
                    foreach (FileInfo file in di_files.GetFiles())
                    {
                        if (file.Name.Contains("MaterialDocument_"))
                        {
                            hdn_material_photo_file.Value = Convert.ToString(file);
                            file.Delete();
                        }
                    }
                    string Material_Photo_Name = "MaterialDocument_";
                    string filename = FileUploadMaterialsPhoto.PostedFile.FileName;
                    filename = Material_Photo_Name + filename;
                    material_photo = filename;
                    MaterialPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename;
                    FileUploadMaterialsPhoto.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename);
                    hdn_Material_Photo.Value = filename;
                    hdn_material_photo_download.Value = MaterialPhotoStr;
                }

                if (FileEquipmentPhoto.HasFile == true)
                {
                    foreach (FileInfo file in di_files.GetFiles())
                    {
                        if (file.Name.Contains("EquipmentDocument_"))
                        {
                            hdn_equipment_photo_file.Value = Convert.ToString(file);
                            file.Delete();
                        }
                    }
                    string Equipment_Photo_Name = "EquipmentDocument_";
                    string filename = FileEquipmentPhoto.PostedFile.FileName;
                    filename = Equipment_Photo_Name + filename;
                    equipment_photo = filename;
                    EquipmentPhotoStr = ("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename;
                    FileEquipmentPhoto.SaveAs(Server.MapPath("~/WorkProposalDoc/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + filename);
                    hdn_Equipment_Photo.Value = filename;
                    hdn_equipment_photo_download.Value = EquipmentPhotoStr;
                }
            }
        }
        try
        {

            if (BtnSave1.Text == "Update")
            {
                if (BtnSave2.Text.ToString().ToLower() == "update")
                {
                    int i = Gen.DeleteWorkProposal(gvpractical0.SelectedDataKey.Values["intWorkActivityId"].ToString());

                    ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(Convert.ToInt32(hdfRowIndex.Value.ToString()));
                    fillTrademappingGriddr((DataTable)Session["tmpdrDataTable"], txtWorkName.Text, txtWorkCode.Text, ddlAreaofWork.SelectedValue, ddlWorkActivity.SelectedValue, txtManpower.Text, txtEstDays.Text, txtEstEquipmntReq.Text, txtEstiMaterial.Text, txtSkillLabour.Text, cmf.convertDateIndia(txtWorkStartDate.Text).ToString("MM-dd-yyyy"), cmf.convertDateIndia(txtEndDate.Text).ToString("MM-dd-yyyy"), txtEstCost.Text, Session["uid"].ToString(), ddlAreaofWork.SelectedItem.Text, ddlWorkActivity.SelectedItem.Text, txtEstquantity1.Text, txtEstquantity2.Text, work_photo, equipment_photo, material_photo, txtLatitude.Text, txtLongitude.Text);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();
                    BtnSave2.Text = "Add New";
                    txtEstquantity1.Text = "";
                    txtEstquantity2.Text = "";
                    txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = "";
                    txtWorkCode.Text = "";
                    txtLongitude.Text = "";
                    txtLatitude.Text = "";
                }
                else
                {
                    fillTrademappingGriddr((DataTable)Session["tmpdrDataTable"], txtWorkName.Text, txtWorkCode.Text, ddlAreaofWork.SelectedValue, ddlWorkActivity.SelectedValue, txtManpower.Text, txtEstDays.Text, txtEstEquipmntReq.Text, txtEstiMaterial.Text, txtSkillLabour.Text, cmf.convertDateIndia(txtWorkStartDate.Text).ToString("MM-dd-yyyy"), cmf.convertDateIndia(txtEndDate.Text).ToString("MM-dd-yyyy"), txtEstCost.Text, Session["uid"].ToString(), ddlAreaofWork.SelectedItem.Text, ddlWorkActivity.SelectedItem.Text, txtEstquantity1.Text, txtEstquantity2.Text, work_photo, equipment_photo, material_photo, txtLatitude.Text, txtLongitude.Text);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();
                    txtEstquantity1.Text = "";
                    txtEstquantity2.Text = "";
                    txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = "";
                    txtWorkCode.Text = "";
                    txtLatitude.Text = "";
                    txtLongitude.Text = "";
                }
            }
            else
            {
                if (BtnSave2.Text.ToString().ToLower() == "update")
                {
                    ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(Convert.ToInt32(hdfRowIndex.Value.ToString()));
                    fillTrademappingGriddr((DataTable)Session["tmpdrDataTable"], txtWorkName.Text, txtWorkCode.Text, ddlAreaofWork.SelectedValue, ddlWorkActivity.SelectedValue, txtManpower.Text, txtEstDays.Text, txtEstEquipmntReq.Text, txtEstiMaterial.Text, txtSkillLabour.Text, cmf.convertDateIndia(txtWorkStartDate.Text).ToString("MM-dd-yyyy"), cmf.convertDateIndia(txtEndDate.Text).ToString("MM-dd-yyyy"), txtEstCost.Text, Session["uid"].ToString(), ddlAreaofWork.SelectedItem.Text, ddlWorkActivity.SelectedItem.Text, txtEstquantity1.Text, txtEstquantity2.Text, work_photo, equipment_photo, material_photo, txtLatitude.Text, txtLongitude.Text);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();
                    BtnSave2.Text = "Add New";
                    txtEstquantity1.Text = "";
                    txtEstquantity2.Text = "";
                    txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = "";
                    txtWorkCode.Text = "";
                    txtLongitude.Text = "";
                    txtLatitude.Text = "";
                }
                else
                {
                    fillTrademappingGriddr((DataTable)Session["tmpdrDataTable"], txtWorkName.Text, txtWorkCode.Text, ddlAreaofWork.SelectedValue, ddlWorkActivity.SelectedValue, txtManpower.Text, txtEstDays.Text, txtEstEquipmntReq.Text, txtEstiMaterial.Text, txtSkillLabour.Text, cmf.convertDateIndia(txtWorkStartDate.Text).ToString("MM-dd-yyyy"), cmf.convertDateIndia(txtEndDate.Text).ToString("MM-dd-yyyy"), txtEstCost.Text, Session["uid"].ToString(), ddlAreaofWork.SelectedItem.Text, ddlWorkActivity.SelectedItem.Text, txtEstquantity1.Text, txtEstquantity2.Text, work_photo, equipment_photo, material_photo, txtLatitude.Text, txtLongitude.Text);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();
                    txtEstquantity1.Text = "";
                    txtEstquantity2.Text = "";
                    txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = "";
                    txtWorkCode.Text = "";
                    txtLongitude.Text = "";
                    txtLatitude.Text = "";
                }
            }
        }
        catch (Exception ex)
        {
        }
        finally
        {

        }
        work_photo = "";
        equipment_photo = "";
        material_photo = "";
    }
    protected void BtnClear0_Click1(object sender, EventArgs e)
    {
        txtWorkName.Text = ""; ddlAreaofWork.SelectedIndex = 0; ddlWorkActivity.SelectedIndex = 0; txtManpower.Text = ""; txtEstDays.Text = ""; txtEstEquipmntReq.Text = ""; txtEstiMaterial.Text = ""; txtSkillLabour.Text = ""; txtWorkStartDate.Text = ""; txtEndDate.Text = ""; txtEstCost.Text = "";
    }
    protected void DownloadMaterialRequired(object sender, EventArgs e)
    {
        if (BtnSave1.Text == "Save")
        {
            string projectid = ddlProject.SelectedValue;
            string[] commandArgs = (sender as LinkButton).CommandArgument.Split(new char[] { ',' });
            string filePath = commandArgs[0];
            string workcode = commandArgs[1];
            string MaterialDoc = ("~/WorkProposalDoc/" + projectid + "/" + workcode + "/") + filePath;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(MaterialDoc));
            Response.WriteFile(MaterialDoc);
            Response.End();
        }
        if (BtnSave1.Text == "Update")
        {
            string MaterialDoc = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(MaterialDoc));
            Response.WriteFile(MaterialDoc);
            Response.End();
        }
    }

    protected void DownloadEquipmentRequire(object sender, EventArgs e)
    {
        if (BtnSave1.Text == "Save")
        {
            string projectid = ddlProject.SelectedValue;
            string[] commandArgs = (sender as LinkButton).CommandArgument.Split(new char[] { ',' });
            string filePath = commandArgs[0];
            string workcode = commandArgs[1];
            string equiDoc = ("~/WorkProposalDoc/" + projectid + "/" + workcode + "/") + filePath;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(equiDoc));
            Response.WriteFile(equiDoc);
            Response.End();
        }
        if (BtnSave1.Text == "Update")
        {
            string equiDoc = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(equiDoc));
            Response.WriteFile(equiDoc);
            Response.End();
        }
    }

    protected void DownloadWorkRequire(object sender, EventArgs e)
    {
        if (BtnSave1.Text == "Save")
        {
            string projectid = ddlProject.SelectedValue;
            string[] commandArgs = (sender as LinkButton).CommandArgument.Split(new char[] { ',' });
            string filePath = commandArgs[0];
            string workcode = commandArgs[1];
            string workDoc = ("~/WorkProposalDoc/" + projectid + "/" + workcode + "/") + filePath;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(workDoc));
            Response.WriteFile(workDoc);
            Response.End();
        }
        if (BtnSave1.Text == "Update")
        {
            string workDoc = (sender as LinkButton).CommandArgument;
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(workDoc));
            Response.WriteFile(workDoc);
            Response.End();
        }
    }

    protected void BindImages(string Project, string WorkCode)
    {
        string[] filePaths = Directory.GetFiles(Server.MapPath("~/WorkProposalDoc/" + Project + "/" + WorkCode + "/"));
        List<ListItem> files = new List<ListItem>();
        foreach (string filePath in filePaths)
        {
            string fileName = Path.GetFileName(filePath);

            if (fileName == "WorkPhoto_")
            {
            }
            if (fileName == "EquipmentPhoto_")
            {
            }
            if (fileName == "MaterialPhoto_")
            {
            }
        }
    }
    #endregion

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

    #region Bulk Insert Functions

    private void fillTrademappingGriddr(DataTable tmpTabledr, string WorkName, string WorkCode, string intAreaofWork, string intWorkActivity, string EstManPower, string EstDays, string EstEquipmnts, string EstMaterial, string EstSkilLabour, string EstWorkStartDate, string EstWorkEndDate, string EstCost, string Created_by, string AreaofWork, string WorkActivity, string EquipmntQuantity, string MaterialQuantity, string WorkPhotoStr, string EquipmentPhotoStr, string MaterialPhotoStr, string Latitude, string Longitude)
    {
        dtrdr = tmpTabledr.NewRow();
        dtrdr["intWorkActivityId"] = "0";
        dtrdr["new"] = "0";
        dtrdr["WorkCode"] = WorkCode.Trim();
        dtrdr["WorkName"] = WorkName.Trim();
        dtrdr["intAreaofWork"] = intAreaofWork.Trim();
        dtrdr["intWorkActivity"] = intWorkActivity.Trim();
        dtrdr["EstManPower"] = EstManPower.Trim();
        dtrdr["EstDays"] = EstDays.Trim();
        dtrdr["EstEquipmnts"] = EstEquipmnts.Trim();
        dtrdr["EstMaterial"] = EstMaterial.Trim();
        dtrdr["intWorkid"] = "0";
        dtrdr["EstSkilLabour"] = EstSkilLabour.Trim();
        dtrdr["EstWorkStartDate"] = EstWorkStartDate.Trim();
        dtrdr["EstWorkEndDate"] = EstWorkEndDate.Trim();
        dtrdr["EstCost"] = EstCost.Trim();
        dtrdr["WorkActivity"] = WorkActivity.Trim();
        dtrdr["AreaofWork"] = AreaofWork.Trim();
        dtrdr["EquipmntQuantity"] = EquipmntQuantity.Trim();
        dtrdr["MaterialQuantity"] = MaterialQuantity.Trim();
        dtrdr["WorkPhoto"] = WorkPhotoStr.Trim();
        dtrdr["MaterialsPhoto"] = MaterialPhotoStr.Trim();
        dtrdr["EquipmentPhoto"] = EquipmentPhotoStr.Trim();
        dtrdr["Latitude"] = Latitude.Trim();
        dtrdr["Longitude"] = Longitude.Trim();
        tmpTabledr.Rows.Add(dtrdr);
    }

    protected void GetNewRectoInsertdr()
    {
        myDtNewRecdr = (DataTable)Session["tmpdrDataTable"];
        DataView dvdr = new DataView(myDtNewRecdr);
        dvdr.RowFilter = "new = 0";
        myDtNewRecdr = dvdr.ToTable();
    }

    protected DataTable createtabledr()
    {
        Session["dtMyTabledr"] = new DataTable("drDetails");
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("new", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("WorkName", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("WorkCode", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intAreaofWork", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intWorkActivity", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstManPower", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstDays", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstEquipmnts", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstMaterial", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstSkilLabour", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstWorkStartDate", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstWorkEndDate", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EstCost", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Created_by", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intWorkid", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("AreaofWork", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("WorkActivity", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intWorkActivityId", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EquipmntQuantity", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("MaterialQuantity", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("EquipmentPhoto", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("WorkPhoto", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("MaterialsPhoto", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Latitude", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Longitude", typeof(string));
        return ((DataTable)Session["dtMyTabledr"]);
    }
    #endregion
    #region Dropdown Bindings
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcountiesbyProjectid();
    }

    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");
        }
    }

    void getcountiesbyProjectid()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCountiesbyProjectid(ddlCounties, ddlState.SelectedValue, ddlProject.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");
        }
    }

    void getPayams()
    {
        ddlPayam.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
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
        }
        else
        {
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        getprojectDet();
        getworksbyPojects();
    }

    void getworksbyPojects()
    {
        if (ddlProject.SelectedIndex != 0)
        {
            Gen.getAreaOfWork(ddlAreaofWork);
        }
        else
        {
            ddlAreaofWork.Items.Insert(0, "--Select--");
        }
    }

    void getprojectDet()
    {
        DataSet ds = new DataSet();
        if (ddlProject.SelectedValue != "--Select--")
        {
            ds = Gen.GetProjectDetbyId(ddlProject.SelectedValue);
            lblTargetBenefeciaries.Text = ds.Tables[0].Rows[0]["TargetBeneficiary"].ToString();
            lblCost.Text = ds.Tables[0].Rows[0]["ProjectCost"].ToString();
            if (ds.Tables[1].Rows.Count > 0)
            {
                ddlState.Items.Clear();
                ddlState.DataSource = ds.Tables[1];
                ddlState.DataTextField = "StateName";
                ddlState.DataValueField = "intStateid";
                ddlState.DataBind();
                ddlState.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlState.Items.Clear();
                ddlState.Items.Insert(0, "--Select--");
            }
        }
        else
        {
            lblTargetBenefeciaries.Text = "";
            lblCost.Text = "";
        }
    }

    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIP.SelectedValue != "--Select--")
        {
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
        }
        else
        {
            ddlProject.Items.Clear();
            ddlProject.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlAreasofWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        getWorkActivities();
    }

    void getWorkActivities()
    {

        ddlWorkActivity.Items.Clear();
        if (ddlAreaofWork.SelectedIndex != 0)
        {
            Gen.getWorkActivities(ddlWorkActivity, ddlAreaofWork.SelectedValue);
        }
        else
        {
            ddlWorkActivity.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlstatus.SelectedValue == "Approved")
        {
            Label426.Text = "Approved Date";
        }
        else if (ddlstatus.SelectedValue == "TST-Rejected-")
        {
            Label426.Text = "Rejected Date";
        }
        else
        {
            Label426.Text = "Date";
        }
    }
    #endregion

    #region Mail Sending
    public void SendMailAnother(string anothermail, string Wokcode)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        anothermail = "[email1],[email2]";
        anothermail = System.Configuration.ConfigurationManager.AppSettings["TwoEmailRecipients"];

        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

  
        string Messge = ddlIP.SelectedItem.Text + " has Proposed some works.Please verify it.";

        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - Notification - From IP : " + Session["user_id"].ToString();

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        // hdfTSTName.Value 
        mail.Body = "Dear All," + "<br><br>  <H2>SSSNP Public Works MIS - Works Notification</H2><br> <b> IP NAME: " + ddlIP.SelectedItem.Text + "</b> <br><br> Work Code: " + Wokcode.ToString() + "<br> <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href='" + sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + ddlIP.SelectedItem.Text + "";
        mail.BodyEncoding = System.Text.Encoding.UTF8;

        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);
     
        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
            // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }


    public void SendMailAnotherApprv(string anothermail, string workcode, string Status, string Name)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        DataSet dsIPEmails = Gen.GetUsersDetailsByRole("IP");
        string strEmailList = string.Empty;
        int icountUsers = dsIPEmails.Tables[0].Rows.Count;
        for (int iCounter = 0; iCounter < icountUsers; iCounter++)
        {
            if (iCounter == 0)
            {
                strEmailList = dsIPEmails.Tables[0].Rows[0]["Email"].ToString();
            }
            else
            {
                strEmailList = strEmailList + "," + dsIPEmails.Tables[0].Rows[0]["Email"].ToString();
            }
        }
 
        anothermail = strEmailList;
        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        string Messge = "";
        if (Status.ToString() == "Approved")
        {
            Messge = "Work code " + workcode.ToString() + " has Approved by TST Team. Please verify it.";
        }

        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "SSSNP Public Works MIS - Notification -From TST - " + Session["user_id"].ToString();
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "Dear " + ddlIP.SelectedItem.Text + "<br><br>  <H2>SSSNP Public Works MIS - Works Notification</H2><br> <b> IP NAME: " + ddlIP.SelectedItem.Text + "</b> <br><br> Work Code: " + workcode.ToString() + "<br> <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href='" + sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + Name.ToString() + "";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

        try
        {
            client.Send(mail);
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
        }
    }

    protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    #endregion


    protected void gvWorkProposalAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string sen, sen1, sen2;

            sen2 = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "file_path")) + "/" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")));
            sen1 = sen2.Replace(@"\", @"/");
            string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            sen = sen1.Replace(sssnpdir, "~/");
            sen = sen.Replace(sssnpdir, "~");

            HyperLink1.NavigateUrl = sen.ToString();
            HyperLink1.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName"));
            HyperLink1.Target = "_blank";
        }
    }
}

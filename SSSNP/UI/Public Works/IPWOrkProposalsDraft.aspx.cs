using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Configuration;
using SSSNP.Network;

public partial class IPWOrkProposalsDraft : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
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
            Session["intWorkid"] = "";
            // Look up Click Function Intialization
            string strJSFunctionWithParams = string.Format("javascript:return OpenPopup(\"{0}\")", Session["user_type"].ToString());
            btnOrgLookup0.Attributes.Add("onclick", strJSFunctionWithParams);

            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);

            // User wise Dropdown allotement
            if (Session["user_type"].ToString() == "EnvSpec")
            {
                lblheading.Text = "Work Approvals";
                lblSite2.Text = "Work Approvals";
                lblSitemap.Text = "EnvSpec";

                if (ddlEnvSpecStatus.SelectedValue == "QE-Approved")
                {
                    btnEnvSpecAppRej.Enabled = false;
                    txtComments.Enabled = true;
                }
            }
            else if (Session["user_type"].ToString() == "QualEng")
            {
                lblheading.Text = "Work Review";
                lblSite2.Text = "Work Review";
                lblSitemap.Text = "QualEng";
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

            // Checkbox Binding
            Gen.getCouncilMems(chkTst, "EnvSpec");
            Gen.getCouncilMems(chkTst, "QualEng");
            Gen.getCouncilMems(ChkCA, "CA");
            Gen.getCouncilMems(ChkPDC, "PDC");
            Gen.getCouncilMems(ChkBDC, "BDC");

            Session["dtMyTabledr"] = createtabledr();
            Session["tmpdrDataTable"] = ((DataTable)Session["dtMyTabledr"]);
            #region fill details Calling using Request query string
            if (Request.QueryString.Count > 0)
            {
                if (Session["user_type"].ToString() == "EnvSpec")
                {
                    divEnvSpecApproval.Visible = true;
                    divQualEngApproval.Visible = false;
                }
                if (Session["user_type"].ToString() == "QualEng")
                {
                    divQualEngApproval.Visible = true;
                    divEnvSpecApproval.Visible = false;
                }
                else
                {
                    divEnvSpecApproval.Visible = false;
                    divQualEngApproval.Visible = false;
                }

                trworkcode.Visible = true;
                txtComments.Text = string.Empty;
                Failure.Visible = false;
                success.Visible = false;
                FillDetails();
            }
            #endregion
        }

        #region fill details Calling using Lookup

        if ((hdfId.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            if (Session["user_type"].ToString() == "EnvSpec")
            {
                divEnvSpecApproval.Visible = true;
                divQualEngApproval.Visible = false;
            }
            else if (Session["user_type"].ToString() == "QualEng")
            {
                divEnvSpecApproval.Visible = false;
                divQualEngApproval.Visible = true;
            }
            else
            {
                divEnvSpecApproval.Visible = false;
                divQualEngApproval.Visible = false;
            }

            trworkcode.Visible = true;
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }
        #endregion
    }
    #endregion

    protected void rgWorkComments_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["intWorkid"] != null && Session["intWorkid"].ToString() != string.Empty)
        {
            DataSet ds = Gen.getWorkComments(Session["intWorkId"].ToString(), string.Empty, string.Empty);
            DataTable dt = ds.Tables[0];

            rgWorkComments.DataSource = dt;
        }
    }

    #region Upload Files
    void UploadImage()
    {
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
            ids = hdfId.Value;
        }

        if (Session["intWorkId"] == null || Session["intWorkId"].ToString() == string.Empty)
        {
            Session["intWorkId"] = hdfId.Value;
        }

        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

        ds = Gen.GetWorkProposalByid(ids.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            hdfTSTID.Value = ds.Tables[0].Rows[0]["intTST"].ToString();
            hdfTSTEmail.Value = ds.Tables[0].Rows[0]["TSTemail"].ToString();
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["Ipid"].ToString()).Value;
            ddlIP.Enabled = false;
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["intprjid"].ToString();
            ddlProject.Enabled = false;
            divComments.Visible = true;

            getworksbyPojects();

            if (ds.Tables[0].Rows[0]["status"].ToString() != "")
            {
                if (Session["user_type"].ToString() == "EnvSpec")
                {
                    if (ds.Tables[0].Rows[0]["status"].ToString() == "QE-Approved")
                    {
                        ddlEnvSpecStatus.Items.Remove("TST-Rejected");
                    }
                    ddlEnvSpecStatus.SelectedValue = ddlEnvSpecStatus.Items.FindByValue(ds.Tables[0].Rows[0]["status"].ToString()).Value;
                }
                else if (Session["user_type"].ToString() == "QualEng")
                {
                    if (ds.Tables[0].Rows[0]["status"].ToString() == "IP-Submitted")
                    {
                        ddlQualEngStatus.Items.Remove("ES-Rejected");
                    }
                    ddlQualEngStatus.SelectedValue = ddlQualEngStatus.Items.FindByValue(ds.Tables[0].Rows[0]["status"].ToString()).Value;
                }
            }
            if (ddlEnvSpecStatus.SelectedValue == "ES-Approved")
            {

            }
            else if (ddlEnvSpecStatus.SelectedValue == "QE-Rejected")
            {
                btnEnvSpecAppRej.Enabled = true;
                ddlEnvSpecStatus.Enabled = false;
            }
            else if (ddlEnvSpecStatus.SelectedValue == "QE-Approved")
            {
                btnEnvSpecAppRej.Enabled = false;
                ddlEnvSpecStatus.Enabled = true;
            }
            else
            {
                btnEnvSpecAppRej.Enabled = true;
                ddlEnvSpecStatus.Enabled = true;
            }

            if (ddlQualEngStatus.SelectedValue == "IP-Submitted")
            {
                btnQualEngAppRej.Enabled = false;
                txtComments.Enabled = false;
            }
            else if (ddlQualEngStatus.SelectedValue == "ES-Rejected")
            {
                btnQualEngAppRej.Enabled = true;
                txtComments.Enabled = true;
            }
            else if (ddlQualEngStatus.SelectedValue == "QE-Approved")
            {
                btnQualEngAppRej.Enabled = true;
                txtComments.Enabled = true;
            }

            //Disable updates for status not compatible for being changed by the role
            if (Session["user_type"].ToString() == "QualEng"
                && ddlQualEngStatus.SelectedValue != "ES-Rejected"
                && ddlQualEngStatus.SelectedValue == "IP-Submitted")
            {
                txtComments.Enabled = false;
                btnQualEngAppRej.Enabled = false;
            }
            if (Session["user_type"].ToString() == "QualEng"
                && ddlEnvSpecStatus.SelectedValue != "QE-Approved"
                && ddlEnvSpecStatus.SelectedValue != "TST-Rejected"
                )
            {
                txtComments.Enabled = false;
                btnEnvSpecAppRej.Enabled = false;
            }


            if (ds.Tables[0].Rows[0]["TypeofMeeting"].ToString() != "")
                ddlMeeting.SelectedValue = ds.Tables[0].Rows[0]["TypeofMeeting"].ToString();
            ddlMeeting.Enabled = false;

            getprojectDet();
            txtStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["MeetingDate"].ToString()).ToString("dd-MM-yyyy");
            txtStartDate.Enabled = false;
            if (ds.Tables[0].Rows[0]["intStateid"].ToString() != "")
            {
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["intStateid"].ToString();
                ddlState.Enabled = false;
                getcountiesbyProjectid();
            }
            if (ds.Tables[0].Rows[0]["intCountieid"].ToString() != "")
            {
                ddlCounties.SelectedValue = ds.Tables[0].Rows[0]["intCountieid"].ToString();
                ddlCounties.Enabled = false;
                getPayams();
            }
            if (ds.Tables[0].Rows[0]["intPayamid"].ToString() != "")
            {
                ddlPayam.SelectedValue = ds.Tables[0].Rows[0]["intPayamid"].ToString();
                ddlPayam.Enabled = false;
                getBomas();
            }
            if (ds.Tables[0].Rows[0]["intBomasid"].ToString() != "")
                ddlBoma.SelectedValue = ds.Tables[0].Rows[0]["intBomasid"].ToString();
            ddlBoma.Enabled = false;
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
            } catch (Exception e)
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

    protected void gvpractical0_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int rowval;
            rowval = gvpractical0.SelectedIndex;
            hdfRowIndex.Value = rowval.ToString();
            hdn_work_id.Value = gvpractical0.SelectedDataKey.Values["intWorkid"].ToString();
            txtWorkCode.Text = gvpractical0.SelectedDataKey.Values["WorkCode"].ToString();
            txtWorkCode.Enabled = false;
            txtWorkName.Text = gvpractical0.SelectedDataKey.Values["WorkName"].ToString();
            txtWorkName.Enabled = false;
            ddlAreaofWork.SelectedValue = gvpractical0.SelectedDataKey.Values["intAreaofWork"].ToString();
            ddlAreaofWork.Enabled = false;
            getWorkActivities();
            ddlWorkActivity.SelectedValue = gvpractical0.SelectedDataKey.Values["intWorkActivity"].ToString();
            ddlWorkActivity.Enabled = false;
            txtManpower.Text = gvpractical0.SelectedDataKey.Values["EstManPower"].ToString();
            txtManpower.Enabled = false;
            txtEstDays.Text = gvpractical0.SelectedDataKey.Values["EstDays"].ToString();
            txtEstDays.Enabled = false;
            txtEstEquipmntReq.Text = gvpractical0.SelectedDataKey.Values["EstEquipmnts"].ToString();
            txtEstiMaterial.Text = gvpractical0.SelectedDataKey.Values["EstMaterial"].ToString();
            txtSkillLabour.Text = gvpractical0.SelectedDataKey.Values["EstSkilLabour"].ToString();
            txtSkillLabour.Enabled = false;
            txtWorkStartDate.Text = Convert.ToDateTime(gvpractical0.SelectedDataKey.Values["EstWorkStartDate"].ToString()).ToString("dd-MM-yyyy");
            txtWorkStartDate.Enabled = false;
            txtEndDate.Text = Convert.ToDateTime(gvpractical0.SelectedDataKey.Values["EstWorkEndDate"].ToString()).ToString("dd-MM-yyyy");
            txtEndDate.Enabled = false;
            txtEstCost.Text = gvpractical0.SelectedDataKey.Values["EstCost"].ToString();
            txtEstCost.Enabled = false;
            txtEstquantity1.Text = gvpractical0.SelectedDataKey.Values["EquipmntQuantity"].ToString();
            txtEstquantity1.Enabled = false;
            txtEstquantity2.Text = gvpractical0.SelectedDataKey.Values["MaterialQuantity"].ToString();
            txtEstquantity2.Enabled = false;
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
        divEnvSpecApproval.Visible = false;
        divQualEngApproval.Visible = false;
        txtLatitude.Text = "";
        txtLongitude.Text = "";
        ddlEnvSpecStatus.SelectedIndex = 0;
        ddlQualEngStatus.SelectedIndex = 0;
        Gen.getCouncilMems(chkTst, "EnvSpec");
        Gen.getCouncilMems(chkTst, "QualEng");
        Gen.getCouncilMems(ChkCA, "CA");
        Gen.getCouncilMems(ChkPDC, "PDC");
        Gen.getCouncilMems(ChkBDC, "BDC");
        ddlProject.SelectedIndex = 0;
        lblworkcode.Text = "";
        lblTargetBenefeciaries.Text = "";
        lblCost.Text = "";
        //BtnSave1.Text = "Save";
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
        ((DataTable)Session["tmpdrDataTable"]).Clear();
        gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
        gvpractical0.DataBind();
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

    protected void ddlEnvSpecStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlEnvSpecStatus.SelectedValue == "QE-Approved")
        {
            btnEnvSpecAppRej.Enabled = false;
            txtComments.Enabled = false;
        }
        else if (ddlEnvSpecStatus.SelectedValue == "TST-Rejected")
        {
            btnEnvSpecAppRej.Enabled = false;
            txtComments.Enabled = false;
        }
        else if (ddlEnvSpecStatus.SelectedValue == "ES-Rejected")
        {
            btnEnvSpecAppRej.Enabled = true;
            txtComments.Enabled = true;
        }
        else if (ddlEnvSpecStatus.SelectedValue == "ES-Approved")
        {
            btnEnvSpecAppRej.Enabled = true;
            txtComments.Enabled = true;
        }
    }

    protected void ddlQualEngStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlQualEngStatus.SelectedValue == "IP-Submitted")
        {
            btnQualEngAppRej.Enabled = false;
            txtComments.Enabled = false;
        }
        else if (ddlQualEngStatus.SelectedValue == "QE-Rejected")
        {
            btnQualEngAppRej.Enabled = true;
            txtComments.Enabled = true;
        }
        else if (ddlQualEngStatus.SelectedValue == "QE-Approved")
        {
            btnQualEngAppRej.Enabled = true;
            txtComments.Enabled = true;
        }
        else if (ddlQualEngStatus.SelectedValue == "ES-Rejected")
        {
            btnQualEngAppRej.Enabled = false;
            txtComments.Enabled = false;
        }
    }
    #endregion

    #region Mail Sending

    protected void ddlMeeting_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    private void sendApprovalEmailToEnvSpecByQualEng(string workId)
    {
        try
        {
            string emailSubject = "Sub-project Approval";
            string emailBody = "<H2>Dear Environment Specialist (SNSOP MIS),</H2> <br><br>" +
                "A sub-project has been approved by Quality Engineer and needs your approval to move forward. Please review.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmailToUserType(emailSubject, emailBody, "EnvSpec");
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending email to Env Spec. Error details:\n" + ex.ToString());
            return;
        }
    }

    private void sendRejectionEmailToIpByQualEng(string ipMail, string workId)
    {
        try
        {
            string emailSubject = "Sub-project Rejection";
            string emailBody = "<H2>Dear Implementing Partner (SNSOP MIS),</H2> <br><br>" +
                "A sub-project that you created has been rejected by Quality Engineer. Please review.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmail(ipMail, emailSubject, emailBody);
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending email to Quality Engineer. Error details:\n" + ex.ToString());
            return;
        }
    }

    private void sendApprovalEmailToTstByEnvSpec(string workId)
    {
        try
        {
            string emailSubject = "Sub-project Approval";
            string emailBody = "<H2>Dear Technical Support Team member (SNSOP MIS),</H2> <br><br>" +
                "A new sub-project has been approved by Environment Specialist and needs your approval to move forward. Please review.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmailToUserType(emailSubject, emailBody, "TST");
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending email to TST. Error details:\n" + ex.ToString());
            return;
        }
    }

    private void sendRejectionEmailToQualEngByEnvSpec(string workId)
    {
        try
        {
            string emailSubject = "Sub-project Rejection";
            string emailBody = "<H2>Dear Quality Engineer (SNSOP MIS),</H2> <br><br>" +
                "A sub-project has been rejected by Environment Specialist. Please review.<br>";

            emailBody += ConfigurationManager.AppSettings["SSSNPWEB"].ToString();
            emailBody += "<br>Please login by clicking the above link<br>Thanks and regards.";

            emailService.sendEmailToUserType(emailSubject, emailBody, "QualEng");
        }
        catch (Exception ex)
        {
            Console.WriteLine("There was an unexpexted error when sending email to Quality Engineer. Error details:\n" + ex.ToString());
            return;
        }
    }

    protected void btnAppRej_Click(object sender, EventArgs e)
    {
        Failure.Visible = false;

        if (
            (Session["user_type"].ToString() == "QualEng" && ddlEnvSpecStatus.SelectedValue != "IP-Submitted") ||
            (Session["user_type"].ToString() == "EnvSpec" && ddlEnvSpecStatus.SelectedValue != "QE-Approved")
            )
        {
            //save new status
            try
            {
                string strSelectedStatus = string.Empty;
                //If rejected, it stores the comments
                //if (
                //    (ddlQualEngStatus.SelectedValue == "QE-Rejected") ||
                //    (ddlEnvSpecStatus.SelectedValue == "ES-Rejected"))
                //{
                if (txtComments.Text == string.Empty
                   &&
                   ((ddlQualEngStatus.SelectedValue == "QE-Rejected") ||
                   (ddlEnvSpecStatus.SelectedValue == "ES-Rejected"))
                   )
                {
                    lblmsg0.Text = "Please add a comment for rejection";
                    lblmsg0.Visible = true;
                    Failure.Visible = true;
                }
                else
                {
                    if (Session["user_type"].ToString() == "EnvSpec")
                    {
                        Gen.UpdateSubProjectDraft(Session["intWorkId"].ToString(), ddlEnvSpecStatus.SelectedValue, Session["uid"].ToString());
                        Gen.insertWorkComment(Session["intWorkId"].ToString(), txtComments.Text, ddlEnvSpecStatus.SelectedValue, Session["uid"].ToString());
                        strSelectedStatus = ddlEnvSpecStatus.SelectedValue;
                    }
                    else if (Session["user_type"].ToString() == "QualEng")
                    {
                        Gen.UpdateSubProjectDraft(Session["intWorkId"].ToString(), ddlQualEngStatus.SelectedValue, Session["uid"].ToString());
                        Gen.insertWorkComment(Session["intWorkId"].ToString(), txtComments.Text, ddlQualEngStatus.SelectedValue, Session["uid"].ToString());
                    }
                    if (fuWorkProposalAttachment.HasFile)
                    {
                        string strWork_Photo_Name = "WorkProposalAttachment_";
                        string strWorkProposalAttachmentFileName = fuWorkProposalAttachment.FileName;
                        Gen.InsertAttachements_Sp("WorkProposalAttachment", "WorkProposalAttachment", strWork_Photo_Name, Session["uid"].ToString(), strWorkProposalAttachmentFileName, hdn_work_activity_id.Value);
                        string WorkPhotoStr = ("~/WorkProposalAttachment/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName;
                        fuWorkProposalAttachment.SaveAs(Server.MapPath("~/WorkProposalAttachment/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName);
                        strSelectedStatus = ddlQualEngStatus.SelectedValue;
                    }
                    lblmsg.Text = "Sub-project Saved as: " + strSelectedStatus;
                    lblmsg.Visible = true;
                    success.Visible = true;
                }



                //}
                //else
                //{
                //    if (Session["user_type"].ToString() == "EnvSpec")
                //    {
                //        Gen.UpdateSubProjectDraft(Session["intWorkId"].ToString(), ddlEnvSpecStatus.SelectedValue, Session["uid"].ToString());
                //        strSelectedStatus = ddlEnvSpecStatus.SelectedValue;
                //    }
                //    else if (Session["user_type"].ToString() == "QualEng")
                //    {
                //        Gen.UpdateSubProjectDraft(Session["intWorkId"].ToString(), ddlQualEngStatus.SelectedValue, Session["uid"].ToString());
                //        strSelectedStatus = ddlQualEngStatus.SelectedValue;
                //    }

                //    if (fuWorkProposalAttachment.HasFile)
                //    {
                //        string strWork_Photo_Name = "WorkProposalAttachment_";
                //        string strWorkProposalAttachmentFileName = fuWorkProposalAttachment.FileName;
                //        Gen.InsertAttachements_Sp("WorkProposalAttachment", "WorkProposalAttachment", strWork_Photo_Name, Session["uid"].ToString(), strWorkProposalAttachmentFileName, hdn_work_activity_id.Value);
                //        string WorkPhotoStr = ("~/WorkProposalAttachment/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName;
                //        fuWorkProposalAttachment.SaveAs(Server.MapPath("~/WorkProposalAttachment/" + ddlProject.SelectedValue + "/" + txtWorkCode.Text + "/") + strWorkProposalAttachmentFileName);
                //    }
                //    lblmsg.Text = "Sub-project Saved as: " + strSelectedStatus;
                //    lblmsg.Visible = true;
                //    success.Visible = true;
                //}
            }
            catch (Exception ex)
            {
                lblmsg.Text = "An Error Occured. Please contact Support" + ex.Message;
            }


            if (Session["user_type"].ToString() == "QualEng")
            {
                if (ddlQualEngStatus.SelectedValue == "QE-Rejected")
                {
                    //send email to the IP that created the Work Proposal
                    DataSet dsWorkProposal = Gen.GetWorkProposalCreatorByIntWorkId(Session["intWorkId"].ToString());
                    string strCreatroEmailAddr = dsWorkProposal.Tables[0].Rows[0]["Email"].ToString();

                    sendRejectionEmailToIpByQualEng(strCreatroEmailAddr, Session["intWorkId"].ToString());

                    //Gen.SendMail(strCreatroEmailAddr, "SNSOP Notifications - Rowk Proposal Status", string.Format("Notification <br>: the Work propossal with ID {0} you created has been rejected by the PMU QUality Engineer. <br> Please check and review it according to comments provided.", Session["intWorkId"].ToString()));
                }
                else if (ddlQualEngStatus.SelectedValue == "QE-Approved")
                {
                    sendApprovalEmailToEnvSpecByQualEng(Session["intWorkId"].ToString());
                    //DataSet dsEnvironmentSpecialistEmails = Gen.GetUsersDetailsByRole("EnvSpec");
                    //string strEmailList = string.Empty;
                    //int icountUsers = dsEnvironmentSpecialistEmails.Tables[0].Rows.Count;
                    //for (int iCounter = 0; iCounter < icountUsers; iCounter++)
                    //{
                    //    if (iCounter == 0)
                    //    {
                    //        strEmailList = dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    //    }
                    //    else
                    //    {
                    //        strEmailList = strEmailList + "," + dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    //    }
                    //}
                    //Gen.SendMail(strEmailList, "SNSOP Notifications - Work Proposal Status", string.Format("Notification <br>: the Work propossal with ID {0} has been reviewed by the PMU QUality Engineer. <br> Please check and review it.", Session["intWorkId"].ToString()));
                }

            }
            else if (Session["user_type"].ToString() == "EnvSpec")
            {
                if (ddlEnvSpecStatus.SelectedValue == "ES-Rejected")
                {
                    sendRejectionEmailToQualEngByEnvSpec(Session["intWorkId"].ToString());
                    //DataSet dsEnvironmentSpecialistEmails = Gen.GetUsersDetailsByRole("QualEng");
                    //string strEmailList = string.Empty;
                    //int icountUsers = dsEnvironmentSpecialistEmails.Tables[0].Rows.Count;
                    //for (int iCounter = 0; iCounter < icountUsers; iCounter++)
                    //{
                    //    if (iCounter == 0)
                    //    {
                    //        strEmailList = dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    //    }
                    //    else
                    //    {
                    //        strEmailList = strEmailList + "," + dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    //    }
                    //}
                    //Gen.SendMail(strEmailList, "SNSOP Notifications - Work Proposal Status", string.Format("Notification <br>: the Work proposal with ID {0} has been rejected by the Environment Specialist. <br> Please check and review it according to comments provided.", Session["intWorkId"].ToString()));
                }
                else if (ddlEnvSpecStatus.SelectedValue == "ES-Approved")
                {
                    sendApprovalEmailToTstByEnvSpec(Session["intWorkId"].ToString());
                    //DataSet dsEnvironmentSpecialistEmails = Gen.GetUsersDetailsByRole("TST");
                    //string strEmailList = string.Empty;
                    //int icountUsers = dsEnvironmentSpecialistEmails.Tables[0].Rows.Count;
                    //for (int iCounter = 0; iCounter < icountUsers; iCounter++)
                    //{
                    //    if (iCounter == 0)
                    //    {
                    //        strEmailList = dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    //    }
                    //    else
                    //    {
                    //        strEmailList = strEmailList + "," + dsEnvironmentSpecialistEmails.Tables[0].Rows[0]["Email"].ToString();
                    //    }
                    //}
                    //Gen.SendMail(strEmailList, "SNSOP Notifications - Work Proposal Status", string.Format("Notification <br>: the Work proposal with ID {0} has been reviewd by the Environment Specialist. <br> Please check and review it.", Session["intWorkId"].ToString()));
                }
            }

        }
    }

    protected void gvWorkProposalAttachments_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string sen, sen1, sen2;

            sen2 = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "File_Path")) + "/" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")));
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

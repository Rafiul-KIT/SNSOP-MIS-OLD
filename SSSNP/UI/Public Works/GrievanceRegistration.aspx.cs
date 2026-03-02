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
using System.IO;
using System.Net.Mail;
using System.Web.Configuration;
#endregion


#region Amendment History Begin
//Form Name : Grievance Registration
//Purpose : To Register Grievance
//Tables used : tm_States,tm_Counties,tm_Payams,tm_Bomas,tbl_Grievance,tbl_Users
//Stored procedures Used : sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,InsUpdGrievance,getGrievanceDetails
#endregion Amendment History End

public partial class GrievanceRegistration : System.Web.UI.Page
{

    #region reference class calling and Variable declaration
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";
    DataRow dtrdr;

    DataTable myDtNewRecdr = new DataTable();
    string path = WebConfigurationManager.AppSettings["AppPath"].ToString();

    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {

        // Look up Click Function Intialization
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        #region Is not Postback Statement
        if (!IsPostBack)
        {

            Gen.getStates(ddlState);

            Session["DocumentName"] = "";
            Session["Documentpath"] = "";

        }
        #endregion

        #region Doc or Image Function Calling
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileUpload1.PostedFile))
        {
            FileUploadphoto1();
        }
        #endregion
        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {

            Failure.Visible = false;
            success.Visible = false;
            FillDetails();

            BtnSave1.Text = "Update";
        }
        #endregion

    }

    #endregion

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
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
    #endregion

    #region Doc or Image Binding in Session While File Upload
    void FileUploadphoto1()
    {
        string newPath = "";
        string sFileDir = Server.MapPath("~\\Grievance");
        string filename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);


        try
        {

            string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;
            filename = fileType[0] + "_" + System.DateTime.Now.ToString("ddMMyyyyhhmmss") + "." + fileType[1];

            if (fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG" ||
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "PDF" ||
                fileType[letterI - 1].ToUpper().Trim() == "DOC" ||
                fileType[letterI - 1].ToUpper().Trim() == "DOCX" ||
                fileType[letterI - 1].ToUpper().Trim() == "XLS" ||
                fileType[letterI - 1].ToUpper().Trim() == "XLSX")
            {
                newPath = System.IO.Path.Combine(sFileDir, Session["uid"].ToString());
                if (!Directory.Exists(newPath))

                    System.IO.Directory.CreateDirectory(newPath);
                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                int count = dir.GetFiles().Length;
                if (count == 0)
                    FileUpload1.PostedFile.SaveAs(newPath + "\\" + filename);
                else
                {
                    if (count == 1)
                    {
                        string[] Files = Directory.GetFiles(newPath);

                        foreach (string file in Files)
                        {
                            File.Delete(file);
                        }
                        FileUpload1.PostedFile.SaveAs(newPath + "\\" + filename);
                    }
                }
                hypDocument.Text = filename;
                string pPath = newPath;
                string vPath = pPath.Replace(path, "~").Replace(@"\", "/");
                hypDocument.Target = "_blank";
                hypDocument.NavigateUrl = vPath + "\\" + filename;
                Session["DocumentName"] = filename;
                Session["Documentpath"] = newPath;
            }
            else
            {
                Failure.Visible = true;
                success.Visible = false;
                lblmsg0.Text = "Upload JPG,JPEG,PNG, PDF,DOC,DOCX,XlS and XLSX files only..!";
            }

        }
        catch (Exception ex)
        {
            Failure.Visible = true;
            success.Visible = false;
            lblmsg0.Text = "An Error Occured. Please Try Again!" + ex.Message;
        }

        finally
        {

        }


    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "";
        DataSet ds = new DataSet();


        ds = Gen.getGrievanceDetails(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            hdfID.Value = ds.Tables[0].Rows[0]["Grievance_RegistrationNo"].ToString();
            lblGrievance_RegistrationNo.Text = "Grievance Registration No :" + ds.Tables[0].Rows[0]["Grievance_RegistrationNo"].ToString();
            lblGrievance_RegistrationNo.Visible = true;
            if (ds.Tables[0].Rows[0]["intStateid"].ToString() != "")
            {
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["intStateid"].ToString();
                getcounties();
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
            {
                ddlBoma.SelectedValue = ds.Tables[0].Rows[0]["intBomasid"].ToString();

            }
            txtComplainant_Name.Text = ds.Tables[0].Rows[0]["Complainant_Name"].ToString();
            txtTypeOfComplaints.Text = ds.Tables[0].Rows[0]["TypeOfComplaints"].ToString();
            txtComplainant_EnrolmentNo.Text = ds.Tables[0].Rows[0]["Complainant_EnrolmentNo"].ToString();
            txtComplainant_EnrolmentNo.Text = ds.Tables[0].Rows[0]["Complainant_EnrolmentNo"].ToString();
            txtDetailsComplainsReported.Text = ds.Tables[0].Rows[0]["AddressedDate"].ToString(); if (ds.Tables[0].Rows[0]["AddressedDate"].ToString() != "")
            {
                txtAddressedDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["AddressedDate"].ToString()).ToString("dd-MM-yyyy");
            } 
            txtAddressedHow.Text = ds.Tables[0].Rows[0]["AddressedHow"].ToString();
            if (ds.Tables[0].Rows[0]["ComplaintRegisteredDate"].ToString() != "")
            {
                txtComplaintRegisteredDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["ComplaintRegisteredDate"].ToString()).ToString("dd-MM-yyyy");
            }

            txtAccused_Name.Text = ds.Tables[0].Rows[0]["Accused_Name"].ToString();
            txtAccused_EnrolmentNo.Text = ds.Tables[0].Rows[0]["Accused_EnrolmentNo"].ToString();
            txtAccused_Complaints_Specification.Text = ds.Tables[0].Rows[0]["Accused_Complaints_Specification"].ToString();
            txtAccused_ComplainsReferred.Text = ds.Tables[0].Rows[0]["Accused_ComplainsReferred"].ToString();
            //lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();


            if (ds.Tables[0].Rows[0]["DocumentPath"].ToString().Trim() != "" || ds.Tables[0].Rows[0]["DocumentName"].ToString().Trim() != null)
            {
                string pPath = ds.Tables[0].Rows[0]["DocumentPath"].ToString();
                string vPath = pPath.Replace(path, "~").Replace(@"\", "/");
                hypDocument.Text = ds.Tables[0].Rows[0]["DocumentName"].ToString();
                hypDocument.Target = "_blank";
                hypDocument.NavigateUrl = vPath + "\\" + ds.Tables[0].Rows[0]["DocumentName"].ToString();

                Session["DocumentName"] = ds.Tables[0].Rows[0]["DocumentName"].ToString();
                Session["Documentpath"] = ds.Tables[0].Rows[0]["DocumentPath"].ToString();

            }

            BtnSave1.Text = "Update";






        }
    }
    #endregion
    #region Button Click functionalities
    protected void BtnSave_Click(object sender, EventArgs e)
    {


        string i = Gen.InsUpdGrievance_Registration(hdfID.Value.ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtComplainant_Name.Text, txtComplainant_EnrolmentNo.Text, txtTypeOfComplaints.Text, txtDetailsComplainsReported.Text, txtComplaintRegisteredDate.Text, txtAccused_Name.Text, txtAccused_EnrolmentNo.Text, txtAccused_Complaints_Specification.Text, txtAccused_ComplainsReferred.Text, Session["Documentpath"].ToString(), Session["DocumentName"].ToString(),txtAddressedHow.Text,txtAddressedDate.Text, Session["uid"].ToString());

        lblmsg.Text = "Submited Successfully..! Grievance Registration No is " + i;
        success.Visible = true;
        Failure.Visible = false;
        clear();
        //Response.Redirect("WorkProcessResult.aspx");
    }

    void clear()
    {
        hdfID.Value = "";
        lblGrievance_RegistrationNo.Visible = false;
        txtComplainant_Name.Text = "";
        txtTypeOfComplaints.Text = "";
        txtComplainant_EnrolmentNo.Text = "";
        txtDetailsComplainsReported.Text = "";
        hypDocument.Text = null;
        Session["DocumentName"] = null;
        Session["Documentpath"] = null;
        txtAddressedDate.Text = null;
        txtAddressedHow.Text = null;
        txtComplaintRegisteredDate.Text = "";
        txtAccused_Name.Text = "";
        txtAccused_EnrolmentNo.Text = "";
        txtAccused_Complaints_Specification.Text = "";
        txtAccused_ComplainsReferred.Text = "";

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
    }

    protected void BtnClear0_Click(object sender, EventArgs e)
    {
        BtnSave1.Text = "Submit";
        success.Visible = false;
        Failure.Visible = false;
        lblGrievance_RegistrationNo.Visible = false;
        txtComplainant_Name.Text = "";
        txtTypeOfComplaints.Text = "";
        txtComplainant_EnrolmentNo.Text = "";
        txtDetailsComplainsReported.Text = "";

        hypDocument.Text = null;
        Session["DocumentName"] = null;
        Session["Documentpath"] = null;
        txtAddressedDate.Text = null;
        txtAddressedHow.Text = null;
        txtComplaintRegisteredDate.Text = "";
        txtAccused_Name.Text = "";
        txtAccused_EnrolmentNo.Text = "";
        txtAccused_Complaints_Specification.Text = "";
        txtAccused_ComplainsReferred.Text = "";

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
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Gridview Events
    protected void grdDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {



    }



    protected void grdAttachments_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

}

#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Web.UI.HtmlControls;
#endregion

#region Amendment History Begin
//
//Form Name   : Beneficiary Registration
//Purpose     : To Add Beneficiary Details
//Tables used : td_IPDetails, tm_Counties, tm_States,tm_Payams,tm_Bomas,tbl_BeneficiaryDet,tbl_BeneficiaryDetTrans,tr_BenfWorkDetTrans,tr_BenfWorkDet,tr_BenAttendanceDet,tr_BenWagesPayment,GroupMaster
//Stored procedures Used : InsUpdBenDetails,sp_getCounties,sp_getPayams,getBomasbyID,getIpDet,sp_getStates,getVulnerbility,CountBeneficiaryType,GetGroupStrength,UpdateAlternateDetails,GetEnrollmentNoByintBenid,CountBeneficiaryTypeInUpdate,DeleteBenficiarynew
// 
#endregion Amendment History End


public partial class IPBeneficiaryRegistration : System.Web.UI.Page
{


    #region reference class calling and Variable declaration
    General Gen = new General();

    byte[] Photo = new byte[1];
    string PhotoFileName = "";
    #endregion

    private bool IsIPRole
    {
        get
        {
            return (Session["user_type"].ToString() == "IP") ? true : false;

        }
    }

    private bool IsTSTRole
    {
        get
        {
            return (Session["user_type"].ToString() == "TST") ? true : false;

        }
    }

    protected static Control FindControlRecursive(Control container, string controlID)
    {
        if (controlID == null || string.IsNullOrEmpty(controlID)) return null;

        if ((container.ID != null) && (container.ID.Equals(controlID)))
            return container;

        foreach (Control ctrl in container.Controls)
        {
            Control foundCtrl = FindControlRecursive(ctrl, controlID);
            if (foundCtrl != null)
                return foundCtrl;
        }
        return null;
    }

    private int UserID
    {
        get
        {
            int result = 0;
            if (Session["uid"] == null) return 0;
            int.TryParse(Session["uid"].ToString(), out result);
            return result;

        }
    }
    private void EnableIPControl()
    {
        txtHeadName.Enabled = true;
        ddlSex.Enabled = true;
        ddlAge.Enabled = true;
        btnSaveCustom.Visible = true;
    }
    private void EnableTSTControl()
    {
        //txtSelectionCriteria.Enabled = true;
        ddlComponent.Enabled = true;
        txtSelectionReason.Enabled = true;
        txtFamilySize.Enabled = true;
        btnSaveCustom.Visible = true;
    }
    private void DisableControl()
    {
        btnSaveCustom.Visible = false;
        ddlBeneficiary.Enabled = false;
        ddlAge.Enabled = false;
        ddlBoma.Enabled = false;
        ddlCounties.Enabled = false;
        ddldependents.Enabled = false;
        ddlDisability.Enabled = false;
        ddlGroupList.Enabled = false;
        ddlIP.Enabled = false;
        ddlIsdisability.Enabled = false;
        ddlLegalStatus.Enabled = false;
        ddlNoofAdults.Enabled = false;
        ddlNumberAdultsAgeAbove65.Enabled = false;
        ddlNumberAdultsAgeBetween18_30.Enabled = false;
        ddlNumberAdultsAgeBetween30_45.Enabled = false;
        ddlNumberAdultsAgeBetween45_64.Enabled = false;
        ddlNumberChildrenAgeAbove12years.Enabled = false;
        ddlNumberChildrenAgeAbove18years.Enabled = false;
        ddlNumberChildrenAgeAbove5years.Enabled = false;
        ddlNumberFemaleAdults.Enabled = false;
        ddlNumberFemaleChildren.Enabled = false;
        ddlNumberMaleAdults.Enabled = false;
        ddlNumberMaleChildren.Enabled = false;
        ddlPayam.Enabled = false;
        ddlSex.Enabled = false;
        ddlState.Enabled = false;
        DdlA1Age.Enabled = false;
        DdlA1Gender.Enabled = false;
        DdlA2Age.Enabled = false;
        DdlA2Gender.Enabled = false;
        txtAccNo.Enabled = false;
        txtAddress.Enabled = false;
        txtBankName.Enabled = false;
        txtBiometric.Enabled = false;
        txtContactNo.Enabled = false;
        txtEmail.Enabled = false;
        txtEthnicityTribeRgin.Enabled = false;
        txtFamilySize.Enabled = false;
        txtHeadName.Enabled = false;
        txtHomePhone.Enabled = false;
        txtIncome.Enabled = false;
        txtName1stAlternatePayee.Enabled = false;
        txtName2ndAlternatePayee.Enabled = false;
        txtNationalID.Enabled = false;
        txtReAccNo.Enabled = false;
        //txtSelectionCriteria.Enabled = false;
        //ddlComponent.Enabled = false;
        txtSelectionReason.Enabled = false;
        txtSourceIncome.Enabled = false;
        txtSpouse.Enabled = false;
        TxtA1ContactNo.Enabled = false;
        TxtA1NationalID.Enabled = false;
        TxtA2ContactNo.Enabled = false;
        TxtA2NationalID.Enabled = false;
        FileUpload1.Enabled = false;
        FileUpload2.Enabled = false;
        FileUpload3.Enabled = false;

    }


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
        #region Is not Postback Statements
        if (!IsPostBack)
        {
            Session["Photo"] = "";
            Session["PhotoFileName"] = "";
            Session["Photo1"] = "";
            Session["PhotoFileName1"] = "";
            Session["Photo2"] = "";
            Session["PhotoFileName2"] = "";

            Gen.getVulnerbility(ddlDisability);
            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
            if (Session["user_type"].ToString() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString()).Value;
                ddlIP.Enabled = false;
            }

            for (int i = 5; i <= 100; i++)
            {

                ddlAge.Items.Add(i.ToString());

            }
            for (int i = 5; i <= 100; i++)
            {

                DdlA1Age.Items.Add(i.ToString());

            }
            for (int i = 5; i <= 100; i++)
            {
                DdlA2Age.Items.Add(i.ToString());

            }


            if (FileUpload1.HasFile)
            {
                Session["FileUpload11"] = FileUpload1;
                hdfUploadFile1.Value = FileUpload1.FileName;
            }
            else if (Session["FileUpload11"] != null)
            {
                FileUpload1 = (FileUpload)Session["FileUpload11"];
                hdfUploadFile1.Value = FileUpload1.FileName;
            }


            if (FileUpload2.HasFile)
            {
                Session["FileUpload12"] = FileUpload2;
                hdfUploadFile2.Value = FileUpload2.FileName;
            }
            else if (Session["FileUpload12"] != null)
            {
                FileUpload2 = (FileUpload)Session["FileUpload12"];
                hdfUploadFile2.Value = FileUpload2.FileName;
            }


            if (FileUpload3.HasFile)
            {
                Session["FileUpload13"] = FileUpload3;
                hdfUploadFile3.Value = FileUpload3.FileName;
            }
            else if (Session["FileUpload13"] != null)
            {
                FileUpload3 = (FileUpload)Session["FileUpload13"];
                hdfUploadFile3.Value = FileUpload3.FileName;
            }

            if (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "TST")
            {
                trlookup.Visible = false;
                BtnSave1.Visible = false;
                BtnClear.Visible = false;
            }

        }
        #endregion

        //Upload Documents Calling
        UploadFile4();
        UploadFile5();
        UploadFile6();


        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            FillDetails();
            if (Session["userlevel"].ToString() == "1" || (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "TST"))
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


    #region Upload Files Functions

    //Beneficiary Photo
    public void UploadFile4()
    {

        string PhotoFilePath = "";
        string PhotoFileName = "";

        string sFileDir4 = Server.MapPath("~\\Photo");

        if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload1.PostedFile))
        {


            //determine file name
            string sFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            try
            {

                string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
                int i = fileType.Length;
                if (fileType[i - 1].ToUpper().Trim() == "GIF" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG" || fileType[i - 1].ToUpper().Trim() == "PNG")
                {
                    string newPath = "";
                    if (Session.Count > 0)
                    {
                        newPath = System.IO.Path.Combine(sFileDir4, Session["uid"].ToString());
                    }
                    else
                    {
                        newPath = System.IO.Path.Combine(sFileDir4, "1000");
                    }

                    newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                    PhotoFilePath = newPath.ToString();

                    PhotoFileName = sFileName;
                    // Create the subfolder
                    if (!Directory.Exists(newPath))

                        System.IO.Directory.CreateDirectory(newPath);
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                    int count = dir.GetFiles().Length;
                    if (count == 0)
                        FileUpload1.PostedFile.SaveAs(newPath + "\\" + sFileName);
                    else
                    {
                        if (count == 1)
                        {
                            string[] Files = Directory.GetFiles(newPath);

                            FileUpload1.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        }
                    }
                    Session["Photo"] = PhotoFilePath;
                    Session["PhotoFileName"] = PhotoFileName;
                    lblFileName.Text = Session["PhotoFileName"].ToString();

                    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
                    string dd = Session["Photo"].ToString().Trim();
                    dd = dd.Replace(@"\", @"/");
                 
                    dd = dd.Replace(sssnpdir, sssnpweb);
                    dd = dd.Replace("~/", sssnpweb);
                    // lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                    Image1.ImageUrl = dd + "/" + lblFileName.Text;
                    // Image1.ImageUrl = Session["Photo"] + "\\"+ lblFileName.Text ;

                }
                else
                {

                    lblresult.Text = "<font color='red'>Upload JPG,JPEG and PNG files only..!</font>";
                    return;
                }
            }
            catch (Exception)//in case of an error
            {
                DeleteFile(PhotoFilePath + "\\" + PhotoFileName);
            }
        }
    }
    //Alternate Photo 1
    public void UploadFile5()
    {

        string PhotoFilePath = "";
        string PhotoFileName = "";

        string sFileDir4 = Server.MapPath("~\\Alternateone");

        if ((FileUpload2.PostedFile != null) && (FileUpload2.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload2.PostedFile))
        {


            //determine file name
            string sFileName = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);
            try
            {

                string[] fileType = FileUpload2.PostedFile.FileName.Split('.');
                int i = fileType.Length;
                if (fileType[i - 1].ToUpper().Trim() == "GIF" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG" || fileType[i - 1].ToUpper().Trim() == "PNG")
                {
                    string newPath = "";
                    if (Session.Count > 0)
                    {
                        newPath = System.IO.Path.Combine(sFileDir4, Session["uid"].ToString());
                    }
                    else
                    {
                        newPath = System.IO.Path.Combine(sFileDir4, "1000");
                    }

                    newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                    PhotoFilePath = newPath.ToString();

                    PhotoFileName = sFileName;
                    // Create the subfolder
                    if (!Directory.Exists(newPath))

                        System.IO.Directory.CreateDirectory(newPath);
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                    int count = dir.GetFiles().Length;
                    if (count == 0)
                        FileUpload2.PostedFile.SaveAs(newPath + "\\" + sFileName);
                    else
                    {
                        if (count == 1)
                        {
                            string[] Files = Directory.GetFiles(newPath);

                            FileUpload2.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        }
                    }
                    Session["Photo1"] = PhotoFilePath;
                    Session["PhotoFileName1"] = PhotoFileName;
                    lblFileName1.Text = Session["PhotoFileName1"].ToString();

                    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
                    string dd = Session["Photo1"].ToString().Trim();
                    dd = dd.Replace(@"\", @"/");
                    dd = dd.Replace(sssnpdir, sssnpweb);
                    dd = dd.Replace("~/", sssnpweb);
                    // lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                    Image2.ImageUrl = dd + "/" + lblFileName1.Text;
                    // Image1.ImageUrl = Session["Photo"] + "\\"+ lblFileName.Text ;

                }
                else
                {

                    lblresult.Text = "<font color='red'>Upload JPG,JPEG and PNG files only..!</font>";
                    return;
                }
            }
            catch (Exception)//in case of an error
            {
                DeleteFile(PhotoFilePath + "\\" + PhotoFileName);
            }
        }
    }
    //Alternate Photo 2
    public void UploadFile6()
    {

        string PhotoFilePath = "";
        string PhotoFileName = "";

        string sFileDir4 = Server.MapPath("~\\Alternatetwo");

        if ((FileUpload3.PostedFile != null) && (FileUpload3.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload3.PostedFile))
        {


            //determine file name
            string sFileName = System.IO.Path.GetFileName(FileUpload3.PostedFile.FileName);
            try
            {

                string[] fileType = FileUpload3.PostedFile.FileName.Split('.');
                int i = fileType.Length;
                if (fileType[i - 1].ToUpper().Trim() == "GIF" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG" || fileType[i - 1].ToUpper().Trim() == "PNG")
                {
                    string newPath = "";
                    if (Session.Count > 0)
                    {
                        newPath = System.IO.Path.Combine(sFileDir4, Session["uid"].ToString());
                    }
                    else
                    {
                        newPath = System.IO.Path.Combine(sFileDir4, "1000");
                    }

                    newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                    PhotoFilePath = newPath.ToString();

                    PhotoFileName = sFileName;
                    // Create the subfolder
                    if (!Directory.Exists(newPath))

                        System.IO.Directory.CreateDirectory(newPath);
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                    int count = dir.GetFiles().Length;
                    if (count == 0)
                        FileUpload3.PostedFile.SaveAs(newPath + "\\" + sFileName);
                    else
                    {
                        if (count == 1)
                        {
                            string[] Files = Directory.GetFiles(newPath);

                            FileUpload3.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        }
                    }
                    Session["Photo2"] = PhotoFilePath;
                    Session["PhotoFileName2"] = PhotoFileName;
                    lblFileName2.Text = Session["PhotoFileName2"].ToString();

                    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
                    string dd = Session["Photo2"].ToString().Trim();
                    dd = dd.Replace(@"\", @"/");
                    dd = dd.Replace(sssnpdir, sssnpweb);
                    // lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                    //dd = dd.Replace("~/", "http://www.snsdpmis.com/");
                    dd = dd.Replace("~/", sssnpweb);
                    Image3.ImageUrl = dd + "/" + lblFileName2.Text;
                    // Image1.ImageUrl = Session["Photo"] + "\\"+ lblFileName.Text ;

                }
                else
                {

                    lblresult.Text = "<font color='red'>Upload JPG,JPEG and PNG files only..!</font>";
                    return;
                }
            }
            catch (Exception)//in case of an error
            {
                DeleteFile(PhotoFilePath + "\\" + PhotoFileName);
            }
        }
    }
    // If Folder file is exist,delte that
    public void DeleteFile(string strFileName)
    {//Delete file from the server
        if (strFileName.Trim().Length > 0)
        {
            FileInfo fi = new FileInfo(strFileName);
            if (fi.Exists)//if file exists delete it
            {
                fi.Delete();
            }
        }
    }

    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetBenficiaryDetByID(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblFileName2.Text = ""; Image3.ImageUrl = "~/Resource/Images/not-available.jpg";
            lblFileName.Text = ""; Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
            lblFileName1.Text = ""; Image2.ImageUrl = "~/Resource/Images/not-available.jpg";

            success.Visible = false;
            Failure.Visible = false;
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["intIP"].ToString()).Value;
            ddlIP.Enabled = false;
            if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
                ddlAge.SelectedValue = ds.Tables[0].Rows[0]["Age"].ToString();

            txtSourceIncome.Text = ds.Tables[0].Rows[0]["SOurceofIncome"].ToString();

            txtHomePhone.Text = ds.Tables[0].Rows[0]["SecondLineHome"].ToString();
            lblEnrollmentNo.Text = "Enrollment No :" + ds.Tables[0].Rows[0]["EnrollmentNo"].ToString();
            lblEnrollmentNo.Visible = true;

            txtHeadName.Text = ds.Tables[0].Rows[0]["NameofHousehold"].ToString();
            txtFamilySize.Text = ds.Tables[0].Rows[0]["FamilySize"].ToString();
            ddlSex.SelectedValue = ddlSex.Items.FindByValue(ds.Tables[0].Rows[0]["Sex"].ToString()).Value;
            txtSpouse.Text = ds.Tables[0].Rows[0]["NameofSpouse"].ToString();
            ddlNoofAdults.SelectedValue = ds.Tables[0].Rows[0]["NoofAdult"].ToString();
            ddldependents.SelectedValue = ds.Tables[0].Rows[0]["NoofDependents"].ToString();
            txtIncome.Text = ds.Tables[0].Rows[0]["FamilyIncome"].ToString();
            txtAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            txtEmail.Text = ds.Tables[0].Rows[0]["Email"].ToString();

            //ddlIsdisability.SelectedValue = ddlIsdisability.Items.FindByValue(ds.Tables[0].Rows[0]["IsDisability"].ToString()).Value;
            //   if (ds.Tables[0].Rows[0]["intDisability"].ToString()!="")
            //  ddlDisability.SelectedValue = ddlDisability.Items.FindByValue(ds.Tables[0].Rows[0]["intDisability"].ToString()).Value;
            txtBiometric.Text = ds.Tables[0].Rows[0]["BiometricNo"].ToString();
            txtBankName.Text = ds.Tables[0].Rows[0]["NameofBank"].ToString();
            txtAccNo.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            if (ds.Tables[0].Rows[0]["LegalStatus"].ToString() != "")
                ddlLegalStatus.SelectedValue = ds.Tables[0].Rows[0]["LegalStatus"].ToString();
            txtReAccNo.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            //txtSelectionCriteria.Text = (ds.Tables[0].Rows[0]["SelectionCriteria"] != null) ? ds.Tables[0].Rows[0]["SelectionCriteria"].ToString() : "";
            if (ds.Tables[0].Rows[0]["SelectionCriteria"] != null)
            {
                if (ds.Tables[0].Rows[0]["SelectionCriteria"].ToString().Contains("1"))
                {
                    ddlComponent.SelectedValue = "Component 1 (Public Works)";
                }
                else if (ds.Tables[0].Rows[0]["SelectionCriteria"].ToString().Contains("2"))
                {
                    ddlComponent.SelectedValue = "Component 1 (Direct Income Support)";
                }
                else
                {
                    ddlComponent.SelectedValue = "Select";
                }
            }
            else
            {
                ddlComponent.SelectedValue = "Select";
            }


            txtSelectionReason.Text = (ds.Tables[0].Rows[0]["SelectionReason"] != null) ? ds.Tables[0].Rows[0]["SelectionReason"].ToString() : "";

            //Component History
            DataSet dsComponentHistory = new DataSet();
            dsComponentHistory = Gen.getBeneficiaryComponentHistory(hdfID.Value.ToString());

            if (dsComponentHistory.Tables[0].Rows.Count > 0)
            {

                grdComponentHistory.DataSource = dsComponentHistory.Tables[0];
                grdComponentHistory.DataBind();
                trComponentHistory.Visible = true;
            }
            else
            {
                trComponentHistory.Visible = false;
            }
            string ben = ds.Tables[0].Rows[0]["BeneficiaryType"].ToString();

            if (ddlBeneficiary.Items.FindByValue(ben) != null)
            {
                ddlBeneficiary.SelectedValue = ddlBeneficiary.Items.FindByValue(ben).Value;
            }

            if (ben == "Group")
            {
                trGroup.Visible = true;
                ddlGroupList.SelectedValue = ddlGroupList.Items.FindByValue(ds.Tables[0].Rows[0]["GroupId"].ToString()).Value;
                hdGroupNo.Value = ddlGroupList.SelectedValue;
                //ddlGroupList.Enabled = false;
            }
            else
            {
                trGroup.Visible = false;
                hdGroupNo.Value = ddlBeneficiary.SelectedValue;
                //ddlGroupList.Enabled = false;
            }

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
                getBomasCod();
            }


            txtName1stAlternatePayee.Text = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();

            txtName2ndAlternatePayee.Text = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
            ddlNumberMaleAdults.Text = ds.Tables[0].Rows[0]["NumberMaleAdults"].ToString();
            ddlNumberFemaleAdults.Text = ds.Tables[0].Rows[0]["NumberFemaleAdults"].ToString();
            ddlNumberAdultsAgeAbove65.Text = ds.Tables[0].Rows[0]["NumberAdultsAgeAbove65"].ToString();
            ddlNumberAdultsAgeBetween45_64.Text = ds.Tables[0].Rows[0]["NumberAdultsAgeBetween45_64"].ToString();
            ddlNumberAdultsAgeBetween18_30.Text = ds.Tables[0].Rows[0]["NumberAdultsAgeBetween18_30"].ToString();
            ddlNumberAdultsAgeBetween30_45.Text = ds.Tables[0].Rows[0]["NumberAdultsAgeBetween30_45"].ToString();
            ddlNumberFemaleChildren.Text = ds.Tables[0].Rows[0]["NumberFemaleChildren"].ToString();
            ddlNumberMaleChildren.Text = ds.Tables[0].Rows[0]["NumberMaleChildren"].ToString();
            ddlNumberChildrenAgeAbove5years.Text = ds.Tables[0].Rows[0]["NumberChildrenAgeAbove5years"].ToString();
            ddlNumberChildrenAgeAbove12years.Text = ds.Tables[0].Rows[0]["NumberChildrenAgeAbove12years"].ToString();
            ddlNumberChildrenAgeAbove18years.Text = ds.Tables[0].Rows[0]["NumberChildrenAgeAbove18years"].ToString();
            txtEthnicityTribeRgin.Text = ds.Tables[0].Rows[0]["EthnicityTribeRgin"].ToString();
            txtNationalID.Text = ds.Tables[0].Rows[0]["NationalID"].ToString();
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString().Trim() != "")
            {
                DdlA1Gender.SelectedValue = DdlA1Gender.Items.FindByValue(ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString()).Value;
            }
            else { DdlA1Gender.SelectedIndex = 0; }
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString().Trim() != "")
            {
                if (DdlA1Age.Items.FindByValue(ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString()) == null)
                {
                    DdlA1Age.Items.Add(ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString().Trim());
                }
                DdlA1Age.SelectedValue = DdlA1Age.Items.FindByValue(ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString()).Value;
            }
            else { DdlA1Age.SelectedIndex = 0; }
            TxtA1NationalID.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeNationID"].ToString();
            TxtA1ContactNo.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeMobile"].ToString();
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString().Trim() != "")
            {
                DdlA2Gender.SelectedValue = DdlA2Gender.Items.FindByValue(ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString()).Value;
            }
            else { DdlA2Gender.SelectedIndex = 0; }
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString().Trim() != "")
            {
                if (DdlA2Age.Items.FindByValue(ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString()) == null)
                {
                    DdlA2Age.Items.Add(ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString().Trim());
                }
                DdlA2Age.SelectedValue = DdlA2Age.Items.FindByValue(ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString()).Value;
            }
            else { DdlA2Age.SelectedIndex = 0; }
            TxtA2NationalID.Text = ds.Tables[0].Rows[0]["SecondAlternateNationalID"].ToString();
            TxtA2ContactNo.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeMobile"].ToString();

            string minioBase = ConfigurationManager.AppSettings["MinioBaseDev"].ToString();

            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                minioBase = ConfigurationManager.AppSettings["MinioBaseProd"].ToString();
            }

            if (ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim() != null)
            {
                Image1.ImageUrl = minioBase + ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim();
            }

            if (ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim() != null)
            {
                Image2.ImageUrl = minioBase + ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim();
            }

            if (ds.Tables[0].Rows[0]["alt2_photo_url"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["alt2_photo_url"].ToString().Trim() != null)
            {
                Image3.ImageUrl = minioBase + ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim();
            }

            //if (ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != null)
            //{
            //    string dd = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
            //    string ddName = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
            //    Session["Photo"] = dd;
            //    Session["PhotoFileName"] = ddName;
            //    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
            //    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            //    dd = dd.Replace(@"\", @"/");

            //    dd = dd.Replace(sssnpdir, sssnpweb);
            //    dd = dd.Replace("~", sssnpweb);
            //    //  dd = dd.Replace("~/", sssnpweb);
            //    //  dd = "http://www.snsdpmis.com/Photo/" + hdfID.Value + "/Household/"; 
            //    lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
            //    //Image1.ImageUrl = dd + "/" + lblFileName.Text;
            //    if (!dd.Contains(ddName))
            //    {
            //        dd += "/" + ddName;
            //    }
            //    Image1.ImageUrl = dd;
            //}
            //if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != null)
            //{
            //    string dd = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim();
            //    string ddName = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
            //    Session["Photo1"] = dd;
            //    Session["PhotoFileName1"] = ddName;
            //    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
            //    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            //    dd = dd.Replace(@"\", @"/");

            //    dd = dd.Replace(sssnpdir, sssnpweb);
            //    dd = dd.Replace("~/", sssnpweb);
            //    lblFileName1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
            //    //   Image2.ImageUrl = dd + "/" + lblFileName1.Text;
            //    // Image2.ImageUrl = dd ;
            //    if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim() != "")
            //    {
            //        if (!dd.Contains(ddName))
            //        {
            //            dd += "/" + ddName;
            //        }
            //        Image2.ImageUrl = dd;
            //        //Image2.ImageUrl = dd + "/" + lblFileName1.Text;
            //    }
            //    else
            //    {
            //        //Image2.ImageUrl = "http:/54.68.125.132:8181/snsdp/public/attachments/" + ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
            //        Image2.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim(); // API_V_1.2
            //    }
            //}
            //if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != null)
            //{
            //    string dd = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim();
            //    string ddName = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
            //    Session["Photo2"] = dd;
            //    Session["PhotoFileName2"] = ddName;
            //    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
            //    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            //    dd = dd.Replace(@"\", @"/");


            //    dd = dd.Replace(sssnpdir, sssnpweb);
            //    dd = dd.Replace("~/", sssnpweb);
            //    lblFileName2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
            //    //  Image3.ImageUrl = dd + "/" + lblFileName2.Text;
            //    //   Image3.ImageUrl = dd;
            //    if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim() != "")
            //    {
            //        if (!dd.Contains(ddName))
            //        {
            //            dd += "/" + ddName;
            //        }
            //        Image3.ImageUrl = dd;
            //        //Image3.ImageUrl = dd + "/" + lblFileName2.Text;
            //    }
            //    else
            //    {
            //        Image3.ImageUrl = System.Configuration.ConfigurationManager.AppSettings["GetAttachments"] + ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim(); // API_V_1.2
            //    }
            //}


            if (ds.Tables[0].Rows[0]["WorkCode"].ToString().Trim() != "")
            {
                BtnSave1.Visible = false;
                lblmsg0.Text = " Beneficiary already allocated to work. Only some fields can be edited";
                success.Visible = false;
                Failure.Visible = true;
                BtnDelete.Visible = false;
                DisableControl();

                if (IsIPRole) { EnableIPControl(); }
                else if (IsTSTRole) { EnableTSTControl(); }

                BtnDelete.Visible = false;
                BtnDelete.Enabled = false;

            }
            else

            {
                BtnSave1.Visible = true;
                lblmsg0.Text = "";
                success.Visible = false;
                Failure.Visible = false;

            }
            //if (ds.Tables[1].Rows.Count > 0)
            //{
            //    if (ds.Tables[1].Rows[0]["PhotoFileName"].ToString() != "" && ds.Tables[1].Rows[0]["PhotoFileName"] != null)
            //    {
            //        lblFileName.Text = ds.Tables[1].Rows[0]["PhotoFileName"].ToString();

            //        Image1.ImageUrl = "viewAttachemnt.aspx?id=" + Convert.ToString(hdfID.Value.ToString()).Trim() + "&Type=Appicant Photo";


            //        if (ds.Tables[1].Rows[0]["Photo"].ToString().Trim() != "")
            //        {
            //            Photo = (byte[])ds.Tables[1].Rows[0]["Photo"];
            //            Session["Photo"] = Photo;
            //            Session["PhotoFileName"] = ds.Tables[1].Rows[0]["PhotoFileName"].ToString();
            //        }

            //    }
            //    else
            //    {
            //        lblFileName.Text = "";
            //    }
            //}



        }
    }
    #endregion


    #region Button Click functionalities Save, Update,Clear, Delete, Look up
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet dsENo = new DataSet();

        if (hdBoma.Value == "")
        {
            lblmsg0.Text = "Please Select Valid Boma / Quarter Council Name which have a Boms code. ";
            success.Visible = false;
            Failure.Visible = true;
        }
        if (ddlComponent.SelectedValue == "Select")
        {
            lblmsg0.Text = "Please Select Component. ";
            success.Visible = false;
            Failure.Visible = true;
        }
        else
        {
            //hdGroupId.Value = ds.Tables[0].Rows[0]["CountBeneficiary"].ToString();
            if (BtnSave1.Text == "Save")
            {
                ds = Gen.CountBeneficiaryType(ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    if (ddlGroupList.SelectedValue == "--Select--")
                    {
                        lblmsg0.Text = "Please select a Group";
                        success.Visible = false;
                        Failure.Visible = true;
                        return;
                    }

                    DataSet dsgs = new DataSet();
                    dsgs = Gen.GetGroupStrength(ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                    int GroupStrength = 0;

                    if (dsgs.Tables[0].Rows.Count > 0)
                    {
                        GroupStrength = Convert.ToInt16(dsgs.Tables[0].Rows[0]["GroupStrength"].ToString());
                    }
                    else
                    {
                        lblmsg0.Text = "There is no group strength for selected boma";
                        success.Visible = false;
                        Failure.Visible = true;
                        return;
                    }

                    int Beneficiary = Convert.ToInt16(ds.Tables[0].Rows[0]["CountBeneficiary"].ToString());
                    if (ddlBeneficiary.SelectedValue == "CST" && Beneficiary >= GroupStrength)
                    {
                        lblmsg0.Text = "Already " + GroupStrength + " members are included for this type. ";
                        success.Visible = false;
                        Failure.Visible = true;
                    }
                    else if (ddlBeneficiary.SelectedValue == "Group" && Beneficiary >= GroupStrength)
                    {
                        lblmsg0.Text = "Already " + GroupStrength + " members are included for this group. ";
                        success.Visible = false;
                        Failure.Visible = true;
                    }
                    else
                    {
                        //if (Convert.ToInt32(ddldependents.SelectedItem.Text) != (Convert.ToInt32(ddlNumberFemaleChildren.SelectedItem.Text) + Convert.ToInt32(ddlNumberMaleChildren.SelectedItem.Text)) && ddldependents.SelectedIndex != 0 && ddlNumberFemaleChildren.SelectedIndex != 0 && ddlNumberMaleChildren.SelectedIndex!=0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Number of dependents equal to total of male and female children')", true);   
                        //}
                        //else
                        //{
                        if (ddldependents.SelectedIndex != 0 && ddlNumberFemaleChildren.SelectedIndex != 0 && ddlNumberMaleChildren.SelectedIndex != 0)
                        {
                            if (Convert.ToInt32(ddldependents.SelectedItem.Text) != (Convert.ToInt32(ddlNumberFemaleChildren.SelectedItem.Text) + Convert.ToInt32(ddlNumberMaleChildren.SelectedItem.Text)))
                            {
                                lblmsg0.Text = "Number of dependents equal to total of male and female children";
                                success.Visible = false;
                                Failure.Visible = true;
                                return;
                            }
                        }

                        //int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, txtSelectionCriteria.Text, txtSelectionReason.Text);
                        int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, ddlComponent.SelectedValue, txtSelectionReason.Text);
                        if (i != 999)
                        {


                            int a = Gen.UpdateAlternateDetails(i.ToString(), txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());
                            // Due to OLD Function Model           // int j = Gen.InsrtUpdAttachment(i.ToString(), (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                            dsENo = Gen.GetEnrollmentNoByintBenid(i.ToString());

                            if (dsENo.Tables[0].Rows.Count > 0)
                            {
                                string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                                lblmsg.Text = "Submitted Successfully..! Your Enrollment No " + EnrollmentNo;
                            }

                            success.Visible = true;
                            Failure.Visible = false;
                            clear();
                        }
                        else
                        {
                            lblmsg0.Text = "Already Exist. ";
                            success.Visible = false;
                            Failure.Visible = true;
                        }

                        //}

                    }
                }
            }
            else
            {
                DataSet dsUp = new DataSet();
                dsUp = Gen.CountBeneficiaryTypeInUpdate(hdfID.Value, ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                if (dsUp.Tables[0].Rows.Count > 0)
                {
                    int BeneficiaryUp = Convert.ToInt16(dsUp.Tables[0].Rows[0]["CountBeneficiary"].ToString());
                    if (BeneficiaryUp > 0)
                    {
                        // if (Convert.ToInt32(ddldependents.SelectedItem.Text) != (Convert.ToInt32(ddlNumberFemaleChildren.SelectedItem.Text) + Convert.ToInt32(ddlNumberMaleChildren.SelectedItem.Text)) && ddldependents.SelectedIndex != 0 && ddlNumberFemaleChildren.SelectedIndex != 0 && ddlNumberMaleChildren.SelectedIndex!=0)
                        //{
                        //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Number of dependents equal to total of male and female children')", true);   
                        //}
                        //else
                        //{
                        //Update for Change Enrolment no


                        if (ddldependents.SelectedIndex != 0 && ddlNumberFemaleChildren.SelectedIndex != 0 && ddlNumberMaleChildren.SelectedIndex != 0)
                        {
                            if (Convert.ToInt32(ddldependents.SelectedItem.Text) != (Convert.ToInt32(ddlNumberFemaleChildren.SelectedItem.Text) + Convert.ToInt32(ddlNumberMaleChildren.SelectedItem.Text)))
                            {
                                lblmsg0.Text = "Number of dependents equal to total of male and female children";
                                success.Visible = false;
                                Failure.Visible = true;
                                return;
                            }
                        }


                        //int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.Text, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, txtSelectionCriteria.Text, txtSelectionReason.Text);
                        int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.Text, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, ddlComponent.SelectedValue, txtSelectionReason.Text);
                        if (i != 999)
                        {
                            int a = Gen.UpdateAlternateDetails(hdfID.Value, txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());

                            //int j = Gen.InsrtUpdAttachment(hdfID.Value, (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                            dsENo = Gen.GetEnrollmentNoByintBenid(hdfID.Value.ToString());

                            if (dsENo.Tables[0].Rows.Count > 0)
                            {
                                string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                                lblmsg.Text = "Updated Successfully..! Your Enrollment No " + EnrollmentNo;
                            }
                            success.Visible = true;
                            Failure.Visible = false;
                            clear();
                        }
                        else
                        {
                            lblmsg0.Text = "Already Exist. ";
                            success.Visible = false;
                            Failure.Visible = true;
                            //lblmsg.Text = "Added Successfully..!";
                        }
                        //}


                    }
                    else
                    {
                        //Update for not Change Enrolment no
                        ds = Gen.CountBeneficiaryType(ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            if (ddlGroupList.SelectedValue == "--Select--")
                            {
                                lblmsg0.Text = "Please select a Group";
                                success.Visible = false;
                                Failure.Visible = true;
                                return;
                            }
                            DataSet dsgs = new DataSet();
                            dsgs = Gen.GetGroupStrength(ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                            int GroupStrength = 0;

                            if (dsgs.Tables[0].Rows.Count > 0)
                            {
                                GroupStrength = Convert.ToInt16(dsgs.Tables[0].Rows[0]["GroupStrength"].ToString());
                            }
                            else
                            {
                                lblmsg0.Text = "There is no group strength for selected boma";
                                success.Visible = false;
                                Failure.Visible = true;
                                return;
                            }
                            int Beneficiary = Convert.ToInt16(ds.Tables[0].Rows[0]["CountBeneficiary"].ToString());

                            if (ddlBeneficiary.SelectedValue == "CST" && Beneficiary >= GroupStrength)
                            {
                                lblmsg0.Text = "Already " + GroupStrength + " members are included for this type. ";
                                success.Visible = false;
                                Failure.Visible = true;
                            }
                            else if (ddlBeneficiary.SelectedValue == "Group" && Beneficiary >= GroupStrength)
                            {
                                lblmsg0.Text = "Already " + GroupStrength + " members are included for this group. ";
                                success.Visible = false;
                                Failure.Visible = true;
                            }
                            else
                            {


                                //if (Convert.ToInt32(ddldependents.SelectedItem.Text) == (Convert.ToInt32(ddlNumberFemaleChildren.SelectedItem.Text) + Convert.ToInt32(ddlNumberMaleChildren.SelectedItem.Text)))
                                //{

                                if (ddldependents.SelectedIndex != 0 && ddlNumberFemaleChildren.SelectedIndex != 0 && ddlNumberMaleChildren.SelectedIndex != 0)
                                {
                                    if (Convert.ToInt32(ddldependents.SelectedItem.Text) != (Convert.ToInt32(ddlNumberFemaleChildren.SelectedItem.Text) + Convert.ToInt32(ddlNumberMaleChildren.SelectedItem.Text)))
                                    {
                                        lblmsg0.Text = "Number of dependents equal to total of male and female children";
                                        success.Visible = false;
                                        Failure.Visible = true;
                                        return;
                                    }
                                }
                                //int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.Text, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, txtSelectionCriteria.Text, txtSelectionReason.Text);
                                int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.Text, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, ddlComponent.SelectedValue, txtSelectionReason.Text);

                                if (i != 999)
                                {

                                    //int j = Gen.InsrtUpdAttachment(hdfID.Value, (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                                    dsENo = Gen.GetEnrollmentNoByintBenid(hdfID.Value.ToString());

                                    if (dsENo.Tables[0].Rows.Count > 0)
                                    {
                                        string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                                        lblmsg.Text = "Updated Successfully..! Your Enrollment No " + EnrollmentNo;
                                    }
                                    success.Visible = true;
                                    Failure.Visible = false;
                                    clear();
                                }
                                else
                                {
                                    lblmsg0.Text = "Already Exist. ";
                                    success.Visible = false;
                                    Failure.Visible = true;
                                    //lblmsg.Text = "Added Successfully..!";
                                }
                                //}
                                //else
                                //{
                                //    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Number of dependents equal to total of male and female children')", true);
                                //}

                            }
                        }
                    }
                }

            }


        }
    }

    void clear()
    {
        ddlLegalStatus.SelectedIndex = 0;
        txtSourceIncome.Text = "";
        Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
        lblFileName.Text = "";
        BtnSave1.Text = "Save";
        txtReAccNo.Text = "";
        ddlAge.SelectedIndex = 0;
        txtHomePhone.Text = "";
        // txtEnrollment.Text="";
        txtHeadName.Text = "";
        txtFamilySize.Text = "";
        ddlSex.SelectedIndex = 0;
        txtSpouse.Text = "";
        ddlNoofAdults.SelectedValue = "-1";
        ddldependents.SelectedValue = "-1";
        txtIncome.Text = "";
        txtAddress.Text = "";
        txtContactNo.Text = "";
        txtEmail.Text = "";
        txtHomePhone.Text = "";
        ddlIsdisability.SelectedIndex = 0;
        ddlDisability.SelectedIndex = 0;
        txtBiometric.Text = "";
        txtBankName.Text = "";
        txtAccNo.Text = "";

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
        ddlBeneficiary.SelectedIndex = 0;
        ddlGroupList.SelectedIndex = 0;
        trGroup.Visible = false;

        txtName1stAlternatePayee.Text = "";
        txtName2ndAlternatePayee.Text = "";
        ddlNumberMaleAdults.SelectedValue = "-1";
        ddlNumberFemaleAdults.SelectedValue = "-1";
        ddlNumberAdultsAgeAbove65.SelectedValue = "-1";
        ddlNumberAdultsAgeBetween45_64.SelectedValue = "-1";
        ddlNumberAdultsAgeBetween18_30.SelectedValue = "-1";
        ddlNumberAdultsAgeBetween30_45.SelectedValue = "-1";
        ddlNumberFemaleChildren.SelectedValue = "-1";
        ddlNumberMaleChildren.SelectedValue = "-1";
        ddlNumberChildrenAgeAbove5years.SelectedValue = "-1";
        ddlNumberChildrenAgeAbove12years.SelectedValue = "-1";
        ddlNumberChildrenAgeAbove18years.SelectedValue = "-1";
        txtEthnicityTribeRgin.Text = "";
        txtNationalID.Text = "";
        DdlA1Age.SelectedIndex = 0;
        DdlA2Gender.SelectedIndex = 0;
        DdlA1Gender.SelectedIndex = 0;
        DdlA2Age.SelectedIndex = 0;
        TxtA1ContactNo.Text = "";
        TxtA1NationalID.Text = "";
        TxtA2ContactNo.Text = "";
        TxtA2NationalID.Text = "";
        lblFileName1.Text = "";
        lblFileName2.Text = "";
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("IPBeneficiaryRegistration.aspx");
    }


    protected void BtnClear0_Click(object sender, EventArgs e)
    {
        int a = Gen.DeleteBenficiary(hdfID.Value, Session["uid"].ToString());
        if (a == 111)
        {
            lblmsg0.Text = "Already Attendance or Wage payment done for this benficiary.So We can't delete this";
            success.Visible = false;
            Failure.Visible = true;
            clear();

        }
        else if (a == 1)
        {


            lblmsg.Text = "Delete successful";
            success.Visible = true;
            Failure.Visible = false;
            clear();
        }
        else
        {
            lblmsg0.Text = "Deletion failed";
            success.Visible = false;
            Failure.Visible = true;
            clear();

        }
    }

    #endregion

    #region Dropdown Binding Functionalities
    protected void DropDownList25_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIsdisability.SelectedValue == "Y")
        {
            trdisability.Visible = true;
        }
        else
        {
            trdisability.Visible = false;
        }
    }

    protected void BeneficiarySelect(object sender, EventArgs e)
    {
        if (ddlBeneficiary.Text == "Group")
        {
            trGroup.Visible = true;
            //hdBeneficiaryType.Value = "";
        }
        else
        {
            trGroup.Visible = false;
            // hdBeneficiaryType.Value = ddlBeneficiary.SelectedValue.ToString();
            hdGroupNo.Value = "";
        }
    }

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

    protected void ddlBoma_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBoma.SelectedIndex != 0)
        {
            getBomasCod();
        }
        else
        {
            hdBoma.Value = "";
        }
    }

    void getBomasCod()
    {

        hdBoma.Value = "";
        DataSet ds = new DataSet();


        ds = Gen.getBomasCode(Convert.ToInt32(ddlBoma.SelectedValue.ToString()));

        if (ds.Tables[0].Rows.Count > 0)
        {
            hdBoma.Value = ds.Tables[0].Rows[0]["BomaCode"].ToString();
        }
        else
        {
            hdBoma.Value = "";
        }
    }

    protected void ddlGroupList_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdGroupNo.Value = ddlGroupList.SelectedValue.ToString();
        // hdBeneficiaryType.Value = ddlGroupList.SelectedValue.ToString();
    }

    #endregion

    protected void btnSaveCustom_Click(object sender, EventArgs e)
    {
        //enable ddlComponent
        //if it did not change NOP
        //else update history table [tbl_BeneficiaryDetNewComponent]
        //  old value set with start and end date (if it is first change, detected by no record in table [tbl_BeneficiaryDetNewComponent], set start date of old state enrollment date
        //  new value set with start date (end date set as null)
        // set new value in tbl_beneficiarydet Table as @SelectionCriteria

        int benID = 0;
        int.TryParse(hdfID.Value.ToLower(), out benID);
        if (benID == 0) return;

        if (IsIPRole)
        {


            if (ddlSex.SelectedIndex == 0)
            {
                lblmsg0.Text = "Please Select a Valid Gender ";
                success.Visible = false;
                Failure.Visible = true;
                return;
            }
            if (ddlComponent.SelectedValue == "Select")
            {
                lblmsg0.Text = "Please Select Component. ";
                success.Visible = false;
                Failure.Visible = true;
                return;
            }

            int result = 0;
            result = Gen.UpdateBeneficiaryByIPRole(benID, txtHeadName.Text, ddlAge.Text, ddlSex.Text, UserID, ddlComponent.SelectedValue);
            Response.Redirect("UpdateSuccess.aspx");

        }
        else if (IsTSTRole)
        {
            int result = 0;
            //result = Gen.UpdateBeneficiaryByTSTRole(benID, txtSelectionCriteria.Text, txtSelectionReason.Text, txtFamilySize.Text, UserID);
            result = Gen.UpdateBeneficiaryByTSTRole(benID, ddlComponent.SelectedValue, txtSelectionReason.Text, txtFamilySize.Text, UserID);
            Response.Redirect("UpdateSuccess.aspx");
        }
    }
}

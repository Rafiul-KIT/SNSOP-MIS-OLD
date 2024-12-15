using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;

public partial class IPBeneficiaryRegistration1 : System.Web.UI.Page
{
    //designing and developed by siva as on 27-02-2016

    //Purpose : To Add Beneficiary Details
    //Tables used : tbl_BeneficiaryDet
    //Stored procedures Used : InsUpdBenDetails,sp_getCounties,sp_getPayams,getBomasbyID

    General Gen = new General();

    byte[] Photo = new byte[1];
    string PhotoFileName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        // btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            hdfFlagID.Value = "0";
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
                //ddlAge.Items.Insert("--Select--", 0);
                ddlAge.Items.Add(i.ToString());

            }
            for (int i = 5; i <= 100; i++)
            {
                //ddlAge.Items.Insert("--Select--", 0);
                DdlA1Age.Items.Add(i.ToString());

            }
            for (int i = 5; i <= 100; i++)
            {
                DdlA2Age.Items.Add(i.ToString());

            }

            //Session["Photo"] = Photo;
            //Session["PhotoFileName"] = "";



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
                //  trlookup.Visible = false;
                BtnSave1.Visible = false;
                BtnClear.Visible = false;
            }

        }


        UploadFile4();
        UploadFile5();
        UploadFile6();



        if (Request.QueryString[0].ToString() != null && Request.QueryString[0].ToString() != "" && hdfFlagID.Value == "0")
        {
            hdfFlagID.Value = "1";
            hdfID.Value = Request.QueryString[0].ToString();
            FillDetails();
            if (Session["userlevel"].ToString() == "1" || (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "IP"))
            {
                BtnDelete.Visible = true;
            }
            else
            {
                BtnDelete.Visible = false;
            }
            BtnSave1.Text = "Update";

        }


    }


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
                    // lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                    Image2.ImageUrl = dd + "/" + lblFileName.Text;
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
                    Image3.ImageUrl = dd + "/" + lblFileName.Text;
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


    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

        if (Request.QueryString[0].ToString() != null && Request.QueryString[0].ToString() != "")
        {
            ds = Gen.GetBenficiaryDetByAPIID1(hdfID.Value);
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            success.Visible = false;
            Failure.Visible = false;
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["intIP"].ToString()).Value;
            ddlIP.Enabled = false;
            if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
            {

                ddlAge.SelectedValue = ds.Tables[0].Rows[0]["Age"].ToString();
            }

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

            string ben = (ds.Tables[0].Rows[0]["BeneficiaryType"].ToString() == "1") ? "Group" : "";

            if (ddlBeneficiary.Items.FindByValue(ben) != null)
            {
                ddlBeneficiary.SelectedValue = ddlBeneficiary.Items.FindByValue(ben).Value;
            }

            // ddlBeneficiary.Enabled = false;

            if (ben == "Group")
            {
                trGroup.Visible = true;
                //    ddlGroupList.SelectedValue = ddlGroupList.Items.FindByValue(ds.Tables[0].Rows[0]["GroupId"].ToString()).Value;
                //    hdGroupNo.Value = ddlGroupList.SelectedValue;
                //ddlGroupList.Enabled = false;
            }
            else
            {
                trGroup.Visible = false;
                //   hdGroupNo.Value = ddlBeneficiary.SelectedValue;
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
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString().Trim() != "")
            {
                DdlA1Age.SelectedValue = DdlA1Age.Items.FindByValue(ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString()).Value;
            }
            TxtA1NationalID.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeNationID"].ToString();
            TxtA1ContactNo.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeMobile"].ToString();
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString().Trim() != "")
            {

                DdlA2Gender.SelectedValue = DdlA2Gender.Items.FindByValue(ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString()).Value;

            }
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString().Trim() != "")
            {
                DdlA2Age.SelectedValue = DdlA2Age.Items.FindByValue(ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString()).Value;
            }
            TxtA2NationalID.Text = ds.Tables[0].Rows[0]["SecondAlternateNationalID"].ToString();
            TxtA2ContactNo.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeMobile"].ToString();


            //txtSelectionCriteria.Text = (ds.Tables[0].Rows[0]["SelectionCriteria"] != null) ? ds.Tables[0].Rows[0]["SelectionCriteria"].ToString() : "";
            
            if (ds.Tables[0].Rows[0]["SelectionCriteria"] != null)
            {
                if (ds.Tables[0].Rows[0]["SelectionCriteria"].ToString().Contains("Public"))
                {
                    ddlComponent.SelectedValue = "Component 1 (Public Works)";
                }
                else if (ds.Tables[0].Rows[0]["SelectionCriteria"].ToString().Contains("Direct"))
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



            if (ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
                Session["Photo"] = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
                Session["PhotoName"] = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                dd = dd.Replace(@"\", @"/");
                dd = sssnpweb + "/Photo/" + hdfID.Value + "/Household/";
                lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                Image1.ImageUrl = dd + lblFileName.Text;
            }
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim();
                Session["Photo1"] = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim();
                Session["PhotoName1"] = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
                string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                dd = dd.Replace(@"\", @"/");
                //dd = "http://www.snsdpmis.com/Alternateone/" + hdfID.Value + "/";
                dd = sssnpweb + "/Alternateone/" + hdfID.Value + "/";
                lblFileName1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
                Image2.ImageUrl = dd + lblFileName1.Text;
            }
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim();
                Session["Photo2"] = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim();
                Session["PhotoName2"] = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
                string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                dd = dd.Replace(@"\", @"/");
                dd = sssnpweb + "/Alternatetwo/" + hdfID.Value + "/";
                lblFileName2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
                Image3.ImageUrl = dd + lblFileName2.Text;
            }

        }
    }


    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        DataSet dsENo = new DataSet();
        if (ddlStatus.SelectedValue == "0")
        {
            Failure.Visible = true;

            lblmsg0.Text = "Please Select Status. ";
            return;
        }
        if (hdBoma.Value == "")
        {
            lblmsg0.Text = "Please Select Valid Boma / Quarter Council Name which have a Boms code. ";
            success.Visible = false;
            Failure.Visible = true;
        }
        if (ddlComponent.SelectedValue == "Select")
        {
            Failure.Visible = true;

            lblmsg0.Text = "Please Select Component. ";
            return;
        }
        else
        {
            //hdGroupId.Value = ds.Tables[0].Rows[0]["CountBeneficiary"].ToString();
            if (BtnSave1.Text == "Save")
            {
                ds = Gen.CountBeneficiaryType(ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                if (ds.Tables[0].Rows.Count > 0)
                {
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

                        //int i = Gen.InsUpdBenDetails1(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, txtSelectionCriteria.Text, txtSelectionReason.Text);
                        int i = Gen.InsUpdBenDetails1(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, ddlComponent.SelectedValue, txtSelectionReason.Text);
                        if (i != 999)
                        {
                            int k = Gen.UpdBenficiaryDetByAPI(hdfID.Value, ddlStatus.SelectedValue);

                            int a = Gen.UpdateAlternateDetails(i.ToString(), txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());
                            // int j = Gen.InsrtUpdAttachment(i.ToString(), (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                            dsENo = Gen.GetEnrollmentNoByintBenid1(i.ToString());

                            if (dsENo.Tables[0].Rows.Count > 0)
                            {
                                string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                                lblmsg.Text = "Submitted Successfully..! Your Enrollment No " + EnrollmentNo;


                            }

                            success.Visible = true;
                            Failure.Visible = false;
                            clear();
                            Response.Redirect("APISuccess.aspx?lbl=" + lblmsg.Text);
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
                dsUp = Gen.CountBeneficiaryTypeInUpdate1(hdfID.Value, ddlBoma.SelectedValue, hdGroupNo.Value.ToString(), ddlBeneficiary.SelectedValue);
                if (ddlStatus.SelectedValue == "2")
                {
                    if (dsUp.Tables[0].Rows.Count > 0)
                    {
                        int BeneficiaryUp = Convert.ToInt16(dsUp.Tables[0].Rows[0]["CountBeneficiary"].ToString());
                        if (BeneficiaryUp > 0)
                        {

                            //int i = Gen.InsUpdBenDetails1(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.Text, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text,txtSelectionCriteria.Text, txtSelectionReason.Text);
                            int i = Gen.InsUpdBenDetails1(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.Text, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, ddlComponent.SelectedValue, txtSelectionReason.Text);
                            if (i != 999)
                            {
                                int k = Gen.UpdBenficiaryDetByAPI(hdfID.Value, ddlStatus.SelectedValue);
                                int a = Gen.UpdateAlternateDetails(hdfID.Value, txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());

                                //int j = Gen.InsrtUpdAttachment(hdfID.Value, (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                                dsENo = Gen.GetEnrollmentNoByintBenid1(hdfID.Value);

                                if (dsENo.Tables[0].Rows.Count > 0)
                                {
                                    string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                                    lblmsg.Text = "Updated Successfully..! Your Enrollment No " + EnrollmentNo;

                                }
                                success.Visible = true;
                                Failure.Visible = false;
                                clear();
                                Response.Redirect("APISuccess.aspx?lbl=" + lblmsg.Text);
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
                                    //int i = Gen.InsUpdBenDetails1(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.Text, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, txtSelectionCriteria.Text, txtSelectionReason.Text);
                                    int i = Gen.InsUpdBenDetails1(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.Text, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text, ddlComponent.SelectedValue, txtSelectionReason.Text);
                                    if (i != 999)
                                    {
                                        int k = Gen.UpdBenficiaryDetByAPI(hdfID.Value, ddlStatus.SelectedValue);
                                        int a = Gen.UpdateAlternateDetails(hdfID.Value, txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());

                                        //int j = Gen.InsrtUpdAttachment(hdfID.Value, (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                                        dsENo = Gen.GetEnrollmentNoByintBenid1(hdfID.Value);

                                        if (dsENo.Tables[0].Rows.Count > 0)
                                        {
                                            string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                                            lblmsg.Text = "Updated Successfully..! Your Enrollment No " + EnrollmentNo;

                                        }
                                        success.Visible = true;
                                        Failure.Visible = false;
                                        clear();

                                        Response.Redirect("APISuccess.aspx?lbl=" + lblmsg.Text);
                                    }
                                    else
                                    {
                                        lblmsg0.Text = "Already Exist. ";
                                        success.Visible = false;
                                        Failure.Visible = true;
                                        //lblmsg.Text = "Added Successfully..!";
                                    }

                                }
                            }
                        }
                    }
                }
                else if (ddlStatus.SelectedValue == "3")
                {
                    int k = Gen.UpdBenficiaryDetByAPI(hdfID.Value, ddlStatus.SelectedValue);
                    // int a = Gen.UpdateAlternateDetails(hdfID.Value, txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());


                    dsENo = Gen.GetEnrollmentNoByintBenid1(hdfID.Value);

                    if (dsENo.Tables[0].Rows.Count > 0)
                    {
                        string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                        lblmsg.Text = "Dropped Successfully..! Your Enrollment No " + EnrollmentNo;

                    }
                    success.Visible = true;
                    Failure.Visible = false;
                    clear();

                    Response.Redirect("APISuccess.aspx?lbl=" + lblmsg.Text);


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
        Response.Redirect("IPBeneficiaryRegistration1.aspx");
    }
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
    protected void BtnClear0_Click(object sender, EventArgs e)
    {
        if (txtRemarks.Text != "")
        {
            int a = Gen.DeleteByAPI(hdfID.Value, txtRemarks.Text);
            if (a == 1)
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
        else
        {
            Failure.Visible = true;
            lblmsg0.Text = "Please Enter Remarks";
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


        ds = Gen.getBomasCode(Convert.ToInt16(ddlBoma.SelectedValue.ToString()));

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
}

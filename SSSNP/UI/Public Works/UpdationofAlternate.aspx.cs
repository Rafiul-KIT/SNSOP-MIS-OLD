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
using System.Text;

public partial class UpdationofAlternate : System.Web.UI.Page
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
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            trlookup.Visible = false;
            Td1.Visible = false;
            Session["Photo"] = "";
            Session["PhotoFileName"] = "";
            Session["Photo1"] = "";
            Session["PhotoFileName1"] = "";
            Session["Photo2"] = "";
            Session["PhotoFileName2"] = "";
            Session["Photo4"] = "";
            Session["PhotoFileName4"] = "";
            ResetFormControlDisable(this);
          
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
            for (int i = 1; i <= 100; i++)
            {
              DdlA2Age.Items.Add(i.ToString());

            }
            for (int i = 1; i <= 100; i++)
            {
                DdlA3Age.Items.Add(i.ToString());

            }

            //Session["Photo"] = Photo;
            //Session["PhotoFileName"] = "";

          

            


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
            if (FileUpload4.HasFile)
            {
                Session["FileUpload14"] = FileUpload4;
                hdfUploadFile4.Value = FileUpload4.FileName;
            }
            else if (Session["FileUpload14"] != null)
            {
                FileUpload4 = (FileUpload)Session["FileUpload14"];
                hdfUploadFile4.Value = FileUpload4.FileName;
            }

            BtnSave1.Visible = false;
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


        //UploadFile4();
        UploadFile5();
        UploadFile6();
        UploadFile8();

       
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            ResetFormControlDisable(this);
            FillDetails();
            
            BtnDelete.Visible = false;
            BtnSave1.Text = "Update";
            trlookup.Visible = false;
            Td1.Visible = false;
            BtnClear.Visible = true;
        }


    }

    #region ClearControls
    public void ResetFormControlDisable(Control parent)
    {


        foreach (Control c in parent.Controls)
        {
            if (c.Controls.Count > 0)
            {
                ResetFormControlDisable(c);
            }
            else
            {
                switch (c.GetType().ToString())
                {
                    case "System.Web.UI.WebControls.TextBox":
                        ((TextBox)c).Enabled = false;
                        break;

                    case "System.Web.UI.WebControls.DropDownList":

                        if (((DropDownList)c).Items.Count > 0)
                        {
                            ((DropDownList)c).Enabled = false;
                        }
                        break;
                    case "System.Web.UI.WebControls.FileUpload":
                        ((FileUpload)c).Enabled = false;
                        break;
                }
            }
        }
    }
    #endregion



    public void UploadFile8()
    {

        string PhotoFilePath = "";
        string PhotoFileName = "";

        string sFileDir4 = Server.MapPath("~\\Alternatethree");

        if ((FileUpload4.PostedFile != null) && (FileUpload4.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload4.PostedFile))
        {


            //determine file name
            string sFileName = System.IO.Path.GetFileName(FileUpload4.PostedFile.FileName);
            try
            {

                string[] fileType = FileUpload4.PostedFile.FileName.Split('.');
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
                        FileUpload4.PostedFile.SaveAs(newPath + "\\" + sFileName);
                    else
                    {
                        if (count == 1)
                        {
                            string[] Files = Directory.GetFiles(newPath);

                            FileUpload4.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        }
                    }
                    Session["Photo4"] = PhotoFilePath;
                    Session["PhotoFileName4"] = PhotoFileName;
                    lblFileName4.Text = Session["PhotoFileName4"].ToString();


                    string dd = Session["Photo4"].ToString().Trim();
                    dd = dd.Replace(@"\", @"/");
                    dd = dd.Replace("D:/Backups/SNSDP Pre TEsting/nikita", "~/");
                //    dd = dd.Replace("~/", "http://www.snsdpmis.com/");
                    // lblFileName.Text = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                    Image4.ImageUrl = dd + "/" + lblFileName4.Text;
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


                    string dd = Session["Photo1"].ToString().Trim();
                    dd = dd.Replace(@"\", @"/");
                    dd = dd.Replace("", "~/");
           
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


                    string dd = Session["Photo2"].ToString().Trim();
                    dd = dd.Replace(@"\", @"/");
                    dd = dd.Replace("", "~/");
                    
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
    //void UploadImage()
    //{

    //    //PhotoFilename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
    //    Session["PhotoFileName"] = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);

    //    try
    //    {
    //        string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
    //        int letterI = fileType.Length;
    //        //Photo = new byte[FileUpload1.PostedFile.ContentLength];

    //        //    Session["Photo"] = new byte[FileUpload1.PostedFile.ContentLength];

    //        if (
    //            fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
    //            fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
    //            fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
    //            fileType[letterI - 1].ToUpper().Trim() == "JPEG")
    //        {
    //            //Photo = new byte[FileUpload1.PostedFile.ContentLength];

    //            Session["Photo"] = new byte[FileUpload1.PostedFile.ContentLength];
    //            HttpPostedFile Image = FileUpload1.PostedFile;
    //            //Image.InputStream.Read(Photo, 0, (int)FileUpload1.PostedFile.ContentLength);
    //            //lblFileName.Text = PhotoFilename;
    //            //  ObjectToByteArray(Session["Photo"]);
    //            Image.InputStream.Read((byte[])Session["Photo"], 0, (int)FileUpload1.PostedFile.ContentLength);
    //            lblFileName.Text = Session["PhotoFileName"].ToString();

    //            //   Session["Photo"] = Photo;
    //            //   Session["PhotoFileName"] = PhotoFilename;

    //            Image1.ImageUrl = "ScanImage.aspx";



    //        }
    //        else
    //        {
    //            lblmsg.Text = "Image format should be JPG or JPEG or GIF or PNG";

    //        }

    //    }
    //    catch (Exception ex)
    //    {

    //        lblmsg.Text = "An Error Occured. Please Try Again!" + ex.Message;

    //    }

    //    finally
    //    {

    //    }


    //    FileUpload1.Focus();

    //}

    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

        success.Visible = false;
        Failure.Visible = false;
        lblmsg0.Text = "";
        lblmsg.Text = "";
        ds = Gen.GetBenficiaryDetByIDforalternate(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblFileName2.Text = ""; Image3.ImageUrl = "~/Resource/Images/not-available.jpg";
          //  lblFileName.Text = ""; Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
            lblFileName1.Text = ""; Image2.ImageUrl = "~/Resource/Images/not-available.jpg";
            lblFileName4.Text = ""; Image4.ImageUrl = "~/Resource/Images/not-available.jpg";
            success.Visible = false;
            Failure.Visible = false;
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["intIP"].ToString()).Value;
            ddlIP.Enabled = false;
            if (ds.Tables[0].Rows[0]["Age"].ToString() != "")
                ddlAge.SelectedValue = ds.Tables[0].Rows[0]["Age"].ToString();

          
          
            lblEnrollmentNo.Text = "Enrollment No :" + ds.Tables[0].Rows[0]["EnrollmentNo"].ToString();
            
            lblEnrollmentNo.Visible = true;
            lblhhno.Text = "Household Number :" + ds.Tables[0].Rows[0]["household_number"].ToString();

            lblhhno.Visible = true;

            txtHeadName.Text = ds.Tables[0].Rows[0]["NameofHousehold"].ToString();
          
            ddlSex.SelectedValue = ddlSex.Items.FindByValue(ds.Tables[0].Rows[0]["Sex"].ToString()).Value;
           // txtSpouse.Text = ds.Tables[0].Rows[0]["NameofSpouse"].ToString();
           
         
          
            //////if (ds.Tables[0].Rows[0]["LegalStatus"].ToString() != "")
            //////    ddlLegalStatus.SelectedValue = ds.Tables[0].Rows[0]["LegalStatus"].ToString();
          
            // ddlBeneficiary.Enabled = false;
          
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
            hdfahname1.Value = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();

            txtName2ndAlternatePayee.Text = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
            hdfahname2.Value = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
            txtName13rdAlternatePayee.Text = ds.Tables[0].Rows[0]["Name3rdAlternatePayee"].ToString();
            hdfahname3.Value = ds.Tables[0].Rows[0]["Name3rdAlternatePayee"].ToString();

            hdfahno1.Value = ds.Tables[0].Rows[0]["alternate_number"].ToString();
            hdfahno2.Value = ds.Tables[0].Rows[0]["Secondalternate_number"].ToString();
            hdfahno3.Value = ds.Tables[0].Rows[0]["thirdalternate_number"].ToString();
            txtNameHN.Text = ds.Tables[0].Rows[0]["household_number"].ToString();
           
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString().Trim() != "")
            {
                DdlA1Gender.SelectedValue = DdlA1Gender.Items.FindByValue(ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString()).Value;
            }
            else { DdlA1Gender.SelectedIndex = 0; }
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString().Trim() != "")
            {
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
                DdlA2Age.SelectedValue = DdlA2Age.Items.FindByValue(ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString()).Value;
            }
            else { DdlA2Age.SelectedIndex = 0; }
                 TxtA2NationalID.Text = ds.Tables[0].Rows[0]["SecondAlternateNationalID"].ToString();
            TxtA2ContactNo.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeMobile"].ToString();



            if (ds.Tables[0].Rows[0]["ThirdAlternatePayeeGender"].ToString().Trim() != "")
            {
                DdlA3Gender.SelectedValue = DdlA1Gender.Items.FindByValue(ds.Tables[0].Rows[0]["ThirdAlternatePayeeGender"].ToString()).Value;
            }
            else { DdlA3Gender.SelectedIndex = 0; }
            if (ds.Tables[0].Rows[0]["ThirdAlternatePayeeAge"].ToString().Trim() != "")
            {
                DdlA3Age.SelectedValue = DdlA1Age.Items.FindByValue(ds.Tables[0].Rows[0]["ThirdAlternatePayeeAge"].ToString()).Value;
            }
            else { DdlA3Age.SelectedIndex = 0; }
            TxtA3NationalID.Text = ds.Tables[0].Rows[0]["ThirdAlternatePayeeNationID"].ToString();
            TxtA3ContactNo.Text = ds.Tables[0].Rows[0]["ThirdAlternatePayeeMobile"].ToString();





            txtName1stAlternatePayeeHN.Text = ds.Tables[0].Rows[0]["alternate_number"].ToString();
            txtName2ndAlternatePayeeHN.Text = ds.Tables[0].Rows[0]["Secondalternate_number"].ToString();
            txtName1st3rdAlternatePayeeHN.Text = ds.Tables[0].Rows[0]["thirdalternate_number"].ToString();

            if (ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
                Session["Photo"] = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
                Session["PhotoName"] = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                dd = dd.Replace(@"\", @"/");
               
                dd = dd.Replace("", "~/");
                Image1.ImageUrl = dd ;
            }
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim();
                Session["Photo1"] = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim();
                Session["PhotoName1"] = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
                dd = dd.Replace(@"\", @"/");
                dd = dd.Replace("D:/Backups/SNSDP Pre TEsting/nikita", "~/");
                lblFileName1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
             //   Image2.ImageUrl = dd + "/" + lblFileName1.Text;
               // Image2.ImageUrl = dd ;
                if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim() != "")
                {
                    Image2.ImageUrl = dd + "/" + lblFileName1.Text;
                }
                else
                {
                 //   Image2.ImageUrl = "http:/54.68.125.132:8181/snsdp/public/attachments/" + ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
                }
            }
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim();
                Session["Photo2"] = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim();
                Session["PhotoName2"] = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
                dd = dd.Replace(@"\", @"/");
                dd = dd.Replace("D:/Backups/SNSDP Pre TEsting/nikita", "~/");
              //  dd = "http://www.snsdpmis.com/Alternatetwo/" + hdfID.Value + "/Household/"; 
                lblFileName2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
              //  Image3.ImageUrl = dd + "/" + lblFileName2.Text;
             //   Image3.ImageUrl = dd;
                string snamep = Session["Photo2"].ToString();
                string sname = Session["PhotoName2"].ToString();
                if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim() != "")
                {
                    Image3.ImageUrl = dd + "/" + lblFileName2.Text;
                }
                else
                {
                 //   Image3.ImageUrl = "http:/54.68.125.132:8181/snsdp/public/attachments/" + ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
                }
            }
            if (ds.Tables[0].Rows[0]["ThirdAlternatepayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["ThirdAlternatepayeePhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["ThirdAlternatePayeePhotoPath"].ToString().Trim();
                Session["Photo4"] = ds.Tables[0].Rows[0]["ThirdAlternatePayeePhotoPath"].ToString().Trim();
                Session["PhotoName4"] = ds.Tables[0].Rows[0]["ThirdAlternatepayeePhotoName"].ToString().Trim();
                dd = dd.Replace(@"\", @"/");
                dd = dd.Replace("D:/Backups/SNSDP Pre TEsting/nikita", "~/");
                //  dd = "http://www.snsdpmis.com/Alternatetwo/" + hdfID.Value + "/Household/"; 
                lblFileName4.Text = ds.Tables[0].Rows[0]["ThirdAlternatepayeePhotoName"].ToString().Trim();
                //  Image3.ImageUrl = dd + "/" + lblFileName2.Text;
                //   Image3.ImageUrl = dd;
                if (ds.Tables[0].Rows[0]["ThirdAlternatePayeePhotoPath"].ToString().Trim() != "")
                {
                    Image4.ImageUrl = dd + "/" + lblFileName4.Text;
                }
                else
                {
                 //   Image4.ImageUrl = "http:/54.68.125.132:8181/snsdp/public/attachments/" + ds.Tables[0].Rows[0]["ThirdAlternatepayeePhotoName"].ToString().Trim();
                }
            }

            //if (ds.Tables[0].Rows[0]["WorkCode"].ToString().Trim() != "")
            //{
            //    BtnSave1.Visible = false;
            //    lblmsg0.Text = "Updation is not possible due to beneficiary has already allocated to work";
            //    success.Visible = false;
            //    Failure.Visible = true;
            //}
            //else

            //{ 
                BtnSave1.Visible = true;
                lblmsg0.Text = "";
                success.Visible = false;
                Failure.Visible = false;
            
           // }
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
                txtName1stAlternatePayeeHN.Enabled = true;
                txtName1stAlternatePayee.Enabled = true;
                DdlA1Gender.Enabled = true;
                DdlA1Age.Enabled = true;
                TxtA1NationalID.Enabled = true;
                TxtA1ContactNo.Enabled = true;
                FileUpload2.Enabled = true;
                txtName2ndAlternatePayeeHN.Enabled = true;
                txtName2ndAlternatePayee.Enabled = true;
                DdlA2Gender.Enabled = true;
                DdlA2Age.Enabled = true;
                TxtA2NationalID.Enabled = true;
                TxtA2ContactNo.Enabled = true;
                FileUpload3.Enabled = true;
                txtName1st3rdAlternatePayeeHN.Enabled = true;
                txtName13rdAlternatePayee.Enabled = true;
                DdlA3Gender.Enabled = true;
                DdlA3Age.Enabled = true;
                TxtA3NationalID.Enabled = true;
                TxtA3ContactNo.Enabled = true;
                FileUpload4.Enabled = true;

                hdfa1.Value = "1";
                hdfa2.Value = "1";
                hdfa3.Value = "1";

                if (ds.Tables[1].Rows.Count > 0)
                { 
                txtName1stAlternatePayeeHN.Enabled = false;
                txtName1stAlternatePayee.Enabled = false;
                DdlA1Gender.Enabled = false;
                DdlA1Age.Enabled = false;
                TxtA1NationalID.Enabled = false;
                TxtA1ContactNo.Enabled = false;
                FileUpload2.Enabled = false ;
                hdfa1.Value = "";
                
                }
                if (ds.Tables[2].Rows.Count > 0)
                {
                    txtName2ndAlternatePayeeHN.Enabled = false;
                    txtName2ndAlternatePayee.Enabled = false;
                    DdlA2Gender.Enabled = false;
                    DdlA2Age.Enabled = false;
                    TxtA2NationalID.Enabled = false;
                    TxtA2ContactNo.Enabled = false;
                    FileUpload3.Enabled = false;
                    hdfa2.Value = "";
                }
                if (ds.Tables[3].Rows.Count > 0)
                {
                    txtName1st3rdAlternatePayeeHN.Enabled = false;
                    txtName13rdAlternatePayee.Enabled = false;
                    DdlA3Gender.Enabled = false;
                    DdlA3Age.Enabled = false;
                    TxtA3NationalID.Enabled = false;
                    TxtA3ContactNo.Enabled = false;
                    FileUpload4.Enabled = false;
                    hdfa3.Value = "";
                }
                if (ds.Tables[4].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[4];
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
                if (ds.Tables[1].Rows.Count > 0 && ds.Tables[2].Rows.Count > 0 && ds.Tables[3].Rows.Count > 0)
                {
                    BtnSave1.Visible = false;
                    success.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = "Three Alternates are already recieved payments , you cannot update any one";
                    //Failure.Visible = true;
                }
            

        }
    }


    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
       
            //hdGroupId.Value = ds.Tables[0].Rows[0]["CountBeneficiary"].ToString();
            if (BtnSave1.Text == "Update")
            {

               

                DataSet dscheckalternate = new DataSet();
                dscheckalternate = Gen.GetBendiciaryalternatechecking(hdfID.Value, txtName1stAlternatePayeeHN.Text, txtName2ndAlternatePayeeHN.Text, txtName1st3rdAlternatePayeeHN.Text);
                if (dscheckalternate.Tables[0].Rows.Count > 0)
                {
                    lblmsg.Text = "";
                    success.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = "Alternate number is already existed in " + dscheckalternate.Tables[0].Rows[0][0].ToString().Trim();


                }
                else
                {
                  string aa= validatiochecking();
                  if (aa != "")
                  {
                       lblmsg.Text = "";
                    success.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = aa;
                  }
                  else
                  {


                      if (hdfa1.Value == "1")
                      {
                          if (txtName1stAlternatePayee.Text != "")
                          {
                              if (hdfahno1.Value.Trim() != txtName1stAlternatePayeeHN.Text.Trim())
                              {
                                  int a = Gen.UpdateAlternateone(hdfID.Value, txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), Session["uid"].ToString().Trim(), txtName1stAlternatePayeeHN.Text);
                              }
                          }
                      }
                      if (hdfa2.Value == "1")
                      {
                          if (txtName2ndAlternatePayee.Text != "")
                          {
                              if (hdfahno2.Value.Trim() != txtName2ndAlternatePayeeHN.Text.Trim())
                              {
                                  int b = Gen.UpdateAlternatetwo(hdfID.Value, txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString(), Session["uid"].ToString().Trim(), txtName2ndAlternatePayeeHN.Text);
                              }
                          }
                      }
                      if (hdfa3.Value == "1")
                      {
                          if (txtName13rdAlternatePayee.Text != "")
                          {
                              if (hdfahno3.Value.Trim() != txtName1st3rdAlternatePayeeHN.Text.Trim())
                              {
                                  int b = Gen.UpdateAlternatethree(hdfID.Value, txtName13rdAlternatePayee.Text, DdlA3Gender.SelectedValue, DdlA3Age.SelectedValue, TxtA3NationalID.Text, TxtA3ContactNo.Text, Session["PhotoFileName4"].ToString(), Session["Photo4"].ToString(), Session["uid"].ToString().Trim(), txtName1st3rdAlternatePayeeHN.Text);
                              }
                          }
                      }

                      success.Visible = true;
                      Failure.Visible = false;
                      lblmsg.Text = "Updated Successfully";
                      Session["PhotoFileName1"] = "";
                      Session["Photo1"] = "";
                      Session["PhotoFileName2"] = "";
                      Session["Photo2"] = "";
                      Session["PhotoFileName3"] = "";
                      Session["Photo3"] = "";
                      clear();
                      Response.Redirect("UpdateAlternatetResult.aspx");
                  }
                }
                          //////////int i = Gen.InsUpdBenDetails(hdfID.Value, ddlIP.SelectedValue, txtHeadName.Text, txtFamilySize.Text, ddlSex.SelectedValue, txtSpouse.Text, ddlNoofAdults.SelectedValue, ddldependents.SelectedValue, txtIncome.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtAddress.Text, txtContactNo.Text, txtEmail.Text, txtHomePhone.Text, ddlIsdisability.SelectedValue, ddlDisability.SelectedValue, txtBiometric.Text, txtBankName.Text, txtAccNo.Text, Session["uid"].ToString(), ddlLegalStatus.SelectedValue, ddlAge.SelectedValue, txtSourceIncome.Text, hdBoma.Value, ddlBeneficiary.SelectedValue, hdGroupNo.Value, txtName1stAlternatePayee.Text, txtName2ndAlternatePayee.Text, ddlNumberMaleAdults.SelectedValue, ddlNumberFemaleAdults.SelectedValue, ddlNumberAdultsAgeAbove65.SelectedValue, ddlNumberAdultsAgeBetween45_64.SelectedValue, ddlNumberAdultsAgeBetween18_30.SelectedValue, ddlNumberAdultsAgeBetween30_45.SelectedValue, ddlNumberFemaleChildren.SelectedValue, ddlNumberMaleChildren.SelectedValue, ddlNumberChildrenAgeAbove5years.SelectedValue, ddlNumberChildrenAgeAbove12years.SelectedValue, ddlNumberChildrenAgeAbove18years.SelectedValue, txtEthnicityTribeRgin.Text, Session["Photo"].ToString(), Session["PhotoFileName"].ToString(), txtNationalID.Text);
                            //////////if (i != 999)
                            //////////{


                            //////////    int a = Gen.UpdateAlternateDetails(i.ToString(), txtName1stAlternatePayee.Text, DdlA1Gender.SelectedValue, DdlA1Age.SelectedValue, TxtA1NationalID.Text, TxtA1ContactNo.Text, Session["PhotoFileName1"].ToString(), Session["Photo1"].ToString(), txtName2ndAlternatePayee.Text, DdlA2Gender.SelectedValue, DdlA2Age.SelectedValue, TxtA2NationalID.Text, TxtA2ContactNo.Text, Session["PhotoFileName2"].ToString(), Session["Photo2"].ToString());
                            //////////    // int j = Gen.InsrtUpdAttachment(i.ToString(), (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), "Appicant Photo", Session["uid"].ToString());

                            //////////    dsENo = Gen.GetEnrollmentNoByintBenid(i.ToString());

                            //////////    if (dsENo.Tables[0].Rows.Count > 0)
                            //////////    {
                            //////////        string EnrollmentNo = dsENo.Tables[0].Rows[0]["EnrollmentNo"].ToString();
                            //////////        lblmsg.Text = "Submitted Successfully..! Your Enrollment No " + EnrollmentNo;
                            //////////    }

                            //////////    success.Visible = true;
                            //////////    Failure.Visible = false;
                            //////////    clear();
                            //////////}
                            //////////else
                            //////////{
                            //////////    lblmsg0.Text = "Already Exist. ";
                            //////////    success.Visible = false;
                            //////////    Failure.Visible = true;
                            //////////}
                           
                        //}
                        
                    
            }
            


        
    }
    protected override void OnPreRender(EventArgs e)
    {

        base.OnPreRender(e);

        StringBuilder javaScript = new StringBuilder();
        javaScript.Append("\n<script language=JavaScript>\n");
        javaScript.Append("window.history.forward(1);\n");
        javaScript.Append("</script>\n");

        Page.RegisterClientScriptBlock("HistoryScript", javaScript.ToString());
    }

    public string validatiochecking()
    {
        string ss = "";
        if (txtNameHN.Text != "")
        {
            if (txtNameHN.Text.Trim().ToLower() == txtName1stAlternatePayeeHN.Text.Trim().ToLower() && txtName1stAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "First Alternate number is same with household number";
                ss = lblmsg0.Text;
           

            }
            if (txtNameHN.Text.Trim().ToLower() == txtName2ndAlternatePayeeHN.Text.Trim().ToLower() && txtName2ndAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Second Alternate number is same with household number";
                ss = lblmsg0.Text;
               

            }
            if (txtNameHN.Text.Trim().ToLower() == txtName1st3rdAlternatePayeeHN.Text.Trim().ToLower() && txtName1st3rdAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Third Alternate number is same with household number";
                ss = lblmsg0.Text;
               

            }
        }
        if (txtName1stAlternatePayeeHN.Text != "")
        {
            if (txtNameHN.Text.Trim().ToLower() == txtName1stAlternatePayeeHN.Text.Trim().ToLower() && txtName1stAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "First Alternate and household number are same";
                ss = lblmsg0.Text;
                

            }
            if (txtName1stAlternatePayeeHN.Text.Trim().ToLower() == txtName2ndAlternatePayeeHN.Text.Trim().ToLower() && txtName2ndAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "First and Second alternate number are same";
                ss = lblmsg0.Text;
                

            }
            if (txtName1stAlternatePayeeHN.Text.Trim().ToLower() == txtName1st3rdAlternatePayeeHN.Text.Trim().ToLower() && txtName1st3rdAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "First and Third alternate number are same";
                ss = lblmsg0.Text;
               

            }

        }

        if (txtName2ndAlternatePayeeHN.Text != "")
        {
            if (txtNameHN.Text.Trim().ToLower() == txtName2ndAlternatePayeeHN.Text.Trim().ToLower() && txtNameHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Second Alternate and household number are same";
                ss = lblmsg0.Text;
               

            }
            if (txtName1stAlternatePayeeHN.Text.Trim().ToLower() == txtName2ndAlternatePayeeHN.Text.Trim().ToLower() && txtName1stAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Second and First alternate number are same";
                ss = lblmsg0.Text;
            

            }
            if (txtName2ndAlternatePayeeHN.Text.Trim().ToLower() == txtName1st3rdAlternatePayeeHN.Text.Trim().ToLower() && txtName1st3rdAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Second and Third alternate number are same";
                ss = lblmsg0.Text;

            }

        }
        if (txtName1st3rdAlternatePayeeHN.Text != "")
        {
            if (txtNameHN.Text.Trim().ToLower() == txtName1st3rdAlternatePayeeHN.Text.Trim().ToLower() && txtNameHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Third Alternate and household number are same";
                ss = lblmsg0.Text;

            }
            if (txtName1stAlternatePayeeHN.Text.Trim().ToLower() == txtName1st3rdAlternatePayeeHN.Text.Trim().ToLower() && txtName1stAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Third and First alternate number are same";
                ss = lblmsg0.Text;

            }
            if (txtName2ndAlternatePayeeHN.Text.Trim().ToLower() == txtName1st3rdAlternatePayeeHN.Text.Trim().ToLower() && txtName1st3rdAlternatePayeeHN.Text.Trim() != "")
            {
                lblmsg.Text = "";
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "Second and Third alternate number are same";
                ss = lblmsg0.Text;

            }

        }
        return ss;
    }
    void clear()
    {
        
        Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
        //lblFileName.Text = "";
        BtnSave1.Text = "Save";
      
        ddlAge.SelectedIndex = 0;
        
        // txtEnrollment.Text="";
        txtHeadName.Text = "";
      
        ddlSex.SelectedIndex = 0;
    //    txtSpouse.Text = "";
       

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
      

        txtName1stAlternatePayee.Text = "";
        txtName2ndAlternatePayee.Text = "";
        
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
        Response.Redirect("UpdationofAlternate.aspx");
    }
    protected void DropDownList25_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }

    protected void BeneficiarySelect(object sender, EventArgs e)
    {
        
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
        int a = Gen.DeleteBenficiary(hdfID.Value,Session["uid"].ToString());
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
       // hdGroupNo.Value = ddlGroupList.SelectedValue.ToString();
        // hdBeneficiaryType.Value = ddlGroupList.SelectedValue.ToString();
    }
}

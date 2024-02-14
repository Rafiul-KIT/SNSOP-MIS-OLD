using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class TSTPhysicalMonitoring : System.Web.UI.Page
{
    //designing and developed by Siva as on 27-02-2016
    //Purpose : To Maintain the Physical Monitoring Details
    //Tables used : tbl_PhysicalMonitoringDet
    //Stored procedures Used :InsertFieldMonitoring


    byte[] Photo = new byte[1];
    string PhotoFileName = "";

    byte[] Photo2 = new byte[1];
    string PhotoFilename2 = "";

    byte[] Photo3 = new byte[1];
    string Photo3Filename = "";

    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (!IsPostBack)
        {
            Session["Photo"] = Photo;
            Session["PhotoFileName"] = "";

            
            Session["Photo2"] = Photo2;
            Session["PhotoFilename2"] = "";

            Session["Photo3"] = Photo3;
            Session["Photo3Filename"] = "";

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
                Session["FileUpload2"] = FileUpload2;
                hdfUploadFile2.Value = FileUpload2.FileName;
            }
            else if (Session["FileUpload2"] != null)
            {
                FileUpload2 = (FileUpload)Session["FileUpload2"];
                hdfUploadFile2.Value = FileUpload2.FileName;
            }


            if (FileUpload3.HasFile)
            {
                Session["FileUpload3"] = FileUpload3;
                hdfUploadFile3.Value = FileUpload3.FileName;
            }
            else if (Session["FileUpload3"] != null)
            {
                FileUpload3 = (FileUpload)Session["FileUpload3"];
                hdfUploadFile3.Value = FileUpload3.FileName;
            }
        }

        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileUpload1.PostedFile))
        {
            if (FileUpload1.PostedFile.ContentLength <= 307200)
            {
                lblmsg.Text = "";                
                UploadImage();
            }
            else
                lblmsg.Text = "File should be less than 300 KB";
        }

        if (FileUpload2.PostedFile != null && FileUpload2.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileUpload2.PostedFile))
        {
            if (FileUpload2.PostedFile.ContentLength <= 307200)
            {
                lblmsg.Text = "";
                UploadImage2();
            }
            else
                lblmsg.Text = "File should be less than 300 KB";
        }
        if (FileUpload3.PostedFile != null && FileUpload3.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileUpload3.PostedFile))
        {
            if (FileUpload3.PostedFile.ContentLength <= 307200)
            {
                lblmsg.Text = "";
                UploadImage3();
            }
            else
                lblmsg.Text = "File should be less than 300 KB";
        }


        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            //Failure.Visible = false;
            success.Visible = false;
            FillDetails();

            BtnSave1.Text = "Update";
        }
           
    }

    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
            
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            

            DataSet dsBen = new DataSet();
            dsBen = Gen.GetBomawiseBeneficiary(ds.Tables[0].Rows[0]["intBomasid"].ToString());
            
            if (ds.Tables[1].Rows.Count > 0)
            {
                gvpractical0.DataSource = ds.Tables[1];
                gvpractical0.DataBind();
            }

           
        }
    }    


    void UploadImage()
    {       
        Session["PhotoFileName"] = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);

        try
        {
            string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;
            
            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {               

                Session["Photo"] = new byte[FileUpload1.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpload1.PostedFile;
                
                Image.InputStream.Read((byte[])Session["Photo"], 0, (int)FileUpload1.PostedFile.ContentLength);
                lblFileName.Text = Session["PhotoFileName"].ToString();
                Image1.ImageUrl = "ScanImage.aspx";
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


        FileUpload1.Focus();

    }


    void UploadImage2()
    {
        Session["PhotoFilename2"] = System.IO.Path.GetFileName(FileUpload2.PostedFile.FileName);

        try
        {
            string[] fileType = FileUpload2.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;

            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {

                Session["Photo2"] = new byte[FileUpload2.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpload2.PostedFile;

                Image.InputStream.Read((byte[])Session["Photo2"], 0, (int)FileUpload2.PostedFile.ContentLength);
                lblFileName2.Text = Session["PhotoFilename2"].ToString();
                Image2.ImageUrl = "ScanImage2.aspx";
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


        FileUpload2.Focus();

    }


    void UploadImage3()
    {
        Session["Photo3Filename"] = System.IO.Path.GetFileName(FileUpload3.PostedFile.FileName);

        try
        {
            string[] fileType = FileUpload3.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;

            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {

                Session["Photo3"] = new byte[FileUpload3.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpload3.PostedFile;

                Image.InputStream.Read((byte[])Session["Photo3"], 0, (int)FileUpload3.PostedFile.ContentLength);
                lblFileName3.Text = Session["Photo3Filename"].ToString();
                Image3.ImageUrl = "ScanImage3.aspx";
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


        FileUpload3.Focus();

    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int i = Gen.InsertFieldMonitoring(hdfID.Value, txtStartDate.Text, ddlOutcome.SelectedValue, txtRemarks.Text, (byte[])(Session["Photo"]), Session["PhotoFileName"].ToString().Trim(), (byte[])(Session["Photo2"]), Session["PhotoFilename2"].ToString().Trim(), (byte[])(Session["Photo3"]), Session["Photo3Filename"].ToString().Trim(), Session["uid"].ToString());
        if (i >0)
        {
            lblmsg.Text = "Submited Successfully..!";
            success.Visible = true;
            //Failure.Visible = false;
            clear();

        }
    }

    void clear()
    {
        Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
        Image2.ImageUrl = "~/Resource/Images/not-available.jpg";
        Image3.ImageUrl = "~/Resource/Images/not-available.jpg";
        lblFileName.Text = "";
        lblFileName2.Text = "";
        lblFileName3.Text = "";
         txtStartDate.Text="";
        ddlOutcome.SelectedIndex=0;

        gvpractical0.DataSource = null;
        gvpractical0.DataBind();

        txtRemarks.Text = "";
        lblProject.Text = "";
        lblWorkCode.Text = "";
        lblBoma.Text = "";
        Session.Remove("PhotoFileName");
        Session.Remove("PhotoFileName2");
        Session.Remove("Photo3FileName");

        Session.Remove("Photo");
        Session.Remove("Photo2");
        Session.Remove("Photo3");
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
}

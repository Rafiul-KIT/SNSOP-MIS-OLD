using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Data.SqlClient;

public partial class viewAttachemnt : System.Web.UI.Page
{
    SqlDataReader drHeads = null;
    DB.DB Dcon = new DB.DB();
    General t1 = new General();
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
            Response.Redirect("frmuserlogin.aspx");

        if (!Page.IsPostBack)
        {
            FillbinaryData();
        }
    }
    protected void FillbinaryData()
    {
        string searchQuery = "";
        if (Request.QueryString[1].ToString() == "Appicant Photo")
        {
            searchQuery = " select PWImages.dbo.tbl_Attachements.Photo as scanImage,PWImages.dbo.tbl_Attachements.PhotoFileName as fileName from PWImages.dbo.tbl_Attachements where PWImages.dbo.tbl_Attachements.Type like 'Appicant Photo' and  PWImages.dbo.tbl_Attachements.intTypeId = '" + Request.QueryString[0].ToString() + "'";
        }
        else if (Request.QueryString[1].ToString() == "TST Photo")
        {
            searchQuery = " select PWImages.dbo.tbl_Attachements.Photo as scanImage,PWImages.dbo.tbl_Attachements.PhotoFileName as fileName from PWImages.dbo.tbl_Attachements where PWImages.dbo.tbl_Attachements.Type like 'TST Photo' and  PWImages.dbo.tbl_Attachements.intTypeId = '" + Request.QueryString[0].ToString() + "'";
        }
        else if (Request.QueryString[1].ToString() == "Work Progress")
        {
            searchQuery = " select PWImages.dbo.tbl_Attachements.Photo as scanImage,PWImages.dbo.tbl_Attachements.PhotoFileName as fileName from PWImages.dbo.tbl_Attachements where PWImages.dbo.tbl_Attachements.Type like 'Work Progress' and  PWImages.dbo.tbl_Attachements.intTypeId = '" + Request.QueryString[0].ToString() + "'";
        }
     
        try
        {
            //Jyotshna On 19-09-2013 to show the image in Google chrome also
            UploadedFile objUploadedFile = new UploadedFile();
            //Jyotshna On 19-09-2013 to show the image in Google chrome also

            Dcon.OpenConnection();
            
           // drHeads = t1.ExecuteDReader(searchQuery, Dcon.GetConnection);
            drHeads = t1.ExecuteDReader(searchQuery, Dcon.GetConnection);
           
            
            while (drHeads.Read())
            {
                Response.ClearContent();
                Response.ClearHeaders();
                if (drHeads["scanImage"].ToString() != "")   
                {

                    string[] contentType = drHeads["fileName"].ToString().Split('.');

                    int i = contentType.Length;
                    switch (contentType[i - 1].ToUpper().Trim())
                    {
                        //case "JPG":
                        //    Response.ContentType = "application/jpg";
                        //    Response.AddHeader("content-disposition", "attachment;filename=Tr.jpg");
                        //    break;
                        //case "JPEG":
                        //    Response.ContentType = "application/jpeg";
                        //    Response.AddHeader("content-disposition", "attachment;filename=Tr.jpeg");
                        //    break;
                        //case "GIF":
                        //    Response.ContentType = "application/gif";
                        //    Response.AddHeader("content-disposition", "attachment;filename=Tr.gif");
                        //    break;
                        //case "PNG":
                        //    Response.ContentType = "application/png";
                        //    Response.AddHeader("content-disposition", "attachment;filename=Tr.png");
                        //    break;
                        //Jyotshna On 19-09-2013 to show the image in Google chrome also
                        case "JPG":
                            objUploadedFile.FileBytes = (byte[])drHeads["scanImage"];
                            objUploadedFile.FileName = drHeads["fileName"].ToString();
                            Session["UploadedFile"] = objUploadedFile;
                            img.ImageUrl = "ImagePreview.aspx";
                            img.Visible = true;
                            break;
                        case "jpg":
                            objUploadedFile.FileBytes = (byte[])drHeads["scanImage"];
                            objUploadedFile.FileName = drHeads["fileName"].ToString();
                            Session["UploadedFile"] = objUploadedFile;
                            img.ImageUrl = "ImagePreview.aspx";
                            img.Visible = true;
                            break;
                        case "JPEG":
                            objUploadedFile.FileBytes = (byte[])drHeads["scanImage"];
                            objUploadedFile.FileName = drHeads["fileName"].ToString();
                            Session["UploadedFile"] = objUploadedFile;
                            img.ImageUrl = "ImagePreview.aspx";
                            img.Visible = true;
                            break;
                        case "GIF":
                            objUploadedFile.FileBytes = (byte[])drHeads["scanImage"];
                            objUploadedFile.FileName = drHeads["fileName"].ToString();
                            Session["UploadedFile"] = objUploadedFile;
                            img.ImageUrl = "ImagePreview.aspx";
                            img.Visible = true;
                            break;
                        case "PNG":
                            objUploadedFile.FileBytes = (byte[])drHeads["scanImage"];
                            objUploadedFile.FileName = drHeads["fileName"].ToString();
                            Session["UploadedFile"] = objUploadedFile;
                            img.ImageUrl = "ImagePreview.aspx";
                            img.Visible = true;
                            break;
                        //Jyotshna On 19-09-2013 to show the image in Google chrome also

                       default:

                            break;
                    }
                    //Jyotshna On 19-09-2013 to show the image in Google chrome also
                    //if (contentType[i - 1].ToUpper().Trim() != "JPG" && contentType[i - 1].ToUpper().Trim() != "JPEG" && contentType[i - 1].ToUpper().Trim() != "GIF" && contentType[i - 1].ToUpper().Trim() != "PNG")
                    //{
                        byte[] buffer = null;
                        buffer = (byte[])drHeads["ScanImage"];
                        //Response.AddHeader("Content-Length", buffer.Length.ToString());
                        Response.Charset = "";
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.BinaryWrite(buffer);
                        Response.End();
                    //}

                 
                }
                else
                    Response.Write("No Data with Specified Request");
                
            }
            
        }
        catch (Exception ex)
        {
            //lblResult.Text = ex.ToString();
        }
        finally
        {
            drHeads.Close();
            Dcon.CloseConnection();
            
        }

    }
   
}

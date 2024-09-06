using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class UploadPaymentCycleDocuments : System.Web.UI.Page
{
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    int maxSize = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("~/frmUserLogin.aspx");
        }

        if (!IsPostBack)
        {
            Session["document_1_file_name"] = "";
            Session["document_2_file_name"] = "";
            Session["document_3_file_name"] = "";
            getPaymentCycle();
        }

        maxSize = Convert.ToInt32(ConfigurationManager.AppSettings["PaymentCycleDocumentMaxSizeInByte"].ToString());

        UploadFile(fuDocument1);
        UploadFile(fuDocument2);
        UploadFile(fuDocument3);
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (Session["document_1_file_name"] == null || Session["document_1_file_name"].ToString() == "")
        {
            success.Visible = false;
            Failure.Visible = true;
            lblmsg0.Text = "Need to upload file";
            return;
        }

        string pcYear = ddlYear.SelectedValue;
        int pc = Convert.ToInt32(ddlPaymentCycle.SelectedValue);
        int userId = Convert.ToInt32(Session["uid"].ToString());

        string doc1 = (Session["document_1_file_name"] != null && Session["document_1_file_name"].ToString() != "")
            ? Session["document_1_file_name"].ToString() : null;

        string doc2 = (Session["document_2_file_name"] != null && Session["document_2_file_name"].ToString() != "")
            ? Session["document_2_file_name"].ToString() : null;

        string doc3 = (Session["document_3_file_name"] != null && Session["document_3_file_name"].ToString() != "")
            ? Session["document_3_file_name"].ToString() : null;

        try
        {
            int r = Gen.savePaymentCycleDocDetails(pcYear, pc, doc1, doc2, doc3, userId);

            if (r == 0)
            {
                success.Visible = true;
                lblmsg.Text = "Saved Successfully";
                Failure.Visible = false;

                ddlYear.SelectedIndex = -1;
                ddlPaymentCycle.SelectedIndex = -1;
                Session["document_1_file_name"] = null;
                lblFUDocument1.Text = null;
                Session["document_2_file_name"] = null;
                lblFUDocument2.Text = null;
                Session["document_3_file_name"] = null;
                lblFUDocument3.Text = null;
            } else
            {
                Failure.Visible = true;
                lblmsg0.Text = "Could not save successfully";
                success.Visible = false;
            }
        } catch(Exception exc)
        {
            Console.WriteLine(exc.ToString());
        }
    }

    private void getPaymentCycle()
    {
        DataSet dsPaymentCycle = new DataSet();
        dsPaymentCycle = Gen.getPaymentCycle();
        if (dsPaymentCycle.Tables[0].Rows.Count > 0)
        {
            ddlPaymentCycle.DataSource = dsPaymentCycle.Tables[0];
            ddlPaymentCycle.DataTextField = "WagePaymentCycle";
            ddlPaymentCycle.DataValueField = "intPaymentCycleID";
            ddlPaymentCycle.DataBind();
        }
    }

    public void UploadFile(FileUpload fileUpload)
    {
        string PhotoFilePath = "";
        string PhotoFileName = "";
        string fullSavePath = "";

        string parentFilePath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"].ToString();

        if ((fileUpload.PostedFile != null) && (fileUpload.PostedFile.ContentLength > 0) && !Gen.IsExecutable(fileUpload.PostedFile))
        {
            if (fileUpload.PostedFile.ContentLength > maxSize)
            {
                Failure.Visible = true;
                lblmsg0.Text = "Attachment size too big";
                return;
            } else
            {
                Failure.Visible = false;
                lblmsg0.Text = "";
            }

            //determine file name
            string sFileName = System.IO.Path.GetFileName(fileUpload.PostedFile.FileName);
            string fileName = "";
            try
            {
                string[] fileType = fileUpload.PostedFile.FileName.Split('.');
                int i = fileType.Length;

                if (fileUpload == fuDocument1)
                {
                    fileName = "File_1";
                }
                else if (fileUpload == fuDocument2)
                {
                    fileName = "File_2";
                }
                else if (fileUpload == fuDocument3)
                {
                    fileName = "File_3";
                }

                string newPath = "";
                if (ddlYear != null && ddlYear.SelectedValue != null && ddlPaymentCycle != null && ddlPaymentCycle.SelectedValue != null)
                {
                    newPath = System.IO.Path.Combine(parentFilePath, ddlYear.SelectedValue.ToString());
                    newPath = Path.Combine(newPath, "Payment_Cycle_" + ddlPaymentCycle.SelectedValue.ToString());
                    newPath = Path.Combine(newPath, fileName);
                }
                else
                {
                    return;
                }

                PhotoFilePath = parentFilePath + ddlYear.SelectedValue.ToString() + "\\Payment_Cycle_" + ddlPaymentCycle.SelectedValue.ToString() + "\\";

                PhotoFileName = sFileName;
                // Create the subfolder
                if (!Directory.Exists(newPath))
                {
                    System.IO.Directory.CreateDirectory(newPath);
                } else
                {
                    System.IO.Directory.Delete(newPath, true);
                    System.IO.Directory.CreateDirectory(newPath);
                }

                fullSavePath = newPath + "\\" + sFileName;

                System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                int count = dir.GetFiles().Length;
                //if (count == 0)
                //{
                fileUpload.PostedFile.SaveAs(fullSavePath);
                //}
                //else
                //{
                //    if (count == 1)
                //    {
                //        string[] Files = Directory.GetFiles(newPath);

                //        fileUpload.PostedFile.SaveAs(newPath + "\\" + fileName);
                //    }
                //}

                if (fileUpload == fuDocument1)
                {
                    Session["document_1_file_name"] = PhotoFileName;
                    lblFUDocument1.Text = Session["document_1_file_name"].ToString();
                }
                else if (fileUpload == fuDocument2)
                {
                    Session["document_2_file_name"] = PhotoFileName;
                    lblFUDocument2.Text = Session["document_2_file_name"].ToString();
                }
                else if (fileUpload == fuDocument3)
                {
                    Session["document_3_file_name"] = PhotoFileName;
                    lblFUDocument3.Text = Session["document_3_file_name"].ToString();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                DeleteFile(fullSavePath);
            }
        }
    }

    public void DeleteFile(string strFileName)
    {
        if (strFileName.Trim().Length > 0)
        {
            FileInfo fi = new FileInfo(strFileName);
            if (fi.Exists)
            {
                fi.Delete();
            }
        }
    }
}
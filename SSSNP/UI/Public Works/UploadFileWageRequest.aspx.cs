using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class UploadFileWageRequest : System.Web.UI.Page
    {
        General Gen = new General();
        DataTable dt = new DataTable();


        private int _wageRequestID;

        private int WageRequestID
        {
            get
            {
                if (Request.QueryString["id"] == null) return 0;
                int result = 0;
                int.TryParse(Request.QueryString["id"], out result);

                return result;

            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (WageRequestID == 0)
            {
                btnUpload.Enabled = false;
                FUFile.Enabled = false;
                imgSuccess.Visible = false;
                txtDescription.Enabled = false;
                txtErrorMessage.Visible = false;

            }
            else
            {
                btnUpload.Enabled = true;
                FUFile.Enabled = true;
             
                if (FUFile.HasFile) imgSuccess.Visible = true;
              
            }
        }

      

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                if (!FUFile.HasFile)
                {
                    txtErrorMessage.Text = "Please, select a file to upload";
                    txtErrorMessage.Visible = true;
                    return;
                }

                txtErrorMessage.Text = "";
                txtErrorMessage.Visible = false;

                bool uploadResult = UploadFile();

                if (!uploadResult)
                {
                    imgSuccess.Visible = false;
                    return;
                }
                if (FUFile.HasFile) imgSuccess.Visible = true;

                if (uploadResult && dt.Rows.Count != 0)
                {
                    Gen.bulkInsertdrInterDetails(dt);

                    txtErrorMessage.Text = "File uploaded Successfully";
                    txtErrorMessage.ForeColor = Color.Green;
                    txtErrorMessage.Visible = true;
                    
                    dt = new DataTable();
                    FUFile = null;
                    txtDescription.Text = "";
                    lblFileName.Text = "";

                    //btnUpload.Enabled = false;
                    //FUFile.Enabled = false;
                    //txtDescription.Enabled = false;
                }
            }
            catch (Exception ex)
            {

                txtErrorMessage.Text = ex.Message;
                txtErrorMessage.Visible = true;
            }


        }

        public bool UploadFile()
        {
            string LeaseFilePath = "";
            string LeaseFileName = "";
            bool result = false;


            string sFileDir6 = Server.MapPath("~\\WageFile");
            if ((FUFile.PostedFile != null) && (FUFile.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FUFile.PostedFile))
            {
                string sFileName = System.IO.Path.GetFileName(FUFile.PostedFile.FileName);
                try
                {
                    string[] fileType = FUFile.PostedFile.FileName.Split('.');
                    int i = fileType.Length;
                    if (fileType[i - 1].ToUpper().Trim() == "PDF" || fileType[i - 1].ToUpper().Trim() == "DOC" || fileType[i - 1].ToUpper().Trim() == "DOCX" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG" || fileType[i - 1].ToUpper().Trim() == "PNG")
                    {
                        string newPath = "";
                        if (Session.Count > 0)
                        {
                            newPath = System.IO.Path.Combine(sFileDir6, Session["uid"].ToString());
                        }
                        else
                        {
                            newPath = System.IO.Path.Combine(sFileDir6, "1000");
                        }

                        newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                        LeaseFilePath = newPath.ToString();
                        LeaseFileName = sFileName;
                        // Create the subfolder
                        if (!Directory.Exists(newPath))
                            System.IO.Directory.CreateDirectory(newPath);
                        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                        int count = dir.GetFiles().Length;
                        if (count == 0)
                            FUFile.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        else
                        {
                            if (count == 1)
                            {
                                string[] Files = Directory.GetFiles(newPath);

                                FUFile.PostedFile.SaveAs(newPath + "\\" + sFileName);
                            }
                        }

                        lblFileName.Text = LeaseFileName;

                        dt.Columns.Add("intWagePaymentReqid", typeof(int));
                        dt.Columns.Add("Description", typeof(string));
                        dt.Columns.Add("PhotoFileName", typeof(string));
                        dt.Columns.Add("PhotoFilePath", typeof(string));
                        dt.Columns.Add("Created_by", typeof(int));
                        dt.Columns.Add("Created_dt", typeof(DateTime));

                        int uid = 0;
                        int.TryParse(Session["uid"].ToString(), out uid);

                        DataRow row = dt.NewRow();
                        row["intWagePaymentReqid"] = WageRequestID;
                        row["Description"] = txtDescription.Text;
                        row["PhotoFileName"] = LeaseFileName;
                        row["PhotoFilePath"] = LeaseFilePath;
                        row["Created_by"] = uid;
                        row["Created_dt"] = DateTime.Now;

                        dt.Rows.Add(row);

                        return true;


                    }
                    else
                    {
                        lblFileName.Text = "";
                        txtErrorMessage.Text = "Wrong file format. Please choose a PDF, DOC, DOCX, PNJ, JPG, JPEG";
                        txtErrorMessage.ForeColor = Color.Red;
                        txtErrorMessage.Visible = true;
                        return false;

                    }
                }
                catch (Exception ex) //in case of an error
                {
                    lblFileName.Text = "";
                      return false;
                    // DeleteFile(sFileDir + sFileName);
                }
            }

            return result;

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "close", "CloseModal();", true);

        }
    }
}
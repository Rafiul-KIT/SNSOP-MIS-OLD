using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.UI.Public_Works
{
    public partial class FSPInvoiceUpload : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Clear message and preview link on initial load (not postback)
            if (!IsPostBack)
            {
                lblMessage.Text = "";
                hlPreview.Visible = false;
            }
        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            if (fileUpload.HasFile)
            {
                string fileExtension = Path.GetExtension(fileUpload.FileName).ToLower();
                string[] allowedExtensions = { ".pdf", ".doc", ".docx", ".xls", ".xlsx", ".txt", ".jpg", ".jpeg", ".png", ".xml", ".csv" };

                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    try
                    {
                        string fileName = Path.GetFileName(fileUpload.FileName);
                        string filePath = @"C:\xampp\htdocs\snsop_docs\invoice\" + fileName;

                        // Ensure the directory exists
                        Directory.CreateDirectory(@"C:\xampp\htdocs\snsop_docs\invoice\");

                        // Save the file to the specified path
                        fileUpload.SaveAs(filePath);

                        // Display success message and preview link
                        lblMessage.Text = "Invoice uploaded successfully!";
                        lblMessage.CssClass = "text-success";

                        string virtualPath = $"/snsop_docs/invoice/{HttpUtility.UrlEncode(fileName)}";
                        hlPreview.NavigateUrl = virtualPath;
                        hlPreview.Text = "View Uploaded Invoice";
                        hlPreview.Visible = true;

                    }
                    catch (Exception ex)
                    {
                        lblMessage.Text = "Invoice upload failed: " + ex.Message;
                        lblMessage.CssClass = "text-danger";
                    }
                }
                else
                {
                    lblMessage.Text = "Invalid file type. Please upload PDF, DOC, DOCX, XLS, XLSX, TXT, JPG, JPEG, PNG, XML, or CSV files only.";
                    lblMessage.CssClass = "text-warning";
                }
            }
            else
            {
                lblMessage.Text = "Please select an invoice file to upload.";
                lblMessage.CssClass = "text-warning";
            }
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            // Clear file upload control and message label
            fileUpload.Attributes.Clear();
            lblMessage.Text = "";
            hlPreview.Visible = false;
        }
    }
}

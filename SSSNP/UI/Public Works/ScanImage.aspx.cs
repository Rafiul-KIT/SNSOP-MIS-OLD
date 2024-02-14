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

public partial class ScanImage : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        byte[] myimage = (byte[])Session["Photo"];
        string filename = Session["PhotoFileName"].ToString();
        FillbinaryData(myimage, filename);

            
    }

    protected void FillbinaryData(byte[] image, string Filename)
    {
        try
        {
            // Dcon.OpenConnection();

            // drHeads = t1.ExecuteDReader(searchQuery, Dcon.GetConnection);
            // drHeads = t1.ExecuteDReader(searchQuery, Dcon.GetConnection);

            // while (drHeads.Read())
            //{
            //  Response.ClearContent();
            // Response.ClearHeaders();
            if (image != null)
            {
                string[] contentType = Filename.ToString().Split('.');

                int i = contentType.Length;
                switch (contentType[i - 1].ToUpper().Trim())
                {
                    case "PDF":
                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=Tr.pdf");
                        break;
                    case "DOC":
                        Response.ContentType = "application/vnd.ms-word";
                        Response.AddHeader("content-disposition", "attachment;filename=Tr.doc");
                        break;
                    case "DOCX":
                        Response.ContentType = "application/vnd.ms-word";
                        Response.AddHeader("content-disposition", "attachment;filename=Tr.doc");
                        break;
                    case "XLS":
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("content-disposition", "attachment;filename=Tr.xls");
                        break;
                    case "XLSX":
                        Response.ContentType = "application/vnd.ms-excel";
                        Response.AddHeader("content-disposition", "attachment;filename=Tr.xls");
                        break;

                    default:

                        break;
                }
                byte[] buffer = null;
                buffer = (byte[])image;
                Response.Charset = "";
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Response.BinaryWrite(buffer);
                Response.End();
            }
            else
                Response.Write("No Data with Specified Request");

            //}

        }
        catch (Exception ex)
        {
            //lblmsg.Text = ex.ToString();
            Response.Write(ex.Message);
        }
        finally
        {
            //drHeads.Close();
            //Dcon.CloseConnection();

        }
    }
}

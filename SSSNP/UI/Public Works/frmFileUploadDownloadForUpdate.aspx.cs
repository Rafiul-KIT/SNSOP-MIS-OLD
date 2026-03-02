using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
public partial class frmFileUploadDownloadForUpdate : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

        string filePath = Server.MapPath(@"~/template/");

        string fileName = "templateupdate.xlsx";
        string fullpath = filePath + "" + fileName;
        fullpath = fullpath.Replace('\\', '/');
        if (File.Exists(fullpath))
        {

            Response.Clear();
            Response.AddHeader("content-disposition", "attachment;filename=" + fileName);
            Response.WriteFile(fullpath);
            Response.End();
        }
        else
        {

        }

    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP
{
    public partial class PhotoEditorTool : System.Web.UI.Page
    {
        private string _filename = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            string filename = Request.QueryString["filename"] ?? "";
            this._filename = filename;

            if (!Page.IsPostBack)
            {

                if (!string.IsNullOrEmpty(filename))
                {
                    RadImageEditor1.ImageUrl = filename; 
                }
                
                                
            }
            
        }
            
        protected void RadImageEditor1_ImageSaving(object sender, ImageEditorSavingEventArgs args)
        {

            string destinationFile = Server.MapPath(this._filename);
   
            Telerik.Web.UI.ImageEditor.EditableImage img = args.Image;
            img.Image.Save(destinationFile);
            args.Argument = String.Format("The image is saved under the name: <strong>{0}</strong>.", destinationFile);

            args.Cancel = true;

        }


    }
}
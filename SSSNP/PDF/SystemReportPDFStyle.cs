using iTextSharp.text;
using SSSNP_SNSOP.Core.BuilderPdfGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP_SNSOP.PDF
{
    public class SystemReportPDFStyle: BuilderPdfStyle
    {
        //public string Image_LogoPath { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\Resource\Images\logo_header.png"); } }
        public string Image_LogoPath { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\Resource\Images\SNSOP_Logo.png"); } }

        public iTextSharp.text.Font fontSubTitle { get { return FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLUE); } }
        public iTextSharp.text.Font fontTextBlackBold { get { return FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font fontTextBlack { get { return FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font fontTextBlackBoldSmall { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font fontTextBlackSmall { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font fontTextGrayBoldSmall { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, BaseColor.GRAY); } }
        public iTextSharp.text.Font fontTextGraySmall { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL, BaseColor.GRAY); } }
        public iTextSharp.text.Font fontTextGreenSmall { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL, BaseColor.GREEN); } }
        public iTextSharp.text.Font fontTextBlueSmall { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL, BaseColor.BLUE); } }

    }
}
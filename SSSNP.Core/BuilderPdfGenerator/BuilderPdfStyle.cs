using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SSSNP_SNSOP.Core.BuilderPdfGenerator
{
    public class BuilderPdfStyle : iTextSharp.text.Font
    {
        public string Title_UN { get { return "United Nations"; } }
        public string Image_Header { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\Resource\images\arrowright3.png"); } }
        public string Image_CheckedControl { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\ATS\Images\BuilderPdfImages\CheckedControlIcon.png"); } }
        public string Image_UnCheckedControl { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\ATS\Images\BuilderPdfImages\UncheckedControlIcon.png"); } }
        public string Image_RadioButtonCheckedControl { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\ATS\Images\BuilderPdfImages\RBCheckedControlIcon.png"); } }
        public string Image_RadioButtonUnCheckedControl { get { return System.Web.HttpContext.Current.Server.MapPath(@"~\ATS\Images\BuilderPdfImages\RBUnCheckedControlIcon.png"); } }
        public float TitleNodeSpacingBefore { get { return 5f; } }
        public float PaddingLeftTableFirstCell { get { return 13f; } }
        public float PaddingTopTableFirstCell { get { return 3f; } }


        public iTextSharp.text.Font Font_Helvetica_6_Black { get { return FontFactory.GetFont("HELVETICA", 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_8_Black { get { return FontFactory.GetFont("HELVETICA", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_10_Black { get { return FontFactory.GetFont("HELVETICA", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_12_Black { get { return FontFactory.GetFont("HELVETICA", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_14_Black { get { return FontFactory.GetFont("HELVETICA", 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }

        public iTextSharp.text.Font Font_Helvetica_6_Black_Bold { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_8_Black_Bold { get { return FontFactory.GetFont("HELVETICA", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_10_Black_Bold { get { return FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_12_Black_Bold { get { return FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Helvetica_14_Black_Bold { get { return FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }

        public iTextSharp.text.Font Font_Helvetica_8_Black_Bold_TitleHeader { get { return FontFactory.GetFont("HELVETICA", 8, iTextSharp.text.Font.BOLD + iTextSharp.text.Font.UNDERLINE, BaseColor.BLACK); } }

        public iTextSharp.text.Font Font_Arial_6_Black { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_8_Black { get { return FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_8_Red { get { return FontFactory.GetFont("ARIAL", 8, iTextSharp.text.Font.NORMAL, BaseColor.RED); } }
        public iTextSharp.text.Font Font_Arial_10_Black { get { return FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_12_Black { get { return FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_14_Black { get { return FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.NORMAL, BaseColor.BLACK); } }


        public iTextSharp.text.Font Font_Arial_6_Black_Bold { get { return FontFactory.GetFont("ARIAL", 6, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_8_Black_Bold { get { return FontFactory.GetFont("HELVETICA", 8, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_10_Black_Bold { get { return FontFactory.GetFont("ARIAL", 10, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_12_Black_Bold { get { return FontFactory.GetFont("ARIAL", 12, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }
        public iTextSharp.text.Font Font_Arial_14_Black_Bold { get { return FontFactory.GetFont("ARIAL", 14, iTextSharp.text.Font.BOLD, BaseColor.BLACK); } }

        public iTextSharp.text.Font Font_Helvetica_8_Black_Bold_TitleNode { get { return FontFactory.GetFont("HELVETICA", 8, iTextSharp.text.Font.BOLD, BaseColor.WHITE); } }


    }
}

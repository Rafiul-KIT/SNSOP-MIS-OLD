using SSSNP_SNSOP.Core.BuilderPdfGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SSSNP_SNSOP.PDF
{
    public class SystemReportGenerate : BuilderPdfGenerator, IBuilderPdfGenerator
    {
        private SystemReportPDFStyle cStyle = new SystemReportPDFStyle();

        private bool _isELRP;
        private int _year;
        private int _month;

        public SystemReportGenerate()
        {

        }

        public SystemReportGenerate(bool isELRP, int year, int month)
        {
            _isELRP = isELRP;
            _year = year;
            _month = month;
        }

        public bool CreatePdfDocument()
        {
            base.CreateDocument();
            return true;

        }
        public MemoryStream DownloadPdfDocument()
        {

            return this.GenerateDocumentToMemoryStream();

        }
        public byte[] DownloadPdfDocumentByte()
        {
            return this.GenerateDocumentToMemoryStream().ToArray();
        }


        // Override Methods
        protected override void HeaderSectionDocument()
        {
            HeaderDocument();
        }
        protected override void BodySectionDocument()
        {

            ExecuteTemplateContainer();

        }

        // Custom Methods
        private void ExecuteTemplateContainer()
        {
 
                SystemReportDesign container = new SystemReportDesign(this._isELRP, this._year, this._month);

                // Add Elements Pdf Document
                AddEach(container.CreateDocument());
                      
        }
     
        // Private Methods
        private void HeaderDocument()
        {

            Add(new BuilderPdfTemplate().CreateLogoHeader(cStyle.Image_LogoPath));


        }
    }
}
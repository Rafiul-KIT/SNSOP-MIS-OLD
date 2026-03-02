using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.BuilderPdfGenerator
{
    public class BuilderPdfGenerator
    {
        private Dictionary<string, IElement> _documentElements;

        public BuilderPdfGenerator()
        {
            _documentElements = new Dictionary<string, IElement>();

        }
        protected void CreateDocument()
        {

            // Common Style Initialize
            InitPdfCommonStyle();

            // Header Section
            HeaderSectionDocument();

            // Body Section
            BodySectionDocument();

            // Footer Section
            FooterSectionDocument();

        }

        // Generate MemoryStream Document
        protected System.IO.MemoryStream GenerateDocumentToMemoryStream()
        {

            using (MemoryStream ms = new MemoryStream())
            using (Document document = new Document(PageSize.A4, 20, 10, 10, 40))
            using (PdfWriter writer = PdfWriter.GetInstance(document, ms))
            {

                document.Open();

                foreach (KeyValuePair<string, IElement> e in this._documentElements)
                {

                    document.Add(e.Value);

                }
                document.Close();
                writer.Close();
                ms.Close();

                return ms;

            }

        }

        // Virtual Methods
        protected virtual void InitPdfCommonStyle()
        {
            //throw new NotImplementedException();
        }
        protected virtual void HeaderSectionDocument()
        {
            // do nothing
        }
        protected virtual void BodySectionDocument()
        {
            //  throw new NotImplementedException();
        }
        protected virtual void FooterSectionDocument()
        {
            // do nothing
        }


        protected void Add(IElement e)
        {
            this.Add(e, string.Format("elementDocument_{0}", this._documentElements.Count.ToString()));
        }
        protected void Add(IElement e, string elementName)
        {

            this._documentElements.Add(elementName, e);

        }
        protected void AddEach(Dictionary<string, IElement> elements)
        {
            foreach (string k in elements.Keys)
            {

                this.Add(elements[k], k);
            }

        }
        protected bool Remove(string elementName)
        {
            if (string.IsNullOrEmpty(elementName)) return false;
            return this._documentElements.Remove(elementName);
        }
        protected Dictionary<string, IElement> GetElementsDocument()
        {
            return this._documentElements;
        }
    }

    [Serializable]
    public class LabelProperty
    {
        public string LabelName { get; set; }
        public string LabelValue { get; set; }
        public IFilterCondition FilterCondition { get; set; }
        public string ReferenceCondition { get; set; }

    }

}

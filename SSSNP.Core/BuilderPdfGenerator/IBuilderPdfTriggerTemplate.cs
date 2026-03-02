using iTextSharp.text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.BuilderPdfGenerator
{
    public interface IBuilderPdfTriggerTemplate
    {
        string Trigger_Reference_Id { get; }
        Dictionary<string, IElement> CreateDocument();
    }
}

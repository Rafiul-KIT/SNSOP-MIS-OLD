using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.BuilderPdfGenerator
{
    public interface IBuilderPdfGenerator
    {
        bool CreatePdfDocument();
        MemoryStream DownloadPdfDocument();

    }
}

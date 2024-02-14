using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
    [Serializable]
    public class GBVImportFile
    {
        public int ImportedID { get; set; }
        public DateTime CreationTimestamp { get; set; }
        public int UserID { get; set; }
        public string SourceFilename { get; set; }
        public string DestinationFilename { get; set; }
        public byte[] UploadedFile { get; set; }
        public string GBVType { get; set; }
        public bool GBVImported { get; set; }


    }
}

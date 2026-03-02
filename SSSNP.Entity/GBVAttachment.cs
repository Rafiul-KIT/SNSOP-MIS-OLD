using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity
{
	[Serializable]
    public class GBVAttachment
    {
		public int GBVAttachmentID { get; set; }
		public int GBVMainID { get; set; }
		public string Filename { get; set; }
		public byte[] FileAttached { get; set; }
		public string ContentType { get; set; }
		public int CreationUserID { get; set; }
		public DateTime LastModificationTimestamp { get; set; }
		public int LastModificationUserID { get; set; }

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator.Interfaces
{
    public interface IMappingPropertyValue
    {
        object ValueProperty { get; set; }
        object ValueReferenceOwnerProperty { get; set; }
        bool IsDBNull { get; set; }

    }
}

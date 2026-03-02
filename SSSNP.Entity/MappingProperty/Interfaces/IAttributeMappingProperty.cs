using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.MappingProperty.Interfaces
{
    public interface IAttributeMappingProperty
    {
        string PropertyName { get; set; }
        string ColumnName { get; set; }
        bool IsMandatory { get; set; }

        //Properties List
        bool AllowMultiple { get; set; }

        //Properties Lookup Implementation
        bool ApplyLookupMapping { get; set; }
        string LookupMappingName { get; set; }
        string LookupPropertyNameOverride { get; set; }
        string LookupPropertyKeyOverride { get; set; } //C-003

        //Properties ReferenceList
        bool ReferenceOwner { get; set; }
        string ReferenceOwnerPropertyName { get; set; }

    }

}

using SSSNP_SNSOP.Entity.MappingProperty.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.MappingProperty
{
    [Serializable]
    [AttributeUsage(System.AttributeTargets.All | System.AttributeTargets.Property, AllowMultiple = true)]
    public class AttributeMappingProperty : Attribute, IAttributeMappingProperty, IAttributeMappingPropertyOleDb
    {
        public string PropertyName { get; set; }
        public string ColumnName { get; set; }
        public bool IsMandatory { get; set; }

        //Properties List
        public bool AllowMultiple { get; set; }

        //Properties Lookup Implementation
        public bool ApplyLookupMapping { get; set; }
        public string LookupMappingName { get; set; }
        public string LookupPropertyNameOverride { get; set; }
        public string LookupPropertyKeyOverride { get; set; } //C-003

        //Properties ReferenceList
        public bool ReferenceOwner { get; set; }
        public string ReferenceOwnerPropertyName { get; set; }

        //Mapping OleDb
        public int PositionIndex { get; set; }
        public Type PropertyTypeOf { get; set; }
        public bool IsHidden { get; set; }
        public string HeaderColumnName { get; set; }
        public string ReferenceKeyPropertyName { get; set; }


    }
}

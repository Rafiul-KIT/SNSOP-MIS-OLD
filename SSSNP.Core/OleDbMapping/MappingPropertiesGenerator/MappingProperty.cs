using SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator.Interfaces;
using SSSNP_SNSOP.Entity.MappingProperty.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator
{
    [Serializable]
    public class MappingProperty : IAttributeMappingProperty, IMappingPropertyValue, IAttributeMappingPropertyOleDb
    {

        //IAttributeMappingProperty
        public string PropertyName { get; set; }
        public string ColumnName { get; set; }
        public bool IsMandatory { get; set; }
        public bool AllowMultiple { get; set; }
        public bool ApplyLookupMapping { get; set; }
        public string LookupMappingName { get; set; }
        public bool ReferenceOwner { get; set; }
        public string ReferenceOwnerPropertyName { get; set; }
        public string FormatProvider { get; set; }

        public string LookupPropertyNameOverride { get; set; }
        public string LookupPropertyKeyOverride { get; set; } //C-003

        //IMappingPropertyValue
        public object ValueProperty { get; set; }
        public object ValueReferenceOwnerProperty { get; set; }
        public bool IsDBNull { get; set; }

        // IAttributeMappingPropertyOleDb
        public int PositionIndex { get; set; }
        public Type PropertyTypeOf { get; set; }
        public bool IsHidden { get; set; }
        public string HeaderColumnName { get; set; }
        public string ReferenceKeyPropertyName { get; set; }


    }
}

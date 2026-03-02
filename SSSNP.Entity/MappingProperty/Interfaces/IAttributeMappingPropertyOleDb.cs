using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Entity.MappingProperty.Interfaces
{
    public interface IAttributeMappingPropertyOleDb
    {
        int PositionIndex { get; set; }
        Type PropertyTypeOf { get; set; }
        bool IsHidden { get; set; }
        string HeaderColumnName { get; set; }
        string ReferenceKeyPropertyName { get; set; }

    }

}

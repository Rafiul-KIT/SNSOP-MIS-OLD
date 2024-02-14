using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator
{
    [Serializable]
    public class ColumnProperty
    {
        public string ColumnName { get; set; }
        public int PositionIndex { get; set; }
        public Type TypeColumn { get; set; }
        public string ParameterName { get; set; }

    }
}

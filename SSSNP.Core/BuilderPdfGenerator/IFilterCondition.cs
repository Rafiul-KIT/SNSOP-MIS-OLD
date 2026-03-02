using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.BuilderPdfGenerator
{
    public interface IFilterCondition
    {
        bool Validate();
        bool Validate(object value);
        bool ValidateReference(string referenceCondition);
    }
}

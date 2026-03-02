using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core
{
    public static class TMapperAPIContext
    {
        private static readonly string namespaceRootApplication = "SSSNP_SNSOP.API.Typemappers.";

        // Typemappers Core
        public static readonly string BeneficiaryAPI = namespaceRootApplication + "BeneficiaryAPITypeMapper.xml";
        public static readonly string APITest = namespaceRootApplication + "APITestTypeMapper.xml";


    }
}

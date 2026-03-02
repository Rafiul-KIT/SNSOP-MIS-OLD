using SSSNP_SNSOP.Core.Business;
using SSSNP_SNSOP.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core
{
    [Serializable]
    public sealed class APICoreContext
    {
        private static readonly object objSyncThreadSafe = new object();
        private static APICoreContext _instance = null;

        // Interfaces
        private IAPIManager _manager = null;

        private APICoreContext()
        {
            // do nothing
        }
        private static APICoreContext Instance
        {
            get
            {
                lock (objSyncThreadSafe)
                {
                    if (_instance == null)
                    {
                        _instance = new APICoreContext();
                    }
                    return _instance;
                }

            }
        }
        public static IAPIManager APIManager
        {
            get
            {
                if (Instance._manager == null)
                {
                    Instance._manager = (IAPIManager)new APIManager();
                }

                return Instance._manager;

            }
        }

    }
}

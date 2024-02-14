using CSDS.Core.Framework.Business;
using SSSNP_SNSOP.Entity.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SSSNP_SNSOP.Core
{
    [Serializable]
    public sealed class SSSNPContext
    {
        private static readonly object objSyncThreadSafe = new object();
        private static SSSNPContext _instance = null;

        // Interfaces
        private ISSSNPManager _manager = null;

        private SSSNPContext()
        {
            // do nothing
        }
        private static SSSNPContext Instance
        {
            get
            {
                lock (objSyncThreadSafe)
                {
                    if (_instance == null)
                    {
                        _instance = new SSSNPContext();
                    }
                    return _instance;
                }

            }
        }
        public static ISSSNPManager SSSNPManager
        {
            get
            {
                if (Instance._manager == null)
                {
                    Instance._manager = BusinessContainer.Instance.Lookup<ISSSNPManager>();
                }

                return Instance._manager;

            }
        }
    }
}

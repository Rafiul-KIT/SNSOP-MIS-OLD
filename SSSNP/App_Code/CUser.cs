using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


    public sealed class CUser
    {
    private static readonly object objSyncThreadSafe = new object();
    private static CUser _instance = null;
    private CUserContext _context = null;

    private CUser()
    {
        // do nothing

    }
    private static CUser Instance
    {
        get
        {
            lock (objSyncThreadSafe)
            {
                if (_instance == null)
                {
                    _instance = new CUser();
                }

                return _instance;
            }

        }
    }

    public static CUserContext Current
    {
        get
        {
            if (Instance._context == null)
            {
                Instance._context = new CUserContext();

            }

            return Instance._context;

        }

    }
        

}

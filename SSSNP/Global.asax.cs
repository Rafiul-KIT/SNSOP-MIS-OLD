
using CSDS.Core.Framework.Configuration;
using SSSNP_SNSOP.App_Code;
using SSSNP_SNSOP.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace SSSNP
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            DataPartitionManagers config = (DataPartitionManagers)ConfigurationManager.GetSection("dataPartitions");


        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

            Exception ex = Server.GetLastError();
                                   
            try
            {
                
                
                ErrorLogger eLog = new ErrorLogger();

                eLog.LogError(ex);

            }
            catch (Exception)
            {

                //throw;
            }

            Server.ClearError();
            Response.Clear();
            Response.Redirect("/Error.aspx", false);


        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}
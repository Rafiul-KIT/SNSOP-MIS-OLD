using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Notifications;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;
using SSSNP_SNSOP.Core.DbHandler;



namespace SSSNP
{
    public partial class Startup
    {

        public void ConfigureAuth(IAppBuilder app)
        {
            //// test code for db operations: START
            //IDbOperation dbOperation = new SqlDbOperation();

            //// native complex queries call goes here
            //var sql = "select * from tbl_Users where IP_Approver = 1";
            //var dataTableForSql = dbOperation.GetDataTable(sql);

            //// native stored procedure call goes here
            //var dataSet = dbOperation.ExecuteCmdForSp(new List<CmdParamModel>()
            //{
            //    new CmdParamModel() { ParamValue = "alamin", ParamName = "@User_id" },
            //}, "CheckUserid");

            //if (dataSet != null && dataSet.Tables.Count > 0)
            //{
            //    var dataTableForStoredProcedure = dataSet.Tables[0];
            //}
            //// test code for db operations: END


            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieManager = new Microsoft.Owin.Host.SystemWeb.SystemWebChunkingCookieManager()

            });


            app.Use(typeof(CookieSameSiteNoneMiddleware));

            string azureADEnv = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_ENV"];
            string azureClientId = "";
            string azurePostLogoutRedirectUri = "";
            string azureRedirectUri = "";
            if (azureADEnv.Equals("dev"))
            {
                azureClientId = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_CLIENTID_LOCAL"];
                azurePostLogoutRedirectUri = System.Configuration.ConfigurationManager.
                    AppSettings["AZUREAD_LOGOUT_REDIRECT_URI_LOCAL"];
                azureRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_REDIRECT_URI_LOCAL"];
            }
            else
            {
                azureClientId = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_CLIENTID"];
                azurePostLogoutRedirectUri = System.Configuration.ConfigurationManager.
                    AppSettings["AZUREAD_LOGOUT_REDIRECT_URI"];
                azureRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_REDIRECT_URI"];
            }


            app.UseOpenIdConnectAuthentication(



                            new OpenIdConnectAuthenticationOptions
                            {
                                ClientId = azureClientId, //clientId,
                                Authority = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_AUTHORITY"], //authority,
                                PostLogoutRedirectUri = azurePostLogoutRedirectUri, //redirectUri,
                                RedirectUri = azureRedirectUri, //redirectUri,
                                Scope = OpenIdConnectScope.OpenIdProfile,                   //new
                                ResponseType = OpenIdConnectResponseType.IdToken,           //new
                                Notifications = new OpenIdConnectAuthenticationNotifications
                                {
                                    AuthenticationFailed = (context) =>
                                    {
                                        context.HandleResponse();
                                        throw new Exception("OWIN Authentication Failed", context.Exception);
                                        //  context.OwinContext.Response.Redirect("~/RedirectAzure.aspx");
                                        //  return Task.FromResult(0);
                                    }
                                }

                            });





        }

        private Task OnAuthenticationFailed(AuthenticationFailedNotification<OpenIdConnectMessage, OpenIdConnectAuthenticationOptions> arg)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
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



namespace SSSNP
{
    public partial class Startup
    {
        
        public void ConfigureAuth(IAppBuilder app)
        {
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;


            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "Cookies",
                CookieManager = new Microsoft.Owin.Host.SystemWeb.SystemWebChunkingCookieManager()

            });


            app.Use(typeof(CookieSameSiteNoneMiddleware));


            app.UseOpenIdConnectAuthentication(



                            new OpenIdConnectAuthenticationOptions
                            {
                                ClientId = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_CLIENTID"], //clientId,
                                Authority = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_AUTHORITY"], //authority,
                                PostLogoutRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LOGOUT_REDIRECT_URI"], //redirectUri,
                                RedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_REDIRECT_URI"], //redirectUri,
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

using System;
using System.Collections.Generic;
using System.Configuration;
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
using SSSNP.Asynch;
using SSSNP.Service;
using SSSNP_SNSOP.Core.DbHandler;
using SSSNP_SNSOP.Core.Model;
using SSSNP_SNSOP.Core.Model.Component2;
using SSSNP_SNSOP.Core.Threads;



namespace SSSNP
{
    public partial class Startup
    {

        public void ConfigureAuth(IAppBuilder app)
        {
            // Mock :: Household Group Linkage Module
            var request = new HouseholdGroupLinkageModel();
            var someHouseholdNumbers = new List<string>();
            someHouseholdNumbers.Add("HH1");
            someHouseholdNumbers.Add("HH2");
            someHouseholdNumbers.Add("HH3");
            request.HouseholdNumbers = someHouseholdNumbers;
            request.GroupId = 1;

            var response = new Component2Service().SaveHouseholdGroupLinkage(request);
            Console.WriteLine(response.IsSuccess + "\n" +response.Message);
            // Mock :: Household Group Linkage Module


            // Mock :: Training and Attendance Tracking Module

            //var list = new List<TrainingAndAttendanceModel>();
            //var req1 = new TrainingAndAttendanceModel()
            //{
            //    HouseholdNumber = "h6",
            //    ClassesHeld = 15,
            //    ClassesAttended = 10,
            //    ClassesMissed = 5,
            //    CourseCode = "FINLIT"
            //};
            //list.Add(req1);
            //var req2 = new TrainingAndAttendanceModel()
            //{
            //    HouseholdNumber = "h7",
            //    ClassesHeld = 14,
            //    ClassesAttended = 11,
            //    ClassesMissed = 3,
            //    CourseCode = "LIVETECH"
            //};
            //list.Add(req2);

            //ResponseModel response = new Component2Service().SaveTrainingAndAttendanceTracking(list);


            //var list = new List<TrainingAndAttendanceModel>();
            //var req1 = new TrainingAndAttendanceModel()
            //{
            //    HouseholdNumber = "h1",
            //    ClassesHeld = 15,
            //    ClassesAttended = 10,
            //    ClassesMissed = 5,
            //    CourseCode = "FINLIT"
            //};
            //list.Add(req1);
            //var req2 = new TrainingAndAttendanceModel()
            //{
            //    HouseholdNumber = "h3",
            //    ClassesHeld = 14,
            //    ClassesAttended = 11,
            //    ClassesMissed = 3,
            //    CourseCode = "LIVETECH"
            //};
            //list.Add(req2);

            //ResponseModel response = new Component2Service().SaveTrainingAndAttendanceTracking(list);

            // Mock :: Training and Attendance Tracking Module



            //// test code for db operations: START
            //var buildProfile = ConfigurationManager.AppSettings["build.profile.active"].ToString();
            //string connStr = "";
            //if(buildProfile.Equals("prod"))
            //{
            //    connStr = ConfigurationManager.AppSettings["snsop_db_connStr_prod"].ToString();
            //}
            //else
            //{
            //    connStr = ConfigurationManager.AppSettings["snsop_db_connStr_dev"].ToString();
            //}
            //IDbOperation dbOperation = new SqlDbOperation(connStr);

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


            //// test code for thread management: START
            //// Start grm notifocation thread
            ThreadHandler.GetInstance(new SendGrmNotificationAsynch()).StartThread();
            //// test code for thread management: END


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
            if (azureADEnv.Equals("local"))
            {
                azureClientId = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_CLIENTID_LOCAL"];
                azurePostLogoutRedirectUri = System.Configuration.ConfigurationManager.
                    AppSettings["AZUREAD_LOGOUT_REDIRECT_URI_LOCAL"];
                azureRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_REDIRECT_URI_LOCAL"];
            }
            if (azureADEnv.Equals("dev"))
            {
                azureClientId = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_CLIENTID_DEV"];
                azurePostLogoutRedirectUri = System.Configuration.ConfigurationManager.
                    AppSettings["AZUREAD_LOGOUT_REDIRECT_URI_DEV"];
                azureRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_REDIRECT_URI_DEV"];
            }
            else if (azureADEnv.Equals("prod"))
            {
                azureClientId = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_CLIENTID_PROD"];
                azurePostLogoutRedirectUri = System.Configuration.ConfigurationManager.
                    AppSettings["AZUREAD_LOGOUT_REDIRECT_URI_PROD"];
                azureRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_REDIRECT_URI_PROD"];
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
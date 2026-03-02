#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Owin.Security.OpenIdConnect;
using Microsoft.Owin.Security.Cookies;
#endregion
#region Amendment History Begin
//Form Name : Login
//Purpose : To Login User
//Tables used : tbl_Users,td_TstTeamDet
//Stored procedures Used : sp_ValidUser
#endregion Amendment History End

public partial class index : System.Web.UI.Page
{

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        #region Is not Postback Statement
        if (!IsPostBack)
        {
            // Session Clearance
            if (Session.Count != 0)
            {
                Session.Clear();
                Session.Abandon();
                Session.RemoveAll();

                if (!Page.IsPostBack)
                    Session.Abandon();

                HttpContext.Current.GetOwinContext().Authentication.SignOut(
                    OpenIdConnectAuthenticationDefaults.AuthenticationType,
                    CookieAuthenticationDefaults.AuthenticationType);
            }

         //   txtUserId.Focus();


            // Firfrox Broswer Blocking
            if (Request.Browser.Browser.ToString().Trim().ToLower() == "firefox")
            {
                //lblmsg.Text = "Please use Internet Explorer or Google Chrome";
                // btnSignIn.Enabled = false;
            }
        }
        #endregion
    }
    #endregion

    protected void Page_PreRender(object sender, EventArgs e)
    {

    }

    #region brute force counter measure
    private void AuthenticateRequest(object obj, EventArgs ea)
    {
        int minSeconds = 0;
        int maxSeconds = 630;
        HttpApplication objApp = (HttpApplication)obj;
        HttpContext objContext = (HttpContext)objApp.Context;
        // If user identity is not blank, pause for a random amount of time
        if (objApp.User.Identity.Name != "")
        {
            Random rand = new Random();
            Thread.Sleep(rand.Next(minSeconds, maxSeconds) * 1000);
        }
    }
    #endregion

    //protected void DelayLogin()
    //{
    //    int minSeconds = 30;
    //    int maxSeconds = 30;
    //    Random rand = new Random();
    //    Thread.Sleep(rand.Next(minSeconds, maxSeconds) * 1000);        
    //}

    //protected void HdrLoginButton_Click(object sender, EventArgs e)
    //{
    //    HttpContext.Current.GetOwinContext().Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
    //    {
    //        RedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LANDING_REDIRECT_URI"]
    //        //RedirectUri = "/"
    //    }, OpenIdConnectAuthenticationDefaults.AuthenticationType);

    //    //Label1.Text = User.Identity.Name;
    //}

    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        string azureADEnv = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_ENV"];
        string azureADLandingRedirectUri = "";
        if (azureADEnv.Equals("local"))
        {
            azureADLandingRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LANDING_REDIRECT_URI_LOCAL"];
        }
        else if (azureADEnv.Equals("dev"))
        {
            azureADLandingRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LANDING_REDIRECT_URI_DEV"];
        }
        else if (azureADEnv.Equals("prod"))
        {
            azureADLandingRedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LANDING_REDIRECT_URI_PROD"];
        }

        HttpContext.Current.GetOwinContext().Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
        {
            RedirectUri = azureADLandingRedirectUri //+ "?try=" + User.Identity.Name
            //RedirectUri = "/"
        }, OpenIdConnectAuthenticationDefaults.AuthenticationType);


    }
}

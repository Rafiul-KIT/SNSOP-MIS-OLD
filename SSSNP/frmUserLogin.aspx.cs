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

    #region Button Event Functionalities - Login
    protected void btnSignIn_Click1(object sender, ImageClickEventArgs e)
    {
        HttpContext.Current.GetOwinContext().Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
        {
            RedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LANDING_REDIRECT_URI"] //+ "?try=" + User.Identity.Name
            //RedirectUri = "/"
        }, OpenIdConnectAuthenticationDefaults.AuthenticationType);

        //string username = HttpContext.Current.GetOwinContext().Authentication.User.Identity.Name;


        ////Label1.Text = User.Identity.Name;

        //General clsGeneral = new General();
        //String UserID = "", Password = "";

        //UserID = txtUserId.Text;
        //if (!String.IsNullOrEmpty(txtPwd.Text))
        //    Password = clsGeneral.CalculateMD5Hash(txtPwd.Text);
        ////Password = txtPwd.Text;

        //// Login attempts
        //if (cntl.Value == "") cntl.Value = "0";
        //int loginAttempts = Convert.ToInt32(cntl.Value);
        //if (!String.IsNullOrEmpty(UserID) && !String.IsNullOrEmpty(Password))
        //{
        //    ++loginAttempts;
        //    cntl.Value = loginAttempts.ToString();
        //}

        //DataSet ds = clsGeneral.ValidateLogin(UserID, Password);
        //DataView dv = ds.Tables[0].DefaultView;

        //if (dv.Table.Rows.Count > 0)
        //{
        //    //DelayLogin();
        //    cntl.Value = "0";

        //    Session["uid"] = dv.Table.Rows[0]["intUserid"].ToString();
        //    Session["username"] = dv.Table.Rows[0]["User_name"].ToString();
        //    Session["user_id"] = dv.Table.Rows[0]["User_id"].ToString();
        //    Session["password"] = dv.Table.Rows[0]["password"].ToString();
        //    Session["userlevel"] = dv.Table.Rows[0]["User_level"].ToString();
        //    Session["user_type"] = dv.Table.Rows[0]["User_type"].ToString();

        //    Session["User_Code"] = dv.Table.Rows[0]["User_code"].ToString();
        //    Session["Desgid"] = "0";
        //    if (dv.Table.Rows[0]["User_type"].ToString() == "TST")
        //    {
        //        if (ds.Tables[1].Rows.Count > 0)
        //        {
        //            Session["Desgid"] = ds.Tables[1].Rows[0][0].ToString().Trim();
        //        }
        //        else
        //        {
        //            Session["Desgid"] = "9999";
        //        }
        //    }
        //    string usert_type = Convert.ToString(Session["user_type"]);
        //    if (usert_type == "IP")
        //    {
        //        Response.Redirect("UI/Public Works/Home.aspx");
        //    }
        //    else if (usert_type == "TST")
        //    {
        //        Response.Redirect("UI/Public Works/Home.aspx");
        //    }
        //    else if (usert_type == "Cordinator")
        //    {
        //        Response.Redirect("UI/Public Works/CoordinatorInvoiceDashboard.aspx");
        //    }
        //    else if (usert_type == "Specialist")
        //    {
        //        Response.Redirect("UI/Public Works/SpecialistInvoiceDashboard.aspx");
        //    }
        //    else if (usert_type == "Manager")
        //    {
        //        Response.Redirect("UI/Public Works/ManagerInvoiceDashboard.aspx");
        //    }
        //    else if (usert_type == "Admin")
        //    {
        //        Response.Redirect("UI/Public Works/Home.aspx");
        //    }
        //    else
        //    {
        //        Response.Redirect("UI/Public Works/Home.aspx");
        //    }





        //}
        //else
        //{
        //    loginAttempts++;
        //    if (loginAttempts > 4)
        //    {
        //        Label2.Text = "Failed in 5 login attempts. Assuming unauthorized access. Please wait 30 seconds to login!";
        //    }

        //    if (loginAttempts > 5 && !String.IsNullOrEmpty(UserID) && !String.IsNullOrEmpty(Password))
        //    {
        //        //txtUserId.Enabled = false;
        //        //txtPwd.Enabled = false;
        //        //btnSignIn.Enabled = false;



        //        DelayLogin();

        //        //Response.Redirect("~/FailedLogin.aspx");

        //        //MessageBox.Show("Failed in 3 login attempts. Assuming unauthorized access. Terminating!", "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        //Application.Exit();
        //    }
        //    //lblmsg.Text = "Invalid UserID/Password";
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", "Invalid UserID/Password", true);


        //    return;
        //}

    }

    #endregion

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
        HttpContext.Current.GetOwinContext().Authentication.Challenge(new Microsoft.Owin.Security.AuthenticationProperties
        {
            RedirectUri = System.Configuration.ConfigurationManager.AppSettings["AZUREAD_LANDING_REDIRECT_URI"] //+ "?try=" + User.Identity.Name
            //RedirectUri = "/"
        }, OpenIdConnectAuthenticationDefaults.AuthenticationType);


    }
}

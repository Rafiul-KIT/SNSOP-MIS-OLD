using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP_SNSOP
{
    public partial class TokenAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {
                if (HttpContext.Current.GetOwinContext().Authentication.User.Identity.IsAuthenticated)
                {
                    General clsGeneral = new General();
                    string UserID = "", Password = "", preferred_user = "";

                    var claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;


                    var claims = claimsIdentity.Claims.ToList();

                    foreach (var prop in claims)
                    {
                        if (prop.Type == "preferred_username")
                        {
                            //object propValue = prop.GetValue(claimsIdentity, null);
                            preferred_user = prop.Value; //  
                            break;
                        }


                        // Do something with propValue
                    }


                    DataSet ds = clsGeneral.ValidateAuthentication(preferred_user);


                    DataView dv = ds.Tables[0].DefaultView;

                    if (dv.Table.Rows.Count > 0)
                    {
                        //DelayLogin();
                        //cntl.Value = "0";

                        Session["uid"] = dv.Table.Rows[0]["intUserid"].ToString();
                        Session["username"] = dv.Table.Rows[0]["User_name"].ToString();
                        Session["user_id"] = dv.Table.Rows[0]["User_id"].ToString();
                        Session["password"] = dv.Table.Rows[0]["password"].ToString();
                        Session["userlevel"] = dv.Table.Rows[0]["User_level"].ToString();
                        Session["user_type"] = dv.Table.Rows[0]["User_type"].ToString();
                        if (Session["user_type"].ToString() == "IP")
                        {
                            if (clsGeneral.CheckIsIPApprover(Session["uid"].ToString().Trim()).Tables[0].Rows.Count > 0)
                            {
                                Session["ip_approver"] = "1";
                            }
                        }
                        int userID = SSSNPHelper.TryParseInt(dv.Table.Rows[0]["intUserid"].ToString());
                        if (userID != 0)
                        {
                            try
                            {
                                tblUser user = SSSNPContext.SSSNPManager.GetUser(userID);
                                if (user != null && user.GBVUser)
                                {
                                    Session["GBVUser"] = "1";
                                }
                                else
                                {
                                    Session["GBVUser"] = "0";
                                }
                            }
                            catch (Exception ex)
                            {
                                Session["GBVUser"] = "0";
                            }
                        }


                        Session["User_Code"] = dv.Table.Rows[0]["User_code"].ToString();
                        Session["Desgid"] = "0";
                        if (dv.Table.Rows[0]["User_type"].ToString() == "TST")
                        {
                            if (ds.Tables[1].Rows.Count > 0)
                            {
                                Session["Desgid"] = ds.Tables[1].Rows[0][0].ToString().Trim();
                            }
                            else
                            {
                                Session["Desgid"] = "9999";
                            }
                        }
                        string usert_type = Convert.ToString(Session["user_type"]);
                        if (usert_type == "IP")
                        {
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                        else if (usert_type == "TST")
                        {
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                        else if (usert_type == "Cordinator")
                        {
                            //Response.Redirect("UI/Public Works/CoordinatorInvoiceDashboard.aspx", false);
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                        else if (usert_type == "Specialist")
                        {
                            //Response.Redirect("UI/Public Works/SpecialistInvoiceDashboard.aspx", false);
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                        else if (usert_type == "Manager")
                        {
                            //Response.Redirect("UI/Public Works/ManagerInvoiceDashboard.aspx", false);
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                        else if (usert_type == "Admin")
                        {
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                        else
                        {
                            Response.Redirect("UI/Public Works/Home.aspx", false);
                        }
                    }
                    else
                    {
                        throw new Exception("Token Access Exception ValidateAuthentication - Username=" + preferred_user);
                        //return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("TokenAccess General Exception", ex);
            }
        }
    }
}
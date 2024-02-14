using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

public partial class UI_masterAtms : System.Web.UI.MasterPage
{
   
  protected void Page_Load(object sender, EventArgs e)
    {

        string section = "";
        if (Session.Count > 0)
        {
            //section=Session["UserLevel"].ToString();
           
        }
    }
        
    //    if (Session.Count == 0)
    //    {
    //        Response.Redirect("login.aspx");
    //    }
    //    else
    //    {
    //        panel2.Visible = false;
    //        panel1.Visible = false;
    //        if (Session["user"].ToString().Trim() == "admin")
    //        {
    //           // panel1.Visible = true;
    //            panel2.Visible = false;
    //            divAct.Visible = false;
    //            //pnlDeposits.Visible = false;
    //            //li1.Visible = true;
    //            divDelete.Visible = true;
    //        }
    //        else
    //        {
    //            //li1.Visible = false;
    //            divDelete.Visible = false;
    //            if (Session["UserLevel"].ToString().Trim() != "Admin")
    //            panel2.Visible = false;
                
    //            if (Session["UserLevel"].ToString().Trim() == "User" || Session["UserLevel"].ToString().Trim() == "MRO")
    //            {
    //                divView.Visible = false;
    //                divAct.Visible = true;
    //                if (Session["UserLevel"].ToString().Trim() == "MRO")
    //                {
    //                    divNew.Visible = true;
    //                    pnlDeposits.Visible = true;
    //                }

    //            }
               
    //            else
    //            {

    //               // td24Hr.Visible = false;
    //                divNew.Visible = false;
    //                pnlDeposits.Visible = false;
    //                divAct.Visible = false;
                    
    //            }

    //        }
                
    //    }

       

    //}

    
}

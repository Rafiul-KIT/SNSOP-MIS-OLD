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
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Configuration;
using System.Text;
using System.Threading;
using System.ComponentModel;
using System.Text;


public partial class FrmUsers : System.Web.UI.Page
{
   
    string cjfsno;
    
    
    protected void Page_Load(object sender, EventArgs e)
    {

        //Btnsave.Focus();
       
            if (!Page.IsPostBack)
            {
                if (Request.QueryString["Msg"].ToString().Trim() != "")
                {
                    lblresult.Text = Request.QueryString["Msg"].ToString();
                    //Session["ids"] = Session["RegNo"].ToString();
                  //  lblresult.Text = "Your Registration Successfully Completed - Reg No: " +cjfsno.ToString();
                    
                }
                
            }
            
       
    }




    protected void Btnsave_Click(object sender, EventArgs e)
    {
       // Response.Redirect("CJFS.aspx");
    }
    protected override void OnPreRender(EventArgs e)
    {

        base.OnPreRender(e);

        StringBuilder javaScript = new StringBuilder();
        javaScript.Append("\n<script language=JavaScript>\n");
        javaScript.Append("window.history.forward(1);\n");
        javaScript.Append("</script>\n");

        Page.RegisterClientScriptBlock("HistoryScript", javaScript.ToString());
    }
}


//************************** This interface is created by Ravi for helpdesk ******************************
// ***********************************  Date  :  14-05-2012  *********************************
/// Software Engineer: R.Ravichandra
/// Created By R.Ravi Chandra on 14-05-2012 at 12.20PM For Inserting Help Desk -, StoredProcedure name:sp_Instfeedback.
/// 
/// 
/// 

// Modified by Srikanth on 21-08-2013
// Modified Store Procedures: sp_getREPPIAswithMORD
// For : MORD Tem Help Desk Display

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
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;
// Created By Srikanth on 15-6-2012

using System.Net.Mail;
using System.Net.Mime;
using System.IO;

// MOdified by Srikanth for View PIAs with MORD Team  Store Procedures: sp_getREPPIAswithMORD
// Modified by Srikanth for Mail Sending on 20-08-2013

public partial class ViewHelpdesk : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    comFunctions fssFunctions = new comFunctions();
    General cmn = new General();
    /// <summary>
    ///Begin  Created By Ravi Chandra on 14-05-2012 at 12.20PM For Help Desk Declaration 
    /// </summary>
    string mobPIA = String.Empty;
    /// <summary>
    ///End  Created By Ravi Chandra on 14-05-2012 at 12.20PM For Help Desk Declaration
    /// </summary>
    string piaid = String.Empty;
    string trCode = String.Empty;
    string mppia = String.Empty;
    DataSet ds;
    string user;
    DataView dv;
    string userid;
  /// <summary>
  /// Created By Srikanht for Restriction
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
  /// 


    string usertype = "", userid1="";
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
       

    try
    {
   
          if (Session.Count == 0)
            {
                Response.Redirect("../../frmUserLogin.aspx");
            }

          user = Session["user_code"].ToString();
          usertype = Session["user_type"].ToString();
          userid = Session["uid"].ToString().Trim();

          userid1 = Session["username"].ToString().Trim();
         
        if (!IsPostBack)
        {

            cmn.getfeedback(ddlfeedbak);
         //   cmn.fillgrid(gvComplaintsList);
            
            //if (usertype.ToString().Trim() == "TST")
            //{
            //    ddlPIA.SelectedValue = ddlPIA.Items.FindByValue(user.Trim()).Value;
            //    ddlPIA.Enabled = false;
            //}
            //else if (usertype.ToString().Trim() == "Adm" || usertype.ToString().Trim() == "Help")
            //{
            //    ddlPIA.SelectedValue = ddlPIA.Items.FindByValue(userid.Trim()).Value;
            //    ddlPIA.Enabled = false;
            //}
            //else if (usertype.ToString().Trim() == "IP")
            //{
            //    ddlPIA.SelectedValue = ddlPIA.Items.FindByValue(user.Trim()).Value;
            //    ddlPIA.Enabled = false;
            //}
            //else
            //{
            //    ddlPIA.Enabled = true;

            //}

            cmn.getPIADetwithMORD(ddlPIA);

            
             if (usertype.ToString().Trim() == "Adm" || usertype.ToString().Trim() == "Help")
            {
                
                //ddlPIA.SelectedValue = ddlPIA.Items.FindByValue(user.Trim()).Value;
                ddlPIA.Enabled = true;
            }
             else 
             {

                 ddlPIA.SelectedValue = ddlPIA.Items.FindByValue(userid.Trim()).Value;
                 ddlPIA.Enabled = false;
             }
            
        FillHelp();
           }
           
          
       }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region Resetfromcontrol
    public void Resetfromcontrol(Control parent)
    {
        try
        {
            foreach (Control c in parent.Controls)
            {
                if (c.Controls.Count > 0)
                {
                    Resetfromcontrol(c);
                }
                else
                {
                    switch (c.GetType().ToString())
                    {
                        case "System.Web.UI.WebControls.TextBox":
                            ((TextBox)c).Text = "";
                            break;

                    }
                }
            }
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region OnPreRender
    protected override void OnPreRender(EventArgs e)
    {
        try
        {
            base.OnPreRender(e);

            StringBuilder javaScript = new StringBuilder();
            javaScript.Append("\n<script language=JavaScript>\n");
            javaScript.Append("window.history.forward(1);\n");
            javaScript.Append("</script>\n");

            Page.RegisterClientScriptBlock("HistoryScript", javaScript.ToString());
        }
        catch (Exception ex)
        {
        }
    }
    #endregion

    #region Selectindexchanged
    protected void gvComplaintsList_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region PageIndex
    protected void gvComplaintsList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvComplaintsList.PageIndex = e.NewPageIndex;
        FillHelp();
      //  cmn.fillgrid(gvComplaintsList);
    }
    #endregion

    #region BtnSave
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Button btn1 = (Button)sender;
        GridViewRow row = (GridViewRow)btn1.NamingContainer;
        
        DropDownList ddlchStatus = (DropDownList)row.FindControl("ddlchStatus");
        TextBox txtremarks = (TextBox)row.FindControl("txtremarks");
        HyperLink int_fbid = (HyperLink)row.Cells[0].Controls[0];
        string Modified_by = user;
        string Created_by = Session["uid"].ToString();
        
        string Remarks;
      
        
 //      int returnValue = cmn.ForwardComplaint( ddlchStatus.SelectedValue.Trim(), remarks);
        int returnValue =  cmn.Changestatusapprove(int_fbid.Text,ddlchStatus.SelectedValue.ToString().Trim(),txtremarks.Text,Modified_by);
        cmn.AddToTrans(int_fbid.Text,ddlchStatus.SelectedValue.ToString(), txtremarks.Text, Created_by);
        DataSet dsdet = new DataSet();
        dsdet = cmn.GetHelpDespDetbyid(int_fbid.Text.Trim());
        if (dsdet.Tables[0].Rows.Count > 0)
        {
            string newhdCode = dsdet.Tables[0].Rows[0]["hd_Code"].ToString();
            string txtemail = dsdet.Tables[0].Rows[0]["anothermailid"].ToString();
            //string Label57 = dsdet.Tables[0].Rows[0]["piaName"].ToString();
            string Label57 = "";
            string Label58 = dsdet.Tables[0].Rows[0]["hd_Username"].ToString();
            string ddlfeedback = dsdet.Tables[0].Rows[0]["Fb_Type"].ToString();
            string txtsubjet = dsdet.Tables[0].Rows[0]["hd_desc"].ToString();
            string Remarksdet = dsdet.Tables[0].Rows[0]["Remarks"].ToString();
            string statusdet = ddlchStatus.SelectedItem.Text;
          //  SendMail(newhdCode, Label57, Label58, ddlfeedback, txtsubjet, Remarksdet, statusdet);
           // SendMailKishore(newhdCode, Label57, Label58, ddlfeedback, txtsubjet, Remarksdet, statusdet);
            //SendMailSekhar(newhdCode, Label57, Label58, ddlfeedback, txtsubjet, Remarksdet, statusdet);

          //  SendMailSri(newhdCode, Label57, Label58, ddlfeedback, txtsubjet, Remarksdet, statusdet);
            if (txtemail != "")
                SendMailAnother(newhdCode, Label57, Label58, ddlfeedback, txtsubjet, Remarksdet, txtemail.Trim(), statusdet);

        }
            if (returnValue == -1)
            {
                lblerrMsg.Text = "<font color=green><b>Saved Successfully</b></font>";
               
            }
            else
            {
                lblerrMsg.Text = "<font color=red><b>Error! Contact System Administrator</b></font>";
            }
            FillHelp();

        }
    
    #endregion

    #region ddlchStatus_SelectedIndexChanged
    protected void ddlchStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

        //DropDownList dd = (DropDownList)sender;
        //GridViewRow row = (GridViewRow)dd.NamingContainer;
        //DropDownList ddlchStatus = (DropDownList)row.FindControl("ddlchStatus");

        //if (ddlchStatus.SelectedValue.ToString().Trim() == "Pending")
        //{
        //    Pending.Visible = true;
        //    Rejected.Visible = false;
        //    Close.Visible = false;
        //}
        //else if (ddlchStatus.SelectedValue.ToString().Trim() == "Rejected")
        //{
        //    Pending.Visible = false;
        //    Rejected.Visible = true;
        //    Close.Visible = false;
        //}
        //else if (ddlchStatus.SelectedValue.ToString().Trim() == "Close")
        //{
        //    Pending.Visible = false;
        //    Rejected.Visible = false;
        //    Close.Visible = true;
        //}
        //else
        //{
        //    Pending.Visible = false;
        //    Rejected.Visible = false;
        //    Close.Visible = false;

        //}

    }
    #endregion

    #region ddlstat_SelectedIndexChanged
    protected void ddlstat_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHelp(); 
    }
    #endregion

    #region
    protected void ddlfeedbak_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHelp(); 
    }
    #endregion

    
  /// <summary>
  ///  Begin Created By Srikanth on 17-5-2012
  /// </summary>
    public void FillHelp()
    {
         DataSet dshelps = new DataSet();

         if (usertype == "Adm" && Session["user_id"].ToString() == "admin")
         {
             dshelps = cmn.Gethelpdeskbackup(ddlfeedbak.SelectedValue.ToString().Trim(), ddlstat.SelectedValue.ToString().Trim(),"%", txtrefno.Text.Trim());
         }
         else
         {
             dshelps = cmn.Gethelpdeskbackup(ddlfeedbak.SelectedValue.ToString().Trim(), ddlstat.SelectedValue.ToString().Trim(), ddlPIA.SelectedValue.ToString().Trim(), txtrefno.Text.Trim());
         }
        if (dshelps.Tables[0].Rows.Count > 0)
        {
            lblresult.Text = "Total Records Found : " + dshelps.Tables[0].Rows.Count.ToString();
        }
        else
        {
            lblresult.Text = "";
        }
        gvComplaintsList.DataSource = dshelps.Tables[0];
        gvComplaintsList.DataBind();
        if (usertype == "Adm" || usertype == "Help")
        {
            if (ddlstat.SelectedValue.ToString().Trim() == "Open")
            {
                gvComplaintsList.Columns[10].Visible = true;
                gvComplaintsList.Columns[9].Visible = false;
            }
            else
            {
                gvComplaintsList.Columns[10].Visible = false;
                gvComplaintsList.Columns[9].Visible = true;
            }
        }
        else
        {
            if (ddlstat.SelectedValue.ToString().Trim() == "Open")
            {
             
                gvComplaintsList.Columns[9].Visible = false;
            }
            else
            {
                
                gvComplaintsList.Columns[9].Visible = true;
            }
            gvComplaintsList.Columns[10].Visible = false;
        }
        lblerrMsg.Text = "";
        lblStatus.Text = "";
      //  lblresult.Text = "";
    }


    protected void ddlPIA_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillHelp(); 
    }
    protected void txtrefno_TextChanged(object sender, EventArgs e)
    {
        FillHelp(); 
    }
    #region Button Disable By Srikanth on 17-5-2012
    public string DisableTheButton(Control pge, Control btn, string vldGroup)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
        if (vldGroup.Trim() == "")
        {
            sb.Append("if (Page_ClientValidate() == false) { return false; }} ");

        }
        else
        {
            sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");

        }
        sb.Append("this.value = 'Please wait...';");
        sb.Append("this.disabled = true;");
        sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }


    #endregion
    protected void gvComplaintsList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Button BtnSave = (Button)e.Row.FindControl("BtnSave");
            //Begin Created By Srikanth on 18-5-2012 
           BtnSave.Attributes.Add("onclick", DisableTheButton(this.Page, BtnSave, "group"));
        
            //Begin Created By Srikanth on 18-5-2012 

          
           
               HyperLink h2 = (HyperLink)e.Row.FindControl("hypLetter");
               h2.NavigateUrl = "HDDocsDL.aspx?type=2&id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "int_fbid"));
               h2.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hd_uplddocname"));
          
        }
    }


    #region Mail Sendings Created By Srikanth

    public void SendMail(string numbers, string Label57, string Label58, string ddlfeedback, string txtsubjet, string Remarksdet, string statusdet)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string supportmail = System.Configuration.ConfigurationManager.AppSettings["SUPPORTMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);


        string to = supportmail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress(from, ":: CAP Foundation  MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "Re: CAP Foundation  - Help Desk Registration Status From t," + Label57;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Re: CAP Foundation  MIS - Help Desk Registration Status </H2><br><br>From : t,  <br> <br>  <br><br> USER NAME: " + Label58 + "<br> Feed Back Type: " + ddlfeedback + "  <br><br> Status: " + statusdet + " <br> <br> Our  Comments : " + txtsubjet + " <br> <br> Remarks : " + Remarksdet + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> <br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //object a = (byte[])Session["File"];
        //mail.Attachments.Add(new Attachment(new MemoryStream((byte[])Session["letterUpload"]), Session["letterUploadFileName"].ToString()));
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
            // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }

    public void SendMailKishore(string numbers, string Label57, string Label58, string ddlfeedback, string txtsubjet, string Remarksdet, string statusdet)
    {


        string from = "snsop.southsudan@gmail.com"; 

        string to = ""; 

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress(from, ":: CAP Foundation  MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "Re: CAP Foundation  - Help .," + Label57;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Re" + Label58 + "<br> Feed Back Type: " + ddlfeedback + " <br><br> Status: " + statusdet + "<br> <br> Our  Comments : " + txtsubjet + " <br> <br> Remarks : " + Remarksdet + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> Please Rectify this as early as possible<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //object a = (byte[])Session["File"];
        //mail.Attachments.Add(new Attachment(new MemoryStream((byte[])Session["letterUpload"]), Session["letterUploadFileName"].ToString()));
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        SmtpClient client = new SmtpClient();
        //Add the Creddentials- use your own email id and password

        client.Credentials = new System.Net.NetworkCredential(from, "ABCabc123@");

         client.Port = 25; 
        client.Host = "smtp.gmail.com";
       client.EnableSsl = true; //
        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
           //  HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }


    public void SendMailSekhar(string numbers, string Label57, string Label58, string ddlfeedback, string txtsubjet, string Remarksdet, string statusdet)
    {


        string from = ""; //

        string to = ""; //

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress(from, ":: CAP Foundation  MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "Re: CAP Foundation  - Help Desk Registration From " + Label57;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Re: CAP Foundation  MIS - Help Desk Registration</H2><br><br>To :   <br> <br> <b> PIA NAME: " + Label57 + "</b> <br><br> USER NAME: " + Label58 + "<br> Feed Back Type: " + ddlfeedback + " <br><br> Status: " + statusdet + "<br> <br> Our  Comments : " + txtsubjet + " <br> <br> Remarks : " + Remarksdet + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> Please Rectify this as early as possible<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //object a = (byte[])Session["File"];
        //mail.Attachments.Add(new Attachment(new MemoryStream((byte[])Session["letterUpload"]), Session["letterUploadFileName"].ToString()));
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        SmtpClient client = new SmtpClient();
        //Add the Creddentials- use your own email id and password

        client.Credentials = new System.Net.NetworkCredential(from, "");

       
        client.Host = "";
        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
           // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }

    public void SendMailSri(string numbers, string Label57, string Label58, string ddlfeedback, string txtsubjet, string Remarksdet, string statusdet)
    {


        string from = ""; 

        string to = ""; //

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress(from, ":: CAP Foundation  MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "Re: CAP Foundation  - Help Desk Registration From " + Label57;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Re: CAP Foundation  MIS - Help Desk Registration</H2><br><br>To : t,  <br> <br> <b> PIA NAME: " + Label57 + "</b> <br><br> USER NAME: " + Label58 + "<br> Feed Back Type: " + ddlfeedback + " <br><br> Status: " + statusdet + " <br> <br> Our  Comments : " + txtsubjet + " <br> <br> Remarks : " + Remarksdet + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> Please Rectify this as early as possible<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //object a = (byte[])Session["File"];
        //mail.Attachments.Add(new Attachment(new MemoryStream((byte[])Session["letterUpload"]), Session["letterUploadFileName"].ToString()));
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        SmtpClient client = new SmtpClient();
        //Add the Creddentials- use your own email id and password

        client.Credentials = new System.Net.NetworkCredential(from, "");

        client.Host = "";
        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
           // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }

    public void SendMailAnother(string numbers, string Label57, string Label58, string ddlfeedback, string txtsubjet, string Remarksdet, string txtemail, string statusdet)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        

        string to = txtemail.Trim(); //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
         mail.CC.Add("");
        
        mail.From = new MailAddress(from, ":: SSSNP Public Works - MIS ::", System.Text.Encoding.UTF8);
        mail.Subject = "Re: SSSNP Public Works  - MIS Help Desk Registration Status From t," + Label57;
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Re: SSSNP Public Works - MIS Help Desk Registration Status</H2><br><br>From : t,  <br> <br>  <br><br> USER NAME: " + Label58 + "<br> Feed Back Type: " + ddlfeedback + " <br><br> Status: " + statusdet + " <br> <br> Our  Comments : " + txtsubjet + " <br> <br> Remarks : " + Remarksdet + " <br><br> Help Desk Our Reference Number is <b>" + numbers + "</b> <br> <br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

        try
        {
            client.Send(mail);
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
           // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }
    

    #endregion
}

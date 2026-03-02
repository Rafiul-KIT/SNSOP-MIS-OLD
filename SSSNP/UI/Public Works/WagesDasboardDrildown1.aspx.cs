using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.IO;
public partial class WagesDasboardDrildown1 : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            clsGeneral.getIpDet(ddlIP);

            Gen.getStates(ddlState);
            if (Request.QueryString.Count>0)
            {
                if (Request.QueryString[0].ToString() != "")
                { hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
    }
    #endregion


   
    void fillgrid()
    {
        DataSet ds = new DataSet();
        if (Request.QueryString[0].ToString().Trim() == "1")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(),Request.QueryString[0].ToString().Trim(),ddlIP.SelectedValue,ddlState.SelectedValue,ddlCounties.SelectedValue,ddlProject.SelectedValue,"",ddlPayam.SelectedValue,ddlBoma.SelectedValue,txtWorkcode.Text,txtWorkcode0.Text);
            h3.InnerText = "Total Wage Requests";
        }
        else  if (hdnStat.Value.ToString() == "2")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Pending Wage Requests";
        }
        else  if (hdnStat.Value.ToString() == "3")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Approved Wage Requests";
        }
        else if (hdnStat.Value.ToString() == "4")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Rejected Wage Requests";
        }
        else if (hdnStat.Value.ToString() == "50")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Rejected Wage Requests";
        }


        if(ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                grdDetails.Columns[10].Visible = false;
                grdDetails.Columns[11].Visible = false;
                grdDetails.Columns[9].Visible = false;
                if (Session["user_type"].ToString().Trim() == "PA")
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "6")
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "50")
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[11].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "3")
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = true;
                        grdDetails.Columns[10].Visible = true;
                    }
                    else
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = false;
                    }
                }
                else
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        grdDetails.Columns[8].Visible = true;
                        grdDetails.Columns[9].Visible = false;
                    }
                    else if (Request.QueryString[0].ToString() == "6")
                    {
                        grdDetails.Columns[8].Visible = true;
                        grdDetails.Columns[9].Visible = false;
                    }

                    else
                    {
                        grdDetails.Columns[8].Visible = false;
                        grdDetails.Columns[9].Visible = false;
                    }
                }

                if (Session["user_type"].ToString().Trim() == "PA" && (Request.QueryString[0].ToString().Trim() == "3" || Request.QueryString[0].ToString().Trim() == "4") )
                {
                    grdDetails.Columns[9].Visible = false;
                }
               
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
        }
      


    }
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hdfid");
            hdfDeptID.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intWagePaymentReqid"));
            DropDownList ddlStatus = (DropDownList)e.Row.FindControl("ddlStatus");
            Label lblwagelabel = (Label)e.Row.FindControl("lblwagelabel");
            TextBox txtExchangerate = (TextBox)e.Row.FindControl("txtExchangerate");

            lblwagelabel.Visible = false;
            txtExchangerate.Visible = false;


            if (Session["user_type"].ToString().Trim() == "Cordinator")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "2"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "100"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "7"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1000"));
                }
            }
            else if (Session["user_type"].ToString().Trim() == "Specialist")
            {
               
                if (Request.QueryString[0].ToString() == "2")
                {
                    lblwagelabel.Visible = true;
                    txtExchangerate.Visible = true;
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "3"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "101"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "8"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1001"));
                }
            }
            else if (Session["user_type"].ToString().Trim() == "Manager")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "4"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "102"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "9"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1002"));
                }
            }
            else if (Session["user_type"].ToString().Trim() == "PA")
            {
                if (Request.QueryString[0].ToString() == "2")
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "5"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "103"));
                }
                else
                {
                    ddlStatus.Items.Insert(0, new ListItem("Approved", "10"));
                    ddlStatus.Items.Insert(1, new ListItem("Rejected", "1003"));
                }
            }   










           // ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intWorkId") + "','" + DataBinder.Eval(e.Row.DataItem, "invoiceid") + "')");
        }
    }


    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gen.getProjects(ddlProject, ddlIP.SelectedValue);
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");

        }
    }
    void getPayams()
    {
        ddlPayam.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }
    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomas();
    }
    void getBomas()
    {
        ddlBoma.Items.Clear();
        if (ddlPayam.SelectedIndex != 0)
        {
            Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
        }
        else
        {
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlCounties_SelectedIndexChanged1(object sender, EventArgs e)
    {
        getPayams();
    }
    protected void ddlPayam_SelectedIndexChanged1(object sender, EventArgs e)
    {
        getBomas();
    }

    protected void ddlAreasofWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        getWorkActivities();
    }
    void getWorkActivities()
    {

       
    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

        getworksbyPojects();
    }

    void getworksbyPojects()
    {

      
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click1(object sender, EventArgs e)
    {

        Button BtnSave = (Button)sender;

        GridViewRow row = (GridViewRow)BtnSave.NamingContainer;
        HiddenField hdfApplID = (HiddenField)row.FindControl("hdfid");
         DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
        TextBox txtPromotor = (TextBox)row.FindControl("txtPromotor");
         TextBox txtExchangerate = (TextBox)row.FindControl("txtExchangerate");

    //    HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hdfid");
         if (Session["user_type"].ToString().Trim() == "Specialist")
         {

             if (Request.QueryString[0].ToString() == "2")
             {
                 if (txtExchangerate.Text.Trim() == "")
                 {
                     lblMsg.Text = "Please Enter Wage Rate";
                     return;
                 }
                 if (txtExchangerate.Text.Trim() == "0")
                 {
                     lblMsg.Text = "Please Enter Wage Rate";
                     return;
                 }

             }
         }



        int a = Gen.InsApprovalStatus(hdfApplID.Value, ddlStatus.SelectedValue, Session["uid"].ToString().Trim(), txtPromotor.Text);
        
        if (Session["user_type"].ToString().Trim() == "Specialist")
            {
               
                if (Request.QueryString[0].ToString() == "2")
                {

                    int ss = Gen.Updatewagedate(hdfApplID.Value, txtExchangerate.Text, Session["uid"].ToString().Trim(), 0);
                }
          }
        
        if (a >= 0)
        {
            lblresult.Text = "Submitted Successfully";

            string status="";
            if (ddlStatus.SelectedValue == "2")
            {
                status = "The above payment has been reviewed by SSSNP program - Co-Ordinator.Please verify it";
            }
            else if (ddlStatus.SelectedValue == "3")
            {
                status = "The above payment has been verified by SSSNP program - Specialist.Please verify it";
            }
            else if (ddlStatus.SelectedValue == "4")
            {
                status = "The above wage payroll has been approved for payment. Please process and pay beneficiaries.";
            }
            else if (ddlStatus.SelectedValue == "100" )
            {
                status = "The above payment has been rejected by SSSNP program - Co-Ordinator.Please verify it";
            }
            else if (ddlStatus.SelectedValue == "101" )
            {
                status = "The above payment has been rejected by SSSNP program - Specialist.Please verify it";
            }
            else if (ddlStatus.SelectedValue == "103")
            {
                status = "The above payment has been rejected by SSSNP program - Manager.Please verify it";
            }
            else
            {
                status = "Done";
            }

          
            fillgrid();
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("WagesDasboardDrildown.aspx?Status=" + Request.QueryString[0].ToString().Trim());
    }



    public void SendMailforWages(string numbers, string anothermail,string Ipname,string WorkCode,string WorkRequest)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);


        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        string anothermail1 = to + ",";
        string[] multi = anothermail1.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }
        mail.From = new MailAddress(from, ":: SSSNP Public works MIS :: Wages", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS – Notification on Wage Payroll for Review";
        
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<H2>Notification on Wage Payroll for Review</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + WorkCode + "<br> <br> Work Request No: " + WorkRequest + "<br>  Remarks: " + numbers + " <br> <br> URL : http://45.35.64.117/Publicworks <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>Action Africa Help International<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
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
}

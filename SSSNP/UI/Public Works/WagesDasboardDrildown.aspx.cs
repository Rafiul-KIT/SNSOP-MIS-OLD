#region Using/Inherits Classess

using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using Telerik.Web.UI;
using System.Drawing;
using System.Data.SqlClient;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Net;
using SSSNP_SNSOP.Extensions;
using System.Text;
using System.IO;
using System.Transactions;
using static ImportBeneficiaryAll;
using SSSNP.Entity.Payroll;
using SSSNP.Network;
using System.Configuration;
using Newtonsoft.Json;
using SSSNP_SNSOP.App_Code;
#endregion
#region Amendment History Begin
//Form Name : Wage Process Dashboard Drilldown and Approvals at Coordinator, Specialist, Manager
//Purpose : To  Wage Process Dashboard Drilldown and Approvals in Coordinator, Specialist, Manager Loign
//Tables used : td_WorkWageRequest,td_WorkWageRequestPA,td_WorkWageRequest,td_WageReqBenPADet,tm_States,tm_Counties,tm_Payams,tm_Bomas,tbl_WorkMaster
//Stored procedures Used : getWageProcessDashboard,sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,getIpDet,GetWagesdashboarddrildownDetLat,Updatewagedate,GetProjectsbyIP,getIpDet
#endregion Amendment History End
public partial class WagesDasboardDrildown : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    ErrorLogger eLog = new ErrorLogger();
    comFunctions cmf = new comFunctions();
    private EmailService emailService = new EmailService();
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        #region Is not Postback Statement
        if (!IsPostBack)
        {
            clsGeneral.getIpDet(ddlIP);

            Gen.getStates(ddlState);
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString[0].ToString() != "")
                {
                    hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
        #endregion
    }
    #endregion


    #region Search Report and Filling Details Functions
    void fillgrid()
    {
        DataSet ds = new DataSet();
        if (Request.QueryString[0].ToString().Trim() == "0")
        {
            //check if the IP is IP_approver
            DataSet dsIsApprover = new DataSet();
            dsIsApprover = Gen.CheckIsIPApprover(Session["uid"].ToString().Trim());
            if (dsIsApprover.Tables[0].Rows.Count > 0)
            {
                ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            }
            else
            {
                //empty table
                lblMsg.Text = "You are not authorized to see Wage Requests drafts";
                lblMsg.ForeColor = Color.Red;
            }
            h3.InnerText = "Wage Request Drafts";
        }

        if (Request.QueryString[0].ToString().Trim() == "1")
        {
            ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            h3.InnerText = "Total Wage Requests";
        }
        else if (hdnStat.Value.ToString() == "2")
        {
            if (Session["user_type"].ToString() == "Specialist")
            {
                ds = clsGeneral.GetWagesdashboarddrildownDetAttachCount(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            }
            else
            {
                ds = clsGeneral.GetWagesdashboarddrildownDet(Session["uid"].ToString().Trim(), Session["user_type"].ToString().Trim(), Request.QueryString[0].ToString().Trim(), ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlProject.SelectedValue, "", ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txtWorkcode0.Text);
            }
            h3.InnerText = "Pending Wage Requests";
        }
        else if (hdnStat.Value.ToString() == "3")
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


        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows.Count == 1)
                {
                    lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Record found.";
                }
                else
                {
                    lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                }
                lblMsg.ForeColor = Color.Green;
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                grdDetails.Columns[10].Visible = false;
                grdDetails.Columns[11].Visible = false;
                grdDetails.Columns[9].Visible = false;
                if (Session["user_type"].ToString().Trim() == "PA")
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[10].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "6")
                    {
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[10].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "50")
                    {
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[10].Visible = false;
                        grdDetails.Columns[12].Visible = true;
                    }
                    else if (Request.QueryString[0].ToString() == "3")
                    {
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[10].Visible = true;
                        grdDetails.Columns[11].Visible = true;
                    }
                    else
                    {
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[10].Visible = false;
                    }
                }
                else
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        grdDetails.Columns[9].Visible = true;
                        grdDetails.Columns[10].Visible = false;
                    }
                    else if (Request.QueryString[0].ToString() == "6")
                    {
                        grdDetails.Columns[9].Visible = true;
                        grdDetails.Columns[10].Visible = false;
                    }

                    else
                    {
                        grdDetails.Columns[9].Visible = false;
                        grdDetails.Columns[10].Visible = false;
                    }
                }

                if (Session["user_type"].ToString().Trim() == "PA" && (Request.QueryString[0].ToString().Trim() == "3" || Request.QueryString[0].ToString().Trim() == "4"))
                {
                    grdDetails.Columns[9].Visible = false;
                }

            }
            else
            {
                lblMsg.Text = "No Records found.";
                lblMsg.ForeColor = Color.Green;
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
            lblMsg.ForeColor = ColorTranslator.FromHtml("#006600");
        }



    }

    #endregion


    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }

    #region Grid view Events
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
            Button btUpload = (Button)e.Row.FindControl("btnUploadFile");
            TextBox txtAmountSSP = (TextBox)e.Row.FindControl("txtAmountSSP");
            Panel pnl = (Panel)e.Row.FindControl("pnlExchange");
            Panel pnlAmount = (Panel)e.Row.FindControl("pnlAmount");


            string amount;
            lblwagelabel.Visible = false;
            txtExchangerate.Visible = false;
            btUpload.Visible = false;
            txtAmountSSP.Text = "0";
            pnl.Visible = false;
            pnlAmount.Visible = false;


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
                    btUpload.Visible = true;
                    pnl.Visible = true;
                    pnlAmount.Visible = false;

                    amount = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AmountInSSP"));
                    txtAmountSSP.Text = amount == null ? "0" : amount;

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


            // amount = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "AmountInSSP"));
            //txtAmountSSP.Text = amount == null ? "0" : amount;


        }
    }

    #endregion

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
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

    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region Button Click functionalitiesS Search adn Clear
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void BtnSave_Click1(object sender, EventArgs e)
    {
        lblresult.Text = "";
        lblMsg.Text = "";
        lblMsg.ForeColor = Color.Green;
        Button BtnSave = (Button)sender;

        GridViewRow row = (GridViewRow)BtnSave.NamingContainer;
        HiddenField hdfApplID = (HiddenField)row.FindControl("hdfid");
        HiddenField wageRateUSD = (HiddenField)row.FindControl("hdWageRate");


        DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
        TextBox txtPromotor = (TextBox)row.FindControl("txtPromotor");
        TextBox txtExchangerate = (TextBox)row.FindControl("txtExchangerate");

        decimal amountInSSPValue = 0;
        try
        {
            decimal valueUSD = 0;
            decimal.TryParse(wageRateUSD.Value, out valueUSD);

            decimal valueSSP = 0;
            decimal.TryParse(txtExchangerate.Text, out valueSSP);

            amountInSSPValue = valueSSP * valueUSD;
        }
        catch (Exception)
        {
            amountInSSPValue = 0;

        }

        if (Session["user_type"].ToString().Trim() == "Specialist" && ddlStatus.SelectedItem.Text == "Approved")
        {

            if (Request.QueryString[0].ToString() == "2")
            {
                if (txtExchangerate.Text.Trim() == "")
                {
                    lblMsg.Text = "Please Enter Wage Rate";
                    lblMsg.ForeColor = Color.Orange;
                    return;
                }
                if (txtExchangerate.Text.Trim() == "0")
                {
                    lblMsg.Text = "Please Enter Wage Rate";
                    lblMsg.ForeColor = Color.Orange;
                    return;
                }

            }

            #region Check attachment (always mandatory)
            string strAttacchCount = null;
            if (grdDetails.DataKeys[row.RowIndex] != null && grdDetails.DataKeys[row.RowIndex].Value != null && grdDetails.DataKeys[row.RowIndex].Value.ToString() != string.Empty)
            {
                strAttacchCount = grdDetails.DataKeys[row.RowIndex].Value.ToString();
            }
            else
            {
                strAttacchCount = "0";
            }

            //Field "ReqNo"
            string strReqNo = row.Cells[3].Text;
            int iStoredAttachments = Gen.getWageAttachmentStoredCount(strReqNo);
            int iGridAttachCount = System.Convert.ToInt32(strAttacchCount);
            if (iStoredAttachments <= iGridAttachCount)
            {
                lblMsg.Text = "Please Attach at least 1 document";
                lblMsg.ForeColor = Color.Red;
                return;
            }
            #endregion
        }

        int a = 0;

        Boolean sendSuccess = false;

        if (Session["user_type"].ToString().Trim() == "Manager" && ddlStatus.SelectedItem.Text == "Approved")
        {
            if (Request.QueryString[0].ToString() == "2")
            {
                sendSuccess = SendWageRequestByApi(hdfApplID.Value);
            }
        }

        using (System.Transactions.TransactionScope transactionScope = new System.Transactions.TransactionScope())
        {
            try
            {
                //Update Coordinator approval date (CApproveDate)
                if (Session["user_type"].ToString().Trim() == "Cordinator" && ddlStatus.SelectedItem.Text == "Approved")
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        //Update Exchange rate and Specialist approval date (SApproveDate)
                        int ss = Gen.UpdateCApproveDate(hdfApplID.Value, Session["uid"].ToString().Trim());
                    }
                }

                //Update Exchange rate and Specialist approval date (SApproveDate)
                if (Session["user_type"].ToString().Trim() == "Specialist" && ddlStatus.SelectedItem.Text == "Approved")
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        int ss = Gen.Updatewagedate(hdfApplID.Value, txtExchangerate.Text, Session["uid"].ToString().Trim(), amountInSSPValue);
                    }
                }

                //Update Manager approval date (CApproveDate)
                if (Session["user_type"].ToString().Trim() == "Manager" && ddlStatus.SelectedItem.Text == "Approved")
                {
                    if (Request.QueryString[0].ToString() == "2")
                    {
                        if (sendSuccess)
                        {
                            //Update Exchange rate and Specialist approval date (SApproveDate)
                            int ss = Gen.UpdateMApproveDate(hdfApplID.Value, Session["uid"].ToString().Trim());
                        }
                        else
                        {
                            lblMsg.Text = "Approval Failed due to API connectivity issues. Please contact the administrator";
                            lblMsg.ForeColor = Color.Red;
                            return;
                        }
                    }
                }

                a = Gen.InsApprovalStatus(hdfApplID.Value, ddlStatus.SelectedValue, Session["uid"].ToString().Trim(), txtPromotor.Text);
                transactionScope.Complete();
            }
            catch (TransactionException ex)
            {
                transactionScope.Dispose();
            }
        }
        if (a >= 0)
        {
            lblresult.Text = "Submitted Successfully";

            if (Session["user_type"].ToString().Trim() == "Cordinator")
            {
                if (ddlStatus.SelectedItem.Text == "Approved")
                {
                    sendEmailForWageRequest(hdfApplID.Value, row.Cells[3].Text, "Specialist", true);
                } 
                else
                {
                    string emailBody = "A wage request with number: " + row.Cells[3].Text + " has been rejected by Initial Reviewer. " +
                        "Please Review it.";
                    emailService.sendEmailToIpApprover("SNSOP Wage Request", emailBody);
                }
            }
            else if (Session["user_type"].ToString().Trim() == "Specialist")
            {
                if (ddlStatus.SelectedItem.Text == "Approved")
                {
                    sendEmailForWageRequest(hdfApplID.Value, row.Cells[3].Text, "Manager", true);
                }
                else
                {
                    sendEmailForWageRequest(hdfApplID.Value, row.Cells[3].Text, "Cordinator", false);
                }
            }
            else if (Session["user_type"].ToString().Trim() == "Manager")
            {
                if (ddlStatus.SelectedItem.Text == "Approved")
                {
                }
                else
                {
                    sendEmailForWageRequest(hdfApplID.Value, row.Cells[3].Text, "Specialist", false);
                }
            }

            string status = "";
            if (ddlStatus.SelectedValue == "2")
            {
                status = "The above payment has been reviewed by SNSOP program - Initial Reviewer. Please verify it";
            }
            else if (ddlStatus.SelectedValue == "3")
            {
                status = "The above payment has been verified by SNSOP program - Financial Approver. Please verify it";
            }
            else if (ddlStatus.SelectedValue == "4")
            {
                status = "The above wage payroll has been approved for payment. Please process and pay beneficiaries.";
            }
            else if (ddlStatus.SelectedValue == "100")
            {
                status = "The above payment has been rejected by SNSOP program - Initial Reviewer.Please verify it";
            }
            else if (ddlStatus.SelectedValue == "101")
            {
                status = "The above payment has been rejected by SNSOP program - Financial Approver. Please verify it";
            }
            else if (ddlStatus.SelectedValue == "103")
            {
                status = "The above payment has been rejected by SNSOP program - Final Payment Authorizer. Please verify it";
            }
            else
            {
                status = "Done";
            }

            //SendMailforWages(status, "", row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text);
            fillgrid();
        }
    }

    private void sendEmailForWageRequest(string intWagePaymentReqid, string reqNo, string userType, Boolean approved)
    {
        string emailSubject = "SNSOP Wage Request";

        string newUserType = "";
        if (userType == "Cordinator")
        {
            newUserType = "Financial Approver";
        } 
        else if (userType == "Specialist")
        {
            newUserType = approved ? "Initial Reviewer" : "Final Payment Authorizer";
        }
        else if (userType == "Manager")
        {
            newUserType = "Financial Approver";
        }

        string emailBody = "";
        if (approved)
        {
            emailBody = "A wage request with number: "+ reqNo+" has been approved by " + newUserType + " Please review it.";
        } 
        else
        {
            emailBody = "A wage request with number: " + reqNo + " has been rejected by " + newUserType + " Please review it.";
        }
        emailService.sendEmailForWageRequest(intWagePaymentReqid, userType, emailSubject, emailBody);
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("WagesDasboardDrildown.aspx?Status=" + Request.QueryString[0].ToString().Trim());
    }
    #endregion

    #region Send Manager Approved Wage Request Data by API
    private Boolean SendWageRequestByApi(string uid)
    {
        try
        {
            string result = "";
            DB.DB con = new DB.DB();
            con.OpenConnection();
            SqlDataAdapter da;
            DataSet dsn = new DataSet();
            PayrollDto request = new PayrollDto();
            try
            {
                dsn = Gen.GetWageRequestDetialsbyID(uid);
                if (dsn == null || dsn.Tables.Count <= 0) return false;

                con.CloseConnection();
                DataTable ds = (DataTable)dsn.Tables[0];

                if (ds.Rows.Count > 0)
                {
                    request = DataTableToJSONWithJavaScriptSerializerV1(ds, uid);
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }


            string uri = ConfigurationManager.AppSettings["PushApprovedPaymentsEndpoint"];

            String token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

            uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["PushApprovedPaymentsEndpoint"];
            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["PushApprovedPaymentsEndpoint"];
            }
            Uri urlCheck = new Uri(uri);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + token);

            string reqStr = "";

            try
            {
                reqStr = JsonConvert.SerializeObject(request);

                var data = Encoding.ASCII.GetBytes(reqStr);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";
                httpWReq.ContentLength = data.Length;

                var newStream = httpWReq.GetRequestStream(); // get a ref to the request body so it can be modified
                newStream.Write(data, 0, data.Length);
                newStream.Flush();
                newStream.Close();

                //using (var stream = httpWReq.GetRequestStream())
                //{
                //    stream.Write(data, 0, data.Length);
                //}

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                String jsonresponse = "";
                String temp = null;
                while ((temp = reader.ReadLine()) != null)
                {
                    jsonresponse += temp;
                }
                JavaScriptSerializer js = new JavaScriptSerializer();
                js.MaxJsonLength = Int32.MaxValue;
                SavePayrollResponse savePayrollResponse = js.Deserialize<SavePayrollResponse>(jsonresponse);
                if (savePayrollResponse != null && savePayrollResponse.operationResult)
                {
                    int value = Gen.InsUpdWageRequestAfisSync(Convert.ToInt32(uid), 1);
                }
                else
                {
                    int value = Gen.InsUpdWageRequestAfisSync(Convert.ToInt32(uid), 0);
                }
                return true;
            }
            catch (Exception ex)
            {
                eLog.LogError(ex);
                Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
                return false;
            }

            //try
            //{
            //    SavePayrollResponse response = NetworkService.SubmitRequest<SavePayrollResponse>
            //        (requestModel: request, url: uri, requestType: "POST", accessToken: token);

            //    if (response != null && response.operationResult)
            //    {
            //        // Insert this record into the td_WageRequestAfisSync table
            //        int value = Gen.InsUpdWageRequestAfisSync(Convert.ToInt32(uid), 1);
            //    }
            //      return true;
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
            //      return false;
            //}
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
            return false;
        }
    }

    private PayrollDto DataTableToJSONWithJavaScriptSerializerV1(DataTable table, string uid)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = Int32.MaxValue;
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();

        PayrollDto payrollRequest = new PayrollDto();
        foreach (DataRow row in table.Rows)
        {
            PayrollDataDto payrollData = new PayrollDataDto();
            payrollData.boma = (cmf.isOk(row["boma"])) ? long.Parse(row["boma"].ToString()) : 0;
            payrollData.county = (cmf.isOk(row["county"])) ? long.Parse(row["county"].ToString()) : 0;
            payrollData.dateofCreation = (row["dateofCreation"] != null) ? row["dateofCreation"].ToString() : null;
            payrollData.enrollmentNo = (row["enrollmentNo"] != null) ? row["enrollmentNo"].ToString() : null;
            payrollData.exchangerateattime = (row["exchangerateattime"] != null) ? double.Parse(row["exchangerateattime"].ToString()) : 0.0;
            payrollData.intBenid = (row["intBenid"] != null) ? int.Parse(row["intBenid"].ToString()) : 0;
            payrollData.intWagePaymentReqid = (row["intWagePaymentReqid"] != null) ? int.Parse(row["intWagePaymentReqid"].ToString()) : 0;
            payrollData.intWagesReqBenTransid = (row["intWagesReqBenTransid"] != null) ? int.Parse(row["intWagesReqBenTransid"].ToString()) : 0;
            payrollData.intWorkid = (row["intWorkid"] != null) ? int.Parse(row["intWorkid"].ToString()) : 0;
            payrollData.payam = (cmf.isOk(row["payam"])) ? long.Parse(row["payam"].ToString()) : 0;
            payrollData.paymentCycle = (row["paymentCycle"] != null) ? row["paymentCycle"].ToString() : null;
            payrollData.reqFromDate = (row["reqFromDate"] != null) ? row["reqFromDate"].ToString() : null;
            payrollData.reqNo = (row["reqNo"] != null) ? row["reqNo"].ToString() : null;
            payrollData.reqToDate = (row["reqToDate"] != null) ? row["reqToDate"].ToString() : null;
            payrollData.state = (cmf.isOk(row["state"])) ? long.Parse(row["state"].ToString()) : 0;
            payrollData.status = (row["status"] != null) ? row["status"].ToString() : null;
            payrollData.supportType = (payrollData.intWorkid == 999) ? "DIS" : "LIPW";
            payrollData.totAttendance = (cmf.isOk(row["totAttendance"])) ? int.Parse(row["totAttendance"].ToString()) : 0;
            payrollData.wageRateSSPTotal = (cmf.isOk(row["wageRateSSPTotal"])) ? double.Parse(row["wageRateSSPTotal"].ToString()) : 0;
            payrollData.wageRateUSD = (cmf.isOk(row["wageRateUSD"])) ? double.Parse(row["wageRateUSD"].ToString()) : 0;
            payrollData.wageRateUSDTotal = (cmf.isOk(row["wageRateUSDTotal"])) ? double.Parse(row["wageRateUSDTotal"].ToString()) : 0;
            payrollData.workCode = (row["workCode"] != null) ? row["workCode"].ToString() : null;

            payrollData.houseHoldDetail = getHouseholdInfo(row);

            payrollData.alternates = getAlternates(row);

            payrollRequest.payrollData.Add(payrollData);
        }

        return payrollRequest;
    }

    private HouseholdDetailDto getHouseholdInfo(DataRow row)
    {
        try
        {
            HouseholdDetailDto dto = new HouseholdDetailDto();

            if (cmf.isOk(row["NameofHousehold"] != null))
            {
                var names = row["NameofHousehold"].ToString().Split(' ');
                if (names != null && names.Length > 0)
                {
                    if (names.Length >= 3)
                    {
                        dto.firstName = names[0];
                        dto.middleName = names[1];
                        dto.lastName = names[2];
                    }
                }
            }
            if (cmf.isOk(row["Age"]))
            {
                dto.age = int.Parse(row["Age"].ToString());
            }
            if (cmf.isOk(row["gender"]))
            {
                dto.gender = int.Parse(row["gender"].ToString());
            }
            dto.houseHoldNumber = (row["household_number"] != null) ? row["household_number"].ToString() : null;
            dto.phoneNumber = (row["ContactNo"] != null) ? row["ContactNo"].ToString() : null;
            return dto;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    private List<PayrollAlternateDto> getAlternates(DataRow row)
    {
        try
        {
            List<PayrollAlternateDto> list = new List<PayrollAlternateDto>();
            if (cmf.isOk(row["Name1stAlternatePayee"]))
            {
                PayrollAlternateDto dto = new PayrollAlternateDto();
                if (cmf.isOk(row["FirstAlternatePayeeAge"]))
                {
                    dto.age = int.Parse(row["FirstAlternatePayeeAge"].ToString());
                }
                if (cmf.isOk(row["FirstAlternatePayeeGender"]))
                {
                    dto.gender = int.Parse(row["FirstAlternatePayeeGender"].ToString());
                }
                dto.phoneNumber = (row["FirstAlternatePayeeMobile"] != null) ? row["FirstAlternatePayeeMobile"].ToString() : null;
                dto.nationalId = (row["FirstAlternatePayeeNationID"] != null) ? row["FirstAlternatePayeeNationID"].ToString() : null;
                var names = row["Name1stAlternatePayee"].ToString().Split(' ');
                if (names != null && names.Length > 0)
                {
                    if (names.Length >= 3)
                    {
                        dto.firstName = names[0];
                        dto.middleName = names[1];
                        dto.lastName = names[2];
                    }
                }

                list.Add(dto);
            }

            if (cmf.isOk(row["Name2ndAlternatePayee"]))
            {
                string name = row["Name2ndAlternatePayee"].ToString();
                PayrollAlternateDto dto = new PayrollAlternateDto();
                if (cmf.isOk(row["SecondAlternatePayeeAge"]))
                {
                    dto.age = int.Parse(row["SecondAlternatePayeeAge"].ToString());
                }
                if (cmf.isOk(row["SecondAlternatePayeeGender"]))
                {
                    dto.gender = int.Parse(row["SecondAlternatePayeeGender"].ToString());
                }
                dto.phoneNumber = (row["SecondAlternatePayeeMobile"] != null) ? row["SecondAlternatePayeeMobile"].ToString() : null;
                dto.nationalId = (row["SecondAlternateNationalID"] != null) ? row["SecondAlternateNationalID"].ToString() : null;
                var names = row["Name2ndAlternatePayee"].ToString().Split(' ');
                if (names != null && names.Length > 0)
                {
                    if (names.Length >= 3)
                    {
                        dto.firstName = names[0];
                        dto.middleName = names[1];
                        dto.lastName = names[2];
                    }
                }

                list.Add(dto);
            }

            return list;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return null;
        }
    }

    #endregion

    #region Mail Send Functionalities

    public void SendMailforWages(string numbers, string anothermail, string Ipname, string WorkCode, string WorkRequest)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("{0}", sssnpmail);


        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();

        if (Session["user_type"].ToString() == "Cordinator")
        {
            anothermail = "";
            anothermail = System.Configuration.ConfigurationManager.AppSettings["FourEmailRecipients"];
        }
        else if (Session["user_type"].ToString() == "Specialist")
        {
            anothermail = "";
            anothermail = System.Configuration.ConfigurationManager.AppSettings["ThreeEmailRecipients"];
        }
        else if (Session["user_type"].ToString() == "Manager")
        {
            anothermail = "";
            anothermail = System.Configuration.ConfigurationManager.AppSettings["TwoEmailRecipients"];
        }
        else
        {
            anothermail = "";
            anothermail = System.Configuration.ConfigurationManager.AppSettings["TwoEmailRecipients"];
        }

        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }


        mail.From = new MailAddress(from);

        mail.Subject = "SNSOP Public Works MIS – Notification on Wage Payroll for Review";

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<H2>Notification on Wage Payroll for Review</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + WorkCode + "<br> <br> Work Request No: " + WorkRequest + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
        mail.BodyEncoding = System.Text.Encoding.UTF8;
        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        SmtpClient client = new SmtpClient();
        client.Port = 587;
        client.Host = smtpserver;
        client.EnableSsl = true;
        client.UseDefaultCredentials = false;
        client.Credentials = new System.Net.NetworkCredential("no-reply@southsudansafetynet.info", "2sOQenMaTV-?");

        MailAddress fromMail = new MailAddress("no-reply@southsudansafetynet.info");
        MailAddress toMail = new MailAddress(ConfigurationManager.AppSettings["OneEmailRecipient"].ToString());

        mail = new MailMessage(fromMail, toMail);
        mail.Subject = "SNSOP Public Works MIS – Notification on Wage Payroll for Review";
        mail.Body = "Notification on Wage Payroll for Review\nWork Code: " + WorkCode + "\nWork Request No: " + WorkRequest + "\nPlease Login by clicking the following link.\n" + sssnpweb;

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
        } // end try

    }
    #endregion

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {

    }
}

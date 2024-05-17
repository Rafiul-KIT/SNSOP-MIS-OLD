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
using static ImportBeneficiaryAll;
using SSSNP.Entity.Payroll;
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

        int a = Gen.InsApprovalStatus(hdfApplID.Value, ddlStatus.SelectedValue, Session["uid"].ToString().Trim(), txtPromotor.Text);

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
                //Update Exchange rate and Specialist approval date (SApproveDate)
                int ss = Gen.UpdateMApproveDate(hdfApplID.Value, Session["uid"].ToString().Trim());

                SendWageRequestByApi(Session["uid"].ToString().Trim());
            }
        }


        if (a >= 0)
        {
            lblresult.Text = "Submitted Successfully";

            string status = "";
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
            else if (ddlStatus.SelectedValue == "100")
            {
                status = "The above payment has been rejected by SSSNP program - Co-Ordinator.Please verify it";
            }
            else if (ddlStatus.SelectedValue == "101")
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

            SendMailforWages(status, "", row.Cells[1].Text, row.Cells[2].Text, row.Cells[3].Text);
            fillgrid();
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("WagesDasboardDrildown.aspx?Status=" + Request.QueryString[0].ToString().Trim());
    }
    #endregion

    #region Send Manager Approved Wage Request Data by API
    private void SendWageRequestByApi(string uid)
    {
        try
        {
            string result = "";
            DB.DB con = new DB.DB();
            con.OpenConnection();
            SqlDataAdapter da;
            DataSet dsn = new DataSet();
            try
            {
                da = new SqlDataAdapter("getBenfWorkWageDetailsService", con.GetConnection);
                da.SelectCommand.CommandType = CommandType.StoredProcedure;
                da.Fill(dsn);
                Session["ErrorDataW"] = dsn.Tables[0];

                con.CloseConnection();
                DataTable ds = new DataTable();
                ds = (DataTable)Session["ErrorDataW"];

                if (ds.Rows.Count > 0)
                {
                    result = DataTableToJSONWithJavaScriptSerializer(ds, uid);
                }
                else
                {
                    result = "{ \"BenfWorkWagesDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
                }
            }
            catch (Exception ex)
            {
                result = "{ \"BenfWorkWagesDetails\":\"failure\",\"status\":\"0\",\"result\": \"null\"}";
            }


            string uri = System.Configuration.ConfigurationManager.AppSettings["PushApprovedPaymentsDevEndpoint"];
            if (System.Configuration.ConfigurationManager.AppSettings["AZUREAD_ENV"].Equals("prod"))
            {
                uri = System.Configuration.ConfigurationManager.AppSettings["PushApprovedPaymentsProdEndpoint"];
            }
            Uri urlCheck = new Uri(uri);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

            string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());

            try
            {
                var data = Encoding.ASCII.GetBytes(result);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/x-www-form-urlencoded";
                httpWReq.ContentLength = data.Length;

                using (var stream = httpWReq.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }

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
                RootObject obj = js.Deserialize<RootObject>(jsonresponse);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error when pushing payment data by api. Error:\n" + ex.ToString());
        }
    }

    private string DataTableToJSONWithJavaScriptSerializer(DataTable table, string uid)
    {
        JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
        jsSerializer.MaxJsonLength = Int32.MaxValue;
        List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
        Dictionary<string, object> childRow;
        foreach (DataRow row in table.Rows)
        {
            //if (row["intWagePaymentReqid"] != null && row["intWagePaymentReqid"].ToString() == uid)
            //{
            childRow = new Dictionary<string, object>();
            foreach (DataColumn col in table.Columns)
            {
                string key = col.ColumnName;
                if (key.Equals("ReqFromDate"))
                {
                    key = "reqFromDate";
                }
                else if (key.Equals("ReqToDate"))
                {
                    key = "reqToDate";
                }
                else if (key.Equals("Exchangerateattime"))
                {
                    key = "exchangerateattime";
                }
                else if (key.Equals("WageRateUSD"))
                {
                    key = "wageRateUSD";
                }
                else if (key.Equals("ReqNo"))
                {
                    key = "reqNo";
                }
                else if (key.Equals("EnrollmentNo"))
                {
                    key = "enrollmentNo";
                }
                else if (key.Equals("WorkCode"))
                {
                    key = "workCode";
                }
                else if (key.Equals("TotAttendance"))
                {
                    key = "totAttendance";
                }
                else if (key.Equals("WageRateUSDTotal"))
                {
                    key = "wageRateUSDTotal";
                }
                else if (key.Equals("DateofCreation"))
                {
                    key = "dateofCreation";
                }
                else if (key.Equals("Status"))
                {
                    key = "status";
                }
                else if (key.Equals("PaymentCycle"))
                {
                    key = "paymentCycle";
                }
                else if (key.Equals("WageRateSSPTotal"))
                {
                    key = "wageRateSSPTotal";
                }
                else if (key.Equals("household_number"))
                {
                    continue;
                }
                else if (key.Equals("intBenid"))
                {
                    DataSet dsChild = new DataSet();
                    dsChild = Gen.GetBenficiaryDetByID(row[col].ToString());

                    if (dsChild.Tables[0].Rows.Count > 0)
                    {
                        HouseholdDetailDto dto = new HouseholdDetailDto();
                        if (dsChild.Tables[0].Rows[0]["Age"].ToString() != "") {
                            dto.age = Convert.ToInt32(dsChild.Tables[0].Rows[0]["Age"].ToString());
                        }
                        dto.lastName = dsChild.Tables[0].Rows[0]["NameofHousehold"].ToString();
                        dto.phoneNumber = dsChild.Tables[0].Rows[0]["ContactNo"].ToString();

                        childRow.Add("houseHoldDetail", dto);
                    }
                }

                childRow.Add(key, row[col]);
            }
            parentRow.Add(childRow);
            //}
        }

        return jsSerializer.Serialize(parentRow);
    }
    #endregion

    #region Mail Send Functionalities

    public void SendMailforWages(string numbers, string anothermail, string Ipname, string WorkCode, string WorkRequest)
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);


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

  
        mail.From = new MailAddress(from, ":: SSSNP Public works MIS :: Wages", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS – Notification on Wage Payroll for Review";

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "<H2>Notification on Wage Payroll for Review</H2><br> <br> <b> IP NAME: " + Ipname + "</b> <br><br> Work Code: " + WorkCode + "<br> <br> Work Request No: " + WorkRequest + "<br>  Remarks: " + numbers + " <br> <br> URL : " + sssnpweb + " <br><br> Please Login by clicking the above link. <br> Thanks & Regards <br>" + Ipname + "<br>";
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
        } // end try

    }
    #endregion

    protected void btnUploadFile_Click(object sender, EventArgs e)
    {

    }
}

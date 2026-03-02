using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Drawing;
#region Amendament History

/// <summary>

/// Project : SNSDP

/// Purpose of Form: SNSDP Wages Entry

/// Working Task : SNSDP Wages Entry

/// Developer : Srikanth

/// Date : 04-08-2017

/// Store Procedures Modified : getWagesReport

/// Tables Used Modified: tr_BenWagesPayment

/// </summary>
#endregion
public partial class UpdateSelectionPayingagencyBenfWages : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int Totalnoofdays1 = 0;
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    decimal TotalAmountSSP = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        this.BtnSave1.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave1, "group"));
        if (!IsPostBack)
        {
            hdfFlagID.Value = "0";
        }

        if (hdfFlagID.Value == "0")
        {

            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }
           

    }

   

    void FillDetails()
    {


        DataSet dsn = new DataSet();
        dsn = Gen.GetWageRequestbyIDPA(Request.QueryString[0].ToString().Trim(),Request.QueryString[1].ToString().Trim());
        if (dsn.Tables[0].Rows.Count > 0)
        {
            hdfFlagID.Value = "1";
            DataSet ds = new DataSet();

           
            //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());
            ds = Gen.GetworkproposalforProgress(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
                lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
                lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
                lblWageRequestNo.Text ="Wage Request No :" + dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                HdfWageRequstNo.Value = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
              txtExchangerate.Text = dsn.Tables[1].Rows[0]["Exchangerateattime"].ToString();
                lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
               lblfromdate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy");
               lbltodate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy");
             //   lblexchangerate.Text = ds.Tables[0].Rows[0]["WageRateUSD"].ToString();
                //if (ds.Tables[3].Rows.Count > 0)
                //{

                //    txtExchangerate.Text = ds.Tables[3].Rows[0]["ExchangeRate"].ToString();
                //    txtExchangerate.Enabled = false;

                //}


            }


            DataSet ds1 = new DataSet();
            ds1 = Gen.getAttendanceCountFromTobyID(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim(), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy"), Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy"), "", "", Request.QueryString[0].ToString().Trim());

            if (ds1.Tables[0].Rows.Count > 0)
            {
                grdDetails.DataSource = ds1.Tables[0];
                grdDetails.DataBind();
               
            }
            else
            {
              

                grdDetails.DataSource = null;
                grdDetails.DataBind();
            }
        }
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
        {
            CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("CheckBox1");


            if (c.Checked == true)
            {
                count++;
            }
            else
            {
            }
        }
        if (count == 0)
        {
            lblmsg0.Text = "At lease one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;

        }
        if (txtExchangerate.Text == "" || txtExchangerate.Text == "0" || txtExchangerate.Text == "0.00")
        {
            lblmsg0.Text = "Please enter exchange rate or more than 0";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        int resultwage = Gen.DeleteWageRequestPayment(Request.QueryString[0].ToString().Trim(), txtExchangerate.Text, Session["uid"].ToString().Trim(), Request.QueryString[1].ToString().Trim());

        for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
        {
            GridViewRow row = (GridViewRow)grdDetails.Rows[iii];
          //  TextBox txtAmt = (TextBox)grdDetails.Rows[iii].Cells[0].FindControl("txtAmt");
            HiddenField HdfReqID = (HiddenField)grdDetails.Rows[iii].Cells[0].FindControl("HdfReqID");
            HiddenField hdfRegBenid = (HiddenField)grdDetails.Rows[iii].Cells[0].FindControl("hdfRegBenid");
            HiddenField HdfBenid = (HiddenField)grdDetails.Rows[iii].Cells[0].FindControl("HdfBenid");
            
            CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("CheckBox1");
            if (c.Checked == true && c.Enabled == true)
            {
                if (txtExchangerate.Text != "" && row.Cells[7].Text != "")
                {



                    int s = Gen.InsUpdBeneficiaryWagesRequestPayingAgent(hdfRegBenid.Value, HdfReqID.Value, hdfID.Value, HdfBenid.Value, row.Cells[5].Text, lblwagerate.Text, row.Cells[6].Text, txtExchangerate.Text, row.Cells[7].Text, Session["uid"].ToString(), Request.QueryString[1].ToString().Trim());

                    if (s == 999)
                    {
                        lblmsg0.Text = "Already Exist. ";
                        success.Visible = false;
                        Failure.Visible = true;
                    }
                    //else
                    //{
                    //    lblmsg.Text = "Submited Successfully..!";
                    //    success.Visible = true;
                    //    Failure.Visible = false;
                    //}
                }
            }
        }


        lblmsg.Text = "Wage Request for Payment Submited Successfully..!";

        success.Visible = true;
        Failure.Visible = false;
       // string message = "Hello! Mudassar.";

        string message = "Wage Request for Payment Submited Successfully..!";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        sb.Append("alert('");

        sb.Append(message);

        sb.Append("');");

        ClientScript.RegisterOnSubmitStatement(this.GetType(), "alert", sb.ToString());
        Response.Redirect("WageProcessDashboard.aspx");



      

    }

   
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HiddenField HdfReqID = (HiddenField)e.Row.Cells[1].FindControl("HdfReqID");
            HiddenField hdfRegBenid = (HiddenField)e.Row.Cells[1].FindControl("hdfRegBenid");
            HiddenField HdfBenid = (HiddenField)e.Row.Cells[1].FindControl("HdfBenid");
            CheckBox CheckBox1 = (CheckBox)e.Row.Cells[1].FindControl("CheckBox1");
            HdfBenid.Value = DataBinder.Eval(e.Row.DataItem, "intBenid").ToString();

            hdfRegBenid.Value = DataBinder.Eval(e.Row.DataItem, "intWagesReqBenTransid").ToString();
            HdfReqID.Value = DataBinder.Eval(e.Row.DataItem, "intWagePaymentReqid").ToString();
            DataSet dsnew1 = Gen.RetriveWageRequestPersonsPA(DataBinder.Eval(e.Row.DataItem, "intBenid").ToString(), lblfromdate.Text, lbltodate.Text, Request.QueryString[0].ToString(), Request.QueryString[1].ToString());

            if (dsnew1.Tables[0].Rows.Count > 0)
            {
                for (int ab = 0; ab < dsnew1.Tables[0].Rows.Count; ab++)
                {
                    if (DataBinder.Eval(e.Row.DataItem, "intBenid").ToString() == dsnew1.Tables[0].Rows[ab]["intBenid"].ToString())
                    {
                        CheckBox1.Checked = true;
                        HdfReqID.Value = dsnew1.Tables[0].Rows[ab]["intWagePaymentReqid"].ToString();

                    }
                }
            }
            DataSet dsnew = Gen.GetBenPaidAmtFromDate(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "intBenid")).ToString(), lblfromdate.Text,lbltodate.Text);

            if (dsnew.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[6].Text = dsnew.Tables[0].Rows[0]["Amt"].ToString();
                if (e.Row.Cells[6].Text == "")
                {
                    e.Row.Cells[6].Text = "0";
                }
            }
            else
            {
                e.Row.Cells[6].Text = "0";
                //Amt = "0";
            }


            e.Row.Cells[6].Text = Convert.ToString(Convert.ToDecimal(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"))) * Convert.ToDecimal(lblwagerate.Text));
                //e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text));
             //   txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text.Trim()));
                decimal TotalAmountGrd = (Convert.ToDecimal(e.Row.Cells[6].Text));
                TotalAmountGrd1 = TotalAmountGrd + TotalAmountGrd1;

                if (txtExchangerate.Text != "")
                {
                    e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"))) * (Convert.ToDecimal(lblwagerate.Text) * Convert.ToDecimal(txtExchangerate.Text))).ToString("f2"));
                }



            //   txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[10].Text) - Convert.ToDecimal(e.Row.Cells[11].Text)); // Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt")).ToString();

          

            int Totalnoofdays = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"));
            Totalnoofdays1 = Totalnoofdays + Totalnoofdays1;
            decimal TotalAmount1 = Convert.ToDecimal(e.Row.Cells[6].Text );
            TotalAmount = TotalAmount + TotalAmount1;
            decimal TotalAmount2 = Convert.ToDecimal(e.Row.Cells[7].Text);
            TotalAmountSSP = TotalAmountSSP + TotalAmount2;
          

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[3].Text = "Total";

            //e.Row.Cells[3].Text = NoofPayams1.ToString();
            //  e.Row.Cells[3].Text = NoofBomas1.ToString();
            e.Row.Cells[5].Text = Totalnoofdays1.ToString();
            e.Row.Cells[6].Text = TotalAmountGrd1.ToString();
            e.Row.Cells[7].Text = TotalAmountSSP.ToString();

        }
    }
    protected void BtnSave3_Click(object sender, EventArgs e)
    {

        
        //if (cmf.convertDateIndia(txtStartDate.Text) < cmf.convertDateIndia(lblWorkStartDate.Text))
        //{
        //    lblmsg0.Text = "Work Start Date should be greater than the paid date. ";
        //    success.Visible = false;
        //    Failure.Visible = true;

        //    return;

        //}

        //if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //{
        //    lblmsg0.Text = "Paid date should be less than the current date.";
        //    success.Visible = false;
        //    Failure.Visible = true;
        //    return;

        //}

        //Modify By Sauvick Date:03/01/2018
       
    }

    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
      

    }
    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
       
    }

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
        //sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }

    protected void txtAmount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
      
    }
    protected void txtExchangerate_TextChanged(object sender, EventArgs e)
    {

        DataSet ds1 = new DataSet();
        ds1 = Gen.getAttendanceCountFromTobyID(hdfID.Value, lblfromdate.Text, lbltodate.Text, "", "", Request.QueryString[0].ToString().Trim());

        if (ds1.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds1.Tables[0];
            grdDetails.DataBind();

        }
        else
        {


            grdDetails.DataSource = null;
            grdDetails.DataBind();
        }
        //int cnt = grdDetails.Rows.Count;
        //decimal sumcal=0;
        
        //for (int a = 0; a < grdDetails.Rows.Count; a++)
        //{
        //    GridViewRow row = (GridViewRow)grdDetails.Rows[a];
        //    if (txtExchangerate.Text != "")
        //    {
        //        row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(row.Cells[6].Text) * Convert.ToDecimal(txtExchangerate.Text)).ToString("f2"));
        //    }
        //    else
                
        //    {
        //        row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(row.Cells[6].Text) * Convert.ToDecimal("0")).ToString("f2"));
        //    }

        //    sumcal=sumcal+Convert.ToDecimal( row.Cells[7].Text);
        
        //}

        //grdDetails.Columns[7].FooterText = sumcal.ToString();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Response.Redirect("UpdateSelectionPayingagencyBenfWages.aspx?ids=" + Request.QueryString[0].ToString() + "&ids=" + Request.QueryString[1].ToString());
    }
}

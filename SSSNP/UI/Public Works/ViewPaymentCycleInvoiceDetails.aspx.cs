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

/// Purpose of Form: SNSDP Paid Cycle wise report

/// Working Task : SNSDP Paid Cycle wise report

/// Developer : Srikanth

/// Date : 03-11-2018

/// Store Procedures Modified : getPaymentDetailsbyPaidCycleWise

/// Tables Used Modified: tr_BenWagesPayment,tbl_WorkMaster,tm_Bomas,tm_Payams,tm_Counties,tm_States,td_ProjectDet

/// </summary>
#endregion
public partial class ViewPaymentCycleInvoiceDetails : System.Web.UI.Page
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
        if (Session.Count == 0)
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }

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
        dsn = Gen.GetWagePaymentDetailsbyDateDetails(Request.QueryString[0].ToString().Trim(), Session["FDate"].ToString().Trim(), Session["TDate"].ToString().Trim());
        if (dsn.Tables[0].Rows.Count > 0)
        {
            hdfFlagID.Value = "1";
            DataSet ds = new DataSet();

           
            //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());

            lblProject.Text = dsn.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = dsn.Tables[0].Rows[0]["workcode"].ToString();
            hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
            lblBoma.Text = dsn.Tables[0].Rows[0]["BomaName"].ToString();

            lblState.Text = dsn.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = dsn.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = dsn.Tables[0].Rows[0]["PayamName"].ToString();



            lblfromdate.Text = Session["FDate"].ToString().Trim();
            lbltodate.Text = Session["TDate"].ToString().Trim();

               Label4.Text = dsn.Tables[0].Rows[0]["PaidCount"].ToString();

               Label11.Text = dsn.Tables[0].Rows[0]["PaidAlternateHouseholds"].ToString();
               Label13.Text = dsn.Tables[0].Rows[0]["PaidActualHouseholds"].ToString();
             
               Label29.Text = dsn.Tables[0].Rows[0]["PaidAmount"].ToString();
               

            
               lblAmountpaidSSP.Text = dsn.Tables[0].Rows[0]["PaidAmountSSP"].ToString();
                //LblMaRevon
             //   lblexchangerate.Text = ds.Tables[0].Rows[0]["WageRateUSD"].ToString();
                //if (ds.Tables[3].Rows.Count > 0)
                //{

                //    txtExchangerate.Text = ds.Tables[3].Rows[0]["ExchangeRate"].ToString();
                //    txtExchangerate.Enabled = false;

                //}



            DataSet ds1 = new DataSet();
            ds1 = Gen.getPaymentDetailsbyPaidCycleWise(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim(), Session["FDate"].ToString().Trim(), Session["TDate"].ToString().Trim());

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
        
      

    }

   
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HyperLink hbm = (HyperLink)e.Row.Cells[4].Controls[0];
            hbm.NavigateUrl = "frmBenInvoice.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();// +"&status=13";
            hbm.Target = "_blank";

            HyperLink hbm1 = (HyperLink)e.Row.Cells[13].Controls[0];


            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PayStatus")).Trim().ToLower() == "paid" && Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PaidToType")).Trim().ToLower() == "")
            {
                hbm1.NavigateUrl = "InvoiceNotAvaialbleDet.aspx";// +"&status=13";
                hbm1.Target = "_blank";
            }
            else
            {


                hbm1.NavigateUrl = "frmBenInvoice.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();// +"&status=13";
                hbm1.Target = "_blank";
            }

            //HyperLink hbm1 = (HyperLink)e.Row.Cells[13].Controls[0];
            //hbm1.NavigateUrl = "frmBenInvoice.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();// +"&status=13";
            //hbm1.Target = "_blank";
        }
        //    HiddenField HdfReqID = (HiddenField)e.Row.Cells[1].FindControl("HdfReqID");
        //    HiddenField hdfRegBenid = (HiddenField)e.Row.Cells[1].FindControl("hdfRegBenid");
        //    HiddenField HdfBenid = (HiddenField)e.Row.Cells[1].FindControl("HdfBenid");
        //    HiddenField HdfBenWagesInvoiceid = (HiddenField)e.Row.Cells[1].FindControl("HdfBenWagesInvoiceid");
        //    HdfBenid.Value = DataBinder.Eval(e.Row.DataItem, "intBenid").ToString();

        //    hdfRegBenid.Value = DataBinder.Eval(e.Row.DataItem, "intWagesReqBenTransid").ToString();
        //    HdfReqID.Value = DataBinder.Eval(e.Row.DataItem, "intWagePaymentReqid").ToString();
        //    HdfBenWagesInvoiceid.Value = "0";

            
           
        //    DataSet dsnew = Gen.GetBenPaidAmtFromDate(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "intBenid")).ToString(), lblfromdate.Text,lbltodate.Text);

        //    if (dsnew.Tables[0].Rows.Count > 0)
        //    {
        //        e.Row.Cells[6].Text = dsnew.Tables[0].Rows[0]["Amt"].ToString();
        //        if (e.Row.Cells[6].Text == "")
        //        {
        //            e.Row.Cells[6].Text = "0";
        //        }
        //    }
        //    else
        //    {
        //        e.Row.Cells[6].Text = "0";
        //        //Amt = "0";
        //    }


        //    e.Row.Cells[6].Text = Convert.ToString(Convert.ToDecimal(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"))) * Convert.ToDecimal(lblwagerate.Text));
        //        //e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text));
        //     //   txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text.Trim()));
        //        decimal TotalAmountGrd = (Convert.ToDecimal(e.Row.Cells[6].Text));
        //        TotalAmountGrd1 = TotalAmountGrd + TotalAmountGrd1;

        //        if (txtExchangerate.Text != "")
        //        {
        //            e.Row.Cells[7].Text = Convert.ToString(Convert.ToDecimal(Convert.ToDecimal(Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"))) * (Convert.ToDecimal(lblwagerate.Text) * Convert.ToDecimal(txtExchangerate.Text))).ToString("f2"));
        //        }



        //    //   txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[10].Text) - Convert.ToDecimal(e.Row.Cells[11].Text)); // Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt")).ToString();

          

        //    int Totalnoofdays = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"));
        //    Totalnoofdays1 = Totalnoofdays + Totalnoofdays1;
        //    decimal TotalAmount1 = Convert.ToDecimal(e.Row.Cells[6].Text );
        //    TotalAmount = TotalAmount + TotalAmount1;
        //    decimal TotalAmount2 = Convert.ToDecimal(e.Row.Cells[7].Text);
        //    TotalAmountSSP = TotalAmountSSP + TotalAmount2;
          

        //}
        //if (e.Row.RowType == DataControlRowType.Footer)
        //{
        //    e.Row.Cells[3].Text = "Total";

        //    //e.Row.Cells[3].Text = NoofPayams1.ToString();
        //    //  e.Row.Cells[3].Text = NoofBomas1.ToString();
        //    e.Row.Cells[5].Text = Totalnoofdays1.ToString();
        //    e.Row.Cells[6].Text = TotalAmountGrd1.ToString();
        //    e.Row.Cells[7].Text = TotalAmountSSP.ToString();

        //}
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
       
    }
  
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        BtnSave2.Visible = false;
    }
    protected void grdDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        HyperLink HyperLink1 = (HyperLink)e.Row.Cells[1].FindControl("HyperLink1");
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string sen, sen1, sen2;

            sen2 = (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFilePath")) + "/" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")));
            sen1 = sen2.Replace(@"\", @"/");
            sen = sen1.Replace(@"", "~/"); 

            string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            sen = sen.Replace(sssnpdir, "~");

            HyperLink1.NavigateUrl = sen.ToString();
            HyperLink1.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName"));
            HyperLink1.Target = "_blank";
        }
    }
}

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
public partial class PDCHouseholdWages : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int Totalnoofdays1 = 0;
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (!IsPostBack)
        {
            DataSet dsnew = new DataSet();
            dsnew = Gen.getExchangeRate();
            if (dsnew.Tables[0].Rows.Count > 0)
            {
                txtExchangerate.Text = dsnew.Tables[0].Rows[0][0].ToString().Trim();
            }

            BtnSave1.Visible = false;
            
        }


        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {


           
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            tr_group_search.Visible = true;
            BtnSave1.Text = "Update";
        }

    }

    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

        //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());
        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");


            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();


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


        for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
        {
            GridViewRow row = (GridViewRow)grdDetails.Rows[iii];
            TextBox txtAmt = (TextBox)grdDetails.Rows[iii].Cells[0].FindControl("txtAmt");
            CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("CheckBox1");
            if (c.Checked == true && c.Enabled == true)
            {
                if (txtAmt.Text != "")
                {



                    int s = Gen.InsUpdBeneficiaryWagesFromToDateLatest(grdDetails.DataKeys[iii].Value.ToString(), TxtFromDate.Text, TxtToDate.Text, txtStartDate.Text, txtAmt.Text, Session["uid"].ToString(), txtAmount.Text.Trim(), hdfID.Value.ToString(),txtExchangerate.Text);

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


        lblmsg.Text = "Submited Successfully..!";
        success.Visible = true;
        Failure.Visible = false;
        clear();

    }

    void clear()
    {
        BtnSave1.Text = "Submit";
        txtStartDate.Text = "";
        grdDetails.DataSource = null;
        grdDetails.DataBind();
        TxtFromDate.Text = "";
        //    ddlyear.SelectedIndex = 0;
        TxtToDate.Text = "";
        lblProject.Text = "";
        lblWorkCode.Text = "";
        lblBoma.Text = "";
        lblWorkStartDate.Text = "";
        lblState.Text = "";
        lblCounty.Text = "";
        lblPayam.Text = "";
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            string Amt = "";
            CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("CheckBox1");
            TextBox txtAmt = (TextBox)e.Row.Cells[0].FindControl("txtAmt");


            DataSet dsnew = Gen.GetBenPaidAmtFromDate(Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "intBenid")).ToString(), TxtFromDate.Text, TxtToDate.Text);

            if (dsnew.Tables[0].Rows.Count > 0)
            {
                e.Row.Cells[11].Text = dsnew.Tables[0].Rows[0]["Amt"].ToString();
                if (e.Row.Cells[11].Text == "")
                {
                    e.Row.Cells[11].Text = "0";
                }
            }
            else
            {
                e.Row.Cells[11].Text = "0";
                //Amt = "0";
            }
            if (e.Row.Cells[9].Text != "")
            {

                e.Row.Cells[10].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text.Trim()));
                //e.Row.Cells[11].Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text));
                txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[9].Text) * Convert.ToDecimal(txtAmount.Text.Trim()));
                decimal TotalAmountGrd = (Convert.ToDecimal(txtAmt.Text.ToString()));
                TotalAmountGrd1 = TotalAmountGrd + TotalAmountGrd1;
            }
            else
            {

                e.Row.Cells[10].Text = "0.00";
                //e.Row.Cells[11].Text = "0.00";
                txtAmt.Text = "0.00";
            }




            //   txtAmt.Text = Convert.ToString(Convert.ToDecimal(e.Row.Cells[10].Text) - Convert.ToDecimal(e.Row.Cells[11].Text)); // Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt")).ToString();

            if (txtAmt.Text == "0.00")
            {
                txtAmt.Enabled = false;

            }
            else
            {
                txtAmt.Enabled = false;
            }

            int Totalnoofdays = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "cnt"));
            Totalnoofdays1 = Totalnoofdays + Totalnoofdays1;
            decimal TotalAmount1 = Convert.ToDecimal(txtAmount.Text.Trim());
            TotalAmount = TotalAmount + TotalAmount1;
          

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[6].Text = "Total";

            //e.Row.Cells[3].Text = NoofPayams1.ToString();
            //  e.Row.Cells[3].Text = NoofBomas1.ToString();
            e.Row.Cells[9].Text = Totalnoofdays1.ToString();
            e.Row.Cells[12].Text = TotalAmountGrd1.ToString();

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
        DataSet ds = new DataSet();
        ds = Gen.getAttendanceCountFromTo(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text,ddl_group_type.SelectedValue.ToString(),ddl_group_code.SelectedValue.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            BtnSave1.Visible = true;
            tr_group_search.Visible = true;
        }
        else
        {
            BtnSave1.Visible = false;
            
            grdDetails.DataSource = null;
            grdDetails.DataBind();
        }
    }

    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddl_group_type.SelectedValue == "Group")
        {
            //tr_group_code.Style.Add("display", "block");
            tr_group_code.Visible = true;
        }

        //if (ddl_group_type.SelectedValue == "Group")
        //{
        //    //tr_group_code.Style.Add("display", "block");
        //    tr_group_code.Visible = true;
        //    DataSet ds = new DataSet();
        //    ds = Gen.GetAttendanceCountBYGroup_Sp(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text,ddl_group_type.SelectedValue.ToString(),ddl_group_code.SelectedValue.ToString());

        //    if (ds.Tables[0].Rows.Count > 0)
        //    {
        //        grdDetails.DataSource = ds.Tables[0];
        //        grdDetails.DataBind();
        //        BtnSave1.Visible = true;
        //        grdDetails.Visible = true;
        //    }
        //    else
        //    {
        //        BtnSave1.Visible = false;
        //        grdDetails.DataSource = null;
        //        grdDetails.DataBind();
        //        grdDetails.Visible = false;
        //    }
        //}
        ////else
        ////{
        ////    DataSet ds = new DataSet();
        ////    ds = Gen.getAttendanceCountFromTo(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text);

        ////    if (ds.Tables[0].Rows.Count > 0)
        ////    {
        ////        grdDetails.DataSource = ds.Tables[0];
        ////        grdDetails.DataBind();
        ////        BtnSave1.Visible = true;
        ////    }
        ////    else
        ////    {
        ////        BtnSave1.Visible = false;
        ////        grdDetails.DataSource = null;
        ////        grdDetails.DataBind();
        ////    }
        ////}

        if (ddl_group_type.SelectedValue == "CST")
        {
            //tr_group_code.Style.Add("display", "none");
            tr_group_code.Visible = false;
            ddl_group_code.SelectedValue = "";
        }


            //if (ddl_group_type.SelectedValue == "CST")
            //{
            //    //tr_group_code.Style.Add("display", "none");
            //    tr_group_code.Visible = false;
            //    ddl_group_code.SelectedValue = "";
            //    DataSet ds = new DataSet();
            //    ds = Gen.GetAttendanceCountBYGroup_Sp(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.SelectedValue.ToString(), ddl_group_code.SelectedValue.ToString());
            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grdDetails.DataSource = ds.Tables[0];
            //        grdDetails.DataBind();
            //        BtnSave1.Visible = true;
            //        grdDetails.Visible = true;
            //    }
            //    else
            //    {
            //        BtnSave1.Visible = false;
            //        grdDetails.Visible = false;
            //        grdDetails.DataSource = null;
            //        grdDetails.DataBind();
            //    }
            //}
            //else
            //{
            //    DataSet ds = new DataSet();
            //    ds = Gen.getAttendanceCountFromTo(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text);

            //    if (ds.Tables[0].Rows.Count > 0)
            //    {
            //        grdDetails.DataSource = ds.Tables[0];
            //        grdDetails.DataBind();
            //        BtnSave1.Visible = true;
            //    }
            //    else
            //    {
            //        BtnSave1.Visible = false;
            //        grdDetails.DataSource = null;
            //        grdDetails.DataBind();
            //    }
            //}


        }
    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataSet ds = new DataSet();
        //ds = Gen.GetAttendanceCountBYGroup_Sp(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.SelectedValue.ToString(), ddl_group_code.SelectedValue.ToString());
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    grdDetails.DataSource = ds.Tables[0];
        //    grdDetails.DataBind();
        //    BtnSave1.Visible = true;
        //    grdDetails.Visible = true;
        //}
        //else
        //{
        //    BtnSave1.Visible = false;
        //    grdDetails.Visible = false;
        //    grdDetails.DataSource = null;
        //    grdDetails.DataBind();
        //}
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdWages.aspx");
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
        if (grdDetails.Rows.Count > 0)
        {
            DataSet ds = new DataSet();
            ds = Gen.getAttendanceCountFromTo(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text,"","");

            if (ds.Tables[0].Rows.Count > 0)
            {
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                BtnSave1.Visible = true;
            }
            else
            {
                BtnSave1.Visible = false;
                grdDetails.DataSource = null;
                grdDetails.DataBind();
            }
        }
    }
    protected void txtExchangerate_TextChanged(object sender, EventArgs e)
    {
        DataSet ds = new DataSet();
        ds = Gen.getAttendanceCountFromTo(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.SelectedValue.ToString(), ddl_group_code.SelectedValue.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            BtnSave1.Visible = true;
            tr_group_search.Visible = true;
        }
        else
        {
            BtnSave1.Visible = false;

            grdDetails.DataSource = null;
            grdDetails.DataBind();
        }
    }
}

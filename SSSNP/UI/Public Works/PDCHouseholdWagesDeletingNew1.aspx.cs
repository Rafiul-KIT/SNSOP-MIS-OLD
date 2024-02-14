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

public partial class PDCHouseholdWagesDeletingNew1 : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages
    decimal paidamount;

    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        this.BtnRemove.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnRemove, "group"));
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (!IsPostBack)
        {

           // FillDetails();

            BtnSave3.Visible = false;
            BtnSave4.Visible = false;
            
        }


        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            

            
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            
            
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

            BtnSave3.Visible = true;
            BtnSave4.Visible = true;
            //DataSet ds1 = new DataSet();
            //ds1= Gen.getWagesReport(hdfID.Value.ToString());

            //if (ds1.Tables[0].Rows.Count > 0)
            //{
            //    grdDetails.DataSource = ds1.Tables[0];
            //    grdDetails.DataBind();

            //}
            //else
            //{

            //    grdDetails.DataSource = null;
            //    grdDetails.DataBind();
            //}
        }
    }    



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdWagesDeletingNew.aspx");



     
       
    }

    void clear()
    {
        
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            HiddenField hdfFlagID0 = (HiddenField)e.Row.Cells[0].FindControl("hdfFlagID0");

            hdfFlagID0.Value = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "PaidDate")).ToString("dd-MM-yyyy");


            decimal paidamount1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Amt"));
            paidamount = paidamount1 + paidamount;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[8].Text = "Total";

            e.Row.Cells[10].Text = paidamount.ToString();
        }
    }
    protected void BtnSave3_Click(object sender, EventArgs e)
    {
        lblStat.Text = "";
        lblmsg0.Text = "";
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
        DataSet ds1 = new DataSet();
        ds1 = Gen.getWagesdetbypaymentdet(hdfID.Value.ToString(), txtStartDate.Text);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds1.Tables[0];
            grdDetails.DataBind();
            BtnRemove.Visible = true;

        }
        else
        {

            grdDetails.DataSource = null;
            grdDetails.DataBind();
            lblStat.Text = " There is no wages for this date of this workcode ";
        }
        

       
    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdWagesDeletingNew.aspx");
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

    protected void BtnRemove_Click(object sender, EventArgs e)
    {



        int count = 0;
        for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
        {
            CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("chkSelect");


            if (c.Checked == true)
            {
                //DataSet ds = new DataSet();
                //ds = Gen.checkWagesonthisdateofben(grdDetails.DataKeys[iii].Value.ToString(), txtStartDate.Text);

                //if (ds.Tables[0].Rows.Count > 0)
                //{

                //    Failure.Visible = true;
                //    success.Visible = false;
                //    lblmsg0.Text = "Please Delete wages  of this date";
                //    return;

                //}


                count++;
            }
            else
            {
            }
        }
        
        if (count == 0)
        {
            lblmsg0.Text = "At leaste one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;

        }

        for (int iii = 0; iii < grdDetails.Rows.Count; iii++)
        {
            GridViewRow row = (GridViewRow)grdDetails.Rows[iii];
            //  DropDownList ddlStatus = (DropDownList)grdDetails.Rows[iii].Cells[0].FindControl("ddlStatus");
            CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("chkSelect");

            if (c.Checked == true && c.Enabled == true)
            {
                //if (ddlStatus.SelectedValue != "--Select--")
                //{

                int s = 0;

                if (BtnSave3.Text == "Update")
                {
                    // s = Gen.UpdateBenefeciaryAttendance(hdfID.Value.ToString(), grdDetails.DataKeys[iii].Value.ToString(), ddlStatus.SelectedValue, txtStartDate.Text, Session["uid"].ToString());
                }
                else
                    //    s = Gen.BenefeciaryAttendance(hdfID.Value.ToString(), grdDetails.DataKeys[iii].Value.ToString(), ddlStatus.SelectedValue,txtStartDate.Text, Session["uid"].ToString());

                    s = Gen.benwagesdeletebyid(grdDetails.DataKeys[iii].Value.ToString(), txtStartDate.Text,Session["uid"].ToString());

                if (s != 999)
                {
                    lblmsg0.Text = "Already Exist. ";
                    success.Visible = false;
                    Failure.Visible = true;
                }
                //else
                //{
                //    lblmsg.Text = "Submitted Successfully..!";
                //    success.Visible = true;
                //    Failure.Visible = false;
                //}
                // }
            }
        }

        DataSet ds1 = new DataSet();
        ds1 = Gen.getWagesdetbypaymentdet(hdfID.Value.ToString(), txtStartDate.Text);

        if (ds1.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds1.Tables[0];
            grdDetails.DataBind();
            BtnRemove.Visible = true;

        }
        else
        {

            grdDetails.DataSource = null;
            grdDetails.DataBind();
            lblStat.Text = " There is no wages for this date of this workcode ";
        }


        lblmsg0.Text = "Deleted successfully ";
        success.Visible = true;
        Failure.Visible = false;
        
        

    }
    public void DeleteFile(string strFileName)
    {//Delete file from the server
        if (strFileName.Trim().Length > 0)
        {
            FileInfo fi = new FileInfo(strFileName);
            if (fi.Exists)//if file exists delete it
            {
                fi.Delete();
            }
        }
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        FillDetails();
    }
}

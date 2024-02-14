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

public partial class PDCHouseholdWagesDeletingNew : System.Web.UI.Page
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
        ds1 = Gen.getWagesReportA(hdfID.Value.ToString(), TxtEnrolmentNo.Text);

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
            lblStat.Text = " There is no wages for this Enrollment  ";
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
        // Select the checkboxes from the GridView control
        int cnt = 0;
        for (int i = 0; i < grdDetails.Rows.Count; i++)
        {
            GridViewRow row = grdDetails.Rows[i];
            lblStat.Text = "";
            HiddenField hdfFlagID0 = (HiddenField)row.FindControl("hdfFlagID0");
            bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
            if (isChecked)
            {

                //DataSet ds = new DataSet();
                //ds = Gen.getWagesReport(hdfID.Value.ToString(), TxtEnrolmentNo.Text);
                //grdDetails.DataSource = ds.Tables[0];
                //grdDetails.DataBind();
                //ds = Gen.getWagesReport(hdfID.Value.ToString());
                
                //ds = Gen.showbenifwages(txtBeneficiary.Text, txtworkcode.Text, txtEnrollment.Text, ddlAreaofWork.SelectedValue, ddlBoma.SelectedValue, txtfromDate0.Text, txtTODate.Text);
                //DataSet ds1 = new DataSet();
                //ds1 = Gen.FeatchFiledatanew2(grdDetails.DataKeys[i].Value.ToString());
                //if (ds1.Tables[0].Rows.Count > 0)
                //{
                //    lblStat.Text = "This User having Wages you can't delete it";
                //    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mesg1 + "');", true);
                //    //TxtMandalCode.Focus();
                //    //ddlVillage.Focus();
                //    return;
                //}
                //else
                //{
                   
                    string ids = grdDetails.DataKeys[i].Value.ToString();
                    //Gen.RemoveWages(ids, ds.Tables[0].Rows[0]["PaidDate"].ToString());
                    Gen.RemoveWages(ids, hdfFlagID0.Value.ToString());
                    cnt = cnt + 1;
                   // lblStat.Text = " successfully Removed. ";
                //}
                //string ids = grdDetails.Rows[i].Cells[1].Text.ToString();

            }


            //lblStat.Text = " successfully Removed. ";
            //grdDetails.DataSource = null;
            //grdDetails.DataBind();

        }
        if (cnt == 0)
            lblStat.Text = " Select Atleast one check to delete ";
            //grdDetails.DataSource = ds1.Tables[0];
            //grdDetails.DataBind();
        else
            lblStat.Text = " successfully Removed. ";
        grdDetails.DataSource = null;
        grdDetails.DataBind();
            FillDetails();
           
        DataSet ds1 = new DataSet();
            ds1 = Gen.getWagesReportA(hdfID.Value.ToString(), TxtEnrolmentNo.Text);

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
                lblStat.Text = " There is no wages for this Enrollment  ";
            }

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

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

public partial class PDCHouseholdAttendanceDeletingnew : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int cnt = -1;
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (!IsPostBack)
        {

            
           
        }


        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            

          
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
            

        }
    }    



    protected void BtnSave_Click(object sender, EventArgs e)
    {
        clear();


       
       
    }

    public void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = Gen.getAttendanceReportA(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text,TxtEnrolmentNo.Text);

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdClosebatchdet.DataSource = ds.Tables[0];
            grdClosebatchdet.DataBind();

        }
        else
        {

            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
        }
        
    }

    void clear()
    {
        

        grdClosebatchdet.DataSource = null;
        grdClosebatchdet.DataBind();
        TxtFromDate.Text = "";
    //    ddlyear.SelectedIndex = 0;
        TxtToDate.Text = "";
        lblProject.Text = "";
        lblWorkCode.Text = "";
        lblBoma.Text = "";
        lblWorkStartDate.Text = "";
        lblState.Text="";
        lblCounty.Text = "";
        lblPayam.Text = "";
        lblStat.Text = "";
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            HiddenField hdfFlagID0 = (HiddenField)e.Row.Cells[0].FindControl("hdfFlagID0");

            hdfFlagID0.Value = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Attendance_Date")).ToString("dd-MM-yyyy");

            //HiddenField hdfdate = (HiddenField)e.Row.Cells[0].FindControl("hdfdate");
            //hdfdate.Value=Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem,""))

            cnt = cnt + 1;
            e.Row.Cells[0].Text = cnt.ToString().Trim();

            DataSet ds = (DataSet)ViewState["dt"];

            int iPresentCount = 0, iAbsentCount = 0;

            for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            {

                if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
                {
                    iPresentCount = iPresentCount + 1;
                }
                if (e.Row.Cells[i].Text.Trim().ToLower() == "absent")
                {
                    iAbsentCount = iAbsentCount + 1;
                }

            }

            e.Row.Cells[ds.Tables[0].Columns.Count - 1].Text = iPresentCount.ToString().Trim();
            e.Row.Cells[ds.Tables[0].Columns.Count - 2].Text = iAbsentCount.ToString().Trim();
            
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
        

        DataSet ds = new DataSet();
        ds = Gen.getAttendanceReportA(hdfID.Value.ToString(),TxtFromDate.Text,TxtToDate.Text,TxtEnrolmentNo.Text);

        if (ds.Tables[0].Rows.Count > 0)
        {
            grdClosebatchdet.DataSource = ds.Tables[0];
            grdClosebatchdet.DataBind();
            BtnRemove.Visible = true;
            lblStat.Text = "";
           
        }
        else
        {
            
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            lblStat.Text = "";
        }
    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("PDCHouseholdAttendanceDeletingnew.aspx");
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

    protected void grdClosebatchdet_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            HiddenField hdfFlagID0 = (HiddenField)e.Row.Cells[0].FindControl("hdfFlagID0");

            hdfFlagID0.Value = Convert.ToDateTime(DataBinder.Eval(e.Row.DataItem, "Attendance_Date")).ToString("yyyy-MM-dd");

        }
        
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{
        //    cnt = cnt + 1;
        //    e.Row.Cells[0].Text = cnt.ToString().Trim();

            //DataSet ds = (DataSet)ViewState["dt"];

            //int iPresentCount = 0, iAbsentCount = 0;

            //for (int i = 0; i < ds.Tables[0].Columns.Count; i++)
            //{
                

            //    if (e.Row.Cells[i].Text.Trim().ToLower() == "present")
            //    {
            //        iPresentCount = iPresentCount + 1;
            //    }
            //    if (e.Row.Cells[i].Text.Trim().ToLower() == "absent")
            //    {
            //        iAbsentCount = iAbsentCount + 1;
            //    }

            //}

            //e.Row.Cells[ds.Tables[0].Columns.Count - 1].Text = iPresentCount.ToString().Trim();
            //e.Row.Cells[ds.Tables[0].Columns.Count - 2].Text = iAbsentCount.ToString().Trim();

            ////localhost.Himayatmis objUserDetails = new localhost.Himayatmis();
            //DataSet dsresult = new DataSet();
            ////dsresult = objUserDetails.getEmployeeDetbyIDDate(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intTraineeID")).Trim(), new comFunctions().convertDateIndia(txtAttendancedate.Text));

            //dsresult = cmn.atdt(grdClosebatchdet.DataKeys[e.Row.RowIndex].Values[0].ToString(),grdClosebatchdet.DataKeys[e.Row.RowIndex].Values[1].ToString(), txtAttendancedate.Text);
            //if (dsresult.Tables[0].Rows.Count > 0)
            //{
            //    e.Row.Cells[6].Text = dsresult.Tables[0].Rows[0]["Attendence"].ToString().Trim();



            //}
            //else
            //{ e.Row.Cells[6].Text = "Absent"; }

            //grdClosebatchdet.Rows[0].Visible = false;
      //  }
    }
    protected void grdClosebatchdet_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdClosebatchdet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdClosebatchdet.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void BtnRemove_Click(object sender, EventArgs e)
    {
        // Select the checkboxes from the GridView control
        lblStat.Text = "";
        int cnt = 0;
        for (int i = 0; i < grdClosebatchdet.Rows.Count; i++)
        {
            GridViewRow row = grdClosebatchdet.Rows[i];
            HiddenField hdfFlagID0=(HiddenField)row.FindControl("hdfFlagID0");
            bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
            if (isChecked)
            {


                DataSet ds = new DataSet();
                ds = Gen.getAttendanceReportA(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text,TxtEnrolmentNo.Text);
                //ds = Gen.showdetails1(txtBeneficiary.Text, txtworkcode.Text, txtEnrollment.Text, ddlAreaofWork.SelectedValue, ddlBoma.SelectedValue, txtfromDate0.Text, txtTODate.Text);
                DataSet ds1 = new DataSet();
                //ds1 = Gen.FetchUserdet112Lat(grdClosebatchdet.DataKeys[i].Value.ToString());
                
                ds1 = Gen.FetchUserdet112Lat(grdClosebatchdet.DataKeys[i].Value.ToString(),hdfFlagID0.Value);
                if (ds1.Tables[0].Rows.Count > 0)
                {
                    //ds1 = Gen.FetchUserdet112(grdClosebatchdet.DataKeys[i].Value.ToString());
                    lblStat.Text = "This User having wages you can't delete it";
                    //ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('" + mesg1 + "');", true);
                    //TxtMandalCode.Focus();
                    //ddlVillage.Focus();
                    return;
                }
                else
                {
                    //string ids = grdDetails.Rows[i].Cells[1].Text.ToString();
                    string ids = grdClosebatchdet.DataKeys[i].Value.ToString();
                   
                    //Gen.RemoveGallery(ids, ds.Tables[0].Rows[0]["Attendance_Date"].ToString());

                    Gen.RemoveGallery(ids, hdfFlagID0.Value.ToString());
                    cnt = cnt + 1;
                    lblStat.Text = " successfully Removed. ";
                }
            }

            // lblStat.Text = " successfully Removed. ";

        }
        if (cnt == 0)
            lblStat.Text = "Select Atleast one check to delete ";
        else
            fillgrid();
    }
}

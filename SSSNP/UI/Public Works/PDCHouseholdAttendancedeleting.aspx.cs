#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Telerik.Web.UI;
#endregion

#region Amendment History Begin
//Form Name : Household Attendance Deleting
//Purpose : To Add Attendance Deleting
//Tables used :tr_BenAttendanceDet,tbl_BeneficiaryDet,tm_Bomas,tr_BenAttendanceDetTrans
//Stored procedures Used : getbenattendancedeletebyid,GetworkproposalforProgress
#endregion Amendment History End

public partial class PDCHouseholdAttendancedeleting : System.Web.UI.Page
{

    #region reference class calling and Variable declaration
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    bool admin = false; bool update = false;
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count == 0)
        {
            Response.Redirect("~/frmuserlogin.aspx");
        }
        if (Session["uid"].ToString() == "1066")
            admin = true;

        // Look up Click Function Intialization
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));

        // Is not Postback Statement
        if (!IsPostBack)
        {
            BtnSave3.Visible = false;
        }

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            RadDetail.Visible = false;
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }
        #endregion
    }
    #endregion


    #region Button Disable Functions
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
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";

        DataSet ds = new DataSet();

        if (string.IsNullOrEmpty(hdfID.Value.ToString()))
            return;

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
    #endregion

    #region Button Click functionalities
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (cmf.convertDateIndia(txtStartDate.Text) > cmf.convertDateIndia(System.DateTime.Now.ToString("dd-MM-yyyy")))
        //if (System.Convert.ToDateTime(txtStartDate.Text) > System.Convert.ToDateTime(System.DateTime.Now.ToString("dd-MM-yyyy")))
        {
            lblmsg0.Text = "Attendance date should be less than the current date.";
            success.Visible = false;
            Failure.Visible = true;
            return;
        }
        int count = 0;

        GridHeaderItem hItem = (GridHeaderItem)RadDetail.MasterTableView.GetItems(GridItemType.Header)[0];
        CheckBox chkAll = (CheckBox)hItem.FindControl("checkAll");

        if (!chkAll.Checked)
        {
            foreach (GridDataItem item in RadDetail.Items)
            {
                CheckBox cbControl = (CheckBox)item.Cells[0].FindControl("chkSelected");

                if (cbControl.Checked)
                {
                    int benId = 0;
                    int.TryParse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["intBenid"].ToString(), out benId);

                    DataSet ds = new DataSet();
                    ds = Gen.checkWagesonthisdateofben(benId.ToString(), txtStartDate.Text);

                    if (ds.Tables[0].Rows.Count > 0)
                    {
                        Failure.Visible = true;
                        success.Visible = false;
                        lblmsg0.Text = "Attendance cannot be deleted: wage already paid";
                        //lblmsg0.Text = "Please Delete wages of this date";
                        return;
                    }

                    if (ds.Tables[1].Rows.Count > 0)
                    {

                        Failure.Visible = true;
                        success.Visible = false;
                        lblmsg0.Text = "Attendance cannot be deleted: wage payment already requested";
                        //lblmsg0.Text = "Please Delete wages of this date";
                        return;
                    }
                    count += 1;
                }
            }
        }
        else
        {
            RadDetail.AllowPaging = false;
            RadDetail.Rebind();
            foreach (GridDataItem item in RadDetail.Items)
            {

                int benId = 0;
                int.TryParse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["intBenid"].ToString(), out benId);

                DataSet ds = new DataSet();
                ds = Gen.checkWagesonthisdateofben(benId.ToString(), txtStartDate.Text);

                if (ds.Tables[0].Rows.Count > 0)
                {

                    Failure.Visible = true;
                    success.Visible = false;
                    lblmsg0.Text = "Attendance cannot be deleted: wage already paid";
                    //lblmsg0.Text = "Please Delete wages of this date";
                    return;

                }

                if (ds.Tables[1].Rows.Count > 0)
                {

                    Failure.Visible = true;
                    success.Visible = false;
                    lblmsg0.Text = "Attendance cannot be deleted: wage payment already requested";
                    //lblmsg0.Text = "Please Delete wages of this date";
                    return;

                }
                count += 1;
            }
        }
        if (count == 0)
        {
            lblmsg0.Text = "At leaste one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        if (!chkAll.Checked)
        {
            foreach (GridDataItem item in RadDetail.Items)
            {
                int benId = 0;
                int.TryParse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["intBenid"].ToString(), out benId);
                CheckBox cbControl = (CheckBox)item.Cells[0].FindControl("chkSelected");

                if (cbControl.Checked)
                {
                    int s = 0;

                    s = Gen.BenattendanceDeletebyid(hdfID.Value.ToString(), benId.ToString(), txtStartDate.Text, Session["uid"].ToString());

                    //if (s != 999)
                    //{
                    //    lblmsg0.Text = "Already Exist. ";
                    //    success.Visible = false;
                    //    Failure.Visible = true;
                    //}
                }
            }
        }
        else
        {
            RadDetail.AllowPaging = false;
            RadDetail.Rebind();

            foreach (GridDataItem item in RadDetail.Items)
            {
                int benId = 0;
                int.TryParse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["intBenid"].ToString(), out benId);
                int s = 0;

                s = Gen.BenattendanceDeletebyid(hdfID.Value.ToString(), benId.ToString(), txtStartDate.Text, Session["uid"].ToString());

                if (s != 999)
                {
                    lblmsg0.Text = "Already Exist. ";
                    success.Visible = false;
                    Failure.Visible = true;
                }
            }
            RadDetail.AllowPaging = true;
        }

        if (txtStartDate.Text != "")
        {
            RadDetail.AllowPaging = true;
            lblmsg0.Text = "";
            FillDetails();
            DataSet dscheck = new DataSet();
            dscheck = Gen.VerifyAttendancelistlatestidbyDelete(hdfID.Value, txtStartDate.Text);

            if (dscheck.Tables[0].Rows.Count > 0)
            {

                BtnSave3.Visible = true;
                RadDetail.DataSource = dscheck.Tables[0];
                RadDetail.Rebind();
            }
            else
            {
                RadDetail.DataSource = null;
                RadDetail.Rebind();
                // grdDetails.DataSource = null;
                // grdDetails.DataBind();
                Failure.Visible = true;
                success.Visible = false;
                lblmsg0.Text = "Attendance deleted successfully.";
                return;
            }
        }
        else
        {
            Failure.Visible = true;
            success.Visible = false;
            lblmsg0.Text = "Please Enter Attendance date";
            //  return;

        }
        lblmsg.Text = "Deleted Successfully..!";
        success.Visible = true;
        Failure.Visible = false;
        clear();
    }

    void clear()
    {
        //  BtnSave3.Text = "Submit";
        //  txtStartDate.Text = "";
        //grdDetails.DataSource = null;
        //grdDetails.DataBind();
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    #endregion

    //#region Gridview Events
    //protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {

    //    }
    //}

    //protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //grdDetails.PageIndex = e.NewPageIndex;
    //    //FillDetails();
    //}
    //#endregion

    #region TextBox Changes Functions


    protected void SearchAttendances()
    {
        //DateTime dt;
        //DateTime.TryParse(txtStartDate.Text, out dt);
        //if (dt == DateTime.MinValue) return;

        if (txtStartDate.Text != "")
        {
            lblmsg0.Text = "";
            FillDetails();
            DataSet dscheck = new DataSet();
            dscheck = Gen.VerifyAttendancelistlatestidbyDelete(hdfID.Value, txtStartDate.Text);

            if (dscheck.Tables[0].Rows.Count > 0)
            {

                BtnSave3.Visible = true;
                //grdDetails.DataSource = dscheck.Tables[0];
                //grdDetails.DataBind();
                RadDetail.Visible = true;
                RadDetail.DataSource = dscheck.Tables[0];
                RadDetail.Rebind();

                Failure.Visible = false;
                success.Visible = false;
                //Failure.Visible = true;
                //success.Visible = false;
                //lblmsg0.Text = "Already you have updated Attendace for this Work Code";
                //return;
            }
            else
            {
                RadDetail.DataSource = null;
                RadDetail.Visible = false;
                // grdDetails.DataSource = null;
                //grdDetails.DataBind();
                Failure.Visible = true;
                success.Visible = false;
                lblmsg0.Text = "Not having attendance on this date";
                return;
            }
        }
        else
        {
            RadDetail.Visible = false;
            if (string.IsNullOrEmpty(hdfID.Value.ToString())) return;
            Failure.Visible = true;
            success.Visible = false;
            lblmsg0.Text = "Please Enter Attendance date";
            return;
        }
    }

    protected void txtStartDate_TextChanged(object sender, EventArgs e)
    {
        //SearchAttendances();        
    }
    #endregion

    protected void RadDetail_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        //txtStartDate_TextChanged(sender, e);
        RadDetail.Visible = true;
    }

    protected void RadDetail_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadDetail.MasterTableView.PageSize * RadDetail.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        SearchAttendances();
    }
}

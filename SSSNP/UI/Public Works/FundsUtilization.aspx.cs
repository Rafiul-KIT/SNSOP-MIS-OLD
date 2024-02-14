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
using System.IO;
using System.Net.Mail;
#endregion

#region Amendment History Begin
//Form Name : Fund Utilization
//Purpose : To Register Fund Utilization
//Tables used : tbl_FundsUtilization
//Stored procedures Used : GetworkproposalforFundsUtilization,InsUpdFundsUtilization,DeleteFundsUtilization
#endregion Amendment History End

public partial class FundsUtilization : System.Web.UI.Page
{


    #region reference class calling and Variable declaration
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";
    DataRow dtrdr;

    DataTable myDtNewRecdr = new DataTable();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // Look up Click Function Intialization
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        #region Is not Postback Statement
        if (!IsPostBack)
        {
            Session["dtMyTabledr"] = createtabledr();
            Session["tmpdrDataTable"] = ((DataTable)Session["dtMyTabledr"]);

        }
        #endregion

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Session["Photo"] = Photo;
            Session["PhotoFileName"] = "";

            Failure.Visible = false;
            success.Visible = false;
            FillDetails();

            BtnSave1.Text = "Update";
        }
        #endregion

    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetworkproposalforFundsUtilization(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
            //lblWorkActivity.Text=ds.Tables[0].Rows[0]["workcode"].ToString();
            //lblTarget.Text = ds.Tables[0].Rows[0]["EstManPower"].ToString();
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            string ad = ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString();
            if (ad != "")
            {
                lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            }

            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            Session["IP_NAME"] = ds.Tables[0].Rows[0]["IPName"].ToString();


            
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTable dt = ds.Tables[1];
                Session["DtList"] = dt;
               
                grdList.DataSource = ds.Tables[1];
                grdList.DataBind();

                BtnSave1.Text = "Update";
                BtnSave1.Enabled = true ;

            }
            else
            {
                DataTable dt = ds.Tables[1];
                Session["DtList"] = dt;
                grdList.DataSource = ds.Tables[1];
                grdList.DataBind();
                BtnSave1.Text = "Submit";
                BtnSave1.Enabled = false;
            }
           

        }
    }
    #endregion

    #region Button Click Functionalities
    protected void BtnAdd_Click(object sender, EventArgs e)
    {
        DataTable dt =  (DataTable)Session["DtList"];
        DataRow dr = dt.NewRow();
        comFunctions cmf = new comFunctions();
        try
        {
            DateTime date= cmf.convertDateIndia(txtFundsUtilizationDate.Text .Trim());
        }
        catch {
            lblmsg0.Text = "Please Enter Correct Date";
            Failure.Visible = true;

        }
        DataRow[] rows = dt.Select("FundsUtilizationFor='" + ddlFundsUtilizationFor.SelectedValue+"'");
        if (rows.Length > 0)
        {
            lblmsg0.Text = "This Type is Already Exists";
            Failure.Visible = true;
        }
        else
        {
            dr["FundsUtilizationDate"] = cmf.convertDateIndia(txtFundsUtilizationDate.Text.Trim());
            dr["Amount"] = txtAmount.Text;
            dr["FundsUtilizationFor"] = ddlFundsUtilizationFor.SelectedValue;
            dr["Remarks"] = txtRemarks.Text.Trim();
            dt.Rows.Add(dr);
            lblmsg0.Text = "";
            Failure.Visible = false;
            if (dt.Rows.Count > 0)
            {
                grdList.DataSource = dt;
                grdList.DataBind();
                BtnSave1.Enabled = true;
                txtFundsUtilizationDate.Text = "";
                txtAmount.Text = "";
                txtRemarks.Text = "";
                ddlFundsUtilizationFor.SelectedIndex = 0;
            }
            else
            {
                grdList.DataSource = dt;
                grdList.DataBind();
                BtnSave1.Enabled = false; }
            
        }
        Session["DtList"] = dt;
    }
    protected void btnDelete_Clicked(object sender,EventArgs  e)
    {
        string id = ((Button)sender).CommandArgument ;
        DataTable dt =  (DataTable)Session["DtList"];
        for (int i = 0;i<= dt.Rows.Count - 1; i++)
        {
            DataRow dr = dt.Rows[i];
            if (dr["FundsUtilizationFor"].ToString() == id)
                dr.Delete();
            dt.AcceptChanges();

        }
        if (dt.Rows.Count > 0)
        {
            grdList.DataSource = dt;
            grdList.DataBind();
            BtnSave1.Enabled = true;
        }
        else
        {
            grdList.DataSource = dt;
            grdList.DataBind();
            BtnSave1.Enabled = false; }
        Session["DtList"] = dt;
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (hdfID.Value.ToString() == "")
        {
            lblmsg0.Text = "Please select project name";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;

        }
        int j = Gen.DeleteFundsUtilization(hdfID.Value.ToString());
        
        DataTable dt = (DataTable)Session["DtList"];
        foreach (DataRow dr in dt.Rows )
        {
            int i = Gen.InsUpdFundsUtilization(hdfID.Value.ToString(),dr["FundsUtilizationDate"].ToString(), dr["FundsUtilizationFor"].ToString(), dr["Remarks"].ToString(), dr["Amount"].ToString(), Session["uid"].ToString());

        }


        lblmsg.Text = "Submited Successfully..!";
            success.Visible = true;
            Failure.Visible = false;
           // clear();
            //Response.Redirect("WorkProcessResult.aspx");
        }
       
    




    void clear()
    {

    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {
        txtFundsUtilizationDate.Text = "";
        ddlFundsUtilizationFor.Text = "--Select--";
        txtAmount.Text = "";
        txtRemarks.Text = "";
    }

    #endregion

    #region grid view Events
    protected void grdDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {



    }
    #endregion

    #region Bulk Insert Finctionalities
    private void fillTrademappingGriddr(DataTable tmpTabledr, string intWorkid, string DocumentName, string WorkPhotoStr, string Status, string Created_by)
    {

        // tmpTabledr = new DataTable();

        dtrdr = tmpTabledr.NewRow();
        //dtrdr["intWorkActivityId"] = "0";
        //dtrdr["new"] = "0";
        //

        //
        dtrdr["intWorkid"] = intWorkid.Trim();
        dtrdr["DocumentName"] = DocumentName.Trim();
        dtrdr["WorkPhoto"] = WorkPhotoStr.Trim();
        dtrdr["Status"] = Status.Trim();
        dtrdr["Created_by"] = Created_by.Trim();
        dtrdr["Created_dt"] = DateTime.Now;
        dtrdr["IntWorkDetailsTypeId"] = 0;

        tmpTabledr.Rows.Add(dtrdr);


    }

    protected DataTable createtabledr()
    {
        Session["dtMyTabledr"] = new DataTable("drDetails");
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("new", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intWorkid", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("DocumentName", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("WorkPhoto", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Status", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Created_by", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Created_dt", typeof(DateTime));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("IntWorkDetailsTypeId", typeof(Int32));
        return ((DataTable)Session["dtMyTabledr"]);
    }



    protected void grdAttachments_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
   
    protected void GetNewRectoInsertdr()
    {
        myDtNewRecdr = (DataTable)Session["tmpdrDataTable"];
        DataView dvdr = new DataView(myDtNewRecdr);
        //dvdr.RowFilter = "new = 0";
        myDtNewRecdr = dvdr.ToTable();
    }
   #endregion

}

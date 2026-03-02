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
#endregion

#region Amendment History Begin
//Form Name : Countie Master Registration
//Purpose : To Add Counties Details
//Tables used : tm_States,tm_Counties
//Stored procedures Used : sp_getStates,getCountiesbyID,insrtUpCounties,deletecounties
#endregion Amendment History End


public partial class AddCounties : System.Web.UI.Page
{
    #region reference class calling
    General Gen = new General();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        #region Is not Postback Statements
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
        }
        #endregion

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            if (Session["userlevel"].ToString() == "1")
            {
                BtnDelete.Visible = true;
            }
            else
            {
                BtnDelete.Visible = false;
            }
            BtnSave1.Text = "Update";
        }
        #endregion
    }
    #endregion

    #region Button Click functionalities Save, Update, Delete, Look up

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        Gen.intStateid = ddlState.SelectedValue;
        Gen.CountieCode = txtMSName.Text;
        Gen.CountieName = txtGuestName.Text;
        Gen.Latitude = txtTripDate.Text;
        Gen.Longitude = txtTripFrom.Text;
        Gen.CPopulation = txtpopulation.Text;


        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {
            int i = Gen.insrtUpCounties("0", Session["uid"].ToString(), txtNoofBlocks.Text);
            if (i != 999)
            {
                lblmsg.Text = "Added Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
            }
        }
        else
        {
            int i = Gen.insrtUpCounties(hdfID.Value, Session["uid"].ToString(), txtNoofBlocks.Text);

            if (i != 999)
            {
                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
                //lblmsg.Text = "Added Successfully..!";
            }
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddCounties.aspx");
    }

    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deletecounties(hdfID.Value);

        if (i == 1)
        {
            lblmsg.Text = "Deleted Successfully.";
            success.Visible = true;
            Failure.Visible = false;
            clear();
        }
        else if (i == 3)
        {
            lblmsg0.Text = "Not Possible.";
            success.Visible = false;
            Failure.Visible = true;
            clear();
        }
        else
        {
            lblmsg0.Text = "Please contact to Administrator.";
            success.Visible = false;
            Failure.Visible = true;
            clear();
        }
    }

    #endregion

    #region fill details using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getCountiesbyID(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            Gen.getStates(ddlState);
            ddlState.SelectedValue = ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["intStateid"].ToString()).Value;

            txtMSName.Text = ds.Tables[0].Rows[0]["CountieCode"].ToString();
            txtGuestName.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            txtTripDate.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
            txtTripFrom.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();

            txtpopulation.Text = ds.Tables[0].Rows[0]["Population"].ToString();
            txtNoofBlocks.Text = ds.Tables[0].Rows[0]["NoofBlocks"].ToString();
        }
    }
    #endregion

    #region Clear Function
    void clear()
    {
        txtNoofBlocks.Text = "";
        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;

        txtpopulation.Text = "";
        ddlState.SelectedIndex = 0;

       
        txtMSName.Text = "";

        txtGuestName.Text = "";
        txtTripDate.Text = "";
        txtTripFrom.Text = "";
    }
    #endregion

}

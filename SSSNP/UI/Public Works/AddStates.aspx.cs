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
//Form Name : Add State
//Purpose : To Add State Details
//Tables used : tm_States
//Stored procedures Used : insrtStates,getStatesbyID
#endregion Amendment History End


public partial class AddStates : System.Web.UI.Page
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

        if (!IsPostBack)
        {
          //  Gen.getStates(ddlState);
        }
        #region fill details calling using Lookup
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



        Gen.StateName = txtMSName.Text;
        Gen.StateCode = txtMSCode.Text;
        Gen.sLatitude = txtMSLatitude.Text;
        Gen.sLongitude = txtMSLongitude.Text;
        Gen.sPopulation = txtpopulation.Text;
       // Gen.BomaCode = txtBomaCode.Text;
       // Gen.intPayamid = ddlPayams.SelectedValue;
       // Gen.BomaName = txtBoma.Text;
       // Gen.Latitude = txtLatitude.Text;
        //Gen.Longitude = txtLongitude.Text;
       // txtMSName,txtGuestName,txtTripDate,txtTripFrom;



        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {
            int i = Gen.insrtStates("0", Session["uid"].ToString(), txtcounties.Text);
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
            int i = Gen.insrtStates(hdfID.Value, Session["uid"].ToString(), txtcounties.Text);

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
        Response.Redirect("AddStates.aspx");
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteStates(hdfID.Value);

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

    #region Clear Function
    void clear()
    {
        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;
        txtcounties.Text = "";

        txtMSName.Text = "";

        txtMSCode.Text = "";
        txtMSLatitude.Text = "";
        txtMSLongitude.Text = "";
        txtpopulation.Text = "";
    }
    #endregion

    #region fill details using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getStatesbyID(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {


            txtMSName.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            txtMSCode.Text = ds.Tables[0].Rows[0]["StateCode"].ToString();
            txtMSLatitude.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
            txtMSLongitude.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();
            txtpopulation.Text = ds.Tables[0].Rows[0]["Population"].ToString();
            txtcounties.Text = ds.Tables[0].Rows[0]["NoofCounties"].ToString();
        }
    }

    #endregion

}

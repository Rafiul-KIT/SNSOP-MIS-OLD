
#region Using/Inherits Classes

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
//Form Name : Vulnarability Master Registration
//Purpose : To Add Vulnarability Master Details
//Tables used : tm_Disability
//Stored procedures Used : insrtDisability,getDisabilitybyID,deleteDisability
#endregion Amendment History End

public partial class AddDisability : System.Web.UI.Page
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
        #region Is not Postback Statement
        if (!IsPostBack)
        {
            //  Gen.getStates(ddlState);
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
        Gen.Disability = txtMSName.Text;
        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {
            int i = Gen.insrtDisability("0", Session["uid"].ToString());
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
            int i = Gen.insrtDisability(hdfID.Value, Session["uid"].ToString());

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
        Response.Redirect("AddDisability.aspx");        
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteDisability(hdfID.Value);

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


        //ddlState.SelectedIndex = 0;

        // ddlCounties.Items.Clear();
        // ddlCounties.Items.Insert(0, "--Select--");
        // ddlCounties.SelectedIndex = 0;
        // ddlPayams.Items.Clear();
        // ddlPayams.Items.Insert(0, "--Select--");
        // ddlPayams.SelectedIndex = 0;
        txtMSName.Text = "";


    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getDisabilitybyID(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {


            txtMSName.Text = ds.Tables[0].Rows[0]["Disability"].ToString();
           


        }
    }
    #endregion


}

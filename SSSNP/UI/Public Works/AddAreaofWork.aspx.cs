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
//Form Name : Add Area of Work
//Purpose : To Add Area of Work Details
//Tables used : tm_AreaofWork
//Stored procedures Used : getAreaofWorkbyID,insrtAreaofWork,deleteAreaofWork,getAreaofworkSearch
#endregion Amendment History End


public partial class AddQualiication : System.Web.UI.Page
{
    #region reference class calling
    General Gen = new General();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        //Look up Click Event
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

        // If No Session, Redirect to Login Page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        #region Is not Postback Statement
        if (!IsPostBack)
        {

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


    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getAreaofWorkbyID(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {


            txtarea.Text = ds.Tables[0].Rows[0]["AreaofWork"].ToString();
           


        }
    }
    #endregion


    #region Button Click functionalities Save, Update, Delete, Look up
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        Gen.AreaofWork = txtarea.Text;
       
       



        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {
            int i = Gen.insrtAreaofWork("0", Session["uid"].ToString());
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
            int i = Gen.insrtAreaofWork(hdfID.Value, Session["uid"].ToString());

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
        Response.Redirect("AddAreaofWork.aspx");        
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteAreaofWork(hdfID.Value);

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

    #region Clear Functions

    void clear()
    {
        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;
        txtarea.Text = "";
    }
    #endregion
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
//designing and developed by siva as on 28-11-2015
//Purpose : To Add Qualification Details
//Tables used : tm_Qualification
public partial class AddQualification : System.Web.UI.Page
{
    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }

        if (!IsPostBack)
        {
            
        }
       
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
    }

    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();

       
            ds = Gen.getQualificationbyID(hdfID.Value.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                txtQualification.Text=ds.Tables[0].Rows[0]["QualificationName"].ToString();
                //txtDecription.Text=ds.Tables[0].Rows[0]["QualificationDescription"].ToString();

            }
        }    

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {
            int i = Gen.insrtUpdQualification("0", txtQualification.Text.Trim(), "", Session["uid"].ToString());
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
            int i = Gen.insrtUpdQualification(hdfID.Value, txtQualification.Text.Trim(), "", Session["uid"].ToString());
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
    void clear()
    {
        BtnSave1.Text = "Save";        
        txtQualification.Text = "";
        BtnDelete.Visible = false;
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddQualification.aspx");        
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteQualification(hdfID.Value);

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
}

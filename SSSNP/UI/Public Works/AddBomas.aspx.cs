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
//Form Name : Boma Master Registration
//Purpose : To Add Boma master Details
//Tables used : tm_Payams,tm_States,tm_Counties,tm_Bomas
//Stored procedures Used : sp_getStates,sp_getCounties,sp_getPayams,insrtUpdBomas,getBomasbyID,deleteBomas
#endregion Amendment History End

public partial class AddBomas : System.Web.UI.Page
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
        txtBomaCode.Enabled = false;
        #endregion
    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getBomasbyID(hdfID.Value.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {
                ddlState.SelectedValue = ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["intStateid"].ToString()).Value;
                getcounties();
                ddlCounties.SelectedValue = ddlCounties.Items.FindByValue(ds.Tables[0].Rows[0]["intCountieid"].ToString()).Value;
                getPayams();
                ddlPayams.SelectedValue = ddlPayams.Items.FindByValue(ds.Tables[0].Rows[0]["intPayamid"].ToString()).Value;
                txtBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                txtBomaCode.Text = ds.Tables[0].Rows[0]["BomaCode"].ToString();
                txtLatitude.Text = ds.Tables[0].Rows[0]["Latitude"].ToString();
                txtLongitude.Text = ds.Tables[0].Rows[0]["Longitude"].ToString();
                txtpopulation.Text = ds.Tables[0].Rows[0]["Population"].ToString();
            }
        }

    #endregion

    #region Button Click functionalities Save, Update, Delete, Look up
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        Gen.BomaCode = txtBomaCode.Text;
        Gen.intPayamid = ddlPayams.SelectedValue;
        Gen.BomaName = txtBoma.Text;
        Gen.Latitude = txtLatitude.Text;
        Gen.Longitude = txtLongitude.Text;
        Gen.BPopulation = txtpopulation.Text;
        

        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {
            int i = Gen.insertBomas("0", Session["uid"].ToString());
            if (i != 999)
            {
                if (i == 3)
                {
                    lblmsg0.Text = "Booma Code Already Exist. ";
                    success.Visible = false;
                    Failure.Visible = true;
                    return;
                }
                else
                {
                    lblmsg.Text = "Added Successfully..!";
                    success.Visible = true;
                    Failure.Visible = false;
                    clear();
                }
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
            int i = Gen.insertBomas(hdfID.Value, Session["uid"].ToString());

            if (i != 999)
            {
                if (i == 3)
                {
                    lblmsg0.Text = "Booma Code Already Exist. ";
                    success.Visible = false;
                    Failure.Visible = true;
                    return;
                }
                else { 
                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
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
        Response.Redirect("AddBomas.aspx");
    }
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteBomas(hdfID.Value);

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
        txtpopulation.Text = "";
        
        ddlState.SelectedIndex = 0;

        ddlCounties.Items.Clear();
        ddlCounties.Items.Insert(0, "--Select--");
        ddlCounties.SelectedIndex = 0;
        ddlPayams.Items.Clear();
        ddlPayams.Items.Insert(0, "--Select--");
        ddlPayams.SelectedIndex = 0;
        txtBomaCode.Text="";
        
         txtBoma.Text = "";
         txtLatitude.Text = "";
         txtLongitude.Text = "";
    }
    #endregion

    #region Dropdown Binding Functionalities (State,Countie,Payam)
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayams.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayams.Items.Insert(0, "--Select--");

        }
    }
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }
    void getPayams()
    {
        ddlPayams.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayams, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayams.Items.Insert(0, "--Select--");
        }
    }

    #endregion

    #region Boma Code Changed
    protected void txtBoma_TextChanged(object sender, EventArgs e)
    {
        lblmsg0.Text = "";
        success.Visible = false;
        Failure.Visible = false;
        if (txtBoma.Text != "" && hdfID.Value.ToString().Trim() == "")
        {
            if (txtBoma.Text.Trim().Length >= 3)
            {
                DataSet dscheck = new DataSet();
                dscheck = Gen.GetBomaCodeVerification(txtBoma.Text.Substring(0, 3));
                if (dscheck.Tables[0].Rows.Count > 0)
                {
                    int cnt = 100;
                    for (int a = 1; a < 100; a++)
                    {
                        DataSet dscheck1 = new DataSet();
                        txtBomaCode.Text = txtBoma.Text.Substring(0, 3) + a.ToString();
                        dscheck1 = Gen.GetBomaCodeVerification(txtBomaCode.Text);
                        if (dscheck1.Tables[0].Rows.Count > 0)
                        {
                            a = a + 1;
                            txtBomaCode.Text = txtBoma.Text.Substring(0, 3) + Convert.ToString(a);
                        }
                        else
                        {
                            txtBomaCode.Text = txtBoma.Text.Substring(0, 3) + a.ToString();
                            a = 101;
                        }
                    }

                }
                else
                {
                    txtBomaCode.Text = txtBoma.Text.Substring(0, 3);
                }
            }
            else
            {
                lblmsg0.Text = "Please Enter Boma name atleast 3 Characters";
                success.Visible = false;
                Failure.Visible = true;
                txtBomaCode.Text = "";
               // txtBoma.Text = "";
            }
        }
    }
    #endregion
}

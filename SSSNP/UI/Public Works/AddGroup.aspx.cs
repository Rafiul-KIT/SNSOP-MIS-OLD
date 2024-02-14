using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


public partial class AddGroup : System.Web.UI.Page
{
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
        }

        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }

    }

    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");

        }
    }
    void getPayams()
    {
        ddlPayam.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
        }
    }
    void getBomas()
    {
        ddlBoma.Items.Clear();
        if (ddlPayam.SelectedIndex != 0)
        {
            Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
        }
        else
        {
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }
    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomas();
    }
    protected void ddlGroupType_SelectedIndexChanged(object sender, EventArgs e)
    {
        GroupCodeShow();
        if (ddlGroupType.SelectedItem.Text == "Group")
        {
            
            txtGroupStrength.Text = "25";
        }
        if (ddlGroupType.SelectedItem.Text == "CST")
        {
            txtGroupStrength.Text = "12";
        }
    }
    void GroupCodeShow()
    { if (ddlGroupType.SelectedValue.ToString() == "Group")
        { trgroupcode.Visible = true; }
        else if (ddlGroupType.SelectedValue.ToString() == "CST")
        { trgroupcode.Visible = false; }
    }
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetGroupMasterById(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlState.SelectedValue = ds.Tables[0].Rows[0]["State"].ToString();
            getcounties();
            ddlCounties.SelectedValue = ds.Tables[0].Rows[0]["County"].ToString();
            getPayams();

            ddlPayam.SelectedValue= ds.Tables[0].Rows[0]["Payam"].ToString();
            getBomas();
            ddlBoma.SelectedValue = ds.Tables[0].Rows[0]["Boma"].ToString();
            ddlGroupType.SelectedValue= ds.Tables[0].Rows[0]["GroupType"].ToString();
            GroupCodeShow();
            ddl_group_code.SelectedValue = ds.Tables[0].Rows[0]["GroupCode"].ToString();
            txtGroupStrength.Text = ds.Tables[0].Rows[0]["GroupStrength"].ToString();
            hdGroupStrength.Value = ds.Tables[0].Rows[0]["GroupStrength"].ToString();
            ddlState.Enabled = false;
            ddl_group_code.Enabled = false;
            ddlPayam.Enabled = false;
            ddlCounties.Enabled = false;
            ddlGroupType.Enabled = false;
            ddlState.Enabled = false;
            ddlBoma.Enabled = false;
            BtnSave1.Text = "Update";
            //txtGroupStrength.Enabled = false;
           
        }
    }

    void Initialize()
    {
        BtnSave1.Text = "Submit";
        hdfID.Value = "";
        hdGroupStrength.Value = "0";
        ddlState.SelectedIndex = 0; 
        getcounties();
        ddlCounties.SelectedIndex = 0;
        getPayams();

        ddlPayam.SelectedIndex = 0;
        getBomas();
        ddlBoma.SelectedIndex = 0;
        ddlGroupType.SelectedIndex = 0;
        ddl_group_code.SelectedIndex = 0;
        txtGroupStrength.Text = "";
        ddlState.Enabled = true;
        ddl_group_code.Enabled = true;
        ddlPayam.Enabled = true;
        ddlCounties.Enabled = true;
        ddlGroupType.Enabled = true;
        ddlState.Enabled = true;
        ddlBoma.Enabled = true;
        txtGroupStrength.Enabled = true;
        
    }
    
    protected void BtnCancel_Click(object sender, EventArgs e)
    { Initialize();
        Failure.Visible = false;
        success.Visible = false;
        lblmsg.Text = "";
        lblmsg0.Text = "";
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        //int count = 0;

        //if (count == 0)
        //{
        //    lblmsg0.Text = "At lease one Household should be selected";
        //    Failure.Visible = true;
        //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
        //    return;

        //}
       // DataSet ds= Gen.CheckDuplicateGroup(ddlBoma.SelectedValue, ddlGroupType.SelectedValue, ddl_group_code.SelectedValue);

        int GroupStrength=Convert.ToInt16(hdGroupStrength.Value.ToString());
        int GroupStrengthNew = Convert.ToInt16(txtGroupStrength.Text.ToString());
        if (GroupStrength > 0 && GroupStrength> GroupStrengthNew)
        {
            lblmsg0.Text = "  Group Strength Should Be More Than Current  Strength";
            success.Visible = false;
            Failure.Visible = true;
        }
        else
        {
            int i = Gen.InsertUpdateGroup(hdfID.Value.ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, ddlGroupType.SelectedValue, ddl_group_code.SelectedValue, txtGroupStrength.Text, Session["uid"].ToString());
            if (i > 0)
            {

                lblmsg.Text = "Submited Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                Initialize();

            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
            }
        }
    }

   
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }

    
}

#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Telerik.Web.UI;
#endregion

#region Amendment History Begin
//Form Name : Add Wage Rate
//Purpose : To Add Wage Rate Details
//Tables used : td_ExchangeRateDet,tm_States,tm_Counties
//Stored procedures Used : sp_getStates,sp_getCounties,Get_Wage_RateStateCountie,Insert_Update_Wage_RateByCountie
#endregion Amendment History End

public partial class WageRate : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    General Gen = new General();
    int i = 0;
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            Getdata();
        }
    }
    #endregion
    #region fill details Calling using Lookup
    void Getdata()
    {
        DataSet ds = new DataSet();
        ds = Gen.Get_Wage_RateFull();
        if (ds.Tables[0].Rows.Count > 0)
        {
            GD_List.DataSource = ds.Tables[0];
            GD_List.DataBind();
        }
    }
    #endregion
    #region Button Click functionalities Save, Update, Clear
    protected void BtnSave1_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(3000);

        if (BtnSave1.Text == "Save")
        {
            i = Gen.Insert_Update_Wage_RateByCountie(Convert.ToDecimal(txt_wage_rate.Text), Convert.ToInt32(Session["uid"]), 0, ddlState.SelectedValue, ddlCounties.SelectedValue);
            if (i == 1)
            {
                lblmsg0.Text = "Already Exist, Can not insert more than one";
                success.Visible = false;
                Failure.Visible = true;
            }
            if (i == 0)
            {
                lblmsg.Text = "Added Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                txt_wage_rate.Text = "";
                ddlState.SelectedIndex = 1;
                ddlCounties.Items.Clear();
                ddlCounties.Items.Insert(0, "--Select--");
                ddlCounties.Enabled = true;
                ddlState.Enabled = true;
                Getdata();
            }
        }
        if (BtnSave1.Text == "Update")
        {
            i = Gen.Insert_Update_Wage_RateByCountie(Convert.ToDecimal(txt_wage_rate.Text), Convert.ToInt32(Session["uid"]), Convert.ToInt32(hdfID.Value), ddlState.SelectedValue, ddlCounties.SelectedValue);
            if (i == 0)
            {
                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                txt_wage_rate.Text = "";
                ddlState.SelectedIndex = 1;
                ddlCounties.Items.Clear();
                ddlCounties.Items.Insert(0, "--Select--");
                ddlCounties.Enabled = true;
                ddlState.Enabled = true;
                BtnSave1.Text = "Save";
                Getdata();
            }
        }


    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        txt_wage_rate.Text = "";
        BtnSave1.Text = "Save";
    }

    #endregion



    #region Gridview Events
    protected void GD_List_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "select")
        {


            int id = Convert.ToInt32(e.CommandArgument);

            BtnSave1.Text = "Update";
            hdfID.Value = Convert.ToString(e.CommandArgument);

            DataSet ds = Gen.Get_Wage_RateByID(id);
            DataTable dt = ds.Tables[0];


            ddlState.SelectedValue = dt.Rows[0]["intStateId"].ToString();
            ddlState.Enabled = false;
            getcounties();
            ddlCounties.SelectedValue = dt.Rows[0]["intCountyId"].ToString();
            ddlCounties.Enabled = false;

            txt_wage_rate.Text = dt.Rows[0]["WageRate"].ToString();
        }
    }
    #endregion
    #region Dropdown Binding Functionalities
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();

        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");


        }
    }
    #endregion
}
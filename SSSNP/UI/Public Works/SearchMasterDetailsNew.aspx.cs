#region Using/Inherits Classess
using System;
using System.Collections;
using System.Configuration;

using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
#endregion

#region Amendment History Begin
//Form Name : Search Master Details
//Purpose : To Get master details
//Tables used : tm_States,tbl_Users,tm_States,tm_Counties,tm_Payams,tm_Bomas
//Stored procedures Used :sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,getTypeSearchNew
#endregion Amendment History End


public partial class SearchMasterDetailsNew : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();
    
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        // Is not Postback Statement
        if (!IsPostBack)
        {
           Gen.getStates(ddlState);
           
        }
    }
    #endregion

    #region fill details by Search
    void fillgrid()
    {
        DataSet ds = new DataSet();
        if (ddlState.SelectedIndex != 0 && ddlCounties.SelectedIndex == 0 && ddlPayam.SelectedIndex == 0 && ddlBoma.SelectedIndex == 0)
        {
            ds = Gen.getTypeSearchNew(ddlState.SelectedValue, "", "", "","S","");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                grdDetails0.DataSource = null;
                grdDetails0.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
                Label351.Visible = true;
                grdDetails3.DataSource = null;
                grdDetails3.DataBind();
                Label351.Text = "State Details";
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                grdDetails0.DataSource = null;
                grdDetails0.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
                grdDetails3.DataSource = null;
                grdDetails3.DataBind();
            }
            
        }
        else if (ddlState.SelectedIndex != 0 && ddlCounties.SelectedIndex != 0 && ddlPayam.SelectedIndex == 0 && ddlBoma.SelectedIndex == 0)
        {
            ds = Gen.getTypeSearchNew(ddlState.SelectedValue, ddlCounties.SelectedValue, "", "", "C","");
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails0.DataSource = ds.Tables[0];
                grdDetails0.DataBind();
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
                grdDetails3.DataSource = null;
                grdDetails3.DataBind();
                Label351.Visible = true;
                Label351.Text="Counties Details";
               
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails0.DataSource = ds.Tables[0];
                grdDetails0.DataBind();
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
                grdDetails3.DataSource = null;
                grdDetails3.DataBind();
            }
          }
        else if (ddlState.SelectedIndex != 0 && ddlCounties.SelectedIndex != 0 && ddlPayam.SelectedIndex != 0 && ddlBoma.SelectedIndex == 0)
        {
            ds = Gen.getTypeSearchNew(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, "", "P","");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                    grdDetails1.DataSource = ds.Tables[0];
                    grdDetails1.DataBind();
                    grdDetails.DataSource = null;
                    grdDetails.DataBind();
                    grdDetails0.DataSource = null;
                    grdDetails0.DataBind();
                    grdDetails2.DataSource = null;
                    grdDetails2.DataBind();
                    grdDetails3.DataSource = null;
                    grdDetails3.DataBind();
                    Label351.Visible = true;
                    Label351.Text = "Payam Details";
                }
                else
                {
                    lblMsg.Text = "No Records found.";
                    grdDetails1.DataSource = ds.Tables[0];
                    grdDetails1.DataBind();
                    grdDetails.DataSource = null;
                    grdDetails.DataBind();
                    grdDetails0.DataSource = null;
                    grdDetails0.DataBind();
                    grdDetails2.DataSource = null;
                    grdDetails2.DataBind();
                    grdDetails3.DataSource = null;
                    grdDetails3.DataBind();
                   
                }
            }
        else if (ddlState.SelectedIndex != 0 && ddlCounties.SelectedIndex != 0 && ddlPayam.SelectedIndex != 0 && ddlBoma.SelectedIndex != 0)
        {
            ds = Gen.getTypeSearchNew(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, "B","");
                if (ds.Tables[0].Rows.Count > 0)
                {
                    lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                    grdDetails2.DataSource = ds.Tables[0];
                    grdDetails2.DataBind();
                    grdDetails.DataSource = null;
                    grdDetails.DataBind();
                    grdDetails1.DataSource = null;
                    grdDetails1.DataBind();
                    grdDetails0.DataSource = null;
                    grdDetails0.DataBind();
                    grdDetails3.DataSource = null;
                    grdDetails3.DataBind();
                    Label351.Visible = true;
                    Label351.Text = "Bomas Details";
                }
                else
                {
                    lblMsg.Text = "No Records found.";
                    grdDetails2.DataSource = ds.Tables[0];
                    grdDetails2.DataBind();
                    grdDetails.DataSource = null;
                    grdDetails.DataBind();
                    grdDetails1.DataSource = null;
                    grdDetails1.DataBind();
                    grdDetails0.DataSource = null;
                    grdDetails0.DataBind();
                    grdDetails3.DataSource = null;
                    grdDetails3.DataBind();
                    
                    
                }
            }
        else if (txtEnrollment.Text!="")
        {
            ds = Gen.getTypeSearchNew("", "", "", "", "IP", txtEnrollment.Text);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails3.DataSource = ds.Tables[0];
                grdDetails3.DataBind();
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails0.DataSource = null;
                grdDetails0.DataBind();
                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
                Label351.Visible = true;
                Label351.Text = "IP Details";
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails3.DataSource = ds.Tables[0];
                grdDetails3.DataBind();
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails0.DataSource = null;
                grdDetails0.DataBind();

                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
            }
        }
        else if (txtEnrollment.Text == "" && ddlState.SelectedValue == "--Select--")
        {

                grdDetails3.DataSource = null;
                grdDetails3.DataBind();
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                grdDetails1.DataSource = null;
                grdDetails1.DataBind();
                grdDetails0.DataSource = null;
                grdDetails0.DataBind();
                grdDetails2.DataSource = null;
                grdDetails2.DataBind();
                Label351.Visible = false;
                //Label351.Text = "IP Details";
                lblMsg.Text = "Please select any one of the above";
        }
        }
    #endregion

    #region Button Click functionalities
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    #endregion

    #region Gridview Events
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        
    }


    protected void grdDetails0_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails0.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails2.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails3_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails3.PageIndex = e.NewPageIndex;
        fillgrid();
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
        ddlPayam.Items.Clear();
        ddlBoma.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
            ddlPayam.Items.Insert(0, "--Select--");
            ddlBoma.Items.Insert(0, "--Select--");
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
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }
    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomas();
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
    protected void ddlBoma_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion
}

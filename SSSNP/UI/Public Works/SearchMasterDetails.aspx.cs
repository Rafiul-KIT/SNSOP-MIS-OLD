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


//created by suresh as on 17-1-2016 tables:td_IPDetails,getIPSearch

public partial class SearchMasterDetails : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();
    
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        //if (Session.Count <= 0)
        //{
        //    Response.Redirect("../../frmUserLogin.aspx");
        //}
        if (!IsPostBack)
        {
           // Gen.getStates(ddlState);
            //fillgrid();
        }
    }
    #endregion


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {
        DataSet ds = new DataSet();
        if (ddltypeip.SelectedValue == "S")
        {
            ds = Gen.getTypeSearch(ddltypeip.SelectedValue);
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
       else if (ddltypeip.SelectedValue == "C")
        {
            ds = Gen.getTypeSearch(ddltypeip.SelectedValue);
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
           else if (ddltypeip.SelectedValue == "P")
            {
                ds = Gen.getTypeSearch(ddltypeip.SelectedValue);
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
           else if (ddltypeip.SelectedValue == "B")
            {
                ds = Gen.getTypeSearch(ddltypeip.SelectedValue);
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
        else if (ddltypeip.SelectedValue == "IP")
        {
            ds = Gen.getTypeSearch(ddltypeip.SelectedValue);
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
        else if (ddltypeip.SelectedValue == "--Select--")
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
                lblMsg.Text = "";
        }
        }
    
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if ((e.Row.RowType == DataControlRowType.DataRow))
        //{
        //    HyperLink h1 = (HyperLink)e.Row.Cells[0].Controls[0];
        //    h1.Text = "Edit";
        //    h1.NavigateUrl = "TSTIPregistration.aspx?prj=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim();
        //}
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
}

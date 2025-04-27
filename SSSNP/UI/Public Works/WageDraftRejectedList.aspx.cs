using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;

public partial class WageDraftRejectedList : System.Web.UI.Page
{
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("../../frmuserlogin.aspx"); // Boss wants us to not show this menu right now

        //if (Session.Count == 0)
        //{
        //    Response.Redirect("../../frmuserlogin.aspx");
        //}
        //HttpContext.Current.Server.ScriptTimeout = 1000;

        //this.btnSearch.Attributes.Add("onclick", DisableTheButton(this.Page, this.btnSearch, "group"));
        //if (!IsPostBack)
        //{
        //    Gen.getStates(ddlState);
        //    Gen.getIpDet(ddlIP);
        //    if (Session["user_type"].ToString().Trim() == "IP")
        //    {
        //        ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
        //        ddlIP.Enabled = false;
        //    }
        //}
    }

    protected void BtnCancel_Click(object sender, EventArgs e)
    {
        TxtRequestNo.Text="";
        txtWorkcode.Text = "";
        ddlState.SelectedIndex = 0;
        ddlCounties.SelectedIndex = 0;
        ddlPayam.SelectedIndex = 0;
        ddlBoma.SelectedIndex = 0;
        ddlIP.SelectedIndex = 0;
            
    }

    void clear()
    {
        grdRejectedWagesDraft.DataSource = null;
        grdRejectedWagesDraft.DataBind();
        tblreport.Visible = false;
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();
            if (Session["user_type"].ToString().Trim() == "IP")

                ds = Gen.getRejectedWageRequestDrafts(Session["uid"].ToString().Trim(), txtWorkcode.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, ddlIP.SelectedValue, TxtRequestNo.Text);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    grdRejectedWagesDraft.DataSource = ds.Tables[0];
                    grdRejectedWagesDraft.DataBind();

                }
                else
                {
                    //DISPLAY A MESSAGE FOR NO RECORD FOUND 
                    grdRejectedWagesDraft.DataSource = null;
                    grdRejectedWagesDraft.DataBind();
                }
            }
            else
            {
                //DISPLAY A MESSAGE FOR NO RECORD FOUND 
                grdRejectedWagesDraft.DataSource = null;
                grdRejectedWagesDraft.DataBind();
            }
        }
        catch (Exception ex)
        {
            grdRejectedWagesDraft.DataSource = null;
            grdRejectedWagesDraft.DataBind();
            lblmessage.Text = "There is no attendance/wages ";
        }
        finally
        {
        }
    }

    public string DisableTheButton(Control pge, Control btn, string vldGroup)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
        if (vldGroup.Trim() == "")
        {
            sb.Append("if (Page_ClientValidate() == false) { return false; }} ");
        }
        else
        {
            sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");
        }
        sb.Append("this.value = 'Please wait...';");
        sb.Append("this.disabled = true;");
        sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }

    protected void grdRejectedWagesDraft_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void fillgrid()
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();
            ds = Gen.getRejectedWageRequestDrafts(Session["uid"].ToString().Trim(), txtWorkcode.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, ddlIP.SelectedValue, TxtRequestNo.Text);

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    grdRejectedWagesDraft.DataSource = ds.Tables[0];
                    grdRejectedWagesDraft.DataBind();
                }
                else
                {
                    //DISPLAY A MESSAGE FOR NO RECORD FOUND
                    grdRejectedWagesDraft.DataSource = null;
                    grdRejectedWagesDraft.DataBind();
                }
            }
            else
            {
                //DISPLAY A MESSAGE FOR NO RECORD FOUND
                grdRejectedWagesDraft.DataSource = null;
                grdRejectedWagesDraft.DataBind();
            }
        }
        catch (Exception ex)
        {
            grdRejectedWagesDraft.DataSource = null;
            grdRejectedWagesDraft.DataBind();
            lblmessage.Text = "There is no attendance/wages ";
        }
        finally
        {
        }
    }

    protected void grdRejectedWagesDraft_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdRejectedWagesDraft_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdRejectedWagesDraft.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
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
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }

    protected void grdRejectedWagesDraft_PreRender(object sender, EventArgs e)
    {
        //Hide SSP and Exchange rate columns to IP
        if (Session["user_type"].ToString().Trim() == "IP")
        {
            int iTotColumns = 0;
            iTotColumns = grdRejectedWagesDraft.Columns.Count;
            for (int iCount = 0; iCount < iTotColumns; iCount++)
            {
                if (grdRejectedWagesDraft.Columns[iCount].HeaderText.ToLower().Trim() == "Exchange Rate".ToLower())
                {
                    grdRejectedWagesDraft.Columns[iCount].Visible = false;
                }
                else if (grdRejectedWagesDraft.Columns[iCount].HeaderText.ToLower().Trim() == "Amount Paid in SSP".ToLower())
                {
                    grdRejectedWagesDraft.Columns[iCount].Visible = false;
                }
                else
                {
                    grdRejectedWagesDraft.Columns[iCount].Visible = true;
                }
            }
        }
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
}

using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;
using Telerik.Web.UI;

public partial class frmwagespaidalter : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            clsGeneral.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
            Gen.getStates(ddlState);
            //if (Request.QueryString.Count>0)
            //{
            //    if (Request.QueryString[0].ToString() != "")
            //    { hdnStat.Value = Request.QueryString[0].ToString();
            //        fillgrid();
            //    }
            //}
        }
    }
    #endregion



    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetWagesPaidalternewlatest(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txthousehold.Text, txtenrollment.Text, DdlType.SelectedValue);
        if (ds.Tables.Count > 0)
        {
            DataTable dt = ds.Tables[0];
            RadGrid1.DataSource = dt;
            RadGrid1.DataBind();

            if (ds.Tables[0].Rows.Count > 0)
            {
                //btnexp.Visible = true;
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                //grdDetails.DataSource = ds.Tables[0];
                //grdDetails.DataBind();

            }
            else
            {
                //btnexp.Visible = false;
                lblMsg.Text = "No Records found.";
                //grdDetails.DataSource = ds.Tables[0];
                //grdDetails.DataBind();
            }
        }
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = clsGeneral.GetWagesPaidalternewlatest(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, txthousehold.Text, txtenrollment.Text, DdlType.SelectedValue);

        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //btnexp.Visible = true;
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                RadGrid1.DataSource = ds.Tables[0];
            }
            else
            {
                //btnexp.Visible = false;
                lblMsg.Text = "No Records found.";
                RadGrid1.DataSource = ds.Tables[0];
            }
        }


    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadGrid1.MasterTableView.PageSize * RadGrid1.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            //HyperLink hyperLinkView = e.Item.FindControl("lnkUpdate") as HyperLink;
            ////hyperLinkView.Target = "_blank";
            //hyperLinkView.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenTempid"];
        }
    }

    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }
    //protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdDetails.PageIndex = e.NewPageIndex;
    //    fillgrid();
    //}
    //protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {
    //        HyperLink hpym = (HyperLink)e.Row.Cells[10].Controls[0];
    //        //h1.Text = "Edit";
    //        hpym.Target = "_blank";
    //        hpym.NavigateUrl = "frmBenViewDetails1.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();

    //    }
    //}


    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        Gen.getProjects(ddlProject, ddlIP.SelectedValue);
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
    protected void ddlCounties_SelectedIndexChanged1(object sender, EventArgs e)
    {
        getPayams();
    }
    protected void ddlPayam_SelectedIndexChanged1(object sender, EventArgs e)
    {
        getBomas();
    }

    protected void ddlAreasofWork_SelectedIndexChanged(object sender, EventArgs e)
    {
        getWorkActivities();
    }
    void getWorkActivities()
    {


    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

        getworksbyPojects();
    }

    void getworksbyPojects()
    {


    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmwagespaidalter.aspx");
    }
    //protected void ExportToExcel()
    //{
    //    //GridView Export = new GridView();

    //    Response.Clear();
    //    Response.Buffer = true;
    //    string FileName = "Wage Paid Alternates Report" + DateTime.Now + ".xls";
    //    Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
    //    Response.Charset = "";
    //    Response.ContentType = "application/vnd.ms-excel";
    //    using (StringWriter sw = new StringWriter())
    //    {
    //        //grdDetails.GridLines = GridLines.Both;
    //        HtmlTextWriter hw = new HtmlTextWriter(sw);

    //        //To Export all pages
    //        //grdDetails.AllowPaging = false;
    //        this.fillgrid();
    //        //grdDetails.Columns[0].Visible = false;
    //        //grdDetails.HeaderRow.BackColor = Color.White;
    //        //foreach (TableCell cell in grdDetails.HeaderRow.Cells)
    //        //{
    //        //    cell.BackColor = grdDetails.HeaderStyle.BackColor;
    //        //}
    //        //foreach (GridViewRow row in grdDetails.Rows)
    //        //{
    //        //    row.BackColor = Color.White;
    //        //    foreach (TableCell cell in row.Cells)
    //        //    {
    //        //        if (row.RowIndex % 2 == 0)
    //        //        {
    //        //            cell.BackColor = grdDetails.AlternatingRowStyle.BackColor;
    //        //        }
    //        //        else
    //        //        {
    //        //            cell.BackColor = grdDetails.RowStyle.BackColor;
    //        //        }
    //        //        cell.CssClass = "textmode";
    //        //    }
    //        //}

    //        //grdDetails.RenderControl(hw);

    //        //style to format numbers to string
    //        string style = @"<style>.textmode{mso-number-format:\@;}</style>";
    //        Response.Write(style);
    //        Response.Output.Write(sw.ToString());
    //        Response.Flush();
    //        Response.End();
    //    }
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    //protected void btnexp_Click(object sender, EventArgs e)
    //{
    //    ExportToExcel();
    //}
}

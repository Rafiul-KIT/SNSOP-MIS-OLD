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
using System.Drawing;
using System.IO;
using System.Text;
using Telerik.Web.UI;
using System.Runtime;
#endregion
#region Amendment History Begin
//Form Name : Beneficiary Search Details
//Purpose : To Search Beneficiary Details
//Tables used : tm_States,tm_Counties,tm_Payams,tm_Bomas,tbl_BeneficiaryDet
//Stored procedures Used : sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,sp_GetBenfsearchdet
#endregion Amendment History End

public partial class frmbenfsearchrpt : System.Web.UI.Page
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
            if (Session["user_type"].ToString() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString()).Value;
                ddlIP.Enabled = false;
            }

            Gen.getStates(ddlState);
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString[0].ToString() != "")
                {
                    hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
        btnexp.Visible = false;
    }
    #endregion


    #region Search Report and Filling Details Functions
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetBenfsearchDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                //lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                //grdDetails.DataSource = ds.Tables[0];
                //grdDetails.DataBind();
                btnexp.Visible = false;
                // btnexpcsv.Visible = true;
            }
            else
            {
                //lblMsg.Text = "No Records found.";
                //grdDetails.DataSource = ds.Tables[0];
                //grdDetails.DataBind();
                btnexp.Visible = false;
                btnexpcsv.Visible = false;
            }
        }
    }
    #endregion

    #region Search Report and Filling Details Functions
    void fillgridExport()
    {
        DataSet ds1 = new DataSet();
        ds1 = clsGeneral.GetBenfsearchDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if (ds1.Tables.Count > 0)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds1.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetailsExport.DataSource = ds1.Tables[0];
                grdDetailsExport.DataBind();

            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetailsExport.DataSource = ds1.Tables[0];
                grdDetailsExport.DataBind();

            }
        }
    }
    #endregion
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }
    #region Gridview Event Function

    protected void telGridDetails_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetBenfsearchDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        DataTable dt = ds.Tables[0];
        telGridDetails.DataSource = dt;
        telGridDetails.Visible = true;

    }

    protected void telGridDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = telGridDetails.MasterTableView.PageSize * telGridDetails.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            HyperLink hyperLinkView = e.Item.FindControl("colLink") as HyperLink;
            hyperLinkView.Target = "_blank";
            hyperLinkView.NavigateUrl = "frmBenViewDetails1.aspx?ID=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenId"];
        }

        //if (Request.QueryString[0].ToString() == "1")
        //{
        //    if (e.Item is GridDataItem)
        //    {
        //        GridDataItem item = (GridDataItem)e.Item;
        //        //item["checkColumn"].Visible = false;
        //        item["btnUpdate"].Text = "Update";
        //    }
        //}
    }
    #endregion

    #region Dropdown Binding Functionalities and Dropdown and TextBox Index Changed Events
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
    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        //Gen.getProjects(ddlProject, ddlIP.SelectedValue);
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

        DataSet ds = new DataSet();
        ds = clsGeneral.GetBenfsearchDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        DataTable dt = ds.Tables[0];
        telGridDetails.DataSource = dt;
        telGridDetails.Visible = true;
        telGridDetails.DataBind();
        //btnexp.Visible = true;
    }
    #endregion

    #region Export Excel and Print Functions
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmbenfsearchrpt.aspx");
    }
    protected void ExportToExcel()
    {
        //GridView Export = new GridView();
        grdDetailsExport.Visible = true;
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Benfeciary Search Report" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            grdDetailsExport.GridLines = GridLines.Both;
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages

            this.fillgridExport();
            //  grdExtentDetails.Columns[0].Visible = false;
            grdDetailsExport.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdDetailsExport.HeaderRow.Cells)
            {
                cell.BackColor = grdDetailsExport.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdDetailsExport.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdDetailsExport.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdDetailsExport.RowStyle.BackColor;
                    }

                }
            }

            grdDetailsExport.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style>.textmode{mso-number-format:\@;}</style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
            grdDetailsExport.Visible = false;
        }
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        //base.VerifyRenderingInServerForm(control);
    }
    protected void btnexp_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
    protected void btnexpcsv_Click(object sender, EventArgs e)
    {
        grdDetailsExport.Visible = true;

        this.fillgridExport();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BeneficiaryDetails.csv");
        Response.Charset = "";
        Response.ContentType = "application/text";


        StringBuilder columnbind = new StringBuilder();
        for (int k = 0; k < grdDetailsExport.Columns.Count; k++)
        {

            columnbind.Append(grdDetailsExport.Columns[k].HeaderText + ',');
        }

        columnbind.Append("\r\n");
        for (int i = 0; i < grdDetailsExport.Rows.Count; i++)
        {
            for (int k = 0; k < grdDetailsExport.Columns.Count; k++)
            {

                columnbind.Append(grdDetailsExport.Rows[i].Cells[k].Text + ',');
            }

            columnbind.Append("\r\n");
        }
        Response.Output.Write(columnbind.ToString());
        Response.Flush();
        Response.End();
        grdDetailsExport.Visible = false;
    }
    #endregion
}

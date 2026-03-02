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

public partial class frmBenDeletionNew : System.Web.UI.Page
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
            if (Request.QueryString.Count>0)
            {
                if (Request.QueryString[0].ToString() == "1")
                {
                    lblhead.Text = "Deletion of Beneficiary Basic Details";
                }
                if (Request.QueryString[0].ToString() == "2")
                {
                    lblhead.Text = "Deletion of Beneficiary Allocation Details";
                }
                if (Request.QueryString[0].ToString() == "3")
                {
                    lblhead.Text = "Deletion of Beneficiary Attendance Details";
                }
                if (Request.QueryString[0].ToString() == "4")
                {
                    lblhead.Text = "Deletion of Beneficiary Wage Details";
                }
            //    if (Request.QueryString[0].ToString() != "")
            //    { hdnStat.Value = Request.QueryString[0].ToString();
            //        fillgrid();
            //    }
            }
        }
    }
    #endregion


   
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetMainBenfDetforDeletion(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if(ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                grdDetails.Columns[10].Visible = false;
                grdDetails.Columns[11].Visible = false;
                grdDetails.Columns[12].Visible = false;
                grdDetails.Columns[13].Visible = false;
                if (Request.QueryString.Count > 0)
                {
                    if (Request.QueryString[0].ToString().Trim() == "1")
                    {
                        grdDetails.Columns[10].Visible = true;
                        grdDetails.Columns[11].Visible = false;
                        grdDetails.Columns[12].Visible = false;
                        grdDetails.Columns[13].Visible = false;
                    }
                    if (Request.QueryString[0].ToString().Trim() == "2")
                    {
                        grdDetails.Columns[10].Visible = false;
                        grdDetails.Columns[11].Visible = true;
                        grdDetails.Columns[12].Visible = false;
                        grdDetails.Columns[13].Visible = false;
                    }
                    if (Request.QueryString[0].ToString().Trim() == "3")
                    {
                        grdDetails.Columns[10].Visible = false;
                        grdDetails.Columns[11].Visible = false;
                        grdDetails.Columns[12].Visible = true;
                        grdDetails.Columns[13].Visible = false;
                    }
                    if (Request.QueryString[0].ToString().Trim() == "4")
                    {
                        grdDetails.Columns[10].Visible = false;
                        grdDetails.Columns[11].Visible = false;
                        grdDetails.Columns[12].Visible = false;
                        grdDetails.Columns[13].Visible = true;
                    }
                }
                else
                {
                    grdDetails.Columns[10].Visible = false;
                    grdDetails.Columns[11].Visible = false;
                    grdDetails.Columns[12].Visible = false;
                    grdDetails.Columns[13].Visible = false;
                }
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
        }
      


    }
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        //string id = (sender as LinkButton).CommandArgument;
        //Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            if (Request.QueryString.Count > 0)
            {
                if (Request.QueryString[0].ToString().Trim() == "1")
                {
                    HyperLink h1 = (HyperLink)e.Row.Cells[10].Controls[0];

                    h1.NavigateUrl = "frmDeleteBenSplitBasicDetails.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim() + "&S=1";
                }
                if (Request.QueryString[0].ToString().Trim() == "2")
                {
                    HyperLink h2 = (HyperLink)e.Row.Cells[11].Controls[0];

                    h2.NavigateUrl = "frmDeleteBenSplitAllocationDetails.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim() + "&S=2";
                }
                if (Request.QueryString[0].ToString().Trim() == "3")
                {
                    HyperLink h3 = (HyperLink)e.Row.Cells[12].Controls[0];

                    h3.NavigateUrl = "frmDeleteBenSplitAttendanceDetails.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim() + "&S=3";
                }
                if (Request.QueryString[0].ToString().Trim() == "4")
                {
                    HyperLink h4 = (HyperLink)e.Row.Cells[13].Controls[0];

                    h4.NavigateUrl = "frmDeleteBenSplitWageDetails.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim() + "&S=4";
                }
            }
        }
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
    //protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    //Gen.getProjects(ddlProject, ddlIP.SelectedValue);
    //}
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
        if (ddlIP.SelectedIndex == 0 && ddlState.SelectedIndex == 0 && ddlCounties.SelectedIndex == 0 && ddlPayam.SelectedIndex == 0 && ddlBoma.SelectedIndex == 0 && txtbenf.Text == "" && txtenrollment.Text == "" && txthousehold.Text == "")
        {
            lblresult.Text = "Please select Atleast one to Search";
        }
        else
        {
            btnexp.Visible = true;
            fillgrid();
            lblresult.Text = "";
        }
    }
    
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmBenDeletion.aspx");
    }
    protected void ExportToExcel()
    {
        //GridView Export = new GridView();

        Response.Clear();
        Response.Buffer = true;
        string FileName = "Benfeciary Details Report" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            grdDetails.GridLines = GridLines.Both;
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdDetails.AllowPaging = false;
            this.fillgrid();
            //  grdExtentDetails.Columns[0].Visible = false;
            grdDetails.HeaderRow.BackColor = Color.White;
            foreach (TableCell cell in grdDetails.HeaderRow.Cells)
            {
                cell.BackColor = grdDetails.HeaderStyle.BackColor;
            }
            foreach (GridViewRow row in grdDetails.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    if (row.RowIndex % 2 == 0)
                    {
                        cell.BackColor = grdDetails.AlternatingRowStyle.BackColor;
                    }
                    else
                    {
                        cell.BackColor = grdDetails.RowStyle.BackColor;
                    }
                    cell.CssClass = "textmode";
                }
            }

            grdDetails.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style>.textmode{mso-number-format:\@;}</style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
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
}

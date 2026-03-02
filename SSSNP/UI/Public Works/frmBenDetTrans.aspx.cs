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
using OfficeOpenXml;

public partial class frmBenDetTrans : System.Web.UI.Page
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
                if (Request.QueryString[0].ToString() != "")
                { hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
    }
    #endregion


   
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetBenftransDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text,txtbenf.Text,txtenrollment.Text,TxtFromDate.Text,TxtToDate.Text);
        if(ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
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
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
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
    }
    
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmBenDetTrans.aspx");
    }
    protected void ExportToExcel()
    {

        //DataSet dsNew = new DataSet();
        //dsNew = clsGeneral.GetBenftransDet(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text, TxtFromDate.Text, TxtToDate.Text);
        //if (dsNew.Tables[0].Rows.Count > 0)
        //{
        //    string filename = "Beneficiary Deleted Details Report" + DateTime.Now + ".xlsx";
        //    DataTable dt = new DataTable();
        //    dt = dsNew.Tables[0];
        //    //Set Name of DataTables.
        //    HttpContext.Current.Response.Clear();
        //    HttpContext.Current.Response.ClearContent();
        //    HttpContext.Current.Response.ClearHeaders();
        //    HttpContext.Current.Response.Buffer = true;
        //    HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //    HttpContext.Current.Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + filename ) ;


        //    using (ExcelPackage pack = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pack.Workbook.Worksheets.Add(filename);
        //        ws.Cells["A1"].LoadFromDataTable(dt, true);
        //        var ms = new System.IO.MemoryStream();
        //        pack.SaveAs(ms);
        //        ms.WriteTo(HttpContext.Current.Response.OutputStream);
        //    }

        //    HttpContext.Current.Response.Flush();
        //    HttpContext.Current.Response.End();

        //}
        //else
        //{
        //    lblMsg.Text = "No Data..So Cannot download Excel";
        //}











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
            grdDetails.Columns[0].Visible = false;
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
            grdDetails.Columns[0].Visible = true;
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

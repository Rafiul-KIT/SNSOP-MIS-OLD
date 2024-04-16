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
using System.IO;
using System.Text;
using System.Drawing;
#region Amendment History Begin
//Form Name : ESMP Reprot Details
//Purpose : To Get  ESMP Reprot Details
// Tabels Used: EnvironmentManagmentPlan,td_IPDetails,tm_States,tm_Counties,tm_Payams,tm_Bomas,tbl_WorkMaster,
//Stored procedures Used : getEnviSearchReportDetailsNew
#endregion Amendment History End

public partial class ESMPDetails : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    DataSet ds = new DataSet();
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

            fillgrid();

        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    #region Search Report and Filling Details Functions
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {

        string User = "";


        DataSet ds = clsGeneral.getEnviSearchReportDetailsNew(txtWorkCode.Text, Request.QueryString[0].ToString(), Request.QueryString[1].ToString(), Request.QueryString[2].ToString(), Request.QueryString[3].ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {

       

            lblMsg1.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            lblMsg1.Visible = true;
        }
        else
        {
            lblMsg1.Visible = true;
            lblMsg1.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();

        }
    }


    protected void BtnClear_Click(object sender, EventArgs e)
    {
        lblMsg1.Visible = false;
        txtWorkCode.Text = "";
       // txtToDate.Text = "";
        grdDetails.DataSource = null;
        grdDetails.DataBind();
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
     
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            HyperLink hlink = (HyperLink)e.Row.FindControl("hypScanLetter0");
            Label bb = (Label)e.Row.FindControl("lblFileName");
            string aa = hlink.Text;
            string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
            string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
            aa = aa.Replace(@"\", @"/");
            aa = aa.Replace(sssnpdir, sssnpweb);
            string url = aa;
            string cc = bb.Text;
            hlink.NavigateUrl = aa + "/" + cc.ToString();
            hlink.Text = cc.ToString();
            bb.Visible = false;
            //  hlink.Text = "Read";
        }
    }

    #endregion



    #region Export Excel and Print Functions

    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        grdDetails.Columns[0].Visible = false;
        ClientScript.RegisterStartupScript(this.GetType(), "myfunction", "PrintPanel()", true);
        grdDetails.Columns[0].Visible = true;
    }

    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Work Proposal LookUp" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdDetails.AllowPaging = false;
            this.fillgrid();
            grdDetails.Columns[0].Visible = false;
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
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    }

    protected void PrintAllPages(object sender, EventArgs e)
    {
        grdDetails.AllowPaging = false;
        fillgrid();
       // grdDetails.Columns[0].Visible = false;
        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);

        grdDetails.RenderControl(hw);

        string gridHTML = sw.ToString().Replace("\"", "'")

            .Replace(System.Environment.NewLine, "");

        StringBuilder sb = new StringBuilder();

        sb.Append("<script type = 'text/javascript'>");

        sb.Append("window.onload = new function(){");

        sb.Append("var printWin = window.open('', '', 'left=0");

        sb.Append(",top=0,width=1000,height=600,status=0');");

        sb.Append("printWin.document.write(\"");

        sb.Append(gridHTML);

        sb.Append("\");");

        sb.Append("printWin.document.close();");

        sb.Append("printWin.focus();");

        sb.Append("printWin.print();");

        sb.Append("printWin.close();};");

        sb.Append("</script>");

        ClientScript.RegisterStartupScript(this.GetType(), "fillgrid", sb.ToString());

        grdDetails.AllowPaging = true;
        fillgrid();

    }

    protected void btnexp_Click(object sender, EventArgs e)
    {


        ExportGridToExcel();
    }

    #endregion


}

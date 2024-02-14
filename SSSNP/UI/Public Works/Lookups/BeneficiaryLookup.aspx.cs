using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class BeneficiaryLookup : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion
    DataSet ds;


    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            grdDetailsExport.Visible = false;
            Gen.getStates(ddlState);
            //fillgrid();
        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {
       
        if (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "TST")
        {
            ds = clsGeneral.getBeneficiaryLookupsearchAll(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(),ddl_group_type.SelectedValue,ddl_group_code.SelectedValue);
        }
        else
        {
            ds = clsGeneral.getBeneficiaryLookupsearch(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);
        }
            
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            BtnPrint.Visible = true;
            btnExportoexcel.Visible = true;
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            BtnPrint.Visible = false;
            btnExportoexcel.Visible = false;
        }
    }

    void fillgrid1()
    {

        if (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "TST")
        {
            ds = clsGeneral.getBeneficiaryLookupsearchAllFullDetails(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, "%", ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);
        }
        else
        {
            ds = clsGeneral.getBeneficiaryLookupsearchAllFullDetails(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);
        }

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetailsExport.Visible = true;
            grdDetailsExport.DataSource = ds.Tables[0];
            grdDetailsExport.DataBind();
            BtnPrint.Visible = true;
            btnExportoexcel.Visible = true;
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetailsExport.DataSource = ds.Tables[0];
            grdDetailsExport.DataBind();
            grdDetailsExport.Visible = false;
            BtnPrint.Visible = false;
            btnExportoexcel.Visible = false;
        }
    }

    protected void btnExportoexcel_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }

    private void ExportGridToExcel()
    {

        Response.Clear();
        Response.Buffer = true;
        string FileName = "Beneficiary Enrollment" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            
            //To Export all pages
            grdDetailsExport.AllowPaging = false;
            this.fillgrid1();
            if (grdDetailsExport.Rows.Count > 0)
            {
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
                        cell.CssClass = "textmode";
                    }
                }
            }
            grdDetailsExport.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

         //grdDetailsExport.AllowPaging = false;
        //fillgrid1();
        //Response.Clear();
        //Response.Buffer = true;
        //Response.ContentType = "application/vnd.ms-excel";
        //Response.AddHeader("content-disposition", "attachment;FileName=BenficiaryDetails.xls");
        //Response.Charset = "";
        //// this.EnableViewState = false;
        //Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        //System.IO.StringWriter sw = new System.IO.StringWriter();
        //System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        //try
        //{
        


        //    grdDetailsExport.RenderControl(htw);

          
        //}
        //catch (Exception eee)
        //{
        //}






     
        //Page.Response.Write(sw.ToString());

      
        //Response.End();

        

    }

    protected void PrintAllPages(object sender, EventArgs e)
    {
        grdDetails.AllowPaging = false;
        fillgrid();
        grdDetails.Columns[0].Visible = false;
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
        grdDetails.Columns[0].Visible = true;
        fillgrid();


    }
    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {

         
        if (ddl_group_type.SelectedValue == "Group")
        {
            //tr_group_code.Style.Add("display", "block");
            tr_group_code.Visible = true;

        }
        if (ddl_group_type.SelectedValue == "CST")
        {
            //tr_group_code.Style.Add("display", "none");
            tr_group_code.Visible = false;
            ddl_group_code.SelectedValue = "";
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
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intBenid") + "')");
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
    
}

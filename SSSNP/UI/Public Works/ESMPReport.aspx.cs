using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
#region Amendment History Begin
//Form Name : ESMP Reprot
//Purpose : To Get  ESMP Reprot
// Tabels Used: EnvironmentManagmentPlan,td_IPDetails,tm_States,tm_Counties,tm_Payams,tm_Bomas,tbl_WorkMaster,
//Stored procedures Used : getESMPReport
#endregion Amendment History End
public partial class UI_Public_Works_GrievanceRegistrationReport : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    DataSet ds = new DataSet();
    #endregion
    int totl = 0;
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            Gen.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
            fillgrid();
          
        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    #region Filling Details
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {

        string User = "";


        DataSet ds = clsGeneral.getESMPReport(txtFromDate.Text, txtToDate.Text,ddlIP.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
        {

            hdFromdate.Value = txtFromDate.Text.ToString();
            hdToDate.Value = txtToDate.Text.ToString();
            
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
        txtFromDate.Text = "";
        txtToDate.Text = "";
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
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            int totla = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "TotalPlans"));
            totl = totl + totla;
            ((HyperLink)e.Row.FindControl("HyperLink1")).NavigateUrl = "ESMPDetails.aspx?id=" + DataBinder.Eval(e.Row.DataItem, "intIP").ToString() + "&bomaid=" + DataBinder.Eval(e.Row.DataItem, "intBomasid").ToString() + "&FromDate=" + txtFromDate.Text + "&ToDate="+ txtToDate.Text;
          //  ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intBomasid") + "')");
        }
        if ((e.Row.RowType == DataControlRowType.Footer))
        {
            e.Row.Cells[5].Text = "Total";
            e.Row.Cells[6].Text = totl.ToString();
        }
    }



    #endregion

    #region Export Excel and Print Functions

    protected void btnexp_Click(object sender, EventArgs e)
    {
        string User = "";
        if (Session["user_type"].ToString() == "TST")
        {
            //User = Session["User_Code"].ToString();
            User = "%";
        }
        else
        {
            User = "%";
        }

        ExportGridToExcel();
    }

    protected void BtnPrint_Click(object sender, EventArgs e)
    {
      //  grdDetails.Columns[0].Visible = false;
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
        fillgrid();

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        ExportGridToExcel();
    }
    #endregion
}
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_Public_Works_GrievanceRegistrationReportDetails : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    DataSet ds = new DataSet();
    string Bomasid ="";
    string FDate = "";
    string TDate = "";
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

          
        }
         Bomasid = HttpContext.Current.Request.QueryString["intBomasid"];
        if(FDate!="")
        {
            FDate = Convert.ToDateTime(HttpContext.Current.Request.QueryString["FromDate"]).ToString("dd-MM-yyyy");
        }
        else
        {
            FDate = "";
        }
       if(TDate!="")
        {
            TDate = Convert.ToDateTime(HttpContext.Current.Request.QueryString["ToDate"]).ToString("dd-MM-yyyy");

        }
        else
        {
            TDate = "";
        }
        fillgrid();
    }


    #endregion

    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

           // ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "Grievance_RegistrationNo"))");
        }
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
      

        ExportGridToExcel();
    }

    void fillgrid()
    {
        //string Bomasid = HttpContext.Current.Request.QueryString["intBomasid"];
        //string FDate = Convert.ToDateTime(HttpContext.Current.Request.QueryString["FromDate"]).ToString("dd-MM-yyyy");
        //string TDate = Convert.ToDateTime(HttpContext.Current.Request.QueryString["ToDate"]).ToString("dd-MM-yyyy");


        DataSet ds = clsGeneral.getGrievanceSearchReportDetails(Bomasid,FDate, TDate);
        //DataSet ds = clsGeneral.getGrievanceSearch_Sp(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtGrievance_RegistrationNo.Text, txtName_Complainant.Text, txtComplainant_Enrolment.Text, txtType_Complaint.Text, txtName_Accused.Text, txtAccused_Enrolment.Text);
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            btnexp.Visible = true;
            BtnPrint.Visible = true;
        }
        else
        {

            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            btnexp.Visible = false;
            BtnPrint.Visible = false;
        }
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
        grdDetails.Columns[0].Visible = false;
        ClientScript.RegisterStartupScript(this.GetType(), "myfunction", "PrintPanel()", true);
        grdDetails.Columns[0].Visible = true;
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    private void ExportGridToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Grievance Registration Report" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdDetails.AllowPaging = false;
            this.fillgrid();
            //grdDetails.Columns[0].Visible = false;
            grdDetails.HeaderRow.BackColor = Color.White;
            //grdDetails.BorderColor = Color.Black;
            foreach (TableCell cell in grdDetails.HeaderRow.Cells)
            {
                cell.BackColor = grdDetails.HeaderStyle.BackColor;
                 
            }
            foreach (GridViewRow row in grdDetails.Rows)
            {
                row.BackColor = Color.White;
                foreach (TableCell cell in row.Cells)
                {
                    cell.Attributes.Add("class", "sborder");
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
            string style = @"<style> .sborder {border : 1px Solid black; }</style>";
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
        //grdDetails.Columns[0].Visible = false;
       
        StringWriter sw = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grdDetails.HeaderStyle.ForeColor = Color.Black;
        foreach (TableCell cell in grdDetails.HeaderRow.Cells)
        {
            cell.Style.Add("border", "1px solid black");
            

        }
        foreach (GridViewRow row in grdDetails.Rows)
        {
             
            foreach (TableCell cell in row.Cells)
            {
                
                cell.Style.Add("border", "1px solid black;");

            }
        }

       
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
        grdDetails.HeaderStyle.ForeColor = Color.White;
        fillgrid();

    }
}
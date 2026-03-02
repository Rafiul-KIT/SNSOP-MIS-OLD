
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


public partial class WorkProposalLookup : System.Web.UI.Page
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
            clsGeneral.getIpDet(ddlIP);
            if (Session["user_type"].ToString() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString()).Value;
                ddlIP.Enabled = false;

                Gen.getProjects(ddlProject, ddlIP.SelectedValue);
                Label404.Visible = true;
                ddlScreeningStatus.Visible = true;
            }
            else if (Session["user_type"].ToString() == "TST")
            {
                clsGeneral.GetIpsByTST(ddlIP, Session["uid"].ToString());

                ddlIP.Enabled = true;
                Label404.Visible = false;
                ddlScreeningStatus.Visible = false;
            }
            //fillgrid();
            ddlScreeningStatus.Items.Add("Accepted");
            ddlScreeningStatus.Items.Add("Rejected");
            Gen.getStates(ddlState);
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

        DataSet ds= new DataSet();

        string screeningStatus = "";
        if (ddlScreeningStatus != null)
        {
            if (ddlScreeningStatus.SelectedValue.ToString() == "Accepted")
            {
                screeningStatus = "1";
            }
            else if (ddlScreeningStatus.SelectedValue.ToString() == "Rejected")
            {
                screeningStatus = "2";
            }
        }

        if (Session["user_type"].ToString() != "IP")
        {
            ds = clsGeneral.getWorkProposalsearchLatest(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, User, ddlstatus.SelectedValue, ddlAreaofWork.SelectedValue, ddlWorkActivity.SelectedValue, ddlProject.SelectedValue);
        }
        else if (Session["user_type"].ToString() == "IP")
        {
            ds = clsGeneral.getWorkProposalsearchLatestIP(Session["uid"].ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, User, ddlstatus.SelectedValue, ddlAreaofWork.SelectedValue, ddlWorkActivity.SelectedValue, ddlProject.SelectedValue, screeningStatus);
        }


        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            btnexp.Visible = true;
            BtnPrint.Visible = true;
            //grdDetails.Style.Add("display", "none");
            grdDetails.Columns[0].Visible = true;
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            btnexp.Visible = false;
            BtnPrint.Visible = false;
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

            ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intWorkid") + "')");
        }
    }
    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {

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

        ddlWorkActivity.Items.Clear();

        if (ddlAreaofWork.SelectedIndex != 0)
        {
            //Gen.getWorkActivitiesByProjects(ddlWorkActivity, ddlAreaofWork.SelectedValue, ddlProject.SelectedValue);
            Gen.getWorkActivities(ddlWorkActivity, ddlAreaofWork.SelectedValue);
        }
        else
        {
            ddlWorkActivity.Items.Insert(0, "--Select--");

        }
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

        getworksbyPojects();
    }

    void getworksbyPojects()
    {

        if (ddlProject.SelectedIndex != 0)
        {
            //Gen.getAreaofWorksByPrjid(ddlAreaofWork, ddlProject.SelectedValue);
            Gen.getAreaOfWork(ddlAreaofWork);
        }
        else
        {
            ddlAreaofWork.Items.Insert(0, "--Select--");

        }
    }


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

        //ExportGridToExcel();
        ExportGridToCSV();
    }

    protected void ExportGridToCSV()
    {
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Work Proposal LookUp" + DateTime.Now.ToString("yyyyMMdd_hh:mm") + ".csv";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/text";
        using (StringWriter sw = new StringWriter())
        {

            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdDetails.AllowPaging = false;
            this.fillgrid();
            DataTable dtGridSource = (DataTable)grdDetails.DataSource;

            StringBuilder strCSV = Gen.CreateCSVString(dtGridSource);

            grdDetails.Columns[0].Visible = false;
            grdDetails.HeaderRow.BackColor = Color.White;

            //style to format numbers to string
            Response.Write(strCSV.ToString());
            Response.Flush();
            Response.End();
        }
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


}

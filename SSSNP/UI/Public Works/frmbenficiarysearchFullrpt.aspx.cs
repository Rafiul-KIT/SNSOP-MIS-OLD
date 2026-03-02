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

public partial class frmbenficiarysearchFullrpt : System.Web.UI.Page
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
            if (Request.QueryString.Count>0)
            {
                if (Request.QueryString[0].ToString() != "")
                { hdnStat.Value = Request.QueryString[0].ToString();
                    fillgrid();
                }
            }
        }
        btnexp.Visible = false;
    }
    #endregion


   
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetBenfsearchDetFullDetails(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if(ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetailsExport.DataSource = ds.Tables[0];
                grdDetailsExport.DataBind();
                btnexp.Visible = false;
                btnexpcsv.Visible = true;
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetailsExport.DataSource = ds.Tables[0];
                grdDetailsExport.DataBind();
                btnexp.Visible = false;
                btnexpcsv.Visible = false;
            }
        }
      


    }
    void fillgridExport()
    {
        DataSet ds1 = new DataSet();
        ds1 = clsGeneral.GetBenfsearchDetFullDetails(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if (ds1.Tables.Count > 0)
        {
            if (ds1.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds1.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetailsExport.DataSource = ds1.Tables[0];
                grdDetailsExport.Columns[0].Visible = false;
            //    grdDetailsExport.Columns[17].Visible = false;
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
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("ViewBenfWages.aspx?q=" + id);
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetailsExport.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

           
            //HyperLink h1 = (HyperLink)e.Row.Cells[9].Controls[0];
            //h1.Target = "_blank";
            //h1.NavigateUrl = "frmBenViewDetails1.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();
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
        //btnexp.Visible = true;
    }
    
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmbenficiarysearchFullrpt.aspx");
    }
    protected void ExportToExcel()
    {
        //GridView Export = new GridView();
        
        Response.Clear();
        Response.Buffer = true;
        string FileName = "Benfeciary Full Details" + DateTime.Now + ".xls";
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
            grdDetailsExport.Columns[0].Visible = true;
        //    grdDetailsExport.Columns[17].Visible = true;
            
            
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
        
        this.fillgridExport();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BeneficiaryFullDetails.csv");
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
        grdDetailsExport.Columns[0].Visible = true;
       // grdDetailsExport.Columns[17].Visible = true;
        
    }

    protected void grdDetailsExport_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetailsExport.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void grdDetailsExport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            //HyperLink h1 = (HyperLink)e.Row.Cells[24].Controls[0];
            //h1.Target = "_blank";
            //h1.NavigateUrl = "frmBenPhotoDetails.aspx?ID=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenid")).Trim();
        }
    }
}

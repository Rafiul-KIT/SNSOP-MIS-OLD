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
using System.Collections.Generic;
using System.Drawing;
using System.IO;
public partial class rptBomanCycleWise : System.Web.UI.Page
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
            fillgrid();
        }
    }
    #endregion
    void fillgrid()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetrptBomanCycleWiseReport(Request.QueryString[0].ToString(), Request.QueryString[1].ToString());
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind(); 
                decimal Discount = ds.Tables[0].AsEnumerable().Sum(row => row.Field<decimal>("TotalAmount"));
                decimal Paid = ds.Tables[0].AsEnumerable().Sum(row => row.Field<int>("TotalBeneficiries")); 


                grdDetails.FooterRow.Cells[1].Text = "Total";
                grdDetails.FooterRow.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                grdDetails.FooterRow.Cells[3].Text = Discount.ToString();
                grdDetails.FooterRow.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                grdDetails.FooterRow.Cells[4].Text = Paid.ToString();
                grdDetails.FooterRow.Cells[4].HorizontalAlign = HorizontalAlign.Right;
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
        }
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
            HyperLink hyp = (HyperLink)e.Row.Cells[5].Controls[0];
            hyp.Target = "_blank";
            if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBomasid")).Trim() != "")
            {
                hyp.NavigateUrl = "rptBomaCycleFullDetails.aspx?IP=" + Request.QueryString[0].ToString() + "&Boma=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBomasid")).Trim() + "&D=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Fromdates")).Trim();
                hyp.Enabled = true;
                hyp.Text = "View";
            }
            else
            {
                hyp.Text = "";

                hyp.Enabled = false;

            }
        }
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    void getcounties()
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void BtnSave_Click1(object sender, EventArgs e)
    {
        Button BtnSave = (Button)sender;
        GridViewRow row = (GridViewRow)BtnSave.NamingContainer;
        HiddenField hdfApplID = (HiddenField)row.FindControl("hdfid");
        DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus");
        TextBox txtPromotor = (TextBox)row.FindControl("txtPromotor");
        //    HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hdfid");
        int a = Gen.InsApprovalStatus(hdfApplID.Value, ddlStatus.SelectedValue, Session["uid"].ToString().Trim(), txtPromotor.Text);
        if (a >= 0)
        {
            lblresult.Text = "Submitted Successfully";
            fillgrid();
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
    public void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Boma and Cycle wise Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //To Export all pages
            grdDetails.AllowPaging = false;
            this.fillgrid();
            grdDetails.Columns[5].Visible=false;
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
                    List<Control> controls = new List<Control>();
                    foreach (Control control in cell.Controls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "HyperLink":
                                controls.Add(control);
                                break;
                            case "TextBox":
                                controls.Add(control);
                                break;
                            case "LinkButton":
                                controls.Add(control);
                                break;
                            case "CheckBox":
                                controls.Add(control);
                                break;
                            case "RadioButton":
                                controls.Add(control);
                                break;
                        }
                    }
                    foreach (Control control in controls)
                    {
                        switch (control.GetType().Name)
                        {
                            case "HyperLink":
                                cell.Controls.Add(new Literal { Text = (control as HyperLink).Text });
                                break;
                            case "TextBox":
                                cell.Controls.Add(new Literal { Text = (control as TextBox).Text });
                                break;
                            case "LinkButton":
                                cell.Controls.Add(new Literal { Text = (control as LinkButton).Text });
                                break;
                            case "CheckBox":
                                cell.Controls.Add(new Literal { Text = (control as CheckBox).Text });
                                break;
                            case "RadioButton":
                                cell.Controls.Add(new Literal { Text = (control as RadioButton).Text });
                                break;
                        }
                        cell.Controls.Remove(control);
                    }
                }
            }

            grdDetails.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            Response.Write(style);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
}

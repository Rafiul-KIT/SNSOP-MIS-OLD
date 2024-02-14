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

public partial class frmNophotosList : System.Web.UI.Page
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
                    //fillgrid();
                }
            }
        }
    }
    #endregion


   
    void fillgrid1()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetNoPhotosList(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if(ds.Tables.Count > 0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                grdDetails.Visible = true;
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
                btnexp.Visible = true;
                GridView1.Visible = false;
                GridView2.Visible = false;
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                btnexp.Visible = false;
            }
        }

    }

    void fillgrid2()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetNoPhotosList(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[1].Rows.Count > 0)
            {
                GridView1.Visible = true;
                lblMsg1.Text = ds.Tables[1].Rows.Count.ToString() + " Records found.";
                GridView1.DataSource = ds.Tables[1];
                GridView1.DataBind();
                btnexp.Visible = true;
                GridView2.Visible = false;
                grdDetails.Visible = false;
            }
            else
            {
                lblMsg1.Text = "No Records found.";
                grdDetails.DataSource = null;
                grdDetails.DataBind();
                btnexp.Visible = false;
            }
        }

    }

    void fillgrid3()
    {
        DataSet ds = new DataSet();
        ds = clsGeneral.GetNoPhotosList(ddlIP.SelectedValue, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txthousehold.Text, txtbenf.Text, txtenrollment.Text);
        if (ds.Tables.Count > 0)
        {
            if (ds.Tables[2].Rows.Count > 0)
            {
                GridView2.Visible = true;
                lblMsg2.Text = ds.Tables[2].Rows.Count.ToString() + " Records found.";
                GridView2.DataSource = ds.Tables[2];
                GridView2.DataBind();
                btnexp.Visible = true;
                GridView1.Visible = false;
                grdDetails.Visible = false;
            }
            else
            {
                lblMsg2.Text = "No Records found.";
                GridView2.DataSource = null;
                GridView2.DataBind();
                btnexp.Visible = false;
            }
        }

    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid1();
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid2();
    }
    protected void GridView2_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView2.PageIndex = e.NewPageIndex;
        fillgrid3();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            
        }
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
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

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (ddllist.SelectedIndex == 1)
        {
            fillgrid1();
            lblresult.Text = "";
            lblMsg.Visible = true;
            lblMsg1.Visible = false;
            lblMsg2.Visible = false;
        }
        else if (ddllist.SelectedIndex == 2)
        {
            fillgrid2();
            lblresult.Text = "";
            lblMsg1.Visible = true;
            lblMsg.Visible = false;
            lblMsg2.Visible = false;
        }
        else if (ddllist.SelectedIndex == 3)
        {
            fillgrid3();
            lblresult.Text = "";
            lblMsg2.Visible = true;
            lblMsg1.Visible = false;
            lblMsg.Visible = false;
        }
        else
        {
            lblresult.Text = "Please Select Benfeciary List";
        }
        //btnexp.Visible = true;
    }
    
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmNophotosList.aspx");
    }
    protected void ExportToExcel()
    {
        //GridView Export = new GridView();

        Response.Clear();
        Response.Buffer = true;
        string FileName = "Photos Not in List Report" + DateTime.Now + ".xls";
        Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            if (ddllist.SelectedIndex == 1)
            {
                grdDetails.GridLines = GridLines.Both;
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                grdDetails.AllowPaging = false;
                this.fillgrid1();
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
            if (ddllist.SelectedIndex == 2)
            {
                GridView1.GridLines = GridLines.Both;
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView1.AllowPaging = false;
                this.fillgrid2();
                //  grdExtentDetails.Columns[0].Visible = false;
                GridView1.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView1.HeaderRow.Cells)
                {
                    cell.BackColor = GridView1.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView1.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView1.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView1.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView1.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style>.textmode{mso-number-format:\@;}</style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            if (ddllist.SelectedIndex == 3)
            {
                GridView2.GridLines = GridLines.Both;
                HtmlTextWriter hw = new HtmlTextWriter(sw);

                //To Export all pages
                GridView2.AllowPaging = false;
                this.fillgrid3();
                //  grdExtentDetails.Columns[0].Visible = false;
                GridView2.HeaderRow.BackColor = Color.White;
                foreach (TableCell cell in GridView2.HeaderRow.Cells)
                {
                    cell.BackColor = GridView2.HeaderStyle.BackColor;
                }
                foreach (GridViewRow row in GridView2.Rows)
                {
                    row.BackColor = Color.White;
                    foreach (TableCell cell in row.Cells)
                    {
                        if (row.RowIndex % 2 == 0)
                        {
                            cell.BackColor = GridView2.AlternatingRowStyle.BackColor;
                        }
                        else
                        {
                            cell.BackColor = GridView2.RowStyle.BackColor;
                        }
                        cell.CssClass = "textmode";
                    }
                }

                GridView2.RenderControl(hw);

                //style to format numbers to string
                string style = @"<style>.textmode{mso-number-format:\@;}</style>";
                Response.Write(style);
                Response.Output.Write(sw.ToString());
                Response.Flush();
                Response.End();
            }
            
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
    //protected void ddllist_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddllist.SelectedIndex == 1)
    //    {
    //        fillgrid1();
    //    }
    //    if (ddllist.SelectedIndex == 2)
    //    {
    //        fillgrid2();
    //    }
    //    if (ddllist.SelectedIndex == 3)
    //    {
    //        fillgrid3();
    //    }
    //}
}

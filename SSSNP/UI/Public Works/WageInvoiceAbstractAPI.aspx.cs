using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
// Developer : Srikanth
// Date : 07-08-2017
// Purpose: Attendance Report Time out expired

public partial class WageInvoiceAbstractAPI : System.Web.UI.Page
{
    //designing and developed by siva as on 27-02-2016
    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int cnt = -1;
    decimal paidamount, AttendanceCount, paidamounta;
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count == 0)
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }
        HttpContext.Current.Server.ScriptTimeout = 1000;

        this.BtnSave3.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSave3, "group"));
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString().Trim()).Value;
                ddlIP.Enabled = false;
            }
        }
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {
    }

    void clear()
    {
        grdClosebatchdet.DataSource = null;
        grdClosebatchdet.DataBind();
        tblreport.Visible = false;
    }

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
        }
    }

    protected void BtnSave3_Click(object sender, EventArgs e)
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ds = Gen.getWageInvoiceAbstractReqNo(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, "All");
            }
            else
            {
                ds = Gen.getWageInvoiceAbstractReqNo(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, "All");
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                    if (Session["user_type"].ToString().Trim() == "IP")
                    {
                        LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString();
                    }
                    else
                    {
                        LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString() + "  | Total Paid Amount in SSP : " + ds.Tables[1].Rows[0][3].ToString() + "  | Exchange Rate : " + ds.Tables[1].Rows[0][4].ToString();
                    }
                    lblmessage.Text = "";
                }
                else
                {
                    if (Session["user_type"].ToString().Trim() == "IP")
                    {
                        LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                    }
                    else
                    {
                        LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                    }

                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                }
            }
            else
            {
                if (Session["user_type"].ToString().Trim() == "IP")
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                }
                else
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0" + "  | Total Paid Amount in SSP : 0";
                }
                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();
            }
        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
            lblmessage.Text = "There is no attendance/wages ";
        }
        finally
        {
        }
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("WageInvoiceAbstractAPI.aspx");
    }

    public string DisableTheButton(Control pge, Control btn, string vldGroup)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
        if (vldGroup.Trim() == "")
        {
            sb.Append("if (Page_ClientValidate() == false) { return false; }} ");
        }
        else
        {
            sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");
        }
        sb.Append("this.value = 'Please wait...';");
        sb.Append("this.disabled = true;");
        sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }

    protected void grdClosebatchdet_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }

    public void fillgrid()
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();
            if (Session["user_type"].ToString().Trim() == "IP")
            {
                ds = Gen.getWageInvoiceAbstractReqNo(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, "All");
            }
            else
            {
                ds = Gen.getWageInvoiceAbstractReqNo(hdfID.Value.ToString(), "", "", ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, "All");
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                    LblResultA.Text = "Total Members: " + ds.Tables[1].Rows[0][0].ToString() + "  | Total Work Days : " + ds.Tables[1].Rows[0][1].ToString() + "  | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][2].ToString();
                    lblmessage.Text = "";
                }
                else
                {
                    LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                }
            }
            else
            {
                LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();
            }
        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            LblResultA.Text = "Total Members : 0" + "  | Total Work Days : 0" + "  | Total Paid Amount in USD : 0";
            lblmessage.Text = "There is no attendance/wages ";
        }
        finally
        {
        }
    }

    protected void grdClosebatchdet_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void grdClosebatchdet_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdClosebatchdet.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void ExportToExcel()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;FileName=ViewFiles.xls");
        Response.Charset = "";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);
        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {
            tblreport.Visible = true;
            tblreport.RenderControl(htw);
        }
        catch (Exception eee)
        {
        }
        Page.Response.Write(sw.ToString());

    }

    protected void ExportToCSV()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/text";
        Response.AddHeader("content-disposition", "attachment;FileName=WagesRequestAbstract.csv");
        Response.Charset = "";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringBuilder sbGrid = new StringBuilder();

        try
        {
            //It does not includes 1st 2 clumns (they are empty in report)    
            for (int k = 2; k < grdClosebatchdet.Columns.Count; k++)
            {
                //add separator
                sbGrid.Append(grdClosebatchdet.Columns[k].HeaderText + ',');
            }
            //append new line
            sbGrid.Append("\r\n");
            for (int i = 0; i < grdClosebatchdet.Rows.Count; i++)
            {
                //It does not includes 1st 2 clumns (they are empty in report)
                for (int k = 2; k < grdClosebatchdet.Columns.Count; k++)
                {
                    //add separator
                    sbGrid.Append(grdClosebatchdet.Rows[i].Cells[k].Text + ',');
                }
                //append new line
                sbGrid.Append("\r\n");
            }
        }
        catch (Exception eee)
        {
        }
        Response.Write(sbGrid.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void btnexp_Click(object sender, EventArgs e)
    {
        ExportToCSV();
    }

    protected void btnexpZipped_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;FileName=WagesRequestAbstract.zip");
        Response.Charset = "";
        Response.ContentType = "application/octet-stream";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringBuilder sbGrid = new StringBuilder();

        //It does not includes 1st 2 clumns (they are empty in report)    
        for (int k = 2; k < grdClosebatchdet.Columns.Count; k++)
        {
            //add separator
            sbGrid.Append(grdClosebatchdet.Columns[k].HeaderText + ',');
        }
        //append new line
        sbGrid.Append("\r\n");
        for (int i = 0; i < grdClosebatchdet.Rows.Count; i++)
        {
            //It does not includes 1st 2 clumns (they are empty in report)
            for (int k = 2; k < grdClosebatchdet.Columns.Count; k++)
            {
                //add separator
                sbGrid.Append(grdClosebatchdet.Rows[i].Cells[k].Text + ',');
            }
            //append new line
            sbGrid.Append("\r\n");
        }

        byte[] byteArray = Gen.zipString(sbGrid.ToString(), "WagesRequestAbstract.csv");
        Response.AppendHeader("Content-Length", byteArray.Length.ToString());
        Response.BinaryWrite(byteArray);
        //Response.Write(byteArray);
        //Response.Write(sbGrid.ToString());
        Response.Flush();
        Response.End();
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

    protected void grdClosebatchdet_PreRender(object sender, EventArgs e)
    {
        //Hide SSP and Exchange rate columns to IP
        if (Session["user_type"].ToString().Trim() == "IP")
        {
            int iTotColumns = 0;
            iTotColumns = grdClosebatchdet.Columns.Count;
            for (int iCount = 0; iCount < iTotColumns; iCount++)
            {
                if (grdClosebatchdet.Columns[iCount].HeaderText.ToLower().Trim() == "Exchange Rate".ToLower())
                {
                    grdClosebatchdet.Columns[iCount].Visible = false;
                }
                else if (grdClosebatchdet.Columns[iCount].HeaderText.ToLower().Trim() == "Amount Paid in SSP".ToLower())
                {
                    grdClosebatchdet.Columns[iCount].Visible = false;
                }
                else
                {
                    grdClosebatchdet.Columns[iCount].Visible = true;
                }
            }
        }
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

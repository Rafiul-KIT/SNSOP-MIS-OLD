using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Diagnostics;

public partial class WageInvoiceAbstractAPIPaid : System.Web.UI.Page
{
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


        this.BtnSearch.Attributes.Add("onclick", DisableTheButton(this.Page, this.BtnSearch, "group"));
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
            getPaymentCycle();
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

    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        LblResultA.Text = "";
        LblResultA0.Text = "";
        LblResultA.Text = "";

        try
        {
            DataSet ds = new DataSet();
            if (Session["user_type"].ToString().Trim() == "IP")
            {

                ds = Gen.getWageInvoiceAbstractReqNoPaidNewLatest(hdfID.Value.ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, TxtFromDate.Text, TxtToDate.Text, ddlPaymentCycle.SelectedItem.Value, "All");
            }
            else
            {
                ds = Gen.getWageInvoiceAbstractReqNoPaidNewLatest(hdfID.Value.ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, TxtFromDate.Text, TxtToDate.Text, ddlPaymentCycle.SelectedItem.Value, "All");
            }
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                    LblResultA.Text = "Total Payments Requested: " + ds.Tables[1].Rows[0][0].ToString() + " | Total Payments Done: " + ds.Tables[1].Rows[0][3].ToString() + " | Total Payments not Done: " + ds.Tables[1].Rows[0][5].ToString()
                        + " <br/> Total Beneficiaries Paid: " + ds.Tables[2].Rows[0][0].ToString() + " | Total Beneficiaries Unpaid: " + ds.Tables[2].Rows[0][1].ToString()
                        + "  <br/> Total Amount in USD : " + ds.Tables[1].Rows[0][2].ToString() + " | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][4].ToString() + " | Total Paid Amount in SSP : " + ds.Tables[1].Rows[0][7].ToString() + " | Total Unpaid Amount in USD : " + ds.Tables[1].Rows[0][6].ToString();
                    lblmessage.Text = "";
                }
                else
                {
                    LblResultA.Text = "Total Applied Members: " + "0" + "Total Members Paid: " + "0" + "Total Members Unpaid: " + "0" + "  | Total Amount in USD : " + "0" + " | Total Paid Amount in USD : " + "0" + " | Total Paid Amount in SSP : " + "0" + " | Total Unpaid Amount in USD : " + "0";
                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                }
            }
            else
            {
                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();
                LblResultA.Text = "Total Applied Members: " + "0" + "Total Members Paid: " + "0" + "Total Members Unpaid: " + "0" + "  | Total Amount in USD : " + "0" + " | Total Paid Amount in USD : " + "0" + " | Total Paid Amount in SSP : " + "0" + " | Total Unpaid Amount in USD : " + "0";
            }
        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            lblmessage.Text = "There is no attendance/wages ";
            LblResultA.Text = "Total Applied Members: " + "0" + "Total Members Paid: " + "0" + "Total Members Unpaid: " + "0" + "  | Total Amount in USD : " + "0" + " | Total Paid Amount in USD : " + "0" + " | Total Paid Amount in SSP : " + "0" + " | Total Unpaid Amount in USD : " + "0";
        }
        finally
        {
        }
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        Response.Redirect("WageInvoiceAbstractAPIPaid.aspx");
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
                ds = Gen.getWageInvoiceAbstractReqNoPaidNewLatest(hdfID.Value.ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, TxtFromDate.Text, TxtToDate.Text, ddlPaymentCycle.SelectedItem.Value, "All");
            }
            else
            {
                ds = Gen.getWageInvoiceAbstractReqNoPaidNewLatest(hdfID.Value.ToString(), ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, TxtRequestNo.Text, TxtFromDate.Text, TxtToDate.Text, ddlPaymentCycle.SelectedItem.Value, "All");
            }

            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    tblreport.Visible = true;
                    ViewState["dt"] = ds;
                    grdClosebatchdet.DataSource = ds.Tables[0];
                    grdClosebatchdet.DataBind();
                    //LblResultA.Text = "Total Applied Members: " + ds.Tables[1].Rows[0][0].ToString() + " | Total Members Paid: " + ds.Tables[1].Rows[0][3].ToString() + " | Total Members Unpaid: " + ds.Tables[1].Rows[0][5].ToString() + " <br/> Total Amount in USD : " + ds.Tables[1].Rows[0][2].ToString() + " | Total Paid Amount in USD : " + ds.Tables[1].Rows[0][4].ToString() + " | Total Unpaid Amount in USD : " + ds.Tables[1].Rows[0][6].ToString();
                    LblResultA.Text = "Total Payments Requested: " + ds.Tables[1].Rows[0][0].ToString() + " | Total Payments Done: " + ds.Tables[1].Rows[0][3].ToString() + " | Total Payments not Done: " + ds.Tables[1].Rows[0][5].ToString()
                        + " <br/> Total Beneficiaries Paid: " + ds.Tables[2].Rows[0][0].ToString() + " | Total Beneficiaries Unpaid: " + ds.Tables[2].Rows[0][1].ToString()
                        + "  <br/> Total Amount in USD : " + ds.Tables[1].Rows[0][2].ToString() + " | Total Paid Amount in USD : " + "60000" + " | Total Paid Amount in SSP : " + ds.Tables[1].Rows[0][7].ToString() + " | Total Unpaid Amount in USD : " + ds.Tables[1].Rows[0][6].ToString();
                    lblmessage.Text = "";
                }
                else
                {
                    LblResultA.Text = "Total Applied Members: " + "0" + "Total Members Paid: " + "0" + "Total Members Unpaid: " + "0" + "  | Total Work Days : " + "0" + "  | Total Amount in USD : " + "0" + " | Total Paid Amount in USD : " + "0" + " | Total Paid Amount in SSP : " + "0" + " | Total Unpaid Amount in USD : " + "0";
                    grdClosebatchdet.DataSource = null;
                    grdClosebatchdet.DataBind();
                }
            }
            else
            {
                grdClosebatchdet.DataSource = null;
                grdClosebatchdet.DataBind();
                LblResultA.Text = "Total Applied Members: " + "0" + "Total Members Paid: " + "0" + "Total Members Unpaid: " + "0" + "  | Total Work Days : " + "0" + "  | Total Amount in USD : " + "0" + " | Total Paid Amount in USD : " + "0" + " | Total Paid Amount in SSP : " + "0" + " | Total Unpaid Amount in USD : " + "0";
            }
        }
        catch (Exception ex)
        {
            grdClosebatchdet.DataSource = null;
            grdClosebatchdet.DataBind();
            lblmessage.Text = "There is no attendance/wages ";
            LblResultA.Text = "Total Applied Members: " + "0" + "Total Members Paid: " + "0" + "Total Members Unpaid: " + "0" + "  | Total Amount in USD : " + "0" + " | Total Paid Amount in USD : " + "0" + " | Total Paid Amount in SSP : " + "0" + " | Total Unpaid Amount in USD : " + "0";
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
        Response.AddHeader("content-disposition", "attachment;FileName=PaidWagesRequestAbstract.zip");
        Response.Charset = "";
        Response.ContentType = "application/octet-stream";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringBuilder sbGrid = new StringBuilder();

        //It does not includes 1st 2 clumns (they are empty in report)    
        for (int k = 3; k < grdClosebatchdet.Columns.Count; k++)
        {
            //add separator
            sbGrid.Append(grdClosebatchdet.Columns[k].HeaderText + ',');
        }
        //append new line
        sbGrid.Append("\r\n");
        for (int i = 0; i < grdClosebatchdet.Rows.Count; i++)
        {
            //It does not includes 1st 2 clumns (they are empty in report)
            for (int k = 3; k < grdClosebatchdet.Columns.Count; k++)
            {
                //add separator
                sbGrid.Append(grdClosebatchdet.Rows[i].Cells[k].Text + ',');
            }
            //append new line
            sbGrid.Append("\r\n");
        }

        byte[] byteArray = Gen.zipString(sbGrid.ToString(), "PaidWagesRequestAbstract.csv");
        Response.AppendHeader("Content-Length", byteArray.Length.ToString());
        Response.BinaryWrite(byteArray);
        //Response.Write(byteArray);
        //Response.Write(sbGrid.ToString());
        Response.Flush();
        Response.End();
    }

    protected void ExportToCSV()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/text";
        Response.AddHeader("content-disposition", "attachment;FileName=PaidWagesRequestAbstract.csv");
        Response.Charset = "";
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        StringBuilder sbGrid = new StringBuilder();

        try
        {
            for (int k = 3; k < grdClosebatchdet.Columns.Count; k++)
            {
                //add separator
                sbGrid.Append(grdClosebatchdet.Columns[k].HeaderText + ',');
            }
            //append new line
            sbGrid.Append("\r\n");
            for (int i = 0; i < grdClosebatchdet.Rows.Count; i++)
            {
                //It does not includes 1st 3 columns (they are empty in report)
                for (int k = 3; k < grdClosebatchdet.Columns.Count; k++)
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

    void getPaymentCycle()
    {
        DataSet dsPaymentCycle = Gen.getPaymentCycle();
        ddlPaymentCycle.Items.Clear();
        ddlPaymentCycle.DataSource = dsPaymentCycle.Tables[0];
        ddlPaymentCycle.DataTextField = "WagePaymentCycle";
        ddlPaymentCycle.DataValueField = "intPaymentCycleID";
        ddlPaymentCycle.DataBind();
        ddlPaymentCycle.Items.Insert(0, "--Select--");
    }
}

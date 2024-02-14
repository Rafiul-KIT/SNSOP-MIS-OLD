using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using System.IO;
using System.IO;

public partial class APIDashboardDrillDown : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion
    decimal NoofPayams1, NoofBomas1, TotalWorkProposed1, TotaltobeApproved1, TotalInprogress1, TotalClosed1, TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1, Type11, Type21, Type31, Type41, Type51, Type61, TotalWorkProposed12;
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            //  clsGeneral.getIpDet(ddlIP);

            Gen.getStates(ddlState);
            if (Request.QueryString.Count > 0)
            {
                string strStatus = Request.QueryString[0].ToString();
                if (strStatus != "")
                {
                    hdnStat.Value = strStatus;
                    fillgrid();
                    switch (strStatus)
                    {
                        case "0":
                            lblPageTitle.InnerText = "Total Beneficiaries";
                            btnExportCSV.Text = "Export all Beneficiaries";
                            btnExportZippedCSV.Text = "Export all Beneficiaries (compressed)";
                            break;
                        case "1":
                            lblPageTitle.InnerText = "Pending Beneficiaries";
                            btnExportCSV.Text = "Export all Pending Beneficiaries";
                            btnExportZippedCSV.Text = "Export all Pending Beneficiaries (compressed)";
                            break;
                        case "2":
                            lblPageTitle.InnerText = "Confirmed Beneficiaries";
                            btnExportCSV.Text = "Export all Confirmed Beneficiaries";
                            btnExportZippedCSV.Text = "Export all Confirmed Beneficiaries (compressed)";
                            break;
                        case "3":
                            lblPageTitle.InnerText = "Dropped Beneficiaries";
                            btnExportCSV.Text = "Export all Dropped Beneficiaries";
                            btnExportZippedCSV.Text = "Export all Dropped Beneficiaries (compressed)";
                            break;
                        default:
                            lblPageTitle.InnerText = "API Dashboard Report";
                            break;
                    }
                }
            }
        }
    }
    #endregion


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        fillgrid();
    }
    void fillgrid()
    {
        Button1.Visible = false;
        Button2.Visible = false;
        DataSet ds = Gen.DrillDownAPI(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        //DataSet dsExport = Gen.DrillDownAPIExport(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);

        DataTable dt = ds.Tables[0];
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();

        if (Request.QueryString[0].ToString() == "0") //Total
        {
            RadGrid1.Columns.FindByUniqueName("checkColumn").Visible = false;
            RadGrid1.Columns.FindByUniqueName("UpdateColumn").Visible = false;
            RadGrid1.Columns.FindByUniqueName("operationCol").Visible = false;
            RadGrid1.Columns.FindByUniqueName("SelectionCriteria").Visible = true;
            RadGrid1.Columns.FindByUniqueName("ComponentDDL").Visible = false;
        }
        else if (Request.QueryString[0].ToString() == "1") //Pending
        {
            Button1.Visible = true;
            string strStatus = Request.QueryString[0].ToString();
            if (strStatus == "1")
            {
                RadGrid1.Columns.FindByUniqueName("SelectionCriteria").Visible = true;
                RadGrid1.Columns.FindByUniqueName("ComponentDDL").Visible = true;
            }
        }
        else if (Request.QueryString[0].ToString() == "2") //Enrolled
        {
            RadGrid1.Columns.FindByUniqueName("checkColumn").Visible = false;
            RadGrid1.Columns.FindByUniqueName("UpdateColumn").Visible = false;
            RadGrid1.Columns.FindByUniqueName("operationCol").Visible = false;
            RadGrid1.Columns.FindByUniqueName("SelectionCriteria").Visible = true;
            RadGrid1.Columns.FindByUniqueName("ComponentDDL").Visible = false;
        }
        else //if (Request.QueryString[0].ToString() == "3") //Fropped
        {
            RadGrid1.Columns.FindByUniqueName("checkColumn").Visible = false;
            RadGrid1.Columns.FindByUniqueName("UpdateColumn").Visible = false;
            RadGrid1.Columns.FindByUniqueName("operationCol").Visible = false;
            RadGrid1.Columns.FindByUniqueName("SelectionCriteria").Visible = true;
            RadGrid1.Columns.FindByUniqueName("ComponentDDL").Visible = false;
        }
        //if (ds.Tables[0].Rows.Count > 0)
        //{
        //    lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
        //    grdDetails.DataSource = dsExport.Tables[0];
        //    grdDetails.DataBind();
        //    lblMsg.Visible = false;
        //    lblMsg.Text = "";

        //    if (Request.QueryString[0].ToString() == "1")
        //    {
        //        Button1.Visible = true;
        //        Button2.Visible = true;
        //        //grdDetails.Columns[1].Visible = true;
        //    }
        //    else
        //    {
        //        //grdDetails.Columns[1].Visible = false;
        //        Button1.Visible = false;
        //        Button2.Visible = false;
        //    }
        //}
        //else
        //{
        //    lblMsg.Visible = true;
        //    lblMsg.Text = "No Records found.";
        //    grdDetails.DataSource = null;
        //    grdDetails.DataBind();
        //}

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        CleanAlertMessages();
    }
    //protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    grdDetails.PageIndex = e.NewPageIndex;
    //    fillgrid();
    //}
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (Request.QueryString[0].ToString() == "1")
        //{
        //    //  HiddenField hdfDeptID = (HiddenField)e.Row.FindControl("hlRedirectAAB");

        //    if ((e.Row.RowType == DataControlRowType.DataRow))
        //    {
        //        HiddenField hdfGroundedNo1 = (HiddenField)e.Row.Cells[0].FindControl("hdfid");
        //        hdfGroundedNo1.Value = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenTempid")).Trim();
        //        e.Row.Cells[9].Visible = true;
        //        e.Row.Cells[10].Visible = true;

        //        HyperLink h1 = (HyperLink)e.Row.Cells[11].Controls[0];
        //        h1.Text = "Update";
        //        h1.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenTempid")).Trim();

        //        // HyperLink h2 = (HyperLink)e.Row.Cells[9].Controls[0];
        //        //  h2.Text = "Delete";
        //        // h1.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBenTempid")).Trim();
        //    }
        //}
        //else
        //{

        //    if ((e.Row.RowType == DataControlRowType.DataRow))
        //    {
        //        e.Row.Cells[9].Visible = false;
        //        e.Row.Cells[10].Visible = false;
        //    }
        //}

    }


    protected void grdDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    //protected void grdDetails_Sorting(object sender, GridViewSortEventArgs e)
    //{
    //    DataSet ds = Gen.DrillDownAPI(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
    //    DataTable dt = ds.Tables[0];

    //    if (dt.Rows.Count > 0)
    //    {
    //        if (Convert.ToString(ViewState["sortDirection"]) == "Asc")
    //        {
    //            dt.DefaultView.Sort = e.SortExpression + " Desc";
    //            ViewState["sortDirection"] = "Desc";
    //        }
    //        else
    //        {
    //            dt.DefaultView.Sort = e.SortExpression + " Asc";
    //            ViewState["sortDirection"] = "Asc";
    //        }
    //        grdDetails.DataSource = dt;
    //        grdDetails.DataBind();
    //    }
    //}

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = Gen.DrillDownAPI(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        DataTable dt = ds.Tables[0];

        RadGrid1.DataSource = dt;
    }
    protected void RadGrid1_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadGrid1.MasterTableView.PageSize * RadGrid1.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            HyperLink hyperLinkView = e.Item.FindControl("lnkUpdate") as HyperLink;
            //hyperLinkView.Target = "_blank";
            hyperLinkView.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenTempid"];


            string strStatus = Request.QueryString[0].ToString();
            if (strStatus != "" && strStatus == "1")
            {
                DropDownList ddlComponent = e.Item.FindControl("ddlRgComponent") as DropDownList;
                string strComponent = ((System.Data.DataRowView)e.Item.DataItem).Row["SelectionCriteria"].ToString();
                if (strComponent.Contains("1"))
                {
                    ddlComponent.SelectedValue = "Component 1 (Public Works)";
                }
                else if (strComponent.Contains("2"))
                {
                    ddlComponent.SelectedValue = "Component 2 (Direct Income Support)";
                }
                else
                {
                    ddlComponent.SelectedValue = "Select";
                }
            }
        }

        if (e.Item is GridPagerItem)
        {
            int iTotalRows = ((DataTable)RadGrid1.DataSource).Rows.Count;
            //int iTotalRows = ((System.Data.DataTable)rgBeneficiariesDIS.DataSource).Rows.Count;
            RadComboBox PageSizeCombo = (RadComboBox)e.Item.FindControl("PageSizeComboBox");
            PageSizeCombo.Items.Clear();
            //if (iTotalRows > 250)
            //{
            PageSizeCombo.Items.Add(new RadComboBoxItem("250", "250"));
            PageSizeCombo.FindItemByText("250").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
            //}
            if (iTotalRows > 500)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("500", "500"));
                PageSizeCombo.FindItemByText("500").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
            }

            if (iTotalRows > 1000)
            {
                PageSizeCombo.Items.Add(new RadComboBoxItem("1000", "1000"));
                PageSizeCombo.FindItemByText("1000").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
            }

            PageSizeCombo.Items.Add(new RadComboBoxItem("All", iTotalRows.ToString()));
            PageSizeCombo.FindItemByText("All").Attributes.Add("ownerTableViewId", RadGrid1.MasterTableView.ClientID);
            //if (!IsPostBack)
            if (e.Item.OwnerTableView.PageSize.ToString() == iTotalRows.ToString())
            {
                PageSizeCombo.FindItemByText("All").Selected = true;
            }
            else
            {
                PageSizeCombo.FindItemByText(e.Item.OwnerTableView.PageSize.ToString()).Selected = true;
            }
        }
    }

    protected void btnExportCSV_Click(object sender, EventArgs e)
    {
        //grdDetails.Visible = true;
        //this.fillgridExport();

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BeneficiariesDetails.csv");
        Response.Charset = "";
        Response.ContentType = "application/text";

        //StringBuilder columnbind = new StringBuilder();

        //for (int k = 0; k < grdDetails.Columns.Count; k++)
        //{
        //    columnbind.Append(grdDetails.Columns[k].HeaderText + ',');
        //}

        //columnbind.Append("\r\n");
        //for (int i = 0; i < grdDetails.Rows.Count; i++)
        //{
        //    for (int k = 0; k < grdDetails.Columns.Count; k++)
        //    {
        //        if (grdDetails.Rows[i].Cells[k].Text != "&nbsp;")
        //            columnbind.Append(grdDetails.Rows[i].Cells[k].Text.Replace(',', '-') + ',');
        //        else
        //            columnbind.Append("NULL" + ',');
        //    }

        //    columnbind.Append("\r\n");
        //}

        DataTable dtCSV = fillgridExportDataTable();
        StringBuilder strCSV = Gen.CreateCSVString(dtCSV);

        Response.Output.Write(strCSV.ToString());
        Response.Flush();
        Response.End();
        grdDetails.Visible = false;
    }

    protected void btnExportZippedCSV_Click(object sender, EventArgs e)
    {
        //Send to user's browser CSV as a compressed .zip file
        //grdDetails.Visible = true;

        //this.fillgridExport();
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BeneficiariesDetails.zip");
        Response.Charset = "";
        Response.ContentType = "application/octet-stream";

        #region CSV creation
        //StringBuilder columnbind = new StringBuilder();
        //for (int k = 0; k < grdDetails.Columns.Count; k++)
        //{
        //    columnbind.Append(grdDetails.Columns[k].HeaderText + ',');
        //}

        //columnbind.Append("\r\n");
        //for (int i = 0; i < grdDetails.Rows.Count; i++)
        //{
        //    for (int k = 0; k < grdDetails.Columns.Count; k++)
        //    {
        //        if (grdDetails.Rows[i].Cells[k].Text != "&nbsp;")
        //            columnbind.Append(grdDetails.Rows[i].Cells[k].Text.Replace(',', '-') + ',');
        //        else
        //            columnbind.Append("NULL" + ',');
        //    }

        //    columnbind.Append("\r\n");
        //}
        #endregion

        DataTable dtCSV = fillgridExportDataTable();
        StringBuilder strCSV = Gen.CreateCSVString(dtCSV);
        //Encription of the string containing CSV to a .zip file        
        byte[] byteArray= Gen.zipString(strCSV.ToString(), "BeneficiariesDetails.csv");
        //ZipEntry csvEntry = new ZipEntry("BeneficiariesDetails.csv");
        //Send zipped file to output
        
        Response.AppendHeader("Content-Length", byteArray.Length.ToString());
        //Response.Output.Write(columnbind.ToString());
        Response.BinaryWrite(byteArray);
        Response.Flush();
        Response.End();
        grdDetails.Visible = false;
    }

    void fillgridExport()
    {
        DataSet dsExport = new DataSet();
        dsExport = Gen.DrillDownAPIExport(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        if (dsExport.Tables.Count > 0)
        {
            if (dsExport.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = dsExport.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = dsExport.Tables[0];
                grdDetails.DataBind();
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = dsExport.Tables[0];
                grdDetails.DataBind();
            }
        }
    }

    DataTable fillgridExportDataTable()
    {
        DataSet dsExport = new DataSet();
        dsExport = Gen.DrillDownAPIExport(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        return dsExport.Tables[0];
    }

    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
    {

    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        getcounties();
    }

    protected void btnExportNew_Click(object sender, EventArgs e)
    {
        //int count = 0;
        //foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        //{
        //    if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
        //    {
        //        count = count + 1;
        //    }
        //}
        //string tt1t = count.ToString();

        DataSet ds1 = new DataSet();
        ds1 = Gen.DrillDownAPI(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);

        if (ds1.Tables.Count > 0)
        {
            StringBuilder ttt = ds1.Tables[0].ToCSV();

            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=BeneficiaryDetails.csv");
            Response.Charset = "";
            Response.ContentType = "application/text";

            Response.Output.Write(ttt.ToString());
            Response.Flush();
            Response.End();
        }
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


    }
    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

        getworksbyPojects();
    }

    void getworksbyPojects()
    {


    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        Button btnDelete = (Button)sender;
        GridDataItem row = (GridDataItem)btnDelete.NamingContainer;
        int id = 0;
        int.TryParse(row["intBenTempid"].Text, out id);

        TextBox TxtResdntladre = (TextBox)row.FindControl("txtRemarks");
        if (TxtResdntladre.Text == "" || id == 0)
        {
            lblMsg.Visible = true;
            lblMsg.Text = "Please Enter Remarks";
            return;
        }
        int i = 0;
        i = Gen.DeleteByAPI(id.ToString(), TxtResdntladre.Text);
        if (i == 1)
        {
            fillgrid();
            lblMsg.Visible = true;
            lblMsg.Text = "Deleted successfully";
        }
    }

    protected void CleanAlertMessages()
    {
        lblmsg0.Text = "";
        lblMsg.Text = "";
        LblMessage2.Text = "";
        Failure.Visible = false;
        success.Visible = false;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        LblMessage2.Text = "";
        int count = 0;

        foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                count = count + 1;
            }
        }

        if (count == 0)
        {
            lblmsg0.Text = "At lease one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        else
        {
            int cntcheck = 0;

            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
                {
                    //Retrieve intBenTempid
                    string strIntBenTempid = "";
                    strIntBenTempid = dataItem.GetDataKeyValue("intBenTempid").ToString();
                    DropDownList ddlComponent = dataItem.FindControl("ddlRgComponent") as DropDownList;
                    if (ddlComponent.SelectedValue != "Select")
                    {
                        int result = Gen.UpdateBenficiariesEnrollment(strIntBenTempid, ddlComponent.SelectedValue); //Parameter intBenId, component
                        if (result == 100)
                        {
                            cntcheck = 1;
                        }
                    }
                    else
                    {
                        lblmsg0.Text = "No Component selected for some beneficiaryes to be enrolled";
                        Failure.Visible = true;
                        ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
                        return;
                    }
                }
            }

            if (cntcheck == 1)
            {
                LblMessage2.Text = "Successfully Updated but some are duplicated those are not updated";
            }
            else
            {
                LblMessage2.Text = "Successfully Updated";
            }
            fillgrid();
        }
    }
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        Response.Redirect("APIDashboard.aspx");
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        //LblMessage2.Text = "";
        int count = 0;

        foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
        {
            if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
            {
                count = count + 1;
            }
        }

        if (count == 0)
        {
            lblmsg0.Text = "At lease one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        else
        {
            int result = 0;
            int iDeleted = 0;
            int iCountToBeDeleted = 0;
            foreach (GridDataItem dataItem in RadGrid1.MasterTableView.Items)
            {
                if ((dataItem.FindControl("chkSelected") as CheckBox).Checked)
                {
                    iCountToBeDeleted = iCountToBeDeleted + 1;
                    //Retrieve intBenTempid
                    string strIntBenTempid = "";
                    strIntBenTempid = dataItem.GetDataKeyValue("intBenTempid").ToString();
                    result = Gen.DeleteByAPI(strIntBenTempid, "Bulk Deletion");
                    if (result > 0)
                    {
                        iDeleted = iDeleted + 1;
                    }
                }
            }
            if (iDeleted > 0)
            {
                if (iDeleted == iCountToBeDeleted)
                {
                    LblMessage2.Text = "All selected beneficiaries successfully Deleted";
                    fillgrid();
                }
                else
                {
                    LblMessage2.Text = "Not all selected Beneficiaries successfully Deleted";
                    fillgrid();
                }
            }
            else
            {
                LblMessage2.Text = "No Beneficiaries deleted";
            }
        }
    }

    protected void btnExportCSV_ClickTEST(object sender, EventArgs e)
    {
        grdDetails.Visible = true;

        //        this.fillgridExport();
        DataSet dsExport = new DataSet();
        dsExport = Gen.DrillDownAPIExport(Session["User_Code"].ToString().Trim(), Request.QueryString[0].ToString(), txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        DataTable dtExport = dsExport.Tables[0];

        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=BeneficiariesDetails.csv");
        Response.Charset = "";
        Response.ContentType = "application/text";

        StringBuilder columnbind = new StringBuilder();

        int iColumnCount = dtExport.Columns.Count;
        for (int iCounter = 0; iCounter < iColumnCount; iCounter++)
        {
            columnbind.Append(dtExport.Columns[iCounter].ColumnName + ',');
        }

        columnbind.Append("\r\n");

        for (int i = 0; i < dtExport.Rows.Count; i++)
        {
            for (int k = 0; k < dtExport.Columns.Count; k++)
            {
                if (dtExport.Rows[i][k].ToString() != "&nbsp;" && dtExport.Rows[i][k] != null && dtExport.Rows[i][k].ToString() != string.Empty)
                    columnbind.Append(dtExport.Rows[i][k].ToString().Replace(',', '-') + ',');
                else
                    columnbind.Append("N/A" + ',');
            }

            columnbind.Append("\r\n");
        }

        Response.Output.Write(columnbind.ToString());
        Response.Flush();
        Response.End();
        grdDetails.Visible = false;
    }
}

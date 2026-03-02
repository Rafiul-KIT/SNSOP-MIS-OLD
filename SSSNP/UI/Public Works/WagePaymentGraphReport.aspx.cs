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
using System.IO;
using System.Text;


//created by suresh as on 17-1-2016 tables:td_IPDetails,getIPSearch

public partial class WagePaymentGraphReport : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();

    #endregion
    decimal NoofPayams1, NoofBomas1, TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1, Type11, Type21, Type31, Type41, Type51, Type61, TotalWorkProposed12;

    decimal NoofPayams2, NoofBomas2, TotalWorkProposed2, TotaltobeApproved2, TotalInProgress2, TotalClosed2, TotalFundsDisbursed2, TotalBeneficiaries2, TotalFemaleBeneficiaries2, TotalWorkDays2;
    decimal TotaltobeApproved379, TotalFundsDisbursed37, TotalClosed37, TotaltobeApproved37, TotalWorkProposed37, TotalWorkProposed25, TotalInProgress25, TotaltobeApproved25, TotalClosed25, TotalFundsDisbursed25, TotalWorkProposed13, TotalInProgress13, TotaltobeApproved13, TotalClosed13, TotalFundsDisbursed13, TotalInProgress37;
    decimal NoofPayams3, NoofBomas3, TotalWorkProposed3, TotaltobeApproved3, TotalInProgress3, TotalClosed3, TotalFundsDisbursed3, TotalBeneficiaries3, TotalFemaleBeneficiaries3, TotalWorkDays3;
    decimal NoofPayams4, NoofBomas4, TotalWorkProposed4, TotaltobeApproved4, TotalInProgress4, TotalClosed4, TotalFundsDisbursed4, TotalBeneficiaries4, TotalFemaleBeneficiaries4, TotalWorkDays4;
    #region Page_Load
    string TST = "%", IP = "%";
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }


        if (Session.Count > 0)
        {
            if (Session["user_type"].ToString() == "TST")
            {
                TST = "%";
                IP = "%";
            }
            else
            {
                TST = "%";
                IP = "%";
            }

            if (Session["user_type"].ToString() == "IP")
            {
                TST = "%";
                IP = Session["User_Code"].ToString();
            }
            else
            {
                TST = "%";
                IP = "%";
            }
        }
        if (!IsPostBack)
        {
            for (int i = 2015; i <= DateTime.Now.Year; i++)
            {
                ddlYear.Items.Add(i.ToString());
            }

            // Gen.getStates(ddlState);
            Gen.getBomas(ddlBoma, "");
            BindPieChart();
            BindBarChart();
        }
    }
    #endregion


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        BindWageChart();
        BindPieChart();
        BindBarChart();
    }
    //protected void fillgrid()
    //{


    //    DataSet ds = Gen.WagespaidReport("%", IP);
    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
    //        grdDetails.DataSource = ds.Tables[0];
    //        grdDetails.DataBind();
    //        GridView1.DataSource = ds.Tables[0];
    //        GridView1.DataBind();
    //    }
    //    else
    //    {
    //        lblMsg.Text = "No Records found.";
    //        grdDetails.DataSource = null;
    //        grdDetails.DataBind();
    //        GridView1.DataSource = null;
    //        GridView1.DataBind();
    //    }


    //    //DataSet dsNew = Gen.CountiesWagespaidReport("%", IP);
    //    //if (dsNew.Tables[0].Rows.Count > 0)
    //    //{
    //    //    lblMsg.Text = dsNew.Tables[0].Rows.Count.ToString() + " Records found.";
    //    //    grdDetails0.DataSource = dsNew.Tables[0];
    //    //    grdDetails0.DataBind();
    //    //    GridView2.DataSource = dsNew.Tables[0];
    //    //    GridView2.DataBind();
    //    //}
    //    //else
    //    //{
    //    //    lblMsg.Text = "No Records found.";
    //    //    grdDetails0.DataSource = null;
    //    //    grdDetails0.DataBind();
    //    //    GridView2.DataSource = null;
    //    //    GridView2.DataBind();
    //    //}

    //    //DataSet dsNew1 = Gen.PayamWagespaidReport("%", IP);
    //    //if (dsNew1.Tables[0].Rows.Count > 0)
    //    //{
    //    //    lblMsg.Text = dsNew1.Tables[0].Rows.Count.ToString() + " Records found.";
    //    //    grdDetails1.DataSource = dsNew1.Tables[0];
    //    //    grdDetails1.DataBind();
    //    //    GridView3.DataSource = dsNew1.Tables[0];
    //    //    GridView3.DataBind();

    //    //}
    //    //else
    //    //{
    //    //    lblMsg.Text = "No Records found.";
    //    //    grdDetails1.DataSource = null;
    //    //    grdDetails1.DataBind();
    //    //    GridView3.DataSource = null;
    //    //    GridView3.DataBind();
    //    //}


    //    //DataSet dsNewrr = Gen.BomaWagepaidReportlatest("%", IP);
    //    //if (dsNewrr.Tables[0].Rows.Count > 0)
    //    //{
    //    //    lblMsg.Text = dsNew1.Tables[0].Rows.Count.ToString() + " Records found.";
    //    //    grdDetails2.DataSource = dsNewrr.Tables[0];
    //    //    grdDetails2.DataBind();
    //    //}
    //    //else
    //    //{
    //    //    grdDetails2.DataSource = null;
    //    //    grdDetails2.DataBind();
    //    //}
    //}

    private void BindPieChart()
    {
        DataTable dsChartData = new DataTable();
        StringBuilder strScript = new StringBuilder();

        try
        {

            dsChartData = Gen.WagespaidReport("%", IP).Tables[0];

            strScript.Append(@"<script type='text/javascript'>  
                    google.load('visualization', '1', {packages: ['corechart']}); </script>  
                      
                    <script type='text/javascript'>  
                     
                    function drawChart() {         
                    var data = google.visualization.arrayToDataTable([  
                    ['Name Of IP', 'Total Works Proposed'],");

            foreach (DataRow row in dsChartData.Rows)
            {
                strScript.Append("['" + row["IPName"] + "'," + row["TotalWorkProposed"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append(@" var options = {     
                                    title: 'IP Wise Works In Progress',            
                                        
                                    pieSliceText:'value',
                                    pieHole: 0.4,
                            legend: { position: 'bottom', maxLines: 3 },
                                    chartArea:{right:20,left:20,top:40,bottom:20,width:'100%',height:'400'},
                                    };   ");

            strScript.Append(@"var chart = new google.visualization.PieChart(document.getElementById('piechart_3d'));          
                                chart.draw(data, options);        
                                }    
                            google.setOnLoadCallback(drawChart);  
                            ");
            strScript.Append(" </script>");


            ltrPie.Text = strScript.ToString();

        }
        catch
        {
        }
        finally
        {
            dsChartData.Dispose();

        }

    }

    private void BindBarChart()
    {
        DataTable dsChartData = new DataTable();
        StringBuilder strScript = new StringBuilder();

        try
        {
            dsChartData = Gen.WagespaidReportBoma("%", IP).Tables[0];

            strScript.Append(@"<script type='text/javascript'>
                    google.load('visualization', '1', {packages: ['corechart']});</script>

                    <script type='text/javascript'>
                    function drawVisualization() {       
                    var data = google.visualization.arrayToDataTable([
                    ['IPName', 'MaleBeneficiaries', 'FeMaleBeneficiaries'],");

            foreach (DataRow row in dsChartData.Rows)
            {
                strScript.Append("['" + row["IPName"] + "'," + row["MaleBeneficiaries"] + "," +
                    row["FeMaleBeneficiaries"] + "],");
            }
            strScript.Remove(strScript.Length - 1, 1);
            strScript.Append("]);");

            strScript.Append("var options = { legend: { position: 'bottom', maxLines: 3 },height: 400, title: 'IP Wise Beneficiaries',colors: ['#1b9e77', '#d95f02'],bars: 'vertical', vAxis: {title: 'Number Of Beneficiaries'},  hAxis: {title: 'IPName'}, tooltip: { trigger: 'visible' },seriesType: 'bars' };");
            strScript.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('Bar_chart'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
            strScript.Append(" </script>");

            ltrlBar.Text = strScript.ToString();
        }
        catch
        {
        }
        finally
        {
            dsChartData.Dispose();
            //strScript.Clear();
        }
    }

    private void BindWageChart()
    {
        if (ddlBoma.SelectedItem.ToString() != "--Select--" || ddlYear.SelectedItem.ToString() != "--Select--")
        {
            DataTable dsChartData = new DataTable();
            StringBuilder strScript = new StringBuilder();

            try
            {
                dsChartData = Gen.getwageGraphReportBoma(ddlBoma.SelectedValue.ToString(), ddlYear.SelectedItem.ToString()).Tables[0];

                strScript.Append(@"<script type='text/javascript'>
                    google.load('visualization', '1', {packages: ['corechart']});</script>

                    <script type='text/javascript'>
                    function drawVisualization() {       
                    var data = google.visualization.arrayToDataTable([
                    ['Month', 'Male Wages', 'FeMale Wages'],");

                foreach (DataRow row in dsChartData.Rows)
                {
                    strScript.Append("['" + row["Month"] + "'," + row["Male Wages"] + "," +
                        row["FeMale Wages"] + "],");
                }
                strScript.Remove(strScript.Length - 1, 1);
                strScript.Append("]);");

                strScript.Append("var options = { legend: { position: 'bottom', maxLines: 3 },height: 400, title: 'Boma Wise Wage Payments',colors: ['#1b9e77', '#d95f02'],bars: 'vertical', vAxis: {title: 'Amount'},  hAxis: {title: 'Month'}, tooltip: { trigger: 'visible' },seriesType: 'bars' };");
                strScript.Append(" var chart = new google.visualization.ColumnChart(document.getElementById('Wage_chart'));  chart.draw(data, options); } google.setOnLoadCallback(drawVisualization);");
                strScript.Append(" </script>");

                ltrWage.Text = strScript.ToString();
            }
            catch
            {
            }
            finally
            {
                dsChartData.Dispose();
                LBLSelect.Text = "";
            }
        }
        else
        {
            LBLSelect.Text = "Please select work and year";
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            //NoofPayams1, NoofBomas1,TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, 
            //TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1;
            //decimal NoofPayams = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofPayams"));
            //NoofPayams1 = NoofPayams + NoofPayams1;

            //decimal NoofBomas = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBomas"));
            //NoofBomas1 = NoofBomas + NoofBomas1;


            decimal TotalWorkProposed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed1 = TotalWorkProposed + TotalWorkProposed1;

            decimal TotaltobeApproved = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved1 = TotaltobeApproved + TotaltobeApproved1;

            decimal TotalInProgress = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress1 = TotalInProgress + TotalInProgress1;

            decimal TotalClosed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed1 = TotalClosed + TotalClosed1;

            decimal TotalFundsDisbursed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed1 = TotalFundsDisbursed + TotalFundsDisbursed1;

            //decimal TotalBeneficiaries = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            //TotalBeneficiaries1 = TotalBeneficiaries + TotalBeneficiaries1;

            //decimal TotalFemaleBeneficiaries = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalFemaleBeneficiaries"));
            //TotalFemaleBeneficiaries1 = TotalFemaleBeneficiaries + TotalFemaleBeneficiaries1;

            //decimal TotalWorkDays = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            //TotalWorkDays1 = TotalWorkDays + TotalWorkDays1;

            //HyperLink h1 = (HyperLink)e.Row.Cells[0].Controls[0];
            //h1.Text = "Edit";
            //h1.NavigateUrl = "TSTIPregistration.aspx?prj=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim();

            HyperLink h1 = (HyperLink)e.Row.Cells[2].Controls[0];
            //h1.Text = "Edit";
            h1.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=1";

            HyperLink h2 = (HyperLink)e.Row.Cells[3].Controls[0];
            //h1.Text = "Edit";
            h2.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=4";

            HyperLink h3 = (HyperLink)e.Row.Cells[4].Controls[0];
            //h1.Text = "Edit";
            h3.NavigateUrl = "DrillDownBenificiaryWagespaymentsReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=999";

            HyperLink h4 = (HyperLink)e.Row.Cells[6].Controls[0];
            //h1.Text = "Edit";
            h4.NavigateUrl = "DrillDownBenificiaryWagesPaidByIPReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=2";

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //NoofPayams1, NoofBomas1,TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, 
            //TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1;

            e.Row.Cells[1].Text = "Total";

            //e.Row.Cells[3].Text = NoofPayams1.ToString();
            //  e.Row.Cells[3].Text = NoofBomas1.ToString();
            e.Row.Cells[2].Text = TotalWorkProposed1.ToString();
            e.Row.Cells[4].Text = TotalInProgress1.ToString();
            e.Row.Cells[3].Text = TotaltobeApproved1.ToString();
            e.Row.Cells[5].Text = TotalClosed1.ToString();
            e.Row.Cells[6].Text = TotalFundsDisbursed1.ToString();
            //e.Row.Cells[9].Text = TotalBeneficiaries1.ToString();
            //e.Row.Cells[10].Text = TotalFemaleBeneficiaries1.ToString();
            //e.Row.Cells[11].Text = TotalWorkDays1.ToString();
        }
    }


    protected void grdDetails_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    //protected void grdDetails0_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {


    //        decimal Type1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type1"));
    //        Type11 = Type1 + Type11;

    //        decimal Type2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type2"));
    //        Type21 = Type2 + Type21;


    //        decimal Type3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type3"));
    //        Type31 = Type3 + Type31;

    //        decimal Type4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type4"));
    //        Type41 = Type4 + Type41;

    //        decimal Type5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type5"));
    //        Type51 = Type5 + Type51;

    //        decimal Type6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type6"));
    //        Type61 = Type6 + Type61;

    //        decimal TotalWorkProposed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
    //        TotalWorkProposed12 = TotalWorkProposed + TotalWorkProposed12;

    //     }
    //    //Type11,Type21,Type31,Type41,Type51,Type61,TotalWorkProposed12
    //    if (e.Row.RowType == DataControlRowType.Footer)
    //    {
    //        e.Row.Cells[1].Text = "Total";

    //        e.Row.Cells[3].Text = Type11.ToString();
    //        e.Row.Cells[4].Text = Type21.ToString();
    //        e.Row.Cells[5].Text = Type31.ToString();
    //        e.Row.Cells[6].Text = Type41.ToString();
    //        e.Row.Cells[7].Text = Type51.ToString();
    //        e.Row.Cells[8].Text = Type61.ToString();
    //        e.Row.Cells[9].Text = TotalWorkProposed12.ToString();

    //    }
    //}
    protected void grdDetails0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal TotalWorkProposed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed2 = TotalWorkProposed + TotalWorkProposed2;

            decimal TotaltobeApproved = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved2 = TotaltobeApproved + TotaltobeApproved2;

            decimal TotalInProgress = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress2 = TotalInProgress + TotalInProgress2;

            decimal TotalClosed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed2 = TotalClosed + TotalClosed2;

            decimal TotalFundsDisbursed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed2 = TotalFundsDisbursed + TotalFundsDisbursed2;


            HyperLink h1 = (HyperLink)e.Row.Cells[2].Controls[0];
            //h1.Text = "Edit";
            h1.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intCountieid")).Trim() + "&status=6";

            HyperLink h2 = (HyperLink)e.Row.Cells[3].Controls[0];
            //h1.Text = "Edit";
            h2.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intCountieid")).Trim() + "&status=7";

            HyperLink h3 = (HyperLink)e.Row.Cells[4].Controls[0];
            //h1.Text = "Edit";
            h3.NavigateUrl = "DrillDownBenificiaryWagespaymentsReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intCountieid")).Trim() + "&status=3";

            HyperLink h4 = (HyperLink)e.Row.Cells[6].Controls[0];
            //h1.Text = "Edit";
            h4.NavigateUrl = "DrillDownBenificiaryWagesPaidByIPReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intCountieid")).Trim() + "&status=4";

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[2].Text = TotalWorkProposed2.ToString();
            e.Row.Cells[4].Text = TotalInProgress2.ToString();
            e.Row.Cells[3].Text = TotaltobeApproved2.ToString();
            e.Row.Cells[5].Text = TotalClosed2.ToString();
            e.Row.Cells[6].Text = TotalFundsDisbursed2.ToString();

        }
    }
    protected void grdDetails1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal TotalWorkProposed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed3 = TotalWorkProposed + TotalWorkProposed3;

            decimal TotaltobeApproved = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved3 = TotaltobeApproved + TotaltobeApproved3;

            decimal TotalInProgress = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress3 = TotalInProgress + TotalInProgress3;

            decimal TotalClosed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed3 = TotalClosed + TotalClosed3;

            decimal TotalFundsDisbursed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed3 = TotalFundsDisbursed + TotalFundsDisbursed3;


            HyperLink h1 = (HyperLink)e.Row.Cells[3].Controls[0];
            //h1.Text = "Edit";
            h1.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intPayamid")).Trim() + "&status=8";

            HyperLink h2 = (HyperLink)e.Row.Cells[4].Controls[0];
            //h1.Text = "Edit";
            h2.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intPayamid")).Trim() + "&status=9";

            HyperLink h3 = (HyperLink)e.Row.Cells[5].Controls[0];
            //h1.Text = "Edit";
            h3.NavigateUrl = "DrillDownBenificiaryWagespaymentsReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intPayamid")).Trim() + "&status=5";

            HyperLink h4 = (HyperLink)e.Row.Cells[7].Controls[0];
            //h1.Text = "Edit";
            h4.NavigateUrl = "DrillDownBenificiaryWagesPaidByIPReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intPayamid")).Trim() + "&status=6";

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[3].Text = TotalWorkProposed3.ToString();
            e.Row.Cells[5].Text = TotalInProgress3.ToString();
            e.Row.Cells[4].Text = TotaltobeApproved3.ToString();
            //      e.Row.Cells[7].Text = TotalClosed3.ToString();
            e.Row.Cells[7].Text = TotalFundsDisbursed3.ToString();

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
    protected void ExportToExcel()
    {



        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + " Wage payment report-IP wise.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {



            //GridView1.Visible = true;
            //fillgrid();

            //GridView1.RenderControl(htw);


        }
        catch (Exception eee)
        {
        }







        Page.Response.Write(sw.ToString());


        Response.End();
        //GridView1.Visible = false;


    }

    protected void btnexp0_Click(object sender, EventArgs e)
    {
        ExportToExcel1();
    }
    protected void ExportToExcel1()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + " Wage payment report - County wise.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {



           // GridView2.Visible = true;
           // fillgrid();

          //  GridView2.RenderControl(htw);


        }
        catch (Exception eee)
        {
        }







        Page.Response.Write(sw.ToString());


        Response.End();
        //GridView2.Visible = false;
    }


    protected void btnexp1_Click(object sender, EventArgs e)
    {

        ExportToExcel2();
    }
    protected void ExportToExcel2()
    {
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + "Wage payment report - Payam wise.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {



            //GridView3.Visible = true;
            //fillgrid();

            //GridView3.RenderControl(htw);


        }
        catch (Exception eee)
        {
        }


        Page.Response.Write(sw.ToString());


        Response.End();
       // GridView3.Visible = false;
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            //NoofPayams1, NoofBomas1,TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, 
            //TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1;
            //decimal NoofPayams = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofPayams"));
            //NoofPayams1 = NoofPayams + NoofPayams1;

            //decimal NoofBomas = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "NoofBomas"));
            //NoofBomas1 = NoofBomas + NoofBomas1;


            decimal TotalWorkProposed2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed13 = TotalWorkProposed2 + TotalWorkProposed13;

            decimal TotaltobeApproved2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved13 = TotaltobeApproved2 + TotaltobeApproved13;

            decimal TotalInProgress2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress13 = TotalInProgress2 + TotalInProgress13;

            decimal TotalClosed2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed13 = TotalClosed2 + TotalClosed13;

            decimal TotalFundsDisbursed2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed13 = TotalFundsDisbursed2 + TotalFundsDisbursed13;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            //NoofPayams1, NoofBomas1,TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, 
            //TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1;

            e.Row.Cells[1].Text = "Total";

            //e.Row.Cells[3].Text = NoofPayams1.ToString();
            //  e.Row.Cells[3].Text = NoofBomas1.ToString();
            e.Row.Cells[2].Text = TotalWorkProposed13.ToString();
            e.Row.Cells[4].Text = TotalInProgress13.ToString();
            e.Row.Cells[3].Text = TotaltobeApproved13.ToString();
            e.Row.Cells[5].Text = TotalClosed13.ToString();
            e.Row.Cells[6].Text = TotalFundsDisbursed13.ToString();
            //e.Row.Cells[9].Text = TotalBeneficiaries1.ToString();
            //e.Row.Cells[10].Text = TotalFemaleBeneficiaries1.ToString();
            //e.Row.Cells[11].Text = TotalWorkDays1.ToString();
        }
    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal TotalWorkProposed4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed25 = TotalWorkProposed4 + TotalWorkProposed25;

            decimal TotaltobeApproved4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved25 = TotaltobeApproved4 + TotaltobeApproved25;

            decimal TotalInProgress4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress25 = TotalInProgress4 + TotalInProgress25;

            decimal TotalClosed4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed25 = TotalClosed4 + TotalClosed25;

            decimal TotalFundsDisbursed4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed25 = TotalFundsDisbursed4 + TotalFundsDisbursed25;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[2].Text = TotalWorkProposed25.ToString();
            e.Row.Cells[4].Text = TotalInProgress25.ToString();
            e.Row.Cells[3].Text = TotaltobeApproved25.ToString();
            e.Row.Cells[5].Text = TotalClosed25.ToString();
            e.Row.Cells[6].Text = TotalFundsDisbursed25.ToString();

        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal TotalWorkProposed6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed37 = TotalWorkProposed6 + TotalWorkProposed37;

            decimal TotaltobeApproved6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved379 = TotaltobeApproved6 + TotaltobeApproved379;

            decimal TotalInProgress6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress37 = TotalInProgress6 + TotalInProgress37;

            decimal TotalClosed6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed37 = TotalClosed6 + TotalClosed37;

            decimal TotalFundsDisbursed6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed37 = TotalFundsDisbursed6 + TotalFundsDisbursed37;

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[2].Text = TotalWorkProposed3.ToString();
            e.Row.Cells[4].Text = TotalInProgress3.ToString();
            e.Row.Cells[3].Text = TotaltobeApproved3.ToString();
            e.Row.Cells[5].Text = TotalClosed3.ToString();
            e.Row.Cells[6].Text = TotalFundsDisbursed3.ToString();

        }
    }
    protected void grdDetails2_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal TotalWorkProposed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkProposed"));
            TotalWorkProposed4 = TotalWorkProposed + TotalWorkProposed4;

            decimal TotaltobeApproved = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotaltobeApproved4 = TotaltobeApproved + TotaltobeApproved4;

            decimal TotalInProgress = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalBeneficiaries"));
            TotalInProgress4 = TotalInProgress + TotalInProgress4;

            decimal TotalClosed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWorkDays"));
            TotalClosed4 = TotalClosed + TotalClosed4;

            decimal TotalFundsDisbursed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalWageAmt"));
            TotalFundsDisbursed4 = TotalFundsDisbursed + TotalFundsDisbursed4;


            HyperLink h1 = (HyperLink)e.Row.Cells[4].Controls[0];
            //h1.Text = "Edit";
            h1.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBomasid")).Trim() + "&status=17";

            HyperLink h2 = (HyperLink)e.Row.Cells[5].Controls[0];
            //h1.Text = "Edit";
            h2.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBomasid")).Trim() + "&status=19";

            HyperLink h3 = (HyperLink)e.Row.Cells[6].Controls[0];
            //h1.Text = "Edit";
            h3.NavigateUrl = "DrillDownBenificiaryWagespaymentsReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intBomasid")).Trim() + "&status=13";

            //HyperLink h4 = (HyperLink)e.Row.Cells[7].Controls[0];
            ////h1.Text = "Edit";
            //h4.NavigateUrl = "DrillDownBenificiaryWagesPaidByIPReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intPayamid")).Trim() + "&status=6";

        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[4].Text = TotalWorkProposed4.ToString();
            e.Row.Cells[6].Text = TotalInProgress4.ToString();
            e.Row.Cells[5].Text = TotaltobeApproved4.ToString();
            //      e.Row.Cells[7].Text = TotalClosed3.ToString();
            e.Row.Cells[8].Text = TotalFundsDisbursed4.ToString();

        }
    }
}

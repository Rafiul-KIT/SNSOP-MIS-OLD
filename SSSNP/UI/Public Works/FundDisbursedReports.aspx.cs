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


//created by suresh as on 17-1-2016 tables:td_IPDetails,getIPSearch

public partial class FundDisbursedReports : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();
    
    #endregion
    decimal TotalWorkProposed13, TotalInProgress13, TotaltobeApproved13, TotalClosed13, TotalFundsDisbursed13, NoofPayams1, NoofBomas1, TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1, Type11, Type21, Type31, Type41, Type51, Type61, TotalWorkProposed12;
    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
           // Gen.getStates(ddlState);
            fillgrid();
        }
    }
    #endregion


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {
        string TST = "%",IP="%";
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

        DataSet ds = Gen.FundDisbursedReports("%", IP);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails.DataSource = null;
            grdDetails.DataBind();
            GridView1.DataSource = null;
            GridView1.DataBind();
        }


        //DataSet dsNew = Gen.PayamwisereportByTypeNew("%", IP);
        //if (dsNew.Tables[0].Rows.Count > 0)
        //{
        //    lblMsg.Text = dsNew.Tables[0].Rows.Count.ToString() + " Records found.";
        //    grdDetails0.DataSource = dsNew.Tables[0];
        //    grdDetails0.DataBind();
        //}
        //else
        //{
        //    lblMsg.Text = "No Records found.";
        //    grdDetails0.DataSource = null;
        //    grdDetails0.DataBind();
        //}

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

            decimal TotaltobeApproved = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalTobeApproved"));
            TotaltobeApproved1 = TotaltobeApproved + TotaltobeApproved1;

            decimal TotalInProgress = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotalInProgress1 = TotalInProgress + TotalInProgress1;

            decimal TotalClosed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalRaiseFundsAmt"));
            TotalClosed1 = TotalClosed + TotalClosed1;

            decimal TotalFundsDisbursed = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalReleaseFundsAmt"));
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
            // h1.Text = "Edit";
            h1.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=1";

            HyperLink h2 = (HyperLink)e.Row.Cells[3].Controls[0];
            // h1.Text = "Edit";
            h2.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=3";

            HyperLink h3 = (HyperLink)e.Row.Cells[4].Controls[0];
            // h1.Text = "Edit";
            h3.NavigateUrl = "DrillDownFundDistributedReports.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=4";

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
        Response.AddHeader("content-disposition", "attachment;FileName=" + DateTime.Now.ToString("M/d/yyyy") + "Fund Disbursed Reports.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {



            GridView1.Visible = true;
            fillgrid();

            GridView1.RenderControl(htw);


        }
        catch (Exception eee)
        {
        }







        Page.Response.Write(sw.ToString());


        Response.End();
        GridView1.Visible = false;


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

            decimal TotaltobeApproved2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalTobeApproved"));
            TotaltobeApproved13 = TotaltobeApproved2 + TotaltobeApproved13;

            decimal TotalInProgress2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalInProgress"));
            TotalInProgress13 = TotalInProgress2 + TotalInProgress13;

            decimal TotalClosed2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalRaiseFundsAmt"));
            TotalClosed13 = TotalClosed2 + TotalClosed13;

            decimal TotalFundsDisbursed2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "TotalReleaseFundsAmt"));
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
}

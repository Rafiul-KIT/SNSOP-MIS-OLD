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

#region Amendment History Begin
//Form Name : IP Work wise Report
//Purpose : To Search IP Work wise Report
//Tables used : tbl_WorkMaster,tr_ProposedWorkAct
//Stored procedures Used : IPwiseTypesofWorkreport
#endregion Amendment History End

public partial class WorkwiseStatusreportNew2 : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();
    decimal NoofPayams1,NoofCounties1, NoofBomas1,TotalmaleBeneficiaries1, TotalWorkProposed1, TotaltobeApproved1, TotalInProgress1, TotalClosed1, TotalFundsDisbursed1, TotalBeneficiaries1, TotalFemaleBeneficiaries1, TotalWorkDays1, Type11, Type21, Type31, Type41, Type51, Type61, TotalWorkProposed12;
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

    #region Search Report and Filling Details Functions

    void fillgrid()
    {
        string TST = "%", IP = "%";
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



        DataSet dsNew = Gen.IPwiseTypesofWorkreport("%", IP);
        if (dsNew.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = dsNew.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails0.DataSource = dsNew.Tables[0];
            grdDetails0.DataBind();
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails0.DataSource = null;
            grdDetails0.DataBind();
        }

    }
    #endregion

    #region Gridview Events
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
  
    }


    protected void grdDetails0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {


            decimal Type1 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type1"));
            Type11 = Type1 + Type11;

            decimal Type2 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type2"));
            Type21 = Type2 + Type21;


            decimal Type3 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type3"));
            Type31 = Type3 + Type31;

            decimal Type4 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type4"));
            Type41 = Type4 + Type41;

            decimal Type5 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type5"));
            Type51 = Type5 + Type51;

            decimal Type6 = Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Type6"));
            Type61 = Type6 + Type61;


            HyperLink h1 = (HyperLink)e.Row.Cells[2].Controls[0];
            // h1.Text = "Edit";
            h1.NavigateUrl = "DrillDownWorkWiseStatusReportnew.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=1";

            HyperLink h2 = (HyperLink)e.Row.Cells[3].Controls[0];
            // h1.Text = "Edit";
            h2.NavigateUrl = "DrillDownWorkWiseStatusReportnew.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=2";

            HyperLink h3 = (HyperLink)e.Row.Cells[4].Controls[0];
            // h1.Text = "Edit";
            h3.NavigateUrl = "DrillDownWorkWiseStatusReportnew.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=3";

            HyperLink h4 = (HyperLink)e.Row.Cells[5].Controls[0];
            // h1.Text = "Edit";
            h4.NavigateUrl = "DrillDownWorkWiseStatusReportnew.aspx?id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intIP")).Trim() + "&status=4";

        

        }
        //Type11,Type21,Type31,Type41,Type51,Type61,TotalWorkProposed12
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total";

            e.Row.Cells[2].Text = Type11.ToString();
            e.Row.Cells[3].Text = Type21.ToString();
            e.Row.Cells[4].Text = Type31.ToString();
            e.Row.Cells[5].Text = Type41.ToString();
            e.Row.Cells[6].Text = Type51.ToString();
            e.Row.Cells[7].Text = Type61.ToString();
            //e.Row.Cells[8].Text = TotalWorkProposed12.ToString();

        }
    }
    #endregion
}

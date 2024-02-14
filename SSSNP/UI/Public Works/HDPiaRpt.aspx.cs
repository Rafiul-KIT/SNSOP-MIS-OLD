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

public partial class HDPiaRpt : System.Web.UI.Page
{
    #region Declarations
    HtmlTableRow tRow;
    HtmlTableCell tcell1;
    string Pia = "",piano="";
    //common clsGeneral = new common();
    General clsGeneral = new General();
    DataSet ds = new DataSet();
    int cnt = 0;
    decimal TotIss, OpenIss, CloseIss, RejIss;
    decimal TotIssP, OpenIssP, CloseIssP, RejIssP;
    string usertype = "";
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //if (Session.Count > 0)
            //{

            //}
            //usertype = Session["user_type"].ToString().Trim();
            if (Session["user_id"].ToString() == "Helpdesk")
                usertype = "ADMN";
            else
                usertype = "USER";

            
            LblStatus.Text = "";
            if (usertype == "ADMN" || usertype == "MORD")
                piano = "%";
            if (usertype == "USER")
                piano = Session["user_id"].ToString();
            
            if (!IsPostBack)
            {
                THeader();
                fillgrid();
                tbl1.Width = "100%";
                TTotal("Sr");
                TTotal("Gr");
            }
        }
        catch (Exception ex)
        {
        }

    }
    protected void Btnsearch_Click(object sender, EventArgs e)
    {
        THeader();
        fillgrid();
        tbl1.Width = "100%";
        TTotal("Sr");
        TTotal("Gr");
    }
    #region GridBinding
    public void fillgrid()
    {
        try
        {
            DataSet HelpDeskDS = new DataSet();
            HelpDeskDS = clsGeneral.GetFBPCounts(txtFromdate.Text.Trim(), txtTodate.Text.Trim(), piano);
            grdHelpdesk.DataSource = HelpDeskDS;
            grdHelpdesk.DataBind();
        }
        catch (Exception ex)
        {
        }
        finally
        {
        }
    }
    #endregion

    protected void grdHelpdesk_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //sno = ++sno;
            //e.Row.Cells[0].Text = sno.ToString();

            //HyperLink h1 = (HyperLink)e.Row.Cells[2].Controls[0];
            ////h1.NavigateUrl = "HelpdeskDet.aspx?code=1,Fr=" + txtFromdate.Text.Trim() + ",To=" + txtFromdate.Text.Trim() + ",FBTyp=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fb_type")).Trim();
            //h1.NavigateUrl = "HelpdeskDet.aspx?code=1&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtFromdate.Text.Trim() + "&FBTyp=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fb_type")).Trim() + "&FBNo=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")).Trim();
            ////h1.NavigateUrl = "HelpdeskDet.aspx?code=1," + txtFromdate.Text.Trim() + "," + txtFromdate.Text.Trim() + "," + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fb_type")).Trim();
            //HyperLink h2 = (HyperLink)e.Row.Cells[3].Controls[0];
            //h2.NavigateUrl = "HelpdeskDet.aspx?code=2&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtFromdate.Text.Trim() + "&FBTyp=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fb_type")).Trim() + "&FBNo=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")).Trim();
            //HyperLink h3 = (HyperLink)e.Row.Cells[4].Controls[0];
            //h3.NavigateUrl = "HelpdeskDet.aspx?code=3&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtFromdate.Text.Trim() + "&FBTyp=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fb_type")).Trim() + "&FBNo=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")).Trim();
            //HyperLink h4 = (HyperLink)e.Row.Cells[5].Controls[0];
            //h4.NavigateUrl = "HelpdeskDet.aspx?code=4&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtFromdate.Text.Trim() + "&FBTyp=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "fb_type")).Trim() + "&FBNo=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")).Trim();


            //TChildHead(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "piaName")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Fb_Type")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Total")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Open1")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Close1")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Reject")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intPIAid")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")));
            TChildHead(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pianame")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Fb_Type")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Total")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Open1")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Close1")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Reject")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "pianame")), Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intfb_id")));
            
            TotIss += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            OpenIss += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Open1"));
            CloseIss += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Close1"));
            RejIss += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Reject"));

            TotIssP += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Total"));
            OpenIssP += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Open1"));
            CloseIssP += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Close1"));
            RejIssP += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Reject"));
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            e.Row.Cells[1].Text = "Total :";
            e.Row.Cells[2].Text = TotIss.ToString("##,##,###");
            e.Row.Cells[3].Text = OpenIss.ToString("##,##,###");
            e.Row.Cells[4].Text = CloseIss.ToString("##,##,###");
            e.Row.Cells[5].Text = RejIss.ToString("##,##,###");
            //e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Center;
            //e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Center;
            
            e.Row.Font.Bold = true;
        }
    }
    #region Create Dynamic Table

    public void THeader()
    {

        #region First Copy
        tRow = new HtmlTableRow();
        tcell1 = new HtmlTableCell();
        tcell1.InnerHtml = "<font color=white><b>SNO</b></font>";
        tcell1.Align = "Center";
        //tcell1.ColSpan = 1;
        //tcell1.RowSpan = 2;
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        tcell1.InnerHtml = "<font color=white><b>Feedback Type</b></font>";
        tcell1.Align = "Center";
        //tcell1.ColSpan = 1;
        //tcell1.RowSpan = 2;
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        tcell1.InnerHtml = "<font color=white><b>Total</b></font>";
        tcell1.Align = "Center";
        //tcell1.ColSpan = 1;
        //tcell1.RowSpan = 2;
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        tcell1.InnerHtml = "<font color=white><b>Open</b></font>";
        tcell1.Align = "Center";
        //tcell1.ColSpan = 2;
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        tcell1.InnerHtml = "<font color=white><b>Closed</b></font>";
        tcell1.Align = "Center";
        //tcell1.ColSpan = 2;
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        tcell1.InnerHtml = "<font color=white><b>Rejected</b></font>";
        tcell1.Align = "Center";
        //tcell1.ColSpan = 1;
        //tcell1.RowSpan = 2;
        tRow.Cells.Add(tcell1);
        //tRow.BgColor = "#441212";
        tRow.BgColor = "#093461";
        tbl1.Rows.Add(tRow);

        #endregion

    }

    public void TChildHead(string intfb_id, string piaName, string FBType, string Total, string Open1, string Close1, string Reject,string piaid,string FBid)
    {
        cnt = cnt + 1;
        if (Pia.Trim() != piaName.Trim())
        {
            if (cnt > 1)
                TTotal("Sr");
            TotIssP=0;
            OpenIssP=0;
            CloseIssP = 0;
            RejIssP=0;

            Pia = piaName.Trim();
            tRow = new HtmlTableRow();
            tcell1 = new HtmlTableCell();
            tcell1.InnerHtml = "<b>" + piaName.Trim() + "</b>";
            tcell1.Align = "Center";
            tcell1.ColSpan = 6;
            tRow.Cells.Add(tcell1);
            tRow.BgColor = "white";
            //tRow.BgColor = "#EBF8D4";
            tbl1.Rows.Add(tRow);
        }

        // tRow = new HtmlTableRow();
        #region First Copy
        tRow = new HtmlTableRow();
        tcell1 = new HtmlTableCell();
        tcell1.InnerText = cnt.ToString().Trim();
        tcell1.Align = "Center";
        tcell1.Width = "50";
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        tcell1.InnerText = FBType.Trim();
        tcell1.Align = "Left";
        tRow.Cells.Add(tcell1);
        //tcell1 = new HtmlTableCell();
        //tcell1.InnerHtml = Total.Trim();
        //tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        //tcell1.InnerHtml = Total.Trim();
        tcell1.InnerHtml = "<a href='HelpdeskDet.aspx?code=1&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtTodate.Text.Trim() + "&FBTyp=" + FBType.Trim() + "&FBNo=" + FBid.Trim() + "&Piano=" + piaid.Trim() + "&Pianam=" + piaName.Trim() + "'> " + Total.Trim() + "</a>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        //tcell1.InnerHtml = Open1.Trim();
        tcell1.InnerHtml = "<a href='HelpdeskDet.aspx?code=2&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtTodate.Text.Trim() + "&FBTyp=" + FBType.Trim() + "&FBNo=" + FBid.Trim() + "&Piano=" + piaid.Trim() + "&Pianam=" + piaName.Trim() + "'> " + Open1.Trim() + "</a>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        //tcell1.InnerHtml = Close1.Trim();
        tcell1.InnerHtml = "<a href='HelpdeskDet.aspx?code=3&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtTodate.Text.Trim() + "&FBTyp=" + FBType.Trim() + "&FBNo=" + FBid.Trim() + "&Piano=" + piaid.Trim() + "&Pianam=" + piaName.Trim() + "'> " + Close1.Trim() + "</a>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);
        tcell1 = new HtmlTableCell();
        //tcell1.InnerHtml = Reject.Trim();
        tcell1.InnerHtml = "<a href='HelpdeskDet.aspx?code=4&Fr=" + txtFromdate.Text.Trim() + "&To=" + txtTodate.Text.Trim() + "&FBTyp=" + FBType.Trim() + "&FBNo=" + FBid.Trim() + "&Piano=" + piaid.Trim() + "&Pianam=" + piaName.Trim() + "'> " + Reject.Trim() + "</a>";
        tcell1.Align = "Center";
        //tRow.Cells.Add(tcell1);
        //tRow.Cells.Add(tcell1);
        //tcell1 = new HtmlTableCell();
        //tcell1.InnerHtml = "<a href='frmCCDetails.aspx?Id=" + Service_id.Trim() + "&Req=Reject&From=" + txtFrom.Text.Trim() + "&To=" + txtTo.Text.Trim() + "'> " + Rejected.Trim() + "</a>";
        //tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);
        tbl1.Rows.Add(tRow);


        #endregion

    }

    public void TTotal(string totStatus)
    {
        //tRow = new HtmlTableRow();


        #region First Copy
        tRow = new HtmlTableRow();
        tcell1 = new HtmlTableCell();
        if (totStatus.Trim() == "Gr")
            tcell1.InnerHtml = "<font color=white><b> Grand Total </b></font>";
        else
            tcell1.InnerHtml = "<b> Sub Total </b>";
        tcell1.ColSpan = 2;
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);

        tcell1 = new HtmlTableCell();
        if (totStatus.Trim() == "Gr")
            tcell1.InnerHtml = "<font color=white><b>" + TotIss.ToString().Trim() + "</b></font>";
        else
            tcell1.InnerHtml = "<b>" + TotIssP.ToString().Trim() + "</b>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);

        tcell1 = new HtmlTableCell();
        if (totStatus.Trim() == "Gr")
            tcell1.InnerHtml = "<font color=white><b>" + OpenIss.ToString().Trim() + "</b></font>";
        else
            tcell1.InnerHtml = "<b>" + OpenIssP.ToString().Trim() + "</b>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);

        tcell1 = new HtmlTableCell();
        if (totStatus.Trim() == "Gr")
            tcell1.InnerHtml = "<font color=white><b>" + CloseIss.ToString().Trim() + "</b></font>";
        else
            tcell1.InnerHtml = "<b>" + CloseIssP.ToString().Trim() + "</b>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);

        tcell1 = new HtmlTableCell();
        if (totStatus.Trim() == "Gr")
            tcell1.InnerHtml = "<font color=white><b>" + RejIss.ToString().Trim() + "</b></font>";
        else
            tcell1.InnerHtml = "<b>" + RejIssP.ToString().Trim() + "</b>";
        tcell1.Align = "Center";
        tRow.Cells.Add(tcell1);

        if (totStatus.Trim() == "Gr")
            //tRow.BgColor = "#441212";
            tRow.BgColor = "#093461";
        else
            tRow.BgColor = "white";
            //tRow.BgColor = "#EBF8D4";
        tbl1.Rows.Add(tRow);
        #endregion


        if (TotIss <= 0)
        {
            //tbl1.Visible = false;
            LblStatus.Text = " No Records Found ";
        }
    }

    #endregion
}

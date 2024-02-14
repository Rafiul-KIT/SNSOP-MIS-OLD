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

public partial class IPInvoices : System.Web.UI.Page
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
            if (Request.QueryString["q"] != null)
            {
                if (Request.QueryString["q"].ToString() != "")
                { hdnStat.Value = Request.QueryString["q"].ToString();
                    fillgrid();
                }
            }
        }
    }
    #endregion


   
    void fillgrid()
    {
        DataSet ds = new DataSet();
        if (hdnStat.Value.ToString() == "Submitted")
        {ds = clsGeneral.SubmittedInvoiceIP(Session["uid"].ToString());
            h3.InnerText = "No Of Invoices Submittted";
        }
       
        else  if (hdnStat.Value.ToString() == "Approved")
        { ds = clsGeneral.ApprovedInvoiceIP (Session["uid"].ToString());
            h3.InnerText = "No Of Invoices Reviewed";
        }
        else if (hdnStat.Value.ToString() == "Rejected")
        { ds = clsGeneral.RejectedInvoiceIP  (Session["uid"].ToString());
            h3.InnerText = "No Of Invoices Rejected";
        }


        if(ds.Tables.Count>0)
        {
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();

                if (hdnStat.Value.ToString() == "Submitted")
                {
                    grdDetails.Columns[4].Visible = false;
                    grdDetails.Columns[5].Visible = false;
                   
                }
               
                else if (hdnStat.Value.ToString() == "Approved")
                {
                    grdDetails.Columns[4].Visible = true;
                    grdDetails.Columns[5].Visible = false;
                   
                }
                else if (hdnStat.Value.ToString() == "Rejected")
                {
                    grdDetails.Columns[4].Visible = true;
                    grdDetails.Columns[5].Visible = true;
                   
                }
            }
            else
            {
                lblMsg.Text = "No Records found.";
                grdDetails.DataSource = ds.Tables[0];
                grdDetails.DataBind();
            }
        }
      


    }
    protected void LinkButton1_Clicked(object sender, EventArgs e)
    {
        string id = (sender as LinkButton).CommandArgument;
        Response.Redirect("Review_Invoice.aspx?q=" + id);
    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {

    //        ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intWorkId") + "','" + DataBinder.Eval(e.Row.DataItem, "invoiceid") + "')");
    //    }
    }
    
   
}

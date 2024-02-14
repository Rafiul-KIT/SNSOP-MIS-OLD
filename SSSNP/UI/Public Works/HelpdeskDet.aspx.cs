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
using System.Data.SqlClient;

public partial class HelpdeskDet : System.Web.UI.Page
{
    #region Declarations

    DB.DB con1 = new DB.DB();
    General clsCommon = new General();
    //General clsGeneral = new General();
    DataSet ds = new DataSet();
    //SqlDataReader drFBNum = null;
    int sno = 0;
    string stat = "", fromdt1 = "", todt1 = "", FBType = "", FBNum = "", PiaName = "", PiaNum = "", ReqType="";
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            //if (Session.Count > 0)
            //{

            //}


            if (!IsPostBack)
            {
                
                if (Request.QueryString.Count != 5)
                {
                    stat = Request.QueryString["code"].Trim();
                    fromdt1 = Request.QueryString["Fr"].Trim();
                    todt1 = Request.QueryString["To"].Trim();
                    FBType = Request.QueryString["FBTyp"].Trim();
                    FBNum = Request.QueryString["FBNo"].Trim();
                    PiaNum = Request.QueryString["Piano"].Trim();
                    PiaName = Request.QueryString["Pianam"].Trim();
                    Session["ReqType"] = "2";
                    //ReqType = "With Pia";
                }
                else
                {
                    stat = Request.QueryString["code"].Trim();
                    fromdt1 = Request.QueryString["Fr"].Trim();
                    todt1 = Request.QueryString["To"].Trim();
                    FBType = Request.QueryString["FBTyp"].Trim();
                    FBNum = Request.QueryString["FBNo"].Trim();
                    PiaNum = "";
                    Session["ReqType"] = "1";
                    //ReqType = "Without Pia";
                }

                fillgrid();
            }
            
        }
        catch (Exception ex)
        {
        }
        
    }

    #region GridBinding
    public void fillgrid()
    {
        try
        {
            //con1.OpenConnection();
            //drFBNum = clsCommon.GetFBNum(FBType, con1.GetConnection);
            ////FBNum = drFBNum.Read();
            //while (drFBNum.Read())
            //{
            //    FBNum = drFBNum.GetString(0);
            //}
            DataSet HelpDeskDS = new DataSet();
            HelpDeskDS = clsCommon.GetFBDet(stat, fromdt1, todt1, FBNum, PiaNum);
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
            sno = ++sno;
            e.Row.Cells[0].Text = sno.ToString();
            e.Row.Cells[1].Text = FBType.ToString();
            HyperLink h2 = (HyperLink)e.Row.FindControl("hypLetter");
            h2.NavigateUrl = "HDDocsDL.aspx?type=2&id=" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "int_fbid"));
            h2.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "hd_uplddocname"));
        }
    }
    protected void BtnBack_Click(object sender, EventArgs e)
    {
        //if (ReqType == "Without Pia")
        if (Session["ReqType"].ToString().Trim() == "1")        
            Response.Redirect("Helpdeskrpt.aspx");
        if (Session["ReqType"].ToString().Trim() == "2")
        //if (ReqType=="With Pia")
            Response.Redirect("HDPiaRpt.aspx");

    }
}

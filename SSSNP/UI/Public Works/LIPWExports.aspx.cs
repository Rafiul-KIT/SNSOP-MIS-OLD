#region Using/Inherits Classess
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Net.Mail;
using System.IO;
using Telerik.Web.UI;
using SSSNP_SNSOP.Entity;
using System.Collections.Generic;
using SSSNP_SNSOP.Core;
using System.Web;
#endregion

#region Amendment History Begin
//Form Name : Apply for Wage Reuest
//Purpose : To Add Wage Reuest
//Tables used : tr_BenAttendanceDet,td_WorkWageRequest,td_WageReqBenDet,tbl_WorkMaster,tbl_BeneficiaryDet,tr_BenfWorkDet
//Stored procedures Used : getOfficers,GetworkproposalforProgress,getAttendanceCountFromToWagesNewWithAmount,getCheckingWagesApplyrestrictionbyIDNewModel,insertWageRequest,InsUpdBeneficiaryWagesRequestAlternateNewModel,insertWageRequestUpdatesStatusRemoveData
#endregion Amendment History End


public partial class LIPWExports : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
     General Gen = new General();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 500;

        // If Session cleared, Redirect to Login page
        if (Session.Count == 0)
        {
            Response.Redirect("~/frmuserlogin.aspx");
        }

        //Code snippet for type of user check
        //if (CUser.Current.IsIPRole || CUser.Current.IsCoordinatorRole)


        #region Is not Postback Statement
        if (!IsPostBack)
        {
            lblMessage.Visible = false;
            divFailure.Visible = false;
        }
        #endregion
    }
    #endregion

    protected void btnLIPWWorkProposalData_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        divFailure.Visible = false;

        try
        {
            DataSet dsExport = new DataSet();
            dsExport = Gen.getLIPWWorkProposalData();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.AddHeader("content-disposition", string.Format("attachment;filename=LIPWWorkProposalData_{0}.csv", DateTime.Today.ToString("yyyyMMdd")));
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            DataTable dtExport = new DataTable();
            dtExport = dsExport.Tables[0];
            StringBuilder sbCSVContent = new StringBuilder();

            for (int k = 0; k < dtExport.Columns.Count; k++)
            {
                sbCSVContent.Append(dtExport.Columns[k].ColumnName + ',');
            }

            sbCSVContent.Append("\r\n");
            for (int i = 0; i < dtExport.Rows.Count; i++)
            {
                for (int k = 0; k < dtExport.Columns.Count; k++)
                {
                    sbCSVContent.Append(dtExport.Rows[i][k].ToString().Replace(',', '-') + ',');
                }
                sbCSVContent.Append("\r\n");
            }
            Response.Write(sbCSVContent.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error in generation and download of LIPW Work Proposal Report";
            lblMessage.Visible = true;
            divFailure.Visible = true;
            //throw ex;
        }
    }

    protected void btnLIPWBeneficiaries_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = false;

        try
        {
            DataSet dsExport = new DataSet();
            dsExport = Gen.getLIPWBeneficiaries();
            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/text";
            Response.AddHeader("content-disposition", string.Format("attachment;filename=LIPWBeneficiaries_{0}.csv", DateTime.Today.ToString("yyyyMMdd")));
            Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            DataTable dtExport = new DataTable();
            dtExport = dsExport.Tables[0];
            StringBuilder sbCSVContent = new StringBuilder();

            for (int k = 0; k < dtExport.Columns.Count; k++)
            {
                sbCSVContent.Append(dtExport.Columns[k].ColumnName + ',');
            }

            sbCSVContent.Append("\r\n");
            for (int i = 0; i < dtExport.Rows.Count; i++)
            {
                for (int k = 0; k < dtExport.Columns.Count; k++)
                {
                    sbCSVContent.Append(dtExport.Rows[i][k].ToString().Replace(',', '-') + ',');
                }
                sbCSVContent.Append("\r\n");
            }
            Response.Write(sbCSVContent.ToString());
            Response.Flush();
            Response.End();
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Error in generation and download of LIPW Work Proposal Report";
            lblMessage.Visible = true;
            divFailure.Visible = true;
            //throw ex;
        }

    }
}
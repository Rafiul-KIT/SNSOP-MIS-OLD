
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;


public partial class WorkProposalLookup2 : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    DataSet ds = new DataSet();
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
            clsGeneral.getIpDet(ddlIP);
            if (Session["user_type"].ToString() == "IP")
            {
                ddlIP.SelectedValue = ddlIP.Items.FindByValue(Session["User_Code"].ToString()).Value;
                ddlIP.Enabled = false;

                Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            }
            else if (Session["user_type"].ToString() == "TST")
            {
                clsGeneral.GetIpsByTST(ddlIP,Session["uid"].ToString());
                
                ddlIP.Enabled = true;
            }
            //fillgrid();

            Gen.getStates(ddlState);
        }
    }
    #endregion

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    void fillgrid()
    {

        string User = "";
        if (Session["user_type"].ToString() == "TST")
        {
            //User = Session["User_Code"].ToString();
            User = "%";
        }
        else
        {
            User = "%";
        }

        DataSet ds = clsGeneral.getWorkProposalsearch(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtWorkcode.Text, ddlIP.SelectedValue, User,ddlstatus.SelectedValue);
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            btnexp.Visible = true;
            BtnPrint.Visible = true;
        }
        else
        {
            lblMsg.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            btnexp.Visible = false;
            BtnPrint.Visible = false;
        }
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {

            ((LinkButton)e.Row.FindControl("LinkButton1")).Attributes.Add("onclick", "javascript:GetRowValue('" + DataBinder.Eval(e.Row.DataItem, "intWorkid") + "')");
        }
    }
    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {

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

        ddlWorkActivity.Items.Clear();

        if (ddlAreaofWork.SelectedIndex != 0)
        {
            //Gen.getWorkActivitiesByProjects(ddlWorkActivity, ddlAreaofWork.SelectedValue, ddlProject.SelectedValue);
            Gen.getWorkActivities(ddlWorkActivity, ddlAreaofWork.SelectedValue);
        }
        else
        {
            ddlWorkActivity.Items.Insert(0, "--Select--");

        }
    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

        getworksbyPojects();
    }

    void getworksbyPojects()
    {

        if (ddlProject.SelectedIndex != 0)
        {
            //Gen.getAreaofWorksByPrjid(ddlAreaofWork, ddlProject.SelectedValue);
            Gen.getAreaOfWork(ddlAreaofWork);
        }
        else
        {
            ddlAreaofWork.Items.Insert(0, "--Select--");

        }
    }


    protected void btnexp_Click(object sender, EventArgs e)
    {
        string User = "";
        if (Session["user_type"].ToString() == "TST")
        {
            //User = Session["User_Code"].ToString();
            User = "%";
        }
        else
        {
            User = "%";
        }

        ExportGridToExcel();
    }

    private void ExportGridToExcel()
    {
        grdDetails.Columns[0].Visible = false;
        Response.Clear();
        Response.Buffer = true;
        Response.ClearContent();
        Response.ClearHeaders();
        Response.Charset = "";
        string FileName = "Work Proposal LookUp" + DateTime.Now + ".xls";
        StringWriter strwritter = new StringWriter();
        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
        //grdDetails.HeaderRow.Cells[0].FindControl("LinkButton1").Visible = false;
        grdDetails.GridLines = GridLines.Both;
        grdDetails.HeaderStyle.Font.Bold = true;
        grdDetails.RenderControl(htmltextwrtter);
        Response.Write(strwritter.ToString());
        Response.End();

    }


}

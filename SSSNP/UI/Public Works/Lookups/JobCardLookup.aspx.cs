using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;

public partial class JobCardLookup : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion
    DataSet ds;


    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            //fillgrid();
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
        RadLookup.Rebind();

    }
    void fillgrid()
    {
       
        if (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "TST")
        {
            ds = clsGeneral.getBeneficiaryLookupsearchAll(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(),ddl_group_type.SelectedValue,ddl_group_code.SelectedValue);
        }
        else
        {
            ds = clsGeneral.getBeneficiaryLookupsearch(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);
        }
            
        if (ds.Tables[0].Rows.Count > 0)
        {
            lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
         //   grdDetails.DataSource = ds.Tables[0];
          //  grdDetails.DataBind();

            RadLookup.DataSource = ds.Tables[0];
            RadLookup.Visible = true;
            //BtnPrint.Visible = true;
           // btnExportoexcel.Visible = true;
        }
        else
        {
            lblMsg.Text = "No Records found.";
            RadLookup.DataSource = null;
            RadLookup.Visible = false;
            // grdDetails.DataSource = ds.Tables[0];
            // grdDetails.DataBind();
            //BtnPrint.Visible = false;
            // btnExportoexcel.Visible = false;
        }
    }

    //protected void btnExportoexcel_Click(object sender, EventArgs e)
    //{
    //   // ExportGridToExcel();
    //}

    //private void ExportGridToExcel()
    //{

        
    //    if (Session["userlevel"].ToString() == "3" && Session["user_type"].ToString().Trim() == "TST")
    //    {
    //        ds = clsGeneral.getBeneficiaryLookupsearchAll(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);
    //    }
    //    else
    //    {
    //        ds = clsGeneral.getBeneficiaryLookupsearch(ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtName.Text, txtEnrollment.Text, Session["User_Code"].ToString(), ddl_group_type.SelectedValue, ddl_group_code.SelectedValue);
    //    }

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        Response.Clear();
    //        Response.Buffer = true;
    //        Response.ClearContent();
    //        Response.ClearHeaders();
    //        Response.Charset = "";
    //        string FileName = "Beneficiary Enrollment" + DateTime.Now + ".csv";
    //        StringWriter strwritter = new StringWriter();
    //        HtmlTextWriter htmltextwrtter = new HtmlTextWriter(strwritter);
    //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
    //        strwritter.Write(ds.Tables[0].ToCSV());
    //        Response.ContentType = "application/vnd.ms-excel";
    //        Response.AddHeader("Content-Disposition", "attachment;filename=" + FileName);
    //        //Response.AddHeader("Content-Length", strwritter.le.Length.ToString());

            

    //        Response.Write(strwritter.ToString());
    //        Response.End();

    //    }



        

    //}
    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {

         
        if (ddl_group_type.SelectedValue == "Group")
        {
            //tr_group_code.Style.Add("display", "block");
            tr_group_code.Visible = true;

        }
        if (ddl_group_type.SelectedValue == "CST")
        {
            //tr_group_code.Style.Add("display", "none");
            tr_group_code.Visible = false;
            ddl_group_code.SelectedValue = "";
        }

    }
    protected void BtnClear_Click(object sender, EventArgs e)
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

    protected void RadLookup_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        fillgrid();

    }
        
    protected void RadLookup_ItemDataBound(object sender, GridItemEventArgs e)
    {

        if ((e.Item.ItemType == GridItemType.Item) || (e.Item.ItemType == GridItemType.AlternatingItem))
        {
            GridDataItem item = (GridDataItem)e.Item;
            int id = 0;

            int.TryParse(e.Item.OwnerTableView.DataKeyValues[e.Item.ItemIndex]["intBenid"].ToString(), out id);
            ((LinkButton)item.FindControl("lnkLookupID")).Attributes.Add("onclick", "javascript:GetRowValue('" + id.ToString () + "')");


        }

    }
}

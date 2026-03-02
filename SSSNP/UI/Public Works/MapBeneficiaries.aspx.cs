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

public partial class MapBeneficiaries : System.Web.UI.Page
{
    #region Declaration
    General clsGeneral = new General();
    General Gen = new General();
    #endregion
    
    string strStatus;
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
            
            //Total to see all beneficiaries
            strStatus = "0";

            
            if (System.Convert.ToInt32(strStatus) > 0)
            {

                if (strStatus != "")
                {
                    hdnStat.Value = strStatus;
                    fillgrid();
                    switch (strStatus)
                    {
                        case "0":
                            lblPageTitle.InnerText = "Total Beneficiaries";
                            break;
                        case "1":
                            lblPageTitle.InnerText = "Pending Beneficiaries";
                            break;
                        case "2":
                            lblPageTitle.InnerText = "Confirmed Beneficiaries";
                            break;
                        case "3":
                            lblPageTitle.InnerText = "Dropped Beneficiaries";
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

    protected void BtnToggleBenList_Click(object sender, EventArgs e)
    {
        bool bRadGrid1Visible = RadGrid1.Visible;
        RadGrid1.Visible = !bRadGrid1Visible;
    }

    void fillgrid()
    {
        DataSet ds = Gen.DrillDownAPI(Session["User_Code"].ToString().Trim(), "0", txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
        

        DataTable dt = ds.Tables[0];
        RadGrid1.DataSource = dt;
        RadGrid1.DataBind();        
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        CleanAlertMessages();
        CleanAlertMessages();
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }


    protected void grdDetails_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        DataSet ds = Gen.DrillDownAPI(Session["User_Code"].ToString().Trim(), "0", txtBenificiaryName.Text, ddlState.SelectedValue, ddlCounties.SelectedValue, ddlPayam.SelectedValue, ddlBoma.SelectedValue, txtEnrollmentNo.Text);
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
            
        }

        if (e.Item is GridPagerItem)
        {
            int iTotalRows = ((DataTable)RadGrid1.DataSource).Rows.Count;
            
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


    protected void RadGrid1_ItemCommand(object sender, GridCommandEventArgs e)
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

    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {
        getworksbyPojects();
    }

    void getworksbyPojects()
    {
    }

    

    protected void CleanAlertMessages()
    {
        lblmsg0.Text = "";
        lblMsg.Text = "";
        LblMessage2.Text = "";
        Failure.Visible = false;
        success.Visible = false;
    }

}

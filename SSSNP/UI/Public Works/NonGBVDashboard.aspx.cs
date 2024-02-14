using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.Entity.FilterCriteria;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Web.UI;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class NonGBVDashboard : System.Web.UI.Page
    {
        General Gen = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count <= 0) { Response.Redirect("../../frmUserLogin.aspx"); }
            if (!Page.IsPostBack)
            {
                rgGRByStatus.Rebind();
            }


        }
        protected void rgGRByStatus_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByStatus();
        }

        protected void rgGRByStatus_ItemDataBound(object sender, GridItemEventArgs e)
        {
            //if (e.Item is GridDataItem)
            //{
            //    int rowCounter;
            //    Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            //    rowCounter = RadGrid1.MasterTableView.PageSize * RadGrid1.MasterTableView.CurrentPageIndex;
            //    lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

            //    HyperLink hyperLinkView = e.Item.FindControl("lnkUpdate") as HyperLink;
            //    //hyperLinkView.Target = "_blank";
            //    hyperLinkView.NavigateUrl = "IPBeneficiaryRegistration1.aspx?BenTempid=" + ((System.Data.DataRowView)e.Item.DataItem).Row["intBenTempid"];
            //}
        }

        protected void rgGRByType_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByType();
        }

        protected void rgGRByType_ItemDataBound(object sender, GridItemEventArgs e)
        {
            
        }

        protected void rgGRByTypeDelayed_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByTypeDelayed();
        }

        protected void rgGRByTypeDelayed_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgGRByLocation_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByLocation();
        }

        protected void rgGRByLocation_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgGRByLocationResTimeAVG_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByLocatioResTimeAVG();
        }

        protected void rgGRByLocationResTimeAVG_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        private void PopulateGridByLocatioResTimeAVG()
        {
            List<NonGBVByLocation> list = new List<NonGBVByLocation>();

            list = SSSNPContext.SSSNPManager.GetNonGBVDashboardByLocationResTimeAVG();

            rgGRByLocationResTimeAVG.DataSource = list;
        }

        private void PopulateGridByLocation()
        {
            List<NonGBVByLocation> list = new List<NonGBVByLocation>();

            list = SSSNPContext.SSSNPManager.GetNonGBVDashboardByLocation();

            rgGRByLocation.DataSource = list;
        }

        private void PopulateGridByTypeDelayed()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetNonGBVDashboardTypeDelayedCount();

            rgGRByTypeDelayed.DataSource = list;
        }

        private void PopulateGridByType()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetNonGBVDashboardTypeCount();

            rgGRByType.DataSource = list;
        }

        private void PopulateGridByStatus()
        {
            List<LabelValue> list = new List<LabelValue>();
            list = SSSNPContext.SSSNPManager.GetNonGBVDashboardStatusCount();

            rgGRByStatus.DataSource = list;
        }

        private NonGBVLandingCriteria CreateFilterCriteria()
        {
            NonGBVLandingCriteria result = new NonGBVLandingCriteria();


            return result;
        }

        protected void rgGRByStatus__ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

    }
}
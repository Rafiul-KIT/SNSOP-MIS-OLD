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
    public partial class GBVDashboard : System.Web.UI.Page
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

        private void PopulateGridByStatus()
        {
            List<LabelValue> list = new List<LabelValue>();
            list = SSSNPContext.SSSNPManager.GetGBVDashboardStatusCount();

            rgGRByStatus.DataSource = list;
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



        protected void rgGRByType_ItemDataBound(object sender, GridItemEventArgs e)
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

         protected void rgTypesOfReferral_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridTypesOfReferral();
        }

        protected void rgTypesOfReferral_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgSentToReferralService_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridSentToReferralService();
        }

        protected void rgSentToReferralService_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgGBVPerpetratorProject_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridGBVPerpetratorProject();
        }

        protected void rgGBVPerpetratorProject_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        private void PopulateGridByLocatioResTimeAVG()
        {
            List<GBVByLocation> list = new List<GBVByLocation>();

            list = SSSNPContext.SSSNPManager.GetGBVDashboardByLocationResTimeAVG();

            rgGRByLocationResTimeAVG.DataSource = list;
        }

        private void PopulateGridByLocation()
        {
            List<GBVByLocation> list = new List<GBVByLocation>();

            list = SSSNPContext.SSSNPManager.GetGBVDashboardByLocation();

            rgGRByLocation.DataSource = list;
        }

        private void PopulateGridTypesOfReferral()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetTypesOfReferral();

            rgTypesOfReferral.DataSource = list;
        }

        private void PopulateGridSentToReferralService()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetSentToReferralService();

            rgSentToReferralService.DataSource = list;
        }
        private void PopulateGridGBVPerpetratorProject()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetGBVPerpetratorProject();

            rgGBVPerpetratorProject.DataSource = list;
        }

    }
}
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
    public partial class GrievancesDashboard : System.Web.UI.Page
    {
        General Gen = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count <= 0) { Response.Redirect("../../frmUserLogin.aspx"); }
            if (!Page.IsPostBack)
            {

            }
        }

        protected void btnSystemReport_Click(object sender, EventArgs e)
        {
            Response.Redirect(@"../../ViewAttachmentHandler.aspx?AttachmentType=8");
        }

        protected void rgGRByCountyAll_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByCountyAll();
        }

        protected void rgGRByCountyAll_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgGRByStatusAll_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridByStatusAll();
        }

        protected void rgGRByStatusAll_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        protected void rgGRAvgToCloseByCountyAll_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGridAvgToCloseByCountyAll();
        }

        protected void rgGRAvgToCloseByCountyAll_ItemDataBound(object sender, GridItemEventArgs e)
        {

        }

        private void PopulateGridByCountyAll()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetGBVDashboardByCountyAll();

            rgGRByCountyAll.DataSource = list;
        }



        private void PopulateGridByStatusAll()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetGBVDashboardByStatusAll();

            rgGRByStatusAll.DataSource = list;
        }

        private void PopulateGridAvgToCloseByCountyAll()
        {
            List<LabelValue> list = new List<LabelValue>();

            list = SSSNPContext.SSSNPManager.GetGBVDashboardAvgToCloseByCountyAll();

            rgGRAvgToCloseByCountyAll.DataSource = list;
        }
        
    }
}
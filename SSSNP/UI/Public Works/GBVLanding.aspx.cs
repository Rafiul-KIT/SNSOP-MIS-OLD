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
    public partial class GBVLanding : System.Web.UI.Page
    {
        General Gen = new General();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session.Count <= 0) { Response.Redirect("../../frmUserLogin.aspx"); }
            if (!Page.IsPostBack)
            {
                PopulateDropDownList();

                RadGridLanding.Rebind();
            }

        }
        protected void RadGridLanding_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            PopulateGrid();
        }
        private void PopulateGrid()
        {

            List<GBVMainExtended> list = new List<GBVMainExtended>();
            GBVLandingCriteria searchCriteria = CreateFilterCriteria();

            list = SSSNPContext.SSSNPManager.GetGBVMainExtended(searchCriteria);

            RadGridLanding.DataSource = list;

            if (list.Count == 0) { RadGridLanding.Visible = false; }
            else
            {
                RadGridLanding.Visible = true;
            }


        }
        private GBVLandingCriteria CreateFilterCriteria()
        {
            GBVLandingCriteria result = new GBVLandingCriteria();
            result.StatusID = SSSNPHelper.TryParseInt(ddlStatus.SelectedValue.ToLower()).ToString();
            result.StateID = SSSNPHelper.TryParseInt(ddlState.SelectedValue.ToLower()).ToString();
            result.CountyID = SSSNPHelper.TryParseInt(ddlCounty.SelectedValue.ToLower()).ToString();
            result.PayamID = SSSNPHelper.TryParseInt(ddlPayam.SelectedValue.ToLower()).ToString();
            result.BomaID = SSSNPHelper.TryParseInt(ddlBoma.SelectedValue.ToLower()).ToString();

            return result;


        }
        protected void btnEditDetail_Click(object sender, EventArgs e)
        {
            RadButton btnEdit = (RadButton)sender;
            GridDataItem row = (GridDataItem)btnEdit.NamingContainer;

            int id = SSSNPHelper.TryParseInt(row.GetDataKeyValue("GBVMainID").ToString());

            if (id != 0)
            {
                Response.Redirect($"GBVManagement.aspx?oType=2&id={id}");
            }
        }
        private void PopulateDropDownList()
        {


            Gen.getStates(ddlState);

            List<LK_GBVStatus> lk_status = SSSNPContext.SSSNPManager.GetNonGBVStatus();
            lk_status.Insert(0, new LK_GBVStatus() { StatusName = "--Select--", StatusID = 0 });
            ddlStatus.DataSource = lk_status;
            ddlStatus.DataTextField = "StatusName";
            ddlStatus.DataValueField = "StatusID";
            ddlStatus.DataBind();

        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetCounties();
        }
        void getPayams()
        {
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();
            if (ddlCounty.SelectedIndex != 0)
            {
                Gen.getPayams(ddlPayam, ddlCounty.SelectedValue);

                ddlPayam.Enabled = true;
                ddlBoma.Items.Insert(0, "--Select--");
                ddlBoma.Enabled = false;
            }
            else
            {
                ddlPayam.Items.Insert(0, "--Select--");
                ddlPayam.Enabled = false;
                ddlBoma.Items.Insert(0, "--Select--");
                ddlBoma.Enabled = false;

            }
        }
        void getBomas()
        {
            ddlBoma.Items.Clear();
            if (ddlPayam.SelectedIndex != 0)
            {
                Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
                ddlBoma.Enabled = true;
            }
            else
            {
                ddlBoma.Items.Insert(0, "--Select--");
                ddlBoma.Enabled = false;
            }
        }
        private void GetCounties()
        {
            ddlCounty.Items.Clear();
            ddlPayam.Items.Clear();
            ddlBoma.Items.Clear();
            if (ddlState.SelectedIndex != 0)
            {
                Gen.getCounties(ddlCounty, ddlState.SelectedValue);
                ddlCounty.Enabled = true;
                ddlPayam.Items.Insert(0, " --Select--");
                ddlBoma.Items.Insert(0, " --Select--");
            }
            else
            {
                ddlCounty.Enabled = false;
                ddlCounty.Items.Insert(0, " --Select--");
                ddlPayam.Items.Insert(0, " --Select--");
                ddlPayam.Enabled = false;
                ddlBoma.Items.Insert(0, " --Select--");
                ddlBoma.Enabled = false;

            }
        }
        protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
        {

            getBomas();
        }
        protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
        {
            getPayams();
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            RadGridLanding.Rebind();
        }
        protected void BtnReset_Click(object sender, EventArgs e)
        {
            ddlState.Items.Clear();

            ddlCounty.Items.Clear();
            ddlCounty.Items.Add(new ListItem() { Value = "0", Text = "--Select--" });
            ddlCounty.Enabled = false;

            ddlPayam.Items.Clear();
            ddlPayam.Items.Add(new ListItem() { Value = "0", Text = "--Select--" });
            ddlPayam.Enabled = false;

            ddlBoma.Items.Clear();
            ddlBoma.Items.Add(new ListItem() { Value = "0", Text = "--Select--" });
            ddlBoma.Enabled = false;

            ddlStatus.Items.Clear();

            PopulateDropDownList();
            RadGridLanding.Rebind();


        }
        protected void RadGridLanding_ItemDataBound(object sender, GridItemEventArgs e)
        {

            if (e.Item is GridDataItem)
            {
                GridDataItem item = (GridDataItem)e.Item;
                RadButton btEdit = item.FindControl("btnEditDetail") as RadButton;

                Image img = item.FindControl("imgExpired") as Image;

                if (item.GetDataKeyValue("Expired").ToString() == "1")
                {
                    img.ImageUrl = "~/Resource/Images/successRed.png";

                }
                else
                {
                    img.ImageUrl = "~/Resource/Images/success.png";
                }

                //if (item.GetDataKeyValue("Status").ToString() == "5")
                //{
                //    btEdit.Enabled  = false;
                //}
            }


        }
        protected void btnBackNonGBV_Click(object sender, EventArgs e)
        {
            Response.Redirect($"GBVManagement.aspx?oType=1");
        }

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            RadGridLanding.ExportSettings.FileName = "GBV_Report" + DateTime.Now.ToString("yyyyMMdd");
            RadGridLanding.ExportSettings.ExportOnlyData = false;
            RadGridLanding.ExportSettings.IgnorePaging = true;
            RadGridLanding.ExportToCsv();
        }
    }
}
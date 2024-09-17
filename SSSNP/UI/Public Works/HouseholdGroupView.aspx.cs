using System;
using System.Data;
using System.Web.UI;

namespace SSSNP.UI.Public_Works
{
    public partial class HouseholdGroupView : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Load some dummy data to populate the GridView for UI testing
                LoadDummyData();
            }
        }

        // Method to load dummy data into the GridView
        private void LoadDummyData()
        {
            // Creating a dummy data table for the GridView
            DataTable dt = new DataTable();
            dt.Columns.Add("GroupID");
            dt.Columns.Add("GroupName");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            // Adding some dummy rows
            dt.Rows.Add("1", "1 JUBANYAKURON BLOCK 2 /SOUTH", "5.12345", "35.67890");
            dt.Rows.Add("2", "1 PIBORMANYIRANY", "5.23456", "35.78901");
            dt.Rows.Add("3", "1 PIBORHAI LANGACHOT", "5.34567", "35.89012");

            // Bind the dummy data to the GridView
            gvData.DataSource = dt;
            gvData.DataBind();
        }
    }
}

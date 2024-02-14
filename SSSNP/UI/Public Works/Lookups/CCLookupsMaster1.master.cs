using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UI_Public_Works_Lookups_CCLookupsMaster1 : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string section = "";
        if (Session.Count > 0)
        {
            //section=Session["UserLevel"].ToString();

        }
    }
}

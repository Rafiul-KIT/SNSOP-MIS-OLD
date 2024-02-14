using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class AddCourses : System.Web.UI.Page
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

        if (!IsPostBack)
        {
            for (int i = 1; i < 12; i++)
            {
                ddlDuration.Items.Add(i.ToString());
            }
        }
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            //filldetails();
            
           


                BtnSave1.Text = "Update";
        }
    }
    

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave_Click(object sender, EventArgs e)
    {

    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }
}

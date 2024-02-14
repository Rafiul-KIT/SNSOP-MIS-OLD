using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class PublicWorksApproval : System.Web.UI.Page
{
    General Gen = new General();
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        fillgrid();
       
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            //filldetails();

            BtnSave1.Text = "Update";
        }
    }

    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            GridView grdDetails123 = (GridView)e.Row.FindControl("grdDetails123");


            DataSet dsnew = new DataSet();

            dsnew = Gen.GetPW1(Convert.ToString(DataBinder.Eval(e.Row.DataItem, "intMobilization")));
            if (dsnew.Tables[0].Rows.Count > 0)
            {

                grdDetails123.DataSource = dsnew.Tables[0];

                grdDetails123.DataBind();


            }
        }

    }
    void fillgrid()
    {
        DataSet ds = Gen.GetPW();
        if (ds.Tables[0].Rows.Count > 0)
        {
            //lblMsg.Text = ds.Tables[0].Rows.Count.ToString() + " Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
        }
        else
        {
            //lblMsg.Text = "No Records found.";
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
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

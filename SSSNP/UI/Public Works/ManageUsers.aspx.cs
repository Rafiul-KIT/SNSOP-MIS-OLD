#region Using/Inherits Classess
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
#endregion

#region Amendment History Begin
//Form Name : Manage Users
//Purpose : To Manage user Detals to view Password and Reset Password
//Tables used : td_TstTeamDet,tbl_Users
//Stored procedures Used : sp_getusertype,sp_getCounties,sp_getPayams,getTSTbyID,deleteTST,insrtTST,InsertUpdateAttachements_Sp.sp_Designations
#endregion Amendment History End

public partial class UI_Public_Works_ManageUsers : System.Web.UI.Page
{
    #region reference class calling
    General Gen = new General();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        grdDetails.Visible = true;
        #region Is not Postback Statement
        if (!IsPostBack)
        {
            GetUsers();
            GetUsersData();
        }
        #endregion 
    }
    #endregion

    #region User Defined functions and Filling Dropdown details and Grid Details

    // To Fill User's User Type details
    protected void GetUsers()
    {
        DataSet ds = new DataSet();
        ds = Gen.GetUsers();
        ddltype.DataSource = ds.Tables[0];
        ddltype.DataTextField = "User_type";
        ddltype.DataValueField = "User_type";
        //ddltype.DataValueField = "intUserid";
        ddltype.DataBind();
        ddltype.Items.Insert(0, "--Select--");
    }

    // To Fill Users Details
    protected void GetUsersData()
    {
        DataSet ds = new DataSet();
        ds = Gen.GetUsersdata(ddltype.SelectedItem.Text.ToString());
        if (ds.Tables[0].Rows.Count > 0)
        {
            grdDetails.DataSource = ds.Tables[0];
            grdDetails.DataBind();
            lblMsg.Text = "Total Records:" + ds.Tables[0].Rows.Count;
        }
        else
        {
            lblMsg.Text = "No Data Found";
            grdDetails.DataSource = "";
        }
    }
    #endregion

    #region Button Click functionalities Search, Update, Clear
    protected void BtnSave1_Click(object sender, EventArgs e)
    {
        GetUsersData();
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("ManageUsers.aspx");
    }
    //protected void btnreset_Click(object sender, EventArgs e)
    //{

    //    Button button1 = (Button)sender;

    //    GridViewRow row = (GridViewRow)button1.NamingContainer;
    //    //Label lblmsg = (Label)row.FindControl("lblMsg");
    //    HiddenField hdfID = (HiddenField)row.FindControl("hdf1");
    //    int i = 0;
    //    i = Gen.UpdateUseridPassword(hdfID.Value);
    //    //GetUsersData();
    //    if (i > 0)
    //    {
    //        lblMsg.Text = "Password Reset Sucessfully";
    //        //GetUsersData();
    //        grdDetails.Visible = false;
    //        ddltype.SelectedIndex = 0;
    //    }
    //}

    // User Type Dropdown Selected Index Changed
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    #endregion

    #region Gridview Events
    protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if ((e.Row.RowType == DataControlRowType.DataRow))
        //{
        //    string item = e.Row.Cells[0].Text;
        //    foreach (Button button in e.Row.Cells[4].Controls.OfType<Button>())
        //    {
        //        if (button.CommandName == "Reset")
        //        {
        //            button.Attributes["onclick"] = "if(!confirm('Do you want to Reset " + item + "?')){ return false; };";
        //        }
        //    }
        //}
    }
  
    protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdDetails.PageIndex = e.NewPageIndex;
        GetUsersData();
    }
    #endregion
}

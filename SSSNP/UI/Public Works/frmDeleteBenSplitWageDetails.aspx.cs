using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class frmDeleteBenSplitWageDetails : System.Web.UI.Page
{
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        //btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {
            FillDetails();
        } 
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            //FillDetails();

            //BtnSave1.Text = "Update";

        }
    }
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetBenficiaryfullDetByID(Request.QueryString[0].ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblhousehead.Text = ds.Tables[0].Rows[0]["NameofHousehold"].ToString();
            lblgender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
            lblspouse.Text = ds.Tables[0].Rows[0]["NameofSpouse"].ToString();
            lblstatename.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblcounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblpayamname.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            lblboma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
          
            lblipname.Text = ds.Tables[0].Rows[0]["IPName"].ToString();
            lblenrollment.Text = ds.Tables[0].Rows[0]["EnrollmentNo"].ToString();
            lblbenhhcode.Text = ds.Tables[0].Rows[0]["household_number"].ToString();

            if (ds.Tables[0].Rows[0]["PhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoURL"].ToString().Trim() != null)
            {
                Image1.ImageUrl = ds.Tables[0].Rows[0]["PhotoURL"].ToString();
            }
           
            
                if (ds.Tables[1].Rows.Count > 0)
                {
                    grdDetails.DataSource = ds.Tables[1];
                    grdDetails.DataBind();
                }
                else
                {
                    grdDetails.DataSource = null;
                    grdDetails.DataBind();
                }
            
           
                lblattendance.Text = ds.Tables[2].Rows[0]["Attendance"].ToString();
            
            
                if (ds.Tables[3].Rows.Count > 0)
                {
                    GridView1.DataSource = ds.Tables[3];
                    GridView1.DataBind();
                }
                else
                {
                    GridView1.DataSource = null;
                    GridView1.DataBind();
                }

                if (ds.Tables[4].Rows.Count > 0)
                {
                    GridView2.DataSource = ds.Tables[4];
                    GridView2.DataBind();
                }
                else
                {
                    GridView2.DataSource = null;
                    GridView2.DataBind();
                }
               
                if (ds.Tables[4].Rows.Count > 0)
                {
                    if (ds.Tables[3].Rows.Count == ds.Tables[4].Rows.Count)
                    {
                        BtnDeleteTransaction.Visible = false;
                        BtnDelete.Visible = false;
                        Failure.Visible = true;
                        lblmsg0.Text = "You cannot delete,beneficiary received all request payments";
                    }
                }

                if (ds.Tables[6].Rows.Count > 0)
                {
                    GrdPendingWages.DataSource = ds.Tables[6];
                    GrdPendingWages.DataBind();
                }
                else
                {
                    GrdPendingWages.DataSource = null;
                    GrdPendingWages.DataBind();
                }
                if (ds.Tables[6].Rows.Count == 0)
                {
                    BtnDeleteTransaction.Visible = false;
                    BtnDelete.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = "You cannot delete,beneficiary received all request payments or no requests";
                }
                else
                {
                    BtnDelete.Visible = true;
                }
                if (lblbenhhcode.Text.Trim() == "")
                {
                    BtnDeleteTransaction.Visible = false;
                    BtnDelete.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = "You can't delete due to old data";
                }
            
        }
    }

    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("frmBenDeletionNew.aspx?s=4");
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        int count = 0;
        for (int iiii = 0; iiii < GrdPendingWages.Rows.Count; iiii++)
        {
            GridViewRow row = GrdPendingWages.Rows[iiii];
            bool isChecked = ((CheckBox)row.FindControl("CheckBox1")).Checked;

            if (isChecked)
            {
                count++;
            }
        }
        if (count == 0)
        {
            lblmsg0.Text = "Please Select at least one request to delete";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;

        }
        else
        {
           // HiddenField hdfreqestids = (HiddenField)e.Row.Cells[1].FindControl("hdfreqestids");
            for (int iii = 0; iii < GrdPendingWages.Rows.Count; iii++)
            {
                GridViewRow row = GrdPendingWages.Rows[iii];
                bool isChecked = ((CheckBox)row.FindControl("CheckBox1")).Checked;
                HiddenField hdfreqestids = (HiddenField)row.FindControl("hdfreqestids");
                if (isChecked)
                {
                    int a = Gen.DeleteBenficiaryWageDetailsNew(Request.QueryString[0].ToString().Trim(),hdfreqestids.Value,   Session["uid"].ToString());

                }
            }
        }
        lblmsg.Text = "Deleted successful";
        success.Visible = true;
        Failure.Visible = false;
        Response.Redirect("DeleteRequestResult.aspx?id=1");
        ////if (Request.QueryString.Count > 0)
        ////{
        ////    int a = Gen.DeleteBenficiaryNewLatest(Request.QueryString[0].ToString().Trim(), Session["uid"].ToString());

        ////    if (a == 1000)
        ////    {


        ////        lblmsg.Text = "Deleted successful";
        ////        success.Visible = true;
        ////        Failure.Visible = false;
        ////        Response.Redirect("DeleteResult.aspx?id=1");
        ////    }
        ////    else
        ////    {
        ////        lblmsg0.Text = "Deletion failed";
        ////        success.Visible = false;
        ////        Failure.Visible = true;
        ////        // clear();

        ////    }
        ////}
        ////else
        ////{
        ////    lblmsg0.Text = "You Can't Delete";
        ////    success.Visible = false;
        ////    Failure.Visible = true;
        ////}
    }
    protected void BtnDeleteTransaction_Click(object sender, EventArgs e)
    {
        //// if (Request.QueryString.Count > 0)
        ////{
        ////int a = Gen.DeleteBenficiaryNewLatestPartiallypaid(Request.QueryString[0].ToString().Trim(), Session["uid"].ToString());

        ////if (a == 1000)
        ////{


        ////    lblmsg.Text = "Deleted successful";
        ////    success.Visible = true;
        ////    Failure.Visible = false;
        ////    Response.Redirect("DeleteResult.aspx?id=1");
        ////}
        ////else
        ////{
        ////    lblmsg0.Text = "Deletion failed";
        ////    success.Visible = false;
        ////    Failure.Visible = true;
        ////    // clear();

        ////}
        ////}
        //// else
        //// {
        ////     lblmsg0.Text = "You Can't Delete";
        ////     success.Visible = false;
        ////     Failure.Visible = true;
        //// }
    }
    protected void GrdPendingWages_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
            HiddenField hdfreqestids = (HiddenField)e.Row.Cells[1].FindControl("hdfreqestids");
            hdfreqestids.Value = DataBinder.Eval(e.Row.DataItem, "intWagesReqBenTransid").ToString();
        }
    
    }
}

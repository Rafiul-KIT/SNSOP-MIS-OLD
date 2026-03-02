using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class frmBenFullDetails : System.Web.UI.Page
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
            lblaname1.Text = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();
            lblacontactno1.Text = ds.Tables[0].Rows[0]["alternate_number"].ToString();
            lblaname2.Text = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
            lblacontactno2.Text = ds.Tables[0].Rows[0]["Secondalternate_number"].ToString();
            lblipname.Text = ds.Tables[0].Rows[0]["IPName"].ToString();
            lblenrollment.Text = ds.Tables[0].Rows[0]["EnrollmentNo"].ToString();
            lblbenhhcode.Text = ds.Tables[0].Rows[0]["household_number"].ToString();
            lblage.Text = ds.Tables[0].Rows[0]["Age"].ToString();
            lblagender1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString();
            lblaage1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString();
            lblagender2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString();
            lblaage2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString();
            if (ds.Tables[0].Rows[0]["PhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoURL"].ToString().Trim() != null)
            {
                Image1.ImageUrl = ds.Tables[0].Rows[0]["PhotoURL"].ToString();
            }
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoURL"].ToString().Trim() != null)
            {
                Image2.ImageUrl = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoURL"].ToString();
            }
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString().Trim() != null)
            {
                Image3.ImageUrl = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString();
            }

            if (ds.Tables[1].Rows.Count > 0)
            {
                grdDetails.DataSource = ds.Tables[1];
                grdDetails.DataBind();
            }
            lblattendance.Text = ds.Tables[2].Rows[0]["Attendance"].ToString();

            if (ds.Tables[3].Rows.Count > 0)
            {
                GridView1.DataSource = ds.Tables[3];
                GridView1.DataBind();
            }
            if (ds.Tables[4].Rows.Count > 0)
            {
                GridView2.DataSource = ds.Tables[4];
                GridView2.DataBind();
            }
            if (ds.Tables[7].Rows.Count > 0)
            {
                GrdAlternate.DataSource = ds.Tables[7];
                GrdAlternate.DataBind();
            }
        }
    }    

}

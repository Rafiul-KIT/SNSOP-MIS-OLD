using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class frmBenPhotoDetails : System.Web.UI.Page
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


        ds = Gen.GetBenViewDet1(Request.QueryString[0].ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {

            //lblJobcard.Text = "SNSDP-PW-" + ds.Tables[0].Rows[0]["intBenid"].ToString();
            lblhousehead.Text = ds.Tables[0].Rows[0]["NameofHousehold"].ToString();
           
            lblaname1.Text = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();
          
            lblahhcode1.Text = ds.Tables[0].Rows[0]["alternate_number"].ToString();
            lblaname2.Text = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
            
            lblahhcode2.Text = ds.Tables[0].Rows[0]["Secondalternate_number"].ToString();
         
            lblbenhhcode.Text = ds.Tables[0].Rows[0]["household_number"].ToString();
            Label20.Text = ds.Tables[0].Rows[0]["thirdalternate_number"].ToString();
            Label22.Text = ds.Tables[0].Rows[0]["Name3rdAlternatePayee"].ToString();

            lblgender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
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
            if (ds.Tables[0].Rows[0]["ThirdAlternatePayeePhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString().Trim() != null)
            {
                Image6.ImageUrl = ds.Tables[0].Rows[0]["ThirdAlternatePayeePhotoURL"].ToString();
            }


        }
    }    

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class frmBenInvoice : System.Web.UI.Page
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


        ds = Gen.GetBenficiaryDetByIDForInvoice(Request.QueryString[0].ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {

            //lblJobcard.Text = "SNSDP-PW-" + ds.Tables[0].Rows[0]["intBenid"].ToString();
            lblhousehead.Text = ds.Tables[0].Rows[0]["NameofHousehold"].ToString();
            lblbentype.Text = ds.Tables[0].Rows[0]["BeneficiaryType"].ToString();
            lblhousesize.Text = ds.Tables[0].Rows[0]["FamilySize"].ToString();
            lblgender.Text = ds.Tables[0].Rows[0]["Gender"].ToString();
            lblage.Text = ds.Tables[0].Rows[0]["Age"].ToString();
            lblspouse.Text = ds.Tables[0].Rows[0]["NameofSpouse"].ToString();
            lblotherhmembers.Text = ds.Tables[0].Rows[0]["NoofAdult"].ToString();
            lblmaleadults.Text = ds.Tables[0].Rows[0]["male_dependants"].ToString();
            lblfemaleadults.Text = ds.Tables[0].Rows[0]["female_dependants"].ToString();
            lbldependents.Text = ds.Tables[0].Rows[0]["NoofDependents"].ToString();
            lblhouseincome.Text = ds.Tables[0].Rows[0]["FamilyIncome"].ToString();
            lblsourceincome.Text = ds.Tables[0].Rows[0]["SOurceofIncome"].ToString();
            lblstatename.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblcounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblpayamname.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            lblboma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            lbladdress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            lblcontact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            lblemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            lbllegalstatus.Text = ds.Tables[0].Rows[0]["LegalStatus"].ToString();
            lblaname1.Text = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();
            lblagender1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString();
            lblaage1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString();
            lblanationalid1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeNationID"].ToString();
            lblacontactno1.Text = ds.Tables[0].Rows[0]["FirstAlternatePayeeMobile"].ToString();
            lblahhcode1.Text = ds.Tables[0].Rows[0]["alternate_number"].ToString();
            lblaname2.Text = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
            lblagender2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString();
            lblaage2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString();
            lblanationalid2.Text = ds.Tables[0].Rows[0]["SecondAlternateNationalID"].ToString();
            lblacontactno2.Text = ds.Tables[0].Rows[0]["SecondAlternatePayeeMobile"].ToString();
            lblahhcode2.Text = ds.Tables[0].Rows[0]["Secondalternate_number"].ToString();
            lblbankname.Text = ds.Tables[0].Rows[0]["NameofBank"].ToString();
            lblaccountno.Text = ds.Tables[0].Rows[0]["AccountNo"].ToString();
            lblipname.Text = ds.Tables[0].Rows[0]["IPName"].ToString();
            lblenrollment.Text = ds.Tables[0].Rows[0]["EnrollmentNo"].ToString();
            lblgroupid.Text = ds.Tables[0].Rows[0]["GroupId"].ToString();
            lbllatitude.Text = ds.Tables[0].Rows[0]["latitude"].ToString();
            lbllong.Text = ds.Tables[0].Rows[0]["longitude"].ToString();
            lblbenhhcode.Text = ds.Tables[0].Rows[0]["household_number"].ToString();

            //if (ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != null)
            //{
            //    string dd = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
            //    dd = dd.Replace(@"\", @"/");
            //    //   dd = dd.Replace("D:/Backups/FOR TESTING/PUBLICWORKSTEST", "http://209.105.242.128/pwtestsunitha");
            //    dd = "http://www.snsdpmis.com/Photo/" + hdfID.Value + "/Household/";
            //    string lblFileName = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
            //    Image1.ImageUrl = dd + "/" + lblFileName.ToString();
            //}
            if (ds.Tables[0].Rows[0]["PhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoURL"].ToString().Trim() != null)
            {
                Image1.ImageUrl = ds.Tables[0].Rows[0]["PhotoURL"].ToString();
            }
            else
            { Image1.ImageUrl = "~/Resource/Images/not-available.jpg"; }
            if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoURL"].ToString().Trim() != null)
            {
                Image2.ImageUrl = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoURL"].ToString();
            }
            else
            { Image2.ImageUrl = "~/Resource/Images/not-available.jpg"; }
            if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString().Trim() != null)
            {
                Image3.ImageUrl = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoURL"].ToString();
            }
            else
            { Image3.ImageUrl = "~/Resource/Images/not-available.jpg"; }

            if (ds.Tables[1].Rows.Count > 0)
            {
                grdDetails.DataSource = ds.Tables[1];
                grdDetails.DataBind();
            }
            lblattendance.Text = ds.Tables[2].Rows[0]["Attendance"].ToString();
            lblSSP.Text = ds.Tables[3].Rows[0]["AmountSSP"].ToString();
            lblUSD.Text = ds.Tables[3].Rows[0]["AmountUSD"].ToString();


            if (ds.Tables[4].Rows.Count > 0)
            {
                GrdAlternate.DataSource = ds.Tables[4];
                GrdAlternate.DataBind();
            }

        }
    }    

}

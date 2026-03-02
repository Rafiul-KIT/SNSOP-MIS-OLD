#region Using/Inherits Classess
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
#endregion

#region Amendment History Begin
//Form Name : Generate Job Card
//Purpose : To Generating JOB Card
//Tables used : tbl_BeneficiaryDet
//Stored procedures Used :GetBenficiaryDetByIDForJobCard
#endregion Amendment History End

public partial class CheckPOITD : System.Web.UI.Page
{

    #region reference class calling and Variable declaration
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";
    #endregion
    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        // Look up Click Function Intialization
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");

        // If Session cleared, Redirect to Login page
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        // Is not Postback Statement
        if (!IsPostBack)
        {

        }
        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            FillDetails();



        }
        #endregion


    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetBenficiaryDetByIDForJobCard(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {

            lblJobcard.Text = "SNSDP-PW-" + ds.Tables[0].Rows[0]["intBenid"].ToString();
            lblHHname.Text = ds.Tables[0].Rows[0]["NameofHousehold"].ToString();
            lblSpouse.Text = ds.Tables[0].Rows[0]["NameofSpouse"].ToString();
            lblRegDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["Created_dt"].ToString()).ToString("dd-MM-yyyy");
            if (ds.Tables[0].Rows[0]["Sex"].ToString() == "M")
            {
                lblGender.Text = "Male";
            }
            else
            {
                lblGender.Text = "Female";
            }
            lblIncome.Text = ds.Tables[0].Rows[0]["FamilyIncome"].ToString();
            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounti.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            lblAddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            lblContactNo.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            lblAge.Text = ds.Tables[0].Rows[0]["Age"].ToString();
            if (ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != null)
            {
                string dd = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
                dd = dd.Replace(@"\", @"/");
          
                string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
                dd = dd.Replace(sssnpdir, sssnpweb);
                dd = dd.Replace("~", sssnpweb);
                string lblFileName = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();

                if (!dd.Contains(lblFileName))
                {
                    dd += "/" + lblFileName;
                }

                Image1.ImageUrl = dd;
            }


        }
    }

    #endregion

    #region Button Click functionalities
    protected void BtnSave_Click(object sender, EventArgs e)
    {

    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    protected void BtnSave1_Click(object sender, EventArgs e)
    {

    }
    #endregion
}

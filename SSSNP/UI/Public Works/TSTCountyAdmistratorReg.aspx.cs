using System;
using System.Collections.Generic;
using System.Linq;
#region Using/Inherits Classess
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
#endregion

#region Amendment History Begin
//Form Name : County Administrator Registration
//Purpose : To Add County Administrator Details
//Tables used : td_CountyAdmDet,tbl_Users,tm_States,tm_Counties
//Stored procedures Used : getCAbyID,CheckUserid,sp_getStates,sp_getCounties
#endregion Amendment History End


public partial class TSTCountyAdmistratorReg : System.Web.UI.Page
{
    #region reference class calling and Declaration Variables
    int delete = 0;
    comFunctions cmf = new comFunctions();
    General Gen = new General();
    DataRow dtrdr;
    DataTable myDtNewRecdr = new DataTable();
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

        #region Is not Postback Statement
        if (!IsPostBack)
        {
            Gen.getStates(ddlState);
            Session["dtMyTabledr"] = createtabledr();
            Session["tmpdrDataTable"] = ((DataTable)Session["dtMyTabledr"]);

            if (Request.QueryString.Count > 0)
            {
                Failure.Visible = false;
                success.Visible = false;
                FillDetails();
                if (Session["userlevel"].ToString() == "1")
                {
                    BtnDelete.Visible = true;
                }
                else
                {
                    BtnDelete.Visible = false;
                }
                BtnSave1.Text = "Update";
            }
        }
        #endregion

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0" ))
        {
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            if (Session["userlevel"].ToString() == "1")
            {
                BtnDelete.Visible = true;
            }
            else
            {
                BtnDelete.Visible = false;
            }
            BtnSave1.Text = "Update";
        }
        #endregion

    }
    #endregion
    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        string ids = "";
        if (Request.QueryString.Count > 0)
        {
            ids = Request.QueryString[0].ToString();
        }
        else
        {
            ids = hdfID.Value;
        }


        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getCAbyID(ids.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {

            txtCAname.Text = ds.Tables[0].Rows[0]["CountyAdmName"].ToString();
            ddlState.SelectedValue = ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["intStateid"].ToString()).Value;
            getcounties();
            ddlCounties.SelectedValue = ddlCounties.Items.FindByValue(ds.Tables[0].Rows[0]["intCountieid"].ToString()).Value;

            txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            txtcontact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            if (ds.Tables[2].Rows.Count > 0)
            {
                txtuser.Text = ds.Tables[2].Rows[0]["User_id"].ToString();
                txtuser.Enabled = false;
                txtpass.Enabled = false;
                txtpass.TextMode = TextBoxMode.SingleLine;
                txtpass.Text = "*****"; //ds.Tables[2].Rows[0]["Password"].ToString();

            }
            if (ds.Tables[1].Rows.Count > 0)
            {
                DataTableReader rdp = new DataTableReader(ds.Tables[1]);
                IDataReader readerp = rdp;


                ((DataTable)Session["tmpdrDataTable"]).Clear();
                ((DataTable)Session["tmpdrDataTable"]).Load(readerp);
                gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                gvpractical0.DataBind();
                gvpractical0.Columns[0].Visible = false;

            }
            else
            {
                gvpractical0.DataSource = null;
                gvpractical0.DataBind();
            }
       


        }
    }

    #endregion

    #region Button Click functionalities Save, Update, Delete, Look up
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    // Save Button Function - Save and Update
    protected void BtnSave_Click(object sender, EventArgs e)
    {

        string ids = "";
        if (Request.QueryString.Count > 0)
        {
            ids = Request.QueryString[0].ToString();
        }
        else
        {
            ids = hdfID.Value;
        }

        Gen.CountyAdmName = txtCAname.Text;
        Gen.intStateid = ddlState.SelectedValue;
        Gen.intCountieid = ddlCounties.SelectedValue;

        Gen.Address = txtaddress.Text;
        Gen.Email = txtemail.Text;
        Gen.ContactNo = txtcontact.Text;

        if (((DataTable)Session["tmpdrDataTable"]).Rows.Count == 0)
        {
            lblmsg0.Text = "Please Add atleast one Member Details. ";
            success.Visible = false;
            Failure.Visible = true;
            return;
        }


        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {

          

            int i = Gen.insrtCA("0", Session["uid"].ToString(), txtuser.Text.ToString(), txtpass.Text.ToString());
            if (i != 999)
            {

                if (((DataTable)Session["tmpdrDataTable"]).Rows.Count > 0)
                {

                    for (int m = 0; m < ((DataTable)Session["tmpdrDataTable"]).Rows.Count; m++)
                    {
                        if (((DataTable)Session["tmpdrDataTable"]).Rows[m]["intMemid"].ToString().Trim() == "0")
                        {
                         
                            ((DataTable)Session["tmpdrDataTable"]).Rows[m]["intCPBid"] = Convert.ToString(i);
                        }
                    }

                    GetNewRectoInsertdr();
                    int statuspr = Gen.bulkInsertdrCouncilNewDetails(myDtNewRecdr);

                }


                lblmsg.Text = "Added Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
            }
        }
        else
        {
            int i = Gen.insrtCA(ids.ToString(), Session["uid"].ToString(), txtuser.Text.ToString(), txtpass.Text.ToString());

            if (i != 999)
            {


                if (((DataTable)Session["tmpdrDataTable"]).Rows.Count > 0)
                {

                    for (int m = 0; m < ((DataTable)Session["tmpdrDataTable"]).Rows.Count; m++)
                    {
                        if (((DataTable)Session["tmpdrDataTable"]).Rows[m]["intMemid"].ToString().Trim() == "0")
                        {
                            ((DataTable)Session["tmpdrDataTable"]).Rows[m]["intCPBid"] = Convert.ToString(hdfID.Value);
                        }
                    }

                    GetNewRectoInsertdr();
                    int statuspr = Gen.bulkInsertdrCouncilNewDetails(myDtNewRecdr);

                }



                lblmsg.Text = "Updated Successfully..!";
                success.Visible = true;
                Failure.Visible = false;
                clear();
            }
            else
            {
                lblmsg0.Text = "Already Exist. ";
                success.Visible = false;
                Failure.Visible = true;
                //lblmsg.Text = "Added Successfully..!";
            }
        }
    }

    // Delete Button Function
    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteCA(hdfID.Value);

        if (i == 1)
        {
            lblmsg.Text = "Deleted Successfully.";
            success.Visible = true;
            Failure.Visible = false;
            clear();
        }
        else if (i == 3)
        {
            lblmsg0.Text = "Not Possible.";
            success.Visible = false;
            Failure.Visible = true;
            clear();
        }
        else
        {
            lblmsg0.Text = "Please contact to Administrator.";
            success.Visible = false;
            Failure.Visible = true;
            clear();
        }

    }
    // Clear Button Function
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("TSTCountyAdmistratorReg.aspx");

    }

    #endregion

    #region Clear Function

    void clear()
    {

        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;
        ddlState.SelectedIndex = 0;
     
        txtCAname.Text = "";
        ddlCounties.SelectedIndex = 0;        
        txtaddress.Text = "";
        txtemail.Text = "";
        txtcontact.Text = "";
        txtuser.Text = "";
        txtpass.Text = "";
        ((DataTable)Session["tmpdrDataTable"]).Clear();
        gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
        gvpractical0.DataBind();
        txtuser.Enabled = true;
        txtpass.Enabled = true;
       
       
    }

   
    #endregion

    #region Dropdown Binding Functionalities
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();

        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");


        }
    }
    protected void ddlState_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    #endregion

    #region Bulk Insert Functionalities

    // While Click on Add Button Values Adding in Session Table
    private void fillTrademappingGriddr(DataTable tmpTabledr, string MemType, string authorised, string designation, string gender, string mobile, string email2, string Created_by)
    {

        dtrdr = tmpTabledr.NewRow();
        dtrdr["intMemid"] = "0";
        dtrdr["new"] = "0";
        dtrdr["MemType"] = MemType.Trim();
        dtrdr["intCPBid"] = "0";
        dtrdr["AuthorizedPerson"] = authorised.Trim();
        dtrdr["Designation"] = designation.Trim();
        dtrdr["MobileNo"] = mobile.Trim();
        dtrdr["Email"] = email2.Trim();
        dtrdr["Gender"] = gender.Trim();


        tmpTabledr.Rows.Add(dtrdr);


    }

    // Bulk Insert table Initialization
    protected DataTable createtabledr()
    {
        Session["dtMyTabledr"] = new DataTable("drDetails");
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("new", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("AuthorizedPerson", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Designation", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("MobileNo", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Email", typeof(string));

        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Gender", typeof(string));



        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Created_by", typeof(string));

        ((DataTable)Session["dtMyTabledr"]).Columns.Add("MemType", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intCPBid", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intMemid", typeof(string));


        return ((DataTable)Session["dtMyTabledr"]);
    }

    // Grid View Row and from Session Deleting
    protected void gvpractical0_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (BtnSave1.Text == "Save")
            {

                ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
                this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                this.gvpractical0.DataBind();

             


            }
            else
            {
                if (((DataTable)Session["tmpdrDataTable"]).Rows[e.RowIndex]["new"].ToString().Trim() == "")
                {
                    int i = Gen.DeleteCouncilMemsInvolved(gvpractical0.DataKeys[e.RowIndex].Values["intMemid"].ToString());
                    ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();

                    txtauthorised.Text = ""; txtdesignation.Text = ""; RadioButtonList1.SelectedIndex = 0; txtmobile.Text = ""; txtemail2.Text = "";

                }
                else
                {
                    ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
                    this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                    this.gvpractical0.DataBind();

                    txtauthorised.Text = ""; txtdesignation.Text = ""; RadioButtonList1.SelectedIndex = 0; txtmobile.Text = ""; txtemail2.Text = "";

                }

            }
        }
        catch (Exception ex)
        {
         

        }
        finally
        {
        }
    }
    // Session table Declaration
    protected void GetNewRectoInsertdr()
    {
        myDtNewRecdr = (DataTable)Session["tmpdrDataTable"];
        DataView dvdr = new DataView(myDtNewRecdr);
        dvdr.RowFilter = "new = 0";
        myDtNewRecdr = dvdr.ToTable();
    }
    // While Click on Add Button 
    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        try
        {

            fillTrademappingGriddr((DataTable)Session["tmpdrDataTable"], "CA", txtauthorised.Text, txtdesignation.Text, RadioButtonList1.SelectedValue, txtmobile.Text, txtemail2.Text, Session["uid"].ToString());

            this.gvpractical0.DataSource = ((DataTable)Session["tmpdrDataTable"]);
            this.gvpractical0.DataBind();


            txtauthorised.Text = ""; txtdesignation.Text = ""; RadioButtonList1.SelectedIndex = 0; txtmobile.Text = ""; txtemail2.Text = "";



        }
        catch (Exception ex)
        {
            lblmsg.Text = ex.ToString();
        }
        finally
        {

        }



    }
    // Clear Button Calling
    protected void BtnClear0_Click1(object sender, EventArgs e)
    {
        txtauthorised.Text = ""; txtdesignation.Text = ""; RadioButtonList1.SelectedIndex = 0; txtmobile.Text = ""; txtemail2.Text = "";
    }
    #endregion
}

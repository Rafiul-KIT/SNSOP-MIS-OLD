
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
using System.IO;
#endregion

#region Amendment History Begin
//Form Name : Environment management Plan
//Purpose : To Register Environment management Plan
//Tables used : tm_States,tm_Counties,tm_Payams,tm_Bomas,td_ProjectDet,td_IPDetails,EnvironmentManagmentPlan
//Stored procedures Used : sp_getStates,sp_getCounties,sp_getPayams,sp_getBomas,Getprojects,getIpDet,Insert_Update_EnviMan,GetWorkProposalByid
#endregion Amendment History End


public partial class UI_Public_Works_EnvironmentManagmentPlan : System.Web.UI.Page
{
    #region reference class calling and Variable declaration
    General Gen = new General();
    string NOC_File_Path, NOC_File_Type, NOC_File_Name;
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
            Projects();
            Gen.getStates(ddlState);
            Gen.getIpDet(ddlIP);
            Session["FileName"] = "";
            Session["FilePath"] = "";

        }
        #endregion
        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {

            if (Session["user_type"].ToString() == "TST")
            {
              
            }
            else
            {
           
            }

         
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
            if (Session["userlevel"].ToString() == "1")
            {
                
            }
            else
            {
              
            }

        }
        #endregion
        // Upload Files Function Calling
        UploadFile();
    }
    #endregion

    #region Dropdown Binding Functionalities
    private void Projects()
    {
        DataSet ds = new DataSet();
        ds = Gen.Getprojects();

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlProject.DataSource = ds.Tables[0];
            ddlProject.DataTextField = "PrjName";
            ddlProject.DataValueField = "intPrjid";
            ddlProject.DataBind();
            ddlProject.Items.Insert(0, "--Select--");
        }


    }

    protected void ddlProject_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        getBomas();
    }
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }

    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlPayam.Items.Clear();
        if (ddlState.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlState.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlPayam.Items.Insert(0, "--Select--");
            ddlBoma.Items.Insert(0, "--Select--");

        }
    }
    void getPayams()
    {
        ddlPayam.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlPayam, ddlCounties.SelectedValue);
        }
        else
        {
            ddlPayam.Items.Insert(0, "--Select--");
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }
    void getBomas()
    {
        ddlBoma.Items.Clear();
        if (ddlPayam.SelectedIndex != 0)
        {
            Gen.getBomas(ddlBoma, ddlPayam.SelectedValue);
        }
        else
        {
            ddlBoma.Items.Insert(0, "--Select--");
        }
    }


    protected void ddlIP_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlIP.SelectedValue != "--Select--")
        {
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
        }
        else
        {
            ddlProject.Items.Clear();
            ddlProject.Items.Insert(0, "--Select--");
        }

    }
    #endregion


    #region Button Click functionalities

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
        clear();
    }
    private void clear()
    {
        ddlIP.SelectedIndex = ddlProject.SelectedIndex = ddlCounties.SelectedIndex = ddlPayam.SelectedIndex = ddlBoma.SelectedIndex = ddlState.SelectedIndex = 0;
        txtEnvironmentImpact.Text = txtMitigationMeasure.Text = txtPerformanceIndicator.Text = txtProposedMitigationMeasure.Text = "";
        lblWorkCode.Text = "";
        HyperLink1.Text = "";

    }

   

    protected void BtnSave1_Click(object sender, EventArgs e)
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
        ds = Gen.GetWorkProposalByid(ids.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
           // FileUpload();

            if (Session["FileName"].ToString() != "")
            {
                if (ds.Tables[0].Rows[0]["intWorkid"].ToString() != "" && ds.Tables[0].Rows[0]["intWorkid"].ToString() != null)
                {
                    if (lblWorkCode.Text != "")
                    {
                        int i = Gen.InsertEnviMan("", "", ddlProject.SelectedValue, ddlProject.SelectedItem.Text,
                            ddlState.SelectedValue, ddlState.SelectedItem.Text, ddlCounties.SelectedValue, ddlCounties.SelectedItem.Text,
                            ddlPayam.SelectedValue, ddlPayam.SelectedItem.Text, ddlBoma.SelectedValue, ddlBoma.SelectedItem.Text,
                            ds.Tables[0].Rows[0]["intWorkid"].ToString(), lblWorkCode.Text, txtEnvironmentImpact.Text, txtProposedMitigationMeasure.Text,
                            txtMitigationMeasure.Text, txtPerformanceIndicator.Text, Session["FilePath"].ToString(), Session["FileName"].ToString());
                        if (i > 0)
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Inserted Succefully')", true);
                            clear();
                        }
                        else
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Insertion Fialed')", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Work Code should be Manditory')", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please Upload File(PDF,DOC and JPG Only)')", true);
            }
        }



    }
    #endregion

    #region Fill Details function
    private void FillDetails()
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


        ds = Gen.GetWorkProposalByid(ids.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlIP.SelectedValue = ddlIP.Items.FindByValue(ds.Tables[0].Rows[0]["Ipid"].ToString()).Value;
            ddlIP.Enabled = false;
            Gen.getProjects(ddlProject, ddlIP.SelectedValue);
            ddlProject.SelectedValue = ds.Tables[0].Rows[0]["intprjid"].ToString();

            if (ds.Tables[0].Rows[0]["intStateid"].ToString() != "")
            {
                ddlState.SelectedValue = ds.Tables[0].Rows[0]["intStateid"].ToString();
                getcounties();
            }
            if (ds.Tables[0].Rows[0]["intCountieid"].ToString() != "")
            {
                ddlCounties.SelectedValue = ds.Tables[0].Rows[0]["intCountieid"].ToString();
                getPayams();
            }
            if (ds.Tables[0].Rows[0]["intPayamid"].ToString() != "")
            {
                ddlPayam.SelectedValue = ds.Tables[0].Rows[0]["intPayamid"].ToString();
                getBomas();
            }
            if (ds.Tables[0].Rows[0]["intBomasid"].ToString() != "")
                ddlBoma.SelectedValue = ds.Tables[0].Rows[0]["intBomasid"].ToString();

            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();



            //getworksbyPojects();
        }
    }
    #endregion


    #region Doc or Image Binding in Session While File Upload

    public void UploadFile()
    {
        string MOAFFilePath = "";
        string MOAFileName = "";

        string sFileDir = Server.MapPath("~\\MOA");

        General t1 = new General();
        if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload1.PostedFile))
        {


            //determine file name
            string sFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            try
            {

                string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
                int i = fileType.Length;
                if (fileType[i - 1].ToUpper().Trim() == "PDF" || fileType[i - 1].ToUpper().Trim() == "DOC" || fileType[i - 1].ToUpper().Trim() == "DOCX" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG")
                {
                    string newPath = "";
                    if (Session.Count > 0)
                    {
                        newPath = System.IO.Path.Combine(sFileDir, Session["uid"].ToString());
                    }
                    else
                    {
                        newPath = System.IO.Path.Combine(sFileDir, "1000");
                    }
                    newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");
                    MOAFFilePath = newPath.ToString();
                    MOAFileName = sFileName;
                    // Create the subfolder
                    if (!Directory.Exists(newPath))

                        System.IO.Directory.CreateDirectory(newPath);
                    System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(newPath);
                    int count = dir.GetFiles().Length;
                    if (count == 0)
                        FileUpload1.PostedFile.SaveAs(newPath + "\\" + sFileName);
                    else
                    {
                        if (count == 1)
                        {
                            string[] Files = Directory.GetFiles(newPath);
                            FileUpload1.PostedFile.SaveAs(newPath + "\\" + sFileName);
                        }
                    }
                    Session["FileName"] = MOAFileName;
                    Session["FilePath"] = MOAFFilePath;
                    HyperLink1.Text = Session["FileName"].ToString();
                }
                else
                {

                    lblResult.Text = "<font color='red'>Upload PDF,Doc files only..!</font>";
                    return;
                }
            }
            catch (Exception)//in case of an error
            {
                DeleteFile(MOAFFilePath + "\\" + MOAFileName);
            }
        }
    }

    public void DeleteFile(string strFileName)
    {//Delete file from the server
        if (strFileName.Trim().Length > 0)
        {
            FileInfo fi = new FileInfo(strFileName);
            if (fi.Exists)//if file exists delete it
            {
                fi.Delete();
            }
        }
    }
    #endregion
}



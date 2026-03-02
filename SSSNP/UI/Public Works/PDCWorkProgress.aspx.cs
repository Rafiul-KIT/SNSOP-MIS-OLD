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
using System.IO;
using System.Net.Mail;
using Telerik.Web.UI;
#endregion

#region Amendment History Begin
//Form Name : Update Work Progress
//Purpose :  To update work Progress Details
//Tables used : tbl_WorkProgress, tbl_WorkMaster,tbl_BeneficiaryDet,td_AllocationDaysDet,tr_BenfWorkDet,tm_Bomas
//Stored procedures Used : GetworkproposalforProgress,GetBomawiseBeneficiarywithIP2,InsrtWorkProgressDet,WorkAllocation,GetBomawiseBeneficiarywithIPbyGroupandGroupCodes,GetBomawiseBeneficiarywithIP2byGroupandGroupCodes,GetBomawiseBeneficiarywithIP2
#endregion Amendment History End
public partial class PDCWorkProgress : System.Web.UI.Page
{


    #region reference class calling and Variable declaration
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";
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
        // Is not Postback Statement
        if (!IsPostBack)
        {
            Session["dtMyTabledr"] = createtabledr();
            Session["tmpdrDataTable"] = ((DataTable)Session["dtMyTabledr"]);

        }

        #region fill details Calling using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
        {
            Session["Photo"] = Photo;
            Session["PhotoFileName"] = "";

           
            Failure.Visible = false;
            success.Visible = false;
            FillDetails();

            BtnSave1.Text = "Update";
        }
        #endregion

    }
    #endregion

    #region Fill Details function in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
           
            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            if (ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()!="")
            {
                lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            }

            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            Session["IP_NAME"] = ds.Tables[0].Rows[0]["IPName"].ToString();


            

            if (ds.Tables[1].Rows.Count > 0)
            {
                RadGrid1.Visible = true;
                RadGrid1.DataSource = ds.Tables[1];

              //  gvpractical0.DataSource = ds.Tables[1];
               //  gvpractical0.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTableReader rdp = new DataTableReader(ds.Tables[2]);
                IDataReader readerp = rdp;
                               

                ((DataTable)Session["tmpdrDataTable"]).Clear();
                ((DataTable)Session["tmpdrDataTable"]).Load(readerp);
                
                //grdAttachments.DataSource = null;
                //grdAttachments.DataBind();
                //grdAttachments.DataSource = ((DataTable)Session["tmpdrDataTable"]);

                RadAttachment.DataSource = null;
                RadAttachment .DataSource = ((DataTable)Session["tmpdrDataTable"]);

                RadAttachment.Visible = true;
                //grdAttachments.DataSource = ds.Tables[2];
                //grdAttachments.DataBind();
            }
            else
            {
                ((DataTable)Session["tmpdrDataTable"]).Clear();

                RadAttachment.DataSource = null;
                RadAttachment.Rebind();

                RadAttachment.Visible = false;
                //grdAttachments.DataSource = null;
                //grdAttachments.DataBind();

            }

            DataSet dsnew = Gen.workProgressBendet(hdfID.Value.ToString());
            if (dsnew.Tables[1].Rows.Count > 0)
            {
                txtStartDate.Text = Convert.ToDateTime(dsnew.Tables[1].Rows[0]["WorkStartDate"].ToString()).ToString("dd-MM-yyyy");
                ddlStatus.SelectedValue = ddlStatus.Items.FindByValue(dsnew.Tables[1].Rows[0]["Status"].ToString()).Value;
                txtRemarks.Text = dsnew.Tables[1].Rows[0]["Remarks"].ToString();
                txtIncharge.Text = dsnew.Tables[1].Rows[0]["Incharge"].ToString();

                DataSet dsBen = new DataSet();
                string intBomasidc = ds.Tables[0].Rows[0]["intBomasid"].ToString();
                hdfbomaid.Value = ds.Tables[0].Rows[0]["intBomasid"].ToString();
                
                string WorkCode1 = "0";
                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    // WorkCode1 = dsnew.Tables[0].Rows[0]["WorkCode"].ToString();
                    WorkCode1 = dsnew.Tables[1].Rows[0]["intWorkid"].ToString();
                    hdfworkid.Value = dsnew.Tables[0].Rows[0]["WorkCode"].ToString();
                }
               
                dsBen = Gen.GetBomawiseBeneficiarywithIP2(intBomasidc, Session["User_Code"].ToString(), WorkCode1);
                if (dsBen.Tables[0].Rows.Count > 0)
                {

                    RadGrid2.DataSource = dsBen.Tables[0];
                    RadGrid2.DataBind();
                    // RadGrid2.DataBind();
                  //  grdDetails.DataSource = dsBen.Tables[0];
                   // grdDetails.DataBind();
                    tr_group_search.Visible = true;
                    tr_group_code.Visible = true;
                    RadGrid2.Visible = true;

                }


            }
            else
            {
                DataSet dsBen1 = new DataSet();
                hdfbomaid.Value = ds.Tables[0].Rows[0]["intBomasid"].ToString();

                dsBen1 = Gen.GetBomawiseBeneficiarywithIP(ds.Tables[0].Rows[0]["intBomasid"].ToString(), Session["User_Code"].ToString());
                if (dsBen1.Tables[0].Rows.Count > 0)
                {
                    RadGrid2.DataSource = dsBen1.Tables[0];
                    RadGrid2.DataBind();
                    //grdDetails.DataSource = dsBen1.Tables[0];
                    //grdDetails.DataBind();
                    RadGrid2.Visible = true;
                }
                else
                {
                    RadGrid2.DataSource = null;
                    RadGrid2.DataBind();
                    //grdDetails.DataSource = dsBen1.Tables[0];
                    //grdDetails.DataBind();
                    RadGrid2.Visible =false;
                }
            }

          //  ddl_group_code.Items.Clear();
          //  ddl_group_code.Items.Insert(0, "--Select--");
            ddl_group_type.SelectedIndex = 0;
            if (ddl_group_type.SelectedIndex == 0)
            {
                tr_group_code.Visible = false;
            }
            else
            {

                if (ddl_group_type.SelectedValue == "Group")
                {
                    tr_group_code.Visible = true;
                }
                else
                {
                    tr_group_code.Visible = false;
                }

            }


            //if (ds.Tables[0].Rows[0]["Status"].ToString() == "Completed")
            //{
            //    BtnSave1.Visible = false;
            //    for (int iii = 0; iii <  grdDetails.Rows.Count; iii++)
            //    {
            //        GridViewRow row = (GridViewRow)grdDetails.Rows[iii];

            //        CheckBox c = (CheckBox)grdDetails.Rows[iii].Cells[0].FindControl("CheckBox1");
            //        if (c.Checked == false )
            //        {
            //            row.Visible = false;
            //        }
            //    }
            //}


        }
    }

    void RefreshFillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
            lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();

            lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
            if (ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString() != "")
            {
                lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
            }

            lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
            lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
            lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
            Session["IP_NAME"] = ds.Tables[0].Rows[0]["IPName"].ToString();




            if (ds.Tables[1].Rows.Count > 0)
            {
                RadGrid1.Visible = true;
                RadGrid1.DataSource = ds.Tables[1];

                //  gvpractical0.DataSource = ds.Tables[1];
                //  gvpractical0.DataBind();
            }

            if (ds.Tables[2].Rows.Count > 0)
            {
                DataTableReader rdp = new DataTableReader(ds.Tables[2]);
                IDataReader readerp = rdp;


                ((DataTable)Session["tmpdrDataTable"]).Clear();
                ((DataTable)Session["tmpdrDataTable"]).Load(readerp);

                //grdAttachments.DataSource = null;
                //grdAttachments.DataBind();
                //grdAttachments.DataSource = ((DataTable)Session["tmpdrDataTable"]);

                RadAttachment.DataSource = null;
                RadAttachment.DataSource = ((DataTable)Session["tmpdrDataTable"]);

                RadAttachment.Visible = true;
                //grdAttachments.DataSource = ds.Tables[2];
                //grdAttachments.DataBind();
            }
            else
            {
                ((DataTable)Session["tmpdrDataTable"]).Clear();

                RadAttachment.DataSource = null;
                RadAttachment.Rebind();

                RadAttachment.Visible = false;
                //grdAttachments.DataSource = null;
                //grdAttachments.DataBind();

            }

            DataSet dsnew = Gen.workProgressBendet(hdfID.Value.ToString());
            if (dsnew.Tables[1].Rows.Count > 0)
            {
                txtStartDate.Text = Convert.ToDateTime(dsnew.Tables[1].Rows[0]["WorkStartDate"].ToString()).ToString("dd-MM-yyyy");
                ddlStatus.SelectedValue = ddlStatus.Items.FindByValue(dsnew.Tables[1].Rows[0]["Status"].ToString()).Value;
                txtRemarks.Text = dsnew.Tables[1].Rows[0]["Remarks"].ToString();
                txtIncharge.Text = dsnew.Tables[1].Rows[0]["Incharge"].ToString();

                DataSet dsBen = new DataSet();
                string intBomasidc = ds.Tables[0].Rows[0]["intBomasid"].ToString();
                hdfbomaid.Value = ds.Tables[0].Rows[0]["intBomasid"].ToString();

                string WorkCode1 = "0";
                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    WorkCode1 = dsnew.Tables[0].Rows[0]["WorkCode"].ToString();
                    hdfworkid.Value = dsnew.Tables[0].Rows[0]["WorkCode"].ToString();
                }

                dsBen = Gen.GetBomawiseBeneficiarywithIP2(intBomasidc, Session["User_Code"].ToString(), WorkCode1);
                if (dsBen.Tables[0].Rows.Count > 0)
                {

                    RadGrid2.DataSource = dsBen.Tables[0];
                    
                    // RadGrid2.DataBind();
                    //  grdDetails.DataSource = dsBen.Tables[0];
                    // grdDetails.DataBind();
                    tr_group_search.Visible = true;
                    tr_group_code.Visible = true;
                    RadGrid2.Visible = true;

                }


            }
            else
            {
                DataSet dsBen1 = new DataSet();
                hdfbomaid.Value = ds.Tables[0].Rows[0]["intBomasid"].ToString();

                dsBen1 = Gen.GetBomawiseBeneficiarywithIP(ds.Tables[0].Rows[0]["intBomasid"].ToString(), Session["User_Code"].ToString());
                if (dsBen1.Tables[0].Rows.Count > 0)
                {
                    RadGrid2.DataSource = dsBen1.Tables[0];
                    
                    //grdDetails.DataSource = dsBen1.Tables[0];
                    //grdDetails.DataBind();
                    RadGrid2.Visible = true;
                }
                else
                {
                    RadGrid2.DataSource = null;
                    RadGrid2.DataBind();
                    //grdDetails.DataSource = dsBen1.Tables[0];
                    //grdDetails.DataBind();
                    RadGrid2.Visible = false;
                }
            }

            //  ddl_group_code.Items.Clear();
            //  ddl_group_code.Items.Insert(0, "--Select--");
            ddl_group_type.SelectedIndex = 0;
            if (ddl_group_type.SelectedIndex == 0)
            {
                tr_group_code.Visible = false;
            }
            else
            {

                if (ddl_group_type.SelectedValue == "Group")
                {
                    tr_group_code.Visible = true;
                }
                else
                {
                    tr_group_code.Visible = false;
                }

            }


  

        }
    }
    #endregion

    #region Button Click functionalities
    protected void BtnSave_Click(object sender, EventArgs e)
    {
        int hasChecked = 0;

        foreach (GridDataItem item in RadGrid2.Items)
        {
            CheckBox cbControl = (CheckBox) item.Cells[0].FindControl("chkSelected");

            if (cbControl.Checked)
            {
                hasChecked = 1;
                break;
            }
        }

        
        if (hasChecked == 0)
        {
            lblmsg0.Text = "At lease one Household should be selected";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;

        }


        int i = Gen.InsrtWorkProgressDet(hdfID.Value.ToString(), txtStartDate.Text, ddlStatus.SelectedValue, txtRemarks.Text, Session["uid"].ToString(), txtIncharge.Text);
        if (i > 0)
        {
            //siva as on 04-1-2016
            if (((DataTable)Session["tmpdrDataTable"]).Rows.Count > 0)
            {

                int di = Gen.DeleteWorkProgress(i);


                GetNewRectoInsertdr();
                int statuspr = Gen.bulkInsertdrWorkProgressPhto(myDtNewRecdr, i);

            }
           
            foreach (GridDataItem item in RadGrid2.Items)
            {
                CheckBox cbControl = (CheckBox)item.Cells[0].FindControl("chkSelected");

                if (cbControl.Checked)
                {
                    int benId = 0;
                    int.TryParse(item.OwnerTableView.DataKeyValues[item.ItemIndex]["intBenid"].ToString(), out benId);


                    if (benId !=0)
                    {
                        //int s = Gen.WorkAllocation(hdfID.Value.ToString(), grdDetails.DataKeys[iii].Value.ToString(), Session["uid"].ToString());
                        int s = Gen.WorkAllocation(hdfID.Value.ToString(), benId.ToString(), Session["uid"].ToString());
                    }
                    
                }
            }


             //SendMailAnother();
            lblmsg.Text = "Submited Successfully..!";
            success.Visible = true;
            Failure.Visible = false;
           
            Response.Redirect("WorkProcessResult.aspx");
        }
        else
        {
            lblmsg0.Text = "Already Exist. ";
            success.Visible = false;
            Failure.Visible = true;
        }
    }
     

    protected void btnAddNew_Click(object sender, EventArgs e)

    {
        Failure.Visible = false;
        //FileWorkPhotoUpload = Session["PhotoFileName"];
        string Directory_Path = Server.MapPath("~/WorkProgressDoc/" + lblProject.Text + "/" + txtUploadDocumentName.Text);
        string WorkPhotoStr = "";

        if (FileWorkPhotoUpload.HasFile == false)
        {
            lblmsg0.Text = "Please choose a file";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }

        if (FileWorkPhotoUpload.PostedFile != null && FileWorkPhotoUpload.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileWorkPhotoUpload.PostedFile))
        {
            //start
            if (!Directory.Exists(Directory_Path))
            {
                Directory.CreateDirectory(Directory_Path);

                //1. work photo upload
                if (FileWorkPhotoUpload.HasFile == true)
                {
                    string Work_Photo_Name = "WorkDocument_";
                    string filename = System.IO.Path.GetFileName(FileWorkPhotoUpload.PostedFile.FileName);
                    filename = Work_Photo_Name + filename;
                    WorkPhotoStr = ("~/WorkProgressDoc/" + lblProject.Text + "/" + txtUploadDocumentName.Text + "/") + filename;
                    FileWorkPhotoUpload.SaveAs(Server.MapPath("~/WorkProgressDoc/" + lblProject.Text + "/" + txtUploadDocumentName.Text + "/") + filename);

                }
                //else
                //{
                //    lblmsg0.Text = "Please choose a file";
                //    Failure.Visible = true;
                //    ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
                //    return;
                //}

            }
            else
            {

                //file check 

                //1. Work Photo upload

                DirectoryInfo di_files = new DirectoryInfo(Directory_Path);

                if (FileWorkPhotoUpload.HasFile == true)
                {
                    foreach (FileInfo file in di_files.GetFiles())
                    {
                        if (file.Name.Contains("WorkDocument_"))
                        {

                            file.Delete();
                        }
                    }

                    string Work_Photo_Name = "WorkDocument_";
                    string filename = System.IO.Path.GetFileName(FileWorkPhotoUpload.PostedFile.FileName);
                    filename = Work_Photo_Name + filename;
                    WorkPhotoStr = ("~/WorkProgressDoc/" + lblProject.Text + "/" + txtUploadDocumentName.Text + "/") + filename;
                    FileWorkPhotoUpload.SaveAs(Server.MapPath("~/WorkProgressDoc/" + lblProject.Text + "/" + txtUploadDocumentName.Text + "/") + filename);

                }





            }

            //end
        }
        try
        {

            //DateTime Current_Date = "";
            if (WorkPhotoStr != "")
            {
                fillTrademappingGriddr((DataTable)Session["tmpdrDataTable"], hdfID.Value.ToString(), txtUploadDocumentName.Text, WorkPhotoStr, "WorkProgress", Session["uid"].ToString());

                RadAttachment.Rebind();

                //this.grdAttachments.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                //this.grdAttachments.DataBind();
                RadAttachment.Visible = true;                               

                txtUploadDocumentName.Text = "";

            }

        }
        catch (Exception ex)
        {
            //   lblresult.Text = ex.ToString();
        }
        finally
        {

        }

    }

    protected void BtnClear0_Click(object sender, EventArgs e)
    {

    }

   
    #endregion

    #region Mail Send Functionalities
    public void SendMailAnother()
    {
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail);

        //string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        // string anothermail = "";
        string anothermail = System.Configuration.ConfigurationManager.AppSettings["TwoEmailRecipients"];
        string[] multi = anothermail.Split(',');
        foreach (string multilailid in multi)
        {
            mail.To.Add(new MailAddress(multilailid));
        }

        string Messge = Convert.ToString(Session["IP_NAME"]) + " has updated progress of some work.Please verify it.";

        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - Work Progress Notification";

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "Dear Team <br><br>  <H2>SSSNP Public Works MIS - Work Progress Notification</H2><br> <b> IP NAME: " + Convert.ToString(Session["IP_NAME"]) + "</b> <br><br> Work Code: " + lblWorkCode.Text.ToString() + "<br> <br> Remarks : " + Messge.ToString() + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>" + Convert.ToString(Session["IP_NAME"]) + "";
        mail.BodyEncoding = System.Text.Encoding.UTF8;

        mail.IsBodyHtml = true;
        mail.Priority = MailPriority.High;

        string smtpserver = System.Configuration.ConfigurationManager.AppSettings["SMTPSERVER"]; // Config smtp
        var client = new SmtpClient(smtpserver);

    
        try
        {
            client.Send(mail);
            //Session.Remove("File");
            //Session.Remove("FileName");
        }
        catch (Exception ex)
        {
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
           // HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }
    #endregion

    #region Gridview Events

    //protected void grdDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if ((e.Row.RowType == DataControlRowType.DataRow))
    //    {
    //        CheckBox chk = (CheckBox)e.Row.Cells[0].FindControl("CheckBox1");

    //        if (Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Workid")) == "1")
    //        {
    //            chk.Checked = true;
    //        }
           
    //    }
    //}
    //protected void grdDetails_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    //{



    //}
    //protected void grdDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    //grdDetails.PageIndex = e.NewPageIndex;

    //    //DataSet ds = Gen.GetworkproposalforProgress(hdfID.Value.ToString());

    //    //if (ds.Tables[0].Rows.Count > 0)
    //    //{

    //    //    DataSet dsBen = new DataSet();
    //    //    dsBen = Gen.GetBomawiseBeneficiary(ds.Tables[0].Rows[0]["intBomasid"].ToString());
    //    //    if (dsBen.Tables[0].Rows.Count > 0)
    //    //    {
              
    //    //        grdDetails.DataSource = dsBen.Tables[0];
    //    //        grdDetails.DataBind();
    //    //    }

    //    //}

    //}

    //protected void grdAttachments_RowCommand(object sender, GridViewCommandEventArgs e)
    //{
    //    if (e.CommandName == "download")
    //    {
    //        string filePath = e.CommandArgument.ToString();
    //        Response.ContentType = ContentType;
    //        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
    //        Response.WriteFile(filePath);
    //        Response.End();
    //        grdAttachments.Dispose();
    //    }
    //}
    #endregion

    #region Bulk Insert Functionalities
    private void fillTrademappingGriddr(DataTable tmpTabledr, string intWorkid, string DocumentName, string WorkPhotoStr, string Status, string Created_by)
    {

        // tmpTabledr = new DataTable();

        dtrdr = tmpTabledr.NewRow();
        //dtrdr["intWorkActivityId"] = "0";
        //dtrdr["new"] = "0";
        //

        //
        dtrdr["intWorkid"] = intWorkid.Trim();
        dtrdr["DocumentName"] = DocumentName.Trim();
        dtrdr["WorkPhoto"] = WorkPhotoStr.Trim();
        dtrdr["Status"] = Status.Trim();
        dtrdr["Created_by"] = Created_by.Trim();
        dtrdr["Created_dt"] = DateTime.Now;
        dtrdr["IntWorkDetailsTypeId"] = 0;

        tmpTabledr.Rows.Add(dtrdr);


    }

    protected DataTable createtabledr()
    {
        Session["dtMyTabledr"] = new DataTable("drDetails");
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("new", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("intWorkid", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("DocumentName", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("WorkPhoto", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Status", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Created_by", typeof(string));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("Created_dt", typeof(DateTime));
        ((DataTable)Session["dtMyTabledr"]).Columns.Add("IntWorkDetailsTypeId", typeof(Int32));
        return ((DataTable)Session["dtMyTabledr"]);
    }


    //protected void grdAttachments_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //{
    //    try
    //    {
    //        ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.RowIndex);
    //        this.grdAttachments.DataSource = ((DataTable)Session["tmpdrDataTable"]);
    //        this.grdAttachments.DataBind();
    //    }
    //    catch (Exception ex)
    //    {
    //        //  lblresult.Text = ex.ToString();

    //    }
    //    finally
    //    {
    //    }
    //}

    //protected void grdAttachments_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //}
  

    protected void GetNewRectoInsertdr()
    {
        myDtNewRecdr = (DataTable)Session["tmpdrDataTable"];
        DataView dvdr = new DataView(myDtNewRecdr);
        //dvdr.RowFilter = "new = 0";
        myDtNewRecdr = dvdr.ToTable();
    }
    #endregion


    #region Dropdown Binding Functionalities and Index Changed 
    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfFlagID.Value = "1";

     
        int cnt = 0;
        cnt = ddl_group_type.Items.Count;


     


        if (ddl_group_type.Items[0].Selected == true)
        {

            tr_group_code.Visible = true;
            int cnta = ddl_group_code.Items.Count;
            int chk = 0;
            for (int b = 0; b < cnta; b++)
            {
                if (ddl_group_code.Items[b].Selected == true)
                {
                    chk = chk + 1;
                }

            }
            if (chk == 0)
            {
                ddl_group_code.Items[0].Selected = true;
            }


        }
        else if (ddl_group_type.Items[1].Selected == true && ddl_group_type.Items[2].Selected == true)
        {
            tr_group_code.Visible = true;
            int cnta = ddl_group_code.Items.Count;
            int chk = 0;
            for (int b = 0; b < cnta; b++)
            {
                if (ddl_group_code.Items[b].Selected == true)
                {
                    chk = chk + 1;
                }

            }
            if (chk == 0)
            {
                ddl_group_code.Items[0].Selected = true;
            }
            // ddl_group_code.Items[0].Selected = true;
        }
        else if (ddl_group_type.Items[1].Selected == true)
        {
            tr_group_code.Visible = true;

            int cnta = ddl_group_code.Items.Count;
            int chk = 0;
            for (int b = 0; b < cnta; b++)
            {
                if (ddl_group_code.Items[b].Selected == true)
                {
                    chk = chk + 1;
                }

            }
            if (chk == 0)
            {
                ddl_group_code.Items[0].Selected = true;
            }
            // ddl_group_code.Items[0].Selected = true;
        }
        else if (ddl_group_type.Items[2].Selected == true)
        {
            tr_group_code.Visible = false;
        }
        
         Failure.Visible = false;
            lblmsg0.Text = "";
            DataSet ds = new DataSet();
            ds.Clear();
            
            DataSet dsnew = new DataSet();
            dsnew.Clear();
            //ds = null;
            //dsnew = null;
        //    int cnt = 0;
            cnt =ddl_group_code.Items.Count;

            if (ddl_group_type.Items[0].Selected == true)
            {
                if (ddl_group_code.Items[0].Selected == true)
                {
                    DataSet dsnewaa = Gen.workProgressBendet(hdfID.Value.ToString());
                    if (dsnewaa.Tables[1].Rows.Count > 0)
                    {

                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, "");
                    }
                    else
                    {
                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, "");
                    }

                
                   
                    if (dsnew.Tables[0].Rows.Count > 0)
                    {
                        ds.Merge(dsnew);
                    }
                }
                else
                {
                    for (int a = 0; a < cnt; a++)
                    {

                        if (a != 0)
                        {
                            if (ddl_group_code.Items[a].Selected == true)
                            {


                                DataSet dsnewaa = Gen.workProgressBendet(hdfID.Value.ToString());
                                if (dsnewaa.Tables[1].Rows.Count > 0)
                                {

                                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                }
                                else
                                {
                                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                }

                           

                                if (dsnew.Tables[0].Rows.Count > 0)
                                {
                                    ds.Merge(dsnew);
                                }
                            }
                        }


                    }
                }

                DataSet dsnewaab = Gen.workProgressBendet(hdfID.Value.ToString());
                if (dsnewaab.Tables[1].Rows.Count > 0)
                {

                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[2].Value, "");
                }
                else
                {
                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[2].Value, "");
                }

             
               // dsnew = Gen.getAttendanceCountFromToWages(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.Items[2].Value, "");

                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    ds.Merge(dsnew);
                }
            }
            else
            {
                if (ddl_group_type.Items[2].Selected == true && ddl_group_type.Items[1].Selected == true)
                {
                    if (ddl_group_code.Items[0].Selected == true)
                    {


                        DataSet dsnewaab = Gen.workProgressBendet(hdfID.Value.ToString());
                        if (dsnewaab.Tables[1].Rows.Count > 0)
                        {

                            dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, "");
                        }
                        else
                        {
                            dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, "");
                        }
                       
                        if (dsnew.Tables[0].Rows.Count > 0)
                        {
                            ds.Merge(dsnew);
                        }
                    }
                    else
                    {
                        for (int a = 0; a < cnt; a++)
                        {


                            if (a != 0)
                            {
                                if (ddl_group_code.Items[a].Selected == true)
                                {


                                    DataSet dsnewaab = Gen.workProgressBendet(hdfID.Value.ToString());
                                    if (dsnewaab.Tables[1].Rows.Count > 0)
                                    {

                                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                    }
                                    else
                                    {
                                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                    }
                       
                                    if (dsnew.Tables[0].Rows.Count > 0)
                                    {
                                        ds.Merge(dsnew);
                                    }
                                }
                            }


                        }
                    }


                    DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                    if (dsnewaabc.Tables[1].Rows.Count > 0)
                    {

                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[2].Value, "");
                    }
                    else
                    {
                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[2].Value, "");
                    }
                 
                    if (dsnew.Tables[0].Rows.Count > 0)
                    {
                        ds.Merge(dsnew);
                    }
                }
                else  if (ddl_group_type.Items[1].Selected == true)
                {
                    if (ddl_group_code.Items[0].Selected == true)
                    {

                        DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                        if (dsnewaabc.Tables[1].Rows.Count > 0)
                        {

                            dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, "");
                        }
                        else
                        {
                            dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, "");
                        }
                      
                        if (dsnew.Tables[0].Rows.Count > 0)
                        {
                            ds.Merge(dsnew);
                        }
                    }
                    else
                    {
                        for (int a = 0; a < cnt; a++)
                        {


                            if (a != 0)
                            {
                                if (ddl_group_code.Items[a].Selected == true)
                                {


                                    DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                                    if (dsnewaabc.Tables[1].Rows.Count > 0)
                                    {

                                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                    }
                                    else
                                    {
                                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                    }
                      
                                  
                                    if (dsnew.Tables[0].Rows.Count > 0)
                                    {
                                        ds.Merge(dsnew);
                                    }
                                }
                            }


                        }
                    }
                }
                else if (ddl_group_type.Items[2].Selected == true)
                {

                    DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                    if (dsnewaabc.Tables[1].Rows.Count > 0)
                    {

                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[2].Value, "");
                    }
                    else
                    {
                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[2].Value, "");
                    }
                   
                    if (dsnew.Tables[0].Rows.Count > 0)
                    {
                        ds.Merge(dsnew);
                    }
                }
               
            }

            if (ds.Tables.Count > 0)
            {

                if (ds.Tables[0].Rows.Count > 0)
                {
                    RadGrid2.DataSource = ds.Tables[0];
                     RadGrid2.DataBind ();

                 //   grdDetails.DataSource = ds.Tables[0];
                  //  grdDetails.DataBind();
                    BtnSave1.Visible = true;
                    BtnSave1.Visible = true;
                    tr_group_search.Visible = true;
                }
                else
                {
                    BtnSave1.Visible = false;

                     RadGrid2.DataSource = null;
                RadGrid2.DataBind();

                // grdDetails.DataSource = null;
                //grdDetails.DataBind();
                }
            
            }
            else
            {
                BtnSave1.Visible = false;

            RadGrid2.DataSource = null;
            RadGrid2.DataBind();

            // grdDetails.DataSource = null;
            // grdDetails.DataBind();
        }

       

    }
    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
        hdfFlagID.Value = "1";
        Failure.Visible = false;
        lblmsg0.Text = "";
        DataSet ds = new DataSet();
        ds.Clear();
        int cnt = 0;
        DataSet dsnew = new DataSet();
        dsnew.Clear();
        //ds = null;
        //dsnew = null;
        //    int cnt = 0;
        cnt = ddl_group_code.Items.Count;

        if (ddl_group_type.Items[0].Selected == true)
        {
            if (ddl_group_code.Items[0].Selected == true)
            {
                DataSet dsnewaa = Gen.workProgressBendet(hdfID.Value.ToString());
                if (dsnewaa.Tables[1].Rows.Count > 0)
                {

                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, "");
                }
                else
                {
                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, "");
                }



                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    ds.Merge(dsnew);
                }
            }
            else
            {
                for (int a = 0; a < cnt; a++)
                {

                    if (a != 0)
                    {
                        if (ddl_group_code.Items[a].Selected == true)
                        {


                            DataSet dsnewaa = Gen.workProgressBendet(hdfID.Value.ToString());
                            if (dsnewaa.Tables[1].Rows.Count > 0)
                            {

                                dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                            }
                            else
                            {
                                dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                            }



                            if (dsnew.Tables[0].Rows.Count > 0)
                            {
                                ds.Merge(dsnew);
                            }
                        }
                    }


                }
            }

            DataSet dsnewaab = Gen.workProgressBendet(hdfID.Value.ToString());
            if (dsnewaab.Tables[1].Rows.Count > 0)
            {

                dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[2].Value, "");
            }
            else
            {
                dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[2].Value, "");
            }


            // dsnew = Gen.getAttendanceCountFromToWages(hdfID.Value.ToString(), TxtFromDate.Text, TxtToDate.Text, ddl_group_type.Items[2].Value, "");

            if (dsnew.Tables[0].Rows.Count > 0)
            {
                ds.Merge(dsnew);
            }
        }
        else
        {
            if (ddl_group_type.Items[2].Selected == true && ddl_group_type.Items[1].Selected == true)
            {
                if (ddl_group_code.Items[0].Selected == true)
                {


                    DataSet dsnewaab = Gen.workProgressBendet(hdfID.Value.ToString());
                    if (dsnewaab.Tables[1].Rows.Count > 0)
                    {

                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, "");
                    }
                    else
                    {
                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, "");
                    }

                    if (dsnew.Tables[0].Rows.Count > 0)
                    {
                        ds.Merge(dsnew);
                    }
                }
                else
                {
                    for (int a = 0; a < cnt; a++)
                    {


                        if (a != 0)
                        {
                            if (ddl_group_code.Items[a].Selected == true)
                            {


                                DataSet dsnewaab = Gen.workProgressBendet(hdfID.Value.ToString());
                                if (dsnewaab.Tables[1].Rows.Count > 0)
                                {

                                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                }
                                else
                                {
                                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                }

                                if (dsnew.Tables[0].Rows.Count > 0)
                                {
                                    ds.Merge(dsnew);
                                }
                            }
                        }


                    }
                }


                DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                if (dsnewaabc.Tables[1].Rows.Count > 0)
                {

                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[2].Value, "");
                }
                else
                {
                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[2].Value, "");
                }

                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    ds.Merge(dsnew);
                }
            }
            else if (ddl_group_type.Items[1].Selected == true)
            {
                if (ddl_group_code.Items[0].Selected == true)
                {

                    DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                    if (dsnewaabc.Tables[1].Rows.Count > 0)
                    {

                        dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, "");
                    }
                    else
                    {
                        dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, "");
                    }

                    if (dsnew.Tables[0].Rows.Count > 0)
                    {
                        ds.Merge(dsnew);
                    }
                }
                else
                {
                    for (int a = 0; a < cnt; a++)
                    {


                        if (a != 0)
                        {
                            if (ddl_group_code.Items[a].Selected == true)
                            {


                                DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                                if (dsnewaabc.Tables[1].Rows.Count > 0)
                                {

                                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                }
                                else
                                {
                                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[1].Value, ddl_group_code.Items[a].Value);
                                }


                                if (dsnew.Tables[0].Rows.Count > 0)
                                {
                                    ds.Merge(dsnew);
                                }
                            }
                        }


                    }
                }
            }
            else if (ddl_group_type.Items[2].Selected == true)
            {

                DataSet dsnewaabc = Gen.workProgressBendet(hdfID.Value.ToString());
                if (dsnewaabc.Tables[1].Rows.Count > 0)
                {

                    dsnew = Gen.GetBomawiseBeneficiarywithIP2byGroupandGroupCodes(hdfbomaid.Value, Session["User_Code"].ToString(), hdfworkid.Value, ddl_group_type.Items[2].Value, "");
                }
                else
                {
                    dsnew = Gen.GetBomawiseBeneficiarywithIPgroupypegroupcode(hdfbomaid.Value, Session["User_Code"].ToString(), ddl_group_type.Items[2].Value, "");
                }

                if (dsnew.Tables[0].Rows.Count > 0)
                {
                    ds.Merge(dsnew);
                }
            }

        }

        if (ds.Tables.Count > 0)
        {

            if (ds.Tables[0].Rows.Count > 0)
            {
              //  grdDetails.DataSource = ds.Tables[0];
               // grdDetails.DataBind();
                BtnSave1.Visible = true;
                BtnSave1.Visible = true;
                tr_group_search.Visible = true;
            }
            else
            {
                BtnSave1.Visible = false;

               // grdDetails.DataSource = null;
                // grdDetails.DataBind();
            }
        }
        else
        {
            BtnSave1.Visible = false;

            // grdDetails.DataSource = null;
            // grdDetails.DataBind();
        }
    }
    #endregion

    protected void RadGrid1_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        //if (!string.IsNullOrEmpty(hdfID.Value.ToString()))
        //{
        //    FillDetails();
        //}
        
    }

    protected void RadGrid1_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadGrid1.MasterTableView.PageSize * RadGrid1.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();

           
        }
    }

    protected void RadGrid2_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (!string.IsNullOrEmpty(hdfID.Value.ToString()))
        {
            RefreshFillDetails();
        }
    }

    protected void RadGrid2_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadGrid2.MasterTableView.PageSize * RadGrid2.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();


        }

    }

    protected void RadAttachment_NeedDataSource(object sender, GridNeedDataSourceEventArgs e)
    {
        if (Session["tmpdrDataTable"] != null)
        {
            RadAttachment.DataSource = ((DataTable)Session["tmpdrDataTable"]);
        }
        

    }

    protected void RadAttachment_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridDataItem)
        {
            int rowCounter;
            Label lbl = e.Item.FindControl("rowNumberLabel") as Label;
            rowCounter = RadAttachment.MasterTableView.PageSize * RadAttachment.MasterTableView.CurrentPageIndex;
            lbl.Text = (e.Item.ItemIndex + 1 + rowCounter).ToString();


        }
    }

    protected void RadAttachment_ItemCommand(object sender, GridCommandEventArgs e)
    {
        if (e.CommandName == "cmd_download")
        {
            string filePath = e.CommandArgument.ToString();
            Response.ContentType = ContentType;
            Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
            Response.WriteFile(filePath);
            Response.End();
            // RadAttachment.Dispose();
            // grdAttachments.Dispose();
        }
        else if (e.CommandName == "cmd_delete")
        {
            try
            {
             
                ((DataTable)Session["tmpdrDataTable"]).Rows.RemoveAt(e.Item.ItemIndex);
                //this.grdAttachments.DataSource = ((DataTable)Session["tmpdrDataTable"]);
                //this.grdAttachments.DataBind();

                // this.RadAttachment.DataSource= ((DataTable)Session["tmpdrDataTable"]);
                RadAttachment.Rebind();

            }
            catch (Exception ex)
            {
                //  lblresult.Text = ex.ToString();

            }
            finally
            {
            }

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.IO;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Web.Configuration;
using System.Threading;
using System.ComponentModel;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.IO;
#region Amendament History

/// <summary>

/// Project : SNSDP

/// Purpose of Form: SNSDP Wages Entry

/// Working Task : SNSDP Wages Entry

/// Developer : Srikanth

/// Date : 04-08-2017

/// Store Procedures Modified : getWagesReport

/// Tables Used Modified: tr_BenWagesPayment

/// </summary>
#endregion
public partial class UpdateAttachments : System.Web.UI.Page
{

    //designing and developed by siva as on 27-02-2016

    //Purpose : To update Wages Payement Details
    //Tables used :tr_BenWagesPayment, tr_BenAttendanceDet
    //Stored procedures Used : GetWorkProgressforWages,InsUpdBeneficiaryWages

    int Totalnoofdays1 = 0;
    decimal TotalAmountGrd1 = Convert.ToDecimal("0");
    decimal TotalAmount = Convert.ToDecimal("0");
    decimal TotalAmountSSP = Convert.ToDecimal("0");
    General Gen = new General();
    comFunctions cmf = new comFunctions();
    DataRow dtrdr;
    DataTable myDtNewRecdr = new DataTable();
    DataTable myDtNewRecdr1 = new DataTable();
    public DataTable dtMyTable1;
    public DataTable dtMyTable2;

    public DataTable dtMyTableCertificate;

    public DataTable dtMyTablepr;
    public DataTable dtMyTableCertificate2;
    string path = WebConfigurationManager.AppSettings["AppPath"].ToString();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            Session["PhotoFilePath"] = "";
            Session["PhotoFileName"] = "";
            hdfFlagID.Value = "0";
            dtMyTableCertificate = createtablecrtificate2();
            Session["CertificateTb1"] = dtMyTableCertificate;
            
        }
      
        UploadFile1();
        if (hdfFlagID.Value == "0")
        {

            Failure.Visible = false;
            success.Visible = false;
            FillDetails();
        }
        txtExchangerate.Enabled = false;  

    }



    void FillDetails()
    {


        DataSet dsn = new DataSet();
        dsn = Gen.GetWageRequestbyID(Request.QueryString[0].ToString().Trim());
        if (dsn.Tables[0].Rows.Count > 0)
        {
            hdfFlagID.Value = "1";
            DataSet ds = new DataSet();

           
            //ds = Gen.GetWorkProgressforWages(hdfID.Value.ToString());
            ds = Gen.GetworkproposalforProgress(dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim());

            if (ds.Tables[0].Rows.Count > 0)
            {
                lblProject.Text = ds.Tables[0].Rows[0]["PrjName"].ToString();
                lblWorkCode.Text = ds.Tables[0].Rows[0]["workcode"].ToString();
                hdfID.Value = dsn.Tables[0].Rows[0]["intWorkid"].ToString().Trim();
                lblBoma.Text = ds.Tables[0].Rows[0]["BomaName"].ToString();
                lblWorkStartDate.Text = Convert.ToDateTime(ds.Tables[0].Rows[0]["PDC_ApprvDate"].ToString()).ToString("dd-MM-yyyy");
                lblState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();
                lblCounty.Text = ds.Tables[0].Rows[0]["CountieName"].ToString();
                lblPayam.Text = ds.Tables[0].Rows[0]["PayamName"].ToString();
                lblWageRequestNo.Text ="Wage Request No :" + dsn.Tables[0].Rows[0]["ReqNo"].ToString();
                lblwagerate.Text = dsn.Tables[0].Rows[0]["WageRateUSD"].ToString();
              txtExchangerate.Text = dsn.Tables[0].Rows[0]["Exchangerateattime"].ToString();
                lblpayingagent.Text = dsn.Tables[0].Rows[0]["SPaymentagent"].ToString();
                lblcoordinator.Text = dsn.Tables[0].Rows[0]["coordinator"].ToString();
                lblspecialist.Text = dsn.Tables[0].Rows[0]["Specilist"].ToString();
                lblmanager.Text = dsn.Tables[0].Rows[0]["SManager"].ToString();
               lblfromdate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqFromDate"].ToString().Trim()).ToString("dd-MM-yyyy");
               lbltodate.Text = Convert.ToDateTime(dsn.Tables[0].Rows[0]["ReqToDate"].ToString().Trim()).ToString("dd-MM-yyyy");
               hdfIP.Value = ds.Tables[0].Rows[0]["IPid"].ToString();
               HdfReqNo.Value = dsn.Tables[0].Rows[0]["ReqNo"].ToString();
             


            }
             
            DataSet ds2 = Gen.GetWageRequestbyIDWages1(Request.QueryString[0].ToString().Trim());
            if (ds2.Tables[0].Rows.Count > 0)
            {

                DataTableReader rdt = new DataTableReader(ds2.Tables[0]);
                IDataReader readert = rdt;

                ((DataTable)Session["CertificateTb1"]).Clear();
                ((DataTable)Session["CertificateTb1"]).Load(readert);
                GridView1.DataSource = ((DataTable)Session["CertificateTb1"]);
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
        }
    }



    protected void BtnSave_Click(object sender, EventArgs e)
    {

        if (((DataTable)Session["CertificateTb1"]).Rows.Count < 1)
        {
            lblmsg0.Text = "Please Uploaad at least one document";
            Failure.Visible = true;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
        if (((DataTable)Session["CertificateTb1"]).Rows.Count > 0)
        {

            for (int n = 0; n < ((DataTable)Session["CertificateTb1"]).Rows.Count; n++)
            {
                if (((DataTable)Session["CertificateTb1"]).Rows[n]["intImageId"].ToString().Trim() == "0")
                {
                    ((DataTable)Session["CertificateTb1"]).Rows[n]["intWagePaymentReqid"] = Request.QueryString[0].ToString().Trim();  //Convert.ToString(i);
                }
            }

            GetNewRectoInsertdr2();
            int statuspr = Gen.bulkInsertdrInterDetails(myDtNewRecdr1);
            if (statuspr == 1)
            {
                lblmsg.Text = "Attachment Details Updated..!";
                Failure.Visible = false;
                success.Visible = true;
                DataSet ds2 = Gen.GetWageRequestbyIDWages1(Request.QueryString[0].ToString().Trim());
                if (ds2.Tables[0].Rows.Count > 0)
                {

                    DataTableReader rdt = new DataTableReader(ds2.Tables[0]);
                    IDataReader readert = rdt;

                    ((DataTable)Session["CertificateTb1"]).Clear();
                    ((DataTable)Session["CertificateTb1"]).Load(readert);
                    GridView1.DataSource = ((DataTable)Session["CertificateTb1"]);
                    GridView1.DataBind();
                }
            }
        }
    }
  protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
    
    protected void BtnSave3_Click(object sender, EventArgs e)
    {
 
    }

    protected void ddl_group_type_SelectedIndexChanged(object sender, EventArgs e)
    {
      

    }
    protected void ddl_group_code_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }

    protected void BtnSave2_Click(object sender, EventArgs e)
    {
       
    }

    public string DisableTheButton(Control pge, Control btn, string vldGroup)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("if (typeof(Page_ClientValidate) == 'function') {");
        if (vldGroup.Trim() == "")
        {
            sb.Append("if (Page_ClientValidate() == false) { return false; }} ");

        }
        else
        {
            sb.Append("if (Page_ClientValidate('" + vldGroup + "') == false) { return false; }} ");

        }
        sb.Append("this.value = 'Please wait...';");
        sb.Append("this.disabled = true;");
        sb.Append(pge.Page.GetPostBackEventReference(btn));
        //sb.Append(pge.Page.GetPostBackEventReference(btn));
        sb.Append(";");
        return sb.ToString();
    }

    protected void txtAmount_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
      
    }
    protected void txtExchangerate_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
       // Response.Redirect("WageIPProcessDashboard.aspx");
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        GridView1.Visible = true;
        try
        {
            if (Session["PhotoFilePath"].ToString() != null && Session["PhotoFilePath"].ToString() != "")
            {
                if (GridView1.Rows.Count == 0)
                {
                    Session["dtMyTable2"] = createtablecrtificate2();
                    Session["CertificateTb1"] = ((DataTable)Session["dtMyTable2"]);
                }
                DataTable dupTradeDataTable2;
                DataView dv = new DataView((DataTable)Session["CertificateTb1"]);
                dupTradeDataTable2 = dv.ToTable();

                if (dupTradeDataTable2.Rows.Count > 0)
                {
                    dupTradeDataTable2 = dv.ToTable();
                }


                AddDataToTableCeertificate1("", txtDescription.Text, ((DataTable)Session["CertificateTb1"]), Session["PhotoFileName"].ToString(), Session["PhotoFilePath"].ToString(), Session["uid"].ToString());

                this.GridView1.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
                this.GridView1.DataBind();

                lblFileName.Text = "";
                clear1();
                lblresult.Text = "";
                // GridView1.Columns[0].Visible = false;
            }
            else
            {
                lblresult.Text = "Please Upload Documnet";
            }

        }
        catch (Exception ee)
        {
            ////lbldtvalid.Text = "Please enter correct Date.";
            ////lbldtvalid.ForeColor = System.Drawing.Color.DarkRed;
        }

        GridView1.Visible = true;
    } 

    protected void GetNewRectoInsertdr2()
    {
        myDtNewRecdr1 = (DataTable)Session["CertificateTb1"];
        DataView dvdr1 = new DataView(myDtNewRecdr1);
        dvdr1.RowFilter = "intImageId = 0";
        myDtNewRecdr1 = dvdr1.ToTable();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
             
            HyperLink h1 = (HyperLink)e.Row.Cells[2].Controls[0];

            if (!string.IsNullOrEmpty(DataBinder.Eval(e.Row.DataItem, "PhotoFilePath").ToString()))
            {
                string pPath = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFilePath")).Trim();
                string vPath = pPath.Replace(path, "~").Replace(@"\", "/");
                h1.Text = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")).Trim();
                h1.NavigateUrl = vPath + "\\" + Convert.ToString(DataBinder.Eval(e.Row.DataItem, "PhotoFileName")).Trim();
                h1.Target = "_blank";
            }
            else
            {
                h1.Text = "No Document";
            }
        }
    }
    protected DataTable createtablecrtificate2()
    {
        dtMyTable2 = new DataTable("CertificateTb1");

        dtMyTable2.Columns.Add("new", typeof(string));
        dtMyTable2.Columns.Add("intWagePaymentReqid", typeof(string));
        dtMyTable2.Columns.Add("Description", typeof(string));
        dtMyTable2.Columns.Add("PhotoFileName", typeof(string));
        dtMyTable2.Columns.Add("PhotoFilePath", typeof(string));
        dtMyTable2.Columns.Add("Created_by", typeof(string));
        dtMyTable2.Columns.Add("intImageId", typeof(string));


        return dtMyTable2;
    }


    public void UploadFile1()
    {
        string LeaseFilePath = "";
        string LeaseFileName = "";

        string sFileDir6 = Server.MapPath("~\\WageFile");
        if ((FileUpload1.PostedFile != null) && (FileUpload1.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FileUpload1.PostedFile))
        {
            string sFileName = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
            try
            {

                string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
                int i = fileType.Length;
                if (fileType[i - 1].ToUpper().Trim() == "PDF" || fileType[i - 1].ToUpper().Trim() == "DOC" || fileType[i - 1].ToUpper().Trim() == "DOCX" || fileType[i - 1].ToUpper().Trim() == "JPG" || fileType[i - 1].ToUpper().Trim() == "JPEG" || fileType[i - 1].ToUpper().Trim() == "PNG")
                {
                    string newPath = "";
                    if (Session.Count > 0)
                    {
                        newPath = System.IO.Path.Combine(sFileDir6, Session["uid"].ToString());
                    }
                    else
                    {
                        newPath = System.IO.Path.Combine(sFileDir6, "1000");
                    }

                    newPath = newPath + System.DateTime.Now.ToString("ddMMyyyyhhmmss");

                    LeaseFilePath = newPath.ToString();

                    LeaseFileName = sFileName;
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

                    Session["PhotoFilePath"] = LeaseFilePath;
                    Session["PhotoFileName"] = LeaseFileName;
                    lblFileName.Text = Session["PhotoFileName"].ToString();

                }
                else
                {

                    //  lblresult.Text = "<font color='red'>Upload PDF,Doc files only..!</font>";
                    return;
                    //  Response.Write("<script>alert('Upload PDF,Doc files only  ')</script> "); //+ fileType[1].Trim(); 
                }
            }
            catch (Exception)//in case of an error
            {
                DeleteFile(LeaseFilePath + "\\" + LeaseFileName);
                // DeleteFile(sFileDir + sFileName);
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


    void clear1()
    {
        txtDescription.Text = "";
        Session["PhotoFilePath"] = "";
        Session["PhotoFileName"] = "";




    }
    private void AddDataToTableCeertificate1(string intWagePaymentReqid, string Description, DataTable myTable2,
      string PhotoFileName, string PhotoFilePath, string Created_by)
    {
        try
        {
            DataRow Row;
            Row = myTable2.NewRow();

            dtMyTable2 = new DataTable("CertificateTb1");


            Row["intWagePaymentReqid"] = intWagePaymentReqid;
            Row["Description"] = Description;
            Row["PhotoFileName"] = PhotoFileName;
            Row["PhotoFilePath"] = PhotoFilePath;
            Row["Created_by"] = Session["uid"].ToString().Trim();
            Row["intImageId"] = "0";

            myTable2.Rows.Add(Row);
        }
        catch (Exception ex)
        {
            //  ((DataTable)Session["myDatatable"]).Rows.Clear();
            //  Response.Redirect("~/EmpFacility.aspx");
            throw ex;
        }
    }

    protected void GridView2_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        if (((DataTable)Session["CertificateTb1"]).Rows.Count > 1)
        {
            try
            {
                if (((DataTable)Session["CertificateTb1"]).Rows[e.RowIndex]["intImageId"].ToString().Trim() == "0")
                {
                    ((DataTable)Session["CertificateTb1"]).Rows.RemoveAt(e.RowIndex);
                    this.GridView1.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
                    this.GridView1.DataBind();
                }
                else
                {
                    int i = Gen.DeletewageDocument(((DataTable)Session["CertificateTb1"]).Rows[e.RowIndex]["intImageId"].ToString().Trim(), Session["uid"].ToString());
                    if (i > 0)
                    {
                        ((DataTable)Session["CertificateTb1"]).Rows.RemoveAt(e.RowIndex);
                        this.GridView1.DataSource = ((DataTable)Session["CertificateTb1"]).DefaultView;
                        this.GridView1.DataBind();
                    }
                    else
                    {
                        lblmsg0.Text = "Failed to Update";
                        success.Visible = false;
                        Failure.Visible = true;
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
        else
        {
            lblmsg0.Text = "At least one Document is Mandatory";
            Failure.Visible = true;
            success.Visible = false;
            ScriptManager.RegisterClientScriptBlock((sender as Control), this.GetType(), "alert", lblmsg0.Text, true);
            return;
        }
    }


}

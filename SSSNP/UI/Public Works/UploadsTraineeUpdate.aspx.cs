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
using System.Text;
using System.IO;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Web.SessionState;
public partial class UploadsTraineeUpdate : System.Web.UI.Page
{
    DataTable dt = new DataTable();
    DataRow dr = null;

    DataTable dt1 = new DataTable();
    DataRow dr1 = null;
    General gen = new General();
    int i = 0;
    int j = 0;
    //common gen = new common();
   // General gen = new General();

    DataTable dt2 = new DataTable();
    comFunctions cmf = new comFunctions();
    DataRow dr2 = null;

    int cnt = 0;
    int month = 1;

    int myearsa ;

    int mmonthsa ;

    decimal amounta;
    int instalmenta = 1;
    DB.DB dbcon = new DB.DB();
    string path = "";

    DataTable mydt = new DataTable();
    static DataTable dtMyTable;
    static DataTable dtMyTableCertificate;
    DataTable mydtp = new DataTable();
    static DataTable dtMyTablep;
    static DataTable dtMyTableCertificatep;
    protected void Page_Load(object sender, EventArgs e)
    {
        //displayUpperFloors();
        if (Session.Count <= 0)
        {
            Response.Redirect("frmUserLogin.aspx");
        }
        //if (fupLetter.PostedFile != null && fupLetter.PostedFile.ContentLength > 0)
        //    UploadImage();
        this.btnSend.Attributes.Add("onclick", DisableTheButton(this.Page, this.btnSend, "group"));
        if (!IsPostBack)
        {
         //   fillgrid();
        //    ExportToExcel();
            //Session["ErrorData"] = null;
            //Session["ErrorDatap"] = null;
            hdfcount.Value = "0";
            Session.Remove("ErrorData");
            Session.Remove("ErrorDatap");
            dtMyTableCertificate = createtablecrtificate();
            Session["ErrorData"] = dtMyTableCertificate;
            dtMyTableCertificatep = createtablecrtificatePorted();
            Session["ErrorDatap"] = dtMyTableCertificatep;
           // GridView1.Visible = false;
            hfdClusterid.Value = Session["User_Code"].ToString().Trim();

            hdfcount.Value = "0";
            Label95.Text = "";
            Label94.Text = "";
            Label93.Text = "";
            btnSend0.Visible = false;
            btnSend1.Visible = false;
          //  lblresult.Text = fileuploadExcel.PostedFile.FileName;
            
        }
     //  Response.Redirect("ReportsforDownloads.aspx");
      
        //if (Request.QueryString.Count > 0 && hdfFlagID.Value == "0")
        //{
        //    hdfID.Value = Request.QueryString[0].ToString().Trim();
        //    FillData();
        //    //  Btnsave.Text = "Update";
        //    lblresult.Text = "";
        //}
    }

    //Select [Implementing Partner],[Enrollment No],[Household Name],[Household Size],[Sex],[Age],[Spouse Name],[No Audlts],[Dependents No],[Income],[Present State],[Present County],[Present Payam],[Present Boma],[Address],[Contact No],[Email],[Second Home],[Legal Status],[Biometric No],[Bank Name],[Account No],[Beneficiary S No] as sno from [TraineeDetails$]


    protected DataTable createtablecrtificate()
    {
        dtMyTable = new DataTable("ErrorData");


        dtMyTable.Columns.Add("[Beneficiary S No]", typeof(string));
        dtMyTable.Columns.Add("[Implementing Partner]", typeof(string));
        dtMyTable.Columns.Add("[Enrollment No]", typeof(string));
        dtMyTable.Columns.Add("[Household Name]", typeof(string));
        dtMyTable.Columns.Add("[Household Size]", typeof(string));
        dtMyTable.Columns.Add("Sex", typeof(string));
        dtMyTable.Columns.Add("Age", typeof(string));
        dtMyTable.Columns.Add("[Spouse Name]", typeof(string));
        dtMyTable.Columns.Add("[No Audlts]", typeof(string));
        dtMyTable.Columns.Add("[Dependents No]", typeof(string));
        dtMyTable.Columns.Add("Income", typeof(string));
        dtMyTable.Columns.Add("[Present State]", typeof(string));
        dtMyTable.Columns.Add("[Present County]", typeof(string));
        dtMyTable.Columns.Add("[Present Payam]", typeof(string));
        dtMyTable.Columns.Add("[Present Boma]", typeof(string));
        dtMyTable.Columns.Add("Address", typeof(string));
        dtMyTable.Columns.Add("[Contact No]", typeof(string));
        dtMyTable.Columns.Add("Email", typeof(string));
        dtMyTable.Columns.Add("[Second Home]", typeof(string));
        dtMyTable.Columns.Add("[Legal Status]", typeof(string));
        dtMyTable.Columns.Add("[Biometric No]", typeof(string));
        dtMyTable.Columns.Add("[Bank Name]", typeof(string));
        dtMyTable.Columns.Add("[Account No]", typeof(string));
       

        dtMyTable.Columns.Add("Name 1st Alternate Payee", typeof(string));
        dtMyTable.Columns.Add("Name 2nd Alternate Payee", typeof(string));
        dtMyTable.Columns.Add("Number Male Adults", typeof(string));
        dtMyTable.Columns.Add("Number Female Adults", typeof(string));
        dtMyTable.Columns.Add("Number Adults Age Above 65", typeof(string));
        dtMyTable.Columns.Add("Number AdultsAge Between 45_64", typeof(string));
        dtMyTable.Columns.Add("Number AdultsAge Between 18_30", typeof(string));
        dtMyTable.Columns.Add("Number Adults Age Between 30_45", typeof(string));
        dtMyTable.Columns.Add("Number Female Children", typeof(string));
        dtMyTable.Columns.Add("Number Male Children", typeof(string));
        dtMyTable.Columns.Add("Number Children Age Above 5 years", typeof(string));
        dtMyTable.Columns.Add("Number Children Age Above 12 years", typeof(string));
        dtMyTable.Columns.Add("Number Children Age Above 18 years", typeof(string));
        dtMyTable.Columns.Add("Ethnicity Tribe Rgin", typeof(string));

        dtMyTable.Columns.Add("Reason", typeof(string));
        dtMyTable.Columns.Add("FirstAlternatePayeeGender", typeof(string));
        dtMyTable.Columns.Add("FirstAlternatePayeeAge", typeof(string));
        dtMyTable.Columns.Add("FirstAlternatePayeeNationID", typeof(string));
        dtMyTable.Columns.Add("FirstAlternatePayeeMobile", typeof(string));

        dtMyTable.Columns.Add("SecondAlternatePayeeGender", typeof(string));
        dtMyTable.Columns.Add("SecondAlternatePayeeAge", typeof(string));
        dtMyTable.Columns.Add("SecondAlternateNationalID", typeof(string));
        dtMyTable.Columns.Add("SecondAlternatePayeeMobile", typeof(string));

        return dtMyTable;
    }


    private void AddDataToTableCeertificate(DataTable myTable, string IPName, string EnrollmentNo,string HouseholdName, string HouseholdSize, string Sex, string Age, string SpouseName, string Audltsno, string Dependentsno, string income, string State, string County, string payam, string boma, string address, string Contactno, string Email, string Secondhome, string legalStatus, string biometricno, string bankname, string Accno, string TraineeSNo,
        string Name1stAlternatePayee, string Name2ndAlternatePayee, string NumberMaleAdults, string NumberFemaleAdults, string NumberAdultsAgeAbove65, string NumberAdultsAgeBetween45_64, string NumberAdultsAgeBetween18_30, string NumberAdultsAgeBetween30_45, string NumberFemaleChildren, string NumberMaleChildren, string NumberChildrenAgeAbove5years, string NumberChildrenAgeAbove12years, string NumberChildrenAgeAbove18years, string EthnicityTribeRgin, string Reason, string FirstAlternatePayeeGender
, string FirstAlternatePayeeAge
, string FirstAlternatePayeeNationID
, string FirstAlternatePayeeMobile
, string SecondAlternatePayeeGender
, string SecondAlternatePayeeAge
, string SecondAlternateNationalID
, string SecondAlternatePayeeMobile)
    {//totStartDate, string totEndDate, string totSummary,
        try
        {
            DataRow Row;
            Row = myTable.NewRow();

            dtMyTable = new DataTable("ErrorData");

            Row["[Beneficiary S No]"] = TraineeSNo;
            Row["[Implementing Partner]"] = IPName;
            Row["[Enrollment No]"] = EnrollmentNo;
            Row["[Household Name]"] = HouseholdName;
            Row["[Household Size]"] = HouseholdSize;
            Row["Sex"] = Sex;
            Row["Age"] = Age;
            Row["[Spouse Name]"] = SpouseName;
            Row["[No Audlts]"] = Audltsno;
            Row["[Dependents No]"] = Dependentsno;
            Row["Income"] = income;
            Row["Address"] = address;
            Row["[Present State]"] = State;
            Row["[Present County]"] = County;
            Row["[Present Payam]"] = payam;
            Row["[Present Boma]"] = boma;
            Row["[Contact No]"] = Contactno;
            Row["Email"] = Email;
            Row["[Second Home]"] = Secondhome;
            Row["[Legal Status]"] = legalStatus;
            Row["[Biometric No]"] = biometricno;
            Row["[Bank Name]"] = bankname;
            Row["[Account No]"] = Accno;
            Row["[Beneficiary S No]"] = TraineeSNo;
            //  Row["Languages"] = Languages;
            

            Row["Name 1st Alternate Payee"] = Name1stAlternatePayee;
            Row["Name 2nd Alternate Payee"] = Name2ndAlternatePayee;
            Row["Number Male Adults"] = NumberMaleAdults;
            Row["Number Female Adults"] = NumberFemaleAdults;
            Row["Number Adults Age Above 65"] = NumberAdultsAgeAbove65;
            Row["Number AdultsAge Between 45_64"] = NumberAdultsAgeBetween45_64;
            Row["Number AdultsAge Between 18_30"] = NumberAdultsAgeBetween18_30;
            Row["Number Adults Age Between 30_45"] = NumberAdultsAgeBetween30_45;
            Row["Number Female Children"] = NumberFemaleChildren;
            Row["Number Male Children"] = NumberMaleChildren;
            Row["Number Children Age Above 5 years"] = NumberChildrenAgeAbove5years;
            Row["Number Children Age Above 12 years"] = NumberChildrenAgeAbove12years;
            Row["Number Children Age Above 18 years"] = NumberChildrenAgeAbove18years;
            Row["Ethnicity Tribe Rgin"] = EthnicityTribeRgin;

            Row["Reason"] = Reason;
            Row["FirstAlternatePayeeGender"] = FirstAlternatePayeeGender;
            Row["FirstAlternatePayeeAge"] = FirstAlternatePayeeAge;
            Row["FirstAlternatePayeeNationID"] = FirstAlternatePayeeNationID;
            Row["FirstAlternatePayeeMobile"] = FirstAlternatePayeeMobile;
            Row["SecondAlternatePayeeGender"] = SecondAlternatePayeeGender;
            Row["SecondAlternatePayeeAge"] = SecondAlternatePayeeAge;
            Row["SecondAlternateNationalID"] = SecondAlternateNationalID;
            Row["SecondAlternatePayeeMobile"] = SecondAlternatePayeeMobile;
            myTable.Rows.Add(Row);
        }
        catch (Exception ee)
        {
            //  ((DataTable)Session["myDatatable"]).Rows.Clear();
            //  Response.Redirect("~/EmpFacility.aspx");
        }
    }

    protected DataTable createtablecrtificatePorted()
    {
        dtMyTablep = new DataTable("ErrorDatap");


        dtMyTablep.Columns.Add("[Beneficiary S No]", typeof(string));
        dtMyTablep.Columns.Add("[Implementing Partner]", typeof(string));
        dtMyTablep.Columns.Add("[Enrollment No]", typeof(string));
        dtMyTablep.Columns.Add("[Household Name]", typeof(string));
        dtMyTablep.Columns.Add("[Household Size]", typeof(string));
        dtMyTablep.Columns.Add("Sex", typeof(string));
        dtMyTablep.Columns.Add("Age", typeof(string));
        dtMyTablep.Columns.Add("[Spouse Name]", typeof(string));
        dtMyTablep.Columns.Add("[No Audlts]", typeof(string));
        dtMyTablep.Columns.Add("[Dependents No]", typeof(string));
        dtMyTablep.Columns.Add("Income", typeof(string));
        dtMyTablep.Columns.Add("[Present State]", typeof(string));
        dtMyTablep.Columns.Add("[Present County]", typeof(string));
        dtMyTablep.Columns.Add("[Present Payam]", typeof(string));
        dtMyTablep.Columns.Add("[Present Boma]", typeof(string));
        dtMyTablep.Columns.Add("Address", typeof(string));
        dtMyTablep.Columns.Add("[Contact No]", typeof(string));
        dtMyTablep.Columns.Add("Email", typeof(string));
        dtMyTablep.Columns.Add("[Second Home]", typeof(string));
        dtMyTablep.Columns.Add("[Legal Status]", typeof(string));
        dtMyTablep.Columns.Add("[Biometric No]", typeof(string));
        dtMyTablep.Columns.Add("[Bank Name]", typeof(string));
        dtMyTablep.Columns.Add("[Account No]", typeof(string));
        

        dtMyTablep.Columns.Add("Name 1st Alternate Payee", typeof(string));
        dtMyTablep.Columns.Add("Name 2nd Alternate Payee", typeof(string));
        dtMyTablep.Columns.Add("Number Male Adults", typeof(string));
        dtMyTablep.Columns.Add("Number Female Adults", typeof(string));
        dtMyTablep.Columns.Add("Number Adults Age Above 65", typeof(string));
        dtMyTablep.Columns.Add("Number AdultsAge Between 45_64", typeof(string));
        dtMyTablep.Columns.Add("Number AdultsAge Between 18_30", typeof(string));
        dtMyTablep.Columns.Add("Number Adults Age Between 30_45", typeof(string));
        dtMyTablep.Columns.Add("Number Female Children", typeof(string));
        dtMyTablep.Columns.Add("Number Male Children", typeof(string));
        dtMyTablep.Columns.Add("Number Children Age Above 5 years", typeof(string));
        dtMyTablep.Columns.Add("Number Children Age Above 12 years", typeof(string));
        dtMyTablep.Columns.Add("Number Children Age Above 18 years", typeof(string));
        dtMyTablep.Columns.Add("Ethnicity Tribe Rgin", typeof(string));

        dtMyTablep.Columns.Add("Reason", typeof(string));

        dtMyTablep.Columns.Add("FirstAlternatePayeeGender", typeof(string));
        dtMyTablep.Columns.Add("FirstAlternatePayeeAge", typeof(string));
        dtMyTablep.Columns.Add("FirstAlternatePayeeNationID", typeof(string));
        dtMyTablep.Columns.Add("FirstAlternatePayeeMobile", typeof(string));

        dtMyTablep.Columns.Add("SecondAlternatePayeeGender", typeof(string));
        dtMyTablep.Columns.Add("SecondAlternatePayeeAge", typeof(string));
        dtMyTablep.Columns.Add("SecondAlternateNationalID", typeof(string));
        dtMyTablep.Columns.Add("SecondAlternatePayeeMobile", typeof(string));

        return dtMyTablep;
    }

    //Select [Implementing Partner],[Enrollment No],[Household Name],[Household Size],[Sex],[Age],[Spouse Name],[No Audlts],[Dependents No],[Income],[Present State],[Present County],[Present Payam],[Present Boma],[Address],[Contact No],[Email],[Second Home],[Legal Status],[Biometric No],[Bank Name],[Account No],[Beneficiary S No] as sno from [TraineeDetails$]


    private void AddDataToTableCeertificatePorted(DataTable myTable, string IPName, string EnrollmentNo, string HouseholdName, string HouseholdSize, string Sex, string Age, string SpouseName, string Audltsno, string Dependentsno, string income, string State, string County, string payam, string boma, string address, string Contactno, string Email, string Secondhome, string legalStatus, string biometricno, string bankname, string Accno, string TraineeSNo,
         string Name1stAlternatePayee, string Name2ndAlternatePayee, string NumberMaleAdults, string NumberFemaleAdults, string NumberAdultsAgeAbove65, string NumberAdultsAgeBetween45_64, string NumberAdultsAgeBetween18_30, string NumberAdultsAgeBetween30_45, string NumberFemaleChildren, string NumberMaleChildren, string NumberChildrenAgeAbove5years, string NumberChildrenAgeAbove12years, string NumberChildrenAgeAbove18years, string EthnicityTribeRgin, string Reason, string FirstAlternatePayeeGender
, string FirstAlternatePayeeAge
, string FirstAlternatePayeeNationID
, string FirstAlternatePayeeMobile
, string SecondAlternatePayeeGender
, string SecondAlternatePayeeAge
, string SecondAlternateNationalID
, string SecondAlternatePayeeMobile)
        //, string Trade, string Education, string Languages)

        
    {//totStartDate, string totEndDate, string totSummary,
        try
        {
            DataRow Row;
            Row = myTable.NewRow();

            dtMyTablep = new DataTable("ErrorDatap");

            Row["[Beneficiary S No]"] = TraineeSNo;
            Row["[Implementing Partner]"] = IPName;
            Row["[Enrollment No]"] = EnrollmentNo;
            Row["[Household Name]"] = HouseholdName;
            Row["[Household Size]"] = HouseholdSize;
            Row["Sex"] = Sex;
            Row["Age"] = Age;
            Row["[Spouse Name]"] = SpouseName;
            Row["[No Audlts]"] = Audltsno;
            Row["[Dependents No]"] = Dependentsno;
            Row["Income"] = income;
            Row["Address"] = address;
            Row["[Present State]"] = State;
            Row["[Present County]"] = County;
            Row["[Present Payam]"] = payam;
            Row["[Present Boma]"] = boma;
            Row["[Contact No]"] = Contactno;
            Row["Email"] = Email;
            Row["[Second Home]"] = Secondhome;
            Row["[Legal Status]"] = legalStatus;
            Row["[Biometric No]"] = biometricno;
            Row["[Bank Name]"] = bankname;
            Row["[Account No]"] = Accno;
            Row["[Beneficiary S No]"] = TraineeSNo;
          //  Row["Languages"] = Languages;
            

            Row["Name 1st Alternate Payee"] = Name1stAlternatePayee;
            Row["Name 2nd Alternate Payee"] = Name2ndAlternatePayee;
            Row["Number Male Adults"] = NumberMaleAdults;
            Row["Number Female Adults"] = NumberFemaleAdults;
            Row["Number Adults Age Above 65"] = NumberAdultsAgeAbove65;
            Row["Number AdultsAge Between 45_64"] = NumberAdultsAgeBetween45_64;
            Row["Number AdultsAge Between 18_30"] = NumberAdultsAgeBetween18_30;
            Row["Number Adults Age Between 30_45"] = NumberAdultsAgeBetween30_45;
            Row["Number Female Children"] = NumberFemaleChildren;
            Row["Number Male Children"] = NumberMaleChildren;
            Row["Number Children Age Above 5 years"] = NumberChildrenAgeAbove5years;
            Row["Number Children Age Above 12 years"] = NumberChildrenAgeAbove12years;
            Row["Number Children Age Above 18 years"] = NumberChildrenAgeAbove18years;
            Row["Ethnicity Tribe Rgin"] = EthnicityTribeRgin;

            Row["Reason"] = Reason;
            Row["FirstAlternatePayeeGender"] = FirstAlternatePayeeGender;
            Row["FirstAlternatePayeeAge"] = FirstAlternatePayeeAge;
            Row["FirstAlternatePayeeNationID"] = FirstAlternatePayeeNationID;
            Row["FirstAlternatePayeeMobile"] = FirstAlternatePayeeMobile;
            Row["SecondAlternatePayeeGender"] = SecondAlternatePayeeGender;
            Row["SecondAlternatePayeeAge"] = SecondAlternatePayeeAge;
            Row["SecondAlternateNationalID"] = SecondAlternateNationalID;
            Row["SecondAlternatePayeeMobile"] = SecondAlternatePayeeMobile;
            myTable.Rows.Add(Row);
        }
        catch (Exception ee)
        {
            //  ((DataTable)Session["myDatatable"]).Rows.Clear();
            //  Response.Redirect("~/EmpFacility.aspx");
        }
    }


    #region Button Disable
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
        sb.Append(";");
        return sb.ToString();
    }


    #endregion
    public void FillData()
    {

        

          



    }

    public void displayUpperFloors()
    {
     
        try
        {
          

          


        }
        catch (Exception ex)
        {

            lblError.Text = "<font color=red>" + ex.ToString() + "</font>";
        }
    }
    protected void grdUpperFloors_RowCreated(object sender, GridViewRowEventArgs e)
    {
      
    }

    public void fillgrid()
    {
        //DataSet dsnew = new DataSet();

        //dsnew = gen.GetSHGMemberGradingDownloads(Session["User_Code"].ToString().Trim(), System.DateTime.Now.AddMonths(-1).ToString("yyyy").Trim(), System.DateTime.Now.AddMonths(-1).ToString("MM").Trim());
        //    if (dsnew.Tables[0].Rows.Count > 0)
        //    {
        //        grdDetails.DataSource = dsnew.Tables[0];
        //        grdDetails.DataBind();



             


        //    }
        //    else
        //    {

        //        grdDetails.DataSource = null;
        //        grdDetails.DataBind();
        //    }
     
    }


  

   
 
    protected void grdHistorical_RowCreated(object sender, GridViewRowEventArgs e)
    {
     
    }
    protected void grdHistorical_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowType == DataControlRowType.DataRow))
        {
          

             
          
        
        
        
        
        }
    }
    protected void ddlLoan0_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }



    protected void ddlBlockname_SelectedIndexChanged(object sender, EventArgs e)
    {
      
    }
    protected void ddlClustername_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlpanchayatname_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
     
   
  
    protected void ddlMandal1_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void ddlSHG_SelectedIndexChanged(object sender, EventArgs e)
    {

       
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
     

    }




    protected void btnSend_Click(object sender, EventArgs e)
    {
        //file upload path
       // string path = Session["letterUploadFileName"].ToString();
       // string path = fupLetter.PostedFile.FileName;
        //lblresult.Text = fupLetter.PostedFile.FileName;
        //path = Session["letterUploadFileName"].ToString();
        //Create connection string to Excel work book
      //  string path = "F:\\Srikanth\\ClusterwiseSHGGradingDetails.xlsx";
      //  string path = Convert.ToString(System.IO.Path.GetFullPath(fupLetter.PostedFile.FileName));
   //     string path = Convert.ToString(fupLetter.PostedFile.FileName);
       // string saveFolder = @"C:\temp\uploads"; //Pick a folder on your machine to store the uploaded files

        btnSend0.Visible = true;
        btnSend1.Visible = true;
        string saveFolder = @"C:\\temp\\uploads";
        
        string newPath = "";

        //Create a new subfolder under the current active folder
        newPath = System.IO.Path.Combine(saveFolder, DateTime.Now.ToString("ddMMyyyyhhmmsstt").Trim());

        // Create the subfolder
        if (!Directory.Exists(newPath))
            System.IO.Directory.CreateDirectory(newPath);

        path = newPath + "\\" + fupLetter.FileName;

        fupLetter.SaveAs(path);
       
        //}
        //else //reject file
        //{
        //    lblError.Visible = true;
        //    lblError.Text = "File Size if Over the Limit of " + lMaxFileSize;
        //}

















        //path = Path.Combine(saveFolder,fupLetter.FileName);

        //fupLetter.SaveAs(path);
  //  path  = "D:\\SSSS\\ClusterwiseSHGGradingDetailsN.xlsx";
      //  string excelConnectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=Excel 12.0;Persist Security Info=False";

        string excelConnectionString = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=\"Excel 12.0\"", path);
        //Create Connection to Excel work book
        OleDbConnection excelConnection = new OleDbConnection(excelConnectionString);
        //Create OleDbCommand to fetch data from Excel

     

      
    


        DataSet dsImp=new DataSet();
        //OleDbCommand cmd = new OleDbCommand("Select [Training Center],[Project Name],[Trainee Name],[Father / Husband Name],[Date of Birth (MM/DD/YYYY)],[Category],[Gender], [Education Qualification],Address,[Present State],[Present District],[Present Block],[Present Gram Panchayat],[Present PIN Code],[Mobile No],[Alternative Mobile No],[Email],[Religion],[Adhaar No] , [Beneficiary S No] as sno,Trade,Education,Languages from [TraineeDetails$]", excelConnection);

        OleDbCommand cmd = new OleDbCommand("Select [Implementing Partner],[Enrollment No] ,[Household Name],[Household Size],[Sex],[Age],[Spouse Name],[No Audlts],[Dependents No],[Income],[Present State],[Present County],[Present Payam],[Present Boma],[Address],[Contact No],[Email],[Second Home],[Legal Status],[Biometric No],[Bank Name],[Account No],[Beneficiary S No] as sno,[Name 1st Alternate Payee],[Name 2nd Alternate Payee],[Number Male Adults],[Number Female Adults],[Number Adults Age Above 65],[Number AdultsAge Between 45_64],[Number AdultsAge Between 18_30],[Number Adults Age Between 30_45],[Number Female Children],[Number Male Children],[Number Children Age Above 5 years],[Number Children Age Above 12 years],[Number Children Age Above 18 years],[Ethnicity Tribe Rgin],FirstAlternatePayeeGender,	FirstAlternatePayeeAge,	FirstAlternatePayeeNationID,	FirstAlternatePayeeMobile,	SecondAlternatePayeeGender,	SecondAlternatePayeeAge,	SecondAlternateNationalID,	SecondAlternatePayeeMobile from [Beneficiary$]", excelConnection);

        excelConnection.Open();
        OleDbDataAdapter Oledbadp= new OleDbDataAdapter();
        Oledbadp.SelectCommand=cmd;
       
        Oledbadp.Fill(dsImp);
        //OleDbDataReader dReader;
        //dReader = cmd.ExecuteReader();
        DataSet dsnew = new DataSet();
        hdfcount.Value = dsImp.Tables[0].Rows.Count.ToString();
        int s = 0;
        for (s = 0; s < dsImp.Tables[0].Rows.Count; s++)
        {
            try
            {
                //dsnew = gen.GetImportSHGCheck(dsImp.Tables[0].Rows[s][1].ToString().Trim());

               

                    try
                    {
                        //if (dsImp.Tables[0].Rows[s][0].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][1].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][2].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][3].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][4].ToString().Trim() != "")

                            if (dsImp.Tables[0].Rows[s][0].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][1].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][2].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][3].ToString().Trim() != "" && dsImp.Tables[0].Rows[s][4].ToString().Trim() != "")
                        {

                             dsnew = gen.getTraineedetAutometicPortForUpdate(
                
                dsImp.Tables[0].Rows[s][1].ToString().Trim().Trim(),
                dsImp.Tables[0].Rows[s][2].ToString().Trim().Trim(),
                dsImp.Tables[0].Rows[s][3].ToString().Trim().Trim(),
                dsImp.Tables[0].Rows[s][6].ToString().Trim().Trim(),
                dsImp.Tables[0].Rows[s][8].ToString().Trim().Trim(),
                dsImp.Tables[0].Rows[s][23].ToString().Trim().Trim(),
                dsImp.Tables[0].Rows[s][24].ToString().Trim().Trim());
                             //dsImp.Tables[0].Rows[s][0].ToString().Trim().Trim(),
                if (dsnew.Tables[0].Rows.Count <= 0)
                {

                    if (dsImp.Tables[0].Rows[s][22].ToString().Trim() != "")
                    {
                                //General Gen = new General();
                                //DataSet ds = new DataSet();
                                int s1 = 0;
                              
                                //duplicay check


                                //exceed check
                              
                                                                          
                                            s1 = gen.updatebenficiaryDetAutomatic(dsImp.Tables[0].Rows[s][0].ToString().Trim()
                                                , dsImp.Tables[0].Rows[s][1].ToString().Trim()
                                                , dsImp.Tables[0].Rows[s][2].ToString().Trim()
                                                , dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                                        , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][19].ToString().Trim()

                                                         , dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                                          , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                                           , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                                           , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                                        , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                                        , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                                       , Session["uid"].ToString()
                                                        , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                                        );
                            


                        if (s1 > 0)
                        {
                            if (s1.ToString().Length > 3)
                            {

                            }
                            else
                            {
                                if (s1.ToString().Trim() == "1")
                                {
                                    AddDataToTableCeertificatePorted((DataTable)Session["ErrorDatap"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                ,  dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                 , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                  
                                , "Ported"
                                , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());
                                  
                                }

                               

                                else if (s1.ToString().Trim() == "99")
                                        {
                                            //  string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Training Center doesnot exist";

                                            AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                        , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                        , dsImp.Tables[0].Rows[s][36].ToString().Trim()

                                    , "Already exist", dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());
                                        }

                                        else if (dsImp.Tables[0].Rows[s][0].ToString().Trim() != "")
                                {

                                    if (s1.ToString().Trim() == "100")
                                    {
                                        //  string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Training Center doesnot exist";

                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                ,dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                 , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()

                                , "IP doesnot exist", dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());
                                    }
                                    else if (s1.ToString().Trim() == "101")
                                    {
                                        //   string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Project doesnot exist";


                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                              , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][19].ToString().Trim(), "Project doesnot exist", dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                               , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                  );

                                    }
                                    else if (s1.ToString().Trim() == "102")
                                    {
                                        //  string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : State  doesnot exist";




                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                             , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][19].ToString().Trim(), dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim(), dsImp.Tables[0].Rows[s][22].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                
                             , "State does not Exist"
                                , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());



                                    }
                                    else if (s1.ToString().Trim() == "777")
                                    {
                                        //  string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : State  doesnot exist";




                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                             , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                             , dsImp.Tables[0].Rows[s][19].ToString().Trim(), "Ported XL is not in Training Center List", dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim(), dsImp.Tables[0].Rows[s][22].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                    );



                                    }
                                    else if (s1.ToString().Trim() == "103")
                                    {
                                        //    string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : District ported";

                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                           , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                           , dsImp.Tables[0].Rows[s][19].ToString().Trim(),  dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim(), dsImp.Tables[0].Rows[s][22].ToString().Trim(), "County does not exist", dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                  );
                                    }
                                    else if (s1.ToString().Trim() == "104")
                                    {
                                        //   string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Project doesnot exist";


                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                              , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][19].ToString().Trim(),  dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim(), dsImp.Tables[0].Rows[s][22].ToString().Trim(), "Payam doesnot exist", dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                  );

                                    }
                                    else if (s1.ToString().Trim() == "105")
                                    {
                                        //   string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Project doesnot exist";


                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                              , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][19].ToString().Trim(),  dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim(), dsImp.Tables[0].Rows[s][22].ToString().Trim(), "Boma doesnot exist", dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                    );

                                    }
                                    else if (s1.ToString().Trim() == "997")
                                    {
                                        //   string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Project doesnot exist";


                                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                              , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                              , dsImp.Tables[0].Rows[s][19].ToString().Trim(), "Language doesnot exist", dsImp.Tables[0].Rows[s][20].ToString().Trim(), dsImp.Tables[0].Rows[s][21].ToString().Trim(), dsImp.Tables[0].Rows[s][22].ToString().Trim()
                               , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                    );

                                    }
                                }








                            }
                        }
                    }
                    else
                    {

                        AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                   , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                   , "Trade was not Entered"
                                   , dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                   , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                   ,dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim()
                                   );
                    }
                    }
                        else
                        {
                         //   string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Duplicate trainee ported";

                            if (dsImp.Tables[0].Rows[s][0].ToString().Trim() != "")
                            {
                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                     , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                     , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                    
                                     , "Duplicate beneficiary ported"
                                        , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());

                            }
                    }




                        }
                        else
                        {

                            // Mail Content
                            //string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Manditory Records are missing";

                            if (dsImp.Tables[0].Rows[s][0].ToString().Trim() != "")
                            {

                                AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                    ,  dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                   
                                    , "Manditory Records are missing"
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());

                            }
                            }
                        
                    }
                    catch (Exception ex)
                    {
                       // string subj = "S.No: " + dsImp.Tables[0].Rows[s][19].ToString().Trim() + " <br> Trainee Name: " + dsImp.Tables[0].Rows[s][2].ToString().Trim() + " <br> Father Name: " + dsImp.Tables[0].Rows[s][3].ToString().Trim() + "<br> Date of Birth : " + dsImp.Tables[0].Rows[s][4].ToString().Trim() + "<br> Reason : Error in Record";
                        if (dsImp.Tables[0].Rows[s][0].ToString().Trim() != "")
                        {
                            AddDataToTableCeertificate((DataTable)Session["ErrorData"], dsImp.Tables[0].Rows[s][0].ToString().Trim(), dsImp.Tables[0].Rows[s][1].ToString().Trim(), dsImp.Tables[0].Rows[s][2].ToString().Trim(), dsImp.Tables[0].Rows[s][3].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][4].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][5].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][6].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][7].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][8].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][9].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][10].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][11].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][12].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][13].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][14].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][15].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][16].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][17].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][18].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][19].ToString().Trim()
                                    ,  dsImp.Tables[0].Rows[s][20].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][21].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][22].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][23].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][24].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][25].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][26].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][27].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][28].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][29].ToString().Trim()

                                    , dsImp.Tables[0].Rows[s][30].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][31].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][32].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][33].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][34].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][35].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][36].ToString().Trim()
                                    
                                    , "Error in Record - " + ex.ToString()
                                       , dsImp.Tables[0].Rows[s][37].ToString().Trim()
                                  , dsImp.Tables[0].Rows[s][38].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][39].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][40].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][41].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][42].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][43].ToString().Trim()
                                    , dsImp.Tables[0].Rows[s][44].ToString().Trim());



                        }
                        }
                    finally
                    {
                    }
                    //Response.Clear();
                    //Response.AddHeader("content-disposition", "attachment;filename=ErrorSummary" + DateTime.Now.ToString("ddMMyyyyhhmmsstt").Trim() + ".xls");
                    //Response.Charset = "";

                    //Response.ContentType = "application/vnd.xls";
                    //System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    //System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                  



                
            }
            catch (Exception ex)
            {
            }
            finally
            {
            }

        }

        

        GridView1.Visible = true;
        GridView1.DataSource = (DataTable)Session["ErrorData"];
        GridView1.DataBind();

        Label95.Text ="Total Beneficiaries: "+ hdfcount.Value;
        Label94.Text = "Ported Beneficiaries: " + Convert.ToString(Convert.ToInt32(hdfcount.Value) - Convert.ToInt32(((DataTable)Session["ErrorData"]).Rows.Count));

        Label93.Text = "Failed Beneficiaries: " + ((DataTable)Session["ErrorData"]).Rows.Count.ToString();
        GridView2.DataSource = (DataTable)Session["ErrorDatap"];
        GridView2.DataBind();
        lblresult.Text = "Import is Completed";
        excelConnection.Close();
    }
    private string GetLocalFilePath(string saveDirectory, FileUpload fileUploadControl)
    {


        string filePath = Path.Combine(saveDirectory, fileUploadControl.FileName);

        fileUploadControl.SaveAs(filePath);

        return filePath;

    }

    #region capture Image and Store of Upload Scan Market Letter

    void UploadImage()
    {
        //Session["File"] = fluPicture.FileBytes;
        //Session["Name"] = fluPicture.FileName;

        // string scanletterFileName = "";

        /// Commented by Srikanth 0n 13-3-2012 at 6.40PM and Modified
        //   letterUploadFileName = System.IO.Path.GetFileName(fupLetter.PostedFile.FileName);



      


        try
        {
            string[] fileType = fupLetter.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;

            //Session["letterUploadFilePath"]=fupLetter.PostedFile.FileName.P
            Session["letterUpload"] = new byte[fupLetter.PostedFile.ContentLength];
            if (fileType[letterI - 1].ToUpper().Trim() == "XLS" || fileType[letterI - 1].ToUpper().Trim() == "XLSX")
            {
                Session["letterUploadFileName"] = fupLetter.PostedFile.FileName;
                
            }
            else
            {
                lblresult.Text = "Please Upload Excel Sheet Only (What we Provide to you)";
            }

        }
        catch (Exception ex)
        {

            lblresult.Text = "An Error Occured. Please Try Again!" + ex.Message;

        }

        finally
        {

        }




    }
    #endregion




    
    // This Method is used to render gridview control
    public string GetGridviewData(GridView gv)
    {
        StringBuilder strBuilder = new StringBuilder();
        StringWriter strWriter = new StringWriter(strBuilder);
        HtmlTextWriter htw = new HtmlTextWriter(strWriter);
        gv.RenderControl(htw);
        return strBuilder.ToString();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void btnSend0_Click(object sender, EventArgs e)
    {
        ExportToExcel();
    }
    protected void ExportToExcel()
    {

        //GridView Export = new GridView();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=TraineePorted.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {
            // grdBReg.DataBind();

            //lblresult0.Visible = true;

            //            Export.DataSource = FillBRegister();
            //          Export.DataBind();
           
            //grdDetails.DataSource = //((DataTable)Session["dtt_load"]).DefaultView; ;

            // grdDetails.DataBind();
            //gen.fillhimayatsummary(grdDetails);

            GridView2.RenderControl(htw);

            // grdBReg.RenderControl(htw);
            // ((DataTable)Session["dtt_load"]).Clear();
        }
        catch (Exception eee)
        {
        }






        //DataTable dt = new DataTable();


        //print.RenderControl(htw);
        Page.Response.Write(sw.ToString());

        grdDetails.PageSize = 15;
        //   LinkButton1.PostBackUrl = Request.Url.ToString();
        //lblresult0.Visible = false;
        Response.End();
    }

    protected void ExportToExcelFailed()
    {

        //GridView Export = new GridView();

        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/vnd.ms-excel";
        Response.AddHeader("content-disposition", "attachment;filename=TraineeFailedList.xls");
        Response.Charset = "";
        // this.EnableViewState = false;
        Page.Response.Cache.SetCacheability(HttpCacheability.NoCache);

        System.IO.StringWriter sw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

        try
        {
            // grdBReg.DataBind();

            //lblresult0.Visible = true;

            //            Export.DataSource = FillBRegister();
            //          Export.DataBind();
           
            //grdDetails.DataSource = //((DataTable)Session["dtt_load"]).DefaultView; ;

            // grdDetails.DataBind();
            //gen.fillhimayatsummary(grdDetails);

            GridView1.RenderControl(htw);

            // grdBReg.RenderControl(htw);
            // ((DataTable)Session["dtt_load"]).Clear();
        }
        catch (Exception eee)
        {
        }






        //DataTable dt = new DataTable();


        //print.RenderControl(htw);
        Page.Response.Write(sw.ToString());

        grdDetails.PageSize = 15;
        //   LinkButton1.PostBackUrl = Request.Url.ToString();
        //lblresult0.Visible = false;
        Response.End();
    }

    protected void btnSend1_Click(object sender, EventArgs e)
    {
        ExportToExcelFailed();
    }
}

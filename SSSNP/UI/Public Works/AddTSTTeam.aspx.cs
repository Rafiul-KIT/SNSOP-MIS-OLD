
#region Using/Inherits Classes
using System;
using System.Web;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
#endregion

#region Amendment History Begin
//Form Name : TST Registration
//Purpose : To Add TST Details
//Tables used : td_TstTeamDet,tbl_Users,tm_States,tm_Counties,tm_Payams,tm_Desginations
//Stored procedures Used : sp_getStates,sp_getCounties,sp_getPayams,getTSTbyID,deleteTST,insrtTST,InsertUpdateAttachements_Sp.sp_Designations
#endregion Amendment History End

public partial class AddTSTTeam : System.Web.UI.Page
{

    #region reference class calling
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";
    comFunctions cmf = new comFunctions();
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        #region Is not Postbacks master Functions Calling
        if (!IsPostBack)
        {
            Gen.getStates(ddlstate);           
            Session["Photo"] = Photo;
            Session["PhotoFileName"] = "";
            if (FileUpload1.HasFile)
            {
                Session["FileUpload11"] = FileUpload1;
                hdfUploadFile1.Value = FileUpload1.FileName;
            }
            else if (Session["FileUpload11"] != null)
            {
                FileUpload1 = (FileUpload)Session["FileUpload11"];
                hdfUploadFile1.Value = FileUpload1.FileName;
            }

        }
        #endregion
        #region Photo and Fill Intialization
        // Restrict file upload        
        if (FileUpload1.PostedFile != null && FileUpload1.PostedFile.ContentLength > 0 && !Gen.IsExecutable(FileUpload1.PostedFile))
        {
            if (FileUpload1.PostedFile.ContentLength <= 307200)
            {
                lblmsg.Text = "";
                //lblFileName.Text = "";
                UploadImage();

            }
            else
                lblmsg.Text = "Trainee image should be less than 300 KB";
        }
        
        #endregion
        #region fill details using Lookup
        if ((hdfID.Value.ToString().Trim() != "" && hdfFlagID.Value.ToString().Trim() == "0"))
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

    #region Doc or Image Binding
    void UploadImage()
    {

        //PhotoFilename = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        Session["PhotoFileName"] = System.IO.Path.GetFileName(FileUpload1.PostedFile.FileName);
        string path = Server.MapPath("~/tempsave/" + FileUpload1.PostedFile.FileName);
        FileUpload1.PostedFile.SaveAs(path);
        HiddenFieldFilePath.Value = path;
        try
        {
            string[] fileType = FileUpload1.PostedFile.FileName.Split('.');
            int letterI = fileType.Length;
            //Photo = new byte[FileUpload1.PostedFile.ContentLength];

            //    Session["Photo"] = new byte[FileUpload1.PostedFile.ContentLength];

            if (
                fileType[letterI - 1].ToUpper().Trim() == "PNG" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPG" ||
                fileType[letterI - 1].ToUpper().Trim() == "GIF" ||
                fileType[letterI - 1].ToUpper().Trim() == "JPEG")
            {
                //Photo = new byte[FileUpload1.PostedFile.ContentLength];

                Session["Photo"] = new byte[FileUpload1.PostedFile.ContentLength];
                HttpPostedFile Image = FileUpload1.PostedFile;
                //Image.InputStream.Read(Photo, 0, (int)FileUpload1.PostedFile.ContentLength);
                //lblFileName.Text = PhotoFilename;
                //  ObjectToByteArray(Session["Photo"]);
                Image.InputStream.Read((byte[])Session["Photo"], 0, (int)FileUpload1.PostedFile.ContentLength);
                lblFileName.Text = Session["PhotoFileName"].ToString();

                //   Session["Photo"] = Photo;
                //   Session["PhotoFileName"] = PhotoFilename;

                Image1.ImageUrl = "ScanImage.aspx";



            }
            else
            {
                lblmsg.Text = "Image format should be JPG or JPEG or GIF or PNG";

            }

        }
        catch (Exception ex)
        {

            lblmsg.Text = "An Error Occured. Please Try Again!" + ex.Message;

        }

        finally
        {

        }


        FileUpload1.Focus();

    }
    #endregion

    #region Fill Details in fields using Lookup
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getTSTbyID(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            ddlministry.SelectedValue = ddlministry.Items.FindByValue(ds.Tables[0].Rows[0]["Ministry"].ToString()).Value;
            txtname.Text = ds.Tables[0].Rows[0]["TstName"].ToString();
            ddlstate.SelectedValue = ddlstate.Items.FindByValue(ds.Tables[0].Rows[0]["intStateid"].ToString()).Value;
            getcounties();
            ddlCounties.SelectedValue = ddlCounties.Items.FindByValue(ds.Tables[0].Rows[0]["intCountieid"].ToString()).Value;
            getPayams();
            ddlpayams.SelectedValue = ddlpayams.Items.FindByValue(ds.Tables[0].Rows[0]["intPayamid"].ToString()).Value;
            txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            txtcontact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();

            ddltype.SelectedValue = ds.Tables[0].Rows[0]["Type"].ToString();
            Gen.Getdesignations(ddldesignation, ddltype.SelectedValue);
            if (ds.Tables[0].Rows[0]["intDesgid"].ToString() != "")
                ddldesignation.SelectedValue = ddldesignation.Items.FindByValue(ds.Tables[0].Rows[0]["intDesgid"].ToString()).Value;
            if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                ddlstatus.SelectedValue = ddlstatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Value;

            //txtuser.Text = ds.Tables[1].Rows[0]["User_id"].ToString();
            //txtuser.Enabled = false;
            //txtpass.Enabled = false;
            //txtpass.TextMode = TextBoxMode.SingleLine;
            //txtpass.Text = "*****"; //ds.Tables[2].Rows[0]["Password"].ToString();

            if (ds.Tables[2].Rows.Count > 0)
            {
                if (ds.Tables[2].Rows[0]["PhotoFileName"].ToString() != "" && ds.Tables[2].Rows[0]["PhotoFileName"] != null)
                {


                    lblFileName.Text = ds.Tables[2].Rows[0]["PhotoFileName"].ToString();

                    //   Image1.ImageUrl = "..viewAttachemnt.aspx?id=" + Convert.ToString(hdfID.Value.ToString()).Trim() + "&Type=TST Photo";
                    Image1.ImageUrl = ds.Tables[2].Rows[0]["File_Path"].ToString();

                    // Fixed system error file not found
                    string path = Server.MapPath("~/tempsave/" + lblFileName.Text);
                    if (System.IO.File.Exists(path))
                    { System.IO.File.Delete(path); }
                    System.IO.File.Copy(Image1.ImageUrl, path);                  
                    HiddenFieldFilePath.Value = path;

                }
                else
                {
                    lblFileName.Text = "";
                }
            }

        }
    }
    #endregion

    #region Click functionalities Save, Update, Delete, Look up

    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }

    private bool ValidatePassword(string password, out string ErrorMessage)
    {
        var input = password;
        ErrorMessage = string.Empty;

        if (string.IsNullOrWhiteSpace(input))
        {
            throw new Exception("Password should not be empty");
        }

        var hasNumber = new Regex(@"[0-9]+");
        var hasUpperChar = new Regex(@"[A-Z]+");
        var hasMiniMaxChars = new Regex(@".{8,15}");
        var hasLowerChar = new Regex(@"[a-z]+");
        var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

        if (!hasLowerChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasUpperChar.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasMiniMaxChars.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else if (!hasNumber.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }

        else if (!hasSymbols.IsMatch(input))
        {
            ErrorMessage = "Password should contain at least one lower, one upper case, one numeric and one special character, should not be lesser than 8 or greater than 15 characters.";
            return false;
        }
        else
        {
            return true;
        }
    }


    protected void BtnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Failure.Visible = false;

            txtpass.Text = "Pa55w.rd";
            Gen.Ministry = ddlministry.SelectedValue;
            Gen.TstName = txtname.Text;
            Gen.intStateid = ddlstate.SelectedValue;
            Gen.intCountieid = ddlCounties.SelectedValue;
            Gen.intPayamid = ddlpayams.SelectedValue;
            Gen.Address = txtaddress.Text;
            Gen.Email = txtemail.Text;
            Gen.ContactNo = txtcontact.Text;
            Gen.Type = ddltype.SelectedValue;
            Gen.intDesgid = ddldesignation.SelectedValue;
            Gen.Status = ddlstatus.SelectedValue;
            Gen.UserType = ddlUserType.SelectedValue;


            string email = txtemail.Text.Trim();

            int charLocation = email.IndexOf("@", StringComparison.Ordinal);
            if (charLocation > 0) {txtuser.Text = email.Substring(0, charLocation); }
                        
                //string message = "";
            //if (!ValidatePassword(txtpass.Text,out message))
            //{
            //    Failure.Visible = true;
            //    lblmsg0.Text = message;
            //    return;
            //}

            System.Threading.Thread.Sleep(5000);

            if (BtnSave1.Text == "Save")
            {

                DataSet dsUserEmail = new DataSet();
                dsUserEmail = Gen.CheckUserIdEmail(txtemail.Text.Trim());
                //
                if (dsUserEmail.Tables[0].Rows.Count > 0)
                {
                    success.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = "<font color=red> Email already Exists, Please specify another Email </font>";
                    txtuser.Text = "";
                    txtpass.Text = "";
                    return;

                }


                DataSet dsUser = new DataSet();
                dsUser = Gen.CheckUserid(txtuser.Text.Trim());
                //
                if (dsUser.Tables[0].Rows.Count > 0)
                {
                    success.Visible = false;
                    Failure.Visible = true;
                    lblmsg0.Text = "<font color=red> User ID already Exists, Please specify another User ID </font>";
                    txtuser.Text = "";
                    txtpass.Text = "";
                    return;
                    // txtRetypePassword.Text = "";
                }
                string passwd = "";
                if (!String.IsNullOrEmpty(txtpass.Text))
                    passwd = Gen.CalculateMD5Hash(txtpass.Text);

                int i = Gen.insrtTST("0", Session["uid"].ToString(), txtuser.Text.ToString(), passwd);

                if (i != 999)
                {
                    if (HiddenFieldFilePath.Value != "")
                    {

                        string filename = System.IO.Path.GetFileName(HiddenFieldFilePath.Value);
                        // filename = hdn_Material_Photo.Value.ToString();//Material_Photo_Name + filename;
                        string TSTPhoto = Server.MapPath("~/TSTPhoto/" + i.ToString() + "/" + filename);
                        string Directory = Server.MapPath("~/TSTPhoto/" + i.ToString());
                        if (System.IO.Directory.Exists(Directory))
                        {

                            if (System.IO.File.Exists(TSTPhoto))
                            { System.IO.File.Delete(TSTPhoto); }
                            System.IO.File.Move(HiddenFieldFilePath.Value, TSTPhoto);
                        }
                        else
                        {

                            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath("~/TSTPhoto"), i.ToString()));

                            System.IO.File.Move(HiddenFieldFilePath.Value, TSTPhoto);
                        }

                        int j = Gen.InsertUpdateAttachements_Sp("TST Photo", i.ToString(), TSTPhoto, Session["uid"].ToString(), filename);


                    }


                    //string body = msgs;
                    SendMailAnother(txtemail.Text);

                    Failure.Visible = false;
                    success.Visible = true;
                    lblmsg.Text = "Added Successfully..!";


                    clear();
                }
                else
                {
                    lblmsg0.Text = "Already Exist. ";
                    success.Visible = true;
                    Failure.Visible = false;
                }
            }
            else
            {
                int i = Gen.insrtTST(hdfID.Value, Session["uid"].ToString(), txtuser.Text.ToString(), Gen.CalculateMD5Hash(txtpass.Text).ToString());

                if (i != 999)
                {
                    string s = HiddenFieldFilePath.Value;
                    if (HiddenFieldFilePath.Value != "")
                    {

                        string filename = System.IO.Path.GetFileName(HiddenFieldFilePath.Value);
                        // filename = hdn_Material_Photo.Value.ToString();//Material_Photo_Name + filename;
                        string TSTPhoto = Server.MapPath("~/TSTPhoto/" + hdfID.Value + "/" + filename);
                        string Directory = Server.MapPath("~/TSTPhoto/" + hdfID.Value);
                        if (System.IO.Directory.Exists(Directory))
                        {

                            if (System.IO.File.Exists(TSTPhoto))
                            { System.IO.File.Delete(TSTPhoto); }
                            System.IO.File.Move(HiddenFieldFilePath.Value, TSTPhoto);
                        }
                        else
                        {

                            System.IO.Directory.CreateDirectory(System.IO.Path.Combine(Server.MapPath("~/TSTPhoto"), hdfID.Value));

                            System.IO.File.Move(HiddenFieldFilePath.Value, TSTPhoto);
                        }

                        int j = Gen.InsertUpdateAttachements_Sp("TST Photo", hdfID.Value, TSTPhoto, Session["uid"].ToString(), filename);


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
            
        catch (Exception ex)
        {

            success.Visible = false;
            Failure.Visible = true;
            lblmsg0.Text = "<font color=red> Email already Exists, Please specify another Email </font>";
            txtuser.Text = "";
            txtpass.Text = "";
            return;
        }

        
       
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddTSTTeam.aspx");
    }

    protected void BtnClear0_Click(object sender, EventArgs e)
    {

        int i = Gen.deleteTST(hdfID.Value);

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
    #endregion

    #region Clear Function
    void clear()
    {
        Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
        lblFileName.Text = "";
        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;
        txtpass.TextMode = TextBoxMode.Password;
        ddlministry.SelectedIndex = 0;
        //ddlministry.Items.Insert(0, "--Select--");
        txtname.Text = "";
        ddlstate.SelectedIndex = 0;


        ddlCounties.SelectedIndex = 0;


        ddlpayams.SelectedIndex = 0;
        txtaddress.Text = "";

        txtemail.Text = "";
        txtcontact.Text = "";
        ddltype.SelectedIndex = 0;

        ddldesignation.SelectedIndex = 0;

        ddlstatus.SelectedIndex = 0;
        txtuser.Text = "";
        txtpass.Text = "";
        txtuser.Enabled = true;
        txtpass.Enabled = true;



    }
    #endregion

    #region Dropdown Binding Functionalities (State,Countie,Payam,Designation)
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        getcounties();
    }
    void getcounties()
    {
        ddlCounties.Items.Clear();
        ddlpayams.Items.Clear();
        if (ddlstate.SelectedIndex != 0)
        {
            Gen.getCounties(ddlCounties, ddlstate.SelectedValue);
        }
        else
        {
            ddlCounties.Items.Insert(0, "--Select--");
            ddlpayams.Items.Insert(0, "--Select--");

        }
    }
    protected void ddlCounties_SelectedIndexChanged(object sender, EventArgs e)
    {
        getPayams();
    }
    void getPayams()
    {
        ddlpayams.Items.Clear();
        if (ddlCounties.SelectedIndex != 0)
        {
            Gen.getPayams(ddlpayams, ddlCounties.SelectedValue);
        }
        else
        {
            ddlpayams.Items.Insert(0, "--Select--");
        }
    }
    protected void ddlState_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        Getdesignations();
    }
    void Getdesignations()
    {

        if (ddltype.SelectedIndex != 0)
        {
            Gen.Getdesignations(ddldesignation, ddltype.SelectedValue);
        }
        else
        {
            ddldesignation.Items.Insert(0, "--Select--");


        }
    }
    #endregion

    #region Mail Send Functionalities

    public void SendMailAnother(string anothermail)
    {        
        string sssnpmail = System.Configuration.ConfigurationManager.AppSettings["SSSNPMAIL"]; // Config email
        string from = string.Format("\"SSSNP System\" <{0}>", sssnpmail); 
        

        string to = anothermail; //Replace this with the Email Address to whom you want to send the mail

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        
        mail.From = new MailAddress(from, ":: SSSNP Public Works MIS ::", System.Text.Encoding.UTF8);

        mail.Subject = "SSSNP Public Works MIS - TST Login Credentials -" + Session["user_id"].ToString();

        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
        mail.Body = "Dear " + txtname.Text + "<br><br>  <H2>SSSNP Public Works MIS - TST Login Credentials</H2><br> <b> NAME: " + txtname.Text + "</b> <br><br> USER ID: " + txtuser.Text + "<br> <br> Email : " + txtemail.Text + "<br> <br> URL:  <a href='"+ sssnpweb + "' target='_blank' > SSSNP Public works </a> <br> <br> Please Login by clicking the above link.<br><br>Thanks & Regards<br>";
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
          //  HttpContext.Current.Response.Write(errorMessage);
        } // end try

    }

    #endregion
}

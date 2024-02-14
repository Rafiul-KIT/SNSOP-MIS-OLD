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

/// Project : SNSDP

/// Purpose of Form: SNSDP Masters Paying Agent

/// Working Task : SNSDP Masters Paying Agent

/// Developer : Manikanta

/// Date : 20-02-2018

/// Store Procedures Used :sp_getStates,sp_getCounties,sp_getPayams, sp_insertpayingagent,sp_getpayingagentlookup,sp_getpayingagentdet                                               

/// Tables Used Modified: td_PayingAgent

/// </summary>


public partial class Payingagent : System.Web.UI.Page
{
    General Gen = new General();
    byte[] Photo = new byte[1];
    string PhotoFileName = "";
    comFunctions cmf = new comFunctions();

    protected void Page_Load(object sender, EventArgs e)
    {
        btnOrgLookup0.Attributes.Add("onclick", "javascript:return OpenPopup()");
         if (!IsPostBack)
        {
            Gen.getStates(ddlstate);
            //Gen.sp_Designations(ddldesignation);
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
        //txtuser1.Enabled = true;
        //txtpass1.Enabled = true;
        //txtuser1.Text = null;
        //txtpass1.Text = null;
    }
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
    void FillDetails()
    {
        hdfFlagID.Value = "1";
        DataSet ds = new DataSet();


        ds = Gen.getpayingagentdet(hdfID.Value.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            txtname.Text = ds.Tables[0].Rows[0]["PAname"].ToString();
            ddlstate.SelectedValue = ddlstate.Items.FindByValue(ds.Tables[0].Rows[0]["intStateid"].ToString()).Value;
            getcounties();
            ddlCounties.SelectedValue = ddlCounties.Items.FindByValue(ds.Tables[0].Rows[0]["intCountieid"].ToString()).Value;
            getPayams();
            ddlpayams.SelectedValue = ddlpayams.Items.FindByValue(ds.Tables[0].Rows[0]["intPayamid"].ToString()).Value;
            txtaddress.Text = ds.Tables[0].Rows[0]["Address"].ToString();
            txtemail.Text = ds.Tables[0].Rows[0]["Email"].ToString();
            txtcontact.Text = ds.Tables[0].Rows[0]["ContactNo"].ToString();
            if (ds.Tables[0].Rows[0]["Status"].ToString() != "")
                ddlstatus.SelectedValue = ddlstatus.Items.FindByValue(ds.Tables[0].Rows[0]["Status"].ToString()).Value;

            txtuser1.Text = ds.Tables[1].Rows[0]["User_id"].ToString();
            txtuser1.Enabled = false;
            txtpass1.Enabled = false;
            txtpass1.TextMode = TextBoxMode.SingleLine;
            txtpass1.Text = "*****"; //ds.Tables[1].Rows[0]["Password"].ToString();

            if (ds.Tables[2].Rows.Count > 0)
            {
                if (ds.Tables[2].Rows[0]["PhotoFileName"].ToString() != "" && ds.Tables[2].Rows[0]["PhotoFileName"] != null)
                {


                    lblFileName.Text = ds.Tables[2].Rows[0]["PhotoFileName"].ToString();

                    //   Image1.ImageUrl = "..viewAttachemnt.aspx?id=" + Convert.ToString(hdfID.Value.ToString()).Trim() + "&Type=TST Photo";
                    Image1.ImageUrl = ds.Tables[2].Rows[0]["File_Path"].ToString();

                }
                else
                {
                    lblFileName.Text = "";
                }
            }

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
    void clear()
    {
        Image1.ImageUrl = "~/Resource/Images/not-available.jpg";
        lblFileName.Text = "";
        BtnSave1.Text = "Save";
        BtnDelete.Visible = false;
        txtpass1.TextMode = TextBoxMode.Password;
        txtname.Text = "";
        ddlstate.SelectedIndex = 0;


        ddlCounties.SelectedIndex = 0;


        ddlpayams.SelectedIndex = 0;
        txtaddress.Text = "";

        txtemail.Text = "";
        txtcontact.Text = "";

        ddlstatus.SelectedIndex = 0;
        txtuser1.Text = "";
        txtpass1.Text = "";
        //txtuser.Enabled = true;
        //txtpass.Enabled = true;
    }
    protected void BtnSave1_Click(object sender, EventArgs e)
    {
        Gen.PAname = txtname.Text;
        Gen.intStateid = ddlstate.SelectedValue;
        Gen.intCountieid = ddlCounties.SelectedValue;
        Gen.intPayamid = ddlpayams.SelectedValue;
        Gen.Address = txtaddress.Text;
        Gen.Email = txtemail.Text;
        Gen.ContactNo = txtcontact.Text;
        Gen.Status = ddlstatus.SelectedValue;

        System.Threading.Thread.Sleep(5000);

        if (BtnSave1.Text == "Save")
        {

            DataSet dsUser = new DataSet();
            dsUser = Gen.CheckUserid(txtuser1.Text.Trim());
            //
            if (dsUser.Tables[0].Rows.Count > 0)
            {
                success.Visible = false;
                Failure.Visible = true;
                lblmsg0.Text = "<font color=red> User ID already Exists, Please specify another User ID </font>";
                txtuser1.Text = "";
                txtpass1.Text = "";
                return;
                // txtRetypePassword.Text = "";
            }

            int i = Gen.insertpayingagent("0", Session["uid"].ToString(), txtuser1.Text.ToString(), txtpass1.Text.ToString());

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

                    //int j = Gen.InsertUpdateAttachements_Sp("TST Photo", i.ToString(), TSTPhoto, Session["uid"].ToString(), filename);



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
                //int i = Gen.insertpayingagent(hdfID.Value, Session["uid"].ToString(), txtuser1.Text.ToString(), txtpass1.Text.ToString());

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

                        //int j = Gen.InsertUpdateAttachements_Sp("TST Photo", hdfID.Value, TSTPhoto, Session["uid"].ToString(), filename);


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
    }
    protected void BtnClear_Click(object sender, EventArgs e)
    {
        Response.Redirect("Payingagent.aspx");
    }
    protected void btnOrgLookup_Click(object sender, EventArgs e)
    {

    }
}

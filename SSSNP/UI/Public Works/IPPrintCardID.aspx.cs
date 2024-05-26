using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using iTextSharp.text.pdf;
using System.IO;
using iTextSharp.text;
using SSSNP_SNSOP.Entity;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Web.Services;

namespace SSSNP_SNSOP.UI.Public_Works
{
    public partial class IPPrintCardID : System.Web.UI.Page
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
            //ddlBrightness.Enabled = false;
            //btnSaveImage.Enabled = false;

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
            Image1.Src = "~/Resource/Images/not-available.jpg";
            imgAlternate1.Src = "~/Resource/Images/not-available.jpg";
            imgAlternate2.Src = "~/Resource/Images/not-available.jpg";
            txtAlternate1.Text = "";
            txtAlternate2.Text = "";
            ddlBrightness.SelectedIndex = 0;
            ddlBrightnessA1.SelectedIndex = 0;
            ddlBrightnessA2.SelectedIndex = 0;

            txtFilename.Text = "";
            txtPhotoCardA1.Text = "";
            txtPhotoCardA2.Text = "";
            RadSlider1.Value = 10;
            RadSliderA1.Value = 10;
            RadSliderA2.Value = 10;

            btnEditPhotoBeneficiary.Enabled = false;
            btnEditPhotoA1.Enabled = false;
            btnEditPhotoA2.Enabled = false;


            ds = Gen.GetBenficiaryDetByIDForJobCard(hdfID.Value.ToString());

            if (ds.Tables[0].Rows.Count > 0)
            {

                hdPhotoMain.Value = "";
                hdPhotoA1.Value = "";
                hdPhotoA2.Value = "";

                //lblJobcard.Text = "SNSOP-PW-" + ds.Tables[0].Rows[0]["intBenid"].ToString();
                lblJobcard.Text = ds.Tables[0].Rows[0]["household_number"].ToString();
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

                txtAlternate1.Text = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();
                txtAlternate2.Text = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();

                string minioBase = ConfigurationManager.AppSettings["MinioBaseDev"].ToString();

                if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
                {
                    minioBase = ConfigurationManager.AppSettings["MinioBaseProd"].ToString();
                }

                if (ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim() != null)
                {
                    hdPhotoMain.Value = CreateCloneImage(ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim());
                    btnEditPhotoBeneficiary.Enabled = true;
                    Image1.Src = minioBase + ds.Tables[0].Rows[0]["household_photo_url"].ToString().Trim();
                }

                if (ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim() != null)
                {
                    hdPhotoA1.Value = CreateCloneImage(ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim());
                    btnEditPhotoA1.Enabled = true;
                    imgAlternate1.Src = minioBase + ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim();
                }

                if (ds.Tables[0].Rows[0]["alt2_photo_url"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["alt2_photo_url"].ToString().Trim() != null)
                {
                    hdPhotoA2.Value = CreateCloneImage(ds.Tables[0].Rows[0]["alt2_photo_url"].ToString().Trim());
                    btnEditPhotoA2.Enabled = true;
                    imgAlternate2.Src = minioBase + ds.Tables[0].Rows[0]["alt1_photo_url"].ToString().Trim();
                }

                //if (ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim() != null)
                //{
                //    string ddName = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                //    string dd = ds.Tables[0].Rows[0]["PhotoPath"].ToString().Trim();
                //    dd = dd.Replace(@"\", @"/");

                //    txtFilename.Text = dd;

                //    if (!dd.Contains(ddName))
                //    {
                //        dd += "/" + ddName;
                //    }

                //    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                //    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir

                //    if (dd.Contains(sssnpdir))
                //    {
                //        dd = dd.Replace(sssnpdir, "~");
                //    }

                //    //if (File.Exists(Server.MapPath(act)))
                //    //{
                //        hdPhotoMain.Value = CreateCloneImage(dd);
                //        btnEditPhotoBeneficiary.Enabled = true;

                //    //}
                    
                //    //dd = dd.Replace(sssnpdir, sssnpweb);
                //    dd = dd.Replace("~", sssnpweb);
         
                //    string lblFileName = ds.Tables[0].Rows[0]["PhotoName"].ToString().Trim();
                //    //Image1.ImageUrl = dd;
                //    Image1.Src = dd;




                //}
                //if (ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim() != null)
                //{
                //    string dd = @"~/alternateone/" + ds.Tables[0].Rows[0]["intBenTempid"].ToString() + @"/" + ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();
                //    dd = dd.Replace(@"\", @"/");
                //    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                //    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
                //    dd = dd.Replace(sssnpdir, sssnpweb);

                //    string ddName = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"]?.ToString().Trim();

                //    txtPhotoCardA1.Text = dd;

                //    if (File.Exists(Server.MapPath(dd)))
                //    {
                //        hdPhotoA1.Value = CreateCloneImage(dd);
                //        btnEditPhotoA1.Enabled = true;

                //    }
                //    else
                //    {
                //        if (ddName != "" && ddName != null)
                //        {
                //            dd = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoPath"].ToString().Trim();
                //            dd = dd.Replace(@"\", @"/");
                //            dd = dd.Replace(sssnpdir, sssnpweb);
                //            if (!dd.Contains(ddName))
                //            {
                //                dd += "/" + ddName;
                //            }
                //            hdPhotoA1.Value = CreateCloneImage(dd.Replace(sssnpweb, "~"));
                //            btnEditPhotoA1.Enabled = true;
                //        }
                //    }
                    
                //    dd = dd.Replace("~", sssnpweb);
                //    string lblFileName = ds.Tables[0].Rows[0]["FirstAlternatePayeePhotoName"].ToString().Trim();


                //    imgAlternate1.Src = dd;

                //}
                //if (ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != "" && ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim() != null)
                //{
                //    string dd = @"~/alternatetwo/" + ds.Tables[0].Rows[0]["intBenTempid"].ToString() + @"/" + ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();
                //    dd = dd.Replace(@"\", @"/");
                //    string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
                //    string sssnpdir = System.Configuration.ConfigurationManager.AppSettings["SSSNPDIR"]; // Config dir
                //    dd = dd.Replace(sssnpdir, sssnpweb);

                //    string ddName = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"]?.ToString().Trim();

                //    txtPhotoCardA2.Text = dd;


                //    if (File.Exists(Server.MapPath(dd)))
                //    {
                //        hdPhotoA2.Value = CreateCloneImage(dd);
                //        btnEditPhotoA2.Enabled = true;

                //    }
                //    else
                //    {
                //        if (ddName != "" && ddName != null)
                //        {
                //            dd = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoPath"].ToString().Trim();
                //            dd = dd.Replace(@"\", @"/");
                //            dd = dd.Replace(sssnpdir, sssnpweb);
                //            if (!dd.Contains(ddName))
                //            {
                //                dd += "/" + ddName;
                //            }
                //            hdPhotoA2.Value = CreateCloneImage(dd.Replace(sssnpweb, "~"));
                //            btnEditPhotoA2.Enabled = true;
                //        }
                //    }
                    
                //    dd = dd.Replace("~", sssnpweb);
                //    string lblFileName = ds.Tables[0].Rows[0]["SecondAlternatePayeePhotoName"].ToString().Trim();


                //    imgAlternate2.Src = dd;

                //}


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

        protected void btnSaveImage_Click(object sender, EventArgs e)
        {
            string loadImage = txtFilename.Text;
            string filename = Server.MapPath(loadImage);
            string suffix = DateTime.Now.ToString().Replace("/", "-");

            string tempFilePath = System.IO.Path.GetDirectoryName(filename);
            string tempFileName = System.IO.Path.GetFileNameWithoutExtension(filename) + suffix + System.IO.Path.GetExtension(filename);


            string tempFileFull = System.IO.Path.Combine(tempFilePath, tempFileName);
            string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");

            string url = tempFileFull.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");


            Image1.Src = "/Resource/Images/not-available.jpg";

            //if (string.IsNullOrEmpty(loadImage)) return ;
            //if (!System.IO.File.Exists(filename)) return;

            //if (!System.IO.File.Exists(filename + "bak"))
            //{
            //    System.IO.File.Copy(filename, filename + "bak");
            //}



        }

        protected void btnChangeBrightness_Click(object sender, EventArgs e)
        {
            try
            {
                string loadImage = txtFilename.Text; // hdPhotoMain.Value.ToString();
                if (string.IsNullOrEmpty(loadImage)) return;

                float bValue = float.Parse(ddlBrightness.Text);

                string imageGenerated = ChangeBrightnessImage(loadImage, bValue);

                if (System.IO.File.Exists(imageGenerated))
                {

                    string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                    string url = imageGenerated.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");

                    hdPhotoMain.Value = url;
                    Image1.Src = url;

                }


            }
            catch (Exception ex)
            {

                throw;
            }



        }

        private Bitmap AdjustBrightness(System.Drawing.Image image, float brightness)
        {
            // Make the ColorMatrix.
            float b = brightness;
            ColorMatrix cm = new ColorMatrix(new float[][]
                {
                    new float[] {b, 0, 0, 0, 0},
                    new float[] {0, b, 0, 0, 0},
                    new float[] {0, 0, b, 0, 0},
                    new float[] {0, 0, 0, 1, 0},
                    new float[] {0, 0, 0, 0, 1},
                });
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(cm);

            // Draw the image onto the new bitmap while applying the new ColorMatrix.
            Point[] points =
            {
                new Point(0, 0),
                new Point(image.Width, 0),
                new Point(0, image.Height),
            };
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(0, 0, image.Width, image.Height);

            // Make the result bitmap.
            Bitmap bm = new Bitmap(image.Width, image.Height);
            using (Graphics gr = Graphics.FromImage(bm))
            {
                gr.DrawImage(image, points, rect, GraphicsUnit.Pixel, attributes);
            }

            // Return the result.
            return bm;
        }

        private string ChangeBrightnessImage(string filenameSource, float brightnessValue)
        {
            string result = "";

            string suffix = DateTime.Now.Ticks.ToString();

            // Generate Temp File Image
            string filename = Server.MapPath(filenameSource);
            string tempFilePath = System.IO.Path.GetDirectoryName(filename);
            string tempFileName = System.IO.Path.GetFileNameWithoutExtension(filename) + suffix + System.IO.Path.GetExtension(filename);
            result = System.IO.Path.Combine(tempFilePath, tempFileName);

            Bitmap imageLoad = (Bitmap)Bitmap.FromFile(filename);
            Bitmap imageGenerated = AdjustBrightness(imageLoad, brightnessValue);

            imageGenerated.Save(result, System.Drawing.Imaging.ImageFormat.Png);

            return result;


        }

        protected void btnChangeA1_Click(object sender, EventArgs e)
        {
            try
            {
                string loadImage = txtPhotoCardA1.Text; // hdPhotoA1.Value.ToString();
                if (string.IsNullOrEmpty(loadImage)) return;

                float bValue = float.Parse(ddlBrightnessA1.Text);

                string imageGenerated = ChangeBrightnessImage(loadImage, bValue);

                if (System.IO.File.Exists(imageGenerated))
                {

                    string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                    string url = imageGenerated.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");
                    hdPhotoA1.Value = url;
                    imgAlternate1.Src = url;

                }


            }
            catch (Exception ex)
            {

                throw;
            }


        }

        protected void btnChangeA2_Click(object sender, EventArgs e)
        {
            try
            {
                string loadImage = txtPhotoCardA2.Text; //hdPhotoA2.Value.ToString();
                if (string.IsNullOrEmpty(loadImage)) return;

                float bValue = float.Parse(ddlBrightnessA2.Text);

                string imageGenerated = ChangeBrightnessImage(loadImage, bValue);

                if (System.IO.File.Exists(imageGenerated))
                {

                    string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                    string url = imageGenerated.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");
                    hdPhotoA2.Value = url;

                    imgAlternate2.Src = url;

                }


            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void btnPdfGenate_Click(object sender, EventArgs e)
        {
            int intID = 0;
            int.TryParse(hdfID.Value, out intID);

            if (intID == 0) return;

            GenerateCardID cardId = new GenerateCardID();
            cardId.intBenId = intID.ToString();
            cardId.NumberID = lblJobcard.Text;
            cardId.BeneficiaryName = lblHHname.Text;
            cardId.BeneficiaryPhoto = hdPhotoMain.Value;
            cardId.ValidTo = "06/2026";
            cardId.AlternateName1 = txtAlternate1.Text;
            cardId.AlternateName2 = txtAlternate2.Text;
            cardId.AlternatePhoto1 = hdPhotoA1.Value;
            cardId.AlternatePhoto2 = hdPhotoA2.Value;
            cardId.CountryName = string.Format("{0}/{1}", lblPayam.Text,  lblCounti.Text);

            string json = JsonConvert.SerializeObject(cardId);

            string sssnpweb = System.Configuration.ConfigurationManager.AppSettings["SSSNPWEB"]; // Config web
            // Response.Redirect(string.Format("~/GenerateCardIDHandler.aspx?json={0}", json) );

            string redLink = sssnpweb +  string.Format("GenerateCardIDHandler.aspx?json={0}", json);
            ScriptManager.RegisterStartupScript(this, typeof(string), "New_Window", "window.open( '" + redLink + "', null, 'height=800,width=1280,status=yes,toolbar=yes,menubar=yes,location=no' );", true);


        }

        protected void RadSlider1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal brightnessValue = 10;
                decimal sliderValue = 0;

                if ((sender as Telerik.Web.UI.RadSlider) != null)
                {
                    sliderValue = (sender as Telerik.Web.UI.RadSlider).Value;
                    brightnessValue = sliderValue / 10;

                }

                ddlBrightness.Text = brightnessValue.ToString();
                btnChangeBrightness_Click(sender, e);

            }
            catch (Exception ex)
            {
                ddlBrightness.Text = "1";
                RadSlider1.Value = 10;


            }




        }

        protected void RadSliderA1_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal brightnessValue = 10;
                decimal sliderValue = 0;

                if ((sender as Telerik.Web.UI.RadSlider) != null)
                {
                    sliderValue = (sender as Telerik.Web.UI.RadSlider).Value;
                    brightnessValue = sliderValue / 10;

                }
                                
                ddlBrightnessA1 .Text = brightnessValue.ToString();
                btnChangeA1_Click(sender, e);

            }
            catch (Exception ex)
            {
                ddlBrightnessA1.Text = "1";
                RadSliderA1.Value = 10;


            }

        }

        protected void RadSliderA2_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                decimal brightnessValue = 10;
                decimal sliderValue = 0;

                if ((sender as Telerik.Web.UI.RadSlider) != null)
                {
                    sliderValue = (sender as Telerik.Web.UI.RadSlider).Value;
                    brightnessValue = sliderValue / 10;

                }

                ddlBrightnessA2.Text = brightnessValue.ToString();
                btnChangeA2_Click(sender, e);

            }
            catch (Exception ex)
            {
                ddlBrightnessA2.Text = "1";
                RadSliderA2.Value = 10;


            }
        }

        private string CreateCloneImage(string filenameSource)
        {
            try
            {
                string result = "";

                string suffix = DateTime.Now.Ticks.ToString();

                // Generate Temp File Image
                string filename = Server.MapPath(filenameSource);
                string tempFilePath = System.IO.Path.GetDirectoryName(filename);
                string tempFileName = System.IO.Path.GetFileNameWithoutExtension(filename) + suffix + System.IO.Path.GetExtension(filename);
                result = System.IO.Path.Combine(tempFilePath, tempFileName);

                Bitmap imageLoad = (Bitmap)Bitmap.FromFile(filename);

                imageLoad.Save(result, System.Drawing.Imaging.ImageFormat.Png);

                if (System.IO.File.Exists(result))
                {

                    string applicationPath = System.Web.Hosting.HostingEnvironment.MapPath("~/");
                    string url = result.Substring(applicationPath.Length).Replace('\\', '/').Insert(0, "~/");

                    imageLoad.Dispose();

                    return url;

                }

                return filenameSource;

            }
            catch (Exception ex)
            {

                return filenameSource;
            }
            

        }

       
    }
}
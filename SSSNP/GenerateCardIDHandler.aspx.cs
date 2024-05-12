using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SSSNP_SNSOP.Entity;
using Newtonsoft.Json;
using System.Drawing;
using System.Xml.Linq;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Data;
using SSSNP.Entity;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography;
using System.Text;

namespace SSSNP
{
    public partial class GenerateCardIDHandler : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                string jsonObj = Request.QueryString["json"];
                if (string.IsNullOrEmpty(jsonObj)) return;


                GenerateCardID obj = JsonConvert.DeserializeObject<GenerateCardID>(jsonObj);
                
                if (obj == null || string.IsNullOrEmpty(obj.BeneficiaryName)) return;

                byte[] data = GeneratePDF(obj);

                Response.ClearHeaders();
                Response.ContentType = "application/pdf";
                //Response.AddHeader("Content-Disposition", "attachment;filename=" + "GenerateCardID.pdf");
                Response.AddHeader("Content-Disposition", "inline; filename=" + "GenerateCardID.pdf");
                Response.AddHeader("Content-Length", data.Length.ToString());


                Response.BinaryWrite((byte[])data);

                Response.Flush();
                Response.Close();


            }
            catch (Exception ex)
            {

                Response.Write(ex.Message);
                Response.Flush();
                Response.Close();
            }


        }
        private byte[] GeneratePDF(GenerateCardID cardID)
        {
            string imageFilePath = Server.MapPath("~/Resource/Images/snsop-id-card-bg1.png");
            string imageFilePath2 = Server.MapPath("~/Resource/Images/snsop-id-card-bg2.png");

            string emptyPhoto = Server.MapPath("~/Resource/Images/not-available.jpg");
            // string emptyPhoto = Server.MapPath("~/Photo/testO1.png");

            string photoBeneficiary = (!string.IsNullOrEmpty(cardID.BeneficiaryPhoto) ? Server.MapPath(cardID.BeneficiaryPhoto) : emptyPhoto);
            string photoAnternate1 = (!string.IsNullOrEmpty(cardID.AlternatePhoto1) ? Server.MapPath(cardID.AlternatePhoto1) : emptyPhoto);
            string photoAnternate2 = (!string.IsNullOrEmpty(cardID.AlternatePhoto2) ? Server.MapPath(cardID.AlternatePhoto2) : emptyPhoto);

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance(imageFilePath);
            jpg.ScaleToFit(iTextSharp.text.Utilities.MillimetersToPoints(90), iTextSharp.text.Utilities.MillimetersToPoints(60));
            jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            jpg.SetAbsolutePosition(35, 35);

            // System.Drawing.Image iii1 = FixedSize(System.Drawing.Image.FromFile(photoBeneficiary), 250, 280);


            //#region Rotate

            


            //#endregion


            iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance(imageFilePath2);
            jpg2.ScaleToFit(iTextSharp.text.Utilities.MillimetersToPoints(90), iTextSharp.text.Utilities.MillimetersToPoints(60));
            jpg2.Alignment = iTextSharp.text.Image.TITLE;
            jpg2.SetAbsolutePosition(300, 35);

            iTextSharp.text.Image jpg3 = iTextSharp.text.Image.GetInstance(photoBeneficiary); 
            jpg3.ScaleAbsoluteWidth(90f);
            jpg3.ScaleAbsoluteHeight(116f);
            //jpg3.ScaleToFit(94f, 120f);
            jpg3.Alignment = iTextSharp.text.Image.TITLE;
            jpg3.SetAbsolutePosition(47, 54);
            // jpg3.SetAbsolutePosition(43, 76);

            //alternative 1
            iTextSharp.text.Image jpg5 = iTextSharp.text.Image.GetInstance(photoAnternate1);
            jpg5.ScaleAbsoluteWidth(88f);
            jpg5.ScaleAbsoluteHeight(114f);
            //jpg5.ScaleToFit(iTextSharp.text.Utilities.MillimetersToPoints(15), iTextSharp.text.Utilities.MillimetersToPoints(15));
            jpg5.Alignment = iTextSharp.text.Image.TITLE;
            jpg5.SetAbsolutePosition(309, 71);

            //alternative 2
            iTextSharp.text.Image jpg4 = iTextSharp.text.Image.GetInstance(photoAnternate2);
            // jpg4.ScaleToFit(iTextSharp.text.Utilities.MillimetersToPoints(15), iTextSharp.text.Utilities.MillimetersToPoints(15));
            jpg4.ScaleAbsoluteWidth(88f);
            jpg4.ScaleAbsoluteHeight(114f);
            jpg4.Alignment = iTextSharp.text.Image.TITLE;
            jpg4.SetAbsolutePosition(406, 71);

            MemoryStream fs = new MemoryStream();

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(600, 250);
            //pageSize.BackgroundColor = iTextSharp.text.BaseColor.RED;

            Document document = new Document(pageSize, 5, 5, 0, 0);

            //pageSize.BackgroundColor = new Color();

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            // SCADENZA 
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetFontAndSize(f_cn, 10);
            cb.SetTextMatrix(147, 90);  //(xPos, yPos)
            cb.ShowText( cardID.ValidTo  );
            cb.EndText();

            // NUMERO
            PdfContentByte cb1 = writer.DirectContent;
            cb1.BeginText();
            //BaseFont f_cn1 = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb1.SetFontAndSize(f_cn, 10);
            cb1.SetTextMatrix(147, 157);  //(xPos, yPos)
            cb1.ShowText(cardID.NumberID);
            cb1.EndText();

            // NOME
            PdfContentByte cb2 = writer.DirectContent;
            cb2.BeginText();
            //BaseFont f_cn1 = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb2.SetFontAndSize(f_cn, 10);
            cb2.SetTextMatrix(147, 133);  //(xPos, yPos)
            cb2.ShowText(cardID.BeneficiaryName);
            cb2.EndText();

            // COUTRY
            PdfContentByte cb3 = writer.DirectContent;
            cb3.BeginText();
            //BaseFont f_cn1 = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb3.SetFontAndSize(f_cn, 10);
            cb3.SetTextMatrix(147, 111);  //(xPos, yPos)
            cb3.ShowText(cardID.CountryName );
            cb3.EndText();

            // NOME ALTERNATIVE 1
            PdfContentByte cb4 = writer.DirectContent;
            cb4.BeginText();
            //BaseFont f_cn1 = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb4.SetFontAndSize(f_cn, 10);
            cb4.SetTextMatrix(312, 61);  //(xPos, yPos)
            cb4.ShowText( "1. " + cardID.AlternateName1);
            cb4.EndText();

            // NOME ALTERNATIVE 1
            PdfContentByte cb5 = writer.DirectContent;
            cb5.BeginText();
            //BaseFont f_cn1 = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb5.SetFontAndSize(f_cn, 10);
            cb5.SetTextMatrix(312, 46);  //(xPos, yPos)
            cb5.ShowText("2. " + cardID.AlternateName2);
            cb5.EndText();

            var qrData = "QrCodeData:\n";
            //var qrData = "";

            NewIdCardInfo newIdCardInfo = new NewIdCardInfo();
            newIdCardInfo.HouseholdNumber = cardID.NumberID;
            newIdCardInfo.HouseholdName = cardID.BeneficiaryName;

            //qrData += "1: " + cardID.NumberID + "\n";
            //qrData += "2: " + cardID.BeneficiaryName + "\n";
            try
            {
                General Gen = new General();
                DataSet ds = Gen.GetBenficiaryDetByID(cardID.intBenId.ToString());
                if (ds != null && ds.Tables?.Count > 0 && ds.Tables[0].Rows?.Count > 0)
                {
                    //qrData += "3: " + ds.Tables[0].Rows[0]["Age"].ToString() + "\n";
                    //qrData += "4: " + ds.Tables[0].Rows[0]["Sex"].ToString() + "\n";
                    //qrData += "5: " + ds.Tables[0].Rows[0]["ContactNo"].ToString() + "\n";

                    //qrData += "6: " + ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();
                    if (ds.Tables[0].Rows[0]["Age"] != null &&
                            ds.Tables[0].Rows[0]["Age"].ToString().Trim() != "")
                    {
                        newIdCardInfo.HouseholdAge = Convert.ToInt32(ds.Tables[0].Rows[0]["Age"].ToString());
                    }
                    newIdCardInfo.HouseholdGender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    newIdCardInfo.HouseholdPhone = ds.Tables[0].Rows[0]["ContactNo"].ToString();

                    if (ds.Tables[0].Rows[0]["Name1stAlternatePayee"] != null)
                    {
                        AlternateNewCardInfo alt1 = new AlternateNewCardInfo();
                        alt1.AlternateName = ds.Tables[0].Rows[0]["Name1stAlternatePayee"].ToString();
                        if (ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"] != null &&
                            ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString().Trim() != "") {
                            string a = ds.Tables[0].Rows[0]["FirstAlternatePayeeAge"].ToString();
                            alt1.AlternateAge = Convert.ToInt32(a);
                        }
                        alt1.AlternateGender = ds.Tables[0].Rows[0]["FirstAlternatePayeeGender"].ToString();
                        alt1.AlternatePhone = ds.Tables[0].Rows[0]["FirstAlternatePayeeMobile"].ToString();

                        newIdCardInfo.Alternates.Add(alt1);
                    }

                    if (ds.Tables[0].Rows[0]["Name2ndAlternatePayee"] != null)
                    {
                        AlternateNewCardInfo alt1 = new AlternateNewCardInfo();
                        alt1.AlternateName = ds.Tables[0].Rows[0]["Name2ndAlternatePayee"].ToString();
                        if (ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"] != null &&
                            ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString().Trim() != "")
                        {
                            string a = ds.Tables[0].Rows[0]["SecondAlternatePayeeAge"].ToString();
                            alt1.AlternateAge = Convert.ToInt32(a);
                        }
                        alt1.AlternateGender = ds.Tables[0].Rows[0]["SecondAlternatePayeeGender"].ToString();
                        alt1.AlternatePhone = ds.Tables[0].Rows[0]["SecondAlternatePayeeMobile"].ToString();

                        newIdCardInfo.Alternates.Add(alt1);
                    }

                    string jsonString = JsonConvert.SerializeObject(newIdCardInfo);
                    newIdCardInfo.Signature = getSignedQrData(jsonString);
                    qrData = JsonConvert.SerializeObject(newIdCardInfo);
                }
            } catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            iTextSharp.text.pdf.PdfContentByte jpg6 = writer.DirectContent;
            iTextSharp.text.pdf.BarcodeQRCode Qr = new BarcodeQRCode(qrData, 25, 25, null);
            iTextSharp.text.Image img = Qr.GetImage();
            img.ScaleAbsoluteWidth(50);
            img.ScaleAbsoluteHeight(50);
            img.SetAbsolutePosition(495, 135);
            img.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

            document.Add(jpg);
            document.Add(jpg2);

            document.Add(jpg3);
            document.Add(jpg4);
            document.Add(jpg5);
            document.Add(img);

            //Paragraph p = new Paragraph("HouseNumber");
            //document.Add(p);


            document.Close();
            writer.Close();
            Byte[] btFile = fs.ToArray();

            fs.Close();
            return btFile;
        }

        public string getSignedQrData(string json)
        {
            try
            {
                X509Certificate2 cert = new X509Certificate2(@"C:\snsop.p12", "SmKM3pXfZraDbpPy",
    X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
                RSACryptoServiceProvider rsacsp = (RSACryptoServiceProvider)cert.PrivateKey;

                CspParameters cspParam = new CspParameters();
                cspParam.KeyContainerName = rsacsp.CspKeyContainerInfo.KeyContainerName;
                cspParam.KeyNumber = rsacsp.CspKeyContainerInfo.KeyNumber == KeyNumber.Exchange ? 1 : 2;
                RSACryptoServiceProvider aescsp = new RSACryptoServiceProvider(cspParam);
                aescsp.PersistKeyInCsp = false;
                var dataToSign = json;
                byte[] signedBA = aescsp.SignData(Encoding.UTF8.GetBytes(dataToSign), "SHA256");
                bool isValid = aescsp.VerifyData(Encoding.UTF8.GetBytes(dataToSign), "SHA256", signedBA);
                string result = Convert.ToBase64String(signedBA);
                Console.WriteLine("\nSignature Base64:\n" + result);
                Console.WriteLine("\nIs signature verified:\n" + isValid);

                return result;
            } catch (Exception e)
            {
                Console.WriteLine("Error when trying to get signed QR data\n" + e.ToString());
                return null;
            }
        }


        public Bitmap Crop(System.Drawing.Image _currentBitmap, int xPosition, int yPosition, int width, int height)
        {
            Bitmap temp = (Bitmap)_currentBitmap;
            Bitmap bmap = (Bitmap)temp.Clone();
            if (xPosition + width > _currentBitmap.Width)
                width = _currentBitmap.Width - xPosition;
            if (yPosition + height > _currentBitmap.Height)
                height = _currentBitmap.Height - yPosition;
            System.Drawing.Rectangle rect = new System.Drawing.Rectangle(xPosition, yPosition, width, height);
            return (Bitmap)bmap.Clone(rect, bmap.PixelFormat);
        }
                              
        public static System.Drawing.Image FixedSize(System.Drawing.Image imgPhoto, int Width, int Height)
        {

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;
            int destX = 0;
            int destY = 0;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)Width / (float)sourceWidth);
            nPercentH = ((float)Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
            {
                nPercent = nPercentH;
                destX = System.Convert.ToInt16((Width -
                              (sourceWidth * nPercent)) / 2);
            }
            else
            {
                nPercent = nPercentW;
                destY = System.Convert.ToInt16((Height -
                              (sourceHeight * nPercent)) / 2);
            }

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(Width, Height,
                              PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                             imgPhoto.VerticalResolution);

            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.Clear(Color.White);
            grPhoto.InterpolationMode =
                    InterpolationMode.HighQualityBicubic;

            grPhoto.DrawImage(imgPhoto,
                new System.Drawing.Rectangle(destX, destY, destWidth, destHeight),
                new System.Drawing.Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);

            grPhoto.Dispose();
            return bmPhoto;
        }

        public Bitmap GetPhotoRotate(string filename)
        {
            try
            {

                System.Drawing.Image _photo = System.Drawing.Image.FromFile(filename);
                if (_photo == null) return null;

                if (_photo.Width >= _photo.Height)
                {
                    ImageHandler iHandler = new ImageHandler();
                    iHandler.CurrentBitmap = (Bitmap) _photo;
                    iHandler.RotateFlip(RotateFlipType.Rotate90FlipY);

                    _photo.Dispose();
                    return iHandler.CurrentBitmap;

                }
                
                return (Bitmap)_photo;

            }
            catch (Exception ex)
            {

                return null;
            }
        }

    }
}
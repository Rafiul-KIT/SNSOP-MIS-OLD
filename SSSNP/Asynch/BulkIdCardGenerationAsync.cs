using iTextSharp.text;
using iTextSharp.text.pdf;
using SSSNP.Entity.IdCard;
using SSSNP_SNSOP.Core.DbHandler;
using SSSNP_SNSOP.Core.Threads;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSSNP.Asynch
{
    public class BulkIdCardGenerationAsync : IThreadable
    {
        private const int SLEEP_TIME = 60 * 1000 * 3; // 10 minutes interval
        private const string NAME = "bg_id_card_generation_daemon";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        General Gen = new General();
        private IDbOperation _dbOperation = null;
        private string _connStr = "";
        private string IdCardAutoGenerationFlag;

        public BulkIdCardGenerationAsync()
        {
            IdCardAutoGenerationFlag = ConfigurationManager.AppSettings["IdCardAutoGenerationFlag"];
            var buildProfile = ConfigurationManager.AppSettings["build.profile.active"].ToString();
            if (buildProfile.Equals("prod"))
            {
                _connStr = ConfigurationManager.AppSettings["snsop_db_connStr_prod"].ToString();
            }
            else
            {
                _connStr = ConfigurationManager.AppSettings["snsop_db_connStr_dev"].ToString();
            }
            _dbOperation = new SqlDbOperation(_connStr);
        }

        public string GetName()
        {
            return NAME;
        }

        public int GetSleepTime()
        {
            return SLEEP_TIME;
        }

        public AutoResetEvent GetWaitHandle()
        {
            return waitHandle;
        }

        public AutoResetEvent GetAbortHandle()
        {
            return abortHandle;
        }

        public void Task()
        {
            try
            {
                Console.WriteLine("SEND GRM NOTIFICATION: task started!");
                while (true)
                {
                    if (IdCardAutoGenerationFlag == null || IdCardAutoGenerationFlag.Equals("no"))
                    {
                        // Only when thread stop request received
                        break;
                    }
                    GenerateIdCardAsync();

                    Console.WriteLine("SEND GRM NOTIFICATION: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    Console.WriteLine("SEND GRM NOTIFICATION: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        Console.WriteLine("SEND GRM NOTIFICATION: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                //logger.Debug("SEND GRM NOTIFICATION: Force shutdown initiated! ThreadAbortException: " + e.Message);
                Console.WriteLine("SEND GRM NOTIFICATION: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void GenerateIdCardAsync()
        {
            try
            {
                String sql1 = "select top 20 intBenid, household_number, " +
                    "concat(concat(cou.CountieName, '/'), pay.PayamName) countyPayamName, boma.BomaName, " +
                    "NameofHousehold, Name1stAlternatePayee, Name2ndAlternatePayee, " +
                    "household_photo_url, alt1_photo_url, alt2_photo_url, " +
                    "bd.intStateid, bd.intCountieid, bd.intPayamid, bd.intBomasid " +
                    "from tbl_BeneficiaryDet bd " +
                    "inner join tm_Counties cou on bd.intCountieid = cou.intCountieid " +
                    "inner join tm_Payams pay on bd.intPayamid = pay.intPayamid " +
                    "inner join tm_Bomas boma on bd.intBomasid = boma.intBomasid " +
                    "where id_card_generation_status = 0";

                DataTable dt1 = _dbOperation.GetDataTable(sql1);
                if (dt1 != null && dt1.Rows != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        try
                        {
                            GenerateCardID dto = new GenerateCardID();
                            dto.AlternateName1 = row["Name1stAlternatePayee"] != null ? row["Name1stAlternatePayee"].ToString() : null;
                            dto.AlternateName2 = row["Name2ndAlternatePayee"] != null ? row["Name2ndAlternatePayee"].ToString() : null;
                            dto.BeneficiaryName = row["NameofHousehold"] != null ? row["NameofHousehold"].ToString() : null;
                            dto.BomaName = row["BomaName"] != null ? row["BomaName"].ToString() : null;
                            dto.CountryName = row["countyPayamName"] != null ? row["countyPayamName"].ToString() : null;
                            dto.intBenId = row["intBenid"] != null ? row["intBenid"].ToString() : null;
                            dto.NumberID = row["household_number"] != null ? row["household_number"].ToString() : null;
                            dto.ValidTo = "06/2026";
                            dto.BeneficiaryPhoto = row["household_photo_url"] != null ? row["household_photo_url"].ToString() : null;
                            dto.AlternatePhoto1 = row["alt1_photo_url"] != null ? row["alt1_photo_url"].ToString() : null;
                            dto.AlternatePhoto2 = row["alt2_photo_url"] != null ? row["alt2_photo_url"].ToString() : null;

                            int stateId = row["intStateid"] != null ? Convert.ToInt32(row["intStateid"]) : 0;
                            int countyId = row["intCountieid"] != null ? Convert.ToInt32(row["intCountieid"]) : 0;
                            int payamId = row["intPayamid"] != null ? Convert.ToInt32(row["intPayamid"]) : 0;
                            int bomaId = row["intBomasid"] != null ? Convert.ToInt32(row["intBomasid"]) : 0;

                            byte[] file = GeneratePDF(dto);
                            if (file != null)
                            {
                                string filePath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"].ToString() +
                                    "\\" + "id_cards" + "\\" + stateId + "\\" + countyId + "\\" + payamId + "\\" + bomaId;
                                if (!Directory.Exists(filePath))
                                {
                                    Directory.CreateDirectory(filePath);
                                }
                                filePath += "\\" + dto.NumberID + ".pdf";
                                File.WriteAllBytes(filePath, file);

                                try
                                {                                   
                                    Dictionary<string, object> data = new Dictionary<string, object>
                                    {
                                        { "id_card_generation_status", 1 }, 
                                        { "id_card_path", filePath }       
                                    };
                                    
                                    string wherePart = $"household_number = '{dto.NumberID}'";
                                    
                                    bool isUpdated = _dbOperation.UpdateData("tbl_BeneficiaryDet", data, wherePart);
                                    
                                    if (isUpdated)
                                    {
                                        Console.WriteLine($"Successfully updated the database for Household Number: {dto.NumberID}");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Failed to update the database for Household Number: {dto.NumberID}");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error updating database for ID card generation status:");
                                    Console.WriteLine(ex.ToString());
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("There was an unexpexted error when processing id card download. Error details:\n" + ex.ToString());
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an unexpexted error when processing id card download. Error details:\n" + ex.ToString());
                return;
            }
        }

        private byte[] GeneratePDF(GenerateCardID cardID)
        {
            string minioBase = ConfigurationManager.AppSettings["MinioBaseDev"].ToString();

            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                minioBase = ConfigurationManager.AppSettings["MinioBaseProd"].ToString();
            }

            string emptyPhoto = "C:/mis_published_folder/SNSOP/Resource/Images/not-available-2.jpg";
            string beneficiaryPhoto = (!string.IsNullOrEmpty(cardID.BeneficiaryPhoto)) ? 
                (minioBase + cardID.BeneficiaryPhoto) : emptyPhoto;
            string alt1Photo = (!string.IsNullOrEmpty(cardID.AlternatePhoto1)) ?
                (minioBase + cardID.AlternatePhoto1) : emptyPhoto;
            string alt2Photo = (!string.IsNullOrEmpty(cardID.AlternatePhoto2)) ?
                (minioBase + cardID.AlternatePhoto2) : emptyPhoto;

            iTextSharp.text.Image jpg = iTextSharp.text.Image.GetInstance("C:/mis_published_folder/SNSOP/Resource/Images/ID_front.jpg");
            jpg.ScaleToFit(iTextSharp.text.Utilities.MillimetersToPoints(90), iTextSharp.text.Utilities.MillimetersToPoints(60));
            jpg.Alignment = iTextSharp.text.Image.ALIGN_CENTER;
            jpg.SetAbsolutePosition(35, 35);

            iTextSharp.text.Image jpg2 = iTextSharp.text.Image.GetInstance("C:/mis_published_folder/SNSOP/Resource/Images/ID_back.jpg");
            jpg2.ScaleToFit(iTextSharp.text.Utilities.MillimetersToPoints(90), iTextSharp.text.Utilities.MillimetersToPoints(60));
            jpg2.Alignment = iTextSharp.text.Image.TITLE;
            jpg2.SetAbsolutePosition(300, 35);

            iTextSharp.text.Image jpg3 = iTextSharp.text.Image.GetInstance(beneficiaryPhoto);
            jpg3.ScaleAbsoluteWidth(55f);
            jpg3.ScaleAbsoluteHeight(80f);
            jpg3.Alignment = iTextSharp.text.Image.TITLE;
            jpg3.SetAbsolutePosition(52, 72);

            //alternative 1
            iTextSharp.text.Image jpg5 = iTextSharp.text.Image.GetInstance(alt1Photo);
            jpg5.ScaleAbsoluteWidth(55f);
            jpg5.ScaleAbsoluteHeight(80f);
            jpg5.Alignment = iTextSharp.text.Image.TITLE;
            jpg5.SetAbsolutePosition(320, 92);

            //alternative 2
            iTextSharp.text.Image jpg4 = iTextSharp.text.Image.GetInstance(alt2Photo);
            jpg4.ScaleAbsoluteWidth(55f);
            jpg4.ScaleAbsoluteHeight(80f);
            jpg4.Alignment = iTextSharp.text.Image.TITLE;
            jpg4.SetAbsolutePosition(390, 92);

            MemoryStream fs = new MemoryStream();

            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(600, 250);

            Document document = new Document(pageSize, 5, 5, 0, 0);

            PdfWriter writer = PdfWriter.GetInstance(document, fs);

            document.Open();

            // SCADENZA 
            PdfContentByte cb = writer.DirectContent;
            cb.BeginText();
            BaseFont f_cn = BaseFont.CreateFont("c:\\windows\\fonts\\calibri.ttf", BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            cb.SetFontAndSize(f_cn, 8);
            cb.SetTextMatrix(147, 85);  //(xPos, yPos)
            cb.ShowText(cardID.ValidTo);
            cb.EndText();

            // NUMERO
            PdfContentByte cb1 = writer.DirectContent;
            cb1.BeginText();
            cb1.SetFontAndSize(f_cn, 7);
            cb1.SetTextMatrix(148, 135);  //(xPos, yPos)
            cb1.ShowText(cardID.NumberID);
            cb1.EndText();

            // NOME
            PdfContentByte cb2 = writer.DirectContent;
            cb2.BeginText();
            cb2.SetFontAndSize(f_cn, 8);
            cb2.SetTextMatrix(133, 123);  //(xPos, yPos)
            cb2.ShowText(cardID.BeneficiaryName);
            cb2.EndText();

            // COUTRY
            PdfContentByte cb3 = writer.DirectContent;
            cb3.BeginText();
            cb3.SetFontAndSize(f_cn, 8);
            cb3.SetTextMatrix(159, 110);  //(xPos, yPos)
            cb3.ShowText(cardID.CountryName);
            cb3.EndText();

            // NOME ALTERNATIVE 1
            PdfContentByte cb4 = writer.DirectContent;
            cb4.BeginText();
            cb4.SetFontAndSize(f_cn, 8);
            cb4.SetTextMatrix(320, 80);  //(xPos, yPos)
            cb4.ShowText("1. " + cardID.AlternateName1);
            cb4.EndText();

            // NOME ALTERNATIVE 1
            PdfContentByte cb5 = writer.DirectContent;
            cb5.BeginText();
            cb5.SetFontAndSize(f_cn, 8);
            cb5.SetTextMatrix(320, 68);  //(xPos, yPos)
            cb5.ShowText("2. " + cardID.AlternateName2);
            cb5.EndText();

            // BOMA
            PdfContentByte cb6 = writer.DirectContent;
            cb6.BeginText();
            cb6.SetFontAndSize(f_cn, 8);
            cb6.SetTextMatrix(134, 98);  //(xPos, yPos)
            cb6.ShowText(cardID.BomaName);
            cb6.EndText();

            cb.SetLineWidth(0.3); // Border thickness
            cb.SetColorStroke(BaseColor.BLACK); // Border color
            cb.Rectangle(35, 35, iTextSharp.text.Utilities.MillimetersToPoints(90), iTextSharp.text.Utilities.MillimetersToPoints(58)); // Front template
            cb.Stroke();
            cb.Rectangle(300, 35, iTextSharp.text.Utilities.MillimetersToPoints(90), iTextSharp.text.Utilities.MillimetersToPoints(58)); // Back template
            cb.Stroke();

            cb.SetLineWidth(0.1); // Border thickness
            cb.SetColorStroke(BaseColor.BLACK); // Border color

            // Border around the main beneficiary photo
            if (!string.IsNullOrEmpty(cardID.BeneficiaryPhoto))
            {
                cb.Rectangle(52, 72, 55, 80); // x, y, width, height
                cb.Stroke();
            }

            // Border around Alternate Photo 1
            if (!string.IsNullOrEmpty(cardID.AlternatePhoto1))
            {
                cb.Rectangle(320, 92, 55, 80);
                cb.Stroke();
            }

            // Border around Alternate Photo 2
            if (!string.IsNullOrEmpty(cardID.AlternatePhoto2))
            {
                cb.Rectangle(390, 92, 55, 80);
                cb.Stroke();
            }


            var qrData = "QrCodeData:\n";
            //var qrData = "";

            IdCardInfoV1 idCardInfoV1 = new IdCardInfoV1();
            idCardInfoV1.HouseholdNumber = cardID.NumberID;

            try
            {
                qrData = idCardInfoV1.HouseholdNumber;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            iTextSharp.text.pdf.PdfContentByte jpg6 = writer.DirectContent;
            iTextSharp.text.pdf.BarcodeQRCode Qr = new BarcodeQRCode(qrData, 25, 25, null);
            iTextSharp.text.Image img = Qr.GetImage();
            img.ScaleAbsoluteWidth(50);
            img.ScaleAbsoluteHeight(50);
            img.SetAbsolutePosition(490, 130);
            img.Alignment = iTextSharp.text.Image.ALIGN_CENTER;

            document.Add(jpg);
            document.Add(jpg2);

            document.Add(jpg3);
            document.Add(jpg4);
            document.Add(jpg5);
            document.Add(img);

            document.Close();
            writer.Close();
            Byte[] btFile = fs.ToArray();

            fs.Close();
            return btFile;
        }
    }
}
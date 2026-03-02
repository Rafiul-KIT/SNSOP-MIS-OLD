using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using SSSNP.Entity.Payroll_Download;
using SSSNP_SNSOP.Core.DbHandler;
using SSSNP_SNSOP.Core.Threads;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SSSNP.Asynch
{
    public class AutoPayrollDownload : IThreadable
    {
        private const int SLEEP_TIME = 60 * 1000 * 1; // 1 minutes interval
        private const string NAME = "payroll_download_daemon";
        private AutoResetEvent waitHandle = new AutoResetEvent(false);
        private AutoResetEvent abortHandle = new AutoResetEvent(false);
        General Gen = new General();
        private IDbOperation _dbOperation = null;
        private string _connStr = "";
        private string UnpaidPayrollDownloadFlag;
        private string DocumentPath;
        private string GetPayrollV2Endpoint;

        public AutoPayrollDownload()
        {
            UnpaidPayrollDownloadFlag = ConfigurationManager.AppSettings["UnpaidPayrollDownloadFlag"];
            DocumentPath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"];
            GetPayrollV2Endpoint = ConfigurationManager.AppSettings["GetPayrollV2"];
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
                while (true)
                {
                    if (UnpaidPayrollDownloadFlag == null || UnpaidPayrollDownloadFlag.Equals("no"))
                    {
                        break;
                    }
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    StartDownloadingPayrollDownload();
                    sw.Stop();
                    Console.WriteLine("Seconds needed for 20 rows: " + sw.ElapsedMilliseconds / 1000);

                    Console.WriteLine("id card NOTIFICATION: Going into sleep mode!");
                    GetWaitHandle().WaitOne(GetSleepTime());
                    Console.WriteLine("id card NOTIFICATION: Woke up from sleep!");
                    if (GetAbortHandle().WaitOne(1))
                    {
                        Console.WriteLine("SEND GRM NOTIFICATION: Abort request received. Getting out!");
                        break;
                    }
                }
            }
            catch (ThreadAbortException e)
            {
                Console.WriteLine("id card NOTIFICATION: Force shutdown initiated! ThreadAbortException: " + e.Message);
            }
        }

        public class GeolocationDto
        {
            public int intBomaId { get; set; }
            public string BomaName { get; set; }
            public int intPayamId { get; set; }
            public string PayamName { get; set; }
            public int intCountyId { get; set; }
            public string CountyName { get; set; }
            public int intStateId { get; set; }
            public string StateName { get; set; }
        }

        private void StartDownloadingPayrollDownloadMock()
        {
            try
            {
                string sql1 = "select tS.intStateid, tS.StateName, tC.intCountieid, tC.CountieName, " +
                    "tP.intPayamid, tP.PayamName, tB2.intBomasid, tB2.BomaName, tWWR2.PaymentCycle " +
                    "from td_WorkWageRequest tWWR2 " +
                    "join td_WageReqBenDet tWM on tWWR2.intWagePaymentReqid = tWM.intWagePaymentReqid " +
                    "join tbl_BeneficiaryDet bd on tWM.intBenid = bd.intBenid " +
                    "join tm_States tS on tS.intStateid = bd.intStateid " +
                    "join tm_Counties tC on bd.intCountieid = tC.intCountieid " +
                    "join tm_Payams tP on bd.intPayamid = tP.intPayamid " +
                    "join tm_Bomas tB2 on bd.intBomasid = tB2.intBomasid " +
                    "where tWWR2.Status in (4, 5, 6) and tB2.BomaName like '%/%' " +
                    "group by tS.intStateid, tS.StateName, tC.intCountieid, tC.CountieName, " +
                    "tP.intPayamid, tP.PayamName, tB2.intBomasid, tB2.BomaName, tWWR2.PaymentCycle " +
                    "order by PaymentCycle, tS.intStateid, tC.intCountieid, tP.intPayamid, tB2.intBomasid";

                DataTable dt1 = _dbOperation.GetDataTable(sql1);
                if (dt1 != null && dt1.Rows != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        try
                        {
                            GeolocationDto dto = new GeolocationDto();
                            dto.StateName = row["StateName"] != null ? row["StateName"].ToString().Replace("/", "-") : null;
                            dto.intStateId = row["intStateid"] != null ? Convert.ToInt32(row["intStateid"]) : 0;
                            dto.CountyName = row["CountieName"] != null ? row["CountieName"].ToString().Replace("/", "-") : null;
                            dto.intCountyId = row["intCountieid"] != null ? Convert.ToInt32(row["intCountieid"]) : 0;
                            dto.PayamName = row["PayamName"] != null ? row["PayamName"].ToString().Replace("/", "-") : null;
                            dto.intPayamId = row["intPayamid"] != null ? Convert.ToInt32(row["intPayamid"]) : 0;
                            dto.BomaName = row["BomaName"] != null ? row["BomaName"].ToString().Replace("/", "-") : null;
                            dto.intBomaId = row["intBomasid"] != null ? Convert.ToInt32(row["intBomasid"]) : 0;

                            int paymentCycleId = row["PaymentCycle"] != null ? Convert.ToInt32(row["PaymentCycle"]) : 0;

                            GetForThreeSupportTypes(dto, 1);
                            GetForThreeSupportTypes(dto, 2);
                            GetForThreeSupportTypes(dto, 3);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("There was an unexpexted error when processing id card download. Error details:\n" + ex.ToString());
                        }
                    }
                }
            } catch (Exception e)
            {

            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        private void StartDownloadingPayrollDownload()
        {
            try
            {
                string sql1 = "select tS.intStateid, tS.StateName, tC.intCountieid, tC.CountieName, " +
                    "tP.intPayamid, tP.PayamName, tB2.intBomasid, tB2.BomaName, tWWR2.PaymentCycle " +
                    "from td_WorkWageRequest tWWR2 " +
                    "join td_WageReqBenDet tWM on tWWR2.intWagePaymentReqid = tWM.intWagePaymentReqid " +
                    "join tbl_BeneficiaryDet bd on tWM.intBenid = bd.intBenid " +
                    "join tm_States tS on tS.intStateid = bd.intStateid " +
                    "join tm_Counties tC on bd.intCountieid = tC.intCountieid " +
                    "join tm_Payams tP on bd.intPayamid = tP.intPayamid " +
                    "join tm_Bomas tB2 on bd.intBomasid = tB2.intBomasid " +
                    "where tWWR2.Status in (4, 5, 6) " +
                    "group by tS.intStateid, tS.StateName, tC.intCountieid, tC.CountieName, " +
                    "tP.intPayamid, tP.PayamName, tB2.intBomasid, tB2.BomaName, tWWR2.PaymentCycle " +
                    "order by PaymentCycle, tS.intStateid, tC.intCountieid, tP.intPayamid, tB2.intBomasid";

                DataTable dt1 = _dbOperation.GetDataTable(sql1);
                if (dt1 != null && dt1.Rows != null && dt1.Rows.Count > 0)
                {
                    foreach (DataRow row in dt1.Rows)
                    {
                        try
                        {
                            GeolocationDto dto = new GeolocationDto();
                            dto.StateName = row["StateName"] != null ? row["StateName"].ToString().Replace("/","-") : null;
                            dto.intStateId = row["intStateid"] != null ? Convert.ToInt32(row["intStateid"]) : 0;
                            dto.CountyName = row["CountieName"] != null ? row["CountieName"].ToString().Replace("/", "-") : null;
                            dto.intCountyId = row["intCountieid"] != null ? Convert.ToInt32(row["intCountieid"]) : 0;
                            dto.PayamName = row["PayamName"] != null ? row["PayamName"].ToString().Replace("/", "-") : null;
                            dto.intPayamId = row["intPayamid"] != null ? Convert.ToInt32(row["intPayamid"]) : 0;
                            dto.BomaName = row["BomaName"] != null ? row["BomaName"].ToString().Replace("/", "-") : null;
                            dto.intBomaId = row["intBomasid"] != null ? Convert.ToInt32(row["intBomasid"]) : 0;

                            int paymentCycleId = row["PaymentCycle"] != null ? Convert.ToInt32(row["PaymentCycle"]) : 0;

                            GetForThreeSupportTypes(dto, paymentCycleId);
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

        private void GetForThreeSupportTypes(GeolocationDto dto, int paymentCycle)
        {
            PerformOpsForSpecificSupportType(dto, paymentCycle, "LIPW");
            PerformOpsForSpecificSupportType(dto, paymentCycle, "DIS");
            PerformOpsForSpecificSupportType(dto, paymentCycle, "ALL");
        }

        private void PerformOpsForSpecificSupportType(GeolocationDto dto, int paymentCycle, string supportType)
        {
            try
            {
                PayrollDownloadResponse response = callApi(dto.intStateId, dto.intCountyId, dto.intPayamId,
                    dto.intBomaId, paymentCycle, supportType);

                if (response != null && response.operationResult != null && response.operationResult)
                {
                    if (response.data != null && response.data.Count > 0)
                    {
                        int totalCount = 0;

                        foreach (PDPayrollDataContainer obj in response.data)
                        {
                            if (obj.payrollData != null && obj.payrollData.Count != null)
                            {
                                totalCount += obj.payrollData.Count;
                            }
                        }

                        string sql = "select id, last_fetched_count from unpaid_payroll_background_download " +
                        "where 1=1 " +
                        "and boma_id = " + dto.intBomaId + " " +
                        "and payment_cycle = " + paymentCycle + " " +
                        "and support_type = '" + supportType + "'"
                        ;

                        DataTable dt1 = _dbOperation.GetDataTable(sql);

                        if (dt1 != null && dt1.Rows != null && dt1.Rows.Count > 0)
                        {
                            foreach (DataRow row in dt1.Rows)
                            {
                                int id = row["id"] != null ? Convert.ToInt32(row["id"]) : 0;
                                int lastFetchedCount = row["last_fetched_count"] != null ? Convert.ToInt32(row["last_fetched_count"]) : 0;
                                if (lastFetchedCount != totalCount)
                                {
                                    try
                                    {
                                        EncryptedPayrollDataHandler(response, dto.StateName, dto.CountyName, dto.PayamName,
                                            dto.BomaName, paymentCycle, supportType);
                                        GeneratePayrollPdf(response, dto.StateName, dto.CountyName, dto.PayamName,
                                            dto.BomaName, paymentCycle, supportType);

                                        writeUnpaidPayrollBackground(id, dto, paymentCycle, supportType, totalCount);
                                    }
                                    catch (Exception e)
                                    {
                                        LogError(e);
                                    }
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                EncryptedPayrollDataHandler(response, dto.StateName, dto.CountyName, dto.PayamName,
                                    dto.BomaName, paymentCycle, supportType);
                                GeneratePayrollPdf(response, dto.StateName, dto.CountyName, dto.PayamName,
                                    dto.BomaName, paymentCycle, supportType);

                                writeUnpaidPayrollBackground(0, dto, paymentCycle, supportType, totalCount);
                            }
                            catch (Exception e)
                            {
                                LogError(e);
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was an unexpexted error when processing id card download. Error details:\n" + ex.ToString());
            }
        }

        private void writeUnpaidPayrollBackground(int id, GeolocationDto dto, int paymentCycle, string supportType, int totalCount)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("state_id", dto.intStateId);
            data.Add("county_id", dto.intCountyId);
            data.Add("payam_id", dto.intPayamId);
            data.Add("boma_id", dto.intBomaId);
            data.Add("payment_cycle", paymentCycle);
            data.Add("support_type", supportType);
            data.Add("last_fetched_at", DateTime.Now);
            data.Add("last_fetched_count", totalCount);

            if (id == 0)
            {
                _dbOperation.InsertData("unpaid_payroll_background_download", data);
            }
            else
            {
                _dbOperation.UpdateData("unpaid_payroll_background_download", data, " id = " + id);
            }
        }

        private PayrollDownloadResponse callApi(int stateId, int countyId, int payamId, int bomaId,
            int paymentCycle, string supportType)
        {
            var request = new PayrollDownloadRequest
            {
                state = stateId,
                county = countyId,
                payam = payamId,
                boma = bomaId,
                paymentCycle = ("Payment Cycle " + paymentCycle),
                supportType = supportType
            };

            string uri = ConfigurationManager.AppSettings["GetPayrollV2"];
            String token = ConfigurationManager.AppSettings["AfisApiAccessToken"].ToString();

            uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["GetPayrollV2"];
            if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
            {
                uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["GetPayrollV2"];
            }
            Uri urlCheck = new Uri(uri);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + token);

            string reqStr = "";

            PayrollDownloadResponse pdResponse;

            try
            {
                reqStr = JsonConvert.SerializeObject(request);
                var data = Encoding.ASCII.GetBytes(reqStr);
                httpWReq.Method = "POST";
                httpWReq.ContentType = "application/json";
                httpWReq.ContentLength = data.Length;

                var newStream = httpWReq.GetRequestStream();
                newStream.Write(data, 0, data.Length);
                newStream.Flush();
                newStream.Close();

                HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string jsonResponse = reader.ReadToEnd();
                    pdResponse = JsonConvert.DeserializeObject<PayrollDownloadResponse>(jsonResponse);

                    return pdResponse;
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }

        private void EncryptedPayrollDataHandler(PayrollDownloadResponse response, string stateName, string countyName,
            string payamName, string bomaName, int paymentCycle, string supportType)
        {
            if (response != null)
            {
                // Retrieve the ApiResponse object from the session
                PayrollDownloadResponse apiResponse = response as PayrollDownloadResponse;

                string json = JsonConvert.SerializeObject(apiResponse);

                // Hexadecimal key (256-bit) and IV (128-bit)
                string hexKey = "dd8de506bd4a90418afe9372f01b979b6f0df2e8aae22ec90b09a0bc197dc80c";
                string hexIv = "4a6f8e2f8a0c5d3e4b6c8d9e0f1a2b3c";

                byte[] key = HexStringToByteArray(hexKey);
                byte[] iv = HexStringToByteArray(hexIv);

                try
                {
                    // Encrypt the data
                    byte[] encryptedData = EncryptData(json, key, iv);

                    string genericPath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"].ToString() +
                                    "\\" + "payroll_download" + "\\";
                    string filePath = genericPath + stateName + "\\" + countyName + "\\" + payamName + "\\" + bomaName
                        + "\\" + paymentCycle;
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }

                    filePath += "\\" + bomaName + "_Payment_Cycle_" + paymentCycle + "_" + supportType + ".bin";

                    File.WriteAllBytes(filePath, encryptedData);
                }
                catch (Exception ex)
                {
                    LogError(ex);
                }
            }
        }

        private void LogError(Exception ex)
        {
            // Implement your logging logic here
            System.Diagnostics.Debug.WriteLine("Error: " + ex.Message);
        }

        private byte[] EncryptData(string plainText, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;
                aesAlg.Mode = CipherMode.CBC;
                aesAlg.Padding = PaddingMode.PKCS7;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        return msEncrypt.ToArray();
                    }
                }
            }
        }

        private byte[] HexStringToByteArray(string hex)
        {
            if (hex.Length % 2 != 0)
                throw new ArgumentException("Hex string must have an even length.");

            byte[] bytes = new byte[hex.Length / 2];
            for (int i = 0; i < hex.Length; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            }
            return bytes;
        }

        public void GeneratePayrollPdf(PayrollDownloadResponse response, string stateName, string countyName,
            string payamName, string bomaName, int paymentCycle, string supportType)
        {
            // Retrieve the ApiResponse object from the session
            PayrollDownloadResponse apiResponse = response as PayrollDownloadResponse;
            if (apiResponse == null)
            {
                throw new InvalidOperationException("apiResponse is not initialized in the session.");
            }

            // Define a custom page size with increased width
            Rectangle customPageSize = new Rectangle(PageSize.LEGAL.Width + 200f, PageSize.LEGAL.Height);

            // Create a new PDF document
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document(customPageSize.Rotate(), 25, 25, 30, 30); // Increased width and rotated to landscape
                PdfWriter writer = PdfWriter.GetInstance(document, ms);

                // Attach the custom PageNumberHelper to the writer
                writer.PageEvent = new PageNumberHelper();

                document.Open();

                // Create a font for the title with size 16 and bold
                Font titleFont = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16);

                Paragraph title = new Paragraph("Payroll Data", titleFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };
                document.Add(title);

                // Add Wage Request label with current date
                Paragraph wageRequest = new Paragraph("Wage Request: " + DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    Alignment = Element.ALIGN_LEFT
                };
                document.Add(wageRequest);

                // Add geo location data (State, County, Payam, Boma)
                document.Add(new Paragraph("State: " + stateName));
                document.Add(new Paragraph("County: " + countyName));
                document.Add(new Paragraph("Payam: " + payamName));
                document.Add(new Paragraph("Boma: " + bomaName));

                // Add space before main table
                document.Add(new Paragraph(" "));

                // Create table with 19 columns for payroll details
                PdfPTable table = new PdfPTable(18)
                {
                    WidthPercentage = 100
                };

                // Adjust column widths: ID and Age reduced, Signature increased
                table.SetWidths(new float[] {
            1f, 1.5f, 1.3f, 1f, 1f,          // Beneficiary (5 columns: Photo, ID, Name, Gender, Age)
            1f, 1.5f, 1.3f, 1f, 1f,          // First Alternate (5 columns: Photo, ID, Name, Gender, Age)
            1.2f, 1.2f, 1f, 1f, 1f, 1f, 1f,   // Payment (3 columns: Cycle, Support Type, Wage Total)
            2f                                 // Signature (1 column)
        });
                // Add section headers
                AddSectionHeaderToPdfTable(table, "Beneficiary", 5);
                AddSectionHeaderToPdfTable(table, "Alternate", 5);
                //AddSectionHeaderToPdfTable(table, "Second Alternate", 5);
                AddSectionHeaderToPdfTable(table, "Payment", 7);
                AddSectionHeaderToPdfTable(table, "Signature", 1);

                // Add payroll data headers (columns)
                AddCellToPdfTableHeader(table, "ID");
                AddCellToPdfTableHeader(table, "Photo");
                AddCellToPdfTableHeader(table, "Name");
                AddCellToPdfTableHeader(table, "Gender");
                AddCellToPdfTableHeader(table, "Age");

                AddCellToPdfTableHeader(table, "ID");
                AddCellToPdfTableHeader(table, "Photo");
                AddCellToPdfTableHeader(table, "Name");
                AddCellToPdfTableHeader(table, "Gender");
                AddCellToPdfTableHeader(table, "Age");

                AddCellToPdfTableHeader(table, "Cycle");
                AddCellToPdfTableHeader(table, "Support Type");
                AddCellToPdfTableHeader(table, "Total Attendance");
                AddCellToPdfTableHeader(table, "Wage Rate (USD)");
                AddCellToPdfTableHeader(table, "Wage Total");
                AddCellToPdfTableHeader(table, "Exchange Rate At Time");
                AddCellToPdfTableHeader(table, "Wage Rate SSP Total");

                AddCellToPdfTableHeader(table, "Sign");

                table.HeaderRows = 2;

                // Combine all payroll data from all containers and sort by beneficiary name
                List<PDPayrollDataDto> sortedPayrollData = apiResponse.data
                    .SelectMany(container => container.payrollData)
                    .GroupBy(payroll => payroll.supportType) // Grouping by Support Type (DIS, LIPW, etc.)
                    .OrderBy(group => group.Key) // Sort groups alphabetically (DIS first, then LIPW, etc.)
                    .SelectMany(group => group.OrderBy(payroll =>
                        payroll.houseHoldDetail.firstName +
                        payroll.houseHoldDetail.middleName +
                        payroll.houseHoldDetail.lastName)) // Sorting within each group
                    .ToList();

                // Iterate through payroll data in the container and add rows
                foreach (var payroll in sortedPayrollData)
                {
                    bool hasTwoAlternates = payroll.alternates.Count > 1;

                    // Beneficiary Details
                    AddCellToPdfTableBody(table, payroll.intBenid.ToString(), false, hasTwoAlternates);
                    string beneficiaryPhoto = payroll.houseHoldDetail.biometric != null ? payroll.houseHoldDetail.biometric.Photo : null;
                    AddPhotoToPdfTable(table, beneficiaryPhoto, hasTwoAlternates);
                    AddCellToPdfTableBody(table, $"{payroll.houseHoldDetail.firstName}\n{payroll.houseHoldDetail.middleName}\n{payroll.houseHoldDetail.lastName}", true, hasTwoAlternates);
                    AddCellToPdfTableBody(table, GetGenderString(payroll.houseHoldDetail.gender), false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.houseHoldDetail.age.ToString(), false, hasTwoAlternates);

                    if (payroll.alternates.Count == 1)
                    {
                        // Single Alternate (occupies full height)
                        var alternate = payroll.alternates[0];

                        AddCellToPdfTableBody(table, alternate.id.ToString(), false, hasTwoAlternates);
                        AddPhotoToPdfTable(table, alternate.biometric?.Photo, hasTwoAlternates);
                        AddCellToPdfTableBody(table, $"{alternate.firstName}\n{alternate.middleName}\n{alternate.lastName}", true, hasTwoAlternates);
                        AddCellToPdfTableBody(table, GetGenderString(alternate.gender), false, hasTwoAlternates);
                        AddCellToPdfTableBody(table, alternate.age.ToString(), false, hasTwoAlternates);
                    }
                    else if (payroll.alternates.Count > 1)
                    {
                        // Two Alternates (stacked in the same columns)
                        var firstAlternate = payroll.alternates[0];
                        var secondAlternate = payroll.alternates[1];

                        // ID Column
                        PdfPTable idTable = new PdfPTable(1);
                        idTable.WidthPercentage = 100;
                        idTable.AddCell(new PdfPCell(new Phrase(firstAlternate.id.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        idTable.AddCell(new PdfPCell(new Phrase(secondAlternate.id.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(idTable) { FixedHeight = 100f });

                        // Photo Column
                        PdfPTable photoTable = new PdfPTable(1);
                        photoTable.WidthPercentage = 100;
                        AddPhotoToPdfTable(photoTable, firstAlternate.biometric?.Photo, hasTwoAlternates);
                        AddPhotoToPdfTable(photoTable, secondAlternate.biometric?.Photo, hasTwoAlternates);
                        table.AddCell(new PdfPCell(photoTable) { FixedHeight = 100f });

                        // Name Column
                        PdfPTable nameTable = new PdfPTable(1);
                        nameTable.WidthPercentage = 100;
                        nameTable.AddCell(new PdfPCell(new Phrase($"{firstAlternate.firstName}\n{firstAlternate.middleName}\n{firstAlternate.lastName}",
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        nameTable.AddCell(new PdfPCell(new Phrase($"{secondAlternate.firstName}\n{secondAlternate.middleName}\n{secondAlternate.lastName}",
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(nameTable) { FixedHeight = 100f });

                        // Gender Column
                        PdfPTable genderTable = new PdfPTable(1);
                        genderTable.WidthPercentage = 100;
                        genderTable.AddCell(new PdfPCell(new Phrase(GetGenderString(firstAlternate.gender),
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        genderTable.AddCell(new PdfPCell(new Phrase(GetGenderString(secondAlternate.gender),
                            FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(genderTable) { FixedHeight = 100f });

                        // Age Column
                        PdfPTable ageTable = new PdfPTable(1);
                        ageTable.WidthPercentage = 100;
                        ageTable.AddCell(new PdfPCell(new Phrase(firstAlternate.age.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        ageTable.AddCell(new PdfPCell(new Phrase(secondAlternate.age.ToString(), FontFactory.GetFont(FontFactory.HELVETICA, 10)))
                        {
                            FixedHeight = 50f,
                            HorizontalAlignment = Element.ALIGN_CENTER,
                            VerticalAlignment = Element.ALIGN_MIDDLE
                        });
                        table.AddCell(new PdfPCell(ageTable) { FixedHeight = 100f });
                    }
                    else
                    {
                        for (int j = 0; j < 5; j++)
                        {
                            AddCellToPdfTableBody(table, string.Empty);
                        }
                    }

                    // Payment and Signature
                    AddCellToPdfTableBody(table, payroll.paymentCycle, false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.supportType);
                    AddCellToPdfTableBody(table, payroll.totAttendance.ToString() ?? "N/A", false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.wageRateUSD.ToString(), false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.wageRateUSDTotal.ToString(), false, hasTwoAlternates);
                    AddCellToPdfTableBody(table, payroll.exchangerateattime.ToString());
                    AddCellToPdfTableBody(table, payroll.wageRateSSPTotal.ToString(), false, hasTwoAlternates);
                    // Signature with increased height
                    PdfPCell signatureCell = new PdfPCell(new Phrase(string.Empty))
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        FixedHeight = 100f // Increased height for thumbprint accommodation
                    };
                    table.AddCell(signatureCell);
                }

                // Add the table to the document
                document.Add(table);

                // Close the document
                document.Close();

                string genericPath = ConfigurationManager.AppSettings["PaymentCycleDocumentPath"].ToString() +
                                    "\\" + "payroll_download" + "\\";
                string filePath = genericPath + stateName + "\\" + countyName + "\\" + payamName + "\\" + bomaName
                    + "\\" + paymentCycle;
                if (!Directory.Exists(filePath))
                {
                    Directory.CreateDirectory(filePath);
                }

                filePath += "\\" + bomaName + "_PaymentCycle_" + paymentCycle + "_" + supportType + ".pdf";

                byte[] contents = ms.ToArray();

                File.WriteAllBytes(filePath, contents);
            }
        }

        // Helper methods remain unchanged
        private void AddPhotoToPdfTable(PdfPTable table, string base64Photo, bool hasTwoAlternates)
        {
            string emptyPhoto = "C:/mis_published_folder/SNSOP/Resource/Images/not-available-2.jpg";

            if (!string.IsNullOrEmpty(base64Photo))
            {
                try
                {
                    byte[] imageBytes = Convert.FromBase64String(base64Photo);
                    Image img = Image.GetInstance(imageBytes);
                    img.ScaleToFit(40f, 50f); // Set image size to fit within the fixed cell height

                    PdfPCell imageCell = new PdfPCell(img, true)
                    {
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        VerticalAlignment = Element.ALIGN_MIDDLE,
                        Padding = 2f,
                        FixedHeight = hasTwoAlternates ? 50f : 50f
                    };
                    table.AddCell(imageCell);
                }
                catch (Exception)
                {
                    // If base64 string is invalid, use the empty photo image
                    AddEmptyPhotoToPdfTable(table, emptyPhoto);
                }
            }
            else
            {
                // If no base64 photo is provided, use the empty photo image
                AddEmptyPhotoToPdfTable(table, emptyPhoto);
            }
        }

        private void AddEmptyPhotoToPdfTable(PdfPTable table, string emptyPhotoPath)
        {
            try
            {
                Image emptyImg = Image.GetInstance(emptyPhotoPath);
                emptyImg.ScaleToFit(40f, 50f); // Set image size to fit within the fixed cell height

                PdfPCell emptyCell = new PdfPCell(emptyImg, true)
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    Padding = 2f,
                    FixedHeight = 50f
                };
                table.AddCell(emptyCell);
            }
            catch (Exception)
            {
                // Fallback in case the empty photo file is missing or invalid
                Font fallbackFont = FontFactory.GetFont(FontFactory.HELVETICA, 10);
                PdfPCell fallbackCell = new PdfPCell(new Phrase("No Image", fallbackFont))
                {
                    HorizontalAlignment = Element.ALIGN_CENTER,
                    VerticalAlignment = Element.ALIGN_MIDDLE,
                    FixedHeight = 50f
                };
                table.AddCell(fallbackCell);
            }
        }


        private void AddSectionHeaderToPdfTable(PdfPTable table, string headerText, int colspan)
        {
            PdfPCell headerCell = new PdfPCell(new Phrase(headerText, FontFactory.GetFont(FontFactory.HELVETICA, 10, Font.BOLD)))
            {
                Colspan = colspan,
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30f
            };
            table.AddCell(headerCell);
        }

        private void AddCellToPdfTableHeader(PdfPTable table, string text)
        {
            PdfPCell cell = new PdfPCell(new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 8, Font.BOLD)))
            {
                BackgroundColor = BaseColor.LIGHT_GRAY,
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = 30f
            };
            table.AddCell(cell);
        }

        private void AddCellToPdfTableBody(PdfPTable table, string text, bool isNameCell = false, bool hasTwoAlternates = false)
        {
            Phrase content;
            if (isNameCell && !string.IsNullOrEmpty(text))
            {
                string[] nameParts = text.Split(' ');
                content = new Phrase(string.Join("\n", nameParts), FontFactory.GetFont(FontFactory.HELVETICA, 10));
            }
            else
            {
                content = new Phrase(text, FontFactory.GetFont(FontFactory.HELVETICA, 10));
            }

            PdfPCell cell = new PdfPCell(content)
            {
                HorizontalAlignment = Element.ALIGN_CENTER,
                VerticalAlignment = Element.ALIGN_MIDDLE,
                FixedHeight = hasTwoAlternates ? 100f : 50f
            };
            table.AddCell(cell);
        }

        public class PageNumberHelper : PdfPageEventHelper
        {
            private Font footerFont = FontFactory.GetFont(FontFactory.HELVETICA, 8);

            public override void OnEndPage(PdfWriter writer, Document document)
            {
                // Create a footer paragraph with the page number
                Paragraph footer = new Paragraph("Page " + writer.PageNumber, footerFont)
                {
                    Alignment = Element.ALIGN_CENTER
                };

                // Set the footer position
                float x = document.PageSize.Width / 2;
                float y = document.BottomMargin / 2;

                // Write the footer at the bottom center
                ColumnText.ShowTextAligned(writer.DirectContent, Element.ALIGN_CENTER, new Phrase(footer), x, y, 0);
            }
        }


        private string GetGenderString(int genderValue)
        {
            return genderValue == 0 ? "Male" : genderValue == 1 ? "Female" : "N/A";
        }
    }
}
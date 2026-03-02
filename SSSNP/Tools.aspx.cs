using iTextSharp.text;
using iTextSharp.text.pdf;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Information;
using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.PDF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using iTextSharp;
//using iTextSharp.text;
//using iTextSharp.text.pdf;
//using iTextSharp.text.html;
//using System.Drawing;
//using Rectangle = iTextSharp.text.Rectangle;

namespace SSSNP_SNSOP
{
    public partial class Tools : System.Web.UI.Page
    {
        General Gen = new General();

        //private Color ChangeColorBrightness(Color color, float correctionFactor)
        //{
        //    float red = (float)color.R;
        //    float green = (float)color.G;
        //    float blue = (float)color.B;

        //    if (correctionFactor < 0)
        //    {
        //        correctionFactor = 1 + correctionFactor;
        //        red *= correctionFactor;
        //        green *= correctionFactor;
        //        blue *= correctionFactor;
        //    }
        //    else
        //    {
        //        red = (255 - red) * correctionFactor + red;
        //        green = (255 - green) * correctionFactor + green;
        //        blue = (255 - blue) * correctionFactor + blue;
        //    }

        //    return Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
        //}





        //public static Bitmap ChangeColorBrightness(Bitmap bmp, float correctionFactor)
        //{
        //    iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(0, 0, bmp.Width, bmp.Height);
        //    System.Drawing.Imaging.BitmapData bmpData = bmp.LockBits(rect, System.Drawing.Imaging.ImageLockMode.ReadWrite, bmp.PixelFormat);
        //    IntPtr ptr = bmpData.Scan0;
        //    int bytes = Math.Abs(bmpData.Stride) * bmp.Height;
        //    byte[] rgbValues = new byte[bytes];
        //    System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);
        //    float correctionFactortemp = correctionFactor;
        //    if (correctionFactor < 0)
        //    {
        //        correctionFactortemp = 1 + correctionFactor;
        //    }
        //    for (int counter = 1; counter < rgbValues.Length; counter++)
        //    {
        //        float color = (float)rgbValues[counter];
        //        if (correctionFactor < 0)
        //        {
        //            color *= (int)correctionFactortemp;
        //        }
        //        else
        //        {
        //            color = (255 - color) * correctionFactor + color;
        //        }
        //        rgbValues[counter] = (byte)color;
        //    }
        //    System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);
        //    bmp.UnlockBits(bmpData);
        //    return bmp;
        //}

        public bool IsGBVUser
        {
            get
            {
                try
                {

                    if (Session["uid"] == null || string.IsNullOrEmpty(Session["uid"].ToString())) return false;
                    int uID = 0;
                    int.TryParse(Session["uid"].ToString(), out uID);
                    if (uID == 0) return false;

                    tblUser usr = SSSNPContext.SSSNPManager.GetUser(uID);
                    if (usr == null) return false;

                    return usr.GBVUser;

                }
                catch (Exception ex)
                {

                    return false;
                }
            }
        }

        private void BuilderPDFGenerate()
        {
            SystemReportGenerate sGenerate = new SystemReportGenerate();
           if (sGenerate.CreatePdfDocument())
            {
                Response.Clear();
                Response.ContentType = "application/pdf";
                Response.AddHeader("Content-Disposition", "length=;  attachment; filename=SystemReportDownload.pdf");
                Response.BinaryWrite(sGenerate.DownloadPdfDocument().ToArray());
                Response.End();
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

            //List<BeneficiaryDIS> lst = SSSNPContext.SSSNPManager.GetBeneficiariesDIS(new Entity.FilterCriteria.BeneficiaryDISCriteria()
            //{
            //    Month = 7,
            //    Year = "2021",
            //    intPayamID = 1461

            //});

            


            //BenAttendanceDetDIS attendance = new BenAttendanceDetDIS();
            //attendance.AttendanceRifYear  = "2021";
            //attendance.Created_by = 1000;
            //attendance.IntWorkActivityID = 1725;
            //attendance.AttendanceRifMonth = 7;
            //attendance.AttendanceRifTotalDay = 10;

            //string ids = "83205P,83206P,83207P,83208P";

            //int result = SSSNPContext.SSSNPManager.CreateAttendanceDIS(attendance, ids);



            // BenAttendanceDetDIS attendance = new BenAttendanceDetDIS();

            //attendance.AttendanceRifMonth = 7;
            //attendance.AttendanceRifYear = "2021";
            //attendance.AttendanceRifTotalDay = 15;
            //attendance.IntBenID = 83205;
            //attendance.IntWorkActivityID = 1728;

            //int result = SSSNPContext.SSSNPManager.CreateAttendanceDIS(attendance);

            //attendance.IntAttendanceID = 1000;

            //attendance.AttendanceRifMonth = 7;
            //attendance.AttendanceRifYear = "2021";
            //attendance.AttendanceRifTotalDay = 15;
            //attendance.IntBenID = 83205;
            //attendance.IntWorkActivityID = 1728;
            //attendance.Created_dt = DateTime.Now;
            //attendance.Modified_by = 1001;
            //attendance.Modified_dt = DateTime.Now;
            //attendance.Status = "Present";
            //attendance.WageApplyFlag = 1;
            //attendance.IntWageReqID = 1845;

            // int result2 = SSSNPContext.SSSNPManager.UpdateAttendanceDIS(attendance);

            // List<BenAttendanceDetDIS> a = SSSNPContext.SSSNPManager.GetAttendanceDISByWorkActivity  (1728);



          //  BuilderPDFGenerate();

            //var fileName = @"C:\Users\ccavallo\source\repos\SSSNP\SSSNP\GBVExcelImportingTemp\1144_24022021010200.xlsx";
            //var connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=NO;TypeGuessRows=0;ImportMixedTypes=Text\""; ;
            //using (var conn = new OleDbConnection(connectionString))
            //{
            //    conn.Open();

            //    var sheets = conn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            //    using (var cmd = conn.CreateCommand())
            //    {
            //        cmd.CommandText = "SELECT * FROM [" + sheets.Rows[0]["TABLE_NAME"].ToString() + "] ";

            //        var adapter = new OleDbDataAdapter(cmd);
            //        var ds = new DataSet();
            //        adapter.Fill(ds);
            //    }
            //}


            //  int res = SSSNPContext.SSSNPManager.DeleteNonGBVDetail(4, 1000);

            //NonGBVDetail d = new NonGBVDetail();
            //d.NonGBVMainID = 2;
            //d.PendingActionRequered = false;
            //d.LastModificationTimestamp = DateTime.Now;
            //d.LastModificationUserID = 1001;
            //d.ActionResolutionDesignation = "A1";
            //d.ActionResponseComplaint = "B1";
            //d.AppealLodged = true;
            //d.CreationUserID = 1001;
            //d.DataOfResolution = DateTime.Now;
            //d.DescriptionOfComplaint = "Description 1";
            //d.EscaletedReferredExternally = false;
            //d.FeedbackComplainant = "Feedback 1";

            //int result = SSSNPContext.SSSNPManager.CreateNonGBVDetail(d);

            //List<NonGBVDetail> dt = SSSNPContext.SSSNPManager.GetNonGBVDetails(2);

            //NonGBVDetail dt1 = SSSNPContext.SSSNPManager.GetNonGBVDetail(4);
            //dt1.DescriptionOfComplaint = "Test Carmelo";

            //int res = SSSNPContext.SSSNPManager.UpdateNonGBVDetail(dt1);



            //NonGBVMain m = SSSNPContext.SSSNPManager.GetNonGBVMain(2);
            //m.Age = "99";

            //int result = SSSNPContext.SSSNPManager.UpdateNonGBVMain(m);

            //NonGBVMain main = new NonGBVMain();
            //main.SerialNumber = "test001";
            //main.Status = 1;
            //main.Date = DateTime.Now;
            //main.StateID = 1010;
            //main.CountyID = 1007;
            //main.MunicipalityID = 0;
            //main.PayamID = 1010;
            //main.BomaID = 1000;
            //main.DateOfComplaint = DateTime.Now.AddDays(-10);
            //main.ReportingChannel = "";
            //main.ReportingChannelOther = "";
            //main.ComplainantName = "Carmelo";
            //main.ContactNumber = "33911111";

            //int resultValue = SSSNPContext.SSSNPManager.CreateNonGBVMain(main);


            // List<LK_GBVStatus> result = SSSNPContext.SSSNPManager.GetNonGBVStatus();
            // LK_GBVStatus result = SSSNPContext.SSSNPManager.GetGBVStatus(2);

            //List<LK_TypeOfGrievance> result = SSSNPContext.SSSNPManager.GetTypeOfGrievances();

            //LK_TypeOfGrievance result1 = SSSNPContext.SSSNPManager.GetTypeOfGrievance(2);

            //List<LK_TypeOfGrievanceDetail> d1 = SSSNPContext.SSSNPManager.GetTypeOfGrievancesDetails(result1.TypeOfGrievanceID);

            //LK_TypeOfGrievanceDetail d2 = SSSNPContext.SSSNPManager.GetTypeOfGrievancesDetail(3);



            //string value = Page.Request.QueryString["value"]; // Get the parameter server side.          
            //TextBox1.Text = value;

            if (!Page.IsPostBack)
            {
                lblServerName.Text = Request.ServerVariables["SERVER_NAME"];
                lblReleaseNumber.Text = ConfigurationManager.AppSettings["Releasenumber"];


            }

        }

        protected void btThrowException_Click(object sender, EventArgs e)
        {
            throw new Exception("Generate Test Exception ");

        }

        protected void btDownloadLog_Click(object sender, EventArgs e)
        {


            string fileLog;
            try
            {
                fileLog = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFullPath"];
            }
            catch (Exception)
            {

                fileLog = "ErrorLog.log";
            }
            

            byte[] buffer = ReadFile(fileLog);

            Response.ClearHeaders();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition", "attachment;filename=" + "ErrorLog.txt");
            Response.AddHeader("Content-Length", buffer.Length.ToString());

            try
            {
                Response.BinaryWrite((byte[])buffer);
            }
            catch { }

            Response.Flush();
            Response.Close();


        }

        public static byte[] ReadFile(string filePath)
        {
            byte[] buffer;
            FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
            try
            {
                int length = (int)fileStream.Length;  // get file length
                buffer = new byte[length];            // create buffer
                int count;                            // actual number of bytes read
                int sum = 0;                          // total number of bytes read

                // read until Read method returns 0 (end of the stream has been reached)
                while ((count = fileStream.Read(buffer, sum, length - sum)) > 0)
                    sum += count;  // sum is a buffer offset for next reading
            }
            finally
            {
                fileStream.Close();
            }
            return buffer;
        }

        protected void btnCreateFileLogTest_Click(object sender, EventArgs e)
        {
            string filename = "ErrorLogTest.txt";

            try
            {
                filename = System.Configuration.ConfigurationManager.AppSettings["ErrorLogFullPath"];
            }
            catch (Exception)
            {

                filename = "ErrorLogTest.txt";
            }


            using (StreamWriter w = File.AppendText(filename))
            {

                w.WriteLine("## Error Log Timestamp: " + DateTime.Now.ToString() + " ===========================================================");
                w.WriteLine("Log Error Test");
              
                w.WriteLine("## End Error Log =========================================================== \r\n");

            }

        }

        protected void btnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                if (!FUFile.HasFile)
                {
                    txtErrorMessage.Text = "Please, select a file to upload";
                    txtErrorMessage.Visible = true;
                    return;
                }

                txtErrorMessage.Text = "";
                txtErrorMessage.Visible = false;
                string outFilename = "";

                bool uploadResult = UploadFile(out outFilename);

                if (!uploadResult && string.IsNullOrEmpty(outFilename)) return;

                              
                



                    txtErrorMessage.Text = "File uploaded Successfully";
                    txtErrorMessage.ForeColor = Color.Green;
                    txtErrorMessage.Visible = true;

                    btnUpload.Enabled = false;
                    FUFile.Enabled = false;
                
                

            }
            catch (Exception ex)
            {

                txtErrorMessage.Text = ex.Message;
                txtErrorMessage.Visible = true;
            }

        }

        public bool UploadFile(out string filename)
        {
            string LeaseFilePath = "";
            string LeaseFileName = "";
            filename = "";
            bool result = false;


            string  pathServer = Server.MapPath("~\\ExcelImport");
            if ((FUFile.PostedFile != null) && (FUFile.PostedFile.ContentLength > 0) && !Gen.IsExecutable(FUFile.PostedFile))
            {
                string sFileName = System.IO.Path.GetFileName(FUFile.PostedFile.FileName);
                try
                {
                    string[] fileType = FUFile.PostedFile.FileName.Split('.');
                    int i = fileType.Length;
                    if (fileType[i - 1].ToUpper().Trim() == "XLSX" || fileType[i - 1].ToUpper().Trim() == "XLS")
                    {
                        string newFilename = "";

                        newFilename =  string.Format("{0}_{1}", System.DateTime.Now.ToString("ddMMyyyyhhmmss") , sFileName);

                        string fullPath = System.IO.Path.Combine(pathServer, newFilename);
                                 
                        FUFile.PostedFile.SaveAs(fullPath);
                        filename = fullPath;
                                               
                        return true;


                    }
                    else
                    {
                        filename = "";
                        txtErrorMessage.Text = "Wrong file format. Please choose a Excel Files";
                        txtErrorMessage.ForeColor = Color.Red;
                        txtErrorMessage.Visible = true;
                        return false;

                    }
                }
                catch (Exception ex) //in case of an error
                {
                    filename = "";
                    return false;
                    // DeleteFile(sFileDir + sFileName);
                }
            }
            
            return result;

        }


    }
}
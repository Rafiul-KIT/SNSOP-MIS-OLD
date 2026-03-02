using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class SourceGenerator:IDisposable
    {

        private readonly string _excelFolder = "IMPORT$";
        private readonly string _excelConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties='Excel 12.0;HDR={1}'";

        private GBVImportFile _uploadFile;
        private DataTable _dataTableGenerated;
        private readonly string _mapPathRoot;
        private string _filenameFull;

        public SourceGenerator(GBVImportFile uploadFile, string mapPath)
        {
            this._uploadFile = uploadFile;
            this._mapPathRoot = mapPath;
            this._dataTableGenerated = new DataTable();
                       
        }
        public SourceGenerator(GBVImportFile uploadFile, string mapPath, string excelFolder, string excelConnectionString)
        {
            this._mapPathRoot = mapPath;
            this._uploadFile = uploadFile;
            this._dataTableGenerated = new DataTable();
            this._excelFolder = excelFolder;
            this._excelConnectionString = excelConnectionString;
                       
        }

        public bool Generate()
        {

            // Check Filename on Server
            _filenameFull = $@"{this._mapPathRoot}\{this._uploadFile.DestinationFilename}";
            if (!File.Exists(_filenameFull)) { throw new Exception("Filename: " + _filenameFull); }

            //open file excel with oledb connection
            OleDbConnectionToExcel(_filenameFull);
            return true;

        }
        private void OleDbConnectionToExcel(string filename)
        {
            try
            {

                string hdrValue = "Yes";
                string connExcelString = string.Format(this._excelConnectionString,
                        filename, hdrValue);

                using (OleDbConnection connExcel = new OleDbConnection(connExcelString))
                {

                    using (OleDbCommand cmdExcel = new OleDbCommand())
                    {
                        OleDbDataAdapter oda = new OleDbDataAdapter();

                        cmdExcel.Connection = connExcel;
                        connExcel.Open();
                        cmdExcel.CommandText = "SELECT * From [" + this._excelFolder + "] where [S#No] <> NULL";
                        oda.SelectCommand = cmdExcel;

                        oda.Fill(_dataTableGenerated);

                    }

                    connExcel.Close();

                }

            }
            catch (Exception ex)
            {

                throw new Exception("OleDbConnectionToExcel Exception", ex);
            }


        }

        public bool HasDataTable
        {
            get
            {

                if (this._dataTableGenerated != null && this._dataTableGenerated.Columns.Count != 0) return true;
                return false;
            }
        }
        public DataTable GetDataTable
        {
            get
            {
                if (HasDataTable) return this._dataTableGenerated;
                else { return null; }
            }
        }
        public string ExcelFolder { get { return this._excelFolder; } }
        public string ExcelConnectionString { get { return this._excelConnectionString; } }
        public bool RemoveFileAfterDispose { get; set; }

        /*IDisposable Implementing */
        private bool _isDisposed = false;
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!_isDisposed)
                {

                    try
                    {
                        if (RemoveFileAfterDispose)
                        {
                            if (File.Exists(this._filenameFull))
                            {
                                File.Delete(this._filenameFull);
                            }
                        }
                        
                    }
                    catch (Exception) { }

                    //Do your unmanaged disposing here
                    _isDisposed = true;
                }
            }
        }
        ~SourceGenerator()
        {
            Dispose(false);
        }

    }
}

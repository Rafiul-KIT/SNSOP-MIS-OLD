using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class GBVGenerate
    {
        private int _userID;
        private GBVImportFile _importedFile;
        private string _mapPath;
        private DataTable _dtSource;
        private List<NonGBVMain> _nonGBVMainMapped;
        private List<NonGBVDetail> _nonGBVDetailMapped;
        private List<GBVMain> _gbvMainMapped;
        private List<GBVDetail> _gbvDetailMapped;

        public GBVGenerate(GBVImportFile uploadFile, string mapPath)
        {
            if (uploadFile == null) throw new ArgumentNullException(nameof(uploadFile), "Parameter null");
            this._importedFile = uploadFile;
            this._userID = _importedFile.UserID ;
            this._mapPath = mapPath;

            //Istanzia SourceGenerator
            #region SourceGenerator

                ExecuteSourceGenerator();

            #endregion SourceGenerator

            #region Mapping Generator

            if (uploadFile.GBVType == "NON-GBV")
            {
                MappingGeneratorNonGBV();
            }
            else
            {
                MappingGeneratorGBV();
            }

            #endregion


        }

        private void ExecuteSourceGenerator()
        {
            using (SourceGenerator sGenerator = new SourceGenerator(this._importedFile, this._mapPath))
            {
                sGenerator.Generate();
                this._dtSource = sGenerator.GetDataTable;

            }

            if (!GetValidateDataSource(this._dtSource)) { throw new Exception("ValidateDataSource Exception: DataSource empty"); }

        }
        private void MappingGeneratorNonGBV()
        {
            using (NonGBVMappingGenerator mGenerator = new NonGBVMappingGenerator(this._dtSource))
            {
                this._nonGBVMainMapped = mGenerator.DataSourceMainGenerated;
                this._nonGBVDetailMapped = mGenerator.DataSourceDetailGenerated;

            }

            if (this._nonGBVMainMapped == null || this._nonGBVMainMapped.Count == 0) throw new Exception("MappingGeneratorNonGBV Main List Empty");
            if (this._nonGBVDetailMapped == null || this._nonGBVDetailMapped.Count == 0) throw new Exception("MappingGeneratorNonGBV Detail List Empty");

        }
        private void MappingGeneratorGBV()
        {
            using (GBVMappingGenerator mGenerator = new GBVMappingGenerator(this._dtSource))
            {
                this._gbvMainMapped = mGenerator.DataSourceMainGenerated;
                this._gbvDetailMapped = mGenerator.DataSourceDetailGenerated;

            }

            if (this._gbvMainMapped == null || this._gbvMainMapped.Count == 0) throw new Exception("MappingGeneratorGBV Main List Empty");
            if (this._gbvDetailMapped == null || this._gbvDetailMapped.Count == 0) throw new Exception("MappingGeneratorGBV Detail List Empty");

        }
        private bool GetValidateDataSource(DataTable dt)
        {
            if (dt == null) return false;
            else if (dt != null && dt.Rows.Count != 0) return true;
            else return false;

        }
        private bool ValidateNonGBVImportMapping(out List<ErrorMessage> errors)
        {
            errors = new List<ErrorMessage>();

            foreach ( NonGBVMain m in this._nonGBVMainMapped)
            {
                try
                {
                    NonGBVDetail detail = this._nonGBVDetailMapped.Single(x => x.NonGBVMainID == m.NonGBVMainID);
                    
                    if (m.NonGBVMainID == detail.NonGBVMainID)
                    {
                        ValidateNonGBVMapping validate = new ValidateNonGBVMapping(m, detail);
                        if (!validate.Validate())
                        {
                         
                            errors = validate.Errors;

                            return false;
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }
                

            }

            return true;

        }
        private bool ValidateGBVImportMapping(out List<ErrorMessage> errors)
        {
            errors = new List<ErrorMessage>();

            foreach (GBVMain m in this._gbvMainMapped)
            {
                try
                {
                    GBVDetail detail = this._gbvDetailMapped.Single(x => x.GBVMainID == m.GBVMainID);

                    if (m.GBVMainID == detail.GBVMainID)
                    {
                        ValidateGBVMapping validate = new ValidateGBVMapping(m, detail);
                        if (!validate.Validate())
                        {

                            errors = validate.Errors;

                            return false;
                        }
                    }

                }
                catch (Exception ex)
                {

                    throw;
                }


            }

            return true;

        }

        public DataTable DataSource {  get { return this._dtSource; } }
        public List<NonGBVMain> NonGBVMainListMapped { get { return this._nonGBVMainMapped; } }
        public List<NonGBVDetail> NonGBVDetailListMapped { get { return this._nonGBVDetailMapped; } }
        public List<GBVMain> GBVMainListMapped { get { return this._gbvMainMapped; } }
        public List<GBVDetail> GBVDetailListMapped { get { return this._gbvDetailMapped; } }
        public bool Validate(out List<ErrorMessage> errors)
        {
            if (_importedFile.GBVType == "NON-GBV")
            {
               return ValidateNonGBVImportMapping(out errors);
            }
            else
            {
                return ValidateGBVImportMapping(out errors);
            }
        }
      
    }
}

using SSSNP_SNSOP.Core.OleDbMapping.MappingPropertiesGenerator;
using SSSNP_SNSOP.Entity;
using SSSNP_SNSOP.Entity.MappingProperty.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class NonGBVMappingGenerator: MappingPropertiesGeneratorBase
    {
        private List<NonGBVMain> _resultMain;
        private List<NonGBVDetail> _resultDetail;
        private List<LK_GBVStatus> _statusList;
        private List<LK_TypeOfGrievance> _typeOfGrievanceList;
        private List<LK_TypeOfGrievanceDetail> _typeOfGrievanceDetailList;
     
        public NonGBVMappingGenerator(DataTable reader): base(reader)
        {
          
            this.MappingPropertiesAttribute<NonGBVMain>();
            List<NonGBVMain> lstMappedMain = this.ExecuteOperationMapping<NonGBVMain>();

            _resultMain = new List<NonGBVMain>(lstMappedMain);

            this.Reset();

            this.MappingPropertiesAttribute<NonGBVDetail>();
            List<NonGBVDetail> lstMappedDetail = this.ExecuteOperationMapping2<NonGBVDetail>();

            _resultDetail = new List<NonGBVDetail>(lstMappedDetail);
                     

        }

        public List<NonGBVMain> DataSourceMainGenerated { get { return this._resultMain; } }
        public List<NonGBVDetail> DataSourceDetailGenerated { get { return this._resultDetail; } }
        protected override object LookupMappingProperty(IAttributeMappingProperty mProperty, object value)
        {
            object returnValue = null;

            switch (mProperty.LookupMappingName)
            {
                case "LK_State_To_StateID":
                    returnValue = LK_StateName_To_StateID(value.ToString().Trim ());
                    break;
                case "LK_Countie_To_CountieID":
                    returnValue = LK_CountieName_To_CountieID(value.ToString().Trim());
                    break;
                case "LK_PayamName_To_PayamID":
                    returnValue = LK_PayamName_To_PayamID(value.ToString().Trim());
                    break;
                case "LK_BomaName_To_BomaID":
                    returnValue = LK_BomaName_To_BomaID(value.ToString().Trim());
                    break;
                
                case "LK_TypeOfGrievanceID":
                    returnValue = LK_TypeOfGrievanceID(value.ToString().Trim());
                    break;
                case "LK_TypeOfGrievanceDetailID":
                    returnValue = LK_TypeOfGrievanceDetailID(value.ToString().Trim());
                    break;
                case "LK_StatusIdToStatusName":
                    returnValue = LK_StatusIdToStatusName(value.ToString().Trim());
                    break;
                case "LK_DatetimeMapping":
                    returnValue = LK_DatetimeMapping(value.ToString().Trim());
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return returnValue;

        }

        #region Lookup
        private int LK_StateName_To_StateID(string value)
        {

            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                LK_State lkState = SSSNPContext.SSSNPManager.GetStateLookup(value);
                if (lkState != null && lkState.StateID != 0) return lkState.StateID;

                return 0;
              
            }
            catch (Exception ex)
            {

                return 0;
            }
                                 
        }
        public int LK_CountieName_To_CountieID(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                LK_County lkCountie = SSSNPContext.SSSNPManager.GetCountyLookup (value);
                if (lkCountie != null && lkCountie.CountyID != 0) return lkCountie.CountyID ;

                return 0;
                
            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public int LK_PayamName_To_PayamID(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                LK_Payam lkPayam = SSSNPContext.SSSNPManager.GetPayamLookup(value);
                if (lkPayam != null && lkPayam.PayamID != 0) return lkPayam.PayamID;

                return 0;

            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public int LK_BomaName_To_BomaID(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                LK_Boma lkBoma = SSSNPContext.SSSNPManager.GetBomaLookup (value);
                if (lkBoma != null && lkBoma.BomaID != 0) return lkBoma.BomaID;

                return 0;

            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public int LK_StatusIdToStatusName(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                if (this._statusList == null) { this._statusList = SSSNPContext.SSSNPManager.GetNonGBVStatus(); }
                if (this._statusList == null || this._statusList.Count == 0) return 0;

                LK_GBVStatus lkStatus = _statusList.FirstOrDefault(x => x.StatusName.Equals(value, StringComparison.InvariantCultureIgnoreCase));

                if (lkStatus != null && lkStatus.StatusID != 0) return lkStatus.StatusID;

                return 0;

            }
            catch (Exception ex)
            {

                return 0;
            }
        }
        public int? LK_TypeOfGrievanceID (string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                if (this._typeOfGrievanceList == null) { this._typeOfGrievanceList = SSSNPContext.SSSNPManager.GetTypeOfGrievances(); }
                if (this._typeOfGrievanceList == null || this._typeOfGrievanceList.Count == 0) return null;

                LK_TypeOfGrievance lk = _typeOfGrievanceList.FirstOrDefault(x => x.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));

                if (lk != null && lk.TypeOfGrievanceID != 0) return (int)lk.TypeOfGrievanceID;

                return null;

            }
            catch (Exception ex)
            {

                return null;
            }

        }
        public int? LK_TypeOfGrievanceDetailID(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return 0;

                if (this._typeOfGrievanceDetailList == null) { this._typeOfGrievanceDetailList = SSSNPContext.SSSNPManager.GetTypeOfGrievancesDetails(); }
                if (this._typeOfGrievanceDetailList == null || this._typeOfGrievanceDetailList.Count == 0) return null;

                LK_TypeOfGrievanceDetail lk = _typeOfGrievanceDetailList.FirstOrDefault(x => x.Name.Equals(value, StringComparison.InvariantCultureIgnoreCase));

                if (lk != null && lk.TypeOfGrievanceDetailID != 0) return (int)lk.TypeOfGrievanceDetailID;

                return null;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DateTime? LK_DatetimeMapping(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value.ToString())) return null;

             
                DateTime dt;

                dt = Convert.ToDateTime(value.ToLower(), new CultureInfo("it-IT"));
                             
                if (dt == DateTime.MinValue) return null;
                return dt;


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}

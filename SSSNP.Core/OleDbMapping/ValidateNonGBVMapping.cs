using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class ValidateNonGBVMapping
    {
        private NonGBVMain _main = null;
        private NonGBVDetail _detail = null;
        private List<ErrorMessage> _errors;
        public ValidateNonGBVMapping(NonGBVMain main, NonGBVDetail detail)
        {
           
            this._main = main;
            this._detail = detail;

            _errors = new List<ErrorMessage>();            

        }
        public bool Validate()
        {
            try
            {
                StatusValidation();
                StateValidation();
                CountieValidation();
                PayamValidation();
                BomaValidation();
                DataInsertValidation();
                DataOfComplaintValidation();
                ReportingChannelValidation();
                ReportingChannelDetailValidation();
                ComplaintNameValidation();
                TypeOfGrievanceValidation();
                TypeOfGrievanceDetailValidation();
                SSSNPBeneficieryValidation();

                //DescriptionOfComplaintValidation();
                ActionResponseComplaintValidation();
                ActionResolutionDesignationValidation();
                FeedbackComplainantValidation();
                DataOfResolutionValidation();
                PendingEscalatedAppealValidation();


                if (this._errors.Count == 0)  return true;

                return false;

            }
            catch (Exception ex)
            {

                this._errors.Add( new ErrorMessage() { SequenceNumber = _main.NonGBVMainID, Exception = ex, Message = $"GENERAL ERROR - {ex.Message} ", TimeStamp = DateTime.Now });
                return false;
            }
                        
        }
        public void AddErrorMessage(string message, Exception ex)
        {
            this._errors.Add ( new ErrorMessage() { SequenceNumber = _main.NonGBVMainID, Exception = ex, Message = message, TimeStamp = DateTime.Now });
        }
        public List<ErrorMessage > Errors {  get { return this._errors; } }

        private void StatusValidation()
        {
            try
            {

                if (_main.Status == 0) AddErrorMessage("Status ID empty", null);
                             

            }
            catch (Exception ex)
            {
                AddErrorMessage($"StatusValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void StateValidation()
        {
            try
            {

                if (_main.StateID == 0) AddErrorMessage("State ID empty", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"StateValidation ERROR - {ex.Message} ", ex);
                                
            }
        }
        private void CountieValidation()
        {
            try
            {

                if (_main.CountyID == 0) AddErrorMessage("Countie ID empty", null);

                LK_County lk = SSSNPContext.SSSNPManager.GetCountyLookup(_main.CountyID);
                if (lk == null || lk.CountyID == 0) AddErrorMessage("Countie Not Exists", null);
                if (lk.StateID != _main.StateID) AddErrorMessage("State and Countie not Match", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"CountieValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void PayamValidation()
        {
            try
            {

                if (_main.PayamID == 0) AddErrorMessage("Payam ID empty", null);

                LK_Payam lk = SSSNPContext.SSSNPManager.GetPayamLookup(_main.PayamID);
                if (lk == null || lk.PayamID == 0) AddErrorMessage("Payam Not Exists", null);
                if (lk.CountyID != _main.CountyID) AddErrorMessage("Countie and Payam not Match", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"PayamValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void BomaValidation()
        {
            try
            {

                if (_main.BomaID == 0) AddErrorMessage("Boma ID empty", null);

                LK_Boma lk = SSSNPContext.SSSNPManager.GetBomaLookup (_main.BomaID);
                if (lk == null || lk.BomaID == 0) AddErrorMessage("Boma Not Exists", null);
                if (lk.PayamID != _main.PayamID)
                {
                    List<LK_Boma> lstBomas = SSSNPContext.SSSNPManager.GetBomasLookup(_main.PayamID);
                    if (lstBomas == null) AddErrorMessage("Boma Not Valid", null);
                    else
                    {
                        LK_Boma newValue = lstBomas.SingleOrDefault(x => x.PayamID == _main.PayamID && x.BomaName == lk.BomaName);
                        if (newValue == null || newValue.BomaID == 0)
                        {
                            AddErrorMessage("Payam and Boma not Match", null);

                        }
                        else
                        {
                            _main.BomaID = newValue.BomaID;
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                AddErrorMessage($"BomaValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void DataInsertValidation()
        {
            try
            {

                if (_main.Date == DateTime.MinValue) AddErrorMessage("Data Insert empty", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"DataInsertValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void DataOfComplaintValidation()
        {
            try
            {

                if (_main.DateOfComplaint == DateTime.MinValue) AddErrorMessage("Date Of Complaint empty", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"DataOfComplaintValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void ReportingChannelValidation()
        {
            try
            {
                if (string.IsNullOrEmpty(_main.ReportingChannel))
                {
                    AddErrorMessage("Reporting Channel empty", null);
                    return;
                }

                List<string> _lkReporting = new List<string> ();

                if (_lkReporting == null || _lkReporting.Count == 0)
                {
                    _lkReporting = new List<string>();
                    _lkReporting.Add("County/Municipality Core Team");
                    _lkReporting.Add("Appeals Committee");
                    _lkReporting.Add("Payam/Block DC");
                    _lkReporting.Add("Boma/QCDC");
                    _lkReporting.Add("CST");
                    _lkReporting.Add("UNOPS");
                    _lkReporting.Add("Other");
                }

                if (!_lkReporting.Any(x => x.Equals(_main.ReportingChannel, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AddErrorMessage("Reporting Channel not match with Lookup Values", null);
                }
                               

            }
            catch (Exception ex)
            {
                AddErrorMessage($"ReportingChannelValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void ReportingChannelDetailValidation()
        {
            try
            {
                if (_main.ReportingChannelOther == null) this._main.ReportingChannelOther = "";

            }
            catch (Exception ex)
            {
               

            }
        }
        private void ComplaintNameValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_main.ComplainantName)) AddErrorMessage("Complainant Name empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"ComplaintNameValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void TypeOfGrievanceValidation()
        {
            try
            {

                if (_main.TypeOfGrievanceID == null || _main.TypeOfGrievanceID == 0) return;

                LK_TypeOfGrievance lk = SSSNPContext.SSSNPManager.GetTypeOfGrievance((int)_main.TypeOfGrievanceID );
                if (lk == null || lk.TypeOfGrievanceID  == 0) AddErrorMessage("Type Of Grievance Not Exists", null);
             

            }
            catch (Exception ex)
            {
                AddErrorMessage($"TypeOfGrievanceValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void TypeOfGrievanceDetailValidation()
        {
            try
            {

                if (_main.TypeOfGrievanceID == null || _main.TypeOfGrievanceID == 0) return;
                if (_main.TypeOfGrievanceDetailID == null || _main.TypeOfGrievanceDetailID == 0) return;

                List<LK_TypeOfGrievanceDetail> lk = SSSNPContext.SSSNPManager.GetTypeOfGrievancesDetails ((int)_main.TypeOfGrievanceID);
                
                if (!lk.Any(x=>x.TypeOfGrievanceDetailID == (int)_main.TypeOfGrievanceDetailID)) 
                {
                    AddErrorMessage("Type Of Grievance Detail Not Match with Type Of Grievance", null);
                }
                
            }
            catch (Exception ex)
            {
                AddErrorMessage($"TypeOfGrievanceDetailValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void SSSNPBeneficieryValidation()
        {
            try
            {
                if (string.IsNullOrEmpty(_main.SSSNPBeneficiary))
                {
                    AddErrorMessage("SSSNP Beneficiary empty", null);
                    return;
                }

                List<string> lk = new List<string>();

                lk.Add("NO");
                lk.Add("LIPW");
                lk.Add("DIS");
              
                if (!lk.Any(x => x.Equals(_main.SSSNPBeneficiary, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AddErrorMessage("SSSNP Beneficiary not match with Lookup Values", null);
                }


            }
            catch (Exception ex)
            {
                AddErrorMessage($"SSSNPBeneficieryValidation ERROR - {ex.Message} ", ex);

            }
        }

        //private void DescriptionOfComplaintValidation()
        //{
        //    try
        //    {

        //        if (string.IsNullOrEmpty(_detail.DescriptionOfComplaint)) AddErrorMessage("Description Of Complaint empty", null);

        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrorMessage($"DescriptionOfComplaintValidation ERROR - {ex.Message} ", ex);

        //    }
        //}
        private void ActionResponseComplaintValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_detail.ActionResponseComplaint)) AddErrorMessage("Action Response Complaint empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"ActionResponseComplaintValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void ActionResolutionDesignationValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_detail.ActionResolutionDesignation)) AddErrorMessage("Action Resolution Designation empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"ActionResolutionDesignationValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void FeedbackComplainantValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_detail.FeedbackComplainant)) AddErrorMessage("Feedback Complainant empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"FeedbackComplainantValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void DataOfResolutionValidation()
        {
            try
            {

                if (_detail.DataOfResolution == DateTime.MinValue) AddErrorMessage("Data Of Resolution empty", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"DataOfResolutionValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void PendingEscalatedAppealValidation()
        {
            try
            {
                short trueValue = 0;

                if (_detail.PendingActionRequered) trueValue += 1;
                if (_detail.EscaletedReferredExternally) trueValue += 1;
                if (_detail.AppealLodged) trueValue += 1;

                if (trueValue >1) AddErrorMessage("More than one among 'Pending Action Required, Escalated Referred Externally, Appeal Lodged' set at TRUE (only one allowed).", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"PendingEscalatedAppealValidation ERROR - {ex.Message} ", ex);

            }
        }

    }
}

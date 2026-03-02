using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSSNP_SNSOP.Core.OleDbMapping
{
    public class ValidateGBVMapping
    {
        private GBVMain _main = null;
        private GBVDetail _detail = null;
        private List<ErrorMessage> _errors;
        public ValidateGBVMapping(GBVMain main, GBVDetail detail)
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
                DataOfIncidentValidation();
                ReportedByValidation();
                FocalPointNameValidation();
                ContactNumberValidation();
                GenderValidation();
                AgeValidation();
                SSSNPBeneficieryValidation();
                ReferralServicesValidation();


                //DescriptionOfIncidentValidation();
                FollowUpFeedbackValidation();


                if (this._errors.Count == 0) return true;

                return false;

            }
            catch (Exception ex)
            {

                this._errors.Add(new ErrorMessage() { SequenceNumber = _main.GBVMainID, Exception = ex, Message = $"GENERAL ERROR - {ex.Message} ", TimeStamp = DateTime.Now });
                return false;
            }

        }
        public void AddErrorMessage(string message, Exception ex)
        {
            this._errors.Add(new ErrorMessage() { SequenceNumber = _main.GBVMainID, Exception = ex, Message = message, TimeStamp = DateTime.Now });
        }
        public List<ErrorMessage> Errors { get { return this._errors; } }

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

                if (_main.CountyID == 0) AddErrorMessage("County ID empty", null);

                LK_County lk = SSSNPContext.SSSNPManager.GetCountyLookup(_main.CountyID);
                if (lk == null || lk.CountyID == 0) AddErrorMessage("County does not Exist", null);
                if (lk.StateID != _main.StateID) AddErrorMessage("State and County do not match", null);


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
                if (lk == null || lk.PayamID == 0) AddErrorMessage("Payam does not Exist", null);
                if (lk.CountyID != _main.CountyID) AddErrorMessage("Countie and Payam do not match", null);


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

                LK_Boma lk = SSSNPContext.SSSNPManager.GetBomaLookup(_main.BomaID);
                if (lk == null || lk.BomaID == 0) AddErrorMessage("Boma does not exist", null);
                if (lk.PayamID != _main.PayamID)
                {
                    List<LK_Boma> lstBomas = SSSNPContext.SSSNPManager.GetBomasLookup(_main.PayamID);
                    if (lstBomas == null) AddErrorMessage("Boma Not Valid", null);
                    else 
                    {
                        LK_Boma newValue = lstBomas.SingleOrDefault(x => x.PayamID == _main.PayamID && x.BomaName == lk.BomaName);
                        if (newValue == null || newValue.BomaID == 0)
                        {
                            AddErrorMessage("Payam and Boma do not match", null);

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
                _main.Date = DateTime.Now;

                if (_main.Date == DateTime.MinValue) AddErrorMessage("Data Insert empty", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"DataInsertValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void DataOfIncidentValidation()
        {
            try
            {

                if (_main.DateOfIncident == DateTime.MinValue) AddErrorMessage("Date of the Incident empty", null);


            }
            catch (Exception ex)
            {
                AddErrorMessage($"DataOfComplaintValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void ReportedByValidation()
        {
            try
            {
                if (string.IsNullOrEmpty(_main.IncidentReportedBy))
                {
                    AddErrorMessage("Incident Reported By is empty", null);
                    return;
                }

                List<string> _lkReporting = new List<string>();

                if (_lkReporting == null || _lkReporting.Count == 0)
                {
                    _lkReporting = new List<string>();
                    _lkReporting.Add("Survivor");
                    _lkReporting.Add("Family Member");
                    _lkReporting.Add("Friend");
                    _lkReporting.Add("Caregiver / Guardian");
                    _lkReporting.Add("Other");
               
                }

                if (!_lkReporting.Any(x => x.Equals(_main.IncidentReportedBy, StringComparison.InvariantCultureIgnoreCase)))
                {
                    AddErrorMessage("Incident Reported By does not match with Lookup Values", null);
                }


            }
            catch (Exception ex)
            {
                AddErrorMessage($"ReportedByValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void FocalPointNameValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_main.ContactFocalPointPersonName)) AddErrorMessage("Contact Focal Point Name empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"FocalPointNameValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void ContactNumberValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_main.ContactNumber)) AddErrorMessage("Contact Number empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"ContactNumberValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void GenderValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_main.Gender)) AddErrorMessage("Gender empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"GenderValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void AgeValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_main.AgeOfSurvivor)) AddErrorMessage("Age of Survivor empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"AgeValidation ERROR - {ex.Message} ", ex);

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
                    AddErrorMessage("SSSNP Beneficiary does not match with Lookup Values", null);
                }


            }
            catch (Exception ex)
            {
                AddErrorMessage($"SSSNPBeneficieryValidation ERROR - {ex.Message} ", ex);

            }
        }
        private void ReferralServicesValidation()
        {
            try
            {
                if (!string.IsNullOrEmpty(_main.ReferralServicesRecommended))
                {

                    List<string> _lkReporting = new List<string>();

                    if (_lkReporting == null || _lkReporting.Count == 0)
                    {
                        _lkReporting = new List<string>();
                        _lkReporting.Add("No");
                        _lkReporting.Add("GBV Case Management");
                        _lkReporting.Add("Protection Services");
                        _lkReporting.Add("Medical Services");
                        _lkReporting.Add("Psychosocial Services");
                        _lkReporting.Add("Legal assistances");
                        _lkReporting.Add("Safe Space");
                        _lkReporting.Add("Other");

                    }

                    if (!_lkReporting.Any(x => x.Equals(_main.ReferralServicesRecommended, StringComparison.InvariantCultureIgnoreCase)))
                    {
                        AddErrorMessage("Referral Services Recommended with Lookup Values", null);
                    }
                   
                } else
                {
                    _main.ReferralServicesRecommended = "No";
                }
                               


            }
            catch (Exception ex)
            {
                AddErrorMessage($"ReportedByValidation ERROR - {ex.Message} ", ex);

            }
        }


        //private void DescriptionOfIncidentValidation()
        //{
        //    try
        //    {

        //        if (string.IsNullOrEmpty(_detail.DescriptionOfTheIncident)) AddErrorMessage("Description Of Incident empty", null);

        //    }
        //    catch (Exception ex)
        //    {
        //        AddErrorMessage($"DescriptionOfIncidentValidation ERROR - {ex.Message} ", ex);

        //    }
        //}
        private void FollowUpFeedbackValidation()
        {
            try
            {

                if (string.IsNullOrEmpty(_detail.FollowUpActionFeedback)) AddErrorMessage("FollowUp Action Feedback empty", null);

            }
            catch (Exception ex)
            {
                AddErrorMessage($"FollowUpFeedbackValidation ERROR - {ex.Message} ", ex);

            }
        }
    

    }
}

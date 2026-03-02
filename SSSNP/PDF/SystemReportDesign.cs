using iTextSharp.text;
using SSSNP_SNSOP.Core;
using SSSNP_SNSOP.Core.BuilderPdfGenerator;
using SSSNP_SNSOP.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSSNP_SNSOP.PDF
{
    public class SystemReportDesign : BuilderPdfTemplate, IBuilderPdfTriggerTemplate
    {
        private Dictionary<string, IElement> _templateElements = new Dictionary<string, IElement>();
        private string _triggerReferenceId;
        private bool _isELRP;
        private int _year;
        private int _month;
        public SystemReportDesign(bool isELRP, int year, int month)
        {
            _isELRP = isELRP;
            _year = year;
            _month = month;

            try
            {
                //Execute DataSource
                this.ExecuteDataSource();

                // Header Section Pdf
                this.HeaderSection();

                // Body Section Pdf
                this.BodySection();
            }
            catch (Exception ex)
            {

                throw new Exception(string.Format("Trigger Template: {0} ", this.Trigger_Reference_Id), ex);
            }
        }

        private void ExecuteDataSource()
        {

        }

        private void HeaderSection()
        {


        }
        private void BodySection()
        {
            GrievanceByStatus();
            GrievanceByTypeGBV_NonGBV();
            Non_GBVGrievanceByType();
            GrievanceByProgram();
            Grievance_DistributionByLocationType();
            GBVByProgramStatus();
            AgeBreakdownSSSNP();
            GBVLocationAgeGroup();
            SSSNPRelatedComplaintLog();
            ResolutionTimes();
            AvarageResolutionTimesByLocaltion();
            DelayedGrievanceByType();
        }
        private void GrievanceByStatus()
        {

            Add(TitleNode("Grievances by Status"));

            List<LabelPropertyThreeTable> source = new List<LabelPropertyThreeTable>();

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetGrievanceByStatus();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyThreeTable() { PropertyName = "Status", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

                source.Add(new LabelPropertyThreeTable() { PropertyName = "Total", PropertyValue1 = _total != null ? _total.ToString() : "0", PropertyValue2 = "" });
            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyThreeTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }
            Add(CreateTableThreeColumnKeyValues(source));
        }

        private void GrievanceByTypeGBV_NonGBV()
        {

            Add(TitleNode("Grievances by Category (GBV & Non-GBV)"));

            List<LabelPropertyThreeTable> source = new List<LabelPropertyThreeTable>();

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetGrievanceByTypeGBV_NonGBV();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyThreeTable() { PropertyName = "Category", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

                source.Add(new LabelPropertyThreeTable() { PropertyName = "Total", PropertyValue1 = _total != null ? _total.ToString() : "0", PropertyValue2 = "" });
            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyThreeTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }



            Add(CreateTableThreeColumnKeyValues(source));

        }
        private void Non_GBVGrievanceByType()
        {

            Add(TitleNode("Non-GBV Grievances by Type"));

            List<LabelPropertyThreeTable> source = new List<LabelPropertyThreeTable>();

            source.Add(new LabelPropertyThreeTable() { PropertyName = "Type", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetNon_GBVGrievanceByType();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyThreeTable() { PropertyName = "Type", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

                source.Add(new LabelPropertyThreeTable() { PropertyName = "Total", PropertyValue1 = _total != null ? _total.ToString() : "0", PropertyValue2 = "" });
            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyThreeTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }
            Add(CreateTableThreeColumnKeyValues(source));

        }



        private void GrievanceByProgram()
        {

            Add(TitleNode("Grievances by Program"));

            List<LabelPropertyThreeTable> source = new List<LabelPropertyThreeTable>();

            source.Add(new LabelPropertyThreeTable() { PropertyName = "Type", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetGrievancesByProgram();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyThreeTable() { PropertyName = "Programme", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

                source.Add(new LabelPropertyThreeTable() { PropertyName = "Total", PropertyValue1 = _total != null ? _total.ToString() : "0", PropertyValue2 = "" });
            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyThreeTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }
            Add(CreateTableThreeColumnKeyValues(source));

        }

        public void Grievance_DistributionByLocationType()
        {
            Add(TitleNode("Grievance Distribution By Location & Category"));

            List<LabelPropertySixTable> source = new List<LabelPropertySixTable>();

            source.Add(new LabelPropertySixTable() { PropertyName = "County", PropertyValue1 = "Category", PropertyValue2 = "Number", PropertyValue3 = "Percentage of total", PropertyValue4 = "% MALE from complaints with gender data", PropertyValue5 = "% FEMALE from complaints with gender data" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetGrievancesByLocationAndType();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue2));

                source.Insert(0, new LabelPropertySixTable() { PropertyName = "Programme", PropertyValue1 = "Category", PropertyValue2 = "Number", PropertyValue3 = "Percentage of Total", PropertyValue4 = "% MALE from complaints with gender data", PropertyValue5 = "% FEMALE from complaints with gender data" });

                source.Add(new LabelPropertySixTable() { PropertyName = "Total", PropertyValue1 = "", PropertyValue2 = _total != null ? _total.ToString() : "0", PropertyValue3 = "", PropertyValue4 = "", PropertyValue5 = "" });
            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertySixTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }



            Add(CreateTableSixColumnKeyValues(source));
        }

        public void GBVByProgramStatus()
        {
            Add(TitleNode("GBV Grievances by Program & Status"));

            List<LabelPropertyFourTable> source = new List<LabelPropertyFourTable>();

            source.Add(new LabelPropertyFourTable() { PropertyName = "Program", PropertyValue1 = "Status", PropertyValue2 = "Number", PropertyValue3 = "Percentage of Total" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetGBVByProgramStatus();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue2));

                source.Insert(0, new LabelPropertyFourTable() { PropertyName = "Program", PropertyValue1 = "Status", PropertyValue2 = "Number", PropertyValue3 = "Percentage of total" });

                source.Add(new LabelPropertyFourTable() { PropertyName = "Total", PropertyValue1 = "", PropertyValue2 = _total != null ? _total.ToString() : "0", PropertyValue3 = "" });

            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyFourTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "", PropertyValue3 = "" });
            }
            Add(CreateTableFourColumnKeyValues(source));
        }
        private void AgeBreakdownSSSNP()
        {

            Add(TitleNode("Age Breakdown of SNSOP - related GBV complaints"));

            List<LabelPropertyThreeTable> source = new List<LabelPropertyThreeTable>();

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetAgeBreakdownSSSNP();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyThreeTable() { PropertyName = "Category", PropertyValue1 = "Number", PropertyValue2 = "Percentage of Total" });

                source.Add(new LabelPropertyThreeTable() { PropertyName = "Total", PropertyValue1 = _total != null ? _total.ToString() : "0", PropertyValue2 = "" });
            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyThreeTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }

            Add(CreateTableThreeColumnKeyValues(source));

        }
        private void GBVLocationAgeGroup()
        {
            Add(TitleNode("GBV Grievances by Location & Age Group"));

            List<LabelPropertyFourTable> source = new List<LabelPropertyFourTable>();

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetGBVLocationAgeGroup();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyFourTable() { PropertyName = "Location", PropertyValue1 = "Number of SSNP-Related GBV Grievances", PropertyValue2 = "Percentage of Total", PropertyValue3 = "Number of minors (17and under)" });


            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyFourTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "", PropertyValue3 = "" });
            }


            Add(CreateTableFourColumnKeyValues(source));

        }
        public void SSSNPRelatedComplaintLog()
        {
            Add(TitleNode("SNSOP-Related GBV Complaint Log"));

            List<LabelPropertyFiveTable> source = new List<LabelPropertyFiveTable>();

            source.Add(new LabelPropertyFiveTable() { PropertyName = "Location (County)", PropertyValue1 = "Date Reported", PropertyValue2 = "Status", PropertyValue3 = "Gender", PropertyValue4 = "Age" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetSSNPRelatedComplaintLog();

                //source = SSSNPContext.SSSNPManager.GetSSNPRelatedComplaintLog();

                //_total = source.Sum(x => Convert.ToInt32(x.PropertyValue1));

                source.Insert(0, new LabelPropertyFiveTable() { PropertyName = "Location", PropertyValue1 = "Date Reported", PropertyValue2 = "Status", PropertyValue3 = "Gender", PropertyValue4 = "Age" });

            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyFiveTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "", PropertyValue3 = "" });
            }

            Add(CreateTableFiveColumnKeyValues(source));
        }
        private void ResolutionTimes()
        {

            Add(TitleNode("Resolution Time of All Grievances"));

            List<LabelPropertyFourTable> source = new List<LabelPropertyFourTable>();
            source.Insert(0, new LabelPropertyFourTable() { PropertyName = "Resolution Time", PropertyValue1 = "Category", PropertyValue2 = "Number", PropertyValue3 = "Percentage of Total" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.GetResolutionTimes();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue2));

                source.Insert(0, new LabelPropertyFourTable() { PropertyName = "Resolution Time", PropertyValue1 = "Category", PropertyValue2 = "Number", PropertyValue3 = "Percentage of Total" });
                source.Add(new LabelPropertyFourTable() { PropertyName = "Total", PropertyValue1 = "", PropertyValue2 = _total != null ? _total.ToString() : "0", PropertyValue3 = "" });

            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyFourTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }


            Add(CreateTableFourColumnKeyValues(source));

        }
        private void AvarageResolutionTimesByLocaltion()
        {

            Add(TitleNode("Average Resolution Times By Location"));

            List<LabelPropertyThreeTable> source = new List<LabelPropertyThreeTable>();

            source.Add(new LabelPropertyThreeTable() { PropertyName = "Location (County)", PropertyValue1 = "Category", PropertyValue2 = "Average Resolution Time (Days)" });

            try
            {

                source = SSSNPContext.SSSNPManager.GetAverageResolutionTimeByLocation();

                source.Insert(0, new LabelPropertyThreeTable() { PropertyName = "Location (County)", PropertyValue1 = "Category", PropertyValue2 = "Average Resolution Time (Days)" });

            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyThreeTable() { PropertyName = "ERROR", PropertyValue1 = "", PropertyValue2 = "" });
            }

            Add(CreateTableThreeColumnKeyValues(source));

        }
        private void DelayedGrievanceByType()
        {

            Add(TitleNode("Delayed Grievance by Category"));

            List<LabelPropertyTwoTable> source = new List<LabelPropertyTwoTable>();

            source.Add(new LabelPropertyTwoTable() { PropertyName = "Category", PropertyValue = "Number of Grievances with Resolution Time of 16 days or more" });

            try
            {
                int? _total = 0;

                source = SSSNPContext.SSSNPManager.DelayedGrievanceByType();

                _total = source.Sum(x => Convert.ToInt32(x.PropertyValue));

                source.Insert(0, new LabelPropertyTwoTable() { PropertyName = "Category", PropertyValue = "Number of Grievances with Resolution Time of 16 days or more" });

                source.Add(new LabelPropertyTwoTable() { PropertyName = "Total", PropertyValue = _total != null ? _total.ToString() : "0" });

            }
            catch (Exception ex)
            {

                source.Add(new LabelPropertyTwoTable() { PropertyName = "ERROR", PropertyValue = "" });
            }

            Add(CreateTableTwoColumnKeyValues(source));

        }
        public void Add(IElement e)
        {
            Add(e, string.Format("{0}_Elements_{1}", this.Trigger_Reference_Id, this._templateElements.Count.ToString()));
        }
        public void Add(IElement e, string elementName)
        {

            this._templateElements.Add(elementName, e);

        }
        public string Trigger_Reference_Id
        {
            get { return this._triggerReferenceId; }
        }
        public Dictionary<string, iTextSharp.text.IElement> CreateDocument()
        {
            return _templateElements;
        }


    }
}
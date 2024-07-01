using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Newtonsoft.Json;
using OfficeOpenXml;
using SSSNP.Entity.Payroll;
using SSSNP.Network;

public partial class PayrollDownload : System.Web.UI.Page
{
    private LookupManagerService _lookupManagerService;
    private string accessToken;
    private string apiUrl;

    protected void Page_Load(object sender, EventArgs e)
    {
        InitializeServices();
        if (!IsPostBack)
        {
            LoadStates();
        }

        accessToken = "";
    }

    private void InitializeServices()
    {
        try
        {
            string connectionString = ConfigurationManager.ConnectionStrings["SNSDPSkils"].ConnectionString;
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new Exception("Connection string 'SNSDPSkils' is not found or empty.");
            }

            _lookupManagerService = new LookupManagerService(connectionString);
        }
        catch (Exception ex)
        {
            LogError(new Exception("Error initializing services: " + ex.Message, ex));
        }
    }

    private void LoadStates()
    {
        try
        {
            DataTable dtStates = _lookupManagerService.GetStateList();
            ddlState.DataSource = dtStates;
            ddlState.DataTextField = "StateName";
            ddlState.DataValueField = "intStateid";
            ddlState.DataBind();
            ddlState.Items.Insert(0, new ListItem("--Select--", ""));
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlCounty.Items.Clear();
        ddlCounty.Items.Add(new ListItem("--Select--", ""));
        if (!string.IsNullOrEmpty(ddlState.SelectedValue))
        {
            try
            {
                int stateId;
                bool isParsed = int.TryParse(ddlState.SelectedValue, out stateId);
                if (isParsed)
                {
                    DataTable dtCounties = _lookupManagerService.GetCountyListByStateId(stateId);
                    ddlCounty.DataSource = dtCounties;
                    ddlCounty.DataTextField = "CountieName";
                    ddlCounty.DataValueField = "intCountieid";
                    ddlCounty.DataBind();
                    ddlCounty.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    LogError(new Exception("ddlState.SelectedValue is not a valid integer: " + ddlState.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }

    protected void ddlCounty_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlPayam.Items.Clear();
        ddlPayam.Items.Add(new ListItem("--Select--", ""));
        if (!string.IsNullOrEmpty(ddlCounty.SelectedValue))
        {
            try
            {
                int countyId;
                bool isParsed = int.TryParse(ddlCounty.SelectedValue, out countyId);
                if (isParsed)
                {
                    DataTable dtPayams = _lookupManagerService.GetPayamListByCountyId(countyId);
                    ddlPayam.DataSource = dtPayams;
                    ddlPayam.DataTextField = "PayamName";
                    ddlPayam.DataValueField = "intPayamid";
                    ddlPayam.DataBind();
                    ddlPayam.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    LogError(new Exception("ddlCounty.SelectedValue is not a valid integer: " + ddlCounty.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }

    protected void ddlPayam_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlBoma.Items.Clear();
        ddlBoma.Items.Add(new ListItem("--Select--", ""));
        if (!string.IsNullOrEmpty(ddlPayam.SelectedValue))
        {
            try
            {
                int payamId;
                bool isParsed = int.TryParse(ddlPayam.SelectedValue, out payamId);
                if (isParsed)
                {
                    DataTable dtBomas = _lookupManagerService.GetBomaListByPayamId(payamId);
                    ddlBoma.DataSource = dtBomas;
                    ddlBoma.DataTextField = "BomaName";
                    ddlBoma.DataValueField = "intBomasid";
                    ddlBoma.DataBind();
                    ddlBoma.Items.Insert(0, new ListItem("--Select--", ""));
                }
                else
                {
                    LogError(new Exception("ddlPayam.SelectedValue is not a valid integer: " + ddlPayam.SelectedValue));
                }
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }
    }


    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        pnlResults.Visible = true;
        tblResults.Visible = true;

        var request = new
        {
            state = int.Parse(ddlState.SelectedValue),
            county = int.Parse(ddlCounty.SelectedValue),
            payam = int.Parse(ddlPayam.SelectedValue),
            boma = int.Parse(ddlBoma.SelectedValue),
            createdFrom = (string)null,
            createdTo = (string)null,
            payrollId = (string)null
        };

        string uri = ConfigurationManager.AppSettings["DownloadPayroll"];
        String token = "eyJhbGciOiJIUzUxMiJ9.eyJzdWIiOiJzYWtpYiIsImlhdCI6MTcxNDk4MTEwOCwiZXhwIjoxODcyNjYxMTA4fQ.7FgalnXhBOcC7q-nYfONbxmc7My4iUOeWpN2eK3b7vc_br43NCEFiKWIrXs1cJTsU_Ds_ZZLHRB28c-jdgiBRw";

        uri = ConfigurationManager.AppSettings["AfisDevBaseUrl"] + ConfigurationManager.AppSettings["DownloadPayroll"];
        if (ConfigurationManager.AppSettings["build.profile.active"].Equals("prod"))
        {
            uri = ConfigurationManager.AppSettings["AfisProdBaseUrl"] + ConfigurationManager.AppSettings["DownloadPayroll"];
        }
        Uri urlCheck = new Uri(uri);
        HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);
        httpWReq.PreAuthenticate = true;
        httpWReq.Headers.Add("Authorization", "Bearer " + token);
        httpWReq.Headers.Add("DeviceId", "29feb6211b04-a56d-7d6a-e79e-018b2f19");

        string reqStr = "";

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
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String jsonresponse = "";
            String temp = null;
            while ((temp = reader.ReadLine()) != null)
            {
                jsonresponse += temp;
            }
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            ApiResponse apiResponse = js.Deserialize<ApiResponse>(jsonresponse);

            // Log the response
            // System.Diagnostics.Debug.WriteLine("API Response: " + JsonConvert.SerializeObject(apiResponse, Formatting.Indented));

            if (apiResponse != null && apiResponse.data != null && apiResponse.data.Count > 0)
            {
                Session["PayrollData"] = apiResponse.data.SelectMany(c => c.payrollData).ToList();
                rgResults.DataSource = Session["PayrollData"];
                rgResults.DataBind();
            }
        }
        catch (Exception ex)
        {
            LogError(ex);
        }
    }

    private void BindResultsToGrid(List<PayrollDataDto> data)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("State");
        dt.Columns.Add("County");
        dt.Columns.Add("Payam");
        dt.Columns.Add("Boma");
        dt.Columns.Add("EnrollmentNo");
        dt.Columns.Add("DateOfCreation");
        dt.Columns.Add("ExchangeRateAtTime");
        dt.Columns.Add("IntBenId");
        dt.Columns.Add("IntWagePaymentReqId");
        dt.Columns.Add("IntWagesReqBenTransId");
        dt.Columns.Add("IntWorkId");
        dt.Columns.Add("PaymentCycle");
        dt.Columns.Add("ReqFromDate");
        dt.Columns.Add("ReqNo");
        dt.Columns.Add("ReqToDate");
        dt.Columns.Add("Status");
        dt.Columns.Add("SupportType");
        dt.Columns.Add("TotAttendance");
        dt.Columns.Add("WageRateSSPTotal");
        dt.Columns.Add("WageRateUSD");
        dt.Columns.Add("WageRateUSDTotal");
        dt.Columns.Add("WorkCode");

        foreach (var item in data)
        {
            dt.Rows.Add(
                item.state,
                item.county,
                item.payam,
                item.boma,
                item.enrollmentNo,
                item.dateofCreation,
                item.exchangerateattime,
                item.intBenid,
                item.intWagePaymentReqid,
                item.intWagesReqBenTransid,
                item.intWorkid,
                item.paymentCycle,
                item.reqFromDate,
                item.reqNo,
                item.reqToDate,
                item.status,
                item.supportType,
                item.totAttendance,
                item.wageRateSSPTotal,
                item.wageRateUSD,
                item.wageRateUSDTotal,
                item.workCode
            );
        }

        rgResults.DataSource = dt;
        rgResults.DataBind();
    }


    protected void rgResults_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        if (Session["PayrollData"] != null)
        {
            rgResults.DataSource = Session["PayrollData"];
        }
    }


    protected void rgResults_ItemDataBound(object sender, Telerik.Web.UI.GridItemEventArgs e)
    {
    }

    protected void BtnDownload_Click(object sender, EventArgs e)
    {
        List<PayrollDataDto> payrollData = Session["PayrollData"] as List<PayrollDataDto>;
        if (payrollData != null && payrollData.Count > 0)
        {
            ExportToExcel(payrollData);
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("No data available to download.");
        }
    }

    private void ExportToExcel(List<PayrollDataDto> payrollData)
    {
        using (ExcelPackage pck = new ExcelPackage())
        {
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("PayrollData");

            ws.Cells["A1"].Value = "State";
            ws.Cells["B1"].Value = "County";
            ws.Cells["C1"].Value = "Payam";
            ws.Cells["D1"].Value = "Boma";
            ws.Cells["E1"].Value = "EnrollmentNo";
            ws.Cells["F1"].Value = "DateOfCreation";
            ws.Cells["G1"].Value = "ExchangeRateAtTime";
            ws.Cells["H1"].Value = "IntBenId";
            ws.Cells["I1"].Value = "IntWagePaymentReqId";
            ws.Cells["J1"].Value = "IntWagesReqBenTransId";
            ws.Cells["K1"].Value = "IntWorkId";
            ws.Cells["L1"].Value = "PaymentCycle";
            ws.Cells["M1"].Value = "ReqFromDate";
            ws.Cells["N1"].Value = "ReqNo";
            ws.Cells["O1"].Value = "ReqToDate";
            ws.Cells["P1"].Value = "Status";
            ws.Cells["Q1"].Value = "SupportType";
            ws.Cells["R1"].Value = "TotAttendance";
            ws.Cells["S1"].Value = "WageRateSSPTotal";
            ws.Cells["T1"].Value = "WageRateUSD";
            ws.Cells["U1"].Value = "WageRateUSDTotal";
            ws.Cells["V1"].Value = "WorkCode";

            int row = 2;
            foreach (var item in payrollData)
            {
                ws.Cells["A" + row].Value = item.state;
                ws.Cells["B" + row].Value = item.county;
                ws.Cells["C" + row].Value = item.payam;
                ws.Cells["D" + row].Value = item.boma;
                ws.Cells["E" + row].Value = item.enrollmentNo;
                ws.Cells["F" + row].Value = item.dateofCreation;
                ws.Cells["G" + row].Value = item.exchangerateattime;
                ws.Cells["H" + row].Value = item.intBenid;
                ws.Cells["I" + row].Value = item.intWagePaymentReqid;
                ws.Cells["J" + row].Value = item.intWagesReqBenTransid;
                ws.Cells["K" + row].Value = item.intWorkid;
                ws.Cells["L" + row].Value = item.paymentCycle;
                ws.Cells["M" + row].Value = item.reqFromDate;
                ws.Cells["N" + row].Value = item.reqNo;
                ws.Cells["O" + row].Value = item.reqToDate;
                ws.Cells["P" + row].Value = item.status;
                ws.Cells["Q" + row].Value = item.supportType;
                ws.Cells["R" + row].Value = item.totAttendance;
                ws.Cells["S" + row].Value = item.wageRateSSPTotal;
                ws.Cells["T" + row].Value = item.wageRateUSD;
                ws.Cells["U" + row].Value = item.wageRateUSDTotal;
                ws.Cells["V" + row].Value = item.workCode;

                row++;
            }

            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment; filename=PayrollData.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }
    }

    private void LogError(Exception ex)
    {
        System.Diagnostics.Debug.WriteLine("Error Message: " + ex.Message);
        System.Diagnostics.Debug.WriteLine("Stack Trace: " + ex.StackTrace);
        if (ex.InnerException != null)
        {
            System.Diagnostics.Debug.WriteLine("Inner Exception: " + ex.InnerException.Message);
            System.Diagnostics.Debug.WriteLine("Inner Exception Stack Trace: " + ex.InnerException.StackTrace);
        }
    }
}

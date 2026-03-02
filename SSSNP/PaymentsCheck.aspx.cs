using SSSNP_SNSOP.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Telerik.Web.UI;
using static ImportBeneficiaryAll;

#region Amondment History
//Project : Public Works
//Modified by : Srikanth M
//Modified Date: 27-02-2017
// Purpose : Issue in Dash board and Drildowns
// Modified Store Procedure: Dashboard1DrillDown
#endregion


//created by suresh as on 17-1-2016 tables:td_IPDetails,getIPSearch

public partial class PaymentsCheck : System.Web.UI.Page
{
    #region Declaration
    General Gen = new General();
    int iTotalPaymentsFromAPI;
    int iTotalPaymentsRegistered;
    #endregion



    protected void telGridDetails_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
    {
        telGridDetails.DataSource = BuildPaymCheckList();
        telGridDetails.Visible = true;
    }
    protected void telGridDetails_ItemDataBound(object sender, GridItemEventArgs e)
    {
        if (e.Item is GridFooterItem)
        {
            GridFooterItem footerItem = e.Item as GridFooterItem;
            footerItem["PaymentsFromAPI"].Text = iTotalPaymentsFromAPI.ToString();
            footerItem["PaymentsRegistered"].Text = iTotalPaymentsRegistered.ToString();
        }
    }

    #region Page_Load
    protected void Page_Load(object sender, EventArgs e)
    {
        HttpContext.Current.Server.ScriptTimeout = 500;

        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        if (!IsPostBack)
        {

        }

    }
    #endregion



    protected void BtnClear_Click(object sender, EventArgs e)
    {

    }


    private DataSet GetCountRegisteredPaymentsBomaDB(string BomaID)
    {
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();

        try
        {
            string strQuery = "";
            strQuery = String.Format("select count(*) as cnt, b.intBomasid as BomaID, bomaname as BomaName   from tr_BenWagesPayment a inner join tbl_BeneficiaryDet b on a.intBenid = b.intBenid inner join tm_bomas c on b.intBomasid = c.intbomasid where b.intbomasid = {0} group by b.intBomasid, c.BomaName", BomaID);

            da = new SqlDataAdapter(strQuery, con.GetConnection);
            da.SelectCommand.CommandType = CommandType.Text;

            da.Fill(ds);
            con.CloseConnection();
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            con.CloseConnection();
        }
        return ds;
    }

    private DataSet BuildPaymCheckList()
    {
        List<structPaymentCheckElement> lsPaymChecks = new List<structPaymentCheckElement>();
        DataSet dsPaymChecks = new DataSet();

        try
        {
            //List of Bomas to check
            DataSet dsBomasWageApplied = new DataSet();
            dsBomasWageApplied = Gen.GetBomaListWageApplied();

            if (dsBomasWageApplied.Tables[0].Rows.Count > 0)
            {
                iTotalPaymentsFromAPI = 0;
                iTotalPaymentsRegistered = 0;
                

                int iCntBomas = dsBomasWageApplied.Tables[0].Rows.Count;
                int iIndexBoma = 0;

                //foreach BOMA
                for (iIndexBoma = 0; iIndexBoma < iCntBomas; iIndexBoma++)
                {
                    int iCountRegPayments;
                    structPaymentCheckElement strctPaymCheckTemp = new structPaymentCheckElement();
                    string strBomaId = dsBomasWageApplied.Tables[0].Rows[iIndexBoma][0].ToString();
                    //values from DB
                    DataSet dsCheckPaym = new DataSet();
                    dsCheckPaym = GetCountRegisteredPaymentsBomaDB(strBomaId);
                    if (dsCheckPaym.Tables[0].Rows.Count > 0)
                    {
                        strctPaymCheckTemp.PaymentsRegistered = System.Convert.ToInt32(dsCheckPaym.Tables[0].Rows[0]["cnt"]);
                        strctPaymCheckTemp.BomaID = System.Convert.ToInt32(dsCheckPaym.Tables[0].Rows[0]["BomaID"]);
                        strctPaymCheckTemp.BomaName = (dsCheckPaym.Tables[0].Rows[0]["BomaName"]).ToString();
                        //Values from API
                        strctPaymCheckTemp.PaymentsFromAPI = GetCountPaymentsFromAPI(strBomaId);

                        lsPaymChecks.Add(strctPaymCheckTemp);
                        iTotalPaymentsFromAPI = iTotalPaymentsFromAPI + strctPaymCheckTemp.PaymentsFromAPI;
                        iTotalPaymentsRegistered = iTotalPaymentsRegistered + strctPaymCheckTemp.PaymentsRegistered;
                    }
                    //
                }
            }

            //DataSet dsPaymChecks = new DataSet();
            dsPaymChecks.Tables.Add();
            dsPaymChecks.Tables[0].Columns.Add("BomaID");
            dsPaymChecks.Tables[0].Columns.Add("BomaName");
            dsPaymChecks.Tables[0].Columns.Add("PaymentsRegistered");
            dsPaymChecks.Tables[0].Columns.Add("PaymentsFromAPI");
            dsPaymChecks.Tables[0].Columns.Add("PaymentsDifference");

            foreach (structPaymentCheckElement paymentCheckElement in lsPaymChecks)
            {
                int iDifference = System.Convert.ToInt32(paymentCheckElement.PaymentsRegistered) - System.Convert.ToInt32(paymentCheckElement.PaymentsFromAPI);
                dsPaymChecks.Tables[0].Rows.Add(paymentCheckElement.BomaID, paymentCheckElement.BomaName, paymentCheckElement.PaymentsRegistered, paymentCheckElement.PaymentsFromAPI, iDifference.ToString());
            }
        }
        catch (Exception ex)
        {
        }

        return dsPaymChecks;
    }



    private int GetCountPaymentsFromAPI(string BomaID)
    {
        DB.DB con = new DB.DB();
        con.OpenConnection();
        SqlDataAdapter da;
        DataSet ds = new DataSet();
        string strJson = "";
        int iRegisteredPayments = 0;

        try
        {
            string URI = System.Configuration.ConfigurationManager.AppSettings["GetPayments"] + "/" + BomaID;

            Uri urlCheck = new Uri(URI);
            HttpWebRequest httpWReq = (HttpWebRequest)WebRequest.Create(urlCheck);

            string apikey = System.Configuration.ConfigurationManager.AppSettings["APIpasskey"];
            httpWReq.PreAuthenticate = true;
            httpWReq.Headers.Add("Authorization", "Bearer " + apikey.HashSHA512());


            httpWReq.ProtocolVersion = HttpVersion.Version11;
            httpWReq.Method = "GET";
            httpWReq.ContentType = "application/json; charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)httpWReq.GetResponse();
            string s = response.ToString();
            StreamReader reader = new StreamReader(response.GetResponseStream());
            String jsonresponse = "";
            String temp = null;
            while ((temp = reader.ReadLine()) != null)
            {
                jsonresponse += temp;
            }
            strJson = jsonresponse;
            JavaScriptSerializer js = new JavaScriptSerializer();
            js.MaxJsonLength = Int32.MaxValue;
            paymentsRootObject obj = js.Deserialize<paymentsRootObject>(strJson);
            if (obj != null)
            {
                if (obj.error == false)
                {
                    iRegisteredPayments = (int)obj.paidPayments;
                }
                else
                {
                    return 0;
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {

        }
        return iRegisteredPayments;
    }

    private class paymentsRootObject
    {
        public bool error { get; set; }
        public int errorCode { get; set; }
        public int paidPayments { get; set; }
    }


    struct structPaymentCheckElement
    {
        public int BomaID;
        public string BomaName;
        public int PaymentsFromAPI;
        public int PaymentsRegistered;

        public structPaymentCheckElement(int BomaID, string BomaName, int PaymentsFromAPI, int PaymentsRegistered)
        {
            this.BomaID = BomaID;
            this.BomaName = BomaName;
            this.PaymentsFromAPI = PaymentsFromAPI;
            this.PaymentsRegistered = PaymentsRegistered;
        }
    }





}

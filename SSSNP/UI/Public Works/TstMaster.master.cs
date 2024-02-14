using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using System.Web.Services;

public partial class TstMaster : System.Web.UI.MasterPage
{

    [WebMethod]
    public string Username()
    {
        return Session["username"].ToString();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
       

        if (Session.Count <= 0)
        {
            Response.Redirect("../../frmUserLogin.aspx");
        }
        //////TSTDashboard.Visible = false;
        //////IPDashboard.Visible = false;
        //////LiPA.Visible = false;
        //////LiDashboardIP.Visible = false;



        BDC.Visible = false;
        PDC.Visible = false;

        LiIp1.Visible = false;
        LiIp2.Visible = false;
        LiIp3.Visible = false;
        LiIp4.Visible = false;
        LiIp5.Visible = false;
       // LiIp6.Visible = false;
        LiIp7.Visible = false;
        LiIp8.Visible = false;
        LiIp9.Visible = false;
        LiIp10.Visible = false;
        LiIp11.Visible = false;
        LiTST1.Visible = false;
        LiTST2.Visible = false;
        LiTST3.Visible = false;
        LiTST4.Visible = false;
        //  LiTST5.Visible = false;
        LiTST6.Visible = false;

        LiTST7.Visible = false;
        LiTST8.Visible = false;
        LiReviewVerify.Visible = false;
        LiCO1.Visible = false;
        LiSpeccialist1.Visible = false;
        LiManger1.Visible = false;
        LiCO2.Visible = false;
        LiSpeccialist2.Visible = false;
        LiManger2.Visible = false;
        LiPA1.Visible = false;
        LiPA2.Visible = false;
        LiPA3.Visible = false;
        LiWB2.Visible = false;
        LiWB1.Visible = false;
        LiWB3.Visible = false;
        LiWB4.Visible = false;
        Masters1.Visible = false;
        Masters2.Visible = false;
        PayConnect2.Visible = false;
        PayConnect1.Visible = false;
        Masters2.Visible = false;
        LiAlpha1.Visible = false;
        LiAlpha2.Visible = false;
        Li.Visible = false;
        LiPA8.Visible = false;
        if (Session.Count > 0)
        {
            lblwecome.Text = Session["username"].ToString();

            if (Session["user_type"].ToString() == "IP")
            {
                LiIp1.Visible = true;
                LiIp2.Visible = true;
                LiIp3.Visible = true;
                LiIp4.Visible = true;
                LiIp5.Visible = true;
               // LiIp6.Visible = true;
                LiIp7.Visible = true;
                LiIp8.Visible = true;
                LiIp9.Visible = true;
                LiIp10.Visible = true;
                LiIp11.Visible = true;

            }
            if (Session["user_type"].ToString() == "TST")
            {
                ////LiTST1.Visible = true;
                ////LiTST2.Visible = true;
                ////LiTST3.Visible = true;
                ////LiTST4.Visible = true;
                //////  LiTST5.Visible = true;
                ////LiTST6.Visible = true;
                ////LiTST7.Visible = true;
                ////LiTST8.Visible = true;

                if (Session["Desgid"].ToString() == "1000")
                {
                    LiTST1.Visible = true;
                    LiTST2.Visible = true;
                    LiTST3.Visible = true;
                    LiTST4.Visible = true;
                    //  LiTST5.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    LiTST8.Visible = true;
                }
                if (Session["Desgid"].ToString() == "1001" || Session["Desgid"].ToString() == "1004")
                {
                    //  LiTST1.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    //   TSTDashboard.Visible = false;
                    //   trCo.Visible = true;
                    // trSp.Visible = true;
                    //  trMa.Visible = true;
                }

                if (Session["Desgid"].ToString() == "1002")
                {
                    LiTST1.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    TSTDashboard.Visible = false;
                    trCo.Visible = true;
                    trSp.Visible = false;
                    trMa.Visible = false;
                }
                if (Session["Desgid"].ToString() == "1003" || Session["Desgid"].ToString() == "1005" || Session["Desgid"].ToString() == "1011" || Session["Desgid"].ToString() == "1007" || Session["Desgid"].ToString() == "1009")
                {
                    if (Session["Desgid"].ToString() == "1007" || Session["Desgid"].ToString() == "1009")
                    {
                        LiTST6.Visible = true;
                        LiTST7.Visible = true;
                    }
                    else
                    {
                        LiTST1.Visible = true;
                        LiTST6.Visible = true;
                        LiTST7.Visible = true;
                        trCo.Visible = false;
                        trSp.Visible = true;
                        trMa.Visible = false;
                        TSTDashboard.Visible = false;
                    }

                }
                if (Session["Desgid"].ToString() == "1006" || Session["Desgid"].ToString() == "1010")
                {
                    if (Session["Desgid"].ToString() == "1010")
                    {
                        LiTST6.Visible = true;
                        LiTST7.Visible = true;
                    }
                    else
                    {
                        LiTST1.Visible = true;
                        LiTST6.Visible = true;
                        LiTST7.Visible = true;
                        trCo.Visible = false;
                        trSp.Visible = false;
                        trMa.Visible = true;
                        TSTDashboard.Visible = false;
                    }
                }

                if (Session["Desgid"].ToString() == "1002")
                {
                    LiTST1.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    trCo.Visible = true;
                    trSp.Visible = false;
                    trMa.Visible = false;
                    TSTDashboard.Visible = false;
                }
                if (Session["Desgid"].ToString() == "1002")
                {
                    LiTST1.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    trCo.Visible = true;
                    trSp.Visible = false;
                    trMa.Visible = false;
                    TSTDashboard.Visible = false;
                }
            }
            if (Session["user_type"].ToString() == "Cordinator")
            {
                LiReviewVerify.Visible = true;
                LiCO1.Visible = true;
                LiCO2.Visible = true;
            }
            if (Session["user_type"].ToString() == "Specialist")
            {
                LiSpeccialist1.Visible = true;
                LiSpeccialist2.Visible = true;
                LiReviewVerify.Visible = true;
            }
            if (Session["user_type"].ToString() == "Manager")
            {
                LiManger1.Visible = true;
                LiManger2.Visible = true;
                LiReviewVerify.Visible = true;
            }
            if (Session["user_type"].ToString() == "PA")
            {
                LiPA1.Visible = true;
                LiPA2.Visible = true;
                LiPA3.Visible = true;
                LiPA8.Visible = true;
            }
            if (Session["user_type"].ToString() == "WB")
            {
                LiWB2.Visible = true;
                LiWB1.Visible = true;
                LiWB3.Visible = true;
                LiWB4.Visible = true;
            }
            if (Session["user_type"].ToString() == "Adm")
            {
                Masters1.Visible = true;
                //   Masters2.Visible = true;
                LiTST6.Visible = true;
                LiTST7.Visible = true;
                LiTST8.Visible = true;
            }
            if (Session["user_type"].ToString() == "EnvSpec")
            {
                LiAlpha1.Visible = false;
                LiAlpha2.Visible = false;
                liEnvSpec.Visible = true;
            }
            if (Session["user_type"].ToString() == "QualEng")
            {
                LiAlpha1.Visible = false;
                LiAlpha2.Visible = false;
                liEnvSpec.Visible = true;
            }
            if (Session["user_type"].ToString() == "")
            {
                // Masters1.Visible = true;
                Masters2.Visible = true;
                PayConnect2.Visible = true;
                PayConnect1.Visible = true;
                Li.Visible = true;
                // LiTST6.Visible = true;
                // LiTST7.Visible = true;
                // LiTST8.Visible = true;
            }
            if (Session["user_type"].ToString() == "PC")
            {
                PayConnect2.Visible = true;
                PayConnect1.Visible = true;
            }
            if (Session["user_type"].ToString() == "ALP")
            {
                LiAlpha1.Visible = true;
                LiAlpha2.Visible = true;
            }
            if (Session["user_type"].ToString() == "BDC")
            {
                BDC.Visible = true;
            }
            if (Session["user_type"].ToString() == "PDC")
            {
                PDC.Visible = true;
            }

        }
        else
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }

    }



}
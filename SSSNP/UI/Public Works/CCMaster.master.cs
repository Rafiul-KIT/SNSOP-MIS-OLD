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

public partial class CCMaster : System.Web.UI.MasterPage
{
    General Gen = new General();
    [WebMethod]
    public string Username()
    {
        return Session["username"].ToString();
    }

    private void setAllMenuPermission(bool value)
    {
        LiIp1.Visible = value;
        IPDashboard.Visible = value;
        IPInvoiceDashboard.Visible = value;
        APIDashboard.Visible = value;

        LiIp2.Visible = value;

        LiIp3.Visible = value;
        workProposalMenu.Visible = value;
        beneficiaryRegistrationMenu.Visible = value;
        jobCardMenu.Visible = value;
        printIdCardMenu.Visible = value;
        workProgressMenu.Visible = value;
        lipwExportMenu.Visible = value;
        BeneficiaryListMenu.Visible = value;

        RejectedWages.Visible = value;
        WageRequestDraft.Visible = value;

        LiIp4.Visible = value;

        LiIp5.Visible = value;
        WageRequestApplyLIPW.Visible = value;
        WageRequestApplyDIS.Visible = value;
        WageRequestDraft.Visible = value;
        RejectedWages.Visible = value;

        LiPaymentCycleDocument.Visible = value;
        liUploadPaymentCycleDoc.Visible = value;
        liViewPaymentCycleDoc.Visible = value;

        EconomicOpportunities.Visible = value;
        EconomicOpportunitiesDashboard.Visible = value;
        Component2TemplateImporting.Visible = value;
        HouseholdGroupView.Visible = value;
        HouseholdAndGroupIdMapping.Visible = value;
        BeneficiaryRandomizationandEnrollment.Visible = value;
        TrainingandAttendanceTracking.Visible = value;
        BusinessPlanDevelopment.Visible = value;
        LivelihoodGrantDisbursement.Visible = value;
        FinancialServiceLinkageTracking.Visible = value;
        CoachingSessionTracking.Visible = value;
        AuditRecordDownload.Visible = value;

        LiPayroll.Visible = value;
        payrollDownload.Visible = value;
        payrollReconcile.Visible = value;

        LiMNE.Visible = value;

        LiUM.Visible = value;

        LiIp6.Visible = value;
        nongbvMenu.Visible = value;
        nongbvDashboardMenu.Visible = value;
        gbvNewMenu.Visible = value;
        gbvNewMenuDashboard.Visible = value;
        grmReport.Visible = value;
        grmImporting.Visible = value;

        LiIp7.Visible = value;

        LiIp8.Visible = value;

        LiIp11.Visible = value;

        LiIp9.Visible = value;

        LiTST1.Visible = value;
        TSTDashboard.Visible = value;

        LiTST2.Visible = value;
        LiTST3.Visible = value;
        LiTST4.Visible = value;
        LiTST5.Visible = value;
        LiTST6.Visible = value;
        LiTST7.Visible = value;
        LiTST8.Visible = value;

        LiCO1.Visible = value;
        LiCO2.Visible = value;
        LiSpeccialist1.Visible = value;
        LiSpeccialist2.Visible = value;
        LiManger1.Visible = value;
        LiManger2.Visible = value;
        LiReviewVerify.Visible = value;

        LiPA1.Visible = value;
        LiPA2.Visible = value;
        LiPA8.Visible = value;
        LiPA3.Visible = value;

        LiWB4.Visible = value;
        LiWB2.Visible = value;
        LiWB1.Visible = value;
        LiWB3.Visible = value;

        PayConnect2.Visible = value;
        PayConnect1.Visible = value;
        LiAlpha1.Visible = value;
        LiAlpha2.Visible = value;

        liFrim.Visible = value;
        BDC.Visible = value;
        PDC.Visible = value;
        liEnvSpec.Visible = value;
        Li1.Visible = value;
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
        LiIp6.Visible = false;
        LiIp7.Visible = false;
        LiIp8.Visible = false;
        LiIp9.Visible = false;
        LiIp10.Visible = false;
        LiIp11.Visible = false;
        LiTST1.Visible = false;
        LiTST2.Visible = false;
        LiTST3.Visible = false;
        LiTST4.Visible = false;
        LiPayroll.Visible = false;
        LiMNE.Visible = false;
        LiUM.Visible = false;
        EconomicOpportunities.Visible = false;
        //  LiTST5.Visible = false;
        LiTST6.Visible = false;
        LiTST5.Visible = false;
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
        liFrim.Visible = false;
        LiPA8.Visible = false;

        LiPaymentCycleDocument.Visible = false;
        liUploadPaymentCycleDoc.Visible = false;
        liViewPaymentCycleDoc.Visible = false;

        setAllMenuPermission(false);

        if (Session.Count > 0)
        {
            lblwecome.Text = Session["username"].ToString();
            liEnvSpec.Visible = false;

            if (Session["user_type"].ToString() == "Superadm")
            {
                setAllMenuPermission(true);
            }

            if (CUser.Current.IsEntryGRMRole)
            {
                LiIp6.Visible = true;
                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                grmReport.Visible = true;
                grmImporting.Visible = true;
            }
            if (CUser.Current.IsManagerGRMRole)
            {
                LiIp6.Visible = true;
                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                grmReport.Visible = true;
                grmImporting.Visible = true;
                //grmImportingELRP.Visible = true;
            }


            if (Session["user_type"].ToString() == "IP")
            {
                if (Session["ip_approver"]?.ToString() == "1")
                {
                    //setAllMenuPermission(false);
                    LiIp5.Visible = true;
                    WageRequestDraft.Visible = true;
                    LiIp8.Visible = true;
                    LiIp9.Visible = true;
                }
                else
                {
                    LiIp1.Visible = true;
                    IPDashboard.Visible = true;
                    IPInvoiceDashboard.Visible = true;
                    APIDashboard.Visible = true;

                    LiIp2.Visible = true;

                    LiIp3.Visible = true;
                    workProposalMenu.Visible = true;
                    beneficiaryRegistrationMenu.Visible = true;
                    jobCardMenu.Visible = true;
                    printIdCardMenu.Visible = true;
                    workProgressMenu.Visible = true;
                    lipwExportMenu.Visible = true;
                    BeneficiaryListMenu.Visible = true;

                    LiIp4.Visible = true;

                    LiIp5.Visible = true;
                    WageRequestApplyLIPW.Visible = true;
                    WageRequestApplyDIS.Visible = true;
                    RejectedWages.Visible = true;

                    LiIp6.Visible = true;
                    nongbvMenu.Visible = true;
                    nongbvDashboardMenu.Visible = true;
                    gbvNewMenu.Visible = true;
                    gbvNewMenuDashboard.Visible = true;
                    grmReport.Visible = true;
                    grmImporting.Visible = true;

                    LiIp7.Visible = true;

                    LiIp8.Visible = true;

                    LiIp9.Visible = true;

                    LiIp10.Visible = true;

                    LiIp11.Visible = true;

                    LiPayroll.Visible = true;
                    payrollDownload.Visible = true;
                    payrollReconcile.Visible = true;

                    LiMNE.Visible = true;
                    EconomicOpportunities.Visible = true;
                    EconomicOpportunitiesDashboard.Visible = true;
                    Component2TemplateImporting.Visible = true;
                    HouseholdGroupView.Visible = true;
                    HouseholdAndGroupIdMapping.Visible = true;
                    BeneficiaryRandomizationandEnrollment.Visible = true;
                    TrainingandAttendanceTracking.Visible = true;
                    BusinessPlanDevelopment.Visible = true;
                    LivelihoodGrantDisbursement.Visible = true;
                    FinancialServiceLinkageTracking.Visible = true;
                    CoachingSessionTracking.Visible = true;
                    AuditRecordDownload.Visible = true;
                    //LiUM.Visible = true;

                    nongbvMenu.Visible = true;
                    nongbvDashboardMenu.Visible = true;
                    gbvNewMenu.Visible = true;
                    gbvNewMenuDashboard.Visible = true;
                    grmReport.Visible = true;
                    grmImporting.Visible = true;

                    LiPaymentCycleDocument.Visible = true;
                    liUploadPaymentCycleDoc.Visible = false;
                    liViewPaymentCycleDoc.Visible = true;
                }
                
            }
            if (Session["user_type"].ToString() == "FSP")
            {
                LiIp3.Visible = true;
                printIdCardMenu.Visible = true;
                Li1.Visible = false;
                LiPayroll.Visible = true;
                payrollDownload.Visible = true;
                payrollReconcile.Visible = true;

                LiPaymentCycleDocument.Visible = true;
                liUploadPaymentCycleDoc.Visible = false;
                liViewPaymentCycleDoc.Visible = true;
            }
            if (Session["user_type"].ToString() == "MIS")
            {
                LiIp1.Visible = true;
                IPDashboard.Visible = true;
                IPInvoiceDashboard.Visible = true;
                APIDashboard.Visible = true;

                LiIp2.Visible = true;

                LiIp3.Visible = true;
                workProposalMenu.Visible = true;
                beneficiaryRegistrationMenu.Visible = true;
                jobCardMenu.Visible = true;
                printIdCardMenu.Visible = true;
                workProgressMenu.Visible = true;
                lipwExportMenu.Visible = true;
                BeneficiaryListMenu.Visible = true;

                LiIp4.Visible = true;
                LiIp5.Visible = false; // Apply Wage Request menus hidden for MIS user_type


                LiIp6.Visible = true;
                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                grmReport.Visible = true;
                grmImporting.Visible = true;

                LiIp7.Visible = true;
                LiIp8.Visible = true;
                LiIp9.Visible = true;
                LiIp10.Visible = true;
                LiIp11.Visible = true;
                LiPayroll.Visible = true;
                payrollDownload.Visible = true;
                payrollReconcile.Visible = true;
                LiMNE.Visible = true;
                LiUM.Visible = true;
                EconomicOpportunities.Visible = true;
                EconomicOpportunitiesDashboard.Visible = true;
                Component2TemplateImporting.Visible = true;
                HouseholdGroupView.Visible = true;
                HouseholdAndGroupIdMapping.Visible = true;
                BeneficiaryRandomizationandEnrollment.Visible = true;
                TrainingandAttendanceTracking.Visible = true;
                BusinessPlanDevelopment.Visible = true;
                LivelihoodGrantDisbursement.Visible = true;
                FinancialServiceLinkageTracking.Visible = true;
                CoachingSessionTracking.Visible = true;
                AuditRecordDownload.Visible = true;

                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                grmReport.Visible = true;
                grmImporting.Visible = true;
            }
            if (Session["user_type"].ToString() == "Report")
            {
                LiIp8.Visible = true;
                LiIp9.Visible = true;
                Li1.Visible = false;

            }
            if (Session["user_type"].ToString() == "TST")
            {
                ////  LiTST1.Visible = true;
                ////  LiTST2.Visible = true;
                ////  LiTST3.Visible = true;
                ////  LiTST4.Visible = true;
                //////  LiTST5.Visible = true;
                ////  LiTST6.Visible = true;
                ////  LiTST7.Visible = true;
                ////  LiTST8.Visible = true;
                if (Session["Desgid"].ToString() == "1000")
                {
                    LiTST1.Visible = true;
                    LiTST2.Visible = true;
                    LiTST3.Visible = true;
                    LiTST4.Visible = true;
                    LiTST5.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    LiTST8.Visible = true;
                }
                if (Session["Desgid"].ToString() == "1001" || Session["Desgid"].ToString() == "1004")
                {
                    //  LiTST1.Visible = true;
                    LiTST6.Visible = true;
                    LiTST7.Visible = true;
                    LiTST5.Visible = true;
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
                    LiTST5.Visible = true;
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
                        LiTST5.Visible = true;
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
                        LiTST5.Visible = true;
                        LiTST7.Visible = true;
                    }
                    else
                    {
                        LiTST1.Visible = true;
                        LiTST6.Visible = true;
                        LiTST5.Visible = true;
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
                    LiTST5.Visible = true;
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
                    LiTST5.Visible = true;
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

                LiIp6.Visible = true;
                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                grmReport.Visible = false;
                grmImporting.Visible = false;               
            }

            if (Session["user_type"].ToString() == "Adm")
            {
                Masters1.Visible = true;
                //   Masters2.Visible = true;
                LiTST6.Visible = true;
                LiTST7.Visible = true;
                LiTST8.Visible = true;
                LiUM.Visible = true;
            }
            if (Session["user_type"].ToString() == ConfigurationManager.AppSettings["PaymentCycleDocumentUploadRole"].ToString())
            {
                LiPaymentCycleDocument.Visible = true;
                liUploadPaymentCycleDoc.Visible = true;
                liViewPaymentCycleDoc.Visible = true;
            }
            
            if (Session["user_type"].ToString() == "PC")
            {
                // Masters1.Visible = true;
                //  Masters2.Visible = true;
                PayConnect2.Visible = true;
                PayConnect1.Visible = true;
                // LiTST6.Visible = true;
                // LiTST7.Visible = true;
                // LiTST8.Visible = true;
            }
            if (Session["user_type"].ToString() == "ALP")
            {
                // Masters1.Visible = true;
                //  Masters2.Visible = true;
                LiAlpha1.Visible = true;
                LiAlpha2.Visible = true;
                // LiTST6.Visible = true;
                // LiTST7.Visible = true;
                // LiTST8.Visible = true;
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
            if (Session["user_type"].ToString() == "BDC")
            {
                BDC.Visible = true;
            }
            if (Session["user_type"].ToString() == "PDC")
            {
                PDC.Visible = true;
            }

            if (Session["user_type"].ToString() == "GenderSpec")
            {
                LiIp6.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                Li1.Visible = false;
            }
            if (Session["user_type"].ToString() == "SocDevSpec")
            {
                LiIp6.Visible = true;
                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                Li1.Visible = false;
            }
            if (Session["user_type"].ToString() == "PcuSpec" || Session["user_type"].ToString() == "WBGBVMgr")
            {
                LiIp6.Visible = true;
                nongbvMenu.Visible = true;
                nongbvDashboardMenu.Visible = true;
                gbvNewMenu.Visible = true;
                gbvNewMenuDashboard.Visible = true;
                grmReport.Visible = true;
                Li1.Visible = false;
            }

            #region old code
            //////    LiTST.Visible = false;
            //////    lblwecome.Text = Session["username"].ToString();
            //////    pnlreport.Visible = false;
            //////    if (Session["user_type"].ToString() == "Adm")
            //////    {
            //////        TST.Visible = false;
            //////        Masters.Visible = true;
            //////        IP.Visible = false;
            //////        APIDashboard.Visible = false;

            //////        //lblwecome.Text = "Admin";
            //////    }
            //////    else if (Session["user_type"].ToString() == "TST" || Session["user_type"].ToString() == "Adm")
            //////    {
            //////        Masters.Visible = false;
            //////        TST.Visible = true;
            //////        IP.Visible = false;
            //////        TSTDashboard.Visible = true;
            //////        //lblwecome.Text = "Technical Support Team";
            //////        APIDashboard.Visible = false;
            //////        LiTST.Visible = true;

            //////    }
            //////    else if (Session["user_type"].ToString() == "IP")
            //////    {
            //////        Masters.Visible = false;
            //////        TST.Visible = false;
            //////        IP.Visible = true;
            //////        IPDashboard.Visible = true;
            //////        IPInvoiceDashboard.Visible = true;
            //////       // ipDeleting.Visible = true;
            //////        //lblwecome.Text = "Implementing Partner";
            //////        APIDashboard.Visible = true;

            //////    }
            //////    else if (Session["user_type"].ToString() == "BDC")
            //////    {
            //////        BDC.Visible = true;
            //////        TST.Visible = false;
            //////        //Master.Visible = false;
            //////        IP.Visible = false;
            //////        PDC.Visible = false;
            //////        //lblwecome.Text = "Implementing Partner";
            //////        APIDashboard.Visible = false;

            //////    }
            //////    else if (Session["user_type"].ToString() == "PDC")
            //////    {
            //////        PDC.Visible = true;
            //////        TST.Visible = false;
            //////        //Master.Visible = false;
            //////        IP.Visible = false;
            //////        BDC.Visible = false;
            //////       //lblwecome.Text = "Implementing Partner";
            //////        APIDashboard.Visible = false;

            //////    }
            //////    else if (Session["user_type"].ToString() == "PA")
            //////    {
            //////        LiPA.Visible = true;
            //////        TST.Visible = false;
            //////        IP.Visible = false;
            //////        pnlreport.Visible = false;

            //////        WagePayment.Visible = false;
            //////        wageAtten.Visible = false;
            //////        Wagep.Visible = false;
            //////        APIDashboard.Visible = false;

            //////    }
            //////    else if (Session["user_type"].ToString() == "Cordinator" || Session["user_type"].ToString() == "Specialist" || Session["user_type"].ToString() == "Manager")
            //////    {
            //////        LiPA.Visible = true;
            //////        TST.Visible = false;
            //////        IP.Visible = false;
            //////        pnlreport.Visible = false;

            //////        WagePayment.Visible = false;
            //////        wageAtten.Visible = false;
            //////        Wagep.Visible = false;
            //////        APIDashboard.Visible = false;

            //////    }

            //////    if (Session["user_type"].ToString() == "Help" || Session["user_type"].ToString() == "Admin")
            //////    {
            //////        pnlreport.Visible = true;
            //////        APIDashboard.Visible = false;


            //////    }
            //////    else
            //////    {
            //////        pnlreport.Visible = false;
            //////    }

            //////    if (Session["user_type"].ToString() == "TST" || Session["user_type"].ToString() == "Help")
            //////    {
            //////        WagePayment.Visible = true;
            //////        wageAtten.Visible = true;
            //////        Wagep.Visible = true;

            //////    }
            //////    else
            //////    {
            //////        WagePayment.Visible = false;
            //////        wageAtten.Visible = false;
            //////        Wagep.Visible = false;


            //////    }
            //////    LiWB.Visible = false;
            //////     if (Session["user_type"].ToString() == "WB")
            //////    {
            //////        LiPA.Visible = false;
            //////        TST.Visible = false;
            //////        IP.Visible = false;
            //////        pnlreport.Visible = false;
            //////        WagePayment.Visible = false;
            //////        wageAtten.Visible = false;
            //////        Wagep.Visible = false;
            //////        LiWB.Visible = true;
            //////        APIDashboard.Visible = false;

            //////    }

            //////    if(Session["user_type"].ToString() == "IP")
            //////    {
            //////        LiDashboardIP.Visible=true;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1000" || Session["Desgid"].ToString() == "1004" || Session["Desgid"].ToString() == "1007")
            //////    {
            //////        trCo.Visible = true;
            //////        trSp.Visible = true;
            //////        trMa.Visible = true;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1001")
            //////    {
            //////        trCo.Visible = true;
            //////        trSp.Visible = true;
            //////        trMa.Visible = true;
            //////    }

            //////    if (Session["Desgid"].ToString() == "1002")
            //////    {
            //////        trCo.Visible = true;
            //////        trSp.Visible = false;
            //////        trMa.Visible = false;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1003" || Session["Desgid"].ToString() == "1005" || Session["Desgid"].ToString() == "1011" || Session["Desgid"].ToString() == "1007" || Session["Desgid"].ToString() == "1009")
            //////    {
            //////        trCo.Visible = false;
            //////        trSp.Visible = true;
            //////        trMa.Visible = false;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1006" || Session["Desgid"].ToString() == "1010")
            //////    {
            //////        trCo.Visible = false;
            //////        trSp.Visible = false;
            //////        trMa.Visible = true;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1002")
            //////    {
            //////        trCo.Visible = true;
            //////        trSp.Visible = false;
            //////        trMa.Visible = false;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1002")
            //////    {
            //////        trCo.Visible = true;
            //////        trSp.Visible = false;
            //////        trMa.Visible = false;
            //////    }
            //////    if (Session["Desgid"].ToString() == "1002")
            //////    {
            //////        trCo.Visible = true;
            //////        trSp.Visible = false;
            //////        trMa.Visible = false;
            //////    }
            //////    liFrim.Visible = false;
            //////    if (Session["user_type"].ToString() == "IP")
            //////    {
            //////        liFrim.Visible = false;
            //////    }
            //////    else
            //////    {
            //////        liFrim.Visible = true;
            //////    }
            //////    WagePayment.Visible = false;
            //////    wageAtten.Visible = false;
            //////    Wagep.Visible = false;

            #endregion

        }
        else
        {
            Response.Redirect("../../frmuserlogin.aspx");
        }

    }



}

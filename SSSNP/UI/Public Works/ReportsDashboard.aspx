<%@ Page Title=":: Public Works : MIS Reports " Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master"
    AutoEventWireup="true" Inherits="ReportsDashboard" Codebehind="ReportsDashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">




    </script>

    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    MIS Reports</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <div class="panel panel-default">
                                                <div class="panel-body" align="center">
                                                    <table cellpadding="3" cellspacing="5" style="width: 90%">
                                                        <%--<tr>
                                                            <td style="font-size: 16px; text-align: center;" colspan="3">
                                                                <asp:HyperLink ID="HyperLink1" runat="server" Font-Size="Small" NavigateUrl="~/UI/TSiPASS/Home.aspx">Back</asp:HyperLink>
                                                            </td>
                                                        </tr>--%>
                                                        <tr>
                                                            <td style="width: 395px; font-size: 18px;">
                                                                <b>Public works Component Progress Reports</b>
                                                            </td>
                                                            <td style="width: 30px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 395px" valign="top">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 395px; font-size: 16px;">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 30px">
                                                                &nbsp;
                                                            </td>
                                                            <td style="width: 395px" valign="top">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 395px" valign="top">
                                                                <div class="list-group" style="font-weight: bold">
                                                                    <%--<a href="#" class="list-group-item">
                                        
                                        <i class="fa fa-fw fa-check"></i> <b>Incentives Applications</b>
                                    </a>--%>
                                                                    <a href="Dashboard1.aspx" class="list-group-item"><i class="fa fa-fw fa-user"></i>R1:
                                                                        Dashboard </a><a href="IPwiseStatus.aspx" class="list-group-item"><i class="fa fa-fw fa-user">
                                                                        </i>R2: IP Wise Report </a><a href="WorkwiseStatusreportNew.aspx" class="list-group-item">
                                                                            <i class="fa fa-fw fa-user"></i>R3: IP Work Wise Report</a> <a class="list-group-item"
                                                                                href="CountyWiseReportNewLatest.aspx"><i class="fa fa-fw fa-user"></i>R4: County Wise
                                                                                Report </a>
                                                                                 <a href="TypesOfWorksrReport.aspx" class="list-group-item">
                                                                        <i class="fa fa-fw fa-user"></i>R5: Types Of Works Report </a>
                                                                                <%--<a href="PayamWiseReportNew.aspx" class="list-group-item"><i class="fa fa-fw fa-user">
                                                                                </i>R5: Payam Wise Report</a>--%>
                                                                </div>
                                                            </td>
                                                            <td style="width: 30px">
                                                                &nbsp;
                                                            </td>
                                                            <td valign="top" style="width: 395px">
                                                                <div class="list-group" style="font-weight: bold">
                                                                    <%--<a href="BomaWiseReportNew.aspx" class="list-group-item"><i class="fa fa-fw fa-user">
                                                                    </i>R6: Boma Wise Report </a>--%>
                                                                   <a href="FundDisbursedReports.aspx"
                                                                            class="list-group-item"><i class="fa fa-fw fa-user"></i>R6: Fund Disbursed Reports</a>
                                                                    <a class="list-group-item" href="WagePaymentReport.aspx"><i class="fa fa-fw fa-user">
                                                                    </i>R7: Wage Payment Report</a> <a href="FundDisbursedReportsByTrans.aspx" class="list-group-item">
                                                                        <i class="fa fa-fw fa-user"></i>R8: Ip wise Fund Disbursed</a><a href="WagePaymentGraphReport.aspx"  target="_blank" class="list-group-item">
                                                                        <i class="fa fa-fw fa-user"></i>R9: Graphs</a>&nbsp; &nbsp;
                                                                    <%--   <a class="list-group-item" href="#"><b><i class="fa fa-fw fa-check"></i>Approval Stages</b></a>
                                              
                                              
                                                  <a class="list-group-item" href="#"><b><i class="fa fa-fw fa-check"></i>Approval Under Process</b></a>
                                              
                                                <a class="list-group-item" href="frmCFEDepartmentsApprovalProcess.aspx?stg=1"><span class="badge" style=" background-color:#87D37C;">
                                                <asp:Label ID="Label1" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> Within Time Limits </a>
                                                
                                                 <a class="list-group-item" href="frmCFEDepartmentsApprovalProcess.aspx?stg=2"><span class="badge" style=" background-color:#d9534f;">
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> Beyond Time Limits</a>
                                                
                                                   <a class="list-group-item" href="#"><span class="badge">
                                                <asp:Label ID="Label5" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> Total</a>
                                                
                                                 
                                                
                                               <a class="list-group-item" href="#"><i class="fa fa-fw fa-check"></i><b> Approval Issued</b></a>
                                               
                                             
                                                
                                                <a class="list-group-item" href="frmCFEDepartmentsViewAppl.aspx?Stg=13"><span class="badge" style=" background-color:#87D37C;">
                                                <asp:Label ID="Label7" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> Within Time Limits </a>
                                                
                                                 <a class="list-group-item" href="frmCFEDepartmentsViewAppl.aspx?Stg=14"><span class="badge" style=" background-color:#d9534f;">
                                                <asp:Label ID="Label9" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> Beyond Time Limits</a>
                                                
                                                   <a class="list-group-item" href="frmCFEDepartmentsViewAppl.aspx?Stg=15"><span class="badge">
                                                <asp:Label ID="Label10" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> Total</a>
                                               
                                                <a class="list-group-item" href="frmCFEDepartmentsViewAppl.aspx?Stg=16"><span class="badge">
                                                <asp:Label ID="Label17" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> No of Applications Rejected </a>
                                                
                                                 <a class="list-group-item" href="frmCFEDepartmentsViewAppl.aspx?Stg=17"><span class="badge">
                                                <asp:Label ID="Label13" runat="server"></asp:Label>
                                                </span><i class="fa fa-fw fa-user"></i> No of Applications appeal against Rejection </a>--%>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                    &nbsp;
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <br />
            <br />
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

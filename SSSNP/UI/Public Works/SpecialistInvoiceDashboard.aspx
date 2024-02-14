<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/TstMaster.master" AutoEventWireup="true" Inherits="SpecialistInvoiceDashboard" Codebehind="SpecialistInvoiceDashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
    </script>


    <asp:UpdatePanel ID="updatepanel1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Specialist: Invoice Dashboard</a>
                            </li>


                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Specialist: Invoice Dashboard</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">

                                            <div class="panel panel-default">
                                                <div class="panel-heading" align="center">
                                                  
                                                </div>
                                                <div class="panel-body" align="center">

                                                    <table cellpadding="3" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 395px">
                                                                <div class="list-group">
                                                                    <a href="SpecialistInvoices.aspx?q=Submitted" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl0" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-calendar"></i>No of Invoices submittted by Coordinator for Verify
                                                                    </a>

                                                                    <a href="SpecialistInvoices.aspx?q=AllSubmitted" class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="lbl1"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-truck"></i>No of invoices pending for Verify
                                                                    </a>
                                                                   
                                                                    

                                                                    



                                                                </div>
                                                            </td>
                                                            <td style="width: 30px">&nbsp;</td>
                                                            <td valign="top">
                                                                <div class="list-group">


                                                                    <a href="SpecialistInvoices.aspx?q=Reviewed" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl7" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>No of Invoices Verified
                                                                    </a>
                                                                    
                                                                    <a href="SpecialistInvoices.aspx?q=Rejected" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl9" runat="server"></asp:Label>


                                                                        </span>
                                                                        <i class="fa fa-fw fa-check"></i>No  of Invoices Rejected
                                                                    </a>
                                                                   
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="width: 395px">&nbsp;</td>
                                                            <td style="width: 30px">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>

                                                    
                                                    &nbsp;
                                <div class="text-right">
                                    <a href="#">View All Activity <i class="fa fa-arrow-circle-right"></i></a>
                                </div>
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


<%@ Page Title=":: :: SNSOP : ::   : Dashboard " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WageProcessDashboardCO" Codebehind="WageProcessDashboardCO.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">




    </script>
    

   <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>--%>
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="#">Paying Agent Dashboard</a>
                            </li>
                            
                           
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <%--
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Dashboard</h3>
                            </div>--%>
                            
                            <div class="col-lg-12">
                        <div class="panel panel-warning">
                                <div class="panel-heading">
                                <h3 class="panel-title"> <asp:Label ID="lblOfficer" runat="server" Text="Label"></asp:Label>&nbsp; Wages Dashboard 
                                   </h3>
                            </div>

                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            
                                            <div class="panel panel-default">
                            <div class="panel-body" align="center">
                            
                                <table cellpadding="3" cellspacing="5" style="width: 100%">
                                                                       
                                    <tr>
                                        <td style="width: 395px; font-size: 16px;">
                                            <b> <asp:Label ID="lblOfficer0" runat="server" Text="Wage Payment Process"></asp:Label></b></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="width: 395px" valign="top">
                                             <div class="list-group">
                                             
                                             <a href="WagesDasboardDrildown.aspx?Status=1" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label1" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Total Wage Requests
                                    </a>
                                    
                                    
                                             <a href="WagesDasboardDrildown.aspx?Status=2" target="_blank" runat="server" id="lnkOther" class="list-group-item">
                                        <span class="badge" style=" background-color:#d9534f;">
                                            <asp:Label ID="Label26" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Pending Wage Requests
                                    </a>
                                    
                                    
                                             <a href="WagesDasboardDrildown.aspx?Status=2" target="_blank" runat="server" id="lnkPA1" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label9" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i> Finalization of Beneficiaries for Payment
                                    </a>
                                    
                                    
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=6" target="_blank" runat="server" id="lnkPA" class="list-group-item">
                                        <span class="badge" style=" background-color:#d9534f;">
                                            <asp:Label ID="Label10" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i> Wage Payment
                                    </a>
                                    
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                            <div class="list-group">
                                             
                                            
                                     
                                    
                                    
                                    
                                  
                                       <a href="WagesDasboardDrildown.aspx?Status=3" target="_blank" class="list-group-item" runat="server" id="lnkOtherD">
                                        <span class="badge" style=" background-color:#87D37C;">
                                            <asp:Label ID="Label41" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Approved Wage Requests
                                    </a>
                                       <a href="WagesDasboardDrildown.aspx?Status=3" target="_blank" class="list-group-item" runat="server" id="lnkPA6">
                                        <span class="badge" style=" background-color:#87D37C;">
                                            <asp:Label ID="Label13" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Paid Wage Requests
                                    </a>
                                    
                                    
                                      <a href="WagesDasboardDrildown.aspx?Status=4" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label8" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Rejected Wage Requests
                                    </a>
                                      
                                    
                                    </div>
                                    </td>
                                    </tr>
                                    
                                     <tr>
                                        <td style="width: 395px; font-size: 16px;">
                                            <b></b></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                   
                                     <tr>
                                        <td style="font-size: 16px; text-align: left;" align="left">
                                            <b> <asp:Label ID="lblOfficer1" runat="server" 
                                                Text="World Bank Confirmation Process"></asp:Label></b></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    
                                       <tr>
                                        <td style="width: 395px" valign="top">
                                             <div class="list-group">
                                             
                                             <a href="WagesPaymentDasboardDrildown.aspx?Status=5" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label2" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Total Wage Invoices Paid
                                    </a>
                                    
                                    
                                             <a href="WagesPaymentDasboardDrildown.aspx?Status=6" runat="server" id="lnkInvoiceCofirmationPending" target="_blank" class="list-group-item">
                                        <span class="badge" style=" background-color:#d9534f;">
                                            <asp:Label ID="Label3" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Pending Invoice Paid Confirmations
                                    </a>
                                    
                                    
                                    
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                            <div class="list-group">
                                             
                                             <a href="WagesPaymentDasboardDrildown.aspx?Status=7" target="_blank" class="list-group-item" runat="server" id="lnlOtherA">
                                        <span class="badge" style=" background-color:#87D37C;">
                                            <asp:Label ID="Label11" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Approved Invoice Confirmations
                                    </a>
                                     
                                    
                                    
                                    
                                  
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=7" target="_blank" class="list-group-item" runat="server" id="lnlPA4">
                                        <span class="badge" style=" background-color:#87D37C;">
                                            <asp:Label ID="Label4" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>World Bank Cleared Invoices
                                    </a>
                                    
                                    
                                      <a href="WagesPaymentDasboardDrildown.aspx?Status=8" target="_blank" class="list-group-item" runat="server" id="lnlOtherB">
                                        <span class="badge">
                                            <asp:Label ID="Label5" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Rejected Invoice Confirmations
                                    </a>
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=8" target="_blank" class="list-group-item" runat="server" id="lnlPA5">
                                        <span class="badge">
                                            <asp:Label ID="Label12" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>World Bank not Cleared Invoices
                                    </a>
                                    
                                    </div>
                                    </td>
                                    </tr>
                                    
                                    <tr runat="server" id="trPA">
                                        <td style="font-size: 16px; text-align: left;" align="left">
                                            <b> <asp:Label ID="Label6" runat="server" 
                                                Text="Wage Payment process for unselected beneficiaries"></asp:Label></b></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top" runat="server" id="trPA2">
                                            <b> 
                                            <asp:Label ID="Label42" runat="server" 
                                                Text=""></asp:Label></b></td>
                                    </tr>
                                    
                                       <tr runat="server" id="trPA1">
                                        <td style="width: 395px" valign="top">
                                             <div class="list-group">
                                             
                                             <a href="WagesDasboardDrildown.aspx?Status=50" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label7" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Wage Requests pending for unselected beneficiaries
                                    </a>
                                    
                                    
                                           
                                    
                                    
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                          <div class="list-group">
                                             
                                    
                                           
                                    
                                    
                                       
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
              
</asp:Content>


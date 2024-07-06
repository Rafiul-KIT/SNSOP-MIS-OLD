<%@ Page Title=":: :: SSSNP : ::   : Dashboard " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WageIPProcessDashboard" Codebehind="WageIPProcessDashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">




    </script>
    

   <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>--%>
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="#">IP Dashboard</a>
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
                                <h3 class="panel-title"> <asp:Label ID="lblOfficer" runat="server"></asp:Label>&nbsp; 
                                    IP Wages Dashboard 
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
                                            <b> <asp:Label ID="lblOfficer0" runat="server" Text="Wage Request Process"></asp:Label></b></td>
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
                                        <i class="fa fa-fw fa-calendar"></i>1. Total Wage Requests Sent
                                    </a>
                                    
                                    
                                             <a href="WagesDasboardDrildown.aspx?Status=2" target="_blank" class="list-group-item">
                                        <span class="badge" >
                                            <asp:Label ID="Label26" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>2. Wage Requests at Initial Reviewer
                                    </a>
                                    
                                      <a href="WagesDasboardDrildown.aspx?Status=3" target="_blank" class="list-group-item">
                                        <span class="badge" >
                                            <asp:Label ID="Label41" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>3. Wage Requests at Financial Approver
                                    </a>
                                    
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                            <div class="list-group">
                                             
                                            
                                     
                                    
                                    
                                    
                                  
                                     
                                    
                                    
                                      <a href="WagesDasboardDrildown.aspx?Status=4" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label8" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>4. Wage Requests at Final Payment Authorizer
                                    </a>
                                        <a href="WagesDasboardDrildown.aspx?Status=5" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label6" runat="server" Text="0"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>5. Wage Requests Rejected
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
                                                Text="Wage Payment Confirmation Process"></asp:Label></b></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                    
                                       <tr>
                                        <td style="width: 395px" valign="top">
                                             <div class="list-group">
                                             
                                             <a href="WagesDasboardDrildown.aspx?Status=6" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label2" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>1. Wage Reguest at Paying Agent for Invoice
                                    </a>
                                    
                                    
                                             <a href="WagesPaymentDasboardDrildown.aspx?Status=7" target="_blank" class="list-group-item">
                                        <span class="badge" >
                                            <asp:Label ID="Label3" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>2. Wage Reguest at Co-Orindator for Invoice
                                    </a>
                                    
                                    
                                      <a href="WagesPaymentDasboardDrildown.aspx?Status=8" target="_blank" class="list-group-item">
                                        <span class="badge" >
                                            <asp:Label ID="Label4" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>3. Wage Reguest at Specialist for Invoice
                                    </a>
                                    
                                      <a href="WagesPaymentDasboardDrildown.aspx?Status=9" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label5" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>4. Wage Reguest at Manager for Invoice
                                    </a>
                                    
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                            <div class="list-group">
                                             
                                            
                                     
                                    
                                    
                                    
                                  
                                     
                                    
                                    
                                    
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=10" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label7" runat="server" Text="0"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>5. Wage Reguest at Paying Agent for Pay
                                    </a>
                                    
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=11" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label10" runat="server" Text="0"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>6. Wage Reguest at Paying Agent Paid
                                    </a>
                                    
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=12" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label9" runat="server" Text="0"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>7. Wage Reguest Invoice Rejected
                                    </a>
                                      
                                    
                                    </div>
                                    </td>
                                    </tr>
                                     <tr>
                                        <td style="width: 395px; font-size: 16px;">
                                            <b> <asp:Label ID="Label11" runat="server" Text="Wage Request need to update"></asp:Label></b></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                   
                                    <tr>
                                        <td style="width: 395px" valign="top">
                                             <div class="list-group">
                                             
                                             <a href="IPWagesDasboardDrildownUpdate.aspx?Status=30" target="_blank" class="list-group-item">
                                        <span class="badge" style="background-color: #CC0000">
                                            <asp:Label ID="Label12" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>1. Wage Requests Rejected at Co-Ordinator
                                    </a>
                                    
                                    
                                         
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                            <div class="list-group" visible="false" runat="server">
                                             
                                            
                                     
                                    
                                    
                                    
                                  
                                     
                                    
                                    
                                      <a href="WagesDasboardDrildownUpdate.aspx?Status=2" target="_blank" class="list-group-item">
                                        <span class="badge" style="background-color: #CC0000"  >
                                            <asp:Label ID="Label15" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>2. Invoice Requests Rejected at Co-Ordinator
                                    </a>
                                     
                                    
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


<%@ Page Title=":: :: SNSOP : ::   : Dashboard " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="WBWageProcessDashboard" Codebehind="WBWageProcessDashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">




    </script>
    

   <%-- <asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>--%>
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="#">World Bank Wage Dashboard</a>
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
                                            &nbsp;</td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td style="width: 395px" valign="top">
                                            &nbsp;</td>
                                    </tr>
                                   
                                     <tr>
                                        <td style="font-size: 16px; text-align: left;" align="left">
                                            &nbsp;</td>
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
                                        <i class="fa fa-fw fa-calendar"></i>Total Invoice Confirmations
                                    </a>
                                    
                                    
                                             <a href="WagesPaymentDasboardDrildown.aspx?Status=6" runat="server" id="lnkInvoiceCofirmationPending" target="_blank" class="list-group-item">
                                        <span class="badge" style=" background-color:#d9534f;">
                                            <asp:Label ID="Label3" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Pending Invoice Confirmations
                                    </a>
                                    
                                    
                                    
                                       
                                    </div></td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td valign="top" style="width: 395px">
                                        
                                            <div class="list-group">
                                             
                                            
                                     
                                    
                                    
                                    
                                  
                                       <a href="WagesPaymentDasboardDrildown.aspx?Status=7" target="_blank" class="list-group-item">
                                        <span class="badge" style=" background-color:#87D37C;">
                                            <asp:Label ID="Label4" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Cleared Invoice Confirmations
                                    </a>
                                    
                                    
                                      <a href="WagesPaymentDasboardDrildown.aspx?Status=8" target="_blank" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="Label5" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i>Not Cleared Invoice Confirmations
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


<%@ Page Title=":: :: SSSNP : : Works wise Report " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="IPDashboardForUSD" Codebehind="IPDashboardForUSD.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">




    </script>
    

    <asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>

    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Dashboard</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr >
                                        <td style="padding: 5px; margin: 5px">
                                            
                                            <div class="panel panel-default">
                            <%--<div class="panel-heading" align="center">
                                <table cellpadding="4" cellspacing="5" style="width: 26%" align="center">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="30px">Year</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="width: 152px;">
                                                <asp:DropDownList ID="ddltypeip" runat="server" class="form-control txtbox" 
                                                            Height="28px" Width="180px">
                                                            
                                                            
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                
                                                </table>
                            </div>--%>
                            <div class="panel-body" align="center">
                            
                                <table cellpadding="3" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="width: 395px">
                                            <div class="list-group">
                                    <a href='DrillDownIPname1.aspx?UID=<%= Session["User_Code"]%>&status=1' class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="lbl0" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i> Name of Implementing Partner
                                    </a>
                                   
                                    <a href='DrillDownCountriesDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=2' class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl1" 
                                                    runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-truck"></i> No. of Implementing Counties
                                    </a>
                                    <a href='DrillDownPayamsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=3' class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl2" 
                                                    runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-money"></i> No. of Implementing Payams
                                    </a>
                                    <a href='DrillDownBomasDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=4' class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl3" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> Number of Implementing Bomas
                                    </a>
                                    <a href='DrillDownFundDistributedReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=5' class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl4" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> Total no. of works proposed
                                    </a>
                                    <a href="DrillDownFundDistributedReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=6" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl5" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> No. of works to be approved
                                    </a>
                                    
                                    <a href="DrillDownFundDistributedReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=7" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl6" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> No. of works in progress
                                    </a>
                                    
                                    <a href="DrillDownFundDistributedReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=99" class="list-group-item">
                                        <span class="badge"><asp:Label ID="Label1" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> No. of works in Rejected
                                    </a>
                                    
                                    
                                    
                                </div></td>
                                        <td style="width: 80px">
                                            &nbsp;</td>
                                        <td valign="top">
                                            <div class="list-group">
                                    
                                    
                                    <a href="DrillDownFundDistributedReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=8" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl7" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> No. of works completed
                                    </a>
                                    <a href="#"  class="list-group-item" visible="false">
                                        <span class="badge">
                                        <asp:Label ID="lbl8" runat="server"></asp:Label>
                                            
                                        
                                        </span>
                                        <i class="fa fa-fw fa-globe"></i> Funds disbursed(In SSP) 
                                    </a>
                                    <a href="DrillDownBenificiaryWagespaymentsReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=10" class="list-group-item">
                                        <span class="badge">
                                        <asp:Label ID="lbl9" runat="server"></asp:Label>
                                        
                                        
                                        </span>
                                        <i class="fa fa-fw fa-user"></i> Direct Beneficiaries
                                    </a>
                                    <a href="DrillDownBenificiaryWagespaymentsReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=12" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl10" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-user"></i> FeMale beneficiaries
                                    </a>
                                    <a href="DrillDownBenificiaryWagespaymentsReportsDashboard1.aspx?UID=<%= Session["User_Code"]%>&status=11" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl11" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-user"></i> Male beneficiaries
                                    </a>
                                    <a href="#" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl12" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-user"></i> No. of work days created
                                    </a>
                                    <a href="#" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lbl13" runat="server"></asp:Label></span>
                                        <i class="fa fa-fw fa-user"></i> Total wages paid(In SSP)
                                    </a>
                                    
                                    
                                </div></td>
                                        <td width="100px">
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                                            
                                            </td>
                                    </tr>
                                </table>
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


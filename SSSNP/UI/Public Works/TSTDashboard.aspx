<%@ Page Title=":: :: SNSOP : : Works wise Report " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="TSTDashboard" Codebehind="TSTDashboard.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">




    </script>
    

    <asp:UpdatePanel ID="updatepanel1" runat="server">
<ContentTemplate>
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            
                           
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Dashboard</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            
                                            <div class="panel panel-default">
                            <div class="panel-heading">
                                <h3 class="panel-title"><i class="fa fa-clock-o fa-fw"></i> Tasks Panel</h3>
                            </div>
                            <div class="panel-body">
                            
                                <table cellpadding="3" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="width: 395px">
                                            <div class="list-group">
                                    <a href="WorkProposalList.aspx?status=%" class="list-group-item">
                                        <span class="badge">
                                            <asp:Label ID="lblworks" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-calendar"></i> Total Works
                                    </a>
                                   
                                    <a href="WorkProposalList.aspx?status=New Proposal" class="list-group-item">
                                        <span class="badge" style=" background-color:#d9534f;"><asp:Label ID="lbltobeApproved" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-truck"></i> Works to be Approved
                                    </a>
                                    <a href="WorkProposalList.aspx?status=Fund Allocation" class="list-group-item">
                                        <span class="badge" style=" background-color:#d9534f;"><asp:Label ID="lbltobeFinApproved" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-money"></i> Works to be Financial Approved
                                    </a>
                                    <a href="WorkProposalList.aspx?status=Fund Released" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lblFundReleased" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> Funds Released
                                    </a>
                                    <a href="WorkProposalList.aspx?status=InProgress" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lblWorkInProgress" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> Works InProgress
                                    </a>
                                    <a href="WorkProposalList.aspx?status=Closed" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lblworkCompleted" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-check"></i> Works Completed
                                    </a>
                                    
                                </div></td>
                                        <td style="width: 30px">
                                            <%--&nbsp;</td>--%>
                                        <td valign="top">
                                            <div class="list-group">
                                    
                                    <%--<a href="TSTIPDetails.aspx"  class="list-group-item">--%>
                                        <span class="badge">
                                        <asp:Label ID="lblIps" runat="server" Text=""></asp:Label>
                                            
                                        
                                        </span>
                                        <i class="fa fa-fw fa-globe"></i> Implementation Partners
                                    </a>
                                    <a href="rptCA.aspx" class="list-group-item">
                                        <span class="badge">
                                        <asp:Label ID="lblCA" runat="server" Text=""></asp:Label>
                                        
                                        
                                        </span>
                                        <i class="fa fa-fw fa-user"></i> County Administrations
                                    </a>
                                    <a href="RptPayams.aspx" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lblPDC" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-user"></i> Payam / Block Development Committees
                                    </a>
                                    <a href="RptBDC.aspx" class="list-group-item">
                                        <span class="badge"><asp:Label ID="lblBDC" runat="server" Text=""></asp:Label></span>
                                        <i class="fa fa-fw fa-user"></i> Boma/Quarter Council  Development Committee
                                    </a>
                                </div></td>
                                    </tr>
                                    <tr>
                                        <td style="width: 395px">
                                            &nbsp;</td>
                                        <td style="width: 30px">
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;</td>
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
                                    <tr>
                                        <td align="center" style="padding: 5px; margin: 5px">
                                            <table cellpadding="2" style="width: 100%">
                                                <tr>
                                                    <td style="width: 417px">
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" align="center">
                                            <asp:GridView ID="grdDetails" runat="server" 
                                                AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" 
                                                ForeColor="#333333" Height="62px" PageSize="15" Width="100%" 
                                                Visible="False">
                                                <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                                <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                                    verticalalign="Middle" />
                                                <columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No" 
                                                                Visible="True">
                                                                    <ItemTemplate >
                                                                        <%# Container.DataItemIndex +1 %>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="50px" />
                                                                </asp:TemplateField>
                                                    <asp:BoundField DataField="IPName" HeaderText="IP Name" />
                                                    <asp:BoundField DataField="TotalWorkProposed" HeaderText="Total Works Proposed" />
                                                    <asp:BoundField DataField="TotalApproved" HeaderText="Total Approved" />
                                                    <asp:BoundField DataField="TotalInProgress" HeaderText="Total InProgress" />
                                                    <asp:BoundField DataField="TotalCompleted" HeaderText="Total Completed" />
                                                    
                                                </columns>
                                                <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                                <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                                <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                                    forecolor="White" />
                                                <editrowstyle backcolor="#B9D684" />
                                                <alternatingrowstyle backcolor="White" />
                                            </asp:GridView>
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


<%@ Page Title=":: :: SNSOP : : Works wise Report " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="True" Inherits="Dashboard" Codebehind="Dashboard.aspx.cs" %>

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
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>


                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Dashboard</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">

                                            <div class="panel panel-default">
                                                <div class="panel-heading" align="center">
                                                  <asp:HiddenField ID="hasflash" runat="server" />
                                                    <table cellpadding="4" cellspacing="5" style="width: 41%">
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="131px">Year</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="ddltypeip" runat="server" class="form-control txtbox"
                                                                    Height="28px" Width="180px" OnSelectedIndexChanged="ddltypeip_SelectedIndexChanged" AutoPostBack="true">
                                                                    <asp:ListItem Text="2016" Value="2016"></asp:ListItem>
                                                                    <asp:ListItem Text="2017" Value="2017"></asp:ListItem>
                                                                    <asp:ListItem Text="2018" Value="2018"></asp:ListItem>
                                                                      <asp:ListItem Text="2019" Value="2019"></asp:ListItem><asp:ListItem Text="2020" Value="2020"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        </tr>

                                                    </table>
                                                </div>
                                                <div class="panel-body" align="center">

                                                    <table cellpadding="3" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 395px">
                                                                <div class="list-group">
                                                                     <a href='DrillDownIPname.aspx?Year=<%=hasflash.Value %>&status=1' target="_blank" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl0" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-calendar"></i>No. of Implementing Partners
                                                                    </a>

                                                                     <a href='DrillDownCountriesDashboard.aspx?Year=<%=hasflash.Value %>&status=2' target="_blank"  class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="lbl1"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-truck"></i>No. of Implementing Counties
                                                                    </a>
                                                                     <a href='DrillDownPayamsDashboard.aspx?Year=<%=hasflash.Value %>&status=3' target="_blank"  class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="lbl2"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-money"></i>No. of Implementing Payams
                                                                    </a>
                                                                      <a href='DrillDownBomasDashboard.aspx?Year=<%=hasflash.Value %>&status=4'  target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl3" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Number of Implementing Bomas
                                                                    </a>
                                                                    
                                                                      <a href="DrillDownFundDistributedReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=6" target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl5" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>No. of works to be approved
                                                                    </a>

                                                                    <a href="DrillDownFundDistributedReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=7" target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl6" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>No. of works in progress
                                                                    </a>

     <a href="DrillDownFundDistributedReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=8" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl7" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>No. of works completed
                                                                    </a>
                                                                    
                                                                       <a href="DrillDownFundDistributedReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=5" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl4" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Total no. of works proposed
                                                                    </a>

                                                                </div>
                                                            </td>
                                                            <td style="width: 80px">&nbsp;</td>
                                                            <td valign="top">
                                                                <div class="list-group">


                                                                 
                                                                    
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LblEnBen" runat="server"></asp:Label>


                                                                        </span>
                                                                        <i class="fa fa-fw fa-user"></i>Enrolled Beneficiaries
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LblEnBenM" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-user"></i>Enrolled Male beneficiaries
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="LblEnBenF" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-user"></i>Enrolled Female beneficiaries
                                                                    </a>
                                                                    
                                                                     <a href="DrillDownBenificiaryWagespaymentsReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=9" target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl9" runat="server"></asp:Label>


                                                                        </span>
                                                                        <i class="fa fa-fw fa-user"></i>Allocated Beneficiaries
                                                                    </a>
                                                                   <a href="DrillDownBenificiaryWagespaymentsReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=10" target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl10" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-user"></i>Allocated Male beneficiaries
                                                                    </a>
                                                                     <a href="DrillDownBenificiaryWagespaymentsReportsDashboard.aspx?Year=<%=hasflash.Value %>&status=11" target="_blank"  class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl11" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-user"></i>Allocated Female beneficiaries
                                                                    </a>
                                                                 
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl_funds_uti" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Funds Utilization(Tools, Skilled labour, Materials) 
                                                                    </a>
                                                                </div>
                                                            </td>
                                                             <td width="100px"></td>
                                                        </tr>
                                                       <%-- <tr>
                                                            <td style="width: 395px">&nbsp;</td>
                                                            <td style="width: 30px">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>--%>
                                                    </table>

                                                    <table cellpadding="3" cellspacing="5" style="width: 100%">
                                                        <tr>
                                                            <td style="width: 395px">
                                                                <div class="list-group">
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl_work_days_target" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Total Work days target
                                                                    </a>

                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                           <asp:Label ID="lbl12" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>No of work days created
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge" style="background-color: #d9534f;">
                                                                            <asp:Label ID="Label3"
                                                                                runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>% of work days created
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label4" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Total wages target in USD
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="lbl13" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>No of wages paid  in USD
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label6" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>% of wages created  in USD
                                                                    </a>                                                                  
                                                                </div>
                                                            </td>
                                                            <td style="width: 80px">&nbsp;</td>
                                                            <td valign="top">
                                                                <div class="list-group">
                                                                   
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label11" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Total funds disbursed target
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                           <asp:Label ID="lbl8" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Funds Disbursed(In SSP)
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label13" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>% of Funds disbursed
                                                                    </a> 
                                                                     <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label8" runat="server"></asp:Label></span>
                                                                        <i class="fa fa-fw fa-check"></i>Total wages target in SSP
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label9" runat="server"></asp:Label>
                                                                        </span>
                                                                        <i class="fa fa-fw fa-check"></i>No of wages paid  in SSP
                                                                    </a>
                                                                    <a href="#" class="list-group-item">
                                                                        <span class="badge">
                                                                            <asp:Label ID="Label10" runat="server"></asp:Label>
                                                                        </span>
                                                                        <i class="fa fa-fw fa-check"></i>% of wages created  in SSP
                                                                    </a>                                                                  
                                                                </div>
                                                            </td>
                                                             <td width="100px"></td>
                                                        </tr>
                                                       <%-- <tr>
                                                            <td style="width: 395px">&nbsp;</td>
                                                            <td style="width: 30px">&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>--%>
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
                                                    <td style="width: 417px">&nbsp;</td>
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
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left"
                                                    VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No"
                                                        Visible="True">
                                                        <ItemTemplate>
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

                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True"
                                                    ForeColor="White" />
                                                <EditRowStyle BackColor="#B9D684" />
                                                <AlternatingRowStyle BackColor="White" />
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


<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Public Works/CCMaster.master" CodeBehind="NonGBVLandingPaging.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.NonGBVLandingPaging" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

  <script type="text/javascript">
    

     
  </script>
<style>   
        a {
         color: #337ab7;
         text-decoration: none;
         padding: 0px;
        }
        .RadButtonStyle {
         border-color: #ccc;
         color: #333;
         background-color: #fff;
         padding: 6px 10px;
         border-width: 1px;
         border-style: solid;
         border-color: #ccc;
         background-position: 0 0;
         border-radius: 4px;
        }
</style>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 30px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">Non-GBV Landing
                        </h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upPanelMain">
                        <ContentTemplate>
                          
                            <div class="panel-body">

                                   <table id="tbMain" runat="server" width="100%" cellpadding="4" cellspacing="5">
                             
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">State Name</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlState" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="128px">Payam/Block Name</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Enabled="false" Height="33px" Width="250px" runat="server" ID="ddlPayam" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">County/Municipality Name</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="250px" Enabled="false" runat="server" ID="ddlCounty" AutoPostBack="True" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="128px">Boma/Quarter Council Name</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Enabled="false" Width="250px" runat="server" ID="ddlBoma" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                                              
                  
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">Status</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="180px" runat="server" ID="ddlStatus" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        
                                        <td style="padding: 5px; margin: 5px">

<%--                                             <asp:DropDownList class="form-control dropdown" Height="33px" Width="180px" runat="server" ID="ddlDelayed" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value=""></asp:ListItem>
                                                 <asp:ListItem Text="YES" Value="1"></asp:ListItem>
                                                 <asp:ListItem Text="NO" Value="0"></asp:ListItem>
                                            </asp:DropDownList>--%>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>

                                <div style="padding: 20px" align="center">
                                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-success" Height="32px" TabIndex="10" Text="Search" Width="90px" OnClick="BtnSearch_Click" />
                                    &nbsp; &nbsp;                                         
                                            <asp:Button ID="BtnReset" runat="server" CssClass="btn btn-danger" Height="32px" TabIndex="10" Text="Reset" Width="90px" OnClick="BtnReset_Click" />

                                
                                      &nbsp; &nbsp; 
                                    <asp:Button ID="btnBackNonGBV" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Visible="true" Text="Back" Width="120px" OnClick="btnBackNonGBV_Click" />

                                </div>
                            </div>
                            <table id="tbDetailsList" width="98%" cellpadding="4" cellspacing="5">
                                <tr align="center">
                                    <td>
                                         <telerik:RadGrid ID="RadGridLanding" AutoGenerateColumns="false" runat="server" Visible="false"
                                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                        MasterTableView-TableLayout="Fixed"
                                                        OnNeedDataSource="RadGridLanding_NeedDataSource" 
                                                        OnItemDataBound="RadGridLanding_ItemDataBound">
                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                        <MasterTableView TableLayout="Fixed"
                                                            DataKeyNames="NonGBVMainID,Expired,Status"
                                                            ClientDataKeyNames="NonGBVMainID,Expired" AllowPaging="true" PageSize="20">
                                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                           
                                                            <Columns>
                                                                <telerik:GridTemplateColumn HeaderText="" UniqueName="ImageExpired"
                                                                    HeaderStyle-Width="30px" ItemStyle-Width="35px" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                     <asp:Image  runat="server" ID="imgExpired" ImageUrl="~/Resource/Images/Success.png" Width="18px" />
                                                                    </ItemTemplate>
                                                                </telerik:GridTemplateColumn>

                                                                <telerik:GridBoundColumn DataField="NonGBVMainID" HeaderText="NonGBVMainID" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="SerialNumber" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Serial Number" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="StateName" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="State Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="Date" Display="true" ItemStyle-HorizontalAlign="left" HeaderText="Creation Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="130px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="ComplainantName" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Complainant Name" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="30%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="ReportingChannel" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Reporting Channel" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="DateOfComplaint" Display="true" ItemStyle-HorizontalAlign="left" HeaderText="Date Of Complaint" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                                                <telerik:GridBoundColumn DataField="SSSNPBeneficiary" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="SSSNP Beneficiary" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="100px"></telerik:GridBoundColumn>
                                                                  <telerik:GridBoundColumn DataField="StatusName" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="140px"></telerik:GridBoundColumn>

                                                               <telerik:GridTemplateColumn HeaderText="" UniqueName="EditColumn"
                                                                    HeaderStyle-Width="100px" ItemStyle-Width="150px" ItemStyle-HorizontalAlign="Center">
                                                                  <ItemTemplate>
                                                                      
                                                                      <telerik:RadButton runat="server" ID="btnEditDetail" OnClick="btnEditDetail_Click" CssClass="RadButtonStyle">
                                                                        <ContentTemplate>
                                                                        <i class="fa fa-pencil"></i>
                                                                        </ContentTemplate> 
                                                                      </telerik:RadButton>
                                                                        </ItemTemplate>
                                                                </telerik:GridTemplateColumn>
                                                                                                                               
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>

                                    </td>
                                </tr>
                                 <tr>
                                            <td align="center" style="padding: 5px; margin: 5px">
                                                &nbsp;</td>
                                        </tr>

                                <tr align="center">
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

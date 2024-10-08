<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="HouseholdAndGroupIdMapping.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.HouseholdAndGroupIdMapping" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Household And Group Id Mapping</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Household And Group Id Mapping</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">

                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelState" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelCounty" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlCounty" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>

                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPayam" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelBoma" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 10px" valign="top" align="center">
                                            <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary" Height="32px" OnClick="BtnSearch_Click" TabIndex="10" Text="Search" ValidationGroup="group" Width="110px" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                        <div>
                            <asp:Label ID="lblErrorMessage" runat="server" CssClass="errorMessage" ForeColor="Red" Visible="false" />
                        </div>
                        <script type="text/javascript">
                            function toggleSelectAll(source) {
                                // Use querySelectorAll to select all checkboxes that end with 'chkSelect' in their id
                                var checkboxes = document.querySelectorAll('input[id$="chkSelect"]');
                                for (var i = 0; i < checkboxes.length; i++) {
                                    checkboxes[i].checked = source.checked;  // Set the checked state based on the header checkbox
                                }
                            }
                        </script>


                        <div id="gridContainer" style="padding: 5px; margin: 10px;">
                            <telerik:RadGrid ID="rgResults" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10"
                                AllowSorting="true" GridLines="Both" EnableViewState="true" OnNeedDataSource="rgResults_NeedDataSource">
                                <MasterTableView DataKeyNames="household_number" NoMasterRecordsText="No records available." AllowPaging="true">
                                    <Columns>


                                        <telerik:GridTemplateColumn HeaderText="Select">
                                            <HeaderTemplate>
                                                <input type="checkbox" id="chkSelectAll" onclick="toggleSelectAll(this);" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelect" runat="server" />
                                            </ItemTemplate>
                                        </telerik:GridTemplateColumn>


                                        <telerik:GridBoundColumn DataField="household_number" HeaderText="Household Number" />
                                        <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Household Name" />
                                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" />
                                        <telerik:GridBoundColumn DataField="Age" HeaderText="Age" />
                                        <telerik:GridBoundColumn DataField="GroupId" HeaderText="Group ID" />
                                        <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group Name" />

                                        
                                        <telerik:GridTemplateColumn HeaderText="Assign Group ID">
                                            <HeaderTemplate>
                                                <asp:DropDownList ID="ddlGroupID" runat="server" AutoPostBack="false">
                                                    <asp:ListItem Text="--Select Group ID--" Value="0"></asp:ListItem>
                                                    
                                                    <asp:ListItem Text="Group 1" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="Group 2" Value="2"></asp:ListItem>
                                                    
                                                </asp:DropDownList>
                                            </HeaderTemplate>
                                        </telerik:GridTemplateColumn>

                                    </Columns>
                                </MasterTableView>
                            </telerik:RadGrid>

                            
                            <asp:Button ID="btnAssignGroup" runat="server" Text="Assign Group ID" OnClick="btnAssignGroup_Click" CssClass="btn btn-primary" />

                        </div>
                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
        <ProgressTemplate>
            <div class="overlay">
                <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                    <img alt="" src="../../Resource/Images/Processing.gif" />
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>

</asp:Content>

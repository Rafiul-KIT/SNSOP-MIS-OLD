<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="BeneficiaryList.aspx.cs" Inherits="SSSNP.UI.Public_Works.BeneficiaryList" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                        <i class="fa fa-edit"></i><a href="#">Beneficiary ID List</a>
                    </li>
                </ol>
            </div>

            <style type="text/css">
                                    .action-button {
                                        font-size: 12px;
                                        color: #ffffff !important;
                                        padding: 5px 10px;
                                    }
                                </style>

            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary ID List</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px; height: 155px;" valign="top">

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
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblEnrollmentNumber" runat="server" CssClass="LBLBLACK">Enrollment Number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEnrollmentNumber" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="50" OnTextChanged="txtEnrollmentNumber_TextChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelGroup" runat="server" CssClass="LBLBLACK" Width="165px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlGroup" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td style="width: 27px; height: 155px;"></td>
                                        <td valign="top" style="height: 155px">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LabelPayam" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
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
                                                            Height="33px" Width="180px" OnSelectedIndexChanged="ddlBoma_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblName" runat="server" CssClass="LBLBLACK">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="50" OnTextChanged="txtName_TextChanged" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblHouseholdNumber" runat="server" CssClass="LBLBLACK">Household Number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtHouseholdNumber" runat="server" CssClass="form-control txtbox" Width="180px" MaxLength="50" OnTextChanged="txtHouseholdNumber_TextChanged" />
                                                    </td>
                                                </tr>


                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <div style="display: flex; justify-content: center; align-items: center; gap: 20px; padding: 10px;">
                                    <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary"
                                        Height="32px" OnClick="BtnSearch_Click" TabIndex="10" Text="Search"
                                        ValidationGroup="group" Width="110px" />

                                    <asp:Button ID="BtnClear" runat="server" CssClass="btn btn-primary"
                                        Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="Clear"
                                        ValidationGroup="group" Width="110px" />
                                </div>


                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>
                            </div>
                        </div>
                        <telerik:RadGrid ID="RadGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnNeedDataSource="RadGrid_NeedDataSource" OnItemCommand="RadGrid_ItemCommand">
                            <MasterTableView AutoGenerateColumns="False" DataKeyNames="EnrollmentNo" AllowPaging="True" PagerStyle-AlwaysVisible="True" >
                                <Columns>
                                    <telerik:GridBoundColumn DataField="intBenid" HeaderText="ID" UniqueName="intBenid" Visible="false" />
                                    <telerik:GridBoundColumn DataField="StateName" HeaderText="State Name" UniqueName="StateName" />
                                    <telerik:GridBoundColumn DataField="CountyName" HeaderText="County Name" UniqueName="CountyName" />
                                    <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam Name" UniqueName="PayamName" />
                                    <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" UniqueName="BomaName" />
                                    <telerik:GridBoundColumn DataField="Name" HeaderText="Name" UniqueName="Name" />
                                    <telerik:GridBoundColumn DataField="Sex" HeaderText="Sex" UniqueName="Sex" />
                                    <telerik:GridBoundColumn DataField="Age" HeaderText="Age" UniqueName="Age" />
                                    <telerik:GridBoundColumn DataField="ContactNo" HeaderText="Contact No" UniqueName="ContactNo" />
                                    <telerik:GridBoundColumn DataField="Email" HeaderText="Email" UniqueName="Email" />
                                    <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Enrollment No" UniqueName="EnrollmentNo" />
                                    <telerik:GridBoundColumn DataField="Component" HeaderText="Component" UniqueName="Component" />
                                    <telerik:GridTemplateColumn HeaderText="Actions" UniqueName="Actions">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("EnrollmentNo") %>' CssClass="btn btn-success action-button" Text="Download" />
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                </Columns>
                            </MasterTableView>
                            <PagerStyle Mode="NextPrevAndNumeric" />
                        </telerik:RadGrid>


                    </div>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

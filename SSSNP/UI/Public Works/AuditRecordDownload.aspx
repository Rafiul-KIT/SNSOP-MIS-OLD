<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="AuditRecordDownload.aspx.cs" Inherits="SSSNP.UI.Public_Works.AuditRecordDownload" %>

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
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Audit Record Download</a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Audit Record Download</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="5" cellspacing="3" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lblAuditRecordType" runat="server" CssClass="LBLBLACK" Text="Select Audit Record&emsp;:"></asp:Label>
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:DropDownList ID="ddlAuditRecordType" runat="server" CssClass="form-control" Width="230px" OnSelectedIndexChanged="ddlAuditRecordType_SelectedIndexChanged" AutoPostBack="true">
                                                <asp:ListItem Text="-- Select Audit Record --" Value=""></asp:ListItem>
                                                <asp:ListItem Text="Beneficiary Randomization and Enrollment Audit" Value="tbl_com2_Household_Enrollment_Data_Audit"></asp:ListItem>
                                                <asp:ListItem Text="Training and Attendance Tracking Audit" Value="tbl_com2_Training_Attendance_Audit"></asp:ListItem>
                                                <asp:ListItem Text="Livelihood Grant Disbursement Audit" Value="tbl_com2_Livelihood_Grant_Disbursement_Audit"></asp:ListItem>
                                                <asp:ListItem Text="Financial Services Linkage Audit" Value="tbl_com2_Financial_Services_Linkage_Tracking_Audit"></asp:ListItem>
                                                <asp:ListItem Text="Business Plan Development Audit" Value="tbl_com2_Business_Plan_Development_Audit"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lblFromDate" runat="server" CssClass="LBLBLACK" Text="From Date&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;:"></asp:Label>
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" Width="150px" placeholder="MM/DD/YYYY" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calFromDate" runat="server" TargetControlID="txtFromDate" Format="MM/dd/yyyy" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px;">
                                            <asp:Label ID="lblToDate" runat="server" CssClass="LBLBLACK" Text="To Date&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;&emsp;:"></asp:Label>
                                        </td>
                                        <td style="padding: 5px;">
                                            <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" Width="150px" placeholder="MM/DD/YYYY" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                            <cc1:CalendarExtender ID="calToDate" runat="server" TargetControlID="txtToDate" Format="MM/dd/yyyy" />
                                        </td>
                                    </tr>
                                   
                                    <tr>
                                        <td colspan="2" style="padding: 30px;" align="center">
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                            <asp:Button ID="btnDownload" runat="server" Text="Download" CssClass="btn btn-success" OnClick="btnDownload_Click" Style="margin-left: 15px;" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger" Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                
                <div class="row" style="margin-top: 20px;">
                    <div class="col-lg-11 grid-container" style="max-width: 92%; float: left; margin-right: 60px;">
                        <telerik:RadGrid ID="gvAuditRecords" runat="server" CssClass="table table-bordered table-striped"
                            AutoGenerateColumns="true" AllowPaging="true" AllowSorting="false" PageSize="10"
                            GridLines="Both" Visible="false" OnNeedDataSource="gvAuditRecords_NeedDataSource">

                            
                            <MasterTableView AutoGenerateColumns="true" TableLayout="Auto"  AllowPaging="True" PagerStyle-AlwaysVisible="True">
                            </MasterTableView>

                            <PagerStyle Mode="NextPrevNumericAndAdvanced" />
                        </telerik:RadGrid>
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

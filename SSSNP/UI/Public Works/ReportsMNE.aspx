<%@ Page Title=":: :: SSSNP : : Reports" MasterPageFile="~/UI/Public Works/CCMaster.master" Language="C#" AutoEventWireup="true" CodeBehind="ReportsMNE.aspx.cs" Inherits="SSSNP.UI.Public_Works.ReportsMNE" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
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
                        <i class="fa fa-edit"></i><a href="#">Reports</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Reports</h3>
                            </div>
                            <div class="panel-body">

                                <script type="text/javascript">
                                    function handleFileChange(input) {
                                        var fileName = input.files[0] ? input.files[0].name : "No file chosen";
                                        var fileSize = input.files[0] ? (input.files[0].size / 1024).toFixed(2) + " KB" : "";
                                        var fileInfo = document.getElementById('fileInfo');
                                        fileInfo.innerText = fileName + (fileSize ? " (" + fileSize + ")" : "");
                                    }
                                </script>

                                <style type="text/css">
                                    .custom-file-upload {
                                        position: relative;
                                        display: inline-block;
                                    }

                                        .custom-file-upload button {
                                            padding: 6px 15px;
                                            font-size: 12px;
                                            cursor: pointer;
                                            border: 1px solid #ccc;
                                            border-radius: 4px;
                                        }

                                    .file-info {
                                        display: inline-block;
                                        margin-left: 10px;
                                        font-size: 14px;
                                        vertical-align: middle;
                                        padding: 6px 15px;
                                        border: 1px solid #ccc;
                                        border-radius: 4px;
                                    }

                                    .action-button {
                                        font-size: 12px;
                                        color: #ffffff !important;
                                        padding: 5px 10px;
                                    }
                                </style>



                                <table style="width: 100%; table-layout: fixed;">
                                    <tr>
                                        <td style="padding: 5px 0;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="fileUpload" class="control-label" style="margin-right: 10px;">Attachment</label>
                                                <div class="custom-file-upload">
                                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" Enabled="true" Style="display: none;" onchange="handleFileChange(this)" />

                                                    <button type="button" class="btn btn-primary" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();">Choose File</button>
                                                    <span id="fileInfo" class="file-info">No file chosen</span>
                                                </div>
                                            </div>
                                        </td>
                                        <td style="padding: 5px 50px;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="ddlSubmitReportType" style="margin-right: 10px;">Report Type</label>
                                                <asp:DropDownList ID="ddlSubmitReportType" runat="server" CssClass="form-control" Style="width: auto;" OnSelectedIndexChanged="ddlSubmitReportType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Report Type" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Partner Monitoring" Value="PartnerMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="SPCU Monitoring" Value="SPCUMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="Third Party Monitoring" Value="ThirdPartyMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="Final Periodic" Value="FinalPeriodic"></asp:ListItem>
                                                    <asp:ListItem Text="Evaluation" Value="Evaluation"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <div class="form-group" style="display: flex; justify-content: center; gap: 15px; margin-top: 10px;">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmit_Click" />
                                </div>




                                <table style="width: 100%;">
                                    <tr>

                                        <td style="padding: 10px 0;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="txtFromDate" style="margin-right: 10px;">From Date &emsp; :</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" placeholder="DD-MM-YYYY" Width="150px" OnTextChanged="txtFromDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="FromDateCalendarExtender" runat="server" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                            </div>
                                        </td>
                                        <td>&emsp;&emsp;&emsp;&emsp;&emsp;&nbsp;&nbsp;</td>
                                        <td style="padding: 10px 0;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="ddlSearchReportType" style="margin-right: 10px;">Report Type</label>
                                                <asp:DropDownList ID="ddlSearchReportType" runat="server" CssClass="form-control" Style="width: auto;" OnSelectedIndexChanged="ddlSearchReportType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Report Type" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Partner Monitoring" Value="PartnerMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="SPCU Monitoring" Value="SPCUMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="Third Party Monitoring" Value="ThirdPartyMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="Final Periodic" Value="FinalPeriodic"></asp:ListItem>
                                                    <asp:ListItem Text="Evaluation" Value="Evaluation"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 10px 0;">
                                            <div style="display: flex; align-items: end;">
                                                <label for="txtToDate" style="margin-right: 10px;">To Date &emsp;&emsp;&nbsp; :</label>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" placeholder="DD-MM-YYYY" Width="150px" OnTextChanged="txtToDate_TextChanged"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ToDateCalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>


                                <div class="form-group" style="display: flex; justify-content: center; gap: 15px; margin-top: 15px;">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                    <asp:Button ID="btnShowDeletedRecords" runat="server" CssClass="btn btn-warning" Text="Deleted Records" OnClick="btnShowDeletedRecords_Click" />
                                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-danger" Text="Clear" OnClick="btnClear_Click" />
                                </div>
                                <br />
                                <br />





                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>

                                <telerik:RadGrid ID="RadGrid" runat="server" OnNeedDataSource="RadGrid_NeedDataSource" DataKeyNames="Id" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" Visible="false" OnItemCommand="RadGrid_ItemCommand">

                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" PagerStyle-AlwaysVisible="True">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="ID" UniqueName="Id" />
                                            <telerik:GridBoundColumn DataField="FileName" HeaderText="File Name" UniqueName="FileName" />
                                            <telerik:GridBoundColumn DataField="ReportType" HeaderText="Report Type" UniqueName="ReportType" />
                                            <telerik:GridBoundColumn DataField="UploadedBy" HeaderText="Uploaded By" UniqueName="UploadedBy" Visible="false" />
                                            <telerik:GridBoundColumn DataField="UploadedDate" HeaderText="Uploaded Date" UniqueName="UploadedDate" Visible="false" />
                                            <telerik:GridBoundColumn DataField="DeletedBy" HeaderText="Deleted By" UniqueName="DeletedBy" Visible="false" />
                                            <telerik:GridBoundColumn DataField="DeletedDate" HeaderText="Deleted Date" UniqueName="DeletedDate" Visible="false" />
                                            <telerik:GridTemplateColumn HeaderText="&emsp;&emsp;&emsp;&emsp;&emsp;Actions" UniqueName="Actions" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnDownload" runat="server" CommandName="Download" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-success action-button" Text="Download" />
                                                    <asp:Label Text="&nbsp;&nbsp;" runat="server" />
                                                    <asp:LinkButton ID="btnRemove" runat="server" CommandName="Remove" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger action-button" Text="Remove" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                            <telerik:GridTemplateColumn HeaderText="&emsp;&emsp;&emsp;&emsp;&emsp;Manage" UniqueName="Manage" Visible="false">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="btnRecover" runat="server" CommandName="Recover" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-success action-button" Text="Recover" />
                                                    <asp:Label Text="&nbsp;&nbsp;" runat="server" />
                                                    <asp:LinkButton ID="btnDelete" runat="server" CommandName="Delete" CommandArgument='<%# Eval("Id") %>' CssClass="btn btn-danger action-button" Text="Delete" />
                                                </ItemTemplate>
                                            </telerik:GridTemplateColumn>
                                        </Columns>
                                    </MasterTableView>
                                </telerik:RadGrid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:PostBackTrigger ControlID="btnSearch" />
            <asp:PostBackTrigger ControlID="btnClear" />
        </Triggers>
    </asp:UpdatePanel>

</asp:Content>

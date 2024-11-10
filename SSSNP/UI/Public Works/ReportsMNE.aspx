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
                                <table style="width: 100%;">
                                    <tr>
                                        <td style="padding: 10px 0;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="ddlReportType" style="margin-right: 10px;">Report Type</label>
                                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control" Style="width: auto;" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                    <asp:ListItem Text="Select Report Type" Value="0"></asp:ListItem>
                                                    <asp:ListItem Text="Partner Monitoring" Value="PartnerMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="SPCU Monitoring" Value="SPCUMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="Third Party Monitoring" Value="ThirdPartyMonitoring"></asp:ListItem>
                                                    <asp:ListItem Text="Final Periodic" Value="FinalPeriodic"></asp:ListItem>
                                                    <asp:ListItem Text="Evaluation" Value="Evaluation"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </td>
                                        <td style="padding: 10px 0;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="txtFromDate" style="margin-right: 10px;">From Date &emsp; :</label>
                                                <asp:TextBox ID="txtFromDate" runat="server" CssClass="form-control" placeholder="DD-MM-YYYY" Width="150px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="FromDateCalendarExtender" runat="server" TargetControlID="txtFromDate" Format="dd-MM-yyyy" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&emsp;</td>

                                        <td style="padding: 10px 0;">
                                            <div style="display: flex; align-items: end;">
                                                <label for="txtToDate" style="margin-right: 10px;">To Date &emsp;&emsp;&nbsp; :</label>
                                                <asp:TextBox ID="txtToDate" runat="server" CssClass="form-control" placeholder="DD-MM-YYYY" Width="150px"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="ToDateCalendarExtender" runat="server" TargetControlID="txtToDate" Format="dd-MM-yyyy" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>


                                <div class="form-group" style="display: flex; justify-content: center; gap: 15px; margin-top: 15px;">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                </div>
                                <br />
                                <br />

                                <table style="width: 100%;">
                                    <tr>
                                        <td style="padding: 5px 0;">
                                            <div style="display: flex; align-items: center;">
                                                <label for="ddlReportType" style="margin-right: 10px;">Report Type</label>
                                                <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" Style="width: auto;" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
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
                                    </tr>
                                </table>
                                <div class="form-group" style="display: flex; justify-content: left; gap: 15px; margin-top: 10px;">
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" Text="Submit" OnClick="btnSubmit_Click" />
                                </div>
                                
                                

                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>

                                <telerik:radgrid id="RadGrid" runat="server" onneeddatasource="RadGrid_NeedDataSource" autogeneratecolumns="false" allowpaging="true" pagesize="10">
                                    <pagerstyle mode="NextPrevAndNumeric" />
                                    <mastertableview autogeneratecolumns="False" datakeynames="Id" allowpaging="True" pagerstyle-alwaysvisible="True">
                                        <columns>
                                            <telerik:gridboundcolumn datafield="Id" headertext="ID" uniquename="Id" />
                                            <telerik:gridboundcolumn datafield="FileName" headertext="File Name" uniquename="FileName" />
                                            <telerik:gridboundcolumn datafield="ReportType" headertext="Report Type" uniquename="ReportType" />
                                        </columns>
                                    </mastertableview>
                                </telerik:radgrid>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
         <Triggers>
    <asp:PostBackTrigger ControlID="btnSubmit"/>
</Triggers>
    </asp:UpdatePanel>
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
    </style>
</asp:Content>

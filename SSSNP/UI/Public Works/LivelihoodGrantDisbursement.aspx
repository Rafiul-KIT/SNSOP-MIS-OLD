<%@ Page Title=":: :: SSSNP : : Livelihood Grant Disbursement" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="LivelihoodGrantDisbursement.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.LivelihoodGrantDisbursement" %>

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
                        <i class="fa fa-edit"></i><a href="#">Livelihood Grant Disbursement</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Livelihood Grant Disbursement</h3>
                            </div>
                            <div class="panel-body">
                                <!-- File Upload for Excel -->
                                <div class="form-group">
                                    <label for="fileUpload" class="control-label">Attachment:</label>

                                    <!-- Custom file upload button -->
                                    <div class="custom-file-upload">
                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" Enabled="true" Style="display: none;" onchange="handleFileChange(this)" />
                                        
                                        <button type="button" class="btn btn-primary" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();">Choose File</button>
                                        <span id="fileInfo" class="file-info">No file chosen</span>
                                        <!-- File name and size display -->
                                    </div>
                                </div>

                                <!-- CSS for styling -->
                                <style type="text/css">
                                    .custom-file-upload {
                                        position: relative;
                                        display: inline-block;
                                    }

                                        .custom-file-upload button {
                                            padding: 6px 15px;
                                            font-size: 12px;
                                            cursor: pointer;
                                            border: 1px solid #ccc; /* Add border around the button */
                                            border-radius: 4px;
                                        }

                                    .file-info {
                                        display: inline-block;
                                        margin-left: 10px;
                                        font-size: 14px;
                                        vertical-align: middle;
                                        padding: 6px 15px; /* Adjust padding for alignment with button */
                                        border: 1px solid #ccc; /* Add border around the "No file chosen" field */
                                        border-radius: 4px;
                                    }
                                </style>



                                <!-- JavaScript to handle file change and display file info -->
                                <script type="text/javascript">
                                    function handleFileChange(input) {
                                        var fileName = input.files[0] ? input.files[0].name : "No file chosen";
                                        var fileSize = input.files[0] ? (input.files[0].size / 1024).toFixed(2) + " KB" : "";
                                        var fileInfo = document.getElementById('fileInfo');
                                        fileInfo.innerText = fileName + (fileSize ? " (" + fileSize + ")" : "");
                                    }
                                </script>
                                <br />
                                <div class="form-group">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-warning" Text="Upload File" OnClick="btnUpload_Click" />
                                    <br />
                                    <br />

                                    <!-- Link for Downloading Excel Template -->
                                    <asp:LinkButton ID="lnkDownloadTemplate" runat="server" CssClass="download-link" OnClick="lnkDownloadTemplate_Click">Import Livelihood Grant Disbursement Template File</asp:LinkButton>
                                </div>

                                <!-- Action Buttons -->
                                <div class="form-group" align="center">
                                    <asp:Button ID="btnAnalyze" runat="server" CssClass="btn btn-warning" Text="Analyze" Enabled="false" OnClick="btnAnalyze_Click" />
                                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-success" Text="Import" Enabled="false" OnClick="btnImport_Click" />
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                </div>

                                <!-- Success and Failure Messages -->
                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>

                                <!-- Telerik RadGrid for displaying data -->
                                <telerik:RadGrid ID="RadGrid" runat="server" AllowPaging="True" PageSize="10"
                                    AllowCustomPaging="True" OnNeedDataSource="RadGrid_NeedDataSource" OnPageSizeChanged="RadGrid_PageSizeChanged"
                                    OnPageIndexChanged="RadGrid_PageIndexChanged" GridLines="None">
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="ID" />
                                            <telerik:GridBoundColumn DataField="HouseholdNumber" HeaderText="Household Number" />
                                            <telerik:GridBoundColumn DataField="PaymentCycle" HeaderText="Payment Cycle" />
                                            <telerik:GridBoundColumn DataField="PaymentAmount" HeaderText="Payment Amount" />
                                            <telerik:GridBoundColumn DataField="PaymentDate" HeaderText="Payment Date" />
                                        </Columns>
                                        <PagerStyle PageSizeControlType="RadComboBox">
                                        </PagerStyle>
                                    </MasterTableView>
                                    <PagerStyle AlwaysVisible="true" />
                                </telerik:RadGrid>
                        </div>   
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
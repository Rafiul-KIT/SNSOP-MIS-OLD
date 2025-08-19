<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="CoachingDetails.aspx.cs" Inherits="SSSNP.UI.Public_Works.CoachingDetails" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
                        <i class="fa fa-edit"></i><a href="#">Coaching Details</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Coaching Details</h3>
                            </div>
                            <div class="panel-body">
                                
                                <div class="form-group">
                                    <label for="fileUpload" class="control-label">Attachment:</label>

                                    
                                    <div class="custom-file-upload">
                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" Enabled="true" Style="display: none;" onchange="handleFileChange(this)" />

                                        <button type="button" class="btn btn-primary" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();">Choose File</button>
                                        <span id="fileInfo" class="file-info">No file chosen</span>
                                        
                                    </div>
                                </div>

                                
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

                                    .download-link {
                                        display: inline-block;
                                        color: #ffffff;
                                        background-color: #007bff;
                                        padding: 10px 20px;
                                        text-decoration: none;
                                        border-radius: 5px;
                                        transition: background-color 0.3s ease;
                                        font-size: 16px;
                                        font-weight: bold;
                                    }

                                        .download-link:hover {
                                            background-color: #0056b3;
                                            color: #fff;
                                            text-decoration: none;
                                        }

                                        .download-link i {
                                            margin-right: 5px;
                                        }

                                    .enhanced-link {
                                        box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                                        transition: all 0.2s ease-in-out;
                                    }

                                        .enhanced-link:hover {
                                            transform: translateY(-2px);
                                            box-shadow: 0 6px 10px rgba(0, 0, 0, 0.15);
                                        }
                                </style>



                                
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


                                </div>

                                
                                <div class="form-group" align="center">
                                    <asp:Button ID="btnAnalyze" runat="server" CssClass="btn btn-warning" Text="Analyze" Enabled="false" OnClick="btnAnalyze_Click" />
                                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-success" Text="Import" Enabled="false" OnClick="btnImport_Click" />
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary" Text="Search" OnClick="btnSearch_Click" />
                                </div>

                                
                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>

                                
                               <telerik:RadGrid ID="RadGrid" runat="server" OnItemDataBound="RadGrid_ItemDataBound" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" 
                                    OnNeedDataSource="RadGrid_NeedDataSource" AllowCustomPaging="True" GridLines="Both" >
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <MasterTableView AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" PagerStyle-AlwaysVisible="True" AllowCustomPaging="True">
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="ID" />
                                            <telerik:GridBoundColumn DataField="CoachingName" HeaderText="Coaching Name" />
                                            <telerik:GridBoundColumn DataField="CoachingSessionHoldingDate" HeaderText="Coaching Session Holding Date" />
                                            <telerik:GridBoundColumn DataField="CoachingSessionDurationMinutes" HeaderText="Coaching Session Duration Minutes" />
                                            <telerik:GridBoundColumn DataField="ValidationStatus" HeaderText="Validation Status" UniqueName="ValidationStatus" Visible="false" />
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
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
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


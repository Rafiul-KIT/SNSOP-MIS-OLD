<%@ Page Title=":: :: SSSNP : : Household Group View" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="HouseholdGroupView.aspx.cs" Inherits="SSSNP.UI.Public_Works.HouseholdGroupView" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <!-- Breadcrumb Navigation -->
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
                        <i class="fa fa-edit"></i><a href="#">Household Group View</a>
                    </li>
                </ol>
            </div>

            <!-- Main Panel -->
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Household Group View</h3>
                            </div>

                            <div class="panel-body">
                                <!-- File Upload for Excel -->
                                <div class="form-group">
                                    <label for="fileUpload" class="control-label">Attachment:</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" Enabled="true" onchange="adjustWidth(this)" />
                                </div>

                                <script type="text/javascript">
                                    function adjustWidth(input) {
                                        var fileName = input.value.split('\\').pop(); // Get file name
                                        var length = fileName.length;

                                        // Set a base width and increase based on file name length
                                        var newWidth = Math.max(200, length * 10); // Adjust the multiplier for finer control

                                        // Apply the calculated width (max 100% to avoid overflow)
                                        input.style.width = newWidth + 'px';
                                    }
                                </script>


                                <!-- Upload and Download Links -->
                                <div class="form-group">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-warning" Text="Upload File" OnClick="btnUpload_Click" />
                                    <br /><br />

                                    <!-- Link for Downloading Excel Template -->
                                    <asp:LinkButton ID="lnkDownloadTemplate" runat="server" CssClass="download-link" OnClick="lnkDownloadTemplate_Click">Import Household Group Template File</asp:LinkButton>
                                </div>

                                <!-- Action Buttons -->
                                <div class="form-group" align="center">
                                    <asp:Button ID="btnAnalyze" runat="server" CssClass="btn btn-warning" Text="Analyze" Enabled="false" OnClick="btnAnalyze_Click"/>
                                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-success" Text="Import" Enabled="false" OnClick="btnImport_Click"/>
                                </div>

                                <!-- Success and Failure Messages -->
                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>

                                <telerik:RadGrid ID="RadGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="GroupName" HeaderText="Group Name" UniqueName="GroupName" />
                                            <telerik:GridBoundColumn DataField="Latitude" HeaderText="Latitude" UniqueName="Latitude" DataFormatString="{0:N7}" />
                                            <telerik:GridBoundColumn DataField="Longitude" HeaderText="Longitude" UniqueName="Longitude" DataFormatString="{0:N7}" />
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
</asp:Content>

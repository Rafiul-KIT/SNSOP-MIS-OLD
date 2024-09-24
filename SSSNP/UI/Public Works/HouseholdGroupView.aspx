<%@ Page Title=":: :: SSSNP : : Household Group View" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="HouseholdGroupView.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.HouseholdGroupView" %>

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
                                <%--<div class="form-group">
                                    <label for="fileUpload" class="control-label">Attachment:</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" Enabled="true" Style="width: 400px;" />
                                </div>

                                <script type="text/javascript">
                                    document.addEventListener("DOMContentLoaded", function () {
                                        var fileInput = document.getElementById('<%= fileUpload.ClientID %>');

                                        // Function to adjust the width based on the file name or default "No file chosen"
                                        function adjustWidth() {
                                            var fileName = fileInput.value.split('\\').pop(); // Get the file name
                                            var defaultText = "No file chosen"; // Default text
                                            var length = fileName.length || defaultText.length; // Use default text length if no file is selected

                                            // Set the width dynamically based on the file name or default text
                                            var newWidth = Math.max(110, length * 10); // Adjust multiplier for control over the width

                                            // Apply the new calculated width
                                            fileInput.style.width = newWidth + 'px';
                                        }

                                        // Set width when the page loads
                                        adjustWidth();

                                        // Add event listeners for when the input changes
                                        fileInput.addEventListener('change', adjustWidth); // When a file is selected
                                        fileInput.addEventListener('input', adjustWidth);  // To handle any other changes
                                    });
                                </script>--%>
                                <div class="form-group">
                                    <label for="fileUpload" class="control-label">Attachment:</label>
                                    <div style="display: flex;">
                                        <input type="text" id="fileNameDisplay" runat="server" class="form-control" readonly="readonly" style="width: 150px;" placeholder="No file chosen" />
                                        <button type="button" class="btn btn-default" onclick="document.getElementById('<%= fileUpload.ClientID %>').click()">Choose File</button>
                                        <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" Style="display: none;" onchange="updateFileName()" />
                                    </div>
                                </div>

                                <script type="text/javascript">
                                    function updateFileName() {
                                        var fileInput = document.getElementById('<%= fileUpload.ClientID %>');
                                        var fileNameDisplay = document.getElementById('<%= fileNameDisplay.ClientID %>');
                                        var fileName = fileInput.value.split('\\').pop(); // Get only the file name
                                        fileNameDisplay.value = fileName || "No file chosen"; // Show file name or default text

                                        // Adjust the width based on the file name length
                                        var length = fileName.length || "No file chosen".length; // Calculate length of file name or default text
                                        var newWidth = Math.max(150, length * 10); // Adjust width dynamically, with minimum width of 150px
                                        fileNameDisplay.style.width = newWidth + 'px';
                                    }

                                    function clearFileNameDisplay() {
                                        var fileNameDisplay = document.getElementById('<%= fileNameDisplay.ClientID %>');
                                        fileNameDisplay.value = "No file chosen"; // Clear only the file name display
                                        fileNameDisplay.style.width = '150px'; // Reset width to default
                                    }
                                </script>

                                <!-- Upload and Download Links -->
                                <div class="form-group">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-warning" Text="Upload File" OnClientClick="clearFileNameDisplay();" OnClick="btnUpload_Click" />
                                    <br />
                                    <br />

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

                                <style type="text/css">
                                    .redundant-row {
                                        background-color: #f8d7da; /* Light red background */
                                        color: #721c24; /* Dark red text */
                                    }
                                </style>


                                <telerik:RadGrid ID="RadGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" PageSize="10" OnItemDataBound="RadGrid_ItemDataBound">
                                    <MasterTableView>
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="GroupId" HeaderText="Group ID" UniqueName="GroupId" />
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

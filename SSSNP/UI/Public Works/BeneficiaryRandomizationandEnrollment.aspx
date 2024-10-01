<%@ Page Title=":: :: SSSNP : : Beneficiary Randomization and Enrollment" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="BeneficiaryRandomizationandEnrollment.aspx.cs" Inherits="SSSNP.UI.Public_Works.BeneficiaryRandomizationandEnrollment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                        <i class="fa fa-edit"></i><a href="#">Beneficiary Randomization and Enrollment</a>
                    </li>
                </ol>
            </div>
            <!-- Main Panel -->
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary Randomization and Enrollment</h3>
                            </div>

                            <div class="panel-body">
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
                                    <asp:LinkButton ID="lnkDownloadTemplate" runat="server" CssClass="download-link" OnClick="lnkDownloadTemplate_Click">Import Beneficiary Randomization and Enrollment Template File</asp:LinkButton>
                                </div>

                                <!-- Action Buttons -->
                                <div class="form-group" align="center">
                                    <asp:Button ID="btnAnalyze" runat="server" CssClass="btn btn-warning" Text="Analyze" Enabled="false" OnClick="btnAnalyze_Click"/>
                                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-success" Text="Import" Enabled="false" OnClick="btnImport_Click"/>
                                    <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-success" Text="Search" Enabled="false" OnClick="btnSearch_Click"/>
                                </div>

                                <!-- Success and Failure Messages -->
                                <div id="DivSuccess" runat="server" visible="false" class="alert alert-success">
                                    <asp:Label ID="lblMessageSuccess" runat="server" />
                                </div>
                                <div id="DivFailure" runat="server" visible="false" class="alert alert-danger">
                                    <asp:Label ID="lblMessageError" runat="server" />
                                </div>

                                <%--<style type="text/css">
                                    .redundant-row {
                                        background-color: #f8d7da; /* Light red background */
                                        color: #721c24; /* Dark red text */
                                    }
                                </style>--%>


                                <telerik:RadGrid ID="RadGrid" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true" AllowCustomPaging="true" PageSize="10" OnNeedDataSource="RadGrid_NeedDataSource">
                                    <PagerStyle Mode="NextPrevAndNumeric" />
                                    <HeaderStyle CssClass="MyGridHeader" />
                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                    <ClientSettings Selecting-AllowRowSelect="true">
                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" AllowResizeToFit="true" />
                                        <Selecting AllowRowSelect="True" />
                                    </ClientSettings>
                                    <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader" AllowPaging="True" PagerStyle-AlwaysVisible="True" AllowCustomPaging="True">
                                        <HeaderStyle CssClass="MyGridHeader" />
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                        <Columns>
                                            <telerik:GridBoundColumn DataField="Id" HeaderText="Id" UniqueName="Id" />
                                            <telerik:GridBoundColumn DataField="HouseholdNumber" HeaderText="Household Number" UniqueName="HouseholdNumber" />
                                            <telerik:GridBoundColumn DataField="InterestedInParticipating" HeaderText="Interested In Participating" UniqueName="InterestedInParticipating" />
                                            <telerik:GridBoundColumn DataField="SurvivesTargeting" HeaderText="Survives Targeting" UniqueName="SurvivesTargeting" />
                                            <telerik:GridBoundColumn DataField="TreatmentArm" HeaderText="Treatment Arm" UniqueName="TreatmentArm" />
                                            <telerik:GridBoundColumn DataField="HHEnrollmentStatus" HeaderText="Enrollment Status" UniqueName="HHEnrollmentStatus" />
                                            <telerik:GridBoundColumn DataField="GroupID" HeaderText="Group ID" UniqueName="GroupID" />
                                            <telerik:GridBoundColumn DataField="EnrolledMemberID" HeaderText="Enrolled Member ID" UniqueName="EnrolledMemberID" />
                                            <telerik:GridBoundColumn DataField="ValidationError" HeaderText="Validation Error" UniqueName="ValidationError" Display="false" />
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

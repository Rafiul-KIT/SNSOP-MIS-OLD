<%@ Page Title=":: :: SSSNP : : Household Group View" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="HouseholdGroupView.aspx.cs" Inherits="SSSNP.UI.Public_Works.HouseholdGroupView" %>
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
                                <!-- File Upload for Excel (Disabled) -->
                                <div class="form-group">
                                    <label for="fileUpload" class="control-label">Attachment:</label>
                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" Enabled="false" />
                                </div>

                                <!-- Upload and Download Links -->
                                <div class="form-group">
                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-warning" Text="Upload File" Enabled="false" />
                                    <br /><br />

                                    <!-- Link for Downloading Excel Template -->
                                    <asp:LinkButton ID="lnkDownloadTemplate" runat="server" CssClass="download-link" OnClick="lnkDownloadTemplate_Click">Download Import File Template</asp:LinkButton>

                                </div>

                                <!-- Action Buttons -->
                                <div class="form-group">
                                    <asp:Button ID="btnAnalyze" runat="server" CssClass="btn btn-warning" Text="Analyze" Enabled="false" />
                                    <asp:Button ID="btnImport" runat="server" CssClass="btn btn-success" Text="Import" Enabled="false" />
                                </div>

                                <!-- Grid to Display Data -->
                                <div class="form-group">
                                    <asp:GridView ID="gvData" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="GroupID" HeaderText="Group ID" />
                                            <asp:BoundField DataField="GroupName" HeaderText="Group Name" />
                                            <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                                            <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

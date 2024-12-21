<%@ Page Title=":: :: SNSOP : : Payroll Reconcile" Language="C#" AutoEventWireup="true" CodeBehind="ReconcilePayroll.aspx.cs" MasterPageFile="~/UI/Public Works/CCMaster.master" Inherits="SSSNP.UI.Public_Works.ReconcilePayroll" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
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
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Payroll Reconciliation</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Payroll Reconciliation</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="vertical-align: top; width: 65%;">
                                            <!-- Left Side: State, County -->
                                            <table cellpadding="10" cellspacing="5" style="width: 100%;">
                                                <tr>
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="LabelState" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; width: 66px;">
                                                        <asp:Label ID="LabelCounty" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlCounty" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelReqNo" runat="server" CssClass="LBLBLACK" Width="165px">Request No</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtReqNo" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                            Height="28px" MaxLength="16" TabIndex="1" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="vertical-align: top; width: 35%;">
                                            <!-- Right Side: Payam, Boma -->
                                            <table cellpadding="10" cellspacing="5" style="width: 100%;">                                                
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelPayam" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelBoma" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px;">
                                                        <asp:Label ID="LabelWorkCode" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                    </td>
                                                    <td>:</td>
                                                    <td>
                                                        <asp:TextBox ID="txtWorkCode" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                            Height="28px" MaxLength="16" TabIndex="1" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <!-- Download Button -->
                                            <asp:Button ID="BtnDownload" runat="server" CssClass="btn btn-primary" Text="Download" OnClick="BtnDownload_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <div class="form-group">
                                                <label for="fileUpload" class="control-label">Attachment:</label>

                                                <div class="custom-file-upload">
                                                    <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control-file" Enabled="true" Style="display: none;" onchange="handleFileChange(this)" />

                                                    <button type="button" class="btn btn-primary" onclick="document.getElementById('<%= fileUpload.ClientID %>').click();">Choose File</button>
                                                    <span id="fileInfo" class="file-info">No file chosen</span>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-warning" Text="Upload File" OnClick="btnUpload_Click" />
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" align="center">
                                            <!-- Warning Label -->
                                            <asp:Label ID="LblWarning" runat="server" ForeColor="Red" Visible="False" ></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>   
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                                    function handleFileChange(input) {
                                        var fileName = input.files[0] ? input.files[0].name : "No file chosen";
                                        var fileSize = input.files[0] ? (input.files[0].size / 1024).toFixed(2) + " KB" : "";
                                        var fileInfo = document.getElementById('fileInfo');
                                        fileInfo.innerText = fileName + (fileSize ? " (" + fileSize + ")" : "");
                                    }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="BtnDownload" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

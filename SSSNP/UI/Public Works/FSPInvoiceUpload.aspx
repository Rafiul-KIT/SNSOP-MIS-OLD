<%@ Page Title=":: :: SNSOP : : FSP Invoice Upload" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="FSPInvoiceUpload.aspx.cs" Inherits="SSSNP.UI.Public_Works.FSPInvoiceUpload" %>

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
                        <i class="fa fa-edit"></i><a href="#">FSP Invoice Upload</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">FSP Invoice Upload</h3>
                            </div>
                            <div class="panel-body">
                                <table class="table">
                                    <tr>
                                        <td>
                                            <asp:FileUpload ID="fileUpload" runat="server" CssClass="form-control" OnChange="showFileName()" />
                                            <label id="selectedFileLabel" style="margin-top: 5px; font-weight: bold;"></label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-success" Text="Upload" OnClick="btnUpload_Click" />
                                            <asp:Button ID="btnClear" runat="server" CssClass="btn btn-warning" Text="Clear" OnClick="btnClear_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblMessage" runat="server" CssClass="text-info"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:HyperLink ID="hlPreview" runat="server" CssClass="btn btn-link" Target="_blank" Visible="false">View Uploaded Invoice</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div id="filePreview" style="display: none; margin-top: 10px; border: 1px solid #ddd; padding: 10px;">
                                                <h4>File Preview:</h4>
                                                <div id="previewContent"></div>
                                            </div>
                                        </td>
                                    </tr>
                                </table>  
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function showFileName() {
                    var fileInput = document.getElementById('<%=fileUpload.ClientID %>');
                    var label = document.getElementById('selectedFileLabel');
                    var preview = document.getElementById('filePreview');
                    var previewContent = document.getElementById('previewContent');

                    if (fileInput.files.length > 0) {
                        var file = fileInput.files[0];
                        var fileName = file.name;
                        var fileSize = (file.size / 1024).toFixed(2) + " KB";
                        var fileType = file.type;
                        label.innerText = "Selected File: " + fileName + " (" + fileSize + ")";

                        previewContent.innerHTML = "";
                        var reader = new FileReader();

                        if (fileType.startsWith("image/")) {
                            reader.onload = function (e) {
                                var img = document.createElement("img");
                                img.src = e.target.result;
                                img.style.maxWidth = "100%";
                                img.style.maxHeight = "300px";
                                previewContent.appendChild(img);
                            };
                            reader.readAsDataURL(file);
                        } else if (fileType === "application/pdf") {
                            var iframe = document.createElement("iframe");
                            iframe.src = URL.createObjectURL(file);
                            iframe.style.width = "100%";
                            iframe.style.height = "400px";
                            previewContent.appendChild(iframe);
                        } else if (fileType.startsWith("text/") || fileType === "application/json") {
                            reader.onload = function (e) {
                                var textArea = document.createElement("pre");
                                textArea.innerText = e.target.result;
                                previewContent.appendChild(textArea);
                            };
                            reader.readAsText(file);
                        } else {
                            previewContent.innerHTML = "Preview not available for this file type.";
                        }
                        preview.style.display = "block";
                    } else {
                        label.innerText = "";
                        preview.style.display = "none";
                    }
                }
            </script>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnUpload" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
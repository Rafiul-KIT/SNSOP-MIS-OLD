<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Public Works/CCMaster.master"  CodeBehind="GBVExcelImporting.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.GBVExcelImporting" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    

  <script type="text/javascript">
    

     
  </script>


    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 30px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">GBV Excel Importing
                        </h3>
                    </div>
                    

                        <asp:UpdatePanel runat="server" ID="upPanelMain">
                                <ContentTemplate>
                                    <asp:HiddenField runat="server" ClientIDMode="Static" ID="hdnUploadedFile" Value="0" />

                                    <div class="panel-body">
                                        <table id="tbMain" width="100%" cellpadding="4" cellspacing="5">
                                                                             
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">Select GBV Type</td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlGBVType" AutoPostBack="True" OnSelectedIndexChanged="ddlGBVType_SelectedIndexChanged" >
                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="GBV" Value="GBV"></asp:ListItem>
                                                        <asp:ListItem Text="Non-GBV" Value="NON-GBV"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">
                                                    
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                           
                                            <tr>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">  </td>
                                                <td style="text-align: left; padding: 5px; margin: 5px">
                                                    <i>Attachment(s) can be uploaded only after detail have been saved</i>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <asp:UpdatePanel ID="upAttachment" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <tr>
                                                        <td style="padding: 5px; margin: 5px">Attachment</td>
                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                        <td style="padding: 5px; margin: 5px">
                                                            <asp:FileUpload runat="server" ID="upFile" Height="33px" Enabled="false" ClientIDMode="Static" Width="400px" />
                                                        </td>
                                                        <td colspan="4" style="padding: 5px; margin: 5px">
                                                            <asp:Label runat="server" ID="Label5" Text="" Font-Italic="true" ForeColor="Green"></asp:Label>
                                                        </td>

                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                        <td style="padding: 5px; margin: 5px; text-align: left;">
                                                            <asp:Button runat="server" ID="btnUploadFile" Enabled="false" Text="Upload File" CssClass="btn btn-warning" OnClick="btnUploadFile_Click" />
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px">&nbsp;
                                                            
                                                        </td>
                                                    </tr>

                                                       <tr>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px; text-align: left;">
                                                          <asp:Label runat="server" ID="lblFilenameUploaded" Text="" ForeColor="Green" Font-Italic="true"></asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        <td style="padding: 5px; margin: 5px"></td>
                                                        <td style="padding: 5px; margin: 5px">&nbsp;
                                                            
                                                        </td>
                                                    </tr>
                                                </ContentTemplate>
                                                <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUploadFile" />
                                                </Triggers>
                                            </asp:UpdatePanel>

                                             <tr>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">  </td>
                                                <td style="text-align: left; padding: 5px; margin: 5px">
                                                    <i>Download Import File Template</i>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">  </td>
                                                <td style="text-align: left; padding: 5px; margin: 5px">
                                                    <i><asp:HyperLink runat="server" ID="lnkDownloadTemplateFile" Enabled="false"  Text="No Template File selected" NavigateUrl="~/ViewAttachmentHandler.aspx?AttachmentType=2" Target="_blank"></asp:HyperLink></i>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px"></td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                        </table>
                                        <hr class="blue" />
                            
                                               <table id="tbGridAttachment" width="98%" cellpadding="1" cellspacing="2">
                                            <tr>
                                                <td>
                                                <%--griglia--%>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <%--GRIGLIA--%>

                                                    <telerik:RadGrid ID="RadGridLanding" AutoGenerateColumns="true" runat="server" Visible="true"
                                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                                        MasterTableView-TableLayout="Fixed"
                                                        OnNeedDataSource="RadGridLanding_NeedDataSource" >
                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                        <MasterTableView TableLayout="Fixed"
                                                            DataKeyNames=""
                                                            ClientDataKeyNames="">
                                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                           
                                                            <Columns>
                                                         
                                                               
                                                                                                                               
                                                            </Columns>
                                                        </MasterTableView>
                                                    </telerik:RadGrid>





                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding: 5px; margin: 5px">
                                                    <div id="DivSuccess" runat="server"  clientidmode="Static"  class="alert alert-success" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#"></a> <strong>Success! </strong><asp:Label ID="lblMessageSuccess" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="DivFailure" runat="server" clientidmode="Static" class="alert alert-danger" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#"></a> <strong>Warning! </strong>
                                                        <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>

                                    <div>
                                         <div style="padding: 20px" align="center">
                                            <asp:Button ID="BtnAnalyze" runat="server" Enabled="false" CssClass="btn btn-warning" Height="32px" TabIndex="10" Text="Analyze" Width="120px" OnClick="BtnAnalyze_Click" />
                                            &nbsp; &nbsp; 
                                            <asp:Button ID="btnImport" runat="server" Enabled="false" CssClass="btn btn-success" Height="32px" TabIndex="10" Text="Import" Width="120px" OnClick="btnImport_Click" />
                                            &nbsp; &nbsp; 
                                                                                        
                                        </div>

                                    </div>
                                       
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                </div>
            </td>
        </tr>
    </table>
</asp:Content>


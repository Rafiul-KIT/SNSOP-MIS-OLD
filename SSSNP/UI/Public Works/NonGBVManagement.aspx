<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="NonGBVManagement.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.NonGBVManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">

        function OnClientClose(sender, args) {
            var ajaxManager = $find("<%= RadAjaxManager1.ClientID %>");
            ajaxManager.ajaxRequest();
            return;
        }

        function OnClientClicking(sender, args) {
            var callBackFunction = Function.createDelegate(sender, function (argument) {
                if (argument) {
                    this.click();
                }
            });
            var text = "Are you sure you want to delete the record?";
            radconfirm(text, callBackFunction, 300, 100, null, "Delete");
            args.set_cancel(true);
        }

    </script>

    <style>
        a {
            color: #337ab7;
            text-decoration: none;
            padding: 0px;
        }

        .RadButtonStyle {
            border-color: #ccc;
            color: #333;
            background-color: #fff;
            padding: 6px 10px;
            border-width: 1px;
            border-style: solid;
            border-color: #ccc;
            background-position: 0 0;
            border-radius: 4px;
        }
    </style>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">

        <tr>
            <td style="padding-top: 10px; padding-bottom: 30px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">Non-GBV Grievance Management
                        </h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upd1">
                        <ContentTemplate>
                            <asp:HiddenField runat="server" ID="hfAttach_DescriptionCompliant" Value="" />
                            <asp:HiddenField runat="server" ID="hfAttach_DescriptionCompliantID" Value="0" />

                            <telerik:RadAjaxManager ID="RadAjaxManager1" ClientIDMode="Static" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest"></telerik:RadAjaxManager>

                            <div class="panel-body">
                                <table id="tbMain" runat="server" width="100%" cellpadding="4" cellspacing="5">
                                    <tr>
                                        <td colspan="8">
                                            <asp:Label ID="lblSNNote" runat="server" CssClass="LBLBLACK" ForeColor="Red">Please, carefully write on the paper form the Serial Number generated for the Grievance</asp:Label>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px;">
                                            <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">Serial Number</asp:Label>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="false" runat="server" ID="txtSerialNumber" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="128px">Date</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="false" runat="server" ID="txtDate" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">State Name</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlState" AutoPostBack="True" OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="128px">Payam/Block Name</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Enabled="false" Height="33px" Width="250px" runat="server" ID="ddlPayam" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">County/Municipality Name</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="250px" Enabled="false" runat="server" ID="ddlCounty" AutoPostBack="True" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="128px">Boma/Quarter Council Name</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Enabled="false" Width="250px" runat="server" ID="ddlBoma" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="128px">Description of Complaint / Allegation / Issue</asp:Label>

                                            <span style="text-align: left; color: red;">*</span>
                                            <br />
                                            <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Width="128px" Font-Size="10px">(Include English translation of local language submission if applicable)</asp:Label>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="133px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtDescriptionOfComplaint" Text=""></asp:TextBox>

                                        </td>
                                        <td colspan="4" style="top: 0px; padding: 5px; margin: 5px; text-align: left;">Attachment:
                                                 <div>
                                                     <asp:Panel runat="server" ID="pnlAttachDescriptionComplaint" Visible="true" Width="400px">
                                                         <asp:UpdatePanel ID="upAttachmentDescriptionComplaint" runat="server" UpdateMode="Conditional">
                                                             <ContentTemplate>
                                                                 <asp:FileUpload runat="server" ID="fuDescriptionComplaint" Height="33px" Enabled="true" ClientIDMode="Static" Width="400px" />
                                                             </ContentTemplate>
                                                             <Triggers>
                                                                 <asp:PostBackTrigger ControlID="BtnUploadDescriptionComplaint" />
                                                             </Triggers>

                                                         </asp:UpdatePanel>

                                                         <asp:Button runat="server" ID="BtnUploadDescriptionComplaint" Text="Upload" Enabled="true" CssClass="btn btn-warning" OnClick="BtnUploadDescriptionComplaint_Click" />
                                                         &nbsp;
                                                           <asp:Button runat="server" ID="btnDownloadDescriptionComplaint" Text="Download" Enabled="false" CssClass="btn btn-success" OnClick="btnDownloadDescriptionComplaint_Click" />

                                                         &nbsp;
                                                    <asp:Button ID="btnRemoveDescriptionComplaint" runat="server" CssClass="btn btn-danger" Height="32px" Enabled="false" OnClientClick="return confirm('Do you want to delete the attach ? ');" TabIndex="10" Text="Delete" OnClick="btnRemoveDescriptionComplaint_Click" />
                                                         <br />
                                                         <asp:Label runat="server" ID="lblDescriptionCompliantFilename" Text="" ForeColor="Red"></asp:Label>
                                                     </asp:Panel>
                                                 </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="128px">Reporting Channel</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlReportingChannel" AutoPostBack="True" OnSelectedIndexChanged="ddlReportingChannel_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="County/Municipality Core Team" Value="County/Municipality Core Team"></asp:ListItem>
                                                <asp:ListItem Text="Payam/Block DC" Value="Payam/Block DC"></asp:ListItem>
                                                <asp:ListItem Text="Boma/QCDC" Value="Boma/QCDC"></asp:ListItem>
                                                <asp:ListItem Text="Appeals Committee" Value="Appeals Committee"></asp:ListItem>
                                                <asp:ListItem Text="CST" Value="CST"></asp:ListItem>
                                                <asp:ListItem Text="UNOPS" Value="UNOPS"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">Other Reporting Channel</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="false" runat="server" ID="txtOtherReportingChannel" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="128px">Date Complaint</asp:Label>
                                            <span style="color: red;">*</span></td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" placeholder="mm/DD/yyyy" ID="txtDateCompliant" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDateCompliant_CalendarExtender" runat="server"
                                                Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtDateCompliant" OnPreRender="txtDateCompliant_CalendarExtender_PreRender"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="filterDateCompliant" runat="server" Enabled="True" TargetControlID="txtDateCompliant" FilterType="Numbers, Custom" ValidChars="/" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="128px">Contact Number</asp:Label>
                                            <%--<span style="color: red;">*</span>--%></td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="txtContractNumber" Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtContactNumberNumberFilter" runat="server" Enabled="True" TargetControlID="txtContractNumber" FilterType="Numbers" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <%--    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="128px">Type of complaint</asp:Label>
                                             <span style="color: red;">*</span></td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                             <asp:DropDownList class="form-control dropdown" Visible="true" Height="33px" Width="250px" runat="server" ID="ddlTypeOfComplaint" AutoPostBack="True">
                                                  <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                             </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>--%>

                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="128px">Complainant&#39;s Name</asp:Label>
                                            <%--<span style="color: red;">*</span>--%>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="txtComplaiantName" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">Gender</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="180px" runat="server" ID="ddlGender">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="Male"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="Female"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">Physical Address</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="txtPhysicalAddress" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">SNSOP Beneficiary</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="180px" runat="server" ID="ddlSNSOPBeneficiary" AutoPostBack="True" OnSelectedIndexChanged="ddlSNSOPBeneficiary_SelectedIndexChanged">
                                                <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="2"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">Age</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" MaxLength="3" Height="33px" Width="180px" runat="server" ID="txtAge" Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtAgeNumberFilter" runat="server" Enabled="True" TargetControlID="txtAge" FilterType="Numbers" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>

                                            <%--  <asp:RangeValidator ID="rvAge" ValidateRequestMode="Enabled" runat="server" ErrorMessage="Age value not valid" 
                                            forecolor="Red" ControlExtender="txtAgeNumberFilter" controltovalidate="txtAge" 
                                    minimumvalue="5" maximumvalue="42" type="Integer"></asp:RangeValidator> --%>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px" id="supportTypeLbl">Support Type</td>
                                        <td style="padding: 5px; margin: 5px" id="supportTypeColonLbl">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="180px" runat="server" ID="ddlSupportType">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="LIPW" Value="LIPW"></asp:ListItem>
                                                <asp:ListItem Text="DIS" Value="DIS"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="128px">Category of Grievance</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="250px" runat="server" ID="ddlTypeOfGrievance" AutoPostBack="True" OnSelectedIndexChanged="ddlTypeOfGrievance_SelectedIndexChanged">
                                                <asp:ListItem Text=" --Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                        <td style="padding: 5px; margin: 5px">Type of Grievance</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Enabled="false" Width="250px" runat="server" ID="ddlTypeOfGrievanceDetail" AutoPostBack="True" OnSelectedIndexChanged="ddlTypeOfGrievanceDetail_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">

                                            <%--<asp:DropDownList class="form-control dropdown" Visible="true" Height="33px" Width="250px" runat="server" ID="ddlCategoryOfGrievance" AutoPostBack="True">
                                                       <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                       <asp:ListItem Text="Targeting/Enrollment" Value="Targeting/Enrollment"></asp:ListItem>
                                                      <asp:ListItem Text="Payment" Value="Payment"></asp:ListItem>
                                                      <asp:ListItem Text="LIPWs" Value="LIPWs"></asp:ListItem>
                                                      <asp:ListItem Text="Question/Query" Value="Question/Query"></asp:ListItem>

                                              </asp:DropDownList>--%>

                                            <asp:DropDownList class="form-control dropdown" Visible="false" Height="33px" Width="180px" runat="server" ID="ddlStatus" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">Type Of Grievance (Other)</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="false" runat="server" ID="txtTypeOfGrievanceOther" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>
                                <div style="padding: 20px" align="center">
                                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success"
                                        Height="32px" TabIndex="10" Text="Save" Width="120px" OnClientClick="return preventMultipleSubmissions();" OnClick="BtnSave_Click" />
                                    &nbsp; &nbsp;                                         
                                            <asp:Button ID="BtnDelete" Enabled="false" runat="server" CssClass="btn btn-danger" Height="32px" OnClientClick="return confirm('Do you want to delete the record ? ');" TabIndex="10" Text="Delete" Width="120px" OnClick="BtnDelete_Click" />

                                    &nbsp; &nbsp;                                         
                                            <asp:Button ID="BtnHome" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="Search" Width="120px" OnClick="BtnHome_Click" />

                                    <asp:CustomValidator runat="server" ID="valCustom" OnServerValidate="valCustom_ServerValidate" ControlToValidate="txtAge" ErrorMessage=""></asp:CustomValidator>

                                </div>
                            </div>
                            <table id="tbDetailsList" width="98%" cellpadding="4" cellspacing="5">
                                <tr align="center">
                                    <td>
                                        <telerik:RadGrid ID="RadGridDetails" AutoGenerateColumns="false" runat="server" Visible="false"
                                            AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both" ClientIDMode="Static"
                                            MasterTableView-TableLayout="Fixed"
                                            OnNeedDataSource="RadGridDetails_NeedDataSource" OnItemDataBound="RadGridDetails_ItemDataBound">
                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                            <MasterTableView TableLayout="Fixed"
                                                DataKeyNames="NonGBVDetailID,Status"
                                                ClientDataKeyNames="NonGBVDetailID,Status">
                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>

                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="NonGBVDetailID" HeaderText="NonGBVDetailID" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                                    <%--<telerik:GridBoundColumn DataField="DescriptionOfComplaint" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Description of Complaint" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%"></telerik:GridBoundColumn>--%>
                                                    <telerik:GridBoundColumn DataField="ActionResponseComplaint" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Action in Response" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="50%"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="FeedbackComplainant" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Feeback from Complainant" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="DataOfResolution" Display="true" ItemStyle-HorizontalAlign="left" HeaderText="Resolution Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
                                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="EditColumn"
                                                        HeaderStyle-Width="100px" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <telerik:RadButton runat="server" ID="btnEditDetail" OnClick="btnEditDetail_Click" CssClass="RadButtonStyle">
                                                                <ContentTemplate>
                                                                    <i class="fa fa-pencil"></i>
                                                                </ContentTemplate>
                                                            </telerik:RadButton>

                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>

                                                    <telerik:GridTemplateColumn HeaderText="" UniqueName="DeleteColumn"
                                                        HeaderStyle-Width="100px" ItemStyle-Width="150px">
                                                        <ItemTemplate>
                                                            <telerik:RadButton runat="server" ID="btnDeleteDetail" OnClientClicking="OnClientClicking" OnClick="btnDelateDetail_Click" CssClass="RadButtonStyle">
                                                                <ContentTemplate>
                                                                    <i class="fa fa-trash"></i>
                                                                </ContentTemplate>
                                                            </telerik:RadButton>
                                                        </ItemTemplate>
                                                    </telerik:GridTemplateColumn>

                                                </Columns>
                                            </MasterTableView>
                                        </telerik:RadGrid>


                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="padding: 5px; margin: 5px">
                                        <div id="DivSuccess" runat="server" clientidmode="Static" class="alert alert-success" visible="false">
                                            <a aria-label="close" class="close" data-dismiss="alert" href="#"></a><strong>Success!</strong><asp:Label ID="lblMessageSuccess" runat="server"></asp:Label>
                                        </div>
                                        <div id="DivFailure" runat="server" clientidmode="Static" class="alert alert-danger" visible="false">
                                            <a aria-label="close" class="close" data-dismiss="alert" href="#"></a><strong>Warning!</strong>
                                            <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>

                                <tr align="center">
                                    <td>
                                        <asp:Button ID="btnNewDetails" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="Complaint processing" Width="195px"
                                            OnClick="btnNewDetails_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>


                            <telerik:RadWindowManager ID="RadWindowManager1" Title="Non-GBV Detail" ClientIDMode="Static" Width="1024" Height="789" Modal="true" runat="server" DestroyOnClose="true"
                                OnClientClose="OnClientClose">
                            </telerik:RadWindowManager>


                        </ContentTemplate>
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
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

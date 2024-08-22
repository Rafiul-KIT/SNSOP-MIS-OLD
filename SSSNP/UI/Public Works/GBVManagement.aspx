<%@ Page Title="" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="GBVManagement.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.GBVManagement" %>

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
                        <h3 class="panel-title" id="h3" runat="server">GBV Management
                        </h3>
                    </div>
                    <asp:UpdatePanel runat="server" ID="upd1">
                        <ContentTemplate>

                            <asp:HiddenField runat="server" ID="hfAttach_DescriptionIncident" Value="" />
                            <asp:HiddenField runat="server" ID="hfAttach_DescriptionIncidentID" Value="0" />

                            <asp:HiddenField runat="server" ID="hfAttach_ConsentSigned" Value="" />
                            <asp:HiddenField runat="server" ID="hfAttach_ConsentSignedID" Value="0" />

                            <telerik:RadAjaxManager ID="RadAjaxManager1" ClientIDMode="Static" runat="server" OnAjaxRequest="RadAjaxManager1_AjaxRequest"></telerik:RadAjaxManager>

                            <div class="panel-body">
                                <table id="tbMain" runat="server" width="100%" cellpadding="4" cellspacing="5">
                                    <tr>
                                        <td colspan="8">
                                            <asp:Label ID="lblSNNote" runat="server" CssClass="LBLBLACK" ForeColor="Red">Please, carefully write on the paper form the Serial Number generated for the Grievance</asp:Label></td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">Serial Number</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="true" runat="server" ID="txtSerialNumber" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="128px">Date</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="true" runat="server" ID="txtDate" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">State Name</asp:Label>
                                            <span style="text-align: left; color: red;">*</span>
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
                                            <span style="text-align: left; color: red;">*</span>
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
                                            <span style="text-align: left; color: red;">*</span>
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
                                            <span style="text-align: left; color: red;">*</span>
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
                                        <td style="padding: 5px; margin: 5px">Date of Incident <span style="text-align: left; color: red;">*</span></td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" placeholder="mm/DD/yyyy" ID="txtDateofIncident" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDateOfIncident_CalendarExtender" runat="server"
                                                Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtDateofIncident" OnPreRender="txtDateOfIncident_CalendarExtender_PreRender"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="filterDateOfIncident" runat="server" Enabled="True" TargetControlID="txtDateofincident" FilterType="Numbers, Custom" ValidChars="/" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">Contact Focal Person Name <span style="text-align: left; color: red;">*</span></td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" Enabled="true" runat="server" ID="txtContfocalPersName" Text=""></asp:TextBox>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px"><span style="text-align: left; color: red;"></span></td>
                                        <td style="padding: 5px; margin: 5px"></td>

                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="128px">Description of Incident</asp:Label>
                                            <span style="text-align: left; color: red;">*</span>
                                            <br />
                                            <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Width="128px" Font-Size="10px">(In local language and English/language it's translated into)</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="133px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtDescriptionOfIncident" Text=""></asp:TextBox>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td colspan="4" style="top: 0px; padding: 5px; margin: 5px; text-align: left;">Attach document if the complaint is in written form:
                                                 <div>
                                                     <asp:Panel runat="server" ID="pnlAttachDescriptionIncident" Visible="true" Width="400px">
                                                         <asp:UpdatePanel ID="upAttachmentDescriptionIncident" runat="server" UpdateMode="Conditional">
                                                             <ContentTemplate>
                                                                 <asp:FileUpload runat="server" ID="fuDescriptionIncident" Height="33px" Enabled="true" ClientIDMode="Static" Width="400px" />
                                                             </ContentTemplate>
                                                             <Triggers>
                                                                 <asp:PostBackTrigger ControlID="BtnUploadDescriptionIncident" />
                                                             </Triggers>

                                                         </asp:UpdatePanel>

                                                         <asp:Button runat="server" ID="BtnUploadDescriptionIncident" Text="Upload" Enabled="true" CssClass="btn btn-warning" OnClick="BtnUploadDescriptionIncident_Click" />
                                                         &nbsp;
                                                           <asp:Button runat="server" ID="btnDownloadDescriptionIncident" Text="Download" Enabled="false" CssClass="btn btn-success" OnClick="btnDownloadDescriptionIncident_Click" />

                                                         &nbsp;
                                                    <asp:Button ID="btnRemoveDescriptionIncident" runat="server" CssClass="btn btn-danger" Height="32px" Enabled="false" OnClientClick="return confirm('Do you want to delete the attach ? ');" TabIndex="10" Text="Delete" OnClick="btnRemoveDescriptionIncident_Click" />
                                                         <br />
                                                         <asp:Label runat="server" ID="lblDescriptionIncidentFilename" Text="" ForeColor="Red"></asp:Label>
                                                     </asp:Panel>

                                                 </div>


                                        </td>


                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="128px">Incident Reported By</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlIncidentReportedBy" AutoPostBack="True" OnSelectedIndexChanged="ddlIncidentReportedBy_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Survivor" Value="Survivor"></asp:ListItem>
                                                <asp:ListItem Text="Family Member" Value="Family Member"></asp:ListItem>
                                                <asp:ListItem Text="Friend" Value="Friend"></asp:ListItem>
                                                <asp:ListItem Text="Caregiver/Guardian" Value="Caregiver/Guardian"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="128px">Incident reported by (Other)</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" Enabled="false" runat="server" ID="txtOtherIncidentReportedBy" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label18" runat="server" CssClass="LBLBLACK" Width="128px">Reporting Channel</asp:Label>
                                            <span style="color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlReportingChannel" AutoPostBack="True" OnSelectedIndexChanged="ddlReportingChannel_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="GBV Focal point" Value="GBV Focal point"></asp:ListItem>
                                                <asp:ListItem Text="Helpline" Value="Helpline"></asp:ListItem>
                                                <asp:ListItem Text="UNOPS" Value="UNOPS"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>
                                                <%--<asp:ListItem Text="County/Municipality Core Team" Value="County/Municipality Core Team"></asp:ListItem>
                                                <asp:ListItem Text="Appeals Committee" Value="Appeals Committee"></asp:ListItem>
                                                <asp:ListItem Text="Payam/Block DC" Value="Payam/Block DC"></asp:ListItem>
                                                <asp:ListItem Text="Boma/QCDC" Value="Boma/QCDC"></asp:ListItem>
                                                <asp:ListItem Text="CST" Value="CST"></asp:ListItem>                                                
                                                <asp:ListItem Text="BAC" Value="BAC"></asp:ListItem>
                                                <asp:ListItem Text="Direct Call" Value="Direct Call"></asp:ListItem>
                                                <asp:ListItem Text="QCAC" Value="QCAC"></asp:ListItem>
                                                <asp:ListItem Text="QCAC & Direct Call" Value="QCAC & Direct Call"></asp:ListItem>
                                                <asp:ListItem Text="Telephone call" Value="Telephone call"></asp:ListItem>
                                                <asp:ListItem Text="UNOPS HSSE Personnel" Value="UNOPS HSSE Personnel"></asp:ListItem>--%>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">Other Reporting Channel</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" Enabled="false" runat="server" ID="txtOtherReportingChannel" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="128px">Contact Number <%--<span style="text-align: left; color: red;">*</span>--%></asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="true" runat="server" ID="txtContactNumber" Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtContactNumberNumberFilter" runat="server" Enabled="True" TargetControlID="txtContactNumber" FilterType="Numbers" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="128px">Gender <span style="text-align: left; color: red;">*</span></asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlGender" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                                <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            </asp:DropDownList>

                                        </td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label12" runat="server" CssClass="LBLBLACK" Width="128px">Age of Survivor <span style="text-align: left; color: red;">*</span></asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" Enabled="true" runat="server" ID="txtAge" Text=""></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="txtAgeNumberFilter" runat="server" Enabled="True" TargetControlID="txtAge" FilterType="Numbers" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="128px">SNSOP Beneficiary</asp:Label>
                                            <span style="text-align: left; color: red;">*</span>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlSSSNPBeneficiary" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <%--<asp:ListItem Text="No" Value="NO"></asp:ListItem>--%>
                                                <asp:ListItem Text="LIPW" Value="LIPW"></asp:ListItem>
                                                <asp:ListItem Text="DIS" Value="DIS"></asp:ListItem>
                                            </asp:DropDownList>

                                        </td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label14" runat="server" CssClass="LBLBLACK" Width="128px">Suspect Associated with Project</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlSuspect" AutoPostBack="True" OnSelectedIndexChanged="ddlSuspect_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">Available Information about the suspected perpetrator
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" TextMode="MultiLine" Height="80px" Width="250px" Enabled="false" runat="server" ID="txtSuspectNotes" Text=""></asp:TextBox>

                                        </td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label16" runat="server" CssClass="LBLBLACK" Width="128px">Suspect Known?</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlSuspectKnown" AutoPostBack="True" OnSelectedIndexChanged="ddlSuspectKnown_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label17" runat="server" CssClass="LBLBLACK" Width="128px">Suspect's details (name...)</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" Enabled="false" runat="server" ID="txtSuspectDetails" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>




                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label15" runat="server" CssClass="LBLBLACK" Width="128px">Referral Services Recommended?</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control dropdown" Height="33px" Width="250px" Enabled="true" runat="server" ID="ddlReferralServiceRecommended" AutoPostBack="True" OnSelectedIndexChanged="ddlReferralServiceRecommended_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="No" Value="NO"></asp:ListItem>
                                                <asp:ListItem Text="GBV Case Management" Value="GBV Case Management"></asp:ListItem>
                                                <asp:ListItem Text="Protection Services" Value="Protection Services"></asp:ListItem>
                                                <asp:ListItem Text="Medical Services" Value="Medical Services"></asp:ListItem>
                                                <asp:ListItem Text="Psychosocial Services" Value="Psychosocial Services"></asp:ListItem>
                                                <asp:ListItem Text="Legal assistances" Value="Legal assistances"></asp:ListItem>
                                                <asp:ListItem Text="Safe Space" Value="Safe Space"></asp:ListItem>
                                                <asp:ListItem Text="Other" Value="Other"></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">Referral Services Recommended Details</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" Enabled="false" runat="server" ID="txtRecommendedDetails" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label19" runat="server" CssClass="LBLBLACK" Width="128px">Status After Referral</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="250px" Enabled="true" runat="server" ID="txtStatusAfterReferral" Text=""></asp:TextBox>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label21" runat="server" CssClass="LBLBLACK" Width="128px">Verbal consent granted</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">

                                            <asp:DropDownList ID="ddlVerbalConsent" runat="server" AutoPostBack="True" class="form-control txtbox" Height="33px" Width="180px">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>

                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                    <tr>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="128px">Consent Signed?</asp:Label>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlConsentSigned" AutoPostBack="True" OnSelectedIndexChanged="ddlConsentSigned_SelectedIndexChanged">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp; </td>

                                        <td colspan="4" style="top: 0px; padding: 5px; margin: 5px; text-align: left;">Please attach consent form:
                                                 <div>
                                                     <asp:Panel runat="server" ID="pnlAttachConsentSigned" Visible="true" Width="400px">
                                                         <asp:UpdatePanel ID="upAttachConsentSigned" runat="server" UpdateMode="Conditional">
                                                             <ContentTemplate>
                                                                 <asp:FileUpload runat="server" ID="fuAttachConsentSigned" Height="33px" Enabled="False" ClientIDMode="Static" Width="400px" />
                                                             </ContentTemplate>
                                                             <Triggers>
                                                                 <asp:PostBackTrigger ControlID="BtnUploadConsentSigned" />
                                                             </Triggers>

                                                         </asp:UpdatePanel>

                                                         <asp:Button runat="server" ID="BtnUploadConsentSigned" Text="Upload" Enabled="False" CssClass="btn btn-warning" OnClick="BtnUploadConsentSigned_Click" />
                                                         &nbsp;
                                                           <asp:Button runat="server" ID="BtnDownloadAttachConsentSigned" Text="Download" Enabled="false" CssClass="btn btn-success" OnClick="BtnDownloadAttachConsentSigned_Click" />

                                                         &nbsp;
                                                    <asp:Button ID="BtnRemoveAttachConsentSigned" runat="server" CssClass="btn btn-danger" Height="32px" Enabled="false" OnClientClick="return confirm('Do you want to delete the attach ? ');" TabIndex="10" Text="Delete" OnClick="BtnRemoveAttachConsentSigned_Click" />
                                                         <br />
                                                         <asp:Label runat="server" ID="lblAttachConsentSignedFilename" Text="" ForeColor="Red"></asp:Label>
                                                     </asp:Panel>

                                                 </div>


                                        </td>

                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px">Status <span style="text-align: left; color: red;">*</span></td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:DropDownList class="form-control txtbox" Height="33px" Width="250px" runat="server" ID="ddlStatus" AutoPostBack="True">
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px"></td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>


                                </table>

                                <hr class="blue" />

                                <div style="padding: 20px" align="center">
                                    <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" Height="32px" TabIndex="10" Text="Save" Width="120px" OnClick="BtnSave_Click" />
                                    &nbsp; &nbsp;                                         
                                            <asp:Button ID="BtnDelete" Enabled="false" runat="server" CssClass="btn btn-danger" Height="32px" OnClientClick="return confirm('Do you want to delete the record ? ');" TabIndex="10" Text="Delete" Width="120px" OnClick="BtnDelete_Click" />

                                    &nbsp; &nbsp;                                         
                                            <asp:Button ID="BtnHome" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="Search" Width="120px" OnClick="BtnHome_Click" />

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
                                                DataKeyNames="GBVDetailID"
                                                ClientDataKeyNames="GBVDetailID">
                                                <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>

                                                <Columns>
                                                    <telerik:GridBoundColumn DataField="GBVDetailID" HeaderText="GBVDetailID" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                                    <%--<telerik:GridBoundColumn DataField="DescriptionOfTheIncident" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Description Of the Incident" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="40%"></telerik:GridBoundColumn>--%>
                                                    <telerik:GridBoundColumn DataField="FollowUpActionFeedback" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Follow Up Action Feedback" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="55%"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="ReportedToManagementHR" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Reported To Management/HR" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                                    <telerik:GridBoundColumn DataField="IsSurvivorSatisfied" Display="true" ItemStyle-HorizontalAlign="Left" HeaderText="Is Survivor Satisfied" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="20%"></telerik:GridBoundColumn>

                                                    <telerik:GridBoundColumn DataField="ClosingDateAfterManagementAction" Display="true" ItemStyle-HorizontalAlign="left" HeaderText="Closing Date" HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="150px" DataFormatString="{0:dd/MM/yyyy}"></telerik:GridBoundColumn>
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
                                        <div id="DivSuccess" runat="server" class="alert alert-success" visible="false">
                                            <a aria-label="close" class="close" data-dismiss="alert" href="#"></a><strong>Success! </strong>
                                            <asp:Label ID="lblMessageSuccess" runat="server"></asp:Label>
                                        </div>
                                        <div id="DivFailure" runat="server" class="alert alert-danger" visible="false">
                                            <a aria-label="close" class="close" data-dismiss="alert" href="#"></a><strong>Warning! </strong>
                                            <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                                        </div>
                                    </td>
                                </tr>

                                <tr align="center">
                                    <td>
                                        <asp:Button ID="btnNewDetails" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="Grievance Processing" Width="180px"
                                            OnClick="btnNewDetails_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                            </table>


                            <telerik:RadWindowManager ID="RadWindowManager1" Title="GBV Detail" ClientIDMode="Static" Width="1024" Height="789" Modal="true" runat="server" DestroyOnClose="true"
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

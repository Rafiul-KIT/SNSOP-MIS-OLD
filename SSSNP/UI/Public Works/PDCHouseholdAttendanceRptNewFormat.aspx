<%@ Page Title=":: :: SNSOP : : Attendance Report" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="PDCHouseholdAttendanceRptNewFormat" CodeBehind="PDCHouseholdAttendanceRptNewFormat.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);
            $('#txtStartDate').datetimepicker();
        });
    </script>
    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>
    <script type="text/javascript" language="javascript">
        function OpenPopup() {
            window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");
            return false;
        }

        function ApplyChanges(val, val1) {
            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;
            document.forms[0].submit();
        }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>PDC
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Beneficiary Attendance Report</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary Attendance Report</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False"
                                                Font-Size="12px" Style="position: static" ToolTip="Rate Lookup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                            Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK"
                                                            Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="TxtFromDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate2_CalendarExtender" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                            ControlToValidate="TxtFromDate" ErrorMessage="Please Enter From Date"
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="TxtToDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="TxtToDate" TargetControlID="TxtToDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                            ControlToValidate="TxtToDate" ErrorMessage="Please Enter To Date"
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="165px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBeneficiary" AutoPostBack="True"
                                                            OnSelectedIndexChanged="BeneficiarySelect" runat="server"
                                                            class="form-control txtbox" Height="28px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Group">Group</asp:ListItem>
                                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr runat="server" id="trGroup" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="160px">Group List</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        <asp:DropDownList ID="ddlGroupList" runat="server" OnSelectedIndexChanged="ddlGroupList_SelectedIndexChanged"
                                                            class="form-control txtbox" Height="28px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="01">01</asp:ListItem>
                                                            <asp:ListItem Value="02">02</asp:ListItem>
                                                            <asp:ListItem Value="03">03</asp:ListItem>
                                                            <asp:ListItem Value="04">04</asp:ListItem>
                                                            <asp:ListItem Value="05">05</asp:ListItem>
                                                            <asp:ListItem Value="06">06</asp:ListItem>
                                                            <asp:ListItem Value="07">07</asp:ListItem>
                                                            <asp:ListItem Value="08">08</asp:ListItem>
                                                            <asp:ListItem Value="09">09</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="16">16</asp:ListItem>
                                                            <asp:ListItem Value="17">17</asp:ListItem>
                                                            <asp:ListItem Value="18">18</asp:ListItem>
                                                            <asp:ListItem Value="19">19</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="21">21</asp:ListItem>
                                                            <asp:ListItem Value="22">22</asp:ListItem>
                                                            <asp:ListItem Value="23">23</asp:ListItem>
                                                            <asp:ListItem Value="24">24</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="26">26</asp:ListItem>
                                                            <asp:ListItem Value="27">27</asp:ListItem>
                                                            <asp:ListItem Value="28">28</asp:ListItem>
                                                            <asp:ListItem Value="23">29</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="31">31</asp:ListItem>
                                                            <asp:ListItem Value="32">32</asp:ListItem>
                                                            <asp:ListItem Value="33">33</asp:ListItem>
                                                            <asp:ListItem Value="34">34</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="36">36</asp:ListItem>
                                                            <asp:ListItem Value="37">37</asp:ListItem>
                                                            <asp:ListItem Value="38">38</asp:ListItem>
                                                            <asp:ListItem Value="39">39</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="41">41</asp:ListItem>
                                                            <asp:ListItem Value="42">42</asp:ListItem>
                                                            <asp:ListItem Value="43">43</asp:ListItem>
                                                            <asp:ListItem Value="44">44</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="46">46</asp:ListItem>
                                                            <asp:ListItem Value="47">47</asp:ListItem>
                                                            <asp:ListItem Value="48">48</asp:ListItem>
                                                            <asp:ListItem Value="49">49</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdGroupId" runat="server" />
                                                        <asp:HiddenField ID="hdGroupNo" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;"></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnSave3_Click" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave4" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Cancel"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<%--<asp:Button ID="btnexp" runat="server" CausesValidation="False"
                                                CssClass="btn btn-primary" Font-Size="12px" Height="32px"
                                                OnClick="btnexp_Click" Style="position: static" Text="Export"
                                                ValidationGroup="vg" Visbile="false"/>--%>
                                            &nbsp;<asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" OnClientClick="return PrintPanel();" ToolTip="Export Excel Sheet" TabIndex="10" Text="Print"
                                                ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <div runat="server" id="div_print">
                                                <asp:Panel ID="pnlContents" runat="server">

                                                    <table runat="server" id="tblreport" visible="false" style="padding: 5px">
                                                        <tr>
                                                            <td align="left" valign="top">
                                                                <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                            <asp:Label ID="lblProject0" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                                                Width="165px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                            <asp:Label ID="lblWorkCode0" runat="server" CssClass="LBLBLACK"
                                                                                Font-Bold="True" Width="165px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label432" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                            <asp:Label ID="lblWorkStartDate0" runat="server" CssClass="LBLBLACK"
                                                                                Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label433" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                            <asp:Label ID="lblfromdate" runat="server" CssClass="LBLBLACK"
                                                                                Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label434" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                            <asp:Label ID="lbltodate" runat="server" CssClass="LBLBLACK"
                                                                                Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                            <td></td>
                                                            <td align="left" valign="top">
                                                                <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="lblState0" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label436" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="lblCounty0" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label437" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="lblPayam0" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label438" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="lblBoma0" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="Label439" runat="server" CssClass="LBLBLACK" Width="165px">Total Beneficiaries</asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px">:</td>
                                                                        <td style="padding: 5px; margin: 5px">
                                                                            <asp:Label ID="lbltotalBenf" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                                    </tr>
                                                                    <tr id="trGroup0" runat="server" visible="false">
                                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                                        <td style="padding: 5px; margin: 5px;">
                                                                            <asp:HiddenField ID="hdGroupId0" runat="server" />
                                                                            <asp:HiddenField ID="hdGroupNo0" runat="server" />
                                                                        </td>
                                                                        <td style="padding: 5px; margin: 5px; width: 10px;"></td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadGrid ID="rgAttendanceReport" PageSize="50" AutoGenerateColumns="true" runat="server" AllowPaging="true"
                                                                    AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                                    MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                                                    OnNeedDataSource="rgAttendanceReport_NeedDataSource" OnItemDataBound="rgAttendanceReport_ItemDataBound" OnItemCommand="rgAttendanceReport_ItemCommand">
                                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                    <ClientSettings Selecting-AllowRowSelect="true">
                                                                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                                                        <Selecting AllowRowSelect="True" />
                                                                    </ClientSettings>
                                                                    <MasterTableView TableLayout="Fixed"
                                                                        DataKeyNames=""
                                                                        ClientDataKeyNames="">
                                                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                        <Columns>
                                                                            <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                                                <ItemTemplate>
                                                                                    <asp:Label ID="rowNumberLabel" runat="server" />
                                                                                </ItemTemplate>
                                                                                <HeaderStyle Width="50px" />
                                                                            </telerik:GridTemplateColumn>
                                                                        </Columns>
                                                                    </MasterTableView>
                                                                </telerik:RadGrid>
                                                            </td>
                                                        </tr>
                                                    </table>

                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="lblmessage" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="btnexp" />
        </Triggers>--%>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

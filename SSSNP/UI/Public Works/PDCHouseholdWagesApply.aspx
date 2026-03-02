<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" EnableEventValidation="true"
    AutoEventWireup="true" Inherits="PDCHouseholdWagesApply" Codebehind="PDCHouseholdWagesApply.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript">
         //<![CDATA[
         <%--function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
             for (i = 1; i < GridView2.rows.length; i++) {
                 if (GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].disabled) {
                     GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = false;
                 }
                 else {
                     GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
                 }
             }
         }--%>

         //]]>
    </script>

    <script>
    $(document).ready(function() {

        var rigtDynHt = $('.form-left-pan').height();
        //alert(rigtDynHt);
        $('.form-right-pan').css("height", rigtDynHt);

        $('#txtStartDate').datetimepicker();
    });
    </script>

        <script type="text/javascript">
            function CheckAll(id) {
                var masterTable = $find("<%= rgAttendances.ClientID %>").get_masterTableView();
                var row = masterTable.get_dataItems();
                if (id.checked == true) {
                    for (var i = 0; i < row.length; i++) {
                        masterTable.get_dataItems()[i].findElement("chkSelected").checked = true; // for checking the checkboxes
                    }
                }
                else {
                    for (var i = 0; i < row.length; i++) {
                        masterTable.get_dataItems()[i].findElement("chkSelected").checked = false; // for unchecking the checkboxes
                    }
                }
            }
            function unCheckHeader(id) {
                var masterTable = $find("<%= rgAttendances.ClientID %>").get_masterTableView();
                var row = masterTable.get_dataItems();
                if (id.checked == false) {
                    var hidden = document.getElementById("HiddenField1");
                    var checkBox = document.getElementById(hidden.value);
                    checkBox.checked = false;
                }
            }
        </script>

    <script type="text/javascript" language="javascript">

function OpenPopup() 
 
   {

       window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

    return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>
    <script type="text/javascript">
        function() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <%--
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here &nbsp;!&nbsp; &nbsp; &nbsp;
                    <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
                    <li class=""><i class="fa fa-fw fa-desktop"></i>PDC </li>
                    <li class="active"><i class="fa fa-edit"></i><a href="#">Apply for Wages Payment</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Apply for Wages Payment</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" Font-Size="12px"
                                                Style="position: static" ToolTip="Rate Lookup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        &nbsp;</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Wage 
                                                        Rate(USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="txtAmount" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                            Height="28px" MaxLength="4" onkeypress="NumberOnly()" OnTextChanged="txtAmount_TextChanged"
                                                            TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAmount"
                                                            ErrorMessage="Please Enter Wage Rate" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="LBLExchangeRate" Visible="false" runat="server" CssClass="LBLBLACK" Width="128px" Text="Exchange Rate"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="txtExchangerate" Visible="false" runat="server" class="form-control txtbox" Enabled="False"
                                                            Height="28px" MaxLength="8" onkeypress="DecimalOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px">54.34</asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtExchangerate"
                                                            ErrorMessage="Please Enter Exchange Rate" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="TxtFromDate" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate2_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                            PopupButtonID="TxtFromDate" TargetControlID="TxtFromDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="TxtFromDate"
                                                            ErrorMessage="Please Enter From Date" InitialValue="" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="TxtToDate" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy" PopupButtonID="TxtToDate"
                                                            TargetControlID="TxtToDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="TxtToDate"
                                                            ErrorMessage="Please Enter To Date" InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="165px">Payment Cycle</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPaymentCycle" runat="server" class="form-control txtbox" Width="178px">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr id="tr_group_search" runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="100px">Group Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:CheckBoxList ID="ddl_group_type" runat="server" OnSelectedIndexChanged="ddl_group_type_SelectedIndexChanged"
                                                            AutoPostBack="True" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True" Value="">Both</asp:ListItem>
                                                            <asp:ListItem Value="Group">Group</asp:ListItem>
                                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td runat="server">
                                                    </td>
                                                </tr>
                                                <tr id="tr_group_code" runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK">Group Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Panel runat="server" ID="pnl" Height="160px" ScrollBars="Vertical">
                                                            <asp:CheckBoxList ID="ddl_group_code" runat="server" OnSelectedIndexChanged="ddl_group_code_SelectedIndexChanged"
                                                                AutoPostBack="True" RepeatColumns="4">
                                                                <asp:ListItem Selected="True" Value="">All</asp:ListItem>
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
                                                                <asp:ListItem Value="29">29</asp:ListItem>
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
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary" Height="32px"
                                                            TabIndex="10" Text="Search" Visible="false" Width="90px" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>                                            
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top"
                                            align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="BtnSave3_Click" TabIndex="10" Text="Search" ValidationGroup="group"
                                                Width="90px" />
                                            &nbsp;<asp:Button ID="BtnCancel_Click" runat="server" CssClass="btn btn-danger" Height="32px"
                                                OnClick="BtnCanc_Click" TabIndex="10" Text="Cancel" ValidationGroup="group" Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                    </tr>
                                    
                                     <tr id="lblDescLabel" runat="server" style="display: none">
                                                    <td >
                                                        <asp:Label ID="Label5" Font-Bold="true" Width="400px" CssClass="LBLBLACK" Text="Upload any reference Documents"  runat="server" />
                                                    </td>
                                                </tr>
                                        <tr  id="lblDesc" runat="server" style="display: none">
                                        
                                           
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server"  Width="180px" CssClass="LBLBLACK" >Document Description</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:TextBox ID="txtDescription" runat="server" class="form-control txtbox" Height="40px"
                                                            MaxLength="200" TabIndex="1" ValidationGroup="group1" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                       <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                                                            ErrorMessage="Please Enter To Date" InitialValue="" SetFocusOnError="True" ValidationGroup="group1">*</asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">Upload File</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" onchange="this.form.submit();" class="form-control txtbox"
                                                            Height="40px" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Height="32px"
                                                            OnClick="Button1_Click" TabIndex="10" Text="Add New" ValidationGroup="group1"
                                                            Width="120px" />
                                                    </td>
                                              </tr>
                                                <tr>
                                                    <td style="color: Red">
                                                        <asp:Label ID="lblresult" CssClass="LBLBLACK" Width="200px" runat="server" />
                                                    </td>
                                                </tr>
                                        <tr style="display: none" id="lblDesc2" runat="server">                                                        
                                                                <td style="padding: 5px; margin: 5px; padding-top: 20px;">
                                                                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="GRD"
                                                                        ForeColor="#333333" Height="62px" PageSize="15" ShowFooter="false" Width="100%"
                                                                        OnRowDeleting="GridView2_RowDeleting">
                                                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                        <Columns>
                                                                            <asp:CommandField ShowDeleteButton="True" HeaderText="Delete"></asp:CommandField>
                                                                            <asp:BoundField DataField="Description" ItemStyle-Width="200px" HeaderText="Description" />
                                                                            <asp:BoundField DataField="PhotoFileName" ItemStyle-Width="200px" HeaderText="File Name" />
                                                                        </Columns>
                                                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                                                        <EditRowStyle BackColor="#B9D684" />
                                                                        <AlternatingRowStyle BackColor="White" />
                                                                    </asp:GridView>
                                                                </td>
                                                </tr>
                                    <tr>
                                        <td>
                                       
                                            <telerik:RadGrid ID="rgAttendances" runat="server" allowmultirowselection="true" allowpaging="true" allowsorting="true" autogeneratecolumns="false" gridlines="Both" mastertableview-tablelayout="Fixed" onitemcommand="rgAttendances_ItemCommand" onitemdatabound="rgAttendances_ItemDataBound" onneeddatasource="rgAttendances_NeedDataSource" PagerStyle-AlwaysVisible="true" PageSize="250" >
                                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                <clientsettings enablerowhoverstyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </clientsettings>
                                                <MasterTableView clientdatakeynames="intBenId" datakeynames="intBenId" tablelayout="Fixed">
                                                    <norecordstemplate>
                                                        &nbsp;&nbsp;&nbsp;No results found</norecordstemplate>
                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn HeaderText="S.No" UniqueName="TemplateColumn">
                                                            <ItemTemplate>
                                                                <asp:Label ID="rowNumberLabel" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="intBenTempid" Display="false" HeaderStyle-Width="8%" HeaderText="ID">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="checkColumn">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelected" runat="server" onclick="unCheckHeader(this)" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderStyle-Width="18%" HeaderText="Enrollment No">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NameofHousehold" HeaderStyle-Width="18%" HeaderText="Name Of Household">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Age" HeaderStyle-Width="8%" HeaderText="Age">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Sex" HeaderStyle-Width="8%" HeaderText="Gender">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderStyle-Width="20%" HeaderText="Component">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Totaldays" HeaderStyle-Width="18%" HeaderText="No Of Days In This Duration">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="TotAttend" HeaderStyle-Width="18%" HeaderText="No Of Days Attended">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="cntdone" HeaderStyle-Width="18%" HeaderText="Already Wage Applied Days">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="cnt" HeaderStyle-Width="18%" HeaderText="Days To Be Applied For Wage">
                                                        </telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Amt" HeaderStyle-Width="18%" HeaderText="Amount To Be Paid In USD">
                                                        </telerik:GridBoundColumn>
                                                        <%--<telerik:GridBoundColumn DataField="Amt" HeaderText="	Alternate Person?" HeaderStyle-Width="18%"></telerik:GridBoundColumn>--%>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                         
                                        </td>
                                    </tr>
                                
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                       
                                            <%--<asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                CssClass="GRD" DataKeyNames="intBenid" ForeColor="#333333" Height="62px" OnRowDataBound="grdDetails_RowDataBound"
                                                PageSize="15" ShowFooter="True" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="All">
                                                        <HeaderTemplate>
                                                            <%-- <asp:CheckBox ID="CheckBox2" runat="server" />
                                                            <asp:CheckBox ID="ChKAll" runat="server" __designer:wfdid="w5" AutoPostBack="True"
                                                                onclick="CheckAll(this);" />
                                                            &nbsp;
                                                      
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4" />
                                                                  <asp:HiddenField ID="hdfcheckdone" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                                 
                                                    <asp:BoundField DataField="NameofHousehold" HeaderText="Name of Household" />
                                                    <asp:BoundField DataField="Age" HeaderText="Age" />
                                                    <asp:BoundField DataField="Sex" HeaderText="Gender" />
                                                    <asp:BoundField DataField="SelectionCriteria" HeaderText="Component" />
                                                    
                                                     <asp:BoundField DataField="Totaldays"  ItemStyle-Width="150px" HeaderText="No of Days in this duration" />
                                                    <asp:BoundField DataField="TotAttend"  ItemStyle-Width="150px" HeaderText="No of days Attended" />
                                                    <asp:BoundField DataField="cntdone"  ItemStyle-Width="150px" HeaderText="Already Wage Applied Days" />
                                                   
                                                    <asp:BoundField DataField="cnt"  ItemStyle-Width="150px" HeaderText="Days To be Applied for wage" />
                                                    
                                                    <%--DataField="Amt" 
                                                    <asp:BoundField DataField="Amt"  ItemStyle-Width="150px" HeaderText="Amount to be Paid in USD" />
                                                     <asp:TemplateField HeaderText="Alternate person?">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlalternate" runat="server" class="form-control txtbox" Width="158px">
                                                                <asp:ListItem Value="If any">If any</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#B9D684" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>--%>
                                         
                                        </td>
                                    </tr>
                                
                                                <tr>
                                                    <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                                        valign="top">
                                                        <asp:Button ID="BtnApplyForWR" runat="server" CssClass="btn btn-primary" Height="32px"
                                                            OnClick="BtnApplyForWR_Click" TabIndex="10" Text="Apply for Wage Request" ValidationGroup="group"
                                                            Width="180px" />
                                                        &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" Height="32px"
                                                            OnClick="BtnSave2_Click" TabIndex="10" Text="Cancel" 
                                                            Visible="False" Width="90px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                        <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                            valign="top">
                                            <table id="Table1" runat="server" visible="false">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label416" runat="server" CssClass="LBLBLACK" Width="128px">Paid Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server" Format="dd-MM-yyyy"
                                                            PopupButtonID="txtStartDate" TargetControlID="txtStartDate">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                           
                                        
                                    
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <div id="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">
                                                    ×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                        
                                            <asp:GridView ID="GrdExisted" runat="server" AutoGenerateColumns="False" 
                                                CssClass="GRD" ForeColor="#333333" Height="62px" 
                                              PageSize="15" ShowFooter="false" 
                                                Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" 
                                                    VerticalAlign="Middle" />
                                                <Columns>
                                                    
                                                    <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" 
                                                        ItemStyle-Width="200px" />
                                                    <asp:BoundField DataField="NameofHousehold" HeaderText="Name of the household" 
                                                        ItemStyle-Width="200px" />
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" 
                                                    ForeColor="White" />
                                                <EditRowStyle BackColor="#B9D684" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        
                                         </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:HiddenField ID="hdfID" runat="server" />
                                            <asp:HiddenField ID="hdfFlagID" runat="server" />
                                             <asp:HiddenField ID="hdfIP" runat="server" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="group" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
              
       <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
    <%--</div>
       </td>
        </tr>
    </table>--%>
      <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
<ProgressTemplate>
<div class="overlay">
<%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
<div style=" z-index: 1000; margin-left: -210px;margin-top:100px;opacity: 1; -moz-opacity: 1;">
<img alt="" src="../../Resource/Images/Processing.gif" />

</div>
    
</div>
</ProgressTemplate>
</asp:UpdateProgress>
       </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

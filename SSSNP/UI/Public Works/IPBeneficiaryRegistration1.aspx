<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="IPBeneficiaryRegistration1"
    Title=":: :: SSSNP : ::" Codebehind="IPBeneficiaryRegistration1.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script>
        $(document).ready(function() {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
        });
    </script>

    <script type="text/javascript" language="javascript">

        function OpenPopup() {

            window.open("Lookups/BeneficiaryLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }
    
  
    </script>

    <script type="text/javascript">
        function inputOnlyNumbers(evt) {
            var e = window.event || evt; // for trans-browser compatibility  
            var charCode = e.which || e.keyCode;
            if ((charCode > 45 && charCode < 58) || charCode == 8) {
                return true;
            }
            return false;
        }


        function AlphaNumericOnly(evt) {
            var AsciiValue = event.keyCode
            if ((AsciiValue >= 65 && AsciiValue <= 90) || (AsciiValue >= 97 && AsciiValue <= 122) || (AsciiValue >= 48 && AsciiValue <= 57))
                event.returnValue = true;
            else {
                event.returnValue = false;

                alert("Enter Alphabets,  and Characters  Only");
            }
        }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here &nbsp;!&nbsp; &nbsp; &nbsp;
                    <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
                    <li class=""><i class="fa fa-fw fa-desktop"></i>Implementing Partner </li>
                    <li class="active"><i class="fa fa-edit"></i><a href="#">Beneficiary</a> </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Beneficiary</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <%--<tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="115px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False" Font-Size="12px"
                                                Style="position: static" ToolTip="Rate Lookup" />
                                            &nbsp;
                                            <div>
                                            </div>
                                        </td>
                                        <td runat="server" id="trlookup" style="padding: 1px; margin: 1px" align="left">
                                            <a href="UploadsTrainee.aspx" class="btn btn-primary">Upload Beneficiary Details</a>
                                        </td>
                                    </tr>--%>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="lblEnrollmentNo" Font-Bold="true" Visible="false" runat="server" CssClass="LBLBLACK"
                                                Width="365px"></asp:Label>
                                        </td>
                                        <%--<td runat="server" id="Td1" style="padding: 1px; margin: 1px" align="left">
                                            <a href="UploadsTraineeUpdate.aspx" class="btn btn-primary">Upload Update Beneficiary
                                                Details</a>
                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" Height="36
                                                            px"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlIP"
                                                            ErrorMessage="Please Select IP" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" Width="160px">Name of household head</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtHeadName" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtHeadName"
                                                            ErrorMessage="Please Enter Name of household head" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="160px">Beneficiary Type</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlBeneficiary" AutoPostBack="True" OnSelectedIndexChanged="BeneficiarySelect"
                                                            runat="server" class="form-control txtbox" Height="34px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Group">Group</asp:ListItem>
                                                            <asp:ListItem Value="CST">CST</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdBeneficiaryType" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlBeneficiary"
                                                            ErrorMessage="Please Select Beneficiary Type?" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trGroup" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="160px">Group List</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlGroupList" runat="server" OnSelectedIndexChanged="ddlGroupList_SelectedIndexChanged"
                                                            class="form-control txtbox" Height="34px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
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
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlGroupList"
                                                            ErrorMessage="Please Select Group?" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label358" runat="server" CssClass="LBLBLACK" Width="165px">Household Size</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtFamilySize" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="2" TabIndex="1" onkeypress="NumberOnly()" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtFamilySize"
                                                            ErrorMessage="Please Enter Household size" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlSex" runat="server" class="form-control txtbox" Height="34px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlSex"
                                                            ErrorMessage="Please Select Sex" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label405" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlAge" runat="server" class="form-control txtbox" Height="33px"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label359" runat="server" CssClass="LBLBLACK" Width="137px">Name of spouse</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtSpouse" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtSpouse"
                                                            ErrorMessage="Please Enter Name of spouse" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label360" runat="server" CssClass="LBLBLACK" Width="154px">No.of other adult household members </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNoofAdults" runat="server" class="form-control txtbox" Height="33px"
                                                            Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
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
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtNoofAdults" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="2" TabIndex="1" onkeypress="NumberOnly()" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlNoofAdults"
                                                            ErrorMessage="Please Select No.of other adult household members" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="165px">Number of male adults</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberMaleAdults" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberMaleAdults" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="165px">Number of Female adults</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberFemaleAdults" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberFemaleAdults" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults Age above 65</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeAbove65" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtNumberAdultsAgeAbove65" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults age between 45 - 64</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeBetween45_64" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%-- <asp:TextBox ID="txtNumberAdultsAgeBetween45_64" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults age between 18 - 30</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeBetween18_30" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberAdultsAgeBetween18_30" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="165px">Number of adults age between 30 - 45 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberAdultsAgeBetween30_45" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberAdultsAgeBetween30_45" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label371" runat="server" CssClass="LBLBLACK" Width="165px">Number of dependents</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddldependents" runat="server" class="form-control txtbox" Height="33px"
                                                            Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtdependents" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="2" TabIndex="1" onkeypress="NumberOnly()" oncopy="return false" onpaste="return false" oncut="return false" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddldependents"
                                                            ErrorMessage="Please Select Number of dependents" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label11" runat="server" CssClass="LBLBLACK" Width="165px">Number of Female Children</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberFemaleChildren" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberFemaleChildren" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label13" runat="server" CssClass="LBLBLACK" Width="165px">Number of Male Children</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberMaleChildren" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberMaleChildren" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label14" runat="server" CssClass="LBLBLACK" Width="165px">Number of children age <5 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberChildrenAgeAbove5years" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberChildrenAgeAbove5years" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label15" runat="server" CssClass="LBLBLACK" Width="165px">Number of chilren age <12 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberChildrenAgeAbove12years" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberChildrenAgeAbove12years" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label16" runat="server" CssClass="LBLBLACK" Width="165px">Number of children age <18 years</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlNumberChildrenAgeAbove18years" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="0">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtNumberChildrenAgeAbove18years" runat="server" class="form-control txtbox" 
                                                            Height="28px" MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
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
                                                        <asp:Label ID="Label31" runat="server" CssClass="LBLBLACK" Width="165px">Component</asp:Label>
                                                    </td>

                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <%--<asp:TextBox ID="txtSelectionCriteria" runat="server" class="form-control txtbox" 
                                                            Height="34px" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>--%>
                                                         <asp:DropDownList ID="ddlComponent"  
                                                            runat="server" class="form-control txtbox" Height="36px">
                                                            <asp:ListItem Value="Select">--- Select ---</asp:ListItem>
                                                            <asp:ListItem Value="Component 1 (Public Works)">Component 1 (LIPW)</asp:ListItem>
                                                            <asp:ListItem Value="Component 2 (Direct Income Support)">Component 2 (DIS)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>

                                                </tr>

                                                 <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label32" runat="server" CssClass="LBLBLACK" Width="165px">Selection Reason</asp:Label>
                                                    </td>

                                                    <td style="padding: 5px; margin: 5px">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtSelectionReason" runat="server" class="form-control txtbox" 
                                                            Height="34px" TabIndex="1" 
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>

                                                </tr>





                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label372" runat="server" CssClass="LBLBLACK" Width="165px">Average Household Income</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtIncome" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="13" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label406" runat="server" CssClass="LBLBLACK" Width="165px">Source of Income</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtSourceIncome" runat="server" class="form-control txtbox" Height="34px"
                                                            MaxLength="80" TabIndex="1" TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                            Height="33px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlState"
                                                            ErrorMessage="Please Select State" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label17" runat="server" CssClass="LBLBLACK" Width="165px">National ID Number</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtNationalID" runat="server" class="form-control txtbox" Height="34px"
                                                            MaxLength="20" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="rvtxtNationalID" runat="server" 
                                                            ControlToValidate="txtNationalID" ErrorMessage="Please Enter National ID" 
                                                            InitialValue="" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>--%>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" Height="33px"
                                                            AutoPostBack="True" OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlCounties"
                                                            ErrorMessage="Please Select County" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Height="28px"
                                                            Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPayam"
                                                            ErrorMessage="Please Select Payam" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" OnSelectedIndexChanged="ddlBoma_SelectedIndexChanged"
                                                            class="form-control txtbox" AutoPostBack="true" Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:HiddenField ID="hdBoma" runat="server" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlBoma"
                                                            ErrorMessage="Please Select Boma" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label12" Visible="false" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px;">
                                                        
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtEthnicityTribeRgin" Visible="false" runat="server" class="form-control txtbox"
                                                            Height="28px" onkeypress="return AlphaNumericOnly(event);" MaxLength="13" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label352" runat="server" CssClass="LBLBLACK" Width="137px">Address</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtAddress" runat="server" class="form-control txtbox" Height="42px"
                                                            MaxLength="100" TabIndex="1" TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtAddress"
                                                            ErrorMessage="Please Enter Address" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtContactNo" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="txtContactNo"
                                                            ErrorMessage="Please Enter Contact No" SetFocusOnError="True" ValidationGroup="group"
                                                            Visible="False">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">Email</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEmail" runat="server" class="form-control txtbox" Height="34px"
                                                            MaxLength="50" TabIndex="1" ValidationGroup="group" Width="180px" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                            ErrorMessage="Please Enter Valid Email" SetFocusOnError="True" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="group">*</asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label374" runat="server" CssClass="LBLBLACK" Width="143px">Second Line / Home</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtHomePhone" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtHomePhone"
                                                            ErrorMessage="Please Enter Second Line / Home" SetFocusOnError="True" ValidationGroup="group"
                                                            Visible="False">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="143px">Legal Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlLegalStatus" runat="server" class="form-control txtbox"
                                                            Height="28px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem>Refugee</asp:ListItem>
                                                            <asp:ListItem>Returnee</asp:ListItem>
                                                            <asp:ListItem>IDP</asp:ListItem>
                                                            <asp:ListItem>Host Community</asp:ListItem>
                                                            <asp:ListItem>Resident</asp:ListItem>
                                                            <asp:ListItem>Citizen</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="rvddlLegalStatus" runat="server" ControlToValidate="ddlLegalStatus"
                                                            ErrorMessage="Please Select Legal Status" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="Tr1" runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label402" runat="server" CssClass="LBLBLACK" Width="165px">Is Vulnerability?</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlIsdisability" runat="server" class="form-control txtbox"
                                                            Height="28px" Width="180px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList25_SelectedIndexChanged">
                                                            <asp:ListItem Value="N">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="Y">Yes</asp:ListItem>
                                                            <asp:ListItem Value="N">No</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="ddlIsdisability"
                                                            ErrorMessage="Please Select Is Disability?" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false" id="trdisability">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label367" runat="server" CssClass="LBLBLACK" Width="165px">Vulnerability Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlDisability" runat="server" class="form-control txtbox" Height="28px"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="ddlDisability"
                                                            ErrorMessage="Please select Disability" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="165px">Biometric No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtBiometric" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="10" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="165px">Beneficiary Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" class="form-control txtbox" Height="28px"
                                                            onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image1" runat="server" Height="153px" ImageUrl="~/Resource/Images/not-available.jpg"
                                                            Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            <asp:Label ID="Label407" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="244px">First Alternate Payee Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top" align="left">
                                            <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="244px">Second Alternate Payee Details</asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName1stAlternatePayee" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtName1stAlternatePayee"
                                                            ErrorMessage="Please Enter First Alternate Payee" InitialValue="" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label18" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA1Gender" runat="server" class="form-control txtbox" Width="180px"
                                                            Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label19" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA1Age" runat="server" class="form-control txtbox" Height="33px"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label28" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA1NationalID" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label22" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA1ContactNo" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label24" runat="server" CssClass="LBLBLACK" Width="165px">First Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload2" runat="server" class="form-control txtbox" Height="28px"
                                                            onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName1" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image2" runat="server" Height="153px" ImageUrl="~/Resource/Images/not-available.jpg"
                                                            Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top" align="left">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="137px">Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtName2ndAlternatePayee" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtName2ndAlternatePayee"
                                                            ErrorMessage="Please Enter Second Alternate Payee" InitialValue="" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label20" runat="server" CssClass="LBLBLACK" Width="137px">Male / Female</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA2Gender" runat="server" class="form-control txtbox" Width="180px"
                                                            Height="28px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="M">Male</asp:ListItem>
                                                            <asp:ListItem Value="F">Female</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label21" runat="server" CssClass="LBLBLACK" Width="137px">Age</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="DdlA2Age" runat="server" class="form-control txtbox" Height="33px"
                                                            Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label29" runat="server" CssClass="LBLBLACK" Width="128px">National ID</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA2NationalID" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label23" runat="server" CssClass="LBLBLACK" Width="128px">Contact No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtA2ContactNo" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label26" runat="server" CssClass="LBLBLACK" Width="165px">Second Alternate Payee Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUpload3" runat="server" class="form-control txtbox" Height="28px"
                                                            onchange="this.form.submit();" />
                                                        <asp:Label ID="lblFileName2" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="Image3" runat="server" Height="153px" ImageUrl="~/Resource/Images/not-available.jpg"
                                                            Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px">Bank Account Details</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label395" runat="server" CssClass="LBLBLACK" Width="165px">Name 
                                                        of Bank</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:TextBox ID="txtBankName" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        &nbsp;
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label396" runat="server" CssClass="LBLBLACK" Width="124px">Account 
                                                        No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtAccNo" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="124px">Retype 
                                                        Account No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtReAccNo" runat="server" class="form-control txtbox" Height="28px"
                                                            MaxLength="15" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtAccNo"
                                                            ControlToValidate="txtReAccNo" ErrorMessage="Account No and Retype Account No should match"
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:CompareValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label25" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px">Status</asp:Label>
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label27" runat="server" CssClass="LBLBLACK" Width="124px">Status
                                                        </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        :
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlStatus" runat="server" class="form-control txtbox" Height="33px"
                                                            Width="180px">
                                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                                            <asp:ListItem Value="2">Confirmed</asp:ListItem>
                                                            <asp:ListItem Value="3">Dropped</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="rvddlStatus" runat="server" ControlToValidate="ddlStatus"
                                                            ErrorMessage="Please Select Status" InitialValue="--Select--" SetFocusOnError="True"
                                                            ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                         <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                               <tr>
                                                <td style="padding: 5px; margin: 5px;">
                                                    <asp:Label ID="Label30" runat="server" CssClass="LBLBLACK" Width="124px">Remarks</asp:Label>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">
                                                    :
                                                </td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox ID="txtRemarks" runat="server" class="form-control txtbox" Height="28px"
                                                        MaxLength="15" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                </td>
                                               </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">
                                            &nbsp;
                                        </td>
                                        <td style="width: 27px">
                                            &nbsp;
                                        </td>
                                        <td valign="top">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="BtnSave_Click" TabIndex="10" Text="Save" ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnDelete" runat="server" CausesValidation="False" CssClass="btn btn-warning"
                                                Height="32px" OnClick="BtnClear0_Click" OnClientClick="return confirm('Do you want to delete the record ? ');"
                                                TabIndex="10" Text="Delete" Visible="false" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="ClearAll" ToolTip="To Clear  the Screen"
                                                Width="90px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div id="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">
                                                    ×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                            <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                            <div style="color: Red">
                                                <asp:Label ID="lblresult" runat="server" />
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile1" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile2" runat="server" />
                                <asp:HiddenField ID="hdfUploadFile3" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                    ShowSummary="False" ValidationGroup="group" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
                <ProgressTemplate>
                    <div class="overlay">
                        <%--<div style=" z-index: 1000; margin-left: 350px;margin-top:200px;opacity: 1;-moz-opacity: 1;">--%>
                        <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="../../Resource/Images/Processing.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

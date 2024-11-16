<%@ Page Title=":: :: SNSOP : : Beneficiary Attendance " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="True" Inherits="PDCHouseholdAttendance" CodeBehind="PDCHouseholdAttendance.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 112px;
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }

        /*BN Add style for RadWindow*/
        .RadWindow RadWindow_Bootstrap rwShadow { width:850px; }
        .RadWindow_Bootstrap .rwTitleBar { border-radius:15px 15px 0px 0px !important; background:#3ca5e0 !important; color:#fff !important;
        background-size: auto 45px !important; background-repeat:no-repeat !important; background-size:auto 45px !important;
        background-position-x:20px !important; background-position-y:center !important; position:relative !important; }
    </style>

    <script type="text/javascript">
         //<![CDATA[
      <%--   function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
             for (i = 1; i < GridView2.rows.length; i++) {
                 GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
             }
         }--%>
         //]]>
    </script>

    <%-- <script type="text/javascript">
          function showProgress() {
              var updateProgress = $get("<%= UpdateProgress.ClientID %>");
              updateProgress.style.display = "block";
          }
</script>--%>

    <script type="text/javascript">


        
           <%-- function ClosePopup() {
                var oWindow = $find("<%=rwConfirmOperation.ClientID%>");

                if (oWindow != null) {
                    oWindow.close();
                }
                Sys.Application.remove_load(f);
            } Sys.Application.add_load(f);--%>
        
        
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

           // $('#txtStartDate').datetimepicker();
        });
    </script>

    <script type="text/javascript" language="javascript">
        function OpenPopup() {
            if ($('#txtStartDate').val() == "") {
                alert('Please Select Date');
                return false;
            }
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
        function progress() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>

    <script type="text/javascript">
        function CheckAll(id) {
            var masterTable = $find("<%= RadDetail .ClientID %>").get_masterTableView();
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
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }


        function FLICheckAll(id) {
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("FLIchkSelected").checked = true; // for checking the checkboxes
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("FLIchkSelected").checked = false; // for unchecking the checkboxes
                }
            }
        }
        function FLIunCheckHeader(id) {
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
        function ECDCheckAll(id) {
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("ECDchkSelected").checked = true; // for checking the checkboxes
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("ECDchkSelected").checked = false; // for unchecking the checkboxes
                }
            }
        }
        function ECDunCheckHeader(id) {
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
		function WASHCheckAll(id) {
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == true) {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("WASHchkSelected").checked = true; // for checking the checkboxes
                }
            }
            else {
                for (var i = 0; i < row.length; i++) {
                    masterTable.get_dataItems()[i].findElement("WASHchkSelected").checked = false; // for unchecking the checkboxes
                }
            }
        }
        function WASHunCheckHeader(id) {
            var masterTable = $find("<%= RadDetail.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
    </script>


    
  

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>


         
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" EnableViewState="false" 
        InitialBehavior="Close" VisibleStatusbar="false" Modal="true" Style="z-index: 7001"  Title="Dialog" > </telerik:RadWindowManager> 
    
    <div class="form-group row">           		           
        <%--<telerik:RadWindow RenderMode="Lightweight" runat="server" ID="rwConfirmOperation" RestrictionZoneID="ContentTemplateZone" 
            Behaviors="Close, Move" Modal="true" CenterIfModal="true" EnableShadow="true" KeepInScreenBounds="true"  
            AutoSize="true" DestroyOnClose="true" ReloadOnShow="false" Top="50"  VisibleOnPageLoad="false"
            style="left: 835px; top: 10px!important; width: 450px!important; height: 280px; visibility: visible; position: absolute; z-index: 7002; 
                transform: none; backface-visibility: visible;">
            <ContentTemplate>                  
                <div style="padding-left: 30px; padding-top: 20px; width: 200px; float: left;">
                    <asp:Label ID="lblConfirm" Font-Size="14px" Text="Are you sure you want to delete ?" runat="server"></asp:Label>
                    <br />
                    <br />
                    <telerik:RadButton ID="RadButtonYes" runat="server" Text="Yes" OnClick="RadButtonYes_Click"></telerik:RadButton>
                    <telerik:RadButton ID="RadButtonNo" runat="server" Text="No" OnClick="RadButtonNo_Click"> </telerik:RadButton>
                </div>                                       
            </ContentTemplate>
        </telerik:RadWindow>--%>
    </div>

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
                        <i class="fa fa-edit"></i><a href="#">Beneficiary Attendance</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary Attendance</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%" runat="server">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False"
                                                Font-Size="12px" Style="position: static" ToolTip="Lookup" />
                                        </td>
                                        <td runat="server" id="trlookup" style="padding: 5px; margin: 5px" align="left">
                                            <a href="UploadAttendance.aspx" class="btn btn-primary">Upload Beneficiary Attendance</a>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label394" runat="server" CssClass="LBLBLACK" Width="128px">Project 
                                                        Name</asp:Label>
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
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work 
                                                        Code</asp:Label>
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
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
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
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam :</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name :</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK">Approved 
                                                        Date :</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td>
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK"
                                                            Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr id="tr_group_search" runat="server" visible="false">
                                                    <td>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="100px">Group Type :</asp:Label>
                                                            </td>

                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="ddl_group_type" runat="server" class="form-control txtbox"
                                                                    AutoPostBack="True" Width="140px"
                                                                    OnSelectedIndexChanged="ddl_group_type_SelectedIndexChanged">
                                                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="Group">Group</asp:ListItem>
                                                                    <asp:ListItem Value="CST">CST</asp:ListItem>

                                                                </asp:DropDownList>
                                                            </td>
                                                            <td runat="server"></td>
                                                            <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                        </tr>
                                                        <tr id="tr_group_code" runat="server" visible="false">
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK">Group Code :</asp:Label>
                                                            </td>

                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:DropDownList ID="ddl_group_code" runat="server" class="form-control txtbox"
                                                                    Width="140px" AutoPostBack="True" OnSelectedIndexChanged="ddl_group_code_SelectedIndexChanged">
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
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Button ID="BtnSearch" runat="server" CssClass="btn btn-primary"
                                                                    Height="32px" OnClick="BtnSearch_Click" TabIndex="10" Text="Search" Visible="false"
                                                                    Width="90px" />
                                                            </td>
                                                        </tr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label410" runat="server" CssClass="LBLBLACK" Width="128px">Attendance Date :</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" AutoPostBack="true"
                                                            Width="180px" OnTextChanged="txtStartDate_TextChanged"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                            ControlToValidate="txtStartDate" ErrorMessage="Please Enter Attendance Date"
                                                            SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <telerik:RadGrid ID="RadDetail" AutoGenerateColumns="false" runat="server" PageSize="50" Visible="false"
                                                AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both" AllowPaging="false"
                                                MasterTableView-TableLayout="Fixed" OnNeedDataSource="RadDetail_NeedDataSource" OnItemDataBound="RadDetail_ItemDataBound" OnItemCreated="RadDetail_ItemCreated">
                                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>
                                                <MasterTableView TableLayout="Fixed"
                                                    DataKeyNames="intBenid"
                                                    ClientDataKeyNames="intBenid">
                                                    <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                    <PagerStyle AlwaysVisible="false" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="rowNumberLabel" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>

                                                        <telerik:GridTemplateColumn UniqueName="checkColumn">
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAll(this)" />
                                                            </HeaderTemplate>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkSelected" runat="server" onclick="unCheckHeader(this)" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="40px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridTemplateColumn UniqueName="FLIcheckColumn" HeaderStyle-Width="36px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate >
                                                                            <asp:CheckBox ID="FLIcheckAll" runat="server" onclick="FLICheckAll(this)" Text="FLI"  />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate >
                                                                            <asp:CheckBox ID="FLIchkSelected" runat="server" onclick="FLIunCheckHeader(this)" />
                                                                        </ItemTemplate>
                                                                        </telerik:GridTemplateColumn>   
                                                                    <telerik:GridTemplateColumn UniqueName="ECDcheckColumn"  HeaderStyle-Width="36px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate >
                                                                            <asp:CheckBox ID="ECDcheckAll" runat="server" onclick="ECDCheckAll(this)" Text="ECD" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="ECDchkSelected" runat="server" onclick="ECDunCheckHeader(this)" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridTemplateColumn UniqueName="WASHcheckColumn" HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="WASHcheckAll" runat="server" onclick="WASHCheckAll(this)" Text="WASH"  />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="WASHchkSelected" runat="server" onclick="WASHunCheckHeader(this)" />
                                                                        </ItemTemplate>
                                                                    </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="intBenid" HeaderText="intBenid" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="EnrollmentNo" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name" HeaderStyle-Width="28%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="sex" HeaderText="Gender" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="25%" Visible="true"></telerik:GridBoundColumn>
                                                        <telerik:GridTemplateColumn UniqueName="StatusColumn" HeaderText="Status">
                                                            <ItemTemplate>
                                                                <asp:DropDownList runat="server" ID="ddlStatus">
                                                                    <asp:ListItem Value="P">Present</asp:ListItem>
                                                                    <asp:ListItem Value="A">Absent</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="100px" />
                                                        </telerik:GridTemplateColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Button ID="BtnSave3" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Submit"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger"
                                                Height="32px" TabIndex="10" Text="Cancel"
                                                ValidationGroup="group" Width="90px" Visible="False" OnClick="BtnSave2_Click" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div id="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert"
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label
                                                        ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                            <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
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
    </asp:UpdatePanel>
    <%--<asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
<ProgressTemplate>
        <div class="overlay">
<div style=" z-index: 1000; margin-left: -210px;margin-top:100px;opacity: 1; -moz-opacity: 1;">
<img alt="" src="../../Resource/Images/Processing.gif" />
        </div>    
</div>
</ProgressTemplate>
</asp:UpdateProgress>--%>
    <%--</div>
       </td>
        </tr>
    </table>--%>
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

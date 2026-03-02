<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" EnableEventValidation="true"
    AutoEventWireup="true" Inherits="PDCHouseholdWagesApplyDIS" CodeBehind="PDCHouseholdWagesApplyDIS.aspx.cs" MaintainScrollPositionOnPostback="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript">

</script>

    <script>
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
        });
    </script>

    <script type="text/javascript">
        function CheckAll(id) {
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
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
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
        function FLICheckAll(id) {
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
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
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
        function ECDCheckAll(id) {
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
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
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
		function WASHCheckAll(id) {
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
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
            var masterTable = $find("<%= rgBeneficiariesDIS.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
    </script>

    <script type="text/javascript">
        function() {
            var updateProgress = $get("<%= UpdateProgress.ClientID %>");
            updateProgress.style.display = "block";
        }
    </script>
    <script type="text/javascript">
        function confirm_meth() {
            return true;
            //var value = document.getElementById("txtDaysToBePaid").value;
            //if (value != "20") {
            //    if (confirm("The specified value for 'Days to be Paid' is different from 20. Do you confirm?") == true) {
            //        return true;
            //    }
            //    else {
            //        return false;
            //    }
            //}
        }


    </script>



    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here &nbsp;!&nbsp; &nbsp; &nbsp;
                    <li><i class="fa fa-dashboard"></i><a href="Home.aspx"></a>Home</li>
                    <li class=""><i class="fa fa-fw fa-desktop"></i>Wage Request </li>
                    <li class="active"><i class="fa fa-edit"></i><a href="#">Direct Income Support - Apply for Wages Payment</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Direct Income Support - Apply for Wages Payment</h3>
                            </div>
                            <asp:UpdatePanel runat="server" ID="upPanelMain">
                                <ContentTemplate>
                                    <div class="panel-body">
                                        <table align="center" cellpadding="4" cellspacing="5" style="width: 83%">
                                            <caption>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px" valign="top">
                                                        <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="125px">Year</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px">: </td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlYear" runat="server" class="form-control txtbox" Width="178px" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="--Select--">Select</asp:ListItem>
                                                                        <asp:ListItem Value="2020">2020</asp:ListItem>
                                                                        <asp:ListItem Value="2021">2021</asp:ListItem>
                                                                        <asp:ListItem Value="2022">2022</asp:ListItem>
                                                                        <asp:ListItem Value="2023">2023</asp:ListItem>
                                                                        <asp:ListItem Value="2024">2024</asp:ListItem>
                                                                        <asp:ListItem Value="2025">2025</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp; </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="128px">State Name</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span> </td>
                                                                <td style="padding: 5px; margin: 5px">:</td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" class="form-control txtbox" Height="33px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="250px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px">&nbsp; </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="128px">County/Municipality Name</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span> </td>
                                                                <td style="padding: 5px; margin: 5px">:</td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlCounty" runat="server" AutoPostBack="True" class="form-control dropdown" Enabled="false" Height="33px" OnSelectedIndexChanged="ddlCounty_SelectedIndexChanged" Width="250px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label6" runat="server" CssClass="LBLBLACK" Width="165px">Payment Cycle</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px">:
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlPaymentCycle" runat="server" AutoPostBack="true" class="form-control txtbox" Width="178px" OnSelectedIndexChanged="ddlPaymentCycle_SelectedIndexChanged">
                                                                        <asp:ListItem Value="">--Select--</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" valign="top">
                                                        <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="165px">Month</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px">: </td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlMonth" runat="server" class="form-control txtbox" Width="178px" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true">
                                                                        <asp:ListItem Value="--Select--">Select</asp:ListItem>
                                                                        <asp:ListItem Value="01">January</asp:ListItem>
                                                                        <asp:ListItem Value="02">February</asp:ListItem>
                                                                        <asp:ListItem Value="03">March</asp:ListItem>
                                                                        <asp:ListItem Value="04">April</asp:ListItem>
                                                                        <asp:ListItem Value="05">May</asp:ListItem>
                                                                        <asp:ListItem Value="06">June</asp:ListItem>
                                                                        <asp:ListItem Value="07">July</asp:ListItem>
                                                                        <asp:ListItem Value="08">August</asp:ListItem>
                                                                        <asp:ListItem Value="09">September</asp:ListItem>
                                                                        <asp:ListItem Value="10">October</asp:ListItem>
                                                                        <asp:ListItem Value="11">November</asp:ListItem>
                                                                        <asp:ListItem Value="12">December</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="128px">Payam/Block Name</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span> </td>
                                                                <td style="padding: 5px; margin: 5px">:</td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlPayam" runat="server" AutoPostBack="True" class="form-control dropdown" Enabled="false" Height="33px" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged" Width="250px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label10" runat="server" CssClass="LBLBLACK" Width="128px">Boma/Quarter Council Name</asp:Label>
                                                                    <span style="text-align: left; color: red;">*</span> </td>
                                                                <td style="padding: 5px; margin: 5px">:</td>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:DropDownList ID="ddlBoma" runat="server" AutoPostBack="True" class="form-control dropdown" Enabled="false" Height="33px" Width="250px">
                                                                        <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                                        <asp:Button ID="btnSearchBeneficiaries" runat="server" CssClass="btn btn-primary" Height="32px" OnClick="btnSearchBeneficiaries_Click" TabIndex="10" Text="Search" ValidationGroup="group" Width="90px" />
                                                        &nbsp;<asp:Button ID="BtnCancel_Click" runat="server" CssClass="btn btn-danger" Height="32px" OnClick="BtnCanc_Click" TabIndex="10" Text="Cancel" ValidationGroup="group" Width="90px" />
                                                    </td>
                                                </tr>


                                                <tr align="center">
                                                    <td style="padding: 5px; margin: 5px" valign="top">
                                                        <table cellpadding="4" cellspacing="5" style="width: 83%">
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Wage Rate(USD)</asp:Label>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">: </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <asp:TextBox ID="txtWageRate" runat="server" AutoPostBack="True" class="form-control txtbox" Height="28px" MaxLength="4" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtWageRate" ErrorMessage="Please Enter Wage Rate" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="true">
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="lblExchangeRate" runat="server" CssClass="LBLBLACK" Width="128px" Visible="false" Text="Exchange Rate"></asp:Label>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;"></td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <asp:TextBox ID="txtExchangerate" Visible="false" runat="server" class="form-control txtbox" Enabled="False"
                                                                        Height="28px" MaxLength="8" onkeypress="DecimalOnly()" TabIndex="1" ValidationGroup="group"
                                                                        Width="180px"></asp:TextBox>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtExchangerate"
                                                                        ErrorMessage="Please Enter Exchange Rate" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>--%>
                                                                </td>
                                                            </tr>

                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="lblDaysToBePaid" runat="server" CssClass="LBLBLACK" Width="128px">Days to be Paid</asp:Label>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">: </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <asp:TextBox ID="txtDaysToBePaid" runat="server" AutoPostBack="True" class="form-control txtbox" ClientIDMode="Static" Height="28px" MaxLength="4" onkeypress="NumberOnly()" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtWageRate" ErrorMessage="Please Enter Wage Rate" SetFocusOnError="True" ValidationGroup="group">*</asp:RequiredFieldValidator>--%>
                                                                    *</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" valign="top">
                                                        
                                                    </td>
                                                </tr>
                                                <tr id="lblDesc" runat="server">
                                                    <td>
                                                        <asp:Label ID="lblDescLabel" runat="server" CssClass="LBLBLACK" Font-Bold="true" Text="Upload any reference Documents" Width="400px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="180px">Document Description</asp:Label>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">: </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <asp:TextBox ID="txtDescription" runat="server" class="form-control txtbox" Height="40px" MaxLength="200" TabIndex="1" ValidationGroup="group1" Width="180px"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" valign="top">
                                                        <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                            <tr>
                                                                <td style="padding: 5px; margin: 5px">
                                                                    <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">Upload File</asp:Label>
                                                                </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">: </td>
                                                                <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                    <asp:FileUpload ID="FileUpload1" runat="server" class="form-control txtbox" Height="40px" onchange="this.form.submit();" />
                                                                    <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-primary" Height="32px" OnClick="btnUpload_Click" TabIndex="10" Text="Add New" ValidationGroup="group1" Width="120px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr id="lblDesc2" runat="server">
                                                    <td style="padding: 5px; margin: 5px; padding-top: 20px;">
                                                        <asp:GridView ID="gvAttachments" runat="server" AutoGenerateColumns="False" CssClass="GRD"
                                                            ForeColor="#333333" Height="62px" PageSize="15" ShowFooter="false" Width="100%"
                                                            OnRowDeleting="gvAttachments_RowDeleting">
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
                                                    <td style="color: Red">
                                                        <asp:Label ID="lblresult" runat="server" CssClass="LBLBLACK" Width="200px" />
                                                    </td>
                                                </tr>

                                                <tr align="center">
                                                    <td colspan="6">
                                                        <telerik:RadGrid ID="rgBeneficiariesDIS" runat="server" Visible="false" lAllowMultiRowSelection="true" AllowPaging="true" AllowSorting="true" AutoGenerateColumns="false" GridLines="Both" MasterTableView-TableLayout="Fixed" OnItemDataBound="rgBeneficiariesDIS_ItemDataBound" OnNeedDataSource="rgBeneficiariesDIS_NeedDataSource" PagerStyle-AlwaysVisible="true" PageSize="250" Width="100%">
                                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                            <ClientSettings EnableRowHoverStyle="true">
                                                                <Selecting AllowRowSelect="True" />
                                                            </ClientSettings>
                                                            <MasterTableView ClientDataKeyNames="intBenId" DataKeyNames="intBenId" TableLayout="Fixed">
                                                                <NoRecordsTemplate>
                                                                    &nbsp;&nbsp;&nbsp;No results found
                                                                </NoRecordsTemplate>
                                                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                                <Columns>
                                                                    <telerik:GridTemplateColumn HeaderText="S.No" UniqueName="TemplateColumn">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="rowNumberLabel" runat="server" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="30px" />
                                                                    </telerik:GridTemplateColumn>
                                                                    <telerik:GridBoundColumn DataField="intBenID" Display="false" HeaderStyle-Width="8%" HeaderText="ID">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridTemplateColumn UniqueName="checkColumn">
                                                                        <HeaderTemplate>
                                                                            <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAll(this)" />
                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>
                                                                            <asp:CheckBox ID="chkSelected" runat="server" onclick="unCheckHeader(this)" />
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="20px" />
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
                                                                    <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderStyle-Width="15%" HeaderText="Enrollment No">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="NameofHousehold" HeaderStyle-Width="30%" HeaderText="Name Of Household">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Age" HeaderStyle-Width="8%" HeaderText="Age">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="Sex" HeaderStyle-Width="8%" HeaderText="Gender">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="StateName" HeaderStyle-Width="8%" HeaderText="State">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="CountieName" HeaderStyle-Width="8%" HeaderText="County">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="PayamName" HeaderStyle-Width="8%" HeaderText="Payam">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="BomaName" HeaderStyle-Width="8%" HeaderText="Boma">
                                                                    </telerik:GridBoundColumn>
                                                                    <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderStyle-Width="30%" HeaderText="Component">
                                                                    </telerik:GridBoundColumn>
                                                                </Columns>
                                                            </MasterTableView>
                                                        </telerik:RadGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="padding: 5px; margin: 5px" valign="top"></td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                                        <asp:Button ID="btnApplyforWage" runat="server" CssClass="btn btn-primary" Height="32px" OnClientClick="return confirm_meth()" OnClick="btnApplyforWage_Click" TabIndex="10" Text="Apply for Wage Request" ValidationGroup="group" Width="200px" />
                                                        &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger" Height="32px" OnClick="btnCancel_Click" TabIndex="10" Text="Cancel" Visible="False" Width="90px" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td colspan="2" style="padding: 5px; margin: 5px" valign="top">&nbsp; </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                                        <div id="success" runat="server" class="alert alert-success" visible="false">
                                                            <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
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
                                                        <asp:HiddenField ID="hdfID" runat="server" />
                                                        <asp:HiddenField ID="hdfFlagID" runat="server" />
                                                        <asp:HiddenField ID="hdfIP" runat="server" />
                                                        <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                                    </td>
                                                </tr>
                                            </caption>
                                        </table>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnUpload" />
                                </Triggers>
                            </asp:UpdatePanel>
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

</asp:Content>

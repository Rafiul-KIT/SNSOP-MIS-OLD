<%@ Page Title=":: :: SSSNP : : Beneficiary Attendance " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="PDCHouseholdAttendancedeleting" CodeBehind="PDCHouseholdAttendancedeleting.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%--<script runat="server">

    protected void BtnSave_Click(object sender, EventArgs e)
    {

    }
</script>--%>

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
    </style>
    <script type="text/javascript">
         //<![CDATA[
    <%--     function CheckAll(oCheckbox) {
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
    <script>
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
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
        function() {
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
                        <i class="fa fa-edit"></i><a href="#">Beneficiary Attendance</a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Beneficiary Attendance Deleting</h3>
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
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
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
                                                        <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK"
                                                            Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label410" runat="server" CssClass="LBLBLACK" Width="128px">Attendance Date
                                                            
                                                        </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group"
                                                            Width="180px" AutoPostBack="True" OnTextChanged="txtStartDate_TextChanged"  placeholder="dd/MM/yyyy"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtStartDate_CalendarExtender" runat="server"
                                                            Format="dd/MM/yyyy" PopupButtonID="txtStartDate" TargetControlID="txtStartDate"></cc1:CalendarExtender>
                                                         <cc1:FilteredTextBoxExtender ID="filterStartDate" runat="server" Enabled="True" TargetControlID="txtStartDate" FilterType="Numbers, Custom" ValidChars="/" FilterMode="ValidChars">

                                                         </cc1:FilteredTextBoxExtender>
                                                    
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                            ControlToValidate="txtStartDate" ErrorMessage="Please Enter Attendance Date"
                                                            SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="Search" runat="server" CssClass="btn btn-primary"
                                                Height="32px" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="90px" OnClick="btnSearch_Click" /></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <telerik:RadGrid ID="RadDetail" AutoGenerateColumns="false" runat="server" PageSize="50" Visible="false"
                                                AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both" AllowPaging="true"
                                                MasterTableView-TableLayout="Fixed" OnNeedDataSource="RadDetail_NeedDataSource" OnItemDataBound="RadDetail_ItemDataBound">
                                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />

                                                <ClientSettings EnableRowHoverStyle="true">
                                                    <Selecting AllowRowSelect="True" />
                                                </ClientSettings>


                                                <MasterTableView TableLayout="Fixed"
                                                    DataKeyNames="intBenid"
                                                    ClientDataKeyNames="intBenid">
                                                    <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />

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

                                                        <telerik:GridBoundColumn DataField="intBenid" HeaderText="intBenid" HeaderStyle-Width="15%" Visible="false"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="EnrollmentNo" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name" HeaderStyle-Width="28%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="sex" HeaderText="Gender" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ContactNo" HeaderText="Contact No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="25%" Visible="true"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Status" HeaderText="Status" HeaderStyle-Width="10%" Visible="true"></telerik:GridBoundColumn>


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
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Delete"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Cancel"
                                                ValidationGroup="group" Width="90px" Visible="False" />
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

    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>


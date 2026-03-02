<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="APIDashboardDrillDown"
    Title=":: :: SNSOP : ::" CodeBehind="APIDashboardDrillDown.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        //<![CDATA[
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }

      //]]>
    </script>
    <script type="text/javascript">
        function GetRowValue(val) {
            if (val != '&nbsp;') {
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;

            }
            window.opener.document.forms[0].submit();
            self.close();

        }

    </script>

    <script type="text/javascript">
        function CheckAll(id) {
            var masterTable = $find("<%= RadGrid1.ClientID %>").get_masterTableView();
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
            var masterTable = $find("<%= RadGrid1.ClientID %>").get_masterTableView();
            var row = masterTable.get_dataItems();
            if (id.checked == false) {
                var hidden = document.getElementById("HiddenField1");
                var checkBox = document.getElementById(hidden.value);
                checkBox.checked = false;
            }
        }
    </script>


    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <label class="panel-title" runat="server" id="lblPageTitle">API Dashboard Report</label>
                        <%--<h3 class="panel-title">API Dashboard Report</h3>--%>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px" valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 83%" __designer:mapid="17eb">
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Beneficiary Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtBenificiaryName" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" Height="33px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" __designer:mapid="17f5" r runat="server" visible="false">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                                Height="33px" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td style="width: 27px">
                                                <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 100%" __designer:mapid="1800">
                                                    <tr __designer:mapid="1801">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                            <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1804">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                            <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1809">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="124px">Enrollment No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtEnrollmentNo" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                                    OnClick="BtnSave_Click" TabIndex="10" Text="Search" Width="90px" />
                                                &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                                                    Height="32px" OnClick="BtnClear_Click" TabIndex="10" Text="Cancel" ToolTip="To Clear  the Screen"
                                                    Width="90px" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="LblMessage2" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">



                                    <telerik:RadGrid ID="RadGrid1" PageSize="250" AutoGenerateColumns="false" runat="server" AllowPaging="true"                                         AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                        MasterTableView-TableLayout="Fixed"
                                        OnNeedDataSource="RadGrid1_NeedDataSource" OnItemDataBound="RadGrid1_ItemDataBound" OnItemCommand="RadGrid1_ItemCommand">
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                        <PagerStyle AlwaysVisible="true" />
                                        <ClientSettings EnableRowHoverStyle="true">
                                            <Selecting AllowRowSelect="True" />
                                        </ClientSettings>


                                        <MasterTableView TableLayout="Fixed"
                                            DataKeyNames="BenfiName, intBenTempid"
                                            ClientDataKeyNames="BenfiName, intBenTempid">
                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />

                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="rowNumberLabel" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="36px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="intBenTempid" Display="false" HeaderText="ID" HeaderStyle-Width="1%"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="checkColumn">
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="checkAll" runat="server" onclick="CheckAll(this)" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelected" runat="server" onclick="unCheckHeader(this)" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="20px" />
                                                </telerik:GridTemplateColumn>

                                                <telerik:GridBoundColumn DataField="BenfiName" HeaderText="Beneficiary Name" HeaderStyle-Width="13%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="4%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="householdgender" HeaderText="Gender" HeaderStyle-Width="5%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Temporary Pre-Enrollment No" HeaderStyle-Width="18%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="StateName" HeaderText="State Name" HeaderStyle-Width="14%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CountieName" HeaderText="County Name" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam Name" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" HeaderStyle-Width="12%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn UniqueName="SelectionCriteria" DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                                <telerik:GridTemplateColumn UniqueName="ComponentDDL" HeaderText="NEW Component" HeaderStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:DropDownList runat="server" ID="ddlRgComponent">
                                                            <asp:ListItem Value="Select">--- Select ---</asp:ListItem>
                                                            <asp:ListItem Value="Component 1 (Public Works)">Component 1 (LIPW)</asp:ListItem>
                                                            <asp:ListItem Value="Component 1 (Direct Income Support)">Component 1 (DIS)</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="UpdateColumn" HeaderText="" HeaderStyle-Width="50px">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="lnkUpdate" runat="server" Text="Update" />
                                                    </ItemTemplate>
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridTemplateColumn UniqueName="operationCol" HeaderText="" HeaderStyle-Width="5%">
                                                    <ItemTemplate>
                                                        <table id="Table2" cellspacing="1" cellpadding="1" width="200" border="0">
                                                            <tr>
                                                                <td width="100%">Remarks:
                                                              <asp:TextBox runat="server" ID="txtRemarks" Height="32px" Width="100px"></asp:TextBox>
                                                                    <%--<%# DataBinder.Eval(Container.DataItem, "StateName") %>--%>
                                                                </td>
                                                                <td>

                                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator67" runat="server" ControlToValidate="txtRemarks"
                                                                        ErrorMessage="Please Enter Remarks" ValidationGroup="group1" Visible="False">*</asp:RequiredFieldValidator>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="text-align: center">
                                                                    <asp:Button runat="server" ID="btnDelete" Text="Delete" OnClick="btnDelete_Click" ValidationGroup="group1" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="200px" ForeColor="White" />
                                                </telerik:GridTemplateColumn>

                                            </Columns>
                                        </MasterTableView>

                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AutoGenerateColumns="false"
                                        Height="62px" Width="99%" Visible="false">
                                        <Columns>
                                            <asp:BoundField DataField="intBenid" HeaderText="intBenid" />
                                            <asp:BoundField DataField="intIP" HeaderText="intIP" />
                                            <asp:BoundField DataField="OLDEnrollmentNo" HeaderText="OLDEnrollmentNo" />
                                            <asp:BoundField DataField="intBenTempid" HeaderText="intBenTempid" />
                                            <asp:BoundField DataField="BenfiName" HeaderText="Beneficiary Name" />
                                            <asp:BoundField DataField="EnrollmentNo" HeaderText="EnrollmentNo" />
                                            <asp:BoundField DataField="Age" HeaderText="Age" />
                                            <asp:BoundField DataField="householdGender" HeaderText="Gender" />
                                            <asp:BoundField DataField="FamilySize" HeaderText="FamilySize" />
                                            <asp:BoundField DataField="NameofSpouse" HeaderText="NameofSpouse" />
                                            <asp:BoundField DataField="NoofAdult" HeaderText="NoofAdult" />
                                            <asp:BoundField DataField="NoofDependents" HeaderText="NoofDependents" />
                                            <asp:BoundField DataField="StateName" HeaderText="StateName" />
                                            <asp:BoundField DataField="CountieName" HeaderText="CountieName" />
                                            <asp:BoundField DataField="PayamName" HeaderText="PayamName" />
                                            <asp:BoundField DataField="BomaName" HeaderText="BomaName" />
                                            <asp:BoundField DataField="ContactNo" HeaderText="ContactNo" />
                                            <asp:BoundField DataField="FamilyIncome" HeaderText="FamilyIncome" />
                                            <asp:BoundField DataField="intCountieid" HeaderText="intCountieid" />
                                            <asp:BoundField DataField="intStateid" HeaderText="intStateid" />
                                            <asp:BoundField DataField="intPayamid" HeaderText="intPayamid" />
                                            <asp:BoundField DataField="intBomasid" HeaderText="intBomasid" />
                                            <asp:BoundField DataField="Address" HeaderText="Address" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                            <asp:BoundField DataField="SecondLineHome" HeaderText="SecondLineHome" />
                                            <asp:BoundField DataField="IsDisability" HeaderText="IsDisability" />
                                            <asp:BoundField DataField="intDisability" HeaderText="intDisability" />
                                            <asp:BoundField DataField="BiometricNo" HeaderText="BiometricNo" />
                                            <asp:BoundField DataField="NameofBank" HeaderText="NameofBank" />
                                            <asp:BoundField DataField="AccountNo" HeaderText="AccountNo" />
                                            <asp:BoundField DataField="WorkCode" HeaderText="WorkCode" />
                                            <asp:BoundField DataField="LegalStatus" HeaderText="LegalStatus" />
                                            <asp:BoundField DataField="Created_by" HeaderText="Created_by" />
                                            <asp:BoundField DataField="Created_dt" HeaderText="Created_dt" />
                                            <asp:BoundField DataField="Modified_by" HeaderText="Modified_by" />
                                            <asp:BoundField DataField="Modified_dt" HeaderText="Modified_dt" />
                                            <asp:BoundField DataField="SOurceofIncome" HeaderText="SOurceofIncome" />
                                            <asp:BoundField DataField="RegYear" HeaderText="RegYear" />
                                            <asp:BoundField DataField="PortYear" HeaderText="PortYear" />
                                            <asp:BoundField DataField="PortPhase" HeaderText="PortPhase" />
                                            <asp:BoundField DataField="PortedSheetName" HeaderText="PortedSheetName" />
                                            <asp:BoundField DataField="BDropout" HeaderText="BDropout" />
                                            <asp:BoundField DataField="DropoutRemarks" HeaderText="DropoutRemarks" />
                                            <asp:BoundField DataField="GroupId" HeaderText="GroupId" />
                                            <asp:BoundField DataField="BeneficiaryType" HeaderText="BeneficiaryType" />
                                            <asp:BoundField DataField="Name1stAlternatePayee" HeaderText="Name1stAlternatePayee" />
                                            <asp:BoundField DataField="Name2ndAlternatePayee" HeaderText="Name2ndAlternatePayee" />
                                            <asp:BoundField DataField="NumberMaleAdults" HeaderText="NumberMaleAdults" />
                                            <asp:BoundField DataField="NumberFemaleAdults" HeaderText="NumberFemaleAdults" />
                                            <asp:BoundField DataField="NumberAdultsAgeAbove65" HeaderText="NumberAdultsAgeAbove65" />
                                            <asp:BoundField DataField="NumberAdultsAgeBetween45_64" HeaderText="NumberAdultsAgeBetween45_64" />
                                            <asp:BoundField DataField="NumberAdultsAgeBetween18_30" HeaderText="NumberAdultsAgeBetween18_30" />
                                            <asp:BoundField DataField="NumberAdultsAgeBetween30_45" HeaderText="NumberAdultsAgeBetween30_45" />
                                            <asp:BoundField DataField="NumberFemaleChildren" HeaderText="NumberFemaleChildren" />
                                            <asp:BoundField DataField="NumberMaleChildren" HeaderText="NumberMaleChildren" />
                                            <asp:BoundField DataField="NumberChildrenAgeAbove5years" HeaderText="NumberChildrenAgeAbove5years" />
                                            <asp:BoundField DataField="NumberChildrenAgeAbove12years" HeaderText="NumberChildrenAgeAbove12years" />
                                            <asp:BoundField DataField="NumberChildrenAgeAbove18years" HeaderText="NumberChildrenAgeAbove18years" />
                                            <asp:BoundField DataField="EthnicityTribeRgin" HeaderText="EthnicityTribeRgin" />
                                            <asp:BoundField DataField="PhotoPath" HeaderText="PhotoPath" />
                                            <asp:BoundField DataField="PhotoName" HeaderText="PhotoName" />
                                            <asp:BoundField DataField="NationalID" HeaderText="NationalID" />
                                            <asp:BoundField DataField="PCPortFlag" HeaderText="PCPortFlag" />
                                            <asp:BoundField DataField="household_number" HeaderText="household_number" />
                                            <asp:BoundField DataField="FirstAlternatePayeeGender" HeaderText="FirstAlternatePayeeGender" />
                                            <asp:BoundField DataField="FirstAlternatePayeeAge" HeaderText="FirstAlternatePayeeAge" />
                                            <asp:BoundField DataField="FirstAlternatePayeeNationID" HeaderText="FirstAlternatePayeeNationID" />
                                            <asp:BoundField DataField="FirstAlternatePayeeMobile" HeaderText="FirstAlternatePayeeMobile" />
                                            <asp:BoundField DataField="FirstAlternatePayeePhotoName" HeaderText="FirstAlternatePayeePhotoName" />
                                            <asp:BoundField DataField="FirstAlternatePayeePhotoPath" HeaderText="FirstAlternatePayeePhotoPath" />
                                            <asp:BoundField DataField="SecondAlternatePayeeGender" HeaderText="SecondAlternatePayeeGender" />
                                            <asp:BoundField DataField="SecondAlternatePayeeAge" HeaderText="SecondAlternatePayeeAge" />
                                            <asp:BoundField DataField="SecondAlternateNationalID" HeaderText="SecondAlternateNationalID" />
                                            <asp:BoundField DataField="SecondAlternatePayeeMobile" HeaderText="SecondAlternatePayeeMobile" />
                                            <asp:BoundField DataField="SecondAlternatepayeePhotoName" HeaderText="SecondAlternatepayeePhotoName" />
                                            <asp:BoundField DataField="SecondAlternatePayeePhotoPath" HeaderText="SecondAlternatePayeePhotoPath" />
                                            <asp:BoundField DataField="supervisor_id" HeaderText="supervisor_id" />
                                            <asp:BoundField DataField="member_number" HeaderText="member_number" />
                                            <asp:BoundField DataField="female_dependants" HeaderText="female_dependants" />
                                            <asp:BoundField DataField="male_dependants" HeaderText="male_dependants" />
                                            <asp:BoundField DataField="latitude" HeaderText="latitude" />
                                            <asp:BoundField DataField="longitude" HeaderText="longitude" />
                                            <asp:BoundField DataField="id_url" HeaderText="id_url" />
                                            <asp:BoundField DataField="TransferDate" HeaderText="TransferDate" />
                                            <asp:BoundField DataField="TransferUpdateat" HeaderText="TransferUpdateat" />
                                            <asp:BoundField DataField="alternate_number" HeaderText="alternate_number" />
                                            <asp:BoundField DataField="Secondalternate_number" HeaderText="Secondalternate_number" />
                                            <asp:BoundField DataField="API_Status" HeaderText="API_Status" />
                                            <asp:BoundField DataField="householdnameupdate" HeaderText="householdnameupdate" />
                                            <asp:BoundField DataField="householddate" HeaderText="householddate" />
                                            <asp:BoundField DataField="Component" HeaderText="Component" />
                                            <asp:BoundField DataField="SelectionReason" HeaderText="SelectionReason" />
                                            <asp:BoundField DataField="TransferFlag" HeaderText="TransferFlag" />
                                            <asp:BoundField DataField="OLDBomaid" HeaderText="OLDBomaid" />
                                            <asp:BoundField DataField="EnrollmentNoBeforeTransfer" HeaderText="EnrollmentNoBeforeTransfer" />
                                            <asp:BoundField DataField="BomaTransferDate" HeaderText="BomaTransferDate" />
                                            <asp:BoundField DataField="BomaTransferBy" HeaderText="BomaTransferBy" />
                                            <asp:BoundField DataField="thirdalternate_number" HeaderText="thirdalternate_number" />
                                            <asp:BoundField DataField="Name3rdAlternatePayee" HeaderText="Name3rdAlternatePayee" />
                                            <asp:BoundField DataField="FirstAlternateUpdDate" HeaderText="FirstAlternateUpdDate" />
                                            <asp:BoundField DataField="FirstAlternateUpdby" HeaderText="FirstAlternateUpdby" />
                                            <asp:BoundField DataField="SecondAlternateUpdDate" HeaderText="SecondAlternateUpdDate" />
                                            <asp:BoundField DataField="SecondAlternateUpdby" HeaderText="SecondAlternateUpdby" />
                                            <asp:BoundField DataField="TransferStaffid" HeaderText="TransferStaffid" />
                                            <asp:BoundField DataField="TransferStaffDt" HeaderText="TransferStaffDt" />
                                            <asp:BoundField DataField="InsertedStaffid" HeaderText="InsertedStaffid" />
                                            <asp:BoundField DataField="InsertedStaffDt" HeaderText="InsertedStaffDt" />
                                            <asp:BoundField DataField="ModifiedStaffid" HeaderText="ModifiedStaffid" />
                                            <asp:BoundField DataField="ModifiedStaffdt" HeaderText="ModifiedStaffdt" />
                                            <asp:BoundField DataField="UFlag" HeaderText="UFlag" />
                                            <asp:BoundField DataField="ThirdAlternatePayeeGender" HeaderText="ThirdAlternatePayeeGender" />
                                            <asp:BoundField DataField="ThirdAlternatePayeeAge" HeaderText="ThirdAlternatePayeeAge" />
                                            <asp:BoundField DataField="ThirdAlternatePayeeNationID" HeaderText="ThirdAlternatePayeeNationID" />
                                            <asp:BoundField DataField="ThirdAlternatePayeeMobile" HeaderText="ThirdAlternatePayeeMobile" />
                                            <asp:BoundField DataField="ThirdAlternatepayeePhotoName" HeaderText="ThirdAlternatepayeePhotoName" />
                                            <asp:BoundField DataField="ThirdAlternatePayeePhotoPath" HeaderText="ThirdAlternatePayeePhotoPath" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnStat" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center"
                                    style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary"
                                        Height="32px" OnClick="Button1_Click" TabIndex="10" Text="Confirm"
                                        ValidationGroup="group" Width="155px" />
                                    &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger"
                                        Height="32px" OnClick="BtnSave2_Click" TabIndex="10" Text="Go Back"
                                        ValidationGroup="group" Width="90px" />
                                    &nbsp;
                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-warning"
                                                Height="32px" OnClick="Button2_Click" TabIndex="10" Text="Delete"
                                                ValidationGroup="group" Width="155px" />
                                    <%--<asp:Button ID="btnExportCSV" runat="server" CssClass="btn btn-primary"
                                        Height="32px" onclick="btnExportCSV_Click" TabIndex="10" Text="Export"
                                        ValidationGroup="group" Width="155px" />--%>


                                    <%-- <asp:Button ID="btnExportNew" runat="server" CssClass="btn btn-success"
                                        Height="32px" onclick="btnExportNew_Click" TabIndex="10" Text="Export New"
                                        ValidationGroup="group" Width="155px" />--%>

                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnExportCSV" runat="server" CssClass="btn btn-primary"
                                        Height="32px" OnClick="btnExportCSV_Click" TabIndex="10" Text="Export"
                                        ValidationGroup="group" Width="350px" />
                                       <asp:Button ID="btnExportZippedCSV" runat="server" CssClass="btn btn-primary"
                                        Height="32px" OnClick="btnExportZippedCSV_Click" TabIndex="10" Text="Export"
                                        ValidationGroup="group" Width="350px" />
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <div id="success" runat="server" class="alert alert-success" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert"
                                            href="#">×</a> <strong>Success!</strong><asp:Label
                                                ID="Label2" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                        <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>Warning!</strong>
                                        <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <asp:HiddenField ID="hdfID" runat="server" />
                                    <asp:HiddenField ID="hdfFlagID" runat="server" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                        ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">&nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

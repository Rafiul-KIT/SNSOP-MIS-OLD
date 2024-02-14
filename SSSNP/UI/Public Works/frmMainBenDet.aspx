<%@ page language="C#" masterpagefile="~/UI/Public Works/TstMaster.master" autoeventwireup="true" inherits="frmMainBenDet" title=":: SSSNP Online Management System ::" enableeventvalidation="false" codebehind="frmMainBenDet.aspx.cs" %>

<%@ register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript">
        function GetRowValue(val, valinvoice) {
            if (val != '&nbsp;') {
                val1 = 0;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;
                window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfInVoiceID').value = valinvoice;

            }
            window.opener.document.forms[0].submit();
            self.close();

        }

    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">Beneficiary Details</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 97%">

                            <tr>

                                <td align="center" style="padding: 5px; margin: 5px">
                                    <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px" valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 83%"
                                                    __designer:mapid="17eb">
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox"
                                                                Height="33px" Width="180px" ValidationGroup="group">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlIP"
                                                                ErrorMessage="Please Select Implementing Partner" InitialValue="--Select--" SetFocusOnError="True"
                                                                ValidationGroup="group">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox"
                                                                Height="33px" Width="180px"
                                                                onselectedindexchanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox"
                                                                Height="33px" AutoPostBack="True"
                                                                onselectedindexchanged="ddlCounties_SelectedIndexChanged1" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="124px">Beneficiary Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtbenf" runat="server" class="form-control txtbox" Height="33px"
                                                                MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>

                                                </table>
                                            </td>
                                            <td style="width: 27px">
                                                <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 100%"
                                                    __designer:mapid="1800">
                                                    <tr __designer:mapid="1801">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                            <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1804">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                            <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox"
                                                                Height="33px" Width="180px" AutoPostBack="True"
                                                                onselectedindexchanged="ddlPayam_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1809">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                                Height="33px" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>

                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;</td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="124px">Household No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txthousehold" runat="server" class="form-control txtbox" Height="33px"
                                                                MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="124px">Enrollment No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtenrollment" runat="server" class="form-control txtbox" Height="33px"
                                                                MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a" runat="server" visible="false">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox"
                                                                Height="33px" MaxLength="40" TabIndex="1"
                                                                ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>



                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;</td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                                    Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Search"
                                                    Width="90px" />
                                                &nbsp;<asp:Button ID="BtnClear" runat="server" CausesValidation="False"
                                                    CssClass="btn btn-danger" Height="32px" onclick="BtnClear_Click" TabIndex="10"
                                                    Text="Cancel" ToolTip="To Clear  the Screen" Width="90px" />
                                                &nbsp;<asp:Button ID="btnexp" runat="server" CausesValidation="False"
                                                    CssClass="btn btn-primary" Font-Size="12px" Height="32px"
                                                    onclick="btnexp_Click" Style="position: static" Text="Export" Width="90px"
                                                    ValidationGroup="vg" Visible="false" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>

                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>

                                    <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:radgrid id="telGridDetails" pagesize="50" autogeneratecolumns="false" runat="server" allowpaging="true"
                                        allowmultirowselection="true" allowsorting="true" gridlines="Both"
                                        mastertableview-tablelayout="Fixed" tablelayout="Auto"
                                        onneeddatasource="telGridDetails_NeedDataSource" onitemdatabound="telGridDetails_ItemDataBound">
                                        
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                          <ClientSettings Selecting-AllowRowSelect="true">
                                              <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true"  />
                                              <Selecting AllowRowSelect="True" />
                                              <ClientEvents OnRowSelected="rowSelected" />
                                          </ClientSettings>
                                                                               
                            <MasterTableView TableLayout="Fixed" 
                                        DataKeyNames="IPName" 
                                        ClientDataKeyNames="IPName" >                             
                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>    
                                
                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
               			         
                                <Columns>
                                    <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No" >
                                    <ItemTemplate>
                                        <asp:Label ID="rowNumberLabel" runat="server" />
                                    </ItemTemplate>
                                        <HeaderStyle Width="50px" />
                                </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn DataField="intBenTempid" Visible="false" HeaderText="ID" > </telerik:GridBoundColumn>
                                       <telerik:GridBoundColumn DataField="IPName" HeaderText="Name of the IP" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="StateName" HeaderText="State Name" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="CountieName" HeaderText="County Name" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam Name" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Enrollment No" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name of the Household" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NameofSpouse" HeaderText="Name of the Spouse" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="household_number" HeaderText="HouseHold Number" HeaderStyle-Width="18%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="4%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="householdgender" HeaderText="Gender" HeaderStyle-Width="6%"></telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                        <%--<telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="30%"></telerik:GridBoundColumn>--%>
                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="View" >
                                    <ItemTemplate>
                                        <asp:HyperLink ID="colLink" runat="server" Text ="View"/>
                                    </ItemTemplate>
                                        <HeaderStyle Width="80px" />
                                </telerik:GridTemplateColumn>
                                 </Columns>
                            </MasterTableView>
                        </telerik:radgrid>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server"
                                        AutoGenerateColumns="False" CellPadding="4"
                                        GridLines="None" Height="62px" Width="100%" Visible="false">
                                        <columns>
                                               <asp:BoundField DataField="IPName" HeaderText="Name of the IP"></asp:BoundField>
                                              <asp:BoundField DataField="StateName" HeaderText="State Name"></asp:BoundField>
                                              <asp:BoundField DataField="CountieName" HeaderText="County Name"></asp:BoundField>
                                              <asp:BoundField DataField="PayamName" HeaderText="Payam Name"></asp:BoundField>
                                              <asp:BoundField DataField="BomaName" HeaderText="Boma Name"></asp:BoundField>
                                              <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                              <asp:BoundField DataField="NameofHousehold" HeaderText="Name of the Household" />
                                              <asp:BoundField DataField="NameofSpouse" HeaderText="Name of the Spouse" />
                                           <asp:BoundField DataField="household_number" HeaderText="HouseHold Number" />
                                            <asp:BoundField DataField="Age" HeaderText="Age" />
                                            <asp:BoundField DataField="householdgender" HeaderText="Gender" />
                                            <asp:BoundField DataField="SelectionCriteria" HeaderText="Component" />                                         
                                        </columns>
                                        <rowstyle cssclass="GridviewScrollC1Item" />
                                        <pagerstyle cssclass="GridviewScrollC1Pager" />
                                        <selectedrowstyle cssclass="GridviewScrollC1Item2" />
                                        <headerstyle cssclass="GridviewScrollC1Header" />
                                        <footerstyle cssclass="GridviewScrollC1Footer" />
                                        <editrowstyle cssclass="GridviewScrollC1Item2" />
                                        <alternatingrowstyle cssclass="GridviewScrollC1Item2" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnStat" runat="server" />
                                    <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                        ShowSummary="False" ValidationGroup="group" />

                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>


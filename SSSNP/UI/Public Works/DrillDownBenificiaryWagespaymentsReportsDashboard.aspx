<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="DrillDownBenificiaryWagespaymentsReportsDashboard" Title=":: SSSNP Online Management System ::" CodeBehind="DrillDownBenificiaryWagespaymentsReportsDashboard.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Dashboard Drilldown</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana"
                                        Font-Size="13px" ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <telerik:RadGrid ID="telGridDetails" PageSize="50" AutoGenerateColumns="false" runat="server" AllowPaging="true"
                                        AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                        MasterTableView-TableLayout="Fixed" tablelayout="Auto"
                                        OnNeedDataSource="telGridDetails_NeedDataSource"
                                        OnItemDataBound="telGridDetails_ItemDataBound">
                                        <HeaderStyle CssClass="MyGridHeader" />
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                        <ClientSettings Selecting-AllowRowSelect="true">
                                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                            <Selecting AllowRowSelect="True" />
                                            <ClientEvents OnRowSelected="rowSelected" />
                                        </ClientSettings>
                                        <MasterTableView TableLayout="Fixed" HeaderStyle-CssClass="MyGridHeader"
                                            DataKeyNames="IPName"
                                            ClientDataKeyNames="IPName">
                                            <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                            <HeaderStyle CssClass="MyGridHeader" />
                                            <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                            <Columns>
                                                <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="rowNumberLabel" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle Width="60px" />
                                                </telerik:GridTemplateColumn>
                                                <telerik:GridBoundColumn DataField="intBenTempid" Visible="false" HeaderText="ID"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Enrollment No" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name of Household" HeaderStyle-Width="20%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" HeaderStyle-Width="4%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="4%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="BiometricNo" HeaderText="BiometricNo" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="AccountNo" HeaderText="Account No" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="StateName" HeaderText="State Name" HeaderStyle-Width="8%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="CountieName" HeaderText="County Name" HeaderStyle-Width="10%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="PayamName" HeaderText="Payam Name" HeaderStyle-Width="10%"></telerik:GridBoundColumn>

                                                <telerik:GridBoundColumn DataField="BomaName" HeaderText="Boma Name" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                                <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="15%"></telerik:GridBoundColumn>
                                            </Columns>
                                        </MasterTableView>
                                    </telerik:RadGrid>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <%--      <asp:GridView ID="grdDetails" runat="server" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" 
                                        ForeColor="#333333" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="100" Width="99%" 
                                        OnSelectedIndexChanged="grdDetails_SelectedIndexChanged" ShowFooter="True"                                         
                                        AllowPaging="True">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                                    <ItemTemplate >
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="50px" />                                                                    
                                                                </asp:TemplateField>                                                     
                                                     <asp:BoundField  DataField="EnrollmentNo" HeaderText="Enrollment No" >                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                       <asp:BoundField  DataField="NameofHousehold" HeaderText="Name of Household" >                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                     <asp:BoundField  DataField="Sex" HeaderText="Gender" >                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                     <asp:BoundField  DataField="BiometricNo" HeaderText="BiometricNo" >                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                     <asp:BoundField  DataField="AccountNo" HeaderText="AccountNo" >                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                           <%--<asp:BoundField  DataField="BiometricNo" HeaderText="BiometricNo" >                                            
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>--%>
                                    <%--     <asp:BoundField DataField="StateName" HeaderText="State Name" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                     <asp:BoundField DataField="CountieName" HeaderText="Countie Name" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                     <asp:BoundField DataField="PayamName" HeaderText="Payam Name" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                                     
                                                     <asp:BoundField DataField="BomaName" HeaderText="Boma Name" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                    --%>
                                    <%-- <asp:TemplateField  HeaderText="Allocated Works" >
                                                <ItemTemplate>
                                                    <asp:GridView ID="GrdWorksAllocated" runat="server" AutoGenerateColumns="False">
                                                        <Columns>
                                                            <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                                            <asp:BoundField DataField="Status" HeaderText="Work Status" />
                                                        </Columns>
                                                    </asp:GridView>
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                    <%--        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnHome" runat="server" CssClass="btn btn-primary"
                                        Height="32px" TabIndex="10" Text="Go Back" Width="90px" OnClick="BtnHome_Click" />
                                    &nbsp;
                                    <%--<asp:Button ID="BtnExport" runat="server" CssClass="btn btn-primary" 
                                                Height="32px"  TabIndex="10" Text="Export" Width="90px" OnClick="BtnExport_Click" />--%>
                                    &nbsp;
                                </td>
                            </tr>
                            <%--<tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails0" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails0_RowDataBound" PageSize="15" Width="100%" 
                                        ShowFooter="True" horizontalalign="center">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                                     <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                                    <ItemTemplate >
                                                                        <%# Container.DataItemIndex + 1%>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle HorizontalAlign="Center" />
                                                                    <ItemStyle Width="50px" />
                                                                    
                                                                </asp:TemplateField>
                                            <asp:BoundField DataField="PayamName" HeaderText="Name of the Payam" />                   
                                            <asp:BoundField DataField="IPname" HeaderText="IP Name" />
                                            <asp:BoundField DataField="Type1" HeaderText="No.of Type1" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type2" HeaderText="No.of Type2" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type3" HeaderText="No.of Type3" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type4" HeaderText="No.of Type4" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type5" HeaderText="No.of Type5" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                            <asp:BoundField DataField="Type6" HeaderText="No.of Type6" >
                                                <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>
                                           <asp:BoundField DataField="TotalWorkProposed" HeaderText="Total" >                                            
                                               <ItemStyle HorizontalAlign="Center" />
                                                     </asp:BoundField>                                            
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                </td>                          
                            </tr>--%>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

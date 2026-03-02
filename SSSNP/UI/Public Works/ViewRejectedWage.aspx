<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"
    AutoEventWireup="true" Inherits="ViewRejectedWage" CodeBehind="ViewRejectedWage.aspx.cs" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <%-- <script type="text/javascript">
        //<![CDATA[
        function CheckAll(oCheckbox) {
            var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
            for (i = 1; i < GridView2.rows.length; i++) {
                GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
            }
        }
         //]]>
    </script>--%>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);
            $('#txtStartDate').datetimepicker();
        });
    </script>

    <script type="text/javascript" language="javascript">
        function OpenPopup() {
            window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");
            return false;
        }
    </script>

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here &nbsp;!&nbsp; &nbsp; &nbsp;
                    <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
                    <li class=""><i class="fa fa-fw fa-desktop"></i>Wages Dashboard </li>
                    <li class="active"><i class="fa fa-edit"></i><a href="#">View Beneficiary wise Wages</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">View Rejected Wage Payment Request Details</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                            <asp:Label ID="lblWageRequestNo" runat="server" CssClass="LBLBLACK" Width="128px"></asp:Label>
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
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblProject" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label370" runat="server" CssClass="LBLBLACK" Width="165px">Work Code</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkCode" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trWageRequest">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblWagwRequest" runat="server" CssClass="LBLBLACK" Width="137px">Request Number</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblRequestNumber" runat="server" CssClass="LBLBLACK" Font-Bold="True" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trWorkStartDate">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label417" runat="server" CssClass="LBLBLACK" Width="137px">Approved 
                                                        Date</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblWorkStartDate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="128px">Wage 
                                                        Rate (USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblwagerate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trwagerate" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Width="128px">Exchange Rate</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblexchangerate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trAmountSSP" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="128px">Wage Rate (SSP)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblAmountInSSP" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>

                                                <tr runat="server" id="trFromDate">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label412" runat="server" CssClass="LBLBLACK" Width="128px">From Date</asp:Label>
                                                    </td>
                                                    <%--<td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblfromdate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trToDate">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label413" runat="server" CssClass="LBLBLACK" Width="128px">To Date</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lbltodate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trBenfCount">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="trBenfCountLabel" runat="server" CssClass="LBLBLACK" Width="128px">Total Beneficiaries</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblBenfCountValue" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trPaidCount">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPaidCountLabel" runat="server" CssClass="LBLBLACK" Width="128px">Paid Beneficiaries</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblPaidCountValue" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trUnpaidCount">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblUnpaidCountLabel" runat="server" CssClass="LBLBLACK" Width="128px">Unpaid Beneficiaries</asp:Label>
                                                    </td>
                                                    <%-- <td style="padding: 5px; margin: 5px">
                                                        :</td>--%>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:Label ID="lblUnpaidCountValue" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr runat="server" id="trState">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="165px">State</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblState" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trCounty">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="165px">County</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblCounty" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trPayam">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label429" runat="server" CssClass="LBLBLACK" Width="165px">Payam</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblPayam" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr runat="server" id="trBoma">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label364" runat="server" CssClass="LBLBLACK" Width="165px">Boma 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblBoma" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label432" runat="server" CssClass="LBLBLACK" Width="165px">Paying Agent</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblpayingagent" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                        <asp:DropDownList ID="ddlPayingAgent" runat="server" class="form-control txtbox" Width="178px" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label433" runat="server" CssClass="LBLBLACK" Width="165px">Co-Ordinator</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblcoordinator" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                        <asp:DropDownList ID="ddlCoordinator" runat="server" class="form-control txtbox" Width="178px" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label434" runat="server" CssClass="LBLBLACK" Width="165px">Specialist</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblspecialist" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                        <asp:DropDownList ID="ddlSpecialist" runat="server" class="form-control txtbox" Width="178px" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="165px">Manager</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblmanager" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                                        <asp:DropDownList ID="ddlManager" runat="server" class="form-control txtbox" Width="178px" Visible="false" AutoPostBack="true">
                                                            <asp:ListItem Value="">--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top"
                                            align="center">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" colspan="2">
                                            <asp:Label ID="Label348" runat="server" Text="Reference Documents" CssClass="LBLBLACK"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" colspan="2">
                                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                CssClass="GRD" ForeColor="#333333" Height="62px" PageSize="15" OnRowDataBound="GridView1_RowDataBound"
                                                OnRowDeleting="GridView1_RowDeleting" DataKeyNames="intImageId"
                                                ShowFooter="false" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:CommandField ShowDeleteButton="True" HeaderText="Delete"></asp:CommandField>
                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                        <ItemTemplate>
                                                            <%# Container.DataItemIndex +1 %>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle Width="30px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="File Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("PhotoFileName") %>'></asp:HyperLink>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="Description" HeaderText="Description" />
                                                    <asp:BoundField DataField="UploadedBy" HeaderText="Uploaded by" />
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
                                        <td colspan="2">
                                            <telerik:RadGrid ID="rgDetails" AutoGenerateColumns="false" runat="server" AllowPaging="true"
                                                AllowMultiRowSelection="true" AllowSorting="true" GridLines="Both"
                                                MasterTableView-TableLayout="Fixed"
                                                OnNeedDataSource="rgDetails_NeedDataSource" OnItemDataBound="rgDetails_ItemDataBound" OnItemCommand="rgDetails_ItemCommand"
                                                PagerStyle-AlwaysVisible="true" PageSize="250" EnableLinqExpressions="false">
                                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                <ClientSettings Selecting-AllowRowSelect="true">
                                                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="true" AllowResizeToFit="true" />
                                                    <Selecting AllowRowSelect="True" />
                                                    <%--<ClientEvents OnRowSelected="rowSelected" />--%>
                                                </ClientSettings>
                                                <MasterTableView TableLayout="Fixed"
                                                    DataKeyNames="NameofHousehold, intBenid"
                                                    ClientDataKeyNames="NameofHousehold,intBenid">
                                                    <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>
                                                    <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                                    <Columns>
                                                        <telerik:GridTemplateColumn UniqueName="TemplateColumn" HeaderText="S.No">
                                                            <ItemTemplate>
                                                                <asp:Label ID="rowNumberLabel" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="50px" />
                                                        </telerik:GridTemplateColumn>
                                                        <telerik:GridBoundColumn DataField="intBenid" Display="false" HeaderText="ID" HeaderStyle-Width="8%" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="EnrollmentNo" HeaderText="Enrollment No" HeaderStyle-Width="15%" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="NameofHousehold" HeaderText="Name Of Household" HeaderStyle-Width="30%" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Age" HeaderText="Age" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Sex" HeaderText="Gender" HeaderStyle-Width="8%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ContactNo" HeaderText="PhoneNumber" HeaderStyle-Width="12%" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="SelectionCriteria" HeaderText="Component" HeaderStyle-Width="35%" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="cnt" HeaderText="No of Days Present" HeaderStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AbsenceDays" HeaderText="No of Days Absent" HeaderStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="Amt" HeaderText="Amount in USD" HeaderStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="ExchangeRateNew" HeaderText="Exchange Rate" HeaderStyle-Width="18%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="AmountInSSP" HeaderText="Amount in SSP" HeaderStyle-Width="18%" DataFormatString="{0:n2}" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PaymentStatus" HeaderText="Payment Status" HeaderStyle-Width="15%" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                        <telerik:GridBoundColumn DataField="PaidDate" HeaderText="Payment Date" HeaderStyle-Width="18%" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"></telerik:GridBoundColumn>
                                                    </Columns>
                                                </MasterTableView>
                                            </telerik:RadGrid>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                            valign="top">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding: 5px; margin: 5px" valign="top">&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnDelete_Click" TabIndex="10" Text="Delete" Visible ="False"
                                                Width="120px" />
                                            &nbsp;&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                            valign="top">&nbsp;
                                        </td>
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
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" ShowMessageBox="True"
                                                ShowSummary="False" ValidationGroup="group" />
                                        </td>
                                    </tr>
                                </table>
                                <td style="width: 27px"></td>
                                <td valign="top"></td>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px" valign="top">&nbsp;&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 5px; margin: 5px" valign="top"></td>
                                </tr>
                                <tr>
                                    <td colspan="3" style="padding: 5px; margin: 5px; text-align: left;" valign="top"></td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="3" style="padding: 5px; margin: 5px"></td>
                                </tr>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

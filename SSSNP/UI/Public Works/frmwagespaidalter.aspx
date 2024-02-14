<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/TstMaster.master" AutoEventWireup="true" Inherits="frmwagespaidalter" Title=":: SSSNP Online Management System ::" EnableEventValidation="false" CodeBehind="frmwagespaidalter.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
<style>
.res {
            overflow-x:scroll;
            
}
.RadGrid1 {
    font-family: Poppins;
    border-width: 0px;
    border-style: solid;
    display: block;
    width: 100%;
    overflow-x: auto;
    -webkit-overflow-scrolling: touch;
    -ms-overflow-style: -ms-autohiding-scrollbar;
}
.MyGridHeader {
            background-color: #dedede !important;
            font-family: Poppins;
            vertical-align: middle;
            /*background-image:none !important;*/
        }

        .MyGridAlternatingRow {
            font-family:Poppins;
            background-color: #D1DDF1 !important;
        }
        .RadGrid_Default .rgRow>td
        {
            font-family:Poppins;
            background-color: #fff;
        }
        div.RadGrid_Default .rgHeader, div.RadGrid_Default th.rgResizeCol .HeaderTitle {
            background-image: none;
            color: #fff !important;
            font-size: 12px !important;
            font-weight: bold;
            font-family: Poppins;
            background-color: #013161 !important;
            vertical-align: top;
        }
        div.RadGrid_Default .rgHeader a {
                color: #fff !important;
                font-size: 12px !important;
                font-weight: bold;
                font-family: Poppins;
                vertical-align: top;
            }

    </style>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title" id="h3" runat="server">Paid Alternate not registered in MIS Report</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">

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
                                                                Height="33px" Width="180px" ValidationGroup="group"
                                                                onselectedindexchanged="ddlIP_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">&nbsp;</td>
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
                                                            <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="124px">Household No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txthousehold" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px">
                                                            </asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">&nbsp;
                                                        </td>
                                                    </tr>

                                                    <tr __designer:mapid="17f5" runat="server" visible="false">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True"
                                                                class="form-control txtbox" Height="33px"
                                                                onselectedindexchanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;</td>
                                                    </tr>
                                                    <tr id="Tr1" __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">Type</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="DdlType" runat="server"
                                                                class="form-control txtbox" Height="33px"
                                                                onselectedindexchanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                                                <asp:ListItem Value="1">Alternate 3 not registered in MIS</asp:ListItem>
                                                                <asp:ListItem Value="2">Alternate not registered in MIS</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">&nbsp;</td>
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
                                                            <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="124px">Wage Request No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">:</td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtWorkcode0" runat="server" class="form-control txtbox"
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
                                                &nbsp;
                                        </tr>
                                    </table>
                                </td>
                            </tr>

                            <tr>

                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="Poppins"
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>

                                    <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="Poppins"
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr style="overflow-x: scroll; overflow-y: hidden;">
                            <td colspan="3" style="max-width:750px">
                                    <telerik:radgrid id="RadGrid1" pagesize="50" autogeneratecolumns="false" runat="server" allowpaging="true"
                                        allowmultirowselection="true" allowsorting="true" gridlines="None" GridLinesVisibility="None"
                                        Font-Names="Poppins" ForeColor="#333333" 
                                        mastertableview-tablelayout="Auto"
                                        onneeddatasource="RadGrid1_NeedDataSource" onitemdatabound="RadGrid1_ItemDataBound">
                                        <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                     
                                        
                            <MasterTableView TableLayout="Auto" Width="100%" >                             
                                        <NoRecordsTemplate>&nbsp;&nbsp;&nbsp;No results found</NoRecordsTemplate>    
                                <AlternatingItemStyle CssClass="MyGridAlternatingRow" />
                                               			        
                                <Columns>
                                    <telerik:gridtemplatecolumn uniquename="TemplateColumn" headertext="S.No">
                                       <ItemTemplate>
                                            <asp:Label ID="rowNumberLabel" runat="server" Style="border-top-left-radius: 10px;" />
                                        </ItemTemplate>
                                             <HeaderStyle CssClass="rgHeader" BackColor="#013161" />
                                    </telerik:gridtemplatecolumn>
                                    <telerik:gridboundcolumn datafield="IPName" display="false" headertext="Name of the IP" headerstyle-width="8%"> </telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="StateName" headertext="State Name" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="CountieName" headertext="County Name" headerstyle-width="8%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="PayamName" headertext="Payam Name" headerstyle-width="8%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="BomaName" headertext="Boma Name" headerstyle-width="18%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="WorkCode" headertext="Work Code" headerstyle-width="12%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="ReqNo" headertext="Req No" headerstyle-width="12%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="EnrollmentNo" headertext="Enrollment No" headerstyle-width="12%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="NameofHousehold" headertext="Name ofH ousehold" headerstyle-width="14%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="household_number" headertext="household number" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="Age" headertext="Age" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="Sex" headertext="Gender" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="FromDate" headertext="Request Date" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="AttendanceDays" headertext="Attendance Days" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="WageRateUSD" headertext="Wage Rate USD" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="PaidAmount" headertext="Amount Paid" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="PaidDate" headertext="Paid Date" headerstyle-width="20%"></telerik:gridboundcolumn>
                                    <telerik:gridboundcolumn datafield="PaidTo" headertext="Alternate Payee number" headerstyle-width="20%"></telerik:gridboundcolumn>
                                </Columns>
                            </MasterTableView>
                        </telerik:radgrid>
                                </td>
                            </tr>

                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px 5px 5px 45px">
                                    <asp:HiddenField ID="hdnStat" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>


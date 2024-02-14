<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="HDPiaRpt" Title=":: SSSNP Public Works  :: Help Desk" Codebehind="HDPiaRpt.aspx.cs" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
       <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>
<div align="left">
    <ol class="breadcrumb">You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i>  <a href="Home.aspx">Dashboard</a>
                            </li>
                            <li class="">
                                <i class="fa fa-fw fa-table"></i> Helpdesk
                            </li>
                            <li class="active">
                                <i class="fa fa-edit"></i> <a href="#">Status</a>
                            </li>
                        </ol>
     </div>
    
<div align="center">
<div class="row" align="center">
                    <div class="col-lg-11" >
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Abstract Report</h3>
                            </div>
                            <div class="panel-body" >
                                                    <table style="vertical-align: top; text-align: center;" cellpadding="0" cellspacing="0"
                                                        width="100%">
                                                        <tr>
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: middle; height: 35px; text-align: left; margin-left: 40px;" 
                                                                valign="middle" >
                                                                <table style="width: 96%; height: 53px" width="80%">
                                                                    <tr>
                                                                        <td style="width: 50%; height: 51px;" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" style="width: 50%">
                                                                                <tr>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label id="lblFromDate" runat="server" CssClass="LBLBLACK" Text="From date  (DD-MMM-YYYY)"
                                                                                            Width="175px"></asp:Label></td>
                                                                                    <td align="right" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 3%; padding-top: 5px; text-align: left">
                                                                                        <asp:Label id="Label3" runat="server" CssClass="LBLBLACK" Text=":" Width="2px"></asp:Label></td>
                                                                                    <td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:TextBox id="txtFromdate" runat="server" class="form-control txtbox" MaxLength="11" tabIndex="1"
                                                                                            Width="180px">
                                                                                        </asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="txtFromdate_CalendarExtender" runat="server" 
                                                                                            Format="dd-MMM-yyyy" PopupButtonID="txtFromdate" TargetControlID="txtFromdate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <%--<td align="left" style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                                        vertical-align: top; width: 22%; padding-top: 5px; text-align: left">
                                                                                        <asp:Image id="imgCalendarStartDate" runat="server" alt="" height="100%" src="Images/CAL.GIF"
                                                                                            style="width: 16px" tabIndex="1">
                                                                                        </asp:Image>&nbsp;</td>--%>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                        <td style="width: 50%; height: 51px;" valign="top">
                                                                            <table cellpadding="0" cellspacing="0" style="width: 50%; height: 40px">
                                                                                <tr>
                                                                                    <td align="right" 
                                                                                        style="padding: 5px; vertical-align: top; width: 2%; text-align: left; height: 26px;">
                                                                                        <asp:Label id="lblToDate" runat="server" CssClass="LBLBLACK" Text="To date  (DD-MMM-YYYY)"
                                                                                            Width="161px"></asp:Label></td>
                                                                                    <td align="right" 
                                                                                        style="padding: 5px; vertical-align: top; width: 2%; text-align: left; height: 26px;">
                                                                                        <asp:Label id="Label4" runat="server" CssClass="LBLBLACK" Text=":" Width="2px"></asp:Label></td>
                                                                                    <td align="left" 
                                                                                        style="padding: 5px; vertical-align: top; width: 2%; text-align: left; height: 26px;">
                                                                                        <asp:TextBox id="txtTodate" runat="server" class="form-control txtbox" MaxLength="11" tabIndex="1"
                                                                                            Width="180px">
                                                                                        </asp:TextBox>
                                                                                        <cc1:CalendarExtender ID="txtTodate_CalendarExtender" runat="server" 
                                                                                            Format="dd-MMM-yyyy" PopupButtonID="txtTodate" TargetControlID="txtTodate">
                                                                                        </cc1:CalendarExtender>
                                                                                    </td>
                                                                                    <%--<td align="left" 
                                                                                        style="padding: 5px; vertical-align: top; width: 22%; text-align: left; height: 26px;">
                                                                                        <asp:Image id="imgCalendarEndDate" runat="server" alt="" height="57%" src="Images/CAL.GIF"
                                                                                            style="width: 16px" tabIndex="1">
                                                                                        </asp:Image></td>--%>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: middle; height: 35px; text-align: center" 
                                                                valign="middle">
                                                                &nbsp;<asp:Label ID="Label5" runat="server"></asp:Label>
                                                                <asp:Button ID="Btnsearch" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10"
                                                                    Text="Search" ToolTip="To Search  the data" ValidationGroup="group" 
                                                                    onclick="Btnsearch_Click" />
                                                                <asp:Label ID="LblStatus" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                                    Font-Size="12px" ForeColor="Red"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td align="center" 
                                                                style="padding: 5px; vertical-align: middle; text-align: left; height: 22px;" 
                                                                valign="middle">
                                                            </td>
                                                        </tr>--%>
                                                        <tr style="width:100%">
                                                            <td align="center" 
                                                                valign="middle">
                                                                                       
                                                                                        <asp:GridView ID="grdHelpdesk" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                                            CssClass="GRD" ForeColor="#333333" GridLines="None" 
                                                                                            PageSize="15" ShowFooter="True" Style="position: static" Width="100%" 
                                                                                            onrowdatabound="grdHelpdesk_RowDataBound" Visible="False">
                                                                                            <RowStyle BackColor="#EFF3FB" CssClass="GRDITEM" HorizontalAlign="Left" 
                                                                                                VerticalAlign="Middle" />
                                                                                            <Columns>
                                                                                                <asp:BoundField HeaderText="Sno" />
                                                                                                <asp:BoundField HeaderText="FeedBack Type" DataField="fb_type" />
                                                                                                <asp:HyperLinkField HeaderText="Total No Of Issues" DataTextField="Total" >  
                                                                                                            <%--NavigateUrl="frmViewApplStatus.aspx?ComplaintNo=">--%>
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:HyperLinkField>
                                                                                                <asp:HyperLinkField HeaderText="Open Issues" DataTextField="Open1" >  
                                                                                                            <%--NavigateUrl="frmViewApplStatus.aspx?ComplaintNo=">--%>
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:HyperLinkField>
                                                                                                <asp:HyperLinkField HeaderText="Close Issues" DataTextField="Close1" >  
                                                                                                            <%--NavigateUrl="frmViewApplStatus.aspx?ComplaintNo=">--%>
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:HyperLinkField>
                                                                                                <asp:HyperLinkField HeaderText="Rejected Issues" DataTextField="Reject" >  
                                                                                                            <%--NavigateUrl="frmViewApplStatus.aspx?ComplaintNo=">--%>
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                                </asp:HyperLinkField>
                                                                                                <asp:BoundField DataField="intfb_id" HeaderText="FBID" Visible="False" />
                                                                                            </Columns>
                                                                                            <FooterStyle BackColor="#093461" Font-Bold="True" ForeColor="White" />
                                                                                            <PagerStyle BackColor="#05693b" ForeColor="White" HorizontalAlign="Center" />
                                                                                            <EmptyDataTemplate>
                                                                                                <table align="center">
                                                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Verdana" 
                                                                                                        Font-Size="11px" ForeColor="Red" Text="No Issues Found"></asp:Label>
                                                                                                </table>
                                                                                            </EmptyDataTemplate>
                                                                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                                            <HeaderStyle BackColor="#093461" CssClass="GRDHEADER" Font-Bold="True" 
                                                                                                ForeColor="White" />
                                                                                            <EditRowStyle BackColor="#05693b" />
                                                                                            <AlternatingRowStyle BackColor="White" />
                                                                                        </asp:GridView>
                                                            </td>
                                                        </tr>
                                                        <%--<tr>
                                                            <td align="center" 
                                                                style="padding-right: 5px; padding-left: 5px; padding-bottom: 5px;
                                                                vertical-align: middle; padding-top: 5px; height: 35px; text-align: center" 
                                                                valign="middle">
                                                                &nbsp;&nbsp;&nbsp;</td>
                                                        </tr>--%>
                                                        </table>
                                                        <table id="tbl1" runat="server" border="1" bordercolor="#54503A" 
                                                        style="color: black; font-size: 12px;">
                                                                               
                                                        </table>
                                                   </div>
                        </div>
                    </div>
                </div>

    </div>
</ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


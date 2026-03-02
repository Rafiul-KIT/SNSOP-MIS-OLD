<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master"
    AutoEventWireup="true" Inherits="UI_Public_Works_Lookups_EnviranmnetManagementLookup"
    Title="Enviranmnet Management Lookup" Codebehind="EnviranmnetManagementLookup.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
   function GetRowValue(val)
    {   
    if(val != '&nbsp;')
    {   
        val1 = 0;
        window.opener.ApplyChanges(val, val1);

   // window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
    //window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfFlagID').value = val1;

    }
    //window.opener.document.forms[0].submit();
    self.close();
   
    }
    
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            &nbsp;Environment Management Lookup</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                   <table cellpadding="4" cellspacing="5" style="width: 83%" 
                                                __designer:mapid="17eb">
                                                <tr __designer:mapid="17ec">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="144px">IP</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox" Height="33px"
                                                    Width="180px"  AutoPostBack="True">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr __designer:mapid="17ec">
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">Project Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                <asp:DropDownList ID="ddlNameOfPrject" runat="server" class="form-control txtbox" Height="33px"
                                                    Width="180px"  AutoPostBack="True">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr __designer:mapid="17f5">
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="124px">State</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                <asp:DropDownList ID="ddlState" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" runat="server" class="form-control txtbox" Height="33px"
                                                    AutoPostBack="True" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        </table>
                                        </td>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 83%" __designer:mapid="1800">
                                        <tr __designer:mapid="1801">
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County / Municipality Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="1804">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                <asp:DropDownList ID="ddlCounties" OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged" runat="server" class="form-control txtbox" Height="33px"
                                                   Width="180px" AutoPostBack="True" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="1809">
                                                &nbsp;
                                            </td>
                                        </tr>
                                       
                                        
                                  
                                 
                                <tr __designer:mapid="180a">
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="144px">Payam / Block Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                <asp:DropDownList ID="ddlPayam" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged" runat="server" class="form-control txtbox" Height="33px"
                                                   Width="180px" AutoPostBack="True" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                &nbsp;
                                            </td>
                                        </tr>
                                 <tr __designer:mapid="180a">
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="144px">Boma / Quarter Council Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                :
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Height="33px"
                                                  Width="180px"  AutoPostBack="True" >
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                &nbsp;
                                            </td>
                                        </tr>
                                       
                                </table>
                                </td>
                                <td style="width: 27px">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                </td>
                                
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                         TabIndex="10" Text="Search" Width="90px" onclick="BtnSave1_Click" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" CssClass="btn btn-danger"
                                        Height="32px"  TabIndex="10" Text="Cancel" ToolTip="To Clear  the Screen"
                                        Width="90px" onclick="BtnClear_Click" />
                                    &nbsp;
                                    <%--<asp:Button ID="BtnPrint" runat="server" CssClass="btn btn-warning" Height="32px"
                                         ToolTip="Print" TabIndex="10" Text="Print" ValidationGroup="group"
                                        Width="90px" Visible="false" />
                                    &nbsp;
                                    <asp:Button ID="btnexp" runat="server" CausesValidation="False" CssClass="btn btn-success"
                                        Font-Size="12px" Height="32px"  Style="position: static"
                                        Text="Export" ValidationGroup="vg" Visible="false" />--%>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <div runat="server" id="div_print">
                                        <asp:Panel ID="pnlContents" runat="server">
                                            <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                                CellPadding="4" CssClass="GRD" ForeColor="#333333" GridLines="None" Height="62px"
                                                onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" 
                                                PageSize="15" Width="100%">
                                                <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:LinkButton ID="LinkButton1" class="fa fa-edit" runat="server">Select</asp:LinkButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                   <%-- <asp:BoundField DataField="Grievance_RegistrationNo" HeaderText="Grievance Registration No">
                                                    </asp:BoundField>--%>
                                                    <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                                    <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                                    <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                                    <asp:BoundField DataField="BomaName" HeaderText="Boma Name" />
                                                       <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                                    <%--<asp:BoundField DataField="Complainant_Name" HeaderText="Complainant Name" />
                                                    <asp:BoundField DataField="Complainant_EnrolmentNo" HeaderText="Complainant Enrolment No" />
                                                    <asp:BoundField DataField="TypeOfComplaints" HeaderText="Type Of Complaints" />
                                                    <asp:BoundField DataField="Accused_Name" HeaderText="Accused Name" />
                                                    <asp:BoundField DataField="Accused_EnrolmentNo" HeaderText="Accused Enrolment No" />--%>
                                                </Columns>
                                                <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#B9D684" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

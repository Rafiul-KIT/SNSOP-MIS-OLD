<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/Lookups/CCLookupsMaster.master" AutoEventWireup="true" Inherits="LookupBDC" Title=":: SSSNP Online Management System ::" Codebehind="LookupBDC.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<script type="text/javascript">
   function GetRowValue(val)
    {   
    if(val != '&nbsp;')
    {   
        val1 = 0;
        window.opener.ApplyChanges(val, val1);

    //window.opener.document.getElementById('ctl00_ContentPlaceHolder1_hdfID').value = val;
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
                            &nbsp;Boma/Quarter Council  Development Committe Lookup</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 80%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%" 
                                                __designer:mapid="363">
                                                <tr __designer:mapid="364">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="365">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="367">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="368">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True" 
                                                            class="form-control txtbox" Height="33px" Width="180px" 
                                                            onselectedindexchanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="36b">
                                                        &nbsp;</td>
                                                </tr>
                                                <tr __designer:mapid="36d">
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="36e">
                                                        <asp:Label ID="Label351" runat="server" CssClass="LBLBLACK" Width="165px">County 
                                                        Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="370">
                                                        :</td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="371">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" AutoPostBack="True" 
                                                            class="form-control txtbox" Height="33px" 
                                                            onselectedindexchanged="ddlCounties_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px" __designer:mapid="374">
                                                        &nbsp;</td>
                                                </tr>
                                                </table>
                                </td>
                                <td style="width: 27px">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                </td>
                                <td valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label352" runat="server" CssClass="LBLBLACK" 
                                                    Width="165px">Payam Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :</td>
                                            <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPayams" runat="server" class="form-control txtbox" 
                                                            Height="33px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            
                                                        </asp:DropDownList>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label304" runat="server" CssClass="LBLBLACK" 
                                                    Width="165px">Boma/Quarter Council  Development Committe Name</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                :</td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:TextBox ID="txtBDC" runat="server" class="form-control txtbox" 
                                                    Height="28px" MaxLength="40" TabIndex="1" ValidationGroup="group" 
                                                    Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" 
                                        Font-Size="13px" ForeColor="#006600"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" 
                                        Height="32px" onclick="BtnSave_Click" TabIndex="10" Text="Search" 
                                        Width="90px" />
                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False" 
                                        CssClass="btn btn-warning" Height="32px" onclick="BtnClear_Click" TabIndex="10" 
                                        Text="Cancel" ToolTip="To Clear  the Screen" Width="90px" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" 
                                        AutoGenerateColumns="False" CellPadding="4" CssClass="GRD" ForeColor="#333333" 
                                        GridLines="None" Height="62px" 
                                        onpageindexchanging="grdDetails_PageIndexChanging" 
                                        onrowdatabound="grdDetails_RowDataBound" PageSize="15" Width="100%">
                                        <footerstyle backcolor="#013161" font-bold="True" forecolor="White" />
                                        <rowstyle backcolor="#EBF2FE" cssclass="GRDITEM" horizontalalign="Left" 
                                            verticalalign="Middle" />
                                        <columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="LinkButton1" class="fa fa-edit" runat="server">Select</asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="StateName" HeaderText="State Name" />
                                            <asp:BoundField DataField="CountieName" HeaderText="County Name" />
                                            <asp:BoundField DataField="PayamName" HeaderText="Payam Name" />
                                            <asp:BoundField DataField="BDCName" HeaderText="Boma/Quarter Council  Development Committe" />
                                            <asp:BoundField DataField="Email" HeaderText="Email" />
                                        </columns>
                                        <pagerstyle backcolor="#013161" forecolor="White" horizontalalign="Center" />
                                        <selectedrowstyle backcolor="#D1DDF1" font-bold="True" forecolor="#333333" />
                                        <headerstyle backcolor="#013161" cssclass="GRDHEADER" font-bold="True" 
                                            forecolor="White" />
                                        <editrowstyle backcolor="#B9D684" />
                                        <alternatingrowstyle backcolor="White" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </td>
        </tr>
    </table>

</asp:Content>


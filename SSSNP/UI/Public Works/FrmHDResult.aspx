<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="FrmUsers" Title=":: SSSNP Public Works ::  Result" Codebehind="FrmHDResult.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div style="width: 90%; text-align: center; margin:0 auto;">
    <table style="width: 100%; position: static; text-align: center;">
                    <tr>
                        <td style="width: 127px">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" style="text-align: center">
                            <asp:Label ID="lblresult" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="11px"
                                ForeColor="Green" Style="position: static"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            </td>
                    </tr>
                  <%--  <tr>
                        <td colspan="3" style="padding-bottom: 20px; padding-top: 20px" align="center">
                         <a href="print_new.aspx" target="_blank" style="color: blue">PRINT</a></td>
                    </tr>--%>
                     <tr>
                        <td colspan="3" style="padding-bottom: 20px; padding-top: 20px" align="center">
                         <a href="Helpdesk.aspx" target="_self" style="color: blue">NEW REGISTRATION</a></td>
                    </tr>
                </table>
</div>
</asp:Content>


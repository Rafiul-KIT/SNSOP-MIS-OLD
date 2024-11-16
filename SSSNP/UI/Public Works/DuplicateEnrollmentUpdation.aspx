<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Public Works/CCMaster.master" Inherits="DuplicateEnrollmentUpdation" Title=":: :: SNSOP : ::" Codebehind="DuplicateEnrollmentUpdation.aspx.cs" %>

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
        function UserDeleteConfirmation() {
            if (confirm("Are you sure you want to Transfer?"))
                return true;
            else
                return false;
        }
    
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            Duplicate Updation</h3>
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
                                                            <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Benificiary Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtBenificiaryName" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px" OnSelectedIndexChanged="ddlState_SelectedIndexChanged" AutoPostBack="True">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox" Height="33px"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr id="Tr1" __designer:mapid="17f5" r runat="server" visible="false">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" class="form-control txtbox"
                                                                Height="33px" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                            &nbsp;
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
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1804">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                            <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox" Height="28px"
                                                                Width="180px" AutoPostBack="True" OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged1">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1809">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Height="28px"
                                                                Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="124px">Enrollment No</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:TextBox ID="txtEnrollmentNo" runat="server" class="form-control txtbox" Height="28px"
                                                                MaxLength="40" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                &nbsp;
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
                                        ForeColor="#006600" ></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <asp:GridView ID="grdDetails" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        CellPadding="4" CssClass="GRD" ForeColor="#333333" Height="62px" OnPageIndexChanging="grdDetails_PageIndexChanging"
                                        OnRowDataBound="grdDetails_RowDataBound" PageSize="50" Width="99%" OnSelectedIndexChanged="grdDetails_SelectedIndexChanged"
                                        ShowFooter="True">
                                        <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                        <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderText="S No">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex + 1%>
                                                    <asp:HiddenField ID="hdfid" runat="server" />
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle Width="50px" />
                                            </asp:TemplateField>
                                               <asp:TemplateField HeaderText="All">
                                                        <HeaderTemplate>
                                                            <%-- <asp:CheckBox ID="CheckBox2" runat="server" />--%>
                                                            <asp:CheckBox ID="ChKAll" runat="server" __designer:wfdid="w5" 
                                                                AutoPostBack="True" onclick="CheckAll(this);" />
                                                            &nbsp;
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBox1" runat="server" __designer:wfdid="w4" />
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                            <asp:BoundField DataField="BenfiName" HeaderText="Benificiary Name" />
                                            <asp:BoundField DataField="EnrollmentNo" HeaderText="Enrollment No" />
                                          
                                            <asp:BoundField DataField="ContactNo" HeaderText="Contact No" />
                                        </Columns>
                                        <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#B9D684" />
                                        <FooterStyle HorizontalAlign="Center" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <asp:HiddenField ID="hdnStat" runat="server" />
                                </td>
                            </tr>
                            
                            
                              <tr>
                                                    <td align="left" colspan="3" style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label369" runat="server" CssClass="LBLBLACK" Font-Bold="True" 
                                                            Width="267px">Transfer To</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">
                                                        &nbsp;</td>
                                                </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <table align="center" cellpadding="10" cellspacing="5" style="width: 80%" visible="false" runat="server">
                                        <tr>
                                            <td style="padding: 5px; margin: 5px" valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 83%" __designer:mapid="17eb" runat="server" id="TrasDisplay">
                                                    <tr __designer:mapid="17ec">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ed">
                                                            <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17ef">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f0">
                                                            <asp:DropDownList ID="ddlstate1" runat="server" class="form-control txtbox" Height="33px"
                                                                Width="180px" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlstate1_SelectedIndexChanged" >
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f4">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                                                            ControlToValidate="ddlstate1" ErrorMessage="Please select State" 
                                                            ValidationGroup="group1" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                               
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="17f5">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f6">
                                                            <asp:Label ID="Label5" runat="server" CssClass="LBLBLACK" Width="124px">County Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f8">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17f9">
                                                            <asp:DropDownList ID="ddlcounty1" runat="server" class="form-control txtbox" Height="33px"
                                                                AutoPostBack="True"   Width="180px" 
                                                                onselectedindexchanged="ddlcounty1_SelectedIndexChanged">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="17fd">
                                                             <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                                                            ControlToValidate="ddlcounty1" ErrorMessage="Please select County" 
                                                            ValidationGroup="group1" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    </table>
                                            </td>
                                            <td style="width: 27px">
                                                <asp:Label ID="Label7" runat="server" CssClass="LBLBLACK" Width="65px"></asp:Label>
                                            </td>
                                            <td valign="top">
                                                <table cellpadding="4" cellspacing="5" style="width: 100%" __designer:mapid="1800" runat="server" id="TrasferBoma1">
                                                    <tr __designer:mapid="1801">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1802">
                                                            <asp:Label ID="Label8" runat="server" CssClass="LBLBLACK" Width="124px">Payam Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1804">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1805">
                                                            <asp:DropDownList ID="ddlpayam1" runat="server" class="form-control txtbox" Height="28px"
                                                                Width="180px" AutoPostBack="True" 
                                                                onselectedindexchanged="ddlpayam1_SelectedIndexChanged" >
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1809">
                                                               <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                                            ControlToValidate="ddlpayam1" ErrorMessage="Please select Payam" 
                                                            ValidationGroup="group1" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                    <tr __designer:mapid="180a">
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180b">
                                                            <asp:Label ID="Label9" runat="server" CssClass="LBLBLACK" Width="144px">Boma Name</asp:Label>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180d">
                                                            :
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="180e">
                                                            <asp:DropDownList ID="ddlboma1" runat="server" class="form-control txtbox" Height="28px"
                                                                Width="180px">
                                                                <asp:ListItem>--Select--</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td style="padding: 5px; margin: 5px" __designer:mapid="1812">
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                                            ControlToValidate="ddlboma1" ErrorMessage="Please select Boma" 
                                                            ValidationGroup="group1" InitialValue="--Select--">*</asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                                &nbsp;
                                            </td>
                                        </tr>
                                        </table>
                             <tr>
                                        <td align="center" 
                                            style="padding: 5px; margin: 5px; text-align: center;" valign="top">
                                            <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" 
                                                Height="32px" onclick="Button1_Click" TabIndex="10" Text="Confirm" 
                                                ValidationGroup="group" Width="155px" />
                                            &nbsp;<asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-danger" 
                                                Height="32px" onclick="BtnSave2_Click" TabIndex="10" Text="Cancel" 
                                                ValidationGroup="group"  Width="90px" />--%>
                                        &nbsp;
                                            <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" 
                                                Height="32px" onclick="Button2_Click" TabIndex="10" Text="Transfer" 
                                                ValidationGroup="group1" Width="155px" OnClientClick="return UserDeleteConfirmation();" />
                                        </td>
                                    </tr>
                                        <tr>
                                        <td  style="padding: 5px; margin: 5px" valign="top">
                                            <div ID="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" 
                                                    href="#">×</a> <strong>Success!</strong><asp:Label 
                                                    ID="Label2" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td  style="padding: 5px; margin: 5px" valign="top">
                                            <div ID="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#">×</a> <strong>
                                                Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <asp:HiddenField ID="hdfID" runat="server" />
                                            <asp:HiddenField ID="hdfFlagID" runat="server" />
                                            <asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                                ShowMessageBox="True" ShowSummary="False" ValidationGroup="group1" />
                                        </td>
                                    </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                        
                    </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>


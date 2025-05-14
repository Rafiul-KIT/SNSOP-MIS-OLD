<%@ Page Title=":: :: SNSOP : : Rejected Wages Deletion " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="RejectedWagesDeletion.aspx.cs" Inherits="SSSNP.UI.Public_Works.RejectedWagesDeletion" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            var rigtDynHt = $('.form-left-pan').height();
            //alert(rigtDynHt);
            $('.form-right-pan').css("height", rigtDynHt);

            $('#txtStartDate').datetimepicker();
        });
    </script>

    <script type="text/javascript">
        function PrintPanel() {
            var panel = document.getElementById("<%=pnlContents.ClientID %>");
            var printWindow = window.open('', '', 'height=400,width=800');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;
        }
    </script>

    <script type="text/javascript" language="javascript">

        function OpenPopup() {

            window.open("Lookups/WorkProgressLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");

            return false;
        }
    </script>

    <script type="text/javascript">
        function showDeletePopup(workReqId) {
            document.getElementById('<%= hdnDeleteWorkReqId.ClientID %>').value = workReqId;
            document.getElementById('<%= txtDeleteComment.ClientID %>').value = '';
            document.getElementById('commentLength').innerText = '0/255';
            document.getElementById('<%= pnlDeletePopup.ClientID %>').style.display = 'block';
        }


    function hideDeletePopup() {
        document.getElementById('<%= pnlDeletePopup.ClientID %>').style.display = 'none';
        document.getElementById('commentLength').innerText = '0/255';
        document.getElementById('<%= lblDeleteWarning.ClientID %>').innerText = '';
        }
    </script>

    <script type="text/javascript">
        function validateDeleteComment() {
            var comment = document.getElementById('<%= txtDeleteComment.ClientID %>').value.trim();
        if (comment === "") {
            document.getElementById('<%= lblDeleteWarning.ClientID %>').innerText = "Comment is required.";
                return false;
            }
            return true;
        }
    </script>
    <script type="text/javascript">
        function updateCommentLength() {
            var textbox = document.getElementById('<%= txtDeleteComment.ClientID %>');
            var counter = document.getElementById('commentLength');
            counter.innerText = textbox.value.length + "/255";
        }
    </script>


    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Wages Request Abstract </a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-11">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Rejected Wage Request Drafts</h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label443" runat="server" CssClass="LBLBLACK" Width="165px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label373" runat="server" CssClass="LBLBLACK" Width="165px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="165px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 192px;">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="165px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="width: 100%">
                                                <%--<tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label303" runat="server" CssClass="LBLBLACK" Width="165px">Implementing Partner</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlIP" runat="server" class="form-control txtbox"
                                                            Height="33px" ValidationGroup="group" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>--%>
                                                <tr runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="144px">Work Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkcode" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>

                                                <tr id="Tr1" runat="server">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">Request No</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="TxtRequestNo" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="100" TabIndex="1" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top" align="center">
                                            <asp:Button ID="btnSearch" runat="server" CssClass="btn btn-primary"
                                                Height="32px" OnClick="btnSearch_Click" TabIndex="10" Text="Search"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-danger"
                                                Height="32px" OnClick="BtnCancel_Click" TabIndex="10" Text="Cancel"
                                                ValidationGroup="group" Width="90px" />
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <div runat="server" id="div_print">
                                                <asp:Panel ID="pnlContents" runat="server">
                                                    <table runat="server" id="tblreport" visible="false" style="padding: 5px">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="LblResultA" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#CC3300"></asp:Label>
                                                                <br />
                                                                <asp:Label ID="LblResultA0" runat="server" Font-Bold="True" Font-Size="12px"
                                                                    ForeColor="#CC3300"></asp:Label>
                                                                <br />
                                                                <asp:GridView ID="grdRejectedWagesDraft" runat="server" CellPadding="4"
                                                                    CssClass="GRD" ForeColor="#333333"
                                                                    OnPageIndexChanging="grdRejectedWagesDraft_PageIndexChanging" AutoGenerateColumns="false"
                                                                    OnRowDataBound="grdRejectedWagesDraft_RowDataBound"
                                                                    OnSelectedIndexChanged="grdRejectedWagesDraft_SelectedIndexChanged" PageSize="50" AllowPaging="True"
                                                                    Width="100%" OnPreRender="grdRejectedWagesDraft_PreRender">
                                                                    <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#FFFFFF" CssClass="GRDITEM" HorizontalAlign="Left"
                                                                        VerticalAlign="Middle" />
                                                                    <Columns>
                                                                        <asp:TemplateField HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex +1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="Beneficiaries">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink
                                                                                    ID="hlRedirect"
                                                                                    runat="server"
                                                                                    CssClass="fa fa-edit"
                                                                                    Target="_blank"
                                                                                    NavigateUrl='<%# String.Format("ViewRejectedWage.aspx?WorkReqId={0}", Eval("intWagePaymentReqid")) %>'>View</asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>

                                                                        <asp:BoundField DataField="Reqno" HeaderText="Request Number" />
                                                                        <asp:BoundField DataField="StateName" HeaderText="State" />
                                                                        <asp:BoundField DataField="CountieName" HeaderText="County" />
                                                                        <asp:BoundField DataField="PayamName" HeaderText="Payam" />
                                                                        <asp:BoundField DataField="BomaName" HeaderText="Boma" />
                                                                        <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                                                        <asp:BoundField DataField="reqFromDate" HeaderText="From Date" />
                                                                        <asp:BoundField DataField="reqToDate" HeaderText="To Date" />
                                                                        <asp:BoundField DataField="PaymentCycle" HeaderText="Payment Cycle" />
                                                                        <asp:BoundField DataField="Status" HeaderText ="Status" />
                                                                        <asp:BoundField DataField="Remarks" HeaderText="Rejection Reason" />
                                                                        <asp:TemplateField HeaderText="Delete">
                                                                            <ItemTemplate>
                                                                                <a href="javascript:void(0);"
                                                                                    onclick='showDeletePopup("<%# Eval("intWagePaymentReqid") %>")'
                                                                                    class="btn btn-danger btn-sm">Delete</a>
                                                                            </ItemTemplate>
                                                                            <ItemStyle Width="60px" />
                                                                        </asp:TemplateField>
                                                                    </Columns>
                                                                    <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                                    <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True"
                                                                        ForeColor="White" />
                                                                    <EditRowStyle BackColor="#013161" />
                                                                    <AlternatingRowStyle BackColor="White" />
                                                                </asp:GridView>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="lblmessage" runat="server" Font-Bold="True" Font-Size="12px"
                                                ForeColor="#CC3300"></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                <asp:HiddenField ID="hdnDeleteWorkReqId" runat="server" />

                                <asp:Panel ID="pnlDeletePopup" runat="server" CssClass="modalPopup" Style="display: none; position: fixed; top: 30%; left: 40%; z-index: 1000; background: white; padding: 20px; border: 1px solid #999;">
                                    <asp:Label ID="lblPopupTitle" runat="server" Text="Add Comment Before Deletion" Font-Bold="true" Font-Size="14px" />
                                    <br />
                                    <br />

                                    <asp:TextBox ID="txtDeleteComment" runat="server" TextMode="MultiLine" Rows="3" Width="300px" MaxLength="255"
                                        placeholder="Write your comment here..." CssClass="form-control"
                                        onkeyup="updateCommentLength()" />

                                    <br />
                                    <asp:Label ID="lblDeleteWarning" runat="server" ForeColor="Red" />
                                    <br />
                                    <span id="commentLength" style="font-size: 12px; color: gray;">0/255</span>
                                    <br />

                                    <asp:Button ID="btnConfirmDelete" runat="server" Text="Confirm Delete"
                                        CssClass="btn btn-danger"
                                        OnClick="btnConfirmDelete_Click"
                                        OnClientClick="return validateDeleteComment();" />

                                    &nbsp;
                                    <asp:Button ID="btnCancelDelete" runat="server" Text="Cancel" CssClass="btn btn-default"
                                        OnClientClick="hideDeletePopup(); return false;" />
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

<%@ Page Title=":: :: SNSOP : : Wages Payment Apply" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master"
    AutoEventWireup="true" Inherits="UpdateAttachments"
    EnableEventValidation="true" CodeBehind="UpdateAttachments.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript">

        function Check_Click(objRef) {

            //Get the Row based on checkbox

            var row = objRef.parentNode.parentNode;

            if (objRef.checked) {

                //If checked change color to Aqua

                row.style.backgroundColor = "aqua";

            }

            else {

                //If not checked change back to original color

                if (row.rowIndex % 2 == 0) {

                    //Alternating Row Color

                    row.style.backgroundColor = "#C2D69B";

                }

                else {

                    row.style.backgroundColor = "white";

                }

            }



            //Get the reference of GridView

            var GridView = row.parentNode;



            //Get all input elements in Gridview

            var inputList = GridView.getElementsByTagName("input");



            for (var i = 0; i < inputList.length; i++) {

                //The First element is the Header Checkbox

                var headerCheckBox = inputList[0];



                //Based on all or none checkboxes

                //are checked check/uncheck Header Checkbox

                var checked = true;

                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {

                    if (!inputList[i].checked) {

                        checked = false;

                        break;

                    }

                }

            }

            headerCheckBox.checked = checked;



        }

    </script>

    <script type="text/javascript">

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        //If the header checkbox is checked

                        //check all checkboxes

                        //and highlight all rows

                        row.style.backgroundColor = "aqua";

                        inputList[i].checked = true;

                    }

                    else {

                        //If the header checkbox is checked

                        //uncheck all checkboxes

                        //and change rowcolor back to original

                        if (row.rowIndex % 2 == 0) {

                            //Alternating Row Color

                            row.style.backgroundColor = "#C2D69B";

                        }

                        else {

                            row.style.backgroundColor = "white";

                        }

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>

    <%--   <script type="text/javascript">
         //<![CDATA[
         function CheckAll(oCheckbox) {
             var GridView2 = document.getElementById("<%=grdDetails.ClientID %>");
             for (i = 1; i < GridView2.rows.length; i++) {
                 GridView2.rows[i].cells[1].getElementsByTagName("INPUT")[0].checked = oCheckbox.checked;
             }
         }

         //]]>
 </script>--%>

    <script>
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

    <%--  <asp:UpdatePanel ID="upd1" runat="server">
<ContentTemplate>--%>
    <div align="left">
        <ol class="breadcrumb">
            You are here &nbsp;!&nbsp; &nbsp; &nbsp;
            <li><i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a> </li>
            <li class="active"><i class="fa fa-edit"></i><a href="#">Update / Delete Wage Attachements
                reject</a> </li>
        </ol>
    </div>
    <div align="center">
        <div class="row" align="center">
            <div class="col-lg-11">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">Update / Delete Wage Attachements</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 90%">
                            <tr>
                                <td style="padding: 5px; margin: 5px" colspan="2" align="center">
                                    <asp:Label ID="lblWageRequestNo" runat="server" CssClass="LBLBLACK" Width="400px"
                                        Font-Bold="True"></asp:Label>
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
                                        <tr>
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
                                                        Rate(USD)</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">:
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:Label ID="lblwagerate" runat="server" CssClass="LBLBLACK" Width="160px"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
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
                                        <tr>
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
                                    </table>
                                </td>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    <table cellpadding="4" cellspacing="5" style="width: 100%">
                                        <tr>
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
                                        <tr>
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
                                        <tr>
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
                                        <tr>
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
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                            </td>
                                        </tr>
                                        <tr runat="server" visible="false">
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Width="128px">Exchange Rate</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">:
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                <asp:TextBox ID="txtExchangerate" runat="server" class="form-control txtbox" Height="28px"
                                                    MaxLength="8" onkeypress="DecimalOnly()" TabIndex="1" ValidationGroup="group"
                                                    Width="180px" OnTextChanged="txtExchangerate_TextChanged" Enabled="False">54.34</asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="padding: 5px; margin: 5px">&nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px">&nbsp;
                                            </td>
                                            <td style="padding: 5px; margin: 5px">&nbsp;
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
                                <td colspan="2">
                                    <table>
                                        <tr id="lblDescLabel" runat="server">
                                            <td>
                                                <asp:Label ID="Label5" Font-Bold="true" Width="400px" CssClass="LBLBLACK" Text="Upload any reference Documents"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="lblDesc" runat="server">
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label1" runat="server" Width="180px" CssClass="LBLBLACK">Document Description</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">:
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                <asp:TextBox ID="txtDescription" runat="server" class="form-control txtbox" Height="40px"
                                                    MaxLength="200" TabIndex="1" ValidationGroup="group1" Width="180px"></asp:TextBox>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                                                    ErrorMessage="Please Enter To Date" InitialValue="" SetFocusOnError="True" ValidationGroup="group1">*</asp:RequiredFieldValidator>
                                            </td>
                                            <td style="padding: 5px; margin: 5px">
                                                <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">Upload File</asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">:
                                            </td>
                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                <asp:FileUpload ID="FileUpload1" runat="server" onchange="this.form.submit();" class="form-control txtbox"
                                                    Height="28px" />
                                                <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-primary" Height="32px"
                                                    OnClick="Button2_Click" TabIndex="10" Text="Add New" ValidationGroup="group1"
                                                    Width="120px" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="color: Red" colspan="8">
                                                <asp:Label ID="lblresult" CssClass="LBLBLACK" Width="200px" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="lblDesc2" runat="server">
                                            <td style="padding: 5px; margin: 5px; padding-top: 20px;" colspan="8">
                                                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-responsive table-bordered"
                                                    ForeColor="#333333" PageSize="15" ShowFooter="false" Width="100%" 
                                                    OnRowDeleting="GridView2_RowDeleting" OnRowDataBound="GridView1_RowDataBound">
                                                    <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                    <Columns>
                                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" ButtonType="Button"></asp:CommandField>
                                                        <asp:BoundField DataField="Description" HeaderText="Description" />
                                                        <asp:HyperLinkField HeaderText="File Name"></asp:HyperLinkField>
                                                    </Columns>
                                                    <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#013161" CssClass="GRDHEADER" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#B9D684" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; margin: 5px" valign="top">
                                    <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK" Width="50px"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="2" style="padding: 5px; margin: 5px; text-align: center;"
                                    valign="top">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding: 5px; margin: 5px; text-align: center;" valign="top"
                                    align="center">
                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary" Height="32px"
                                        OnClick="BtnSave_Click" TabIndex="10" Text="Update" ValidationGroup="group" Width="155px" />
                                    &nbsp;
                                    <asp:Button ID="Button1" runat="server" CssClass="btn btn-warning" Height="32px"
                                        TabIndex="10" Text="Back" ValidationGroup="group" Width="155px" OnClick="Button1_Click" />
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
                                    <asp:HiddenField ID="hdfIP" runat="server" />
                                    <asp:HiddenField ID="HdfReqNo" runat="server" />
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
    <%--  </ContentTemplate>
  </asp:UpdatePanel>--%>
    <%--</div>
       </td>
        </tr>
    </table>--%>
</asp:Content>

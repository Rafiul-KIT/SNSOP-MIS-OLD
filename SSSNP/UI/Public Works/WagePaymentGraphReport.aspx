<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/EmptyMaster1.master" AutoEventWireup="true" Inherits="WagePaymentGraphReport" Title=":: SSSNP Online Management System ::" Codebehind="WagePaymentGraphReport.aspx.cs" %>

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

    <%-- Here We need to write some js code for load google chart with database data --%>

    <script src="Scripts/jquery-1.7.1.js"></script>

    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script>
        var chartData; // globar variable for hold chart data
        google.load("visualization", "1", { packages: ["corechart"] });

        // Here We will fill chartData

        $(document).ready(function() {

            $.ajax({
                url: "GoogleChart.aspx/GetChartData",
                data: "",
                dataType: "json",
                type: "POST",
                contentType: "application/json; chartset=utf-8",
                success: function(data) {
                    chartData = data.d;
                },
                error: function() {
                    alert("Error loading data! Please try again.");
                }
            }).done(function() {
                // after complete loading data
                google.setOnLoadCallback(drawChart);
                drawChart();
            });
        });


        function drawChart() {
            var data = google.visualization.arrayToDataTable(chartData);

            var options = {
                title: "Company Revenue",
                pointSize: 5
            };

            var barChart = new google.visualization.BarChart(document.getElementById('chart_div'));
            barChart.draw(data, options);

        }
 
    </script>

    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
        <tr>
            <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; height: 675px;"
                valign="top" align="center">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <h3 class="panel-title">
                            &nbsp;Graph report - IP wise</h3>
                    </div>
                    <div class="panel-body">
                        <table align="center" cellpadding="10" cellspacing="5" style="width: 99%">
                            <tr>
                                <td class="col-xs-3" style="padding: 5px; margin: 5px" align="left">
                                    &nbsp;</td>
                                <td class="col-xs-3" align="center" style="padding: 5px; margin: 5px; text-align: center;">
                                    <%--<asp:Button ID="btnexp" runat="server" CssClass="btn btn-primary" Height="32px" CausesValidation="False"
                                        Font-Size="12px" Style="position: static" Text="Export" OnClick="btnexp_Click"
                                        ValidationGroup="vg" />--%>
                                    <asp:Label ID="lblMsg2" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600"></asp:Label>
                                </td>
                                <td class="col-xs-3" align="center" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="lblMsg" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="#006600" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <%-- <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    
                                </td>
                            </tr>--%>
                            <tr id="GraphPrint" runat="server">
                                <td align="center" style="padding: 5px; margin: 5px">
                                    <div>
                                        <br />
                                        <br />

                                        <script type="text/javascript" src="https://www.google.com/jsapi"></script>

                                        <asp:Literal ID="ltrPie" runat="server"></asp:Literal>
                                        <div id="piechart_3d" style="padding: 10px width: 100%;">
                                        </div>
                                    </div>
                                </td>
                                <td align="center" colspan="2" style="padding: 5px; margin: 5px">
                                    <div>
                                        <br />
                                        <br />

                                        <script type="text/javascript" src="https://www.google.com/jsapi"></script>

                                        <asp:Literal ID="ltrlBar" runat="server"></asp:Literal>
                                        <div id="Bar_chart" style="padding: 10px width: 100%;">
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td style="padding: 5px; margin: 5px" valign="top">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td align="center" style="padding: 5px; margin: 5px" colspan="3">
                                    <table cellpadding="4" cellspacing="5" style="width: 60%; visibility: hidden;">
                                        <tr>
                                            <td class="col-xs-3" style="padding: 5px; margin: 5px">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        Boma</div>
                                                    <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox" Width="180px">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td class="col-xs-3" style="padding: 5px; margin: 5px">
                                                <div class="input-group">
                                                    <div class="input-group-addon">
                                                        Year</div>
                                                    <asp:DropDownList ID="ddlYear" runat="server" class="form-control txtbox" Height="33px"
                                                        Width="180px">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" align="right">
                                                <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" TabIndex="10"
                                                    Text="Submit" OnClick="BtnSave_Click" />
                                            &nbsp;<asp:Button ID="Button1" runat="server" CssClass="btn btn-default" PostBackUrl="~/UI/Public Works/ReportsDashboard.aspx"
                                        Text="Back" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="col-xs-3" style="padding: 5px; margin: 5px">
                                                &nbsp;</td>
                                            <td class="col-xs-3" style="padding: 5px; margin: 5px">
                                    <asp:Label ID="LBLSelect" runat="server" Font-Bold="True" Font-Names="verdana" Font-Size="13px"
                                        ForeColor="Red"></asp:Label>
                                            </td>
                                            <td style="padding: 5px; margin: 5px" align="right">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="center" style="padding: 5px; margin: 5px">
                                    <div>
                                        <br />
                                        <br />

                                        <script type="text/javascript" src="https://www.google.com/jsapi"></script>

                                        <asp:Literal ID="ltrWage" runat="server"></asp:Literal>
                                        <div id="Wage_chart" style="padding: 10px width: 100%;">
                                        </div>
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GBVDetailManagement.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.GBVDetailManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">


    <title></title>
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=Edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script type="text/javascript" src="./assets/js/jquery.min.js"></script>
    <script src="./assets/js/fullcalendar.js"></script>
    <script type="text/javascript" src="./assets/js/popper.min.js"></script>
    <script type="text/javascript" src="./assets/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="./assets/js/jquery.mCustomScrollbar.concat.min.js"></script>
    <script type="text/javascript" src="./assets/js/jquery.dcjqaccordion.2.7.js"></script>
    <script type="text/javascript" src="./assets/js/jquery.validate.js"></script>
    <%--<script src="./assets/js/MEAPValidation.js"></script>--%>
    <link href="https://fonts.googleapis.com/css2?family=Noto+Sans" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css2?family=Righteous&display=swap" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet" />
    <link href="./assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/ionicons.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/simple-line-icons.css" rel="stylesheet" type="text/css" />
    <link href="./assets/css/jquery.mCustomScrollbar.css" rel="stylesheet" />
    <link href="./assets/css/style.css" rel="stylesheet" />
    <link href="./assets/css/responsive.css" rel="stylesheet" />
    <link type="text/css" href="./assets/fonts/Meap_Fonts/styleFA.css" rel="stylesheet" />
    <link href="./assets/css/fullcalendar.css" rel='stylesheet' />
    <link href="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/css/datepicker.css" rel="stylesheet" type="text/css" />
    <script src="https://cdnjs.cloudflare.com/ajax/libs/bootstrap-datepicker/1.3.0/js/bootstrap-datepicker.js"></script>
    <script src="https://code.jquery.com/jquery-latest.min.js" type="text/javascript"></script>

    <style type="text/css">
        .overlay {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 1px;
            background-color: Gray;
            filter: alpha(opacity=60);
            opacity: 0.9;
            -moz-opacity: 0.9;
        }

        .changeBrightnessButton {
            border: 1px solid #563d7c;
            border-radius: 5px;
            color: white;
            padding: 5px 10px 5px 25px;
            background: url(~/Resource/Images/brightnessIcon.png) left 3px top 5px no-repeat #563d7c;
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
        .RadGrid_Default .rgRow>td {
            font-family:Poppins;
            background-color: #fff;
            border-color: #fff;
            height: 35px;
        }

        div.RadGrid_Default .rgHeader, div.RadGrid_Default th.rgResizeCol .HeaderTitle {
            background-image: none;
            font-size: 12px !important;
            font-family: Poppins;
        }

            div.RadGrid_Default .rgHeader a {
                color: #333 !important;
                font-size: 12px !important;
                font-family: Poppins;
            }

            div.RadGrid_Default .rgHeader {
                color: #333 !important;
                font-size: 12px !important;
                font-family: Poppins;
            }
            a {
             color: #337ab7;
             text-decoration: none;
             padding: 0px;
            }
            .LinkButton {
             color: #333;
             border-color: #b3b3b3;
             background-image: linear-gradient(white,#e6e6e6);
             padding: 2px 9px;
             border-width: 1px;
             border-style: solid;
             border-color: #ccc;
             background-position: 0 0;
             border-radius: 4px;
            }
            .LinkButton:hover {
             border-color: #b3b3b3;
             background-image: linear-gradient(gainsboro,#c2c2c2);
            }
            /* Dashed red border */
            hr.blue {
              border-top: 2px dashed #337ab7;
            }

    </style>

    <script src="../../Resource/Styles/SideMenu/script.js" type="text/javascript"></script>

    <link href="../../Resource/Styles/SideMenu/styles.css" rel="stylesheet" type="text/css" />

    <script src="../Resource/Scripts/js/validation.js" type="text/javascript"></script>

    <script src="../../Resource/Scripts/js/jquery.js" type="text/javascript"></script>

    <script src="../../Resource/Scripts/js/bootstrap.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="../../Resource/Scripts/js/plugins/morris/raphael.min.js"></script>

    <script src="../../Resource/Scripts/js/plugins/morris/morris.min.js" type="text/javascript"></script>

    <script src="../../Resource/Scripts/js/plugins/morris/morris-data.js" type="text/javascript"></script>

    <link href="../../Resource/Styles/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <link href="../../Resource/Styles/css/sb-admin.css" rel="stylesheet" type="text/css" />
    <link href="../../Resource/Styles/css/plugins/morris.css" rel="stylesheet" type="text/css" />
    <link href="../../Resource/Styles/font-awesome/css/font-awesome.css" rel="stylesheet"
        type="text/css" />

    <style type="text/css">
       
        a {
         color: #337ab7;
         text-decoration: none;
         padding: 0px;
        }

        .style1 {
            width: 100%;
            max-height: 60px;
        }

        .style3 {
            width: 200px;
            background-color: #56bfe7;
        }

        .style4 {
            max-height: 60px;
         }

      
    </style>

    <style type="text/css">
        .classImage
        {
            background: url();
            width: 36px;
            height: 26px;
        }

    .classImage2{
             color: #333;
             border-color: #b3b3b3;
             background-image: linear-gradient(white,#e6e6e6);
             /*padding: 2px 9px;*/
             border-width: 1px;
             border-style: solid;
             border-color: #ccc;
             background-position: 0 0;
             border-radius: 4px;
             padding-top: 3px!important;
             padding-bottom: 3px!important;
             width: 36px;
             height: 26px;
    }
    </style>


    <script type="text/javascript">

 

     function GetRadWindow() {

            var oWindow = null;

            if (window.radWindow)

                oWindow = window.radWindow;

            else if (window.frameElement && window.frameElement.radWindow)

                oWindow = window.frameElement.radWindow;

            return oWindow;

        }

        function CloseModal() {

            // GetRadWindow().close();

            setTimeout(function () {

                GetRadWindow().close();

            }, 0);

        }

 

        function OnClientClicking(sender, args) {

            var callBackFunction = Function.createDelegate(sender, function (argument) {

                if (argument) {

                    this.click();

                }

            });

            var text = "Are you sure you want to delete the record?";

            radconfirm(text, callBackFunction, 300, 100, null, "Delete");

            args.set_cancel(true);

        }

 

    </script>

    
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="padding-top: 10px; padding-bottom: 20px; padding-left: 9px; padding-right: 9px; height: 575px;"
                        valign="top" align="center">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title" id="h3" runat="server">GBV Detail
                                </h3>
                            </div>
                            <asp:UpdatePanel runat="server" ID="upPanelMain">
                                <ContentTemplate>
                                    <div class="panel-body">
                                        <table id="tbMain" width="100%" cellpadding="4" cellspacing="5">
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">Follow up Action/Feedback</asp:Label>
                                                     <span style="text-align: left; color: red;">*</span>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                      <asp:TextBox class="form-control txtbox" Height="73px" Width="100%" TextMode="MultiLine" Enabled="true" runat="server" ID="txtFollowUpAction" Text=""></asp:TextBox>
                                                    
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px"> </td>
                                                  
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                <td style="padding: 5px; margin: 5px">
                                                   
                                                   </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">Reported to Management/HR</asp:Label>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                   <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlRepotedTo" AutoPostBack="True" OnSelectedIndexChanged="ddlRepotedTo_SelectedIndexChanged" >
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp; </td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label3" runat="server" CssClass="LBLBLACK" Width="128px">Reported Details</asp:Label>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="73px" Width="400px" TextMode="MultiLine" Enabled="False" runat="server" ID="txtReportedDetails" Text=""></asp:TextBox>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                           <tr>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="128px">Is survivor satisfied? </asp:Label>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                   <asp:DropDownList class="form-control txtbox" Height="33px" Width="180px" runat="server" ID="ddlIsSurvivorSatisfied" AutoPostBack="True" OnSelectedIndexChanged="ddlIsSurvivorSatisfied_SelectedIndexChanged" >
                                                <asp:ListItem Text="--Select--" Value="0"></asp:ListItem>
                                                <asp:ListItem Text="YES" Value="YES"></asp:ListItem>
                                                <asp:ListItem Text="NO" Value="NO"></asp:ListItem>
                                            </asp:DropDownList>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp; </td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:Label ID="Label4" runat="server" CssClass="LBLBLACK" Width="128px">Details</asp:Label>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">:</td>
                                                <td style="padding: 5px; margin: 5px">
                                                    <asp:TextBox class="form-control txtbox" Height="73px" Width="400px" TextMode="MultiLine" Enabled="False" runat="server" ID="txtSurvivorDetails" Text=""></asp:TextBox>
                                                </td>
                                                <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                            </tr>
                                       
                                     <tr>
                                        <td style="padding: 5px; margin: 5px">Closing date after management action</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">
                                            <asp:TextBox class="form-control txtbox" Height="33px" Width="180px" runat="server" placeholder="mm/DD/yyyy" ID="txtDateOfClosed" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtDateOfClosed_CalendarExtender" runat="server"
                                                Format="dd/MM/yyyy" PopupButtonID="txtStartDate" OnPreRender="txtDateOfClosed_CalendarExtender_PreRender" TargetControlID="txtDateOfClosed"></cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="filterDateOfIncident" runat="server" Enabled="True" TargetControlID="txtDateOfClosed" FilterType="Numbers, Custom" ValidChars="/" FilterMode="ValidChars"></cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">:</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                        <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                    </tr>

                                           
                                        
                                        </table>
                                        <hr class="blue" />
                                        <table id="tbGridAttachment" width="95%" cellpadding="1" cellspacing="2">
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td align="center" style="padding: 5px; margin: 5px">
                                                    <div id="DivSuccess" runat="server" class="alert alert-success" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#"></a> <strong>Success!</strong><asp:Label ID="lblMessageSuccess" runat="server"></asp:Label>
                                                    </div>
                                                    <div id="DivFailure" runat="server" class="alert alert-danger" visible="false">
                                                        <a aria-label="close" class="close" data-dismiss="alert" href="#"></a> <strong>Warning!</strong>
                                                        <asp:Label ID="lblMessageError" runat="server"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="padding: 20px" align="center">
                                            <asp:Button ID="BtnSave" runat="server" CssClass="btn btn-success" Height="32px" TabIndex="10" Text="Save" Width="120px" OnClick="BtnSave_Click"  />
                                            &nbsp; &nbsp; 
                                        <asp:Button ID="BtnClose" runat="server" CssClass="btn btn-primary" Height="32px" TabIndex="10" Text="Close" Width="120px" OnClick="BtnClose_Click" />
                                            &nbsp; &nbsp;                                                 
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </td>
                    
                </tr>
            </table>


    </form>
</body>
</html>

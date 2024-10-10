<%@ Page Title=":: :: SSSNP : : Work Proposals " Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="IPWOrkProposals" CodeBehind="IPWOrkProposals.aspx.cs" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function checkDate(sender, args) {
            var toDate = new Date();
            toDate.setMinutes(0);
            toDate.setSeconds(0);
            toDate.setHours(0);
            toDate.setMilliseconds(0);
            if (sender._selectedDate < toDate) {
                alert("You can't select day earlier than today!");
                sender._selectedDate = toDate;
                //set the date back to the current date
                sender._textbox.set_Value(sender._selectedDate.format(sender._format))
            }
        }
    </script>

    <script src="../../Resource/Scripts/js/validations.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">

        function OpenPopup() {
            window.open("Lookups/WorkProposalLookup.aspx", "List", "scrollbars=yes,resizable=yes,width=1000,height=650;display = block;position=absolute");
            return false;
        }

        function ApplyChanges(val, val1) {

            document.getElementById("<%=hdfID.ClientID%>").value = val;
            document.getElementById("<%=hdfFlagID.ClientID%>").value = val1;

            document.forms[0].submit();
        }
    </script>

    <style type="text/css">
        .res {
            overflow-x: scroll;
        }
    </style>


    <asp:UpdatePanel ID="upd1" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave2" />
            <asp:PostBackTrigger ControlID="gvpractical0" />
            <asp:PostBackTrigger ControlID="BtnSave1" />
            <asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
        </Triggers>
        <ContentTemplate>
            <div align="left">
                <ol class="breadcrumb">
                    You are here
        &nbsp;!&nbsp; &nbsp; &nbsp; 
                            <li>
                                <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                            </li>
                    <li class="">
                        <i class="fa fa-fw fa-edit"></i>
                        <asp:Label ID="lblSitemap" runat="server" Text=""></asp:Label>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">
                            <asp:Label ID="lblSite2" runat="server" Text=""></asp:Label>
                        </a>
                    </li>
                </ol>
            </div>

            <div align="center">
                <div class="row" align="center">
                    <div class="col-lg-12">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    <asp:Label ID="lblheading" runat="server" Text="Label"></asp:Label>
                                </h3>
                            </div>
                            <div class="panel-body">
                                <table align="center" cellpadding="10" cellspacing="5" style="width: 100%">
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" colspan="3" align="center">
                                            <asp:Label ID="Label347" runat="server" CssClass="LBLBLACK" Width="165px">Search</asp:Label>
                                            <asp:Button ID="btnOrgLookup0" runat="server" CssClass="btn btn-primary" Height="32px"
                                                OnClick="btnOrgLookup_Click" Text="Look Up" CausesValidation="False"
                                                Font-Size="12px" Style="position: static" ToolTip="Rate Lookup" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top" align="left">&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label298" runat="server" CssClass="LBLBLACK" Width="128px">IP</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlIP" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlIP_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label380" runat="server" CssClass="LBLBLACK" Width="128px">Project Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True"
                                                            class="form-control txtbox" Height="33px"
                                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr id="trworkcode" runat="server" visible="false">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label410" runat="server" CssClass="LBLBLACK" Width="128px">Work Activity Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblworkcode" runat="server" CssClass="LBLBLACK"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label348" runat="server" CssClass="LBLBLACK"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label383" runat="server" CssClass="LBLBLACK" Width="136px">Target Beneficiaries</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblTargetBenefeciaries" runat="server" CssClass="LBLBLACK"
                                                            Width="128px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label387" runat="server" CssClass="LBLBLACK" Width="144px">Project Cost (In USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblCost" runat="server" CssClass="LBLBLACK" Width="128px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label432" runat="server" CssClass="LBLBLACK" Width="144px">Type of Meeting</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlMeeting" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px"
                                                            OnSelectedIndexChanged="ddlMeeting_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                            <asp:ListItem Value="CC">Community Consultation</asp:ListItem>
                                                            <asp:ListItem Value="AM">Approval Meeting</asp:ListItem>
                                                            <asp:ListItem Value="OT">Others</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server"
                                                            ControlToValidate="ddlMeeting" ErrorMessage="Please Select Type of Meeting"
                                                            InitialValue="--Select--" ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label350" runat="server" CssClass="LBLBLACK" Width="144px">Meeting Date</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtStartDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="12" onkeypress="Dates()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px">--Select--</asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MM-yyyy"
                                                            PopupButtonID="txtStartDate" TargetControlID="txtStartDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                            ControlToValidate="txtStartDate" ErrorMessage="Please Enter Meeting Date"
                                                            ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="tdMember" runat="server" visible="false">
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label396" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                Width="233px">Members Involved</asp:Label>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr id="tdTstMem" runat="server" visible="false">
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table border="1" cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:Label ID="Label431" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                            Width="128px">TST Members</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:CheckBoxList ID="chkTst" runat="server" CellPadding="3" CellSpacing="5"
                                                            CssClass="LBLBLACK" Font-Bold="False" RepeatColumns="2"
                                                            RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td valign="top">
                                            <table border="1" cellpadding="4" cellspacing="5">
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:Label ID="Label395" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                            Width="260px">County / Municipality Members</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:CheckBoxList ID="ChkCA" runat="server" CellPadding="3" CellSpacing="5"
                                                            RepeatColumns="2" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr id="payamblock" runat="server" visible="false">
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table border="1" cellpadding="4" cellspacing="5">
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:Label ID="Label392" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                            Width="319px">Payam / Block Development Committee</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:CheckBoxList ID="ChkPDC" runat="server" CellPadding="3" CellSpacing="5"
                                                            RepeatColumns="2" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">
                                            <table border="1" cellpadding="4" cellspacing="5" style="width: 100%">
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:Label ID="Label393" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                            Width="348px">Boma / Quarter Council Development Committee</asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" style="padding: 5px; margin: 5px; width: 218px;">
                                                        <asp:CheckBoxList ID="ChkBDC" runat="server" CellPadding="3" CellSpacing="5"
                                                            RepeatColumns="2" RepeatDirection="Horizontal">
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label397" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                Width="233px">Work Proposed Location</asp:Label>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="144px">State Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 224px;">
                                                        <asp:DropDownList ID="ddlState" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlState_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server"
                                                            ControlToValidate="ddlState" ErrorMessage="Please Select State"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label300" runat="server" CssClass="LBLBLACK" Width="124px">County / Municipality Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px; width: 224px;">
                                                        <asp:DropDownList ID="ddlCounties" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlCounties_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server"
                                                            ControlToValidate="ddlCounties" ErrorMessage="Please Select County"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&emsp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td style="position: fixed;"></td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label353" runat="server" CssClass="LBLBLACK" Width="124px">Payam / Block Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlPayam" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px" AutoPostBack="True"
                                                            OnSelectedIndexChanged="ddlPayam_SelectedIndexChanged">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server"
                                                            ControlToValidate="ddlPayam" ErrorMessage="Please Select Payam"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label400" runat="server" CssClass="LBLBLACK" Width="144px">Boma / Quarter Council Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlBoma" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server"
                                                            ControlToValidate="ddlBoma" ErrorMessage="Please Select Boma"
                                                            InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" style="padding: 5px; margin: 5px" valign="top">
                                            <asp:Label ID="Label379" runat="server" CssClass="LBLBLACK" Font-Bold="True"
                                                Width="233px">Proposed Public work Activities</asp:Label>
                                        </td>
                                        <td style="width: 27px">&nbsp;</td>
                                        <td valign="top">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="padding: 5px; margin: 5px" valign="top">
                                            <table cellpadding="4" cellspacing="5" style="position: static;">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label401" runat="server" CssClass="LBLBLACK" Width="128px">Work Name</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkName" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="Names()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server"
                                                            ControlToValidate="txtWorkName" ErrorMessage="Please Enter Work Name"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label435" runat="server" CssClass="LBLBLACK" Width="128px">Work 
                                                      Activity  Code</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkCode" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="25" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator28" runat="server"
                                                            ControlToValidate="txtWorkCode" ErrorMessage="Please Enter Work Code"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label402" runat="server" CssClass="LBLBLACK" Width="124px">Type of Work</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlAreaofWork" runat="server" class="form-control txtbox" OnSelectedIndexChanged="ddlAreasofWork_SelectedIndexChanged"
                                                            Height="33px" AutoPostBack="True" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server"
                                                            ControlToValidate="ddlAreaofWork" ErrorMessage="Please Type Of Work"
                                                            SetFocusOnError="True" ValidationGroup="child" InitialValue="--Select--"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label403" runat="server" CssClass="LBLBLACK" Width="124px"> Work Activity</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlWorkActivity" runat="server" class="form-control txtbox"
                                                            Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>

                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server"
                                                            ControlToValidate="ddlWorkActivity" ErrorMessage="Please Select Work Activity"
                                                            SetFocusOnError="True" ValidationGroup="child" InitialValue="--Select--"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label377" runat="server" CssClass="LBLBLACK" Width="144px">Estimated 
                                                        Manpower</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtManpower" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server"
                                                            ControlToValidate="txtManpower" ErrorMessage="Please Enter Man Power"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label378" runat="server" CssClass="LBLBLACK" Width="137px">Estimated Man Days </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEstDays" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server"
                                                            ControlToValidate="txtEstDays" ErrorMessage="Please Enter Estimated Man Days"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label404" runat="server" CssClass="LBLBLACK" Width="137px">Type of equipment required </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileEquipmentPhoto" runat="server" class="form-control txtbox"
                                                            Height="32px" Width="180px" />
                                                        <asp:Label ID="lblEquipment" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label433" runat="server" CssClass="LBLBLACK" Width="137px">Estimated cost of hand tools and equipment(In USD)</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEstquantity1" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="CharnumOnlyAll()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server"
                                                            ControlToValidate="txtEstquantity1" ErrorMessage="Please Enter Estimated cost of hand tools and equipment(In USD)"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label376" runat="server" CssClass="LBLBLACK" Width="165px">Work Photo</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileWorkPhotoUpload" runat="server" class="form-control txtbox"
                                                            Height="32px" Width="180px" />
                                                        <asp:Label ID="lblFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                </tr>
                                                <tr style="display: none;">
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Image ID="ImageMaterial" runat="server" Height="153px"
                                                            ImageUrl="~/Resource/Images/not-available.jpg" Width="210px" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEstEquipmntReq" runat="server" class="form-control txtbox"
                                                            Height="40px" MaxLength="40" TabIndex="1" TextMode="MultiLine"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td valign="top">
                                            <table cellpadding="4" cellspacing="5" style="position: static;">
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label434" runat="server" CssClass="LBLBLACK" Width="137px">Materials Photo </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:FileUpload ID="FileUploadMaterialsPhoto" runat="server" class="form-control txtbox"
                                                            Height="32px" Width="180px" />
                                                        <asp:Label ID="lblMaterialFileName" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label405" runat="server" CssClass="LBLBLACK" Width="137px"> Estimated cost of materials required (In USD) </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEstquantity2" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1" onkeypress="CharnumOnlyAll()"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator30" runat="server"
                                                            ControlToValidate="txtEstquantity2"
                                                            ErrorMessage="Please Enter Estimated cost of materials required (In USD)"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label406" runat="server" CssClass="LBLBLACK" Width="137px">Estimated Skill Labour Required </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtSkillLabour" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator35" runat="server"
                                                            ControlToValidate="txtSkillLabour"
                                                            ErrorMessage="Please Enter Estimated Skill Labour Required"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label407" runat="server" CssClass="LBLBLACK" Width="137px">Estimated Work Start Date </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtWorkStartDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="NumberhyphenOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px">--Select--</asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="txtWorkStartDate"
                                                            TargetControlID="txtWorkStartDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server"
                                                            ControlToValidate="txtWorkStartDate"
                                                            ErrorMessage="Please Enter Estimated Work Start Date" SetFocusOnError="True"
                                                            ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label408" runat="server" CssClass="LBLBLACK" Width="137px">Estimated Work End Date </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEndDate" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" TabIndex="1"
                                                            ValidationGroup="group" Width="180px">--Select--</asp:TextBox>
                                                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server"
                                                            Format="dd-MM-yyyy" PopupButtonID="txtEndDate"
                                                            TargetControlID="txtEndDate"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator37" runat="server"
                                                            ControlToValidate="txtEndDate" ErrorMessage="Please Enter Estimated End Date"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label409" runat="server" CssClass="LBLBLACK" Width="137px">Estimated Cost (In USD) </asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEstCost" runat="server" class="form-control txtbox"
                                                            Height="28px" MaxLength="40" onkeypress="NumberOnly()" TabIndex="1"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator38" runat="server"
                                                            ControlToValidate="txtEstCost" ErrorMessage="Please Enter Estimated Cost (In USD)"
                                                            SetFocusOnError="True" ValidationGroup="child"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblLatitude" runat="server" CssClass="LBLBLACK" Width="137px">Start Coordinate</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtLatitude" runat="server" class="form-control txtbox"
                                                            Height="28px" TabIndex="1"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                </tr>

                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="lblLongitude" runat="server" CssClass="LBLBLACK" Width="137px">End Coordinate</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtLongitude" runat="server" class="form-control txtbox"
                                                            Height="28px" TabIndex="1"
                                                            Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="screeningStatusLabel" runat="server" CssClass="LBLBLACK" Width="137px">Paper Screening Status</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:DropDownList ID="ddlScreeningStatus" runat="server" AutoPostBack="True" class="form-control txtbox" Height="33px" Width="180px">
                                                            <asp:ListItem>--Select--</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="ddlScreeningStatus" ErrorMessage="Please Select Paper Screening Status"
                                                            InitialValue="--Select--" ValidationGroup="group"><span style="color: red;">&ensp;*</span></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                                <tr style="display: none;">
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="137px">Type Of Material Required</asp:Label>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">:</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:TextBox ID="txtEstiMaterial" runat="server" class="form-control txtbox"
                                                            Height="35px" MaxLength="40" TabIndex="1" TextMode="MultiLine"
                                                            ValidationGroup="group" Width="180px"></asp:TextBox>
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px"></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                    <td style="padding: 5px; margin: 5px">
                                                        <asp:Button ID="BtnSave2" runat="server" CssClass="btn btn-xs btn-warning"
                                                            Height="28px" TabIndex="10" Text="Add New" ValidationGroup="child"
                                                            Width="72px" OnClick="BtnSave2_Click" />
                                                        &nbsp;<asp:Button ID="BtnClear0" runat="server" CausesValidation="False"
                                                            CssClass="btn btn-xs btn-danger" Height="28px" TabIndex="10" Text="Cancel"
                                                            ToolTip="To Clear  the Screen" Width="73px" OnClick="BtnClear0_Click1" />
                                                    </td>
                                                    <td style="padding: 5px; margin: 5px">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr style="overflow-x: scroll; overflow-y: hidden;">
                                        <td colspan="3" style="padding-top: 20px; max-width: 750px">

                                            <table class="table-responsive">
                                                <asp:GridView ID="gvpractical0" runat="server" AutoGenerateColumns="True"
                                                    BorderColor="#003399" BorderStyle="Solid" BorderWidth="1px"
                                                    CssClass="table table-bordered table-responsive"
                                                    DataKeyNames="intWorkid,intWorkActivityId,intAreaofWork,intWorkActivity,WorkName,EstManPower,EstDays,EstEquipmnts,EstMaterial,EstSkilLabour,EstWorkStartDate,EstWorkEndDate,EstCost,EquipmntQuantity,MaterialQuantity,WorkCode,Latitude,Longitude,WorkPhoto,MaterialsPhoto,EquipmentPhoto"
                                                    Font-Names="Poppins" Font-Size="12px" OnRowDataBound="gvpractical0_RowDataBound"
                                                    ForeColor="#333333" GridLines="None" OnRowDeleting="gvpractical0_RowDeleting"
                                                    Width="100%" OnSelectedIndexChanged="gvpractical0_SelectedIndexChanged">
                                                    <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                    <RowStyle BackColor="#FFFFFF" Width="200px" />
                                                    <Columns>
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblproposedID" runat="server" Text='<%# Eval("intWorkActivityId") %>' Visible="false"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField HeaderText="Select" ShowSelectButton="True" />
                                                        <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                                                        <asp:BoundField DataField="WorkName" HeaderText="Work Name" />
                                                        <asp:BoundField DataField="AreaofWork" HeaderText="Area of Work" />
                                                        <asp:BoundField DataField="WorkActivity" HeaderText="Work Activity" />
                                                        <asp:BoundField DataField="EstManPower" HeaderText="Estimated Manpower" />
                                                        <asp:BoundField DataField="EstDays" HeaderText="Estimated Man Days" />
                                                        <asp:BoundField DataField="EstDays" HeaderText="Estimated Days"
                                                            Visible="False" />
                                                        <asp:BoundField DataField="EquipmentPhoto"
                                                            HeaderText="Type of Equipment required" Visible="false" />
                                                        <asp:TemplateField HeaderText="Equipment Photo" ItemStyle-CssClass="Shorter" SortExpression="description" HeaderStyle-Width="1px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadEquipmentRequire" Text='<%# Eval("EquipmentPhoto") %>' CommandArgument='<%#Eval("EquipmentPhoto")+","+ Eval("WorkCode")%>'
                                                                    runat="server" OnClick="DownloadEquipmentRequire"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EquipmntQuantity"
                                                            HeaderText="Estimated quantity of equipment required" />
                                                        <asp:BoundField DataField="EstMaterial"
                                                            HeaderText="Type of materials required" Visible="false" />
                                                        <asp:BoundField DataField="MaterialsPhoto"
                                                            HeaderText="Estimated quantity of materials required" Visible="false" />
                                                        <asp:TemplateField HeaderText="Materials Photo" ItemStyle-CssClass="Shorter" SortExpression="description">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadMaterialRequire" Text='<%# Eval("MaterialsPhoto") %>' CommandArgument='<%#Eval("MaterialsPhoto")+","+ Eval("WorkCode")%>' runat="server" OnClick="DownloadMaterialRequired"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="EstSkilLabour"
                                                            HeaderText="Estimated Skill Labour Required" />
                                                        <asp:BoundField DataField="EstWorkStartDate"
                                                            HeaderText="Estimated Work Start Date" DataFormatString="{0:MM-dd-yyyy}" />
                                                        <asp:BoundField DataField="EstWorkEndDate"
                                                            HeaderText="Estimated Work End Date" DataFormatString="{0:MM-dd-yyyy}" />
                                                        <asp:BoundField DataField="EstCost" HeaderText="Estimated Cost (In USD)" />
                                                        <asp:BoundField DataField="WorkCode" HeaderText="Work Code" />
                                                        <asp:BoundField DataField="WorkPhoto" HeaderText="Work Photo" Visible="false" />
                                                        <asp:TemplateField HeaderText="Work Photo" ItemStyle-CssClass="Shorter" SortExpression="description">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkDownloadWorkRequire" Text='<%# Eval("WorkPhoto") %>' CommandArgument='<%#Eval("WorkPhoto")+","+ Eval("WorkCode")%>' runat="server" OnClick="DownloadWorkRequire"></asp:LinkButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Latitude" HeaderText="Start Coordinate" />
                                                        <asp:BoundField DataField="Longitude" HeaderText="End Coordinate" />
                                                        <asp:BoundField DataField="MaterialQuantity" HeaderText="Estimated cost of Material required (In USD)" />
                                                    </Columns>
                                                    <PagerStyle BackColor="#013161" ForeColor="White" HorizontalAlign="Center" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                    <EditRowStyle BackColor="#013161" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>
                                                <div style="padding: 20px" align="center">
                                                    <asp:Button ID="BtnSave1" runat="server" CssClass="btn btn-primary"
                                                        Height="32px" OnClick="BtnSave_Click" TabIndex="10" Text="Save"
                                                        ValidationGroup="group" Width="90px" />
                                                    <asp:Button ID="BtnClear" runat="server" CausesValidation="False"
                                                        CssClass="btn btn-danger" Height="32px" OnClick="BtnClear_Click" TabIndex="10"
                                                        Text="ClearAll" ToolTip="To Clear  the Screen" Width="90px" />
                                                    <asp:Button ID="BtnDelete" runat="server" CausesValidation="False" CssClass="btn btn-warning" Height="32px" OnClick="BtnClear0_Click" OnClientClick="return confirm('Do you want to delete the record ? ');" TabIndex="10" Text="Delete" Visible="false" Width="90px" />
                                                </div>
                                            </table>
                                            <div visible="false" id="trTSTApproval" runat="server">
                                                <div style="margin-right: 25px;" align="center" valign="top">
                                                    <table cellpadding="4" cellspacing="5" style="max-width: 100%; text-align: right;">
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label427" runat="server" CssClass="LBLBLACK" Width="128px">Status</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:DropDownList ID="ddlstatus" runat="server"
                                                                    class="form-control txtbox" Height="28px" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlstatus_SelectedIndexChanged">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem>ES-Approved</asp:ListItem>
                                                                    <asp:ListItem>Approved</asp:ListItem>
                                                                    <asp:ListItem>Invoice Request</asp:ListItem>
                                                                    <asp:ListItem>Invoice Appovals</asp:ListItem>
                                                                    <asp:ListItem>InProgress</asp:ListItem>
                                                                    <asp:ListItem>Completed</asp:ListItem>
                                                                    <asp:ListItem>TST-Rejected</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">&nbsp;</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label426" runat="server" CssClass="LBLBLACK" Width="156px">Approved/Rejected 
                                                        Date</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px; width: 192px;">
                                                                <asp:TextBox ID="txtApprRejDate" runat="server" class="form-control txtbox"
                                                                    Height="28px" MaxLength="40" onkeypress="Dates()" TabIndex="1"
                                                                    ValidationGroup="group" Width="180px"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CalendarExtender4" runat="server"
                                                                    Format="dd-MM-yyyy" PopupButtonID="txtApprRejDate"
                                                                    TargetControlID="txtApprRejDate"></cc1:CalendarExtender>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 10px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server"
                                                                    ControlToValidate="txtApprRejDate"
                                                                    ErrorMessage="Please Enter Approved/Rejected Date" ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 15px; margin: 15px">
                                                                <asp:Label ID="Label430" runat="server" CssClass="LBLBLACK" Width="136px">Payam / Block Development Committee Approved Date</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:TextBox ID="txtPDCApprvDate" runat="server" class="form-control txtbox"
                                                                    Height="28px" MaxLength="40" onkeypress="Dates()" TabIndex="1"
                                                                    ValidationGroup="group" Width="180px"></asp:TextBox>

                                                                <cc1:CalendarExtender ID="CalendarExtender5" runat="server"
                                                                    Format="dd-MM-yyyy" PopupButtonID="txtPDCApprvDate"
                                                                    TargetControlID="txtPDCApprvDate"></cc1:CalendarExtender>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 13px;">
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server"
                                                                    ControlToValidate="txtPDCApprvDate"
                                                                    ErrorMessage="Please Enter Payam / Block Development Committee Approved Date"
                                                                    ValidationGroup="group"><span style="color: red;">*</span></asp:RequiredFieldValidator>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:Label ID="Label428" runat="server" CssClass="LBLBLACK" Width="136px">Remarks</asp:Label>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px">:</td>
                                                            <td style="padding: 5px; margin: 5px">
                                                                <asp:TextBox ID="txtRemarks" runat="server" class="form-control txtbox"
                                                                    Height="44px" MaxLength="50" TabIndex="1"
                                                                    TextMode="MultiLine" ValidationGroup="group" Width="180px"></asp:TextBox>
                                                            </td>
                                                            <td style="padding: 5px; margin: 5px; width: 13px;">&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td>Attachment: 
                                                            </td>
                                                            <td>
                                                                <asp:FileUpload ID="fuWorkProposalAttachment" runat="server" class="form-control txtbox"
                                                                    Height="32px" Width="400px" />
                                                                <asp:Label ID="lblWorkProposalAttachment" runat="server" CssClass="LBLBLACK" Width="165px"></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="padding: 5px; margin: 5px" valign="top" colspan="2">
                                                                <asp:GridView ID="gvWorkProposalAttachments" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                                    CssClass="GRD" ForeColor="#333333" Height="62px" PageSize="15" OnRowDataBound="gvWorkProposalAttachments_RowDataBound"
                                                                    DataKeyNames="intImageId"
                                                                    ShowFooter="false" Width="100%">
                                                                    <FooterStyle BackColor="#013161" Font-Bold="True" ForeColor="White" />
                                                                    <RowStyle BackColor="#EBF2FE" CssClass="GRDITEM" HorizontalAlign="Left" VerticalAlign="Middle" />
                                                                    <Columns>
                                                                        <%--<asp:CommandField ShowDeleteButton="True" HeaderText="Delete"></asp:CommandField>--%>
                                                                        <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderText="S.No">
                                                                            <ItemTemplate>
                                                                                <%# Container.DataItemIndex +1 %>
                                                                            </ItemTemplate>
                                                                            <HeaderStyle HorizontalAlign="Left" />
                                                                            <ItemStyle Width="30px" />
                                                                        </asp:TemplateField>
                                                                        <asp:TemplateField HeaderText="File Name">
                                                                            <ItemTemplate>
                                                                                <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("PhotoFileName") %>'></asp:HyperLink>
                                                                            </ItemTemplate>
                                                                        </asp:TemplateField>
                                                                        <%--<asp:BoundField DataField="Description" HeaderText="Description" />--%>
                                                                        <asp:BoundField DataField="Created_byName" HeaderText="Uploaded by" />
                                                                        <asp:BoundField DataField="file_path" HeaderText="File Path" Visible="false" />
                                                                        <asp:BoundField DataField="UserRole" HeaderText="Role" />
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
                                                    <div style="padding: 20px; margin: 20px">
                                                    </div>
                                                </div>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div id="success" runat="server" class="alert alert-success" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert"
                                                    href="AddQualification.aspx">×</a> <strong>Success!</strong><asp:Label
                                                        ID="lblmsg" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" style="padding: 5px; margin: 5px">
                                            <div id="Failure" runat="server" class="alert alert-danger" visible="false">
                                                <a aria-label="close" class="close" data-dismiss="alert" href="#"><span style="color: red;">×</span></a> <strong>Warning!</strong>
                                                <asp:Label ID="lblmsg0" runat="server"></asp:Label>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <asp:HiddenField ID="hdfID" runat="server" />
                                <asp:HiddenField ID="hdfTSTID" runat="server" />
                                <asp:HiddenField ID="hdfTSTName" runat="server" />
                                <asp:HiddenField ID="hdfWorkCode" runat="server" />
                                <asp:HiddenField ID="hdfTSTEmail" runat="server" />
                                <asp:HiddenField ID="hdfIPEmail" runat="server" />
                                <asp:HiddenField ID="hdfFlagID" runat="server" />
                                <asp:HiddenField ID="hdfRowIndex" runat="server" />
                                <asp:HiddenField ID="hdfTargetID" runat="server" />
                                <asp:HiddenField ID="hdn_Work_Photo" runat="server" />
                                <asp:HiddenField ID="hdn_Equipment_Photo" runat="server" />
                                <asp:HiddenField ID="hdn_Material_Photo" runat="server" />
                                <asp:HiddenField ID="hdn_return_value" runat="server" />
                                <asp:HiddenField ID="hdn_work_code" runat="server" />
                                <asp:HiddenField ID="hdn_work_id" runat="server" />
                                <asp:HiddenField ID="hdn_work_activity_id" runat="server" />
                                <asp:HiddenField ID="hdn_work_photo_file" runat="server" />
                                <asp:HiddenField ID="hdn_material_photo_file" runat="server" />
                                <asp:HiddenField ID="hdn_equipment_photo_file" runat="server" />
                                <asp:HiddenField ID="hdn_work_photo_download" runat="server" />
                                <asp:HiddenField ID="hdn_equipment_photo_download" runat="server" />
                                <asp:HiddenField ID="hdn_material_photo_download" runat="server" />
                                <asp:ValidationSummary ID="ValidationSummary1" runat="server"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="group" />
                                <asp:ValidationSummary ID="ValidationSummary2" runat="server"
                                    ShowMessageBox="True" ShowSummary="False" ValidationGroup="child" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:UpdateProgress ID="UpdateProgress" runat="server" AssociatedUpdatePanelID="upd1">
                <ProgressTemplate>
                    <div class="overlay">
                        <div style="z-index: 1000; margin-left: -210px; margin-top: 100px; opacity: 1; -moz-opacity: 1;">
                            <img alt="" src="../../Resource/Images/Processing.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
            <asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave2"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="gvpractical0"></asp:PostBackTrigger>
<asp:PostBackTrigger ControlID="BtnSave1"></asp:PostBackTrigger>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>

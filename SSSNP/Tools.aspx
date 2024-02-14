<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/UI/Public Works/CCMaster.master" CodeBehind="Tools.aspx.cs" Inherits="SSSNP.Tools" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<!DOCTYPE html>--%>

<%--<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>

    <script type="text/javascript">
    function modifyCommand(imageEditor, args) {
        if (args.get_commandName() == "Crop") {
            waitForCommand(imageEditor, args.get_commandName(), function (widget) {
                var width = 140;
                var height = 190;
                var ratio = width / height;

                //stop the aspect ratio constraint
                //widget._constraintBtn.set_checked(false); 
                //widget._setCropBoxRatio(null);
                //widget._sizeRatio = null;

                widget._setCropBoxRatio(ratio);
                widget._sizeRatio = ratio;
                widget.set_width(width);
                widget.set_height(height);
                widget._constraintBtn.set_enabled(false);
                widget._updateCropBoxFromControls();
            });
        }
    }

    function waitForCommand(imageEditor, commandName, callback) {
        var timer = setInterval(function () {
            var widget = imageEditor.get_currentToolWidget();
            if (widget && widget.get_name() == commandName) {
                clearInterval(timer);
                callback(widget);
            }
        }, 100);
        }

        function openRadWindow() {
            var value = document.getElementById("TextBox1").value;
            var oWnd = radopen("PhotoEditorTool.aspx?" + value, "RadWindow1"); //Pass parameter using URL   
            oWnd.center();
            return false;
        }

</script>

</head> <body> --%>
<asp:Content ID="xxxx"  ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <script  type="text/javascript">

        function openRadWindow() {
            // var value = document.getElementById("TextBox1").value;
            var oWnd = radopen("PhotoEditorTool.aspx", "RadWindow1"); //Pass parameter using URL   
            oWnd.center();
            return false;
        }

    </script>
   <%--  <form id="form1" runat="server">--%>
        <div>
            <%--<asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>--%>
            Server Name: <asp:Label runat="server" ID="lblServerName" Text=""></asp:Label>
            <br />
              <br />
            &nbsp;<asp:Button ID="btThrowException" runat="server" OnClick="btThrowException_Click" Text="Generate Exception" />
&nbsp; <br />
            <br />
              Release Number: <asp:Label runat="server" ID="lblReleaseNumber" Text=""></asp:Label>
            <br />
            <br />
            <asp:Button ID="btDownloadLog" runat="server" OnClick="btDownloadLog_Click" Text="Download Error Log" />
            <br />
            <br />
            <asp:Button ID="btnCreateFileLogTest" runat="server" OnClick="btnCreateFileLogTest_Click" Text="Create File Log Test" />
        </div>
        

        <div>
            
        </div>
        <div>
                <telerik:radwindowmanager id="RadWindowManager1" ClientIDMode="Static" Width="890" Height="520" Modal="true" runat="server" DestroyOnClose="true">      
                 
                    </telerik:radwindowmanager>    
                
                    <asp:Button runat="server" ID="btnPhotoEditor" Text ="PhotoEditor"  OnClientClick="return openRadWindow();"/>
                <asp:TextBox ID="TextBox1" ClientIDMode="Static" runat="server" Text="filename=TestO.png?type=1"></asp:TextBox>

                <br />
                <br />

               <asp:FileUpload ID="FUFile" runat="server" class="form-control txtbox" Width="340px"  Height="28px"/>
                   
            
                <asp:Button ID="btnUpload" runat="server" CssClass="btn-success" Height="32px" Text="Upload" OnClick="btnUpload_Click" />
                    &nbsp;
                <br />
        
                         <asp:Label ID="txtErrorMessage" runat="server" Text="Error Message" ForeColor="Red" Visible="false"></asp:Label>
                    
        </div>
                        
  <%--  </form>--%>

</asp:Content>
   

<%--</html>--%>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadFileWageRequest.aspx.cs" Inherits="SSSNP_SNSOP.UI.Public_Works.UploadFileWageRequest" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    	<link href="https://fonts.googleapis.com/css2?family=Noto+Sans" rel="stylesheet" />
			<link href="https://fonts.googleapis.com/css2?family=Righteous&display=swap" rel="stylesheet"/>
  			<link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet"  />
			<link href="./assets/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
			<link href="./assets/css/style.css" rel="stylesheet" />			
     
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
     </script>
</head>
<body>
    <form id="form1" runat="server">
        <div style="float:none; display: block; width: 100%">

            <table align="left" cellpadding="10" cellspacing="5" style="width: 400px">
                <tr>
                         <td style="padding: 5px; margin: 5px">
                              <asp:Label ID="Label1" runat="server" CssClass="LBLBLACK" Width="128px">Description</asp:Label>
                         </td>
                    </tr>
                <tr>

                         <td style="padding: 5px; margin: 5px; ">
                            <asp:TextBox Id="txtDescription" runat="server" Width="340px" Height="32px" Text=""></asp:TextBox>

                        </td>

                </tr>

                <tr>
                         <td style="padding: 5px; margin: 5px">
                              <asp:Label ID="Label2" runat="server" CssClass="LBLBLACK" Width="128px">Selected File</asp:Label>
                         </td>
                    </tr>
                <tr>

                        <td style="padding: 5px; margin: 5px; ">
                            <asp:FileUpload ID="FUFile" runat="server" class="form-control txtbox" Width="340px"  Height="28px"/>
                            <asp:Label ID="lblFileName" runat="server" Text="" CssClass="LBLBLACK" Width="340px"></asp:Label>
                        </td>
                         <td style="padding: 5px; margin: 5px; ">
                            <asp:Image runat="server" ID="imgSuccess" Visible="false" ImageUrl="~/Resource/Images/Success.png" Width="24px" />
                        </td>

                </tr>
                <tr>
                    <td  style="padding: 5px; margin: 5px; ">
                         <asp:Button ID="btnUpload" runat="server" CssClass="btn-success" Height="32px" Text="Upload" OnClick="btnUpload_Click" />
                    &nbsp;
                        <asp:Button ID="btnClose" runat="server" CssClass="btn-success" Height="32px" Text="Close" OnClick="btnClose_Click" />
                    </td>
                                         
                </tr>

                <tr>
                    <td>
                         <asp:Label ID="txtErrorMessage" runat="server" Text="errore" ForeColor="Red" Visible="false"></asp:Label>
                    </td>
                </tr>

                </table>
                                 
        </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RedirectAzure.aspx.cs" Inherits="SSSNP.RedirectAzure" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>: SSSNP ::</title>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center" style="text-align: center">
    <asp:Label ID="Label2" runat="server" Text="Some Technical with azure login Error happend.Please contact to us.Mail to : ---" Font-Bold="True" Font-Size="18" ForeColor="LightBlue" Font-Names="Verdana"></asp:Label>
    <br />
    <br />
     <br />
        <asp:Label ID="Label1" runat="server" Text="Sorry for the Inconvenience.Something wrong here.Please relogin in MIS" Font-Bold="True" Font-Size="18" ForeColor="LightBlue" Font-Names="Verdana"></asp:Label>
   <br /> <br /> <a href="frmuserlogin.aspx" style="font-family: Verdana; font-size: 18px; color: #FF9900">Click here to Re-Login</a>
    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" Inherits="UI_Public_Works_APISuccess" Title=":: :: SSSNP : ::" Codebehind="APISuccess.aspx.cs" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div style="padding-bottom:20px;">
        <asp:LinkButton ID="LinkButton1" class="fa fa-edit" runat="server" 
            onclick="LinkButton1_Click">API Dashboard</asp:LinkButton>
    </div>
    <div id="success" runat="server" class="alert alert-success" visible="false">
        <a aria-label="close" class="close" data-dismiss="alert" href="AddQualification.aspx">
            ×</a> <strong>Success!</strong><asp:Label ID="lblmsg" runat="server"></asp:Label>
    </div>
</asp:Content>

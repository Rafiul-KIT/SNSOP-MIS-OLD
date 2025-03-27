<%@ Page Title=":: :: SNSOP : : Invoice Admin Approval" Language="C#" MasterPageFile="~/UI/Public Works/CCMaster.master" AutoEventWireup="true" CodeBehind="InvoiceAdminApproval.aspx.cs" Inherits="SSSNP.UI.Public_Works.InvoiceAdminApproval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div align="left" style="vertical-align: top">
                <ol class="breadcrumb">
                    <li>You are here&nbsp;!&nbsp; &nbsp; &nbsp;</li>
                    <li>
                        <i class="fa fa-dashboard"></i><a href="Home.aspx">Dashboard</a>
                    </li>
                    <li class="">
                        <i class="fa fa-fw fa-desktop"></i>
                    </li>
                    <li class="active">
                        <i class="fa fa-edit"></i><a href="#">Invoice Admin Approval</a>
                    </li>
                </ol>
            </div>
            <div align="center">
                <div class="row" align="left" style="width: 98%">
                    <div class="col-lg-11" style="padding-left: 0px">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <h3 class="panel-title">Invoice Admin Approval</h3>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="gvInvoiceHistory" runat="server" AutoGenerateColumns="False" CssClass="table table-striped" OnRowCommand="gvInvoiceHistory_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="ID" />
                                        <asp:BoundField DataField="state_id" HeaderText="State" />
                                        <asp:BoundField DataField="county_id" HeaderText="County" />
                                        <asp:BoundField DataField="payment_cycle_id" HeaderText="Payment Cycle" />
                                        <asp:BoundField DataField="document_path" HeaderText="Document" />
                                        <asp:BoundField DataField="created_at" HeaderText="Created At" />
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlStatus" runat="server">
                                                    <asp:ListItem Value="0">Uploaded</asp:ListItem>
                                                    <asp:ListItem Value="1">Approved</asp:ListItem>
                                                    <asp:ListItem Value="2">Rejected</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField Text="Update" CommandName="Update" ButtonType="Button" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

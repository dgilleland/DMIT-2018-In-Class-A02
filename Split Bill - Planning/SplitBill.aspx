<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="SplitBill.aspx.cs" Inherits="Staff_SplitBill" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Split Bill</h1>
        Active Bills:
        <asp:DropDownList ID="ActiveBills" runat="server" AppendDataBoundItems="true" DataSourceID="ActiveBillsDataSource" DataTextField="DisplayText" DataValueField="KeyValue">
            <asp:ListItem Value="0">[select a bill]</asp:ListItem>
        </asp:DropDownList>
        <asp:LinkButton ID="SelectBill" runat="server"
            CssClass="btn btn-primary" OnClick="SelectBill_Click">Select Bill</asp:LinkButton>
        <asp:HiddenField ID="BillToSplit" runat="server" />
        <asp:ObjectDataSource runat="server" ID="ActiveBillsDataSource" OldValuesParameterFormatString="original_{0}" SelectMethod="ListActiveBills" TypeName="eRestaurant.BLL.WaiterController"></asp:ObjectDataSource>
        <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    </div>
    <div class="row">
        <div class="col-md-6">
            <asp:GridView ID="OriginalBillItems" runat="server"
                ItemType="eRestaurant.Entities.DTOs.OrderItem" 
                AutoGenerateColumns="false"
                OnSelectedIndexChanging="BillItems_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="MoveOver" runat="server" CommandName="Select"
                                CssClass="btn btn-default"><span class="glyphicon glyphicon-forward"></span> Move</asp:LinkButton>
                            <asp:Label ID="Quantity" runat="server" Text="<%# Item.Quantity %>" />
                            <asp:Label ID="ItemName" runat="server" Text="<%# Item.ItemName %>" />
                            <asp:Label ID="Price" runat="server" Text="<%# Item.Price %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

        <div class="col-md-6">
            <asp:GridView ID="NewBillItems" runat="server"
                ItemType="eRestaurant.Entities.DTOs.OrderItem"
                AutoGenerateColumns="false"
                OnSelectedIndexChanging="BillItems_SelectedIndexChanging">
                <EmptyDataTemplate>
                    New bill is empty. Move an item from the other bill.
                </EmptyDataTemplate>
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="MoveOver" runat="server" CommandName="Select"
                                CssClass="btn btn-default"><span class="glyphicon glyphicon-backward"></span> Move</asp:LinkButton>
                            <asp:Label ID="Quantity" runat="server" Text="<%# Item.Quantity %>" />
                            <asp:Label ID="ItemName" runat="server" Text="<%# Item.ItemName %>" />
                            <asp:Label ID="Price" runat="server" Text="<%# Item.Price %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

    <div class="row col-md-12">
        <asp:LinkButton ID="SplitBill" runat="server"
            CssClass="btn btn-default" OnClick="SplitBill_Click">Split the Bill</asp:LinkButton>
    </div>
</asp:Content>


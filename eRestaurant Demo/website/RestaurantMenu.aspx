<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="RestaurantMenu.aspx.cs" Inherits="RestaurantMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Our Menu Items</h1>
        <asp:Repeater ID="MenuItemRepeater" runat="server" DataSourceID="MenuItemDataSource"
            ItemType="eRestaurant.Framework.Entities.DTOs.CategoryDTO">
            <ItemTemplate>
                <div>
                    <img src="http://placehold.it/150x100/" />
                    <%# Item.Description %>
                    <asp:Repeater ID="ItemDetailRepeater" runat="server"
                        DataSource="<%# Item.MenuItems %>"
                        ItemType="eRestaurant.Framework.Entities.DTOs.MenuItemDTO">
                        <ItemTemplate>
                            <div>
                                <%# Item.Description %>
                                &mdash;
                                <%# Item.Calories %>
                                &mdash;
                                <%# Item.Price.ToString("C") %>
                                <br />
                                <%# Item.Comment %>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </ItemTemplate>
            <SeparatorTemplate>
                <hr />
            </SeparatorTemplate>
        </asp:Repeater>

        <asp:ObjectDataSource ID="MenuItemDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListMenuItems" TypeName="eRestaurant.Framework.BLL.MenuController">
        </asp:ObjectDataSource>
    </div>
</asp:Content>


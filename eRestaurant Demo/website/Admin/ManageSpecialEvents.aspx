<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="ManageSpecialEvents.aspx.cs" Inherits="Admin_ManageSpecialEvents" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row col-md-12">
        <h1>Manage Special Events
            <span class="glyphicon glyphicon-glass"></span>
        </h1>
    </div>

    <!-- ObjectDataSource control to do the underlying communication with my BLL and my ListView control -->
    <asp:ObjectDataSource ID="SpecialEventsDataSource" runat="server"
        TypeName="eRestaurant.Framework.BLL.RestaurantAdminController"
        SelectMethod="ListAllSpecialEvents"
        DataObjectTypeName="eRestaurant.Framework.Entities.SpecialEvent">
    </asp:ObjectDataSource>

    <asp:GridView ID="SpecialEventsGridView" runat="server"
        DataSourceID="SpecialEventsDataSource"></asp:GridView>
</asp:Content>


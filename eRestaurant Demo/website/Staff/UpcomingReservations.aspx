<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="UpcomingReservations.aspx.cs" Inherits="Staff_UpcomingReservations" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="row">
        <div class="col-md-12">
            <h1>Upcoming Reservations</h1>
            <asp:RadioButtonList ID="ActiveEventRadioButtonList" runat="server" 
                DataSourceID="ActiveEventsDataSource"
                AppendDataBoundItems="true"
                RepeatLayout="Flow" RepeatDirection="Horizontal"
                DataTextField="Description" DataValueField="Code">
                <asp:ListItem Selected="True" Value="All">All Events</asp:ListItem>
                <asp:ListItem Value="None">No Events</asp:ListItem>
            </asp:RadioButtonList>
            <asp:ObjectDataSource ID="ActiveEventsDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListActiveEvents" TypeName="eRestaurant.Framework.BLL.ReservationsController">
            </asp:ObjectDataSource>

            <!-- Show the upcoming reservations here -->
            <asp:Repeater ID="ReservationsRepeater" runat="server"
                 DataSourceID="DailyReservationsDataSource"
                 ItemType="eRestaurant.Framework.Entities.DTOs.DailyReservation">
                <ItemTemplate>
                    Month: <%# Item.Month %>
                    Day: <%# Item.Day %>

                    TODO: Display the Item.Reservations in a GridView.
                </ItemTemplate>
                <SeparatorTemplate>
                    <hr />
                </SeparatorTemplate>
            </asp:Repeater>
            <asp:ObjectDataSource ID="DailyReservationsDataSource" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="ListUpcomingReservations" TypeName="eRestaurant.Framework.BLL.ReservationsController">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ActiveEventRadioButtonList" PropertyName="SelectedValue" Name="eventCode" Type="String"></asp:ControlParameter>
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </div>
</asp:Content>


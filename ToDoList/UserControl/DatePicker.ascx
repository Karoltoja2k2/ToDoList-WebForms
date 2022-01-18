<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="ToDoList.UserControl.DatePicker" %>
<link href="../Style/UserControl/DatePicker.css" rel="stylesheet" />

<asp:TextBox ID="CalendarState" runat="server" Style="display: none;" />
<div class="calendar-values">
    <asp:TextBox ID="CalendarValueTextBox" CssClass="default-input" Enabled="false" runat="server"></asp:TextBox>
    <asp:LinkButton ID="OpenCalendarButton" runat="server" OnClick="OpenCalendar" CssClass="calendar-values-trigger" CausesValidation="false">
        <i class="far fa-calendar-alt"></i>
    </asp:LinkButton>
</div>
<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please insert date in format day.month.year" ControlToValidate="CalendarValueTextBox" ForeColor="Red" OnServerValidate="DateTimeFormatValidator"></asp:CustomValidator>
<div class='<%= IsHidden ? "calendar-container-hidden" : "calendar-container" %>'>
    <div class="calendar-exit-container">
        <asp:LinkButton runat="server" OnClick="CloseCalendar" CausesValidation="false" CssClass="calendar-exit">x</asp:LinkButton>
    </div>
    <asp:Calendar ID="CalendarControl" class="calendar" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px" OnSelectionChanged="CalendarSelectionChangeHandler">
        <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" ForeColor="#333333" Height="10pt" />
        <DayStyle Width="14%" />
        <NextPrevStyle Font-Size="8pt" ForeColor="White" />
        <OtherMonthDayStyle ForeColor="#999999" />
        <SelectedDayStyle BackColor="#CC3333" ForeColor="White" />
        <SelectorStyle BackColor="#CCCCCC" Font-Bold="True" Font-Names="Verdana" Font-Size="8pt" ForeColor="#333333" Width="1%" />
        <TitleStyle BackColor="Black" Font-Bold="True" Font-Size="13pt" ForeColor="White" Height="14pt" />
        <TodayDayStyle BackColor="#CCCC99" />
    </asp:Calendar>
</div>

<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DatePicker.ascx.cs" Inherits="ToDoList.UserControl.DatePicker" %>
<link href="../Style/UserControl/DatePicker.css" rel="stylesheet" />

<asp:TextBox ID="CalendarState" runat="server" style="display:none;"/>
<asp:TextBox ID="CalendarValueTextBox" Enabled="true" runat="server" class="addToDoItemRow-input"></asp:TextBox>
<asp:LinkButton runat="server" OnClick="OpenCalendar"/>
<asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please insert date in format day.month.year" ControlToValidate="CalendarValueTextBox" ForeColor="Red" OnServerValidate="DateTimeFormatValidator"></asp:CustomValidator>
<asp:Panel ID="CalendarContainer" CssClass="calendar-container" runat="server">
    <div class="calendar-exit-container">
        <button type="button" class="calendar-exit" onClick='<%=$"CloseCalendar_{ID}()" %>'>x</button>
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
</asp:Panel>

<script language="javascript">
    function OpenCalendar_<%=ID %>() {
        var calendar = document.getElementById('<%=CalendarContainer.ClientID%>');
        calendar.style.display = 'flex';
    
        var calendarState = document.getElementById('<%=CalendarState.ClientID%>');
        calendarState.value = 'flex';
        console.log(calendarState.value)
    }

    function CloseCalendar_<%=ID %>() {
        var calendar = document.getElementById('<%=CalendarContainer.ClientID%>');
        calendar.style.display = 'none';
        
        var calendarState = document.getElementById('<%=CalendarState.ClientID%>');
        calendarState.value = 'none';
        console.log(calendarState.value)
    }

    window.onload += pageLoad_<%=ID %>
    function pageLoad_<%=ID %>() {
        var calendarState = document.getElementById('<%=CalendarState.ClientID%>');
        var calendar = document.getElementById('<%=CalendarContainer.ClientID%>');
        console.log('<%=CalendarState.ClientID%>')
        console.log('<%=CalendarContainer.ClientID%>')

        calendar.style.display = calendarState.value;
   }
</script>
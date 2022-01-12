function txtBox1_ClientClicked() {
    console.log("runn")
    var calendar = document.getElementById('<%=DueDateCalendar.ClientID%>');
    calendar.style.display = 'table';  
}

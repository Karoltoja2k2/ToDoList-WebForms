<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="addToDoItemForm">
        <div class="addToDoItemRow">
            <asp:Label ID="TitleLabel" Text="Title" runat="server"/>
            <asp:TextBox ID="TitleTextBox" AutoPostBack="true" MaxLength="64" runat="server" class="addToDoItemRow-input"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Title field is required" ControlToValidate="TitleTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>

        <div class="addToDoItemRow">
            <asp:Label ID="DescriptionLabel" Text="Description" runat="server"/>
            <asp:TextBox ID="DescriptionTextBox" AutoPostBack="true" runat="server" MaxLength="4096" class="addToDoItemRow-textarea" mode="multiline" Wrap="true"></asp:TextBox>
        </div>

        <div class="addToDoItemRow">
            <asp:Label ID="StatusLabel" Text="Status" runat="server"/>
            <asp:DropDownList ID="StatusDropDownList" runat="server" Enabled="false">
                <asp:ListItem value="0" Text="New"/>
            </asp:DropDownList>
        </div>

        <div class="addToDoItemRow">
            <asp:TextBox ID="CalendarState" runat="server" style="display:none;"/>
            <asp:Label ID="DueDateLabel" Text="Due date" runat="server"/>
            <asp:TextBox ID="CalendarValueTextBox" Enabled="true" runat="server" class="addToDoItemRow-input"></asp:TextBox>
            <asp:CustomValidator ID="CustomValidator1" runat="server" ErrorMessage="Please insert date in format day.month.year" ControlToValidate="CalendarValueTextBox" ForeColor="Red" OnServerValidate="CustomValidator1_ServerValidate"></asp:CustomValidator>
            <div>
                <div id="CalendarContainer" class="calendar-container" runat="server">
                    <div class="calendar-exit-container">
                        <button type="button" class="calendar-exit" onClick="close_calendar()">x</button>
                    </div>
                    <asp:Calendar ID="DueDateCalendar" class="calendar" runat="server" BackColor="White" BorderColor="Black" DayNameFormat="Shortest" Font-Names="Times New Roman" Font-Size="10pt" ForeColor="Black" Height="220px" NextPrevFormat="FullMonth" TitleFormat="Month" Width="400px" OnSelectionChanged="CalendarSelectionChangeHandler">
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
            </div>
        </div>

        <div class="addToDoItemForm-submit">
            <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitHandler" Text="Submit"/>
        </div>
    </div>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="ToDoList.DataLayer.Repository.ToDoItemRepository">
    </asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" OnRowCommand="GridView1_RowCommand" DataSourceId="ObjectDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" Width="100%">
        <Columns>
            <asp:TemplateField HeaderText="Is done">
                <ItemTemplate>
                    <asp:CheckBox ItemId='<%# Eval("Id") %>' Checked='<%# (bool)Eval("IsDone") %>' OnCheckedChanged="IsDoneCheckBox_CheckedChanged" runat="server" AutoPostBack="true" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="">
                <ItemTemplate>
                    <asp:Button Text="x" CommandArgument='<%# Eval("Id") %>' CommandName="Delete" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

    <div style="height: 1000px; width: 100px; background-color: red;">

    </div>

    <script language="javascript">  
        function txtBox1_ClientClicked() {
            var calendar = document.getElementById('<%=CalendarContainer.ClientID%>');
            calendar.style.display = 'flex';

            var calendarState = document.getElementById('<%=CalendarState.ClientID%>');
            calendarState.value = 'flex';
        }
        function close_calendar() {
            var calendar = document.getElementById('<%=CalendarContainer.ClientID%>');
            calendar.style.display = 'none';

            var calendarState = document.getElementById('<%=CalendarState.ClientID%>');
            calendarState.value = 'none';
        }
    </script>
</asp:Content>
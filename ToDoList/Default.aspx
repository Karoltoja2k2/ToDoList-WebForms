<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="formTrigger-container">
        <asp:TextBox ID="FormState" runat="server" style="display:none;"/>
        <button class="formTrigger" Type="button" onclick="trigger_form()" >+ Add item</button>
    </div>

    <div class="addToDoItemForm-hidden" id="addToDoItemForm">
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

        <div class="addToDoItemForm-buttons">
            <% if(form.Id != 0) { %>
                <asp:Button ID="CancelButton" CssClass="addToDoItemForm-cancel-input" runat="server" OnClick="CancelUpdateHandler" Text="Cancel"/>
                <asp:Button ID="UpdateButton" CssClass="addToDoItemForm-update-input" runat="server" OnClick="UpdateItemHandler" Text="Update"/>
            <% }
            else { %>
                <asp:Button ID="SubmitButton" CssClass="addToDoItemForm-submit-input" runat="server" OnClick="AddItemHandler" Text="Submit"/>
            <% } %>
        </div>
    </div>

    <div class="filter">
        <div class="filter-item">
            <asp:Label runat="server" Text="Is done"></asp:Label>
            <asp:DropDownList ID="IsDoneFilter" runat="server" AutoPostBack="true">
                <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="2">false</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="filter-item">
            <asp:Label runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleFilter" runat="server" AutoPostBack="true"></asp:TextBox>
        </div>
        <div class="filter-item">
            <asp:Label runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="DescriptionFilter" runat="server" AutoPostBack="true"></asp:TextBox>
        </div>
    </div>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="ToDoList.DataLayer.Repository.ToDoItemRepository">
        <SelectParameters>
            <asp:ControlParameter ControlID="IsDoneFilter" DefaultValue="0" Name="isDone" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TitleFilter" DefaultValue="" Name="title" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="DescriptionFilter" Name="description" PropertyName="Text" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"  DataSourceId="ObjectDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="100%" AutoGenerateColumns="False" CaptionAlign="Left" RowStyle-HorizontalAlign="Left">
        <Columns>
            <asp:TemplateField HeaderText="Done" ItemStyle-CssClass="list-column-done">
                <ItemTemplate>
                    <asp:CheckBox ID="IsDoneCheckBoxWithItemId" ItemId='<%# Eval("Id") %>' Checked='<%# (bool)Eval("IsDone") %>' OnCheckedChanged="IsDoneCheckBox_CheckedChanged" runat="server" AutoPostBack="true"/>
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Title" ItemStyle-CssClass="list-column-title">
              <ItemTemplate>
                  <asp:Label id="TitleLabel" CssClass="list-column-title"
                      Text= '<%# Eval("Title") %>'
                      runat="server"
                      ToolTip='<%# Eval("Title") %>' /> 
              </ItemTemplate>
          </asp:TemplateField>

            <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="list-column-description">
              <ItemTemplate>
                  <asp:Label id="DescriptionLabel" CssClass="list-column-description"
                      Text= '<%# Eval("Description") %>'
                      runat="server"
                      ToolTip='<%# Eval("Description") %>' /> 
              </ItemTemplate>
          </asp:TemplateField>

            <asp:BoundField DataField="DueDate" HeaderText="Due date" SortExpression="DueDate" DataFormatString="{0:dd.MM.yyyy}" />

            <asp:CommandField ItemStyle-CssClass="list-column-delete" ControlStyle-CssClass="list-cell-delete" ButtonType="Button" ShowDeleteButton="True" DeleteText="Delete"/>
            <asp:CommandField ItemStyle-CssClass="list-column-edit" ControlStyle-CssClass="list-cell-edit" ButtonType="Button" ShowEditButton="True" EditText="Edit" />

        </Columns>
        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
        <HeaderStyle BackColor="#333333" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
        <RowStyle HorizontalAlign="Center"></RowStyle>
        <SelectedRowStyle BackColor="#CC3333" Font-Bold="True" ForeColor="White" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#242121" />
    </asp:GridView>

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

        window.onload = pageLoad
        function pageLoad() {
            var formState = document.getElementById('<%=FormState.ClientID%>');
            var form = document.getElementById('addToDoItemForm');
            setFormState(form, formState.value)
        }

        function setFormState(form, value) {
            if (value == 1) {
                form.className = "addToDoItemForm"
            } else {
                form.className = "addToDoItemForm-hidden"
            }
        }

        function trigger_form() {
            var formState = document.getElementById('<%=FormState.ClientID%>');
            var newFormState = formState.value == 0 ? 1 : 0;

            formState.value = newFormState;
            var form = document.getElementById('addToDoItemForm');
            setFormState(form, newFormState)
        }
    </script>
</asp:Content>
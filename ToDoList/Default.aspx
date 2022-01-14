<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList.Default" %>

<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Register Src="~/UserControl/ContentWraper.ascx" TagPrefix="uc1" TagName="ContentWraper" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="formTrigger-container" onclick="trigger_form()">
        <asp:TextBox ID="FormState" runat="server" style="display:none;" Text="0"/>
        <label id="formTriggerLabel" class="formTrigger-label"></label>
        <button id="formTrigger" class="formTrigger" Type="button"  >
            <% if(CurrentToDoItemId != 0) { %>
                Update item
            <% }
            else { %>
                Add item
            <% } %>
        </button>
    </div>

    <uc1:ContentWraper
        ID="FormTrigger"
        TargetId="addToDoItemForm"
        ButtonValue='<%: CurrentToDoItemId != 0 ? "Update item" : "Add item" %>'

        runat="server"/>
    
    <%--        ButtonValue='
            <% if(CurrentToDoItemId != 0) { %>
                Update item
            <% }
            else { %>
                Add item
            <% } %>'--%>

    <div class="addToDoItemForm" id="addToDoItemForm">
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
            <asp:Label runat="server" Text="Due date"/>
            <uc1:DatePicker runat="server" ID="DueDatePicker" />
        </div>

        <div class="addToDoItemForm-buttons">
            <% if(CurrentToDoItemId != 0) { %>
                <asp:Button ID="CancelButton" CssClass="addToDoItemForm-cancel-input" runat="server" OnClick="CancelUpdateHandler" Text="Cancel"/>
                <asp:Button ID="UpdateButton" CssClass="addToDoItemForm-update-input" runat="server" OnClick="UpdateItemHandler" Text="Update"/>
            <% }
            else { %>
                <asp:Button ID="SubmitButton" CssClass="addToDoItemForm-submit-input" runat="server" OnClick="AddItemHandler" Text="Submit"/>
            <% } %>
        </div>
    </div>

    <div class="formTrigger-container" onclick="trigger_filter()">
        <asp:TextBox ID="FilterState" runat="server" style="display:none;" Text="0"/>
        <label id="filterTriggerLabel" class="formTrigger-label"></label>
        <button id="filterTrigger" class="formTrigger" Type="button"  >
            Filters
        </button>
    </div>  

    <div class="filter" id="toDoItemFilters">
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
            <div class="filter-item">
            <asp:Label runat="server" Text="Due date from"></asp:Label>
            <uc1:DatePicker runat="server" id="DueDateFromFilter" />
        </div>

    <div class="filter-item">
            <asp:Label runat="server" Text="Due date to"></asp:Label>
            <uc1:DatePicker runat="server" id="DueDateToFilter" />
        </div>
    </div>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="ToDoList.DataLayer.Repository.ToDoItemRepository">
        <SelectParameters>
            <asp:ControlParameter ControlID="IsDoneFilter" DefaultValue="0" Name="isDone" PropertyName="SelectedValue" Type="Int32" />
            <asp:ControlParameter ControlID="TitleFilter" DefaultValue="" Name="title" PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="DescriptionFilter" Name="description" PropertyName="Text" Type="String" DefaultValue="" />
            <asp:ControlParameter ControlID="DueDateFromFilter" Name="dueDateFrom" PropertyName="selected" Type="DateTime" />
            <asp:Parameter Name="dueDateTo" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand"  DataSourceId="ObjectDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="100%" AutoGenerateColumns="False" CaptionAlign="Left" RowStyle-HorizontalAlign="Left">
        <Columns>
            <asp:TemplateField HeaderText="Done" ItemStyle-CssClass="list-column-done">
                <ItemTemplate>
                    <asp:CheckBox ID="IsDoneCheckBoxWithItemId" ItemId='<%# Eval("Id") %>' Checked='<%# (bool)Eval("IsDone") %>' OnCheckedChanged="IsDoneCheckBoxChangedHandler" runat="server" AutoPostBack="true"/>
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

        window.onload = pageLoad
        function pageLoad() {
            var formState = document.getElementById('<%=FormState.ClientID%>');
            var form = document.getElementById('addToDoItemForm');
            var trigger = document.getElementById('formTriggerLabel');
            setFormState(form, formState.value, trigger)

            console.log(formState.value)

            var filterState = document.getElementById('<%=FilterState.ClientID%>');
            var filter = document.getElementById('toDoItemFilters');
            var filterTrigger = document.getElementById('filterTriggerLabel');
            setFormState(filter, filterState.value, filterTrigger)
        }

        function setFormState(form, value, trigger) {
            if (value == 1) {
                form.className = form.className.replace("-hidden", "")
                trigger.innerText = `/\\`
            } else {
                form.className += "-hidden"
                trigger.innerText = '\\/'
            }
        }

        function trigger_form() {
            var formState = document.getElementById('<%=FormState.ClientID%>');
            var newFormState = formState.value == 0 ? 1 : 0;

            formState.value = newFormState;
            var form = document.getElementById('addToDoItemForm');
            var trigger = document.getElementById('formTriggerLabel');
            setFormState(form, newFormState, trigger)
        }

        function trigger_filter() {
            var filterState = document.getElementById('<%=FilterState.ClientID%>');
            var newFormState = filterState.value == 0 ? 1 : 0;

            filterState.value = newFormState;
            var filter = document.getElementById('toDoItemFilters');
            var trigger = document.getElementById('filterTriggerLabel');

            setFormState(filter, filterState.value, trigger)
        }
    </script>
</asp:Content>
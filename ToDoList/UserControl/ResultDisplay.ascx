<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResultDisplay.ascx.cs" Inherits="ToDoList.UserControl.ResultDisplay" %>
<%@ Register Src="~/UserControl/ContentWraper.ascx" TagPrefix="uc1" TagName="ContentWraper" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Import Namespace="ToDoList.Helper" %>

<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc1:ContentWraper
                ID="ContentWraper2"
                ButtonValue="Filters"
                runat="server"
                DefaultIsHidden="true" />

            <div id="toDoItemFilters" class='<%= ContentWraper2.IsHidden ? "filter-hidden" : "filter" %>'>
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
                    <asp:TextBox ID="TitleFilter" runat="server" AutoPostBack="true" CssClass="default-input"></asp:TextBox>
                </div>
                <div class="filter-item">
                    <asp:Label runat="server" Text="Description"></asp:Label>
                    <asp:TextBox ID="DescriptionFilter" runat="server" AutoPostBack="true" CssClass="default-input"></asp:TextBox>
                </div>
                <div class="filter-item-date filter-item">
                    <asp:Label runat="server" Text="Due date from"></asp:Label>
                    <uc1:DatePicker runat="server" ID="DueDateFromFilter" DefaultValue='<%# DateTime.Now.ToMonthStart()%>'/>
                </div>

                <div class="filter-item filter-item-date">
                    <asp:Label runat="server" Text="Due date to"></asp:Label>
                    <uc1:DatePicker runat="server" ID="DueDateToFilter" DefaultValue='<%# DateTime.Now.ToMonthEnd() %>' />
                </div>
            </div>

            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="ToDoList.DataLayer.Repository.ToDoItemRepository">
                <SelectParameters>
                    <asp:ControlParameter ControlID="IsDoneFilter" DefaultValue="0" Name="isDone" PropertyName="SelectedValue" Type="Int32" />
                    <asp:ControlParameter ControlID="TitleFilter" DefaultValue="" Name="title" PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="DescriptionFilter" Name="description" PropertyName="Text" Type="String" DefaultValue="" />
                    <asp:ControlParameter ControlID="DueDateFromFilter" Name="dueDateFrom" PropertyName="selected" Type="DateTime" />
                    <asp:ControlParameter ControlID="DueDateToFilter" Name="dueDateTo" PropertyName="selected" Type="DateTime" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <uc1:ContentWraper
                ID="ContentWraper3"
                ButtonValue="Items"
                runat="server"
                DefaultIsHidden="false" />
            <div class='<%= ContentWraper3.IsHidden ? "list-hidden" : "list" %>'>
                <asp:GridView ID="GridView1" runat="server" ShowHeaderWhenEmpty="True" ItemType="ToDoList.DataLayer.Model.ToDoItem"  OnRowCommand="ResultDisplayRowCommand" DataSourceID="ObjectDataSource1" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="100%" AutoGenerateColumns="False" CaptionAlign="Left" RowStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="Done" ItemStyle-CssClass="list-column-done">
                            <ItemTemplate>
                                <asp:CheckBox ID="IsDoneCheckBoxWithItemId" ItemId='<%# Item.Id %>' Checked='<%# (bool)Eval("IsDone") %>' OnCheckedChanged="IsDoneCheckBoxChangedHandler" runat="server" AutoPostBack="true" />
                                <asp:HiddenField Value='<%# Item.Id %>' runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Title" ItemStyle-CssClass="list-column-title">
                            <ItemTemplate>
                                <asp:Label ID="TitleLabel" CssClass="list-column-title"
                                    Text='<%# Eval("Title") %>'
                                    runat="server"
                                    ToolTip='<%# Eval("Title") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="list-column-description">
                            <ItemTemplate>
                                <asp:Label ID="DescriptionLabel" CssClass="list-column-description"
                                    Text='<%# Eval("Description") %>'
                                    runat="server"
                                    ToolTip='<%# Eval("Description") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:BoundField DataField="DueDate" HeaderText="Due date" SortExpression="DueDate" DataFormatString="{0:dd.MM.yyyy}" />

                        <asp:CommandField ItemStyle-CssClass="list-column-delete" ControlStyle-CssClass="list-cell-delete" ButtonType="Button" ShowDeleteButton="True" DeleteText="Delete" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
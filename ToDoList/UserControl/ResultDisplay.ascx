<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResultDisplay.ascx.cs" Inherits="ToDoList.UserControl.ResultDisplay" %>
<%@ Register Src="~/UserControl/ContentWraper.ascx" TagPrefix="uc1" TagName="ContentWraper" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Import Namespace="ToDoList.Helper" %>
<link href="../Style/UserControl/ResultDisplay.css" rel="stylesheet" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <uc1:ContentWraper
                ID="FiltersWraper"
                ButtonValue="Filters"
                runat="server"
                DefaultIsHidden="true" />

            <div id="toDoItemFilters" class='<%= FiltersWraper.IsHidden ? "filter-hidden" : "filter" %>'>
                <div class="filter-item">
                    <asp:Label runat="server" Text="Is done"></asp:Label>
                    <asp:DropDownList ID="IsDoneFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterFieldChanged">
                        <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
                        <asp:ListItem Value="1">true</asp:ListItem>
                        <asp:ListItem Value="2">false</asp:ListItem>
                    </asp:DropDownList>
                </div>

                <div class="filter-item">
                    <asp:Label runat="server" Text="Title"></asp:Label>
                    <asp:TextBox ID="TitleFilter" runat="server" AutoPostBack="true" OnTextChanged="FilterFieldChanged" CssClass="default-input"></asp:TextBox>
                </div>
                <div class="filter-item">
                    <asp:Label runat="server" Text="Description"></asp:Label>
                    <asp:TextBox ID="DescriptionFilter" runat="server" AutoPostBack="true" OnTextChanged="FilterFieldChanged" CssClass="default-input"></asp:TextBox>
                </div>
                <div class="filter-item-date filter-item">
                    <asp:Label runat="server" Text="Due date from"></asp:Label>
                    <uc1:DatePicker runat="server" ID="DueDateFromFilter"/>
                </div>

                <div class="filter-item filter-item-date">
                    <asp:Label runat="server" Text="Due date to"></asp:Label>
                    <uc1:DatePicker runat="server" ID="DueDateToFilter"/>
                </div>
            </div>

            <uc1:ContentWraper
                ID="ListWraper"
                ButtonValue="Items"
                runat="server"
                DefaultIsHidden="false" />

            <div class='<%= ListWraper.IsHidden ? "list-hidden" : "list" %>'>
                <asp:GridView ID="GridView1" DataSource='<%# DataSource %>' runat="server" ShowHeaderWhenEmpty="True" ItemType="ToDoList.DataLayer.Model.ToDoItem" OnRowCommand="ResultDisplayRowCommand" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="100%" AutoGenerateColumns="False" CaptionAlign="Left" RowStyle-HorizontalAlign="Left">
                    <Columns>
                        <asp:TemplateField HeaderText="Done" ItemStyle-CssClass="list-column-done">
                            <ItemTemplate>
                                <asp:CheckBox ID="IsDoneCheckBoxWithItemId" ItemId='<%# Item.Id %>' Checked='<%# Item.IsDone %>' OnCheckedChanged="IsDoneCheckBoxChangedHandler" runat="server" AutoPostBack="true" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Title" ItemStyle-CssClass="list-column-title">
                            <ItemTemplate>
                                <asp:Label ID="TitleLabel" CssClass="list-column-title"
                                    Text='<%# Item.Title %>'
                                    runat="server"
                                    ToolTip='<%# Item.Title %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Description" ItemStyle-CssClass="list-column-description">
                            <ItemTemplate>
                                <asp:Label ID="DescriptionLabel" CssClass="list-column-description"
                                    Text='<%# Item.Description %>'
                                    runat="server"
                                    ToolTip='<%# Item.Description %>' />
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
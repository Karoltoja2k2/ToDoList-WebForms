<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResultDisplay.ascx.cs" Inherits="ToDoList.UserControl.ResultDisplay" %>
<%@ Register Src="~/UserControl/ContentWraper.ascx" TagPrefix="uc1" TagName="ContentWraper" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Register Src="~/UserControl/ResultDisplayFilter.ascx" TagPrefix="uc1" TagName="ResultDisplayFilter" %>
<%@ Register Src="~/UserControl/Pagination.ascx" TagPrefix="uc1" TagName="Pagination" %>


<%@ Import Namespace="ToDoList.Helper" %>
<link href="../Style/UserControl/ResultDisplay.css" rel="stylesheet" />
<asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <uc1:ResultDisplayFilter runat="server" id="ResultDisplayFilter" />

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

            <uc1:Pagination runat="server" id="Pagination" />
        </ContentTemplate>
    </asp:UpdatePanel>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagination.ascx.cs" Inherits="ToDoList.UserControl.Pagination" %>
<link href="../Style/UserControl/Pagination.css" rel="stylesheet" />
<div class="pagination-row">
    <div class="pagination-container">
        <asp:DropDownList ID="PagesDropDownList" runat="server" AutoPostBack="true" CssClass="container-item default-select" OnSelectedIndexChanged="AmountPerPageChanged">
            <asp:ListItem Value="10">10</asp:ListItem>
            <asp:ListItem Value="20">20</asp:ListItem>
            <asp:ListItem Value="50">50</asp:ListItem>
            <asp:ListItem Value="100">100</asp:ListItem>
        </asp:DropDownList>

        <div class="container-item">
            <asp:Button ID="PreviousPageButton" runat="server" CommandArgument="-1" OnCommand="PageChangeCommand" CausesValidation="false" Text="<" />
            <asp:Label ID="PageLabel" runat="server" CssClass="item-label" />
            <asp:Button ID="NextPageButton" runat="server" CommandArgument="1" OnCommand="PageChangeCommand"  CausesValidation="false" Text=">" />
        </div>
    </div>
</div>
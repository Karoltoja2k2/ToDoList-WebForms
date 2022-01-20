<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Pagination.ascx.cs" Inherits="ToDoList.UserControl.Pagination" %>
<div>
    <asp:DropDownList ID="PagesDropDownList" runat="server" AutoPostBack="true" OnSelectedIndexChanged="AmountPerPageChanged">
        <asp:ListItem Value="10">10</asp:ListItem>
        <asp:ListItem Value="20">20</asp:ListItem>
        <asp:ListItem Value="50">50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
    </asp:DropDownList>

    <div>
        <asp:LinkButton ID="PreviousPageButton" runat="server" CommandArgument="-1" OnCommand="PageChangeCommand" CausesValidation="false" Text="<" />
        <asp:Label ID="CurrentPageLabel" runat="server"/>
        z
        <asp:Label ID="TotalPagesLabel" runat="server"/>
        <asp:LinkButton ID="NextPageButton" runat="server" CommandArgument="1" OnCommand="PageChangeCommand"  CausesValidation="false" Text=">" />
    </div>
</div>
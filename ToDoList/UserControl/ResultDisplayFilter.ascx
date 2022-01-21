<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ResultDisplayFilter.ascx.cs" Inherits="ToDoList.UserControl.ResultDisplayFilter" %>
<%@ Register Src="~/UserControl/ContentWraper.ascx" TagPrefix="uc1" TagName="ContentWraper" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<link href="../Style/UserControl/ResultDisplayFilter.css" rel="stylesheet" />
<uc1:ContentWraper
    ID="FiltersWraper"
    ButtonValue="Filters"
    runat="server"
    DefaultIsHidden="true" />
<div id="toDoItemFilters" class='<%= FiltersWraper.IsHidden ? "filter-hidden" : "filter" %>'>
    <div class="filter-row">
        <div class="row-input">
            <asp:Label runat="server" Text="Title"></asp:Label>
            <asp:TextBox ID="TitleFilter" runat="server" AutoPostBack="true" OnTextChanged="FilterFieldChanged" CssClass="default-input"></asp:TextBox>
        </div>
        <div class="row-input">
            <asp:Label runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="DescriptionFilter" runat="server" AutoPostBack="true" OnTextChanged="FilterFieldChanged" CssClass="default-input"></asp:TextBox>
        </div>
    </div>

    <div class="filter-row">
         <div class="row-select">
            <asp:Label runat="server" Text="Is done"></asp:Label>
            <asp:DropDownList CssClass="default-select" ID="IsDoneFilter" runat="server" AutoPostBack="true" OnSelectedIndexChanged="FilterFieldChanged">
                <asp:ListItem Value="0" Text="" Selected="True"></asp:ListItem>
                <asp:ListItem Value="1">true</asp:ListItem>
                <asp:ListItem Value="2">false</asp:ListItem>
            </asp:DropDownList>
        </div>
        <div class="row-date">
            <asp:Label runat="server" Text="Due date from"></asp:Label>
            <uc1:DatePicker runat="server" ID="DueDateFromFilter"/>
        </div>

        <div class="row-date">
            <asp:Label runat="server" Text="Due date to"></asp:Label>
            <uc1:DatePicker runat="server" ID="DueDateToFilter"/>
        </div>

        <asp:Button runat="server" Text="Reset" CssClass="row-reset" CausesValidation="false" OnClick="ResetFiltersClick"/>
    </div>
</div>
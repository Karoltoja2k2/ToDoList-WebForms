<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="ToDoList.aspx.cs" Inherits="ToDoList.ToDoList" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h1>ToDoList</h1>
            <asp:TextBox ID="TitleTextBox" runat="server"></asp:TextBox>
            <asp:TextBox ID="DescriptionTextBox" runat="server"></asp:TextBox>
            <asp:DropDownList ID="StatusDropDownList" runat="server" Enabled="false">
                <asp:ListItem value="0" Text="New"/>
            </asp:DropDownList>
            <asp:Calendar ID="DueDateCalendar" runat="server"></asp:Calendar>

    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="Get" TypeName="ToDoList.DataLayer.Repository.ToDoItemRepository"></asp:ObjectDataSource>
    <asp:GridView ID="GridView1" runat="server" DataSourceId="ObjectDataSource1"></asp:GridView>
</asp:Content>

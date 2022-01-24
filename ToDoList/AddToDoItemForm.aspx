<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="AddToDoItemForm.aspx.cs" Inherits="ToDoList.AddToDoItemForm" %>
<%@ Register Src="~/UserControl/ToDoItemForm.ascx" TagPrefix="uc1" TagName="ToDoItemForm" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <uc1:ToDoItemForm ID="ToDoItemForm" runat="server"/>
</asp:Content>

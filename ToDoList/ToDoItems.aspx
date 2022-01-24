<%@ Page Title="" Language="C#" MasterPageFile="~/App.Master" AutoEventWireup="true" CodeBehind="ToDoItems.aspx.cs" Inherits="ToDoList.ToDoItems" %>
<%@ MasterType virtualpath="~/App.master" %>

<%@ Register Src="~/UserControl/ResultDisplay.ascx" TagPrefix="uc1" TagName="ResultDisplay" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>
    <uc1:ResultDisplay ID="ResultDisplay" runat="server" />
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="App.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList.Default" %>

<%@ Register Src="~/UserControl/ToDoItemForm.ascx" TagPrefix="uc1" TagName="ToDoItemForm" %>
<%@ Register Src="~/UserControl/ResultDisplay.ascx" TagPrefix="uc1" TagName="ResultDisplay" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>

    <uc1:ToDoItemForm ID="ToDoItemForm" runat="server"/>
    <uc1:ResultDisplay ID="ResultDisplay" runat="server" />
    
</asp:Content>
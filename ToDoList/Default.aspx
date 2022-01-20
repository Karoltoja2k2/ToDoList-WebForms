<%@ Page Title="" Language="C#" MasterPageFile="App.Master" MaintainScrollPositionOnPostback="true" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="ToDoList.Default" %>
<%@ MasterType virtualpath="~/App.master" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="Style/Page/Default.css" rel="stylesheet" />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <asp:Panel ID="FormPanel" CssClass="login-form-container" DefaultButton="LoginButton" runat="server">
            <div class="input-container" runat="server">
                <asp:Label runat="server" Text="Name"/>
                <asp:TextBox CssClass="default-input" ID="UsernameTextBox" runat="server" AutoPostBack="true"></asp:TextBox>
            </div>
            <div class="errors" runat="server">
                <asp:RequiredFieldValidator ID="UserNameValidator" runat="server" ErrorMessage="Name must not be empty" ControlToValidate="UsernameTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
                <asp:CustomValidator ID="UserExistValidator" runat="server" ControlToValidate="UsernameTextBox" ForeColor="Red"></asp:CustomValidator>
            </div>
            <div class="form-buttons" runat="server">
                <asp:Button ID="RegisterButton" runat="server" Text="Register" CssClass="buttons-register" OnClick="RegisterClick"/>
                <asp:Button ID="LoginButton" runat="server" Text="Login" CssClass="buttons-login" OnClick="LoginClick"/>
            </div>
        </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
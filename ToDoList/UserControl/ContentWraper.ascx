<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentWraper.ascx.cs" Inherits="ToDoList.UserControl.ContentWraper" %>
    <link href="../Style/UserControl/ContentWraper.css" rel="stylesheet" />
    <div class="trigger-container">
        <asp:LinkButton ID="TriggerButton" CssClass="trigger" OnClick="TriggerElem" runat="server" CausesValidation="false">
            <label id="formTriggerLabel" class="trigger-direction"><%= IsHidden ? "\\/" : "/\\" %></label>
            <asp:Label ID="ButtonLabel" runat="server" />
        </asp:LinkButton>
    </div>

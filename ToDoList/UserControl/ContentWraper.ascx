<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentWraper.ascx.cs" Inherits="ToDoList.UserControl.ContentWraper" %>
    <link href="../Style/UserControl/ContentWraper.css" rel="stylesheet" />
    <div class="formTrigger-container">
        <asp:LinkButton ID="TriggerButton" CssClass="formTrigger-button" OnClick="TriggerElem" runat="server" CausesValidation="false">
            <label id="formTriggerLabel" class="formTrigger-label"><%= IsHidden ? "\\/" : "/\\" %></label>
            <asp:Label ID="ButtonLabel" runat="server" />
        </asp:LinkButton>
    </div>

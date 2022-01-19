﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ToDoItemForm.ascx.cs" Inherits="ToDoList.UserControl.ToDoItemForm" %>
<%@ Register Src="~/UserControl/DatePicker.ascx" TagPrefix="uc1" TagName="DatePicker" %>
<%@ Register Src="~/UserControl/ContentWraper.ascx" TagPrefix="uc1" TagName="ContentWraper" %>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <uc1:ContentWraper
                ID="ContentWraper1"
                runat="server"
                ButtonValue='<%# FormId == 0 ? "Add item" : "Update item" %>'
                DefaultIsHidden="true" />

            <div id="addToDoItemForm" class='<%= ContentWraper1.IsHidden ? "form-hidden" : "form" %>'>
                <div class="form-row">
                    <asp:Label ID="TitleLabel" Text="Title" runat="server" />
                    <asp:TextBox ID="TitleTextBox" AutoPostBack="true" MaxLength="64" runat="server" class="default-input"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Title field is required" ControlToValidate="TitleTextBox" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

                <div class="form-row">
                    <asp:Label ID="DescriptionLabel" Text="Description" runat="server" />
                    <asp:TextBox ID="DescriptionTextBox" AutoPostBack="true" runat="server" MaxLength="4096" CssClass="default-input form-textarea" mode="multiline" Wrap="true"></asp:TextBox>
                </div>

                <div class="form-row">
                    <asp:Label runat="server" Text="Due date" />
                    <uc1:DatePicker runat="server" ID="DueDatePicker" selected='<%# DateTime.Now %>' />
                </div>

                <div class="form-buttons">
                    <% if (FormId != 0)
                        { %>
                    <asp:Button ID="CancelButton" CssClass="form-buttons-cancel" runat="server" OnClick="CancelUpdateHandler" Text="Cancel" />
                    <asp:Button ID="UpdateButton" CssClass="form-buttons-update" runat="server" OnClick="SubmitHandler" Text="Update" />
                    <% }
                        else
                        { %>
                    <asp:Button ID="SubmitButton" CssClass="form-buttons-submit" runat="server" OnClick="SubmitHandler" Text="Submit" />
                    <% } %>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
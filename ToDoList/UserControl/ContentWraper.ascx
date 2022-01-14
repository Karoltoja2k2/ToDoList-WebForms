<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ContentWraper.ascx.cs" Inherits="ToDoList.UserControl.ContentWraper" %>

    <div class="formTrigger-container" onclick="triggerContainer()">
        <asp:TextBox ID="FormState" runat="server" style="display:none;" Text="0"/>
        <label id="formTriggerLabel" class="formTrigger-label"></label>
        <button id="formTrigger" class="formTrigger" Type="button"  >
            <%=ButtonValue %>
        </button>
    </div>
    <script language="javascript">

        function pageLoad() {
            var formState = document.getElementById('<%=FormState.ClientID%>');
            var form = document.getElementById('<%=TargetId%>');
            var trigger = document.getElementById('formTriggerLabel');
            setFormState(form, formState.value, trigger)
        }

        function triggerContainer() {
            var formState = document.getElementById('<%=FormState.ClientID%>');
            var form = document.getElementById('<%=TargetId%>');
            var trigger = document.getElementById('formTriggerLabel');

            var newFormState = formState.value == 0 ? 1 : 0;
            formState.value = newFormState;
            setFormState(form, newFormState, trigger)
        }

        function setFormState(form, value, trigger) {
            if (value == 1) {
                form.className = form.className.replace("-hidden", "")
                trigger.innerText = `/\\`
            } else {
                form.className += "-hidden"
                trigger.innerText = '\\/'
            }
        }
    </script>
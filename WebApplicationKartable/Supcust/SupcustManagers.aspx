<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupcustManagers.aspx.cs" Inherits="WebApplicationKartable.SupcustManagers" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
        <style type="text/css">
        .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
        .modalBackground
        {
            background-color: black;
        }
        .modalPopup
        {
            background-color:whitesmoke;
            border:3px solid #ccc;
            padding:10px;
            height: 510px;
            width:610px;
            font-size:15px
        }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center;"><h2>مدیریت مشتری</h2></div>
        <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>

   <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>   
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td><asp:ImageButton ID="add_new" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="add_new_Click"/></td>
            <td style="padding-left:5%;"></td>
            <td><asp:ImageButton ID="edit" ToolTip="ویرایش" runat="server" ImageUrl="~/images/Controls/edit.png" OnClick="edit_Click" ValidationGroup="RegisterUserValidationGroup"/>
            </td>
            <td style="padding-left:5%;"></td>
            <td><asp:ImageButton ID="delete" ToolTip="حذف" runat="server" ImageUrl="~/images/Controls/delete.png" OnClick="delete_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>
            <td style="padding-left:5%;"></td>
            <td><asp:ImageButton ID="report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="report_Click"/></td>
        </tr>
    </table>
    <table>
        <tr>
            <td>نام مشتری :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="300"  CssClass="textbox"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txtContactsSearch" ErrorMessage="برای ویرایش یا حذف مشتری باید انتخاب شود" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>تلفن همراه :</td>
            <td><asp:TextBox ID="txt_cell_phone" runat="server" Width="250"  CssClass="textbox" ></asp:TextBox><asp:Button ID="btn_cell_phone_finder" runat="server" OnClick="btn_cell_phone_finder_Click" Text="جستجو" CssClass="btn-default" />
            </td>
        </tr>
    </table>  
    </asp:Panel>    
        <cc1:AutoCompleteExtender ServiceMethod="FilterSearch" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch"
            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
    <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</asp:Content>

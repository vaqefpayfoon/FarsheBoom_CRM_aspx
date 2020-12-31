<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Opportunity_Managment.aspx.cs" Inherits="WebApplicationKartable.Opportunity_Managment" %>
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
    <div style="text-align:center;"><h2>فرصت فروش</h2></div>
        <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center">
    <asp:GridView ID="grid" runat="server" AutoGenerateColumns="False" AllowPaging="true" PageSize="14"  OnPageIndexChanging="grid_PageIndexChanging" 
     OnSelectedIndexChanging="CustomersGridView_SelectedIndexChanging" GridLines="Horizontal" Width="600" Height="500">
        <AlternatingRowStyle BackColor="LemonChiffon" />
        <Columns>
        <asp:BoundField HeaderText="شماره" DataField="srl" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
        </asp:BoundField>
        <asp:BoundField HeaderText="نام نمایشگاه" DataField="project_name" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300" />
        </asp:BoundField>
         <asp:BoundField HeaderText="تاریخ نمایشگاه" DataField="from_date" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300" />
        </asp:BoundField>
        <asp:CommandField ShowSelectButton="True" HeaderText="انتخاب" SelectText="انتخاب"/>
        </Columns>
    </asp:GridView>
    <asp:Button ID="btnClose" runat="server" Text="بستن" Font-Size="15" />
    </asp:Panel>    
<cc1:ModalPopupExtender ID="mp1" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
   <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>   
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td><asp:ImageButton ID="edit" ToolTip="ویرایش" runat="server" ImageUrl="~/images/Controls/edit.png" OnClick="edit_Click" ValidationGroup="RegisterUserValidationGroup"/>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>نام پروژ :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="300"  CssClass="textbox"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txtContactsSearch" ErrorMessage="برای ویرایش یا حذف نمایشگاه ای باید انتخاب شود" 
ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>کد نمایشگاه :</td>
            <td><asp:TextBox ID="txt_code" runat="server" Width="250"  CssClass="textbox" ReadOnly="true"></asp:TextBox>
                    <asp:ImageButton ID="btnShow" runat="server"  ImageUrl="~/images/Controls/Select.gif" />
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

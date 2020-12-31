<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FactorsManagment.aspx.cs" Inherits="WebApplicationKartable.FactorsManagment" %>
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
    <div style="text-align:center;"><h2>مدیریت فاکتورها</h2></div>
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
        <asp:BoundField HeaderText="شماره فاکتور" DataField="factor_no" >
            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="300" />
        </asp:BoundField>
         <asp:BoundField HeaderText="تاریخ فاکتور" DataField="u_date_tome" >
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
            <td><asp:ImageButton ID="add_new" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="add_new_Click"/></td>
            <td style="padding-left:5%;"></td>
            <td><asp:ImageButton ID="edit" ToolTip="ویرایش" runat="server" ImageUrl="~/images/Controls/edit.png" OnClick="edit_Click" ValidationGroup="RegisterUserValidationGroup"/>
            </td>
            <td><asp:ImageButton ID="btn_delete" ToolTip="حذف" runat="server" ImageUrl="~/images/Controls/delete.png" OnClick="btn_delete_Click" ValidationGroup="RegisterUserValidationGroup"/>
            </td>
            <td style="padding-left:5%;"></td>
            <td>
                <asp:Button ID="btn_return" runat="server" CssClass="btn-facebook" OnClick=
                    "btn_return_Click" Text="مرجوع فرش" ValidationGroup="RegisterUserValidationGroup"/>
            </td>
            <td style="padding-left:5%;"></td>
            <td>
                <asp:Button ID="btn_cancelreturn" runat="server" CssClass="btn-facebook" OnClick=
                    "btn_cancelreturn_Click" Text="کنسل مرجوع" ValidationGroup="RegisterUserValidationGroup"/>
            </td>
        </tr>
    </table>
     <table>
        <tr>
            <td>شماره فاکتور :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="300"  CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>نام مشتری :</td>
            <td><asp:TextBox ID="txtContactsSearch2" runat="server" Width="300"  CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>تلفن همراه :</td>
            <td><asp:TextBox ID="txt_cell_phone" runat="server" Width="250"  CssClass="textbox" ></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>تاریخ فاکتور :</td>
            <td><asp:TextBox ID="txt_code" runat="server" Width="250"  CssClass="textbox" ReadOnly="true"></asp:TextBox>
                    <asp:ImageButton ID="btnShow" runat="server"  ImageUrl="~/images/Controls/Select.gif" />
            </td>
        </tr>
        <tr>
            <td>کد فرش بوم:</td>
            <td><asp:TextBox ID="txt_code_igd" runat="server" Width="250"  CssClass="textbox" ></asp:TextBox><asp:Button ID="btn_cell_phone_finder" runat="server" OnClick="btn_cell_phone_finder_Click" Text="جستجو" CssClass="btn-default" />
            </td>
        </tr>
    </table>  
    </asp:Panel>    
        <cc1:AutoCompleteExtender ServiceMethod="FilterSearch" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch"
            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ServiceMethod="FilterSearch2" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch2"
            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
    <cc1:AutoCompleteExtender ServiceMethod="FilterSearch3" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_cell_phone"
            ID="AutoCompleteExtender3" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
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

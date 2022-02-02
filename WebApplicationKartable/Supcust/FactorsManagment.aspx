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
<%--        <asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center">
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
</cc1:ModalPopupExtender --%>
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
                    "btn_return_Click" Text="مرجوع فرش"/>
            </td>
            <td style="padding-left:5%;"></td>
<%--            <td>
                <asp:Button ID="btn_cancelreturn" runat="server" CssClass="btn-facebook" OnClick=
                    "btn_cancelreturn_Click" Text="کنسل مرجوع" ValidationGroup="RegisterUserValidationGroup"/>
            </td>--%>
            <td>
                <asp:Button ID="btn_cancel_report" runat="server" CssClass="btn-facebook" PostBackUrl="~/Supcust/RejectFactor.aspx" Text="گزارش مرجوع"/>
            </td>
            <td>
                <asp:Button ID="Button1" runat="server" CssClass="btn-facebook" PostBackUrl="~/Supcust/Depositis.aspx" Text="لیست بیعانه ها"/>
            </td>
            <td>
                <asp:Button ID="Button2" runat="server" CssClass="btn-facebook" PostBackUrl="~/Supcust/FactorsList.aspx" Text="گزارش فاکتور"/>
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
            <td><asp:TextBox ID="txtContactsSearch2" runat="server" Width="300"  CssClass="textbox"></asp:TextBox>
                <asp:Button ID="btn_supcustname" runat="server" OnClick="btn_supcustname_Click" Text="نمایش لیست" CssClass="btn-default" />
            </td>
        </tr>
        <tr>
            <td>تلفن همراه :</td>
            <td><asp:TextBox ID="txt_cell_phone" runat="server" Width="250"  CssClass="textbox" ></asp:TextBox>
                <asp:Button ID="btn_cell_phone" runat="server" OnClick="btn_cell_phone_Click" Text="نمایش لیست" CssClass="btn-default" />
            </td>
        </tr>
        <tr>
            <td>کد فرش بوم:</td>
            <td><asp:TextBox ID="txt_code_igd" runat="server" Width="250"  CssClass="textbox" ></asp:TextBox><asp:Button ID="btn_cell_phone_finder" runat="server" OnClick="btn_cell_phone_finder_Click" Text="جستجو" CssClass="btn-default" />
            </td>
        </tr>
    </table>
     <br />
    <div class="row">
            <asp:GridView ID="gridview" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D" HeaderStyle-ForeColor="White" Width="950px">
    <Columns>                      
    <asp:HyperLinkField DataTextField="srl_f" DataNavigateUrlFields="srl_f" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}"  HeaderText="فاکتور"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
        <asp:HyperLinkField DataTextField="srl" DataNavigateUrlFields="srl" DataNavigateUrlFormatString="~/Provider_Goods/ProductAssign.aspx?srl={0}"  HeaderText="فرش"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
    <asp:BoundField DataField="u_date_tome" HeaderText="تاریخ" ItemStyle-Width="80" ReadOnly="True"/> 
    <asp:BoundField DataField="code_igd" HeaderText="کد" ItemStyle-Width="80" ReadOnly="True"/>   
    <asp:BoundField DataField="factor_no" HeaderText="ش فاکتور" ItemStyle-Width="80" ReadOnly="True"/>
    <asp:BoundField DataField="full_name" HeaderText="نام مشتری" ItemStyle-Width="300" ReadOnly="True"/>  
    <asp:BoundField DataField="cell_phone" HeaderText="موبایل" ItemStyle-Width="200" ReadOnly="True"/>  
    <asp:BoundField DataField="provider_name" HeaderText="تامین کننده" ItemStyle-Width="200" ReadOnly="True"/>  
    <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="80" ReadOnly="True"/>   
    <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="80" ReadOnly="True"/>
    <asp:BoundField DataField="area" HeaderText="مساحت" ItemStyle-Width="80" ReadOnly="True"/>    
            <asp:BoundField DataField="discount" HeaderText="تخفیف" ItemStyle-Width="60" />
    <asp:BoundField DataField="discount_amount" HeaderText="تخفیف" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="down_payment" HeaderText="پیش پرداخت" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
<asp:BoundField DataField="sale_price" HeaderText="قیمت فروش" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="final_price" HeaderText="مبلغ فاکتور" ItemStyle-Width="90" DataFormatString="{0:C0}"/>

        </Columns>
    <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" /> 
    </asp:GridView>
    </div>
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

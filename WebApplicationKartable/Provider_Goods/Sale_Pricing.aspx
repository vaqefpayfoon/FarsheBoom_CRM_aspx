<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Sale_Pricing.aspx.cs" Inherits="WebApplicationKartable.Sale_Pricing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
        .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
</style>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="text-align:center;"><h2>قیمت گذاری</h2></div>
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
<asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size"/>
<asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand"/>
<cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
                <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/> 
    <table>
        <tr>
            <td>
            <table>
        <tr>
            <td>نام تامین کننده :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="300"  CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>کد فرش :</td>
            <td><asp:TextBox ID="txt_product" runat="server" Width="300"  CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>گونه :</td>
            <td><asp:DropDownList ID="lst_city2" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>اندازه :</td>
            <td><asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
                <td><asp:Button ID="btn_filter" runat="server" CssClass="btn btn-primary" Text="فیلتر" OnClick="btn_filter_Click" /></td>
                <td><asp:Label ID="lbl_profit" runat="server" CssClass="text-blue"></asp:Label></td>
        </tr>
    </table> 
            </td>
            <td>
    <table>
         <tr>
            <td>مبلغ خرید :</td>
            <td><asp:TextBox ID="txt_buy" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_buy" ErrorMessage="مبلغ اجباریست" ToolTip="مبلغ اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
        </tr>
         <tr>
            <td>مبلغ فروش :</td>
            <td><asp:TextBox ID="txt_sell" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_sell" ErrorMessage="مبلغ اجباریست" ToolTip="مبلغ اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
        </tr>
         <tr>
            <td>تخفیف واحد :</td>
            <td><asp:TextBox ID="txt_discount" runat="server" CssClass="textbox"  Width="180px"   MaxLength="2"></asp:TextBox></td>
        </tr>
        <tr>
            <td><asp:Button ID="btn_save" runat="server" CssClass="btn btn-primary" Text="ذخیره" OnClick="ImageButton2_save_Click" ValidationGroup="RegisterUserValidationGroup" /></td>
        </tr> 
    </table>
            </td>
            <td>
                <asp:Image ID="image1" runat="server" Height="300" Width="350" />
            </td>
        </tr>
    </table>        
        <div><hr /></div>        
<cc1:AutoCompleteExtender ServiceMethod="FilterSearch" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txtContactsSearch"
            ID="AutoCompleteExtender1" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
<cc1:AutoCompleteExtender ServiceMethod="FilterSearch2" MinimumPrefixLength="2"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_product"
            ID="AutoCompleteExtender2" runat="server" FirstRowSelected="false" OnClientHiding="OnClientCompleted"
            OnClientPopulated="OnClientCompleted" OnClientPopulating="OnClientPopulating">
        </cc1:AutoCompleteExtender>
    <div style="width:auto">
        <asp:GridView ID="grid" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" OnSelectedIndexChanged="grid_SelectedIndexChanged" Font-Size="11">
        <Columns>
        <asp:HyperLinkField DataTextField="srl" DataNavigateUrlFields="srl" DataNavigateUrlFormatString="ProductAssign.aspx?srl={0}"  HeaderText="کد"  ControlStyle-ForeColor="Black" ItemStyle-Width="60" Target="_blank" HeaderStyle-Font-Size="11px" ItemStyle-Font-Size="11px" />
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="90" /> 
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="90" />
            <asp:BoundField DataField="carpet_title" HeaderText="نوع" ItemStyle-Width="120" />
            <asp:BoundField DataField="area" HeaderText="مساحت" ItemStyle-Width="70" />
            <asp:BoundField DataField="discount" HeaderText="تخفیف" ItemStyle-Width="60" />
            <asp:BoundField DataField="buy_price" HeaderText="خرید" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
            <asp:BoundField DataField="sale_price" HeaderText="فروش" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
            <asp:BoundField DataField="u_buy" HeaderText="خرید متری" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
            <asp:BoundField DataField="u_sale" HeaderText="فروش متری" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
            <asp:BoundField DataField="discount_amount" HeaderText="مبلغ تخفیف" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
            <asp:BoundField DataField="final_sale" HeaderText="مبلغ فروش" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
            <asp:CommandField ShowSelectButton="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        </Columns>
 <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
</div>
 <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_sell" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
 <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_buy" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
 <cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_discount" Mask="99" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
    <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</asp:Content>

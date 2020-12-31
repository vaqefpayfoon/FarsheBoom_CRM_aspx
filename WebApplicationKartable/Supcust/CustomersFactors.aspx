<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CustomersFactors.aspx.cs" Inherits="WebApplicationKartable.CustomersFactors" %>
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
    <div style="text-align:center;"><h2>فاکتورهای مشتری</h2></div>
        <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>

   <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>   
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
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

<br /><hr />
    <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" Font-Size="10">
    <Columns>                      
    <asp:HyperLinkField DataTextField="srl_f" DataNavigateUrlFields="srl_f" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}"  HeaderText="فاکتور"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
    <asp:BoundField DataField="u_date_tome" HeaderText="تاریخ" ItemStyle-Width="80" ReadOnly="True"/> 
    <asp:BoundField DataField="code_igd" HeaderText="کد" ItemStyle-Width="80" ReadOnly="True"/>   
    <asp:BoundField DataField="factor_no" HeaderText="ش فاکتور" ItemStyle-Width="80" ReadOnly="True"/>
    <asp:BoundField DataField="provider_name" HeaderText="تامین کننده" ItemStyle-Width="200" ReadOnly="True"/>  
    <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="80" ReadOnly="True"/>   
    <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="80" ReadOnly="True"/>
    <asp:BoundField DataField="area" HeaderText="مساحت" ItemStyle-Width="80" ReadOnly="True"/>    
    <asp:BoundField DataField="buy_price" HeaderText="خرید" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="sale_price" HeaderText="فروش" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="discount" HeaderText="تخفیف" ItemStyle-Width="60" />
    <asp:BoundField DataField="discount_amount" HeaderText="تخفیف" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="final_sale" HeaderText="نمایشگاه" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="final_discount" HeaderText="تخفیف ن" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="final_price" HeaderText="قیمت ن" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="margin_profit" HeaderText="حاشیه سود" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="final_profit2" HeaderText="سود" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
        </Columns>
<AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" /> 
    </asp:GridView>
    <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</asp:Content>

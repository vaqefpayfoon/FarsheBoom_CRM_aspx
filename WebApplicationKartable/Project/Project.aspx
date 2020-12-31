<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Project.aspx.cs" Inherits="WebApplicationKartable.Project" %>
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
</style>
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
<cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
<asp:SqlDataSource ID="source_size" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, size_title FROM dbo.inv_size"/>
<asp:SqlDataSource ID="source_brand" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, brand_name FROM dbo.inv_brand"/>
<asp:SqlDataSource ID="source_locate" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, location FROM dbo.bas_locate"/>
        <div style="text-align:center"><h2>تعریف نمایشگاه</h2></div>
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>                       
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>کد نمایشگاه :</td>
            <td><asp:TextBox ID="txt_project_code" runat="server" CssClass="textbox"  Width="180px"   MaxLength="20"></asp:TextBox><asp:Label ID="Label1" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_project_code"
                ErrorMessage="کد نمایشگاه اجباریست" ToolTip="کد نمایشگاه اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
            <td>نام نمایشگاه :</td>
            <td><asp:TextBox ID="txt_project_name" runat="server" CssClass="textbox"  Width="180px"   MaxLength="100"></asp:TextBox><asp:Label ID="lblName" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_project_name"
                ErrorMessage="نام نمایشگاه اجباریست" ToolTip="نام نمایشگاه اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td> 
        </tr>
        <tr>
            <td>از تاریخ :</td>
            <td><pdc:PersianDateTextBox ID="txt_from_date" runat="server"  IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_from_date"
                ErrorMessage="تاریخ نمایشگاه  اجباریست" ToolTip="تاریخ نمایشگاه اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
            <td>تا تاریخ :</td>
            <td><pdc:PersianDateTextBox ID="txt_to_date" runat="server"  IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txt_to_date"
                ErrorMessage="تاریخ نمایشگاه  اجباریست" ToolTip="تاریخ نمایشگاه اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <td>محل برگزاری :</td>
            <td><asp:DropDownList ID="lst_locate" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_locate" DataTextField="location" DataValueField="srl"></asp:DropDownList></td>
        </tr>      
        <tr>
            <td>توضیحات  :</td>
            <td colspan="3"><asp:TextBox ID="txt_desc" runat="server" CssClass="textbox"  Width="480px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>بستن نمایشگاه :</td>
            <td><asp:CheckBox ID="chk_confirm" runat="server" /></td>
        </tr>
    </table><br />
        <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
            <td><asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Project/ProjectManagment.aspx"/></td>
            <td><asp:Button ID="btnShow" runat="server" CssClass="btn btn-primary" Text="نمایش فرش ها" OnClick="btnShow_Click" /></td>
            <td><asp:Button ID="btn_print_rejected_goods" runat="server" CssClass="btn btn-primary" Text="چاپ لیست فرش های مرجوعی" PostBackUrl="~/Provider_Goods/Rejected_Goods.aspx" /></td>
                </tr>               
            </table>
        </asp:Panel><br /><br /><br />
    <asp:Panel ID="Panel_grid" runat="server" CssClass="panelbackcolor" Visible="false">
    <table>
        <tr>
            <td>نام تامین کننده :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="300" CssClass="textbox"></asp:TextBox></td>
            <td>کد فرش :</td>
            <td><asp:TextBox ID="txt_product" runat="server" Width="300" CssClass="textbox"></asp:TextBox></td>
        </tr>
        <tr>
            <td>گونه :</td>
            <td><asp:DropDownList ID="lst_city2" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_brand" DataTextField="brand_name" DataValueField="srl"></asp:DropDownList></td>
            <td>اندازه :</td>
            <td><asp:DropDownList ID="lst_size" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_size" DataTextField="size_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:Button ID="btn_filter" runat="server" CssClass="btn btn-primary" Text="فیلتر" OnClick="btn_filter_Click" /></td>
            <td><asp:Button ID="btn_save_assign" runat="server" CssClass="btn btn-primary" Text="تخصیص فرش به پرژه" OnClick="btn_save_assign_Click" /></td>
            <td><asp:Button ID="btn_view" runat="server" CssClass="btn btn-primary" Text="نمایش فرش های نمایشگاه" OnClick="btn_view_Click" /></td>
            <td><asp:Button ID="btn_delete_allassign" runat="server" CssClass="btn btn-primary" Text="حذف تمام فرش های تخصیصی" OnClick="btn_delete_allassign_Click" OnClientClick = "return confirm('آیا مطمئن هستید ؟')" /></td>
        </tr>
    </table> 
        <div><br /><hr /></div>        
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
        <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
<table style="width:900px">
  <tr>
    <td  style="width:550px">
        <asp:GridView ID="grid" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" OnSelectedIndexChanging="grid_SelectedIndexChanging">
        <Columns>                      
            <asp:HyperLinkField DataTextField="provider_srl" DataNavigateUrlFields="provider_srl" DataNavigateUrlFormatString="~/Provider_Goods/ProductAssign.aspx?srl={0}"  HeaderText="کد"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
            <asp:BoundField DataField="provider_name" HeaderText="نام تامین کننده" ItemStyle-Width="300" ReadOnly="True"/>  
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True"/>  
            <asp:TemplateField HeaderText="تائید">
                <ItemTemplate>
                    <asp:CheckBox ID="confirm" runat="server"  Width="100"/>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowSelectButton="True" HeaderText="تصویر" SelectText="تصویر"/>
        </Columns>
<AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>
    </td>
    <td style="width:350px"><asp:Image ID="image1" runat="server" Height="300" Width="350" /></td>
  </tr>
</table>

</asp:Panel>
</asp:Content>

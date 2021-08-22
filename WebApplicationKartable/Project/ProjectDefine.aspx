<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ProjectDefine.aspx.cs" Inherits="WebApplicationKartable.ProjectDefine" %>
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
<%--        <tr>
            <td>محل برگزاری :</td>
            <td><asp:DropDownList ID="lst_locate" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_locate" DataTextField="location" DataValueField="srl"></asp:DropDownList></td>
        </tr>      --%>
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
                </tr>               
            </table>
        </asp:Panel><br /><br /><br />
    <asp:Panel ID="Panel_grid" runat="server" CssClass="panelbackcolor">
        <table>
            <tr>
                <td><asp:Button ID="btn_add" runat="server" CssClass="btn btn-primary" Text="تخصیص کدها" OnClick="btn_add_Click" /></td>
                <td style="padding-right:5%"></td>
                <td><asp:Button ID="btn_remove" runat="server" CssClass="btn btn-primary" Text="حذف کدهای تخصیصی" OnClick="btn_remove_Click" OnClientClick = "return confirm('آیا مطمئن هستید ؟')" /></td>
                <td style="padding-right:5%"></td>
                <td><asp:Button ID="btn_remove_all" runat="server" CssClass="btn btn-primary" Text="حذف تمام فرشهای تخصیصی" OnClick="btn_remove_all_Click" OnClientClick = "return confirm('آیا مطمئن هستید ؟')" /></td>
            </tr>
        </table>        
        <asp:Label ID="lblAdd" runat="server" ForeColor="Red"></asp:Label>
        <br />
      <asp:TextBox ID="txt_enter_codes" TextMode="MultiLine" runat="server" Width="650" Height="300" CssClass="textbox"></asp:TextBox>        

</asp:Panel>
</asp:Content>

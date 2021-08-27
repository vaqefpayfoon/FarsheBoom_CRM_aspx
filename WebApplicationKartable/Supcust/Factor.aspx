<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Factor.aspx.cs" Inherits="WebApplicationKartable.Factor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">      
    <link rel="stylesheet" href="../css/ControlStyle.css" type="text/css" />
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>

    <asp:SqlDataSource ID="source_bank" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, bank_name FROM dbo.inv_bank order by srl desc"/>


    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
        <div style="text-align:center"><h2>صدور فاکتور</h2></div>                     
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
<asp:ValidationSummary ID="ValidationSummary1" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup2"/>
<asp:Panel ID="Panel_grid" runat="server" CssClass="panelbackcolor" Visible="false">
    <table>
        <tr>
            <td>نام مشتری :</td>
            <td><asp:TextBox ID="txtContactsSearch" runat="server" Width="180"  CssClass="textbox"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txtContactsSearch" ErrorMessage="برای ویرایش یا حذف نام مشتری باید انتخاب شود" 
ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
            <td>موبایل مشتری :</td>
            <td><asp:TextBox ID="txt_cellphone" runat="server" Width="180"  CssClass="textbox"></asp:TextBox></td>          
        </tr>
        <tr>
            <td>کد فرش :</td>
            <td><asp:TextBox ID="txt_product" runat="server" Width="150"  CssClass="textbox"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator4" runat="server" ControlToValidate="txt_product" ErrorMessage="برای ویرایش یا حذف کد فرش باید انتخاب شود" 
ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>&nbsp;&nbsp;&nbsp;<asp:Button ID="btn_list" runat="server" OnClick="btn_list_Click" Text="نمایش" CssClass="btn-facebook" /></td> 
        </tr>
    </table> 
    <br /><hr />
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
<cc1:AutoCompleteExtender ServiceMethod="FilterSearch3" MinimumPrefixLength="5"
            CompletionInterval="100" EnableCaching="false" CompletionSetCount="10" TargetControlID="txt_cellphone"
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
</asp:Panel>
<table>
    <tr>
        <td>
            <table>
        <tr>
            <td>حساب بانکی :</td>
        <td><asp:DropDownList ID="lst_bank" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_bank" DataTextField="bank_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
        <td>انتخاب نمایشگاه :</td>
        <td><asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_project" DataTextField="project_code" DataValueField="srl" AutoPostBack = "true" OnSelectedIndexChanged = "lst_project_SelectedIndexChanged"></asp:DropDownList></td>
            <td>کد فرش :</td>
            <td><asp:TextBox ID="txt_code" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue" ></asp:TextBox></td>
        </tr>
        <tr>
            <td>تاریخ :</td>
            <td><pdc:PersianDateTextBox ID="txt_factor_date" runat="server"  IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="170px" CssClass="textbox"></pdc:PersianDateTextBox></td>
            <td>شماره فاکتور :</td>
            <td><asp:TextBox ID="txt_factor_no" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox></td>
        </tr> 
        <tr>
            <td>نوع فرش :</td>
            <td><asp:TextBox ID="txt_carpet_type" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>محل بافت :</td>
            <td><asp:TextBox ID="txt_brand_name" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>اندازه :</td>
            <td><asp:TextBox ID="txt_size" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>نقشه :</td>
            <td><asp:TextBox ID="txt_plan" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>رنگ زمینه :</td>
            <td><asp:TextBox ID="txt_color" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>رنگ حاشیه :</td>
            <td><asp:TextBox ID="txt_margin" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>نوع پرز :</td>
            <td><asp:TextBox ID="txt_porz" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>نوع چله :</td>
            <td><asp:TextBox ID="txt_chele" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>قیمت خرید :</td>
            <td><asp:TextBox ID="txt_buy" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>متراژ خرید :</td>
            <td><asp:TextBox ID="txt_u_buy" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>قیمت فروش :</td>
            <td><asp:TextBox ID="txt_sale" runat="server" CssClass="textbox" Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>متراژ فروش :</td>
            <td><asp:TextBox ID="txt_u_sale" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>تخفیف نمایشگاه :</td>
            <td><asp:TextBox ID="txt_first_discount" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>تخفیف نهایی :</td>
            <td><asp:TextBox ID="txt_discount" runat="server" CssClass="textbox"  Width="180px" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>پیش پرداخت :</td>
            <td><asp:TextBox ID="txt_down_payment" runat="server" CssClass="textbox"  Width="180px" MaxLength="20"></asp:TextBox>
            </td>
            <td>مبلغ مانده :</td>
            <td><asp:TextBox ID="txt_payment" runat="server" CssClass="textbox"  Width="180px" MaxLength="20"></asp:TextBox><asp:ImageButton ID="btnShow" runat="server"  ImageUrl="~/images/Controls/Select.gif" OnClick="btnShow_Click" /><asp:Label ID="Label2" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_payment" ErrorMessage="مبلغ مانده اجباریست" ToolTip="مبلغ مانده اجباریست" ValidationGroup="RegisterUserValidationGroup2"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>نام مشتری :</td>
            <td><asp:TextBox ID="txt_full_name" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
            <td>تلفن :</td>
            <td><asp:TextBox ID="txt_tel1" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true" BackColor="LightSteelBlue"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>آدرس :</td>
            <td colspan="3"><asp:TextBox ID="txt_address" runat="server" CssClass="textbox" ReadOnly="true" BackColor="LightSteelBlue" Width="450px"></asp:TextBox></td>
        </tr>
        <tr>
            <%--<td>بیانه :</td>--%>
            <td><asp:CheckBox ID="chk_bayane" runat="server" Visible="false" ></asp:CheckBox></td>
        </tr>
</table>
        </td>
    <td>
        <asp:Image ID="image1" runat="server" Height="300" Width="350" />
    </td>
    </tr>
</table>
        <asp:Panel ID="Panel1" runat="server" CssClass="panelbackcolor">
            <table>
            <tr>
            <td><asp:ImageButton ID="ImageButton_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="ImageButton_save_Click" ValidationGroup="RegisterUserValidationGroup2"/></td>
            <td><asp:ImageButton ID="ImageButton_print" ToolTip="چاپ" runat="server" ImageUrl="~/images/Controls/Print.png" OnClick="ImageButton_print_Click"/></td>
            <td><asp:ImageButton ID="ImageButton2" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Supcust/FactorsManagment.aspx"/></td></tr>               
            </table>
        </asp:Panel>
<cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_discount" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
<cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="txt_down_payment" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
<cc1:MaskedEditExtender ID="MaskedEditExtender3" runat="server" TargetControlID="txt_payment" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
</asp:Content>

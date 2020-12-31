<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Purchase_Price.aspx.cs" Inherits="WebApplicationKartable.Purchase_Price" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
    <div style="text-align:center;"><h2><asp:Label ID="lbl_header" runat="server">قیمت خرید</asp:Label></h2></div>
<cc1:ToolkitScriptManager ID = "ToolkitScriptManager" runat = "server"></cc1:ToolkitScriptManager>
         <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>     
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
         <tr>
            <td>مبلغ خرید :</td>
            <td><asp:TextBox ID="txt_price" runat="server" CssClass="textbox"  Width="180px"   MaxLength="12"></asp:TextBox><asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_price" ErrorMessage="مبلغ اجباریست" ToolTip="مبلغ اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
            <td>تاریخ :</td>
            <td><pdc:PersianDateTextBox ID="txt_from_date" runat="server"  IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox></td>
        </tr>   
        <tr>
            <td>مبلغ فعال :</td>
            <td><asp:CheckBox ID="chk_active" runat="server" /></td>
        </tr>
    </table>
    <br/><hr /><br />
        <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor" Enabled="false">
            <table>
                <tr>
            <td><asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
            <td><asp:ImageButton ID="ImageButton_addnew" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="ImageButton_addnew_Click"/></td>
                <td><asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" OnClick="btn_return_Click"/></td>
                </tr>               
            </table>
        </asp:Panel>
    <asp:GridView ID="grid" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" OnSelectedIndexChanged="gridview_SelectedIndexChanged">
        <Columns>                      
            <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True"/>             
            <asp:BoundField DataField="u_date_time" HeaderText="تاریخ" ItemStyle-Width="120" ReadOnly="True"/>
            <asp:BoundField DataField="price" HeaderText="مبلغ خرید" ItemStyle-Width="300"  DataFormatString="{0:C0}" /> 
            <asp:TemplateField HeaderText="تائید">
                <ItemTemplate>
                    <asp:CheckBox ID="active" runat="server" Checked='<%# Eval("active") %>'  Width="40"/>
                </ItemTemplate>
            </asp:TemplateField>
    <asp:CommandField ShowSelectButton ="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:LinkButton ID="lnkRemove" runat="server" CommandArgument = '<%# Eval("srl")%>' 
                 OnClientClick = "return confirm('آیا مطمئن هستید ؟')"
                Text = "حذف" OnClick = "lnkRemove_Click"  ControlStyle-ForeColor="Maroon" ControlStyle-Font-Bold="true">
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        </Columns>
         <AlternatingRowStyle BackColor="#41b5ff" /><PagerStyle BackColor="Azure" ForeColor="Navy" HorizontalAlign="Center" />     
    </asp:GridView>
 <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="txt_price" Mask="9,999,999,999" MessageValidatorTip="true" OnFocusCssClass="MaskedEditFocus" OnInvalidCssClass="MaskedEditError" MaskType="Number" InputDirection="RightToLeft" AcceptNegative="None" DisplayMoney="None" ErrorTooltipEnabled="True" />
</asp:Content>

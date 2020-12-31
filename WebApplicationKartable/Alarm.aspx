<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Alarm.aspx.cs" Inherits="WebApplicationKartable.Alarm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="text-align:center;"><h2><asp:Label ID="lbl_header" runat="server">تعریف آلارم</asp:Label></h2></div>
         <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>     
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>عنوان :</td>
            <td><asp:TextBox ID="txt_subject" runat="server" CssClass="textbox"  Width="200px"   MaxLength="100"></asp:TextBox><asp:Label ID="lblName" Text="*"  runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_subject"
                ErrorMessage="عنوان اجباریست" ToolTip="عنوان اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td> 
            <td>تاریخ :</td>
            <td><pdc:PersianDateTextBox ID="txt_from_date" runat="server"  IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox>
                <asp:RequiredFieldValidator Display = "None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_from_date"
                ErrorMessage="تاریخ اجباریست" ToolTip="تاریخ اجباریست"
                ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator></td>
        </tr>      
        <tr>
            <td>توضیحات  :</td>
            <td colspan="3"><asp:TextBox ID="txt_desc" runat="server" CssClass="textbox"  Width="480px"></asp:TextBox></td>
        </tr>
    </table><br />
            <table>
                <tr>
            <td><asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
            <td><asp:ImageButton ID="btn_new" ToolTip="جدید" runat="server" ImageUrl="~/images/Controls/addnew.png" OnClick="btn_new_Click" ValidationGroup="RegisterUserValidationGroup"/></td>
            <td><asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/HomePage.aspx"/></td>
                </tr>               
            </table>
<br /><br /><br />
<asp:GridView ID="grid" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview_SelectedIndexChanged">
        <Columns>                      
            <asp:BoundField DataField="srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True"/>              
            <asp:BoundField DataField="date_time" HeaderText="تاریخ" ItemStyle-Width="200" ReadOnly="True"/>        
            <asp:BoundField DataField="alarm_subject" HeaderText="عنوان" ItemStyle-Width="200" ReadOnly="True"/> 
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
</asp:Content>

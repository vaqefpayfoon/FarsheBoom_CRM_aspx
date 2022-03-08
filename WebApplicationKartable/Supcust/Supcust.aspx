<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Supcust.aspx.cs" Inherits="WebApplicationKartable.Supcust" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/ControlStyle.css" type="text/css" />
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />
    <style type="text/css">
        .loading {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="text-align: center">
        <h2>مشتری</h2>
    </div>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
        ValidationGroup="RegisterUserValidationGroup" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>نام مشتری :</td>
            <td colspan="3">
                <asp:TextBox ID="txt_full_name" runat="server" CssClass="textbox" Width="580px" MaxLength="100"></asp:TextBox><asp:Label ID="lblName" Text="*" runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_full_name"
                    ErrorMessage="نام مشتری اجباریست" ToolTip="نام مشتری اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>تلفن :</td>
            <td>
                <asp:TextBox ID="txt_tel1" runat="server" CssClass="textbox" Width="180px" MaxLength="12"></asp:TextBox></td>
            <td>موبایل :</td>
            <td>
                <asp:TextBox ID="txt_cell_phone" runat="server" CssClass="textbox" Width="180px" MaxLength="12"></asp:TextBox>
                <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="RequiredFieldValidator1" runat="server" ControlToValidate="txt_cell_phone"
                    ErrorMessage="نام مشتری اجباریست" ToolTip="نام مشتری اجباریست" ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
            <tr>
            <td>مراجعه به نمایشگاه :</td>
            <td>
                <asp:CheckBox ID="chk_sex" runat="server" Width="180px" Checked="false"></asp:CheckBox>
            </td>
            <td>خریدار :</td>
            <td>
                <asp:CheckBox ID="chk_age" runat="server" Width="180px" Enabled="false" Checked="false"></asp:CheckBox>
            </td>
        </tr>
        <tr>
            <td>کد ملی :</td>
            <td>
                <asp:TextBox ID="txt_email" runat="server" CssClass="textbox" Width="180px" MaxLength="10"></asp:TextBox></td>
        </tr>
        <tr>
            <td>تاریخ :</td>
            <td>
                <asp:TextBox ID="txt_u_date_time" runat="server" CssClass="textbox" Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>آدرس  :</td>
            <td colspan="3">
                <asp:TextBox ID="txt_address" runat="server" CssClass="textbox" Width="580px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>توضیحات  :</td>
            <td colspan="3">
                <asp:TextBox ID="txt_desc" runat="server" CssClass="textbox" Width="580px"></asp:TextBox></td>
        </tr>
    </table>
    <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup" /></td>
                <td>
                    <asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Supcust/SupcustManagers.aspx" /></td>
            </tr>
        </table>
    </asp:Panel>

    <script type="text/javascript">
        function OnClientPopulating(sender, e) {
            sender._element.className = "loading";
        }
        function OnClientCompleted(sender, e) {
            sender._element.className = "";
        }
    </script>
</asp:Content>

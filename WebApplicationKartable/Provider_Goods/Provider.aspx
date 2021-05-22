<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Provider.aspx.cs" Inherits="WebApplicationKartable.Provider" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="../css/ControlStyle.css" type="text/css" />
    <%@ Register Assembly="PersianDateControls 2.0" Namespace="PersianDateControls" TagPrefix="pdc" %>
    <link rel="stylesheet" href="../css/StyleSheetDate.css" type="text/css" />
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:SqlDataSource ID="source_city" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, city_name FROM dbo.bas_city" />
    <pdc:PersianDateScriptManager ID="PersianDateScriptManager" runat="server" CalendarCSS="PickerCalendarCSS"
        CalendarDayWidth="50" FooterCSS="PickerFooterCSS" ForbidenCSS="PickerForbidenCSS"
        ForbidenDates="" ForbidenWeekDays="" FrameCSS="PickerCSS" HeaderCSS="PickerHeaderCSS"
        SelectedCSS="PickerSelectedCSS" WeekDayCSS="PickerWeekDayCSS" WorkDayCSS="PickerWorkDayCSS">
    </pdc:PersianDateScriptManager>
    <div style="text-align: center">
        <h2>تعریف تامین کننده</h2>
    </div>
    <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
        ValidationGroup="RegisterUserValidationGroup" />
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager" runat="server"></cc1:ToolkitScriptManager>
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>کد تامین کننده :</td>
            <td>
                <asp:TextBox ID="txt_provider_code" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox><asp:Label ID="Label3" Text="*" runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="RequiredFieldValidator3" runat="server" ControlToValidate="txt_provider_code"
                    ErrorMessage="کد تامین کننده  اجباریست" ToolTip="کد تامین کننده  اجباریست"
                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>نام تامین کننده :</td>
            <td>
                <asp:TextBox ID="txt_provider_name" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox><asp:Label ID="lblName" Text="*" runat="server"></asp:Label>
                <asp:RequiredFieldValidator Display="None" ForeColor="Red" ID="UserNameRequired" runat="server" ControlToValidate="txt_provider_name"
                    ErrorMessage="نام تامین کننده اجباریست" ToolTip="نام تامین کننده  اجباریست"
                    ValidationGroup="RegisterUserValidationGroup"></asp:RequiredFieldValidator>
            </td>
            <td>برند تجاری :</td>
            <td>
                <asp:TextBox ID="txt_related_person" runat="server" CssClass="textbox" Width="180px" MaxLength="100"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>تلفن اول :</td>
            <td>
                <asp:TextBox ID="txt_tel1" runat="server" CssClass="textbox" Width="180px" MaxLength="12"></asp:TextBox></td>
            <td>تلفن دوم :</td>
            <td>
                <asp:TextBox ID="txt_tel2" runat="server" CssClass="textbox" Width="180px" MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td>همراه :</td>
            <td>
                <asp:TextBox ID="txt_cell_phone" runat="server" CssClass="textbox" Width="180px" MaxLength="12"></asp:TextBox></td>
            <td>فکس :</td>
            <td>
                <asp:TextBox ID="txt_fax1" runat="server" CssClass="textbox" Width="180px" MaxLength="12"></asp:TextBox></td>
        </tr>
        <tr>
            <td>نوع فعالیت :</td>
            <td>
                <asp:DropDownList ID="lst_type" runat="server" CssClass="dropdown1" Width="180px">
                    <asp:ListItem Value="1" Text="..."></asp:ListItem>
                    <asp:ListItem Value="2" Text="تولید کننده"></asp:ListItem>
                    <asp:ListItem Value="3" Text="پخش کننده"></asp:ListItem>
                </asp:DropDownList></td>

        </tr>
        <tr>
            <td>آدرس  :</td>
            <td colspan="3">
                <asp:TextBox ID="txt_address" runat="server" CssClass="textbox" Width="570px"></asp:TextBox></td>
        </tr>
        <tr>
            <td>توضیحات  :</td>
            <td colspan="3">
                <asp:TextBox ID="txt_desc" runat="server" CssClass="textbox" Width="570px"></asp:TextBox></td>
        </tr>
    </table>
    <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="btn_save" ToolTip="ذخیره" runat="server" ImageUrl="~/images/Controls/save.png" OnClick="btn_save_Click" ValidationGroup="RegisterUserValidationGroup" /></td>
                <td>
                    <asp:ImageButton ID="btn_return" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/Provider_Goods/Provider_Managment.aspx" /></td>
            </tr>
        </table>
    </asp:Panel>


</asp:Content>

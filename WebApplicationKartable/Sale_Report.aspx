<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Sale_Report.aspx.cs" Inherits="WebApplicationKartable.Sale_Report" %>

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
    <asp:Panel ID="Panel_grid" runat="server" CssClass="panelbackcolor">
        <table>
            <tr>
                <td>از فاکتور :</td>
                <td>
                    <pdc:PersianDateTextBox ID="txt_from_date" runat="server" IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox></td>
                <td>تا تاریخ :</td>
                <td>
                    <pdc:PersianDateTextBox ID="txt_to_date" runat="server" IconUrl="~/images/Calendar.gif" SetDefaultDateOnEvent="OnClick" Width="200px" CssClass="textbox"></pdc:PersianDateTextBox></td>
            </tr>
            <tr>
                <td>انتخاب نمایشگاه :</td>
                <td>
                    <asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1" Width="180px"></asp:DropDownList></td>
                <td>تامین کننده :</td>
                <td>
                    <asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1" Width="180px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>حساب بانکی :</td>
                <td>
                    <asp:DropDownList ID="lst_bank" runat="server" CssClass="dropdown1" Width="180px"></asp:DropDownList></td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton_Report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/Report.png" OnClick="btn_filter_Click" /></td>
                <td>
                    <asp:ImageButton ID="ImageButton_print" ToolTip="چاپ" runat="server" ImageUrl="~/images/Controls/Print.png" PostBackUrl="~/Project/Financial.aspx" /></td>
                <td>
                    <asp:ImageButton ID="imaged_product" ToolTip="چاپ عکس دار" runat="server" ImageUrl="~/images/Controls/Print.png" OnClick="imaged_product_Click" /></td>
            </tr>
        </table>
    </asp:Panel>
    <br />
    <hr />
    <table>
        <tr>
            <td>تعداد :</td>
            <td>
                <asp:TextBox ID="txt_sum" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td>مجموع خرید :</td>
            <td>
                <asp:TextBox ID="txt_buy" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
                <tr>
            <td>مجموع بیعانه</td>
            <td>
                <asp:TextBox ID="txt_down_payment" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td>مجموع مانده :</td>
            <td>
                <asp:TextBox ID="txt_remain" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>قیمت نمایشگاه</td>
            <td>
                <asp:TextBox ID="txt_payment" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td>مجموع تخفیف :</td>
            <td>
                <asp:TextBox ID="txt_total_discount" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td>حاشیه سود</td>
            <td>
                <asp:TextBox ID="txt_margin" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td>سود نهایی :</td>
            <td>
                <asp:TextBox ID="txt_profit" runat="server" CssClass="textbox" BackColor="LightSteelBlue" Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
    </table>
    <br />
    <hr />
    <asp:GridView ID="gridview" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D" HeaderStyle-ForeColor="White" Font-Size="10">
        <Columns>
            <asp:HyperLinkField DataTextField="srl_f" DataNavigateUrlFields="srl_f" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}" HeaderText="فاکتور" ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
            <asp:BoundField DataField="u_date_tome" HeaderText="تاریخ" ItemStyle-Width="80" ReadOnly="True" />
            <asp:BoundField DataField="code_igd" HeaderText="کد" ItemStyle-Width="80" ReadOnly="True" />
            <asp:BoundField DataField="factor_no" HeaderText="ش فاکتور" ItemStyle-Width="80" ReadOnly="True" />
            <asp:BoundField DataField="provider_name" HeaderText="تامین کننده" ItemStyle-Width="200" ReadOnly="True" />
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="80" ReadOnly="True" />
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="80" ReadOnly="True" />
            <%--<asp:BoundField DataField="area" HeaderText="مساحت" ItemStyle-Width="80" ReadOnly="True" />--%>
            <asp:BoundField DataField="buy_price" HeaderText="خرید" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="sale_price" HeaderText="فروش" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <%--<asp:BoundField DataField="discount" HeaderText="تخفیف" ItemStyle-Width="60" />--%>
            <asp:BoundField DataField="discount_amount" HeaderText="تخفیف" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="down_payment" HeaderText="پیش پرداخت" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="final_sale" HeaderText="نمایشگاه" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="final_discount" HeaderText="تخفیف ن" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="final_price" HeaderText="قیمت ن" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="margin_profit" HeaderText="حاشیه سود" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="final_profit2" HeaderText="سود" ItemStyle-Width="90" DataFormatString="{0:C0}" />
            <asp:BoundField DataField="bank_name" HeaderText="بانک" ItemStyle-Width="80" ReadOnly="True" />
        </Columns>
        <AlternatingRowStyle BackColor="Azure" />
        <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>
</asp:Content>

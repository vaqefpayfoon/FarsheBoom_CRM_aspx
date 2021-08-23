<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="FactorsList.aspx.cs" Inherits="WebApplicationKartable.FactorsList" %>
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

    <%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>
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
                <td>
                    درصد تخفیف :
                </td>
                <td>
                    <asp:TextBox ID="txt_discount" runat="server" CssClass="textbox" Width="150px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:ImageButton ID="ImageButton_Report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/Report.png" OnClick="ImageButton_Report_Click" /></td>
                <td>
                    <asp:ImageButton ID="ImageButton_print" ToolTip="چاپ" runat="server" ImageUrl="~/images/Controls/Print.png" OnClick="ImageButton_print_Click" /></td>
                <td>
                    <asp:ImageButton ID="excel_export" ToolTip="خروجی اکسل" runat="server" ImageUrl="~/images/Controls/Report.png" OnClick="excel_export_Click" /></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:GridView ID="gridview" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D" HeaderStyle-ForeColor="White" AllowPaging="true" PageSize="14" OnPageIndexChanging="gridview_PageIndexChanging" Width="900px">
    <Columns>                      
    <asp:HyperLinkField DataTextField="srl_f" DataNavigateUrlFields="srl_f" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}"  HeaderText="فاکتور"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
        <asp:HyperLinkField DataTextField="srl" DataNavigateUrlFields="srl" DataNavigateUrlFormatString="~/Provider_Goods/ProductAssign.aspx?srl={0}"  HeaderText="فرش"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
    <asp:BoundField DataField="u_date_tome" HeaderText="تاریخ" ItemStyle-Width="80" ReadOnly="True"/> 
    <asp:BoundField DataField="code_igd" HeaderText="کد" ItemStyle-Width="80" ReadOnly="True"/>   
    <asp:BoundField DataField="factor_no" HeaderText="ش فاکتور" ItemStyle-Width="80" ReadOnly="True"/>
    <asp:BoundField DataField="provider_name" HeaderText="تامین کننده" ItemStyle-Width="200" ReadOnly="True"/>  
    <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="80" ReadOnly="True"/>   
    <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="80" ReadOnly="True"/>
    <asp:BoundField DataField="area" HeaderText="مساحت" ItemStyle-Width="80" ReadOnly="True"/>    
            <asp:BoundField DataField="discount" HeaderText="تخفیف" ItemStyle-Width="60" />
    <asp:BoundField DataField="discount_amount" HeaderText="تخفیف" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="down_payment" HeaderText="پیش پرداخت" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
<asp:BoundField DataField="sale_price" HeaderText="قیمت فروش" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
    <asp:BoundField DataField="final_price" HeaderText="مبلغ فاکتور" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
                <asp:TemplateField HeaderText="چاپ">  
                    <ItemTemplate>  
                        <asp:CheckBox ID="CheckBox1" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField>  

        </Columns>
    <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" /> 
    </asp:GridView>
    <br /><br />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" Width="900" runat="server"></rsweb:ReportViewer>
</asp:Content>

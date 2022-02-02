<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupcustReport.aspx.cs" Inherits="WebApplicationKartable.SupcustReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table>
        <tr>
            <td>نوع مشتری :</td>
            <td>
                <asp:DropDownList ID="lst_status" runat="server" CssClass="dropdown1" Width="150px">
                    <asp:ListItem Value="0" Text="همه"></asp:ListItem>
                    <asp:ListItem Value="1" Text="مراجعه کننده"></asp:ListItem>
                    <asp:ListItem Value="2" Text="خریدار"></asp:ListItem>
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btn_report" runat="server" CssClass="btn-facebook" OnClick="btn_report_Click" Text="گزارش مشتریان"/>
            </td>
            <td>
                <asp:Button ID="btn_excel" runat="server" CssClass="btn-facebook" OnClick="btn_excel_Click" Text="خروجی اکسل"/>
            </td>
        </tr>
    </table>
    <div class="row">
            <asp:GridView ID="gridview" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D" HeaderStyle-ForeColor="White" AllowPaging="true" PageSize="14" Width="950px" OnPageIndexChanging="gridview_PageIndexChanging">
    <Columns>                      
    <asp:HyperLinkField DataTextField="srl" DataNavigateUrlFields="srl" DataNavigateUrlFormatString="~/Supcust/Supcust.aspx?srl={0}"  HeaderText="لینک"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
 
    <asp:BoundField DataField="u_date_time" HeaderText="تاریخ" ItemStyle-Width="80" ReadOnly="True"/> 
    <asp:BoundField DataField="full_name" HeaderText="نام و نام خانوادگی" ItemStyle-Width="200" ReadOnly="True"/>
    <asp:BoundField DataField="tel1" HeaderText="تلفن" ItemStyle-Width="300" ReadOnly="True"/>  
    <asp:BoundField DataField="cell_phone" HeaderText="موبایل" ItemStyle-Width="100" ReadOnly="True"/>  
    <asp:BoundField DataField="isRefrence" HeaderText="وضعیت مراجعه" ItemStyle-Width="300" ReadOnly="True"/>
    <asp:BoundField DataField="isBuyer" HeaderText="وضعیت خرید" ItemStyle-Width="300" ReadOnly="True"/>
        </Columns>
    <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" /> 
    </asp:GridView>
    </div>
</asp:Content>

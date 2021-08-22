<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Depositis.aspx.cs" Inherits="WebApplicationKartable.Depositis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" AllowPaging ="true"  ShowFooter = "true"  
        OnPageIndexChanging = "OnPaging">
    <Columns>                      
    <asp:HyperLinkField DataTextField="srl_f"  DataNavigateUrlFields="srl_f" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}"  HeaderText="فاکتور"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />
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
    <asp:BoundField DataField="final_price" HeaderText="مبلغ فاکتور" ItemStyle-Width="90" DataFormatString="{0:C0}"/>
        </Columns>
<AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" /> 
    </asp:GridView>

</asp:Content>

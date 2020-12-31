<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SaleReportPics.aspx.cs" Inherits="WebApplicationKartable.SaleReportPics" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name"/>
       <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
            <td>تعداد فرش ها :</td>
            <td><asp:TextBox ID="txt_count" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true"></asp:TextBox></td>
        </tr>
    </table>
        <br />
           <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" Width="900px">
        <Columns>
            <asp:HyperLinkField DataTextField="srl_f" DataNavigateUrlFields="srl_f" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}"  HeaderText="فاکتور"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />               
            <asp:BoundField DataField="factor_no" HeaderText="شماره" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="u_date_tome" HeaderText="تاریخ" ItemStyle-Width="80" ReadOnly="True"/>   
            <asp:BoundField DataField="code_igd" HeaderText="کد" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:BoundField DataField="provider_name" HeaderText="تامین کننده" ItemStyle-Width="120" ReadOnly="True"/>
            <asp:BoundField DataField="buy_price" HeaderText="خرید" ItemStyle-Width="80" ReadOnly="True" DataFormatString="{0:C0}"/>
            <asp:BoundField DataField="final_profit2" HeaderText="سود نهایی" ItemStyle-Width="80" ReadOnly="True" DataFormatString="{0:C0}"/>
            <asp:ImageField DataImageUrlField="title_igd" HeaderText="تصویر" ItemStyle-Width="300"></asp:ImageField> 
            <asp:CommandField ShowSelectButton ="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
</asp:Panel>
</asp:Content>

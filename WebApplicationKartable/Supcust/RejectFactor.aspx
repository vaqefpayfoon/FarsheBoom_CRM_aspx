<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RejectFactor.aspx.cs" Inherits="WebApplicationKartable.RejectFactor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
        <td><asp:ImageButton ID="btn_report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>
        </tr>
    </table>
        <br />
           <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview_SelectedIndexChanged" Width="900px">
        <Columns>  
        <asp:HyperLinkField DataTextField="srl" DataNavigateUrlFields="srl" DataNavigateUrlFormatString="~/Supcust/Factor.aspx?snd={0}"  HeaderText="فاکتور"  ControlStyle-ForeColor="Black" ItemStyle-Width="70" Target="_blank" />  
            <asp:BoundField DataField="factor_no" HeaderText="شماره" ItemStyle-Width="100" ReadOnly="True"/>      
            <asp:BoundField DataField="u_date_tome" HeaderText="تاریخ" ItemStyle-Width="100" ReadOnly="True"/>             
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="chele_title" HeaderText="چله" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="carpet_title" HeaderText="نوع" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="payment" HeaderText="مبلغ" ItemStyle-Width="100" ReadOnly="True" DataFormatString="{0:C0}"/>
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
</asp:Content>

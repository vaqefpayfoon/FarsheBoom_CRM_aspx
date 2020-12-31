<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SupcustLevel_Export.aspx.cs" Inherits="WebApplicationKartable.SupcustLevel_Export" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="text-align:center"><h2>خروجی سطح ارتباطات</h2></div>
      <asp:ValidationSummary ID="RegisterUserValidationSummary" runat="server" ForeColor="Red"
            ValidationGroup="RegisterUserValidationGroup"/>    
 <asp:SqlDataSource ID="source_clue" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, meet_title FROM dbo.bas_sale_clue"/>                  
<asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
        <td>سطح ارتباط :</td>
        <td><asp:DropDownList ID="lst_clue" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_clue" DataTextField="meet_title" DataValueField="srl"></asp:DropDownList></td>
        </tr>
        <tr>
            <td><asp:ImageButton ID="report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="report_Click"/></td>
        </tr>
    </table>
</asp:Content>

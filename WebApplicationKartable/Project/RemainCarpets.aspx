<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="RemainCarpets.aspx.cs" Inherits="WebApplicationKartable.RemainCarpets" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name"/>
       <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
        <td>انتخاب نمایشگاه :</td>
        <td><asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_project" DataTextField="project_code" DataValueField="srl"></asp:DropDownList></td>
        <td>تامین کننده :</td>
        <td><asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_provider" DataTextField="provider_name" DataValueField="srl"></asp:DropDownList></td>
        <td><asp:ImageButton ID="btn_report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>
        </tr>
        <tr>
            <td>تعداد فرش ها :</td>
            <td><asp:TextBox ID="txt_count" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td><asp:Button ID="btn_assign" runat="server" CssClass="btn-facebook" Text="تخصیص به نمایشگاه" OnClick="btn_assign_Click"/></td>
            <td><asp:Button ID="btn_delete_assign" runat="server" CssClass="btn-facebook" Text="حذف تخصیص" OnClick="btn_delete_assign_Click"/></td>
        </tr>
    </table>
        <br />
           <asp:GridView ID="gridview" runat="server"  CssClass="textbox"  AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D"  HeaderStyle-ForeColor="White" onselectedindexchanged="gridview_SelectedIndexChanged" Width="900px">
        <Columns>                      
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True"/>
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="80" ReadOnly="True"/>   
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:BoundField DataField="plan_title" HeaderText="نقشه" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:BoundField DataField="carpet_title" HeaderText="نوع" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:BoundField DataField="chele_title" HeaderText="چله" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:BoundField DataField="porz_title" HeaderText="پرز" ItemStyle-Width="80" ReadOnly="True"/>
            <asp:ImageField DataImageUrlField="title_igd" HeaderText="title_igd" ItemStyle-Width="300"></asp:ImageField> 
            <asp:CommandField ShowSelectButton ="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        </Columns>
         <AlternatingRowStyle BackColor="Azure" /> <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />     
    </asp:GridView>
</asp:Panel>
</asp:Content>

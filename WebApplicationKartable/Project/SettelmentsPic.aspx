<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="SettelmentsPic.aspx.cs" Inherits="WebApplicationKartable.SettelmentsPic" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name"/>
       <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
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

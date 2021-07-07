<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="MainCallBack.aspx.cs" Inherits="WebApplicationKartable.MainCallBack" %>


<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=15.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>

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
        <td><asp:ImageButton ID="btn_print" ToolTip="چاپ عکس دار" runat="server" ImageUrl="~/images/Controls/Print.png" OnClick="btn_print_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>
        </tr>
        <tr>
            <td>تعداد فرش ها :</td>
            <td><asp:TextBox ID="txt_count" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true"></asp:TextBox></td>
            <td>تمام تامین کننده ها :</td>
            <td><asp:CheckBox ID="chk_all" runat="server" Checked="false" CssClass="checkbox" /></td>
        </tr>
        <tr>
            <td>الگو مرتب سازی :</td>
            <td><asp:DropDownList ID="lst_sort" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="brand_name,size_title" Text="گونه - اندازه"></asp:ListItem>
                <asp:ListItem  Value="size_title,brand_name" Text="اندازه - گونه"></asp:ListItem>
                <asp:ListItem  Value="carpet_title" Text="نوع"></asp:ListItem>
            </asp:DropDownList></td>
        </tr>
    </table>
</asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:reportviewer ID="ReportViewer1" runat="server" Width="900"></rsweb:reportviewer>
</asp:Content>

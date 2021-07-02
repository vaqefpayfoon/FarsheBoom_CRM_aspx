<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CallBackReportCouples.aspx.cs" Inherits="WebApplicationKartable.CallBackReportCouples" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>
       <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
    <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
    <table>
        <tr>
        <td>انتخاب نمایشگاه :</td>
        <td><asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_project" DataTextField="project_code" DataValueField="srl"></asp:DropDownList></td>
        <td style="padding-left:5%;"></td>
        <td>تامین کننده :</td>
        <td><asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1"  Width="180px"></asp:DropDownList></td>
        <td style="padding-left:5%;"></td>
        <td><asp:ImageButton ID="btn_report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>            
        </tr>
        <tr>
            <td>الگو مرتب سازی :</td>
            <td><asp:DropDownList ID="lst_sort" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="code_igd" Text="کد فرش بوم"></asp:ListItem>
                <asp:ListItem  Value="brand_name,size_title" Text="گونه"></asp:ListItem>
                <asp:ListItem  Value="size_title,brand_name" Text="اندازه"></asp:ListItem>
                <asp:ListItem  Value="carpet_title" Text="نوع"></asp:ListItem>
                <asp:ListItem  Value="provider_code" Text="کد تامین کننده"></asp:ListItem>              
            </asp:DropDownList></td>
        </tr>
    </table>
</asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:reportviewer Id="ReportViewer1" Width="900" runat="server"></rsweb:reportviewer>
</asp:Content>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Settelment.aspx.cs" Inherits="WebApplicationKartable.Settelment" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
        <td style="padding-left:5%;"></td>
        <td>تامین کننده :</td>
        <td><asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1"  Width="180px"></asp:DropDownList></td>
        <td style="padding-left:5%;"></td>
        <td><asp:ImageButton ID="btn_report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>          <td> 
                <asp:Button ID="btn_print_pic" runat="server" CssClass="btn btn-primary" Text="لیست عکس دار" OnClick="btn_print_pic_Click"/>
            </td>  
        </tr>
    </table>
</asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:reportviewer runat="server" Id="ReportViewer1" Width="900"></rsweb:reportviewer>
</asp:Content>

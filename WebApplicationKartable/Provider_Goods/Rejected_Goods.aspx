<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Rejected_Goods.aspx.cs" Inherits="WebApplicationKartable.Rejected_Goods" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div style="text-align:center"><h2>بازگشت فرش ها به تامین کننده ها</h2></div>
<asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_name FROM dbo.bas_project order by srl desc"/>
    <table>
        <tr>
        <td>نام نمایشگاه :</td>
        <td><asp:DropDownList ID="lst_project" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_project" DataTextField="project_name" DataValueField="srl"></asp:DropDownList></td>
        </tr>
    </table>
        <asp:Panel ID="Panel1" runat="server" CssClass="panelbackcolor">
            <table>
                <tr>
            <td><asp:ImageButton ID="ImageButton_print" ToolTip="چاپ" runat="server" ImageUrl="~/images/Controls/Print.png" OnClick="ImageButton_print_Click"/></td>
            <td><asp:ImageButton ID="ImageButton2" ToolTip="بازگشت" runat="server" ImageUrl="~/images/Controls/back.png" PostBackUrl="~/HomePage.aspx"/></td></tr>               
            </table>
        </asp:Panel>    
    <br />
    <hr />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" Width="800px"></rsweb:ReportViewer>
</asp:Content>

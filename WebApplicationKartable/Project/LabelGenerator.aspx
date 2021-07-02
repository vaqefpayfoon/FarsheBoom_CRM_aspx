<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="LabelGenerator.aspx.cs" Inherits="WebApplicationKartable.LabelGenerator" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
            <style type="text/css">
        .loading
        {
            background-image: url(../images/loader.gif);
            background-position: left;
            background-repeat: no-repeat;
        }
        .modalBackground
        {
            background-color: black;
        }
        .modalPopup
        {
            background-color:whitesmoke;
            border:3px solid #ccc;
            padding:10px;
            height: 510px;
            width:610px;
            font-size:15px
        }
</style>
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
            <td><asp:ImageButton ID="btn_report" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>            
        </tr>
        <tr>
            <td>الگو :</td>
            <td><asp:DropDownList ID="lst_sort" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="0" Text="تمام لیبل ها"></asp:ListItem>
                <asp:ListItem  Value="1" Text="با تخفیف 5%"></asp:ListItem>
                <asp:ListItem  Value="2" Text="بیشتر از 5%"></asp:ListItem>
                <asp:ListItem  Value="3" Text="کمتر از 5%"></asp:ListItem>
                <asp:ListItem  Value="brand_name,size_title" Text="گونه - اندازه"></asp:ListItem>
                <asp:ListItem  Value="size_title,brand_name" Text="اندازه - گونه"></asp:ListItem>
            </asp:DropDownList></td>
        </tr>
    </table>
        <table>
        <tr>
        <td>کد فرش ها :</td>
        <td><asp:TextBox ID="txt_enter_codes" TextMode="MultiLine" runat="server" Width="650" Height="100" CssClass="textbox"></asp:TextBox></td>
            <td style="padding-left:5%;"></td>
            <td><asp:ImageButton ID="btn_filter" ToolTip="گزارش" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_filter_Click"  ValidationGroup="RegisterUserValidationGroup"/></td>            
        </tr>
    </table>
           <br />
           <hr />
           <table>
               <tr>
        <td>تامین کننده :</td>
        <td><asp:DropDownList ID="lst_provider" runat="server" CssClass="dropdown1"  Width="180px" DataSourceID="source_provider" DataTextField="provider_name" DataValueField="srl"></asp:DropDownList></td>
            <td><asp:ImageButton ID="btn_report_provider" ToolTip="بر اساس تامین کننده" runat="server" ImageUrl="~/images/Controls/report.png" OnClick="btn_report_provider_Click"  ValidationGroup="RegisterUserValidationGroup"/></td> 
               </tr>
           </table>
</asp:Panel>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<rsweb:ReportViewer ID="ReportViewer1" runat="server" Width="900"></rsweb:ReportViewer>
</asp:Content>

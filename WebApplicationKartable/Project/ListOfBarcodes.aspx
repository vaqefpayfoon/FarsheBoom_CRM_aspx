﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="ListOfBarcodes.aspx.cs" Inherits="WebApplicationKartable.ListOfBarcodes" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
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
            <td>الگو مرتب سازی :</td>
            <td><asp:DropDownList ID="lst_sort" runat="server" CssClass="dropdown1"  Width="180px">
                <asp:ListItem  Value="brand_name,size_title" Text="گونه - اندازه"></asp:ListItem>
                <asp:ListItem  Value="size_title,brand_name" Text="اندازه - گونه"></asp:ListItem>
                <asp:ListItem  Value="carpet_title" Text="نوع"></asp:ListItem>
            </asp:DropDownList></td>
               </tr>
               <tr>
                <td>تعداد فرش ها :</td>
                <td><asp:TextBox ID="txt_count" runat="server" CssClass="textbox"  Width="180px" ReadOnly="true"></asp:TextBox></td>
               </tr>
            <tr>
                <td><asp:Button ID="btn_absent" runat="server" CssClass="btn btn-primary" Text="فرش های غایب" OnClick="btn_add_Click"
                    /></td>
               <td><asp:Button ID="btn_present" runat="server" CssClass="btn btn-primary" Text="فرش های حاضر" OnClick="btn_present_Click"
                    /></td>
            </tr>
           </table>
       </asp:Panel>
        <br />
      <asp:TextBox ID="txt_enter_codes" TextMode="MultiLine" runat="server" Width="650" Height="300" CssClass="textbox"></asp:TextBox> 
    <br /><br /><hr />
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="500px" Width="900px"></rsweb:ReportViewer> 
</asp:Content>
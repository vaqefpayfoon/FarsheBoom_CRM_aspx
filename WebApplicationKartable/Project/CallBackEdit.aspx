<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="CallBackEdit.aspx.cs" Inherits="WebApplicationKartable.CallBackEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

        <asp:SqlDataSource ID="source_project" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, project_code FROM dbo.bas_project order by srl desc"/>
    <asp:SqlDataSource ID="source_provider" runat="server" ConnectionString="<%$ ConnectionStrings:FarsheBoom %>" SelectCommand="SELECT srl, provider_name FROM dbo.bas_provider order by provider_name"/>
       <asp:Panel ID="search_panel" runat="server" CssClass="panelbackcolor">
    
               <div class="row">
                   <asp:Label ID="lblError" runat="server" ForeColor="Yellow" BackColor="#003399"></asp:Label>
<asp:LinkButton ID="lnk_delete" CssClass="btn btn-primary" runat="server" Enabled="true" PostBackUrl="~/Project/CallPerform.aspx"> تخصیص به نمایشگاه</asp:LinkButton>
    </div>
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
                            <td>
                    <asp:ImageButton ID="ImageButton_delete" ToolTip="حذف" runat="server" ImageUrl="~/images/Controls/delete.png" OnClick="ImageButton_delete_Click" /></td>
        </tr>
    </table>
</asp:Panel>
        <br />
    <asp:GridView ID="gridview" runat="server" CssClass="textbox" AutoGenerateColumns="False" HeaderStyle-BackColor="#5D7B9D" HeaderStyle-ForeColor="White"  OnPageIndexChanging="gridview_PageIndexChanging" Width="900px">
        <Columns>
            <asp:BoundField DataField="igd_srl" HeaderText="کد" ItemStyle-Width="70" ReadOnly="True" />
            <asp:BoundField DataField="code_igd" HeaderText="کد فرش" ItemStyle-Width="100" ReadOnly="True" />
            <asp:BoundField DataField="brand_name" HeaderText="گونه" ItemStyle-Width="200" ReadOnly="True" />
            <asp:BoundField DataField="size_title" HeaderText="اندازه" ItemStyle-Width="100" ReadOnly="True" />
            <asp:BoundField DataField="color_name" HeaderText="رنگ" ItemStyle-Width="150" ReadOnly="True" />
        <asp:ImageField DataImageUrlField="title_igd" HeaderText="تصویر" ItemStyle-Width="300"></asp:ImageField> 
            <asp:TemplateField HeaderText="حذف">  
                    <ItemTemplate>  
                        <asp:CheckBox ID="chk_delete" runat="server" />  
                    </ItemTemplate>  
                </asp:TemplateField> 
            <asp:CommandField ShowSelectButton="true" HeaderText="انتخاب" SelectText="انتخاب" ItemStyle-Width="50" ControlStyle-ForeColor="Maroon" />
        </Columns>
        <AlternatingRowStyle BackColor="Azure" />
        <PagerStyle BackColor="Navy" ForeColor="White" HorizontalAlign="Center" />
    </asp:GridView>

</asp:Content>
